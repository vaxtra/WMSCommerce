using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_NetRevenue_Pembayaran : System.Web.UI.Page
{
    public Dictionary<string, dynamic> Result { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();
                StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
                JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);

                ClassTempat.DataListBox(ListBoxTempat);
                ClassJenisTransaksi.DataListBox(db, ListBoxJenisTransaksi);
                StatusTransaksi_Class.DataListBox(db, ListBoxStatusTransaksi);
                ClassJenisPembayaran.DataListBox(ListBoxJenisPembayaran);

                ListBoxStatusTransaksi.SelectedValue = "2";
                //ListBoxJenisTransaksi.Items.Add(new ListItem("- Semua Jenis Transaksi -", "0"));


                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy HH:mm");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy HH:mm");
            }
        }
        //else
        //    LinkDownload.Visible = false;
    }

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy HH:mm");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy HH:mm");

        //KONFIGURASI BUTTON SUBMIT
        if (ListBoxStatusTransaksi.SelectedItem.Value == "5")
            ButtonApprove.Enabled = false;
        else
            ButtonApprove.Enabled = true;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            List<int> ListIDTempat = new List<int>();

            foreach (ListItem item in ListBoxTempat.Items)
            {
                if (item.Selected)
                    ListIDTempat.Add(item.Value.ToInt());
            }

            List<int> ListIDJenisTransaksi = new List<int>();

            foreach (ListItem item in ListBoxJenisTransaksi.Items)
            {
                if (item.Selected)
                    ListIDJenisTransaksi.Add(item.Value.ToInt());
            }

            List<int> ListIDStatusTransaksi = new List<int>();

            foreach (ListItem item in ListBoxStatusTransaksi.Items)
            {
                if (item.Selected)
                    ListIDStatusTransaksi.Add(item.Value.ToInt());
            }

            List<int> ListIDJenisPembayaran = new List<int>();

            foreach (ListItem item in ListBoxJenisPembayaran.Items)
            {
                if (item.Selected)
                    ListIDJenisPembayaran.Add(item.Value.ToInt());
            }

            Result = Laporan_Class.NetRevenuePembayaranDressSofia(ListIDTempat, ListIDJenisTransaksi, ListIDStatusTransaksi, ListIDJenisPembayaran,
                TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime(),
                ListBoxStatusTransaksi.SelectedItem.Value);

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();

            //FILE EXCEL
            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "return popitup('PembayaranPrint.aspx" + Laporan_Class.TempPencarian + "')";

            foreach (RepeaterItem item in RepeaterLaporan.Items)
            {
                CheckBox CheckBoxIDTransaksi = (CheckBox)item.FindControl("CheckBoxIDTransaksi");
                CheckBoxIDTransaksi.Checked = true;
            }
        }
    }

    protected void ButtonApprove_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LiteralWarning.Text = "";
                foreach (RepeaterItem item in RepeaterLaporan.Items)
                {
                    CheckBox CheckBoxIDTransaksi = (CheckBox)item.FindControl("CheckBoxIDTransaksi");
                    if (CheckBoxIDTransaksi.Checked == true)
                    {
                        HiddenField HiddenFieldIDTransaksi = (HiddenField)item.FindControl("HiddenFieldIDTransaksi");

                        Transaksi_Model Transaksi = new Transaksi_Model(HiddenFieldIDTransaksi.Value, Pengguna.IDPengguna);

                        if (Transaksi.GrandTotal == Transaksi.TotalPembayaran)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
                            Transaksi.StatusPrint = true;
                            Transaksi.ConfirmTransaksi(db);
                        }
                        else
                        {
                            LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Danger, Transaksi.IDTransaksi);
                        }
                    }
                }
                db.SubmitChanges();
                LoadData();
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text);
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text);
        LoadData();
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        GenerateExcel();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    private void LoadData()
    {
        LoadData(false);
    }
    private void GenerateExcel()
    {
        LoadData(true);
    }

}