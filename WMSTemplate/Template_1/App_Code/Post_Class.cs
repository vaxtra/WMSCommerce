using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Post_Class
/// </summary>
public class Post_Class
{
    private DataClassesDatabaseDataContext db;
    public Post_Class(DataClassesDatabaseDataContext db)
    {
        this.db = db;
    }

    #region CRUD
    public TBPost[] GetAll()
    {
        return db.TBPosts.ToArray();
    }

    public TBPost GetData(int ID)
    {
        return db.TBPosts.FirstOrDefault(item => item.IDPost == ID);
    }

    public void InsertData(TBPost Data)
    {
        db.TBPosts.InsertOnSubmit(Data);
    }

    public TBPost InsertData(int IDPage, int IDPengguna, int Urutan, DateTime Tanggal, string Judul, string Deskripsi, string Align, string Tags)
    {
        TBPost Data = new TBPost()
        {
            IDPage = IDPage,
            IDPengguna = IDPengguna,
            Urutan = Urutan,
            Tanggal = Tanggal,
            Judul = Judul,
            Deskripsi = Deskripsi,
            Align = Align,
            Tags = Tags
        };
        db.TBPosts.InsertOnSubmit(Data);

        return Data;
    }

    public void InsertAllData(TBPost[] AllData)
    {
        db.TBPosts.InsertAllOnSubmit(AllData);
    }

    public void DeleteData(TBPost Data)
    {
        db.TBPosts.DeleteOnSubmit(Data);
    }

    public void DeleteData(int ID)
    {
        db.TBPosts.DeleteOnSubmit(db.TBPosts.FirstOrDefault(item => item.IDPost == ID));
    }

    public void DeleteAllData(TBPost[] AllData)
    {
        db.TBPosts.DeleteAllOnSubmit(AllData);
    }
    #endregion

    public void DropDownList(DropDownList DropDownList, string InsertIndexAwal)
    {
        DropDownList.DataSource = GetAll();
        DropDownList.DataValueField = "IDPost";
        DropDownList.DataTextField = "Nama";
        DropDownList.DataBind();

        if (!string.IsNullOrEmpty(InsertIndexAwal))
            DropDownList.Items.Insert(0, new ListItem { Value = "0", Text = InsertIndexAwal });
    }

    public void DataListBox(ListBox ListBox)
    {
        ListBox.DataSource = GetAll();
        ListBox.DataValueField = "IDPost";
        ListBox.DataTextField = "Nama";
        ListBox.DataBind();
    }
}