using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Barcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HiddenFieldID.Value = Request.QueryString["id"];
            LoadData();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {

        LoadData();
       
        ScriptManager.RegisterStartupScript(
            UpdatePanel1,
            this.GetType(),
            "MyAction",
            "window.print();",
            true);
    }

    private void LoadData()
    {
        int id = HiddenFieldID.Value.ToInt();


        LabelNomor.Text = id.ToString();
        HiddenFieldID.Value = (HiddenFieldID.Value.ToInt() + 1).ToString();
    }
}