using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassDashboard
/// </summary>
public class BusinessIntelligence_Class
{
    private PenggunaLogin pengguna;
    private DateTime tanggalAwal;
    private DateTime tanggalAkhir;
    private bool excel;
    private DataClassesDatabaseDataContext db;

    public BusinessIntelligence_Class(DataClassesDatabaseDataContext _db, PenggunaLogin _pengguna, DateTime _tanggalAwal, DateTime _tanggalAkhir, bool _excel)
    {
        pengguna = _pengguna;
        tanggalAwal = _tanggalAwal.Date;
        tanggalAkhir = _tanggalAkhir.Date;
        excel = _excel;
        db = _db;
    }

    public string Periode
    {
        get
        {
            if (tanggalAwal == tanggalAkhir)
                return Pengaturan.FormatTanggalRingkas(tanggalAwal);
            else
                return Pengaturan.FormatTanggalRingkas(tanggalAwal) + " - " + Pengaturan.FormatTanggalRingkas(tanggalAkhir);
        }
    }
    private string tempPencarian;
    public string TempPencarian
    {
        get { return tempPencarian; }
    }

    private string linkDownload;
    public string LinkDownload
    {
        get { return linkDownload; }
    }

    public static string GabungkanSemuaKategoriProduk(TBStokProduk stokProduk, TBKombinasiProduk kombinasiProduk)
    {
        string kategori = string.Empty;

        if (stokProduk != null)
        {
            if (stokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0)
            {
                foreach (var item in stokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriProduk.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriProduk.Nama;
                }
            }
        }
        else
        {
            if (kombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0)
            {
                foreach (var item in kombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriProduk.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriProduk.Nama;
                }
            }
        }

        return kategori;
    }

    public static string GabungkanSemuaKategoriBahanBaku(TBStokBahanBaku stokBahanBaku, TBBahanBaku bahanBaku)
    {
        string kategori = string.Empty;

        if (stokBahanBaku != null)
        {
            if (stokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0)
            {
                foreach (var item in stokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriBahanBaku.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriBahanBaku.Nama;
                }
            }
        }
        else if (bahanBaku != null)
        {
            if (bahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0)
            {
                foreach (var item in bahanBaku.TBRelasiBahanBakuKategoriBahanBakus)
                {
                    if (kategori == string.Empty)
                        kategori = kategori + item.TBKategoriBahanBaku.Nama;
                    else
                        kategori = kategori + ", " + item.TBKategoriBahanBaku.Nama;
                }
            }
        }

        return kategori;
    }

