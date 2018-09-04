using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_BusinessIntelligence_SalesRegion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

                ListBoxGrupPelanggan.DataSource = db.TBGrupPelanggans.ToArray();
                ListBoxGrupPelanggan.DataValueField = "IDGrupPelanggan";
                ListBoxGrupPelanggan.DataTextField = "Nama";
                ListBoxGrupPelanggan.DataBind();
            }

            LoadData();
        }
        ////////////else
        ////////////    LinkDownload.Visible = false;
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
            BusinessIntelligence_Class ClassLaporan = new BusinessIntelligence_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            List<int> ListIDGrupPelanggan = new List<int>();

            foreach (ListItem item in ListBoxGrupPelanggan.Items)
            {
                if (item.Selected)
                    ListIDGrupPelanggan.Add(Parse.Int(item.Value));
            }

            var Result = ClassLaporan.CustomerLocation(ListIDGrupPelanggan);

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = ClassLaporan.Periode;

            ////////////LinkDownload.Visible = GenerateExcel;

            ////////////if (LinkDownload.Visible)
            ////////////    LinkDownload.HRef = ClassLaporan.LinkDownload;

            ////////////ButtonPrint.OnClientClick = "return popitup('Print.aspx" + ClassLaporan.TempPencarian + "')";
            #endregion

            LabelBaruJumlahTransaksi.Text = Result["BaruJumlahTransaksi"];
            LabelBaruJumlahProduk.Text = Result["BaruJumlahProduk"];
            LabelBaruGrandtotal.Text = Result["BaruGrandtotal"];
            LabelLamaJumlahTransaksi.Text = Result["LamaJumlahTransaksi"];
            LabelLamaJumlahProduk.Text = Result["LamaJumlahProduk"];
            LabelLamaGrandtotal.Text = Result["LamaGrandtotal"];

            RepeaterLaporanGrupPelanggan.DataSource = Result["DataGrupPelanggan"];
            RepeaterLaporanGrupPelanggan.DataBind();
            LabelTotalGrupPelangganJumlahTransaksi.Text = Result["TotalGrupPelangganJumlahTransaksi"];
            LabelTotalGrupPelangganJumlahProduk.Text = Result["TotalGrupPelangganJumlahProduk"];
            LabelTotalGrupPelangganGrandtotal.Text = Result["TotalGrupPelangganGrandtotal"];

            RepeaterLaporanPelanggan.DataSource = Result["DataPelanggan"];
            RepeaterLaporanPelanggan.DataBind();
            LabelTotalPelangganJumlahTransaksi.Text = Result["TotalPelangganJumlahTransaksi"];
            LabelTotalPelangganJumlahProduk.Text = Result["TotalPelangganJumlahProduk"];
            LabelTotalPelangganGrandtotal.Text = Result["TotalPelangganGrandtotal"];

            RepeaterLaporanKota.DataSource = Result["DataKota"];
            RepeaterLaporanKota.DataBind();
            LabelTotalKotaJumlahTransaksi.Text = Result["TotalKotaJumlahTransaksi"];
            LabelTotalKotaJumlahProduk.Text = Result["TotalKotaJumlahProduk"];
            LabelTotalKotaGrandtotal.Text = Result["TotalKotaGrandtotal"];

            RepeaterLaporanProvinsi.DataSource = Result["DataProvinsi"];
            RepeaterLaporanProvinsi.DataBind();
            LabelTotalProvinsiJumlahTransaksi.Text = Result["TotalProvinsiJumlahTransaksi"];
            LabelTotalProvinsiJumlahProduk.Text = Result["TotalProvinsiJumlahProduk"];
            LabelTotalProvinsiGrandtotal.Text = Result["TotalProvinsiGrandtotal"];
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
        LoadData();
    }

    protected void RepeaterLaporan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<int> ListIDGrupPelanggan = new List<int>();

        foreach (ListItem item in ListBoxGrupPelanggan.Items)
        {
            if (item.Selected)
                ListIDGrupPelanggan.Add(Parse.Int(item.Value));
        }

        if (e.CommandName == "Pelanggan")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPelanggan pelanggan = db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == Parse.Int(e.CommandArgument.ToString()));
                LabelDetailJudul.Text = "Pelanggan " + pelanggan.NamaLengkap.ToUpper();

                BusinessIntelligence_Class ClassLaporan = new BusinessIntelligence_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], false);

                var Result = ClassLaporan.CustomerDetailTransaksiProduk(ListIDGrupPelanggan, Parse.Int(e.CommandArgument.ToString()), "Pelanggan");
                RepeaterLaporanDetailTransaksi.DataSource = Result["HasilTransaksi"];
                RepeaterLaporanDetailTransaksi.DataBind();
                RepeaterLaporanDetailProduk.DataSource = Result["HasilProduk"];
                RepeaterLaporanDetailProduk.DataBind();
            }

            DivKota.Visible = false;
            DivProvinsi.Visible = false;
            DivDetail.Visible = true;

            PanelDetail.Attributes.Add("class", "panel panel-success");
        }
        else if (e.CommandName == "Kota")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBWilayah pelanggan = db.TBWilayahs.FirstOrDefault(item => item.IDWilayah == Parse.Int(e.CommandArgument.ToString()));
                LabelDetailJudul.Text = "Kota " + pelanggan.Nama.ToUpper();

                BusinessIntelligence_Class ClassLaporan = new BusinessIntelligence_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], false);

                var Result = ClassLaporan.CustomerDetailTransaksiProduk(ListIDGrupPelanggan, Parse.Int(e.CommandArgument.ToString()), "Kota");
                RepeaterLaporanDetailTransaksi.DataSource = Result["HasilTransaksi"];
                RepeaterLaporanDetailTransaksi.DataBind();
                RepeaterLaporanDetailProduk.DataSource = Result["HasilProduk"];
                RepeaterLaporanDetailProduk.DataBind();
            }

            DivPelanggan.Visible = false;
            DivProvinsi.Visible = false;
            DivDetail.Visible = true;

            PanelDetail.Attributes.Add("class", "panel panel-info");
        }
        else if (e.CommandName == "Provinsi")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBWilayah pelanggan = db.TBWilayahs.FirstOrDefault(item => item.IDWilayah == Parse.Int(e.CommandArgument.ToString()));
                LabelDetailJudul.Text = "Provinsi " + pelanggan.Nama.ToUpper();

                BusinessIntelligence_Class ClassLaporan = new BusinessIntelligence_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], false);

                var Result = ClassLaporan.CustomerDetailTransaksiProduk(ListIDGrupPelanggan, Parse.Int(e.CommandArgument.ToString()), "Provinsi");
                RepeaterLaporanDetailTransaksi.DataSource = Result["HasilTransaksi"];
                RepeaterLaporanDetailTransaksi.DataBind();
                RepeaterLaporanDetailProduk.DataSource = Result["HasilProduk"];
                RepeaterLaporanDetailProduk.DataBind();
            }

            DivPelanggan.Visible = false;
            DivKota.Visible = false;
            DivDetail.Visible = true;

            PanelDetail.Attributes.Add("class", "panel panel-warning");
        }
    }

    protected void ButtonDetailTutup_Click(object sender, EventArgs e)
    {
        DivPelanggan.Visible = true;
        DivKota.Visible = true;
        DivProvinsi.Visible = true;
        DivDetail.Visible = false;
    }
}