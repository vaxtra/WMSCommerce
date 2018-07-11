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

public partial class WITReport_StokOpname_BahanBaku : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataBind();
                DropDownListTempat.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            }

            LoadData();
        }
        else
            LinkDownload.Visible = false;
    }

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
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            //QUERY DATA
            string QueryPencarian = string.Empty;

            var DataStok = db.TBStokBahanBakus.Where(item => item.IDTempat == DropDownListTempat.SelectedItem.Value.ToInt()).ToArray();
            var DataPerpindahanStokBahanBakuIncludeSO = db.TBPerpindahanStokBahanBakus
                .Where(item =>
                    item.Tanggal >= (DateTime)ViewState["TanggalAwal"] &&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            var DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item =>
            item.Tanggal > (DateTime)ViewState["TanggalAwal"] &&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            QueryPencarian += "?Awal=" + ViewState["TanggalAwal"];
            QueryPencarian += "&Akhir=" + ViewState["TanggalAkhir"];

            if (DropDownListTempat.SelectedValue != "0")
            {
                DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuExcludeSO.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();
                DataPerpindahanStokBahanBakuIncludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();
            }

            QueryPencarian += "&IDTempat=" + DropDownListTempat.SelectedValue;

            //BAHAN BAKU
            if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
            {
                DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuExcludeSO.Where(item => item.TBStokBahanBaku.TBBahanBaku.Nama.ToLower().Contains(TextBoxProduk.Text.ToLower())).ToArray();
                DataPerpindahanStokBahanBakuIncludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item => item.TBStokBahanBaku.TBBahanBaku.Nama.ToLower().Contains(TextBoxProduk.Text.ToLower())).ToArray();
                QueryPencarian += "&BahanBaku=" + TextBoxProduk.Text;

                DataStok = DataStok.Where(item => item.TBBahanBaku.Nama.ToLower().Contains(TextBoxProduk.Text.ToLower())).ToArray();
            }
            //KATEGORI
            if (!string.IsNullOrWhiteSpace(TextBoxKategori.Text))
            {
                DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuExcludeSO.Where(item =>
                item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.TBKategoriBahanBaku.Nama.ToLower().Contains(TextBoxKategori.Text.ToLower())) != null).ToArray();

                DataPerpindahanStokBahanBakuIncludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item =>
               item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.TBKategoriBahanBaku.Nama.ToLower().Contains(TextBoxKategori.Text.ToLower())) != null).ToArray();

                DataStok = DataStok.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.TBKategoriBahanBaku.Nama.ToLower().Contains(TextBoxKategori.Text.ToLower())) != null).ToArray();

                QueryPencarian += "&Kategori=" + TextBoxKategori.Text;
            }
            List<Stok_Model> DataClassStokOpname = new List<Stok_Model>();
            int i = 0;
            for (int index = 0; index < DataStok.Count(); index++)
            {
                //ADA STOK OPNAME TIDAK BAHAN BAKU INI ?
                var LogPerpindahanStokOpname = DataPerpindahanStokBahanBakuIncludeSO.Where(item2 =>
                item2.TBStokBahanBaku.IDBahanBaku == DataStok[index].IDBahanBaku &&
                (item2.IDJenisPerpindahanStok == (int)EnumJenisPerpindahanStok.StokOpnameBerkurang || item2.IDJenisPerpindahanStok == (int)EnumJenisPerpindahanStok.StokOpnameBertambah)).OrderByDescending(item2 => item2.IDPerpindahanStokBahanBaku);
                //if (DataStok[index].TBBahanBaku.KodeBahanBaku == "BA-B01")
                //{
                if (LogPerpindahanStokOpname.Count() > 0)
                {
                    Stok_Model newDataClassStokOpname = new Stok_Model();

                    #region Data Bahan Baku
                    DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
                    #endregion

                    bool statStokOpname = false;
                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == newDataClassStokOpname.IDBahanBaku).Jumlah;

                    //LOG PERPINDAHAN STOK DETAIL (SELURUH STATUS)
                    var LogPerpindahanStokDetail = DataPerpindahanStokBahanBakuIncludeSO.Where(item2 => item2.TBStokBahanBaku.IDBahanBaku == DataStok[index].IDBahanBaku).OrderByDescending(item2 => item2.IDPerpindahanStokBahanBaku);

                    foreach (var item in LogPerpindahanStokDetail)
                    {
                        if (item.IDJenisPerpindahanStok == (int)EnumJenisPerpindahanStok.StokOpnameBerkurang)
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

                                #region Data Bahan Baku
                                DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
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
                        else if (item.IDJenisPerpindahanStok == (int)EnumJenisPerpindahanStok.StokOpnameBertambah)
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

                                #region Data Bahan Baku
                                DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
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

                    #region Data Bahan Baku
                    DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
                    #endregion

                    newDataClassStokOpname.StokMasuk = 0;
                    newDataClassStokOpname.StokKeluar = 0;

                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == newDataClassStokOpname.IDBahanBaku).Jumlah;
                    var LogPerpindahanStokDetail = DataPerpindahanStokBahanBakuIncludeSO.Where(item2 => item2.TBStokBahanBaku.IDBahanBaku == DataStok[index].IDBahanBaku).OrderByDescending(item2 => item2.IDPerpindahanStokBahanBaku);

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
                //}
            }

            var ListPerpindahanStokBahanBaku = DataClassStokOpname
                .Select(item => new
                {
                    Index = item.IndexClass,
                    Kode = item.KodeBahanBaku,
                    BahanBaku = item.NamaBahanBaku,
                    Satuan = item.SatuanKecil,
                    Kategori = item.Kategori,

                    StokSebelumSO = item.StokSebelumSO,
                    NominalSebelumSO = item.StokSebelumSO / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli,
                    StokSetelahSO = item.StokSetelahSO,
                    NominalSetelahSO = item.StokSetelahSO / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli,
                    SelisihQtyPositif = item.StokSetelahSO > item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,
                    SelisihQtyNegatif = item.StokSetelahSO < item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,

                    SelisihNominalPositif = item.StokSetelahSO > item.StokSebelumSO ? ((item.StokSetelahSO - item.StokSebelumSO) / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi) * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli
                : 0,

                    SelisihNominalNegatif = item.StokSetelahSO < item.StokSebelumSO ? ((item.StokSetelahSO - item.StokSebelumSO) / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi) * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli
                : 0,
                }).ToArray().OrderBy(item => item.BahanBaku).ThenByDescending(item => item.Index);

            if (ListPerpindahanStokBahanBaku.Count() > 0)
            {
                RepeaterLaporan.DataSource = ListPerpindahanStokBahanBaku;

                #region HEADER
                LabelGtandTotalSelisihQty.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyPositif) + ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelGtandTotalSelisihNominal.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalPositif) + ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                LabelTotalJumlahQtyPositif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyPositif).ToFormatHarga();
                LabelTotalJumlahQtyNegatif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelTotalJumlahNominalPositif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                LabelNominalSebelumSO.Text = ListPerpindahanStokBahanBaku.Sum(item => item.NominalSebelumSO).ToFormatHarga();
                LabelNominalSetelahSO.Text = ListPerpindahanStokBahanBaku.Sum(item => item.NominalSetelahSO).ToFormatHarga();
                #endregion

                #region FOOTER
                LabelNominalSebelumSO2.Text = LabelNominalSebelumSO.Text;
                LabelNominalSetelahSO2.Text = LabelNominalSetelahSO.Text;
                LabelTotalJumlahQtyPositif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyPositif).ToFormatHarga();
                LabelTotalJumlahQtyNegatif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelTotalJumlahNominalPositif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                #endregion

            }
            else
            {
                RepeaterLaporan.DataSource = null;
            }

            RepeaterLaporan.DataBind();

            ButtonPrint.OnClientClick = "return popitup('BahanBakuPrint.aspx" + QueryPencarian + "')";

            return ListPerpindahanStokBahanBaku;
        }
    }

    private static void DataBahanBaku(DataClassesDatabaseDataContext db, TBStokBahanBaku[] DataStok, int i, Stok_Model newDataClassStokOpname)
    {
        newDataClassStokOpname.IndexClass = i;
        newDataClassStokOpname.IDBahanBaku = (int)DataStok[i].IDBahanBaku;
        newDataClassStokOpname.KodeBahanBaku = DataStok[i].TBBahanBaku.KodeBahanBaku;
        newDataClassStokOpname.NamaBahanBaku = DataStok[i].TBBahanBaku.Nama;
        newDataClassStokOpname.SatuanKecil = DataStok[i].TBBahanBaku.TBSatuan.Nama;
        //////ERROR BANU
        //////newDataClassStokOpname.Kategori = StokBahanBaku.GabungkanSemuaKategoriBahanBaku(db, null, DataStok[i].TBBahanBaku);
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string NamaWorksheet = "Stok Opname Bahan Baku";

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
            worksheet.Cells[1, 3].Value = "Bahan Baku";
            worksheet.Cells[1, 4].Value = "Kategori";
            worksheet.Cells[1, 5].Value = "Stok Sebelum SO";
            worksheet.Cells[1, 6].Value = "Stok Setelah SO";
            worksheet.Cells[1, 7].Value = "Satuan";
            worksheet.Cells[1, 8, 1, 9].Value = "Qty";
            worksheet.Cells[1, 10, 1, 11].Value = "Nominal";

            worksheet.Cells[2, 8].Value = "+";
            worksheet.Cells[2, 9].Value = "-";
            worksheet.Cells[2, 10].Value = "+";
            worksheet.Cells[2, 11].Value = "-";



            int index = 3;

            foreach (var item in LoadDatabase())
            {
                worksheet.Cells[index, 1].Value = index - 1;
                worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 2].Value = item.Kode;
                worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 3].Value = item.BahanBaku;
                worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 4].Value = item.Kategori;
                worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 5].Value = item.StokSebelumSO;
                worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 6].Value = item.StokSetelahSO;
                worksheet.Cells[index, 6].Style.Numberformat.Format = "@";


                worksheet.Cells[index, 7].Value = item.Satuan;
                worksheet.Cells[index, 7].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 8].Value = item.SelisihQtyPositif;
                worksheet.Cells[index, 8].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 9].Value = item.SelisihQtyNegatif;
                worksheet.Cells[index, 9].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 10].Value = item.SelisihNominalPositif;
                worksheet.Cells[index, 10].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 11].Value = item.SelisihNominalNegatif;
                worksheet.Cells[index, 11].Style.Numberformat.Format = "@";

                #region Pengaturan
                if (item.StokSebelumSO.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 5].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 5].Style.Numberformat.Format = "#,##0";

                if (item.StokSetelahSO.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 6].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 6].Style.Numberformat.Format = "#,##0";

                if (item.SelisihQtyPositif.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0";

                if (item.SelisihQtyNegatif.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";

                if (item.SelisihNominalPositif.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0";

                if (item.SelisihNominalNegatif.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 11].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 11].Style.Numberformat.Format = "#,##0";
                #endregion


                index++;
            }

            using (var range = worksheet.Cells[1, 1, 1, 11])
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
        ViewState["TanggalAkhir"] = DateTime.Now.Date;
        LoadData();
    }
    #endregion
}