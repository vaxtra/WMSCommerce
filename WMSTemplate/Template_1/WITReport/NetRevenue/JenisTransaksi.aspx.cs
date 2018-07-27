using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_NetRevenue_JenisTransaksi : System.Web.UI.Page
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
                HeaderGrandtotalCOGS.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue);
                HeaderGrandtotalGrossProfit.Visible = HeaderGrandtotalCOGS.Visible;
                FooterGrandtotalCOGS.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue);
                FooterGrandtotalGrossProfit.Visible = FooterGrandtotalCOGS.Visible;

                ClassTempat.DataListBox(ListBoxTempat);
                ClassJenisTransaksi.DataListBox(db, ListBoxJenisTransaksi);
                StatusTransaksi_Class.DataListBox(db, ListBoxStatusTransaksi);

                ListBoxStatusTransaksi.SelectedValue = "5";

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
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

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

            Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

            if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.MelihatCOGSNetRevenue))
            {
                Result = Laporan_Class.NetRevenueJenisTransaksi(ListIDTempat, ListIDJenisTransaksi, ListIDStatusTransaksi, TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime());
            }
            else
            {
                Result = Laporan_Class.NetRevenueJenisTransaksi(ListIDTempat, ListIDJenisTransaksi, ListIDStatusTransaksi, TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime());
            }

            LabelHeaderGrandtotalJumlahProduk.Text = Parse.ToFormatHargaBulat(Result["GrandtotalJumlahProduk"]);
            LabelHeaderGrandtotalGross.Text = Parse.ToFormatHarga(Result["GrandtotalGross"]);
            LabelHeaderGrandtotalDiscount.Text = Parse.ToFormatHarga(Result["GrandtotalDiscount"]);
            LabelHeaderGrandtotalNetRevenue.Text = Parse.ToFormatHarga(Result["GrandtotalNetRevenue"]);
            LabelHeaderGrandtotalCOGS.Text = Parse.ToFormatHarga(Result["GrandtotalCOGS"]);
            LabelHeaderGrandtotalGrossProfit.Text = Parse.ToFormatHarga(Result["GrandtotalGrossProfit"]);

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();

            LabelFooterGrandtotalJumlahProduk.Text = LabelHeaderGrandtotalJumlahProduk.Text;
            LabelFooterGrandtotalGross.Text = LabelHeaderGrandtotalGross.Text;
            LabelFooterGrandtotalDiscount.Text = LabelHeaderGrandtotalDiscount.Text;
            LabelFooterGrandtotalNetRevenue.Text = LabelHeaderGrandtotalNetRevenue.Text;
            LabelFooterGrandtotalCOGS.Text = LabelHeaderGrandtotalCOGS.Text;
            LabelFooterGrandtotalGrossProfit.Text = LabelHeaderGrandtotalGrossProfit.Text;

            foreach (RepeaterItem item in RepeaterLaporan.Items)
            {
                HtmlTableCell TitleCOGS = (HtmlTableCell)item.FindControl("TitleCOGS");
                HtmlTableCell TitleGrossProfit = (HtmlTableCell)item.FindControl("TitleGrossProfit");
                HtmlTableCell FooterCOGS = (HtmlTableCell)item.FindControl("FooterCOGS");
                HtmlTableCell FooterGrossProfit = (HtmlTableCell)item.FindControl("FooterGrossProfit");
                TitleCOGS.Visible = Result["MelihatCOGS"];
                TitleGrossProfit.Visible = TitleCOGS.Visible;
                FooterCOGS.Visible = TitleCOGS.Visible;
                FooterGrossProfit.Visible = TitleCOGS.Visible;

                Repeater RepeaterBody = (Repeater)item.FindControl("RepeaterBody");

                foreach (RepeaterItem item2 in RepeaterBody.Items)
                {
                    HtmlTableCell PanelCOGS = (HtmlTableCell)item2.FindControl("PanelCOGS");
                    HtmlTableCell PanelGrossProfit = (HtmlTableCell)item2.FindControl("PanelGrossProfit");
                    PanelCOGS.Visible = TitleCOGS.Visible;
                    PanelGrossProfit.Visible = TitleCOGS.Visible;
                }
            }

            //FILE EXCEL
            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "www.facebook.com";
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

    protected void ButtonNetRevenue_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?TanggalAwal=" + TextBoxTanggalAwal.Text + "&TanggalAkhir=" + TextBoxTanggalAkhir.Text);
    }
}