using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_SaldoAwal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListBulan.Items.Clear();
                DropDownListBulan.Items.AddRange(Akuntansi_Class.DropdownlistBulanLaporan());
                DropDownListBulan.SelectedValue = DateTime.Now.Month.ToString();

                DropDownListTahun.Items.Clear();
                DropDownListTahun.Items.AddRange(Akuntansi_Class.DropdownlistTahunLaporan());
                DropDownListTahun.SelectedValue = DateTime.Now.Year.ToString();

                LoadNeraca(db);

                LoadNeracaAktiva(db);
                LoadNeracaPasiva(db);
            }
        }
    }

    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadNeraca(db);
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        List<TBJurnal> ListJurnal = new List<TBJurnal>();
        List<TBAkunSaldoAwal> ListAkunSaldoAwal = new List<TBAkunSaldoAwal>();
        List<string> ListSaldoAwalSudahAda = new List<string>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var DataAkun = db.TBAkuns.ToArray();
            var DataSaldoAwal = db.TBAkunSaldoAwals.Where(item =>
            item.TanggalSaldoAwal.Value.Date == (DropDownListBulan.SelectedItem.Value.ToInt() + "/" + "01" + "/" + DropDownListTahun.Text).ToDateTime() &&
            item.IDTempat == Pengguna.IDTempat);
            LiteralWarning.Text = "";
            peringatan.Visible = false;

            foreach (RepeaterItem item in RepeaterLaporan.Items)
            {
                TextBox SaldoAwalAktiva = (TextBox)item.FindControl("TextBoxNominalSaldoAwalAktiva");
                HiddenField IDAkun = (HiddenField)item.FindControl("HiddenFieldIDAkun");

                if (DataSaldoAwal.FirstOrDefault(data => data.IDAkun == IDAkun.Value.ToInt()) != null && SaldoAwalAktiva.Text.ToDecimal() != 0)
                {
                    peringatan.Visible = true;
                    ListSaldoAwalSudahAda.Add("Saldo Awal " + DataAkun.FirstOrDefault(item2 => item2.IDAkun == IDAkun.Value.ToInt()).Nama + " sudah ada pada system");
                }
                else
                {
                    if ((SaldoAwalAktiva.Text).ToDecimal() > 0)
                    {
                        TBJurnal Jurnal = NewJurnal(Pengguna);
                        TBAkunSaldoAwal AkunSaldoAwal = NewAkunSaldoAwal(IDAkun.Value.ToInt(), Pengguna.IDPengguna, ((DropDownListBulan.SelectedItem.Value).ToInt() + "/" + "01" + "/" + DropDownListTahun.Text).ToDateTime(),Pengguna.IDTempat);

                        var Data = db.TBAkuns.FirstOrDefault(item2 => item2.IDAkun == (IDAkun.Value).ToInt());

                        if (Data.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Debit && Data.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva)
                        {
                            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = (IDAkun.Value).ToInt(),
                                Debit = (SaldoAwalAktiva.Text).ToDecimal(),
                                Kredit = 0
                            });

                            ListJurnal.Add(Jurnal);
                            ListAkunSaldoAwal.Add(AkunSaldoAwal);
                        }
                        else if (Data.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit && Data.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva)
                        {
                            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = (IDAkun.Value).ToInt(),
                                Debit = 0,
                                Kredit = (SaldoAwalAktiva.Text).ToDecimal()
                            });

                            ListJurnal.Add(Jurnal);
                            ListAkunSaldoAwal.Add(AkunSaldoAwal);
                        }
                        else if (Data.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit && Data.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva)
                        {
                            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = (IDAkun.Value).ToInt(),
                                Debit = 0,
                                Kredit = (SaldoAwalAktiva.Text).ToDecimal()
                            });

                            ListJurnal.Add(Jurnal);
                            ListAkunSaldoAwal.Add(AkunSaldoAwal);
                        }

                    }
                }
            }

            for (int i = 0; i < ListSaldoAwalSudahAda.Count; i++)
            {
                LiteralWarning.Text += ListSaldoAwalSudahAda.ElementAt(i).ToString() + "<br/>";
            }

            db.TBAkunSaldoAwals.InsertAllOnSubmit(ListAkunSaldoAwal);
            db.TBJurnals.InsertAllOnSubmit(ListJurnal);
            db.SubmitChanges();
        }
        Response.Redirect("SaldoAwal.aspx");
    }

    private TBJurnal NewJurnal(PenggunaLogin Pengguna)
    {
        return new TBJurnal
        {
            Tanggal = ((DropDownListBulan.SelectedItem.Value).ToInt() + "/" + "01" + "/" + DropDownListTahun.Text).ToDateTime(),
            IDTempat = Pengguna.IDTempat,
            Keterangan = "#SaldoAwal " +
            (DropDownListBulan.SelectedItem.Value + "/" + "01" + "/" +
            DropDownListTahun.Text).ToDateTime().ToString("MMMM", new CultureInfo("id-ID")) + " - " +
            Pengguna.NamaLengkap + " (" +
            DateTime.Now.ToLongTimeString() + ")",

            IDPengguna = Pengguna.IDPengguna,
            Referensi = ""
        };
    }

    private TBAkunSaldoAwal NewAkunSaldoAwal(int _IDAkun, int _IDPengguna, DateTime _TanggalSaldoAwal, int _idtempat)
    {
        return new TBAkunSaldoAwal
        {
            IDAkun = _IDAkun,
            IDTempat = _idtempat,
            IDPengguna = _IDPengguna,
            TanggalDaftar = DateTime.Now,
            TanggalSaldoAwal = _TanggalSaldoAwal
        };
    }

    private List<ListAkun> LoadNeraca(DataClassesDatabaseDataContext db)
    {

        List<ListAkun> listAkun = new List<ListAkun>();

        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        decimal TotalAktiva = 0;
        decimal TotalPasiva = 0;

        //CARI AKUN GRUP
        CariAkunGrup("1", result, listAkun);

        TotalAktiva = listAkun.Where(item => item.Grup == false).Where(item => item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva).Sum(item2 => item2.Nominal);
        TotalPasiva = listAkun.Where(item => item.Grup == false).Where(item => item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva).Sum(item2 => item2.Nominal);

        RepeaterLaporan.DataSource = listAkun;
        RepeaterLaporan.DataBind();

        return listAkun;
    }


    private void CariAkunGrup(string index, TBAkunGrup[] listAkunGrup, List<ListAkun> listAkun)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

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
                CariAkun(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item, listAkun, DropDownListBulan.SelectedItem.Value, DropDownListTahun.SelectedItem.Text, Pengguna.IDTempat);
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

    private void CariAkun(string index, TBAkunGrup akunGrup, List<ListAkun> listAkun, string bulan, string tahun, int idtempat)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];


            int urutan = 1;
            var DataJurnalSaldoAwal = db.TBJurnals.Where(item => item.Keterangan.Contains("#SaldoAwal") &&
            item.Tanggal.Value.Month.ToString() == bulan && item.IDTempat == pengguna.IDTempat);

            foreach (var item in akunGrup.TBAkuns)
            {
                listAkun.Add(new ListAkun
                {
                    TBAkunGrup = item.TBAkunGrup,
                    IDAkun = item.IDAkun,
                    Nomor = "&nbsp&nbsp&nbsp" + index + "." + urutan,
                    Kode = item.Kode,
                    Grup = false,
                    ClassWarna = string.Empty,
                    Nama = item.Nama,
                    Nominal = item.TBAkunSaldoAwals.FirstOrDefault(item2 => item.IDAkun == item2.IDAkun &&
                    item2.TanggalSaldoAwal.Value.Month.ToString() == bulan) == null ?

                                                                        ((Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() - 1 &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false) < 0 && item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ?

                                                                       Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() - 1 &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false))

                                                                            :

                                                                            (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                            .Where(item2 =>
                                                                            item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() - 1 &&
                                                                                item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)))) :

                                                                                 DataJurnalSaldoAwal.FirstOrDefault(item2 => item2.TBJurnalDetails.FirstOrDefault().IDAkun == item.IDAkun) != null ? DataJurnalSaldoAwal.FirstOrDefault(item2 => item2.TBJurnalDetails.FirstOrDefault().IDAkun == item.IDAkun).IDTempat == pengguna.IDTempat ?
                                                                                item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ?
                                                                                Math.Abs(DataJurnalSaldoAwal.FirstOrDefault(item2 => item2.TBJurnalDetails.FirstOrDefault().IDAkun == item.IDAkun).TBJurnalDetails.FirstOrDefault().Kredit.Value) :
                                                                                DataJurnalSaldoAwal.FirstOrDefault(item2 => item2.TBJurnalDetails.FirstOrDefault().IDAkun == item.IDAkun).TBJurnalDetails.FirstOrDefault().Debit.Value :
                                                                                0 : 0, /*: 0,*/

                    StatusSaldoAwal = DataJurnalSaldoAwal.FirstOrDefault(item2 => item2.TBJurnalDetails.FirstOrDefault().IDAkun == item.IDAkun) == null ? true : false

                });

                urutan++;
            }
        }

    }

    private class ListAkun
    {
        public TBAkunGrup TBAkunGrup { get; set; }
        public int IDAkun { get; set; }
        public string Nomor { get; set; }
        public string Kode { get; set; }
        public bool Grup { get; set; }
        public bool StatusParent { get; set; }
        public string ClassWarna { get; set; }
        public string Nama { get; set; }
        public decimal Nominal { get; set; }
        public bool StatusSaldoAwal { get; set; }
    }

    #region Neraca versi Simple
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
                Nominal = HitungSaldo(item.IDAkunGrup.ToString(), DropDownListBulan.SelectedItem.Value.ToString(), DropDownListTahun.SelectedItem.Text),
                StatusParent = item.IDAkunGrupParent == null ? true : false
            });

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
    private List<ListAkun> LoadNeracaAktiva(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        decimal TotalSaldoAwalAktiva = 0;
     
        List<ListAkun> listAkun = new List<ListAkun>();
        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva) &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup2("1", result, listAkun);
             
        TotalSaldoAwalAktiva = (Akuntansi_Class.HitungSaldo(db.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (DropDownListBulan.SelectedItem.Value).ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (DropDownListTahun.SelectedItem.Value).ToInt() &&
                                                                            item2.TBJurnal.Keterangan.Contains("#SaldoAwal") &&
                                                                            item2.TBAkun.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva &&
                                                                             item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false));

        RepeaterLaporanAktiva.DataSource = listAkun;
        RepeaterLaporanAktiva.DataBind();

        LabelTotalSaldoAktiva.Text = (TotalSaldoAwalAktiva).ToFormatHarga();


        return listAkun;
    }

    private List<ListAkun> LoadNeracaPasiva(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        decimal TotalSaldoAwalPasiva = 0;
        decimal LabaRugiBerjalan = 0;
        decimal LabaRugiBulanSebelumnya = 0;

        List<ListAkun> listAkun = new List<ListAkun>();
        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva) &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup2("1", result, listAkun);

        var dataBulanIni = Akuntansi_Class.LaporanLabaRugi(DropDownListBulan.SelectedItem.Value, DropDownListTahun.SelectedItem.Value, false, pengguna, "");
        var dataBulanSebelumnya = Akuntansi_Class.LaporanLabaRugi(((DropDownListBulan.SelectedItem.Value).ToInt() - 1).ToString(), DropDownListTahun.SelectedItem.Value, false, pengguna, "");

        LabaRugiBerjalan = dataBulanIni["TotalLabaRugi"];
        LabaRugiBulanSebelumnya = dataBulanSebelumnya["TotalLabaRugi"];

        TotalSaldoAwalPasiva = Math.Abs((Akuntansi_Class.HitungSaldo(db.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (DropDownListBulan.SelectedItem.Value).ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (DropDownListTahun.SelectedItem.Value).ToInt() &&
                                                                            item2.TBJurnal.Keterangan.Contains("#SaldoAwal") &&
                                                                            item2.TBAkun.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva 
                                                                            && item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)));

        LabelLabaRugiBulanBerjalan.Text = (LabaRugiBerjalan).ToFormatHarga();
        LabelLabaRugiBulanSebelumnya.Text = (LabaRugiBulanSebelumnya).ToFormatHarga();

        RepeaterLaporanPasiva.DataSource = listAkun;
        RepeaterLaporanPasiva.DataBind();

        LabelTotalSaldoPasiva.Text = (TotalSaldoAwalPasiva).ToFormatHarga();

        return listAkun;
    }

    private decimal HitungSaldo(string _idAkunGrup, string bulan, string tahun)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            decimal saldo = 0;
            var akunGrup = db.TBAkunGrups.FirstOrDefault(item => item.IDAkunGrup == (_idAkunGrup).ToInt());

            foreach (var item in akunGrup.TBAkuns.Where(item2 => item2.IDTempat == pengguna.IDTempat))
            {
                saldo += (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                        item2.TBJurnal.Keterangan.Contains("#SaldoAwal") &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false) < 0 &&
                                                                         item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ?

                                                                   Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() && item2.TBJurnal.Keterangan.Contains("#SaldoAwal") &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)) :

                                                                         (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() && item2.TBJurnal.Keterangan.Contains("#SaldoAwal") &&
                                                                            item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)));
            }
            return saldo;

        }
    }

    #endregion

}