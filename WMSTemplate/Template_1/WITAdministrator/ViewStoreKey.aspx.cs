using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_ViewStoreKey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                RepeaterData.DataSource = db.TBStoreKeys.OrderBy(item => item.TanggalKey);
                RepeaterData.DataBind();
            }
        }
    }
}