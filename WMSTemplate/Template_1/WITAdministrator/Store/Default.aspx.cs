using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Store_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Store_Class ClassStore = new Store_Class(db);

                var Store = ClassStore.Data();

                if (Store != null)
                {
                    TextBoxNama.Text = Store.Nama;
                    TextBoxAlamat.Text = Store.Alamat;
                    TextBoxEmail.Text = Store.Email;
                    TextBoxKodePos.Text = Store.KodePos;
                    TextBoxHandphone.Text = Store.Handphone;
                    TextBoxTeleponLain.Text = Store.TeleponLain;
                    TextBoxWebsite.Text = Store.Website;
                    TextBoxSMTPServer.Text = Store.SMTPServer;
                    TextBoxSMTPPort.Text = Store.SMTPPort.ToString();
                    TextBoxSMTPUser.Text = Store.SMTPUser;
                    TextBoxSMTPPassword.Text = Store.SMTPPassword;
                    CheckBoxSecureSocketsLayer.Checked = Store.SecureSocketsLayer.Value;

                    ButtonSimpan.Text = "Ubah";
                }
                else
                    ButtonSimpan.Text = "Tambah";
            }

            ImageLogo.ImageUrl = "~/images/logo.jpg";
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Store_Class ClassStore = new Store_Class(db);

            ClassStore.Ubah(TextBoxNama.Text, TextBoxAlamat.Text, TextBoxEmail.Text, TextBoxKodePos.Text, TextBoxHandphone.Text, TextBoxTeleponLain.Text, TextBoxWebsite.Text, TextBoxSMTPServer.Text, TextBoxSMTPPort.Text.ToInt(), TextBoxSMTPUser.Text, TextBoxSMTPPassword.Text, CheckBoxSecureSocketsLayer.Checked);

            db.SubmitChanges();
        }
    }

    protected void ButtonPercobaanEmail_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Store_Class ClassStore = new Store_Class(db);

            var Store = ClassStore.Data();

            try
            {
                Pengaturan.KirimEmail(TextBoxSMTPServer.Text, TextBoxSMTPPort.Text.ToInt(), TextBoxSMTPUser.Text, TextBoxSMTPPassword.Text, CheckBoxSecureSocketsLayer.Checked, true, TextBoxEmail.Text, "WIT. Management System", TextBoxPercobaanEmail.Text, "Test Email - WIT Enterprise System", "This is a test message, your server is now available to send email");
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "mengirim test email");
            }
            catch (Exception ex)
            {
                LogError_Class LogError_Class = new LogError_Class(ex, "ButtonPercobaanEmail_Click");
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "mengirim test email gagal");
            }
        }
    }

    protected void ButtonUpload_Command(object sender, CommandEventArgs e)
    {
        warning.Visible = false;

        using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(FileUploadLogo.PostedFile.InputStream))
        {
            if (Path.GetExtension(FileUploadLogo.FileName) == ".jpg")
            {
                if (myImage.Height == 115 && myImage.Width == 380)
                {
                    string path = Server.MapPath("~/images/logo.jpg");
                    FileInfo file = new FileInfo(path);

                    if (file.Exists)
                        file.Delete();

                    FileUploadLogo.SaveAs(Server.MapPath("~/Images/") + "logo.jpg");
                }
                else
                {
                    warning.Visible = true;
                    LabeWarningLogo.Text = "Ukuran image salah. Ukuran height 115px dan width 380px";
                }
            }
            else
            {
                warning.Visible = true;
                LabeWarningLogo.Text = "Format image harus .jpg";
            }

        }

        ImageLogo.ImageUrl = "~/images/logo.jpg";
    }
}