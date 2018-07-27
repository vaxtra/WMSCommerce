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

public partial class WITAdministrator_Produk_Stok_MultiStore : System.Web.UI.Page
{
    public Dictionary<string, dynamic> Result { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListKategoriTempat.DataSource = db.TBKategoriTempats.OrderBy(item => item.Nama);
                DropDownListKategoriTempat.DataTextField = "Nama";
                DropDownListKategoriTempat.DataValueField = "IDKategoriTempat";
                DropDownListKategoriTempat.DataBind();
                DropDownListKategoriTempat.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua Kategori -" });

                DropDownListCariProduk.DataSource = db.TBProduks.OrderBy(item => item.Nama);
                DropDownListCariProduk.DataTextField = "Nama";
                DropDownListCariProduk.DataValueField = "IDProduk";
                DropDownListCariProduk.DataBind();
                DropDownListCariProduk.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariPemilikProduk.DataSource = db.TBPemilikProduks.OrderBy(item => item.Nama);
                DropDownListCariPemilikProduk.DataTextField = "Nama";
                DropDownListCariPemilikProduk.DataValueField = "IDPemilikProduk";
                DropDownListCariPemilikProduk.DataBind();
                DropDownListCariPemilikProduk.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();

                DropDownListCariAtributProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());
                DropDownListCariKategoriProduk.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                //if (Request.QueryString["action"] == "Tempat")
                //    DropDownListKategoriTempat.Visible = false;
                //else if (Request.QueryString["action"] == "KategoriTempat")
                //    DropDownListTempat.Visible = false;
                //else
                //    Response.Redirect("/WITWarehouse/Produk.aspx");

            }
            LoadData();
        }
        else
            LinkDownload.Visible = false;
    }
    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], DateTime.Now, DateTime.Now, GenerateExcel);

            Result = Laporan_Class.StokMultistore(DropDownListJenisStokProduk.SelectedValue.ToInt(), DropDownListKategoriTempat.SelectedValue.ToInt(), TextBoxKode.Text, DropDownListCariProduk.SelectedValue.ToInt(), DropDownListCariAtributProduk.SelectedValue.ToInt(), DropDownListCariPemilikProduk.SelectedValue.ToInt(), DropDownListCariKategoriProduk.SelectedValue.ToInt());

            RepeaterTempat.DataSource = Result["Tempat"];
            RepeaterTempat.DataBind();

            RepeaterTotalTempat1.DataSource = Result["Tempat"];
            RepeaterTotalTempat1.DataBind();

            RepeaterTotalTempat2.DataSource = Result["Tempat"];
            RepeaterTotalTempat2.DataBind();

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();

            //FILE EXCEL
            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Laporan_Class.LinkDownload;

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "return popitup('MultistorePrint.aspx" + Laporan_Class.TempPencarian + "')";
        }
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