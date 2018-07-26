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

            TextBoxStoreKey.Focus();
        }
    }

    protected void ButtonVerifikasi_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(TextBoxStoreKey.Text))
                throw new Exception("Store Key harus diisi");

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StoreKey_Class ClassStoreKey = new StoreKey_Class(db, true);

                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, ClassStoreKey.Verifikasi(TextBoxStoreKey.Text));

                #region GENERATE STORE KEY
                ClassStoreKey.Generate();
                #endregion

                TextBoxStoreKey.Text = "";
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
        }
    }
}