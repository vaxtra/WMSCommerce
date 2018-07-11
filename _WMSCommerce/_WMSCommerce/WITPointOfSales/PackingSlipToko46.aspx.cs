using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_PackingSlipToko46 : System.Web.UI.Page
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
                    #region TRANSAKSI PRINT LOG
                    TransaksiPrintLog_Class TransaksiPrintLog_Class = new TransaksiPrintLog_Class(db);
                    TransaksiPrintLog_Class.Tambah(EnumPrintLog.PackingSlip, Transaksi.IDTransaksi);
                    db.SubmitChanges();
                    #endregion

                    var Store = Transaksi.TBTempat.TBStore;
                    var Tempat = Transaksi.TBTempat;

                    LabelFooterPrint.Text = Transaksi.TBTempat.FooterPrint;

                    LabelNamaStore.Text = Store.Nama + " - " + Tempat.Nama;
                    LabelAlamatStore.Text = Tempat.Alamat;
                    LabelTeleponStore.Text = Tempat.Telepon1;
                    LabelWebsite.Text = Store.Website;
                    HyperLinkEmail.Text = Tempat.Email;
                    HyperLinkEmail.NavigateUrl = Tempat.Email;

                    LabelIDTransaksi.Text = Transaksi.IDTransaksi;
                    LabelTanggalTransaksi.Text = Pengaturan.FormatTanggal(Transaksi.TanggalTransaksi);
                    LabelNamaPelanggan.Text = Transaksi.TBPelanggan.NamaLengkap;

                    if (Transaksi.IDPelanggan != 1)
                    {
                        var Alamat = Transaksi.TBAlamat;

                        LabelAlamatPelanggan.Text = Alamat.AlamatLengkap;
                        LabelTeleponPelanggan.Text = Alamat.Handphone;
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
                            JumlahProduk = item.Quantity
                        })
                        .OrderBy(item => item.Nama);
                    RepeaterDetailTransaksi.DataBind();

                    LabelTotalQuantity.Text = Pengaturan.FormatHarga(TransaksiDetail.Sum(item => item.Quantity));

                    LabelKeterangan.Text = Transaksi.Keterangan;
                }
                else
                    Response.Redirect("Transaksi.aspx");
            }
        }
    }
}