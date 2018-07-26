using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Meja_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBMeja Meja = db.TBMejas.FirstOrDefault(item => item.IDMeja == Request.QueryString["id"].ToInt());

                if (Meja != null)
                {
                    TextBoxNama.Text = Meja.Nama;
                    TextBoxJumlahKursi.Text = Meja.JumlahKursi.ToString();
                    CheckBoxVIP.Checked = Meja.VIP.Value;
                    CheckBoxStatus.Checked = Meja.Status.Value;
                    CheckBoxStatus.Enabled = true;

                    LabelKeterangan.Text = "Ubah";
                    ButtonSimpan.Text = "Ubah";
                }
                else
                {
                    LabelKeterangan.Text = "Tambah";
                    ButtonSimpan.Text = "Tambah";
                }
            }
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Meja_Class Meja_Class = new Meja_Class();

            if (ButtonSimpan.Text == "Tambah")
            {
                Meja_Class.Tambah(db, TextBoxNama.Text, TextBoxJumlahKursi.Text.ToDecimal().ToInt(), CheckBoxVIP.Checked, true);
            }
            else if (ButtonSimpan.Text == "Ubah")
            {
                Meja_Class.Ubah(db, Request.QueryString["id"].ToInt(), TextBoxNama.Text, TextBoxJumlahKursi.Text.ToDecimal().ToInt(), CheckBoxVIP.Checked, true);
            }
            
            db.SubmitChanges();

            Response.Redirect("Default.aspx");
        }
    }
    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}