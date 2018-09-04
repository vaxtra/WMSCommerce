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
using RendFramework;

public partial class WITAdministrator_Produk_Default : System.Web.UI.Page
{
    public int Skip { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataProduk(db);
                LoadDataPemilikProduk(db);
                LoadDataAtributProduk(db);
                LoadDataWarna(db);
                LoadDataKategori(db);
            }
        }
        else
            LinkDownload.Visible = false;
    }

    #region PRODUK
    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadDataProduk(new DataClassesDatabaseDataContext());
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
                LoadDataProduk(new DataClassesDatabaseDataContext());
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
                LoadDataProduk(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    private void LoadDataProduk(DataClassesDatabaseDataContext db)
    {
        DataDisplay DataDisplay = new DataDisplay();

        var ListData = db.TBProduks
            .AsEnumerable()
            .Select(item => new
            {
                item.IDProduk,
                item.Nama,
                KombinasiProduk = item.TBKombinasiProduks.Select(item2 => new
                {
                    item2.IDKombinasiProduk,
                    item2.KodeKombinasiProduk,
                    item2.Nama,
                    Atribut = !string.IsNullOrWhiteSpace(item2.TBAtributProduk.Nama) ? item2.TBAtributProduk.Nama : item.Nama
                }),
                Kategori = item.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : string.Empty,
                Status = item._IsActive,
            })
            .OrderByDescending(item => item.IDProduk)
            .ToArray();

        if (!string.IsNullOrWhiteSpace(TextBoxCari.Text))
            ListData = ListData.Where(item => item.Nama.ToLower().Contains(TextBoxCari.Text.ToLower())).ToArray();

        int skip = 0;
        int take = 0;
        int count = ListData.Count();

        DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

        RepeaterProduk.DataSource = ListData.Skip(skip).Take(take);
        RepeaterProduk.DataBind();
    }
    protected void RepeaterProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "UbahStatus")
            {
                Produk_Class ClassProduk = new Produk_Class(db);

                ClassProduk.UbahStatus(e.CommandArgument.ToInt());

                db.SubmitChanges();

                LoadDataProduk(db);
            }
        }
    }

    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string NamaWorksheet = "Produk";

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
            worksheet.Cells[1, 3].Value = "Brand";
            worksheet.Cells[1, 4].Value = "Produk";
            worksheet.Cells[1, 5].Value = "Varian";
            worksheet.Cells[1, 6].Value = "Warna";
            worksheet.Cells[1, 7].Value = "Berat";
            worksheet.Cells[1, 8].Value = "Jumlah";
            worksheet.Cells[1, 9].Value = "Harga Beli";
            worksheet.Cells[1, 10].Value = "Harga Jual";
            worksheet.Cells[1, 11].Value = "Kategori";
            worksheet.Cells[1, 12].Value = "Keterangan";

            int index = 2;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                foreach (var item in db.TBStokProduks.Where(item => item.IDTempat == Pengguna.IDTempat).OrderBy(item => item.TBKombinasiProduk.TBProduk.Nama).ThenBy(item => item.TBKombinasiProduk.TBAtributProduk.IDAtributProduk).ToArray())
                {
                    //No
                    worksheet.Cells[index, 1].Value = index - 1;
                    worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                    //Kode
                    worksheet.Cells[index, 2].Value = item.TBKombinasiProduk.KodeKombinasiProduk;
                    worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                    //Brand
                    worksheet.Cells[index, 3].Value = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama;
                    worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                    //Produk
                    worksheet.Cells[index, 4].Value = item.TBKombinasiProduk.TBProduk.Nama;
                    worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                    //Varian
                    worksheet.Cells[index, 5].Value = item.TBKombinasiProduk.TBAtributProduk.Nama;
                    worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                    //Warna
                    worksheet.Cells[index, 6].Value = item.TBKombinasiProduk.TBProduk.TBWarna.Nama;
                    worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                    //Berat
                    worksheet.Cells[index, 7].Value = item.TBKombinasiProduk.Berat;

                    if (item.TBKombinasiProduk.Berat.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 7].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 7].Style.Numberformat.Format = "#,##0";

                    //Jumlah
                    worksheet.Cells[index, 8].Value = item.Jumlah;

                    worksheet.Cells[index, 8].Style.Numberformat.Format = "#,##0";

                    //Harga Beli
                    worksheet.Cells[index, 9].Value = item.HargaBeli;

                    if (item.HargaBeli.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";

                    //Harga Jual
                    worksheet.Cells[index, 10].Value = item.HargaJual;

                    if (item.HargaJual.ToFormatHarga().Contains(","))
                        worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0.00";
                    else
                        worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0";

                    //Kategori
                    string Kategori = "";

                    if (item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0)
                    {
                        foreach (var item2 in item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.ToArray())
                        {
                            Kategori += item2.TBKategoriProduk.Nama + ", ";
                        }

                        Kategori = Kategori.Substring(0, Kategori.Length - 2);
                    }

                    worksheet.Cells[index, 11].Value = Kategori;
                    worksheet.Cells[index, 11].Style.Numberformat.Format = "@";

                    //Keterangan
                    worksheet.Cells[index, 12].Value = item.TBKombinasiProduk.Deskripsi;
                    worksheet.Cells[index, 12].Style.Numberformat.Format = "@";

                    index++;
                }
            }

            using (var range = worksheet.Cells[1, 1, 1, 12])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                range.Style.Font.Color.SetColor(Color.White);
            }

            worksheet.Cells["B1:L1"].AutoFilter = true;

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
    #endregion

    #region PEMILIK PRODUK
    private void LoadDataPemilikProduk(DataClassesDatabaseDataContext db)
    {
        RepeaterPemilikProduk.DataSource = db.TBPemilikProduks.OrderBy(item => item.Nama).ToArray();
        RepeaterPemilikProduk.DataBind();
    }
    protected void RepeaterPemilikProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {

                TBPemilikProduk PemilikProduk = db.TBPemilikProduks.FirstOrDefault(item => item.IDPemilikProduk == e.CommandArgument.ToInt());

                HiddenFieldIDPemilikProduk.Value = PemilikProduk.IDPemilikProduk.ToString();
                TextBoxNamaPemilikProduk.Text = PemilikProduk.Nama;
                TextBoxAlamatPemilikProduk.Text = PemilikProduk.Alamat;
                TextBoxEmailPemilikProduk.Text = PemilikProduk.Email;
                TextBoxTelepon1PemilikProduk.Text = PemilikProduk.Telepon1;
                TextBoxTelepon2PemilikProduk.Text = PemilikProduk.Telepon2;

                ButtonSimpanPemilikProduk.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                PemilikProduk_Class pemilikProduk = new PemilikProduk_Class(db);
                pemilikProduk.Hapus(e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataPemilikProduk(db);
            }
        }
    }

    protected void ButtonSimpanPemilikProduk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
                TBPemilikProduk PemilikProduk = null;

                if (ButtonSimpanPemilikProduk.Text == "Tambah")
                {
                    ClassPemilikProduk.Tambah(TextBoxNamaPemilikProduk.Text, TextBoxAlamatPemilikProduk.Text, TextBoxEmailPemilikProduk.Text, TextBoxTelepon1PemilikProduk.Text, TextBoxTelepon2PemilikProduk.Text);
                }
                else if (ButtonSimpanPemilikProduk.Text == "Ubah")
                {
                    PemilikProduk = db.TBPemilikProduks.FirstOrDefault(item => item.IDPemilikProduk == HiddenFieldIDPemilikProduk.Value.ToInt());
                    PemilikProduk.Nama = TextBoxNamaPemilikProduk.Text;
                    PemilikProduk.Alamat = TextBoxAlamatPemilikProduk.Text;
                    PemilikProduk.Email = TextBoxEmailPemilikProduk.Text;
                    PemilikProduk.Telepon1 = TextBoxTelepon1PemilikProduk.Text;
                    PemilikProduk.Telepon2 = TextBoxTelepon2PemilikProduk.Text;
                }

                db.SubmitChanges();

                HiddenFieldIDPemilikProduk.Value = null;
                TextBoxNamaPemilikProduk.Text = string.Empty;
                TextBoxAlamatPemilikProduk.Text = string.Empty;
                TextBoxEmailPemilikProduk.Text = string.Empty;
                TextBoxTelepon1PemilikProduk.Text = string.Empty;
                TextBoxTelepon2PemilikProduk.Text = string.Empty;
                ButtonSimpanPemilikProduk.Text = "Tambah";

                LoadDataPemilikProduk(db);
            }
        }
    }
    #endregion

    #region WARNA
    protected void RepeaterWarna_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Warna_Class ClassWarna = new Warna_Class(db);

            if (e.CommandName == "Ubah")
            {
                var Warna = ClassWarna.Cari(e.CommandArgument.ToInt());

                if (Warna != null)
                {
                    HiddenFieldIDWarna.Value = e.CommandArgument.ToString();
                    TextBoxNamaWarna.Text = Warna.Nama;

                    ButtonSimpanWarna.Text = "Ubah";
                }
            }
            else if (e.CommandName == "Hapus")
            {
                if (ClassWarna.Hapus(e.CommandArgument.ToInt()))
                {
                    db.SubmitChanges();
                    LoadDataWarna(db);
                }
            }
        }
    }

    private void LoadDataWarna(DataClassesDatabaseDataContext db)
    {
        Warna_Class ClassWarna = new Warna_Class(db);

        RepeaterWarna.DataSource = ClassWarna.Data();
        RepeaterWarna.DataBind();
    }

    protected void ButtonSimpanWarna_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Warna_Class ClassWarna = new Warna_Class(db);

                if (ButtonSimpanWarna.Text == "Tambah")
                    ClassWarna.Tambah(TextBoxKodeWarna.Text, TextBoxNamaWarna.Text);
                else if (ButtonSimpanWarna.Text == "Ubah")
                    ClassWarna.Ubah(HiddenFieldIDWarna.Value.ToInt(), TextBoxKodeWarna.Text, TextBoxNamaWarna.Text);

                db.SubmitChanges();

                HiddenFieldIDWarna.Value = null;
                TextBoxKodeWarna.Text = string.Empty;
                TextBoxNamaWarna.Text = string.Empty;
                ButtonSimpanWarna.Text = "Tambah";

                LoadDataWarna(db);
            }
        }
    }
    #endregion

    #region ATRIBUT PRODUK
    protected void RepeaterAtributProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

            if (e.CommandName == "Ubah")
            {
                var AtributProduk = ClassAtributProduk.Cari(e.CommandArgument.ToInt());

                if (AtributProduk != null)
                {
                    HiddenFieldIDAtributProduk.Value = e.CommandArgument.ToString();
                    TextBoxNamaAtributProduk.Text = AtributProduk.Nama;

                    ButtonSimpanAtributProduk.Text = "Ubah";
                }
            }
            else if (e.CommandName == "Hapus")
            {
                if (ClassAtributProduk.Hapus(e.CommandArgument.ToInt()))
                {
                    db.SubmitChanges();
                    LoadDataAtributProduk(db);
                }
            }
        }
    }

    private void LoadDataAtributProduk(DataClassesDatabaseDataContext db)
    {
        AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

        RepeaterAtributProduk.DataSource = ClassAtributProduk.Data();
        RepeaterAtributProduk.DataBind();
    }

    protected void ButtonSimpanAtributProduk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

                if (ButtonSimpanAtributProduk.Text == "Tambah")
                    ClassAtributProduk.CariTambah("", TextBoxNamaAtributProduk.Text);
                else if (ButtonSimpanAtributProduk.Text == "Ubah")
                    ClassAtributProduk.Ubah(HiddenFieldIDAtributProduk.Value.ToInt(), TextBoxNamaAtributProduk.Text);

                db.SubmitChanges();

                HiddenFieldIDAtributProduk.Value = null;
                TextBoxNamaAtributProduk.Text = string.Empty;
                ButtonSimpanAtributProduk.Text = "Tambah";

                LoadDataAtributProduk(db);
            }
        }
    }
    #endregion

    #region KATEGORI PRODUK
    private void LoadDataKategori(DataClassesDatabaseDataContext db)
    {
        RepeaterKategoriProduk.DataSource = db.TBKategoriProduks.OrderBy(item => item.Nama).ToArray();
        RepeaterKategoriProduk.DataBind();
    }
    protected void RepeaterKategoriProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {

                TBKategoriProduk kategoriProduk = db.TBKategoriProduks.FirstOrDefault(item => item.IDKategoriProduk == e.CommandArgument.ToString().ToInt());
                HiddenFieldIDKategoriProduk.Value = kategoriProduk.IDKategoriProduk.ToString();
                TextBoxKetegoriProdukNama.Text = kategoriProduk.Nama;
                ButtonSimpanKategoriProduk.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                KategoriProduk_Class.DeleteKategoriProduk(db, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataKategori(db);
            }
        }
    }

    protected void ButtonSimpanKategoriProduk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (ButtonSimpanKategoriProduk.Text == "Tambah")
                db.TBKategoriProduks.InsertOnSubmit(new TBKategoriProduk { IDKategoriProdukParent = null, Nama = TextBoxKetegoriProdukNama.Text });
            else if (ButtonSimpanKategoriProduk.Text == "Ubah")
            {
                TBKategoriProduk kategoriProduk = db.TBKategoriProduks.FirstOrDefault(item => item.IDKategoriProduk == HiddenFieldIDKategoriProduk.Value.ToInt());
                kategoriProduk.Nama = TextBoxKetegoriProdukNama.Text;
            }
            db.SubmitChanges();

            HiddenFieldIDKategoriProduk.Value = null;
            TextBoxKetegoriProdukNama.Text = string.Empty;
            ButtonSimpanKategoriProduk.Text = "Tambah";

            LoadDataKategori(db);
        }
    }
    #endregion
}