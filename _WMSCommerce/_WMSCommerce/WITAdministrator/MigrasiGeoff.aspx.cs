using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_MigrasiGeoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            string status = string.Empty;
            try
            {
                status = "Harga Supplier & Vendor";
                #region Harga Supplier & Vendor
                TBHargaSupplier[] daftarHargaSupplier = db.TBHargaSuppliers.OrderByDescending(item => item.Tanggal).ToArray();

                foreach (var item in daftarHargaSupplier.GroupBy(item => new { item.IDSupplier, item.IDStokBahanBaku }))
                {
                    db.TBHargaSuppliers.DeleteAllOnSubmit(daftarHargaSupplier.Where(item2 => item2.IDSupplier == item.Key.IDSupplier && item2.IDStokBahanBaku == item.Key.IDStokBahanBaku).Skip(1));
                }

                TBHargaVendor[] daftarHargaVendor = db.TBHargaVendors.OrderByDescending(item => item.Tanggal).ToArray();

                foreach (var item in daftarHargaVendor.GroupBy(item => new { item.IDVendor, item.IDStokProduk }))
                {
                    db.TBHargaVendors.DeleteAllOnSubmit(daftarHargaVendor.Where(item2 => item2.IDVendor == item.Key.IDVendor && item2.IDStokProduk == item.Key.IDStokProduk).Skip(1));
                }

                db.SubmitChanges();
                #endregion

                status = "POBahanBaku";
                #region PO Bahan Baku
                //TBPOBahanBaku[] daftarPOBahanBaku = db.TBPOBahanBakus.ToArray();
                //TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = new TBPOProduksiBahanBaku[daftarPOBahanBaku.Count()];
                //foreach (var data in daftarPOBahanBaku)
                //{
                //    string IDPOProduksiBahanBaku = string.Empty;

                //    db.Proc_InsertPOProduksiBahanBaku(ref IDPOProduksiBahanBaku, data.IDTempat, data.IDPengguna, (int)PilihanEnumJenisProduksi.PurchaseOrder, data.TanggalPO);

                //    TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);

                //    poProduksiBahanBaku.IDProyeksi = null;
                //    poProduksiBahanBaku.IDSupplier = data.IDSupplier;

                //    foreach (var data2 in data.TBPenerimaanPOBahanBakus.ToArray())
                //    {
                //        string IDPenerimaanPOProduksiBahanBaku = string.Empty;

                //        db.Proc_InsertPenerimaanPOProduksiBahanBaku(ref IDPenerimaanPOProduksiBahanBaku, poProduksiBahanBaku.IDPOProduksiBahanBaku, poProduksiBahanBaku.IDSupplier, poProduksiBahanBaku.IDTempat, data2.IDPenggunaPenerima, data2.TanggalPenerimaan);

                //        TBPenerimaanPOProduksiBahanBaku penerimaan = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPenerimaanPOProduksiBahanBaku == IDPenerimaanPOProduksiBahanBaku);

                //        penerimaan.IDPenggunaTerima = penerimaan.IDPenggunaDatang;
                //        penerimaan.TanggalTerima = penerimaan.TanggalDatang;
                //        penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.AddRange(data2.TBPenerimaanPOBahanBakuDetails.Select(item => new TBPenerimaanPOProduksiBahanBakuDetail
                //        {
                //            IDBahanBaku = item.IDBahanBaku.Value,
                //            IDSatuan = item.IDSatuan.Value,
                //            BiayaTambahan = 0,
                //            HargaPokokKomposisi = 0,
                //            TotalHPP = 0,
                //            HargaSupplier = item.HargaSupplier.Value,
                //            PotonganHargaSupplier = item.PotonganHargaSupplier.Value,
                //            TotalHargaSupplier = item.HargaSupplier.Value + item.PotonganHargaSupplier.Value,
                //            Datang = item.Datang.Value,
                //            Diterima = item.Diterima.Value,
                //            TolakKeSupplier = item.Ditolak.Value,
                //            TolakKeGudang = 0,
                //            Sisa = item.Sisa.Value
                //        }));
                //        penerimaan.TotalDatang = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.Datang);
                //        penerimaan.TotalDiterima = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.Diterima);
                //        penerimaan.TotalTolakKeSupplier = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TolakKeSupplier);
                //        penerimaan.TotalTolakKeGudang = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TolakKeGudang);
                //        penerimaan.TotalSisa = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.Sisa);
                //        penerimaan.SubtotalBiayaTambahan = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.BiayaTambahan * item.Diterima);
                //        penerimaan.SubtotalTotalHPP = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TotalHPP * item.Diterima);
                //        penerimaan.SubtotalTotalHargaSupplier = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TotalHargaSupplier * item.Diterima);
                //        penerimaan.Grandtotal = penerimaan.SubtotalTotalHPP + penerimaan.SubtotalTotalHargaSupplier;
                //        penerimaan.EnumStatusPenerimaan = (int)PilihanEnumStatusPenerimaanPO.Terima;
                //        penerimaan.Keterangan = data2.Keterangan;
                //    }
                //    db.SubmitChanges();


                //    poProduksiBahanBaku.IDPenggunaProses = data.IDPenggunaUpdate;
                //    poProduksiBahanBaku.IDPenggunaSelesai = data.IDPenggunaUpdate;
                //    poProduksiBahanBaku.IDPenggunaPIC = data.IDPengguna;
                //    poProduksiBahanBaku.IDPenggunaRevisi = null;
                //    poProduksiBahanBaku.IDJenisPOProduksi = null;
                //    poProduksiBahanBaku.LevelProduksi = null;
                //    poProduksiBahanBaku.TanggalProses = data.TanggalUpdate;
                //    poProduksiBahanBaku.TanggalSelesai = data.TanggalUpdate;
                //    poProduksiBahanBaku.TanggalRevisi = null;
                //    poProduksiBahanBaku.TanggalJatuhTempo = data.TanggalJatuhTempo;
                //    poProduksiBahanBaku.TanggalPengiriman = data.TanggalTerima;

                //    poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.AddRange(data.TBPOBahanBakuDetails.Select(item => new TBPOProduksiBahanBakuDetail
                //    {
                //        IDBahanBaku = item.IDBahanBaku.Value,
                //        IDSatuan = item.IDSatuan.Value,
                //        HargaPokokKomposisi = 0,
                //        BiayaTambahan = 0,
                //        TotalHPP = 0,
                //        HargaSupplier = item.HargaSupplier.Value,
                //        PotonganHargaSupplier = item.PotonganHargaSupplier.Value,
                //        TotalHargaSupplier = item.HargaSupplier.Value + item.PotonganHargaSupplier.Value,
                //        Jumlah = item.Jumlah.Value,
                //        JumlahRevisi = item.Jumlah.Value,
                //        Sisa = item.Jumlah.Value - HitungSisaPesanan(db, item)
                //    }));

                //    poProduksiBahanBaku.TotalJumlah = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => item.Jumlah);
                //    poProduksiBahanBaku.TotalJumlahRevisi = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => item.JumlahRevisi);
                //    poProduksiBahanBaku.EnumJenisHPP = (int)PilihanEnumJenisHPP.HargaSupplierVendor;
                //    poProduksiBahanBaku.SubtotalBiayaTambahan = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.JumlahRevisi * item.BiayaTambahan));
                //    poProduksiBahanBaku.SubtotalTotalHPP = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.JumlahRevisi * item.TotalHPP));
                //    poProduksiBahanBaku.SubtotalTotalHargaSupplier = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.JumlahRevisi * item.TotalHargaSupplier));
                //    poProduksiBahanBaku.PotonganPOProduksiBahanBaku = data.PotonganPO;
                //    poProduksiBahanBaku.BiayaLainLain = data.BiayaLainLain;
                //    poProduksiBahanBaku.PersentaseTax = 0;

                //    decimal subtotal = poProduksiBahanBaku.SubtotalTotalHargaSupplier.Value + poProduksiBahanBaku.BiayaLainLain.Value - poProduksiBahanBaku.PotonganPOProduksiBahanBaku.Value;
                //    poProduksiBahanBaku.Tax = subtotal * poProduksiBahanBaku.PersentaseTax;
                //    poProduksiBahanBaku.Grandtotal = subtotal + poProduksiBahanBaku.Tax;


                //    poProduksiBahanBaku.TBPOProduksiBahanBakuJenisPembayarans.AddRange(data.TBPembayaranPOBahanBakus.Select(item => new TBPOProduksiBahanBakuJenisPembayaran
                //    {
                //        IDPengguna = item.IDPengguna.Value,
                //        IDJenisPembayaran = 1,
                //        Tanggal = item.Tanggal,
                //        Bayar = item.Bayar,
                //        Keterangan = item.Keterangan
                //    }));

                //    poProduksiBahanBaku.TotalBayar = poProduksiBahanBaku.TBPOProduksiBahanBakuJenisPembayarans.Sum(item => item.Bayar);

                //    if (poProduksiBahanBaku.TBPenerimaanPOProduksiBahanBakus.Count == 0 && poProduksiBahanBaku.TBPengirimanPOProduksiBahanBakus.Count == 0 && poProduksiBahanBaku.TBPOProduksiBahanBakuJenisPembayarans.Count == 0)
                //    {
                //        poProduksiBahanBaku.EnumStatusProduksi = (int)PilihanEnumStatusPOProduksi.Baru;
                //    }
                //    else
                //        poProduksiBahanBaku.EnumStatusProduksi = (int)PilihanEnumStatusPOProduksi.Proses;

                //    if (poProduksiBahanBaku.TotalJumlahRevisi == poProduksiBahanBaku.TBPenerimaanPOProduksiBahanBakus.Sum(item => item.TotalDiterima + item.TotalTolakKeGudang) && poProduksiBahanBaku.Grandtotal == poProduksiBahanBaku.TotalBayar && poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.Sum(item => item.Sisa) == 0)
                //    {
                //        poProduksiBahanBaku.EnumStatusProduksi = (int)PilihanEnumStatusPOProduksi.Selesai;
                //    }

                //    poProduksiBahanBaku.StatusRevisi = false;
                //    poProduksiBahanBaku.Keterangan = data.Keterangan;
                //}
                #endregion

                status = "PO Produk";
                #region PO Produk
                //TBPOProduk[] daftarPOProduk = db.TBPOProduks.ToArray();
                //TBPOProduksiProduk[] daftarPOProduksiProduk = new TBPOProduksiProduk[daftarPOProduk.Count()];
                //foreach (var data in daftarPOProduk)
                //{
                //    string IDPOProduksiProduk = string.Empty;

                //    db.Proc_InsertPOProduksiProduk(ref IDPOProduksiProduk, data.IDTempat, data.IDPengguna, (int)PilihanEnumJenisProduksi.PurchaseOrder, data.TanggalPO);

                //    TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);

                //    poProduksiProduk.IDProyeksi = null;
                //    poProduksiProduk.IDVendor = data.IDVendor;

                //    foreach (var data2 in data.TBPenerimaanPOProduks.ToArray())
                //    {
                //        string IDPenerimaanPOProduksiProduk = string.Empty;

                //        db.Proc_InsertPenerimaanPOProduksiProduk(ref IDPenerimaanPOProduksiProduk, poProduksiProduk.IDPOProduksiProduk, poProduksiProduk.IDVendor, poProduksiProduk.IDTempat, data2.IDPenggunaPenerima, data2.TanggalPenerimaan);

                //        TBPenerimaanPOProduksiProduk penerimaan = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item => item.IDPenerimaanPOProduksiProduk == IDPenerimaanPOProduksiProduk);

                //        penerimaan.IDPenggunaTerima = penerimaan.IDPenggunaDatang;
                //        penerimaan.TanggalTerima = penerimaan.TanggalDatang;
                //        penerimaan.TBPenerimaanPOProduksiProdukDetails.AddRange(data2.TBPenerimaanPOProdukDetails.Select(item => new TBPenerimaanPOProduksiProdukDetail
                //        {
                //            IDKombinasiProduk = item.IDKombinasiProduk.Value,
                //            BiayaTambahan = 0,
                //            HargaPokokKomposisi = 0,
                //            TotalHPP = 0,
                //            HargaVendor = item.HargaVendor.Value,
                //            PotonganHargaVendor = item.PotonganHargaVendor.Value,
                //            TotalHargaVendor = item.HargaVendor.Value + item.PotonganHargaVendor.Value,
                //            Datang = item.Datang.Value,
                //            Diterima = item.Diterima.Value,
                //            TolakKeVendor = item.Ditolak.Value,
                //            TolakKeGudang = 0,
                //            Sisa = item.Sisa.Value
                //        }));
                //        penerimaan.TotalDatang = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.Datang);
                //        penerimaan.TotalDiterima = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.Diterima);
                //        penerimaan.TotalTolakKeVendor = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TolakKeVendor);
                //        penerimaan.TotalTolakKeGudang = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TolakKeGudang);
                //        penerimaan.TotalSisa = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.Sisa);
                //        penerimaan.SubtotalBiayaTambahan = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.BiayaTambahan * item.Diterima);
                //        penerimaan.SubtotalTotalHPP = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TotalHPP * item.Diterima);
                //        penerimaan.SubtotalTotalHargaVendor = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TotalHargaVendor * item.Diterima);
                //        penerimaan.Grandtotal = penerimaan.SubtotalTotalHPP + penerimaan.SubtotalTotalHargaVendor;
                //        penerimaan.EnumStatusPenerimaan = (int)PilihanEnumStatusPenerimaanPO.Terima;
                //        penerimaan.Keterangan = data2.Keterangan;
                //    }
                //    db.SubmitChanges();

                //    poProduksiProduk.IDPenggunaProses = data.IDPenggunaUpdate;
                //    poProduksiProduk.IDPenggunaSelesai = data.IDPenggunaUpdate;
                //    poProduksiProduk.IDPenggunaPIC = data.IDPengguna;
                //    poProduksiProduk.IDPenggunaRevisi = null;
                //    poProduksiProduk.IDJenisPOProduksi = null;
                //    poProduksiProduk.TanggalProses = data.TanggalUpdate;
                //    poProduksiProduk.TanggalSelesai = data.TanggalUpdate;
                //    poProduksiProduk.TanggalRevisi = null;
                //    poProduksiProduk.TanggalJatuhTempo = data.TanggalJatuhTempo;
                //    poProduksiProduk.TanggalPengiriman = data.TanggalTerima;
                //    poProduksiProduk.TBPOProduksiProdukDetails.AddRange(data.TBPOProdukDetails.Select(item => new TBPOProduksiProdukDetail
                //    {
                //        IDKombinasiProduk = item.IDKombinasiProduk.Value,
                //        HargaPokokKomposisi = 0,
                //        BiayaTambahan = 0,
                //        TotalHPP = 0,
                //        HargaVendor = item.HargaVendor.Value,
                //        PotonganHargaVendor = item.PotonganHargaVendor.Value,
                //        TotalHargaVendor = item.HargaVendor.Value + item.PotonganHargaVendor.Value,
                //        Jumlah = item.Jumlah.Value,
                //        JumlahRevisi = item.Jumlah.Value,
                //        Sisa = item.Jumlah.Value - HitungSisaPesanan(db, item)
                //    }));

                //    poProduksiProduk.TotalJumlah = poProduksiProduk.TBPOProduksiProdukDetails.Sum(item => item.Jumlah);
                //    poProduksiProduk.TotalJumlahRevisi = poProduksiProduk.TBPOProduksiProdukDetails.Sum(item => item.JumlahRevisi);
                //    poProduksiProduk.EnumJenisHPP = (int)PilihanEnumJenisHPP.HargaSupplierVendor;
                //    poProduksiProduk.SubtotalBiayaTambahan = poProduksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.JumlahRevisi * item.BiayaTambahan));
                //    poProduksiProduk.SubtotalTotalHPP = poProduksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.JumlahRevisi * item.TotalHPP));
                //    poProduksiProduk.SubtotalTotalHargaVendor = poProduksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.JumlahRevisi * item.TotalHargaVendor));
                //    poProduksiProduk.PotonganPOProduksiProduk = data.PotonganPO;
                //    poProduksiProduk.BiayaLainLain = data.BiayaLainLain;
                //    poProduksiProduk.PersentaseTax = 0;

                //    decimal subtotal = poProduksiProduk.SubtotalTotalHargaVendor.Value + poProduksiProduk.BiayaLainLain.Value - poProduksiProduk.PotonganPOProduksiProduk.Value;
                //    poProduksiProduk.Tax = subtotal * poProduksiProduk.PersentaseTax;
                //    poProduksiProduk.Grandtotal = subtotal + poProduksiProduk.Tax;

                //    poProduksiProduk.TBPOProduksiProdukJenisPembayarans.AddRange(data.TBPembayaranPOProduks.Select(item => new TBPOProduksiProdukJenisPembayaran
                //    {
                //        IDPengguna = item.IDPengguna.Value,
                //        IDJenisPembayaran = 1,
                //        Tanggal = item.Tanggal,
                //        Bayar = item.Bayar,
                //        Keterangan = item.Keterangan
                //    }));

                //    poProduksiProduk.TotalBayar = poProduksiProduk.TBPOProduksiProdukJenisPembayarans.Sum(item => item.Bayar);

                //    if (poProduksiProduk.TBPenerimaanPOProduksiProduks.Count == 0 && poProduksiProduk.TBPengirimanPOProduksiProduks.Count == 0 && poProduksiProduk.TBPOProduksiProdukJenisPembayarans.Count == 0)
                //    {
                //        poProduksiProduk.EnumStatusProduksi = (int)PilihanEnumStatusPOProduksi.Baru;
                //    }
                //    else
                //        poProduksiProduk.EnumStatusProduksi = (int)PilihanEnumStatusPOProduksi.Proses;

                //    if (poProduksiProduk.TotalJumlahRevisi == poProduksiProduk.TBPenerimaanPOProduksiProduks.Sum(item => item.TotalDiterima + item.TotalTolakKeGudang) && poProduksiProduk.Grandtotal == poProduksiProduk.TotalBayar && poProduksiProduk.TBPOProduksiProdukKomposisis.Sum(item => item.Sisa) == 0)
                //    {
                //        poProduksiProduk.EnumStatusProduksi = (int)PilihanEnumStatusPOProduksi.Selesai;
                //    }

                //    poProduksiProduk.StatusRevisi = false;
                //    poProduksiProduk.Keterangan = data.Keterangan;
                //}
                #endregion

                status = "Transaksi Detail";
                #region Transaksi Detail
                //int index = 1;
                //StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
                //foreach (var item in db.TBTransaksis.ToArray())
                //{
                //    status = item.IDTransaksi + "(" + index + ")";
                //    item.TBTransaksiDetails.AddRange(item.TBDetailTransaksis.Select(item2 => new TBTransaksiDetail
                //    {
                //        IDKombinasiProduk = item2.IDKombinasiProduk,
                //        TBStokProduk = item2.TBKombinasiProduk.TBStokProduks.FirstOrDefault(item3 => item3.IDTempat == item2.TBTransaksi.IDTempat) != null ?
                //                    item2.TBKombinasiProduk.TBStokProduks.FirstOrDefault(item3 => item3.IDTempat == item2.TBTransaksi.IDTempat) :
                //                    StokProduk_Class.MembuatStok(0, 0, item2.TBTransaksi.IDTempat.Value, item2.TBTransaksi.IDPenggunaTransaksi.Value, item2.TBKombinasiProduk, 0, 0, ""),
                //        Quantity = item2.JumlahProduk,
                //        Berat = item2.Berat,
                //        HargaBeliKotor = item2.HargaPokok,
                //        HargaJual = item2.HargaJual,
                //        DiscountStore = item2.PotonganHargaJual,
                //        DiscountKonsinyasi = 0,
                //        Keterangan = item2.Keterangan
                //    }));

                //    index = index + 1;
                //}
                #endregion

                db.SubmitChanges();

                status = string.Empty;
                LabelNotif.Text = "sukses";
                LabelNotif.Visible = true;
            }
            catch (Exception ex)
            {
                LabelNotif.Text = "gagal " + ex.Message + (status);
                LabelNotif.Visible = true;
            }
        }
    }

    //private decimal HitungSisaPesanan(DataClassesDatabaseDataContext db, TBPOBahanBakuDetail detailPOBahanBaku)
    //{
    //    var detail = db.TBPenerimaanPOBahanBakuDetails
    //        .Where(item => item.IDBahanBaku == detailPOBahanBaku.IDBahanBaku &&
    //            item.TBPenerimaanPOBahanBaku.TBPOBahanBaku == detailPOBahanBaku.TBPOBahanBaku)
    //        .Select(item => item.Diterima.Value);

    //    return detail.Count() == 0 ? 0 : detail.Sum(item => (item == null ? 0 : item));
    //}

    //private int HitungSisaPesanan(DataClassesDatabaseDataContext db, TBPOProdukDetail detailPOProduk)
    //{
    //    var detail = db.TBPenerimaanPOProdukDetails
    //        .Where(item => item.IDKombinasiProduk == detailPOProduk.IDKombinasiProduk &&
    //            item.TBPenerimaanPOProduk.TBPOProduk == detailPOProduk.TBPOProduk)
    //        .Select(item => item.Diterima.Value);

    //    return detail.Count() == 0 ? 0 : detail.Sum(item => (item == null ? 0 : item));
    //}
}