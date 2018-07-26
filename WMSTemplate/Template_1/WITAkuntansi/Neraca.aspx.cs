using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_Neraca : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                #region LOAD DROPDOWN
                DropDownListBulan.Items.Clear();
                DropDownListBulan.Items.AddRange(Akuntansi_Class.DropdownlistBulanLaporan());
                DropDownListBulan.SelectedValue = DateTime.Now.Month.ToString();

                DropDownListTahun.Items.Clear();
                DropDownListTahun.Items.AddRange(Akuntansi_Class.DropdownlistTahunLaporan());
                DropDownListTahun.SelectedValue = DateTime.Now.Year.ToString();
                #endregion

                LoadNeraca(db);
                LoadNeracaAktiva(db);
                LoadNeracaPasiva(db);
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

        //CARI AKUN GRUP
        CariAkunGrup("1", result, listAkun);

        var dataBulanIni = Akuntansi_Class.LaporanLabaRugi(DropDownListBulan.SelectedItem.Value, DropDownListTahun.SelectedItem.Value, false, pengguna, "");
        var dataBulanSebelumnya = Akuntansi_Class.LaporanLabaRugi(((DropDownListBulan.SelectedItem.Value).ToInt()-1).ToString(), DropDownListTahun.SelectedItem.Value, false, pengguna, "");


        LabaRugiBerjalan = dataBulanIni["TotalLabaRugi"];
        LabaRugiBulanSebelumnya = dataBulanSebelumnya["TotalLabaRugi"];

        TotalAktiva = listAkun.Where(item => item.Grup == false).Where(item => item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva).Sum(item2 => item2.Nominal);
        TotalPasiva = listAkun.Where(item => item.Grup == false).Where(item => item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva).Sum(item2 => item2.Nominal) + LabaRugiBerjalan +
            LabaRugiBulanSebelumnya;

        LabelLabaRugiBulanBerjalan.Text = (LabaRugiBerjalan).ToFormatHarga();
        LabelLabaRugiBulanSebelumnya.Text = (LabaRugiBulanSebelumnya).ToFormatHarga();

        LabelTotalSaldoAktiva.Text = (TotalAktiva).ToFormatHarga();
        LabelTotalSaldoPasiva.Text = (TotalPasiva).ToFormatHarga();

        RepeaterLaporan.DataSource = listAkun;
        RepeaterLaporan.DataBind();
        ButtonPrint.OnClientClick = "return popitup('NeracaPrint.aspx" + "?Bulan=" + DropDownListBulan.SelectedItem.Value + "&Tahun=" + DropDownListTahun.SelectedItem.Text + "')";

        return listAkun;
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
                Nominal = 0,
                StatusGeneralLedger = false

            });

            #region CARI AKUN
            if (item.TBAkuns.Count > 0)
            {
                //CARI AKUN
                CariAkun(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item, listAkun, DropDownListBulan.SelectedItem.Value, DropDownListTahun.SelectedItem.Text);
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
        //BANU, disini orderby
        foreach (var item in akunGrup.TBAkuns.OrderBy(item=> item.Kode))
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
                                                                    item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                          item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false) < 0 && item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit && item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva ?

                                                                   Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                          item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)) :

                                                                        (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                          item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false))),
                GeneralLedger = "return popitup('/WITAkuntansi/BukuBesar.aspx?id=" + item.IDAkun + "&month=" + DropDownListBulan.SelectedItem.Value + "')",
                StatusGeneralLedger = true
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
        public bool StatusGeneralLedger { get; set; }
        public bool StatusParent { get; set; }
        public string ClassWarna { get; set; }
        public string Nama { get; set; }
        public decimal Nominal { get; set; }
        public string GeneralLedger { get; set; }
    }


    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadNeraca(db);
            LoadNeracaAktiva(db);
            LoadNeracaPasiva(db);
            LinkDownload.Visible = false;
            ButtonExcel.Visible = true;
        }
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Excel_Class Excel_Class = new Excel_Class(pengguna, "Neraca", DropDownListBulan.SelectedItem.Text + " " + DropDownListTahun.SelectedItem.Text, 4);
            var result = new Excel_Class(pengguna, "Neraca", DropDownListBulan.SelectedItem.Text + " " + DropDownListTahun.SelectedItem.Text, 4);
            ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            Excel_Class.Header(1, "No");
            Excel_Class.Header(2, "Kode");
            Excel_Class.Header(3, "Nama");
            Excel_Class.Header(4, "Nominal");

            Excel_Class.SetBackground(1, 1, Color.Black);
            Excel_Class.SetBackground(1, 2, Color.Black);
            Excel_Class.SetBackground(1, 3, Color.Black);
            Excel_Class.SetBackground(1, 4, Color.Black);
            Excel_Class.SetColor(1, 1, Color.White);
            Excel_Class.SetColor(1, 2, Color.White);
            Excel_Class.SetColor(1, 3, Color.White);
            Excel_Class.SetColor(1, 4, Color.White);

            int index = 2;

            foreach (var item in LoadNeraca(db))
            {
                if (item.TBAkunGrup == null)
                {
                    Excel_Class.SetBackground(index, 1, Color.LightGreen);
                    Excel_Class.SetBackground(index, 2, Color.LightGreen);
                    Excel_Class.SetBackground(index, 3, Color.LightGreen);
                    Excel_Class.SetBackground(index, 4, Color.LightGreen);
                    Excel_Class.SetColor(index, 1, Color.Black);
                    Excel_Class.SetColor(index, 2, Color.Black);
                    Excel_Class.SetColor(index, 3, Color.Black);
                    Excel_Class.SetColor(index, 4, Color.Black);
                }
                else
                {
                    if (item.Grup == true)
                    {
                        Excel_Class.SetBackground(index, 1, Color.LightYellow);
                        Excel_Class.SetBackground(index, 2, Color.LightYellow);
                        Excel_Class.SetBackground(index, 3, Color.LightYellow);
                        Excel_Class.SetBackground(index, 4, Color.LightYellow);
                        Excel_Class.SetColor(index, 1, Color.Black);
                        Excel_Class.SetColor(index, 2, Color.Black);
                        Excel_Class.SetColor(index, 3, Color.Black);
                        Excel_Class.SetColor(index, 4, Color.Black);
                    }
                }
                Excel_Class.Content(index, 1, item.Nomor.Replace("&nbsp", ""));
                Excel_Class.Content(index, 2, item.Grup == true ? string.Empty : item.Kode);
                Excel_Class.Content(index, 3, item.Nama);
                Excel_Class.Content(index, 4, item.Grup == true ? string.Empty : item.Nominal.ToString());

                index++;
            }

            List<ListAkun> listAkun = new List<ListAkun>();
            var result2 = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null &&
            (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

            //CARI AKUN GRUP
            CariAkunGrup("1", result2, listAkun);

            var dataBulanIni = Akuntansi_Class.LaporanLabaRugi(DropDownListBulan.SelectedItem.Value, DropDownListTahun.SelectedItem.Value, false, pengguna, "");
            var dataBulanSebelumnya = Akuntansi_Class.LaporanLabaRugi(((DropDownListBulan.SelectedItem.Value).ToInt() - 1).ToString(), DropDownListTahun.SelectedItem.Value, false, pengguna, "");


            decimal LabaRugiBerjalan = dataBulanIni["TotalLabaRugi"];
            decimal LabaRugiBulanSebelumnya = dataBulanSebelumnya["TotalLabaRugi"];

            Excel_Class.Content(index, 1, "Laba Rugi Bulan Berjalan");
            Excel_Class.Content(index, 2, "");
            Excel_Class.Content(index, 3, "");
            Excel_Class.Content(index, 4, LabaRugiBerjalan);

            Excel_Class.Content(index+1, 1, "Laba Rugi Bulan Sebelumnya");
            Excel_Class.Content(index+1, 2, "");
            Excel_Class.Content(index+1, 3, "");
            Excel_Class.Content(index+1, 4, LabaRugiBulanSebelumnya);

            Excel_Class.Save();

            LinkDownload.Visible = true;
            ButtonExcel.Visible = false;
            if (LinkDownload.Visible)
                LinkDownload.HRef = Excel_Class.LinkDownload;
        }
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
        List<ListAkun> listAkun = new List<ListAkun>();
        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva) &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup2("1", result, listAkun);


        RepeaterLaporanAktiva.DataSource = listAkun;
        RepeaterLaporanAktiva.DataBind();

        ButtonPrint.OnClientClick = "return popitup('NeracaPrint.aspx" + "?Bulan=" + DropDownListBulan.SelectedItem.Value + "&Tahun=" + DropDownListTahun.SelectedItem.Text + "')";

        return listAkun;
    }

    private List<ListAkun> LoadNeracaPasiva(DataClassesDatabaseDataContext db)
    {
        List<ListAkun> listAkun = new List<ListAkun>();
        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva) &&
        (item.IDAkunGrup != 4 && item.IDAkunGrup != 5)).ToArray();

        decimal TotalPasiva = 0;

        //CARI AKUN GRUP
        CariAkunGrup2("1", result, listAkun);

        RepeaterLaporanPasiva.DataSource = listAkun;
        RepeaterLaporanPasiva.DataBind();

        ButtonPrint.OnClientClick = "return popitup('NeracaPrint.aspx" + "?Bulan=" + DropDownListBulan.SelectedItem.Value + "&Tahun=" + DropDownListTahun.SelectedItem.Text + "')";

        return listAkun;
    }

    private decimal HitungSaldo(string _idAkunGrup, string bulan, string tahun)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            decimal saldo = 0;
            var akunGrup = db.TBAkunGrups.FirstOrDefault(item => item.IDAkunGrup == (_idAkunGrup).ToInt());

            foreach (var item in akunGrup.TBAkuns)
            {
                saldo += (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                          item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false) < 0 && item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit && item.TBAkunGrup.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva ?

                                                                   Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                          item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)) :

                                                                         (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == (bulan).ToInt() &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == (tahun).ToInt() &&
                                                                          item2.TBJurnal.IDTempat == pengguna.IDTempat).ToArray(), false)));
            }
            return saldo;

        }
    }

    #endregion
}