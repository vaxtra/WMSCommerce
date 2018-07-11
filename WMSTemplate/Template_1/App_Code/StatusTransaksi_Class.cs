using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public class StatusTransaksi_Class
{
    public TBStatusTransaksi[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBStatusTransaksis.OrderBy(item => item.Urutan).ToArray();
    }
    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db)
    {
        List<ListItem> ListData = new List<ListItem>();

        ListData.Add(new ListItem { Value = "0", Text = "- Semua Status Transaksi -" });

        ListData.AddRange(Data(db).Select(item => new ListItem
        {
            Value = item.IDStatusTransaksi.ToString(),
            Text = item.Nama
        }));

        return ListData.ToArray();
    }
    public void DataListBox(DataClassesDatabaseDataContext db, ListBox listBox)
    {
        listBox.DataSource = Data(db);
        listBox.DataValueField = "IDStatusTransaksi";
        listBox.DataTextField = "Nama";
        listBox.DataBind();
    }
    public TBStatusTransaksi Cari(DataClassesDatabaseDataContext db, int idStatusTransaksi)
    {
        return db.TBStatusTransaksis.FirstOrDefault(item => item.IDStatusTransaksi == idStatusTransaksi);
    }
}
