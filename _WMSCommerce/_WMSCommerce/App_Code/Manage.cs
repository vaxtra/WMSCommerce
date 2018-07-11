using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Manage
/// </summary>
public static class Manage
{
    public static DateTime GetJamServer()
    {
        return DateTime.Now;
    }

    public static bool CheckFileDirectory(string path)
    {
        if (System.IO.File.Exists(path))
            return true;
        else
            return false;
    }

    public static DateTime[] GetRangeMinggu()
    {
        DateTime[] tanggal = new DateTime[7];

        DateTime _tanggalSekarang = GetJamServer();
        int _hariIni = (int)_tanggalSekarang.DayOfWeek;

        _hariIni = (_hariIni == 0) ? 7 : _hariIni;

        tanggal[0] = _tanggalSekarang.AddDays((1 - _hariIni)).Date;
        tanggal[1] = _tanggalSekarang.AddDays((2 - _hariIni)).Date;
        tanggal[2] = _tanggalSekarang.AddDays((3 - _hariIni)).Date;
        tanggal[3] = _tanggalSekarang.AddDays((4 - _hariIni)).Date;
        tanggal[4] = _tanggalSekarang.AddDays((5 - _hariIni)).Date;
        tanggal[5] = _tanggalSekarang.AddDays((6 - _hariIni)).Date;
        tanggal[6] = _tanggalSekarang.AddDays((7 - _hariIni)).Date;

        return tanggal;
    }
    public static List<DateTime> GetRangeBulan()
    {
        List<DateTime> tanggal = new List<DateTime>();

        DateTime _tanggalSekarang = GetJamServer();
        for (var date = new DateTime(_tanggalSekarang.Year, _tanggalSekarang.Month, 1); date.Month == _tanggalSekarang.Month; date = date.AddDays(1))
        {
            tanggal.Add(date);
        }

        return tanggal;
    }

    public static string[] GetRangeDayOfMonth(DateTime _tanggalSekarang)
    {
        string[] RangeBulan = new string[DateTime.DaysInMonth(_tanggalSekarang.Year, _tanggalSekarang.Month)];

        for (int i = 0; i < RangeBulan.Count(); i++)
        {
            RangeBulan[i] = (i + 1).ToString();
        }

        return RangeBulan;
    }

    public static string GetHexaDecimalColor(EnumColor parEnumColor)
    {
        switch (parEnumColor)
        {
            case EnumColor.Primary:
                return "#2196f3";
            case EnumColor.Secondary:
                return "#9e9e9e";
            case EnumColor.Success:
                return "#4db6ac";
            case EnumColor.Danger:
                return "#ef5350";
            case EnumColor.Warning:
                return "#ffd740";
            case EnumColor.Info:
                return "#4dd0e1";
            case EnumColor.Light:
                return "#f5f5f5";
            case EnumColor.Dark:
                return "#424242";
            default:
                {
                    Random rd = new Random();
                    return String.Format("#{0:X6}", rd.Next(0x1000000));
                }
        }
    }

    public static string GetHexaDecimalColor(int parEnumColor)
    {
        switch (parEnumColor)
        {
            case (int)EnumColor.Primary:
                return "#2196f3";
            case (int)EnumColor.Secondary:
                return "#9e9e9e";
            case (int)EnumColor.Success:
                return "#4db6ac";
            case (int)EnumColor.Danger:
                return "#ef5350";
            case (int)EnumColor.Warning:
                return "#ffd740";
            case (int)EnumColor.Info:
                return "#4dd0e1";
            case (int)EnumColor.Light:
                return "#f5f5f5";
            case (int)EnumColor.Dark:
                return "#424242";
            default:
                {
                    Random rd = new Random();
                    return String.Format("#{0:X6}", rd.Next(0x1000000));
                }
        }
    }

    public static string GetHexaDecimalChart(EnumColorChart parEnumColor)
    {
        switch (parEnumColor)
        {
            case EnumColorChart.Primary:
                return "#2196f3";
            case EnumColorChart.Success:
                return "#4db6ac";
            case EnumColorChart.Info:
                return "#4dd0e1";
            case EnumColorChart.Warning:
                return "#ffd740";
            case EnumColorChart.Danger:
                return "#ef5350";
            case EnumColorChart.Secondary:
                return "#9e9e9e";
            case EnumColorChart.Dark:
                return "#424242";
            case EnumColorChart.Light:
                return "#f5f5f5";
            default:
                {
                    Random rd = new Random();
                    return String.Format("#{0:X6}", rd.Next(0x1000000));
                }
        }
    }

