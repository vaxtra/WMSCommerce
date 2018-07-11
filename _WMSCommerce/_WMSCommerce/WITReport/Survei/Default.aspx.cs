using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Survei_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            int Koresponden = db.TBSoalJawabanPelanggans
                .Select(item => new
                {
                    item.IDPelanggan
                })
                .Distinct()
                .Count();

            var _dataDatabase = db.TBSoalPertanyaans
                .Select(item => new
                {
                    item.Nomor,
                    Pertanyaan = item.Isi,
                    SoalJawabans = item.TBSoalJawabans.Select(item2 => new
                    {
                        Jawaban = item2.Isi.ToString(),
                        Jumlah = item2.TBSoalJawabanPelanggans.Count,
                        Persentase = Koresponden > 0 ? (int)Math.Round((((decimal)item2.TBSoalJawabanPelanggans.Count / Koresponden) * 100), 0, MidpointRounding.AwayFromZero) : 0,
                        Bobot = PenghitunganBobot(item2.TBSoalJawabanPelanggans.ToList())
                    })
                }).OrderBy(item => item.Nomor);

            RepeaterSoalPertanyaan.DataSource = _dataDatabase;
            RepeaterSoalPertanyaan.DataBind();
        }
    }

    private int PenghitunganBobot(List<TBSoalJawabanPelanggan> JawabanPelanggan)
    {
        if (JawabanPelanggan.Count > 0)
            return JawabanPelanggan.Sum(item => item.TBSoalJawaban.Bobot);
        else
            return 0;
    }
}