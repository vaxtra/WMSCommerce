using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PageTemplate_Class
/// </summary>
public class PageTemplate_Class
{
    private DataClassesDatabaseDataContext db;
    public PageTemplate_Class(DataClassesDatabaseDataContext db)
    {
        this.db = db;
    }

    #region CRUD
    public TBPageTemplate[] GetAll()
    {
        return db.TBPageTemplates.ToArray();
    }

    public TBPageTemplate GetData(int ID)
    {
        return db.TBPageTemplates.FirstOrDefault(item => item.IDPageTemplate == ID);
    }

    public void InsertData(TBPageTemplate Data)
    {
        db.TBPageTemplates.InsertOnSubmit(Data);
    }

    public TBPageTemplate InsertData(string Nama, string DefaultURL)
    {
        TBPageTemplate Data = new TBPageTemplate()
        {
            Nama = Nama,
            DefaultURL = DefaultURL
        };
        db.TBPageTemplates.InsertOnSubmit(Data);

        return Data;
    }

    public void InsertAllData(TBPageTemplate[] AllData)
    {
        db.TBPageTemplates.InsertAllOnSubmit(AllData);
    }

    public void DeleteData(TBPageTemplate Data)
    {
        db.TBPageTemplates.DeleteOnSubmit(Data);
    }

    public void DeleteData(int ID)
    {
        db.TBPageTemplates.DeleteOnSubmit(db.TBPageTemplates.FirstOrDefault(item => item.IDPageTemplate == ID));
    }

    public void DeleteAllData(TBPageTemplate[] AllData)
    {
        db.TBPageTemplates.DeleteAllOnSubmit(AllData);
    }
    #endregion

    public void DropDownList(DropDownList DropDownList, string InsertIndexAwal)
    {
        DropDownList.DataSource = GetAll();
        DropDownList.DataValueField = "IDPageTemplate";
        DropDownList.DataTextField = "Nama";
        DropDownList.DataBind();

        if (!string.IsNullOrEmpty(InsertIndexAwal))
            DropDownList.Items.Insert(0, new ListItem { Value = "0", Text = InsertIndexAwal });
    }

    public void DataListBox(ListBox ListBox)
    {
        ListBox.DataSource = GetAll();
        ListBox.DataValueField = "IDPageTemplate";
        ListBox.DataTextField = "Nama";
        ListBox.DataBind();
    }
}