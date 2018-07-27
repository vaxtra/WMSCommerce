using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Grup_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    AtributGrup_Class ClassAtributGrup = new AtributGrup_Class(db);

                    var AtributGrup = ClassAtributGrup.Cari(Request.QueryString["id"].ToInt());

                    if (AtributGrup != null)
                    {
                        TextBoxNama.Text = AtributGrup.Nama;

                        ButtonOk.Text = "Ubah";
                    }
                    else
                        ButtonOk.Text = "Tambah";
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                AtributGrup_Class ClassAtributGrup = new AtributGrup_Class(db);

                if (ButtonOk.Text == "Tambah")
                    ClassAtributGrup.Tambah(TextBoxNama.Text);
                else if (ButtonOk.Text == "Ubah")
                    ClassAtributGrup.Ubah(Request.QueryString["id"].ToInt(), TextBoxNama.Text);

                db.SubmitChanges();

                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}