    public Dictionary<string, dynamic> TEMPLATE()
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        var Data = db.TBTransaksis
            .Where(item =>
                item.TanggalOperasional.Value >= tanggalAwal &&
                item.TanggalOperasional.Value <= tanggalAkhir)
            .ToArray();

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        var DataResult = Data.Select(item => new
        {
            item.IDTransaksi,
        });

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Transaksi", Periode, 27);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Transaksi");

            int index = 2;

            foreach (var item in Data)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransaksi);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Data", DataResult);

        Result.Add("JumlahTamu", Pengaturan.FormatHarga(Data.Sum(item => item.JumlahTamu)));

        return Result;
    }

    #region CUSTOMER
    public Dictionary<string, dynamic> CustomerLocation(List<int> ListIDGrupPelanggan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        TBTransaksi[] ListTransaksi = db.TBTransaksis.Where(item => item.TanggalTransaksi.Value.Date >= tanggalAwal && item.TanggalTransaksi.Value.Date <= tanggalAkhir &&
            item.IDPelanggan > 1 && item.TBPelanggan.TBAlamats.Count > 0 && item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).ToArray();

        #region FILTER GRUP PELANGGAN
        if (ListIDGrupPelanggan.Count > 0)
        {
            JenisTransaksi_Class JenisTransaksi_Class = new JenisTransaksi_Class();
            string tempListGrupPelanggan = "Grup Pelanggan : ";

            foreach (var item in ListIDGrupPelanggan)
            {
                tempListGrupPelanggan += db.TBGrupPelanggans.FirstOrDefault(item2 => item2.IDGrupPelanggan == item).Nama + ", ";
            }

            Result.Add("ListGrupPelanggan", tempListGrupPelanggan.Substring(0, tempListGrupPelanggan.Length - 2));

            ListTransaksi = ListTransaksi.Where(item => ListIDGrupPelanggan.Contains(item.TBPelanggan.IDGrupPelanggan)).ToArray();
        }
        else
            Result.Add("ListGrupPelanggan", "Semua Grup Pelanggan");
        #endregion

        var Pelanggan = ListTransaksi
            .GroupBy(item => item.TBPelanggan)
            .Select(item => new
            {
                item.Key.TBAlamats,
                item.Key.IDGrupPelanggan,
                item.Key.IDPelanggan,
                item.Key.TBGrupPelanggan,
                Pelanggan = item.Key.NamaLengkap,
                TanggalDaftar = item.Key.TanggalDaftar,
                JumlahTransaksi = item.Count(),
                JumlahProduk = item.Sum(item2 => item2.JumlahProduk),
                TotalDiskon = item.Sum(item2 => (item2.TotalPotonganHargaJualDetail + item2.TotalDiscountVoucher + item2.PotonganTransaksi)),
                NetRevenue = item.Sum(item2 => (item2.Subtotal + item2.TotalPotonganHargaJualDetail) - (item2.TotalPotonganHargaJualDetail + item2.TotalDiscountVoucher + item2.PotonganTransaksi) + item2.Pembulatan),
                Grandtotal = item.Sum(item2 => item2.GrandTotal),
                StatusLama = item.Key.TBTransaksis.Where(item2 => item2.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).Count() > 1 ? true : false
            }).OrderByDescending(item => item.NetRevenue);

        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        var listBaru = Pelanggan.Where(item => item.TanggalDaftar.Value.Date >= tanggalAwal && item.TanggalDaftar.Value.Date <= tanggalAkhir);
        Result.Add("BaruJumlahTransaksi", Pengaturan.FormatHarga(listBaru.Count() > 0 ? listBaru.Sum(item => item.JumlahTransaksi) : 0));
        Result.Add("BaruJumlahProduk", Pengaturan.FormatHarga(listBaru.Count() > 0 ? listBaru.Sum(item => item.JumlahProduk) : 0));
        Result.Add("BaruGrandtotal", Pengaturan.FormatHarga(listBaru.Count() > 0 ? listBaru.Sum(item => item.Grandtotal) : 0));

        var listLama = Pelanggan.Where(item => item.StatusLama == true);
        Result.Add("LamaJumlahTransaksi", Pengaturan.FormatHarga(listLama.Count() > 0 ? listLama.Sum(item => item.JumlahTransaksi) : 0));
        Result.Add("LamaJumlahProduk", Pengaturan.FormatHarga(listLama.Count() > 0 ? listLama.Sum(item => item.JumlahProduk) : 0));
        Result.Add("LamaGrandtotal", Pengaturan.FormatHarga(listLama.Count() > 0 ? listLama.Sum(item => item.Grandtotal) : 0));

        var GrupPelanggan = ListTransaksi
            .GroupBy(item => item.TBPelanggan.TBGrupPelanggan)
            .Select(item => new
            {
                GrupPelanggan = item.Key.Nama,
                JumlahTransaksi = item.Count(),
                JumlahProduk = item.Sum(item2 => item2.JumlahProduk),
                Grandtotal = item.Sum(item2 => item2.GrandTotal)
            }).OrderByDescending(item => item.Grandtotal);
        Result.Add("DataGrupPelanggan", GrupPelanggan);
        Result.Add("TotalGrupPelangganJumlahTransaksi", Pengaturan.FormatHarga(GrupPelanggan.Sum(item => item.JumlahTransaksi)));
        Result.Add("TotalGrupPelangganJumlahProduk", Pengaturan.FormatHarga(GrupPelanggan.Sum(item => item.JumlahProduk)));
        Result.Add("TotalGrupPelangganGrandtotal", Pengaturan.FormatHarga(GrupPelanggan.Sum(item => item.Grandtotal)));

        Result.Add("DataPelanggan", Pelanggan);
        Result.Add("TotalPelangganJumlahTransaksi", Pengaturan.FormatHarga(Pelanggan.Sum(item => item.JumlahTransaksi)));
        Result.Add("TotalPelangganJumlahProduk", Pengaturan.FormatHarga(Pelanggan.Sum(item => item.TotalDiskon)));
        Result.Add("TotalPelangganGrandtotal", Pengaturan.FormatHarga(Pelanggan.Sum(item => item.NetRevenue)));

        var Kota = Pelanggan.Where(item => item.TBAlamats.FirstOrDefault().IDNegara != null)
            .GroupBy(item => item.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1)
            .Select(item => new
            {
                item.Key,
                item.Key.IDWilayah,
                Kota = item.Key.Nama,
                JumlahTransaksi = item.Sum(item2 => item2.JumlahTransaksi),
                TotalDiskon = item.Sum(item2 => item2.TotalDiskon),
                NetRevenue = item.Sum(item2 => item2.NetRevenue)
            }).OrderByDescending(item => item.NetRevenue);
        Result.Add("DataKota", Kota);
        Result.Add("TotalKotaJumlahTransaksi", Pengaturan.FormatHarga(Kota.Sum(item => item.JumlahTransaksi)));
        Result.Add("TotalKotaJumlahProduk", Pengaturan.FormatHarga(Kota.Sum(item => item.TotalDiskon)));
        Result.Add("TotalKotaGrandtotal", Pengaturan.FormatHarga(Kota.Sum(item => item.NetRevenue)));

        var Provinsi = Kota
            .GroupBy(item => item.Key.TBWilayah1)
            .Select(item => new
            {
                item.Key.IDWilayah,
                Provinsi = item.Key.Nama,
                JumlahTransaksi = item.Sum(item2 => item2.JumlahTransaksi),
                TotalDiskon = item.Sum(item2 => item2.TotalDiskon),
                NetRevenue = item.Sum(item2 => item2.NetRevenue)
            }).OrderByDescending(item => item.NetRevenue);
        Result.Add("DataProvinsi", Provinsi);
        Result.Add("TotalProvinsiJumlahTransaksi", Pengaturan.FormatHarga(Provinsi.Sum(item => item.JumlahTransaksi)));
        Result.Add("TotalProvinsiJumlahProduk", Pengaturan.FormatHarga(Provinsi.Sum(item => item.TotalDiskon)));
        Result.Add("TotalProvinsiGrandtotal", Pengaturan.FormatHarga(Provinsi.Sum(item => item.NetRevenue)));
        if (excel)
        {
            #region EXCEL
            //Excel_Class Excel_Class = new Excel_Class(pengguna, "Transaksi", Periode, 27);
            //ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            //Excel_Class.Header(1, "No.");
            //Excel_Class.Header(2, "Transaksi");

            //int index = 2;

            //foreach (var item in Data)
            //{
            //    Excel_Class.Content(index, 1, index - 1);
            //    Excel_Class.Content(index, 2, item.IDTransaksi);

            //    index++;
            //}

            //Excel_Class.Save();

            //linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        return Result;
    }

    public Dictionary<string, dynamic> CustomerDetailTransaksiProduk(List<int> ListIDGrupPelanggan, int id, string status)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        TBTransaksi[] ListTransaksi = db.TBTransaksis.Where(item => item.TanggalTransaksi.Value.Date >= tanggalAwal && item.TanggalTransaksi.Value.Date <= tanggalAkhir &&
            item.IDPelanggan > 1 && item.TBPelanggan.TBAlamats.Count > 0 && item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).ToArray();

        TBTransaksiDetail[] ListTransaksiDetail = db.TBTransaksiDetails.Where(item => item.TBTransaksi.TanggalTransaksi.Value.Date >= tanggalAwal && item.TBTransaksi.TanggalTransaksi.Value.Date <= tanggalAkhir &&
            item.TBTransaksi.IDPelanggan > 1 && item.TBTransaksi.TBPelanggan.TBAlamats.Count > 0 && item.TBTransaksi.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).ToArray();

        #region FILTER GRUP PELANGGAN
        if (ListIDGrupPelanggan.Count > 0)
        {
            JenisTransaksi_Class JenisTransaksi_Class = new JenisTransaksi_Class();
            string tempListGrupPelanggan = "Grup Pelanggan : ";

            foreach (var item in ListIDGrupPelanggan)
            {
                tempListGrupPelanggan += db.TBGrupPelanggans.FirstOrDefault(item2 => item2.IDGrupPelanggan == item).Nama + ", ";
            }

            ListTransaksi = ListTransaksi.Where(item => ListIDGrupPelanggan.Contains(item.TBPelanggan.IDGrupPelanggan)).ToArray();
            ListTransaksiDetail = ListTransaksiDetail.Where(item => ListIDGrupPelanggan.Contains(item.TBTransaksi.TBPelanggan.IDGrupPelanggan)).ToArray();
        }
        #endregion

        if (status == "Pelanggan")
        {
            var Transaksi = ListTransaksi.Where(item => item.IDPelanggan == id)
                .GroupBy(item => new
                {
                    item.TanggalTransaksi.Value.Month,
                    item.TanggalTransaksi.Value.Year,
                })
                .Select(item => new
                {
                    Bulan = DateTime.Parse(item.Key.Month + "/" + "01" + "/" + item.Key.Year).ToString("MMMM", new CultureInfo("id-ID")),
                    Tahun = item.Key.Year,
                    JumlahTransaksi = item.Count(),
                    NetRevenue = item.Sum(item2 => (item2.Subtotal + item2.TotalPotonganHargaJualDetail) - (item2.TotalPotonganHargaJualDetail + item2.TotalDiscountVoucher + item2.PotonganTransaksi) + item2.Pembulatan),
                }).ToArray();
            Result.Add("HasilTransaksi", Transaksi);

            var Produk = ListTransaksiDetail.Where(item => item.TBTransaksi.IDPelanggan == id)
                .GroupBy(item => new
                {
                    item.TBTransaksi.TanggalTransaksi.Value.Month,
                    item.TBTransaksi.TanggalTransaksi.Value.Year,
                    item.TBKombinasiProduk
                })
                .Select(item => new
                {
                    Bulan = DateTime.Parse(item.Key.Month + "/" + "01" + "/" + item.Key.Year).ToString("MMMM", new CultureInfo("id-ID")),
                    Tahun = item.Key.Year,
                    KombinasiProduk = item.Key.TBKombinasiProduk.Nama,
                    KategoriProduk = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                    JumlahProduk = item.Sum(item2 => item2.Quantity)
                }).ToArray();
            Result.Add("HasilProduk", Produk);
        }
        else if (status == "Kota")
        {
            var Transaksi = ListTransaksi.Where(item => item.TBPelanggan.TBAlamats.FirstOrDefault().IDNegara != null && item.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1.IDWilayah == id)
                .GroupBy(item => new
                {
                    item.TanggalTransaksi.Value.Month,
                    item.TanggalTransaksi.Value.Year,
                })
                .Select(item => new
                {
                    Bulan = DateTime.Parse(item.Key.Month + "/" + "01" + "/" + item.Key.Year).ToString("MMMM", new CultureInfo("id-ID")),
                    Tahun = item.Key.Year,
                    JumlahTransaksi = item.Count(),
                    NetRevenue = item.Sum(item2 => (item2.Subtotal + item2.TotalPotonganHargaJualDetail) - (item2.TotalPotonganHargaJualDetail + item2.TotalDiscountVoucher + item2.PotonganTransaksi) + item2.Pembulatan),
                }).ToArray();
            Result.Add("HasilTransaksi", Transaksi);

            var Produk = ListTransaksiDetail.Where(item => item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().IDNegara != null && item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1.IDWilayah == id)
                .GroupBy(item => new
                {
                    item.TBTransaksi.TanggalTransaksi.Value.Month,
                    item.TBTransaksi.TanggalTransaksi.Value.Year,
                    item.TBKombinasiProduk
                })
                .Select(item => new
                {
                    Bulan = DateTime.Parse(item.Key.Month + "/" + "01" + "/" + item.Key.Year).ToString("MMMM", new CultureInfo("id-ID")),
                    Tahun = item.Key.Year,
                    KombinasiProduk = item.Key.TBKombinasiProduk.Nama,
                    KategoriProduk = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                    JumlahProduk = item.Sum(item2 => item2.Quantity)
                }).ToArray();
            Result.Add("HasilProduk", Produk);
        }
        else if (status == "Provinsi")
        {
            var Transaksi = ListTransaksi.Where(item => item.TBPelanggan.TBAlamats.FirstOrDefault().IDNegara != null && item.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1.TBWilayah1.IDWilayah == id)
                .GroupBy(item => new
                {
                    item.TanggalTransaksi.Value.Month,
                    item.TanggalTransaksi.Value.Year,
                })
                .Select(item => new
                {
                    Bulan = DateTime.Parse(item.Key.Month + "/" + "01" + "/" + item.Key.Year).ToString("MMMM", new CultureInfo("id-ID")),
                    Tahun = item.Key.Year,
                    JumlahTransaksi = item.Count(),
                    NetRevenue = item.Sum(item2 => (item2.Subtotal + item2.TotalPotonganHargaJualDetail) - (item2.TotalPotonganHargaJualDetail + item2.TotalDiscountVoucher + item2.PotonganTransaksi) + item2.Pembulatan),
                }).ToArray();
            Result.Add("HasilTransaksi", Transaksi);

            var Produk = ListTransaksiDetail.Where(item => item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().IDNegara != null && item.TBTransaksi.TBPelanggan.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1.TBWilayah1.IDWilayah == id)
                .GroupBy(item => new
                {
                    item.TBTransaksi.TanggalTransaksi.Value.Month,
                    item.TBTransaksi.TanggalTransaksi.Value.Year,
                    item.TBKombinasiProduk
                })
                .Select(item => new
                {
                    Bulan = DateTime.Parse(item.Key.Month + "/" + "01" + "/" + item.Key.Year).ToString("MMMM", new CultureInfo("id-ID")),
                    Tahun = item.Key.Year,
                    KombinasiProduk = item.Key.TBKombinasiProduk.Nama,
                    KategoriProduk = GabungkanSemuaKategoriProduk(null, item.Key.TBKombinasiProduk),
                    JumlahProduk = item.Sum(item2 => item2.Quantity)
                }).ToArray();
            Result.Add("HasilProduk", Produk);
        }

        return Result;
    }

    public Dictionary<string, dynamic> CustomerTransaksiBrandKategoriProduk(List<int> ListIDGrupPelanggan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        TBTransaksiDetail[] ListTransaksiDetail = db.TBTransaksiDetails
            .Where(item =>
                item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                item.TBTransaksi.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled)
            .ToArray();

        #region FILTER GRUP PELANGGAN
        if (ListIDGrupPelanggan.Count > 0)
        {
            JenisTransaksi_Class JenisTransaksi_Class = new JenisTransaksi_Class();
            string tempListGrupPelanggan = "Grup Pelanggan : ";

            foreach (var item in ListIDGrupPelanggan)
            {
                tempListGrupPelanggan += db.TBGrupPelanggans.FirstOrDefault(item2 => item2.IDGrupPelanggan == item).Nama + ", ";
            }

            Result.Add("ListGrupPelanggan", tempListGrupPelanggan.Substring(0, tempListGrupPelanggan.Length - 2));

            ListTransaksiDetail = ListTransaksiDetail.Where(item => ListIDGrupPelanggan.Contains(item.TBTransaksi.TBPelanggan.IDGrupPelanggan)).ToArray();
        }
        else
            Result.Add("ListGrupPelanggan", "Semua Grup Pelanggan");
        #endregion

        var Data = ListTransaksiDetail
            .GroupBy(item => new
            {
                item.TBKombinasiProduk.TBProduk.TBPemilikProduk,
                item.TBKombinasiProduk
            })
            .Select(item => new
            {
                KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                PemilikProduk = item.Key.TBPemilikProduk.Nama,
                Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                IDPemilikProduk = item.Key.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                IDProduk = item.Key.TBKombinasiProduk.TBProduk.IDProduk,
                IDAtributProduk = item.Key.TBKombinasiProduk.IDAtributProduk,
                RelasiKategori = item.Key.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks,
                Kategori = item.Key.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama,
                JumlahProduk = item.Sum(item2 => item2.Quantity),
                HargaPokok = item.Sum(item2 => item2.HargaBeli * item2.Quantity),
                HargaJual = item.Sum(item2 => item2.HargaJual * item2.Quantity),
                PotonganHargaJual = item.Sum(item2 => item2.Discount * item2.Quantity),
                Subtotal = item.Sum(item2 => item2.Subtotal),
                PenjualanBersih = item.Sum(item2 => (item2.HargaJual - item2.Discount - item2.HargaBeli) * item2.Quantity),
            }).OrderBy(item => item.IDPemilikProduk).ThenBy(item => item.Produk).ThenBy(item => item.IDAtributProduk);

        var DataResult = db.TBPemilikProduks.AsEnumerable().Where(item => Data.Any(data => data.IDPemilikProduk == item.IDPemilikProduk)).Select(item => new
        {
            PemilikProduk = item.Nama,
            Body = db.TBKategoriProduks.AsEnumerable().Where(item2 => Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk).Any(item3 => item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null)).Select(item2 => new
            {
                KategoriProduk = item2.Nama,
                Body = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null),
                TotalJumlahProduk = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null).Sum(item3 => item3.JumlahProduk),
                TotalHargaPokok = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null).Sum(item3 => item3.HargaPokok),
                TotalHargaJual = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null).Sum(item3 => item3.HargaJual),
                TotalPotonganHargaJual = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null).Sum(item3 => item3.PotonganHargaJual),
                TotalSubtotal = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null).Sum(item3 => item3.Subtotal),
                TotalPenjualanBersih = Data.Where(item3 => item3.IDPemilikProduk == item.IDPemilikProduk && item3.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item2.IDKategoriProduk) != null).Sum(item3 => item3.PenjualanBersih),
            }).ToArray(),
            TotalJumlahProduk = Data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.JumlahProduk),
            TotalHargaPokok = Data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.HargaPokok),
            TotalHargaJual = Data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.HargaJual),
            TotalPotonganHargaJual = Data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.PotonganHargaJual),
            TotalSubtotal = Data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.Subtotal),
            TotalPenjualanBersih = Data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.PenjualanBersih),
        }).ToArray();

        if (excel)
        {
            #region EXCEL
            Excel_Class Excel_Class = new Excel_Class(pengguna, "Transaksi", Periode, 27);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No.");
            Excel_Class.Header(2, "Transaksi");

            int index = 2;

            foreach (var item in ListTransaksiDetail)
            {
                Excel_Class.Content(index, 1, index - 1);
                Excel_Class.Content(index, 2, item.IDTransaksi);

                index++;
            }

            Excel_Class.Save();

            linkDownload = Excel_Class.LinkDownload;
            #endregion
        }

        Result.Add("Data", DataResult);

        return Result;
    }

    public Dictionary<string, dynamic> CustomerRangeAgeByCity()
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();
        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        Dictionary<int, int> RANGE_AGE = new Dictionary<int, int>
        {
            { -9999, 12 },
            { 13, 17 },
            { 18, 24 },
            { 25, 34 },
            { 35, 44 },
            { 45, 9999 }
        };

        TBTransaksi[] ListTransaksi = db.TBTransaksis.Where(item => item.TanggalTransaksi.Value.Date >= tanggalAwal && item.TanggalTransaksi.Value.Date <= tanggalAkhir &&
            item.IDPelanggan > 1 && item.TBPelanggan.TBAlamats.Count > 0 && item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).ToArray();

        var Pelanggan = ListTransaksi
            .GroupBy(item => item.TBPelanggan)
            .Select(item => new
            {
                item.Key.TBAlamats,
                Umur = DateTime.Now.Year - item.Key.TanggalLahir.Value.Year,
                JumlahTransaksi = item.Count(),
            });

        var DataResult = Pelanggan.Where(item => item.TBAlamats.FirstOrDefault().IDNegara != null)
            .GroupBy(item => item.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1)
            .Select(item => new
            {
                Kota = item.Key.Nama,
                Body = RANGE_AGE.Select(item2 => new
                {
                    Umur = (item2.Key == -9999 ? "Bawah" : item2.Key.ToString()) + " - " + (item2.Value == 9999 ? "Atas" : item2.Value.ToString()),
                    JumlahTransaksi = item.Where(item3 => item3.Umur >= item2.Key && item3.Umur <= item2.Value).Sum(item3 => item3.JumlahTransaksi)
                }),
                JumlahTransaksi = item.Sum(item2 => item2.JumlahTransaksi)
            }).Where(item => item.JumlahTransaksi > 0).OrderByDescending(item => item.JumlahTransaksi);

        //var DataResult = db.TBPelanggans.AsEnumerable()
        //    .Where(item => item.TBTransaksis.TanggalTransaksi.Value.Date >= tanggalAwal && item.TanggalTransaksi.Value.Date <= tanggalAkhir && item.IDPelanggan > 1 && item.TBAlamats.Count > 0)
        //    .Select(item => new { item.TBAlamats, JumlahTransaksi = item.TBTransaksis.Where(item2 => item2.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).Count(), Umur = DateTime.Now.Year - item.TanggalLahir.Value.Year })
        //    .Where(item => item.TBAlamats.FirstOrDefault().IDNegara != null)
        //    .GroupBy(item => item.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1).Select(item => new
        //    {
        //        Kota = item.Key.Nama,
        //        Body = RANGE_AGE.Select(item2 => new
        //        {
        //            Umur = item2.Key + " - " + item2.Value,
        //            JumlahTransaksi = item.Where(item3 => item3.Umur >= item2.Key && item3.Umur <= item2.Value).Sum(item3 => item3.JumlahTransaksi)
        //        }),
        //        JumlahTransaksi = item.Sum(item2 => item2.JumlahTransaksi)
        //    }).Where(item => item.JumlahTransaksi > 0).OrderByDescending(item => item.JumlahTransaksi);

        //if (excel)
        //{
        //    #region EXCEL
        //    Excel_Class Excel_Class = new Excel_Class(pengguna, "Transaksi", Periode, 27);
        //    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

        //    Excel_Class.Header(1, "No.");
        //    Excel_Class.Header(2, "Transaksi");

        //    int index = 2;

        //    foreach (var item in Data)
        //    {
        //        Excel_Class.Content(index, 1, index - 1);
        //        Excel_Class.Content(index, 2, item.IDTransaksi);

        //        index++;
        //    }

        //    Excel_Class.Save();

        //    linkDownload = Excel_Class.LinkDownload;
        //    #endregion
        //}

        Result.Add("Data", DataResult);

        //Result.Add("JumlahTamu", Pengaturan.FormatHarga(Data.Sum(item => item.JumlahTamu)));

        return Result;
    }

    public Dictionary<string, dynamic> CustomerSignUpWebsite()
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();
        tempPencarian += "?TanggalAwal=" + tanggalAwal;
        tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

        Dictionary<int, int> RANGE_AGE = new Dictionary<int, int>
        {
            { -9999, 12 },
            { 13, 17 },
            { 18, 24 },
            { 25, 34 },
            { 35, 44 },
            { 45, 9999 }
        };

        TBTransaksi[] ListTransaksi = db.TBTransaksis.Where(item => item.TBPelanggan.TBAlamats.Count > 0 && item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled).ToArray();

        var Pelanggan = db.TBPelanggans.AsEnumerable().Where(item => item.TanggalDaftar.Value.Date >= tanggalAwal && item.TanggalDaftar.Value.Date <= tanggalAkhir &&
            item.IDPelanggan > 1 && item.TBAlamats.Count > 0)
            .Select(item => new
            {
                item.TBAlamats,
                Umur = DateTime.Now.Year - item.TanggalLahir.Value.Year
            });

        var DataResult = Pelanggan.Where(item => item.TBAlamats.FirstOrDefault().IDNegara != null)
            .GroupBy(item => item.TBAlamats.FirstOrDefault().TBWilayah.TBWilayah1)
            .Select(item => new
            {
                Kota = item.Key.Nama,
                Body = RANGE_AGE.Select(item2 => new
                {
                    Umur = (item2.Key == -9999 ? "Bawah" : item2.Key.ToString()) + " - " + (item2.Value == 9999 ? "Atas" : item2.Value.ToString()),
                    JumlahSign = item.Where(item3 => item3.Umur >= item2.Key && item3.Umur <= item2.Value).Count()
                }),
                JumlahSign = item.Count()
            }).Where(item => item.JumlahSign > 0).OrderByDescending(item => item.JumlahSign);

        Result.Add("Data", DataResult);

        return Result;
    }
    #endregion
}