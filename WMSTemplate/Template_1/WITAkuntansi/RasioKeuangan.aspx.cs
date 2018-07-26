using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_RasioKeuangan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int index = 0;
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                DropDownListTahun.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                index++;
            }
            DropDownListTahun.SelectedIndex = 5;

            LabelHeader.Text = "FINANCIAL RATIO ANALYSIS " + DropDownListTahun.SelectedValue.ToString();
            
        }
    }
    public string Pertumbuhan(decimal pertubuhan)
    {
        if (pertubuhan < 0)
            return "<span class='label label-danger'>" + pertubuhan.ToFormatHarga() + "</span>";
        else
            return "<span class='label label-success'>" + pertubuhan.ToFormatHarga() + "</span>";
    }
}