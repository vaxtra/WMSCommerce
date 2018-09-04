using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RendFramework;

public partial class WITAdministrator_BahanBaku_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataBahanBaku(db);
                LoadDataSatuan(db);
                LoadDataKategori(db);
            }
        }
    }

    #region BAHAN BAKU
    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadDataBahanBaku(new DataClassesDatabaseDataContext());
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
                LoadDataBahanBaku(new DataClassesDatabaseDataContext());
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
                LoadDataBahanBaku(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    private void LoadDataBahanBaku(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        DataDisplay DataDisplay = new DataDisplay();

        var ListData = db.TBBahanBakus.Where(item => (!string.IsNullOrWhiteSpace(TextBoxCari.Text) ? item.Nama.ToLower().Contains(TextBoxCari.Text.ToLower()) : true))
        .Select(item => new
        {
            item.IDBahanBaku,
            item.Nama,
            item.KodeBahanBaku,
            Kategori = StokBahanBaku_Class.OutPutKategori(db, null, item),
            HargaBeli = item.TBStokBahanBakus.FirstOrDefault(data => data.IDTempat == Pengguna.IDTempat).HargaBeli * item.Konversi,
            SatuanBesar = item.TBSatuan1.Nama,
            item.Deskripsi
        }).OrderBy(item => item.Nama).ToArray();

        int skip = 0;
        int take = 0;
        int count = ListData.Count();

        DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

        RepeaterBahanBaku.DataSource = ListData.Skip(skip).Take(take);
        RepeaterBahanBaku.DataBind();
    }
    protected void RepeaterBahanBaku_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Hapus")
            {
                BahanBaku_Class.DeleteBahanBaku(db, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataBahanBaku(db);
            }
        }
    }

    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string NamaWorksheet = "Bahan Baku";

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
            worksheet.Cells[1, 4].Value = "Satuan Besar";
            worksheet.Cells[1, 5].Value = "Konversi";
            worksheet.Cells[1, 6].Value = "Satuan Kecil";
            worksheet.Cells[1, 7].Value = "Berat";
            worksheet.Cells[1, 8].Value = "Jumlah (Satuan Besar)";
            worksheet.Cells[1, 9].Value = "Harga Beli per Satuan Besar";
            worksheet.Cells[1, 10].Value = "Jumlah Minimum (Satuan Besar)";
            worksheet.Cells[1, 11].Value = "Kategori";
            worksheet.Cells[1, 12].Value = "Keterangan";

            int index = 2;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == Pengguna.IDTempat).ToArray();
                foreach (var item in db.TBBahanBakus.OrderBy(item => item.Nama).ToArray())
                {
                    TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

                    //No
                    worksheet.Cells[index, 1].Value = index - 1;
                    worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                    //Kode
                    worksheet.Cells[index, 2].Value = item.KodeBahanBaku;
                    worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                    //Bahan Baku
                    worksheet.Cells[index, 3].Value = item.Nama;
                    worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                    //Satuan Besar
                    worksheet.Cells[index, 4].Value = item.TBSatuan1.Nama;
                    worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                    //Konversi
                    worksheet.Cells[index, 5].Value = item.Konversi.Value;
                    if (item.Konversi.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 5].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 5].Style.Numberformat.Format = "#,##0";

                    //Satuan Kecil
                    worksheet.Cells[index, 6].Value = item.TBSatuan.Nama;
                    worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                    //Berat
                    worksheet.Cells[index, 7].Value = item.Berat.Value;
                    if (item.Berat.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 4].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 4].Style.Numberformat.Format = "#,##0";

                    //Jumlah
                    decimal jumlah = stokBahanBaku != null ? (stokBahanBaku.Jumlah.Value / stokBahanBaku.TBBahanBaku.Konversi.Value) : 0;
                    worksheet.Cells[index, 8].Value = jumlah;
                    if (jumlah.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0";

                    //Harga Beli
                    decimal hargaBeli = stokBahanBaku != null ? (stokBahanBaku.HargaBeli.Value * stokBahanBaku.TBBahanBaku.Konversi.Value) : 0;
                    worksheet.Cells[index, 9].Value = hargaBeli;
                    if (hargaBeli.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";

                    //Jumlah Minimum
                    decimal jumlahMinimum = stokBahanBaku != null ? (stokBahanBaku.JumlahMinimum.Value / stokBahanBaku.TBBahanBaku.Konversi.Value) : 0;
                    worksheet.Cells[index, 10].Value = jumlahMinimum;
                    if (jumlahMinimum.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0";

                    //Kategori
                    worksheet.Cells[index, 11].Value = item.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(db, null, item) : "";
                    worksheet.Cells[index, 11].Style.Numberformat.Format = "@";

                    //Keterangan
                    worksheet.Cells[index, 12].Value = item.Deskripsi;
                    worksheet.Cells[index, 12].Style.Numberformat.Format = "@";

                    index++;
                }
            }

            using (var range = worksheet.Cells[1, 1, 1, 12])
            {
                range.Style.Font.Bold = true;
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
    #endregion

    #region SATUAN
    protected void RepeaterSatuan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {
                TBSatuan satuan = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == e.CommandArgument.ToInt());
                HiddenFieldIDSatuan.Value = satuan.IDSatuan.ToString();
                TextBoxSatuanNama.Text = satuan.Nama;

                ButtonSimpanSatuan.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                Satuan_Class.DeleteSatuan(db, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataSatuan(db);
            }
        }
    }

    private void LoadDataSatuan(DataClassesDatabaseDataContext db)
    {
        RepeaterSatuan.DataSource = db.TBSatuans.OrderBy(item => item.Nama).ToArray();
        RepeaterSatuan.DataBind();
    }

    protected void ButtonSimpanSatuan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (ButtonSimpanSatuan.Text == "Tambah")
                    db.TBSatuans.InsertOnSubmit(new TBSatuan { Nama = TextBoxSatuanNama.Text });
                else if (ButtonSimpanSatuan.Text == "Ubah")
                {
                    TBSatuan satuan = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == HiddenFieldIDSatuan.Value.ToInt());
                    satuan.Nama = TextBoxSatuanNama.Text;
                }
                db.SubmitChanges();

                HiddenFieldIDSatuan.Value = null;
                TextBoxSatuanNama.Text = string.Empty;
                ButtonSimpanSatuan.Text = "Tambah";

                LoadDataSatuan(db);
            }
        }
    }
    #endregion

    #region KATEGORI BAHAN BAKU
    private void LoadDataKategori(DataClassesDatabaseDataContext db)
    {
        RepeaterKategoriBahanBaku.DataSource = db.TBKategoriBahanBakus.OrderBy(item => item.Nama).ToArray();
        RepeaterKategoriBahanBaku.DataBind();
    }
    protected void RepeaterKategoriBahanBaku_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {

                TBKategoriBahanBaku kategoriBahanBaku = db.TBKategoriBahanBakus.FirstOrDefault(item => item.IDKategoriBahanBaku == e.CommandArgument.ToInt());
                HiddenFieldIDKategoriBahanBaku.Value = kategoriBahanBaku.IDKategoriBahanBaku.ToString();
                TextBoxKetegoriBahanBakuNama.Text = kategoriBahanBaku.Nama;
                ButtonSimpanKategoriBahanBaku.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                KategoriBahanBaku_Class.DeleteKategoriBahanBaku(db, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataKategori(db);
            }
        }
    }

    protected void ButtonSimpanKategoriBahanBaku_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (ButtonSimpanKategoriBahanBaku.Text == "Tambah")
                db.TBKategoriBahanBakus.InsertOnSubmit(new TBKategoriBahanBaku { IDKategoriBahanBakuParent = null, Nama = TextBoxKetegoriBahanBakuNama.Text });
            else if (ButtonSimpanKategoriBahanBaku.Text == "Ubah")
            {
                TBKategoriBahanBaku kategoriBahanBaku = db.TBKategoriBahanBakus.FirstOrDefault(item => item.IDKategoriBahanBaku == HiddenFieldIDKategoriBahanBaku.Value.ToInt());
                kategoriBahanBaku.Nama = TextBoxKetegoriBahanBakuNama.Text;
            }
            db.SubmitChanges();

            HiddenFieldIDKategoriBahanBaku.Value = null;
            TextBoxKetegoriBahanBakuNama.Text = string.Empty;
            ButtonSimpanKategoriBahanBaku.Text = "Tambah";

            LoadDataKategori(db);
        }
    }
    #endregion
}