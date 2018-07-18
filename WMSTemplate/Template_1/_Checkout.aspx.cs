using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

                var Data = db.TBPelanggans
                    .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

                if (Data != null)
                {
                    TextBoxNamaLengkap.Text = Data.NamaLengkap;
                    TextBoxAlamatEmail.Text = Data.Email;
                    TextBoxNomorTelepon.Text = Data.Handphone;

                    var Alamat = Data.TBAlamats
                        .OrderByDescending(item => item.IDAlamat)
                        .FirstOrDefault();

                    if (Alamat != null)
                    {
                        TextBoxAlamat.Text = Alamat.AlamatLengkap;
                        TextBoxKodePos.Text = Alamat.KodePos;
                    }

                    //MENCARI TRANSAKSI SESSION
                    var TransaksiECommerceDetail = db.TBTransaksiECommerceDetails
                        .Where(item => item.TBTransaksiECommerce.IDPelanggan == Pelanggan.IDPelanggan);

                    if (TransaksiECommerceDetail.Count() > 0)
                    {
                        RepeaterCart.DataSource = TransaksiECommerceDetail
                            .Select(item => new
                            {
                                item.IDTransaksiECommerceDetail,
                                Foto = "/images/cover/" + item.TBStokProduk.TBKombinasiProduk.IDProduk + ".jpg",
                                item.TBStokProduk.TBKombinasiProduk.Nama,
                                item.Quantity,
                                Harga = item.TBStokProduk.HargaJual,
                                Total = (item.Quantity * item.TBStokProduk.HargaJual)
                            })
                            .ToArray();
                        RepeaterCart.DataBind();
                    }
                    else
                        Response.Redirect("Cart.aspx");
                }
                else
                    Response.Redirect("Cart.aspx");
            }
        }
    }

    protected void ButtonKembaliKeKeranjangBelanja_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cart.aspx");
    }

    protected void ButtonLanjutkanKePengiriman_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

            var Data = db.TBPelanggans
                .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

            if (Data != null)
            {
                Data.NamaLengkap = TextBoxNamaLengkap.Text;
                Data.Email = TextBoxAlamatEmail.Text;
                Data.Handphone = TextBoxNomorTelepon.Text;
                //Data.IDGrupPelanggan
                Data.Username = TextBoxAlamatEmail.Text;

                var Alamat = Data.TBAlamats
                    .OrderByDescending(item => item.IDAlamat)
                    .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(Alamat.AlamatLengkap))
                {
                    Alamat.NamaLengkap = TextBoxNamaLengkap.Text;
                    Alamat.AlamatLengkap = TextBoxAlamat.Text;
                    Alamat.KodePos = TextBoxKodePos.Text;
                    Alamat.Handphone = TextBoxNomorTelepon.Text;
                }
                else
                {
                    Data.TBAlamats.Add(new TBAlamat
                    {
                        NamaLengkap = TextBoxNamaLengkap.Text,
                        AlamatLengkap = TextBoxAlamat.Text,
                        KodePos = TextBoxKodePos.Text,
                        Handphone = TextBoxNomorTelepon.Text
                    });
                }

                db.SubmitChanges();
            }
            else
                Response.Redirect("Cart.aspx");

            //LOAD DATA PELANGGAN

            LiteralNamaLengkap.Text = TextBoxNamaLengkap.Text;
            LiteralAlamat.Text = TextBoxAlamat.Text;
            //LiteralKecamatan.Text = DropDownListKecamatan.SelectedItem.Text;
            //LiteralKota.Text = DropDownListKota.SelectedItem.Text;
            //LiteralProvinsi.Text = DropDownListProvinsi.SelectedItem.Text;
            LiteralKodePos.Text = TextBoxKodePos.Text;
            //LiteralNegara.Text = DropDownListNegara.SelectedItem.Text;
            LiteralNomorTelepon.Text = TextBoxNomorTelepon.Text;
        }
    }

    protected void ButtonKembaliKeInformasiPelanggan_Click(object sender, EventArgs e)
    {

    }

    protected void ButtonLanjutkanKePembayaran_Click(object sender, EventArgs e)
    {

    }

    protected void ButtonKembaliKeDetailPengiriman_Click(object sender, EventArgs e)
    {

    }

    protected void ButtonProsesPemesanan_Click(object sender, EventArgs e)
    {

    }
}