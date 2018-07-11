using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITSurvey_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    protected void RepeaterSoal_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var Soal = db.TBSoals.FirstOrDefault(item => item.IDSoal == e.CommandArgument.ToInt());

                    db.TBSoals.DeleteOnSubmit(Soal);
                    db.SubmitChanges();
                }

                LoadData();

            }
            catch (Exception)
            {
                Response.Redirect("Hapus.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterSoal.DataSource = db.TBSoals.ToArray();
            RepeaterSoal.DataBind();
        }
    }
}