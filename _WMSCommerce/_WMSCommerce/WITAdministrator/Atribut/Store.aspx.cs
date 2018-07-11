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
                Store_Class ClassStore = new Store_Class(db);
                AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                var Store = ClassStore.Cari(Request.QueryString["id"].ToInt());

                if (Store != null)
                {
                    LiteralJavascript.Text = ClassAtribut.DropdownListSelect2(GrupAtribut.Store);

                    LabelJudul.Text = Store.Nama;

                    ClassAtributPilihan.StoreData(Store, RepeaterAtribut);
                }
                else
                    Response.Redirect("/WITAdministrator/Store/");
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);

            ClassAtributPilihan.StoreProses(Request.QueryString["id"].ToInt(), LabelJudul.Text, RepeaterAtribut);
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Store/");
    }
}