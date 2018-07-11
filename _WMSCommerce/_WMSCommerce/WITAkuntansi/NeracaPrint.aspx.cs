using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_NeracaPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Bulan"] != null && Request.QueryString["Tahun"] != null)
                {
                    #region LOAD HEADER
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                    TBStore _store = db.TBStores.FirstOrDefault();

                    string _bulan = DateTime.Parse(int.Parse(Request.QueryString["Bulan"].ToString()) + "/" + "01" + "/" + "2016").ToString("MMMM", new CultureInfo("id-ID"));

                    LabelPeriode.Text = _bulan + " " + Request.QueryString["Tahun"].ToString();
                    LabelNamaPencetak.Text = Pengguna.NamaLengkap;
                    LabelTanggalCetak.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");

                    LabelNamaStore.Text = _store.Nama;
                    LabelAlamatStore.Text = _store.Alamat;
                    LabelTeleponStore.Text = _store.TeleponLain + " / " + _store.Handphone;
                    LabelWebsite.Text = _store.Website;
                    HyperLinkEmail.Text = _store.Email;
                    #endregion

                    LoadNeraca(db);
                    LoadAktiva(db);
                    LoadPasiva(db);
                }
            }
        }
    }
    private List<ListAkun> LoadNeraca(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        List<ListAkun> listAkun = new List<ListAkun>();

        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        decimal TotalAktiva = 0;
        decimal TotalPasiva = 0;
        decimal LabaRugiBerjalan = 0;
        decimal LabaRugiBulanSebelumnya = 0;

        int bulan = int.Parse(Request.QueryString["Bulan"]);
        int tahun = int.Parse(Request.QueryString["Tahun"]);

        //CARI AKUN GRUP
        CariAkunGrup("1", result, listAkun);

        var dataBulanIni = Akuntansi_Class.LaporanLabaRugi(bulan.ToString(), tahun.ToString(), false, pengguna, "");
        var dataBulanSebelumnya = Akuntansi_Class.LaporanLabaRugi((bulan - 1).ToString(), tahun.ToString(), false, pengguna, "");


        LabaRugiBerjalan = dataBulanIni["TotalLabaRugi"];
        LabaRugiBulanSebelumnya = dataBulanSebelumnya["TotalLabaRugi"];

        TotalAktiva = listAkun.Where(item => item.Grup == false).Where(item => item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva).Sum(item2 => item2.Nominal);
        TotalPasiva = listAkun.Where(item => item.Grup == false).Where(item => item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva).Sum(item2 => item2.Nominal) + LabaRugiBerjalan +
            LabaRugiBulanSebelumnya;

        LabelLabaRugiBulanBerjalan.Text = LabaRugiBerjalan.ToFormatHarga();
        LabelLabaRugiBulanSebelumnya.Text = LabaRugiBulanSebelumnya.ToFormatHarga();

        LabelTotalSaldoAktiva.Text =TotalAktiva.ToFormatHarga();
        LabelTotalSaldoPasiva.Text = TotalPasiva.ToFormatHarga();

        return listAkun;
    }
    private void LoadAktiva(DataClassesDatabaseDataContext db)
    {
        List<ListAkun> listAkun = new List<ListAkun>();
        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva) &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup2("1", result, listAkun);

        RepeaterLaporanAktiva.DataSource = listAkun;
        RepeaterLaporanAktiva.DataBind();
    }

    private void LoadPasiva(DataClassesDatabaseDataContext db)
    {
        List<ListAkun> listAkun = new List<ListAkun>();
        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva) &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup2("1", result, listAkun);

        RepeaterLaporanPasiva.DataSource = listAkun;
        RepeaterLaporanPasiva.DataBind();
    }

    private void CariAkunGrup2(string index, TBAkunGrup[] listAkunGrup, List<ListAkun> listAkun)
    {
        int urutan = 1;
        foreach (var item in listAkunGrup)
        {
            listAkun.Add(new ListAkun
            {
                TBAkunGrup = item.TBAkunGrup1,
                Nomor = item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan,
                Kode = string.Empty,
                Grup = true,
                ClassWarna = item.IDAkunGrupParent == null ? "class='success'" : "class='warning'",
                Nama = item.Nama,
                Nominal = 0,
                StatusParent = item.IDAkunGrupParent == null ? true : false

            });

            #region CARI AKUN
            if (item.TBAkuns.Count > 0)
            {
                //CARI AKUN
                CariAkun2(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item, listAkun, Request.QueryString["Bulan"].ToString(), Request.QueryString["Tahun"].ToString());
            }
            #endregion

            #region CARI ANAK AKUN GRUP
            if (item.TBAkunGrups.Count > 0)
            {
                //CARI ANAK AKUN GRUP
                CariAkunGrup2(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item.TBAkunGrups.ToArray(), listAkun);
            }
            #endregion

            urutan++;
        }
    }

    private void CariAkunGrup(string index, TBAkunGrup[] listAkunGrup, List<ListAkun> listAkun)
    {
        int urutan = 1;
        foreach (var item in listAkunGrup)
        {
            listAkun.Add(new ListAkun
            {
                TBAkunGrup = item.TBAkunGrup1,
                Nomor = item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan,
                Kode = string.Empty,
                Grup = true,
                ClassWarna = item.IDAkunGrupParent == null ? "class='success'" : "class='warning'",
                Nama = item.Nama,
                Nominal = 0
            });

            #region CARI AKUN
            if (item.TBAkuns.Count > 0)
            {
                //CARI AKUN
                CariAkun(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item, listAkun,Request.QueryString["bulan"], Request.QueryString["tahun"]);
            }
            #endregion

            #region CARI ANAK AKUN GRUP
            if (item.TBAkunGrups.Count > 0)
            {
                //CARI ANAK AKUN GRUP
                CariAkunGrup(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item.TBAkunGrups.ToArray(), listAkun);
            }
            #endregion

            urutan++;
        }
    }

    private void CariAkun(string index, TBAkunGrup akunGrup, List<ListAkun> listAkun, string bulan, string tahun)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        int urutan = 1;
        foreach (var item in akunGrup.TBAkuns.OrderBy(item => item.Kode))
        {
            listAkun.Add(new ListAkun
            {
                TBAkunGrup = item.TBAkunGrup,
                Nomor = "&nbsp&nbsp&nbsp" + index + "." + urutan,
                Kode = item.Kode,
                Grup = false,
                ClassWarna = string.Empty,
                Nama = item.Nama,
                Nominal = (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false) < 0 && item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ?

                                                                   Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)) :

                                                                        (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)))
            });

            urutan++;
        }
    }

    private void CariAkun2(string index, TBAkunGrup akunGrup, List<ListAkun> listAkun, string bulan, string tahun)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        int urutan = 1;
        foreach (var item in akunGrup.TBAkuns.OrderBy(item => item.Kode))
        {
            listAkun.Add(new ListAkun
            {
                TBAkunGrup = item.TBAkunGrup,
                Nomor = "&nbsp&nbsp&nbsp" + index + "." + urutan,
                Kode = item.Kode,
                Grup = false,
                ClassWarna = string.Empty,
                Nama = item.Nama,
                Nominal = (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false) < 0 && item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ?

                                                                   Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)) :

                                                                        (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)))
            });

            urutan++;
        }
    }

    private class ListAkun
    {
        public TBAkunGrup TBAkunGrup { get; set; }
        public string Nomor { get; set; }
        public string Kode { get; set; }
        public bool Grup { get; set; }
        public bool StatusParent { get; set; }
        public string ClassWarna { get; set; }
        public string Nama { get; set; }
        public decimal Nominal { get; set; }
    }
}
