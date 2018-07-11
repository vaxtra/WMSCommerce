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
            string Email = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == 7).Pengaturan;
            string path = Server.MapPath("ProdukAlert.html");
            var TanggalLaporan = DateTime.Now.ToFormatTanggalHari();
            string Store = db.TBStores.FirstOrDefault().Nama;

            string Judul = "[Notifikasi WMS] " + Store + " " + TanggalLaporan;
            Guid IDWMSStore = Guid.NewGuid();

            string body = "";

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Store}", Store);
            body = body.Replace("{TanggalLaporan}", TanggalLaporan);

            string Data = string.Empty;

            foreach (var Tempat in db.TBTempats.ToArray())
            {
                var ListStokProduk = Tempat.TBStokProduks
                    .Where(item =>
                        item.TBKombinasiProduk.TBProduk._IsActive && //STATUS PRODUK ACTIVE
                        item.JumlahMinimum.HasValue && //JUMLAH MINIMUM TIDAK NULL
                        item.Jumlah <= item.JumlahMinimum) //LEBIH KECIL ATAU SAMA DENGAN MINIMUM STOK PRODUK
                    .OrderBy(item => item.TBKombinasiProduk.TBProduk.Nama)
                    .ThenBy(item => item.TBKombinasiProduk.TBAtributProduk.IDAtributProduk)
                    .ToArray();

                if (ListStokProduk.Count() > 0)
                {
                    Data = @"<h4>" + Tempat.Nama + "</h4>";

                    Data += "<table border='1' cellspacing='0'><thead><tr>";

                    Data += "<th>No.</th>";
                    Data += "<th>Produk</th>";
                    Data += "<th>Varian</th>";
                    Data += "<th>Jumlah</th>";

                    Data += "</tr></thead><tbody>";

                    int index = 0;

                    foreach (var StokProduk in ListStokProduk)
                    {
                        Data += "<tr>";

                        Data += " <td>" + (++index) + "</td>";
                        Data += " <td>" + StokProduk.TBKombinasiProduk.TBProduk.Nama + "</td>";
                        Data += " <td>" + StokProduk.TBKombinasiProduk.TBAtributProduk.Nama + "</td>";
                        Data += " <td style='text-align: right;'>" + StokProduk.Jumlah.ToFormatHargaBulat() + "</td>";

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