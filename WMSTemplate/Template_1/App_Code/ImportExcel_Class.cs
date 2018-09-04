using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelLibrary.SpreadSheet;

public class KeyValue
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class ImportExcel_Class
{
    public string Message { get; set; }
    public int Baris { get; set; }
    public PenggunaLogin PenggunaLogin { get; set; }
    public string LokasiFileExcel { get; set; }

    public ImportExcel_Class(PenggunaLogin penggunaLogin, string lokasiFileExcel)
    {
        PenggunaLogin = penggunaLogin;
        LokasiFileExcel = lokasiFileExcel;
    }

    public Dictionary<string, dynamic> ImportPemasukanAkuntansi()
    {
        Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<TBJurnal> _jurnalList = new List<TBJurnal>();

            Workbook book = Workbook.Load(LokasiFileExcel);

            for (int i = 0; i < 2; i++)
            {
                Worksheet sheet = book.Worksheets[i];

                bool valid = true;

                #region Validasi Excel Required Field
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    Cell _tanggal = row.GetCell(0);
                    Cell _nomorReferensi = row.GetCell(1);
                    Cell _keterangan = row.GetCell(2);
                    Cell _akun1 = row.GetCell(3);
                    Cell _akun2 = row.GetCell(4);
                    Cell _nominal = row.GetCell(5);

                    if (_tanggal.IsEmpty ||
                        _nomorReferensi.IsEmpty ||
                        _keterangan.IsEmpty ||
                        _akun1.IsEmpty ||
                        _akun2.IsEmpty ||
                        _nominal.IsEmpty)
                    {
                        Message = "Semua kolom wajib diisi";
                        valid = false; break;
                    }

                    if (db.TBAkuns.FirstOrDefault(item => item.Nama.ToLower() == _akun1.StringValue.ToLower()) == null)
                    {
                        Message = "Akun " + _akun1.StringValue + " tidak ditemukan";
                        valid = false; break;
                    }

                    if (db.TBAkuns.FirstOrDefault(item => item.Nama.ToLower() == _akun2.StringValue.ToLower()) == null)
                    {
                        Message = "Akun " + _akun2.StringValue + " tidak ditemukan";
                        valid = false; break;
                    }
                }
                #endregion

                #region Input ke Database
                if (valid)
                {
                    for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                    {
                        Row row = sheet.Cells.GetRow(rowIndex);

                        //0. Tanggal
                        //1. Nomor Referensi
                        //2. Keterangan
                        //3. Akun1
                        //4. Akun2
                        //5. Nominal

                        Cell _tanggal = row.GetCell(0);
                        Cell _nomorReferensi = row.GetCell(1);
                        Cell _keterangan = row.GetCell(2);
                        Cell _akun1 = row.GetCell(3);
                        Cell _akun2 = row.GetCell(4);
                        Cell _nominal = row.GetCell(5);

                        string[] _temp;
                        _temp = _tanggal.StringValue.Split('-');
                        DateTime Tanggal = new DateTime(int.Parse(_temp[2]), int.Parse(_temp[1]), int.Parse(_temp[0]));

                        TBJurnal _jurnal = new TBJurnal
                        {
                            Tanggal = Tanggal,
                            Keterangan = _keterangan.StringValue,
                            IDPengguna = PenggunaLogin.IDPengguna,
                            Referensi = _nomorReferensi.StringValue
                        };

                        //JIKA PEMASUKAN
                        if (i == 0)
                        {
                            _jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = db.TBAkuns.FirstOrDefault(item => item.Nama.ToLower() == _akun2.StringValue.ToLower()).IDAkun,
                                Debit = decimal.Parse(_nominal.StringValue),
                                Kredit = 0
                            });

                            _jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = db.TBAkuns.FirstOrDefault(item => item.Nama.ToLower() == _akun1.StringValue.ToLower()).IDAkun,
                                Debit = 0,
                                Kredit = decimal.Parse(_nominal.StringValue)
                            });
                        }
                        //JIKA PENGELUARAN
                        else
                        {
                            _jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = db.TBAkuns.FirstOrDefault(item => item.Nama.ToLower() == _akun1.StringValue.ToLower()).IDAkun,
                                Debit = decimal.Parse(_nominal.StringValue),
                                Kredit = 0
                            });

                            _jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                            {
                                IDAkun = db.TBAkuns.FirstOrDefault(item => item.Nama.ToLower() == _akun2.StringValue.ToLower()).IDAkun,
                                Debit = 0,
                                Kredit = decimal.Parse(_nominal.StringValue)
                            });
                        }

                        _jurnalList.Add(_jurnal);
                    }
                }
                #endregion
            }

            //JIKA ADA ITEM YANG DI UPLOAD
            if (_jurnalList.Count > 0)
            {
                db.TBJurnals.InsertAllOnSubmit(_jurnalList);
                db.SubmitChanges();

                _result.Add("DataImport", _jurnalList);
            }
        }

        return _result;
    }

    public int ImportSurvei()
    {
        List<TBSoalPertanyaan> ListSoalPertanyaan = new List<TBSoalPertanyaan>();
        TBSoalPertanyaan SoalPertanyaan = new TBSoalPertanyaan();

        Workbook book = Workbook.Load(LokasiFileExcel);

        Worksheet sheet = book.Worksheets[0];

        //MEMASUKKAN SOAL
        TBSoal Soal = new TBSoal
        {
            IDPengguna = PenggunaLogin.IDPengguna,
            IDTempat = PenggunaLogin.IDTempat,
            TanggalPembuatan = DateTime.Now,
            TanggalMulai = DateTime.Now,
            EnumStatusSoal = (int)PilihanStatusSoal.TidakAktif,
            Keterangan = " ",
            Judul = " "
        };

        int index = 1;

        #region Input ke Database
        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex + 1; rowIndex++)
        {
            Row row = sheet.Cells.GetRow(rowIndex);

            Cell _pertanyaan = row.GetCell(1);
            Cell _jawaban = row.GetCell(2);
            Cell _bobot = row.GetCell(3);

            //MEMASUKKAN PERTANYAAN TERAKHIR
            if (rowIndex == sheet.Cells.LastRowIndex + 1)
                ListSoalPertanyaan.Add(SoalPertanyaan);

            if (!_pertanyaan.IsEmpty)
            {
                //JIKA PERTANYAAN SEBELUMNYA SUDAH ADA MAKA DIMASUKKAN KE LIST
                if (SoalPertanyaan.Nomor != null)
                    ListSoalPertanyaan.Add(SoalPertanyaan);

                //MEMASUKKAN PERTANYAAN
                SoalPertanyaan = new TBSoalPertanyaan
                {
                    Isi = _pertanyaan.StringValue,
                    Nomor = index++,
                    TBSoal = Soal
                };
            }

            if (!_jawaban.IsEmpty)
            {
                //MEMASUKKAN JAWABAN
                SoalPertanyaan.TBSoalJawabans.Add(new TBSoalJawaban
                {
                    Bobot = _bobot.StringValue.ToInt(),
                    Isi = _jawaban.StringValue
                });
            }
        }
        #endregion

        //JIKA ADA ITEM YANG DI UPLOAD
        if (ListSoalPertanyaan.Count > 0)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                db.TBSoalPertanyaans.InsertAllOnSubmit(ListSoalPertanyaan);
                db.SubmitChanges();
            }
        }

        return Soal.IDSoal;
    }

    public void ImportBahanBaku()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarKode = new List<string>();
        List<string> _daftarNama = new List<string>();

        //cek apakah KODE BAHAN BAKU, NAMA BAHAN BAKU, HARGA BELU, SATUAN, JENIS BAHAN BAKU valid
        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = sheet.Cells.GetRow(rowIndex);

            for (int colIndex = 0; colIndex <= 13; colIndex++)
            {
                Cell cell = row.GetCell(colIndex);

                if (colIndex == 2 || colIndex == 3 || colIndex == 4 || colIndex == 5 || colIndex == 7 || colIndex == 8 || colIndex == 9)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Bahan Baku, Satuan Besar, Konversi, Satuan Kecil, Jumlah, Harga Beli, Jumlah Minimum, brand wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }
            }

            if (valid)
            {
                Cell _kodeTemp = row.GetCell(1);

                if (!_kodeTemp.IsEmpty)
                {
                    if (_daftarKode.FirstOrDefault(item => item == _kodeTemp.StringValue.ToUpper()) == null)
                        _daftarKode.Add(_kodeTemp.StringValue.ToUpper());
                    else
                    {
                        Message = "Kode " + _kodeTemp.StringValue.ToUpper() + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }

                string _namaTemp = row.GetCell(2).StringValue;

                if (_daftarNama.FirstOrDefault(item => item.ToLower() == _namaTemp.ToLower()) == null)
                    _daftarNama.Add(_namaTemp);
                else
                {
                    Message = "Bahan Baku " + _namaTemp + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                    valid = false; break;
                }
            }
        }

        //jika valid
        if (valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                List<TBSatuan> listDataSatuan = db.TBSatuans.ToList();
                List<TBSupplier> listDataSupplier = db.TBSuppliers.ToList();
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    Cell _nomorUrut = row.GetCell(0);
                    Cell _kodeBahanBaku = row.GetCell(1);
                    Cell _namaBahanBaku = row.GetCell(2);
                    Cell _namaSatuanBesar = row.GetCell(3);
                    Cell _konversi = row.GetCell(4);
                    Cell _namaSatuanKecil = row.GetCell(5);
                    Cell _berat = row.GetCell(6);
                    Cell _jumlah = row.GetCell(7);
                    Cell _hargaBeli = row.GetCell(8);
                    Cell _jumlahMinimum = row.GetCell(9);
                    Cell _kategori = row.GetCell(10);
                    Cell _keterangan = row.GetCell(11);
                    Cell _supplier = row.GetCell(12);
                    Cell _brand = row.GetCell(13);

                    #region PemilikProduk
                    TBPemilikProduk PemilikProduk = null;
                    if (!string.IsNullOrEmpty(_brand.StringValue))
                    {
                        PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                        //PEMILIK PRODUK
                        PemilikProduk = ClassPemilikProduk.CariTambah(_brand.StringValue);
                    }
                    #endregion

                    #region Satuan
                    //Pencarian BAHAN BAKU berdasarkan nama bahan baku
                    TBSatuan dataSatuanBesar, dataSatuanKecil;

                    if (!string.IsNullOrWhiteSpace(_namaSatuanBesar.StringValue))
                    {
                        dataSatuanBesar = listDataSatuan.FirstOrDefault(item => item.Nama.ToLower() == _namaSatuanBesar.StringValue.ToLower());

                        if (dataSatuanBesar == null)
                        {
                            dataSatuanBesar = new TBSatuan { Nama = _namaSatuanBesar.StringValue };
                            listDataSatuan.Add(dataSatuanBesar);
                            db.TBSatuans.InsertOnSubmit(dataSatuanBesar);
                        }
                    }
                    else
                        dataSatuanBesar = null;

                    if (!string.IsNullOrWhiteSpace(_namaSatuanKecil.StringValue))
                    {
                        dataSatuanKecil = listDataSatuan.FirstOrDefault(item => item.Nama.ToLower() == _namaSatuanKecil.StringValue.ToLower());

                        if (dataSatuanKecil == null)
                        {
                            dataSatuanKecil = new TBSatuan { Nama = _namaSatuanKecil.StringValue };
                            listDataSatuan.Add(dataSatuanKecil);
                            db.TBSatuans.InsertOnSubmit(dataSatuanKecil);
                        }
                    }
                    else
                        dataSatuanKecil = null;


                    #endregion

                    #region Bahan Baku
                    TBBahanBaku dataBahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.Nama.ToLower() == _namaBahanBaku.StringValue.ToLower());

                    if (dataBahanBaku == null)
                    {
                        dataBahanBaku = new TBBahanBaku
                        {
                            IDWMS = Guid.NewGuid(),
                            TBSatuan = dataSatuanKecil,
                            TBSatuan1 = dataSatuanBesar,
                            TanggalDaftar = DateTime.Now,
                            TanggalUpdate = DateTime.Now,
                            Urutan = _nomorUrut.StringValue.ToInt(),
                            KodeBahanBaku = _kodeBahanBaku.StringValue.ToUpper(),
                            Nama = _namaBahanBaku.StringValue,
                            Berat = _berat.StringValue.ToInt(),
                            Konversi = _konversi.StringValue.ToDecimal(),
                            Deskripsi = _keterangan.StringValue
                        };
                        db.TBBahanBakus.InsertOnSubmit(dataBahanBaku);
                    }
                    else
                    {
                        dataBahanBaku.TBSatuan = dataSatuanKecil;
                        dataBahanBaku.TBSatuan1 = dataSatuanBesar;
                        dataBahanBaku.Urutan = _nomorUrut.StringValue.ToInt();
                        dataBahanBaku.TanggalUpdate = DateTime.Now;
                        dataBahanBaku.KodeBahanBaku = _kodeBahanBaku.StringValue.ToUpper();
                        dataBahanBaku.Nama = _namaBahanBaku.StringValue;
                        dataBahanBaku.Berat = _berat.StringValue.ToInt();
                        dataBahanBaku.Konversi = _konversi.StringValue.ToDecimal();
                        dataBahanBaku.Deskripsi = _keterangan.StringValue;
                    }

                    #endregion

                    #region Stok Bahan Baku
                    TBStokBahanBaku dataStokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == PenggunaLogin.IDTempat && item.TBBahanBaku == dataBahanBaku);

                    if (dataStokBahanBaku == null)
                    {
                        decimal jumlahStok = _jumlah.StringValue.ToDecimal() * dataBahanBaku.Konversi.Value;
                        decimal hargaBeli = _hargaBeli.StringValue.ToDecimal() / dataBahanBaku.Konversi.Value;
                        decimal jumlahMinimum = _jumlahMinimum.StringValue.ToDecimal() * dataBahanBaku.Konversi.Value;

                        dataStokBahanBaku = StokBahanBaku_Class.InsertStokBahanBaku(db, DateTime.Now, PenggunaLogin.IDPengguna, PenggunaLogin.IDTempat, dataBahanBaku, hargaBeli, jumlahStok, jumlahMinimum, "Import Excel");
                    }
                    else
                    {
                        StokBahanBaku_Class.UpdateStockOpname(db, DateTime.Now, PenggunaLogin.IDPengguna, dataStokBahanBaku, _jumlah.StringValue.ToDecimal(), true, "Penyesuaian Stok Import Excel");
                    }
                    #endregion

                    #region Kategori
                    db.TBRelasiBahanBakuKategoriBahanBakus.DeleteAllOnSubmit(dataBahanBaku.TBRelasiBahanBakuKategoriBahanBakus);
                    string[] listNamaKategori = _kategori.StringValue.Replace(", ", ",").Split(',');
                    List<TBKategoriBahanBaku> listDataKategoriBahanBaku = db.TBKategoriBahanBakus.ToList();
                    List<TBRelasiBahanBakuKategoriBahanBaku> listDataRelasiKategori = new List<TBRelasiBahanBakuKategoriBahanBaku>();
                    foreach (var itemNamaKategori in listNamaKategori)
                    {
                        TBKategoriBahanBaku kategoriBahanBaku;
                        if (!string.IsNullOrWhiteSpace(itemNamaKategori))
                        {
                            kategoriBahanBaku = listDataKategoriBahanBaku.FirstOrDefault(item => item.Nama.ToLower() == itemNamaKategori.ToLower());

                            if (kategoriBahanBaku == null)
                            {
                                kategoriBahanBaku = new TBKategoriBahanBaku { Nama = itemNamaKategori };
                                listDataKategoriBahanBaku.Add(kategoriBahanBaku);
                                db.TBKategoriBahanBakus.InsertOnSubmit(kategoriBahanBaku);
                            }
                        }
                        else
                            kategoriBahanBaku = null;

                        if (kategoriBahanBaku != null)
                        {
                            if (listDataRelasiKategori.FirstOrDefault(data => data.TBKategoriBahanBaku == kategoriBahanBaku && data.TBBahanBaku == dataBahanBaku) == null)
                            {
                                listDataRelasiKategori.Add(new TBRelasiBahanBakuKategoriBahanBaku { TBKategoriBahanBaku = kategoriBahanBaku, TBBahanBaku = dataBahanBaku });
                            }
                        }
                    }
                    db.TBRelasiBahanBakuKategoriBahanBakus.InsertAllOnSubmit(listDataRelasiKategori);
                    #endregion

                    #region Supplier
                    if (!string.IsNullOrEmpty(_supplier.StringValue))
                    {
                        TBSupplier dataSupplier = listDataSupplier.FirstOrDefault(item => item.Nama.ToLower() == _supplier.StringValue.ToLower());

                        if (dataSupplier == null)
                        {
                            dataSupplier = new TBSupplier
                            {
                                Nama = _supplier.StringValue,
                                Alamat = string.Empty,
                                Email = string.Empty,
                                Telepon1 = string.Empty,
                                Telepon2 = string.Empty
                            };
                            listDataSupplier.Add(dataSupplier);
                            db.TBSuppliers.InsertOnSubmit(dataSupplier);
                        }

                        HargaSupplier_Class.Update(db, dataSupplier, dataStokBahanBaku, _hargaBeli.StringValue.ToDecimal());
                    }
                    #endregion

                    db.SubmitChanges();

                    if (_kodeBahanBaku.IsEmpty)
                    {
                        BahanBaku_Class classBahanBaku = new BahanBaku_Class();
                        classBahanBaku.Barcode(db, PenggunaLogin.IDTempat, dataBahanBaku);
                    }
                }
            }
        }
    }

    public void ImportKomposisiProduk()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarNamaProdukYangDiproduksi = new List<string>();
        List<string> _daftarNamaBahanBaku = new List<string>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<TBJenisBiayaProduksi> listJenisBiayaProduksi = db.TBJenisBiayaProduksis.ToList();
            var listBahanBaku = db.TBBahanBakus.ToArray();
            var listKombinasiProduk = db.TBKombinasiProduks.ToArray();

            for (int colIndex = 6; colIndex <= sheet.Cells.LastColIndex; colIndex++)
            {
                Cell cell = sheet.Cells.GetRow(0).GetCell(colIndex);

                if (!cell.IsEmpty)
                {
                    TBJenisBiayaProduksi jenisBiayaProduksi = listJenisBiayaProduksi.FirstOrDefault(item => item.Nama.ToLower() == cell.StringValue.ToLower());

                    if (jenisBiayaProduksi == null)
                    {
                        jenisBiayaProduksi = new TBJenisBiayaProduksi { Nama = cell.StringValue };
                        listJenisBiayaProduksi.Add(jenisBiayaProduksi);
                        db.TBJenisBiayaProduksis.InsertOnSubmit(jenisBiayaProduksi);
                    }
                }
            }

            //cek apakah BAHAN BAKU, JUMLAH valid
            for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
            {
                Row row = sheet.Cells.GetRow(rowIndex);

                for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                {
                    Cell cell = row.GetCell(colIndex);

                    if (colIndex == 3 || colIndex == 4)
                    {
                        if (cell.IsEmpty)
                        {
                            Message = "Bahan Baku atau Jumlah wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }
                    }

                    if (colIndex >= 6)
                    {
                        if (!row.GetCell(1).IsEmpty)
                        {
                            if (cell.IsEmpty)
                            {
                                Message = sheet.Cells.GetRow(0).GetCell(colIndex).StringValue + " wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                                valid = false; break;
                            }
                        }
                    }
                }

                if (valid)
                {
                    string _namaProdukYangDiproduksiTemp = row.GetCell(1).StringValue;

                    string _namaKombinasiProdukYangDiproduksi;

                    if (!string.IsNullOrWhiteSpace(row.GetCell(2).StringValue))
                    {
                        _namaKombinasiProdukYangDiproduksi = _namaProdukYangDiproduksiTemp + " (" + row.GetCell(2).StringValue + ")";
                    }
                    else
                    {
                        _namaKombinasiProdukYangDiproduksi = _namaProdukYangDiproduksiTemp;
                    }

                    if (_daftarNamaProdukYangDiproduksi.FirstOrDefault(item => item.ToLower() == _namaKombinasiProdukYangDiproduksi.ToLower()) == null)
                    {
                        if (!row.GetCell(1).IsEmpty)
                        {
                            _daftarNamaProdukYangDiproduksi.Add(_namaKombinasiProdukYangDiproduksi.ToLower());
                            _daftarNamaBahanBaku = new List<string>();
                        }
                    }
                    else
                    {
                        Message = "Produk " + _namaKombinasiProdukYangDiproduksi + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }

                    if (!row.GetCell(1).IsEmpty && listKombinasiProduk.FirstOrDefault(item => item.Nama.ToLower() == _namaKombinasiProdukYangDiproduksi.ToLower()) == null)
                    {
                        Message = "Produk " + _namaKombinasiProdukYangDiproduksi + " tidak ada dalam daftar produk (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }

                    string _namaBahanBakuTemp = row.GetCell(3).StringValue;

                    if (_daftarNamaBahanBaku.FirstOrDefault(item => item.ToLower() == _namaBahanBakuTemp.ToLower()) == null)
                        _daftarNamaBahanBaku.Add(_namaBahanBakuTemp);
                    else
                    {
                        Message = "Bahan Baku " + _namaBahanBakuTemp + " tidak boleh ada yang duplikat dalam 1 varian (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }

                    if (!row.GetCell(3).IsEmpty && listBahanBaku.FirstOrDefault(item => item.Nama.ToLower() == _namaBahanBakuTemp.ToLower()) == null)
                    {
                        Message = "Bahan Baku " + _namaBahanBakuTemp + " tidak ada dalam daftar bahan baku (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }
            }

            //jika valid
            if (valid)
            {
                TBKombinasiProduk kombinasiProduk = null;

                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    List<Cell> listData = new List<Cell>();

                    for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                    {
                        //Cell(1) = _produk;
                        //Cell(2) = _varian;
                        //Cell(3) = _bahanBaku;
                        //Cell(4) = _jumlahBahanBaku;
                        //Cell(6 sampai n) = _biayaProduksi;

                        Cell cell = row.GetCell(colIndex);

                        listData.Add(cell);
                    }

                    if (!listData[1].IsEmpty)
                    {
                        string _namaProdukYangDiproduksiTemp = listData[1].StringValue;

                        string _namaKombinasiProdukYangDiproduksi;

                        if (!string.IsNullOrWhiteSpace(listData[2].StringValue))
                        {
                            _namaKombinasiProdukYangDiproduksi = _namaProdukYangDiproduksiTemp + " (" + row.GetCell(2).StringValue + ")";
                        }
                        else
                        {
                            _namaKombinasiProdukYangDiproduksi = _namaProdukYangDiproduksiTemp;
                        }

                        kombinasiProduk = listKombinasiProduk.FirstOrDefault(item => item.Nama.ToLower() == _namaKombinasiProdukYangDiproduksi.ToLower());

                        var listKomposisiKombinasiProduk = db.TBKomposisiKombinasiProduks.Where(item => item.TBKombinasiProduk == kombinasiProduk).ToArray();

                        if (listKomposisiKombinasiProduk.Count() > 0)
                        {
                            db.TBKomposisiKombinasiProduks.DeleteAllOnSubmit(listKomposisiKombinasiProduk);
                        }

                        var listRelasiJenisBiayaProduksiKombinasiProduk = db.TBRelasiJenisBiayaProduksiKombinasiProduks.Where(item => item.TBKombinasiProduk == kombinasiProduk).ToArray();

                        if (listRelasiJenisBiayaProduksiKombinasiProduk.Count() > 0)
                        {
                            db.TBRelasiJenisBiayaProduksiKombinasiProduks.DeleteAllOnSubmit(listRelasiJenisBiayaProduksiKombinasiProduk);
                        }

                        //Biaya Produksi

                        for (int kolom = 6; kolom < listData.Count; kolom++)
                        {
                            TBJenisBiayaProduksi jenisBiayaProduksi = listJenisBiayaProduksi.FirstOrDefault(item => item.Nama.ToLower() == sheet.Cells.GetRow(0).GetCell(kolom).StringValue.ToLower());

                            if (jenisBiayaProduksi != null)
                            {
                                if (listData[kolom].StringValue.Contains("%"))
                                {
                                    decimal biaya = decimal.Parse(listData[kolom].StringValue.Replace("%", "")) / 100;

                                    if (biaya > 0)
                                    {
                                        db.TBRelasiJenisBiayaProduksiKombinasiProduks.InsertOnSubmit(new TBRelasiJenisBiayaProduksiKombinasiProduk
                                        {
                                            TBJenisBiayaProduksi = jenisBiayaProduksi,
                                            TBKombinasiProduk = kombinasiProduk,
                                            EnumBiayaProduksi = (int)PilihanBiayaProduksi.Persen,
                                            Persentase = biaya,
                                            Nominal = 0
                                        });
                                    }
                                }
                                else
                                {
                                    decimal biaya = decimal.Parse(listData[kolom].StringValue);

                                    if (biaya > 0)
                                    {
                                        db.TBRelasiJenisBiayaProduksiKombinasiProduks.InsertOnSubmit(new TBRelasiJenisBiayaProduksiKombinasiProduk
                                        {
                                            TBJenisBiayaProduksi = jenisBiayaProduksi,
                                            TBKombinasiProduk = kombinasiProduk,
                                            EnumBiayaProduksi = (int)PilihanBiayaProduksi.Harga,
                                            Persentase = 0,
                                            Nominal = listData[kolom].StringValue.ToDecimal()
                                        });
                                    }
                                }

                            }
                        }
                    }

                    if (kombinasiProduk != null)
                    {
                        TBBahanBaku bahanBaku = listBahanBaku.FirstOrDefault(item => item.Nama.ToLower() == listData[3].StringValue.ToLower());

                        db.TBKomposisiKombinasiProduks.InsertOnSubmit(new TBKomposisiKombinasiProduk { TBBahanBaku = bahanBaku, TBKombinasiProduk = kombinasiProduk, Jumlah = listData[4].StringValue.ToDecimal() });
                    }
                }
            }
            db.SubmitChanges();
        }
    }

    public void ImportKomposisiBahanBaku()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarNamaBahanBakuYangDiproduksi = new List<string>();
        List<string> _daftarNamaBahanBaku = new List<string>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<TBJenisBiayaProduksi> listJenisBiayaProduksi = db.TBJenisBiayaProduksis.ToList();
            var listBahanBaku = db.TBBahanBakus.ToArray();
            var listBahanBakuProduksi = listBahanBaku;

            for (int colIndex = 6; colIndex <= sheet.Cells.LastColIndex; colIndex++)
            {
                Cell cell = sheet.Cells.GetRow(0).GetCell(colIndex);

                if (!cell.IsEmpty)
                {
                    TBJenisBiayaProduksi jenisBiayaProduksi = listJenisBiayaProduksi.FirstOrDefault(item => item.Nama.ToLower() == cell.StringValue.ToLower());

                    if (jenisBiayaProduksi == null)
                    {
                        jenisBiayaProduksi = new TBJenisBiayaProduksi { Nama = cell.StringValue };
                        listJenisBiayaProduksi.Add(jenisBiayaProduksi);
                        db.TBJenisBiayaProduksis.InsertOnSubmit(jenisBiayaProduksi);
                    }
                }
            }

            //cek apakah BAHAN BAKU, JUMLAH valid
            for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
            {
                Row row = sheet.Cells.GetRow(rowIndex);

                for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                {
                    Cell cell = row.GetCell(colIndex);

                    if (colIndex == 3 || colIndex == 4)
                    {
                        if (cell.IsEmpty)
                        {
                            Message = "Bahan Baku, Quantity wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }
                    }

                    if (colIndex >= 6)
                    {
                        if (!row.GetCell(1).IsEmpty)
                        {
                            if (cell.IsEmpty)
                            {
                                Message = sheet.Cells.GetRow(0).GetCell(colIndex).StringValue + " wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                                valid = false; break;
                            }
                        }
                    }
                }

                if (valid)
                {
                    string _namaBahanBakuYangDiproduksiTemp = row.GetCell(1).StringValue;

                    if (_daftarNamaBahanBakuYangDiproduksi.FirstOrDefault(item => item.ToLower() == _namaBahanBakuYangDiproduksiTemp.ToLower()) == null)
                    {
                        if (!row.GetCell(1).IsEmpty)
                        {
                            _daftarNamaBahanBakuYangDiproduksi.Add(_namaBahanBakuYangDiproduksiTemp.ToLower());
                            _daftarNamaBahanBaku = new List<string>();
                        }
                    }
                    else
                    {
                        Message = "Bahan Baku " + _namaBahanBakuYangDiproduksiTemp + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }

                    if (!row.GetCell(1).IsEmpty && listBahanBakuProduksi.FirstOrDefault(item => item.Nama.ToLower() == _namaBahanBakuYangDiproduksiTemp.ToLower()) == null)
                    {
                        Message = "Bahan Baku " + _namaBahanBakuYangDiproduksiTemp + " tidak ada dalam daftar bahan baku (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }

                    string _namaBahanBakuTemp = row.GetCell(3).StringValue;

                    if (_daftarNamaBahanBaku.FirstOrDefault(item => item.ToLower() == _namaBahanBakuTemp.ToLower()) == null)
                        _daftarNamaBahanBaku.Add(_namaBahanBakuTemp);
                    else
                    {
                        Message = "Bahan Baku " + _namaBahanBakuTemp + " tidak boleh ada yang duplikat dalam 1 varian (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }

                    if (!row.GetCell(3).IsEmpty && listBahanBaku.FirstOrDefault(item => item.Nama.ToLower() == _namaBahanBakuTemp.ToLower()) == null)
                    {
                        Message = "Bahan Baku " + _namaBahanBakuTemp + " tidak ada dalam daftar bahan baku (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }
            }

            //jika valid
            if (valid)
            {
                TBBahanBaku bahanBakuProduksi = null;

                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    List<Cell> listData = new List<Cell>();

                    for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                    {
                        //Cell(1) = _bahanBakuProduksi;
                        //Cell(3) = _bahanBaku;
                        //Cell(4) = _jumlahBahanBaku;
                        //Cell(6 sampai n) = _biayaProduksi;

                        Cell cell = row.GetCell(colIndex);

                        listData.Add(cell);
                    }

                    if (!listData[1].IsEmpty)
                    {
                        bahanBakuProduksi = listBahanBakuProduksi.FirstOrDefault(item => item.Nama.ToLower() == listData[1].StringValue.ToLower());

                        var listKomposisiBahanBaku = db.TBKomposisiBahanBakus.Where(item => item.TBBahanBaku == bahanBakuProduksi).ToList();

                        if (listKomposisiBahanBaku.Count() > 0)
                        {
                            db.TBKomposisiBahanBakus.DeleteAllOnSubmit(listKomposisiBahanBaku);
                        }

                        var listRelasiJenisBiayaProduksiBahanBaku = db.TBRelasiJenisBiayaProduksiBahanBakus.Where(item => item.TBBahanBaku == bahanBakuProduksi).ToList();

                        if (listRelasiJenisBiayaProduksiBahanBaku.Count() > 0)
                        {
                            db.TBRelasiJenisBiayaProduksiBahanBakus.DeleteAllOnSubmit(listRelasiJenisBiayaProduksiBahanBaku);
                        }

                        //Biaya Produksi

                        for (int kolom = 6; kolom < listData.Count; kolom++)
                        {
                            TBJenisBiayaProduksi jenisBiayaProduksi = listJenisBiayaProduksi.FirstOrDefault(item => item.Nama.ToLower() == sheet.Cells.GetRow(0).GetCell(kolom).StringValue.ToLower());

                            if (jenisBiayaProduksi != null)
                            {
                                if (listData[kolom].StringValue.Contains("%"))
                                {
                                    decimal biaya = decimal.Parse(listData[kolom].StringValue.Replace("%", "")) / 100;

                                    if (biaya > 0)
                                    {
                                        db.TBRelasiJenisBiayaProduksiBahanBakus.InsertOnSubmit(new TBRelasiJenisBiayaProduksiBahanBaku
                                        {
                                            TBJenisBiayaProduksi = jenisBiayaProduksi,
                                            TBBahanBaku = bahanBakuProduksi,
                                            EnumBiayaProduksi = (int)PilihanBiayaProduksi.Persen,
                                            Persentase = biaya,
                                            Nominal = 0
                                        });
                                    }
                                }
                                else
                                {
                                    decimal biaya = decimal.Parse(listData[kolom].StringValue);

                                    if (biaya > 0)
                                    {
                                        db.TBRelasiJenisBiayaProduksiBahanBakus.InsertOnSubmit(new TBRelasiJenisBiayaProduksiBahanBaku
                                        {
                                            TBJenisBiayaProduksi = jenisBiayaProduksi,
                                            TBBahanBaku = bahanBakuProduksi,
                                            EnumBiayaProduksi = (int)PilihanBiayaProduksi.Harga,
                                            Persentase = 0,
                                            Nominal = listData[kolom].StringValue.ToDecimal()
                                        });
                                    }
                                }

                            }
                        }
                    }

                    if (bahanBakuProduksi != null)
                    {
                        TBBahanBaku _bahanBaku = listBahanBaku.FirstOrDefault(item => item.Nama.ToLower() == listData[3].StringValue.ToLower());

                        db.TBKomposisiBahanBakus.InsertOnSubmit(new TBKomposisiBahanBaku { TBBahanBaku = bahanBakuProduksi, TBBahanBaku1 = _bahanBaku, Jumlah = listData[4].StringValue.ToDecimal() });
                    }
                }
            }
            db.SubmitChanges();
        }
    }

    public void ImportJenisBiayaProyeksi()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarUrutan = new List<string>();
        List<string> _daftarNama = new List<string>();

        //cek apakah KODE BAHAN BAKU, NAMA BAHAN BAKU, HARGA BELU, SATUAN, JENIS BAHAN BAKU valid
        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = sheet.Cells.GetRow(rowIndex);

            for (int colIndex = 0; colIndex <= 4; colIndex++)
            {
                Cell cell = row.GetCell(colIndex);

                if (colIndex == 0)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Kolom Urutan wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }

                if (colIndex == 1)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Jenis Biaya Proyeksi wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }

                if (colIndex == 2)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Batas Bawah wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }

                if (colIndex == 3)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Batas Tengah wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }

                if (colIndex == 4)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Batas Atas wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }


            }

            if (valid)
            {
                string _urutanTemp = row.GetCell(0).StringValue;

                if (_daftarUrutan.FirstOrDefault(item => item == _urutanTemp.ToUpper()) == null)
                    _daftarUrutan.Add(_urutanTemp.ToUpper());
                else
                {
                    Message = "Urutan " + _urutanTemp.ToUpper() + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                    valid = false; break;
                }

                string _namaTemp = row.GetCell(1).StringValue;

                if (_daftarNama.FirstOrDefault(item => item.ToLower() == _namaTemp.ToLower()) == null)
                    _daftarNama.Add(_namaTemp);
                else
                {
                    Message = "Jenis Biaya Proyeksi " + _namaTemp + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                    valid = false; break;
                }
            }
        }

        if (_daftarNama.FirstOrDefault(item => item.ToLower() == "profit") == null)
        {
            Message = "Tidak Ada Profit dalam kolom Jenis Biaya Proyeksi";
            valid = false;
        }

        if (_daftarNama.FirstOrDefault(item => item.ToLower() == "ppn") == null)
        {
            Message = "Tidak Ada PPN dalam kolom Jenis Biaya Proyeksi";
            valid = false;
        }

        if (valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                List<TBJenisBiayaProyeksiDetail> daftarJenisBiayaProyeksiDetailBatasBawah = new List<TBJenisBiayaProyeksiDetail>();
                List<TBJenisBiayaProyeksiDetail> daftarJenisBiayaProyeksiDetailBatasTengah = new List<TBJenisBiayaProyeksiDetail>();
                List<TBJenisBiayaProyeksiDetail> daftarJenisBiayaProyeksiDetailBatasAtas = new List<TBJenisBiayaProyeksiDetail>();

                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    Cell _urutan = row.GetCell(0);
                    Cell _namaJenisBiayaProyeksi = row.GetCell(1);
                    Cell _batasBawah = row.GetCell(2);
                    Cell _batasTengah = row.GetCell(3);
                    Cell _batasAtas = row.GetCell(4);

                    TBJenisBiayaProyeksi dataJenisBiayaProyeksi = JenisBiayaProyeksi_Class.CariTambahJenisBiayaProyeksiByUrutanNama(db, _urutan.StringValue.ToInt(), _namaJenisBiayaProyeksi.StringValue);

                    //BATAS BAWAH
                    if (_batasBawah.StringValue.Contains("%"))
                    {
                        decimal biaya = decimal.Parse(_batasBawah.StringValue.Replace("%", "")) / 100;

                        JenisBiayaProyeksiDetail_Class.CariTambahJenisBiayaProyeksiDetailByNama(db, dataJenisBiayaProyeksi, _namaJenisBiayaProyeksi.StringValue, (int)PilihanBiayaProyeksi.Persen, biaya, 0, (int)PilihanStatusBatasProyeksi.BatasBawah);
                    }
                    else
                    {
                        decimal biaya = decimal.Parse(_batasBawah.StringValue);

                        JenisBiayaProyeksiDetail_Class.CariTambahJenisBiayaProyeksiDetailByNama(db, dataJenisBiayaProyeksi, _namaJenisBiayaProyeksi.StringValue, (int)PilihanBiayaProyeksi.Harga, 0, biaya, (int)PilihanStatusBatasProyeksi.BatasBawah);
                    }

                    //BATAS TENGAH
                    if (_batasTengah.StringValue.Contains("%"))
                    {
                        decimal biaya = decimal.Parse(_batasTengah.StringValue.Replace("%", "")) / 100;

                        JenisBiayaProyeksiDetail_Class.CariTambahJenisBiayaProyeksiDetailByNama(db, dataJenisBiayaProyeksi, _namaJenisBiayaProyeksi.StringValue, (int)PilihanBiayaProyeksi.Persen, biaya, 0, (int)PilihanStatusBatasProyeksi.BatasTengah);
                    }
                    else
                    {
                        decimal biaya = decimal.Parse(_batasTengah.StringValue);

                        JenisBiayaProyeksiDetail_Class.CariTambahJenisBiayaProyeksiDetailByNama(db, dataJenisBiayaProyeksi, _namaJenisBiayaProyeksi.StringValue, (int)PilihanBiayaProyeksi.Harga, 0, biaya, (int)PilihanStatusBatasProyeksi.BatasTengah);
                    }

                    //BATAS ATAS
                    if (_batasAtas.StringValue.Contains("%"))
                    {
                        decimal biaya = decimal.Parse(_batasAtas.StringValue.Replace("%", "")) / 100;

                        JenisBiayaProyeksiDetail_Class.CariTambahJenisBiayaProyeksiDetailByNama(db, dataJenisBiayaProyeksi, _namaJenisBiayaProyeksi.StringValue, (int)PilihanBiayaProyeksi.Persen, biaya, 0, (int)PilihanStatusBatasProyeksi.BatasAtas);
                    }
                    else
                    {
                        decimal biaya = decimal.Parse(_batasAtas.StringValue);

                        JenisBiayaProyeksiDetail_Class.CariTambahJenisBiayaProyeksiDetailByNama(db, dataJenisBiayaProyeksi, _namaJenisBiayaProyeksi.StringValue, (int)PilihanBiayaProyeksi.Harga, 0, biaya, (int)PilihanStatusBatasProyeksi.BatasAtas);
                    }
                }
                db.SubmitChanges();
            }
        }
    }

    public void ImportSupplier()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarNama = new List<string>();

        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = sheet.Cells.GetRow(rowIndex);

            for (int colIndex = 0; colIndex <= 6; colIndex++)
            {
                Cell cell = row.GetCell(colIndex);

                if (colIndex == 1)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Kolom Nama Supplier wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }
            }

            if (valid)
            {
                string _namaTemp = row.GetCell(1).StringValue;

                if (_daftarNama.FirstOrDefault(item => item.ToLower() == _namaTemp.ToLower()) == null)
                    _daftarNama.Add(_namaTemp);
                else
                {
                    Message = "Nama Supplier " + _namaTemp + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                    valid = false; break;
                }
            }
        }

        //jika valid
        if (valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    Cell _nomorUrut = row.GetCell(0);
                    Cell _namaSupplier = row.GetCell(1);
                    Cell _alamat = row.GetCell(2);
                    Cell _email = row.GetCell(3);
                    Cell _telepon1 = row.GetCell(4);
                    Cell _telepon2 = row.GetCell(5);
                    Cell _persentaseTax = row.GetCell(6);

                    #region Supplier
                    TBSupplier dataSupplier = db.TBSuppliers.FirstOrDefault(item => item.Nama.ToLower() == _namaSupplier.StringValue.ToLower());

                    if (dataSupplier == null)
                    {
                        dataSupplier = new TBSupplier
                        {
                            Nama = _namaSupplier.StringValue,
                            Alamat = _alamat.StringValue,
                            Email = _email.StringValue,
                            Telepon1 = _telepon1.StringValue,
                            Telepon2 = _telepon2.StringValue,
                            PersentaseTax = _persentaseTax.StringValue.ToDecimal()
                        };
                        db.TBSuppliers.InsertOnSubmit(dataSupplier);
                    }
                    else
                    {
                        dataSupplier.Alamat = _alamat.StringValue;
                        dataSupplier.Email = _email.StringValue;
                        dataSupplier.Telepon1 = _telepon1.StringValue;
                        dataSupplier.Telepon2 = _telepon2.StringValue;
                        dataSupplier.PersentaseTax = _persentaseTax.StringValue.ToDecimal();
                    }

                    #endregion

                }
                db.SubmitChanges();
            }
        }
    }

    public void ImportHargaSupplier()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarNamaBahanBaku = new List<string>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var listSupplier = db.TBSuppliers.ToArray();
            var listStokBahanBaku = db.TBStokBahanBakus.ToArray();
            var listSatuan = db.TBSatuans.ToArray();

            for (int colIndex = 3; colIndex <= sheet.Cells.LastColIndex; colIndex++)
            {
                Cell cell = sheet.Cells.GetRow(0).GetCell(colIndex);

                if (!cell.IsEmpty)
                {
                    if (listSupplier.FirstOrDefault(item => item.Nama.ToLower() == cell.StringValue.ToLower()) == null)
                    {
                        Message = "Supplier " + cell.StringValue + " tidak ada dalam daftar supplier";
                        valid = false; break;
                    }
                }
            }

            if (valid)
            {
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                    {
                        Cell cell = row.GetCell(colIndex);

                        if (colIndex >= 2)
                        {
                            if (!row.GetCell(1).IsEmpty)
                            {
                                if (cell.IsEmpty)
                                {
                                    Message = sheet.Cells.GetRow(0).GetCell(colIndex).StringValue + " wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                                    valid = false; break;
                                }
                            }
                        }
                    }

                    if (valid)
                    {
                        string _namaBahanBakuTemp = row.GetCell(1).StringValue;
                        string _namaSatuanTemp = row.GetCell(2).StringValue;

                        if (_daftarNamaBahanBaku.FirstOrDefault(item => item.ToLower() == _namaBahanBakuTemp.ToLower()) == null)
                            _daftarNamaBahanBaku.Add(_namaBahanBakuTemp);
                        else
                        {
                            Message = "Bahan Baku " + _namaBahanBakuTemp + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }

                        if (!row.GetCell(1).IsEmpty && listStokBahanBaku.FirstOrDefault(item => item.TBBahanBaku.Nama.ToLower() == _namaBahanBakuTemp.ToLower()) == null)
                        {
                            Message = "Bahan Baku " + _namaBahanBakuTemp + " tidak ada dalam daftar bahan baku (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }

                        if (!row.GetCell(2).IsEmpty && listSatuan.FirstOrDefault(item => item.Nama.ToLower() == _namaSatuanTemp.ToLower()) == null)
                        {
                            Message = "Satuan " + _namaSatuanTemp + " tidak ada dalam daftar satuan (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }
                    }
                }
            }

            //jika valid
            if (valid)
            {
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    List<Cell> listData = new List<Cell>();

                    for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                    {
                        //Cell(1) = _bahanBaku;
                        //Cell(3 sampai n) = _hargaVendor;

                        Cell cell = row.GetCell(colIndex);

                        listData.Add(cell);
                    }

                    if (!listData[1].IsEmpty)
                    {
                        string _namaBahanBakuTemp = listData[1].StringValue;

                        for (int kolom = 3; kolom < listData.Count; kolom++)
                        {
                            TBSupplier supplier = listSupplier.FirstOrDefault(item => item.Nama.ToLower() == sheet.Cells.GetRow(0).GetCell(kolom).StringValue.ToLower());
                            TBStokBahanBaku stokBahanBaku = listStokBahanBaku.FirstOrDefault(item => item.TBBahanBaku.Nama.ToLower() == _namaBahanBakuTemp.ToLower());

                            if (supplier != null && stokBahanBaku != null)
                            {
                                decimal biaya = listData[kolom].StringValue.ToDecimal();

                                if (biaya > 0)
                                {
                                    TBSatuan satuan = listSatuan.FirstOrDefault(item => item.Nama.ToLower() == listData[2].StringValue.ToLower());

                                    HargaSupplier_Class.Update(db, supplier, stokBahanBaku, listData[kolom].StringValue.ToDecimal());
                                }
                            }
                        }
                    }
                }
            }
            db.SubmitChanges();
        }
    }

    public void ImportVendor()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarNama = new List<string>();

        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = sheet.Cells.GetRow(rowIndex);

            for (int colIndex = 0; colIndex <= 6; colIndex++)
            {
                Cell cell = row.GetCell(colIndex);

                if (colIndex == 1)
                {
                    if (cell.IsEmpty)
                    {
                        Message = "Kolom Nama Vendor wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                        valid = false; break;
                    }
                }
            }

            if (valid)
            {
                string _namaTemp = row.GetCell(1).StringValue;

                if (_daftarNama.FirstOrDefault(item => item.ToLower() == _namaTemp.ToLower()) == null)
                    _daftarNama.Add(_namaTemp);
                else
                {
                    Message = "Nama Vendor " + _namaTemp + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                    valid = false; break;
                }
            }
        }

        //jika valid
        if (valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    Cell _nomorUrut = row.GetCell(0);
                    Cell _namaVendor = row.GetCell(1);
                    Cell _alamat = row.GetCell(2);
                    Cell _email = row.GetCell(3);
                    Cell _telepon1 = row.GetCell(4);
                    Cell _telepon2 = row.GetCell(5);
                    Cell _persentaseTax = row.GetCell(6);

                    #region Vendor
                    TBVendor dataVendor = db.TBVendors.FirstOrDefault(item => item.Nama.ToLower() == _namaVendor.StringValue.ToLower());

                    if (dataVendor == null)
                    {
                        dataVendor = new TBVendor
                        {
                            Nama = _namaVendor.StringValue,
                            Alamat = _alamat.StringValue,
                            Email = _email.StringValue,
                            Telepon1 = _telepon1.StringValue,
                            Telepon2 = _telepon2.StringValue,
                            PersentaseTax = _persentaseTax.StringValue.ToDecimal()
                        };
                        db.TBVendors.InsertOnSubmit(dataVendor);
                    }
                    else
                    {
                        dataVendor.Alamat = _alamat.StringValue;
                        dataVendor.Email = _email.StringValue;
                        dataVendor.Telepon1 = _telepon1.StringValue;
                        dataVendor.Telepon2 = _telepon2.StringValue;
                        dataVendor.PersentaseTax = _persentaseTax.StringValue.ToDecimal();
                    }
                    #endregion
                }
                db.SubmitChanges();
            }
        }
    }

    public void ImportHargaVendor()
    {
        Workbook book = Workbook.Load(LokasiFileExcel);
        Worksheet sheet = book.Worksheets[0];
        bool valid = true;
        List<string> _daftarNamaProduk = new List<string>();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var listVendor = db.TBVendors.ToArray();
            var listStokProduk = db.TBStokProduks.ToArray();

            for (int colIndex = 3; colIndex <= sheet.Cells.LastColIndex; colIndex++)
            {
                Cell cell = sheet.Cells.GetRow(0).GetCell(colIndex);

                if (!cell.IsEmpty)
                {
                    TBVendor vendor = listVendor.FirstOrDefault(item => item.Nama.ToLower() == cell.StringValue.ToLower());

                    if (vendor == null)
                    {
                        Message = "Vendor " + cell.StringValue + " tidak ada dalam daftar vendor";
                        valid = false; break;
                    }
                }
            }

            if (valid)
            {
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                    {
                        Cell cell = row.GetCell(colIndex);

                        if (colIndex >= 3)
                        {
                            if (!row.GetCell(1).IsEmpty)
                            {
                                if (cell.IsEmpty)
                                {
                                    Message = sheet.Cells.GetRow(0).GetCell(colIndex).StringValue + " wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                                    valid = false; break;
                                }
                            }
                        }
                    }

                    if (valid)
                    {
                        string _namaProdukTemp = row.GetCell(1).StringValue;

                        string _namaKombinasiProduk;

                        if (!string.IsNullOrWhiteSpace(row.GetCell(2).StringValue))
                        {
                            _namaKombinasiProduk = _namaProdukTemp + " (" + row.GetCell(2).StringValue + ")";
                        }
                        else
                        {
                            _namaKombinasiProduk = _namaProdukTemp;
                        }

                        if (_daftarNamaProduk.FirstOrDefault(item => item.ToLower() == _namaKombinasiProduk.ToLower()) == null)
                        {
                            if (!row.GetCell(1).IsEmpty)
                            {
                                _daftarNamaProduk.Add(_namaKombinasiProduk.ToLower());
                            }
                        }
                        else
                        {
                            Message = "Produk " + _namaKombinasiProduk + " tidak boleh ada yang duplikat (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }

                        if (!row.GetCell(1).IsEmpty && listStokProduk.FirstOrDefault(item => item.TBKombinasiProduk.Nama.ToLower() == _namaKombinasiProduk.ToLower()) == null)
                        {
                            Message = "Produk " + _namaKombinasiProduk + " tidak ada dalam daftar produk (Baris ke " + (rowIndex + 1).ToString() + ")";
                            valid = false; break;
                        }
                    }
                }
            }

            //jika valid
            if (valid)
            {
                for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);

                    List<Cell> listData = new List<Cell>();

                    for (int colIndex = 0; colIndex <= sheet.Cells.LastColIndex; colIndex++)
                    {
                        //Cell(1) = _produk;
                        //Cell(2) = _varian;
                        //Cell(3 sampai n) = _hargaVendor;

                        Cell cell = row.GetCell(colIndex);

                        listData.Add(cell);
                    }

                    if (!listData[1].IsEmpty)
                    {
                        string _namaProdukTemp = listData[1].StringValue;

                        string _namaKombinasiProduk;

                        if (!string.IsNullOrWhiteSpace(listData[2].StringValue))
                        {
                            _namaKombinasiProduk = _namaProdukTemp + " (" + row.GetCell(2).StringValue + ")";
                        }
                        else
                        {
                            _namaKombinasiProduk = _namaProdukTemp;
                        }

                        for (int kolom = 3; kolom < listData.Count; kolom++)
                        {
                            TBVendor vendor = listVendor.FirstOrDefault(item => item.Nama.ToLower() == sheet.Cells.GetRow(0).GetCell(kolom).StringValue.ToLower());
                            TBStokProduk stokProduk = listStokProduk.FirstOrDefault(item => item.TBKombinasiProduk.Nama.ToLower() == _namaKombinasiProduk.ToLower());

                            if (vendor != null && stokProduk != null)
                            {
                                decimal biaya = listData[kolom].StringValue.ToDecimal();

                                if (biaya > 0)
                                {
                                    HargaVendor_Class.Update(db, vendor, stokProduk, listData[kolom].StringValue.ToDecimal());
                                }
                            }
                        }
                    }
                }
            }
            db.SubmitChanges();
        }
    }
    public void ImportProduk(string jenisImport, string sessionCode)
    {
        Workbook Book = Workbook.Load(LokasiFileExcel);
        Worksheet Sheet = Book.Worksheets[0];
        bool Valid = true;

        #region VALIDASI EXCEL
        List<KeyValue> ProdukVarian = new List<KeyValue>();

        for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = Sheet.Cells.GetRow(rowIndex);

            Cell _CellNomor = row.GetCell(0);
            Cell _CellBrand = row.GetCell(2);
            Cell _CellProduk = row.GetCell(3);
            Cell _CellVarian = row.GetCell(4);
            Cell _CellQuantity = row.GetCell(7);
            Cell _CellHargaBeli = row.GetCell(8);
            Cell _CellHargaJual = row.GetCell(9);
            Cell _CellKategori = row.GetCell(10);

            #region VALIDASI EXCEL WAJIB DIISI
            if (_CellNomor.IsEmpty)
            {
                Message = "Kolom Nomor wajib diisi";
                Valid = false; break;
            }

            if (_CellBrand.IsEmpty)
            {
                Message = "Kolom Brand wajib diisi";
                Valid = false; break;
            }

            if (_CellProduk.IsEmpty)
            {
                Message = "Kolom Produk wajib diisi";
                Valid = false; break;
            }

            if (_CellQuantity.IsEmpty)
            {
                Message = "Kolom Quantity wajib diisi";
                Valid = false; break;
            }

            if (_CellHargaBeli.IsEmpty)
            {
                Message = "Kolom Harga Beli wajib diisi";
                Valid = false; break;
            }

            if (_CellHargaJual.IsEmpty)
            {
                Message = "Kolom Harga Jual wajib diisi";
                Valid = false; break;
            }

            if (_CellKategori.IsEmpty)
            {
                Message = "Kolom Kategori wajib diisi";
                Valid = false; break;
            }
            #endregion

            //PENGECEKAN DUPLIKAT DATA DALAM 1 EXCEL
            if (ProdukVarian.FirstOrDefault(item => item.Key == _CellProduk.StringValue && item.Value == _CellVarian.StringValue) == null)
                ProdukVarian.Add(new KeyValue
                {
                    Key = _CellProduk.StringValue,
                    Value = _CellVarian.StringValue
                });
            else
            {
                Message = "Produk " + _CellProduk.StringValue + " (" + _CellVarian.StringValue + ") tidak boleh duplikat";
                Valid = false;
                break;
            }
        }
        #endregion

        if (Valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Produk_Class ClassProduk = new Produk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                List<StokProduk_ModelImport> ListPerubahanStokProduk = new List<StokProduk_ModelImport>();

                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    //0. No. -
                    //1. Kode
                    //2. Brand -
                    //3. Produk -
                    //4. Varian
                    //5. Warna
                    //6. Berat
                    //7. Quantity -
                    //8. Harga Beli -
                    //9. Harga Jual -
                    //10. Kategori -
                    //11. Keterangan
                    //12. Vendor

                    Cell _nomor = row.GetCell(0);
                    Cell _kode = row.GetCell(1);
                    Cell _brand = row.GetCell(2);
                    Cell _produk = row.GetCell(3);
                    Cell _varian = row.GetCell(4);
                    Cell _warna = row.GetCell(5);
                    Cell _berat = row.GetCell(6);
                    Cell _quantity = row.GetCell(7);
                    Cell _hargaBeli = row.GetCell(8);
                    Cell _hargaJual = row.GetCell(9);
                    Cell _kategori = row.GetCell(10);
                    Cell _keterangan = row.GetCell(11);
                    Cell _vendor = row.GetCell(12);
                    #endregion

                    #region PRODUK
                    //PENCARIAN PRODUK BERDASARKAN NAMA
                    var Produk = ClassProduk.Cari(_produk.StringValue);

                    if (Produk == null)
                        Produk = ClassProduk.Tambah(_kategori.StringValue, _warna.StringValue, _brand.StringValue, _produk.StringValue);
                    else
                        Produk = ClassProduk.Ubah(Produk, _warna.StringValue, _brand.StringValue);
                    #endregion

                    //KATEGORI PRODUK
                    KategoriProduk_Class.KategoriProduk(db, Produk, _kategori.StringValue);

                    #region KOMBINASI PRODUK
                    string NamaKombinasiProduk = KombinasiProduk_Class.NamaKombinasiProduk(_produk.StringValue, _varian.StringValue);

                    //PENCARIAN KOMBINASI PRODUK BERDASARKAN NAMA
                    var KombinasiProduk = KombinasiProduk_Class.Cari(db, NamaKombinasiProduk);

                    if (KombinasiProduk == null)
                        KombinasiProduk = KombinasiProduk_Class.Tambah(db, Produk, "", _varian.StringValue, _kode.StringValue, _berat.StringValue.ToDecimal(), _keterangan.StringValue);
                    else
                        KombinasiProduk = KombinasiProduk_Class.Ubah(db, PenggunaLogin.IDTempat, KombinasiProduk, Produk, "", _varian.StringValue, _kode.StringValue, _berat.StringValue.ToDecimal(), _keterangan.StringValue);
                    #endregion

                    #region STOK PRODUK
                    var StokProduk = StokProduk_Class.Cari(PenggunaLogin.IDTempat, KombinasiProduk.IDKombinasiProduk);

                    if (StokProduk == null)
                    {
                        if (_hargaBeli.Format.FormatType == CellFormatType.Percentage)
                            StokProduk = StokProduk_Class.MembuatStokKonsinyasi(0, PenggunaLogin.IDTempat, PenggunaLogin.IDPengguna, KombinasiProduk, _hargaBeli.StringValue.ToDecimal(), _hargaJual.StringValue.ToDecimal(), "");
                        else
                            StokProduk = StokProduk_Class.MembuatStok(0, PenggunaLogin.IDTempat, PenggunaLogin.IDPengguna, KombinasiProduk, _hargaBeli.StringValue.ToDecimal(), _hargaJual.StringValue.ToDecimal(), "");
                    }
                    else
                    {
                        if (_hargaBeli.Format.FormatType == CellFormatType.Percentage)
                        {
                            StokProduk.HargaBeli = _hargaJual.StringValue.ToDecimal() - (_hargaJual.StringValue.ToDecimal() * _hargaBeli.StringValue.ToDecimal());
                            StokProduk.PersentaseKonsinyasi = _hargaBeli.StringValue.ToDecimal();
                        }
                        else
                        {
                            StokProduk.HargaBeli = _hargaBeli.StringValue.ToDecimal();
                            StokProduk.PersentaseKonsinyasi = 0;
                        }

                        StokProduk.HargaJual = _hargaJual.StringValue.ToDecimal();
                    }

                    ListPerubahanStokProduk.Add(new StokProduk_ModelImport
                    {
                        StokProduk = StokProduk,
                        Jumlah = _quantity.StringValue.ToInt()
                    });
                    #endregion

                    #region VENDOR
                    if (!_vendor.IsEmpty)
                    {
                        Vendor_Class ClassVendor = new Vendor_Class(db);

                        var Vendor = ClassVendor.CariTambah(_vendor.StringValue);

                        HargaVendor_Class.Update(db, Vendor, StokProduk, StokProduk.HargaBeli.Value);
                    }
                    #endregion

                    db.SubmitChanges();

                    if (string.IsNullOrEmpty(_kode.StringValue))
                        KombinasiProduk_Class.PengaturanBarcode(db, PenggunaLogin.IDTempat, KombinasiProduk);
                }

                decimal TotalPersediaan = 0;

                foreach (var item in ListPerubahanStokProduk)
                {
                    if (jenisImport == "1") //JENIS 1 PENYESUAIAN STOK PRODUK
                    {
                        //PENYESUAIAN
                        int SelisihStok = item.StokProduk.Jumlah.Value - item.Jumlah;

                        if (SelisihStok > 0)
                            TotalPersediaan -= SelisihStok * item.StokProduk.HargaBeli.Value;
                        if (SelisihStok < 0)
                            TotalPersediaan += Math.Abs(SelisihStok) * item.StokProduk.HargaBeli.Value;

                        StokProduk_Class.Penyesuaian(PenggunaLogin.IDTempat, PenggunaLogin.IDPengguna, item.StokProduk, item.Jumlah, "Import Excel " + sessionCode);
                    }
                    else
                    {
                        if (item.Jumlah > 0)
                        {
                            TotalPersediaan += item.Jumlah * item.StokProduk.HargaBeli.Value;

                            StokProduk_Class.BertambahBerkurang(PenggunaLogin.IDTempat, PenggunaLogin.IDPengguna, item.StokProduk, item.Jumlah, item.StokProduk.HargaBeli.Value, item.StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.RestockBarang, "Import Excel " + sessionCode);
                        }
                        if (item.Jumlah < 0)
                        {
                            TotalPersediaan -= Math.Abs(item.Jumlah) * item.StokProduk.HargaBeli.Value;

                            StokProduk_Class.BertambahBerkurang(PenggunaLogin.IDTempat, PenggunaLogin.IDPengguna, item.StokProduk, item.Jumlah, item.StokProduk.HargaBeli.Value, item.StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.StokOpnameBerkurang, "Import Excel " + sessionCode);
                        }
                    }
                }

                //JURNAL
                if (TotalPersediaan != 0)
                {
                    Jurnal_Class Jurnal_Class = new Jurnal_Class();
                    List<TBJurnalDetail> DetailJurnal = new List<TBJurnalDetail>();

                    if (TotalPersediaan > 0)
                    {
                        //400 - PERSEDIAAN
                        DetailJurnal.Add(new TBJurnalDetail
                        {
                            IDAkun = 400,
                            Debit = TotalPersediaan,
                            Kredit = 0
                        });

                        //389 - MODAL DISETOR
                        DetailJurnal.Add(new TBJurnalDetail
                        {
                            IDAkun = 389,
                            Debit = 0,
                            Kredit = TotalPersediaan
                        });
                    }
                    else if (TotalPersediaan < 0)
                    {
                        TotalPersediaan = Math.Abs(TotalPersediaan);

                        //389 - MODAL DISETOR
                        DetailJurnal.Add(new TBJurnalDetail
                        {
                            IDAkun = 389,
                            Debit = TotalPersediaan,
                            Kredit = 0
                        });

                        //400 - PERSEDIAAN
                        DetailJurnal.Add(new TBJurnalDetail
                        {
                            IDAkun = 400,
                            Debit = 0,
                            Kredit = TotalPersediaan
                        });
                    }

                    Jurnal_Class.Tambah(db, DateTime.Now, "Import Excel " + sessionCode, PenggunaLogin.IDPengguna, "", DetailJurnal);
                }

                db.SubmitChanges();
            }
        }
    }
    public List<string> ImportProdukChecker(string sessionCode)
    {
        List<string> DataDuplicate = new List<string>();

        Workbook Book = Workbook.Load(LokasiFileExcel);
        Worksheet Sheet = Book.Worksheets[0];
        bool Valid = true;

        #region VALIDASI EXCEL
        List<KeyValue> ProdukVarian = new List<KeyValue>();

        for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = Sheet.Cells.GetRow(rowIndex);

            Cell _CellNomor = row.GetCell(0);
            Cell _CellBrand = row.GetCell(2);
            Cell _CellProduk = row.GetCell(3);
            Cell _CellVarian = row.GetCell(4);
            Cell _CellQuantity = row.GetCell(7);
            Cell _CellHargaBeli = row.GetCell(8);
            Cell _CellHargaJual = row.GetCell(9);
            Cell _CellKategori = row.GetCell(10);

            #region VALIDASI EXCEL WAJIB DIISI
            if (_CellNomor.IsEmpty)
            {
                Message = "Kolom Nomor wajib diisi";
                Valid = false; break;
            }

            if (_CellBrand.IsEmpty)
            {
                Message = "Kolom Brand wajib diisi";
                Valid = false; break;
            }

            if (_CellProduk.IsEmpty)
            {
                Message = "Kolom Produk wajib diisi";
                Valid = false; break;
            }

            if (_CellQuantity.IsEmpty)
            {
                Message = "Kolom Quantity wajib diisi";
                Valid = false; break;
            }

            if (_CellHargaBeli.IsEmpty)
            {
                Message = "Kolom Harga Beli wajib diisi";
                Valid = false; break;
            }

            if (_CellHargaJual.IsEmpty)
            {
                Message = "Kolom Harga Jual wajib diisi";
                Valid = false; break;
            }

            if (_CellKategori.IsEmpty)
            {
                Message = "Kolom Kategori wajib diisi";
                Valid = false; break;
            }
            #endregion

            //PENGECEKAN DUPLIKAT DATA DALAM 1 EXCEL
            if (ProdukVarian.FirstOrDefault(item => item.Key == _CellProduk.StringValue && item.Value == _CellVarian.StringValue) == null)
                ProdukVarian.Add(new KeyValue
                {
                    Key = _CellProduk.StringValue,
                    Value = _CellVarian.StringValue
                });
            else
            {
                Message = "Produk " + _CellProduk.StringValue + " (" + _CellVarian.StringValue + ") tidak boleh duplikat";
                Valid = false;
                break;
            }
        }
        #endregion

        if (Valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Produk_Class ClassProduk = new Produk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                List<StokProduk_Model> ListPerubahanStokProduk = new List<StokProduk_Model>();

                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    //0. No. -
                    //1. Kode
                    //2. Brand -
                    //3. Produk -
                    //4. Varian
                    //5. Warna
                    //6. Berat
                    //7. Quantity -
                    //8. Harga Beli -
                    //9. Harga Jual -
                    //10. Kategori -
                    //11. Keterangan
                    //12. Vendor

                    Cell _nomor = row.GetCell(0);
                    Cell _kode = row.GetCell(1);
                    Cell _brand = row.GetCell(2);
                    Cell _produk = row.GetCell(3);
                    Cell _varian = row.GetCell(4);
                    Cell _warna = row.GetCell(5);
                    Cell _berat = row.GetCell(6);
                    Cell _quantity = row.GetCell(7);
                    Cell _hargaBeli = row.GetCell(8);
                    Cell _hargaJual = row.GetCell(9);
                    Cell _kategori = row.GetCell(10);
                    Cell _keterangan = row.GetCell(11);
                    Cell _vendor = row.GetCell(12);
                    #endregion

                    #region KOMBINASI PRODUK
                    string NamaKombinasiProduk = KombinasiProduk_Class.NamaKombinasiProduk(_produk.StringValue, _varian.StringValue);

                    //PENCARIAN KOMBINASI PRODUK BERDASARKAN NAMA
                    var KombinasiProduk = KombinasiProduk_Class.Cari(db, NamaKombinasiProduk);

                    if (KombinasiProduk != null)
                    {
                        DataDuplicate.Add("Produk = " + _produk.StringValue + ", Varian = " + _varian.StringValue);
                    }
                    #endregion
                }
            }
        }

        return DataDuplicate;
    }

    public void ImportKurir()
    {
        Workbook Book = Workbook.Load(LokasiFileExcel);
        Worksheet Sheet = Book.Worksheets[0];
        bool Valid = true;

        #region VALIDASI EXCEL
        for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
        {
            Row row = Sheet.Cells.GetRow(rowIndex);

            Cell _CellNomor = row.GetCell(0);
            Cell _CellKurir = row.GetCell(1);
            Cell _CellNegara = row.GetCell(2);
            Cell _CellProvinsi = row.GetCell(3);
            Cell _CellKota = row.GetCell(4);
            Cell _CellZona = row.GetCell(5);
            Cell _CellBiaya = row.GetCell(6);

            #region VALIDASI EXCEL WAJIB DIISI
            if (_CellNomor.IsEmpty)
            {
                Message = "Kolom Nomor wajib diisi";
                Valid = false; break;
            }

            if (_CellKurir.IsEmpty)
            {
                Message = "Kolom Kurir wajib diisi";
                Valid = false; break;
            }

            if (_CellNegara.IsEmpty)
            {
                Message = "Kolom Negara wajib diisi";
                Valid = false; break;
            }

            if (_CellProvinsi.IsEmpty)
            {
                Message = "Kolom Provinsi wajib diisi";
                Valid = false; break;
            }

            if (_CellKota.IsEmpty)
            {
                Message = "Kolom Kota wajib diisi";
                Valid = false; break;
            }

            if (_CellZona.IsEmpty)
            {
                Message = "Kolom Zona wajib diisi";
                Valid = false; break;
            }

            if (_CellBiaya.IsEmpty)
            {
                Message = "Kolom Biaya wajib diisi";
                Valid = false; break;
            }
            #endregion
        }
        #endregion

        if (Valid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Wilayah_Class ClassWilayah = new Wilayah_Class(db);

                #region KURIR
                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    Cell _kurir = row.GetCell(1);
                    #endregion

                    var Kurir = db.TBKurirs.FirstOrDefault(item => item.Nama.ToLower() == _kurir.StringValue.ToLower());

                    if (Kurir == null)
                    {
                        db.TBKurirs.InsertOnSubmit(new TBKurir
                        {
                            //IDKurir
                            Nama = _kurir.StringValue,
                            Deskripsi = "",
                            _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                            _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                            _IDTempatInsert = PenggunaLogin.IDTempat,
                            _IDTempatUpdate = PenggunaLogin.IDTempat,
                            _IDWMS = Guid.NewGuid(),
                            _IDWMSStore = PenggunaLogin.IDWMSStore,
                            _IsActive = true,
                            _TanggalInsert = DateTime.Now,
                            _TanggalUpdate = DateTime.Now,
                            _Urutan = 1
                        });

                        db.SubmitChanges();
                    }
                }
                #endregion

                #region NEGARA
                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    Cell _negara = row.GetCell(2);
                    #endregion

                    var Negara = ClassWilayah.Negara(_negara.StringValue);

                    if (Negara == null)
                    {
                        db.TBWilayahs.InsertOnSubmit(new TBWilayah
                        {
                            IDGrupWilayah = 1, //NEGARA
                            Nama = _negara.StringValue,
                            _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                            _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                            _IDTempatInsert = PenggunaLogin.IDTempat,
                            _IDTempatUpdate = PenggunaLogin.IDTempat,
                            _IDWMS = Guid.NewGuid(),
                            _IDWMSStore = PenggunaLogin.IDWMSStore,
                            _IsActive = true,
                            _TanggalInsert = DateTime.Now,
                            _TanggalUpdate = DateTime.Now,
                            _Urutan = 1
                        });

                        db.SubmitChanges();
                    }
                }
                #endregion

                #region PROVINSI
                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    Cell _negara = row.GetCell(2);
                    Cell _provinsi = row.GetCell(3);
                    #endregion

                    var Provinsi = ClassWilayah.Provinsi(_negara.StringValue, _provinsi.StringValue);

                    if (Provinsi == null)
                    {
                        db.TBWilayahs.InsertOnSubmit(new TBWilayah
                        {
                            IDGrupWilayah = 2, //PROVINSI
                            TBWilayah1 = ClassWilayah.Negara(_negara.StringValue),
                            Nama = _provinsi.StringValue,
                            _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                            _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                            _IDTempatInsert = PenggunaLogin.IDTempat,
                            _IDTempatUpdate = PenggunaLogin.IDTempat,
                            _IDWMS = Guid.NewGuid(),
                            _IDWMSStore = PenggunaLogin.IDWMSStore,
                            _IsActive = true,
                            _TanggalInsert = DateTime.Now,
                            _TanggalUpdate = DateTime.Now,
                            _Urutan = 1
                        });

                        db.SubmitChanges();
                    }
                }
                #endregion

                #region KOTA
                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    Cell _negara = row.GetCell(2);
                    Cell _provinsi = row.GetCell(3);
                    Cell _kota = row.GetCell(4);
                    #endregion

                    var Kota = ClassWilayah.Kota(_negara.StringValue, _provinsi.StringValue, _kota.StringValue);

                    if (Kota == null)
                    {
                        db.TBWilayahs.InsertOnSubmit(new TBWilayah
                        {
                            IDGrupWilayah = 3, //KOTA
                            TBWilayah1 = ClassWilayah.Provinsi(_negara.StringValue, _provinsi.StringValue),
                            Nama = _kota.StringValue,
                            _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                            _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                            _IDTempatInsert = PenggunaLogin.IDTempat,
                            _IDTempatUpdate = PenggunaLogin.IDTempat,
                            _IDWMS = Guid.NewGuid(),
                            _IDWMSStore = PenggunaLogin.IDWMSStore,
                            _IsActive = true,
                            _TanggalInsert = DateTime.Now,
                            _TanggalUpdate = DateTime.Now,
                            _Urutan = 1
                        });

                        db.SubmitChanges();
                    }
                }
                #endregion

                #region ZONA DAN BIAYA PENGIRIMAN
                for (int rowIndex = Sheet.Cells.FirstRowIndex + 1; rowIndex <= Sheet.Cells.LastRowIndex; rowIndex++)
                {
                    #region KOLOM EXCEL
                    Row row = Sheet.Cells.GetRow(rowIndex);

                    Cell _kurir = row.GetCell(1);
                    Cell _negara = row.GetCell(2);
                    Cell _provinsi = row.GetCell(3);
                    Cell _kota = row.GetCell(4);
                    Cell _zona = row.GetCell(5);
                    Cell _biaya = row.GetCell(6);
                    #endregion

                    var Zona = ClassWilayah.Zona(_negara.StringValue, _provinsi.StringValue, _kota.StringValue, _zona.StringValue);
                    var Kurir = db.TBKurirs.FirstOrDefault(item => item.Nama.ToLower() == _kurir.StringValue.ToLower());

                    if (Zona == null)
                    {
                        //ZONA TIDAK DITEMUKAN
                        //INSERT ZONA
                        Zona = new TBWilayah
                        {
                            IDGrupWilayah = 4, //ZONA
                            TBWilayah1 = ClassWilayah.Kota(_negara.StringValue, _provinsi.StringValue, _kota.StringValue),
                            Nama = _zona.StringValue,
                            _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                            _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                            _IDTempatInsert = PenggunaLogin.IDTempat,
                            _IDTempatUpdate = PenggunaLogin.IDTempat,
                            _IDWMS = Guid.NewGuid(),
                            _IDWMSStore = PenggunaLogin.IDWMSStore,
                            _IsActive = true,
                            _TanggalInsert = DateTime.Now,
                            _TanggalUpdate = DateTime.Now,
                            _Urutan = 1
                        };

                        //INSERT BIAYA PENGIRIMAN
                        var KurirBiaya = new TBKurirBiaya
                        {
                            Biaya = _biaya.StringValue.ToDecimal(),
                            TBKurir = Kurir,
                            TBWilayah = Zona,
                            _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                            _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                            _IDTempatInsert = PenggunaLogin.IDTempat,
                            _IDTempatUpdate = PenggunaLogin.IDTempat,
                            _IDWMS = Guid.NewGuid(),
                            _IDWMSStore = PenggunaLogin.IDWMSStore,
                            _IsActive = true,
                            _TanggalInsert = DateTime.Now,
                            _TanggalUpdate = DateTime.Now,
                            _Urutan = 1
                        };

                        db.TBKurirBiayas.InsertOnSubmit(KurirBiaya);
                    }
                    else
                    {
                        //ZONA DITEMUKAN
                        var KurirBiaya = db.TBKurirBiayas.FirstOrDefault(item => item.IDKurir == Kurir.IDKurir && item.IDWilayah == Zona.IDWilayah);

                        if (KurirBiaya == null)
                        {
                            //INSERT BIAYA PENGIRIMAN
                            KurirBiaya = new TBKurirBiaya
                            {
                                Biaya = _biaya.StringValue.ToDecimal(),
                                TBKurir = Kurir,
                                TBWilayah = Zona,
                                _IDPenggunaInsert = PenggunaLogin.IDPengguna,
                                _IDPenggunaUpdate = PenggunaLogin.IDPengguna,
                                _IDTempatInsert = PenggunaLogin.IDTempat,
                                _IDTempatUpdate = PenggunaLogin.IDTempat,
                                _IDWMS = Guid.NewGuid(),
                                _IDWMSStore = PenggunaLogin.IDWMSStore,
                                _IsActive = true,
                                _TanggalInsert = DateTime.Now,
                                _TanggalUpdate = DateTime.Now,
                                _Urutan = 1
                            };

                            db.TBKurirBiayas.InsertOnSubmit(KurirBiaya);
                        }
                        else
                        {
                            //UPDATE BIAYA PENGIRIMAN
                            KurirBiaya.Biaya = _biaya.StringValue.ToDecimal();
                            KurirBiaya._TanggalUpdate = DateTime.Now;
                        }
                    }

                    db.SubmitChanges();
                }
                #endregion
            }
        }
    }
}