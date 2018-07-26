using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_Plugins_Select2_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        Response.Write(TextBox1.Text);
    }
}