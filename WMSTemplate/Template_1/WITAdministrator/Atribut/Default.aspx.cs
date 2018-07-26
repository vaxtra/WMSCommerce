using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }
    protected void RepeaterAtribut_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                if (e.CommandName == "Hapus")
                {
                    ClassAtribut.Hapus(e.CommandArgument.ToInt());
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
                Atribut_Class ClassAtribut = new Atribut_Class(db);

                RepeaterAtribut.DataSource = ClassAtribut.Data();
                RepeaterAtribut.DataBind();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}