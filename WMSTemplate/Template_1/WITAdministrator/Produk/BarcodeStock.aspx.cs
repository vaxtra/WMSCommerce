using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_BarcodeStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPrintBarcode PrintBarcode = db.TBPrintBarcodes.FirstOrDefault();

                if (PrintBarcode != null)
                {
                    StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                    var StokProduk = StokProduk_Class.Cari(PrintBarcode.IDStokProduk);
                    
                    //UBAH ANGKANYA setelah <= dan setelah : (SESUAI DENGAN JUMLAH BARIS YANG DIINGINKAN) 
                    //5 BARIS = 10 Stok/Barcode
                    int jumlahBaris = Math.Ceiling(PrintBarcode.Jumlah.ToDecimal() / 2).ToInt() <= 5 ? Math.Ceiling(PrintBarcode.Jumlah.ToDecimal() / 2).ToInt() : 5;
                    dynamic[] ListBarcode = new dynamic[jumlahBaris];
                    List<dynamic> ListBody = new List<dynamic>();

                    //JUMLAH BARCODE KE KANAN
                    for (int i = 0; i < 2; i++)
                        ListBody.Add(new
                        {
                            Nama = StokProduk.TBKombinasiProduk.TBProduk.Nama,
                            Kode = StokProduk.TBKombinasiProduk.KodeKombinasiProduk,
                            Varian = !string.IsNullOrWhiteSpace(StokProduk.TBKombinasiProduk.TBAtributProduk.Nama) ? "(" + StokProduk.TBKombinasiProduk.TBAtributProduk.Nama + ")" : "&nbsp;",
                            Warna = StokProduk.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                            Kategori = StokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? StokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            Harga = StokProduk.HargaJual.ToFormatHarga()
                        });


                    RepeaterBarcodeBarcode.DataSource = ListBarcode.Select(item => new
                    {
                        Body = ListBody
                    });
                    RepeaterBarcodeBarcode.DataBind();

                    //JANGAN DI UBAH ANGKANYA
                    PrintBarcode.Jumlah -= (jumlahBaris * 2);

                    if (PrintBarcode.Jumlah <= 0)
                    {
                        db.TBPrintBarcodes.DeleteOnSubmit(PrintBarcode);
                    }

                    db.SubmitChanges();
                }
                else
                    Response.Redirect("/WITWarehouse/Default.aspx");
            }

            TimerBarcode.Enabled = true;
        }
    }

    protected void TimerBarcode_Tick(object sender, EventArgs e)
    {
        Response.Redirect("BarcodeStock.aspx");
    }
}