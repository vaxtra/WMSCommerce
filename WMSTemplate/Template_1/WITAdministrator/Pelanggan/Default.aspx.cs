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
            {
                LoadDataPelanggan();
                LoadDataGrup();
            }
            else
                LinkDownload.Visible = false;
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    #region PELANGGAN
    private void LoadDataPelanggan()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
            DataDisplay DataDisplay = new DataDisplay();

            if (!string.IsNullOrWhiteSpace(TextBoxCari.Text))
            {
                var ListData = db.TBPelanggans.Where(item => item.IDPelanggan != (int)EnumPelanggan.GeneralCustomer && item.NamaLengkap.ToLower().Contains(TextBoxCari.Text.ToLower())).OrderBy(item => item.NamaLengkap).Select(item => new
                {
                    item.IDPelanggan,
                    Grup = item.TBGrupPelanggan.Nama,
                    item.NamaLengkap,
                    item.Email,
                    item.Handphone,
                    item.Deposit,
                    Status = item._IsActive
                }).ToArray();
                int skip = 0;
                int take = 0;

                DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

                RepeaterPelanggan.DataSource = ListData.Skip(skip).Take(take).ToArray();
                RepeaterPelanggan.DataBind();
            }
            else
            {
                var ListData = db.TBPelanggans.Where(item => item.IDPelanggan != (int)EnumPelanggan.GeneralCustomer).OrderBy(item => item.NamaLengkap).Select(item => new
                {
                    item.IDPelanggan,
                    Grup = item.TBGrupPelanggan.Nama,
                    item.NamaLengkap,
                    item.Email,
                    item.Handphone,
                    item.Deposit,
                    Status = item._IsActive
                }).ToArray(); ;

                int skip = 0;
                int take = 0;
                int count = ListData.Count();

                DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

                RepeaterPelanggan.DataSource = ListData.Skip(skip).Take(take).ToArray();
                RepeaterPelanggan.DataBind();
            }
        }
    }

    protected void RepeaterPelanggan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                if (e.CommandName == "Hapus")
                    ClassPelanggan.Hapus(e.CommandArgument.ToInt());
                else if (e.CommandName == "UbahStatus")
                    ClassPelanggan.UbahStatus(e.CommandArgument.ToInt());

                LoadDataPelanggan();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadDataPelanggan();
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
                LoadDataPelanggan();
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
                LoadDataPelanggan();
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                string NamaWorksheet = "Data Pelanggan";

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
                    worksheet.Cells[1, 2].Value = "Grup";
                    worksheet.Cells[1, 3].Value = "Nama";
                    worksheet.Cells[1, 4].Value = "Email";
                    worksheet.Cells[1, 5].Value = "Phone";
                    worksheet.Cells[1, 6].Value = "Deposit";
                    worksheet.Cells[1, 7].Value = "Status";

                    int index = 2;

                    foreach (var item in db.TBPelanggans.Where(item => item.IDPelanggan != 1).ToArray())
                    {
                        worksheet.Cells[index, 1].Value = index - 1;
                        worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 2].Value = item.TBGrupPelanggan.Nama;
                        worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 3].Value = item.NamaLengkap;
                        worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 4].Value = item.Email;
                        worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 5].Value = item.Handphone;
                        worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 6].Value = item.Deposit;
                        worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 7].Value = item._IsActive;
                        worksheet.Cells[index, 7].Style.Numberformat.Format = "@";

                        index++;
                    }

                    using (var range = worksheet.Cells[1, 1, 1, 7])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                        range.Style.Font.Color.SetColor(Color.White);
                    }

                    worksheet.Cells[1, 1, 1, 7].AutoFilter = true;

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

                ButtonExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    #endregion

    #region GRUP PELANGGAN
    private void LoadDataGrup()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

            RepeaterGrupPelanggan.DataSource = GrupPelanggan_Class.Data(db);
            RepeaterGrupPelanggan.DataBind();
        }
    }

    protected void ButtonSimpanGrup_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (ButtonSimpanGrup.Text == "Tambah")
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                db.TBGrupPelanggans.InsertOnSubmit(new TBGrupPelanggan
                {
                    Nama = TextBoxNama.Text,
                    EnumBonusGrupPelanggan = DropDownListBonusGrupPelanggan.SelectedValue.ToInt(),
                    Persentase = TextBoxPersentase.Text.ToDecimal(),
                    LimitTransaksi = 0,
                    _IDWMSStore = Pengguna.IDWMSStore,
                    _IDWMS = Guid.NewGuid(),
                    _Urutan = db.TBGrupPelanggans.Count() + 1,
                    _TanggalInsert = DateTime.Now,
                    _IDTempatInsert = Pengguna.IDTempat,
                    _IDPenggunaInsert = Pengguna.IDPengguna,
                    _TanggalUpdate = DateTime.Now,
                    _IDTempatUpdate = Pengguna.IDTempat,
                    _IDPenggunaUpdate = Pengguna.IDPengguna,
                    _IsActive = true,
                });
            }
            else if (ButtonSimpanGrup.Text == "Ubah")
            {
                TBGrupPelanggan grupPelanggan = db.TBGrupPelanggans.FirstOrDefault(item => item.IDGrupPelanggan == HiddenFieldIDGrupPelanggan.Value.ToInt());
                grupPelanggan.Nama = TextBoxNama.Text;
                grupPelanggan.EnumBonusGrupPelanggan = DropDownListBonusGrupPelanggan.SelectedValue.ToInt();
                grupPelanggan.Persentase = TextBoxPersentase.Text.ToDecimal();
                grupPelanggan.LimitTransaksi = 0;
            }
            db.SubmitChanges();

            HiddenFieldIDGrupPelanggan.Value = null;
            TextBoxNama.Text = string.Empty;
            TextBoxPersentase.Text = "0";
            DropDownListBonusGrupPelanggan.SelectedValue = "1";

            ButtonSimpanGrup.Text = "Tambah";
        }

        LoadDataGrup();
    }

    protected void RepeaterGrupPelanggan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBGrupPelanggan grupPelanggan = db.TBGrupPelanggans.FirstOrDefault(item => item.IDGrupPelanggan == e.CommandArgument.ToInt());

                HiddenFieldIDGrupPelanggan.Value = grupPelanggan.IDGrupPelanggan.ToString();
                TextBoxNama.Text = grupPelanggan.Nama;
                TextBoxPersentase.Text = grupPelanggan.Persentase.ToString();
                DropDownListBonusGrupPelanggan.SelectedValue = grupPelanggan.EnumBonusGrupPelanggan.ToString();

                ButtonSimpanGrup.Text = "Ubah";
            }
        }
        else if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

                if (GrupPelanggan_Class.Hapus(e.CommandArgument.ToInt()))
                {
                    LoadDataGrup();
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Grup Pelanggan dihapus");
                }
                else
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Grup Pelanggan tidak bisa dihapus");
            }
        }
    }
    #endregion
}