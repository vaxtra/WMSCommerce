using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public class JenisTransaksi_Class
{
    public TBJenisTransaksi[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBJenisTransaksis.OrderBy(item => item.IDJenisTransaksi).ToArray();
    }
    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db)
    {
        List<ListItem> DataList = new List<ListItem>();

        DataList.Add(new ListItem
        {
            Value = "0",
            Text = "- Semua Jenis Transaksi -"
        });

        DataList.AddRange(Data(db).Select(item => new ListItem
        {
            Value = item.IDJenisTransaksi.ToString(),
            Text = item.Nama
        }));

        return DataList.ToArray();
    }
    public void DataListBox(DataClassesDatabaseDataContext db, ListBox listBox)
    {
        listBox.DataSource = Data(db);
        listBox.DataValueField = "IDJenisTransaksi";
        listBox.DataTextField = "Nama";
        listBox.DataBind();
    }
    public TBJenisTransaksi Cari(DataClassesDatabaseDataContext db, int idJenisTransaksi)
    {
        return db.TBJenisTransaksis.FirstOrDefault(item => item.IDJenisTransaksi == idJenisTransaksi);
    }
}
