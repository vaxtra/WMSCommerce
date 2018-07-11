using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LogError_Class
{
    private int idBlackBox;
    public int IdBlackBox
    {
        get
        {
            return idBlackBox;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="halaman">Request.Url.PathAndQuery</param>
    public LogError_Class(Exception ex, string halaman)
    {
        try
        {
            if (!ex.Message.StartsWith("[WMS Error] "))
            {
                string Pesan = "";

                if (ex.InnerException != null)
                    Pesan += ex.InnerException.Message + " " + ex.InnerException.StackTrace;
                else
                    Pesan += ex.Message + " " + ex.StackTrace;

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var BlackBox = new TBBlackBox
                    {
                        Tanggal = DateTime.Now,
                        Pesan = Pesan,
                        Halaman = halaman
                    };

                    db.TBBlackBoxes.InsertOnSubmit(BlackBox);

                    db.SubmitChanges();

                    idBlackBox = BlackBox.IDBlackBox;
                }
            }
        }
        catch (Exception)
        {
        }
    }
}