using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITHumanCapitalManagement_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);

                var DataPegawaiDB = ClassPengguna.Data();

                RepeaterDataPegawai.DataSource = DataPegawaiDB;
                RepeaterDataPegawai.DataBind();

                LoadDataRecentClockInOut();

                DropDownListPegawai.DataSource = DataPegawaiDB;
                DropDownListPegawai.DataTextField = "NamaLengkap";
                DropDownListPegawai.DataValueField = "IDPengguna";
                DropDownListPegawai.DataBind();

                DropDownListPegawai.Items.Insert(0, new ListItem { Value = "0", Text = "Pilih Pegawai" });
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderClockInOut.Show();
    }
    protected void RepeaterDataPegawai_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataPegawaiDB = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna.ToString() == e.CommandArgument.ToString());

                LabelUsername.Text = DataPegawaiDB.NamaLengkap;
                LabelWaktuClockInOut.Text = DateTime.Now.ToShortTimeString();
                HiddenFieldIDPengguna.Value = DataPegawaiDB.IDPengguna.ToString();
            }
        }
    }
    protected void ButtonDone_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = new PenggunaLogin(TextBoxUsername.Text, TextBoxPassword.Text);

            //Ketika Login Berhasil
            if (Pengguna.IDPengguna > 0)
            {
                //Cek action terakhir pegawai, clock in atau clock out ?
                var DataAbsensiDB = db.TBPenggunaLogKehadirans.Where(item => item.IDPengguna == HiddenFieldIDPengguna.Value.ToInt()).OrderByDescending(item2 => item2.IDPenggunaLogKehadiran).FirstOrDefault();

                //Kalau belum clock in, dia clock in
                if (DataAbsensiDB == null || DataAbsensiDB.Status == true)
                {
                    TBPenggunaLogKehadiran Absensi = TambahAbsensi();

                    db.TBPenggunaLogKehadirans.InsertOnSubmit(Absensi);
                    db.SubmitChanges();
                }
                //Kalau sudah clock in, dia clock out
                else
                {
                    DataAbsensiDB.JadwalJamKeluar = DateTime.ParseExact(DateTime.Now.Date.ToString("MM/dd/yyyy") + " " + db.TBPenggunaJadwals.FirstOrDefault(item => item.NamaHari == (DateTime.Now.ToString("dddd", new CultureInfo("id-ID")).ToString())).JadwalJamKeluar.Value.ToString("HH:mm"), "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None);
                    DataAbsensiDB.JamKeluar = DateTime.Now;
                    DataAbsensiDB.Status = true;
                    db.SubmitChanges();
                }

                LoadDataRecentClockInOut();
            }
        }
    }
    private TBPenggunaLogKehadiran TambahAbsensi()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPenggunaLogKehadiran Absensi = new TBPenggunaLogKehadiran
            {
                IDPengguna = HiddenFieldIDPengguna.Value.ToInt(),
                JamMasuk = DateTime.Now,
                JadwalJamMasuk = DateTime.ParseExact(DateTime.Now.Date.ToString("MM/dd/yyyy") + " " + db.TBPenggunaJadwals.FirstOrDefault(item => item.NamaHari == (DateTime.Now.ToString("dddd", new CultureInfo("id-ID")).ToString())).JadwalJamMasuk.Value.ToString("HH:mm"), "MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None),
                Status = false,
                Keterangan = ""
            };

            return Absensi;
        }

    }
    protected void LoadDataRecentClockInOut()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var DataPegawaiDB = db.TBPenggunas.ToArray();
            var LogAbsensi = db.TBPenggunaLogKehadirans.Where(item => item.JamMasuk.Value.Date == DateTime.Now.Date).OrderByDescending(item2 => item2.IDPenggunaLogKehadiran).Take(10).ToArray();
            RepeaterRecentClockInOut.DataSource = (from item in LogAbsensi
                                                   join item2 in DataPegawaiDB on item.IDPengguna equals item2.IDPengguna
                                                   select new
                                                   {
                                                       NamaLengkap = item2.NamaLengkap,
                                                       RecentActivity = item.Status == true ? "Last Clock Out " + item.JamKeluar.Value.ToString("HH:mm") : "Last Clock In " + item.JamMasuk.Value.ToString("HH:mm"),
                                                       Status = item.Status
                                                   }).ToArray();
            RepeaterRecentClockInOut.DataBind();
        }

    }
    protected void RepeaterRecentClockInOut_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void DropDownListPegawai_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            ModalPopupExtenderClockInOut.Show();

            var DataPegawaiDB = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna.ToString() == DropDownListPegawai.SelectedValue.ToString());
            LabelUsername.Text = DataPegawaiDB.NamaLengkap;
            LabelWaktuClockInOut.Text = DateTime.Now.ToShortTimeString();
            HiddenFieldIDPengguna.Value = DataPegawaiDB.IDPengguna.ToString();
        }
    }
}