using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebServiceTomahawk : System.Web.Services.WebService
{
    [WebMethod]
    public void Proses()
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

                var StoreKonfigurasi = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.UrutanProsesTomahawk);

                var KonfigurasiTomahawk = db.TBKonfigurasiTomahawks.FirstOrDefault(item => item.Urutan == StoreKonfigurasi.Pengaturan.ToInt());

                #region MENYIMPAN KONFIGURASI URUTAN SELANJUTNYA
                var MaksimalUrutan = db.TBKonfigurasiTomahawks.Max(item => item.Urutan);

                if (StoreKonfigurasi.Pengaturan.ToInt() == MaksimalUrutan)
                    StoreKonfigurasi_Class.UbahPengaturan(db, (int)EnumStoreKonfigurasi.UrutanProsesTomahawk, "1");
                else
                    StoreKonfigurasi_Class.UbahPengaturan(db, (int)EnumStoreKonfigurasi.UrutanProsesTomahawk, (StoreKonfigurasi.Pengaturan.ToInt() + 1).ToString());

                db.SubmitChanges();
                #endregion

                if (KonfigurasiTomahawk != null)
                {
                    if ((DateTime.Now - KonfigurasiTomahawk.TanggalTerakhirProses).TotalMinutes > KonfigurasiTomahawk.DurasiProses)
                    {
                        switch (KonfigurasiTomahawk.Urutan)
                        {
                            case 1: Context.Response.Write(KirimEmail()); break;
                            case 2: Context.Response.Write(LaporanPenjualan()); break;
                            //case 3: Context.Response.Write(LaporanPOProduksi()); break;
                            case 4: Context.Response.Write(UpdateDiscountSelesai()); break;
                            case 5: Context.Response.Write(UpdateDiscountMulai()); break;
                            case 6: Context.Response.Write(GenerateStoreKey()); break;
                        }

                        var UpdateKonfigurasiTomahawk = db.TBKonfigurasiTomahawks.FirstOrDefault(item => item.Urutan == KonfigurasiTomahawk.Urutan);

                        UpdateKonfigurasiTomahawk.TanggalTerakhirProses = UpdateKonfigurasiTomahawk.TanggalTerakhirProses.AddMinutes(UpdateKonfigurasiTomahawk.DurasiProses);
                        db.SubmitChanges();
                    }
                    else
                    {
                        Context.Response.Write(JsonConvert.SerializeObject(new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoAction,
                            Pesan = "Tomahawk Up To Date"
                        }, Formatting.Indented));
                    }
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.NoAction,
                        Pesan = "Konfigurasi Tomahawk Tidak Ditemukan"
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_Proses");

            Context.Response.Write(JsonConvert.SerializeObject(new WebServiceResult
            {
                EnumWebService = (int)EnumWebService.Exception,
                Pesan = ex.Message
            }, Formatting.Indented));
        }
    }

    #region URUTAN
    //CASE 1
    private string KirimEmail()
    {
        try
        {
            TBPengirimanEmail EmailKirim;
            TBStore Store;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Store = db.TBStores.FirstOrDefault();
                EmailKirim = db.TBPengirimanEmails.FirstOrDefault(item => item.TanggalKirim <= DateTime.Now);
            }

            if (EmailKirim != null)
            {
                Pengaturan.KirimEmail(Store.SMTPServer, Store.SMTPPort.Value, Store.SMTPUser, Store.SMTPPassword, Store.SecureSocketsLayer.Value, true, Store.SMTPUser, Store.Nama, EmailKirim.Tujuan, EmailKirim.Judul, EmailKirim.Isi);

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var EmailHapus = db.TBPengirimanEmails.FirstOrDefault(item => item.IDPengirimanEmail == EmailKirim.IDPengirimanEmail);

                    db.TBPengirimanEmails.DeleteOnSubmit(EmailHapus);
                    db.SubmitChanges();
                }

                return JsonConvert.SerializeObject(new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Success,
                    Pesan = "[BERHASIL DIKIRIM] EMAIL : " + EmailKirim.Judul + " - " + EmailKirim.Tujuan
                }, Formatting.Indented);
            }
            else
            {
                return JsonConvert.SerializeObject(new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.NoAction,
                    Pesan = ""
                }, Formatting.Indented);
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_KirimEmail");

            return JsonConvert.SerializeObject(new WebServiceResult
            {
                EnumWebService = (int)EnumWebService.Exception,
                Pesan = ex.Message
            }, Formatting.Indented);
        }
    }

    //CASE 2
    private string LaporanPenjualan()
    {
        try
        {
            string path = Server.MapPath("/WITEmail/Laporan.html");
            Guid IDWMSStore = Guid.NewGuid();
            var TanggalLaporan = DateTime.Now.AddDays(-1).Date;
            bool IsSimpan = false;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();
                string Email = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.EmailReportSales).Pengaturan;

                foreach (var Tempat in db.TBTempats.ToArray())
                {
                    string Judul = "Laporan Penjualan " + Tempat.TBStore.Nama + " - " + Tempat.Nama + " " + TanggalLaporan.ToFormatTanggal();
                    string body = "";

                    using (StreamReader reader = new StreamReader(path))
                    {
                        body = reader.ReadToEnd();
                    }

                    body = body.Replace("{Store}", Tempat.TBStore.Nama);
                    body = body.Replace("{Tempat}", Tempat.Nama);
                    body = body.Replace("{TanggalLaporan}", TanggalLaporan.ToFormatTanggal());

                    var ListTransaksi = db.TBTransaksis
                        .AsEnumerable()
                        .Where(item => item.IDTempat == Tempat.IDTempat &&
                            item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                            item.TanggalOperasional.Value.Date == TanggalLaporan).ToArray();

                    if (ListTransaksi.Count() > 0)
                    {
                        #region DATA
                        var ListTransaksiJenisPembayaran = db.TBTransaksiJenisPembayarans
                            .AsEnumerable()
                            .Where(item => item.TBTransaksi.IDTempat == Tempat.IDTempat &&
                                item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TBTransaksi.TanggalOperasional.Value.Date == TanggalLaporan).ToArray();

                        var ListTransaksiDetail = db.TBTransaksiDetails
                            .AsEnumerable()
                            .Where(item => item.TBTransaksi.IDTempat == Tempat.IDTempat &&
                                item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TBTransaksi.TanggalOperasional.Value.Date == TanggalLaporan)
                            .GroupBy(item => new
                            {
                                item.IDKombinasiProduk,
                                item.TBKombinasiProduk.Nama
                            })
                            .Select(item => new
                            {
                                item.Key,
                                Total = item.Sum(item2 => item2.Quantity)
                            })
                            .OrderByDescending(item => item.Total);

                        var TransaksiPerJam = ListTransaksi
                            .GroupBy(item => item.TanggalTransaksi.Value.Hour)
                            .Select(item => new
                            {
                                item.Key,
                                Total = item.Sum(item2 => item2.GrandTotal)
                            });

                        var ListPelanggan = ListTransaksi
                            .GroupBy(item => new
                            {
                                item.IDPelanggan,
                                item.TBPelanggan.NamaLengkap
                            })
                            .Select(item => new
                            {
                                item.Key,
                                Total = item.Sum(item2 => item2.GrandTotal)
                            });
                        #endregion

                        var HariIni = ListTransaksi.Sum(item => item.GrandTotal);

                        #region GROWTH HARIAN
                        body = body.Replace("{GrandTotal}", HariIni.ToFormatHarga());

                        var Kemarin = db.TBTransaksis
                            .Where(item =>
                                item.IDTempat == Tempat.IDTempat &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TanggalOperasional.Value.Date == TanggalLaporan.Date.AddDays(-1)).Sum(item => item.GrandTotal);

                        //LAPORAN SALES KEMARIN
                        body = body.Replace("{KemarinKeterangan}", TanggalLaporan.Date.AddDays(-1).ToFormatTanggal());
                        body = body.Replace("{Kemarin}", Kemarin.ToFormatHarga());

                        if (Kemarin > 0)
                        {
                            var GrowthHarian = (HariIni - Kemarin) / Kemarin * 100;

                            if (GrowthHarian > 0)
                                body = body.Replace("{GrowthHarian}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\" style=\"color:Green;\">&#9650;" + GrowthHarian.ToFormatHarga() + "%</td>");
                            else
                                body = body.Replace("{GrowthHarian}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\" style=\"color:Red;\">&#9660;" + GrowthHarian.ToFormatHarga() + "%</td>");
                        }
                        else
                            body = body.Replace("{GrowthHarian}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\"></td>");
                        #endregion

                        #region GROWTH BULANAN
                        var BulanIni = db.TBTransaksis
                            .Where(item =>
                                item.IDTempat == Tempat.IDTempat &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TanggalOperasional.Value.Month == TanggalLaporan.Month).Sum(item => item.GrandTotal);

                        //LAPORAN SALES BULAN INI
                        body = body.Replace("{BulanIniKeterangan}", TanggalLaporan.ToString("MMMM"));
                        body = body.Replace("{BulanIni}", BulanIni.ToFormatHarga());

                        var BulanLalu = db.TBTransaksis
                            .Where(item =>
                                item.IDTempat == Tempat.IDTempat &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TanggalOperasional.Value.Month == TanggalLaporan.AddMonths(-1).Month).Sum(item => item.GrandTotal);

                        if (BulanLalu > 0)
                        {
                            var GrowthBulanan = (BulanIni - BulanLalu) / BulanLalu * 100;

                            if (GrowthBulanan > 0)
                                body = body.Replace("{GrowthBulanan}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\" style=\"color:Green;\">&#9650;" + GrowthBulanan.ToFormatHarga() + "%</td>");
                            else
                                body = body.Replace("{GrowthBulanan}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\" style=\"color:Red;\">&#9660;" + GrowthBulanan.ToFormatHarga() + "%</td>");
                        }
                        else
                            body = body.Replace("{GrowthBulanan}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\"></td>");
                        #endregion

                        #region GROWTH TAHUNAN
                        var TahunLalu = db.TBTransaksis
                            .Where(item =>
                                item.IDTempat == Tempat.IDTempat &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TanggalOperasional.Value.Year == TanggalLaporan.AddYears(-1).Year)
                            .Sum(item => item.GrandTotal);

                        var TahunIni = db.TBTransaksis
                            .Where(item =>
                                item.IDTempat == Tempat.IDTempat &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                item.TanggalOperasional.Value.Year == TanggalLaporan.Year)
                            .Sum(item => item.GrandTotal);

                        if (TahunLalu > 0)
                        {
                            var GrowthTahunan = (TahunIni - TahunLalu) / TahunLalu * 100;

                            if (GrowthTahunan > 0)
                                body = body.Replace("{GrowthTahunan}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\" style=\"color:Green;\">&#9650;" + GrowthTahunan.ToFormatHarga() + "%</td>");
                            else
                                body = body.Replace("{GrowthTahunan}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\" style=\"color:Red;\">&#9660;" + GrowthTahunan.ToFormatHarga() + "%</td>");
                        }
                        else
                            body = body.Replace("{GrowthTahunan}", "<td align=\"right\" colspan=\"1\" height=\"40\" rowspan=\"1\" width=\"275\"></td>");
                        #endregion

                        body = body.Replace("{PenjualanItem}", ListTransaksi.Sum(item => item.Subtotal + item.TotalPotonganHargaJualDetail + item.PotonganTransaksi).ToFormatHargaBulat());
                        body = body.Replace("{Discount}", ListTransaksi.Sum(item => item.TotalPotonganHargaJualDetail + item.PotonganTransaksi).ToFormatHarga());
                        body = body.Replace("{Pembulatan}", ListTransaksi.Sum(item => item.Pembulatan).ToFormatHarga());

                        #region BIAYA TAMBAHAN
                        string LiteralBiayaTambahan = "";

                        var BiayaTambahan1 = ListTransaksi.Sum(item => item.BiayaTambahan1);

                        if (BiayaTambahan1 > 0)
                        {
                            LiteralBiayaTambahan += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + Tempat.KeteranganBiayaTambahan1 + "</td>";
                            LiteralBiayaTambahan += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + BiayaTambahan1.ToFormatHarga() + "</td></tr><tr>";
                            LiteralBiayaTambahan += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                        }

                        var BiayaTambahan2 = ListTransaksi.Sum(item => item.BiayaTambahan2);

                        if (BiayaTambahan2 > 0)
                        {
                            LiteralBiayaTambahan += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + Tempat.KeteranganBiayaTambahan2 + "</td>";
                            LiteralBiayaTambahan += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + BiayaTambahan2.ToFormatHarga() + "</td></tr><tr>";
                            LiteralBiayaTambahan += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                        }

                        var BiayaTambahan3 = ListTransaksi.Sum(item => item.BiayaTambahan3);

                        if (BiayaTambahan3 > 0)
                        {
                            LiteralBiayaTambahan += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + Tempat.KeteranganBiayaTambahan3 + "</td>";
                            LiteralBiayaTambahan += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + BiayaTambahan3.ToFormatHarga() + "</td></tr><tr>";
                            LiteralBiayaTambahan += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                        }

                        var BiayaTambahan4 = ListTransaksi.Sum(item => item.BiayaTambahan4);

                        if (BiayaTambahan4 > 0)
                        {
                            LiteralBiayaTambahan += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + Tempat.KeteranganBiayaTambahan4 + "</td>";
                            LiteralBiayaTambahan += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + BiayaTambahan4.ToFormatHarga() + "</td></tr><tr>";
                            LiteralBiayaTambahan += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                        }

                        body = body.Replace("{BiayaTambahan}", LiteralBiayaTambahan);
                        #endregion

                        #region JENIS PEMBAYARAN
                        string LiteralJenisPembayaran = "";

                        foreach (var item in db.TBJenisPembayarans.ToArray())
                        {
                            var Total = ListTransaksiJenisPembayaran
                                .Where(item2 => item2.IDJenisPembayaran == item.IDJenisPembayaran)
                                .Sum(item2 => item2.Total);

                            if (Total > 0)
                            {
                                LiteralJenisPembayaran += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + item.Nama + "</td>";
                                LiteralJenisPembayaran += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + Total.ToFormatHarga() + "</td></tr><tr>";
                                LiteralJenisPembayaran += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                            }
                        }

                        body = body.Replace("{JenisPembayaran}", LiteralJenisPembayaran);
                        #endregion

                        #region PELANGGAN
                        string LiteralPelanggan = "";

                        foreach (var item in ListPelanggan.OrderByDescending(item2 => item2.Total))
                        {
                            LiteralPelanggan += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + item.Key.NamaLengkap + "</td>";
                            LiteralPelanggan += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + item.Total.ToFormatHarga() + "</td></tr><tr>";
                            LiteralPelanggan += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                        }

                        body = body.Replace("{Pelanggan}", LiteralPelanggan);
                        #endregion

                        #region LAPORAN PER JAM
                        string LiteralLaporanPerJam = "";

                        foreach (var item in TransaksiPerJam.OrderBy(item => item.Key))
                        {
                            LiteralLaporanPerJam += "<tr><td colspan='1' rowspan='1' width='80'><table bgcolor='#f0f0f0' border='0' cellpadding='0' cellspacing='0' width='100%'>";
                            LiteralLaporanPerJam += "<tr><td align='left' colspan='1' height='42' rowspan='1' style='font-family: Verdana; font-size: 14px; line-height: 18px; font-weight: 600; color: rgb(61, 69, 76); white-space: nowrap; padding: 0 15px;' width='100%'>" + item.Key + ":00</td></tr></table></td>";
                            LiteralLaporanPerJam += "<td align='left' bgcolor='#FFFFFF' colspan='1' rowspan='1' width='500'>";
                            LiteralLaporanPerJam += "<table bgcolor='#f0f0f0' border='0' cellpadding='0' cellspacing='0' width='100%'><tr>";
                            LiteralLaporanPerJam += "<td align='right' colspan='1' height='42' rowspan='1' style='font-family: Verdana; font-size: 14px; line-height: 18px; font-weight: 600; color: rgb(61, 69, 76); white-space: nowrap; padding: 0 15px;' width='100%'>" + item.Total.ToFormatHarga() + "</td></tr></table></td></tr><tr>";
                            LiteralLaporanPerJam += "<td bgcolor='#FFFFFF' colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0;'>&nbsp;</td></tr>";
                        }

                        body = body.Replace("{LaporanPerJam}", LiteralLaporanPerJam);
                        #endregion

                        #region TOP ITEM
                        string LiteralTopItem = "";

                        foreach (var item in ListTransaksiDetail)
                        {
                            LiteralTopItem += "<tr><td align='left' colspan='1' rowspan='1' width='275'>" + item.Key.Nama + "</td>";
                            LiteralTopItem += "<td align='right' colspan='1' height='40' rowspan='1' width='275'>" + item.Total.ToFormatHargaBulat() + "</td></tr><tr>";
                            LiteralTopItem += "<td colspan='2' height='1' rowspan='1' style='font-size: 0; line-height: 0; background-color: #EBECED;'>&nbsp;</td></tr>";
                        }

                        body = body.Replace("{TopItem}", LiteralTopItem);
                        body = body.Replace("{TotalItem}", ListTransaksi.Sum(item => item.JumlahProduk).ToFormatHargaBulat());
                        #endregion

                        #region KIRIM EMAIL
                        foreach (var item in Email.Replace(" ", "").Split(','))
                        {
                            IsSimpan = true;

                            db.TBPengirimanEmails.InsertOnSubmit(new TBPengirimanEmail
                            {
                                Judul = Judul,
                                TanggalKirim = DateTime.Now,
                                Tujuan = item,
                                Isi = body.Replace("{Logo}", "<img src='" + "http://wit.systems/Logo.aspx?IDWMSStore=" + IDWMSStore + "&IDWMSEmail=" + Guid.NewGuid() + "&EmailPenerima=" + item + "&Judul=" + Judul + "' height=\"35\" />")
                            });
                        }
                        #endregion
                    }
                }

                if (IsSimpan)
                    db.SubmitChanges();
            }

            if (IsSimpan)
            {
                return JsonConvert.SerializeObject(new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Success,
                    Pesan = "[BERHASIL GENERATE] Laporan Penjualan " + TanggalLaporan.ToFormatTanggalHari() + " berhasil digenerate",
                }, Formatting.Indented);
            }
            else
            {
                return JsonConvert.SerializeObject(new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.NoAction,
                    Pesan = "Tidak ada Laporan Penjualan " + TanggalLaporan.ToFormatTanggalHari(),
                }, Formatting.Indented);
            }

        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_LaporanPenjualan");

            return JsonConvert.SerializeObject(new WebServiceResult
            {
                EnumWebService = (int)EnumWebService.Exception,
                Pesan = ex.Message
            }, Formatting.Indented);
        }
    }

    //CASE 3
    private string LaporanPOProduksi()
    {
        return string.Empty;
        //try
        //{
        //    string path = Server.MapPath("/WITEmail/POProduksi.html");
        //    Guid IDWMSStore = Guid.NewGuid();
        //    bool IsSimpan = false;

        //    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        //    {
        //        StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();
        //        string Email = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.EmailReportSales).Pengaturan;

        //        foreach (var Tempat in db.TBTempats.ToArray())
        //        {
        //            string Judul = "Laporan Purchase Order & Produksi " + Tempat.TBStore.Nama + " - " + Tempat.Nama;
        //            string body = "";

        //            using (StreamReader reader = new StreamReader(path))
        //            {
        //                body = reader.ReadToEnd();
        //            }

        //            body = body.Replace("{Store}", Tempat.TBStore.Nama);
        //            body = body.Replace("{Tempat}", Tempat.Nama);

        //            decimal batas = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)EnumStoreKonfigurasi.JumlahHariSebelumJatuhTempo).Pengaturan.ToDecimal();
        //            TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = db.TBPOProduksiBahanBakus.Where(item => item.IDTempat == Tempat.IDTempat && item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && item.EnumStatusProduksi < (int)PilihanEnumStatusPOProduksi.Selesai && item.Grandtotal != item.TotalBayar && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas)).ToArray();
        //            TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => item.IDTempat == Tempat.IDTempat && item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && item.EnumStatusProduksi < (int)PilihanEnumStatusPOProduksi.Selesai && item.Grandtotal != item.TotalBayar && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas)).ToArray();

        //            if (daftarPOProduksiBahanBaku.Count() > 0 || daftarPOProduksiProduk.Count() > 0)
        //            {
        //                body = body.Replace("{SetengahJatuhTempo}", "1-" + Math.Floor(batas / 2).ToFormatHarga() + " Hari");
        //                body = body.Replace("{JatuhTempo}", (Math.Floor(batas / 2) + 1).ToFormatHarga() + "-" + batas.ToFormatHarga() + " Hari");

        //                if (daftarPOProduksiBahanBaku.Count() > 0)
        //                {
        //                    var jatuhTempoBahanBaku = daftarPOProduksiBahanBaku
        //                                    .Select(item => new
        //                                    {
        //                                        item.IDPOProduksiBahanBaku,
        //                                        item.TBSupplier.Nama,
        //                                        item.TanggalPending,
        //                                        item.TanggalJatuhTempo,
        //                                        Jarak = (item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays
        //                                    })
        //                                    .OrderBy(item => item.Jarak)
        //                                    .ToArray();

        //                    int count = 1;
        //                    string LiteralJatuhTempoBahanBaku = "";
        //                    foreach (var item in jatuhTempoBahanBaku.Where(item => (decimal)item.Jarak <= batas && (decimal)item.Jarak > Math.Floor(batas / 2)))
        //                    {
        //                        LiteralJatuhTempoBahanBaku += "<tr>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='center' style='background-color:#d9edf7'>" + count.ToString() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#d9edf7'>" + item.IDPOProduksiBahanBaku + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#d9edf7'>" + item.Nama + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#d9edf7'>" + item.TanggalPending.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#d9edf7'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='right' style='background-color:#d9edf7'>" + item.Jarak + " Hari</td>";
        //                        LiteralJatuhTempoBahanBaku += "</tr>";

        //                        count++;
        //                    }

        //                    body = body.Replace("{BahanBaku}", LiteralJatuhTempoBahanBaku);

        //                    count = 1;
        //                    LiteralJatuhTempoBahanBaku = "";
        //                    foreach (var item in jatuhTempoBahanBaku.Where(item => (decimal)item.Jarak <= Math.Floor(batas / 2) && (decimal)item.Jarak > 0))
        //                    {
        //                        LiteralJatuhTempoBahanBaku += "<tr>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='center' style='background-color:#fcf8e3'>" + count.ToString() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#fcf8e3'>" + item.IDPOProduksiBahanBaku + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#fcf8e3'>" + item.Nama + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#fcf8e3'>" + item.TanggalPending.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#fcf8e3'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='right' style='background-color:#fcf8e3'>" + item.Jarak + " Hari</td>";
        //                        LiteralJatuhTempoBahanBaku += "</tr>";

        //                        count++;
        //                    }

        //                    body = body.Replace("{BahanBakuSetengahJatuhTempo}", LiteralJatuhTempoBahanBaku);

        //                    count = 1;
        //                    LiteralJatuhTempoBahanBaku = "";
        //                    foreach (var item in jatuhTempoBahanBaku.Where(item => (decimal)item.Jarak <= 0))
        //                    {
        //                        LiteralJatuhTempoBahanBaku += "<tr>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='center' style='background-color:#f2dede'>" + count.ToString() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#f2dede'>" + item.IDPOProduksiBahanBaku + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#f2dede'>" + item.Nama + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#f2dede'>" + item.TanggalPending.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='left' style='background-color:#f2dede'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoBahanBaku += "<td align='right' style='background-color:#f2dede'>" + item.Jarak + " Hari</td>";
        //                        LiteralJatuhTempoBahanBaku += "</tr>";

        //                        count++;
        //                    }

        //                    body = body.Replace("{BahanBakuJatuhTempo}", LiteralJatuhTempoBahanBaku);
        //                }

        //                if (daftarPOProduksiProduk.Count() > 0)
        //                {
        //                    var jatuhTempoProduk = daftarPOProduksiProduk
        //                                    .Select(item => new
        //                                    {
        //                                        item.IDPOProduksiProduk,
        //                                        item.TBVendor.Nama,
        //                                        item.TanggalPending,
        //                                        item.TanggalJatuhTempo,
        //                                        Jarak = (item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays
        //                                    })
        //                                    .OrderBy(item => item.Jarak)
        //                                    .ToArray();

        //                    int count = 1;
        //                    string LiteralJatuhTempoProduk = "";
        //                    foreach (var item in jatuhTempoProduk.Where(item => (decimal)item.Jarak <= batas && (decimal)item.Jarak > Math.Floor(batas / 2)))
        //                    {
        //                        LiteralJatuhTempoProduk += "<tr>";
        //                        LiteralJatuhTempoProduk += "<td align='center' style='background-color:#d9edf7'>" + count.ToString() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#d9edf7'>" + item.IDPOProduksiProduk + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#d9edf7'>" + item.Nama + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#d9edf7'>" + item.TanggalPending.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#d9edf7'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='right' style='background-color:#d9edf7'>" + item.Jarak + " Hari</td>";
        //                        LiteralJatuhTempoProduk += "</tr>";

        //                        count++;
        //                    }

        //                    body = body.Replace("{Produk}", LiteralJatuhTempoProduk);

        //                    count = 1;
        //                    LiteralJatuhTempoProduk = "";
        //                    foreach (var item in jatuhTempoProduk.Where(item => (decimal)item.Jarak <= Math.Floor(batas / 2) && (decimal)item.Jarak > 0))
        //                    {
        //                        LiteralJatuhTempoProduk += "<tr>";
        //                        LiteralJatuhTempoProduk += "<td align='center' style='background-color:#fcf8e3'>" + count.ToString() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#fcf8e3'>" + item.IDPOProduksiProduk + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#fcf8e3'>" + item.Nama + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#fcf8e3'>" + item.TanggalPending.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#fcf8e3'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='right' style='background-color:#fcf8e3'>" + item.Jarak + " Hari</td>";
        //                        LiteralJatuhTempoProduk += "</tr>";

        //                        count++;
        //                    }

        //                    body = body.Replace("{ProdukSetengahJatuhTempo}", LiteralJatuhTempoProduk);

        //                    count = 1;
        //                    LiteralJatuhTempoProduk = "";
        //                    foreach (var item in jatuhTempoProduk.Where(item => (decimal)item.Jarak <= 0))
        //                    {
        //                        LiteralJatuhTempoProduk += "<tr>";
        //                        LiteralJatuhTempoProduk += "<td align='center' style='background-color:#f2dede'>" + count.ToString() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#f2dede'>" + item.IDPOProduksiProduk + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#f2dede'>" + item.Nama + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#f2dede'>" + item.TanggalPending.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='left' style='background-color:#f2dede'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
        //                        LiteralJatuhTempoProduk += "<td align='right' style='background-color:#f2dede'>" + item.Jarak + " Hari</td>";
        //                        LiteralJatuhTempoProduk += "</tr>";

        //                        count++;
        //                    }

        //                    body = body.Replace("{ProdukJatuhTempo}", LiteralJatuhTempoProduk);
        //                }

        //                #region KIRIM EMAIL
        //                foreach (var item in Email.Replace(" ", "").Split(','))
        //                {
        //                    IsSimpan = true;

        //                    db.TBPengirimanEmails.InsertOnSubmit(new TBPengirimanEmail
        //                    {
        //                        Judul = Judul,
        //                        TanggalKirim = DateTime.Now,
        //                        Tujuan = item,
        //                        Isi = body.Replace("{Logo}", "<img src='" + "http://wit.systems/Logo.aspx?IDWMSStore=" + IDWMSStore + "&IDWMSEmail=" + Guid.NewGuid() + "&EmailPenerima=" + item + "&Judul=" + Judul + "' height=\"35\" />")
        //                    });
        //                }
        //                #endregion
        //            }
        //        }

        //        if (IsSimpan)
        //            db.SubmitChanges();
        //    }

        //    if (IsSimpan)
        //    {
        //        return JsonConvert.SerializeObject(new WebServiceResult
        //        {
        //            EnumWebService = (int)EnumWebService.Success,
        //            Pesan = "[BERHASIL GENERATE] Laporan Purchase Order & Produksi berhasil digenerate",
        //        }, Formatting.Indented);
        //    }
        //    else
        //    {
        //        return JsonConvert.SerializeObject(new WebServiceResult
        //        {
        //            EnumWebService = (int)EnumWebService.NoAction,
        //            Pesan = "Tidak ada Laporan Penjualan Purchase Order & Produksi",
        //        }, Formatting.Indented);
        //    }

        //}
        //catch (Exception ex)
        //{
        //    LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_LaporanPenjualan");

        //    return JsonConvert.SerializeObject(new WebServiceResult
        //    {
        //        EnumWebService = (int)EnumWebService.Exception,
        //        Pesan = ex.Message
        //    }, Formatting.Indented);
        //}
    }

    //CASE 4
    private string UpdateDiscountSelesai()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var EventDiscountSelesai = db.TBDiscountEvents
                    .FirstOrDefault(item =>
                        item.EnumStatusDiscountEvent == (int)EnumStatusDiscountEvent.Implementasi &&
                        item.TanggalAkhir <= DateTime.Now);

                if (EventDiscountSelesai != null && EventDiscountSelesai.TBDiscounts.Count > 0)
                {
                    foreach (var item in EventDiscountSelesai.TBDiscounts.ToArray())
                    {
                        var StokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == item.IDStokProduk);

                        if (StokProduk != null)
                        {
                            StokProduk.EnumDiscountStore = (int)EnumDiscount.TidakAda;
                            StokProduk.DiscountStore = 0;
                            StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.TidakAda;
                            StokProduk.DiscountKonsinyasi = 0;
                        }
                    }

                    EventDiscountSelesai.EnumStatusDiscountEvent = (int)EnumStatusDiscountEvent.NonAktif;

                    db.SubmitChanges();

                    return JsonConvert.SerializeObject(new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = "[BERHASIL UPDATE DISCOUNT SELESAI] : " + EventDiscountSelesai.TBTempat.Nama + " - " + EventDiscountSelesai.Nama
                    }, Formatting.Indented);
                }
                else
                {
                    return JsonConvert.SerializeObject(new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.NoAction,
                        Pesan = ""
                    }, Formatting.Indented);
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_UpdateDiscountSelesai");

            return JsonConvert.SerializeObject(new WebServiceResult
            {
                EnumWebService = (int)EnumWebService.Exception,
                Pesan = ex.Message
            }, Formatting.Indented);
        }
    }

    //CASE 5
    private string UpdateDiscountMulai()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var EventDiscountMulai = db.TBDiscountEvents
                    .FirstOrDefault(item =>
                        item.EnumStatusDiscountEvent == (int)EnumStatusDiscountEvent.Aktif &&
                        item.TanggalAwal <= DateTime.Now);

                if (EventDiscountMulai != null && EventDiscountMulai.TBDiscounts.Count > 0)
                {
                    foreach (var item in EventDiscountMulai.TBDiscounts.ToArray())
                    {
                        var StokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == item.IDStokProduk);

                        if (StokProduk != null)
                        {
                            StokProduk.EnumDiscountStore = item.EnumDiscountStore;
                            StokProduk.DiscountStore = item.DiscountStore;
                            StokProduk.EnumDiscountKonsinyasi = item.EnumDiscountKonsinyasi;
                            StokProduk.DiscountKonsinyasi = item.DiscountKonsinyasi;
                        }
                    }

                    EventDiscountMulai.EnumStatusDiscountEvent = (int)EnumStatusDiscountEvent.Implementasi;

                    db.SubmitChanges();

                    return JsonConvert.SerializeObject(new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = "[BERHASIL UPDATE DISCOUNT MULAI] : " + EventDiscountMulai.TBTempat.Nama + " - " + EventDiscountMulai.Nama
                    }, Formatting.Indented);
                }
                else
                {
                    return JsonConvert.SerializeObject(new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.NoAction,
                        Pesan = ""
                    }, Formatting.Indented);
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_UpdateDiscountMulai");

            return JsonConvert.SerializeObject(new WebServiceResult
            {
                EnumWebService = (int)EnumWebService.Exception,
                Pesan = ex.Message
            }, Formatting.Indented);
        }
    }

    //CASE 6
    private string GenerateStoreKey()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StoreKey_Class ClassStoreKey = new StoreKey_Class(db, true);
                Store_Class ClassStore = new Store_Class(db);

                ClassStoreKey.Generate();

                return JsonConvert.SerializeObject(new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Success,
                    Pesan = "[BERHASIL GENERATE STORE KEY] : " + ClassStore.Data().Nama
                }, Formatting.Indented);
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "WebServiceTomahawk_GenerateStoreKey");

            return JsonConvert.SerializeObject(new WebServiceResult
            {
                EnumWebService = (int)EnumWebService.Exception,
                Pesan = ex.Message
            }, Formatting.Indented);
        }
    }
    #endregion
}
