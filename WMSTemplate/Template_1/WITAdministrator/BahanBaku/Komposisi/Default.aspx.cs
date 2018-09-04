using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RendFramework;
public partial class WITAdministrator_BahanBaku_Komposisi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadData(db);
            }
        }
    }

    #region KOMPOSISI
    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadData(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            DataDisplay DataDisplay = new DataDisplay();

            if (DataDisplay.Prev(DropDownListHalaman))
                LoadData(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonNext_Click(object sender, EventArgs e)
    {
        try
        {
            DataDisplay DataDisplay = new DataDisplay();

            if (DataDisplay.Next(DropDownListHalaman))
                LoadData(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    private void LoadData(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        DataDisplay DataDisplay = new DataDisplay();

        var ListData = db.TBStokBahanBakus
            .AsEnumerable()
            .Where(item => item.IDTempat == pengguna.IDTempat && (!string.IsNullOrWhiteSpace(TextBoxCari.Text) ? item.TBBahanBaku.Nama.ToLower().Contains(TextBoxCari.Text.ToLower()) : true))
            .Select(item => new
            {
                item.TBBahanBaku.IDBahanBaku,
                item.TBBahanBaku.KodeBahanBaku,
                item.TBBahanBaku.Nama,
                HargaPokokProduksi = StokBahanBaku_Class.HitungHargaPokokProduksi(db, pengguna.IDTempat, item.TBBahanBaku) / item.TBBahanBaku.Konversi,
                HargaBeli = item.HargaBeli,
                Satuan = item.TBBahanBaku.TBSatuan.Nama,
                PunyaKomposisi = item.TBBahanBaku.TBKomposisiBahanBakus.Count
            }).OrderBy(item => item.Nama).ToArray();

        int skip = 0;
        int take = 0;
        int count = ListData.Count();

        DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

        RepeaterBahanBaku.DataSource = ListData.Skip(skip).Take(take);
        RepeaterBahanBaku.DataBind();

        DropDownListBahanBaku.DataSource = ListData.Select(item => new { item.IDBahanBaku, item.Nama }).OrderBy(item => item.Nama);
        DropDownListBahanBaku.DataTextField = "Nama";
        DropDownListBahanBaku.DataValueField = "IDBahanBaku";
        DropDownListBahanBaku.DataBind();
        DropDownListBahanBaku.Items.Insert(0, new ListItem { Text = "-Bahan Baku-", Value = "0" });
    }

    protected void RepeaterBahanBaku_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "UbahHargaPokokProduksi")
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StokBahanBaku_Class.PerbaharuiHargaBeli(db, pengguna.IDTempat, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadData(db);
            }
        }
    }
    #endregion

    #region CARI KOMPOSISI
    protected void ButtonCariPemakaian_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterKomposisiBahanBaku.DataSource = db.TBKomposisiBahanBakus.Where(item => item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt()).Select(item => new
            {
                Nama = item.TBBahanBaku.Nama,
                Jumlah = item.Jumlah,
                Satuan = item.TBBahanBaku1.TBSatuan.Nama
            }).ToArray();
            RepeaterKomposisiBahanBaku.DataBind();

            RepeaterKomposisiProduk.DataSource = db.TBKomposisiKombinasiProduks.Where(item => item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt()).Select(item => new
            {
                Nama = item.TBKombinasiProduk.TBProduk.Nama,
                Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Jumlah = item.Jumlah,
                Satuan = item.TBBahanBaku.TBSatuan.Nama
            }).ToArray();
            RepeaterKomposisiProduk.DataBind();
        }
    }
    #endregion

    protected void ImageButtonUpdate_Click(object sender, ImageClickEventArgs e)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StokBahanBaku_Class.PerbaharuiSemuaHargaBeli(db, pengguna.IDTempat);
            db.SubmitChanges();
            LoadData(db);
        }
    }

    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string NamaWorksheet = "Komposisi Bahan Baku";

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

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 2].Value = "Bahan Baku";
                worksheet.Cells[1, 3].Value = "Satuan";
                worksheet.Cells[1, 4].Value = "Komposisi Bahan Baku";
                worksheet.Cells[1, 5].Value = "Jumlah";
                worksheet.Cells[1, 6].Value = "Satuan";

                var daftarJenisBiayaProduksi = db.TBJenisBiayaProduksis.OrderBy(item => item.Nama).ToArray();
                int indexColumn = 7;

                foreach (var item in daftarJenisBiayaProduksi)
                {
                    worksheet.Cells[1, indexColumn].Value = item.Nama;
                    indexColumn++;
                }

                int nomor = 1;
                int indexRow = 2;

                foreach (var itemBahanBaku in db.TBBahanBakus.Where(item => item.TBKomposisiBahanBakus.Count > 0).OrderBy(item => item.Nama).ToArray())
                {
                    //No
                    worksheet.Cells[indexRow, 1].Value = nomor;
                    worksheet.Cells[indexRow, 1].Style.Numberformat.Format = "@";

                    //Bahan Baku
                    worksheet.Cells[indexRow, 2].Value = itemBahanBaku.Nama;
                    worksheet.Cells[indexRow, 2].Style.Numberformat.Format = "@";

                    //Satuan
                    worksheet.Cells[indexRow, 3].Value = itemBahanBaku.TBSatuan.Nama;
                    worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "@";

                    indexColumn = 7;

                    //Jenis Biaya Produksi
                    foreach (var itemJenisBiayaProduksi in daftarJenisBiayaProduksi)
                    {
                        var biayaProduksi = itemBahanBaku.TBRelasiJenisBiayaProduksiBahanBakus.FirstOrDefault(data => data.TBJenisBiayaProduksi == itemJenisBiayaProduksi);

                        if (biayaProduksi != null)
                        {
                            if (biayaProduksi.EnumBiayaProduksi == (int)PilihanBiayaProduksi.Persen)
                            {
                                worksheet.Cells[indexRow, indexColumn].Value = (biayaProduksi.Persentase * 100).Value.ToString() + "%";
                                worksheet.Cells[indexRow, indexColumn].Style.Numberformat.Format = "@";
                            }
                            else
                            {
                                worksheet.Cells[indexRow, indexColumn].Value = biayaProduksi.Nominal.Value;

                                if (biayaProduksi.Nominal.ToFormatHarga().Contains(","))
                                    worksheet.Cells[indexRow, indexColumn].Style.Numberformat.Format = "#,##0.00";
                                else
                                    worksheet.Cells[indexRow, indexColumn].Style.Numberformat.Format = "#,##0";
                            }
                        }
                        else
                        {
                            worksheet.Cells[indexRow, indexColumn].Value = 0;
                            worksheet.Cells[indexRow, indexColumn].Style.Numberformat.Format = "#,##0";
                        }

                        indexColumn++;
                    }

                    foreach (var itemKomposisiBahanBaku in itemBahanBaku.TBKomposisiBahanBakus.OrderBy(item => item.TBBahanBaku1.Nama).ToArray())
                    {
                        //Komposisi Bahan Baku
                        worksheet.Cells[indexRow, 4].Value = itemKomposisiBahanBaku.TBBahanBaku1.Nama;
                        worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "@";

                        //Jumlah
                        worksheet.Cells[indexRow, 5].Value = itemKomposisiBahanBaku.Jumlah.Value;

                        if (itemKomposisiBahanBaku.Jumlah.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0";

                        //Satuan
                        worksheet.Cells[indexRow, 6].Value = itemKomposisiBahanBaku.TBBahanBaku1.TBSatuan.Nama;
                        worksheet.Cells[indexRow, 6].Style.Numberformat.Format = "@";

                        indexRow++;
                    }

                    nomor++;
                }

                using (var range = worksheet.Cells[1, 1, 1, (6 + daftarJenisBiayaProduksi.Count())])
                {
                    range.Style.Font.Bold = true;
                }
            }

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

        ButtonExport.Visible = false;
        LinkDownload.Visible = true;
        LinkDownload.HRef = "/file_excel/" + NamaWorksheet + "/Export/" + NamaFile;
    }
}