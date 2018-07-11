using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_NewInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Transaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == Request.QueryString["id"]);

                if (Transaksi != null)
                {
                    #region TRANSAKSI PRINT LOG
                    TransaksiPrintLog_Class TransaksiPrintLog_Class = new TransaksiPrintLog_Class(db);
                    TransaksiPrintLog_Class.Tambah(EnumPrintLog.Invoice, Transaksi.IDTransaksi);
                    db.SubmitChanges();
                    #endregion

                    var DetailPembayaran = db.TBTransaksiJenisPembayarans.Where(item => item.IDTransaksi == Request.QueryString["id"]).Select(item => new
                    {
                        Nama = item.TBJenisPembayaran.Nama,
                        Total = item.Bayar,

                    });

                    RepeaterPembayaran.DataSource = DetailPembayaran;
                    RepeaterPembayaran.DataBind();


                    var Store = Transaksi.TBTempat.TBStore;
                    var Tempat = Transaksi.TBTempat;

                    LabelFooterPrint.Text = Tempat.FooterPrint;

                    LabelNamaStore.Text = Store.Nama + " - " + Tempat.Nama;
                    LabelAlamatStore.Text = Tempat.Alamat;
                    LabelTeleponStore.Text = Tempat.Telepon1;
                    LabelWebsite.Text = Store.Website;
                    HyperLinkEmail.Text = Tempat.Email;
                    HyperLinkEmail.NavigateUrl = Tempat.Email;

                    LabelIDTransaksi.Text = Transaksi.IDTransaksi;
                    LabelTanggalTransaksi.Text = Pengaturan.FormatTanggal(Transaksi.TanggalTransaksi);
                    LabelNamaPelanggan.Text = Transaksi.TBPelanggan.NamaLengkap;

                    if (Transaksi.IDPelanggan != 1)
                    {
                        var Alamat = Transaksi.TBAlamat;

                        LabelAlamatPelanggan.Text = Alamat.AlamatLengkap;
                        LabelTeleponPelanggan.Text = Alamat.Handphone;
                    }

                    LabelJenisPembayaran.Text = Transaksi.TBJenisPembayaran.Nama;
                    LabelStatusTransaksi.Text = Transaksi.TBStatusTransaksi.Nama;

                    RepeaterDetailTransaksi.DataSource = Transaksi.TBTransaksiDetails
                        .Select(item => new
                        {
                            Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
                            Nama = item.TBKombinasiProduk.Nama,
                            Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            JumlahProduk = item.Quantity,
                            HargaJual = item.HargaJual,
                            PotonganHargaJual = item.Discount,
                            Persentase = item.HargaJual > 0 ? item.Discount / item.HargaJual * 100 : 0,
                            item.Subtotal
                        })
                        .OrderBy(item => item.Nama)
                        .ToArray();
                    RepeaterDetailTransaksi.DataBind();

                    LabelTotalQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
                    LabelSubtotal.Text = Pengaturan.FormatHarga(Transaksi.Subtotal);

                    //Discount
                    LabelSebelumDiscount.Text = Pengaturan.FormatHarga(Transaksi.Subtotal + Transaksi.TotalDiscountVoucher + Transaksi.PotonganTransaksi + Transaksi.TotalPotonganHargaJualDetail);
                    LabelDiscountProduk.Text = Pengaturan.FormatHarga(Transaksi.TotalPotonganHargaJualDetail);

                    LabelDiscountTransaksi.Text = Pengaturan.FormatHarga(Transaksi.PotonganTransaksi);

                    if (LabelDiscountTransaksi.Text == "0")
                    {
                        panelDiscountTransaksi.Attributes.Add("style", "display: none;");
                        LabelDiscountTransaksi.Attributes.Add("style", "display: none;");
                    }
                    else
                    {
                        panelDiscountTransaksi.Attributes.Add("style", "");
                        LabelDiscountTransaksi.Attributes.Add("style", "");
                    }

                    //Charge
                    if (Transaksi.IDJenisBebanBiaya == (int)PilihanJenisBebanBiaya.BebanCustomer)
                        LabelCharge.Text = Pengaturan.FormatHarga(Transaksi.BiayaJenisPembayaran);
                    else
                        LabelCharge.Text = "0";

                    if (LabelCharge.Text == "0")
                    {
                        panelCharge.Attributes.Add("style", "display: none;");
                        LabelCharge.Attributes.Add("style", "display: none;");
                    }
                    else
                    {
                        panelCharge.Attributes.Add("style", "");
                        LabelCharge.Attributes.Add("style", "");
                    }

                    //Biaya Tambahan
                    LabelKeteranganBiayaTambahan.Text = Tempat.KeteranganBiayaTambahan1;
                    LabelBiayaTambahan.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan1);

                    if (LabelBiayaTambahan.Text == "0")
                    {
                        LabelKeteranganBiayaTambahan.Attributes.Add("style", "display: none;");
                        LabelBiayaTambahan.Attributes.Add("style", "display: none;");
                    }
                    else
                    {
                        LabelKeteranganBiayaTambahan.Attributes.Add("style", "");
                        LabelBiayaTambahan.Attributes.Add("style", "");
                    }

                    LabelBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);

                    if (LabelBiayaPengiriman.Text == "0")
                    {
                        panelBiayaPengiriman.Attributes.Add("style", "display: none;");
                        LabelBiayaPengiriman.Attributes.Add("style", "display: none;");
                    }
                    else
                    {
                        panelBiayaPengiriman.Attributes.Add("style", "");
                        LabelBiayaPengiriman.Attributes.Add("style", "");
                    }

                    LabelPembulatan.Text = Pengaturan.FormatHarga(Transaksi.Pembulatan);

                    if (LabelPembulatan.Text == "0")
                    {
                        panelPembulatan.Attributes.Add("style", "display: none;");
                        LabelPembulatan.Attributes.Add("style", "display: none;");
                    }
                    else
                    {
                        panelPembulatan.Attributes.Add("style", "");
                        LabelPembulatan.Attributes.Add("style", "");
                    }

                    LabelGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);
                    LabelKeterangan.Text = Transaksi.Keterangan;

                    LabelNamaPengirim.Text = Transaksi.TBPengguna.NamaLengkap;
                    LabelNamaPenerima.Text = Transaksi.TBPelanggan.NamaLengkap;

                    LabelTotalBayar.Text = Pengaturan.FormatHarga(DetailPembayaran.Sum(item => item.Total));
                    LabelSisaBayar.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal - (DetailPembayaran.Sum(item => item.Total) == null ? 0 : DetailPembayaran.Sum(item => item.Total)));

                }
                else
                    Response.Redirect("Transaksi.aspx");
            }
        }
    }
}