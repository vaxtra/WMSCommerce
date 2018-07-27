using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_NeracaSaldo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownListBulan.Items.Clear();
            DropDownListBulan.Items.AddRange(Akuntansi_Class.DropdownlistBulanLaporan());
            DropDownListBulan.SelectedValue = DateTime.Now.Month.ToString();

            DropDownListTahun.Items.Clear();
            DropDownListTahun.Items.AddRange(Akuntansi_Class.DropdownlistTahunLaporan());
            DropDownListTahun.SelectedValue = DateTime.Now.Year.ToString();

            LoadData();
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


            var _grupAkun = db.TBAkunGrups.ToArray()
            .Select(item => new
            {
                item.IDAkunGrup,
                item.Nama,
                Akuns = item.TBAkuns.Select(item2 => new
                {
                    item2.Kode,
                    item2.Nama,
                    Results = HitungSaldo((PilihanDebitKredit)item.EnumSaldoNormal, item2.TBJurnalDetails
                    .Where(item3 =>
                        item3.TBJurnal.Tanggal.Value.Year == int.Parse(DropDownListTahun.SelectedValue) &&
                        item3.TBJurnal.Tanggal.Value.Month == int.Parse(DropDownListBulan.SelectedValue)).ToArray())
                })
            });

            RepeaterGrupAkun.DataSource = _grupAkun;
            RepeaterGrupAkun.DataBind();

            LabelKredit.Text = Kredit.ToFormatHarga();
            LabelDebit.Text = Debit.ToFormatHarga();
        }
    }

    private decimal Debit { get; set; }
    private decimal Kredit { get; set; }
    public Dictionary<string, dynamic> HitungSaldo(PilihanDebitKredit pilihanDebitKredit, TBJurnalDetail[] _jurnalDetail)
    {
        Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();

        decimal _saldo = 0;

        foreach (var item in _jurnalDetail)
            _saldo += (decimal)item.Debit - (decimal)item.Kredit;

        //Saldo normal DEBIT dan lebih besar dari 0 --> tetap DEBIT
        if (PilihanDebitKredit.Debit == pilihanDebitKredit && _saldo >= 0)
        {
            Debit += _saldo;

            _result.Add("Debit", _saldo);
            _result.Add("Kredit", 0);
        }
        //Saldo normal DEBIT dan kecil dari 0 --> pindah ke KREDIT
        else if (PilihanDebitKredit.Debit == pilihanDebitKredit && _saldo < 0)
        {
            _saldo = Math.Abs(_saldo);
            Kredit += _saldo;

            _result.Add("Debit", 0);
            _result.Add("Kredit", _saldo);
        }
        //Saldo normal KREDIT dan kecil dari 0 --> tetap KREDIT
        else if (PilihanDebitKredit.Kredit == pilihanDebitKredit && _saldo < 0)
        {
            _saldo = Math.Abs(_saldo);
            Kredit += _saldo;

            _result.Add("Debit", 0);
            _result.Add("Kredit", _saldo);
        }
        //Saldo normal KREDIT dan lebih besar dari 0 --> pindah ke DEBIT
        else if (PilihanDebitKredit.Kredit == pilihanDebitKredit && _saldo >= 0)
        {
            Debit += _saldo;

            _result.Add("Debit", _saldo);
            _result.Add("Kredit", 0);
        }

        return _result;
    }
}