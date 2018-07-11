using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassTempBahanBaku
/// </summary>
/// 

[Serializable]
public class BahanBaku_Class
{
    public int IDBahanBaku { get; set; }
    public string Kode { get; set; }
    public string BahanBaku { get; set; }
    public string Kategori { get; set; }
    public string Satuan { get; set; }
    public decimal Jumlah { get; set; }
    public decimal HargaBeli { get; set; }
    public decimal Subtotal { get; set; }

    public void Barcode(DataClassesDatabaseDataContext db, int idTempat, TBBahanBaku bahanBaku)
    {
        int idBahanBaku = bahanBaku.IDBahanBaku;
        string kode = "";

        do
        {
            kode = DateTime.Now.ToString("yyMM") + idTempat.ToString() + idBahanBaku.ToString();
            idBahanBaku++;

        } while (db.TBBahanBakus.FirstOrDefault(item => item.KodeBahanBaku == kode) != null);

        bahanBaku.KodeBahanBaku = kode;

        db.SubmitChanges();
    }

    public TBBahanBaku Cari(DataClassesDatabaseDataContext db, string nama)
    {
        return db.TBBahanBakus.FirstOrDefault(item => item.Nama.ToLower() == nama.ToLower());
    }
    public TBBahanBaku Cari(DataClassesDatabaseDataContext db, int idBahanBaku)
    {
        return db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == idBahanBaku);
    }

    public TBBahanBaku Tambah(DataClassesDatabaseDataContext db, string satuanKecil, string satuanBesar, string nama, decimal konversi)
    {
        #region SATUAN KECIL
        var SatuanKecil = db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == satuanKecil.ToLower());

        if (SatuanKecil == null)
        {
            TBSatuan satuan = new TBSatuan { Nama = satuanKecil };
            SatuanKecil = satuan;
            db.TBSatuans.InsertOnSubmit(satuan);
        }
        #endregion

        #region SATUAN BESAR
        TBSatuan SatuanBesar;
        if (satuanKecil.ToLower() != satuanBesar.ToLower())
        {
            SatuanBesar = db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == satuanBesar.ToLower());

            if (SatuanBesar == null)
            {
                TBSatuan satuan = new TBSatuan { Nama = satuanBesar };
                SatuanBesar = satuan;
                db.TBSatuans.InsertOnSubmit(satuan);
            }
        }
        else
            SatuanBesar = SatuanKecil;
        #endregion

        return Tambah(db, SatuanKecil, SatuanBesar, "", nama, konversi, "");
    }

    public TBBahanBaku Tambah(DataClassesDatabaseDataContext db, TBSatuan satuanKecil,
        TBSatuan satuanBesar, string kodeBahanBaku, string nama, decimal konversi, string deskripsi)
    {
        var BahanBaku = new TBBahanBaku
        {
            //IDProduk
            IDWMS = Guid.NewGuid(),
            TBSatuan = satuanKecil, //IDWarna
            TBSatuan1 = satuanBesar, //IDPemilikProduk
            TanggalDaftar = DateTime.Now,
            TanggalUpdate = DateTime.Now,
            Urutan = db.TBBahanBakus.Count() + 1,
            KodeBahanBaku = kodeBahanBaku,
            Nama = nama,
            Berat = 0,
            Konversi = konversi,
            Deskripsi = deskripsi
        };

        db.TBBahanBakus.InsertOnSubmit(BahanBaku);

        return BahanBaku;
    }

    public TBBahanBaku Ubah(DataClassesDatabaseDataContext db, int idBahanBaku, string satuanKecil,
        string satuanBesar, string kodeBahanBaku, string nama, decimal konversi, string deskripsi)
    {
        var BahanBaku = Cari(db, idBahanBaku);

        if (BahanBaku != null)
        {
            #region SATUAN KECIL
            var SatuanKecil = db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == satuanKecil.ToLower());

            if (SatuanKecil == null)
            {
                TBSatuan satuan = new TBSatuan { Nama = satuanKecil };
                SatuanKecil = satuan;
                db.TBSatuans.InsertOnSubmit(satuan);
            }
            #endregion

            #region SATUAN BESAR
            TBSatuan SatuanBesar;
            if (satuanKecil.ToLower() != satuanBesar.ToLower())
            {
                SatuanBesar = db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == satuanBesar.ToLower());

                if (SatuanBesar == null)
                {
                    TBSatuan satuan = new TBSatuan { Nama = satuanBesar };
                    SatuanBesar = satuan;
                    db.TBSatuans.InsertOnSubmit(satuan);
                }
            }
            else
                SatuanBesar = SatuanKecil;
            #endregion

            //IDBahanBaku
            //IDWMS
            BahanBaku.TBSatuan = SatuanKecil; //IDSatuan
            BahanBaku.TBSatuan1 = SatuanBesar; //IDSatuanKonversi
            //TanggalDaftar
            BahanBaku.TanggalUpdate = DateTime.Now;
            //Urutan
            BahanBaku.KodeBahanBaku = kodeBahanBaku;
            BahanBaku.Nama = nama;
            BahanBaku.Konversi = konversi;
            BahanBaku.Deskripsi = deskripsi;
        }

        return BahanBaku;
    }

    public TBBahanBaku Ubah(DataClassesDatabaseDataContext db, TBBahanBaku BahanBaku, string satuanKecil, string satuanBesar, decimal konversi)
    {
        #region SATUAN KECIL
        var SatuanKecil = db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == satuanKecil.ToLower());

        if (SatuanKecil == null)
        {
            TBSatuan satuan = new TBSatuan { Nama = satuanKecil };
            BahanBaku.TBSatuan = satuan;
            db.TBSatuans.InsertOnSubmit(satuan);
        }
        #endregion

        #region SATUAN BESAR
        TBSatuan SatuanBesar;
        if (satuanKecil.ToLower() != satuanBesar.ToLower())
        {
            SatuanBesar = db.TBSatuans.FirstOrDefault(item => item.Nama.ToLower() == satuanBesar.ToLower());

            if (SatuanBesar == null)
            {
                TBSatuan satuan = new TBSatuan { Nama = satuanBesar };
                BahanBaku.TBSatuan1 = satuan;
                db.TBSatuans.InsertOnSubmit(satuan);
            }
        }
        else
            BahanBaku.TBSatuan1 = SatuanKecil;
        #endregion

        BahanBaku.Konversi = konversi;

        return BahanBaku;
    }
    public static void DeleteBahanBaku(DataClassesDatabaseDataContext db, int idBahanBaku)
    {
        TBBahanBaku bahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == idBahanBaku);

        if (bahanBaku.TBAtributPilihanBahanBakus.Count == 0 &&
            bahanBaku.TBKomposisiBahanBakus.Count == 0 &&
            bahanBaku.TBKomposisiBahanBakus1.Count == 0 &&
            bahanBaku.TBKomposisiKombinasiProduks.Count == 0 &&
            bahanBaku.TBPenerimaanPOProduksiBahanBakuDetails.Count == 0 &&
            bahanBaku.TBPengirimanPOProduksiBahanBakuDetails.Count == 0 &&
            bahanBaku.TBPengirimanPOProduksiProdukDetails.Count == 0 &&
            bahanBaku.TBPOProduksiBahanBakuDetails.Count == 0 &&
            bahanBaku.TBPOProduksiBahanBakuKomposisis.Count == 0 &&
            bahanBaku.TBPOProduksiProdukKomposisis.Count == 0 &&
            bahanBaku.TBProyeksiKomposisis.Count == 0 &&
            bahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count == 0 &&
            bahanBaku.TBRelasiJenisBiayaProduksiBahanBakus.Count == 0 &&
            bahanBaku.TBStokBahanBakus.Count == 0 &&
            bahanBaku.TBTransferBahanBakuDetails.Count == 0)
        {
            db.TBBahanBakus.DeleteOnSubmit(bahanBaku);
        }
    }
}
