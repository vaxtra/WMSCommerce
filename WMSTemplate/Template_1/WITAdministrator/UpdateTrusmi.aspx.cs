using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_UpdateTrusmi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //DateTime tanggal = DateTime.Now.Date.AddDays(-1).AddHours(12);
            //LabelTanggal.Text = tanggal.ToFormatTanggalJam();
            //RepeaterData.DataSource = db.TBPenerimaanPOProduksiProdukDetails.Where(item => item.TBPenerimaanPOProduksiProduk.TanggalTerima.Value >= tanggal.Date && item.TBPenerimaanPOProduksiProduk.TanggalTerima.Value <= tanggal).Select(item => new
            //{
            //    item.TBPenerimaanPOProduksiProduk.TanggalTerima,
            //    Tanggal = item.TBPenerimaanPOProduksiProduk.TanggalTerima.ToFormatTanggalJam(),
            //    Tempat = item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.TBTempat.Nama,
            //    Pengguna = item.TBPenerimaanPOProduksiProduk.TBPengguna1.NamaLengkap,
            //    KodeKombinasiProduk = item.TBKombinasiProduk.KodeKombinasiProduk,
            //    Produk = item.TBKombinasiProduk.TBProduk.Nama,
            //    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
            //    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.TBKombinasiProduk),
            //    Jumlah = item.Diterima.ToFormatHargaBulat()
            //}).OrderByDescending(item => item.TanggalTerima).ToArray();
            //RepeaterData.DataBind();
        }
    }

    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {

            try
            {
                #region UPDATE HARGA BELI PERPINDAHAN STOK BAHAN BAKU
                //TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.ToArray();

                //foreach (var item in db.TBPerpindahanStokBahanBakus)
                //{
                //    item.HargaBeli = daftarStokBahanBaku.FirstOrDefault(data => data.IDStokBahanBaku == item.IDStokBahanBaku).HargaBeli.Value;
                //}
                #endregion

                #region TAMBAH DATA PENGIRIMAN BAHAN BAKU DI PRODUKSI BAHAN BAKU
                //TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = db.TBPOProduksiBahanBakus.Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.PurchaseOrder && item.EnumStatusProduksi > (int)PilihanEnumStatusPOProduksi.Baru && item.TBPOProduksiBahanBakuKomposisis.Sum(data => data.Kirim) > 0).ToArray();

                //foreach (var poProduksi in daftarPOProduksiBahanBaku)
                //{
                //    string IDPengirimanPOProduksiBahanBaku = string.Empty;

                //    db.Proc_InsertPengirimanPOProduksiBahanBaku(ref IDPengirimanPOProduksiBahanBaku, poProduksi.IDPOProduksiBahanBaku, (poProduksi.IDSupplier != null ? poProduksi.IDSupplier : 0), poProduksi.IDTempat, poProduksi.IDPenggunaProses, poProduksi.TanggalProses);

                //    TBPengirimanPOProduksiBahanBaku pengiriman = db.TBPengirimanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPengirimanPOProduksiBahanBaku == IDPengirimanPOProduksiBahanBaku);

                //    pengiriman.TBPOProduksiBahanBaku = poProduksi;
                //    pengiriman.TBPengirimanPOProduksiBahanBakuDetails.AddRange(poProduksi.TBPOProduksiBahanBakuKomposisis.Select(item => new TBPengirimanPOProduksiBahanBakuDetail
                //    {
                //        TBBahanBaku = item.TBBahanBaku,
                //        TBSatuan = item.TBSatuan,
                //        HargaBeli = item.HargaBeli,
                //        Kirim = item.Kirim
                //    }));
                //    pengiriman.Grandtotal = pengiriman.TBPengirimanPOProduksiBahanBakuDetails.Sum(item => (item.Kirim * item.HargaBeli));
                //    pengiriman.Keterangan = null;
                //}
                #endregion

                #region TAMBAH DATA PENGIRIMAN BAHAN BAKU DI PRODUKSI PRODUK
                //TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.PurchaseOrder && item.EnumStatusProduksi > (int)PilihanEnumStatusPOProduksi.Baru && item.TBPOProduksiProdukKomposisis.Sum(data => data.Kirim) > 0).ToArray();

                //foreach (var poProduksi in daftarPOProduksiProduk)
                //{
                //    string IDPengirimanPOProduksiProduk = string.Empty;

                //    db.Proc_InsertPengirimanPOProduksiProduk(ref IDPengirimanPOProduksiProduk, poProduksi.IDPOProduksiProduk, (poProduksi.IDVendor != null ? poProduksi.IDVendor : 0), poProduksi.IDTempat, poProduksi.IDPenggunaProses, poProduksi.TanggalProses);

                //    TBPengirimanPOProduksiProduk pengiriman = db.TBPengirimanPOProduksiProduks.FirstOrDefault(item => item.IDPengirimanPOProduksiProduk == IDPengirimanPOProduksiProduk);

                //    pengiriman.TBPOProduksiProduk = poProduksi;
                //    pengiriman.TBPengirimanPOProduksiProdukDetails.AddRange(poProduksi.TBPOProduksiProdukKomposisis.Select(item => new TBPengirimanPOProduksiProdukDetail
                //    {
                //        TBBahanBaku = item.TBBahanBaku,
                //        TBSatuan = item.TBSatuan,
                //        HargaBeli = item.HargaBeli,
                //        Kirim = item.Kirim
                //    }));
                //    pengiriman.Grandtotal = pengiriman.TBPengirimanPOProduksiProdukDetails.Sum(item => (item.Kirim * item.HargaBeli));
                //    pengiriman.Keterangan = null;
                //}
                #endregion

                #region TAMBAH DATA PEMABAYARAN PO DARI TOTAL BAYAR
                //TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = db.TBPOProduksiBahanBakus.Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && item.TotalBayar > 0).ToArray();

                //foreach (var item in daftarPOProduksiBahanBaku)
                //{
                //    db.TBPOProduksiBahanBakuJenisPembayarans.InsertOnSubmit(new TBPOProduksiBahanBakuJenisPembayaran
                //    {
                //        TBPOProduksiBahanBaku = item,
                //        IDPengguna = item.IDPenggunaSelesai != null ? item.IDPenggunaSelesai.Value : (item.IDPenggunaProses != null ? item.IDPenggunaProses.Value : item.IDPenggunaPending),
                //        IDJenisPembayaran = 1,
                //        Tanggal = item.TanggalSelesai != null ? item.TanggalSelesai.Value : (item.TanggalProses != null ? item.TanggalProses.Value : item.TanggalPending),
                //        Bayar = item.TotalBayar,
                //        Keterangan = null
                //    });
                //}

                //TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && item.TotalBayar > 0).ToArray();

                //foreach (var item in daftarPOProduksiProduk)
                //{
                //    db.TBPOProduksiProdukJenisPembayarans.InsertOnSubmit(new TBPOProduksiProdukJenisPembayaran
                //    {
                //        TBPOProduksiProduk = item,
                //        IDPengguna = item.IDPenggunaSelesai != null ? item.IDPenggunaSelesai.Value : (item.IDPenggunaProses != null ? item.IDPenggunaProses.Value : item.IDPenggunaPending),
                //        IDJenisPembayaran = 1,
                //        Tanggal = item.TanggalSelesai != null ? item.TanggalSelesai.Value : (item.TanggalProses != null ? item.TanggalProses.Value : item.TanggalPending),
                //        Bayar = item.TotalBayar,
                //        Keterangan = null
                //    });
                //}
                #endregion

                #region UPDATE IDPENGGUNAPIC & IDPENGGUNATERIMA + TANGGAL TERIMA
                //foreach (var item in db.TBPOProduksiBahanBakus.ToArray())
                //{
                //    item.IDPenggunaPIC = item.IDPenggunaPending;
                //}

                //foreach (var item in db.TBPOProduksiProduks.ToArray())
                //{
                //    item.IDPenggunaPIC = item.IDPenggunaPending;
                //}

                //foreach (var item in db.TBPenerimaanPOProduksiBahanBakus.ToArray())
                //{
                //    item.IDPenggunaTerima = item.IDPenggunaDatang;
                //    item.TanggalTerima = item.TanggalDatang;
                //    item.EnumStatusPenerimaan = (int)PilihanEnumStatusPenerimaanPO.Terima;
                //}

                //foreach (var item in db.TBPenerimaanPOProduksiProduks.ToArray())
                //{
                //    item.IDPenggunaTerima = item.IDPenggunaDatang;
                //    item.TanggalTerima = item.TanggalDatang;
                //    item.EnumStatusPenerimaan = (int)PilihanEnumStatusPenerimaanPO.Terima;
                //}
                #endregion

                DateTime tanggal = DateTime.Now.Date.AddDays(-1).AddHours(12);
                var hasil = db.TBPenerimaanPOProduksiProdukDetails.Where(item => item.TBPenerimaanPOProduksiProduk.TanggalTerima.Value >= tanggal.Date && item.TBPenerimaanPOProduksiProduk.TanggalTerima.Value <= tanggal).Select(item => new
                {
                    item.IDKombinasiProduk,
                    item.TBPenerimaanPOProduksiProduk.TanggalTerima,
                    Jumlah = item.Diterima
                }).OrderByDescending(item => item.TanggalTerima).ToArray();

                TBStokProduk[] daftarStokProduk = db.TBStokProduks.Where(item => item.IDTempat == 1).ToArray();

                int index = 0;
                foreach (var item in hasil)
                {
                    TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk);
                    stokProduk.Jumlah = stokProduk.Jumlah.Value - item.Jumlah;
                    index++;
                }

                db.SubmitChanges();

                LabelNotif.Text = "sukses " + index.ToString();
                LabelNotif.Visible = true;
            }
            catch (Exception)
            {
                LabelNotif.Text = "gagal";
                LabelNotif.Visible = true;
            } 
        }
    }
}