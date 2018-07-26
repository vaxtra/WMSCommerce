using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_BusinessIntelligence_CustomerBehavior : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int index = 0;
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                DropDownListTahun.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                index++;
            }
            DropDownListTahun.SelectedIndex = 5;

            LabelHeader.Text = "MARKET SHARE ANALYSIS " + DropDownListTahun.SelectedValue.ToString();

        }
    }
    public void QueryData(int searchBY, int tahun)
    {
        Literal LiteralChart = (Literal)this.Page.Master.FindControl("LiteralChart");
        LiteralChart.Text = string.Empty;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PanelMarketShare.Visible = true;

            #region MARKET SHARE
            List<string> ListKota = new List<string>();
            var KotaDB = db.TBAlamats.Select(item => item.Kota).Distinct();

            foreach (var item in KotaDB)
            {
                ListKota.Add(item);
            }

            var DataSalesDB = db.TBTransaksis.Where(item => item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).GroupBy(item => new
            {
                item.TBPelanggan.TBAlamats.FirstOrDefault().Kota
            })
                                          .Select(item => new
                                          {
                                              Key = item.Key,
                                              GrandTotal = item.Sum(item2 => item2.GrandTotal),
                                              TotalTransaksi = item.Where(item2 => item2.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).Count(),
                                              TotalNominalDiscount = item.Sum(item2 => item2.TotalPotonganHargaJualDetail),
                                              JumlahProduk = item.Sum(item2 => item2.JumlahProduk)
                                          }).OrderByDescending(item => item.GrandTotal).ToArray();

            var DataSales = ListKota.Select(item => new
            {
                Kota = item,
                JumlahTransaksi = DataSalesDB.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesDB.Where(item2 => item2.Key.Kota == item).Sum(item2 => item2.TotalTransaksi),
                GrandTotal = DataSalesDB.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesDB.Where(item2 => item2.Key.Kota == item).Sum(item2 => item2.GrandTotal),
                JumlahProduk = DataSalesDB.Where(item2 => item2.Key.Kota == item).Count() == 0 ? 0 : DataSalesDB.Where(item2 => item2.Key.Kota == item).Sum(item2 => item2.JumlahProduk),
                SumJumlahProduk = DataSalesDB.Sum(item2 => item2.JumlahProduk),
                SumJumlahTransaksi = DataSalesDB.Sum(item2 => item2.TotalTransaksi),
                SumGrandTotal = DataSalesDB.Sum(item2 => item2.GrandTotal)
            }).ToArray();

            if (searchBY == 1)
            {
                RepeaterMarketShare.DataSource = DataSales.OrderByDescending(item2 => item2.GrandTotal).ToArray();
                RepeaterMarketShare.DataBind();                
            }
            else
            {
                RepeaterMarketShare.DataSource = DataSales.OrderByDescending(item2 => item2.JumlahProduk).ToArray();
                RepeaterMarketShare.DataBind();         
            }


            var Data5Kota = DataSales.OrderByDescending(item2 => item2.GrandTotal).Take(5).ToArray();
            List<string> Top5_Kota = new List<string>();

            for (int i = 0; i < Data5Kota.Count(); i++)
            {
                Top5_Kota.Add(Data5Kota[i].Kota);
            }

            LiteralChart.Text += "<script> $(function () { var dataChart = [";

            for (int i = 0; i < Top5_Kota.Count(); i++)
            {
                var _transaksiTahunIni = DataSalesDB.FirstOrDefault(item => item.Key.Kota == Top5_Kota[i].ToString());
                //var _transaksiTahunLalu = _dataDetailTransaksiTahunLaluGroupByIDTempat.FirstOrDefault(item => item.Key.IDTempat == tempatDatabase[i].IDTempat);

                decimal _grandTotal = 0, _grandTotalTahunLalu = 0;

                if (_transaksiTahunIni != null)
                    _grandTotal = (decimal)_transaksiTahunIni.GrandTotal;

                //if (_transaksiTahunLalu != null)
                //    _grandTotalTahunLalu = (decimal)_transaksiTahunLalu.GrandTotal;

                LiteralChart.Text += "{ 'y': '" + Top5_Kota[i].ToString() + "', '" + (tahun - 1) + "': " + _grandTotalTahunLalu + ", '" + tahun + "': " + _grandTotal + " }, ";
            }
            LiteralChart.Text += "]; Morris.Bar({ element: 'graph13', behaveLikeLine: true, data: dataChart, xkey: 'y', ";

            LiteralChart.Text += "ykeys: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
            LiteralChart.Text += "labels: ['" + (DropDownListTahun.SelectedValue.ToInt() - 1) + "','" + DropDownListTahun.SelectedValue.ToInt() + "'],";
            LiteralChart.Text += "barColors:['#7FC4C1','#FF625D'],";
            LiteralChart.Text += " parseTime: false,resize:true}); eval(dataChart); }); </script>";

            #region Labelling
            LabelTotalJumlahTransaksiHeader.Text = DataSales.Sum(item => item.JumlahTransaksi).ToFormatHargaBulat();
            LabelTotalGrandTotalHeader.Text = DataSales.Sum(item => item.GrandTotal).ToFormatHarga();
            LabelJumlahProdukHeader.Text = DataSales.Sum(item => item.JumlahProduk).ToFormatHargaBulat();

            LabelTotalJumlahTransaksiFooter.Text = DataSales.Sum(item => item.JumlahTransaksi).ToFormatHargaBulat();
            LabelTotalGrandTotalFooter.Text = DataSales.Sum(item => item.GrandTotal).ToFormatHarga();
            LabelJumlahProdukFooter.Text = DataSales.Sum(item => item.JumlahProduk).ToFormatHargaBulat();
            #endregion

            #endregion
        }
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        QueryData(int.Parse(DropDownListFilter.SelectedValue), int.Parse(DropDownListTahun.SelectedValue));
    }

    public string Pertumbuhan(decimal pertumbuhan)
    {
        if (pertumbuhan < 0)
            return "<span class='label label-danger'>" + pertumbuhan.ToFormatHarga() + "</span>";
        else
            return "<span class='label label-success'>" + pertumbuhan.ToFormatHarga() + "</span>";
    }
}
