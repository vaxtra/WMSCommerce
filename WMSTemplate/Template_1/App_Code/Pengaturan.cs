using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

#region Enum
public enum PilihanStatusSoal
{
    Aktif = 1,
    TidakAktif
}
public enum JenisPotonganHarga
{
    TidakAda = 0,
    Harga = 1,
    Persentase = 2
}
public enum PilihanBonusGrupPelanggan
{
    Potongan = 1,
    Komisi = 2,
    Deposit = 3
}
public enum PilihanBiayaJenisPembayaran
{
    TidakAda = 0,
    Harga = 1,
    Persentase = 2
}



public enum PilihanPotonganTransaksi
{
    TidakAda = 0,
    Harga = 1,
    Persentase = 2
}

public enum PilihanJenisBebanBiaya
{
    TidakAda = 1,
    BebanCustomer,
    BebanStore
}

public enum PilihanStatusPrint
{
    Order = 1,
    Tambahan = 2,
    Void = 3,
    Reprint = 4
}


#endregion



#region Banu
public enum PilihanOrderKategori
{
    Makanan = 1,
    Minuman
}

public enum PilihanOrderStatus
{
    Cancel = 0,
    TakingOrder,
    Cooking,
    Done
}

public enum PilihanOrderMeja
{
    DineIn = 1,
    Tambahan,
    Delivery,
    TakeAway
}

public enum PilihanBiayaProyeksi
{
    TidakAda = 0,
    Persen,
    Harga
}

public enum PilihanStatusBatasProyeksi
{
    TidakAda = 0,
    BatasBawah,
    BatasTengah,
    BatasAtas
}

public enum PilihanBiayaProduksi
{
    TidakAda = 0,
    Persen,
    Harga
}

public enum PilihanJenisTransfer
{
    TransferProses = 1,
    PermintaanProses = 2,

    TransferBatal = 3,
    PermintaanBatal = 4,

    TransferPending = 5,
    PermintaanPending = 6,

    TransferSelesai = 7,
    PermintaanSelesai = 8
}

//public enum PilihanJenisTransaksiLainnya
//{
//    Batal = 1,
//    Pending,
//    Selesai
//}

#region PRODUKSI
public enum PilihanEnumStatusProyeksi
{
    Proses = 1,
    Selesai,
    Batal
}
public enum PilihanEnumStatusPOProduksi
{
    Baru = 1,
    Proses,
    Selesai,
    Batal
}
public enum PilihanEnumJenisProduksi
{
    PurchaseOrder = 1,
    ProduksiSendiri,
    ProduksiKeSupplierVendor
}
public enum PilihanEnumJenisHPP
{
    HargaSupplierVendor = 1,
    Komposisi,
    RataRata,
    KomposisiTambahHargaSupplierVendor,
    RataRataTambahHargaSupplierVendor
}
public enum PilihanEnumStatusPenerimaanPO
{
    Datang = 1,
    Terima
}
#endregion

public enum EnumStatusPORetur
{
    Baru = 1,
    Proses,
    Selesai,
    Batal
}

public enum PilihanStatusPO
{
    Baru = 1,
    Proses,
    Selesai,
    Batal
}

public enum PilihanStatusPenerimaan
{
    Pending = 1,
    Selesai
}

public enum PilihanStatusProduksi
{
    Proyeksi = 1,
    Pending,
    Proses,
    Selesai,
    Batal
}

#endregion

public enum PilihanJenisAkunGrup
{
    Aktiva = 1,
    Pasiva
}

public enum PilihanHutangPiutang
{
    Hutang = 1,
    Piutang = 2,
    HutangSisa = 3,
    PiutangSisa = 4,
    HutangLunas = 5,
    PiutangLunas = 6
}

public enum PilihanDebitKredit
{
    Debit = 1,
    Kredit = 2
}
public enum PilihanBertambahBerkurang
{
    Bertambah = 1,
    Berkurang = 2
}
public static class Pengaturan
{
    public static string ReplaceNol(string input)
    {
        if (input == "0")
            return "";
        else
            return input;
    }
    #region Format Harga Indonesia
    public static string FormatHargaNegatif(string harga)
    {
        if (harga.StartsWith("-"))
            return "<span style='color: red;'>" + harga.Replace("-", "(") + ")</span>";
        else
            return harga;
    }
    public static string FormatHarga(object harga)
    {
        if (harga != null)
            return FormatHarga(harga.ToString());
        else
            return "0";
    }

    public static string FormatHarga(string harga)
    {
        if (!string.IsNullOrWhiteSpace(harga))
        {
            decimal Harga;
            decimal.TryParse(harga, out Harga);

            return FormatHarga(Harga);
        }
        else
            return "0";
    }

