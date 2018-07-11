using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_PerpindahanStok_PemakaianBahanBaku : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                KategoriBahanBaku_Class KategoriBahanBaku_Class = new KategoriBahanBaku_Class();
                Tempat_Class ClassTempat = new Tempat_Class(db);
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();
                DropDownListCariKategori.Items.AddRange(KategoriBahanBaku_Class.Dropdownlist(db));

                DropDownListCariBahanBaku.DataSource = db.TBBahanBakus.OrderBy(item => item.Nama).ToArray();
                DropDownListCariBahanBaku.DataValueField = "IDBahanBaku";
                DropDownListCariBahanBaku.DataTextField = "Nama";
                DropDownListCariBahanBaku.DataBind();

                DropDownListCariBahanBaku.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });

                DropDownListCariSatuan.DataSource = db.TBSatuans.OrderBy(item => item.Nama).ToArray();
                DropDownListCariSatuan.DataValueField = "IDSatuan";
                DropDownListCariSatuan.DataTextField = "Nama";
                DropDownListCariSatuan.DataBind();

                DropDownListCariSatuan.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");
            }

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

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);           

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            ButtonPrint.OnClientClick = "return popitup('PemakaianBahanBakuPrint.aspx" + Laporan_Class.TempPencarian + "&Status=BahanBaku')";
            #endregion

            if (DropDownListPOProduksi.SelectedValue == "BahanBaku")
            {
                var Result = Laporan_Class.PemakaianBahanBakuProduksiBahanBaku(DropDownListCariTempat.SelectedValue.ToInt(), TextBoxCariKode.Text, DropDownListCariBahanBaku.SelectedValue.ToInt(), DropDownListCariSatuan.SelectedValue.ToInt(), DropDownListCariKategori.SelectedValue.ToInt(), true);

                LabelTotalSubtotaPemakaian.Text = Result["TotalSubtotalKirim"];
                RepeaterLaporanPemakaianBahanBaku.DataSource = Result["Data"];
                RepeaterLaporanPemakaianBahanBaku.DataBind();
            }
            else
            {
                var Result = Laporan_Class.PemakaianBahanBakuProduksiProduk(DropDownListCariTempat.SelectedValue.ToInt(), TextBoxCariKode.Text, DropDownListCariBahanBaku.SelectedValue.ToInt(), DropDownListCariSatuan.SelectedValue.ToInt(), DropDownListCariKategori.SelectedValue.ToInt(), true);

                LabelTotalSubtotaPemakaian.Text = Result["TotalSubtotalKirim"];
                RepeaterLaporanPemakaianBahanBaku.DataSource = Result["Data"];
                RepeaterLaporanPemakaianBahanBaku.DataBind();
            }
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
}