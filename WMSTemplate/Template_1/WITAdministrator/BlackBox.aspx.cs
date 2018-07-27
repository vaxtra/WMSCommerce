using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RendFramework;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml;
using System.IO;

public partial class WITAdministrator_Pelanggan_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                LoadData();
            else
                LinkDownload.Visible = false;
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            DataDisplay DataDisplay = new DataDisplay();
            TBBlackBox[] ListData = db.TBBlackBoxes.OrderByDescending(item => item.Tanggal).ToArray();

            if (!string.IsNullOrEmpty(TextBoxCari.Text))
                ListData = ListData
                    .Where(item =>
                        item.Pesan.ToLower().Contains(TextBoxCari.Text.ToLower()) ||
                        item.Halaman.ToLower().Contains(TextBoxCari.Text.ToLower()))
                    .ToArray();

            int skip = 0;
            int take = 0;

            DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

            RepeaterLaporan.DataSource = ListData.Skip(skip).Take(take).ToArray();
            RepeaterLaporan.DataBind();
        }
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            string NamaWorksheet = "Black Box";

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
                worksheet.Cells[1, 2].Value = "Tanggal";
                worksheet.Cells[1, 3].Value = "Pesan";
                worksheet.Cells[1, 4].Value = "Halaman";

                int index = 2;

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    foreach (var item in db.TBBlackBoxes.OrderByDescending(item => item.Tanggal).ToArray())
                    {
                        //No
                        worksheet.Cells[index, 1].Value = index - 1;
                        worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                        //Tanggal
                        worksheet.Cells[index, 2].Value = item.Tanggal;
                        worksheet.Cells[index, 2].Style.Numberformat.Format = "d MMMM yyyy HH:mm";

                        //Pesan
                        worksheet.Cells[index, 3].Value = item.Pesan;
                        worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                        //Halaman
                        worksheet.Cells[index, 4].Value = item.Halaman;
                        worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                        index++;
                    }
                }

                using (var range = worksheet.Cells[1, 1, 1, 4])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                worksheet.Cells["B1:D1"].AutoFilter = true;

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
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void RepeaterLaporan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var BlackBox = db.TBBlackBoxes.FirstOrDefault(item => item.IDBlackBox == e.CommandArgument.ToInt());

                if (BlackBox != null)
                {
                    db.TBBlackBoxes.DeleteOnSubmit(BlackBox);
                    db.SubmitChanges();

                    LoadData();
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    #region DEFAULT
    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadData();
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
                LoadData();
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
                LoadData();
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    #endregion
}