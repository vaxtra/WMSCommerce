using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Transaksi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiViewTransaksi.SetActiveView(ViewTransaksi);
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxTanggalAwal.Text = Pengaturan.HariIni()[0].ToFormatDateMedium();
                TextBoxTanggalAkhir.Text = Pengaturan.HariIni()[1].ToFormatDateMedium();
            }

            LoadData();
        }
    }

    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.HariIni()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.HariIni()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.MingguIni()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.MingguIni()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.BulanIni()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.BulanIni()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.TahunIni()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.TahunIni()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.HariSebelumnya()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.HariSebelumnya()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.MingguSebelumnya()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.MingguSebelumnya()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.BulanSebelumnya()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.BulanSebelumnya()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        TextBoxTanggalAwal.Text = Pengaturan.TahunSebelumnya()[0].ToFormatDateMedium();
        TextBoxTanggalAkhir.Text = Pengaturan.TahunSebelumnya()[1].ToFormatDateMedium();
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    #endregion

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            ;
            List<int> ListIDStatusTransaksi = new List<int>();

            foreach (ListItem item in CheckBoxListStatusTransaksi.Items)
            {
                if (item.Selected)
                    ListIDStatusTransaksi.Add(item.Value.ToInt());
            }

            RepeaterTransaksi.DataSource = db.TBTransaksis.AsEnumerable().Where(item =>
            (!string.IsNullOrEmpty(TextBoxCari.Text) ? item.IDTransaksi.Contains(TextBoxCari.Text) : true) &&
            (ListIDStatusTransaksi.Any(item2 => item2 == item.IDStatusTransaksi)) &&
            item.TanggalOperasional >= TextBoxTanggalAwal.Text.ToDateTime() && item.TanggalOperasional <= TextBoxTanggalAkhir.Text.ToDateTime()).Select(item => new
            {
                item.IDTransaksi,
                TanggalOperasional = item.TanggalOperasional.ToFormatDateMedium(),
                JenisTransaksi = item.TBJenisTransaksi.Nama,
                StatusTransaksi = Manage.HTMLStatusTransaksi(item.IDStatusTransaksi.Value),
                Pelanggan = item.TBPelanggan.NamaLengkap,
                JumlahProduk = item.JumlahProduk.ToFormatHargaBulat(),
                SubtotalSebelumDiscount = (item.Subtotal + item.TotalPotonganHargaJualDetail).ToFormatHarga(),
                TotalPotonganHargaJualDetail = item.TotalPotonganHargaJualDetail.ToFormatHarga(),
                GrandTotal = item.GrandTotal.ToFormatHarga(),
                item.Keterangan
            });
            RepeaterTransaksi.DataBind();
        }
    }

    protected void RepeaterTransaksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MultiViewTransaksi.SetActiveView(ViewDetail);

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Transaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == e.CommandArgument.ToString());

            if (Transaksi != null)
            {
                ButtonPrint2.OnClientClick = "return popitup('/WITPointOfSales/Invoice.aspx?id=" + Transaksi.IDTransaksi + "')";
                ButtonPrint3.OnClientClick = "return popitup('/WITPointOfSales/PackingSlip.aspx?id=" + Transaksi.IDTransaksi + "')";

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
                LabelStatusTransaksi.Text = Manage.HTMLStatusTransaksi(Transaksi.IDStatusTransaksi.Value);
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
            }
            else
                Response.Redirect("Transaksi.aspx");
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        MultiViewTransaksi.SetActiveView(ViewTransaksi);
    }

    protected void ButtonStatusPendingShippingCost_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBTransaksis.FirstOrDefault(item2 => item2.IDTransaksi == LabelIDTransaksi.Text).IDStatusTransaksi = (int)EnumStatusTransaksi.PendingShippingCost;
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        LoadData();
    }

    protected void ButtonAwaitingPayment_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBTransaksis.FirstOrDefault(item2 => item2.IDTransaksi == LabelIDTransaksi.Text).IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        LoadData();
    }

    protected void ButtonAwaitingPaymentVerification_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBTransaksis.FirstOrDefault(item2 => item2.IDTransaksi == LabelIDTransaksi.Text).IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPaymentVerification;
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        LoadData();
    }

    protected void ButtonPaymentVerified_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBTransaksis.FirstOrDefault(item2 => item2.IDTransaksi == LabelIDTransaksi.Text).IDStatusTransaksi = (int)EnumStatusTransaksi.PaymentVerified;
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        LoadData();
    }

    protected void ButtonComplete_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBTransaksis.FirstOrDefault(item2 => item2.IDTransaksi == LabelIDTransaksi.Text).IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        LoadData();
    }

    protected void ButtonCanceled_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBTransaksis.FirstOrDefault(item2 => item2.IDTransaksi == LabelIDTransaksi.Text).IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        LoadData();
    }
}