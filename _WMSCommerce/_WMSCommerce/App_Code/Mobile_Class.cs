using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobile
{
    public class KombinasiProduk
    {
        public double Berat { get; set; }
        public string Deskripsi { get; set; }
        public int IDAtributProduk { get; set; }
        public int IDKombinasiProduk { get; set; }
        public int IDProduk { get; set; }
        public string IDWMS { get; set; }
        public string KodeKombinasiProduk { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalDaftar { get; set; }
        public DateTime TanggalUpdate { get; set; }
        public int Urutan { get; set; }
    }

    public class TransaksiDetail
    {
        public double discount { get; set; }
        public double hargaJual { get; set; }
        public int idDetailTransaksi { get; set; }
        public int idKombinasiProduk { get; set; }
        public int idPkTransaksi { get; set; }
        public int idStokProduk { get; set; }
        public string idTransaksi { get; set; }
        public KombinasiProduk kombinasiProduk { get; set; }
        public int quantity { get; set; }
        public double subtotal { get; set; }
    }

    public class TransaksiJenisPembayaran
    {
        public double bayar { get; set; }
        public int idJenisPembayaran { get; set; }
        public int idTransaksi { get; set; }
        public int idTransaksiJenisPembayaran { get; set; }
        public string tanggal { get; set; }
    }

    public class Transaksi
    {
        public double biayaTambahan1 { get; set; }
        public double discountTransaksi { get; set; }
        public double grandTotal { get; set; }
        public int id { get; set; }
        public int idJenisTransaksi { get; set; }
        public int idPenggunaTransaksi { get; set; }
        public int idStatusTransaksi { get; set; }
        public int idTempat { get; set; }
        public string idTransaksi { get; set; }
        public int jumlahProduk { get; set; }
        public double subtotal { get; set; }
        public string tanggalTransaksi { get; set; }
        public double totalDiscountTransaksi { get; set; }
        public double totalPembayaran { get; set; }
        public List<TransaksiDetail> transaksiDetails { get; set; }
        public List<TransaksiJenisPembayaran> transaksiJenisPembayarans { get; set; }
    }

    public class RootObject
    {
        public Transaksi Transaksi { get; set; }
    }
}
