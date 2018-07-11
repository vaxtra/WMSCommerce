using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Alamat_Class
{
    public TBAlamat Tambah(DataClassesDatabaseDataContext db, int idWilayah, TBPelanggan Pelanggan, string alamatLengkap, string kodePos, decimal biayaPengiriman, string keterangan)
    {
        var Alamat = new TBAlamat
        {
            //IDAlamat
            //IDNegara,
            //IDProvinsi
            //IDKota
            TBPelanggan = Pelanggan,

            NamaLengkap = Pelanggan.NamaLengkap,
            Handphone = Pelanggan.Handphone,
            TeleponLain = Pelanggan.TeleponLain,

            AlamatLengkap = alamatLengkap,
            KodePos = kodePos,
            //Kota = kota,
            //Provinsi = provinsi,

            BiayaPengiriman = biayaPengiriman,
            Keterangan = keterangan
        };

        if (idWilayah > 0)
            Alamat.IDNegara = idWilayah;

        db.TBAlamats.InsertOnSubmit(Alamat);

        return Alamat;
    }
    public TBAlamat Cari(DataClassesDatabaseDataContext db, int idAlamat)
    {
        return db.TBAlamats.FirstOrDefault(item => item.IDAlamat == idAlamat);
    }

    public TBAlamat Ubah(DataClassesDatabaseDataContext db, int idWilayah, TBAlamat Alamat, TBPelanggan Pelanggan, string alamatLengkap, string kodePos, decimal biayaPengiriman, string keterangan)
    {
        //IDAlamat

        if (idWilayah > 0)
            Alamat.IDNegara = idWilayah;
        else
            Alamat.TBWilayah = null;

        //IDProvinsi
        //IDKota
        Alamat.TBPelanggan = Pelanggan;

        Alamat.NamaLengkap = Pelanggan.NamaLengkap;
        Alamat.Handphone = Pelanggan.Handphone;
        Alamat.TeleponLain = Pelanggan.TeleponLain;

        Alamat.AlamatLengkap = alamatLengkap;
        Alamat.KodePos = kodePos;

        //Kota
        //provinsi

        Alamat.BiayaPengiriman = biayaPengiriman;
        Alamat.Keterangan = keterangan;

        return Alamat;
    }
}
