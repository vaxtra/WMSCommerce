using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Grup_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }
    protected void RepeaterAtributGrup_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                AtributGrup_Class ClassAtributGrup = new AtributGrup_Class(db);

                if (e.CommandName == "Hapus")
                {
                    ClassAtributGrup.Hapus(e.CommandArgument.ToInt());
                    LoadData();
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    private void LoadData()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                AtributGrup_Class ClassAtributGrup = new AtributGrup_Class(db);

                RepeaterAtributGrup.DataSource = ClassAtributGrup.Data();
                RepeaterAtributGrup.DataBind();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}