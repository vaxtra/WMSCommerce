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

public partial class WITAdministrator_Laporan_Transaksi_Transaksi : System.Web.UI.Page
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
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();
            }
        }
        else
            LinkDownload.Visible = false;
    }

    private void LoadData()
    {
        LoadDatabase();
    }

    private dynamic LoadDatabase()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //QUERY DATA
            string _tempPencarian = string.Empty;

            //ButtonPrint.OnClientClick = "return popitup('ProdukPrint.aspx" + _tempPencarian + "')";

            //Stok Awal : 1
            //Restok : 2
            //Reject : 18
            //Bertambah 
            //Berkurang
            //Stok Akhir

            var _data = db.TBKombinasiProduks
                .Select(item => new
                {
                    IDProduk = item.IDProduk,
                    Produk = item.TBProduk.Nama,
                    Warna = item.TBProduk.TBWarna.Nama ?? "",
                    Brand = item.TBProduk.TBPemilikProduk.Nama ?? "",
                    Kategori = item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama ?? "",
                    Kode = item.KodeKombinasiProduk,
                    Varian = item.TBAtributProduk.Nama ?? "",

                    Vendor = item.TBStokProduks
                    .FirstOrDefault(item2 =>
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)).TBHargaVendors.FirstOrDefault().TBVendor.Nama ?? "",

                    HargaBeli = item.TBStokProduks
                    .FirstOrDefault(item2 =>
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)).HargaBeli ?? 0,

                    HargaJual = item.TBStokProduks
                    .FirstOrDefault(item2 =>
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)).HargaJual ?? 0,

                    StokAwal = db.TBPerpindahanStokProduks
                    .Where(item2 =>
                        item2.TBStokProduk.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue) &&
                        item2.IDJenisPerpindahanStok == 1).Sum(item3 => item3.Jumlah),

                    Restok = db.TBPerpindahanStokProduks
                    .Where(item2 =>
                        item2.TBStokProduk.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue) &&
                        item2.IDJenisPerpindahanStok == 2).Sum(item3 => item3.Jumlah),

                    Reject = db.TBPerpindahanStokProduks
                    .Where(item2 =>
                        item2.TBStokProduk.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue) &&
                        item2.IDJenisPerpindahanStok == 18).Sum(item3 => item3.Jumlah),

                    Bertambah = db.TBPerpindahanStokProduks
                    .Where(item2 =>
                        item2.TBStokProduk.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue) &&
                        item2.IDJenisPerpindahanStok != 1 &&
                        item2.IDJenisPerpindahanStok != 2 &&
                        item2.TBJenisPerpindahanStok.Status == true).Sum(item3 => item3.Jumlah),

                    Berkurang = db.TBPerpindahanStokProduks
                    .Where(item2 =>
                        item2.TBStokProduk.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue) &&
                        item2.IDJenisPerpindahanStok != 18 &&
                        item2.TBJenisPerpindahanStok.Status == false).Sum(item3 => item3.Jumlah),

                    StokAkhir = item.TBStokProduks
                    .FirstOrDefault(item2 =>
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)).Jumlah ?? 0
                })
                .ToArray();

            LabelTotalStokAkhir.Text = _data.Sum(item => item.StokAkhir).ToFormatHargaBulat();
            LabelTotalHargaBeli.Text = _data.Sum(item => item.HargaBeli * item.StokAkhir).ToFormatHarga();
            LabelTotalHargaJual.Text = _data.Sum(item => item.HargaJual * item.StokAkhir).ToFormatHarga();
            LabelTotalKeuntungan.Text = (_data.Sum(item => (item.HargaJual - item.HargaBeli) * item.StokAkhir)).ToFormatHarga();

            LabelTotalHeader.Text = "TOTAL";
            LabelTotalFooter.Text = "TOTAL";

            RepeaterLaporanKolom1.DataSource = _data;
            RepeaterLaporanKolom1.DataBind();

            RepeaterLaporan.DataSource = _data;
            RepeaterLaporan.DataBind();

            LabelTotalStokAkhir1.Text = LabelTotalStokAkhir.Text;
            LabelTotalHargaBeli1.Text = LabelTotalHargaBeli.Text;
            LabelTotalHargaJual1.Text = LabelTotalHargaJual.Text;
            LabelTotalKeuntungan1.Text = LabelTotalKeuntungan.Text;

            return _data;
        }
    }

    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        PenggunaLogin _pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string _namaWorksheet = "Laporan Nilai Stok Produk";

        #region Default
        string _namaFile = "Laporan " + _namaWorksheet + " " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xlsx";
        string _path = Server.MapPath("~/file_excel/" + _namaWorksheet + "/Export/") + _namaFile;
        string _judul = "Laporan " + _namaWorksheet + " " + _pengguna.Store + " - " + _pengguna.Tempat + " " + DateTime.Now.ToString("d MMMM yyyy");

        FileInfo newFile = new FileInfo(_path);
        #endregion

        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_namaWorksheet);

            worksheet.Cells[1, 1].Value = "No.";
            worksheet.Cells[1, 2].Value = "Produk";
            worksheet.Cells[1, 3].Value = "Warna";
            worksheet.Cells[1, 4].Value = "Brand";
            worksheet.Cells[1, 5].Value = "Kategori";
            worksheet.Cells[1, 6].Value = "Vendor";
            worksheet.Cells[1, 7].Value = "Kode";
            worksheet.Cells[1, 8].Value = "Varian";
            worksheet.Cells[1, 9].Value = "Harga Beli";
            worksheet.Cells[1, 10].Value = "Harga Jual";
            worksheet.Cells[1, 11].Value = "Stok Awal";
            worksheet.Cells[1, 12].Value = "Restok";
            worksheet.Cells[1, 13].Value = "Reject";
            worksheet.Cells[1, 14].Value = "Bertambah";
            worksheet.Cells[1, 15].Value = "Berkurang";
            worksheet.Cells[1, 16].Value = "Stok Akhir";
            worksheet.Cells[1, 17].Value = "Subtotal Harga Beli";
            worksheet.Cells[1, 18].Value = "Subtotal Harga Jual";
            worksheet.Cells[1, 19].Value = "Subtotal Keuntungan";

            int index = 2;

            foreach (var item in LoadDatabase())
            {
                worksheet.Cells[index, 1].Value = index - 1;
                worksheet.Cells[index, 1].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 2].Value = item.Produk;
                worksheet.Cells[index, 2].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 3].Value = item.Warna;
                worksheet.Cells[index, 3].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 4].Value = item.Brand;
                worksheet.Cells[index, 4].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 5].Value = item.Kategori;
                worksheet.Cells[index, 5].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 6].Value = item.Vendor;
                worksheet.Cells[index, 6].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 7].Value = item.Kode;
                worksheet.Cells[index, 7].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 8].Value = item.Varian;
                worksheet.Cells[index, 8].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 9].Value = item.HargaBeli;
                worksheet.Cells[index, 9].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 10].Value = item.HargaJual;
                worksheet.Cells[index, 10].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 11].Value = item.StokAwal;
                worksheet.Cells[index, 11].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 12].Value = item.Restok;
                worksheet.Cells[index, 12].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 13].Value = item.Reject;
                worksheet.Cells[index, 13].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 14].Value = item.Bertambah;
                worksheet.Cells[index, 14].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 15].Value = item.Berkurang;
                worksheet.Cells[index, 15].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 16].Value = item.StokAkhir;
                worksheet.Cells[index, 16].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 17].Value = (item.HargaBeli * item.StokAkhir);
                worksheet.Cells[index, 17].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 18].Value = (item.HargaJual * item.StokAkhir);
                worksheet.Cells[index, 18].Style.Numberformat.Format = "@";

                worksheet.Cells[index, 19].Value = ((item.HargaJual - item.HargaBeli) * item.StokAkhir);
                worksheet.Cells[index, 19].Style.Numberformat.Format = "@";

                if (item.HargaBeli.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 9].Style.Numberformat.Format = "#,##0";

                if (item.HargaJual.ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 10].Style.Numberformat.Format = "#,##0";

                worksheet.Cells[index, 11].Style.Numberformat.Format = "#,##0";

                worksheet.Cells[index, 12].Style.Numberformat.Format = "#,##0";

                worksheet.Cells[index, 13].Style.Numberformat.Format = "#,##0";

                worksheet.Cells[index, 14].Style.Numberformat.Format = "#,##0";

                worksheet.Cells[index, 15].Style.Numberformat.Format = "#,##0";

                worksheet.Cells[index, 16].Style.Numberformat.Format = "#,##0";

                if (((item.HargaBeli * item.StokAkhir)).ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 17].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 17].Style.Numberformat.Format = "#,##0";

                if (((item.HargaJual * item.StokAkhir)).ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 18].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 18].Style.Numberformat.Format = "#,##0";

                if ((((item.HargaJual - item.HargaBeli) * item.StokAkhir)).ToFormatHarga().Contains(","))
                    worksheet.Cells[index, 19].Style.Numberformat.Format = "#,##0.00";
                else
                    worksheet.Cells[index, 19].Style.Numberformat.Format = "#,##0";

                index++;
            }

            using (var range = worksheet.Cells[1, 1, 1, 19])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                range.Style.Font.Color.SetColor(Color.White);
            }

            worksheet.Cells["B1:S1"].AutoFilter = true;

            #region Default
            worksheet.Cells.AutoFitColumns(0);

            worksheet.HeaderFooter.OddHeader.CenteredText = "&16&\"Tahoma,Regular Bold\"" + _judul;
            worksheet.HeaderFooter.OddFooter.LeftAlignedText = "Print : " + _pengguna.NamaLengkap + " - " + _pengguna.Tempat + " - " + DateTime.Now.ToString("d MMMM yyyy hh:mm");

            worksheet.HeaderFooter.OddFooter.RightAlignedText = "WIT. Warehouse Management System - " + string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

            package.Workbook.Properties.Title = _namaWorksheet;
            package.Workbook.Properties.Author = "WIT. Warehouse Management System";
            package.Workbook.Properties.Comments = _judul;

            package.Workbook.Properties.Company = "WIT. Warehouse Management System";
            package.Save();
            #endregion
        }

        LinkDownload.Visible = true;
        LinkDownload.HRef = "/file_excel/" + _namaWorksheet + "/Export/" + _namaFile;
    }

    protected void ButtonReset_Click(object sender, EventArgs e)
    {

    }
    protected void Event_LoadData(object sender, EventArgs e)
    {
        LoadData();
    }
}