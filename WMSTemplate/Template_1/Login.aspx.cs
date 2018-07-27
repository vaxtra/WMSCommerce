using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region GENERATE STORE KEY
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StoreKey_Class ClassStoreKey = new StoreKey_Class(db, true);

                ClassStoreKey.Generate();

                EnumAlert enumAlert = ClassStoreKey.Validasi();

                if (enumAlert == EnumAlert.danger)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ClassStoreKey.MessageDanger);
                else if (enumAlert == EnumAlert.warning)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, ClassStoreKey.MessageWarning);
                else
                    LiteralWarning.Text = "";
            }
            #endregion

            TextBoxUsername.Focus();

            if (Request.QueryString["do"] == "logout")
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                if (Pengguna != null)
                {
                    //menambah LogPengguna tipe Logout : 2
                    LogPengguna.Tambah(2, Pengguna.IDPengguna);

                    //MENGHAPUS SESSION
                    Session.Abandon();

                    //MENGHAPUS COOKIES
                    Response.Cookies["WITEnterpriseSystem"].Value = string.Empty;
                    Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddDays(-1);

                    if (Pengguna.PointOfSales == TipePointOfSales.Retail) //RETAIL KEMBALI KE HALAMAN LOGIN
                    {
                        Response.Cookies["WMSLogin"].Value = string.Empty;
                        Response.Cookies["WMSLogin"].Expires = DateTime.Now.AddDays(-1);
                    }
                    else if (Pengguna.PointOfSales == TipePointOfSales.Restaurant) //RESTAURANT KEMBALI KE HALAMAN LOGIN PIN
                    {
                        //JIKA VALUE COOKIES ADA MELAKUKAN ENCRYPT
                        string[] value = EncryptDecrypt.Decrypt(Request.Cookies["WMSLogin"].Value).Split('|');

                        //AMBIL VALUE TANGGAL DAN TANGGAL HARI INI HARUS LEBIH KECIL DARI VALUE COOKIES
                        if (DateTime.Now <= value[0].ToDateTime())
                            Response.Redirect("LoginPIN.aspx");
                    }
                }
            }
            else
                Redirect();
        }
    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = new PenggunaLogin(TextBoxUsername.Text, TextBoxPassword.Text);

            if (Pengguna.IDPengguna > 0)
            {
                Session["PenggunaLogin"] = Pengguna;
                Session.Timeout = 525000;

                #region VALIDASI KEY STORE
                EnumAlert enumAlert;

                StoreKey_Class ClassStoreKey = new StoreKey_Class(db);

                var Pesan = ClassStoreKey.Validasi(out enumAlert);
                #endregion

                if (enumAlert == EnumAlert.danger)
                {
                    //MENGHAPUS SESSION
                    Session.Abandon();

                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, Pesan);
                }
                else
                {
                    //MEMBUAT COOKIES ENCRYPT
                    Response.Cookies["WITEnterpriseSystem"].Value = Pengguna.EnkripsiIDPengguna;
                    Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddYears(1);

                    Response.Cookies["WMSLogin"].Value = EncryptDecrypt.Encrypt(DateTime.Now.AddYears(1) + "|" + Pengguna.IDPengguna);
                    Response.Cookies["WMSLogin"].Expires = DateTime.Now.AddYears(1);

                    Redirect();
                }
            }
            else
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Username atau password salah");
        }
    }

    private void Redirect()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        if (Pengguna != null)
        {
            if (Request.QueryString["returnUrl"] != null)
                Response.Redirect(Request.QueryString["returnUrl"]);
            else
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var GrupPengguna = db.TBGrupPenggunas.FirstOrDefault(item => item.IDGrupPengguna == Pengguna.IDGrupPengguna);

                    if (GrupPengguna != null && !string.IsNullOrWhiteSpace(GrupPengguna.DefaultURL))
                        Response.Redirect(GrupPengguna.DefaultURL);
                    else
                        Response.Redirect("/WITAdministrator/Default.aspx");
                }
            }
        }
    }
}