using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Atribut_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    private void LoadData()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

                RepeaterProdukKategori.DataSource = ClassProdukKategori
                    .Data()
                    .Where(item => item.IDProdukKategori > 1)
                    .Select(item => new
                    {
                        item.IDProdukKategori,
                        KategoriUtama = item.IDProdukKategoriParent.HasValue ? item.TBProdukKategori1.Nama : "",
                        item.Nama,
                        item.Deskripsi,
                        IsActive = item._IsActive
                    });
                RepeaterProdukKategori.DataBind();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void RepeaterProdukKategori_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);

                if (e.CommandName == "Hapus")
                {
                    ClassProdukKategori.Hapus(e.CommandArgument.ToInt());
                    LoadData();
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}