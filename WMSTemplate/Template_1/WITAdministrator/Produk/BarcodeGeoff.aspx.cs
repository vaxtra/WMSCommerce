using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_BarcodeGeoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["PenggunaLogin"] == null)
            {
                Response.Redirect("/WITWarehouse/Default.aspx");
                return;
            }

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
                var StokProduk = StokProduk_Class.Cari(Pengguna.IDTempat, Request.QueryString["id"].ToInt());

                if (StokProduk != null)
                {
                    //PanelKeterangan.Visible = Request.QueryString["jumlah"].ToInt() > 0;

                    //if (PanelKeterangan.Visible)
                    //{
                    //    var jumlah = Request.QueryString["jumlah"].ToDecimal();
                    //    var jumlahPrint = Math.Ceiling(jumlah / 3);
                    //    var tidakTerpakai = (jumlahPrint * 3) - jumlah;

                    //    LabelJumlahPrint.Text = jumlahPrint.ToFormatHargaBulat();
                    //    LabelTidakTerpakai.Text = tidakTerpakai.ToFormatHargaBulat();
                    //}

                    List<dynamic> ListBarcode = new List<dynamic>();

                    for (int i = 0; i < 1; i++)
                        ListBarcode.Add(new
                        {
                            Nama = StokProduk.TBKombinasiProduk.TBProduk.Nama,
                            Kode = StokProduk.TBKombinasiProduk.KodeKombinasiProduk,
                            Varian = !string.IsNullOrWhiteSpace(StokProduk.TBKombinasiProduk.TBAtributProduk.Nama) ? StokProduk.TBKombinasiProduk.TBAtributProduk.Nama : "-",
                            Warna = StokProduk.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                            Kategori = StokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? StokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            Harga = StokProduk.HargaJual.ToFormatHarga()
                        });

                    RepeaterBarcode.DataSource = ListBarcode;
                    RepeaterBarcode.DataBind();
                }
                else
                    Response.Redirect("/WITWarehouse/Default.aspx");
            }
        }
    }
}