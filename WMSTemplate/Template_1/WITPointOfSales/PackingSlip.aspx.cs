using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_NewInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Transaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == Request.QueryString["id"]);
                if (Transaksi != null)
                {
                    if (Transaksi.TBTempat1.TBKategoriTempat.IDKategoriTempat == 7)
                        PanelLogo.Visible = false;
                    else
                        PanelLogo.Visible = true;

                    var Store = Transaksi.TBTempat.TBStore;
                    var Tempat = Transaksi.TBTempat;

                    LabelFooterPrint.Text = Transaksi.TBTempat.FooterPrint;
                    
                    LabelIDTransaksi.Text = Transaksi.IDTransaksi;
                    LabelTanggalTransaksi.Text = Pengaturan.FormatTanggal(Transaksi.TanggalTransaksi);
                    LabelNamaPelanggan.Text = "TO : " + Transaksi.TBPelanggan.NamaLengkap;

                    if (Transaksi.IDPelanggan > 1)
                    {
                        var Alamat = Transaksi.TBPelanggan.TBAlamats.FirstOrDefault();

                        if (Alamat != null)
                        {
                            LabelAlamatPelanggan.Text = Alamat.AlamatLengkap;
                            LabelTeleponPelanggan.Text = Alamat.Handphone;
                        }
                    }

                    LabelJenisPembayaran.Text = Transaksi.TBJenisPembayaran.Nama;
                    LabelStatusTransaksi.Text = Transaksi.TBStatusTransaksi.Nama;

                    var TransaksiDetail = Transaksi.TBTransaksiDetails.ToArray();

                    RepeaterDetailTransaksi.DataSource = TransaksiDetail
                        .Select(item => new
                        {
                            Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
                            Nama = item.TBKombinasiProduk.Nama,
                            Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            item.Quantity
                        })
                        .OrderBy(item => item.Nama);
                    RepeaterDetailTransaksi.DataBind();

                    LabelTotalQuantity.Text = Pengaturan.FormatHarga(TransaksiDetail.Sum(item => item.Quantity));

                    //LabelKeterangan.Text = Transaksi.Keterangan + "<br/>" + Transaksi.TBTempat1.Nama +
                    //     " / " + Transaksi.TBTempat1.Telepon1; 

                    LabelKeterangan.Text = Transaksi.TBTempat1.Nama +
                         " / " + Transaksi.TBTempat1.Telepon1;
                }
                else
                    Response.Redirect("Transaksi.aspx");
            }
        }
    }
}