    public static string FormatHarga(decimal Harga)
    {
        string hasil = Harga.ToString("N", CultureInfo.CreateSpecificCulture("id-ID")).Replace('p', '(');
        return (hasil.Contains(",00")) ? hasil.Replace(",00", "") : hasil;
    }

    public static bool FormatHarga(Label LabelInput, decimal? harga)
    {
        var Harga = harga.HasValue ? harga.Value : 0;

        if (Harga < 0)
        {
            LabelInput.ForeColor = System.Drawing.Color.Red;
            LabelInput.Text = "(" + Pengaturan.FormatHarga(Math.Abs(Harga)) + ")";
        }
        else
        {
            LabelInput.ForeColor = System.Drawing.Color.Black;
            LabelInput.Text = Pengaturan.FormatHarga(Harga);
        }

        return Harga != 0;
    }

    public static string FormatHargaRepeater(decimal? harga)
    {
        var Harga = harga.HasValue ? harga.Value : 0;

        if (Harga < 0)
            return "<td class='text-right FontRed'>" + "(" + Pengaturan.FormatHarga(Math.Abs(Harga)) + ")" + "</td>";
        else
            return "<td class='text-right'>" + Pengaturan.FormatHarga(Harga) + "</td>";
    }

    public static decimal FormatAngkaInput(string angka)
    {
        if (!string.IsNullOrWhiteSpace(angka))
        {
            decimal Angka;
            decimal.TryParse(angka.Replace(".", "").Replace(",", "."), out Angka);

            return Angka;
        }
        else
            return 0;
    }
    public static decimal FormatAngkaInput(TextBox TextBoxAngka)
    {
        return FormatAngkaInput(TextBoxAngka.Text);
    }
    #endregion

    #region Format Harga English United States

    public static string FormatHargaUS(object harga)
    {
        if (harga != null)
            return FormatHargaUS(harga.ToString());
        else
            return "0";
    }
    public static string FormatHargaUS(string harga)
    {
        decimal Harga;
        decimal.TryParse(harga, out Harga);

        return FormatHargaUS(Harga);
    }

    public static string FormatHargaUS(decimal Harga)
    {
        string hasil = Harga.ToString("N", CultureInfo.CreateSpecificCulture("en-US")).Replace('p', '(');
        return (hasil.Contains(".00")) ? hasil.Replace(".00", "") : hasil;
    }
    #endregion

    #region Cek Null Value
    public static string CekNullValueHarga(object value)
    {
        if (value == null)
            return "0";
        else
            return Pengaturan.FormatHarga(value.ToString());
    }

    public static decimal CekNullValueDesimal(object value)
    {
        if (value == null)
            return 0;
        else
            return (decimal)value;
    }

    public static int CekNullValueInteger(object value)
    {
        if (value == null)
            return 0;
        else
            return (int)value;
    }

