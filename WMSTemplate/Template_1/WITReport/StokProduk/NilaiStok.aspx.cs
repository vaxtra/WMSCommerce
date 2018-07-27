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
    DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext();

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

                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataBind();
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

                DropDownListVendor.DataSource = db.TBVendors.ToArray();
                DropDownListVendor.DataTextField = "Nama";
                DropDownListVendor.DataValueField = "IDVendor";
                DropDownListVendor.DataBind();
                DropDownListVendor.Items.Insert(0, new ListItem { Value = "-1", Text = "- Semua -" });
                DropDownListVendor.Items.Insert(1, new ListItem { Value = "0", Text = " " });
            }
        }
        else
            LinkDownload.Visible = false;
    }

    private void LoadData()
    {
        LoadDatabase();
    }

    private IEnumerable<dynamic> LoadDatabase()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            DateTime _tanggalAwal = DateTime.Now;

            Server.ScriptTimeout = 1000000;

            //QUERY DATA
            string _tempPencarian = string.Empty;

            //ButtonPrint.OnClientClick = "return popitup('ProdukPrint.aspx" + _tempPencarian + "')";

            //Stok Awal : 1
            //Restok : 2
            //Reject : 18
            //Bertambah 
            //Berkurang
            //Stok Akhir

            var _database = db.TBKombinasiProduks.Select(item => item);

            #region PRODUK
            if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
            {
                _database = _database.Where(item => item.TBProduk.Nama.Contains(TextBoxProduk.Text));
                TextBoxProduk.Focus();

                _tempPencarian += "&Produk=" + TextBoxProduk.Text;
            }
            #endregion

            #region KODE
            if (!string.IsNullOrWhiteSpace(TextBoxKode.Text))
            {
                _database = _database.Where(item => item.KodeKombinasiProduk.Contains(TextBoxKode.Text));
                TextBoxKode.Focus();

                _tempPencarian += "&Kode=" + TextBoxKode.Text;
            }
            #endregion

            #region WARNA
            if (DropDownListWarna.SelectedValue != "-1")
            {
                _database = _database.Where(item => item.TBProduk.IDWarna == DropDownListWarna.SelectedValue.ToInt());

                TextBoxProduk.Focus();
                _tempPencarian += "&IDWarna=" + DropDownListWarna.SelectedValue;
            }
            #endregion

            #region PEMILIK PRODUK
            if (DropDownListPemilik.SelectedValue != "-1")
            {
                _database = _database.Where(item => item.TBProduk.IDPemilikProduk == DropDownListPemilik.SelectedValue.ToInt());

                TextBoxProduk.Focus();
                _tempPencarian += "&IDPemilikProduk=" + DropDownListPemilik.SelectedValue;
            }
            #endregion

            #region ATRIBUT
            if (DropDownListVarian.SelectedValue != "-1")
            {
                _database = _database.Where(item => item.IDAtributProduk == DropDownListVarian.SelectedValue.ToInt());

                TextBoxProduk.Focus();
                _tempPencarian += "&IDAtribut=" + DropDownListVarian.SelectedValue;
            }
            #endregion

            #region KATEGORI
            if (DropDownListKategori.SelectedValue != "-1")
            {
                if (DropDownListKategori.SelectedValue == "0")
                    _database = _database.Where(item => item.TBProduk.TBRelasiProdukKategoriProduks.Count == 0);
                else
                {
                    _database = _database.Where(item =>
                        item.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 &&
                        item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == int.Parse(DropDownListKategori.SelectedValue));
                }

                TextBoxProduk.Focus();
                _tempPencarian += "&IDKategori=" + DropDownListKategori.SelectedValue;
            }
            #endregion

            #region QUERY DATA
            var _data = _database
                .Select(item => new
                {
                    IDProduk = item.IDProduk,
                    IDKombinasiProduk = item.IDKombinasiProduk,
                    Produk = item.TBProduk.Nama,
                    Warna = item.TBProduk.TBWarna.Nama ?? "",
                    Brand = item.TBProduk.TBPemilikProduk.Nama ?? "",
                    Kategori = item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama ?? "",
                    Kode = item.KodeKombinasiProduk,
                    Varian = item.TBAtributProduk.Nama ?? "",

                    IDVendor = item.TBStokProduks
                    .FirstOrDefault(item2 =>
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)).TBHargaVendors.FirstOrDefault().IDVendor ?? 0,

                    Vendor = item.TBStokProduks
                    .FirstOrDefault(item2 =>
                        item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)).TBHargaVendors.FirstOrDefault().TBVendor.Nama ?? "",

                    StokHarga = StokHarga(item.TBStokProduks.FirstOrDefault(item2 => item2.IDTempat == int.Parse(DropDownListTempat.SelectedValue)))
                });
            #endregion

            #region HARGA BELI
            if (!string.IsNullOrWhiteSpace(TextBoxHargaBeli.Text))
            {
                if (TextBoxHargaBeli.Text.Contains("-"))
                {
                    string[] _angka = TextBoxHargaBeli.Text.Split('-');
                    _data = _data.Where(item =>
                        item.StokHarga[0] >= _angka[0].ToDecimal() &&
                        item.StokHarga[0] <= _angka[1].ToDecimal()).OrderBy(item => item.StokHarga[0]);
                }
                else
                    _data = _data.Where(item => item.StokHarga[0] == TextBoxHargaBeli.Text.ToDecimal());

                TextBoxHargaBeli.Focus();
                _tempPencarian += "&HargaBeli=" + TextBoxHargaBeli.Text;
            }
            #endregion

            #region HARGA JUAL
            if (!string.IsNullOrWhiteSpace(TextBoxHargaJual.Text))
            {
                if (TextBoxHargaJual.Text.Contains("-"))
                {
                    string[] _angka = TextBoxHargaJual.Text.Split('-');
                    _data = _data.Where(item =>
                        item.StokHarga[1] >= _angka[0].ToDecimal() &&
                        item.StokHarga[1] <= _angka[1].ToDecimal()).OrderBy(item => item.StokHarga[1]);
                }
                else
                    _data = _data.Where(item => item.StokHarga[1] == TextBoxHargaJual.Text.ToDecimal());

                TextBoxHargaJual.Focus();
                _tempPencarian += "&HargaJual=" + TextBoxHargaJual.Text;
            }
            #endregion

            #region STOK AWAL
            //if (!string.IsNullOrWhiteSpace(TextBoxStokAwal.Text))
            //{
            //    if (TextBoxStokAwal.Text.Contains("-"))
            //    {
            //        string[] _angka = TextBoxStokAwal.Text.Split('-');
            //        _data = _data.Where(item =>
            //            item.StokAwal >= _angka[0].ToDecimal() &&
            //            item.StokAwal <= _angka[1].ToDecimal()).OrderBy(item => item.StokAwal);
            //    }
            //    else
            //        _data = _data.Where(item => item.StokAwal == TextBoxStokAwal.Text.ToDecimal());

            //    TextBoxStokAwal.Focus();
            //    _tempPencarian += "&StokAwal=" + TextBoxStokAwal.Text;
            //}
            #endregion

            #region RESTOK
            //if (!string.IsNullOrWhiteSpace(TextBoxRestok.Text))
            //{
            //    if (TextBoxRestok.Text.Contains("-"))
            //    {
            //        string[] _angka = TextBoxRestok.Text.Split('-');
            //        _data = _data.Where(item =>
            //            item.Restok >= _angka[0].ToDecimal() &&
            //            item.Restok <= _angka[1].ToDecimal()).OrderBy(item => item.Restok);
            //    }
            //    else
            //        _data = _data.Where(item => item.Restok == TextBoxRestok.Text.ToDecimal());

            //    TextBoxRestok.Focus();
            //    _tempPencarian += "&Restok=" + TextBoxRestok.Text;
            //}
            #endregion

            #region VENDOR
            if (DropDownListVendor.SelectedValue != "-1")
            {
                if (DropDownListVendor.SelectedValue == "0")
                    _data = _data.Where(item => item.IDVendor == 0);
                else
                {
                    _data = _data.Where(item =>
                        item.IDVendor > 0 &&
                        item.IDVendor == int.Parse(DropDownListVendor.SelectedValue));
                }

                TextBoxProduk.Focus();
                _tempPencarian += "&IDVendor=" + DropDownListVendor.SelectedValue;
            }
            #endregion

            if (_data.Count() > 0)
            {
                var _kombinasiProduk = _data.Select(item => item.IDKombinasiProduk).Distinct();

                var _dataKombinasiProduk = db.TBPerpindahanStokProduks
                            .Where(item =>
                                item.IDTempat == int.Parse(DropDownListTempat.SelectedValue) &&
                                _kombinasiProduk.Any(item2 => item2 == item.TBStokProduk.IDKombinasiProduk))
                            .GroupBy(item => new
                            {
                                item.TBStokProduk.IDKombinasiProduk,
                                item.IDJenisPerpindahanStok,
                                item.TBJenisPerpindahanStok.Status
                            })
                            .Select(item => new
                            {
                                Key = item.Key,
                                Jumlah = item.Sum(item2 => item2.Jumlah)
                            });

                var _newData = _data.ToList()
                .Select(item => new
                {
                    IDProduk = item.IDProduk,
                    Produk = item.Produk,
                    Warna = item.Warna,
                    Brand = item.Brand,
                    Kategori = item.Kategori,
                    Kode = item.Kode,
                    Varian = item.Varian,
                    IDVendor = item.IDVendor,
                    Vendor = item.Vendor,
                    HargaBeli = item.StokHarga[0],
                    HargaJual = item.StokHarga[1],
                    StokAkhir = item.StokHarga[2],
                    SubtotalHargaBeli = item.StokHarga[3],
                    SubtotalHargaJual = item.StokHarga[4],
                    SubtotalKeuntungan = item.StokHarga[5],

                    //PerpindahanStok = PerpindahanStok(item.IDKombinasiProduk, int.Parse(DropDownListTempat.SelectedValue))

                    StokAwal = _dataKombinasiProduk.Where(item2 =>
                        item2.Key.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.Key.IDJenisPerpindahanStok == 1).Sum(item2 => item2.Jumlah),

                    Restok = _dataKombinasiProduk.Where(item2 =>
                        item2.Key.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.Key.IDJenisPerpindahanStok == 2).Sum(item2 => item2.Jumlah),

                    Reject = _dataKombinasiProduk.Where(item2 =>
                        item2.Key.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.Key.IDJenisPerpindahanStok == 18).Sum(item2 => item2.Jumlah),

                    Bertambah = _dataKombinasiProduk.Where(item2 =>
                        item2.Key.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.Key.IDJenisPerpindahanStok != 1 &&
                        item2.Key.IDJenisPerpindahanStok != 2 &&
                        item2.Key.Status == true).Sum(item2 => item2.Jumlah),

                    Berkurang = _dataKombinasiProduk.Where(item2 =>
                        item2.Key.IDKombinasiProduk == item.IDKombinasiProduk &&
                        item2.Key.IDJenisPerpindahanStok != 18 &&
                        item2.Key.Status == false).Sum(item2 => item2.Jumlah),
                });

                LabelTotalStokAkhir.Text = _newData.Sum(item => item.StokAkhir).ToFormatHargaBulat();
                LabelTotalHargaBeli.Text = _newData.Sum(item => item.SubtotalHargaBeli).ToFormatHarga();
                LabelTotalHargaJual.Text = _newData.Sum(item => item.SubtotalHargaJual).ToFormatHarga();
                LabelTotalKeuntungan.Text = _newData.Sum(item => item.SubtotalKeuntungan).ToFormatHarga();

                LabelTotalStokAkhir1.Text = LabelTotalStokAkhir.Text;
                LabelTotalHargaBeli1.Text = LabelTotalHargaBeli.Text;
                LabelTotalHargaJual1.Text = LabelTotalHargaJual.Text;
                LabelTotalKeuntungan1.Text = LabelTotalKeuntungan.Text;

                LabelTotalHeader.Text = "TOTAL";
                LabelTotalFooter.Text = "TOTAL";

                RepeaterLaporaKolom1.DataSource = _newData;
                RepeaterLaporaKolom1.DataBind();

                RepeaterLaporan.DataSource = _newData;
                RepeaterLaporan.DataBind();

                DateTime _tanggalAkhir = DateTime.Now;

                Response.Write(_tanggalAwal + "<br/>");
                Response.Write(_tanggalAkhir + "<br/>");
                Response.Write(_tanggalAkhir - _tanggalAwal);

                return _newData;
            }
            else
            {
                LabelTotalStokAkhir.Text = "0";
                LabelTotalHargaBeli.Text = "0";
                LabelTotalHargaJual.Text = "0";
                LabelTotalKeuntungan.Text = "0";

                LabelTotalStokAkhir1.Text = LabelTotalStokAkhir.Text;
                LabelTotalHargaBeli1.Text = LabelTotalHargaBeli.Text;
                LabelTotalHargaJual1.Text = LabelTotalHargaJual.Text;
                LabelTotalKeuntungan1.Text = LabelTotalKeuntungan.Text;

                RepeaterLaporan.DataSource = null;
                RepeaterLaporan.DataBind();

                return null;
            }
        }
    }

    private decimal[] StokHarga(TBStokProduk _stokProduk)
    {
        decimal[] _stokHarga = new decimal[6];

        if (_stokProduk != null)
        {
            //HargaBeli
            _stokHarga[0] = _stokProduk.HargaBeli ?? 0;

            //HargaJual
            _stokHarga[1] = _stokProduk.HargaJual ?? 0;

            //StokAkhir
            _stokHarga[2] = _stokProduk.Jumlah ?? 0;

            //SubtotalHargaBeli
            _stokHarga[3] = _stokHarga[0] * _stokHarga[2];

            //SubtotalHargaJual
            _stokHarga[4] = _stokHarga[1] * _stokHarga[2];

            //SubtotalKeuntungan
            _stokHarga[5] = (_stokHarga[1] - _stokHarga[0]) * _stokHarga[2];
        }

        return _stokHarga;
    }

    private double[] PerpindahanStok(IQueryable<object> _data)
    {
        double[] _perpindahanStok = new double[5];

        //var _data = db.TBPerpindahanStokProduks
        //.Where(item =>
        //    item.TBStokProduk.IDKombinasiProduk == idKombinasiProduk &&
        //    item.IDTempat == idTempat)
        //.GroupBy(item => new
        //{
        //    item.IDJenisPerpindahanStok,
        //    item.TBJenisPerpindahanStok.Status
        //})
        //.Select(item => new
        //{
        //    Key = item.Key,
        //    Jumlah = item.Sum(item2 => item2.Jumlah)
        //});

        ////StokAwal
        //_perpindahanStok[0] = (double)(_data.Where(item => item.Key.IDJenisPerpindahanStok == 1).Sum(item => item.Jumlah));

        ////Restok
        //_perpindahanStok[1] = (double)(_data.Where(item => item.Key.IDJenisPerpindahanStok == 2).Sum(item => item.Jumlah));

        ////Reject
        //_perpindahanStok[2] = (double)(_data.Where(item => item.Key.IDJenisPerpindahanStok == 18).Sum(item => item.Jumlah));

        ////Bertambah
        //_perpindahanStok[3] = (double)(_data.Where(item =>
        //    item.Key.IDJenisPerpindahanStok != 1 &&
        //    item.Key.IDJenisPerpindahanStok != 2 &&
        //    item.Key.Status == true).Sum(item => item.Jumlah));

        ////Berkurang
        //_perpindahanStok[4] = (double)(_data.Where(item =>
        //    item.Key.IDJenisPerpindahanStok != 18 &&
        //    item.Key.Status == false).Sum(item => item.Jumlah));

        return _perpindahanStok;
    }

    private double[] PerpindahanStok(int idKombinasiProduk, int idTempat)
    {
        double[] _perpindahanStok = new double[5];

        var _data = db.TBPerpindahanStokProduks
        .Where(item =>
            item.IDTempat == idTempat &&
            item.TBStokProduk.IDKombinasiProduk == idKombinasiProduk)
        .GroupBy(item => new
        {
            item.IDJenisPerpindahanStok,
            item.TBJenisPerpindahanStok.Status
        })
        .Select(item => new
        {
            Key = item.Key,
            Jumlah = item.Sum(item2 => item2.Jumlah)
        });

        //StokAwal
        _perpindahanStok[0] = _data.Where(item => item.Key.IDJenisPerpindahanStok == 1).Sum(item => item.Jumlah);

        //Restok
        _perpindahanStok[1] = _data.Where(item => item.Key.IDJenisPerpindahanStok == 2).Sum(item => item.Jumlah);

        //Reject
        _perpindahanStok[2] = _data.Where(item => item.Key.IDJenisPerpindahanStok == 18).Sum(item => item.Jumlah);

        //Bertambah
        _perpindahanStok[3] = _data.Where(item =>
            item.Key.IDJenisPerpindahanStok != 1 &&
            item.Key.IDJenisPerpindahanStok != 2 &&
            item.Key.Status == true).Sum(item => item.Jumlah);

        //Berkurang
        _perpindahanStok[4] = _data.Where(item =>
            item.Key.IDJenisPerpindahanStok != 18 &&
            item.Key.Status == false).Sum(item => item.Jumlah);

        return _perpindahanStok;
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