using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_ReportSalesTahunan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["year"]))
                {
                    int Year = 0;
                    int.TryParse(Request.QueryString["year"].ToString(), out Year);

                    var DataStore = db.TBStores.FirstOrDefault();
                    LabelNamaStore.Text = DataStore.Nama;
                    LabelAlamatStore.Text = DataStore.Alamat;

                    #region Data Header KategoriTempat
                    RepeaterKategoriTempat.DataSource = db.TBKategoriTempats.ToArray().OrderBy(item => item.IDKategoriTempat);
                    RepeaterKategoriTempat.DataBind();
                    #endregion

                    #region Data Sales
                    var _result = db.TBTransaksis.AsEnumerable().Where(x => x.TanggalPembayaran.Value.Year == Year
                        && x.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).GroupBy(item => new
                        {
                            Bulan = item.TanggalPembayaran.Value.Month,
                            NamaBulan = item.TanggalPembayaran.Value.ToString("MMMM")
                        })
                        .Select(itemTransaksi => new
                        {
                            itemTransaksi.Key,
                            NamaBulan = itemTransaksi.Key.NamaBulan,
                            Hasil = db.TBKategoriTempats.Select(itemHasil => new
                            {
                                itemHasil.IDKategoriTempat,
                                Grandtotal = db.TBTransaksis.Where(item => item.TBTempat.TBKategoriTempat.IDKategoriTempat == itemHasil.IDKategoriTempat && item.TanggalPembayaran.Value.Month == itemTransaksi.Key.Bulan).Select(item => item.IDTransaksi).Count() == 0
                                ?
                                0
                                : db.TBTransaksis.Where(item => item.TBTempat.TBKategoriTempat.IDKategoriTempat == itemHasil.IDKategoriTempat && item.TanggalPembayaran.Value.Month == itemTransaksi.Key.Bulan).Sum(item => item.GrandTotal)
                            }).OrderBy(item => item.IDKategoriTempat),
                        });

                    if (_result.Count() != 0)
                    {
                        RepeaterDataSales.DataSource = _result.OrderBy(item => item.Key.Bulan);
                        RepeaterDataSales.DataBind();

                        var DataTotal = db.TBKategoriTempats.Select(itemHasil => new
                        {
                            GrandTotalKategoriTempat = db.TBTransaksis.Where(item => item.TBTempat.TBKategoriTempat.IDKategoriTempat == itemHasil.IDKategoriTempat).Select(item => item.IDTransaksi).Count() == 0
                            ? 0
                            : db.TBTransaksis.Where(item => item.TBTempat.TBKategoriTempat.IDKategoriTempat == itemHasil.IDKategoriTempat).Sum(item2 => item2.GrandTotal)
                        }).ToArray();

                        RepeaterTotalKategoriTempat.DataSource = DataTotal;
                        RepeaterTotalKategoriTempat.DataBind();

                        #region Labelling
                        LabelTahun.Text = Request.QueryString["year"].ToString().ToUpper();
                        LabelGrandTotal.Text = Parse.ToFormatHarga(_result.Sum(x => x.Hasil.Sum(y => y.Grandtotal)));
                        #endregion
                    }
                    #endregion
                }
            }
        }
    }
}