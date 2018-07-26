using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RendFramework;

public partial class WITAdministrator_Produk_Komposisi_Default : System.Web.UI.Page
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

        var ListData = db.TBStokProduks
            .AsEnumerable()
            .Where(item => item.IDTempat == pengguna.IDTempat && (!string.IsNullOrWhiteSpace(TextBoxCari.Text) ? item.TBKombinasiProduk.Nama.ToLower().Contains(TextBoxCari.Text.ToLower()) : true))
            .Select(item => new
            {
                item.TBKombinasiProduk.IDKombinasiProduk,
                item.TBKombinasiProduk.KodeKombinasiProduk,
                item.TBKombinasiProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                HargaPokokProduksi = StokProduk_Class.HitungHargaPokokProduksi(db, pengguna.IDTempat, item.TBKombinasiProduk),
                item.HargaBeli,
                item.HargaJual,
                PersentaseSelisihHarga = (item.HargaJual > 0) ? (item.HargaBeli / item.HargaJual) * 100 : 0,
                PunyaKomposisi = item.TBKombinasiProduk.TBKomposisiKombinasiProduks.Count
            }).OrderBy(item => item.Nama).ToArray();

        int skip = 0;
        int take = 0;
        int count = ListData.Count();

        DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);


        RepeaterKombinasiProduk.DataSource = ListData.Skip(skip).Take(take);
        RepeaterKombinasiProduk.DataBind();

        DropDownListBahanBaku.DataSource = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).Select(item => new { item.IDBahanBaku, item.TBBahanBaku.Nama }).OrderBy(item => item.Nama);
        DropDownListBahanBaku.DataTextField = "Nama";
        DropDownListBahanBaku.DataValueField = "IDBahanBaku";
        DropDownListBahanBaku.DataBind();
        DropDownListBahanBaku.Items.Insert(0, new ListItem { Text = "-Bahan Baku-", Value = "0" });
    }
    protected void RepeaterKombinasiProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "UbahHargaPokokProduksi")
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                StokProduk_Class.PerbaharuiHargaBeli(db, pengguna.IDTempat, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadData(db);
            }
        }
    }
    #endregion

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

    protected void ButtonPerbaharuiSemuaHargaPokokProduksiProduk_Click(object sender, EventArgs e)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StokProduk_Class.PerbaharuiSemuaHargaBeli(db, pengguna.IDTempat);
            db.SubmitChanges();
            LoadData(db);
        }
    }

    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string NamaWorksheet = "Komposisi Produk";

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
                worksheet.Cells[1, 2].Value = "Produk";
                worksheet.Cells[1, 3].Value = "Varian";
                worksheet.Cells[1, 4].Value = "Bahan Baku";
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

                foreach (var itemKombinasiProduk in db.TBKombinasiProduks.OrderBy(item => item.Nama).ToArray())
                {
                    //No
                    worksheet.Cells[indexRow, 1].Value = nomor;
                    worksheet.Cells[indexRow, 1].Style.Numberformat.Format = "@";

                    //Produk
                    worksheet.Cells[indexRow, 2].Value = itemKombinasiProduk.TBProduk.Nama;
                    worksheet.Cells[indexRow, 2].Style.Numberformat.Format = "@";

                    //Varian
                    worksheet.Cells[indexRow, 3].Value = itemKombinasiProduk.TBAtributProduk.Nama;
                    worksheet.Cells[indexRow, 3].Style.Numberformat.Format = "@";

                    indexColumn = 7;

                    //Jenis Biaya Produksi
                    foreach (var itemJenisBiayaProduksi in daftarJenisBiayaProduksi)
                    {
                        var biayaProduksi = itemKombinasiProduk.TBRelasiJenisBiayaProduksiKombinasiProduks.FirstOrDefault(data => data.TBJenisBiayaProduksi == itemJenisBiayaProduksi);

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

                    foreach (var itemBahanBaku in itemKombinasiProduk.TBKomposisiKombinasiProduks.OrderBy(item => item.TBBahanBaku.Nama).ToArray())
                    {
                        //Bahan Baku
                        worksheet.Cells[indexRow, 4].Value = itemBahanBaku.TBBahanBaku.Nama;
                        worksheet.Cells[indexRow, 4].Style.Numberformat.Format = "@";

                        //Jumlah
                        worksheet.Cells[indexRow, 5].Value = itemBahanBaku.Jumlah.Value;

                        if (itemBahanBaku.Jumlah.ToFormatHarga().Contains(","))
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0.00";
                        else
                            worksheet.Cells[indexRow, 5].Style.Numberformat.Format = "#,##0";

                        //Satuan
                        worksheet.Cells[indexRow, 6].Value = itemBahanBaku.TBBahanBaku.TBSatuan.Nama;
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