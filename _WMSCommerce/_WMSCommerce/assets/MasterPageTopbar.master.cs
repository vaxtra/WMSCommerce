using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_MasterPageTopbar : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            LabelNamaTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;
            LabelUsername.Text = Pengguna.NamaLengkap;
        }
    }
}
