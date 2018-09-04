using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Stok_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                Warna_Class ClassWarna = new Warna_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListKategori.DataSource = db.TBKategoriProduks.ToArray();
                DropDownListKategori.DataTextField = "Nama";
                DropDownListKategori.DataValueField = "IDKategoriProduk";
                DropDownListKategori.DataBind();
                DropDownListKategori.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListPemilik.DataSource = ClassPemilikProduk.Data();
                DropDownListPemilik.DataTextField = "Nama";
                DropDownListPemilik.DataValueField = "IDPemilikProduk";
                DropDownListPemilik.DataBind();
                DropDownListPemilik.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListVarian.DataSource = ClassAtributProduk.Data();
                DropDownListVarian.DataTextField = "Nama";
                DropDownListVarian.DataValueField = "IDAtributProduk";
                DropDownListVarian.DataBind();
                DropDownListVarian.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListWarna.DataSource = ClassWarna.Data();
                DropDownListWarna.DataTextField = "Nama";
                DropDownListWarna.DataValueField = "IDWarna";
                DropDownListWarna.DataBind();
                DropDownListWarna.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });
                DropDownListJenisStok.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                DropDownListJenisStok.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                DropDownListJenisStok.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                LoadData();
            }
        }
        else
            LinkDownload.Visible = false;
    }


    private void LoadData()
    {
        var stokProduk = LoadStokProduk();
    }

    private IQueryable<dynamic> LoadStokProduk()
    {
        DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext();

        string _tempPencarian = string.Empty;
        _tempPencarian = "?IDTempat=" + DropDownListTempat.SelectedValue;
        _tempPencarian += "&IDJenisStok=" + DropDownListJenisStok.SelectedValue;
        _tempPencarian += "&Kode=" + TextBoxKode.Text;
        _tempPencarian += "&Produk=" + TextBoxProduk.Text;
        _tempPencarian += "&IDWarna=" + DropDownListWarna.SelectedValue;
        _tempPencarian += "&HargaJual=" + TextBoxHargaJual.Text;
        _tempPencarian += "&Stok=" + TextBoxStok.Text;
        _tempPencarian += "&IDAtribut=" + DropDownListVarian.SelectedValue;
        _tempPencarian += "&IDKategori=" + DropDownListKategori.SelectedValue;

        var _dataStok = db.TBStokProduks
            .Where(item => (item.TBKombinasiProduk.TBProduk._IsActive) &&
                (DropDownListTempat.SelectedValue != "0" ? item.IDTempat == DropDownListTempat.SelectedValue.ToInt() : true) &&
                (DropDownListJenisStok.SelectedValue != "0" ? (DropDownListJenisStok.SelectedValue != "1" ? (DropDownListJenisStok.SelectedValue != "2" ? item.Jumlah < 0 : item.Jumlah == 0) : item.Jumlah > 0) : true) &&
                (!string.IsNullOrWhiteSpace(TextBoxKode.Text) ? item.TBKombinasiProduk.KodeKombinasiProduk.Contains(TextBoxKode.Text) : true) &&
                (!string.IsNullOrWhiteSpace(TextBoxProduk.Text) ? item.TBKombinasiProduk.TBProduk.Nama.Contains(TextBoxProduk.Text) : true) &&
                (!string.IsNullOrWhiteSpace(TextBoxProduk.Text) ? item.TBKombinasiProduk.TBProduk.Nama.Contains(TextBoxProduk.Text) : true) &&
                (DropDownListWarna.SelectedValue != "0" ? item.TBKombinasiProduk.TBProduk.IDWarna == DropDownListWarna.SelectedValue.ToInt() : true) &&
                (!string.IsNullOrWhiteSpace(TextBoxHargaJual.Text) ? item.HargaJual == TextBoxHargaJual.Text.ToDecimal() : true) &&
                (!string.IsNullOrWhiteSpace(TextBoxStok.Text) ? item.Jumlah == TextBoxStok.Text.ToInt() : true) &&
                (DropDownListPemilik.SelectedValue != "0" ? item.TBKombinasiProduk.TBProduk.IDPemilikProduk == DropDownListPemilik.SelectedValue.ToInt() : true) &&
                (DropDownListVarian.SelectedValue != "0" ? item.TBKombinasiProduk.IDAtributProduk == DropDownListVarian.SelectedValue.ToInt() : true) &&
                (DropDownListKategori.SelectedValue != "0" ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(item2 => item2.IDKategoriProduk == DropDownListKategori.SelectedValue.ToInt()) != null : true))
            .GroupBy(item => new
            {
                item.TBKombinasiProduk.TBProduk
            })
            .Select(item => new
            {
                Produk = item.Key.TBProduk.Nama,
                Warna = item.Key.TBProduk.TBWarna.Nama,
                PemilikProduk = item.Key.TBProduk.TBPemilikProduk.Nama,
                Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.Key.TBProduk.TBKombinasiProduks.FirstOrDefault()),
                Body = item.Select(item2 => new
                {
                    IDKombinasiProduk = item2.IDKombinasiProduk,
                    Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                    AtributProduk = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                    HargaJual = item2.HargaJual.Value,
                    Jumlah = item2.Jumlah.Value
                }),
                Jumlah = item.Sum(item2 => item2.Jumlah),
                Subtotal = item.Sum(item2 => item2.Jumlah * item2.HargaJual),
                Count = item.Count()
            }).OrderBy(item => item.Produk);

        ButtonPrint.OnClientClick = "return popitup('ProdukPrint.aspx" + _tempPencarian + "')";

        RepeaterProduk.DataSource = _dataStok;
        RepeaterProduk.DataBind();

        LabelTotalJumlah.Text = _dataStok.Sum(item => item.Jumlah).ToFormatHargaBulat();
        LabelTotalNominal.Text = _dataStok.Sum(item => item.Subtotal).ToFormatHarga();

        return _dataStok;
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string _namaFile = "Laporan Stok Produk " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";
        string _path = Server.MapPath("~/file_excel/Stok Produk/Export/") + _namaFile;
        string _namaWorksheet = "Stok Produk";
        string _judul = "Laporan Stok Produk " + Pengguna.Store + " - " + Pengguna.Tempat + " " + DateTime.Now.ToString("d MMMM yyyy");

        FileInfo newFile = new FileInfo(_path);

        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_namaWorksheet);

            worksheet.Cells[1, 1].Value = "No.";
            worksheet.Cells[1, 2].Value = "Produk";
            worksheet.Cells[1, 3].Value = "Warna";
            worksheet.Cells[1, 4].Value = "Brand";
            worksheet.Cells[1, 5].Value = "Kategori";
            worksheet.Cells[1, 6].Value = "Kode";
            worksheet.Cells[1, 7].Value = "Varian";
            worksheet.Cells[1, 8].Value = "Harga Jual";
            worksheet.Cells[1, 9].Value = "Jumlah";

            int index = 2;
            int indexProduk = 1;
            int indexVarian = 1;

            if (LoadStokProduk() != null)
            {
                foreach (var item in LoadStokProduk())
                {
                    indexVarian = 1;

                    worksheet.Cells[index, 1].Value = indexProduk;
                    worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                    worksheet.Cells[index, 2].Value = item.Produk;
                    worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                    worksheet.Cells[index, 3].Value = item.Warna;
                    worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                    worksheet.Cells[index, 4].Value = item.PemilikProduk;
                    worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                    worksheet.Cells[index, 5].Value = item.Kategori;
                    worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                    foreach (var item2 in item.Stok)
                    {
                        if (indexVarian > 1)
                        {
                            worksheet.Cells[index, 1].Value = indexProduk;
                            worksheet.Cells[index, 1].Style.Numberformat.Format = "@";
                            worksheet.Cells[index, 1].Style.Font.Color.SetColor(Color.White);

                            worksheet.Cells[index, 2].Value = item.Produk;
                            worksheet.Cells[index, 2].Style.Numberformat.Format = "@";
                            worksheet.Cells[index, 2].Style.Font.Color.SetColor(Color.White);

                            worksheet.Cells[index, 3].Value = item.Warna;
                            worksheet.Cells[index, 3].Style.Numberformat.Format = "@";
                            worksheet.Cells[index, 3].Style.Font.Color.SetColor(Color.White);

                            worksheet.Cells[index, 4].Value = item.PemilikProduk;
                            worksheet.Cells[index, 4].Style.Numberformat.Format = "@";
                            worksheet.Cells[index, 4].Style.Font.Color.SetColor(Color.White);

                            worksheet.Cells[index, 5].Value = item.Kategori;
                            worksheet.Cells[index, 5].Style.Numberformat.Format = "@";
                            worksheet.Cells[index, 5].Style.Font.Color.SetColor(Color.White);
                        }

                        indexVarian++;

                        worksheet.Cells[index, 6].Value = item2.Kode;
                        worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 7].Value = item2.Atribut;
                        worksheet.Cells[index, 7].Style.Numberformat.Format = "@";

                        worksheet.Cells[index, 8].Value = item2.HargaJual;
                        worksheet.Cells[index, 9].Value = item2.Jumlah;

                        worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";

                        index++;
                    }

                    indexProduk++;
                }
            }

            using (var range = worksheet.Cells[1, 1, 1, 9])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                range.Style.Font.Color.SetColor(Color.White);
            }

            worksheet.Cells["B1:I1"].AutoFilter = true;

            worksheet.Cells.AutoFitColumns(0);

            worksheet.HeaderFooter.OddHeader.CenteredText = "&16&\"Tahoma,Regular Bold\"" + _judul;
            worksheet.HeaderFooter.OddFooter.LeftAlignedText = "Print : " + Pengguna.NamaLengkap + " - " + Pengguna.Tempat + " - " + DateTime.Now.ToString("d MMMM yyyy hh:mm");

            worksheet.HeaderFooter.OddFooter.RightAlignedText = "WIT. Warehouse Management System - " + string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

            package.Workbook.Properties.Title = _namaWorksheet;
            package.Workbook.Properties.Author = "WIT. Warehouse Management System";
            package.Workbook.Properties.Comments = _judul;

            package.Workbook.Properties.Company = "WIT. Warehouse Management System";
            package.Save();
        }

        LinkDownload.Visible = true;
        LinkDownload.HRef = "/file_excel/Stok Produk/Export/" + _namaFile;
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
}