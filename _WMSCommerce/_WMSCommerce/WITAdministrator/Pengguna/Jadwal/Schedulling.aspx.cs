using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITHumanCapitalManagement_Schedulling_Schedulling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);

                DropDownListPegawai.DataSource = ClassPengguna.Data();
                DropDownListPegawai.DataTextField = "NamaLengkap";
                DropDownListPegawai.DataValueField = "IDPengguna";
                DropDownListPegawai.DataBind();
                DropDownListPegawai.Items.Insert(0, new ListItem { Value = "0", Text = "Pilih Pegawai" });
            }

            LoadData();
        }
    }
    protected void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //HiddenFieldIDBulan.Value = bulan;
            Dictionary<int, string[]> ListData = new Dictionary<int, string[]>();

            ////////int JumlahHari = DateTime.DaysInMonth(int.Parse(DropDownListTahun.SelectedItem.Value), int.Parse(bulan));

            ////////CARI DI DATABASE
            ////////var ListSchedulling = Database.db.TBPenggunaJadwals
            ////////    .Where(item =>
            ////////        item.IDPengguna == DropDownListPegawai.SelectedItem.Value.ToInt() &&
            ////////        item.JamMasuk.Value.Year == new DateTime(DropDownListTahun.SelectedValue.ToInt(), bulan.ToInt(), 1).Year &&
            ////////        item.JamKeluar.Value.Month == new DateTime(DropDownListTahun.SelectedValue.ToInt(), bulan, 1).Month)
            ////////    .Select(item => new
            ////////    {
            ////////        Hari = item.JamMasuk.Value.Day,
            ////////        item.JamMasuk,
            ////////        item.JamKeluar
            ////////    });

            var ListSchedulling = db.TBPenggunaJadwals
                .Where(item =>
                    item.IDPengguna == int.Parse(DropDownListPegawai.SelectedItem.Value))
                .Select(item => new
                {
                    NamaHari = item.NamaHari,
                    Hari = item.JadwalJamMasuk.Value.Day,
                    item.JadwalJamMasuk,
                    item.JadwalJamKeluar,
                    Keterangan = item.Keterangan
                });

            //////if (DropDownListJenisSchedulling.SelectedItem.Text == "Tetap")
            //////{
            MultiView.Visible = true;
            MultiView.SetActiveView(ScheduleBulanan);

            var indonesia = new CultureInfo("ID-id");
            var info = indonesia.DateTimeFormat;

            for (int i = 1; i <= 7; i++)
            {
                string[] dataSchedulling = new string[4];

                var Schedule = ListSchedulling.FirstOrDefault(item => item.NamaHari == info.DayNames[i - 1]);

                if (Schedule != null)
                {
                    dataSchedulling[0] = Schedule.JadwalJamMasuk.Value.ToString("HH:mm");
                    dataSchedulling[1] = Schedule.JadwalJamKeluar.Value.ToString("HH:mm");
                    dataSchedulling[2] = info.DayNames[i - 1];
                    dataSchedulling[3] = Schedule.Keterangan;
                    ListData.Add(i, dataSchedulling);
                }
                else
                {
                    dataSchedulling[2] = info.DayNames[i - 1];
                    ListData.Add(i, dataSchedulling);
                }
            }

            RepeaterScheduleTetap.DataSource = ListData.Take(7);
            RepeaterScheduleTetap.DataBind();

            //////}
            //////else
            //////{
            //////    //LabelHeader.Text = "SCHEDULLING (" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), bulan.ToInt(), 1).Date.ToString("MMMM").ToUpper() + ")";

            //////    MultiView.Visible = true;
            //////    MultiView.SetActiveView(ScheduleHarian);

            //////    for (int i = 1; i <= JumlahHari; i++)
            //////    {
            //////        string[] dataSchedulling = new string[2];


            //////        var Schedule = ListSchedulling.FirstOrDefault(item => item.Hari == i);

            //////        if (Schedule != null)
            //////        {
            //////            dataSchedulling[0] = Schedule.JamMasuk.Value.ToString("HH:mm");
            //////            dataSchedulling[1] = Schedule.JamKeluar.Value.ToString("HH:mm");
            //////            ListData.Add(i, dataSchedulling);
            //////        }
            //////        else
            //////        {
            //////            ListData.Add(i, dataSchedulling);
            //////        }
            //////    }

            //////    RepeaterSchedule1.DataSource = ListData.Take(10);
            //////    RepeaterSchedule1.DataBind();

            //////    RepeaterSchedule2.DataSource = ListData.Skip(10).Take(10);
            //////    RepeaterSchedule2.DataBind();

            //////    RepeaterSchedule3.DataSource = ListData.Skip(20);
            //////    RepeaterSchedule3.DataBind();
            //////}
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region Untuk Jadwal Dinamis
            ////////if (DropDownListJenisSchedulling.SelectedItem.Text == "Harian")
            ////////{
            ////////    foreach (RepeaterItem item in RepeaterSchedule1.Items)
            ////////    {
            ////////        InsertData(db, item);
            ////////    }

            ////////    foreach (RepeaterItem item in RepeaterSchedule2.Items)
            ////////    {
            ////////        InsertData(db, item);
            ////////    }

            ////////    foreach (RepeaterItem item in RepeaterSchedule3.Items)
            ////////    {
            ////////        InsertData(db, item);
            ////////    }
            ////////}
            ////////else
            ////////{     
            ////////}

            #endregion

            foreach (RepeaterItem item in RepeaterScheduleTetap.Items)
            {
                InsertData(db, item);
            }
            db.SubmitChanges();
        }
    }
    private void InsertData(DataClassesDatabaseDataContext db, RepeaterItem item)
    {
        TextBox TextBoxJamMasuk = (TextBox)item.FindControl("TextBoxJamMasuk");
        TextBox TextBoxJamKeluar = (TextBox)item.FindControl("TextBoxJamKeluar");
        TextBox TextBoxKeterangan = (TextBox)item.FindControl("TextBoxKeterangan");
        Label LabelHiddenTanggal = (Label)item.FindControl("LabelHiddenTanggal");
        Label LabelNamaHari = (Label)item.FindControl("LabelNamaHari");


        if (!String.IsNullOrWhiteSpace(TextBoxJamMasuk.Text) && !String.IsNullOrWhiteSpace(TextBoxJamKeluar.Text))
        {
            Label LabelHari = (Label)item.FindControl("LabelHari");

            //string TESTANJING = LabelHiddenTanggal.Text + " " + TextBoxJamMasuk.Text;

            InsertSatuan(db,
            LabelNamaHari.Text,
            LabelHari.Text.ToInt(),
            DateTime.ParseExact("01/01/1990" + " " + TextBoxJamMasuk.Text, "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None),
            DateTime.ParseExact("01/01/1990" + " " + TextBoxJamKeluar.Text, "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None), TextBoxKeterangan.Text);

            #region Untuk Jadwal Dinamis
            //////InsertSatuan(db,
            //////    LabelNamaHari.Text,
            //////    LabelHari.Text.ToInt(),
            //////    DateTime.ParseExact(LabelHiddenTanggal.Text + " " + TextBoxJamMasuk.Text, "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None),
            //////    DateTime.ParseExact(LabelHiddenTanggal.Text + " " + TextBoxJamKeluar.Text, "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None));       
            #endregion
        }
    }

    private void InsertSatuan(DataClassesDatabaseDataContext db, string namaHari, int tanggal, DateTime JamMasuk, DateTime JamKeluar, string keterangan)
    {
        TBPenggunaJadwal jadwalKerja = db.TBPenggunaJadwals
            .FirstOrDefault(item2 =>
                item2.IDPengguna == DropDownListPegawai.SelectedItem.Value.ToInt() &&
                item2.JadwalJamMasuk.Value.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, tanggal).Date);

        if (jadwalKerja == null)
        {
            db.TBPenggunaJadwals.InsertOnSubmit(new TBPenggunaJadwal
            {
                IDPengguna = int.Parse(DropDownListPegawai.SelectedItem.Value),
                NamaHari = namaHari,
                JadwalJamMasuk = JamMasuk,
                JadwalJamKeluar = JamKeluar,
                Keterangan = keterangan
            });
        }
        else
        {
            jadwalKerja.NamaHari = namaHari;
            jadwalKerja.JadwalJamMasuk = JamMasuk;
            jadwalKerja.JadwalJamKeluar = JamKeluar;
            jadwalKerja.Keterangan = keterangan;
        }
    }
    protected void ButtonSendEmail_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var PenerimaPesan = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == DropDownListPegawai.SelectedItem.Value.ToInt());

            TBPengirimanEmail PengirimanEmail = new TBPengirimanEmail
            {
                //IDPengirimanEmail
                TanggalKirim = DateTime.Now,
                Tujuan = PenerimaPesan.Email,
                Judul = "Jadwal Kerja " + PenerimaPesan.NamaLengkap,
                //Isi
            };

            PengirimanEmail.Isi = "<table><tr><th>Hari</th><th>Jam Masuk</th><th>Jam Keluar</th><tr>";

            foreach (RepeaterItem item in RepeaterScheduleTetap.Items)
            {
                TextBox TextBoxJamMasuk = (TextBox)item.FindControl("TextBoxJamMasuk");
                TextBox TextBoxJamKeluar = (TextBox)item.FindControl("TextBoxJamKeluar");
                TextBox TextBoxKeterangan = (TextBox)item.FindControl("TextBoxKeterangan");
                Label LabelHiddenTanggal = (Label)item.FindControl("LabelHiddenTanggal");
                Label LabelNamaHari = (Label)item.FindControl("LabelNamaHari");

                PengirimanEmail.Isi += "<tr><td>" + LabelNamaHari.Text + "</td>" + "<td>" + TextBoxJamMasuk.Text + "</td>" + "<td>" + TextBoxJamKeluar.Text + "</td></tr>";
            }

            PengirimanEmail.Isi += "</table>";

            db.TBPengirimanEmails.InsertOnSubmit(PengirimanEmail);
            db.SubmitChanges();
        }
    }

    #region Untuk Jadwal Dinamis
    //protected void ButtonJanuari_Click(object sender, EventArgs e)
    //{
    //    LoadData("1");
    //}
    //protected void ButtonFebruari_Click(object sender, EventArgs e)
    //{
    //    LoadData("2");
    //}
    //protected void ButtonMaret_Click(object sender, EventArgs e)
    //{
    //    LoadData("3");
    //}
    //protected void ButtonApril_Click(object sender, EventArgs e)
    //{
    //    LoadData("4");
    //}
    //protected void ButtonMei_Click(object sender, EventArgs e)
    //{
    //    LoadData("5");
    //}
    //protected void ButtonJuni_Click(object sender, EventArgs e)
    //{
    //    LoadData("6");
    //}
    //protected void ButtonJuli_Click(object sender, EventArgs e)
    //{
    //    LoadData("7");
    //}
    //protected void ButtonAgustus_Click(object sender, EventArgs e)
    //{
    //    LoadData("8");
    //}
    //protected void ButtonSeptember_Click(object sender, EventArgs e)
    //{
    //    LoadData("9");
    //}
    //protected void ButtonOktober_Click(object sender, EventArgs e)
    //{
    //    LoadData("10");
    //}
    //protected void ButtonNopember_Click(object sender, EventArgs e)
    //{
    //    LoadData("11");
    //}
    //protected void ButtonDesember_Click(object sender, EventArgs e)
    //{
    //    LoadData("12");
    //}  
    #endregion

    protected void DropDownListPegawai_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}