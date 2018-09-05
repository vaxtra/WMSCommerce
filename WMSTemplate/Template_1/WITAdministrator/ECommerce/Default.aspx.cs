using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;

public partial class WITAdministrator_ECommerce_Default : System.Web.UI.Page
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


                JenisPembayaran_Class JenisPembayaran_Class = new JenisPembayaran_Class(db);
                DropDownListJenisPembayaran.DataSource = JenisPembayaran_Class.Data();
                DropDownListJenisPembayaran.DataTextField = "Nama";
                DropDownListJenisPembayaran.DataValueField = "IDJenisPembayaran";
                DropDownListJenisPembayaran.DataBind();
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

    #region TRANSAKSI
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }


    protected void CheckBoxPilihSemua_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in RepeaterTransaksi.Items)
        {
            CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

            CheckBoxPilih.Checked = CheckBoxPilihSemua.Checked;
        }
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
                ClassGrandtotal = item.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled ? "text-right text-danger fitSize font-weight-bold text-line-through" : "text-right fitSize font-weight-bold",
                item.IDTransaksi,
                TanggalOperasional = item.TanggalOperasional.ToFormatDateMedium(),
                Pelanggan = item.TBPelanggan.NamaLengkap,
                StatusTransaksi = Manage.HTMLStatusTransaksi(item.IDStatusTransaksi.Value),
                StatusPengiriman = string.IsNullOrEmpty(item.KodePengiriman) ? Manage.HTMLBagde(EnumColor.Danger, "Belum") : Manage.HTMLBagde(EnumColor.Success, "Sudah"),
                GrandTotal = item.GrandTotal.ToFormatHarga(),
            });
            RepeaterTransaksi.DataBind();

            LabelIDTransaksi.Text = string.Empty;
        }
    }

    protected void RepeaterTransaksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MultiViewTransaksi.SetActiveView(ViewDetail);
        ButtonCetakInvoice.Visible = true;
        ButtonCetakPackingSlip.Visible = true;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Transaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == e.CommandArgument.ToString());

            if (Transaksi != null)
            {
                ButtonCetakInvoice.OnClientClick = "return popitup('/WITPointOfSales/Invoice.aspx?id=" + Transaksi.IDTransaksi + "')";
                ButtonCetakPackingSlip.OnClientClick = "return popitup('/WITPointOfSales/PackingSlip.aspx?id=" + Transaksi.IDTransaksi + "')";

                LabelIDTransaksi.Text = Transaksi.IDTransaksi;
                LabelTempat.Text = Transaksi.TBTempat.Nama;

                //PENGGUNA
                LabelPenggunaTransaksi.Text = Transaksi.TBPengguna.NamaLengkap;
                LabelPenggunaUpdate.Text = Transaksi.IDPenggunaUpdate != null ? Transaksi.TBPengguna2.NamaLengkap : " ";
                LabelPenggunaBatal.Text = Transaksi.IDPenggunaBatal != null ? Transaksi.TBPengguna4.NamaLengkap : " ";

                //PELANGGAN
                LabelPelangganNama.Text = Transaksi.TBPelanggan.NamaLengkap;
                var Alamat = Transaksi.TBPelanggan.TBAlamats.FirstOrDefault();

                LabelPelangganTelepon.Text = Alamat != null ? Alamat.Handphone : "";
                LabelPelangganAlamat.Text = Alamat != null ? Alamat.AlamatLengkap : "";

                //STATUS TRANSAKSI
                LabelStatusTransaksi.Text = Manage.HTMLStatusTransaksi(Transaksi.IDStatusTransaksi.Value);

                //KALKULASI TRANSAKSI
                var SebelumDiscount = Transaksi.Subtotal + Transaksi.TotalPotonganHargaJualDetail;
                var SetelahDiscount = Transaksi.Subtotal - Transaksi.PotonganTransaksi - Transaksi.TotalDiscountVoucher;

                //TANGGAL
                LabelTanggalOperasional.Text = Pengaturan.FormatTanggalHari(Transaksi.TanggalOperasional);
                LabelTanggalTransaksi.Text = Pengaturan.FormatTanggalJam(Transaksi.TanggalTransaksi);
                LabelTanggalUpdate.Text = Pengaturan.FormatTanggalJam(Transaksi.TanggalUpdate);

                //BIAYA PENGIRIMAN
                Pengaturan.FormatHarga(LabelBiayaPengiriman, Transaksi.BiayaPengiriman);

                //PEMBULATAN
                Pengaturan.FormatHarga(LabelPembulatan, Transaksi.Pembulatan);

                Pengaturan.FormatHarga(LabelSubtotal, SebelumDiscount);
                Pengaturan.FormatHarga(LabelGrandTotal, Transaksi.GrandTotal);

                //KETERANGAN
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

                RepeaterPembayaran.DataSource = Pembayaran;
                RepeaterPembayaran.DataBind();

                Pengaturan.FormatHarga(LabelTotalPembayaran, Pembayaran.Sum(item => item.Total));
                Pengaturan.FormatHarga(LabelTotalQuantity1, Transaksi.JumlahProduk);

                Pengaturan.FormatHarga(LabelDiscount, Transaksi.TotalPotonganHargaJualDetail * -1);
            }
            else
                Response.Redirect("Transaksi.aspx");
        }
    }
    protected void ButtonSemuaCanceled_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            foreach (RepeaterItem item in RepeaterTransaksi.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");
                Label LabelID = (Label)item.FindControl("LabelID");

                if (CheckBoxPilih.Checked == true)
                {
                    Transaksi_Class Transaksi = new Transaksi_Class(LabelID.Text, PenggunaLogin.IDPengguna);
                    Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;
                    Transaksi.ConfirmTransaksi(db, " ", true);
                }
            }
            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);
        ButtonCetakInvoice.Visible = false;
        ButtonCetakPackingSlip.Visible = false;

        LoadData();
    }
    #endregion

    #region DETAIL
    protected void ButtonCanceled_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            Transaksi_Class Transaksi = new Transaksi_Class(LabelIDTransaksi.Text, PenggunaLogin.IDPengguna);
            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;
            Transaksi.ConfirmTransaksi(db, " ", true);
            db.SubmitChanges();

            //KIRIM EMAIL KE CUSTOMER
            //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/frontend/assets/email-template/awaiting-payment.html")))
            //{

            //    string body = "";
            //    string listProduk = "";
            //    body = reader.ReadToEnd();
            //    body = body.Replace("{nama_customer}", Transaksi.Pelanggan.Nama);
            //    body = body.Replace("{nomor_order}", Transaksi.IDTransaksi);
            //    body = body.Replace("{list_produk}", listProduk);
            //    body = body.Replace("{subtotal}", Transaksi.Subtotal.ToFormatHarga());
            //    body = body.Replace("{biaya_pengiriman}", Transaksi.BiayaPengiriman.ToString().ToFormatHarga());
            //    body = body.Replace("{grand_total}", Transaksi.GrandTotal.ToFormatHarga());
            //    body = body.Replace("{nama_toko}", "Trendsetter");
            //    body = body.Replace("{logo_email}", "http://ecommerce.wit.co.id/assets/images/email_logo/email_logo.png");
            //    body = body.Replace("{url_konfirmasi}", "http://wit.co.id");
            //    body = body.Replace("{url_website}", "http://localhost:54517/");
            //    SendEmail(Transaksi.Pelanggan, "Trendsetter", "Order Notification", body);
            //}
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);
        ButtonCetakInvoice.Visible = false;
        ButtonCetakPackingSlip.Visible = false;

        LoadData();
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        MultiViewTransaksi.SetActiveView(ViewTransaksi);

        ButtonCetakInvoice.Visible = false;
        ButtonCetakPackingSlip.Visible = false;

        LabelIDTransaksi.Text = string.Empty;
    }
    #endregion

    protected void ButtonComplete_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            if (!string.IsNullOrEmpty(LabelIDTransaksi.Text))
            {
                Transaksi_Class Transaksi = new Transaksi_Class(LabelIDTransaksi.Text, PenggunaLogin.IDPengguna);
                Transaksi.TambahPembayaran(DateTime.Now, PenggunaLogin.IDPengguna, DropDownListJenisPembayaran.SelectedValue.ToInt(), Transaksi.GrandTotal, string.Empty);
                Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
                Transaksi.ConfirmTransaksi(db, " ", false);
            }
            else
            {
                foreach (RepeaterItem item in RepeaterTransaksi.Items)
                {
                    CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");
                    Label LabelID = (Label)item.FindControl("LabelID");

                    if (CheckBoxPilih.Checked == true)
                    {
                        Transaksi_Class Transaksi = new Transaksi_Class(LabelID.Text, PenggunaLogin.IDPengguna);
                        Transaksi.TambahPembayaran(DateTime.Now, PenggunaLogin.IDPengguna, DropDownListJenisPembayaran.SelectedValue.ToInt(), Transaksi.GrandTotal, string.Empty);
                        Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
                        Transaksi.ConfirmTransaksi(db, " ", false);
                    }
                }
            }

            db.SubmitChanges();
        }

        MultiViewTransaksi.SetActiveView(ViewTransaksi);
        ButtonCetakInvoice.Visible = false;
        ButtonCetakPackingSlip.Visible = false;

        LoadData();
    }

    protected void SendEmail(string email, string display_name, string subject, string body)
    {
        string sender = "ecommerce@wit.co.id";
        string passEmail = "empatTH3010*#";
        string smtpHost = "mail.wit.co.id";
        int smtpPort = 25;
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(sender, display_name);
        msg.To.Add(new MailAddress(email));
        msg.Subject = subject;
        msg.Body = body;
        msg.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
        smtp.Credentials = new NetworkCredential(sender, passEmail);
        smtp.Send(msg);
    }
}