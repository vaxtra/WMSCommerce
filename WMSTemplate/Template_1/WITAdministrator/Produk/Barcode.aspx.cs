using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Barcode : System.Web.UI.Page
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
                var StokProduk = StokProduk_Class.Cari(Pengguna.IDTempat, Parse.Int(Request.QueryString["id"]));

                if (StokProduk != null)
                {
                    PanelKeterangan.Visible = Parse.Int(Request.QueryString["jumlah"]) > 0;

                    if (PanelKeterangan.Visible)
                    {
                        var jumlah = Parse.Decimal(Request.QueryString["jumlah"]);
                        var jumlahPrint = Math.Ceiling(jumlah / 3);
                        var tidakTerpakai = (jumlahPrint * 3) - jumlah;

                        LabelJumlahPrint.Text = Pengaturan.FormatHarga(jumlahPrint);
                        LabelTidakTerpakai.Text = Pengaturan.FormatHarga(tidakTerpakai);
                    }

                    List<dynamic> ListBarcode = new List<dynamic>();

                    for (int i = 0; i < 3; i++)
                        ListBarcode.Add(new
                        {
                            Nama = StokProduk.TBKombinasiProduk.TBProduk.Nama,
                            Kode = StokProduk.TBKombinasiProduk.KodeKombinasiProduk,
                            Varian = !string.IsNullOrWhiteSpace(StokProduk.TBKombinasiProduk.TBAtributProduk.Nama) ? "(" + StokProduk.TBKombinasiProduk.TBAtributProduk.Nama + ")" : "&nbsp;",
                            Warna = StokProduk.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                            Kategori = StokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? StokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            Harga = Pengaturan.FormatHarga(StokProduk.HargaJual)
                        });

                    RepeaterBarcode2.DataSource = ListBarcode;
                    RepeaterBarcode2.DataBind();
                }
                else
                    Response.Redirect("/WITWarehouse/Default.aspx");
            }
        }
    }
}