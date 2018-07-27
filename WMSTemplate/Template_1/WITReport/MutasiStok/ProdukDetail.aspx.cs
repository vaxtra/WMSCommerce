using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Laporan_Transaksi_Transaksi : System.Web.UI.Page
{
    public Dictionary<string, dynamic> Result { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                LabelNamaProduk.Text = db.TBKombinasiProduks.FirstOrDefault(item => item.IDKombinasiProduk == Request.QueryString["id"].ToInt()).Nama;

                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");
            }

            LoadData();
        }
        //else
        //LinkDownload.Visible = false;
    }

    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        if (TextBoxTanggalAwal.Text.ToDateTime().Date == TextBoxTanggalAkhir.Text.ToDateTime().Date)
        {
            LabelPeriode.Text = TextBoxTanggalAwal.Text;
        }
        else
        {
            LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;
        }

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            int StokSekarang = 0;
            if (DropDownListTempat.SelectedValue != "0")
                StokSekarang = db.TBStokProduks.FirstOrDefault(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt() && item.IDKombinasiProduk == Request.QueryString["id"].ToInt()).Jumlah.Value;
            else
                StokSekarang = db.TBStokProduks.Where(item => item.IDKombinasiProduk == Request.QueryString["id"].ToInt()).Sum(item => item.Jumlah.Value);

            DataModelStokProdukMutasiDetail[] StokProdukMutasi = db.TBStokProdukMutasis
                    .Where(item =>
                        (DropDownListTempat.SelectedValue != "0" ? item.TBStokProduk.IDTempat == DropDownListTempat.SelectedValue.ToInt() : item.TBStokProduk.IDTempat > 0) &&
                        item.TBStokProduk.IDKombinasiProduk == Request.QueryString["id"].ToInt() &&
                        item.Tanggal >= TextBoxTanggalAwal.Text.ToDateTime().Date &&
                        item.Tanggal <= DateTime.Now)
                    .Select(item => new DataModelStokProdukMutasiDetail
                    {
                        Tanggal = item.Tanggal,
                        Debit = item.Debit,
                        Kredit = item.Kredit,
                        Total = item.Kredit - item.Debit,
                        StokSekarang = item.TBStokProduk.Jumlah.Value,
                        IDJenisStokMutasi = item.IDJenisStokMutasi,
                        Keterangan = item.Keterangan
                    }).ToArray();

            List<DataModelStokProdukMutasiDetailJenis> ListMutasiStokProduk = new List<DataModelStokProdukMutasiDetailJenis>();

            for (DateTime i = TextBoxTanggalAwal.Text.ToDateTime(); i <= TextBoxTanggalAkhir.Text.ToDateTime(); i = i.AddDays(1))
            {
                ListMutasiStokProduk.Add(new DataModelStokProdukMutasiDetailJenis
                {
                    Tanggal = i,

                    StokAwal = CariStok(StokProdukMutasi, i, StokSekarang),

                    StokOpnameIN = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.StokOpname, "debit"), // STOK OPNAME BERTAMBAH
                    TransferIN = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.Transfer, "debit"), //TRANSFER MASUK, TRANSFER BATAL
                    TransaksiBatal = HitungStokProdukMutasiTransaksi(StokProdukMutasi, i, EnumJenisStokMutasi.Transaksi, "debit"), //PERUBAHAN TRANSAKSI, TRANSAKSI BATAL
                    Restock = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.Restock, "debit"), //RESTOCK BARANG
                    PurchaseOrder = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.PurchaseOrder, "debit"), // PENERIMAAN PO
                    Produksi = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.Produksi, "debit"), //HASIL PRODUKSI
                    ReturPelanggan = HitungStokProdukMutasiReturPelanggan(StokProdukMutasi, i, EnumJenisStokMutasi.ReturPelanggan), // RETUR DARI PEMBELI bisa Masuk / Keluar

                    StokOpnameOUT = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.StokOpname, "kredit"), // STOK OPNAME BERKURANG
                    TransferOUT = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.Transfer, "kredit"), // TRANSFER KELUAR
                    Transaksi = HitungStokProdukMutasiTransaksi(StokProdukMutasi, i, EnumJenisStokMutasi.Transaksi, "kredit"), // TRANSAKSI KELUAR
                    PembuanganRusak = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.PembuanganRusak, "kredit"), //PEMBUANGAN BARANG RUSAK
                    ReturPurchaseOrder = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.ReturPurchaseOrder, "kredit"), //TOLAK PENRIMAAN PO
                    ReturProduksi = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.ReturProduksi, "kredit"), //RETUR PRODUKSI
                    PenguranganUntukProduksi = HitungStokProdukMutasi(StokProdukMutasi, i, EnumJenisStokMutasi.PenguranganUntukProduksi, "kredit"), //PENGURANGAN PRODUKSI
                });
            }

            List<DataModelStokProdukMutasiDetailJenis> ListMutasiStokProdukResult = new List<DataModelStokProdukMutasiDetailJenis>();

            ListMutasiStokProdukResult.AddRange(ListMutasiStokProduk.Select(item => new DataModelStokProdukMutasiDetailJenis
            {
                Tanggal = item.Tanggal,

                StokAwal = item.StokAwal,

                StokOpnameIN = item.StokOpnameIN,
                TransferIN = item.TransferIN,
                TransaksiBatal = item.TransaksiBatal,
                Restock = item.Restock,
                PurchaseOrder = item.PurchaseOrder,
                Produksi = item.Produksi,
                ReturPelanggan = item.ReturPelanggan,

                IN = (
                        item.StokOpnameIN +
                        item.TransferIN +
                        item.TransaksiBatal +
                        item.Restock +
                        item.PurchaseOrder +
                        item.Produksi +
                        item.ReturPelanggan
                    ),

                StokOpnameOUT = item.StokOpnameOUT,
                TransferOUT = item.TransferOUT,
                Transaksi = item.Transaksi,
                PembuanganRusak = item.PembuanganRusak,
                ReturPurchaseOrder = item.ReturPurchaseOrder,
                ReturProduksi = item.ReturProduksi,
                PenguranganUntukProduksi = item.PenguranganUntukProduksi,

                OUT = (
                        item.StokOpnameOUT +
                        item.TransferOUT +
                        item.Transaksi +
                        item.PembuanganRusak +
                        item.ReturPurchaseOrder +
                        item.ReturProduksi +
                        item.PenguranganUntukProduksi
                    ),

                StokAkhir = (
                            item.StokAwal +

                            item.StokOpnameIN +
                            item.TransferIN +
                            item.TransaksiBatal +
                            item.Restock +
                            item.PurchaseOrder +
                            item.Produksi +
                            item.ReturPelanggan +

                            item.StokOpnameOUT +
                            item.TransferOUT +
                            item.Transaksi +
                            item.PembuanganRusak +
                            item.ReturPurchaseOrder +
                            item.ReturProduksi +
                            item.PenguranganUntukProduksi
                            )
            }));

            Result = new Dictionary<string, dynamic>();

            Result.Add("StokAwal", ListMutasiStokProdukResult.FirstOrDefault().StokAwal);
            Result.Add("StokAkhir", ListMutasiStokProdukResult.OrderByDescending(item => item.Tanggal).FirstOrDefault().StokAkhir);

            Result.Add("IN", ListMutasiStokProdukResult.Sum(item => item.IN));
            Result.Add("StokOpnameIN", ListMutasiStokProdukResult.Sum(item => item.StokOpnameIN));
            Result.Add("TransferIN", ListMutasiStokProdukResult.Sum(item => item.TransferIN));
            Result.Add("TransaksiBatal", ListMutasiStokProdukResult.Sum(item => item.TransaksiBatal));
            Result.Add("Restock", ListMutasiStokProdukResult.Sum(item => item.Restock));
            Result.Add("PurchaseOrder", ListMutasiStokProdukResult.Sum(item => item.PurchaseOrder));
            Result.Add("Produksi", ListMutasiStokProdukResult.Sum(item => item.Produksi));
            Result.Add("ReturPelanggan", ListMutasiStokProdukResult.Sum(item => item.ReturPelanggan));

            Result.Add("OUT", ListMutasiStokProdukResult.Sum(item => item.OUT));
            Result.Add("StokOpnameOUT", ListMutasiStokProdukResult.Sum(item => item.StokOpnameOUT));
            Result.Add("TransferOUT", ListMutasiStokProdukResult.Sum(item => item.TransferOUT));
            Result.Add("Transaksi", ListMutasiStokProdukResult.Sum(item => item.Transaksi));
            Result.Add("PembuanganRusak", ListMutasiStokProdukResult.Sum(item => item.PembuanganRusak));
            Result.Add("ReturPurchaseOrder", ListMutasiStokProdukResult.Sum(item => item.ReturPurchaseOrder));
            Result.Add("ReturProduksi", ListMutasiStokProdukResult.Sum(item => item.ReturProduksi));
            Result.Add("PenguranganUntukProduksi", ListMutasiStokProdukResult.Sum(item => item.PenguranganUntukProduksi));

            RepeaterLaporan.DataSource = ListMutasiStokProdukResult;
            RepeaterLaporan.DataBind();

            //#region KONFIGURASI LAPORAN
            //LabelPeriode.Text = Laporan_Class.Periode;

            //LinkDownload.Visible = GenerateExcel;

            //if (LinkDownload.Visible)
            //    LinkDownload.HRef = Laporan_Class.LinkDownload;

            //ButtonPrint.OnClientClick = "return popitup('PerpindahanStokProdukDetailPrint.aspx" + Laporan_Class.TempPencarian + "')";
            //#endregion
        }
    }

    private decimal HitungStokProdukMutasiTransaksi(DataModelStokProdukMutasiDetail[] StokProdukMutasi, DateTime tanggal, EnumJenisStokMutasi enumJenisStokMutasi, string status)
    {
        var resultGroup1 = StokProdukMutasi.Where(item => item.Tanggal.Date == tanggal.Date && item.IDJenisStokMutasi == (int)enumJenisStokMutasi)
            .GroupBy(item => new
            {
                item.Keterangan
            })
            .Select(item => new
            {
                jumlah = item.Sum(item2 => item2.Debit - item2.Kredit)
            }).ToList();

        resultGroup1.RemoveAll(item => item.jumlah == 0);

        if (status == "debit")
            return resultGroup1.Where(item => item.jumlah > 0).Sum(item => item.jumlah);
        else
            return resultGroup1.Where(item => item.jumlah < 0).Sum(item => item.jumlah);
    }

    private decimal HitungStokProdukMutasiReturPelanggan(DataModelStokProdukMutasiDetail[] StokProdukMutasi, DateTime tanggal, EnumJenisStokMutasi enumJenisStokMutasi)
    {
        var retur = StokProdukMutasi
            .Where(item =>
                item.Tanggal.Date == tanggal.Date &&
                item.IDJenisStokMutasi == (int)enumJenisStokMutasi).Sum(item => item.Debit - item.Kredit);

        if (retur > 0)
            return retur;
        else
            return 0;
    }

    private decimal HitungStokProdukMutasi(DataModelStokProdukMutasiDetail[] StokProdukMutasi, DateTime tanggal, EnumJenisStokMutasi enumJenisStokMutasi, string status)
    {
        var resultStokProdukMutasi = StokProdukMutasi
                                    .Where(item =>
                                        item.Tanggal.Date == tanggal.Date &&
                                        item.IDJenisStokMutasi == (int)enumJenisStokMutasi);
        if (status == "debit")
            return resultStokProdukMutasi.Sum(item => item.Debit);
        else
            return resultStokProdukMutasi.Sum(item => item.Kredit) * (-1);
    }

    private decimal CariStok(DataModelStokProdukMutasiDetail[] StokProdukMutasi, DateTime tanggal, int StokSekarang)
    {
        return StokSekarang + StokProdukMutasi.Where(item => item.Tanggal >= tanggal).Sum(item => item.Total);
    }

    private void LoadData()
    {
        LoadData(false);
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Request.QueryString["returnUrl"]))
            Response.Redirect(Request.QueryString["returnUrl"]);
        else
            Response.Redirect("/WITAdministrator/Default.aspx");
    }

    protected void DropDownListTempat_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}

