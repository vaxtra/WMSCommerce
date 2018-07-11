using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_BarcodeChecked : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListProduk.DataSource = db.TBProduks;
                DropDownListProduk.DataTextField = "Nama";
                DropDownListProduk.DataValueField = "IDProduk";
                DropDownListProduk.DataBind();
                DropDownListProduk.Items.Insert(0, new ListItem { Text = "-Pilih Produk-", Value = "0" });

                ViewState["ViewStateListDetail"] = new List<StokProduk_Model>();
            }
        }
    }

    private void LoadData()
    {
        List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

        RepeaterDetail.DataSource = ViewStateListDetail.GroupBy(item => item.IDProduk).Select(item => new
        {
            item.FirstOrDefault().Produk,
            IDProduk = item.Key,
            Body = item.Select(item2 => new
            {
                item2.Atribut,
                item2.Jumlah
            }),
            CountDetail = item.Count()
        });
        RepeaterDetail.DataBind();
    }

    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        if (DropDownListProduk.SelectedValue != "0")
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBStokProduk[] StokProduk = db.TBStokProduks.Where(item => item.IDTempat == pengguna.IDTempat && item.TBKombinasiProduk.IDProduk == DropDownListProduk.SelectedValue.ToInt()).ToArray();

                foreach (var item in StokProduk)
                {
                    if (item.Jumlah > 0)
                    {
                        ViewStateListDetail.Add(new StokProduk_Model
                        {
                            IDProduk = item.TBKombinasiProduk.IDProduk,
                            IDStokProduk = item.IDStokProduk,
                            Produk = item.TBKombinasiProduk.TBProduk.Nama,
                            KombinasiProduk = item.TBKombinasiProduk.Nama,
                            Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
                            Jumlah = item.Jumlah.Value
                        });
                    }
                }
            }

            ViewState["ViewStateListDetail"] = ViewStateListDetail;

            LoadData();
        }
    }

    protected void RepeaterDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];
            ViewStateListDetail.RemoveAll(item => item.IDProduk == e.CommandArgument.ToInt());
            ViewState["ViewStateListDetail"] = ViewStateListDetail;
        }

        LoadData();
    }

    protected void ButtonCetak_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.TBPrintBarcodes.DeleteAllOnSubmit(db.TBPrintBarcodes);
            db.SubmitChanges();

            List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

            db.TBPrintBarcodes.InsertAllOnSubmit(ViewStateListDetail.Select(item => new TBPrintBarcode
            {
                IDStokProduk = item.IDStokProduk,
                Jumlah = item.Jumlah
            }));

            db.SubmitChanges();
        }

        Response.Redirect("BarcodeStock.aspx");
    }
}