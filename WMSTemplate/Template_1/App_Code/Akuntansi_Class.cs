using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public enum PilihanAkunGrup
{
    Aset = 1,
    HutangLiabilitas,
    Modal,
    Pemasukan,
    Pengeluaran,
    Piutang
}

public enum PilihanPemasukanPengeluaran
{
    Pemasukan = 1,
    Pengeluaran
}

public static class Akuntansi_Class
{
    private static decimal Saldo { get; set; }
    private static DateTime tanggalAwal;
    private static DateTime tanggalAkhir;
    private static PenggunaLogin pengguna;
    private static bool excel;
    private static string linkDownload;
    public static string LinkDownload
    {
        get { return linkDownload; }
    }
    public static string Periode
    {
        get
        {
            if (tanggalAwal == tanggalAkhir)
                return tanggalAwal.ToFormatTanggal();
            else
                return tanggalAwal.ToFormatTanggal() + " - " + tanggalAkhir.ToFormatTanggal();
        }
    }

    #region Arie Anggono
    public static ListItem[] DropdownlistTanggalLaporan()
    {
        //BULAN
        List<ListItem> ListTanggal = new List<ListItem>();
        int bulan = 1;
        for (int i = 1; i <= 31; i++)
        {
            DateTime Tanggal = new DateTime(2016, bulan, i);

            ListTanggal.Add(new ListItem
            {
                Text = Tanggal.ToString("dd", new CultureInfo("id-ID")),
                Value = i.ToString()
            });
        }

        return ListTanggal.ToArray();
    }
    #endregion
    public static ListItem[] DropdownlistBulanLaporan()
    {
        //BULAN
        List<ListItem> ListBulan = new List<ListItem>();

        for (int i = 1; i <= 12; i++)
        {
            DateTime Tanggal = new DateTime(2014, i, 1);

            ListBulan.Add(new ListItem
            {
                Text = Tanggal.ToString("MMMM", new CultureInfo("id-ID")),
                Value = i.ToString()
            });
        }

        return ListBulan.ToArray();
    }

    public static ListItem[] DropdownlistTahunLaporan()
    {
        //TAHUN
        List<ListItem> ListTahun = new List<ListItem>();

        for (int i = 2010; i <= 2025; i++)
        {
            ListTahun.Add(new ListItem
            {
                Text = i.ToString(),
                Value = i.ToString()
            });
        }

        return ListTahun.ToArray();
    }

    //Absolute membuat negatif menjadi positif
    public static decimal HitungSaldo(TBJurnalDetail[] _jurnalDetail, bool angkaAbsolute)
    {
        decimal _saldo = 0;

        foreach (var item in _jurnalDetail)
            _saldo += (decimal)item.Debit - (decimal)item.Kredit;

        if (angkaAbsolute)
            _saldo = Math.Abs(_saldo);

        return _saldo;
    }

    public static decimal HitungSaldoBukuBesar(decimal debit, decimal kredit)
    {
        decimal _saldo = 0;

        _saldo = debit - kredit;

        return Saldo += _saldo;
    }
    public static Dictionary<string, dynamic> LaporanBukuBesar(string periode1, string periode2, int _IDakun, bool _excel, PenggunaLogin pengguna)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var BukuBesar = db.TBJurnalDetails.OrderBy(item => item.TBJurnal.Tanggal).ToArray()
                .Where(item =>
                    item.TBJurnal.Tanggal.Value.Date >= periode1.ToDateTime() &&
                    item.TBJurnal.Tanggal.Value.Date <= periode2.ToDateTime() &&
                    item.IDAkun == _IDakun &&
                    item.TBJurnal.IDTempat == pengguna.IDTempat)
                    .Select(item => new
                    {
                        item.IDJurnal,
                        item.TBJurnal.Tanggal,
                        item.TBJurnal.Referensi,
                        item.TBJurnal.Keterangan,
                        Debit = item.Debit == 0 ? "-" : item.Debit.ToFormatHarga(),
                        Kredit = item.Kredit == 0 ? "-" : item.Kredit.ToFormatHarga(),
                        item.TBAkun.TBAkunGrup.EnumSaldoNormal,
                        Saldo = (HitungSaldoBukuBesar((decimal)item.Debit, (decimal)item.Kredit) < 0) && item.TBAkun.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ? Math.Abs(Saldo) : Saldo
                    })
                    .OrderBy(item => item.Tanggal)
                    .ThenBy(item => item.IDJurnal);

