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

public partial class WITReport_StokOpname_Produk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataBind();
                DropDownListTempat.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");

                DropDownListBrand.DataSource = ClassPemilikProduk.Data();
                DropDownListBrand.DataValueField = "IDPemilikProduk";
                DropDownListBrand.DataTextField = "Nama";
                DropDownListBrand.DataBind();
                DropDownListBrand.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

            }

            LoadData();
        }
        else
            LinkDownload.Visible = false;
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
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text);
        ViewState["TanggalAkhir"] = DateTime.Now;
        LoadData();
    }
    #endregion

    private void LoadData()
    {
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");

        LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal();

        LoadDatabase();
    }
    private dynamic LoadDatabase()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //QUERY DATA
            string QueryPencarian = string.Empty;

            var DataStok = db.TBStokProduks.ToArray();
            var DataPerpindahanStokProdukIncludeSO = db.TBPerpindahanStokProduks
                .Where(item =>
                    item.Tanggal >= (DateTime)ViewState["TanggalAwal"] &&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            var DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukIncludeSO.Where(item =>
            item.Tanggal > (DateTime)ViewState["TanggalAwal"] &&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            #region QueryPencarian
            QueryPencarian += "?Awal=" + ViewState["TanggalAwal"];
            QueryPencarian += "&Akhir=" + ViewState["TanggalAkhir"];
            //TEMPAT
            if (DropDownListTempat.SelectedValue != "0")
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();
                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();
            }

            QueryPencarian += "&IDTempat=" + DropDownListTempat.SelectedValue;

            //PRODUK
            if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item => item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(TextBoxProduk.Text.ToLower())).ToArray();
                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item => item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(TextBoxProduk.Text.ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(TextBoxProduk.Text.ToLower())).ToArray();

                QueryPencarian += "&Produk=" + TextBoxProduk.Text;
            }
            //KATEGORI
            if (!string.IsNullOrWhiteSpace(TextBoxKategori.Text))
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama.ToLower().Contains(TextBoxKategori.Text.ToLower())).ToArray();

                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama.ToLower().Contains(TextBoxKategori.Text.ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama.ToLower().Contains(TextBoxKategori.Text.ToLower())).ToArray();

                QueryPencarian += "&Kategori=" + TextBoxKategori.Text;
            }
            //BRAND
            if (!string.IsNullOrWhiteSpace(DropDownListBrand.SelectedItem.Text) && DropDownListBrand.SelectedItem.Value != "0")
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama.ToLower().Contains(DropDownListBrand.SelectedItem.Text.ToLower())).ToArray();

                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama.ToLower().Contains(DropDownListBrand.SelectedItem.Text.ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama.ToLower().Contains(DropDownListBrand.SelectedItem.Text.ToLower())).ToArray();

                QueryPencarian += "&Brand=" + DropDownListBrand.SelectedItem.Text;
            }
            #endregion

            List<Stok_Model> DataClassStokOpname = new List<Stok_Model>();

            int i = 0;
            for (int index = 0; index < DataStok.Count(); index++)
            {
                //ADA STOK OPNAME TIDAK PRODUK INI ?
                var LogPerpindahanStokOpname = DataPerpindahanStokProdukIncludeSO.Where(item2 =>
                item2.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DataStok[index].IDKombinasiProduk &&
                (item2.IDJenisPerpindahanStok == 11 || item2.IDJenisPerpindahanStok == 12)).OrderByDescending(item2 => item2.IDPerpindahanStokProduk);

                if (LogPerpindahanStokOpname.Count() > 0)
                {
                    Stok_Model newDataClassStokOpname = new Stok_Model();

                    #region Data Produk
                    DataProduk(DataStok, i, index, newDataClassStokOpname);
                    #endregion

                    bool statStokOpname = false;
                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == newDataClassStokOpname.IDKombinasiProduk).Jumlah;

                    //LOG PERPINDAHAN STOK DETAIL (SELURUH STATUS)
                    var LogPerpindahanStokDetail = DataPerpindahanStokProdukIncludeSO.Where(item2 => item2.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DataStok[index].IDKombinasiProduk).OrderByDescending(item2 => item2.IDPerpindahanStokProduk);

                    foreach (var item in LogPerpindahanStokDetail)
                    {
                        //STOK OPNAME BERKURANG
                        if (item.IDJenisPerpindahanStok == 11)
                        {
                            //SUDAH PERNAH STOK OPNAME BLM ?
                            if (statStokOpname == false)
                            {
                                newDataClassStokOpname.StokSebelumSO = saldoStok + item.Jumlah;
                                newDataClassStokOpname.StokBerkurangSO = item.Jumlah;
                                newDataClassStokOpname.StokKeluar = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                                statStokOpname = true;

                                DataClassStokOpname.Add(newDataClassStokOpname);
                            }
                            else
                            {
                                i += 1;
                                newDataClassStokOpname = new Stok_Model();

                                #region Data Produk
                                DataProduk(DataStok, i, index, newDataClassStokOpname);
                                #endregion

                                newDataClassStokOpname.StokSebelumSO = saldoStok + item.Jumlah;
                                newDataClassStokOpname.StokBerkurangSO = item.Jumlah;
                                newDataClassStokOpname.StokKeluar = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                                DataClassStokOpname.Add(newDataClassStokOpname);
                            }


                            saldoStok += item.Jumlah;
                        }
                        //STOK OPNAME BERTAMBAH
                        else if (item.IDJenisPerpindahanStok == 12)
                        {
                            if (statStokOpname == false)
                            {
                                newDataClassStokOpname.StokSebelumSO = saldoStok - item.Jumlah; //50
                                newDataClassStokOpname.StokBertambahSO = item.Jumlah;
                                newDataClassStokOpname.StokMasuk = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                                statStokOpname = true;

                                DataClassStokOpname.Add(newDataClassStokOpname);
                            }
                            else
                            {
                                i += 1;
                                newDataClassStokOpname = new Stok_Model();

                                #region Data Produk
                                DataProduk(DataStok, i, index, newDataClassStokOpname);
                                #endregion

                                newDataClassStokOpname.StokSebelumSO = saldoStok - item.Jumlah;
                                newDataClassStokOpname.StokBertambahSO = item.Jumlah;
                                newDataClassStokOpname.StokMasuk = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                            }
                            saldoStok -= item.Jumlah;
                        }
                        //SELAIN STOK OPNAME
                        else
                        {
                            if (item.TBJenisPerpindahanStok.Status == false)
                            {
                                saldoStok += item.Jumlah;
                            }
                            else
                            {
                                saldoStok -= item.Jumlah;
                            }
                        }
                    }
                }
                else
                {
                    Stok_Model newDataClassStokOpname = new Stok_Model();

                    #region Data Produk
                    DataProduk(DataStok, i, index, newDataClassStokOpname);
                    #endregion

                    newDataClassStokOpname.StokMasuk = 0;
                    newDataClassStokOpname.StokKeluar = 0;

                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == newDataClassStokOpname.IDKombinasiProduk).Jumlah;
                    var LogPerpindahanStokDetail = DataPerpindahanStokProdukIncludeSO.Where(item2 => item2.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DataStok[index].IDKombinasiProduk).OrderByDescending(item2 => item2.IDPerpindahanStokProduk);

                    foreach (var item in LogPerpindahanStokDetail)
                    {
                        if (item.TBJenisPerpindahanStok.Status == false)
                        {
                            newDataClassStokOpname.StokKeluar = +item.Jumlah;
                            //saldoStok += item.Jumlah;
                        }
                        else
                        {
                            newDataClassStokOpname.StokMasuk = +item.Jumlah;
                            //saldoStok -= item.Jumlah;
                        }
                    }
                    newDataClassStokOpname.StokSetelahSO = Math.Abs(newDataClassStokOpname.StokMasuk - newDataClassStokOpname.StokKeluar);
                    newDataClassStokOpname.StokSebelumSO = Math.Abs(newDataClassStokOpname.StokMasuk - newDataClassStokOpname.StokKeluar);
                    i += 1;

                    DataClassStokOpname.Add(newDataClassStokOpname);
                }
            }

            var ListPerpindahanStokProduk = DataClassStokOpname.Select(item =>
            new
            {
                Index = item.IndexClass,
                Kode = item.KodeKombinasiProduk,
                NamaProduk = item.NamaProduk,
                Varian = item.NamaKombinasiProduk,
                Kategori = item.Kategori,
                Warna = item.Warna,
                PemilikProduk = item.Brand,
                StokSebelumSO = item.StokSebelumSO,
                StokSetelahSO = item.StokSetelahSO,

                SelisihQtyPositif = item.StokSetelahSO > item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,
                SelisihQtyNegatif = item.StokSetelahSO < item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,

                SelisihNominalPositif = item.StokSetelahSO > item.StokSebelumSO ? (item.StokSetelahSO - item.StokSebelumSO) * DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).HargaJual
                : 0,

                SelisihNominalNegatif = item.StokSetelahSO < item.StokSebelumSO ? (item.StokSetelahSO - item.StokSebelumSO) * DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).HargaJual
                : 0,
            }).ToArray().OrderBy(item => item.NamaProduk).ThenByDescending(item => item.Index);

            if (ListPerpindahanStokProduk.Count() > 0)
            {
                RepeaterLaporan.DataSource = ListPerpindahanStokProduk;

                #region HEADER
                LabelGtandTotalSelisihQty.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyPositif) + ListPerpindahanStokProduk.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelGtandTotalSelisihNominal.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalPositif) + ListPerpindahanStokProduk.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                LabelTotalJumlahQtyPositif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyPositif).ToFormatHargaBulat();
                LabelTotalJumlahQtyNegatif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyNegatif).ToFormatHargaBulat();
                LabelTotalJumlahNominalPositif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                #endregion

                #region FOOTER
                LabelTotalJumlahQtyPositif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyPositif).ToFormatHargaBulat();
                LabelTotalJumlahQtyNegatif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyNegatif).ToFormatHargaBulat();
                LabelTotalJumlahNominalPositif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                #endregion

            }
            else
            {
                RepeaterLaporan.DataSource = null;
            }

            RepeaterLaporan.DataBind();

            ButtonPrint.OnClientClick = "return popitup('ProdukPrint.aspx" + QueryPencarian + "')";

            return ListPerpindahanStokProduk;
        }
    }

    private static void DataProduk(TBStokProduk[] DataStok, int i, int index, Stok_Model newDataClassStokOpname)
    {
        newDataClassStokOpname.IndexClass = i;
        newDataClassStokOpname.IDKombinasiProduk = DataStok[index].IDKombinasiProduk;
        newDataClassStokOpname.KodeKombinasiProduk = DataStok[index].TBKombinasiProduk.KodeKombinasiProduk;
        newDataClassStokOpname.NamaKombinasiProduk = DataStok[index].TBKombinasiProduk.Nama;
        newDataClassStokOpname.NamaProduk = DataStok[index].TBKombinasiProduk.TBProduk.Nama;
        newDataClassStokOpname.Warna = DataStok[index].TBKombinasiProduk.TBProduk.TBWarna.Nama;
        newDataClassStokOpname.Kategori = DataStok[index].TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama;
        newDataClassStokOpname.Brand = DataStok[index].TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama;
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string NamaWorksheet = "Stok Opname Produk";

        #region Default
        string NamaFile = "Laporan " + NamaWorksheet + " " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";
        string Folder = Server.MapPath("~/file_excel/" + NamaWorksheet + "/Export/");

        if (!Directory.Exists(Folder))
            Directory.CreateDirectory(Folder);

        string Path = Folder + NamaFile;
        string Judul = "Laporan " + NamaWorksheet + " " + Pengguna.Store + " - " + Pengguna.Tempat + " " + DateTime.Now.ToString("d MMMM yyyy");

        FileInfo newFile = new FileInfo(Path);
        #endregion

        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(NamaWorksheet);

            worksheet.Cells[1, 1].Value = "No.";
            worksheet.Cells[1, 2].Value = "Kode";
            worksheet.Cells[1, 3].Value = "NamaProduk";
            worksheet.Cells[1, 4].Value = "Varian";
            worksheet.Cells[1, 5].Value = "Warna";
            worksheet.Cells[1, 6].Value = "Kategori";
            worksheet.Cells[1, 7].Value = "PemilikProduk";
            worksheet.Cells[1, 8].Value = "Stok Sebelum SO";
            worksheet.Cells[1, 9].Value = "Stok Setelah SO";
            worksheet.Cells[1, 10, 1, 11].Value = "Qty";
            worksheet.Cells[1, 12, 1, 13].Value = "Nominal";

            worksheet.Cells[2, 10].Value = "+";
            worksheet.Cells[2, 11].Value = "-";
            worksheet.Cells[2, 12].Value = "+";
            worksheet.Cells[2, 13].Value = "-";



            int index = 3;

            foreach (var item in LoadDatabase())
            {
                worksheet.Cells[index, 1].Value = index - 1;
                worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 2].Value = item.Kode;
                worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 3].Value = item.NamaProduk;
                worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 4].Value = item.Varian;
                worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 5].Value = item.Warna;
                worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 6].Value = item.Kategori;
                worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 7].Value = item.PemilikProduk;
                worksheet.Cells[index, 7].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 8].Value = item.StokSebelumSO;
                worksheet.Cells[index, 8].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 9].Value = item.StokSetelahSO;
                worksheet.Cells[index, 9].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 10].Value = item.SelisihQtyPositif;
                worksheet.Cells[index, 10].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 11].Value = item.SelisihQtyNegatif;
                worksheet.Cells[index, 11].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 12].Value = item.SelisihNominalPositif;
                worksheet.Cells[index, 12].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 13].Value = item.SelisihNominalNegatif;
                worksheet.Cells[index, 13].Style.Numberformat.Format = "@";

                #region Pengaturan   
                worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0";
                worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";
                worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0";
                worksheet.Cells[index, 11].Style.Numberformat.Format = "#,##0";

                if (item.SelisihNominalPositif.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 12].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 12].Style.Numberformat.Format = "#,##0";

                if (item.SelisihNominalNegatif.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 13].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 13].Style.Numberformat.Format = "#,##0";
                #endregion


                index++;
            }

            using (var range = worksheet.Cells[1, 1, 1, 13])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                range.Style.Font.Color.SetColor(Color.White);
            }

            worksheet.Cells["B1:I1"].AutoFilter = true;

            #region Default
            worksheet.Cells.AutoFitColumns(0);

            worksheet.HeaderFooter.OddHeader.CenteredText = "&16&\"Tahoma,Regular Bold\"" + Judul;
            worksheet.HeaderFooter.OddFooter.LeftAlignedText = "Print : " + Pengguna.NamaLengkap + " - " + Pengguna.Tempat + " - " + DateTime.Now.ToString("d MMMM yyyy hh:mm");

            worksheet.HeaderFooter.OddFooter.RightAlignedText = "WIT. Warehouse Management System - " + string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

            package.Workbook.Properties.Title = NamaWorksheet;
            package.Workbook.Properties.Author = "WIT. Warehouse Management System";
            package.Workbook.Properties.Comments = Judul;

            package.Workbook.Properties.Company = "WIT. Warehouse Management System";
            package.Save();
            #endregion
        }

        LinkDownload.Visible = true;
        LinkDownload.HRef = "/file_excel/" + NamaWorksheet + "/Export/" + NamaFile;
    }
    protected void Event_LoadData(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {

    }
}