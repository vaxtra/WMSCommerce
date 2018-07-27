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
                item.TanggalSaldoAwal.Value.Date == DateTime.Parse(int.Parse(DropDownListBulan.SelectedItem.Value) + "/" + "01" + "/" + DropDownListTahun.Text));
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
                        if (SaldoAwalAktiva.Text.ToDecimal() > 0)
                        {
                            TBJurnal Jurnal = NewJurnal(Pengguna);
                            TBAkunSaldoAwal AkunSaldoAwal = NewAkunSaldoAwal(IDAkun.Value.ToInt(), Pengguna.IDPengguna, DateTime.Parse(int.Parse(DropDownListBulan.SelectedItem.Value) + "/" + "01" + "/" + DropDownListTahun.Text));

                        var Data = db.TBAkuns.FirstOrDefault(item2 => item2.IDAkun == int.Parse(IDAkun.Value));

                        if (Data.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Debit)
                        {
                            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = int.Parse(IDAkun.Value),
                                Debit = SaldoAwalAktiva.Text.ToDecimal(),
                                Kredit = 0
                            });

                            ListJurnal.Add(Jurnal);
                            ListAkunSaldoAwal.Add(AkunSaldoAwal);
                        }
                        else if (Data.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit)
                        {
                            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = int.Parse(IDAkun.Value),
                                Debit = 0,
                                Kredit = SaldoAwalAktiva.Text.ToDecimal()
                            });

                            ListJurnal.Add(Jurnal);
                            ListAkunSaldoAwal.Add(AkunSaldoAwal);
                        }

                        }
                    }
                }

            //foreach (RepeaterItem item in RepeaterPasiva.Items)
            //{
            //    TextBox SaldoAwalPasiva = (TextBox)item.FindControl("TextBoxNominalSaldoAwalPasiva");
            //    HiddenField IDAkun = (HiddenField)item.FindControl("HiddenFieldIDAkun");

            //    if (DataSaldoAwal.FirstOrDefault(data => data.IDAkun == IDAkun.Value.ToInt()) != null)
            //    {
            //        peringatan.Visible = true;
            //        ListSaldoAwalSudahAda.Add("Saldo Awal " + DataAkun.FirstOrDefault(item2 => item2.IDAkun == IDAkun.Value.ToInt()).Nama + " sudah ada pada system");
            //    }
            //    else
            //    {
            //        if (SaldoAwalPasiva.Text.ToDecimal() > 0)
            //        {
            //            TBJurnal Jurnal = NewJurnal(Pengguna);
            //            TBAkunSaldoAwal AkunSaldoAwal = NewAkunSaldoAwal(IDAkun.Value), Pengguna.IDPengguna, DateTime.Parse(DropDownListBulan.SelectedItem.Value.ToInt() + "/" + "01" + "/" + DropDownListTahun.Text));

            //            Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
            //            {
            //                IDAkun = int.Parse(IDAkun.Value),
            //                Debit = 0,
            //                Kredit = SaldoAwalPasiva.Text.ToDecimal()
            //            });

            //            ListJurnal.Add(Jurnal);
            //            ListAkunSaldoAwal.Add(AkunSaldoAwal);
            //        }
            //    }
            //}

            for (int i = 0; i < ListSaldoAwalSudahAda.Count; i++)
                {
                    LiteralWarning.Text += ListSaldoAwalSudahAda.ElementAt(i).ToString() + "<br/>";
                }

                db.TBAkunSaldoAwals.InsertAllOnSubmit(ListAkunSaldoAwal);
                db.TBJurnals.InsertAllOnSubmit(ListJurnal);
                db.SubmitChanges();
            }
    }

    private TBJurnal NewJurnal(PenggunaLogin Pengguna)
    {
        return new TBJurnal
        {
            Tanggal = DateTime.Parse(int.Parse(DropDownListBulan.SelectedItem.Value) + "/" + "01" + "/" + DropDownListTahun.Text),

            Keterangan = "#SaldoAwal " +
            DateTime.Parse(DropDownListBulan.SelectedItem.Value + "/" + "01" + "/" +
            DropDownListTahun.Text).ToString("MMMM", new CultureInfo("id-ID")) + " - " +
            Pengguna.NamaLengkap + " (" +
            DateTime.Now.ToLongTimeString() + ")",

            IDPengguna = Pengguna.IDPengguna,
            Referensi = ""
        };
    }

    private TBAkunSaldoAwal NewAkunSaldoAwal(int _IDAkun, int _IDPengguna, DateTime _TanggalSaldoAwal)
    {
        return new TBAkunSaldoAwal
        {
            IDAkun = _IDAkun,
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
        int urutan = 1;
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
                Nominal = (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() - 1 &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt()).ToArray(), false) < 0 && item.TBAkunGrup.EnumSaldoNormal == (int)PilihanDebitKredit.Kredit ?

                                                                   Math.Abs(Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                    .Where(item2 =>
                                                                    item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() - 1 &&
                                                                        item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt()).ToArray(), false)) :

                                                                        (Akuntansi_Class.HitungSaldo(item.TBJurnalDetails
                                                                        .Where(item2 =>
                                                                        item2.TBJurnal.Tanggal.Value.Month == bulan.ToInt() - 1 &&
                                                                            item2.TBJurnal.Tanggal.Value.Year == tahun.ToInt()).ToArray(), false)))
            });

            urutan++;
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
    }

}