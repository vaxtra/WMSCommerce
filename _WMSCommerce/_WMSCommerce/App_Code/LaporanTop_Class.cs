using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LaporanTop_Class
{
    private PenggunaLogin pengguna;
    private DateTime tanggalAwal;
    private DateTime tanggalAkhir;
    private int idTempat;
    private int idJenisTransaksi;
    private int orderBy;
    private bool excel;
    private bool chart;

    public string Periode
    {
        get
        {
            if (tanggalAwal == tanggalAkhir)
                return tanggalAwal.ToFormatTanggal();
            else
                return tanggalAwal.ToFormatTanggal() + " - " + tanggalAkhir.ToFormatTanggal();
        }
    }

    private string tempPencarian;
    public string TempPencarian
    {
        get { return tempPencarian; }
    }

    private decimal totalQuantity;
    public decimal TotalQuantity
    {
        get { return totalQuantity; }
    }

    private decimal totalPenjualan;
    public decimal TotalPenjualan
    {
        get { return totalPenjualan; }
    }

    private decimal totalDiscount;
    public decimal TotalDiscount
    {
        get { return totalDiscount; }
    }

    private int jumlahData;
    public int JumlahData
    {
        get { return jumlahData; }
    }

    private string linkDownload;
    public string LinkDownload
    {
        get { return linkDownload; }
    }

    private string javascriptChart;
    public string JavascriptChart
    {
        get { return javascriptChart; }
    }

    //PRINT
    public string StoreTempat
    {
        get
        {
            string Result = string.Empty;

            if (idTempat == 0)
                Result = "Semua Tempat";
            else
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Tempat_Class ClassTempat = new Tempat_Class(db);
                    var Tempat = ClassTempat.Cari(idTempat);

                    Result = Tempat.TBStore.Nama + " - " + Tempat.Nama;
                }
            }

            return Result;
        }
    }
    public string JenisTransaksiKeterangan
    {
        get
        {
            string Result = string.Empty;

            if (idJenisTransaksi == 0)
                Result = "Semua Jenis";
            else
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    JenisTransaksi_Class ClassJenisTransaksi = new JenisTransaksi_Class();

                    Result = ClassJenisTransaksi.Cari(db, idJenisTransaksi).Nama;
                }
            }

            return Result;
        }
    }
    public string OrderByKeterangan
    {
        get
        {
            switch (orderBy)
            {
                case 0: return "Berdasarkan Quantity & Penjualan";
                case 1: return "Berdasarkan Quantity";
                case 2: return "Berdasarkan Discount";
                case 3: return "Berdasarkan Penjualan";
                default: return string.Empty;
            }
        }
    }

    public LaporanTop_Class(PenggunaLogin _pengguna, DateTime _tanggalAwal, DateTime _tanggalAkhir, int _idTempat, int _idJenisTransaksi, int _orderBy, bool _excel, bool _chart)
    {
        pengguna = _pengguna;
        tanggalAwal = _tanggalAwal.Date;
        tanggalAkhir = _tanggalAkhir.Date;

        idTempat = _idTempat;
        idJenisTransaksi = _idJenisTransaksi;
        orderBy = _orderBy;
        excel = _excel;
        chart = _chart;
    }
    public dynamic[] TopProduk()
    {
        string judul = "Produk";
        int JumlahKolom = 6;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT QUERY
            var DetailTransaksi = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            tempPencarian += "?TanggalAwal=" + tanggalAwal;
            tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

            if (idTempat > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

            tempPencarian += "&IDTempat=" + idTempat;

            if (idJenisTransaksi > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDJenisTransaksi == idJenisTransaksi).ToArray();

            tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

            totalQuantity = DetailTransaksi.Sum(item => (decimal)item.Quantity);
            totalPenjualan = DetailTransaksi.Sum(item => item.Subtotal.Value);
            totalDiscount = DetailTransaksi.Sum(item => item.Discount.Value * item.Quantity);
            #endregion

            var Result = DetailTransaksi
                .GroupBy(item => item.TBKombinasiProduk.TBProduk.Nama)
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity),
                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                    TotalPenjualan = item.Sum(item2 => item2.Subtotal.Value),
                    Persentase = (orderBy == 1) ? item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100 :
                                 (orderBy == 2) ? item.Sum(item2 => item2.Discount * (decimal)item2.Quantity) / totalDiscount * 100 :
                                 (orderBy == 3) ? item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100 :
                                 (item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100) + (item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100)
                })
                .OrderByDescending(item => item.Persentase)
                .ToArray();

            tempPencarian += "&OrderBy=" + orderBy;

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Produk");
                Excel_Class.Header(3, "Quantity");
                Excel_Class.Header(4, "Total Discount");
                Excel_Class.Header(5, "Total Penjualan");
                Excel_Class.Header(6, "%");

                int index = 2;

                foreach (var item in Result)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Key);
                    Excel_Class.Content(index, 3, item.Quantity);
                    Excel_Class.Content(index, 4, item.TotalDiscount);
                    Excel_Class.Content(index, 5, item.TotalPenjualan);
                    Excel_Class.Content(index, 6, item.Persentase);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            if (chart)
            {
                #region CHART
                //MENGHITUNG JUMLAH DATA
                jumlahData = Result.Count();

                Chart_Class Chart_Class = new Chart_Class();
                Chart_Class.JudulX = judul;
                Chart_Class.JudulY = judul;

                string DataX = string.Empty;
                string DataY = string.Empty;

                foreach (var item in Result)
                {
                    DataX += "\"" + item.Key + "\",";

                    switch (orderBy)
                    {
                        case 0:
                            {
                                Chart_Class.Tooltip = " %";
                                DataY += Math.Round(item.Persentase.Value, 2) + ",";
                            } break;
                        case 1: DataY += item.Quantity + ","; break;
                        case 2: DataY += item.TotalDiscount + ","; break;
                        case 3: DataY += item.TotalPenjualan + ","; break;
                    }
                }

                Chart_Class.DataX = DataX;
                Chart_Class.DataY = DataY;

                Chart_Class.ChartHorizontal();

                javascriptChart = Chart_Class.Javascript;
                #endregion
            }

            return Result;
        }
    }
    public dynamic[] TopVarian()
    {
        string judul = "Varian";
        int JumlahKolom = 6;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT QUERY
            var DetailTransaksi = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            tempPencarian += "?TanggalAwal=" + tanggalAwal;
            tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

            if (idTempat > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

            tempPencarian += "&IDTempat=" + idTempat;

            if (idJenisTransaksi > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDJenisTransaksi == idJenisTransaksi).ToArray();

            tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

            totalQuantity = DetailTransaksi.Sum(item => (decimal)item.Quantity);
            totalPenjualan = DetailTransaksi.Sum(item => item.Subtotal.Value);
            totalDiscount = DetailTransaksi.Sum(item => item.Discount.Value * item.Quantity);
            #endregion

            var Result = DetailTransaksi
                .GroupBy(item => item.TBKombinasiProduk.TBAtributProduk.Nama)
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity),
                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                    TotalPenjualan = item.Sum(item2 => item2.Subtotal),
                    Persentase = (orderBy == 1) ? item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100 :
                                 (orderBy == 2) ? item.Sum(item2 => item2.Discount * (decimal)item2.Quantity) / totalDiscount * 100 :
                                 (orderBy == 3) ? item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100 :
                                 (item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100) + (item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100)
                })
                .OrderByDescending(item => item.Persentase)
                .ToArray();

            tempPencarian += "&OrderBy=" + orderBy;

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Varian");
                Excel_Class.Header(3, "Quantity");
                Excel_Class.Header(4, "Total Discount");
                Excel_Class.Header(5, "Total Penjualan");
                Excel_Class.Header(6, "%");

                int index = 2;

                foreach (var item in Result)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Key);
                    Excel_Class.Content(index, 3, item.Quantity);
                    Excel_Class.Content(index, 4, item.TotalDiscount);
                    Excel_Class.Content(index, 5, item.TotalPenjualan.Value);
                    Excel_Class.Content(index, 6, item.Persentase);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            if (chart)
            {
                #region CHART
                ////MENGHITUNG JUMLAH DATA
                jumlahData = Result.Count();

                Chart_Class Chart_Class = new Chart_Class();
                Chart_Class.JudulX = judul;
                Chart_Class.JudulY = judul;

                string DataX = string.Empty;
                string DataY = string.Empty;

                foreach (var item in Result)
                {
                    DataX += "\"" + item.Key + "\",";

                    switch (orderBy)
                    {
                        case 0:
                            {
                                Chart_Class.Tooltip = " %";
                                DataY += Math.Round(item.Persentase.Value, 2) + ",";
                            } break;
                        case 1: DataY += item.Quantity + ","; break;
                        case 2: DataY += item.TotalDiscount + ","; break;
                        case 3: DataY += item.TotalPenjualan + ","; break;
                    }
                }

                Chart_Class.DataX = DataX;
                Chart_Class.DataY = DataY;

                Chart_Class.ChartHorizontal();

                javascriptChart = Chart_Class.Javascript;
                #endregion
            }

            return Result;
        }
    }
    public dynamic[] TopWarna()
    {
        string judul = "Warna";
        int JumlahKolom = 6;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT QUERY
            var DetailTransaksi = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            tempPencarian += "?TanggalAwal=" + tanggalAwal;
            tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

            if (idTempat > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

            tempPencarian += "&IDTempat=" + idTempat;

            if (idJenisTransaksi > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDJenisTransaksi == idJenisTransaksi).ToArray();

            tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

            totalQuantity = DetailTransaksi.Sum(item => (decimal)item.Quantity);
            totalPenjualan = DetailTransaksi.Sum(item => item.Subtotal.Value);
            totalDiscount = DetailTransaksi.Sum(item => item.Discount.Value * item.Quantity);
            #endregion

            var Result = DetailTransaksi
                .GroupBy(item => item.TBKombinasiProduk.TBProduk.TBWarna.Nama)
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity),
                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                    TotalPenjualan = item.Sum(item2 => item2.Subtotal),
                    Persentase = (orderBy == 1) ? item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100 :
                                 (orderBy == 2) ? item.Sum(item2 => item2.Discount * (decimal)item2.Quantity) / totalDiscount * 100 :
                                 (orderBy == 3) ? item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100 :
                                 (item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100) + (item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100)
                })
                .OrderByDescending(item => item.Persentase)
                .ToArray();

            tempPencarian += "&OrderBy=" + orderBy;

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Warna");
                Excel_Class.Header(3, "Quantity");
                Excel_Class.Header(4, "Total Discount");
                Excel_Class.Header(5, "Total Penjualan");
                Excel_Class.Header(6, "%");

                int index = 2;

                foreach (var item in Result)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Key);
                    Excel_Class.Content(index, 3, item.Quantity);
                    Excel_Class.Content(index, 4, item.TotalDiscount);
                    Excel_Class.Content(index, 5, item.TotalPenjualan.Value);
                    Excel_Class.Content(index, 6, item.Persentase);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            if (chart)
            {
                #region CHART
                //MENGHITUNG JUMLAH DATA
                jumlahData = Result.Count();

                Chart_Class Chart_Class = new Chart_Class();
                Chart_Class.JudulX = judul;
                Chart_Class.JudulY = judul;

                string DataX = string.Empty;
                string DataY = string.Empty;

                foreach (var item in Result)
                {
                    DataX += "\"" + item.Key + "\",";

                    switch (orderBy)
                    {
                        case 0:
                            {
                                Chart_Class.Tooltip = " %";
                                DataY += Math.Round(item.Persentase.Value, 2) + ",";
                            } break;
                        case 1: DataY += item.Quantity + ","; break;
                        case 2: DataY += item.TotalDiscount + ","; break;
                        case 3: DataY += item.TotalPenjualan + ","; break;
                    }
                }

                Chart_Class.DataX = DataX;
                Chart_Class.DataY = DataY;

                Chart_Class.ChartHorizontal();

                javascriptChart = Chart_Class.Javascript;
                #endregion
            }

            return Result;
        }
    }
    public dynamic[] TopKategori()
    {
        string judul = "Kategori";
        int JumlahKolom = 6;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT QUERY
            var DetailTransaksi = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            tempPencarian += "?TanggalAwal=" + tanggalAwal;
            tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

            if (idTempat > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

            tempPencarian += "&IDTempat=" + idTempat;

            if (idJenisTransaksi > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDJenisTransaksi == idJenisTransaksi).ToArray();

            tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

            totalQuantity = DetailTransaksi.Sum(item => (decimal)item.Quantity);
            totalPenjualan = DetailTransaksi.Sum(item => item.Subtotal.Value);
            totalDiscount = DetailTransaksi.Sum(item => item.Discount.Value * item.Quantity);
            #endregion

            var Result = DetailTransaksi
                .GroupBy(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 1 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama)
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity),
                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                    TotalPenjualan = item.Sum(item2 => item2.Subtotal),
                    Persentase = (orderBy == 1) ? item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100 :
                                 (orderBy == 2) ? item.Sum(item2 => item2.Discount * (decimal)item2.Quantity) / totalDiscount * 100 :
                                 (orderBy == 3) ? item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100 :
                                 (item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100) + (item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100)
                })
                .OrderByDescending(item => item.Persentase)
                .ToArray();

            tempPencarian += "&OrderBy=" + orderBy;

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Kategori");
                Excel_Class.Header(3, "Quantity");
                Excel_Class.Header(4, "Total Discount");
                Excel_Class.Header(5, "Total Penjualan");
                Excel_Class.Header(6, "%");

                int index = 2;

                foreach (var item in Result)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Key);
                    Excel_Class.Content(index, 3, item.Quantity);
                    Excel_Class.Content(index, 4, item.TotalDiscount);
                    Excel_Class.Content(index, 5, item.TotalPenjualan.Value);
                    Excel_Class.Content(index, 6, item.Persentase);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            if (chart)
            {
                #region CHART
                //MENGHITUNG JUMLAH DATA
                jumlahData = Result.Count();

                Chart_Class Chart_Class = new Chart_Class();
                Chart_Class.JudulX = judul;
                Chart_Class.JudulY = judul;

                string DataX = string.Empty;
                string DataY = string.Empty;

                foreach (var item in Result)
                {
                    DataX += "\"" + item.Key + "\",";

                    switch (orderBy)
                    {
                        case 0:
                            {
                                Chart_Class.Tooltip = " %";
                                DataY += Math.Round(item.Persentase.Value, 2) + ",";
                            } break;
                        case 1: DataY += item.Quantity + ","; break;
                        case 2: DataY += item.TotalDiscount + ","; break;
                        case 3: DataY += item.TotalPenjualan + ","; break;
                    }
                }

                Chart_Class.DataX = DataX;
                Chart_Class.DataY = DataY;

                Chart_Class.ChartHorizontal();

                javascriptChart = Chart_Class.Javascript;
                #endregion
            }

            return Result;
        }
    }
    public dynamic[] TopBrand()
    {
        string judul = "Brand";
        int JumlahKolom = 6;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT QUERY
            var DetailTransaksi = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            tempPencarian += "?TanggalAwal=" + tanggalAwal;
            tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

            if (idTempat > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

            tempPencarian += "&IDTempat=" + idTempat;

            if (idJenisTransaksi > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDJenisTransaksi == idJenisTransaksi).ToArray();

            tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

            totalQuantity = DetailTransaksi.Sum(item => (decimal)item.Quantity);
            totalPenjualan = DetailTransaksi.Sum(item => item.Subtotal.Value);
            totalDiscount = DetailTransaksi.Sum(item => item.Discount.Value * item.Quantity);
            #endregion

            var Result = DetailTransaksi
                .GroupBy(item => item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama)
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity),
                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                    TotalPenjualan = item.Sum(item2 => item2.Subtotal),
                    Persentase = (orderBy == 1) ? item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100 :
                                 (orderBy == 2) ? item.Sum(item2 => item2.Discount * (decimal)item2.Quantity) / totalDiscount * 100 :
                                 (orderBy == 3) ? item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100 :
                                 (item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100) + (item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100)
                })
                .OrderByDescending(item => item.Persentase)
                .ToArray();

            tempPencarian += "&OrderBy=" + orderBy;

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Brand");
                Excel_Class.Header(3, "Quantity");
                Excel_Class.Header(4, "Total Discount");
                Excel_Class.Header(5, "Total Penjualan");
                Excel_Class.Header(6, "%");

                int index = 2;

                foreach (var item in Result)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Key);
                    Excel_Class.Content(index, 3, item.Quantity);
                    Excel_Class.Content(index, 4, item.TotalDiscount);
                    Excel_Class.Content(index, 5, item.TotalPenjualan.Value);
                    Excel_Class.Content(index, 6, item.Persentase);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            if (chart)
            {
                #region CHART
                //MENGHITUNG JUMLAH DATA
                jumlahData = Result.Count();

                Chart_Class Chart_Class = new Chart_Class();
                Chart_Class.JudulX = judul;
                Chart_Class.JudulY = judul;

                string DataX = string.Empty;
                string DataY = string.Empty;

                foreach (var item in Result)
                {
                    DataX += "\"" + item.Key + "\",";

                    switch (orderBy)
                    {
                        case 0:
                            {
                                Chart_Class.Tooltip = " %";
                                DataY += Math.Round(item.Persentase.Value, 2) + ",";
                            } break;
                        case 1: DataY += item.Quantity + ","; break;
                        case 2: DataY += item.TotalDiscount + ","; break;
                        case 3: DataY += item.TotalPenjualan + ","; break;
                    }
                }

                Chart_Class.DataX = DataX;
                Chart_Class.DataY = DataY;

                Chart_Class.ChartHorizontal();

                javascriptChart = Chart_Class.Javascript;
                #endregion
            }

            return Result;
        }
    }
    public dynamic[] TopKombinasiProduk()
    {
        string judul = "Produk dan Varian";
        int JumlahKolom = 10;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT QUERY
            var DetailTransaksi = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.TanggalOperasional.Value.Date >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value.Date <= tanggalAkhir &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                .ToArray();

            tempPencarian += "?TanggalAwal=" + tanggalAwal;
            tempPencarian += "&TanggalAkhir=" + tanggalAkhir;

            if (idTempat > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDTempat == idTempat).ToArray();

            tempPencarian += "&IDTempat=" + idTempat;

            if (idJenisTransaksi > 0)
                DetailTransaksi = DetailTransaksi.Where(item => item.TBTransaksi.IDJenisTransaksi == idJenisTransaksi).ToArray();

            tempPencarian += "&IDJenisTransaksi=" + idJenisTransaksi;

            totalQuantity = DetailTransaksi.Sum(item => (decimal)item.Quantity);
            totalPenjualan = DetailTransaksi.Sum(item => item.Subtotal.Value);
            totalDiscount = DetailTransaksi.Sum(item => item.Discount.Value * item.Quantity);
            #endregion

            var Result = DetailTransaksi
                .GroupBy(item => new
                {
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 1 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Skip(1).FirstOrDefault().TBKategoriProduk.Nama,
                    Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama
                })
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity),
                    TotalDiscount = item.Sum(item2 => item2.Discount * item2.Quantity),
                    TotalPenjualan = item.Sum(item2 => item2.Subtotal),
                    Persentase = (orderBy == 1) ? item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100 :
                                 (orderBy == 2) ? item.Sum(item2 => item2.Discount * (decimal)item2.Quantity) / totalDiscount * 100 :
                                 (orderBy == 3) ? item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100 :
                                 (item.Sum(item2 => (decimal)item2.Quantity) / totalQuantity * 100) + (item.Sum(item2 => item2.Subtotal.Value) / totalPenjualan * 100)
                })
                .OrderByDescending(item => item.Persentase)
                .ToArray();

            tempPencarian += "&OrderBy=" + orderBy;

            if (excel)
            {
                #region EXCEL
                Excel_Class Excel_Class = new Excel_Class(pengguna, judul, Periode, JumlahKolom);
                ExcelWorksheet Worksheet = Excel_Class.Worksheet;

                Excel_Class.Header(1, "No.");
                Excel_Class.Header(2, "Produk");
                Excel_Class.Header(3, "Varian");
                Excel_Class.Header(4, "Warna");
                Excel_Class.Header(5, "Kategori");
                Excel_Class.Header(6, "Brand");
                Excel_Class.Header(7, "Quantity");
                Excel_Class.Header(8, "Total Discount");
                Excel_Class.Header(9, "Total Penjualan");
                Excel_Class.Header(10, "%");

                int index = 2;

                foreach (var item in Result)
                {
                    Excel_Class.Content(index, 1, index - 1);
                    Excel_Class.Content(index, 2, item.Key.Produk);
                    Excel_Class.Content(index, 3, item.Key.Varian);
                    Excel_Class.Content(index, 4, item.Key.Warna);
                    Excel_Class.Content(index, 5, item.Key.Kategori);
                    Excel_Class.Content(index, 6, item.Key.Brand);
                    Excel_Class.Content(index, 7, item.Quantity);
                    Excel_Class.Content(index, 8, item.TotalDiscount);
                    Excel_Class.Content(index, 9, item.TotalPenjualan.Value);
                    Excel_Class.Content(index, 10, item.Persentase);

                    index++;
                }

                Excel_Class.Save();

                linkDownload = Excel_Class.LinkDownload;
                #endregion
            }

            if (chart)
            {
                #region CHART
                //MENGHITUNG JUMLAH DATA
                jumlahData = Result.Count();

                Chart_Class Chart_Class = new Chart_Class();
                Chart_Class.JudulX = judul;
                Chart_Class.JudulY = judul;

                string DataX = string.Empty;
                string DataY = string.Empty;

                foreach (var item in Result)
                {
                    DataX += "\"" + item.Key.Produk + (!string.IsNullOrWhiteSpace(item.Key.Varian) ? " (" + item.Key.Varian + ")" : "") + "\",";

                    switch (orderBy)
                    {
                        case 0:
                            {
                                Chart_Class.Tooltip = " %";
                                DataY += Math.Round(item.Persentase.Value, 2) + ",";
                            } break;
                        case 1: DataY += item.Quantity + ","; break;
                        case 2: DataY += item.TotalDiscount + ","; break;
                        case 3: DataY += item.TotalPenjualan + ","; break;
                    }
                }

                Chart_Class.DataX = DataX;
                Chart_Class.DataY = DataY;

                Chart_Class.ChartHorizontal();

                javascriptChart = Chart_Class.Javascript;
                #endregion
            }

            return Result;
        }
    }
}