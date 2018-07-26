using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Post_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }
    protected void RepeaterKonten_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Hapus")
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Konten_Class Konten_Class = new Konten_Class(db);

                    Konten_Class.Hapus(e.CommandArgument.ToInt());

                    if (File.Exists(Server.MapPath("~/images/konten/") + e.CommandArgument + ".jpg"))
                        File.Delete(Server.MapPath("~/images/konten/") + e.CommandArgument + ".jpg");

                    LoadData();
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Konten_Class Konten_Class = new Konten_Class(db);

            RepeaterKonten.DataSource = Konten_Class.Data(EnumKontenJenis.Post);
            RepeaterKonten.DataBind();
        }
    }
}