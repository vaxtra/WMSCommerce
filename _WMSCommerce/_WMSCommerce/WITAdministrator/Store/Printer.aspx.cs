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
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadData(db);
            }
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            KonfigurasiPrinter_Class Konfigurasi_ClassPrinter = new KonfigurasiPrinter_Class();

            foreach (RepeaterItem item in RepeaterKonfigurasi.Items)
            {
                Label LabelIDKonfigurasiPrinter = (Label)item.FindControl("LabelIDKonfigurasiPrinter");
                TextBox TextBoxPengaturan = (TextBox)item.FindControl("TextBoxPengaturan");

                Konfigurasi_ClassPrinter.Ubah(db, LabelIDKonfigurasiPrinter.Text.ToInt(), TextBoxPengaturan.Text);
            }

            db.SubmitChanges();
        }
    }
    private void LoadData(DataClassesDatabaseDataContext db)
    {
        KonfigurasiPrinter_Class Konfigurasi_ClassPrinter = new KonfigurasiPrinter_Class();

        RepeaterKonfigurasi.DataSource = Konfigurasi_ClassPrinter.Data(db);
        RepeaterKonfigurasi.DataBind();
    }
}