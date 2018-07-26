using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_BukuBesarPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Periode1"] != null && Request.QueryString["Periode2"] != null && Request.QueryString["IDAkun"] != null )
            {
                LoadData();
            }
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            TBStore _store = db.TBStores.FirstOrDefault();

            var Periode1 = Request.QueryString["Periode1"];
            var Periode2 = Request.QueryString["Periode2"];
            var IDAkun = Request.QueryString["IDAkun"];
            var Akun = db.TBAkuns.FirstOrDefault(item => item.IDAkun == IDAkun.ToInt());

            LabelPeriode.Text = Periode1 + " - " + Periode2;
            LabelNamaPencetak.Text = Pengguna.NamaLengkap;
            LabelTanggalCetak.Text = DateTime.Now.ToString("d MMMM yyyy");

            LabelNamaStore.Text = _store.Nama;
            LabelAlamatStore.Text = _store.Alamat;
            LabelTeleponStore.Text = _store.TeleponLain + " / " + _store.Handphone;
            LabelWebsite.Text = _store.Website;
            #endregion

            LabelNamaAkun.Text = Akun.Nama;

            RepeaterBukuBesar.DataSource = db.TBJurnalDetails.ToArray()
                .Where(item =>
                    item.TBJurnal.Tanggal.Value.Date >= Periode1.ToDateTime() &&
                    item.TBJurnal.Tanggal.Value.Date <= Periode2.ToDateTime() &&
                    item.IDAkun == Akun.IDAkun &&
                    item.TBJurnal.IDTempat == Pengguna.IDTempat)
                    .Select(item => new
                    {
                        item.IDJurnal,
                        item.TBJurnal.Tanggal,
                        item.TBJurnal.Referensi,
                        item.TBJurnal.Keterangan,
                        Debit = item.Debit == 0 ? "-" : item.Debit.ToFormatHarga(),
                        Kredit = item.Kredit == 0 ? "-" : item.Kredit.ToFormatHarga(),
                        item.TBAkun.TBAkunGrup.EnumSaldoNormal,
                        Saldo = (HitungSaldo((decimal)item.Debit, (decimal)item.Kredit) < 0) && item.TBAkun.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ? Math.Abs(Saldo) : Saldo
                    })
                    .OrderBy(item => item.Tanggal)
                    .ThenBy(item => item.IDJurnal);
            RepeaterBukuBesar.DataBind();
        }
    }

    private decimal Saldo { get; set; }

    public decimal HitungSaldo(decimal debit, decimal kredit)
    {
        decimal _saldo = 0;

        _saldo = debit - kredit;

        return Saldo += _saldo;
    }
}