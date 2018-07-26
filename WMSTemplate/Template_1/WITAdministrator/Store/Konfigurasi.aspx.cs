using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Store_Konfigurasi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            RepeaterKonfigurasi.DataSource = StoreKonfigurasi_Class.LoadData();
            RepeaterKonfigurasi.DataBind();
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            foreach (RepeaterItem item in RepeaterKonfigurasi.Items)
            {
                Label LabelIDStoreKonfigurasi = (Label)item.FindControl("LabelIDStoreKonfigurasi");
                TextBox TextBoxPengaturan = (TextBox)item.FindControl("TextBoxPengaturan");

                StoreKonfigurasi_Class.UbahPengaturan(db, LabelIDStoreKonfigurasi.Text.ToInt(), TextBoxPengaturan.Text);
            }

            db.SubmitChanges();
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}