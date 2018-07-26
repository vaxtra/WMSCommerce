using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Email_Laporan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Generate();
    }

    private void Generate()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            string Email = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == 7).Pengaturan;
            string path = Server.MapPath("HappyHoliday.html");

            string Store = db.TBStores.FirstOrDefault().Nama;

            string Judul = "Happy Holiday  - " + Store;
            Guid IDWMSStore = Guid.NewGuid();

            string body = "";

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            #region KIRIM EMAIL
            foreach (var item in Email.Replace(" ", "").Split(','))
            {
                db.TBPengirimanEmails.InsertOnSubmit(new TBPengirimanEmail
                {
                    Judul = Judul,
                    TanggalKirim = DateTime.Now,
                    Tujuan = item,
                    Isi = body.Replace("{Logo}", "<img src='" + "http://wit.systems/Logo.aspx?IDWMSStore=" + IDWMSStore + "&IDWMSEmail=" + Guid.NewGuid() + "&EmailPenerima=" + item + "&Judul=" + Judul + "' height=\"35\" />")
                });
            }
            #endregion

            db.SubmitChanges();

            Response.Write("Success");
        }
    }
}