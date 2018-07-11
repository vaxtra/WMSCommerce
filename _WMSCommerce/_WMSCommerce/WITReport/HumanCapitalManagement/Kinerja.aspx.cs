using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_HumanCapitalManagement_Performance : System.Web.UI.Page
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
                DropDownListEmployee.Items.Insert(0, new ListItem { Value = "0", Text = "All Employees" });

                //DropDownListFilterGender.Items.Insert(0, new ListItem { Value = "0", Text = "All Gender" });
                //DropDownListFilterGender.Items.Insert(1, new ListItem { Value = "1", Text = "Male" });
                //DropDownListFilterGender.Items.Insert(2, new ListItem { Value = "2", Text = "Female" });

                TextBoxTanggalAwal.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                    LabelPeriode.Text = TextBoxTanggalAwal.Text;
                else
                    LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;
            }
        }
    }
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        LoadData(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));
    }
    private void LoadData(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwal.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                LabelPeriode.Text = TextBoxTanggalAwal.Text;
            else
                LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

            var DataPegawaiDB = db.TBPenggunas.ToArray();
            var LogAbsensiDB = db.TBPenggunaLogKehadirans.ToArray();

            if (DropDownListEmployee.SelectedValue != "0")
            {
                LogAbsensiDB = LogAbsensiDB.Where(item => item.IDPengguna == DropDownListEmployee.SelectedValue.ToInt()).ToArray();
            }

            var LogAbsensiDetail = LogAbsensiDB
                .Where(item => item.JamMasuk.Value.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date &&
                    item.JamKeluar.Value.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date)
                    .GroupBy(item => new
                    {
                        IDPengguna = item.IDPengguna,
                        NamaLengkap = item.TBPengguna.NamaLengkap
                    })
                .Select(item => new
                {
                    item.Key,
                    Alamat = item.FirstOrDefault().TBPengguna.Alamat,
                    Telepon = item.FirstOrDefault().TBPengguna.Telepon,
                    TotalHr = item.Sum(item2 => (TimeSpan.Parse(item2.TotalJamKerja.Value.ToString("HH:mm")).TotalHours)),
                    OverTimeHr = item.Sum(item2 => (TimeSpan.Parse(item2.TotalJamLembur.Value.ToString("HH:mm")).TotalHours)),
                    TotalKeterlambatan = item.Sum(item2 => (TimeSpan.Parse(item2.TotalJamKeterlambatan.Value.ToString("HH:mm")).TotalHours))
                }).ToArray();


            //(item.JamKeluar.Value - item.JamMasuk.Value).TotalHours
            //var hasil = db.TBSuppliers.AsEnumerable().Select(item => new
            //{
            //    item.IDSupplier,
            //    Supplier = item.Nama,
            //    item.Alamat,
            //    TotalPO = poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier).Select(data => data.IDPOBahanBaku).Distinct().Count(),
            //    Penerimaan = poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier).Select(data => data.IDPOBahanBaku).Count() == 0 ? -1 : ((decimal)poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier).Sum(data => data.JumlahDiTerima.Value) / (decimal)poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier).Sum(data => data.TotalDatang.Value)) * 100,
            //    Pengiriman = poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier).Select(data => data.IDPOBahanBaku).Count() == 0 ? -1 : ((decimal)poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier && data.TanggalTerima.Value.Date <= data.TanggalPengiriman.Value.Date).Select(data => data.IDPOBahanBaku).Count() / (decimal)poBahanBakuDetail.Where(data => data.IDSupplier == item.IDSupplier).Select(data => data.IDPOBahanBaku).Count()) * 100
            //}).ToArray();

            //if (DropDownListSupplier.SelectedValue != "0")
            //{
            //    hasil = hasil.Where(item => item.IDSupplier == DropDownListSupplier.SelectedValue.ToInt()).ToArray();
            //}

            RepeaterEmployeePerformance.DataSource = LogAbsensiDetail;
            RepeaterEmployeePerformance.DataBind();
        }
    }
}
