using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_XForecast : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);
                Tanggal_Class Tanggal_Class = new Tanggal_Class();

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList().Where(item => item.Value != "0").ToArray());

                DropDownListBulan.Items.AddRange(Tanggal_Class.DropdownlistBulan());
                DropDownListTahun.Items.AddRange(Tanggal_Class.DropdownlistTahun());

                DropDownListBulan.Visible = false;
            }
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            int tahun = DropDownListTahun.SelectedValue.ToInt();
            int bulan = DropDownListBulan.SelectedValue.ToInt();
            int idTempat = DropDownListTempat.SelectedValue.ToInt();
            Tanggal_Class Tanggal_Class = new Tanggal_Class();

            if (DropDownListTipe.SelectedValue == "1")
            {
                var ForecastBulan = db.TBForecasts
                    .Where(item =>
                        item.IDTempat == idTempat &&
                        item.Tanggal.Year == tahun)
                    .GroupBy(item => item.Tanggal.Month)
                    .Select(item => new
                    {
                        item.Key,
                        Nominal = item.Sum(item2 => item2.Nominal),
                        Quantity = item.Sum(item2 => item2.Quantity)
                    });

                var Result = Tanggal_Class.DropdownlistBulan()
                    .Select(item => new
                    {
                        Key = item.Value,
                        Nama = item.Text,
                        Nominal = ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()) != null ? ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()).Nominal : 0,
                        Quantity = ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()) != null ? ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()).Quantity : 0,
                        Weekend = 1 //BUKAN WEEKEND
                    });

                TextBoxTotalNominal.Text = Result.Sum(item => item.Nominal).ToString();
                TextBoxTotalQuantity.Text = Result.Sum(item => item.Quantity).ToString();

                RepeaterForecastBulan.DataSource = Result;
                RepeaterForecastBulan.DataBind();
            }
            else if (DropDownListTipe.SelectedValue == "2")
            {
                var ForecastBulan = db.TBForecasts
                    .Where(item =>
                        item.IDTempat == idTempat &&
                        item.Tanggal.Month == bulan &&
                        item.Tanggal.Year == tahun)
                    .Select(item => new
                    {
                        Key = item.Tanggal.Day,
                        Nominal = item.Nominal,
                        Quantity = item.Quantity
                    });

                var Result = Tanggal_Class.DropDownListHariBulan(tahun, bulan)
                    .Select(item => new
                    {
                        Key = item.Value,
                        Nama = item.Text,
                        Nominal = ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()) != null ? ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()).Nominal : 0,
                        Quantity = ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()) != null ? ForecastBulan.FirstOrDefault(item2 => item2.Key == item.Value.ToInt()).Quantity : 0,
                        Weekend = new DateTime(tahun, bulan, item.Value.ToInt()).DayOfWeek
                    });

                TextBoxTotalNominal.Text = Result.Sum(item => item.Nominal).ToString();
                TextBoxTotalQuantity.Text = Result.Sum(item => item.Quantity).ToString();

                RepeaterForecastBulan.DataSource = Result;
                RepeaterForecastBulan.DataBind();
            }
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            foreach (RepeaterItem item in RepeaterForecastBulan.Items)
            {
                HiddenField HiddenFieldID = (HiddenField)item.FindControl("HiddenFieldID");
                TextBox TextBoxNominal = (TextBox)item.FindControl("TextBoxNominal");
                TextBox TextBoxQuantity = (TextBox)item.FindControl("TextBoxQuantity");

                int idTempat = DropDownListTempat.SelectedValue.ToInt();
                int tahun = DropDownListTahun.SelectedValue.ToInt();

                if (DropDownListTipe.SelectedValue == "1")
                {
                    #region FORECAST TAHUN
                    int bulan = HiddenFieldID.Value.ToInt();

                    int JumlahHari = DateTime.DaysInMonth(tahun, bulan);

                    decimal Nominal = TextBoxNominal.Text.ToDecimal();
                    decimal Quantity = TextBoxQuantity.Text.ToDecimal();

                    decimal NominalPerHari = Nominal / JumlahHari;
                    decimal QuantityPerHari = Math.Ceiling(Quantity / JumlahHari);

                    for (int i = 1; i <= JumlahHari; i++)
                    {
                        var Forecast = db.TBForecasts
                            .FirstOrDefault(item2 =>
                                item2.IDTempat == idTempat &&
                                item2.Tanggal == new DateTime(tahun, bulan, i));

                        if (NominalPerHari > 0 || QuantityPerHari > 0) //JIKA NOMINAL QUANTITY LEBIH DARI 0
                        {
                            if (Forecast == null)
                            {
                                //MEMBUAT FORECAST BARU
                                Forecast = new TBForecast
                                {
                                    IDTempat = idTempat,
                                    Tanggal = new DateTime(tahun, bulan, i),
                                    //Nominal
                                    //Quantity
                                };

                                db.TBForecasts.InsertOnSubmit(Forecast);
                            }

                            Forecast.Nominal = NominalPerHari;
                            Forecast.Quantity = QuantityPerHari;
                        }
                        else if (Forecast != null)
                            db.TBForecasts.DeleteOnSubmit(Forecast);

                        db.SubmitChanges();
                    }
                    #endregion
                }
                else if (DropDownListTipe.SelectedValue == "2")
                {
                    #region FORECAST BULAN
                    int bulan = DropDownListBulan.SelectedValue.ToInt();
                    int hari = HiddenFieldID.Value.ToInt();
                    decimal Nominal = TextBoxNominal.Text.ToDecimal();
                    decimal Quantity = TextBoxQuantity.Text.ToDecimal();
                    DateTime tanggal = new DateTime(tahun, bulan, hari);

                    var Forecast = db.TBForecasts
                        .FirstOrDefault(item2 =>
                            item2.IDTempat == idTempat &&
                            item2.Tanggal == tanggal);

                    if (Nominal > 0 || Quantity > 0) //JIKA NOMINAL QUANTITY LEBIH DARI 0
                    {
                        if (Forecast == null)
                        {
                            //MEMBUAT FORECAST BARU
                            Forecast = new TBForecast
                            {
                                IDTempat = idTempat,
                                Tanggal = tanggal,
                                //Nominal
                                //Quantity
                            };

                            db.TBForecasts.InsertOnSubmit(Forecast);
                        }

                        Forecast.Nominal = Nominal;
                        Forecast.Quantity = Quantity;
                    }
                    else if (Forecast != null)
                        db.TBForecasts.DeleteOnSubmit(Forecast);

                    db.SubmitChanges();
                    #endregion
                }
            }
        }
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonBagiRata_Click(object sender, EventArgs e)
    {
        decimal totalNominal = 0;
        decimal totalQuantity = 0;

        foreach (RepeaterItem item in RepeaterForecastBulan.Items)
        {
            TextBox TextBoxNominal = (TextBox)item.FindControl("TextBoxNominal");
            TextBox TextBoxQuantity = (TextBox)item.FindControl("TextBoxQuantity");

            var nominal = TextBoxTotalNominal.Text.ToDecimal() / RepeaterForecastBulan.Items.Count;
            var quantity = Math.Ceiling(TextBoxTotalQuantity.Text.ToDecimal() / RepeaterForecastBulan.Items.Count);

            totalNominal += nominal;
            totalQuantity += quantity;

            TextBoxNominal.Text = (nominal).ToString();
            TextBoxQuantity.Text = (quantity).ToString();
        }

        TextBoxTotalNominal.Text = totalNominal.ToString();
        TextBoxTotalQuantity.Text = totalQuantity.ToString();
    }
    protected void DropDownListTipe_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListTipe.SelectedValue == "1")
            DropDownListBulan.Visible = false;
        else
            DropDownListBulan.Visible = true;
    }
}