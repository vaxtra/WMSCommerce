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

public partial class WITAdministrator_Laporan_Transaksi_Transaksi : System.Web.UI.Page
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

                Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);
                Title1COGS.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue);
                Title2COGS.Visible = Title1COGS.Visible;
                Title3COGS.Visible = Title1COGS.Visible;
                Title4COGS.Visible = Title1COGS.Visible;
                Footer1COGS.Visible = Title1COGS.Visible;
                Footer2COGS.Visible = Title1COGS.Visible;

                Title1GrossProfit.Visible = Title1COGS.Visible;
                Title2GrossProfit.Visible = Title1COGS.Visible;
                Title3GrossProfit.Visible = Title1COGS.Visible;
                Title4GrossProfit.Visible = Title1COGS.Visible;
                Footer1GrossProfit.Visible = Title1COGS.Visible;
                Footer2GrossProfit.Visible = Title1COGS.Visible;

                ClassTempat.DataListBox(ListBoxTempat);
                ClassJenisTransaksi.DataListBox(db, ListBoxJenisTransaksi);
                StatusTransaksi_Class.DataListBox(db, ListBoxStatusTransaksi);

                ListBoxStatusTransaksi.SelectedValue = ((int)EnumStatusTransaksi.Complete).ToString(); ;

                if (!string.IsNullOrEmpty(Request.QueryString["TanggalAwal"]) && !string.IsNullOrEmpty(Request.QueryString["TanggalAkhir"]))
                {
                    ViewState["TanggalAwal"] = Request.QueryString["TanggalAwal"].ToDateTime();
                    ViewState["TanggalAkhir"] = Request.QueryString["TanggalAkhir"].ToDateTime();
                }
                else
                {
                    ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                    ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1].AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                }

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy HH:mm");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy HH:mm");

                LoadData();
            }
        }
        else
            LinkDownload.Visible = false;
    }

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy HH:mm");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy HH:mm");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            List<int> ListIDJenisTransaksi = new List<int>();

            foreach (ListItem item in ListBoxJenisTransaksi.Items)
            {
                if (item.Selected)
                    ListIDJenisTransaksi.Add(item.Value.ToInt());
            }

            List<int> ListIDTempat = new List<int>();

            foreach (ListItem item in ListBoxTempat.Items)
            {
                if (item.Selected)
                    ListIDTempat.Add(item.Value.ToInt());
            }

            List<int> ListIDStatusTransaksi = new List<int>();

            foreach (ListItem item in ListBoxStatusTransaksi.Items)
            {
                if (item.Selected)
                    ListIDStatusTransaksi.Add(item.Value.ToInt());
            }

            Result = Laporan_Class.NetRevenue(ListIDTempat, ListIDJenisTransaksi, ListIDStatusTransaksi, TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime());

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();

            RepeaterJenisPembayaran.DataSource = Result["DataJenisPembayaran"];
            RepeaterJenisPembayaran.DataBind();

            RepeaterRetur.DataSource = Result["DataRetur"];
            RepeaterRetur.DataBind();

            //RepeaterBrandDetail.DataSource = Result["DataBrand"];
            //RepeaterBrandDetail.DataBind();

            Title1COGS.Visible = Result["MelihatCOGS"];
            Title2COGS.Visible = Title1COGS.Visible;
            Title3COGS.Visible = Title1COGS.Visible;
            Title4COGS.Visible = Title1COGS.Visible;
            Footer1COGS.Visible = Title1COGS.Visible;
            Footer2COGS.Visible = Title1COGS.Visible;

            Title1GrossProfit.Visible = Title1COGS.Visible;
            Title2GrossProfit.Visible = Title1COGS.Visible;
            Title3GrossProfit.Visible = Title1COGS.Visible;
            Title4GrossProfit.Visible = Title1COGS.Visible;
            Footer1GrossProfit.Visible = Title1COGS.Visible;
            Footer2GrossProfit.Visible = Title1COGS.Visible;

            foreach (RepeaterItem item in RepeaterLaporan.Items)
            {
                HtmlTableCell PanelCOGS = (HtmlTableCell)item.FindControl("PanelCOGS");
                PanelCOGS.Visible = Title1COGS.Visible;

                HtmlTableCell PanelGrossProfit = (HtmlTableCell)item.FindControl("PanelGrossProfit");
                PanelGrossProfit.Visible = Title1COGS.Visible;
            }

            foreach (RepeaterItem item in RepeaterRetur.Items)
            {
                HtmlTableCell PanelCOGS = (HtmlTableCell)item.FindControl("PanelCOGS");
                PanelCOGS.Visible = Title1COGS.Visible;

                HtmlTableCell PanelGrossProfit = (HtmlTableCell)item.FindControl("PanelGrossProfit");
                PanelGrossProfit.Visible = Title1COGS.Visible;
            }

            //FILE EXCEL
            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "return popitup('DefaultPrint.aspx" + Laporan_Class.TempPencarian + "')";
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

    protected void ButtonJenisTransaksi_Click(object sender, EventArgs e)
    {
        Response.Redirect("JenisTransaksi.aspx?TanggalAwal=" + TextBoxTanggalAwal.Text + "&TanggalAkhir=" + TextBoxTanggalAkhir.Text);
    }
}