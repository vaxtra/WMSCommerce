using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class TransaksiPelangganRetail_Model
{
    #region IDPelanggan
    private int iDPelanggan;
    public int IDPelanggan
    {
        get { return iDPelanggan; }
    }
    #endregion

    #region IDGrupPelanggan
    private int iDGrupPelanggan;
    public int IDGrupPelanggan
    {
        get { return iDGrupPelanggan; }
    }
    #endregion

    #region GrupPelanggan
    private string grupPelanggan;
    public string GrupPelanggan
    {
        get { return grupPelanggan; }
    }
    #endregion

    #region Nama
    private string nama;
    public string Nama
    {
        get { return nama; }
    }
    #endregion

    #region AlamatLengkap
    private string alamatLengkap;
    public string AlamatLengkap
    {
        get { return alamatLengkap; }
    }
    #endregion

    #region Handphone
    private string handphone;
    public string Handphone
    {
        get { return handphone; }
    }
    #endregion

    #region NilaiDiscount
    private decimal nilaiDiscount;
    public decimal NilaiDiscount
    {
        get { return nilaiDiscount; }
    }
    #endregion

    public TransaksiPelangganRetail_Model(int idPelanggan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPelanggan Pelanggan = db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == idPelanggan);

            if (Pelanggan == null) //JIKA PELANGGAN TIDAK DITEMUKAN MAKA DEFAULT PELANGGAN 1
                Pelanggan = db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == 1);

            iDPelanggan = Pelanggan.IDPelanggan;
            iDGrupPelanggan = (int)Pelanggan.IDGrupPelanggan;
            grupPelanggan = Pelanggan.TBGrupPelanggan.Nama;
            nama = Pelanggan.NamaLengkap;

            TBAlamat Alamat = Pelanggan.TBAlamats.FirstOrDefault();

            if (Alamat != null)
                alamatLengkap = Alamat.AlamatLengkap;

            handphone = Pelanggan.Handphone;
            nilaiDiscount = (decimal)Pelanggan.TBGrupPelanggan.Persentase;
        }
    }
}