    public static string CekNullValueKata(string value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value))
            return "";
        else
            return value;
    }
    #endregion

    #region Format Status True | False
    public static string FormatStatus(object status)
    {
        return status.ToBool() ? "/assets/images/enabled.gif" : "/assets/images/disabled.gif";
    }
    public static string FormatDataStatus(object status)
    {
        return "<img src='" + (status.ToBool() ? "/assets/images/enabled.gif" : "/assets/images/disabled.gif") + "' />";
    }

    public static string FormatStatusStok(bool status)
    {
        return status ? "/assets/images/arrow_up.png" : "/assets/images/arrow_down.png";
    }
    #endregion

    public static void ResetAutoIncrement(string namaTable)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            db.ExecuteCommand("DBCC CHECKIDENT('" + namaTable + "', RESEED, 0);");
        }
    }

    public static string JenisPotongan(object pilihan)
    {
        if (pilihan != null)
        {
            int Pilihan;
            int.TryParse(pilihan.ToString(), out Pilihan);

            switch ((JenisPotonganHarga)Pilihan)
            {
                case JenisPotonganHarga.TidakAda: return "Tidak Ada";
                case JenisPotonganHarga.Harga: return "Harga";
                case JenisPotonganHarga.Persentase: return "Persentase";
                default: return string.Empty;
            }
        }
        else
            return string.Empty;
    }

    #region ARIE ANGGONO
    public static string JenisHutangPiutang(object pilihan)
    {
        if (pilihan != null)
        {
            int Pilihan;
            int.TryParse(pilihan.ToString(), out Pilihan);

            switch ((PilihanHutangPiutang)Pilihan)
            {
                case PilihanHutangPiutang.Hutang: return "<label class=\"label label-important\">Hutang</label>";
                case PilihanHutangPiutang.HutangLunas: return "<label class=\"label label-success\">Hutang Lunas</label>";
                case PilihanHutangPiutang.HutangSisa: return "<label class=\"label label-warning\">Hutang Sisa</label>";
                case PilihanHutangPiutang.Piutang: return "<label class=\"label label-important\">Piutang</label>";
                case PilihanHutangPiutang.PiutangLunas: return "<label class=\"label label-success\">Piutang Lunas</label>";
                case PilihanHutangPiutang.PiutangSisa: return "<label class=\"label label-warning\">Piutang Sisa</label>";
                default: return string.Empty;
            }

        }
        else
            return string.Empty;
    }
    #endregion
    public static string JenisTransfer(object pilihan)
    {
        if (pilihan != null)
        {
            int Pilihan;
            int.TryParse(pilihan.ToString(), out Pilihan);

            switch ((PilihanJenisTransfer)Pilihan)
            {
                case PilihanJenisTransfer.TransferProses: return "Transfer Proses";
                case PilihanJenisTransfer.PermintaanProses: return "Permintaan Proses";
                case PilihanJenisTransfer.TransferBatal: return "Transfer Batal";
                case PilihanJenisTransfer.PermintaanBatal: return "Permintaan Batal";
                case PilihanJenisTransfer.TransferPending: return "Transfer Pending";
                case PilihanJenisTransfer.PermintaanPending: return "Permintaan Pending";
                case PilihanJenisTransfer.TransferSelesai: return "Transfer Selesai";
                case PilihanJenisTransfer.PermintaanSelesai: return "Permintaan Selesai";
                default: return string.Empty;
            }
        }
        else
            return string.Empty;
    }

    public static string StatusTransfer(object pilihan)
    {
        if (pilihan != null)
        {
            int Pilihan;
            int.TryParse(pilihan.ToString(), out Pilihan);

            switch ((PilihanJenisTransfer)Pilihan)
            {
                case PilihanJenisTransfer.TransferPending: return "<span class=\"badge badge-pill badge-secondary\">Pending</span>";
                case PilihanJenisTransfer.TransferProses: return "<span class=\"badge badge-pill badge-info\">Proses</span>";
                case PilihanJenisTransfer.TransferSelesai: return "<span class=\"badge badge-pill badge-success\">Selesai</span>";
                case PilihanJenisTransfer.TransferBatal: return "<span class=\"badge badge-pill badge-danger\">Batal</span>";
                default: return string.Empty;
            }
        }
        else
            return string.Empty;
    }

    public static string StatusSoal(object pilihan)
    {
        if (pilihan != null)
        {
            switch ((PilihanStatusSoal)pilihan.ToInt())
            {
                case PilihanStatusSoal.Aktif: return "Aktif";
                case PilihanStatusSoal.TidakAktif: return "Tidak Aktif";
                default: return string.Empty;
            }
        }
        else
            return string.Empty;
    }

    //public static string JenisTransaksiLainnya(object pilihan)
    //{
    //    if (pilihan != null)
    //    {
    //        int Pilihan;
    //        int.TryParse(pilihan.ToString(), out Pilihan);

    //        switch ((PilihanJenisTransaksiLainnya)Pilihan)
    //        {
    //            case PilihanJenisTransaksiLainnya.Batal: return "Batal";
    //            case PilihanJenisTransaksiLainnya.Pending: return "Pending";
    //            case PilihanJenisTransaksiLainnya.Selesai: return "Selesai";
    //            default: return string.Empty;
    //        }
    //    }
    //    else
    //        return string.Empty;
    //}

    public static string JenisTransferHTML(object pilihan)
    {
        if (pilihan != null)
        {
            int Pilihan;
            int.TryParse(pilihan.ToString(), out Pilihan);

            switch ((PilihanJenisTransfer)Pilihan)
            {
                case PilihanJenisTransfer.TransferProses: return "<span class='badge badge-pill badge-primary'>Transfer Proses</span>";
                case PilihanJenisTransfer.PermintaanProses: return "<span class='badge badge-pill badge-primary'>Permintaan Proses</span>";
                case PilihanJenisTransfer.TransferBatal: return "<span class='badge badge-pill badge-danger'>Transfer Batal</span>";
                case PilihanJenisTransfer.PermintaanBatal: return "<span class='badge badge-pill badge-danger'>Permintaan Batal</span>";
                case PilihanJenisTransfer.TransferPending: return "<span class='badge badge-pill badge-warning'>Transfer Pending</span>";
                case PilihanJenisTransfer.PermintaanPending: return "<span class='badge badge-pill badge-warning'>Permintaan Pending</span>";
                case PilihanJenisTransfer.TransferSelesai: return "<span class='badge badge-pill badge-success'>Transfer Selesai</span>";
                case PilihanJenisTransfer.PermintaanSelesai: return "<span class='badge badge-pill badge-success'>Permintaan Selesai</span>";
                default: return string.Empty;
            }
        }
        else
            return string.Empty;
    }
    /// <summary>
    /// dddd
    /// </summary>
    public static string Hari(DateTime Tanggal)
    {
        return Tanggal.ToString("dddd", new CultureInfo("id-ID"));
    }
    /// <summary>
    /// MMMM
    /// </summary>
    public static string Bulan(int Angka)
    {
        return new DateTime(2015, Angka, 1).ToString("MMMM", new CultureInfo("id-ID"));
    }
    public static string Bulan(DateTime Tanggal)
    {
        return Tanggal.ToString("MMMM", new CultureInfo("id-ID"));
    }
    /// <summary>
    /// dddd, d MMMM yyyy HH.mm
    /// </summary>
    public static string FormatTanggal(object tanggal)
    {
        if (tanggal != null)
        {
            DateTime Tanggal;
            bool valid = DateTime.TryParse(tanggal.ToString(), out Tanggal);

            if (valid)
                return Tanggal.ToString("dddd, d MMMM yyyy HH.mm", new CultureInfo("id-ID"));
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }
    /// <summary>
    /// dddd, d MMMM yyyy
    /// </summary>
    public static string FormatTanggalHari(object tanggal)
    {
        if (tanggal != null)
        {
            DateTime Tanggal;
            bool valid = DateTime.TryParse(tanggal.ToString(), out Tanggal);

            if (valid)
                return Tanggal.ToString("dddd, d MMMM yyyy", new CultureInfo("id-ID"));
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }
    /// <summary>
    /// d MMMM yyyy
    /// </summary>
    public static string FormatTanggalRingkas(object tanggal)
    {
        if (tanggal != null)
        {
            DateTime Tanggal;
            bool valid = DateTime.TryParse(tanggal.ToString(), out Tanggal);

            if (valid)
                return Tanggal.ToString("d MMMM yyyy", new CultureInfo("id-ID"));
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }
    /// <summary>
    /// d MMMM yyyy HH:mm
    /// </summary>
    public static string FormatTanggalJam(object tanggal)
    {
        if (tanggal != null)
        {
            DateTime Tanggal;
            bool valid = DateTime.TryParse(tanggal.ToString(), out Tanggal);

            if (valid)
                return Tanggal.ToString("d MMMM yyyy HH:mm", new CultureInfo("id-ID"));
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }

    public static string FormatJam(object tanggal)
    {
        if (tanggal != null)
        {
            DateTime Tanggal;
            bool valid = DateTime.TryParse(tanggal.ToString(), out Tanggal);

            if (valid)
                return Tanggal.ToString("HH:mm", new CultureInfo("id-ID"));
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }

    public static DateTime? FormatTanggalInput(string tanggal)
    {
        if (!string.IsNullOrWhiteSpace(tanggal))
        {
            DateTime Tanggal;

            bool valid = DateTime.TryParseExact(tanggal, "dddd, d MMMM yyyy HH.mm", new CultureInfo("id-ID"), DateTimeStyles.None, out Tanggal);

            if (valid)
                return Tanggal;
            else
                return (DateTime?)null;
        }
        else
            return (DateTime?)null;
    }

    public static string LabelStatusTransaksi(int idStatusTransaksi, string statusTransaksi)
    {
        switch (idStatusTransaksi)
        {
            case 1: return "<span class=\"label label-warning\">" + statusTransaksi + "</span>";
            case 2: return "<span class=\"label label-warning\">" + statusTransaksi + "</span>";
            case 3: return "<span class=\"label label-warning\">" + statusTransaksi + "</span>";
            case 4: return "<span class=\"label label-success\">" + statusTransaksi + "</span>";
            case 5: return "<span class=\"label label-success\">" + statusTransaksi + "</span>";
            case 6: return "<span class=\"label label-important\">" + statusTransaksi + "</span>";
            default: return string.Empty;
        }
    }

    public static string ProgressStatusTransaksi(int idStatusTransaksi, string statusTransaksi)
    {
        switch (idStatusTransaksi)
        {
            case 1: return "<div class=\"details-wrapper\"><div class=\"name\">" + statusTransaksi + "</div><div class=\"description\"><div class=\"progress progress-small\"><div data-percentage=\"20%\" class=\"progress-bar progress-bar-warning\" style=\"width: 20%;\"></div></div></div></div>";
            case 2: return "<div class=\"details-wrapper\"><div class=\"name\">" + statusTransaksi + "</div><div class=\"description\"><div class=\"progress progress-small\"><div data-percentage=\"40%\" class=\"progress-bar progress-bar-warning\" style=\"width: 40%;\"></div></div></div></div>";
            case 3: return "<div class=\"details-wrapper\"><div class=\"name\">" + statusTransaksi + "</div><div class=\"description\"><div class=\"progress progress-small\"><div data-percentage=\"60%\" class=\"progress-bar progress-bar-warning\" style=\"width: 60%;\"></div></div></div></div>";
            case 4: return "<div class=\"details-wrapper\"><div class=\"name\">" + statusTransaksi + "</div><div class=\"description\"><div class=\"progress progress-small\"><div data-percentage=\"80%\" class=\"progress-bar progress-bar-success\" style=\"width: 80%;\"></div></div></div></div>";
            case 5: return "<div class=\"details-wrapper\"><div class=\"name\">" + statusTransaksi + "</div><div class=\"description\"><div class=\"progress progress-small\"><div data-percentage=\"100%\" class=\"progress-bar progress-bar-success\" style=\"width: 100%;\"></div></div></div></div>";
            case 6: return "<div class=\"details-wrapper\"><div class=\"name\">" + statusTransaksi + "</div><div class=\"description\"><div class=\"progress progress-small\"><div data-percentage=\"100%\" class=\"progress-bar progress-bar-danger\" style=\"width: 100%;\"></div></div></div></div>";
            default: return string.Empty;
        }
    }

    public static void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int NewHeight)
    {
        try
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

            //The user only set the width, calculate the new height
            if (NewWidth > 0 && NewHeight == 0)
                NewHeight = (int)Math.Floor((double)FullsizeImage.Height / ((double)FullsizeImage.Width / (double)NewWidth));

            //The user only set the height, calculate the width
            if (NewHeight > 0 && NewWidth == 0)
                NewWidth = (int)Math.Floor((double)FullsizeImage.Width / ((double)FullsizeImage.Height / (double)NewHeight));

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            // Save resized picture
            NewImage.Save(NewFile);
        }
        catch (Exception)
        { }
    }

    public static void KirimEmail(string SMTPServer, int SMTPPort, string SMTPUser, string SMTPPassword, bool EnableSSL, bool EnableHTML, string emailPengirim, string namaPengirim, string emailTujuan, string judul, string isi)
    {
        try
        {
            //herdiawan.rendy@gmail.com tpgzkilgnvyzkrml
            //GMAIL "smtp.gmail.com" : 587
            //JARINGAN HOSTING "asp199mail.asphostserver.com" : 25
            //"mail.mypartitur.com", "26", "promo@mypartitur.com", "warmup12345*"

            MailMessage Mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SMTPServer, SMTPPort);

            Mail.From = new MailAddress(emailPengirim, namaPengirim);

            Mail.To.Add(emailTujuan);
            Mail.Subject = judul;
            Mail.IsBodyHtml = EnableHTML;
            Mail.Body = isi;

            SmtpServer.Credentials = new NetworkCredential(SMTPUser, SMTPPassword);
            SmtpServer.EnableSsl = EnableSSL;
            SmtpServer.Send(Mail);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Thumbnail(int idKonten, string isiKonten)
    {
        try
        {
            string pattern = @"<img.*?src=""(?<url>.*?)"".*?>";
            string gambarBlank = System.Web.HttpContext.Current.Server.MapPath("/assets/images/blank.jpg");
            string gambarTujuan = System.Web.HttpContext.Current.Server.MapPath("/images/Konten/") + idKonten.ToString() + ".jpg";
            string pathFoto = Regex.Match(isiKonten, pattern).Groups["url"].Value;

            if (!string.IsNullOrEmpty(pathFoto))
            {
                if (pathFoto[0] == '/') //jika foto diambil dari directory sendiri
                {
                    string pathResult = System.Web.HttpContext.Current.Server.MapPath(pathFoto);

                    if (File.Exists(pathResult)) //cek apakah ada di directory
                        ResizeImage(pathResult, gambarTujuan, 360, 0);
                    else //jika tidak ada maka akan mengupload gambar blank
                        ResizeImage(gambarBlank, gambarTujuan, 360, 268);
                }
                else
                {
                    //melakukan upload foto baru yang mengambil dari path website lain
                    byte[] data;
                    using (WebClient client = new WebClient())
                        data = client.DownloadData(pathFoto);

                    File.WriteAllBytes(gambarTujuan, data);
                    ResizeImage(gambarTujuan, gambarTujuan, 360, 0);
                }
            }
            else
                ResizeImage(gambarBlank, gambarTujuan, 360, 268); //jika tidak ada thumbnail yang diupload
        }
        catch (Exception) //jika terjadi exception maka upload gambar blank
        {
            ResizeImage(System.Web.HttpContext.Current.Server.MapPath("/assets/images/blank.jpg"), System.Web.HttpContext.Current.Server.MapPath("~/images/Konten/") + idKonten.ToString() + ".jpg", 360, 268);
        }
    }

    public static string Ringkasan(string isiKonten)
    {
        if (string.IsNullOrWhiteSpace(isiKonten))
            return "";
        else
        {
            int maxLength = 200;
            int strLength = 0;
            string fixedString = "";

            // Remove HTML tags and newline characters from the text, and decode HTML encoded characters. 
            // This is a basic method. Additional code would be needed to more thoroughly  
            // remove certain elements, such as embedded Javascript. 

            // Remove HTML tags. 
            fixedString = Regex.Replace(isiKonten.ToString(), "<[^>]+>", string.Empty);

            // Remove newline characters.
            fixedString = fixedString.Replace("\r", " ").Replace("\n", " ");

            // Remove encoded HTML characters.
            fixedString = HttpUtility.HtmlDecode(fixedString);

            //remove double space
            fixedString = Regex.Replace(fixedString, @"\s+", " ", RegexOptions.Multiline).Trim();

            strLength = fixedString.ToString().Length;

            // Some feed management tools include an image tag in the Description field of an RSS feed, 
            // so even if the Description field (and thus, the Summary property) is not populated, it could still contain HTML. 
            // Due to this, after we strip tags from the string, we should return null if there is nothing left in the resulting string. 
            if (strLength == 0)
                return string.Empty;

            // Truncate the text if it is too long. 
            else if (strLength >= maxLength)
            {
                fixedString = fixedString.Substring(0, maxLength);

                // Unless we take the next step, the string truncation could occur in the middle of a word.
                // Using LastIndexOf we can find the last space character in the string and truncate there. 
                fixedString = fixedString.Substring(0, fixedString.LastIndexOf(" "));
            }

            fixedString += "...";

            return fixedString;
        }
    }

    #region Tanggal
    //Index 0 : Tanggal awal
    //Index 1 : Tanggal akhir
    public static DateTime[] HariIni()
    {
        DateTime[] tanggal = new DateTime[2];
        tanggal[0] = DateTime.Now.Date;
        tanggal[1] = DateTime.Now.Date;

        return tanggal;
    }

    public static DateTime[] MingguIni()
    {
        DateTime[] tanggal = new DateTime[2];

        DateTime _tanggalSekarang = DateTime.Now;
        int _hariIni = (int)_tanggalSekarang.DayOfWeek;

        _hariIni = (_hariIni == 0) ? 7 : _hariIni;

        tanggal[0] = _tanggalSekarang.AddDays(-(_hariIni - 1)).Date;
        tanggal[1] = _tanggalSekarang.AddDays((7 - _hariIni)).Date;

        return tanggal;
    }

    public static DateTime[] BulanIni()
    {
        DateTime[] tanggal = new DateTime[2];
        tanggal[0] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
        tanggal[1] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).Date;

        return tanggal;
    }

    public static DateTime[] TahunIni()
    {
        DateTime[] tanggal = new DateTime[2];
        tanggal[0] = new DateTime(DateTime.Now.Year, 1, 1).Date;
        tanggal[1] = new DateTime(DateTime.Now.Year, 12, DateTime.DaysInMonth(DateTime.Now.Year, 12)).Date;

        return tanggal;
    }

    public static DateTime[] HariSebelumnya()
    {
        DateTime[] tanggal = new DateTime[2];
        tanggal[0] = DateTime.Now.AddDays(-1).Date;
        tanggal[1] = DateTime.Now.AddDays(-1).Date;

        return tanggal;
    }

    public static DateTime[] MingguSebelumnya()
    {
        DateTime[] tanggal = new DateTime[2];

        DateTime _tanggalSekarang = DateTime.Now.AddDays(-7);
        int _hariIni = (int)_tanggalSekarang.DayOfWeek;

        _hariIni = (_hariIni == 0) ? 7 : _hariIni;

        tanggal[0] = _tanggalSekarang.AddDays(-(_hariIni - 1)).Date;
        tanggal[1] = _tanggalSekarang.AddDays((7 - _hariIni)).Date;

        return tanggal;
    }

    public static DateTime[] BulanSebelumnya()
    {
        DateTime[] tanggal = new DateTime[2];

        var bulanSebelumnya = DateTime.Now.AddMonths(-1);

        tanggal[0] = new DateTime(bulanSebelumnya.Year, bulanSebelumnya.Month, 1).Date;
        tanggal[1] = new DateTime(bulanSebelumnya.Year, bulanSebelumnya.Month, DateTime.DaysInMonth(bulanSebelumnya.Year, bulanSebelumnya.Month)).Date;

        return tanggal;
    }

    public static DateTime[] TahunSebelumnya()
    {
        DateTime[] tanggal = new DateTime[2];
        tanggal[0] = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1).Date;
        tanggal[1] = new DateTime(DateTime.Now.AddYears(-1).Year, 12, DateTime.DaysInMonth(DateTime.Now.AddYears(-1).Year, 12)).Date;

        return tanggal;
    }
    #endregion

    public static string GeneratePIN(DataClassesDatabaseDataContext db)
    {
        Random random = new Random();
        var dataPengguna = db.TBPenggunas.Select(item => new { item.PIN });
        string PIN;

        do
        {
            PIN = random.Next(1000, 9999).ToString();
        } while (dataPengguna.FirstOrDefault(item => item.PIN == PIN) != null);

        return PIN;
    }

    public static string InputHandphone(string nomorHandphone)
    {
        nomorHandphone = nomorHandphone.Replace(" ", "");

        if (nomorHandphone[0] == '0')
            nomorHandphone = "+62" + nomorHandphone.Substring(1);

        return nomorHandphone;
    }

    public static string ProgressBar(int input)
    {
        if (input == -1)
            return "<div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + 0 + "%;\"><p style=\"color:black\">None</p></div>";
        else if (input > 0 && input < 25)
            return "<div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + input + "%;\"><p style=\"color:black\">" + input.ToFormatHarga() + "%</p></div>";
        else if (input >= 25 && input < 50)
            return "<div class=\"progress-bar progress-bar-warning\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + input + "%;\"><p style=\"color:black\">" + input.ToFormatHarga() + "%</p></div>";
        else if (input >= 50 && input < 75)
            return "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + input + "%;\"><p style=\"color:black\">" + input.ToFormatHarga() + "%</p></div>";
        else
            return "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + input + "%;\"><p style=\"color:black\">" + input.ToFormatHarga() + "%</p></div>";
    }

    public static string JenisPOProduksi(int status, string jenis)
    {
        if (jenis == "BahanBaku")
        {
            switch (status)
            {
                case (int)PilihanEnumJenisProduksi.PurchaseOrder: return "PURCHASE ORDER RAW MATERIAL";
                case (int)PilihanEnumJenisProduksi.ProduksiSendiri: return "IN-HOUSE PRODUCTION RAW MATERIAL";
                case (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor: return "PRODUCTION TO SUPPLIER";
                default: return string.Empty;
            }
        }
        else
        {
            switch (status)
            {
                case (int)PilihanEnumJenisProduksi.PurchaseOrder: return "PURCHASE ORDER PRODUCT";
                case (int)PilihanEnumJenisProduksi.ProduksiSendiri: return "IN-HOUSE PRODUCTION PRODUCT";
                case (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor: return "PRODUCTION TO VENDOR";
                default: return string.Empty;
            }
        }
    }

    public static string StatusPOProduksi(string status)
    {
        switch (status.ToInt())
        {
            case (int)PilihanEnumStatusPOProduksi.Baru: return "<label class=\"label label-default\">Baru</label>";
            case (int)PilihanEnumStatusPOProduksi.Proses: return "<label class=\"label label-info\">Proses</label>";
            case (int)PilihanEnumStatusPOProduksi.Selesai: return "<label class=\"label label-success\">Selesai</label>";
            case (int)PilihanEnumStatusPOProduksi.Batal: return "<label class=\"label label-important\">Batal</label>";
            default: return string.Empty;
        }
    }

    public static string StatusPOProduksi(int status)
    {
        switch (status)
        {
            case (int)PilihanEnumStatusPOProduksi.Baru: return "Baru";
            case (int)PilihanEnumStatusPOProduksi.Proses: return "Proses";
            case (int)PilihanEnumStatusPOProduksi.Selesai: return "Selesai";
            case (int)PilihanEnumStatusPOProduksi.Batal: return "Batal";
            default: return string.Empty;
        }
    }

    public static string StatusJenisHPP(int status)
    {
        switch (status)
        {
            case (int)PilihanEnumJenisHPP.HargaSupplierVendor: return "Harga Supplier / Vendor";
            case (int)PilihanEnumJenisHPP.Komposisi: return "Komposisi tiap produk";
            case (int)PilihanEnumJenisHPP.RataRata: return "Rata-Rata dari keseluruhan komposisi";
            case (int)PilihanEnumJenisHPP.KomposisiTambahHargaSupplierVendor: return "Komposisi tiap produk + Harga vendor";
            case (int)PilihanEnumJenisHPP.RataRataTambahHargaSupplierVendor: return "Rata-Rata dari keseluruhan komposisi + Harga vendor";
            default: return string.Empty;
        }
    }

    //================== ERIWANTO CAKEP ====================
    public static string LinkJenisPOProduksi(int status, string jenis)
    {
        if (jenis == "BahanBaku")
        {
            switch (status)
            {
                case (int)PilihanEnumJenisProduksi.PurchaseOrder: return "../../WITAdministrator/BahanBaku/POProduksi/PurchaseOrder/Detail.aspx?id=";
                case (int)PilihanEnumJenisProduksi.ProduksiSendiri: return "../../WITAdministrator/BahanBaku/POProduksi/ProduksiSendiri/Detail.aspx?id=";
                case (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor: return "../../WITAdministrator/BahanBaku/POProduksi/ProduksiKeSupplier/Detail.aspx?id=";
                default: return string.Empty;
            }
        }
        else
        {
            switch (status)
            {
                case (int)PilihanEnumJenisProduksi.PurchaseOrder: return "../../WITAdministrator/Produk/POProduksi/PurchaseOrder/Detail.aspx?id=";
                case (int)PilihanEnumJenisProduksi.ProduksiSendiri: return "../../WITAdministrator/Produk/POProduksi/ProduksiSendiri/Detail.aspx?id=";
                case (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor: return "../../WITAdministrator/Produk/POProduksi/ProduksiKeVendor/Detail.aspx?id=";
                default: return string.Empty;
            }
        }
    }

    //================== END ERI CAKEP =======================

    public static string StatusProyeksi(int status)
    {
        switch (status)
        {
            case (int)PilihanEnumStatusProyeksi.Proses: return "Proses";
            case (int)PilihanEnumStatusProyeksi.Selesai: return "Selesai";
            case (int)PilihanEnumStatusProyeksi.Batal: return "Batal";
            default: return string.Empty;
        }
    }

    public static string StatusProyeksi(string status)
    {
        switch (status.ToInt())
        {
            case (int)PilihanEnumStatusProyeksi.Proses: return "<label class=\"label label-info\">Proses</label>";
            case (int)PilihanEnumStatusProyeksi.Selesai: return "<label class=\"label label-success\">Selesai</label>";
            case (int)PilihanEnumStatusProyeksi.Batal: return "<label class=\"label label-important\">Batal</label>";
            default: return string.Empty;
        }
    }

    public static string StatusTransfer(int status)
    {
        switch (status)
        {
            case (int)PilihanJenisTransfer.TransferProses: return "Proses";
            case (int)PilihanJenisTransfer.PermintaanProses: return "Proses";
            case (int)PilihanJenisTransfer.TransferBatal: return "Batal";
            case (int)PilihanJenisTransfer.PermintaanBatal: return "Batal";
            case (int)PilihanJenisTransfer.TransferPending: return "Pending";
            case (int)PilihanJenisTransfer.PermintaanPending: return "Pending";
            case (int)PilihanJenisTransfer.TransferSelesai: return "Selesai";
            case (int)PilihanJenisTransfer.PermintaanSelesai: return "Selesai";
            default: return string.Empty;
        }
    }

    public static string StatusTransfer(string status)
    {
        switch (status.ToInt())
        {
            case (int)PilihanJenisTransfer.TransferProses: return "<label class=\"label label-info\">Proses</label>";
            case (int)PilihanJenisTransfer.PermintaanProses: return "<label class=\"label label-info\">Proses</label>";
            case (int)PilihanJenisTransfer.TransferBatal: return "<label class=\"label label-danger\">Batal</label>";
            case (int)PilihanJenisTransfer.PermintaanBatal: return "<label class=\"label label-danger\">Batal</label>";
            case (int)PilihanJenisTransfer.TransferPending: return "<label class=\"label label-default\">Pending</label>";
            case (int)PilihanJenisTransfer.PermintaanPending: return "<label class=\"label label-default\">Pending</label>";
            case (int)PilihanJenisTransfer.TransferSelesai: return "<label class=\"label label-success\">Selesai</label>";
            case (int)PilihanJenisTransfer.PermintaanSelesai: return "<label class=\"label label-success\">Selesai</label>";
            default: return string.Empty;
        }
    }
}