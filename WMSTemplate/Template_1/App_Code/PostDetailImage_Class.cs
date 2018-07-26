using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PostDetailImage_Class
/// </summary>
public class PostDetailImage_Class
{
    private DataClassesDatabaseDataContext db;
    public PostDetailImage_Class(DataClassesDatabaseDataContext db)
    {
        this.db = db;
    }

    #region CRUD
    public TBPostDetailImage[] GetAll()
    {
        return db.TBPostDetailImages.ToArray();
    }

    public TBPostDetailImage GetData(int ID)
    {
        return db.TBPostDetailImages.FirstOrDefault(item => item.IDPostDetailImage == ID);
    }

    public void InsertData(TBPostDetailImage Data)
    {
        db.TBPostDetailImages.InsertOnSubmit(Data);
    }

    public TBPostDetailImage InsertData(int IDPostDetail, int Urutan, string DefaultURL, string Judul, string Deskripsi, string Link, string Alt)
    {
        TBPostDetailImage Data = new TBPostDetailImage()
        {
            IDPostDetail = IDPostDetail,
            Urutan = Urutan,
            DefaultURL = DefaultURL,
            Judul = Judul,
            Deskripsi = Deskripsi,
            Link = Link,
            Alt = Alt
        };
        db.TBPostDetailImages.InsertOnSubmit(Data);

        return Data;
    }

    public TBPostDetailImage InsertData(TBPostDetail TBPostDetail, int Urutan, string DefaultURL, string Judul, string Deskripsi, string Link, string Alt)
    {
        TBPostDetailImage Data = new TBPostDetailImage()
        {
            TBPostDetail = TBPostDetail,
            Urutan = Urutan,
            DefaultURL = DefaultURL,
            Judul = Judul,
            Deskripsi = Deskripsi,
            Link = Link,
            Alt = Alt
        };
        db.TBPostDetailImages.InsertOnSubmit(Data);

        return Data;
    }

    public void InsertAllData(TBPostDetailImage[] AllData)
    {
        db.TBPostDetailImages.InsertAllOnSubmit(AllData);
    }

    public void DeleteData(TBPostDetailImage Data)
    {
        db.TBPostDetailImages.DeleteOnSubmit(Data);
    }

    public void DeleteData(int ID)
    {
        db.TBPostDetailImages.DeleteOnSubmit(db.TBPostDetailImages.FirstOrDefault(item => item.IDPostDetailImage == ID));
    }

    public void DeleteAllData(TBPostDetailImage[] AllData)
    {
        db.TBPostDetailImages.DeleteAllOnSubmit(AllData);
    }
    #endregion

    public void DropDownList(DropDownList DropDownList, string InsertIndexAwal)
    {
        DropDownList.DataSource = GetAll();
        DropDownList.DataValueField = "IDPostDetailImage";
        DropDownList.DataTextField = "Nama";
        DropDownList.DataBind();

        if (!string.IsNullOrEmpty(InsertIndexAwal))
            DropDownList.Items.Insert(0, new ListItem { Value = "0", Text = InsertIndexAwal });
    }

    public void DataListBox(ListBox ListBox)
    {
        ListBox.DataSource = GetAll();
        ListBox.DataValueField = "IDPostDetailImage";
        ListBox.DataTextField = "Nama";
        ListBox.DataBind();
    }
}