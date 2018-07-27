using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Page_Class
/// </summary>
public class Page_Class
{
    private DataClassesDatabaseDataContext db;
    public Page_Class(DataClassesDatabaseDataContext db)
    {
        this.db = db;
    }

    #region CRUD
    public TBPage[] GetAll()
    {
        return db.TBPages.ToArray();
    }

    public TBPage GetData(int ID)
    {
        return db.TBPages.FirstOrDefault(item => item.IDPage == ID);
    }

    public void InsertData(TBPage Data)
    {
        db.TBPages.InsertOnSubmit(Data);
    }

    public TBPage InsertData(int IDPageTemplate, string Nama, string Deskripsi)
    {
        TBPage Data = new TBPage()
        {
            IDPageTemplate = IDPageTemplate,
            Nama = Nama,
            Deskripsi = Deskripsi
        };
        db.TBPages.InsertOnSubmit(Data);

        return Data;
    }

    public void InsertAllData(TBPage[] AllData)
    {
        db.TBPages.InsertAllOnSubmit(AllData);
    }

    public void DeleteData(TBPage Data)
    {
        db.TBPages.DeleteOnSubmit(Data);
    }

    public void DeleteData(int ID)
    {
        db.TBPages.DeleteOnSubmit(db.TBPages.FirstOrDefault(item => item.IDPage == ID));
    }

    public void DeleteAllData(TBPage[] AllData)
    {
        db.TBPages.DeleteAllOnSubmit(AllData);
    }
    #endregion

    public void DropDownList(DropDownList DropDownList, string InsertIndexAwal)
    {
        DropDownList.DataSource = GetAll();
        DropDownList.DataValueField = "IDPage";
        DropDownList.DataTextField = "Nama";
        DropDownList.DataBind();

        if (!string.IsNullOrEmpty(InsertIndexAwal))
            DropDownList.Items.Insert(0, new ListItem { Value = "0", Text = InsertIndexAwal });
    }

    public void DataListBox(ListBox ListBox)
    {
        ListBox.DataSource = GetAll();
        ListBox.DataValueField = "IDPage";
        ListBox.DataTextField = "Nama";
        ListBox.DataBind();
    }
}