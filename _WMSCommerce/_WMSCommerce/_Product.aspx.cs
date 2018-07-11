using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Produk_Class ClassProduk = new Produk_Class(db, true);

                var Produk = ClassProduk.Cari(Request.QueryString["id"].ToInt());

                if (Produk == null)
                    Response.Redirect("_Default.aspx");

                LiteralHarga.Text = Produk.TBKombinasiProduks.FirstOrDefault().TBStokProduks.FirstOrDefault(item2 => item2.IDTempat == 1).HargaJual.ToFormatHarga();
                LiteralNama.Text = Produk.Nama;
                LiteralDeskripsi.Text = Produk.Deskripsi;

                var Foto = Produk.TBFotoProduks.ToArray();

                RepeaterFoto.DataSource = Foto.Select(item => new
                {
                    Foto = "/images/Produk/" + item.IDFotoProduk + item.ExtensiFoto,
                    item.FotoUtama,
                });
                RepeaterFoto.DataBind();

                var StokProduk = db.TBStokProduks
                    .Where(item =>
                        item.TBKombinasiProduk.IDProduk == Request.QueryString["id"].ToInt() &&
                        item.IDTempat == 1 &&
                        item.Jumlah >= 0)
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.TBKombinasiProduk.TBAtributProduk.Nama
                    });

                DropDownListStokProduk.DataSource = StokProduk;
                DropDownListStokProduk.DataValueField = "IDStokProduk";
                DropDownListStokProduk.DataTextField = "Nama";
                DropDownListStokProduk.DataBind();
            }
        }
    }

    protected void ButtonAddToCart_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

            //MENCARI TRANSAKSI SESSION
            var TransaksiECommerce = db.TBTransaksiECommerces
                .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

            if (TransaksiECommerce == null)
            {
                TransaksiECommerce = new TBTransaksiECommerce
                {
                    //IDTransaksiECommerce
                    IDPelanggan = Pelanggan.IDPelanggan,
                    _IDWMSPelanggan = Pelanggan.IDWMSPelanggan,
                    _TanggalInsert = DateTime.Now
                };

                db.TBTransaksiECommerces.InsertOnSubmit(TransaksiECommerce);
            }

            TransaksiECommerce.TBTransaksiECommerceDetails.Add(new TBTransaksiECommerceDetail
            {
                TBTransaksiECommerce = TransaksiECommerce,
                //IDTransaksiECommerceDetail
                IDStokProduk = DropDownListStokProduk.SelectedValue.ToInt(),
                Quantity = TextBoxQuantity.Text.ToInt(),
                _TanggalInsert = DateTime.Now
            });

            db.SubmitChanges();
        }

        Response.Redirect("_Cart.aspx");

        //PENGATURAN PELANGGAN DI TRANSAKSI
        //PengaturanPelanggan(Pelanggan.IDPelanggan);

        //ClassTransaksi Transaksi = new ClassTransaksi(1, 1, DateTime.Now);

        //Transaksi.TambahDetailTransaksi(DropDownListKombinasiProduk.SelectedValue.ToInt(), TextBoxQuantity.Text.ToInt());

        //Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
        //Transaksi.StatusPrint = true;

        //Transaksi.ConfirmTransaksi(db);
        //db.SubmitChanges();
    }

    protected void ButtonRemove_Click(object sender, EventArgs e)
    {
        //MENGHAPUS COOKIES
        Response.Cookies["WMSCommerce"].Value = string.Empty;
        Response.Cookies["WMSCommerce"].Expires = DateTime.Now.AddDays(-1);
    }

    protected void ButtonRemoveSession_Click(object sender, EventArgs e)
    {
        //MENGHAPUS SESSION
        Session.Abandon();
    }
}