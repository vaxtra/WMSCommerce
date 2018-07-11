using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pelanggan_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

                var Pelanggan = ClassPelanggan.Cari(Request.QueryString["id"].ToInt());

                DropDownListGrupPelanggan.Items.AddRange(GrupPelanggan_Class.DataDropDownListNamaPotongan(db));
                DropDownListPenggunaPIC.Items.AddRange(ClassPengguna.DropDownList());
                DropDownListPenggunaPIC.SelectedValue = Pengguna.IDPengguna.ToString();

                if (Pelanggan != null && Pelanggan.IDPelanggan != 1)
                {
                    //IDPelanggan
                    DropDownListPenggunaPIC.SelectedValue = Pelanggan.IDPenggunaPIC.ToString();
                    DropDownListGrupPelanggan.SelectedValue = Pelanggan.IDGrupPelanggan.ToString();
                    TextBoxNamaLengkap.Text = Pelanggan.NamaLengkap;
                    TextBoxUsername.Text = Pelanggan.Username;
                    TextBoxPassword.Text = Pelanggan.Password;
                    TextBoxHandphone.Text = Pelanggan.Handphone;
                    TextBoxTeleponLain.Text = Pelanggan.TeleponLain;
                    TextBoxEmail.Text = Pelanggan.Email;
                    TextBoxDeposit.Text = Pelanggan.Deposit.ToString();
                    TextBoxTanggalLahir.Text = Pelanggan.TanggalLahir.HasValue ? Pelanggan.TanggalLahir.Value.ToString("d MMMM yyyy") : "";
                    TextBoxCatatan.Text = Pelanggan.Catatan;
                    //TanggalDaftar
                    CheckBoxStatus.Checked = Pelanggan._IsActive;

                    var Alamat = db.TBAlamats.FirstOrDefault(item => item.IDPelanggan == Request.QueryString["id"].ToInt());

                    if (Alamat != null)
                        TextBoxAlamat.Text = Alamat.AlamatLengkap;

                    ButtonSimpan.Text = "Ubah";
                }
                else
                {
                    TextBoxTanggalLahir.Text = DateTime.Now.ToString("d MMMM yyyy");
                    ButtonSimpan.Text = "Tambah";
                }
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
            Alamat_Class ClassAlamat = new Alamat_Class();

            if (ButtonSimpan.Text == "Tambah")
            {
                var Pelanggan = ClassPelanggan.Tambah(
                        IDGrupPelanggan: DropDownListGrupPelanggan.SelectedValue.ToInt(),
                        IDPenggunaPIC: DropDownListPenggunaPIC.SelectedValue.ToInt(),
                        NamaLengkap: TextBoxNamaLengkap.Text,
                        Username: TextBoxUsername.Text,
                        Password: TextBoxPassword.Text,
                        Email: TextBoxEmail.Text,
                        Handphone: TextBoxHandphone.Text,
                        TeleponLain: TextBoxTeleponLain.Text,
                        TanggalLahir: TextBoxTanggalLahir.Text.ToDateTime(),
                        Deposit: TextBoxDeposit.Text.ToDecimal(),
                        Catatan: TextBoxCatatan.Text,
                        _IsActive: CheckBoxStatus.Checked.ToBool()
                        );

                ClassAlamat.Tambah(db, 0, Pelanggan, TextBoxAlamat.Text, "", 0, "");
            }
            else if (ButtonSimpan.Text == "Ubah")
            {
                var Pelanggan = ClassPelanggan.Ubah(
                    IDPelanggan: Request.QueryString["id"].ToInt(),
                    IDGrupPelanggan: DropDownListGrupPelanggan.SelectedValue.ToInt(),
                    IDPenggunaPIC: DropDownListPenggunaPIC.SelectedValue.ToInt(),
                    NamaLengkap: TextBoxNamaLengkap.Text,
                    Username: TextBoxUsername.Text,
                    Password: TextBoxPassword.Text,
                    Email: TextBoxEmail.Text,
                    Handphone: TextBoxHandphone.Text,
                    TeleponLain: TextBoxTeleponLain.Text,
                     TanggalLahir: TextBoxTanggalLahir.Text.ToDateTime(),
                    Deposit: TextBoxDeposit.Text.ToDecimal(),
                    Catatan: TextBoxCatatan.Text,
                    _IsActive: CheckBoxStatus.Checked.ToBool()
                    );

                if (Pelanggan.TBAlamats != null && Pelanggan.TBAlamats.Count() > 0)
                {
                    var Alamat = Pelanggan.TBAlamats.FirstOrDefault();
                    ClassAlamat.Ubah(db, 0, Alamat, Pelanggan, TextBoxAlamat.Text, "", Alamat.BiayaPengiriman.HasValue ? Alamat.BiayaPengiriman.Value : 0, Alamat.Keterangan);
                }
                else
                    ClassAlamat.Tambah(db, 0, Pelanggan, TextBoxAlamat.Text, "", 0, "");
            }

            db.SubmitChanges();
        }
        Response.Redirect("Default.aspx");
    }
}