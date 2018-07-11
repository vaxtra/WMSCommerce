using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class JenisPerpindahanStok_Class
{
    public TBJenisPerpindahanStok[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBJenisPerpindahanStoks.OrderBy(item => item.Nama).ToArray();
    }
    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db)
    {
        List<ListItem> JenisPerpindahanStok = new List<ListItem>();

        JenisPerpindahanStok.Add(new ListItem { Value = "0", Text = "- Semua Perpindahan -" });

        JenisPerpindahanStok.AddRange(Data(db).Select(item => new ListItem
        {
            Value = item.IDJenisPerpindahanStok.ToString(),
            Text = item.Nama
        }));

        return JenisPerpindahanStok.ToArray();
    }
}