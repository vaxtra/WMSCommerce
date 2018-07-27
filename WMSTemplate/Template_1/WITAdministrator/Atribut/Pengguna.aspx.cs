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
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                var Pengguna = ClassPengguna.Cari(Request.QueryString["id"].ToInt());

                if (Pengguna != null)
                {
                    LiteralJavascript.Text = ClassAtribut.DropdownListSelect2(GrupAtribut.Pengguna);

                    LabelJudul.Text = Pengguna.NamaLengkap;

                    ClassAtributPilihan.PenggunaData(Pengguna, RepeaterAtribut);
                }
                else
                    Response.Redirect("/WITAdministrator/Pengguna/");
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);

            ClassAtributPilihan.PenggunaProses(Request.QueryString["id"].ToInt(), LabelJudul.Text, RepeaterAtribut);
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Pengguna/");
    }
}