using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                Store_Class ClassStore = new Store_Class(db);

                //SUPER ADMINISTRATOR HANYA BISA DIBUAT OLEH SUPER ADMINISTRATOR
                if (Pengguna.IDGrupPengguna == (int)EnumGrupPengguna.SuperAdministrator)
                    DropDownListGrupPengguna.DataSource = db.TBGrupPenggunas.ToArray();
                else
                    DropDownListGrupPengguna.DataSource = db.TBGrupPenggunas.Where(item => item.IDGrupPengguna != (int)EnumGrupPengguna.SuperAdministrator).ToArray();

                DropDownListGrupPengguna.DataTextField = "Nama";
                DropDownListGrupPengguna.DataValueField = "IDGrupPengguna";
                DropDownListGrupPengguna.DataBind();
                DropDownListGrupPengguna.SelectedValue = ((int)EnumGrupPengguna.Employee).ToString();

                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataBind();
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                var DataPengguna = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == Request.QueryString["id"].ToInt());

                if (DataPengguna != null && DataPengguna.IDPengguna != (int)EnumPengguna.RendyHerdiawan)
                {
                    //IDPengguna
                    //IDPenggunaParent

                    DropDownListGrupPengguna.SelectedValue = DataPengguna.IDGrupPengguna.ToString();
                    DropDownListTempat.SelectedValue = DataPengguna.IDTempat.ToString();
                    TextBoxNomorIdentitas.Text = DataPengguna.NomorIdentitas;
                    TextBoxNomorNPWP.Text = DataPengguna.NomorNPWP;
                    TextBoxNomorRekening.Text = DataPengguna.NomorRekening;
                    TextBoxNamaBank.Text = DataPengguna.NamaBank;
                    TextBoxNamaRekening.Text = DataPengguna.NamaRekening;
                    TextBoxNamaLengkap.Text = DataPengguna.NamaLengkap;
                    TextBoxTempatLahir.Text = DataPengguna.TempatLahir;
                    TextBoxTanggalLahir.Text = DataPengguna.TanggalLahir.ToString("d MMMM yyyy");
                    DropDownListGender.SelectedValue = DataPengguna.JenisKelamin.ToString();
                    TextBoxAlamat.Text = DataPengguna.Alamat;
                    TextBoxAgama.Text = DataPengguna.Agama;
                    TextBoxTelepon.Text = DataPengguna.Telepon;
                    TextBoxHandphone.Text = DataPengguna.Handphone;
                    TextBoxEmail.Text = DataPengguna.Email;
                    TextBoxStatusPerkawinan.Text = DataPengguna.StatusPerkawinan;
                    TextBoxKewarganegaraan.Text = DataPengguna.Kewarganegaraan;
                    TextBoxPendidikanTerakhir.Text = DataPengguna.PendidikanTerakhir;
                    TextBoxTanggalBekerja.Text = DataPengguna.TanggalBekerja.ToString("d MMMM yyyy");
                    TextBoxUsername.Text = DataPengguna.Username;
                    TextBoxPassword.Text = DataPengguna.Password;
                    TextBoxPIN.Text = DataPengguna.PIN;
                    DropDownListStatus.SelectedValue = DataPengguna._IsActive.ToString();
                    TextBoxCatatan.Text = DataPengguna.Catatan;

                    //EkstensiFoto
                    //RFID
                    //SidikJari
                    //GajiPokok
                    //JaminanHariTua
                    //JaminanKecelakaan
                    //PPH21
                    //TunjanganHariRaya
                    //TunjanganMakan
                    //TunjanganTransportasi

                    ButtonSimpan.Text = "Ubah";
                    LabelKeterangan.Text = "Ubah";
                }
                else
                {
                    PanelStatus.Visible = false;
                    DropDownListStatus.SelectedValue = true.ToString();

                    TextBoxPIN.Text = Pengaturan.GeneratePIN(db);
                    TextBoxTanggalLahir.Text = DateTime.Now.ToString("d MMMM yyyy");
                    TextBoxTanggalBekerja.Text = DateTime.Now.ToString("d MMMM yyyy");
                    TextBoxGajiKotor.Text = "0";
                    TextBoxTambahanGaji.Text = "0";
                    TextBoxPotonganGaji.Text = "0";
                    TextBoxGajiBersih.Text = "0";

                    LabelKeterangan.Text = "Tambah";
                    ButtonSimpan.Text = "Tambah";
                }
            }
        }
        else
        {
            LiteralWarning.Text = "";
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);

                if (ButtonSimpan.Text == "Tambah")
                    ClassPengguna.Tambah(
                        DropDownListGrupPengguna.SelectedValue.ToInt(),
                        DropDownListTempat.SelectedValue.ToInt(),
                        TextBoxNomorIdentitas.Text,
                        TextBoxNomorNPWP.Text,
                        TextBoxNomorRekening.Text,
                        TextBoxNamaBank.Text,
                        TextBoxNamaRekening.Text,
                        TextBoxNamaLengkap.Text,
                        TextBoxTempatLahir.Text,
                        TextBoxTanggalLahir.Text.ToDateTime(),
                        DropDownListGender.SelectedValue.ToBool(),
                        TextBoxAlamat.Text,
                        TextBoxAgama.Text,
                        TextBoxTelepon.Text,
                        TextBoxHandphone.Text,
                        TextBoxEmail.Text,
                        TextBoxStatusPerkawinan.Text,
                        TextBoxKewarganegaraan.Text,
                        TextBoxPendidikanTerakhir.Text,
                        TextBoxTanggalBekerja.Text.ToDateTime(),
                        TextBoxUsername.Text,
                        TextBoxPassword.Text,
                        TextBoxPIN.Text,
                        TextBoxCatatan.Text
                        );
                else if (ButtonSimpan.Text == "Ubah")
                    ClassPengguna.Ubah(
                        Request.QueryString["id"].ToInt(),
                        DropDownListGrupPengguna.SelectedValue.ToInt(),
                        DropDownListTempat.SelectedValue.ToInt(),
                        TextBoxNomorIdentitas.Text,
                        TextBoxNomorNPWP.Text,
                        TextBoxNomorRekening.Text,
                        TextBoxNamaBank.Text,
                        TextBoxNamaRekening.Text,
                        TextBoxNamaLengkap.Text,
                        TextBoxTempatLahir.Text,
                        TextBoxTanggalLahir.Text.ToDateTime(),
                        DropDownListGender.SelectedValue.ToBool(),
                        TextBoxAlamat.Text,
                        TextBoxAgama.Text,
                        TextBoxTelepon.Text,
                        TextBoxHandphone.Text,
                        TextBoxEmail.Text,
                        TextBoxStatusPerkawinan.Text,
                        TextBoxKewarganegaraan.Text,
                        TextBoxPendidikanTerakhir.Text,
                        TextBoxTanggalBekerja.Text.ToDateTime(),
                        TextBoxUsername.Text,
                        TextBoxPassword.Text,
                        TextBoxPIN.Text,
                        TextBoxCatatan.Text,
                        DropDownListStatus.SelectedValue.ToBool()
                        );

                db.SubmitChanges();

                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonGeneratePIN_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxPassword.Attributes.Add("value", TextBoxPassword.Text);
            TextBoxPIN.Text = Pengaturan.GeneratePIN(db);
        }
    }

    protected void TextBoxGaji_TextChanged(object sender, EventArgs e)
    {
        TextBoxGajiBersih.Text = (TextBoxGajiKotor.Text.ToDecimal() + TextBoxTambahanGaji.Text.ToDecimal() - TextBoxPotonganGaji.Text.ToDecimal()).ToFormatHarga();
    }
}