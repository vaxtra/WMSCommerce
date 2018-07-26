using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_HumanCapitalManagement_Presensi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListEmployee.DataSource = db.TBPenggunas.OrderBy(item => item.NamaLengkap).ToArray();
                DropDownListEmployee.DataTextField = "NamaLengkap";
                DropDownListEmployee.DataValueField = "IDPengguna";
                DropDownListEmployee.DataBind();
                DropDownListEmployee.Items.Insert(0, new ListItem { Value = "0", Text = "Pilih Pegawai" });

                int index = 0;
                for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
                {
                    DropDownListTahun.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                    index++;
                }
                DropDownListTahun.SelectedIndex = 5;
            }
        }
    }

    protected void ButtonJanuari_Click(object sender, EventArgs e)
    {
        LoadData("1", "Januari");
    }
    protected void ButtonFebruari_Click(object sender, EventArgs e)
    {
        LoadData("2", "Febuari");
    }
    protected void ButtonMaret_Click(object sender, EventArgs e)
    {
        LoadData("3", "Maret");
    }
    protected void ButtonApril_Click(object sender, EventArgs e)
    {
        LoadData("4", "April");
    }
    protected void ButtonMei_Click(object sender, EventArgs e)
    {
        LoadData("5", "Mei");
    }
    protected void ButtonJuni_Click(object sender, EventArgs e)
    {
        LoadData("6", "Juni");
    }
    protected void ButtonJuli_Click(object sender, EventArgs e)
    {
        LoadData("7", "Juli");
    }
    protected void ButtonAgustus_Click(object sender, EventArgs e)
    {
        LoadData("8", "Agustus");
    }
    protected void ButtonSeptember_Click(object sender, EventArgs e)
    {
        LoadData("9", "September");
    }
    protected void ButtonOktober_Click(object sender, EventArgs e)
    {
        LoadData("10", "Oktober");
    }
    protected void ButtonNopember_Click(object sender, EventArgs e)
    {
        LoadData("11", "November");
    }
    protected void ButtonDesember_Click(object sender, EventArgs e)
    {
        LoadData("12", "Desember");
    }
    private void LoadData(string bulan, string namaBulan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //TextBoxTanggalAwal.Text = tanggalAwal.ToString("d MMMM yyyy");
            //TextBoxTanggalAkhir.Text = tanggalAkhir.ToString("d MMMM yyyy");

            LabelPeriode.Text = namaBulan + " " + DropDownListTahun.SelectedItem.Text;
            int JumlahHari = DateTime.DaysInMonth(int.Parse(DropDownListTahun.SelectedValue), int.Parse(bulan));
            int JumlahKetidakHarian;

            var LogAbsensiDB = db.TBPenggunaLogKehadirans.ToArray();

            if (DropDownListEmployee.SelectedValue != "0")
            {
                LogAbsensiDB = LogAbsensiDB.Where(item => item.IDPengguna == DropDownListEmployee.SelectedValue.ToInt()).ToArray();

                var LogAbsensiDetail = LogAbsensiDB
                .Where(item =>
                    (item.JamMasuk.Value.Month == int.Parse(bulan) && item.JamKeluar.Value.Month == int.Parse(bulan)) &&
                    (item.JamMasuk.Value.Year == int.Parse(DropDownListTahun.SelectedItem.Text) && item.JamKeluar.Value.Year == int.Parse(DropDownListTahun.SelectedItem.Text)))
                .Select(item => new
                {
                    Tanggal = item.JamMasuk.Value.Date.ToString("dd, MMMM yyyy"),
                    NamaLengkap = item.TBPengguna.NamaLengkap,
                    Alamat = item.TBPengguna.Alamat,
                    Tlp = item.TBPengguna.Telepon,
                    JamMasuk = item.JamMasuk.Value.ToString("HH:mm"),
                    JamKeluar = item.JamKeluar.Value.ToString("HH:mm"),
                    TotalHr = TimeSpan.Parse(item.TotalJamKerja.Value.ToString("HH:mm")).TotalHours,
                    TotalKeterlambatan = TimeSpan.Parse(item.TotalJamKeterlambatan.Value.ToString("HH:mm")).TotalHours,
                    OverTimeHr = TimeSpan.Parse(item.TotalJamLembur.Value.ToString("HH:mm")).TotalHours
                }).ToArray();

                JumlahKetidakHarian = LogAbsensiDetail.Count() - JumlahHari;
                LabelAbsensi.Text = " " + Math.Abs(JumlahKetidakHarian).ToString() + " Hari";
                RepeaterEmployeePerformance.DataSource = LogAbsensiDetail.OrderBy(item => item.Tanggal);
                RepeaterEmployeePerformance.DataBind();
            }
        }
    }

}