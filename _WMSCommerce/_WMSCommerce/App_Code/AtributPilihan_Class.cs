using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class AtributPilihan_Class : BaseWMSClass
{
    public AtributPilihan_Class(DataClassesDatabaseDataContext _db) : base(_db)
    {
    }
    public AtributPilihan_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna) : base(_db, _pengguna)
    {
    }

    public TBAtributPilihan Cari(int idAtribut, string nama)
    {
        return db.TBAtributPilihans.FirstOrDefault(item2 => item2.IDAtribut == idAtribut && item2.Nama == nama);
    }
    public TBAtributPilihan CariTambah(int idAtribut, string nama)
    {
        var AtributPilihan = Cari(idAtribut, nama);

        if (AtributPilihan == null)
            AtributPilihan = new TBAtributPilihan
            {
                IDAtribut = idAtribut,
                IDWMS = Guid.NewGuid(),
                Nama = nama,
                TanggalDaftar = DateTime.Now,
                TanggalUpdate = DateTime.Now
            };

        return AtributPilihan;
    }

    #region PELANGGAN
    public void PelangganReset(int idPelanggan)
    {
        db.TBAtributPilihanPelanggans.DeleteAllOnSubmit(db.TBAtributPilihanPelanggans.Where(item => item.IDPelanggan == idPelanggan));
    }
    public TBAtributPilihanPelanggan PelangganTambah(TBAtributPilihan AtributPilihan, int idPelanggan)
    {
        TBAtributPilihanPelanggan AtributPilihanPelanggan = new TBAtributPilihanPelanggan
        {
            IDPelanggan = idPelanggan,
            TBAtributPilihan = AtributPilihan
        };

        db.TBAtributPilihanPelanggans.InsertOnSubmit(AtributPilihanPelanggan);

        return AtributPilihanPelanggan;
    }
    public void PelangganProses(int idPelanggan, string nama, Repeater RepeaterAtribut)
    {
        PelangganReset(idPelanggan);

        foreach (RepeaterItem item in RepeaterAtribut.Items)
        {
            HiddenField HiddenFieldIDAtribut = (HiddenField)item.FindControl("HiddenFieldIDAtribut");
            TextBox TextBoxValue = (TextBox)item.FindControl("TextBoxValue");

            if (!string.IsNullOrWhiteSpace(TextBoxValue.Text))
            {
                var AtributPilihan = CariTambah(HiddenFieldIDAtribut.Value.ToInt(), TextBoxValue.Text);

                PelangganTambah(AtributPilihan, idPelanggan);
            }
        }

        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, EnumAlert.success, "Atribut Pelanggan " + nama + " berhasil disimpan");

        db.SubmitChanges();
    }
    public void PelangganData(TBPelanggan pelanggan, Repeater RepeaterAtribut)
    {
        //ATRIBUT PILIHAN
        var AtributPilihan = pelanggan.TBAtributPilihanPelanggans
            .Select(item => new
            {
                item.TBAtributPilihan.TBAtribut.IDAtribut,
                item.TBAtributPilihan.Nama
            })
            .ToArray();

        //MUNCUL KE REPEATER
        RepeaterAtribut.DataSource = db.TBAtributs
            .Where(item => item.IDAtributGrup == (int)GrupAtribut.Pelanggan)
            .ToArray()
            .Select(item => new
            {
                item.IDAtribut,
                item.Nama,
                item.Pilihan,
                Value = AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut) != null ? AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut).Nama : ""
            });
        RepeaterAtribut.DataBind();
    }
    #endregion

    #region PRODUK
    public void ProdukReset(int idProduk)
    {
        db.TBAtributPilihanProduks.DeleteAllOnSubmit(db.TBAtributPilihanProduks.Where(item => item.IDProduk == idProduk));
    }
    public TBAtributPilihanProduk ProdukTambah(TBAtributPilihan AtributPilihan, int idProduk)
    {
        TBAtributPilihanProduk AtributPilihanProduk = new TBAtributPilihanProduk
        {
            IDProduk = idProduk,
            TBAtributPilihan = AtributPilihan
        };

        db.TBAtributPilihanProduks.InsertOnSubmit(AtributPilihanProduk);

        return AtributPilihanProduk;
    }
    public void ProdukProses(int idProduk, string nama, Repeater RepeaterAtribut)
    {
        ProdukReset(idProduk);

        foreach (RepeaterItem item in RepeaterAtribut.Items)
        {
            HiddenField HiddenFieldIDAtribut = (HiddenField)item.FindControl("HiddenFieldIDAtribut");
            TextBox TextBoxValue = (TextBox)item.FindControl("TextBoxValue");

            if (!string.IsNullOrWhiteSpace(TextBoxValue.Text))
            {
                var AtributPilihan = CariTambah(HiddenFieldIDAtribut.Value.ToInt(), TextBoxValue.Text);

                ProdukTambah(AtributPilihan, idProduk);
            }
        }

        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, EnumAlert.success, "Atribut Produk " + nama + " berhasil disimpan");

        db.SubmitChanges();
    }
    public void ProdukData(TBProduk Produk, Repeater RepeaterAtribut)
    {
        //ATRIBUT PILIHAN
        var AtributPilihan = Produk.TBAtributPilihanProduks
            .Select(item => new
            {
                item.TBAtributPilihan.TBAtribut.IDAtribut,
                item.TBAtributPilihan.Nama
            })
            .ToArray();

        //MUNCUL KE REPEATER
        RepeaterAtribut.DataSource = db.TBAtributs
            .Where(item => item.IDAtributGrup == (int)GrupAtribut.Produk)
            .ToArray()
            .Select(item => new
            {
                item.IDAtribut,
                item.Nama,
                item.Pilihan,
                Value = AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut) != null ? AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut).Nama : ""
            });
        RepeaterAtribut.DataBind();
    }
    #endregion

    #region BAHAN BAKU
    public void BahanBakuReset(int idBahanBaku)
    {
        db.TBAtributPilihanBahanBakus.DeleteAllOnSubmit(db.TBAtributPilihanBahanBakus.Where(item => item.IDBahanBaku == idBahanBaku));
    }
    public TBAtributPilihanBahanBaku BahanBakuTambah(TBAtributPilihan AtributPilihan, int idBahanBaku)
    {
        TBAtributPilihanBahanBaku AtributPilihanBahanBaku = new TBAtributPilihanBahanBaku
        {
            IDBahanBaku = idBahanBaku,
            TBAtributPilihan = AtributPilihan
        };

        db.TBAtributPilihanBahanBakus.InsertOnSubmit(AtributPilihanBahanBaku);

        return AtributPilihanBahanBaku;
    }
    public void BahanBakuProses(int idBahanBaku, string nama, Repeater RepeaterAtribut)
    {
        BahanBakuReset(idBahanBaku);

        foreach (RepeaterItem item in RepeaterAtribut.Items)
        {
            HiddenField HiddenFieldIDAtribut = (HiddenField)item.FindControl("HiddenFieldIDAtribut");
            TextBox TextBoxValue = (TextBox)item.FindControl("TextBoxValue");

            if (!string.IsNullOrWhiteSpace(TextBoxValue.Text))
            {
                var AtributPilihan = CariTambah(HiddenFieldIDAtribut.Value.ToInt(), TextBoxValue.Text);

                BahanBakuTambah(AtributPilihan, idBahanBaku);
            }
        }

        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, EnumAlert.success, "Atribut BahanBaku " + nama + " berhasil disimpan");

        db.SubmitChanges();
    }
    public void BahanBakuData(TBBahanBaku BahanBaku, Repeater RepeaterAtribut)
    {
        //ATRIBUT PILIHAN
        var AtributPilihan = BahanBaku.TBAtributPilihanBahanBakus
            .Select(item => new
            {
                item.TBAtributPilihan.TBAtribut.IDAtribut,
                item.TBAtributPilihan.Nama
            })
            .ToArray();

        //MUNCUL KE REPEATER
        RepeaterAtribut.DataSource = db.TBAtributs
            .Where(item => item.IDAtributGrup == (int)GrupAtribut.BahanBaku)
            .ToArray()
            .Select(item => new
            {
                item.IDAtribut,
                item.Nama,
                item.Pilihan,
                Value = AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut) != null ? AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut).Nama : ""
            });
        RepeaterAtribut.DataBind();
    }
    #endregion

    #region PENGGUNA
    public void PenggunaReset(int idPengguna)
    {
        db.TBAtributPilihanPenggunas.DeleteAllOnSubmit(db.TBAtributPilihanPenggunas.Where(item => item.IDPengguna == idPengguna));
    }
    public TBAtributPilihanPengguna PenggunaTambah(TBAtributPilihan AtributPilihan, int idPengguna)
    {
        TBAtributPilihanPengguna AtributPilihanPengguna = new TBAtributPilihanPengguna
        {
            IDPengguna = idPengguna,
            TBAtributPilihan = AtributPilihan
        };

        db.TBAtributPilihanPenggunas.InsertOnSubmit(AtributPilihanPengguna);

        return AtributPilihanPengguna;
    }
    public void PenggunaProses(int idPengguna, string nama, Repeater RepeaterAtribut)
    {
        PenggunaReset(idPengguna);

        foreach (RepeaterItem item in RepeaterAtribut.Items)
        {
            HiddenField HiddenFieldIDAtribut = (HiddenField)item.FindControl("HiddenFieldIDAtribut");
            TextBox TextBoxValue = (TextBox)item.FindControl("TextBoxValue");

            if (!string.IsNullOrWhiteSpace(TextBoxValue.Text))
            {
                var AtributPilihan = CariTambah(HiddenFieldIDAtribut.Value.ToInt(), TextBoxValue.Text);

                PenggunaTambah(AtributPilihan, idPengguna);
            }
        }

        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, EnumAlert.success, "Atribut Pengguna " + nama + " berhasil disimpan");

        db.SubmitChanges();
    }
    public void PenggunaData(TBPengguna Pengguna, Repeater RepeaterAtribut)
    {
        //ATRIBUT PILIHAN
        var AtributPilihan = Pengguna.TBAtributPilihanPenggunas
            .Select(item => new
            {
                item.TBAtributPilihan.TBAtribut.IDAtribut,
                item.TBAtributPilihan.Nama
            })
            .ToArray();

        //MUNCUL KE REPEATER
        RepeaterAtribut.DataSource = db.TBAtributs
            .Where(item => item.IDAtributGrup == (int)GrupAtribut.Pengguna)
            .ToArray()
            .Select(item => new
            {
                item.IDAtribut,
                item.Nama,
                item.Pilihan,
                Value = AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut) != null ? AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut).Nama : ""
            });
        RepeaterAtribut.DataBind();
    }
    #endregion

    #region STORE
    public void StoreReset(int idStore)
    {
        db.TBAtributPilihanStores.DeleteAllOnSubmit(db.TBAtributPilihanStores.Where(item => item.IDStore == idStore));
    }
    public TBAtributPilihanStore StoreTambah(TBAtributPilihan AtributPilihan, int idStore)
    {
        TBAtributPilihanStore AtributPilihanStore = new TBAtributPilihanStore
        {
            IDStore = idStore,
            TBAtributPilihan = AtributPilihan
        };

        db.TBAtributPilihanStores.InsertOnSubmit(AtributPilihanStore);

        return AtributPilihanStore;
    }
    public void StoreProses(int idStore, string nama, Repeater RepeaterAtribut)
    {
        StoreReset(idStore);

        foreach (RepeaterItem item in RepeaterAtribut.Items)
        {
            HiddenField HiddenFieldIDAtribut = (HiddenField)item.FindControl("HiddenFieldIDAtribut");
            TextBox TextBoxValue = (TextBox)item.FindControl("TextBoxValue");

            if (!string.IsNullOrWhiteSpace(TextBoxValue.Text))
            {
                var AtributPilihan = CariTambah(HiddenFieldIDAtribut.Value.ToInt(), TextBoxValue.Text);

                StoreTambah(AtributPilihan, idStore);
            }
        }

        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, EnumAlert.success, "Atribut Store " + nama + " berhasil disimpan");

        db.SubmitChanges();
    }
    public void StoreData(TBStore Store, Repeater RepeaterAtribut)
    {
        //ATRIBUT PILIHAN
        var AtributPilihan = Store.TBAtributPilihanStores
            .Select(item => new
            {
                item.TBAtributPilihan.TBAtribut.IDAtribut,
                item.TBAtributPilihan.Nama
            })
            .ToArray();

        //MUNCUL KE REPEATER
        RepeaterAtribut.DataSource = db.TBAtributs
            .Where(item => item.IDAtributGrup == (int)GrupAtribut.Store)
            .ToArray()
            .Select(item => new
            {
                item.IDAtribut,
                item.Nama,
                item.Pilihan,
                Value = AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut) != null ? AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut).Nama : ""
            });
        RepeaterAtribut.DataBind();
    }
    #endregion

    #region TEMPAT
    public void TempatReset(int idTempat)
    {
        db.TBAtributPilihanTempats.DeleteAllOnSubmit(db.TBAtributPilihanTempats.Where(item => item.IDTempat == idTempat));
    }
    public TBAtributPilihanTempat TempatTambah(TBAtributPilihan AtributPilihan, int idTempat)
    {
        TBAtributPilihanTempat AtributPilihanTempat = new TBAtributPilihanTempat
        {
            IDTempat = idTempat,
            TBAtributPilihan = AtributPilihan
        };

        db.TBAtributPilihanTempats.InsertOnSubmit(AtributPilihanTempat);

        return AtributPilihanTempat;
    }
    public void TempatProses(int idTempat, string nama, Repeater RepeaterAtribut)
    {
        TempatReset(idTempat);

        foreach (RepeaterItem item in RepeaterAtribut.Items)
        {
            HiddenField HiddenFieldIDAtribut = (HiddenField)item.FindControl("HiddenFieldIDAtribut");
            TextBox TextBoxValue = (TextBox)item.FindControl("TextBoxValue");

            if (!string.IsNullOrWhiteSpace(TextBoxValue.Text))
            {
                var AtributPilihan = CariTambah(HiddenFieldIDAtribut.Value.ToInt(), TextBoxValue.Text);

                TempatTambah(AtributPilihan, idTempat);
            }
        }

        Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, EnumAlert.success, "Atribut Tempat " + nama + " berhasil disimpan");

        db.SubmitChanges();
    }
    public void TempatData(TBTempat Tempat, Repeater RepeaterAtribut)
    {
        //ATRIBUT PILIHAN
        var AtributPilihan = Tempat.TBAtributPilihanTempats
            .Select(item => new
            {
                item.TBAtributPilihan.TBAtribut.IDAtribut,
                item.TBAtributPilihan.Nama
            })
            .ToArray();

        //MUNCUL KE REPEATER
        RepeaterAtribut.DataSource = db.TBAtributs
            .Where(item => item.IDAtributGrup == (int)GrupAtribut.Tempat)
            .ToArray()
            .Select(item => new
            {
                item.IDAtribut,
                item.Nama,
                item.Pilihan,
                Value = AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut) != null ? AtributPilihan.FirstOrDefault(item2 => item2.IDAtribut == item.IDAtribut).Nama : ""
            });
        RepeaterAtribut.DataBind();
    }
    #endregion
}