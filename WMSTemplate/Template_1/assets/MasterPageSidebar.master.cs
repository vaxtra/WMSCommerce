using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_MasterPageSidebar : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            //LabelUsername.Text = Pengguna.NamaLengkap;
            LabelTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Menubar_Class ClassMenubar = new Menubar_Class(db);

                LiteralMenubar.Text = ClassMenubar.GenerateHTML(Pengguna.IDGrupPengguna, EnumMenubarModul.WITAdministrator_Sidebar);
            }
        }
    }
}
