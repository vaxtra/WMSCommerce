using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Post_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Konten_Class Konten_Class = new Konten_Class(db);

                var Konten = Konten_Class.Cari(Request.QueryString["id"].ToInt());

                if (Konten != null && Konten.EnumKontenJenis == (int)EnumKontenJenis.Post)
                {
                    ImageCover.ImageUrl = "/images/konten/" + Konten.IDKonten + ".jpg?" + DateTime.Now.Ticks;
                    TextBoxJudul.Text = Konten.Judul;
                    TextBoxIsi.Text = Konten.Isi;

                    ButtonOk.Text = "Ubah";
                    LabelKeterangan.Text = "Ubah";
                }
                else
                {
                    LabelKeterangan.Text = "Tambah";
                    ButtonOk.Text = "Tambah";
                }
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Konten_Class Konten_Class = new Konten_Class(db);

            TBKonten Konten = new TBKonten();

            if (ButtonOk.Text == "Tambah")
                Konten = Konten_Class.Tambah(EnumKontenJenis.Post, TextBoxJudul.Text, HttpUtility.HtmlDecode(TextBoxIsi.Text));
            else if (ButtonOk.Text == "Ubah")
                Konten = Konten_Class.Ubah(Request.QueryString["id"].ToInt(), EnumKontenJenis.Post, TextBoxJudul.Text, HttpUtility.HtmlDecode(TextBoxIsi.Text));

            db.SubmitChanges();

            if (FileUploadCover.HasFile)
                FileUploadCover.SaveAs(Server.MapPath("~/images/konten/") + Konten.IDKonten + ".jpg");
        }

        Response.Redirect("Default.aspx");
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}