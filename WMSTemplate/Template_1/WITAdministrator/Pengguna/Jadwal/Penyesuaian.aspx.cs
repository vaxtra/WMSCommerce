using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITHumanCapitalManagement_Schedulling_Penyesuaian : System.Web.UI.Page
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
            }
        }
    }
    protected void DropDownListJenisSchedulling_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ButtonJanuari_Click(object sender, EventArgs e)
    {
        LoadData("1");
    }
    protected void ButtonFebruari_Click(object sender, EventArgs e)
    {
        LoadData("2");
    }
    protected void ButtonMaret_Click(object sender, EventArgs e)
    {
        LoadData("3");
    }
    protected void ButtonApril_Click(object sender, EventArgs e)
    {
        LoadData("4");
    }
    protected void ButtonMei_Click(object sender, EventArgs e)
    {
        LoadData("5");
    }
    protected void ButtonJuni_Click(object sender, EventArgs e)
    {
        LoadData("6");
    }
    protected void ButtonJuli_Click(object sender, EventArgs e)
    {
        LoadData("7");
    }
    protected void ButtonAgustus_Click(object sender, EventArgs e)
    {
        LoadData("8");
    }
    protected void ButtonSeptember_Click(object sender, EventArgs e)
    {
        LoadData("9");
    }
    protected void ButtonOktober_Click(object sender, EventArgs e)
    {
        LoadData("10");
    }
    protected void ButtonNopember_Click(object sender, EventArgs e)
    {
        LoadData("11");
    }
    protected void ButtonDesember_Click(object sender, EventArgs e)
    {
        LoadData("12");
    }
    protected void LoadData(string bulan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            HiddenFieldIDBulan.Value = bulan;
            LabelHeader.Text = "PENYESUAIAN PRESENSI (" + new DateTime(DropDownListTahun.SelectedValue.ToInt(), bulan.ToInt(), 1).Date.ToString("MMMM").ToUpper() + ")";
            Dictionary<int, string[]> ListData = new Dictionary<int, string[]>();

            int JumlahHari = DateTime.DaysInMonth(int.Parse(DropDownListTahun.SelectedItem.Value), int.Parse(bulan));

            //CARI DI DATABASE
            var ListSchedulling = db.TBPenggunaLogKehadirans
                .Where(item =>
                    item.IDPengguna == int.Parse(DropDownListPegawai.SelectedItem.Value) &&
                    item.JamMasuk.Value.Year == new DateTime(DropDownListTahun.SelectedValue.ToInt(), bulan.ToInt(), 1).Year &&
                    item.JamKeluar.Value.Month == new DateTime(DropDownListTahun.SelectedValue.ToInt(), bulan.ToInt(), 1).Month)
                .Select(item => new
                {
                    Hari = item.JamMasuk.Value.Day,
                    item.JamMasuk,
                    item.JamKeluar,
                    Keterangan = item.Keterangan
                });

            MultiView.Visible = true;
            MultiView.SetActiveView(ScheduleHarian);

            for (int i = 1; i <= JumlahHari; i++)
            {
                string[] dataSchedulling = new string[3];

                var Schedule = ListSchedulling.FirstOrDefault(item => item.Hari == i);

                if (Schedule != null)
                {
                    dataSchedulling[0] = Schedule.JamMasuk.Value.ToString("HH:mm");
                    dataSchedulling[1] = Schedule.JamKeluar.Value.ToString("HH:mm");
                    dataSchedulling[2] = Schedule.Keterangan;
                    ListData.Add(i, dataSchedulling);
                }
                else
                    ListData.Add(i, dataSchedulling);
            }

            RepeaterSchedule1.DataSource = ListData.Take(10);
            RepeaterSchedule1.DataBind();

            RepeaterSchedule2.DataSource = ListData.Skip(10).Take(10);
            RepeaterSchedule2.DataBind();

            RepeaterSchedule3.DataSource = ListData.Skip(20);
            RepeaterSchedule3.DataBind();
        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            foreach (RepeaterItem item in RepeaterSchedule1.Items)
            {
                InsertData(db, item);
            }

            foreach (RepeaterItem item in RepeaterSchedule2.Items)
            {
                InsertData(db, item);
            }

            foreach (RepeaterItem item in RepeaterSchedule3.Items)
            {
                InsertData(db, item);
            }

            db.SubmitChanges();
        }
    }

    private void InsertData(DataClassesDatabaseDataContext db, RepeaterItem item)
    {
        TextBox TextBoxJamMasuk = (TextBox)item.FindControl("TextBoxJamMasuk");
        TextBox TextBoxKeterangan = (TextBox)item.FindControl("TextBoxKeterangan");
        TextBox TextBoxJamKeluar = (TextBox)item.FindControl("TextBoxJamKeluar");
        Label LabelHiddenTanggal = (Label)item.FindControl("LabelHiddenTanggal");
        Label LabelNamaHari = (Label)item.FindControl("LabelNamaHari");


        if (!String.IsNullOrWhiteSpace(TextBoxJamMasuk.Text) && !String.IsNullOrWhiteSpace(TextBoxJamKeluar.Text))
        {
            Label LabelHari = (Label)item.FindControl("LabelHari");

            string TESTANJING = LabelHiddenTanggal.Text + " " + TextBoxJamMasuk.Text;

            InsertSatuan(db,
                LabelHari.Text.ToInt(),
                DateTime.ParseExact(LabelHiddenTanggal.Text + " " + TextBoxJamMasuk.Text, "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None),
                DateTime.ParseExact(LabelHiddenTanggal.Text + " " + TextBoxJamKeluar.Text, "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None), TextBoxKeterangan.Text);
        }
    }

    private void InsertSatuan(DataClassesDatabaseDataContext db, int hari, DateTime JamMasuk, DateTime JamKeluar, string keterangan)
    {
        TBPenggunaLogKehadiran JamKerja = db.TBPenggunaLogKehadirans
            .FirstOrDefault(item2 =>
                item2.IDPengguna == DropDownListPegawai.SelectedItem.Value.ToInt() &&
                item2.JamMasuk.Value.Date == new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), hari).Date);

        if (JamKerja == null)
        {
            db.TBPenggunaLogKehadirans.InsertOnSubmit(new TBPenggunaLogKehadiran
            {
                IDPengguna = int.Parse(DropDownListPegawai.SelectedItem.Value),
                JadwalJamMasuk = DateTime.ParseExact(DateTime.Now.Date.ToString("MM/dd/yyyy") + " " + db.TBPenggunaJadwals.FirstOrDefault(item => item.NamaHari == (DateTime.Now.ToString("dddd", new CultureInfo("id-ID")).ToString())).JadwalJamMasuk.Value.ToString("HH:mm"), "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None),
                JamMasuk = JamMasuk,
                JadwalJamKeluar = DateTime.ParseExact(DateTime.Now.Date.ToString("MM/dd/yyyy") + " " + db.TBPenggunaJadwals.FirstOrDefault(item => item.NamaHari == (DateTime.Now.ToString("dddd", new CultureInfo("id-ID")).ToString())).JadwalJamKeluar.Value.ToString("HH:mm"), "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None),
                JamKeluar = JamKeluar,
                Keterangan = keterangan
            });
        }
        else
        {
            JamKerja.JamMasuk = JamMasuk;
            JamKerja.JamKeluar = JamKeluar;
            JamKerja.Keterangan = keterangan;
        }
    }
}