            if (_excel)
            {
                #region EXCEL BUKU BESAR
                Excel_Class Excel_Class = new Excel_Class(pengguna, "Buku Besar", periode1 + " - " + periode2 + " - ", 6);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Worksheet.Cells[1, 1].Value = "Tanggal";
                Worksheet.Cells[1, 2].Value = "No. Referensi";
                Worksheet.Cells[1, 3].Value = "Keterangan";
                Worksheet.Cells[1, 4].Value = "Debit";
                Worksheet.Cells[1, 5].Value = "Kredit";
                Worksheet.Cells[1, 6].Value = "Balance";


                int index = 2;

                foreach (var item in BukuBesar)
                {
                    Excel_Class.Content(index, 1, item.Tanggal.Value);
                    Excel_Class.Content(index, 2, item.Referensi);
                    Excel_Class.Content(index, 3, item.Keterangan);
                    Excel_Class.Content(index, 4, item.Debit);
                    Excel_Class.Content(index, 5, item.Kredit);
                    Excel_Class.Content(index, 6, item.Saldo);
                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }
        }
        return Result;
    }

    public static Dictionary<string, dynamic> LaporanLabaRugi(string bulan, string tahun, bool _excel, PenggunaLogin pengguna, string tipeLaporan)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Pemasukan = db.TBAkuns
                .Where(item => item.TBAkunGrup.IDAkunGrupParent == 4)
                .Select(item => new
                {
                    item.IDAkun,
                    item.Nama,
                    Saldo = HitungSaldo(item.TBJurnalDetails
                    .Where(item2 =>
                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                        item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), true)
                }).ToArray();

            var Pengeluaran = db.TBAkuns
                .Where(item => item.TBAkunGrup.IDAkunGrupParent == 5)
                .Select(item => new
                {
                    TBAkunGrup = item.TBAkunGrup,
                    item.Nama,
                    item.IDAkun,
                    Saldo = HitungSaldo(item.TBJurnalDetails
                    .Where(item2 =>
                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                        item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), true)
                }).ToArray();

            var OPEX = Pengeluaran.Where(item => item.TBAkunGrup.Nama != "Taxation" && item.IDAkun != 404 && item.Nama != "Beban Bunga");
            var PengeluaranTaxInterest = Pengeluaran.Where(item => item.TBAkunGrup.Nama == "Taxation" || item.Nama == "Beban Bunga");


            decimal TotalPemasukan = Pemasukan.Sum(item => item.Saldo);
            decimal TotalPengeluaran = Pengeluaran.Sum(item => item.Saldo);
            decimal TotalLabaRugi = TotalPemasukan - TotalPengeluaran;

            #region MOD TEST
            string NamaAkunPenjualan = Pemasukan.FirstOrDefault(item => item.Nama.ToUpper() == "PENJUALAN").Nama;
            decimal NominalAkunPenjualan = Pemasukan.FirstOrDefault(item => item.Nama.ToUpper() == "PENJUALAN").Saldo;

            string NamaAkunCOGS = Pengeluaran.FirstOrDefault(item => item.Nama == "Harga Pokok Penjualan").Nama;
            decimal NominalCOGS = Pengeluaran.FirstOrDefault(item => item.Nama == "Harga Pokok Penjualan").Saldo;
            decimal NominalGrossProfit = NominalAkunPenjualan - NominalCOGS;
            decimal NominalOPEX = OPEX.Sum(item2 => item2.Saldo);
            decimal NominalEBIT = NominalGrossProfit - NominalOPEX + Pemasukan.Where(item => item.IDAkun != 388).Sum(item2 => item2.Saldo);
            decimal NominalNetIncome = NominalEBIT - PengeluaranTaxInterest.Sum(item => item.Saldo);

            Result.Add("NamaAkunPenjualan", NamaAkunPenjualan);
            Result.Add("NominalAkunPenjualan", NominalAkunPenjualan);
            Result.Add("NamaAkunCOGS", NamaAkunCOGS);
            Result.Add("NominalOPEX", NominalOPEX);
            Result.Add("NominalCOGS", NominalCOGS);
            Result.Add("NominalGrossProfit", NominalGrossProfit);
            Result.Add("NominalEBIT", NominalEBIT);
            Result.Add("NominalNetIncome", NominalNetIncome);

            #endregion

            Result.Add("Pemasukan", Pemasukan.Where(item => item.IDAkun != 388));
            Result.Add("Pengeluaran", OPEX);
            Result.Add("PengeluaranTax", PengeluaranTaxInterest);
            Result.Add("TotalPemasukan", TotalPemasukan);
            Result.Add("TotalPengeluaran", TotalPengeluaran);
            Result.Add("TotalLabaRugi", TotalLabaRugi);

            if (_excel)
            {
                string _bulan = DateTime.Parse(int.Parse(bulan) + "/" + "01" + "/" + "2016").ToString("MMMM", new CultureInfo("id-ID"));

                if (tipeLaporan == "LabaRugi")
                {
                    #region EXCEL LABA RUGI
                    Excel_Class Excel_Class = new Excel_Class(pengguna, "Laba Rugi", _bulan + " - " + tahun, 5);
                    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                    using (var range = Worksheet.Cells[1, 1, 2, 2])
                    {
                        range.Style.Font.Bold = true;
                        range.Merge = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    Excel_Class.Content(1, 1, "Pemasukan");

                    using (var range = Worksheet.Cells[1, 3, 2, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Merge = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    Excel_Class.Content(1, 3, "Pengeluaran");

                    Worksheet.Cells[3, 1].Value = "Akun";
                    Worksheet.Cells[3, 2].Value = "Saldo";
                    Worksheet.Cells[3, 3].Value = "Akun";
                    Worksheet.Cells[3, 4].Value = "Saldo";

                    int index = 4;

                    foreach (var item in Pemasukan)
                    {
                        Excel_Class.Content(index, 1, item.Nama);
                        Excel_Class.Content(index, 2, item.Saldo);
                        index++;
                    }

                    index = 4;

                    foreach (var item in Pengeluaran)
                    {
                        Excel_Class.Content(index, 3, item.Nama);
                        Excel_Class.Content(index, 4, item.Saldo);
                        index++;
                    }

                    Excel_Class.Save();

                    linkDownload = Excel_Class.LinkDownload;
                    #endregion
                }
                else
                {
                    var AktivaPasiva = db.TBAkuns.Where(item =>
                                                                     item.TBAkunGrup.IDAkunGrupParent != 4 &&
                                                                     item.TBAkunGrup.IDAkunGrupParent != 5)
                                                                                .ToArray()
                                                                                .Select(item => new
                                                                                {
                                                                                    item.TBAkunGrup.EnumJenisAkunGrup,
                                                                                    item.TBAkunGrup.EnumSaldoNormal,
                                                                                    Grup = item.TBAkunGrup.Nama,
                                                                                    item.Nama,
                                                                                    Saldo = Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                                    .Where(item2 =>
                                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                                        item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)
                                                                                })
                                                                                .Select(item => new
                                                                                {
                                                                                    item.EnumJenisAkunGrup,
                                                                                    item.EnumSaldoNormal,
                                                                                    item.Grup,
                                                                                    item.Nama,
                                                                                    Saldo = (item.Saldo < 0) && item.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ? Math.Abs(item.Saldo) : item.Saldo
                                                                                });

                    var _aktiva = AktivaPasiva.Where(item => item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva);
                    var _pasiva = AktivaPasiva.Where(item => item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva);

                    #region EXCEL NERACA
                    Excel_Class Excel_Class = new Excel_Class(pengguna, "Laba Rugi", _bulan + " - " + tahun, 5);
                    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                    using (var range = Worksheet.Cells[1, 1, 2, 3])
                    {
                        range.Style.Font.Bold = true;
                        range.Merge = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    Excel_Class.Content(1, 1, "Aktiva");

                    using (var range = Worksheet.Cells[1, 4, 2, 6])
                    {
                        range.Style.Font.Bold = true;
                        range.Merge = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    Excel_Class.Content(1, 4, "Pasiva");

                    Worksheet.Cells[3, 1].Value = "Tipe";
                    Worksheet.Cells[3, 2].Value = "Akun";
                    Worksheet.Cells[3, 3].Value = "Saldo";
                    Worksheet.Cells[3, 4].Value = "Tipe";
                    Worksheet.Cells[3, 5].Value = "Akun";
                    Worksheet.Cells[3, 6].Value = "Saldo";


                    int index = 4;

                    foreach (var item in _aktiva)
                    {
                        Excel_Class.Content(index, 1, item.Grup);
                        Excel_Class.Content(index, 2, item.Nama);
                        Excel_Class.Content(index, 3, item.Saldo);
                        index++;
                    }

                    index = 4;

                    foreach (var item in _pasiva)
                    {
                        Excel_Class.Content(index, 4, item.Grup);
                        Excel_Class.Content(index, 5, item.Nama);
                        Excel_Class.Content(index, 6, item.Saldo);
                        index++;
                    }
                    Excel_Class.Content(index + 1, 4, "Laba/Rugi Bulan Berjalan");
                    Excel_Class.Content(index + 1, 5, "");
                    Excel_Class.Content(index + 1, 6, TotalLabaRugi);

                    Excel_Class.Save();

                    linkDownload = Excel_Class.LinkDownload;
                    #endregion
                }


            }

        }

        return Result;
    }

    public static Dictionary<string, dynamic> LaporanLabaRugi(bool _excel, PenggunaLogin pengguna, string tipeLaporan, string _tgl1, string _tgl2)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            DateTime Tanggal1, Tanggal2;
            Tanggal1 = DateTime.Parse(_tgl1).Date;
            Tanggal2 = DateTime.Parse(_tgl2).Date;

            var Pemasukan = db.TBAkuns
                            .Where(item => item.TBAkunGrup.IDAkunGrupParent == 4 && item.IDTempat == pengguna.IDTempat)
                            .Select(item => new
                            {
                                item.IDAkun,
                                item.Nama,
                                Saldo = HitungSaldo(item.TBJurnalDetails
                                .Where(item2 =>
                                    item2.TBJurnal.Tanggal.Value.Date >= DateTime.Parse(_tgl1) &&
                                    item2.TBJurnal.Tanggal.Value.Date <= DateTime.Parse(_tgl2) &&
                                    item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), true)
                            }).ToArray();

            var Pengeluaran = db.TBAkuns
                            .Where(item => item.TBAkunGrup.IDAkunGrupParent == 5 && item.IDTempat == pengguna.IDTempat)
                            .Select(item => new
                            {
                                TBAkunGrup = item.TBAkunGrup,
                                item.Nama,
                                item.IDAkun,
                                Saldo = HitungSaldo(item.TBJurnalDetails
                                .Where(item2 =>
                                    item2.TBJurnal.Tanggal.Value.Date >= DateTime.Parse(_tgl1) &&
                                    item2.TBJurnal.Tanggal.Value.Date <= DateTime.Parse(_tgl2) &&
                                    item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), true)
                            }).ToArray();

            var OPEX = Pengeluaran.Where(item => item.TBAkunGrup.Nama != "Taxation" && item.IDAkun != 404 && item.Nama != "Beban Bunga");
            var PengeluaranTaxInterest = Pengeluaran.Where(item => item.TBAkunGrup.Nama == "Taxation" || item.Nama == "Beban Bunga");


            decimal TotalPemasukan = Pemasukan.Sum(item => item.Saldo);
            decimal TotalPengeluaran = Pengeluaran.Sum(item => item.Saldo);
            decimal TotalLabaRugi = TotalPemasukan - TotalPengeluaran;

            #region MOD TEST
            string NamaAkunPenjualan = Pemasukan.FirstOrDefault(item => item.Nama.ToUpper() == "PENJUALAN").Nama;
            decimal NominalAkunPenjualan = Pemasukan.FirstOrDefault(item => item.Nama.ToUpper() == "PENJUALAN").Saldo;

            string NamaAkunCOGS = Pengeluaran.FirstOrDefault(item => item.Nama == "Harga Pokok Penjualan").Nama;
            decimal NominalCOGS = Pengeluaran.FirstOrDefault(item => item.Nama == "Harga Pokok Penjualan").Saldo;
            decimal NominalGrossProfit = NominalAkunPenjualan - NominalCOGS;
            decimal NominalOPEX = OPEX.Sum(item2 => item2.Saldo);
            decimal NominalEBIT = NominalGrossProfit - NominalOPEX + Pemasukan.Where(item => item.IDAkun != 388).Sum(item2 => item2.Saldo);
            decimal NominalNetIncome = NominalEBIT - PengeluaranTaxInterest.Sum(item => item.Saldo);

            Result.Add("NamaAkunPenjualan", NamaAkunPenjualan);
            Result.Add("NominalAkunPenjualan", NominalAkunPenjualan);
            Result.Add("NamaAkunCOGS", NamaAkunCOGS);
            Result.Add("NominalOPEX", NominalOPEX);
            Result.Add("NominalCOGS", NominalCOGS);
            Result.Add("NominalGrossProfit", NominalGrossProfit);
            Result.Add("NominalEBIT", NominalEBIT);
            Result.Add("NominalNetIncome", NominalNetIncome);

            #endregion

            Result.Add("Pemasukan", Pemasukan.Where(item => item.IDAkun != 388));
            Result.Add("Pengeluaran", OPEX);
            Result.Add("PengeluaranTax", PengeluaranTaxInterest);
            Result.Add("TotalPemasukan", TotalPemasukan);
            Result.Add("TotalPengeluaran", TotalPengeluaran);
            Result.Add("TotalLabaRugi", TotalLabaRugi);

            if (_excel)
            {
                string periode = Tanggal1 + " - " + Tanggal2;

                if (tipeLaporan == "LabaRugi")
                {
                    #region EXCEL LABA RUGI
                    Excel_Class Excel_Class = new Excel_Class(pengguna, "Laba Rugi", periode, 5);
                    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                    using (var range = Worksheet.Cells[1, 1, 2, 2])
                    {
                        range.Style.Font.Bold = true;
                        range.Merge = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    Excel_Class.Content(1, 1, "Pemasukan");

                    using (var range = Worksheet.Cells[1, 3, 2, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Merge = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    Excel_Class.Content(1, 3, "Pengeluaran");

                    Worksheet.Cells[3, 1].Value = "Akun";
                    Worksheet.Cells[3, 2].Value = "Saldo";
                    Worksheet.Cells[3, 3].Value = "Akun";
                    Worksheet.Cells[3, 4].Value = "Saldo";

                    int index = 4;

                    foreach (var item in Pemasukan)
                    {
                        Excel_Class.Content(index, 1, item.Nama);
                        Excel_Class.Content(index, 2, item.Saldo);
                        index++;
                    }

                    index = 4;

                    foreach (var item in Pengeluaran)
                    {
                        Excel_Class.Content(index, 3, item.Nama);
                        Excel_Class.Content(index, 4, item.Saldo);
                        index++;
                    }

                    Excel_Class.Save();

                    linkDownload = Excel_Class.LinkDownload;
                    #endregion  
                }
            }

        }

        return Result;
    }

    public static Dictionary<string, dynamic> LaporanArusKas(string bulan, string tahun)
    {
        Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Akun = db.TBAkuns
                .Where(item => item.IDAkunGrup == (int)PilihanAkunGrup.Aset)
                .Select(item => new
                {
                    Nama = item.Kode + " - " + item.Nama,
                    BulanLalu = Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                    .Where(item2 =>
                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() - 1).ToArray(), false),

                    BulanIni = Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                    .Where(item2 =>
                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt()).ToArray(), false)
                }).ToArray()
                .Select(item => new
                {
                    item.Nama,
                    item.BulanLalu,
                    item.BulanIni,
                    Perubahan = item.BulanIni - item.BulanLalu
                });

            Result.Add("LaporanArusKas", Akun);
            Result.Add("TotalBulanLalu", Akun.Sum(item => item.BulanLalu));
            Result.Add("TotalBulanIni", Akun.Sum(item => item.BulanIni));
            Result.Add("TotalPerubahan", Akun.Sum(item => item.Perubahan));
        }

        return Result;
    }

    public static decimal LaporanArusKas(string tahun)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var ArusKas = db.TBAkuns
                .Where(item => item.IDAkunGrup == (int)PilihanAkunGrup.Aset)
                .Select(item =>
                    Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                    .Where(item2 => item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt()).ToArray(), false)).ToArray();

            return ArusKas.Sum(item => item);
        }
    }
}