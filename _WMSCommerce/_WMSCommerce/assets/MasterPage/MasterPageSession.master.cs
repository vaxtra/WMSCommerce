using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_MasterPage_MasterPageSession : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //CEK APAKAH SESSION ADA
        if (Session["PenggunaLogin"] == null)
        {
            if (Request.Cookies["WITEnterpriseSystem"] != null)
            {
                //JIKA COOKIES ADA
                PenggunaLogin Pengguna = new PenggunaLogin(Request.Cookies["WITEnterpriseSystem"].Value, true);

                if (Pengguna.IDPengguna > 0)
                {
                    Session["PenggunaLogin"] = Pengguna;
                    Session.Timeout = 525000;

                    //MEMBUAT COOKIES ENCRYPT
                    Response.Cookies["WITEnterpriseSystem"].Value = Pengguna.EnkripsiIDPengguna;
                    Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddYears(1);
                }
                else
                    Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=" + Request.RawUrl);
            }
            else
                Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=" + Request.RawUrl);
        }

        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        //LabelUsernamePengguna.Text = pengguna.Username;
        //LabelNamaTempat.Text = pengguna.Store + " - " + pengguna.Tempat;

        #region MENUBAR
        string path = Server.MapPath("~/App_Data/MenubarBeranda/") + pengguna.IDGrupPengguna + ".html";

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                //LiteralMenubar.Text = reader.ReadToEnd();
                reader.Dispose();
            }
        }
        #endregion

        Session.Timeout = 525000;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region VALIDASI KEY STORE
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                EnumAlert enumAlert;

                StoreKey_Class ClassStoreKey = new StoreKey_Class(db);

                ClassStoreKey.Validasi(out enumAlert);

                if (enumAlert == EnumAlert.danger)
                    Response.Redirect("/WITAdministrator/Login.aspx?do=logout");
            }
            #endregion
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            List<AlertMessage> ListAlertMessage = new List<AlertMessage>();

            foreach (var item in db.TBNotifikasis.Where(item => item.IDPenggunaPenerima == Pengguna.IDPengguna && !item.TanggalDibaca.HasValue).Take(5).ToArray())
            {
                ListAlertMessage.Add(new AlertMessage
                {
                    EnumAlert = (EnumAlert)item.EnumAlert,
                    Title = item.IDPenggunaPengirim != item.IDPenggunaPenerima ? item.TBPengguna.NamaLengkap : "",
                    Body = item.Isi
                });

                item.TanggalDibaca = DateTime.Now;
            }

            if (ListAlertMessage.Count > 0)
            {
                db.SubmitChanges();
                AlertMessage_Class.Show(this, ListAlertMessage);
            }
        }
    }
}
