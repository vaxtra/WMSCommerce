using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_Maintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext();
        var ListKonfigurasiStore = db.TBStoreKonfigurasis.Where(item => item.IDStoreKonfigurasi == 1 || item.IDStoreKonfigurasi == 2);

        string KonfigurasiJamBuka = ListKonfigurasiStore.FirstOrDefault(item => item.IDStoreKonfigurasi == 1).Pengaturan;
        string KonfigurasiJamTutup = ListKonfigurasiStore.FirstOrDefault(item => item.IDStoreKonfigurasi == 2).Pengaturan;

        DateTime TanggalTransaksi = new DateTime(2015, 1, 1, 6, 0, 0);

        for (int i = 1; i <= 50; i++)
        {
            TimeSpan JamBuka = TimeSpan.Parse(KonfigurasiJamBuka);
            TimeSpan JamTutup = TimeSpan.Parse(KonfigurasiJamTutup);

            if (JamBuka > JamTutup)
            {
                //JIKA JAM BUKA LEBIH BESAR DARI JAM TUTUP : GANTI HARI
                //JIKA TRANSAKSI LEBIH BESAR DARI JAM BUKA DAN TANGGAL TRANSAKSI SUDAH LEWAT 00:00 MAKA HARI MUNDUR
                if (TanggalTransaksi.TimeOfDay < JamBuka && TanggalTransaksi.TimeOfDay >= TimeSpan.Parse("00:00"))
                    Response.Write(TanggalTransaksi.ToFormatTanggal() + " - " + TanggalTransaksi.Date.AddDays(-1).ToFormatTanggal() + "<br/>");
                else
                    Response.Write(TanggalTransaksi.ToFormatTanggal() + " - " + TanggalTransaksi.Date.ToFormatTanggal() + "<br/>");
            }
            else
                Response.Write(TanggalTransaksi.ToFormatTanggal() + " - " + TanggalTransaksi.Date.ToFormatTanggal() + "<br/>"); //JAM BUKA DAN JAM TUTUP PADA HARI YANG SAMA

            TanggalTransaksi = TanggalTransaksi.AddHours(1);
        }
    }

    private bool Tanggal(DateTime TanggalTransaksi, TimeSpan JamBuka, TimeSpan JamTutup)
    {
        // convert everything to TimeSpan
        // see if start comes before end
        if (JamBuka < JamTutup)
            return JamBuka <= TanggalTransaksi.TimeOfDay && TanggalTransaksi.TimeOfDay <= JamTutup;
        // start is after end, so do the inverse comparison
        return !(JamTutup < TanggalTransaksi.TimeOfDay && TanggalTransaksi.TimeOfDay < JamBuka);
    }
}