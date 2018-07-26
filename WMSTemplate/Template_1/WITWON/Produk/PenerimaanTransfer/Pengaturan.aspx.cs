using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITWON_Produk_PenerimaanTransfer_Pengaturan : System.Web.UI.Page
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

                    //PENGGUNA PENERIMA
                    DropDownListPenggunaPenerima.Items.Insert(0, new ListItem
                    {
                        Text = Pengguna.NamaLengkap,
                        Selected = true
                    });

                    TextBoxTanggalKirim.Text = TransferProduk.TanggalKirim.ToFormatTanggal();
                    TextBoxTanggalTerima.Text = DateTime.Now.ToString("d MMMM yyyy");
                    TextBoxKeterangan.Text = TransferProduk.Keterangan;

                    var TransferProdukDetail = TransferProduk.TBTransferProdukDetails.ToArray();

                    var ListTransferStokProdukDetail = TransferProdukDetail
                        .GroupBy(item => new
                        {
                            IDProduk = item.TBKombinasiProduk.TBProduk.IDProduk,
                            Produk = item.TBKombinasiProduk.TBProduk.Nama,
                            Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                            Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                        })
                        .Select(item => new
                        {
                            item.Key,
                            Detail = TransferProdukDetail
                                .Where(item2 => item2.TBKombinasiProduk.IDProduk == item.Key.IDProduk)
                                .Select(item2 => new StokProduk_Model
                                {
                                    IDProduk = item2.TBKombinasiProduk.TBProduk.IDProduk,
                                    Produk = item2.TBKombinasiProduk.TBProduk.Nama,
                                    IDKombinasiProduk = item2.IDKombinasiProduk,
                                    Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                                    IDAtribut = item2.TBKombinasiProduk.IDAtributProduk,
                                    Atribut = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                                    HargaJual = item2.HargaJual,
                                    Jumlah = item2.Jumlah
                                })
                                .OrderBy(item2 => item2.IDAtribut)
                        })
                        .OrderBy(item => item.Key.Produk);

                    LiteralLaporan.Text = string.Empty;
                    int index = 1;
                    int indexVarian = 1;

                    foreach (var item in ListTransferStokProdukDetail)
                    {
                        indexVarian = 1;

                        LiteralLaporan.Text += "<tr>";

                        string rowspan = item.Detail.Count() >= 2 ? "rowspan='" + item.Detail.Count() + "'" : "";

                        LiteralLaporan.Text += "<td " + rowspan + ">" + index++ + "</td>";
                        LiteralLaporan.Text += "<td " + rowspan + ">" + item.Key.PemilikProduk + "</td>";
                        LiteralLaporan.Text += "<td " + rowspan + ">" + item.Key.Kategori + "</td>";

                        foreach (var item2 in item.Detail)
                        {
                            if (indexVarian > 1)
                                LiteralLaporan.Text += "<tr>";

                            LiteralLaporan.Text += "<td>" + item2.Atribut + "</td>";
                            LiteralLaporan.Text += "<td class='text-right'>" + item2.HargaJual.ToFormatHarga() + "</td>";
                            LiteralLaporan.Text += "<td class='text-right'>" + item2.Jumlah.ToFormatHargaBulat() + "</td>";
                            LiteralLaporan.Text += "<td class='text-right warning bold'>" + item2.Subtotal.ToFormatHarga() + "</td>";

                            LiteralLaporan.Text += "</tr>";

                            indexVarian++;
                        }
                    }

                    LabelTotalJumlah.Text = TransferProduk.TotalJumlah.ToFormatHargaBulat();
                    LabelTotalNominal.Text = TransferProduk.GrandTotalHargaJual.ToFormatHarga();

                    LabelTotalJumlah1.Text = LabelTotalJumlah.Text;
                    LabelTotalNominal1.Text = LabelTotalNominal.Text;
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

                    Response.Redirect("Detail.aspx?id=" + TransferProduk.IDTransferProduk, false);
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