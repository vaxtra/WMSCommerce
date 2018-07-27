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

                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
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
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        LoadData();
    }
    #endregion

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");

        if (TextBoxTanggalAwal.Text.ToDateTime().Date == DateTime.Now.Date)
        {
            LabelPeriode.Text = TextBoxTanggalAwal.Text;
        }
        else
        {
            LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + DateTime.Now.Date.ToFormatTanggal();
        }

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var StokProdukMutasi = db.TBStokProdukMutasis
                    .Where(item =>
                    (DropDownListTempat.SelectedValue != "0" ? item.TBStokProduk.IDTempat == DropDownListTempat.SelectedValue.ToInt() : item.TBStokProduk.IDTempat > 0) &&
                    item.Tanggal >= TextBoxTanggalAwal.Text.ToDateTime().Date &&
                    item.Tanggal <= DateTime.Now)
                    .Select(item => new DataModelStokProdukMutasiDetail
                    {
                        IDKombinasiProduk = item.TBStokProduk.IDKombinasiProduk,
                        Tanggal = item.Tanggal,
                        Debit = item.Debit,
                        Kredit = item.Kredit,
                        Total = item.Kredit - item.Debit,
                        StokSekarang = item.TBStokProduk.Jumlah.Value,
                        IDJenisStokMutasi = item.IDJenisStokMutasi,
                        Keterangan = item.Keterangan
                    }).ToArray();

            List<DataModelStokProduk> DaftarStokProduk = new List<DataModelStokProduk>();

            if (DropDownListTempat.SelectedValue != "0")
            {
                DaftarStokProduk = db.TBStokProduks.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt() && item.TBKombinasiProduk.TBProduk._IsActive == true).Select(item => new DataModelStokProduk
                {
                    IDKombinasiProduk = item.TBKombinasiProduk.IDKombinasiProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,
                    Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                    StokSekarang = item.Jumlah.Value
                }).ToList();
            }
            else
            {
                DaftarStokProduk = db.TBStokProduks.Where(item => item.TBKombinasiProduk.TBProduk._IsActive == true).GroupBy(item => new
                {
                    item.TBKombinasiProduk
                }).Select(item => new DataModelStokProduk
                {
                    IDKombinasiProduk = item.Key.TBKombinasiProduk.IDKombinasiProduk,
                    Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                    Varian = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                    Warna = item.Key.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Brand = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.Key.TBKombinasiProduk),
                    StokSekarang = item.Sum(item2 => item2.Jumlah.Value)
                }).ToList();
            }

            var StokProduk = DaftarStokProduk.Select(item => new
            {
                item.IDStokProduk,
                item.IDKombinasiProduk,
                item.Produk,
                item.Varian,
                item.Warna,
                item.Brand,
                item.Kategori,

                StokAwal = item.StokSekarang + StokProdukMutasi.Where(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).Sum(item2 => item2.Total),

                StokOpnameIN = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.StokOpname, "debit"), // STOK OPNAME BERTAMBAH
                TransferIN = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.Transfer, "debit"), //TRANSFER MASUK, TRANSFER BATAL
                TransaksiBatal = HitungStokProdukMutasiTransaksi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.Transaksi, "debit"), //PERUBAHAN TRANSAKSI, TRANSAKSI BATAL
                Restock = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.Restock, "debit"), //RESTOCK BARANG
                PurchaseOrder = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.PurchaseOrder, "debit"), // PENERIMAAN PO
                Produksi = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.Produksi, "debit"), //HASIL PRODUKSI
                ReturPelanggan = HitungStokProdukMutasiReturPelanggan(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.ReturPelanggan), // RETUR DARI PEMBELI bisa Masuk / Keluar

                StokOpnameOUT = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.StokOpname, "kredit"), // STOK OPNAME BERKURANG
                TransferOUT = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.Transfer, "kredit"), // TRANSFER KELUAR
                Transaksi = HitungStokProdukMutasiTransaksi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.Transaksi, "kredit"), // TRANSAKSI KELUAR
                PembuanganRusak = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.PembuanganRusak, "kredit"), //PEMBUANGAN BARANG RUSAK
                ReturPurchaseOrder = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.ReturPurchaseOrder, "kredit"), //TOLAK PENRIMAAN PO
                ReturProduksi = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.ReturProduksi, "kredit"), //RETUR PRODUKSI
                PenguranganUntukProduksi = HitungStokProdukMutasi(StokProdukMutasi, item.IDKombinasiProduk, EnumJenisStokMutasi.PenguranganUntukProduksi, "kredit"), //PENGURANGAN PRODUKSI
            })
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.IDKombinasiProduk,

                        item.Produk,
                        item.Varian,
                        item.Warna,
                        item.Brand,
                        item.Kategori,

                        item.StokAwal,

                        item.StokOpnameIN,
                        item.TransferIN,
                        item.TransaksiBatal,
                        item.Restock,
                        item.PurchaseOrder,
                        item.Produksi,
                        item.ReturPelanggan,

                        item.StokOpnameOUT,
                        item.TransferOUT,
                        item.Transaksi,
                        item.PembuanganRusak,
                        item.ReturPurchaseOrder,
                        item.ReturProduksi,
                        item.PenguranganUntukProduksi,

                        IN = (
                            item.StokOpnameIN +
                            item.TransferIN +
                            item.TransaksiBatal +
                            item.Restock +
                            item.PurchaseOrder +
                            item.Produksi +
                            item.ReturPelanggan
                        ),

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
                        ),
                    })
                    .OrderBy(item => item.Produk)
                    .ToArray();

            Result = new Dictionary<string, dynamic>();

            Result.Add("StokAwal", StokProduk.Sum(item => item.StokAwal));
            Result.Add("StokAkhir", StokProduk.Sum(item => item.StokAkhir));

            Result.Add("IN", StokProduk.Sum(item => item.IN));
            Result.Add("StokOpnameIN", StokProduk.Sum(item => item.StokOpnameIN));
            Result.Add("TransferIN", StokProduk.Sum(item => item.TransferIN));
            Result.Add("TransaksiBatal", StokProduk.Sum(item => item.TransaksiBatal));
            Result.Add("Restock", StokProduk.Sum(item => item.Restock));
            Result.Add("PurchaseOrder", StokProduk.Sum(item => item.PurchaseOrder));
            Result.Add("Produksi", StokProduk.Sum(item => item.Produksi));
            Result.Add("ReturPelanggan", StokProduk.Sum(item => item.ReturPelanggan));

            Result.Add("OUT", StokProduk.Sum(item => item.OUT));
            Result.Add("StokOpnameOUT", StokProduk.Sum(item => item.StokOpnameOUT));
            Result.Add("TransferOUT", StokProduk.Sum(item => item.TransferOUT));
            Result.Add("Transaksi", StokProduk.Sum(item => item.Transaksi));
            Result.Add("PembuanganRusak", StokProduk.Sum(item => item.PembuanganRusak));
            Result.Add("ReturPurchaseOrder", StokProduk.Sum(item => item.ReturPurchaseOrder));
            Result.Add("ReturProduksi", StokProduk.Sum(item => item.ReturProduksi));
            Result.Add("PenguranganUntukProduksi", StokProduk.Sum(item => item.PenguranganUntukProduksi));

            RepeaterLaporan.DataSource = StokProduk;
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

    private decimal HitungStokProdukMutasiTransaksi(DataModelStokProdukMutasiDetail[] StokProdukMutasi, int IDKombinasiProduk, EnumJenisStokMutasi enumJenisStokMutasi, string status)
    {
        var resultGroup1 = StokProdukMutasi.Where(item => item.IDKombinasiProduk == IDKombinasiProduk && item.IDJenisStokMutasi == (int)enumJenisStokMutasi)
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

    private decimal HitungStokProdukMutasiReturPelanggan(DataModelStokProdukMutasiDetail[] StokProdukMutasi, int IDKombinasiProduk, EnumJenisStokMutasi enumJenisStokMutasi)
    {
        var retur = StokProdukMutasi
            .Where(item =>
                item.IDKombinasiProduk == IDKombinasiProduk &&
                item.IDJenisStokMutasi == (int)enumJenisStokMutasi).Sum(item => item.Debit - item.Kredit);

        if (retur > 0)
            return retur;
        else
            return 0;
    }

    private decimal HitungStokProdukMutasi(DataModelStokProdukMutasiDetail[] StokProdukMutasi, int IDKombinasiProduk, EnumJenisStokMutasi enumJenisStokMutasi, string status)
    {
        var resultStokProdukMutasi = StokProdukMutasi
                                    .Where(item =>
                                        item.IDKombinasiProduk == IDKombinasiProduk &&
                                        item.IDJenisStokMutasi == (int)enumJenisStokMutasi);
        if (status == "debit")
            return resultStokProdukMutasi.Sum(item => item.Debit);
        else
            return resultStokProdukMutasi.Sum(item => item.Kredit) * (-1);
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
    public int IDKombinasiProduk { get; set; }
    public DateTime Tanggal { get; set; }
    public int IDJenisStokMutasi { get; set; }
    public decimal Debit { get; set; }
    public decimal Kredit { get; set; }
    public decimal Total { get; set; }
    public int StokSekarang { get; set; }
    public string Keterangan { get; set; }
}

public class DataModelStokProduk
{
    public int IDStokProduk { get; set; }
    public int IDKombinasiProduk { get; set; }
    public string Produk { get; set; }
    public string Varian { get; set; }
    public string Warna { get; set; }
    public string Brand { get; set; }
    public string Kategori { get; set; }
    public int StokSekarang { get; set; }
}