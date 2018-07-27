using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Discount_Event_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    private void LoadData()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

                RepeaterGrupPelanggan.DataSource = GrupPelanggan_Class.Data(db);
                RepeaterGrupPelanggan.DataBind();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}