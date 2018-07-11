using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plugins_datatable_Result : System.Web.UI.Page
{
    public class ListData
    {
        public dynamic[][] aaData { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                ListData _listData = new ListData();

                _listData.aaData = db.TBPerpindahanStokProduks.Select(x => new[]
                {
                x.IDJenisPerpindahanStok.ToString(),
                x.IDPengguna.ToString(),
                x.IDPerpindahanStokProduk.ToString(),
                x.Jumlah.ToString(),
                x.TBStokProduk.TBKombinasiProduk.Nama
                }).ToArray();

                Response.Clear();
                Response.Write(JsonConvert.SerializeObject(_listData));
                Response.End();
            }

        }
    }
}