using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_LoginPIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region LOGOUT
            //menghapus session
            Session.Abandon();

            //MENGHAPUS COOKIES
            Response.Cookies["WITEnterpriseSystem"].Value = string.Empty;
            Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddDays(-1);
            #endregion

            //COKIES TIDAK ADA ATAU VALUE TIDAK ADA MAKA REDIRECT KE HALAMAN LOGIN PIN
            if (Request.Cookies["WMSLogin"] == null || string.IsNullOrWhiteSpace(Request.Cookies["WMSLogin"].Value))
                Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=/WITAdministrator/LoginPIN.aspx");
            else
            {
                //JIKA VALUE COOKIES ADA MELAKUKAN ENCRYPT
                string[] value = EncryptDecrypt.Decrypt(Request.Cookies["WMSLogin"].Value).Split('|');

                //AMBIL VALUE TANGGAL DAN TANGGAL HARI INI HARUS LEBIH KECIL DARI VALUE COOKIES
                if (DateTime.Now <= value[0].ToDateTime())
                {
                    TextBoxPIN.Focus();
                }
                else //COOKIES EXPIRED
                    Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=/WITAdministrator/LoginPIN.aspx");
            }
        }
    }
    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = new PenggunaLogin(TextBoxPIN.Text);

        if (Pengguna.IDPengguna > 0)
        {
            if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
            {
                Session["PenggunaLogin"] = Pengguna;
                Session.Timeout = 525000;

                //MEMBUAT COOKIES ENCRYPT
                Response.Cookies["WITEnterpriseSystem"].Value = Pengguna.EnkripsiIDPengguna;
                Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddYears(1);

                Response.Redirect("/WITRestaurantV2/Default.aspx");
            }
            else
                Response.Redirect("/WITAdministrator/Login.aspx");
        }
        else
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "PIN salah");

            TextBoxPIN.Text = string.Empty;
            TextBoxPIN.Focus();
        }
    }    
}