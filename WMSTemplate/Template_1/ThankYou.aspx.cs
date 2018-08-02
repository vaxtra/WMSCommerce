using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Transaksi = db.TBTransaksis
                    .FirstOrDefault(item => item._IDWMSTransaksi == Guid.Parse(Request.QueryString["order"]));

                if (Transaksi != null)
                {
                    var Pelanggan = Transaksi.TBPelanggan;
                    var Alamat = Transaksi.TBAlamat;
                    var TransaksiDetail = Transaksi.TBTransaksiDetails.Select(item => new {
                        item.IDDetailTransaksi,
                        Foto = "/images/cover/" + item.TBStokProduk.TBKombinasiProduk.IDProduk + ".jpg",
                        item.TBStokProduk.TBKombinasiProduk.Nama,
                        item.Quantity,
                        Harga = item.TBStokProduk.HargaJual,
                        Total = (item.Quantity * item.TBStokProduk.HargaJual),
                    });

                    LiteralEmail.Text = Pelanggan.Email;
                    LiteralIDTransaksi.Text = Transaksi.IDTransaksi;
                    LiteralNamaLengkap.Text = Pelanggan.NamaLengkap;
                    LiteralAlamat.Text = Alamat.AlamatLengkap;
                    LiteralKecamatan.Text = Alamat.Kecamatan;
                    LiteralKota.Text = Alamat.Kota;
                    LiteralProvinsi.Text = Alamat.Provinsi;
                    LiteralKodePos.Text = Alamat.KodePos;
                    LiteralNegara.Text = Alamat.Negara;

                    LiteralStatusPembayaran.Text = Transaksi.TBStatusTransaksi.Nama;

                    ListViewDetailTransaksi.DataSource = TransaksiDetail;
                    ListViewDetailTransaksi.DataBind();

                    LiteralBiayaPengiriman.Text = Transaksi.BiayaPengiriman.ToFormatHarga();
                    LiteralTotal.Text = Transaksi.Subtotal.ToFormatHarga();
                    LiteralSubotal.Text = Transaksi.GrandTotal.ToFormatHarga();
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}