    public static string GetHexaDecimalChart(int parEnumColor)
    {
        switch (parEnumColor)
        {
            case (int)EnumColorChart.Primary:
                return "#2196f3";
            case (int)EnumColorChart.Success:
                return "#4db6ac";
            case (int)EnumColorChart.Info:
                return "#4dd0e1";
            case (int)EnumColorChart.Warning:
                return "#ffd740";
            case (int)EnumColorChart.Danger:
                return "#ef5350";
            case (int)EnumColorChart.Secondary:
                return "#9e9e9e";
            case (int)EnumColorChart.Dark:
                return "#424242";
            case (int)EnumColorChart.Light:
                return "#f5f5f5";
            default:
                {
                    Random rd = new Random();
                    return String.Format("#{0:X6}", rd.Next(0x1000000));
                }
        }
    }

    public static string GetHexadecimalSAP(int parEnumColor)
    {
        switch (parEnumColor)
        {
            case (int)EnumColorSAP.Hue1:
                return "#5cbae6";
            case (int)EnumColorSAP.Hue2:
                return "#b6d957";
            case (int)EnumColorSAP.Hue3:
                return "#fac364";
            case (int)EnumColorSAP.Hue4:
                return "#8cd3ff";
            case (int)EnumColorSAP.Hue5:
                return "#d998cb";
            case (int)EnumColorSAP.Hue6:
                return "#f2d249";
            case (int)EnumColorSAP.Hue7:
                return "#93b9c6";
            case (int)EnumColorSAP.Hue8:
                return "#ccc5a8";
            case (int)EnumColorSAP.Hue9:
                return "#52bacc";
            case (int)EnumColorSAP.Hue10:
                return "#dbdb46";
            case (int)EnumColorSAP.Hue11:
                return "#98aafb";
            case (int)EnumColorSAP.SemanticRed:
                return "#f66364";
            case (int)EnumColorSAP.SemanticYellow:
                return "#f5b04d";
            case (int)EnumColorSAP.SemanticGreen:
                return "#71c989";
            case (int)EnumColorSAP.SemanticGray:
                return "#bac1c4";
            default:
                {
                    Random rd = new Random();
                    return String.Format("#{0:X6}", rd.Next(0x1000000));
                }
        }
    }

    public static string GetHexadecimalSAP(EnumColorSAP parEnumColor)
    {
        switch (parEnumColor)
        {
            case EnumColorSAP.Hue1:
                return "#5cbae6";
            case EnumColorSAP.Hue2:
                return "#b6d957";
            case EnumColorSAP.Hue3:
                return "#fac364";
            case EnumColorSAP.Hue4:
                return "#8cd3ff";
            case EnumColorSAP.Hue5:
                return "#d998cb";
            case EnumColorSAP.Hue6:
                return "#f2d249";
            case EnumColorSAP.Hue7:
                return "#93b9c6";
            case EnumColorSAP.Hue8:
                return "#ccc5a8";
            case EnumColorSAP.Hue9:
                return "#52bacc";
            case EnumColorSAP.Hue10:
                return "#dbdb46";
            case EnumColorSAP.Hue11:
                return "#98aafb";
            case EnumColorSAP.SemanticRed:
                return "#f66364";
            case EnumColorSAP.SemanticYellow:
                return "#f5b04d";
            case EnumColorSAP.SemanticGreen:
                return "#71c989";
            case EnumColorSAP.SemanticGray:
                return "#bac1c4";
            default:
                {
                    Random rd = new Random();
                    return String.Format("#{0:X6}", rd.Next(0x1000000));
                }
        }
    }

    #region OUTPUT HTML
    //STRING
    public static string HTMLBagde(EnumColor parEnumColor, string message)
    {
        switch (parEnumColor)
        {
            case EnumColor.Primary:
                return "<span class=\"badge badge-pill badge-primary\">" + message + "</span>";
            case EnumColor.Secondary:
                return "<span class=\"badge badge-pill badge-secondary\">" + message + "</span>";
            case EnumColor.Success:
                return "<span class=\"badge badge-pill badge-success\">" + message + "</span>";
            case EnumColor.Danger:
                return "<span class=\"badge badge-pill badge-danger\">" + message + "</span>";
            case EnumColor.Warning:
                return "<span class=\"badge badge-pill badge-warning\">" + message + "</span>";
            case EnumColor.Info:
                return "<span class=\"badge badge-pill badge-info\">" + message + "</span>";
            case EnumColor.Light:
                return "<span class=\"badge badge-pill badge-light\">" + message + "</span>";
            case EnumColor.Dark:
                return "<span class=\"badge badge-pill badge-dark\">" + message + "</span>";
            default:
                return string.Empty;
        }
    }

