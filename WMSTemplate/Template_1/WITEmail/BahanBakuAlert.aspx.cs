using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Email_Laporan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ReportPenjualan();
    }

    private void ReportPenjualan()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Store = db.TBStores.FirstOrDefault();
            string Email = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == 7).Pengaturan;

            string path = Server.MapPath("BahanBakuAlert.html");
            var TanggalLaporan = DateTime.Now.ToFormatTanggalHari();
            string Judul = "[Notifikasi WMS] Bahan Baku - " + Store.Nama + " " + TanggalLaporan;
            Guid IDWMSStore = Guid.NewGuid();

            string body = "";

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Store}", Store.Nama);
            body = body.Replace("{TanggalLaporan}", TanggalLaporan);

            string Data = string.Empty;

            foreach (var Tempat in db.TBTempats.ToArray())
            {
                var ListStokBahanBaku = db.TBStokBahanBakus
                    .Where(item =>
                        item.JumlahMinimum.HasValue &&  //JUMLAH MINIMUM TIDAK NULL
                        item.Jumlah <= item.JumlahMinimum && //LEBIH KECIL ATAU SAMA DENGAN MINIMUM STOK PRODUK
                        item.IDTempat == Tempat.IDTempat)
                    .Select(item => new
                    {
                        item.TBBahanBaku.Nama,
                        Satuan = item.Jumlah / item.TBBahanBaku.Konversi >= 1 ? item.TBBahanBaku.TBSatuan1.Nama : item.TBBahanBaku.TBSatuan.Nama,
                        Jumlah = item.Jumlah / item.TBBahanBaku.Konversi >= 1 ? item.Jumlah / item.TBBahanBaku.Konversi : item.Jumlah,
                    })
                    .OrderBy(item => item.Nama)
                    .ToArray();


                if (ListStokBahanBaku.Count() > 0)
                {
                    Data = @"<h4>" + Tempat.Nama + "</h4>";

                    Data += "<table border='0' cellpadding='0' cellspacing='0' style='border: 1px solid black;'><thead><tr style='border: 1px solid black;'>"; //cellspacing='0' 

                    Data += "<th style='border: 1px solid black;'>No.</th>";
                    Data += "<th style='border: 1px solid black;'>Bahan Baku</th>";
                    Data += "<th style='border: 1px solid black;'>Jumlah</th>";
                    Data += "<th style='border: 1px solid black;'>Satuan</th>";

                    Data += "</tr></thead><tbody>";

                    int index = 0;

                    foreach (var BahanBaku in ListStokBahanBaku)
                    {
                        Data += "<tr style='border: 1px solid black;'>";

                        Data += " <td style='border: 1px solid black;'>" + (++index) + "</td>";
                        Data += " <td style='border: 1px solid black;'>" + BahanBaku.Nama + "</td>";
                        Data += " <td style='border: 1px solid black; text-align: right;'>" + BahanBaku.Jumlah.ToFormatHarga() + "</td>";
                        Data += " <td style='border: 1px solid black;'>" + BahanBaku.Satuan + "</td>";

                        Data += "</tr>";
                    }

                    Data += "</tbody></table>";
                }
            }

            body = body.Replace("{Data}", Data);

            #region KIRIM EMAIL
            foreach (var item in Email.Replace(" ", "").Split(','))
            {
                db.TBPengirimanEmails.InsertOnSubmit(new TBPengirimanEmail
                {
                    Judul = Judul,
                    TanggalKirim = DateTime.Now,
                    Tujuan = item,
                    Isi = body.Replace("{Logo}", "<img src='" + "http://wit.systems/Logo.aspx?IDWMSStore=" + IDWMSStore + "&IDWMSEmail=" + Guid.NewGuid() + "&EmailPenerima=" + item + "&Judul=" + Judul + "' height=\"35\" />")
                });
            }
            #endregion

            db.SubmitChanges();

            Response.Write("Success");
        }
    }
}