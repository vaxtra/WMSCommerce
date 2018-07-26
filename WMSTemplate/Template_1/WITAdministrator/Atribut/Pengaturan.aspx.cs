using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Atribut_Class ClassAtribut = new Atribut_Class(db);

                    AtributGrup_Class ClassAtributGrup = new AtributGrup_Class(db);

                    ClassAtributGrup.DropDownList(DropDownListAtributGrup);

                    var Atribut = ClassAtribut.Cari(Request.QueryString["id"].ToInt());

                    if (Atribut != null)
                    {
                        DropDownListAtributGrup.SelectedValue = Atribut.IDAtributGrup.ToString();
                        TextBoxNama.Text = Atribut.Nama;
                        CheckBoxPilihan.Checked = Atribut.Pilihan;

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
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                if (ButtonOk.Text == "Tambah")
                    ClassAtribut.Tambah(DropDownListAtributGrup.SelectedValue.ToInt(), TextBoxNama.Text, CheckBoxPilihan.Checked);
                else if (ButtonOk.Text == "Ubah")
                    ClassAtribut.Ubah(Request.QueryString["id"].ToInt(), DropDownListAtributGrup.SelectedValue.ToInt(), TextBoxNama.Text, CheckBoxPilihan.Checked);

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