    //PROGRESS BAR
    public static string HTMLProgressColor(object percentage)
    {
        decimal hasil;
        decimal.TryParse(percentage.ToString(), out hasil);

        string Output = hasil.ToFormatNumber();

        if (hasil >= 0 && hasil <= 25)
            return "<div class=\"progress\"><div class=\"progress-bar bg-danger\" role=\"progressbar\" style=\"width: " + Output + "%\" aria-valuenow=\"" + Output + "\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + Output + "%</div></div>";
        else if (hasil > 25 && hasil <= 50)
            return "<div class=\"progress\"><div class=\"progress-bar bg-warning\" role=\"progressbar\" style=\"width: " + Output + "%\" aria-valuenow=\"" + Output + "\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + Output + "%</div></div>";
        else if (hasil > 50 && hasil <= 75)
            return "<div class=\"progress\"><div class=\"progress-bar bg-info\" role=\"progressbar\" style=\"width: " + Output + "%\" aria-valuenow=\"" + Output + "\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + Output + "%</div></div>";
        else if (hasil > 75 && hasil <= 100)
            return "<div class=\"progress\"><div class=\"progress-bar bg-success\" role=\"progressbar\" style=\"width: " + Output + "%\" aria-valuenow=\"" + Output + "\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + Output + "%</div></div>";
        else
            return string.Empty;
    }

    //SUPPORT
    public static string HTMLPathImageNotAvailable()
    {
        return "/images/no-image.jpg";
    }

    public static string HTMLDivAlert(EnumColor parEnumColor, string parMessageParent, string parMessageDetail)
    {
        switch (parEnumColor)
        {
            case EnumColor.Primary:
                return "<div class=\"alert alert-primary\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Secondary:
                return "<div class=\"alert alert-secondary\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Success:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Danger:
                return "<div class=\"alert alert-danger\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Warning:
                return "<div class=\"alert alert-warning\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Info:
                return "<div class=\"alert alert-info\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Light:
                return "<div class=\"alert alert-light\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case EnumColor.Dark:
                return "<div class=\"alert alert-dark\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            default:
                return string.Empty;
        }
    }

    public static string HTMLDivAlert(int parEnumAlert, string parMessageParent, string parMessageDetail)
    {
        switch (parEnumAlert)
        {
            case (int)EnumColor.Primary:
                return "<div class=\"alert alert-primary\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Secondary:
                return "<div class=\"alert alert-secondary\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Success:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Danger:
                return "<div class=\"alert alert-danger\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Warning:
                return "<div class=\"alert alert-warning\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Info:
                return "<div class=\"alert alert-info\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Light:
                return "<div class=\"alert alert-light\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            case (int)EnumColor.Dark:
                return "<div class=\"alert alert-dark\" role=\"alert\"><strong>" + parMessageParent + "</strong> " + parMessageDetail + "</div>";
            default:
                return string.Empty;
        }
    }

    public static string HTMLDivAlertCRUD(EnumAlertCRUD parEnumAlert)
    {
        switch (parEnumAlert)
        {
            case EnumAlertCRUD.BerhasilTambah:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>Sukses!</strong> Data berhasil disimpan</div>";
            case EnumAlertCRUD.BerhasilUbah:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>Sukses!</strong> Data berhasil diubah</div>";
            case EnumAlertCRUD.BerhasilHapus:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>Sukses!</strong> Data berhasil dihapus</div>";
            case EnumAlertCRUD.GagalTambah:
                return "<div class=\"alert alert-warning\" role=\"alert\"><strong>Gagal!</strong> Data tidak bisa disimpan, mohon cek kembali data inputan</div>";
            case EnumAlertCRUD.GagalUbah:
                return "<div class=\"alert alert-warning\" role=\"alert\"><strong>Gagal!</strong> Data tidak bisa diubah, mohon cek kembali data inputan</div>";
            case EnumAlertCRUD.GagalHapus:
                return "<div class=\"alert alert-danger\" role=\"alert\"><strong>Gagal!</strong> Data tidak bisa dihapus karena masih berkorelasi dengan data lain</div>";
            default:
                return string.Empty;
        }
    }

    public static string HTMLDivAlertCRUD(int parEnumAlert)
    {
        switch (parEnumAlert)
        {
            case (int)EnumAlertCRUD.BerhasilTambah:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>Sukses!</strong> Data berhasil disimpan</div>";
            case (int)EnumAlertCRUD.BerhasilUbah:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>Sukses!</strong> Data berhasil diubah</div>";
            case (int)EnumAlertCRUD.BerhasilHapus:
                return "<div class=\"alert alert-success\" role=\"alert\"><strong>Sukses!</strong> Data berhasil dihapus</div>";
            case (int)EnumAlertCRUD.GagalTambah:
                return "<div class=\"alert alert-warning\" role=\"alert\"><strong>Gagal!</strong> Data tidak bisa disimpan, mohon cek kembali data inputan</div>";
            case (int)EnumAlertCRUD.GagalUbah:
                return "<div class=\"alert alert-warning\" role=\"alert\"><strong>Gagal!</strong> Data tidak bisa diubah, mohon cek kembali data inputan</div>";
            case (int)EnumAlertCRUD.GagalHapus:
                return "<div class=\"alert alert-danger\" role=\"alert\"><strong>Gagal!</strong> Data tidak bisa dihapus karena masih berkorelasi dengan data lain</div>";
            default:
                return string.Empty;
        }
    }
    #endregion
}