public class DataModelStokProdukMutasiDetail
{
    public DateTime Tanggal { get; set; }
    public int IDJenisStokMutasi { get; set; }
    public decimal Debit { get; set; }
    public decimal Kredit { get; set; }
    public decimal Total { get; set; }
    public int StokSekarang { get; set; }
    public string Keterangan { get; set; }
}

public class DataModelStokProdukMutasiDetailJenis
{
    public DateTime Tanggal { get; set; }
    public decimal StokAwal { get; set; }
    public decimal StokOpnameIN { get; set; }
    public decimal TransferIN { get; set; }
    public decimal TransaksiBatal { get; set; }
    public decimal Restock { get; set; }
    public decimal PurchaseOrder { get; set; }
    public decimal Produksi { get; set; }
    public decimal ReturPelanggan { get; set; }
    public decimal IN { get; set; }
    public decimal OUT { get; set; }
    public decimal StokOpnameOUT { get; set; }
    public decimal TransferOUT { get; set; }
    public decimal Transaksi { get; set; }
    public decimal PembuanganRusak { get; set; }
    public decimal ReturPurchaseOrder { get; set; }
    public decimal ReturProduksi { get; set; }
    public decimal PenguranganUntukProduksi { get; set; }
    public decimal StokAkhir { get; set; }
}