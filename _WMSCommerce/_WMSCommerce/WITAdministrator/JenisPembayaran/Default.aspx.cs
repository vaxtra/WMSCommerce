using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_JenisPembayaran_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }
    protected void RepeaterJenisPembayaran_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);

                if (e.CommandName == "Hapus")
                {
                    ClassJenisPembayaran.Hapus(e.CommandArgument.ToInt());
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
                JenisPembayaran_Class ClassJenisPembayaran = new JenisPembayaran_Class(db);

                RepeaterJenisPembayaran.DataSource = ClassJenisPembayaran.Data().Where(item => item.IDJenisPembayaran != 1 && item.IDJenisPembayaran != 2);
                RepeaterJenisPembayaran.DataBind();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

}