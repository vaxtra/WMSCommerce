using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class TransaksiJenisPembayaran_Model
{
    public int Urutan { get; set; }
    public string IDTransaksi { get; set; }
    public int IDPengguna { get; set; }
    public int IDJenisPembayaran { get; set; }
    public int IDJenisBebanBiaya { get; set; }
    public DateTime Tanggal { get; set; }
    public int EnumBiayaJenisPembayaran { get; set; }
    public decimal PersentaseBiayaJenisPembayaran { get; set; }
    public decimal BiayaJenisPembayaran { get; set; }
    public decimal Bayar { get; set; }
    public decimal Total { get; set; }
    public string Keterangan { get; set; }
	public TransaksiJenisPembayaran_Model()
	{
        
	}
    public TransaksiJenisPembayaran_Model(string urutan,
        string idTransaksi,
        string idPengguna,
        string idJenisPembayaran,
        string idJenisBebanBiaya,
        string tanggal,
        string enumBiayaJenisPembayaran,
        string persentaseBiayaJenisPembayaran,
        string biayaJenisPembayaran,
        string bayar,
        string total)
    {
        #region Proses Parsing

        int _Urutan;
        int.TryParse(urutan, out _Urutan);

        DateTime _Tanggal;
        DateTime.TryParse(tanggal, out _Tanggal);

        int _IDPengguna;
        int.TryParse(idPengguna, out _IDPengguna);

        int _EnumBiayaJenisPembayaran;
        int.TryParse(enumBiayaJenisPembayaran, out _EnumBiayaJenisPembayaran);

        decimal _PersentaseBiayaJenisPembayaran;
        decimal.TryParse(persentaseBiayaJenisPembayaran, out _PersentaseBiayaJenisPembayaran);

        decimal _BiayaJenisPembayaran;
        decimal.TryParse(biayaJenisPembayaran, out _BiayaJenisPembayaran);

        int _IDJenisPembayaran;
        int.TryParse(idJenisPembayaran, out _IDJenisPembayaran);

        int _IDJenisBebanBiaya;
        int.TryParse(idJenisBebanBiaya, out _IDJenisBebanBiaya);

        decimal _Bayar;
        decimal.TryParse(bayar, out _Bayar);

        decimal _Total;
        decimal.TryParse(total, out _Total);
        #endregion

        IDTransaksi = idTransaksi;
        IDPengguna = _IDPengguna;
        IDJenisPembayaran = _IDJenisPembayaran;
        IDJenisBebanBiaya = _IDJenisBebanBiaya;
        Tanggal = _Tanggal;
        EnumBiayaJenisPembayaran = _EnumBiayaJenisPembayaran;
        PersentaseBiayaJenisPembayaran = _PersentaseBiayaJenisPembayaran;
        BiayaJenisPembayaran = _BiayaJenisPembayaran;
        Bayar = _Bayar;
        Total = _Total;

    }

    public void Tambah (string urutan,
        string idTransaksi,
        string idPengguna,
        string idJenisPembayaran,
        string idJenisBebanBiaya,
        string tanggal,
        string enumBiayaJenisPembayaran,
        string persentaseBiayaJenisPembayaran,
        string biayaJenisPembayaran,
        string bayar,
        string keterangan)
    {
        #region Proses Parsing

        int _Urutan;
        int.TryParse(urutan, out _Urutan);

        DateTime _Tanggal;
        DateTime.TryParse(tanggal, out _Tanggal);

        int _IDPengguna;
        int.TryParse(idPengguna, out _IDPengguna);

        int _EnumBiayaJenisPembayaran;
        int.TryParse(enumBiayaJenisPembayaran, out _EnumBiayaJenisPembayaran);

        decimal _PersentaseBiayaJenisPembayaran;
        decimal.TryParse(persentaseBiayaJenisPembayaran, out _PersentaseBiayaJenisPembayaran);

        decimal _BiayaJenisPembayaran;
        decimal.TryParse(biayaJenisPembayaran, out _BiayaJenisPembayaran);

        int _IDJenisPembayaran;
        int.TryParse(idJenisPembayaran, out _IDJenisPembayaran);

        int _IDJenisBebanBiaya;
        int.TryParse(idJenisBebanBiaya, out _IDJenisBebanBiaya);

        decimal _Bayar;
        decimal.TryParse(bayar, out _Bayar);

        #endregion

        Urutan = Urutan;
        IDTransaksi = idTransaksi;
        IDPengguna = _IDPengguna;
        IDJenisPembayaran = _IDJenisPembayaran;
        IDJenisBebanBiaya = _IDJenisBebanBiaya;
        Tanggal = _Tanggal;
        EnumBiayaJenisPembayaran = _EnumBiayaJenisPembayaran;
        PersentaseBiayaJenisPembayaran = _PersentaseBiayaJenisPembayaran;
        BiayaJenisPembayaran = _BiayaJenisPembayaran;
        Bayar = _Bayar;
        Keterangan = keterangan;

        Urutan++;
    }

}