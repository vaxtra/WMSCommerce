using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Transaksi_TransaksiDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariStatusTransaksi.Items.AddRange(StatusTransaksi_Class.DataDropDownList(db));
                DropDownListCariStatusTransaksi.SelectedValue = ((int)EnumStatusTransaksi.Complete).ToString();

                ClassPelanggan.DropDownList(DropDownListCariPelanggan, true);

                DropDownListCariPemilikProduk.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                DropDownListCariAtributProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());
                DropDownListCariKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                DropDownListCariProduk.DataSource = db.TBProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListCariProduk.DataValueField = "IDProduk";
                DropDownListCariProduk.DataTextField = "Nama";
                DropDownListCariProduk.DataBind();
                DropDownListCariProduk.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                LoadData();
            }
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

            var Result = Laporan_Class.TransaksiDetail(DropDownListCariTempat.SelectedValue.ToInt(), DropDownListCariStatusTransaksi.SelectedValue.ToInt(), TextBoxIDTransaksi.Text, DropDownListCariPelanggan.SelectedValue.ToInt(), TextBoxKode.Text, DropDownListCariPemilikProduk.SelectedValue.ToInt(), DropDownListCariProduk.SelectedValue.ToInt(), DropDownListCariAtributProduk.SelectedValue.ToInt(), DropDownListCariKategori.SelectedValue.ToInt(), false);

            #region KONFIGURASI LAPORAN
            LabelPeriode.Text = Laporan_Class.Periode;

            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            ButtonPrint.OnClientClick = "return popitup('TransaksiDetailPrint.aspx" + Laporan_Class.TempPencarian + "')";
            #endregion

            LabelSubtotalHeader.Text = Result["Subtotal"];
            LabelJumlahProdukHeader.Text = Result["JumlahProduk"];
            LabelJumlahProdukFooter.Text = LabelJumlahProdukHeader.Text;
            LabelSubtotalFooter.Text = LabelSubtotalHeader.Text;

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
        LoadData();
    }
}