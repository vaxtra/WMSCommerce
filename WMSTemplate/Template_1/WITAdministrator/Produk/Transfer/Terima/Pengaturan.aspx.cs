using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Terima_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TransferProduk_Class TransferProduk_Class = new TransferProduk_Class();

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                var TransferProduk = TransferProduk_Class.Cari(db, Request.QueryString["id"]);

                if (TransferProduk != null && TransferProduk.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses)
                {
                    LabelIDTransferProduk.Text = TransferProduk.IDTransferProduk;

                    //TEMPAT PENGIRIM
                    DropDownListTempatPengirim.Items.Insert(0, new ListItem
                    {
                        Text = TransferProduk.TBTempat.Nama,
                        Selected = true
                    });

                    //TEMPAT PENERIMA
                    DropDownListTempatPenerima.Items.Insert(0, new ListItem
                    {
                        Text = TransferProduk.TBTempat1.Nama,
                        Selected = true
                    });

                    //PENGGUNA PENGIRIM
                    DropDownListPenggunaPengirim.Items.Insert(0, new ListItem
                    {
                        Text = TransferProduk.TBPengguna.NamaLengkap,
                        Selected = true
                    });

                    TextBoxTanggalKirim.Text = TransferProduk.TanggalKirim.ToFormatTanggal();
                    TextBoxKeterangan.Text = TransferProduk.Keterangan;

                    RepeaterTransferKombinasiProduk.DataSource = TransferProduk.TBTransferProdukDetails
                        .GroupBy(item => item.TBKombinasiProduk.TBProduk)
                        .Select(item => new
                        {
                            Produk = item.Key.Nama,
                            Kategori = item.Key.TBRelasiProdukKategoriProduks.Count > 0 ? item.Key.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            Count = item.Count(),
                            Body = item.Select(item2 => new
                            {
                                item2.IDTransferProdukDetail,
                                Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                                AtributProduk = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                                item2.HargaJual,
                                item2.Jumlah,
                                item2.SubtotalHargaJual
                            }),
                            Jumlah = item.Sum(item2 => item2.Jumlah),
                            SubtotalHargaJual = item.Sum(item2 => item2.SubtotalHargaJual)
                        })
                        .ToArray();
                    RepeaterTransferKombinasiProduk.DataBind();

                    LabelTotalJumlah.Text = TransferProduk.TotalJumlah.ToFormatHargaBulat();
                    LabelTotalNominal.Text = TransferProduk.GrandTotalHargaJual.ToFormatHarga();
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }

    protected void ButtonTerima_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TransferProduk_Class TransferProduk_Class = new TransferProduk_Class();

                var TransferProduk = TransferProduk_Class.Cari(db, Request.QueryString["id"]);

                if (TransferProduk != null && TransferProduk.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses)
                {
                    StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                    foreach (var item in TransferProduk.TBTransferProdukDetails.ToArray())
                    {
                        var StokProduk = StokProduk_Class.Cari(Pengguna.IDTempat, item.IDKombinasiProduk);

                        if (StokProduk == null)
                            StokProduk = StokProduk_Class.MembuatStok(0, Pengguna.IDTempat, Pengguna.IDPengguna, item.IDKombinasiProduk, item.HargaBeli, item.HargaJual, "");

                        StokProduk_Class.BertambahBerkurang(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, item.Jumlah, item.HargaBeli, item.HargaJual, EnumJenisPerpindahanStok.TransferStokMasuk, "Transfer #" + TransferProduk.IDTransferProduk);
                    }

                    TransferProduk.IDPenerima = Pengguna.IDPengguna;
                    TransferProduk.TanggalTerima = DateTime.Now;
                    TransferProduk.TanggalUpdate = DateTime.Now;
                    TransferProduk.EnumJenisTransfer = (int)PilihanJenisTransfer.TransferSelesai;

                    db.SubmitChanges();

                    Response.Redirect("/WITWarehouse/Produk/Transfer/Detail.aspx?id=" + TransferProduk.IDTransferProduk, false);
                }
                else
                    Response.Redirect("Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError_Class = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
}