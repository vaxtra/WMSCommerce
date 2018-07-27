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
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    for (int i = 0; i < 2; i++)
                        DaftarBarcode.Add(new
                        {
                            Nama = KombinasiProduk.Nama,
                            Kode = KombinasiProduk.KodeKombinasiProduk,
                            Harga = db.TBStokProduks.FirstOrDefault(item => item.IDKombinasiProduk == KombinasiProduk.IDKombinasiProduk && item.IDTempat == Pengguna.IDTempat).HargaJual.ToFormatHarga()
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