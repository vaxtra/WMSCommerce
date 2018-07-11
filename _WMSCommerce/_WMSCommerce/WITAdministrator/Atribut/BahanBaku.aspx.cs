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
                BahanBaku_Class ClassBahanBaku = new BahanBaku_Class();
                AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                var BahanBaku = ClassBahanBaku.Cari(db, Request.QueryString["id"].ToInt());

                if (BahanBaku != null)
                {
                    LiteralJavascript.Text = ClassAtribut.DropdownListSelect2(GrupAtribut.BahanBaku);

                    LabelJudul.Text = BahanBaku.Nama;

                    ClassAtributPilihan.BahanBakuData(BahanBaku, RepeaterAtribut);
                }
                else
                    Response.Redirect("/WITAdministrator/BahanBaku/");
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            AtributPilihan_Class ClassAtributPilihan = new AtributPilihan_Class(db);

            ClassAtributPilihan.BahanBakuProses(Request.QueryString["id"].ToInt(), LabelJudul.Text, RepeaterAtribut);
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/BahanBaku/");
    }
}