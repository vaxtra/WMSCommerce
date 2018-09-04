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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                JenisPerpindahanStok_Class JenisPerpindahanStok_Class = new JenisPerpindahanStok_Class();

                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariPengguna.Items.AddRange(ClassPengguna.DropDownList(true));
                DropDownListCariAtributProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());

                DropDownListCariJenisPerpindahanStok.Items.AddRange(JenisPerpindahanStok_Class.DataDropDownList(db));
                DropDownListCariJenisPerpindahanStok.SelectedValue = "0";

                DropDownListCariProduk.DataSource = db.TBProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListCariProduk.DataValueField = "IDProduk";
                DropDownListCariProduk.DataTextField = "Nama";
                DropDownListCariProduk.DataBind();
                DropDownListCariProduk.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");
            }
            //KETERANGAN
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Keterangan"]))
                TextBoxCariKeterangan.Text = Request.QueryString["Keterangan"];

            LoadData();
        }
        else
            LinkDownload.Visible = false;
    }

    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            var Result = Laporan_Class.PerpindahanStokProdukDetail(DropDownListCariTempat.SelectedValue.ToInt(), DropDownListCariPengguna.SelectedValue.ToInt(), TextBoxCariKode.Text, DropDownListCariProduk.SelectedValue.ToInt(), DropDownListCariAtributProduk.SelectedValue.ToInt(), 0, DropDownListCariJenisPerpindahanStok.SelectedValue.ToInt(), TextBoxCariKeterangan.Text, false);

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            ButtonPrint.OnClientClick = "return popitup('ProdukDetailPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

            LabelTotalJumlahHeader.Text = Result["Jumlah"];
            LabelTotalJumlahFooter.Text = LabelTotalJumlahHeader.Text;

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();
        }
    }
    private void LoadData()
    {
        LoadData(false);
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Request.QueryString["returnUrl"]))
            Response.Redirect(Request.QueryString["returnUrl"]);
        else
            Response.Redirect("/WITAdministrator/Default.aspx");
    }
}