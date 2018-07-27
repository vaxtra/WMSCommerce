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

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                List<dynamic> ListBarcode = new List<dynamic>();

                if (KombinasiProduk != null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ListBarcode.Add(new
                        {
                            Nama = (KombinasiProduk.Nama.Length > 22) ? KombinasiProduk.Nama.Substring(0, 21) : KombinasiProduk.Nama,
                            Kode = KombinasiProduk.KodeKombinasiProduk,
                            Harga = KombinasiProduk.TBStokProduks.FirstOrDefault(item => item.IDKombinasiProduk == KombinasiProduk.IDKombinasiProduk).HargaJual.ToFormatHarga()
                        });
                    }

                    RepeaterBarcode.DataSource = ListBarcode;
                    RepeaterBarcode.DataBind();
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}