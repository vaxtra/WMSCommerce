using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Produk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Produk_Class ClassProduk = new Produk_Class(db);
                AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                var Produk = ClassProduk.Cari(Request.QueryString["id"].ToInt());

                if (Produk != null)
                {
                    LiteralJavascript.Text = ClassAtribut.DropdownListSelect2(GrupAtribut.Produk);

                    LabelJudul.Text = Produk.Nama;

                    ClassAtributPilihan.ProdukData(Produk, RepeaterAtribut);
                }
                else
                    Response.Redirect("/WITAdministrator/Produk/");
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);

            ClassAtributPilihan.ProdukProses(Request.QueryString["id"].ToInt(), LabelJudul.Text, RepeaterAtribut);
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Produk/");
    }
}