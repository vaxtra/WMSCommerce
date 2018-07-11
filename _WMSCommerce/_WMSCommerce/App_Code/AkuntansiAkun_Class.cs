using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public class AkuntansiAkun_Class
{
    public TBAkuntansiAkun[] DataParent(DataClassesDatabaseDataContext db)
    {
        return db.TBAkuntansiAkuns.Where(item => !item.IDAkuntansiAkunParent.HasValue).ToArray();
    }

    public TBAkuntansiAkun Cari(DataClassesDatabaseDataContext db, int idAkun)
    {
        return db.TBAkuntansiAkuns.FirstOrDefault(item => item.IDAkuntansiAkun == idAkun);
    }

    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db, PilihanAkunTipe pilihanAkunTipe)
    {
        List<ListItem> ListAkun = new List<ListItem>();

        foreach (var item in db.TBAkuntansiAkuns.Where(item => item.IDAkuntansiAkunTipe == (int)pilihanAkunTipe && item.IDAkuntansiAkunParent.HasValue).ToArray())
        {
            ListAkun.Add(new ListItem
            {
                Text = item.Nomor + " - " + item.Nama,
                Value = item.IDAkuntansiAkun.ToString()
            });
        }

        return ListAkun.ToArray();
    }

    public ListItem[] DataDropDownList(DataClassesDatabaseDataContext db, int IDAkuntansiAkunParent)
    {
        List<ListItem> ListAkun = new List<ListItem>();

        foreach (var item in db.TBAkuntansiAkuns.Where(item => item.IDAkuntansiAkunParent == IDAkuntansiAkunParent).ToArray())
        {
            ListAkun.Add(new ListItem
            {
                Text = item.Nomor + " - " + item.Nama,
                Value = item.IDAkuntansiAkun.ToString()
            });
        }

        return ListAkun.ToArray();
    }

    public ListItem[] DataDropDownListAkunChild(DataClassesDatabaseDataContext db)
    {
        List<ListItem> ListAkun = new List<ListItem>();

        foreach (var item in db.TBAkuntansiAkuns
            .Where(item => item.IDAkuntansiAkunParent.HasValue)
            .OrderBy(item => item.IDAkuntansiAkunParent)
            .ThenBy(item => item.IDAkuntansiAkun)
            .ToArray())
        {
            ListAkun.Add(new ListItem
            {
                Text = item.Nomor + " - " + item.Nama,
                Value = item.IDAkuntansiAkun.ToString()
            });
        }

        return ListAkun.ToArray();
    }

    //public void DropDownListAkun(DataClassesDatabaseDataContext db, DropDownList dropDownList)
    //{
    //    dropDownList.DataSource = db.TBAkuntansiAkuns
    //         .Where(item => item.IDAkuntansiAkunParent.HasValue)
    //         .OrderBy(item => item.IDAkuntansiAkunParent)
    //         .ThenBy(item => item.IDAkuntansiAkun)
    //         .ToArray();
    //    dropDownList.DataValueField = "Nomor";
    //    dropDownList.DataTextField = "Nama";
    //    dropDownList.DataBind();
    //}
}
