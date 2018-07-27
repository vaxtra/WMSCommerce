using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Store_Tempat_Distance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    foreach (var item in db.TBTempatJaraks.ToArray())
                    {
                        if (item.JarakNilai == 0)
                        {
                            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + item.TBTempat.Latitude + "," + item.TBTempat.Longitude + "&destinations=" + item.TBTempat1.Latitude + "," + item.TBTempat1.Longitude;

                            WebClient client = new WebClient();
                            string result = client.DownloadString(url);
                            client.Dispose();

                            var Jarak = JsonConvert.DeserializeObject<GoogleMapsDistanceMatrix>(result);

                            if (Jarak.rows.Count() > 0 && Jarak.rows.FirstOrDefault().elements.Count() > 0 && Jarak.rows.FirstOrDefault().elements.FirstOrDefault().status == "OK")
                            {
                                var Penghitungan = Jarak.rows.FirstOrDefault().elements.FirstOrDefault();

                                item.Jarak = Penghitungan.distance.text;
                                item.JarakNilai = Penghitungan.distance.value;

                                item.Durasi = Penghitungan.duration.text.Replace("mins", "menit").Replace("min", "menit").Replace("hours", "jam").Replace("hour", "jam").Replace("days", "hari").Replace("day", "hari");
                                item.DurasiNilai = Penghitungan.duration.value;
                            }

                        }
                    }

                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
            }

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                HiddenFieldJumlahTempat.Value = db.TBTempats.Count().ToString();

                RepeaterTempat.DataSource = db.TBTempats
                    .Select(item => new
                    {

                        item.Nama,
                        Kategori = item.TBKategoriTempat.Nama,
                        Jarak = LoadTempatJarak(item.TBTempatJaraks.ToList(), item.TBTempatJaraks1.ToList())
                    }).ToArray();
                RepeaterTempat.DataBind();
            }
        }
    }

    private List<TBTempatJarak> LoadTempatJarak(List<TBTempatJarak> jarak1, List<TBTempatJarak> jarak2)
    {
        var DataJarak = jarak1;

        DataJarak.AddRange(jarak2.Select(item => new TBTempatJarak
        {
            Durasi = item.Durasi,
            DurasiNilai = item.DurasiNilai,
            Jarak = item.Jarak,
            JarakNilai = item.JarakNilai,
            TBTempat = item.TBTempat1,
            TBTempat1 = item.TBTempat
        }));

        return DataJarak
            .Where(item => item.JarakNilai > 0)
            .OrderBy(item => item.JarakNilai)
            .ToList();
    }
}