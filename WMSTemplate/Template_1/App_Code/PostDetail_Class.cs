using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PostDetail_Class
/// </summary>
public class PostDetail_Class
{
    private DataClassesDatabaseDataContext db;
    public PostDetail_Class(DataClassesDatabaseDataContext db)
    {
        this.db = db;
    }

    #region CRUD
    public TBPostDetail[] GetAll()
    {
        return db.TBPostDetails.ToArray();
    }

    public TBPostDetail GetData(int ID)
    {
        return db.TBPostDetails.FirstOrDefault(item => item.IDPostDetail == ID);
    }

    public void InsertData(TBPostDetail Data)
    {
        db.TBPostDetails.InsertOnSubmit(Data);
    }

    public TBPostDetail InsertData(int IDPost, int IDPengguna, int Urutan, DateTime Tanggal, string Nama, int Jenis, string Konten)
    {
        TBPostDetail Data = new TBPostDetail()
        {
            IDPost = IDPost,
            IDPengguna = IDPengguna,
            Urutan = Urutan,
            Tanggal = Tanggal,
            Nama = Nama,
            Jenis = Jenis,
            Konten = Konten
        };
        db.TBPostDetails.InsertOnSubmit(Data);

        return Data;
    }

    public void InsertAllData(TBPostDetail[] AllData)
    {
        db.TBPostDetails.InsertAllOnSubmit(AllData);
    }

    public void DeleteData(TBPostDetail Data)
    {
        db.TBPostDetails.DeleteOnSubmit(Data);
    }

    public void DeleteData(int ID)
    {
        db.TBPostDetails.DeleteOnSubmit(db.TBPostDetails.FirstOrDefault(item => item.IDPostDetail == ID));
    }

    public void DeleteAllData(TBPostDetail[] AllData)
    {
        db.TBPostDetails.DeleteAllOnSubmit(AllData);
    }
    #endregion

    public void DropDownList(DropDownList DropDownList, string InsertIndexAwal)
    {
        DropDownList.DataSource = GetAll();
        DropDownList.DataValueField = "IDPostDetail";
        DropDownList.DataTextField = "Nama";
        DropDownList.DataBind();

        if (!string.IsNullOrEmpty(InsertIndexAwal))
            DropDownList.Items.Insert(0, new ListItem { Value = "0", Text = InsertIndexAwal });
    }

    public void DataListBox(ListBox ListBox)
    {
        ListBox.DataSource = GetAll();
        ListBox.DataValueField = "IDPostDetail";
        ListBox.DataTextField = "Nama";
        ListBox.DataBind();
    }
}