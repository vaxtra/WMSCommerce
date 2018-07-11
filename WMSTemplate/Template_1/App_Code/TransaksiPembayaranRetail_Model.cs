using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class TransaksiPembayaranRetail_Model
{
    public int IDTransaksiJenisPembayaran { get; set; }

    #region IDPengguna
    private int idPengguna;
    public int IDPengguna
    {
        get { return idPengguna; }
        set
        {
            idPengguna = value;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);

                var Pengguna = ClassPengguna.Cari(IDPengguna);

                if (Pengguna != null)
                    namaPengguna = Pengguna.NamaLengkap;
            }
        }
    }
    #endregion

    #region NamaPengguna
    private string namaPengguna;
    public string NamaPengguna
    {
        get { return namaPengguna; }
    }
    #endregion

    #region IDJenisPembayaran
    private int iDJenisPembayaran;
    public int IDJenisPembayaran
    {
        get { return iDJenisPembayaran; }
        set
        {
            iDJenisPembayaran = value;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var JenisPembayaran = db.TBJenisPembayarans.FirstOrDefault(item => item.IDJenisPembayaran == IDJenisPembayaran);

                if (JenisPembayaran != null)
                {
                    iDJenisBebanBiaya = (PilihanJenisBebanBiaya)JenisPembayaran.IDJenisBebanBiaya;
                    enumBiayaJenisPembayaran = PilihanBiayaJenisPembayaran.Persentase;
                    nilaiBiayaJenisPembayaran = (decimal)JenisPembayaran.PersentaseBiaya;
                    namaJenisPembayaran = JenisPembayaran.Nama;
                }
            }
        }
    }
    #endregion

    #region IDJenisBebanBiaya
    private PilihanJenisBebanBiaya iDJenisBebanBiaya;

    public PilihanJenisBebanBiaya IDJenisBebanBiaya
    {
        get { return iDJenisBebanBiaya; }
    }
    #endregion

    public DateTime Tanggal { get; set; }

    #region EnumBiayaJenisPembayaran
    private PilihanBiayaJenisPembayaran enumBiayaJenisPembayaran;

    public PilihanBiayaJenisPembayaran EnumBiayaJenisPembayaran
    {
        get { return enumBiayaJenisPembayaran; }
    }
    #endregion

    #region NilaiBiayaJenisPembayaran
    private decimal nilaiBiayaJenisPembayaran;

    public decimal NilaiBiayaJenisPembayaran
    {
        get { return nilaiBiayaJenisPembayaran; }
    }
    #endregion

    public decimal BiayaJenisPembayaran
    {
        get
        {
            switch (EnumBiayaJenisPembayaran)
            {
                case PilihanBiayaJenisPembayaran.Harga: return 0;
                case PilihanBiayaJenisPembayaran.Persentase: return 0;
                default: return 0;
            }
        }
    }
    public decimal Bayar { get; set; }
    public decimal Total
    {
        get
        {
            return Bayar + BiayaJenisPembayaran;
        }
    }
    public string Keterangan { get; set; }

    //TAMBAHAN
    #region NamaJenisPembayaran
    private string namaJenisPembayaran;
    public string NamaJenisPembayaran
    {
        get { return namaJenisPembayaran; }
    }
    #endregion
}