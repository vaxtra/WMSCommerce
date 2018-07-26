using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Vendor_PerbandinganHarga : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

                DropDownListCariAtributProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());

                DropDownListCariProduk.DataSource = db.TBProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListCariProduk.DataValueField = "IDProduk";
                DropDownListCariProduk.DataTextField = "Nama";
                DropDownListCariProduk.DataBind();
                DropDownListCariProduk.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });  
            }

            LoadData();
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], Pengaturan.HariIni()[0], Pengaturan.HariIni()[1], false);

            var Result = Laporan_Class.PerbandinganHargaVendor(pengguna.IDTempat, DropDownListCariProduk.SelectedValue.ToInt(), DropDownListCariAtributProduk.SelectedValue.ToInt());

            LiteralColspan.Text = "<td colspan='" + Result["DataJumlahVendor"].ToString() + "'></td>";

            RepeaterVendor.DataSource = Result["DataVendor"];
            RepeaterVendor.DataBind();
            RepeaterProduk.DataSource = Result["DataHargaVendor"];
            RepeaterProduk.DataBind();
        }
    }

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
}