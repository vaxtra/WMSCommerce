using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_Forecast : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PENCARIAN Mean Square Error Terkecil dengan merubah Alpha dan Beta

        TrendAdjustedExponentialSmoothing Forecast = new TrendAdjustedExponentialSmoothing();

        Forecast.Alpha = (decimal)1;
        Forecast.Beta = (decimal)1;
        decimal Pengurangan = (decimal)0.001; //0.05

        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1991", Actual = 2634 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1992", Actual = 3169 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1993", Actual = 3301 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1994", Actual = 3754 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1995", Actual = 3834 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1996", Actual = 5117 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1997", Actual = 6448 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1998", Actual = 7908 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "1999", Actual = 9213 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2000", Actual = 11502 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2001", Actual = 10791 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2002", Actual = 10022 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2003", Actual = 8342 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2004", Actual = 10453 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2005", Actual = 10784 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2006", Actual = 10718 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2007", Actual = 12460 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2008", Actual = 13262 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2009", Actual = 8772 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2010", Actual = 10431 });
        Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "2011" });

        //Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "April", Actual = 570 });
        //Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "Mei", Actual = 2645 });
        //Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "Juni", Actual = 15672 });
        //Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "Juli", Actual = 16273 });
        //Forecast.Detail.Add(new TrendAdjustedExponentialSmoothingDetail { Title = "Agustus" });

        decimal _tempAlpha = 0;
        decimal _tempBeta = 0;
        decimal? _tempMeanSquareError = null;

        do
        {
            do
            {
                Forecast.Hitung();

                if (!_tempMeanSquareError.HasValue || _tempMeanSquareError > Forecast.MeanSquareError)
                {
                    _tempAlpha = Forecast.Alpha;
                    _tempBeta = Forecast.Beta;
                    _tempMeanSquareError = Forecast.MeanSquareError;
                }

                Forecast.Beta -= Pengurangan;
            } while (Forecast.Beta >= 0);

            Forecast.Beta = 1;
            Forecast.Alpha -= Pengurangan;

        } while (Forecast.Alpha >= 0);

        Forecast.Alpha = _tempAlpha;
        Forecast.Beta = _tempBeta;
        Forecast.Hitung();

        LabelAlpha.Text = Forecast.Alpha.ToString();
        LabelBeta.Text = Forecast.Beta.ToString();
        LabelMeanSquareError.Text = Forecast.MeanSquareError.ToString();
        LabelStandardError.Text = Forecast.StandardError.ToString();

        RepeaterData.DataSource = Forecast.Detail;
        RepeaterData.DataBind();
    }
}