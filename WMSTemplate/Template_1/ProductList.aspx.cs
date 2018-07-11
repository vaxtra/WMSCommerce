using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Produk = db.TBProduks.Select(item => new
            {
                item.IDProduk,
                item.Nama,
                Foto = "/images/cover/" + item.IDProduk + ".jpg",
                Harga = item.TBKombinasiProduks.FirstOrDefault().TBStokProduks.FirstOrDefault(item2 => item2.IDTempat == 1).HargaJual
            }).ToArray();

            RepeaterProduk.DataSource = Produk;
            RepeaterProduk.DataBind();

        }
    }
}