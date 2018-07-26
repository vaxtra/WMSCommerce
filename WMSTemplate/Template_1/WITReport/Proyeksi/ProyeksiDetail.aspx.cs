using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Proyeksi_ProyeksiDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariPemilikProdukProyeksiDetail.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                DropDownListCariAtributProdukProyeksiDetail.Items.AddRange(ClassAtributProduk.Dropdownlist());
                DropDownListCariKategoriProyeksiDetail.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                DropDownListCariProdukProyeksiDetail.DataSource = db.TBProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListCariProdukProyeksiDetail.DataValueField = "IDProduk";
                DropDownListCariProdukProyeksiDetail.DataTextField = "Nama";
                DropDownListCariProdukProyeksiDetail.DataBind();
                DropDownListCariProdukProyeksiDetail.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });
            }

            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            LoadData();
        }
    }

    #region DEFAULT
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

    #region Load Proyeksi Detail
    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            var Result = Laporan_Class.ProyeksiDetail(DropDownListCariTempat.SelectedValue.ToInt(), null, 0, DropDownListCariStatus.SelectedValue.ToInt(), TextBoxCariKodeProyeksiDetail.Text, DropDownListCariPemilikProdukProyeksiDetail.SelectedValue.ToInt(), DropDownListCariProdukProyeksiDetail.SelectedValue.ToInt(), DropDownListCariAtributProdukProyeksiDetail.SelectedValue.ToInt(), DropDownListCariKategoriProyeksiDetail.SelectedValue.ToInt(), true);

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            LinkDownloadProyeksiDetail.Visible = GenerateExcel;

            if (LinkDownloadProyeksiDetail.Visible)
                LinkDownloadProyeksiDetail.HRef = Laporan_Class.LinkDownload;

            ButtonPrintProyeksiDetail.OnClientClick = "return popitup('ProyeksiDetailPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

            LabelTotalJumlahHeaderProyeksiDetail.Text = Result["Jumlah"];
            LabelTotalJumlahFooterProyeksiDetail.Text = LabelTotalJumlahHeaderProyeksiDetail.Text;

            RepeaterProyeksiDetail.DataSource = Result["Data"];
            RepeaterProyeksiDetail.DataBind();
        }
    }

    private void LoadData()
    {
        LoadData(false);
    }
    protected void ButtonExcelProyeksiDetail_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
    protected void LoadData_EventProyeksiDetail(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion
}