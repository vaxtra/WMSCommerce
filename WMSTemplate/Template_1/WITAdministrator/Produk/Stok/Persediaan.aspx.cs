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
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                Warna_Class ClassWarna = new Warna_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "Semua" });
                DropDownListJenisStok.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                DropDownListJenisStok.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                DropDownListJenisStok.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList().Where(item => item.Value != "0").ToArray());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListVarian.Items.AddRange(ClassAtributProduk.Dropdownlist());
                DropDownListWarna.Items.AddRange(ClassWarna.Dropdownlist());
                DropDownListKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));
                DropDownListBrand.Items.AddRange(ClassPemilikProduk.Dropdownlist());
            }
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], DateTime.Now, DateTime.Now, false);
            Result = Laporan_Class.PersediaanStokProduk(DropDownListTempat.SelectedValue.ToInt(), DropDownListJenisStok.Text.ToInt(), TextBoxProduk.Text, DropDownListWarna.SelectedValue.ToInt(), DropDownListBrand.SelectedValue.ToInt(), DropDownListKategori.SelectedValue.ToInt(), TextBoxKodeProduk.Text, DropDownListVarian.SelectedValue.ToInt(), TextBoxHarga.Text, TextBoxCOGS.Text, TextBoxQuantity.Text, TextBoxTotalHarga.Text, TextBoxTotalCOGS.Text);

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "return popitup('PersediaanPrint.aspx" + Laporan_Class.TempPencarian + "')";
        }
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}