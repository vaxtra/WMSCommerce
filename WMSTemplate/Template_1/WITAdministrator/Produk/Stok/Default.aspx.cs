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
                DropDownListKategori.Items.Insert(0, new ListItem { Value = "-1", Text = "- Semua -" });
                DropDownListKategori.Items.Insert(1, new ListItem { Value = "0", Text = " " });

                DropDownListPemilik.DataSource = ClassPemilikProduk.Data();
                DropDownListPemilik.DataTextField = "Nama";
                DropDownListPemilik.DataValueField = "IDPemilikProduk";
                DropDownListPemilik.DataBind();
                DropDownListPemilik.Items.Insert(0, new ListItem { Value = "-1", Text = "- Semua -" });

                DropDownListVarian.DataSource = ClassAtributProduk.Data();
                DropDownListVarian.DataTextField = "Nama";
                DropDownListVarian.DataValueField = "IDAtributProduk";
                DropDownListVarian.DataBind();
                DropDownListVarian.Items.Insert(0, new ListItem { Value = "-1", Text = "- Semua -" });

                DropDownListWarna.DataSource = ClassWarna.Data();
                DropDownListWarna.DataTextField = "Nama";
                DropDownListWarna.DataValueField = "IDWarna";
                DropDownListWarna.DataBind();
                DropDownListWarna.Items.Insert(0, new ListItem { Value = "-1", Text = "- Semua -" });

                DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "Semua" });
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

        #region TEMPAT
        _tempPencarian = "?IDTempat=" + DropDownListTempat.SelectedValue;

        IQueryable<TBStokProduk> _stokProduk;

        if (DropDownListTempat.SelectedValue == "0")
            _stokProduk = db.TBStokProduks.Where(item => item.TBKombinasiProduk.TBProduk._IsActive);
        else
        {
            _stokProduk = db.TBStokProduks
                .Where(item =>
                    item.IDTempat == DropDownListTempat.SelectedValue.ToInt() &&
                    item.TBKombinasiProduk.TBProduk._IsActive);
        }
        #endregion

        #region STATUS STOK
        _tempPencarian += "&IDJenisStok=" + DropDownListJenisStok.SelectedValue;

        if (DropDownListJenisStok.SelectedValue == "1")
            _stokProduk = _stokProduk.Where(item => item.Jumlah > 0);
        else if (DropDownListJenisStok.SelectedValue == "2")
            _stokProduk = _stokProduk.Where(item => item.Jumlah == 0);
        else if (DropDownListJenisStok.SelectedValue == "3")
            _stokProduk = _stokProduk.Where(item => item.Jumlah < 0);
        #endregion

        #region KODE
        if (!string.IsNullOrWhiteSpace(TextBoxKode.Text))
        {
            _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(TextBoxKode.Text));
            TextBoxKode.Focus();

            _tempPencarian += "&Kode=" + TextBoxKode.Text;
        }
        #endregion

        #region PRODUK
        if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
        {
            _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.Nama.Contains(TextBoxProduk.Text));
            TextBoxProduk.Focus();

            _tempPencarian += "&Produk=" + TextBoxProduk.Text;
        }
        #endregion

        #region WARNA
        if (DropDownListWarna.SelectedValue != "-1")
        {
            _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDWarna == DropDownListWarna.SelectedValue.ToInt());

            TextBoxProduk.Focus();
            _tempPencarian += "&IDWarna=" + DropDownListWarna.SelectedValue;
        }
        #endregion

        #region HARGA JUAL
        if (!string.IsNullOrWhiteSpace(TextBoxHargaJual.Text))
        {
            if (TextBoxHargaJual.Text.Contains("-"))
            {
                string[] _angka = TextBoxHargaJual.Text.Split('-');
                _stokProduk = _stokProduk.Where(item => item.HargaJual >= _angka[0].ToDecimal() && item.HargaJual <= _angka[1].ToDecimal()).OrderBy(item => item.HargaJual);
            }
            else
                _stokProduk = _stokProduk.Where(item => item.HargaJual == TextBoxHargaJual.Text.ToDecimal());

            TextBoxHargaJual.Focus();
            _tempPencarian += "&HargaJual=" + TextBoxHargaJual.Text;
        }
        #endregion

        #region STOK PRODUK
        if (!string.IsNullOrWhiteSpace(TextBoxStok.Text))
        {
            if (TextBoxStok.Text.Contains("-"))
            {
                string[] _angka = TextBoxStok.Text.Split('-');
                _stokProduk = _stokProduk.Where(item => item.Jumlah >= _angka[0].ToInt() && item.Jumlah <= _angka[1].ToInt()).OrderBy(item => item.Jumlah);
            }
            else
                _stokProduk = _stokProduk.Where(item => item.Jumlah == TextBoxStok.Text.ToInt());

            TextBoxStok.Focus();

            _tempPencarian += "&Stok=" + TextBoxStok.Text;
        }
        #endregion

        #region PEMILIK PRODUK
        if (DropDownListPemilik.SelectedValue != "-1")
        {
            _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == DropDownListPemilik.SelectedValue.ToInt());

            TextBoxProduk.Focus();
            _tempPencarian += "&IDPemilikProduk=" + DropDownListPemilik.SelectedValue;
        }
        #endregion

        #region ATRIBUT
        if (DropDownListVarian.SelectedValue != "-1")
        {
            _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == DropDownListVarian.SelectedValue.ToInt());

            TextBoxProduk.Focus();
            _tempPencarian += "&IDAtribut=" + DropDownListVarian.SelectedValue;
        }
        #endregion

        #region KATEGORI
        if (DropDownListKategori.SelectedValue != "-1")
        {
            if (DropDownListKategori.SelectedValue == "0")
                _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0);
            else
            {
                _stokProduk = _stokProduk.Where(item =>
                    item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 &&
                    item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == int.Parse(DropDownListKategori.SelectedValue));
            }

            TextBoxProduk.Focus();
            _tempPencarian += "&IDKategori=" + DropDownListKategori.SelectedValue;
        }
        #endregion

        var _dataStok = _stokProduk.Select(item => new
        {
            IDProduk = item.TBKombinasiProduk.TBProduk.IDProduk,
            IDKombinasiProduk = item.IDKombinasiProduk,
            Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
            Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
            HargaJual = item.HargaJual.Value,
            Jumlah = item.Jumlah.Value
        });

        ButtonPrint.OnClientClick = "return popitup('ProdukPrint.aspx" + _tempPencarian + "')";

        if (_dataStok.Count() > 0)
        {
            var _dataProduk = _dataStok.Select(item => item.IDProduk).Distinct();

            var _produk = db.TBProduks
                .Where(item => _dataProduk.Any(item2 => item2 == item.IDProduk))
                .Select(item => new
                {
                    ID = item.IDProduk,
                    Produk = item.Nama,
                    Kategori = (item.TBRelasiProdukKategoriProduks.Count > 0) ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                    PemilikProduk = item.TBPemilikProduk.Nama,
                    Warna = item.TBWarna.Nama,
                    Stok = _dataStok.Where(item2 => item2.IDProduk == item.IDProduk)
                }).OrderBy(item => item.Produk);

            RepeaterProduk.DataSource = _produk;
            RepeaterProduk.DataBind();

            LabelTotalJumlah.Text = _dataStok.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelTotalNominal.Text = _dataStok.ToArray().Sum(item => item.Jumlah * item.HargaJual).ToFormatHarga();

            return _produk;
        }
        else
        {
            RepeaterProduk.DataSource = null;
            RepeaterProduk.DataBind();

            LabelTotalJumlah.Text = "0";
            LabelTotalNominal.Text = "0";

            return null;
        }
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