using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Store_Tempat_Maps : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LiteralMaps.Text = string.Empty;
            LiteralMaps.Text += "<script type=\"text/javascript\">";

            LiteralMaps.Text += "var markers = [";

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                foreach (var item in db.TBTempats.ToArray())
                {
                    LiteralMaps.Text += "{";

                    string namaStore = item.TBStore.Nama + " - " + item.Nama + " - " + item.TBKategoriTempat.Nama;

                    LiteralMaps.Text += "\"title\": '" + namaStore + "',";
                    LiteralMaps.Text += "\"lat\": '" + item.Latitude + "',";
                    LiteralMaps.Text += "\"lng\": '" + item.Longitude + "',";
                    LiteralMaps.Text += "\"description\": '<b>" + namaStore + "</b><br/><br/>" + item.Alamat + "'";

                    LiteralMaps.Text += "},";
                }
            }

            LiteralMaps.Text += "];";

            LiteralMaps.Text += "</script>";
        }
    }
}