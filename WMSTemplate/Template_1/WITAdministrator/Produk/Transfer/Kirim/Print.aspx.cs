using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Kirim_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBTransferProduk DataTransferProduk = db.TBTransferProduks
                    .FirstOrDefault(item => item.IDTransferProduk == Request.QueryString["id"]);

                if (DataTransferProduk != null)
                {
                    LabelIDTransferProduk.Text = DataTransferProduk.IDTransferProduk;

                    //PRINT
                    LabelPrintPengguna.Text = Pengguna.NamaLengkap;
                    LabelPrintTempat.Text = Pengguna.Tempat;
                    LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();

                    //PENGIRIM
                    LabelPengirimTempat.Text = Pengguna.Store + " - " + DataTransferProduk.TBTempat.Nama;
                    LabelPengirimTempat1.Text = LabelPengirimTempat.Text;
                    LabelPengirimanTanggal.Text = DataTransferProduk.TanggalKirim.ToFormatTanggal();
                    LabelPengirimanPengguna.Text = DataTransferProduk.TBPengguna.NamaLengkap;
                    LabelPengirimanPengguna1.Text = LabelPengirimanPengguna.Text;
                    LabelPengirimAlamat.Text = DataTransferProduk.TBTempat.Alamat;
                    LabelPengirimTelepon.Text = DataTransferProduk.TBTempat.Telepon1 + " - " + DataTransferProduk.TBTempat.Telepon2;
                    LabelPengirimEmail.Text = DataTransferProduk.TBTempat.Email;

                    LabelKeterangan.Text = DataTransferProduk.Keterangan;

                    //PENERIMA
                    LabelPenerimaTempat.Text = Pengguna.Store + " - " + DataTransferProduk.TBTempat1.Nama;
                    LabelPenerimaTanggal.Text = DataTransferProduk.TanggalTerima.ToFormatTanggal();
                    LabelPenerimaPengguna.Text = DataTransferProduk.IDPenerima.HasValue ? DataTransferProduk.TBPengguna1.NamaLengkap : "";
                    LabelPenerimaPengguna1.Text = LabelPenerimaPengguna.Text;
                    LabelPenerimaAlamat.Text = DataTransferProduk.TBTempat1.Alamat;
                    LabelPenerimaTelepon.Text = DataTransferProduk.TBTempat1.Telepon1 + " - " + DataTransferProduk.TBTempat1.Telepon2;
                    LabelPenerimaEmail.Text = DataTransferProduk.TBTempat1.Email;

                    RepeaterTransferKombinasiProduk.DataSource = DataTransferProduk.TBTransferProdukDetails
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

                    LabelTotalQuantity.Text = DataTransferProduk.TotalJumlah.ToFormatHargaBulat();
                    LabelGrandTotal.Text = DataTransferProduk.GrandTotalHargaJual.ToFormatHarga();

                    LabelTotalQuantity1.Text = LabelTotalQuantity.Text;
                    LabelGrandTotal1.Text = LabelGrandTotal.Text;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}