using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_Detail : System.Web.UI.Page
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
                    ButtonPrint2.OnClientClick = "return popitup('Invoice.aspx?id=" + Transaksi.IDTransaksi + "')";
                    ButtonPrint3.OnClientClick = "return popitup('PackingSlip.aspx?id=" + Transaksi.IDTransaksi + "')";

                    LabelIDTransaksi.Text = Transaksi.IDTransaksi;

                    LabelMeja.Text = Transaksi.TBMeja.Nama;
                    LabelPAX.Text = Pengaturan.FormatHarga(Transaksi.JumlahTamu);

                    //PENGGUNA
                    LabelPenggunaTransaksi.Text = Transaksi.TBPengguna.NamaLengkap;

                    if (Transaksi.IDPenggunaUpdate.HasValue)
                    {
                        LabelPenggunaUpdate.Text = Transaksi.TBPengguna2.NamaLengkap;
                        PanelPerubahanTerakhir1.Visible = true;
                    }
                    else
                    {
                        LabelPenggunaUpdate.Text = "";
                        PanelPerubahanTerakhir1.Visible = false;
                    }

                    LabelTempat.Text = Transaksi.TBTempat.Nama;

                    //PELANGGAN
                    LabelPelangganNama.Text = Transaksi.TBPelanggan.NamaLengkap;
                    PanelPelanggan2.Visible = Transaksi.IDPelanggan > 1;

                    if (PanelPelanggan2.Visible)
                    {
                        var Alamat = Transaksi.TBPelanggan.TBAlamats.FirstOrDefault();

                        LabelPelangganTelepon.Text = Alamat != null ? Alamat.Handphone : "";
                        LabelPelangganAlamat.Text = Alamat != null ? Alamat.AlamatLengkap : "";
                    }

                    //STATUS TRANSAKSI
                    if (Transaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                        LabelStatusTransaksi.CssClass = "label label-success";
                    else if (Transaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                        LabelStatusTransaksi.CssClass = "label label-danger";
                    else
                        LabelStatusTransaksi.CssClass = "label label-primary";

                    LabelStatusTransaksi.Text = Transaksi.TBStatusTransaksi.Nama;
                    LabelJenisTransaksi.Text = Transaksi.TBJenisTransaksi.Nama;

                    //KALKULASI TRANSAKSI
                    var SebelumDiscount = Transaksi.Subtotal + Transaksi.TotalPotonganHargaJualDetail;
                    var SetelahDiscount = Transaksi.Subtotal - Transaksi.PotonganTransaksi - Transaksi.TotalDiscountVoucher;

                    //TANGGAL
                    LabelTanggalOperasional.Text = Pengaturan.FormatTanggalHari(Transaksi.TanggalOperasional);
                    LabelTanggalTransaksi.Text = Pengaturan.FormatTanggalJam(Transaksi.TanggalTransaksi);
                    LabelTanggalUpdate.Text = Pengaturan.FormatTanggalJam(Transaksi.TanggalUpdate);

                    //BIAYA TAMBAHAN 1
                    LabelKeteranganBiayaTambahan1.Text = Transaksi.TBTempat.KeteranganBiayaTambahan1;
                    PanelBiayaTambahan11.Visible = Pengaturan.FormatHarga(LabelBiayaTambahan1, Transaksi.BiayaTambahan1);

                    //BIAYA TAMBAHAN 2
                    LabelKeteranganBiayaTambahan2.Text = Transaksi.TBTempat.KeteranganBiayaTambahan2;
                    PanelBiayaTambahan12.Visible = Pengaturan.FormatHarga(LabelBiayaTambahan2, Transaksi.BiayaTambahan2);

                    //BIAYA TAMBAHAN 3
                    LabelKeteranganBiayaTambahan3.Text = Transaksi.TBTempat.KeteranganBiayaTambahan3;
                    PanelBiayaTambahan13.Visible = Pengaturan.FormatHarga(LabelBiayaTambahan3, Transaksi.BiayaTambahan3);

                    //BIAYA TAMBAHAN 4
                    LabelKeteranganBiayaTambahan4.Text = Transaksi.TBTempat.KeteranganBiayaTambahan4;
                    PanelBiayaTambahan14.Visible = Pengaturan.FormatHarga(LabelBiayaTambahan4, Transaksi.BiayaTambahan4);

                    //DISCOUNT
                    var Discount = (Transaksi.TotalPotonganHargaJualDetail + Transaksi.PotonganTransaksi + Transaksi.TotalDiscountVoucher) * -1;
                    PanelDiscount.Visible = Pengaturan.FormatHarga(LabelDiscount, Discount);

                    //BIAYA PENGIRIMAN
                    PanelBiayaPengiriman1.Visible = Pengaturan.FormatHarga(LabelBiayaPengiriman, Transaksi.BiayaPengiriman);

                    //PEMBULATAN
                    PanelPembulatan1.Visible = Pengaturan.FormatHarga(LabelPembulatan, Transaksi.Pembulatan);

                    Pengaturan.FormatHarga(LabelSubtotal, SebelumDiscount);
                    Pengaturan.FormatHarga(LabelGrandTotal, Transaksi.GrandTotal);

                    //KETERANGAN
                    PanelKeterangan2.Visible = !string.IsNullOrWhiteSpace(Transaksi.Keterangan);

                    if (PanelKeterangan2.Visible)
                        LabelKeterangan.Text = Transaksi.Keterangan;

                    var TransaksiDetail = Transaksi.TBTransaksiDetails
                        .Select(item => new
                        {
                            JumlahProduk = item.Quantity,
                            HargaJual = item.HargaJual,
                            Subtotal = item.Subtotal,
                            Produk = item.TBKombinasiProduk.Nama,
                            TotalTanpaPotonganHargaJual = item.HargaJual * item.Quantity,
                            PotonganHargaJual = item.Discount,
                            TotalPotonganHargaJual = item.Discount * item.Quantity
                        }).ToArray();

                    RepeaterDetailTransaksi.DataSource = TransaksiDetail;
                    RepeaterDetailTransaksi.DataBind();

                    //PEMBAYARAN
                    var Pembayaran = Transaksi.TBTransaksiJenisPembayarans.ToArray();

                    TabelPembayaran.Visible = Pembayaran.Count() > 0;

                    if (TabelPembayaran.Visible)
                    {
                        RepeaterPembayaran.DataSource = Pembayaran;
                        RepeaterPembayaran.DataBind();

                        Pengaturan.FormatHarga(LabelTotalPembayaran, Pembayaran.Sum(item => item.Total));
                    }

                    Pengaturan.FormatHarga(LabelTotalQuantity1, Transaksi.JumlahProduk);

                    Pengaturan.FormatHarga(LabelDiscountSebelum, SebelumDiscount);

                    PanelDiscountDetailProduk.Visible = Pengaturan.FormatHarga(LabelDiscountProduk, Transaksi.TotalPotonganHargaJualDetail * -1);
                    PanelDiscountDetailTransaksi.Visible = Pengaturan.FormatHarga(LabelDiscountTransaksi, Transaksi.PotonganTransaksi * -1);
                    PanelDiscountDetailVoucher.Visible = Pengaturan.FormatHarga(LabelDiscountVoucher, Transaksi.TotalDiscountVoucher * -1);

                    PanelTotalDiscount.Visible = false; //Pengaturan.FormatHarga(LabelTotalDiscount, Discount);

                    PanelSetelahDiscount.Visible = !(SetelahDiscount == SebelumDiscount);
                    Pengaturan.FormatHarga(LabelDiscountSetelah, SetelahDiscount);

                    #region PRINT
                    LabelPrintStore.Text = Transaksi.TBTempat.TBStore.Nama;
                    LabelPrintTempatNama.Text = Transaksi.TBTempat.Nama;
                    LabelTempatAlamat.Text = Transaksi.TBTempat.Alamat;
                    LabelTempatTelepon.Text = Transaksi.TBTempat.Telepon1;

                    LabelPrintIDOrder.Text = Transaksi.IDTransaksi;
                    LabelPrintMeja.Text = Transaksi.TBMeja.Nama;
                    LabelPrintPengguna.Text = Transaksi.TBPengguna.NamaLengkap;
                    LabelPrintTanggal.Text = Pengaturan.FormatTanggal(Transaksi.TanggalTransaksi);

                    //JENIS PEMBAYARAN
                    if (Transaksi.TBTransaksiJenisPembayarans.Count > 0)
                    {
                        if (Transaksi.TBTransaksiJenisPembayarans.Count > 1)
                            LabelPrintJenisPembayaran.Text = "Multiple Payment"; //LEBIH DARI 1 PAYMENT
                        else
                            LabelPrintJenisPembayaran.Text = Transaksi.TBTransaksiJenisPembayarans.FirstOrDefault().TBJenisPembayaran.Nama;
                    }
                    else
                        LabelPrintJenisPembayaran.Text = "Awaiting Payment";

                    PanelPelanggan.Visible = Transaksi.TBPelanggan.IDPelanggan > 1;

                    if (PanelPelanggan.Visible)
                    {
                        LabelPrintPelangganNama.Text = Transaksi.TBPelanggan.NamaLengkap;
                        LabelPrintPelangganTelepon.Text = Transaksi.TBPelanggan.Handphone;
                        LabelPrintPelangganAlamat.Text = Transaksi.TBPelanggan.TBAlamats.Count > 0 ? Transaksi.TBPelanggan.TBAlamats.FirstOrDefault().AlamatLengkap : "";
                    }

                    RepeaterPrintTransaksiDetail.DataSource = TransaksiDetail;
                    RepeaterPrintTransaksiDetail.DataBind();

                    LabelPrintQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
                    LabelPrintSubtotal.Text = Pengaturan.FormatHarga(Transaksi.TBTransaksiDetails.Sum(item => item.TotalHargaJual));

                    LabelPrintDiscountTransaksi.Text = Pengaturan.FormatHarga(Transaksi.TotalPotonganHargaJualDetail);

                    PanelDiscountTransaksi.Visible = LabelPrintDiscountTransaksi.Text != "0";

                    PanelBiayaTambahan1.Visible = Transaksi.BiayaTambahan1 > 0;

                    if (PanelBiayaTambahan1.Visible)
                    {
                        LabelPrintKeteranganBiayaTambahan1.Text = Transaksi.TBTempat.KeteranganBiayaTambahan1;
                        LabelPrintBiayaTambahan1.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan1);
                    }

                    PanelBiayaTambahan2.Visible = Transaksi.BiayaTambahan2 > 0;

                    if (PanelBiayaTambahan2.Visible)
                    {
                        LabelPrintKeteranganBiayaTambahan2.Text = Transaksi.TBTempat.KeteranganBiayaTambahan2;
                        LabelPrintBiayaTambahan2.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan2);
                    }

                    LabelPrintBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);

                    PanelBiayaPengiriman.Visible = LabelPrintBiayaPengiriman.Text != "0";


                    LabelPrintPembulatan.Text = Pengaturan.FormatHarga(Transaksi.Pembulatan);

                    PanelPembulatan.Visible = LabelPrintPembulatan.Text != "0";

                    LabelPrintGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);

                    PanelPembayaran.Visible = false;
                    //LabelPrintPembayaran

                    PanelKembalian.Visible = false;
                    //LabelPrintKembalian

                    PanelJenisPembayaran.Visible = Transaksi.TBTransaksiJenisPembayarans.Count > 0;
                    PanelJenisPembayaran1.Visible = PanelJenisPembayaran.Visible;

                    if (PanelJenisPembayaran.Visible)
                    {
                        RepeaterPrintJenisPembayaran.DataSource = Transaksi.TBTransaksiJenisPembayarans.ToArray();
                        RepeaterPrintJenisPembayaran.DataBind();
                    }

                    PanelKeterangan.Visible = !string.IsNullOrWhiteSpace(Transaksi.Keterangan);
                    PanelKeterangan1.Visible = PanelKeterangan.Visible;

                    LabelPrintKeterangan.Text = Transaksi.Keterangan;

                    if (!string.IsNullOrWhiteSpace(Transaksi.TBTempat.FooterPrint))
                    {
                        PanelFooter.Visible = true;
                        PanelFooter1.Visible = true;
                        LabelPrintFooter.Text = Transaksi.TBTempat.FooterPrint;
                    }
                    else
                    {
                        PanelFooter.Visible = false;
                        PanelFooter1.Visible = false;
                    }
                    #endregion
                }
                else
                    Response.Redirect("Transaksi.aspx");
            }
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Request.QueryString["returnUrl"]))
            Response.Redirect(Request.QueryString["returnUrl"]);
        else
            Response.Redirect("Transaksi.aspx");
    }
}