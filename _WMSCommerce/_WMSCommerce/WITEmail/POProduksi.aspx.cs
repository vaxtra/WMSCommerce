using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITEmail_POProduksi : System.Web.UI.Page
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
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();
            string Email = StoreKonfigurasi_Class.Cari(db, EnumStoreKonfigurasi.EmailReportSales).Pengaturan;

            foreach (var Tempat in db.TBTempats.ToArray())
            {
                string Judul = "Laporan Purchase Order & Produksi " + Tempat.TBStore.Nama + " - " + Tempat.Nama;

                TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = db.TBPOProduksiBahanBakus.Where(item => item.IDTempat == Tempat.IDTempat).ToArray();

                if (daftarPOProduksiBahanBaku.Count() > 0)
                {
                    #region Jatuh Tempo
                    string LiteralJatuhTempo = "";
                    decimal batas = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)EnumStoreKonfigurasi.JumlahHariSebelumJatuhTempo).Pengaturan.ToDecimal();

                    var hasil = daftarPOProduksiBahanBaku
                                    .Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas))
                                    .Select(item => new
                                    {
                                        item.IDPOProduksiBahanBaku,
                                        item.TBSupplier.Nama,
                                        item.Tanggal,
                                        item.TanggalJatuhTempo,
                                        Jarak = (item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays
                                    })
                                    .OrderBy(item => item.Tanggal)
                                    .ToArray();

                    int count = 1;
                    foreach (var item in hasil)
                    {
                        LiteralJatuhTempo += "<tr>";
                        LiteralJatuhTempo += "<td align='center'>" + count.ToString() + "</td>";
                        LiteralJatuhTempo += "<td align='left'>" + item.IDPOProduksiBahanBaku + "</td>";
                        LiteralJatuhTempo += "<td align='left'>" + item.Nama + "</td>";
                        LiteralJatuhTempo += "<td align='left'>" + item.Tanggal.ToFormatTanggal() + "</td>";
                        LiteralJatuhTempo += "<td align='left'>" + item.TanggalJatuhTempo.ToFormatTanggal() + "</td>";
                        LiteralJatuhTempo += "<td align='right'>" + item.Jarak + " Hari</td>";
                        LiteralJatuhTempo += "</tr>";

                        count++;
                    }

                    Literal1.Text = LiteralJatuhTempo;
                    #endregion
                }
            }
        }
    }
}