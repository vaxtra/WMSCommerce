using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class TransaksiVoucherRetail_Model
{
    private int iDVoucher;
    public int IDVoucher
    {
        get { return iDVoucher; }
    }

    //IDPelanggan

    private string kode;
    public string Kode
    {
        get { return kode; }
    }

    private string nama;
    public string Nama
    {
        get { return nama; }
    }

    //TanggalAwal
    //TanggalAkhir

    private PilihanPotonganTransaksi enumJenisDiscount;
    public PilihanPotonganTransaksi EnumJenisDiscount
    {
        get { return enumJenisDiscount; }
    }

    private decimal nilaiDiscount;
    public decimal NilaiDiscount
    {
        get { return nilaiDiscount; }
    }

    public decimal Discount
    {
        get
        {
            switch (EnumJenisDiscount)
            {
                case PilihanPotonganTransaksi.Harga: return NilaiDiscount;
                case PilihanPotonganTransaksi.Persentase: return GrandTotal * NilaiDiscount / 100;
                default: return 0;
            }
        }
    }

    //Pemakaian
    //BatasPemakaian

    private string keterangan;
    public string Keterangan
    {
        get { return keterangan; }
    }

    //Status

    //TAMBAHAN
    private decimal grandTotal;
    public decimal GrandTotal
    {
        get { return grandTotal; }
    }

    public TransaksiVoucherRetail_Model(string _kode, DateTime tanggalTransaksi, int idPelanggan, decimal grandtotal)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //PENCARIAN BERDASARKAN KODE VOUCHER
            var Voucher = db.TBVouchers.FirstOrDefault(item => item.Kode.ToLower() == _kode.ToLower());

            //VALIDASI VOUCHER ADA, STATUS, BATAS PEMAKAIAN, TANGGAL AKTIF VALID ATAU TIDAK
            if (Voucher != null && Voucher.Status && Voucher.Pemakaian < Voucher.BatasPemakaian && (tanggalTransaksi >= Voucher.TanggalAwal || tanggalTransaksi <= Voucher.TanggalAkhir))
            {
                if (!Voucher.IDPelanggan.HasValue || Voucher.IDPelanggan.Value == idPelanggan)
                {
                    //IDPELANGGAN JIKA NULL SIAPAPUN BISA MENGGUNAKAN
                    //ADA IDPELANGGAN HANYA UNTUK PELANGGAN TERTENTU

                    iDVoucher = Voucher.IDVoucher;

                    //IDPelanggan

                    kode = Voucher.Kode;
                    nama = Voucher.Nama;

                    //TanggalAwal
                    //TanggalAkhir

                    enumJenisDiscount = (PilihanPotonganTransaksi)Voucher.EnumJenisDiscount;
                    nilaiDiscount = Voucher.NilaiDiscount;

                    //Discount
                    //Pemakaian
                    //BatasPemakaian

                    keterangan = Voucher.Keterangan;
                    grandTotal = grandtotal;
                }
            }
        }
    }
}