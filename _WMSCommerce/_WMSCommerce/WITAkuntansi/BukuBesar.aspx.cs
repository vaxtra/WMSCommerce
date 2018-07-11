using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_BukuBesar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            TextBoxTanggalPeriode1.Text = DateTime.Now.ToString("d MMMM yyyy");
            TextBoxTanggalPeriode2.Text = DateTime.Now.ToString("d MMMM yyyy");

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Akun_Class ClassAkun = new Akun_Class();

                var ListAkun = ClassAkun.Data(db)
                    .OrderBy(item => item.IDAkunGrup)
                    .ThenBy(item => item.IDAkun)
                    .ToArray();

                DropDownListAkun.Items.Clear();

                foreach (var item in ListAkun)
                {
                    DropDownListAkun.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });
                }
                if (Request.QueryString["id"] != null && Request.QueryString["month"] != null)
                    LoadDataQueryString(Request.QueryString["id"], Request.QueryString["month"]);
                else
                    LoadData();
            }
        }
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            RepeaterBukuBesar.DataSource = db.TBJurnalDetails.OrderBy(item => item.TBJurnal.Tanggal).ToArray()
                .Where(item =>
                    item.TBJurnal.Tanggal.Value.Date >= (TextBoxTanggalPeriode1.Text).ToDateTime() &&
                    item.TBJurnal.Tanggal.Value.Date <= (TextBoxTanggalPeriode2.Text).ToDateTime() &&
                    item.TBJurnal.IDTempat == Pengguna.IDTempat &&
                    item.IDAkun == (DropDownListAkun.SelectedValue).ToInt())
                    .Select(item => new
                    {
                        item.IDJurnal,
                        item.TBJurnal.Tanggal,
                        item.TBJurnal.Referensi,
                        item.TBJurnal.Keterangan,
                        Debit = item.Debit == 0 ? "-" : (item.Debit).ToFormatHarga(),
                        Kredit = item.Kredit == 0 ? "-" : (item.Kredit).ToFormatHarga(),
                        item.TBAkun.TBAkunGrup.EnumSaldoNormal,
                        Saldo = (HitungSaldo((decimal)item.Debit, (decimal)item.Kredit) < 0) && item.TBAkun.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit && item.TBAkun.TBAkunGrup.EnumJenisAkunGrup == 
                        (int)PilihanJenisAkunGrup.Pasiva ? Math.Abs(Saldo) : Saldo
                    });
            RepeaterBukuBesar.DataBind();

            ButtonPrint.OnClientClick = "return popitup('BukuBesarPrint.aspx" + "?Periode1=" + TextBoxTanggalPeriode1.Text + "&Periode2=" + TextBoxTanggalPeriode2.Text + "&IDAkun=" + DropDownListAkun.SelectedValue + "')";
        }
    }

    private void LoadDataQueryString(string id, string month)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            RepeaterBukuBesar.DataSource = db.TBJurnalDetails.OrderBy(item => item.TBJurnal.Tanggal).ToArray()
                .Where(item =>
                    item.TBJurnal.Tanggal.Value.Month >= (month).ToInt() &&
                    item.TBJurnal.Tanggal.Value.Month <= (month).ToInt() &&
                    item.IDAkun == (id).ToInt() &&
                    item.TBJurnal.IDTempat == Pengguna.IDTempat)
                    .Select(item => new
                    {
                        item.IDJurnal,
                        item.TBJurnal.Tanggal,
                        item.TBJurnal.Referensi,
                        item.TBJurnal.Keterangan,
                        Debit = item.Debit == 0 ? "-" : (item.Debit).ToFormatHarga(),
                        Kredit = item.Kredit == 0 ? "-" : (item.Kredit).ToFormatHarga(),
                        item.TBAkun.TBAkunGrup.EnumSaldoNormal,
                        Saldo = (HitungSaldo((decimal)item.Debit, (decimal)item.Kredit) < 0) && item.TBAkun.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit && item.TBAkun.TBAkunGrup.EnumJenisAkunGrup ==
                        (int)PilihanJenisAkunGrup.Pasiva ? Math.Abs(Saldo) : Saldo
                    });
            RepeaterBukuBesar.DataBind();

            ButtonPrint.OnClientClick = "return popitup('BukuBesarPrint.aspx" + "?Periode1=" + TextBoxTanggalPeriode1.Text + "&Periode2=" + TextBoxTanggalPeriode2.Text + "&IDAkun=" + DropDownListAkun.SelectedValue + "')";
        }
    }

    private decimal Saldo { get; set; }

    public decimal HitungSaldo(decimal debit, decimal kredit)
    {
        decimal _saldo = 0;

        _saldo = debit - kredit;

        return Saldo += _saldo;
    }

    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            var _result = Akuntansi_Class.LaporanBukuBesar(TextBoxTanggalPeriode1.Text, TextBoxTanggalPeriode2.Text, (DropDownListAkun.SelectedValue).ToInt(), true, Pengguna);
            LinkDownload.Visible = true;

            if (LinkDownload.Visible)
                LinkDownload.HRef = Akuntansi_Class.LinkDownload;
        }
    }
}