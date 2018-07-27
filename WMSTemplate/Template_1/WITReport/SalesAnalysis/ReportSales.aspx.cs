using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_ReportSales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]) && !string.IsNullOrWhiteSpace(Request.QueryString["month"]) && !string.IsNullOrWhiteSpace(Request.QueryString["year"]))
                {
                    var DataStore = db.TBStores.FirstOrDefault();
                    LabelNamaStore.Text = DataStore.Nama;
                    LabelAlamatStore.Text = DataStore.Alamat;

                    //cari kategori tempat sesuai dengan yang dicari
                    var KategoriTempatDB = db.TBKategoriTempats.FirstOrDefault(item => item.IDKategoriTempat.ToString() == Request.QueryString["id"].ToString());

                    //cari forecast seluruh store dengan IDKategoriTempat yang sesuai
                    var forecastDB = db.TBForecasts.Where(item => item.TBTempat.IDKategoriTempat == KategoriTempatDB.IDKategoriTempat).ToArray();

                    var hasil = db.TBTransaksis.AsEnumerable()
                            .Where(item => item.TBTempat.TBKategoriTempat.IDKategoriTempat.ToString() == Request.QueryString["id"].ToString() &&
                                 item.TanggalTransaksi.Value.Year == int.Parse(Request.QueryString["year"].ToString()) &&
                                 item.TanggalTransaksi.Value.Month.ToString() == Request.QueryString["month"].ToString()
                                 && item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                            .GroupBy(item => new
                            {
                                IDTempat = item.IDTempat,
                                Tempat = item.TBTempat.Nama,
                                item.TanggalTransaksi,
                                NamaPenggunaTransaksi = item.TBPengguna.NamaLengkap
                            }).Select(item => new
                            {
                                item.Key,
                                Qty = item.Sum(item2 => item2.JumlahProduk),
                                ActualIncome = item.Sum(item2 => item2.GrandTotal),
                                ForecastIncome = forecastDB.FirstOrDefault(item2 => item2.Tanggal.Date == item.Key.TanggalTransaksi.Value.Date) == null ? 0 : forecastDB.FirstOrDefault(item2 => item2.Tanggal.Date == item.Key.TanggalTransaksi.Value.Date).Nominal,
                                ForecastQuantity = forecastDB.FirstOrDefault(item2 => item2.Tanggal.Date == item.Key.TanggalTransaksi.Value.Date) == null ? 0 : forecastDB.FirstOrDefault(item2 => item2.Tanggal.Date == item.Key.TanggalTransaksi.Value.Date).Quantity
                            }).ToArray().OrderBy(item => item.Key.Tempat).ThenBy(item2 => item2.Key.TanggalTransaksi);

                    RepeaterDataSales.DataSource = hasil;
                    RepeaterDataSales.DataBind();

                    if (hasil != null)
                    {
                        LabelTotalForecastQuantity.Text = hasil.Sum(item => item.ForecastQuantity).ToFormatHargaBulat();
                        LabelTotalForecastIncome.Text = hasil.Sum(item => item.ForecastIncome).ToFormatHarga();
                        LabelTotalQty.Text = hasil.Sum(item => item.Qty).ToFormatHargaBulat();
                        LabelTotalActualIncome.Text = hasil.Sum(item => item.ActualIncome).ToFormatHarga();

                        LabelNamaKategoriTempat.Text = KategoriTempatDB.Nama.ToUpper() + " " + DataStore.Nama.ToUpper();
                        LabelNamaKategoriTempatHeader.Text = KategoriTempatDB.Nama;
                    }
                }
            }
        }
    }
}