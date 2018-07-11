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
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();

                var KombinasiProduk = KombinasiProduk_Class.Cari(db, Request.QueryString["id"].ToInt());

                List<dynamic> DaftarBarcode = new List<dynamic>();

                if (KombinasiProduk != null)
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["harga"]))
                    {
                        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                        for (int i = 0; i < 3; i++)
                            DaftarBarcode.Add(new
                            {
                                Nama = db.TBStokProduks.FirstOrDefault(item => item.IDKombinasiProduk == KombinasiProduk.IDKombinasiProduk && item.IDTempat == Pengguna.IDTempat).HargaJual.ToFormatHarga(),
                                Kode = KombinasiProduk.KodeKombinasiProduk
                            });
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                            DaftarBarcode.Add(new
                            {
                                Nama = (KombinasiProduk.Nama.Length > 30) ? KombinasiProduk.Nama.Substring(0, 29) : KombinasiProduk.Nama,
                                Kode = KombinasiProduk.KodeKombinasiProduk
                            });
                    }

                    RepeaterBarcode.DataSource = DaftarBarcode;
                    RepeaterBarcode.DataBind();
                }
                else if (!string.IsNullOrWhiteSpace(Request.QueryString["Nama"]) || !string.IsNullOrWhiteSpace(Request.QueryString["Kode"]))
                {
                    for (int i = 0; i < 3; i++)
                        DaftarBarcode.Add(new
                        {
                            Nama = (Request.QueryString["Nama"].Length > 30) ? Request.QueryString["Nama"].Substring(0, 29) : Request.QueryString["Nama"],
                            Kode = Request.QueryString["Kode"]
                        });

                    RepeaterBarcode.DataSource = DaftarBarcode;
                    RepeaterBarcode.DataBind();
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}