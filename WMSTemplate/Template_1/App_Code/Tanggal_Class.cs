using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public class Tanggal_Class
{
    public ListItem[] DropdownlistBulan()
    {
        //BULAN
        List<ListItem> ListBulan = new List<ListItem>();

        for (int i = 1; i <= 12; i++)
        {
            DateTime Tanggal = new DateTime(1991, i, 1);

            ListBulan.Add(new ListItem
            {
                Text = Tanggal.ToString("MMMM", new CultureInfo("id-ID")),
                Value = i.ToString(),
                Selected = DateTime.Now.Month == i
            });
        }

        return ListBulan.ToArray();
    }
    public ListItem[] DropdownlistTahun()
    {
        //TAHUN
        List<ListItem> ListTahun = new List<ListItem>();

        int Tahun = DateTime.Now.Year;

        for (int i = (Tahun - 5); i <= (Tahun + 5); i++)
        {
            ListTahun.Add(new ListItem
            {
                Text = i.ToString(),
                Value = i.ToString(),
                Selected = DateTime.Now.Year == i
            });
        }

        return ListTahun.ToArray();
    }

    public ListItem[] DropDownListHariBulan(int tahun, int bulan)
    {
        //HARI
        List<ListItem> ListHari = new List<ListItem>();

        int jumlahHari = DateTime.DaysInMonth(tahun, bulan);

        for (int i = 1; i <= jumlahHari; i++)
        {
            ListHari.Add(new ListItem
            {
                Text = i + " " + Pengaturan.Hari(new DateTime(tahun, bulan, i)),
                Value = i.ToString(),
                Selected = i == 1
            });
        }

        return ListHari.ToArray();
    }
}
