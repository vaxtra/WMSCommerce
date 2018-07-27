using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_UbahPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (Pengguna != null)
            {
                var DataPengguna = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Pengguna.IDPengguna && item.Password == TextBoxPasswordLama.Text);

                if (DataPengguna != null)
                {
                    DataPengguna.Password = TextBoxPasswordBaru.Text;
                    Response.Redirect("/WITAdministrator/Default.aspx");
                }
                else
                    LabelWarning.Text = "Terjadi kesalahan";
            }
        }
    }
}