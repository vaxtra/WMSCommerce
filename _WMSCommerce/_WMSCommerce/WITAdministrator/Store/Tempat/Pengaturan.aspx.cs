using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Tempat_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListKategoriTempat.DataSource = db.TBKategoriTempats.ToArray();
                DropDownListKategoriTempat.DataTextField = "Nama";
                DropDownListKategoriTempat.DataValueField = "IDKategoriTempat";
                DropDownListKategoriTempat.DataBind();

                Tempat_Class ClassTempat = new Tempat_Class(db);

                var Tempat = ClassTempat.Cari(Request.QueryString["id"].ToInt());

                if (Tempat != null)
                {
                    DropDownListKategoriTempat.SelectedValue = Tempat.IDKategoriTempat.ToString();
                    TextBoxKode.Text = Tempat.Kode;
                    TextBoxNama.Text = Tempat.Nama;
                    TextBoxAlamat.Text = Tempat.Alamat;
                    TextBoxEmail.Text = Tempat.Email;
                    TextBoxTelepon1.Text = Tempat.Telepon1;
                    TextBoxTelepon2.Text = Tempat.Telepon2;

                    TextBoxKeteranganBiayaTambahan1.Text = Tempat.KeteranganBiayaTambahan1;
                    TextBoxBiayaTambahan1.Text = Tempat.BiayaTambahan1.ToString();

                    TextBoxKeteranganBiayaTambahan2.Text = Tempat.KeteranganBiayaTambahan2;
                    TextBoxBiayaTambahan2.Text = Tempat.BiayaTambahan2.ToString();

                    TextBoxKeteranganBiayaTambahan3.Text = Tempat.KeteranganBiayaTambahan3;
                    TextBoxBiayaTambahan3.Text = Tempat.BiayaTambahan3.ToString();

                    TextBoxKeteranganBiayaTambahan4.Text = Tempat.KeteranganBiayaTambahan4;
                    TextBoxBiayaTambahan4.Text = Tempat.BiayaTambahan4.ToString();

                    TextBoxLatitude.Text = Tempat.Latitude;
                    TextBoxLongitude.Text = Tempat.Longitude;

                    TextBoxFooterPrint.Text = Tempat.FooterPrint;

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
            Tempat_Class ClassTempat = new Tempat_Class(db);

            if (ButtonSimpan.Text == "Tambah")
                ClassTempat.Tambah(
                    IDKategoriTempat: DropDownListKategoriTempat.SelectedValue.ToInt(),
                    Kode: TextBoxKode.Text,
                    Nama: TextBoxNama.Text,
                    Alamat: TextBoxAlamat.Text,
                    Email: TextBoxEmail.Text,
                    Telepon1: TextBoxTelepon1.Text,
                    Telepon2: TextBoxTelepon2.Text,
                    KeteranganBiayaTambahan1: TextBoxKeteranganBiayaTambahan1.Text,
                    BiayaTambahan1: TextBoxBiayaTambahan1.Text.ToDecimal(),
                    KeteranganBiayaTambahan2: TextBoxKeteranganBiayaTambahan2.Text,
                    BiayaTambahan2: TextBoxBiayaTambahan2.Text.ToDecimal(),
                    KeteranganBiayaTambahan3: TextBoxKeteranganBiayaTambahan3.Text,
                    BiayaTambahan3: TextBoxBiayaTambahan3.Text.ToDecimal(),
                    KeteranganBiayaTambahan4: TextBoxKeteranganBiayaTambahan4.Text,
                    BiayaTambahan4: TextBoxBiayaTambahan4.Text.ToDecimal(),
                    Latitude: TextBoxLatitude.Text,
                    Longitude: TextBoxLongitude.Text,
                    FooterPrint: TextBoxFooterPrint.Text,
                    _IsActive: true
                    );
            else if (ButtonSimpan.Text == "Ubah")
                ClassTempat.Ubah(
                    IDTempat: Request.QueryString["id"].ToInt(),
                    IDKategoriTempat: DropDownListKategoriTempat.SelectedValue.ToInt(),
                    Kode: TextBoxKode.Text,
                    Nama: TextBoxNama.Text,
                    Alamat: TextBoxAlamat.Text,
                    Email: TextBoxEmail.Text,
                    Telepon1: TextBoxTelepon1.Text,
                    Telepon2: TextBoxTelepon2.Text,
                    KeteranganBiayaTambahan1: TextBoxKeteranganBiayaTambahan1.Text,
                    BiayaTambahan1: TextBoxBiayaTambahan1.Text.ToDecimal(),
                    KeteranganBiayaTambahan2: TextBoxKeteranganBiayaTambahan2.Text,
                    BiayaTambahan2: TextBoxBiayaTambahan2.Text.ToDecimal(),
                    KeteranganBiayaTambahan3: TextBoxKeteranganBiayaTambahan3.Text,
                    BiayaTambahan3: TextBoxBiayaTambahan3.Text.ToDecimal(),
                    KeteranganBiayaTambahan4: TextBoxKeteranganBiayaTambahan4.Text,
                    BiayaTambahan4: TextBoxBiayaTambahan4.Text.ToDecimal(),
                    Latitude: TextBoxLatitude.Text,
                    Longitude: TextBoxLongitude.Text,
                    FooterPrint: TextBoxFooterPrint.Text
                    );

            db.SubmitChanges();

            Response.Redirect("Default.aspx");
        }
    }

    protected void ButtonBatal_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}