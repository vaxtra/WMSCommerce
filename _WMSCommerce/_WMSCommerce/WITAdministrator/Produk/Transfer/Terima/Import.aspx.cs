using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Terima_Import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        string NamaFile = Path.GetFileNameWithoutExtension(FileUploadTransferProduk.FileName);
        string ExtensiFile = Path.GetExtension(FileUploadTransferProduk.FileName);

        #region Format import harus .zip
        if (ExtensiFile != ".zip")
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Format import harus .zip");
            return;
        }
        #endregion

        if (FileUploadTransferProduk.HasFile)
        {
            string Folder = Server.MapPath("~/Files/Transfer Produk/Penerimaan/");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            string LokasiFile = Folder + NamaFile + ExtensiFile;
            string Output = Folder + NamaFile + "_dec" + ExtensiFile;

            FileUploadTransferProduk.SaveAs(LokasiFile);

            EncryptDecrypt.Decrypt(LokasiFile, Output);

            string file = File.ReadAllText(Output);

            File.Delete(Output);

            var FileTransferProduk = JsonConvert.DeserializeObject<FileTransferProduk>(file);

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                #region Transfer transfer sudah terdaftar
                if (db.TBTransferProduks.FirstOrDefault(item => item.IDTransferProduk == FileTransferProduk.IDTransferProduk) != null)
                {
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Data Transfer sudah terdaftar");
                    return;
                }
                #endregion

                Tempat_Class ClassTempat = new Tempat_Class(db);
                Produk_Class ClassProduk = new Produk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                #region TEMPAT PENERIMA
                var TempatPenerima = ClassTempat.Cari(FileTransferProduk.FileTempatPenerima.IDWMS);

                if (TempatPenerima == null)
                {
                    TempatPenerima = new TBTempat
                    {
                        Alamat = FileTransferProduk.FileTempatPenerima.Alamat,
                        BiayaTambahan1 = FileTransferProduk.FileTempatPenerima.BiayaTambahan1,
                        BiayaTambahan2 = FileTransferProduk.FileTempatPenerima.BiayaTambahan2,
                        BiayaTambahan3 = FileTransferProduk.FileTempatPenerima.BiayaTambahan3,
                        BiayaTambahan4 = FileTransferProduk.FileTempatPenerima.BiayaTambahan4,
                        Email = FileTransferProduk.FileTempatPenerima.Email,
                        EnumBiayaTambahan1 = FileTransferProduk.FileTempatPenerima.EnumBiayaTambahan1,
                        EnumBiayaTambahan2 = FileTransferProduk.FileTempatPenerima.EnumBiayaTambahan2,
                        EnumBiayaTambahan3 = FileTransferProduk.FileTempatPenerima.EnumBiayaTambahan3,
                        EnumBiayaTambahan4 = FileTransferProduk.FileTempatPenerima.EnumBiayaTambahan4,
                        FooterPrint = FileTransferProduk.FileTempatPenerima.FooterPrint,
                        IDKategoriTempat = FileTransferProduk.FileTempatPenerima.IDKategoriTempat,
                        IDStore = FileTransferProduk.FileTempatPenerima.IDStore,
                        _IDWMS = FileTransferProduk.FileTempatPenerima.IDWMS,
                        KeteranganBiayaTambahan1 = FileTransferProduk.FileTempatPenerima.KeteranganBiayaTambahan1,
                        KeteranganBiayaTambahan2 = FileTransferProduk.FileTempatPenerima.KeteranganBiayaTambahan2,
                        KeteranganBiayaTambahan3 = FileTransferProduk.FileTempatPenerima.KeteranganBiayaTambahan3,
                        KeteranganBiayaTambahan4 = FileTransferProduk.FileTempatPenerima.KeteranganBiayaTambahan4,
                        Kode = FileTransferProduk.FileTempatPenerima.Kode,
                        Latitude = FileTransferProduk.FileTempatPenerima.Latitude,
                        Longitude = FileTransferProduk.FileTempatPenerima.Longitude,
                        Nama = FileTransferProduk.FileTempatPenerima.Nama,
                        _TanggalInsert = FileTransferProduk.FileTempatPenerima.TanggalDaftar,
                        _TanggalUpdate = FileTransferProduk.FileTempatPenerima.TanggalUpdate,
                        Telepon1 = FileTransferProduk.FileTempatPenerima.Telepon1,
                        Telepon2 = FileTransferProduk.FileTempatPenerima.Telepon2
                    };

                    db.TBTempats.InsertOnSubmit(TempatPenerima);
                    db.SubmitChanges();
                }
                #endregion

                //MASTER DATA
                foreach (var item in FileTransferProduk.TransferProdukDetails)
                {
                    #region PRODUK
                    var Produk = ClassProduk.Cari(item.Produk);

                    if (Produk == null)
                        Produk = ClassProduk.Tambah(item.Kategori, item.Warna, item.PemilikProduk, item.Produk);
                    else
                        Produk = ClassProduk.Ubah(Produk, item.Warna, item.PemilikProduk);
                    #endregion

                    #region KATEGORI
                    KategoriProduk_Class.KategoriProduk(db, Produk, item.Kategori);
                    #endregion

                    #region KOMBINASI PRODUK
                    var KombinasiProduk = KombinasiProduk_Class.Cari(db, item.KombinasiProduk);

                    if (KombinasiProduk == null)
                    {
                        AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

                        KombinasiProduk = KombinasiProduk_Class.Tambah(db, Produk, ClassAtributProduk.CariTambah("", item.Atribut), item.TanggalDaftar, item.TanggalUpdate, item.Kode, item.Berat, item.Keterangan);
                    }
                    else
                        KombinasiProduk = KombinasiProduk_Class.Ubah(db, TempatPenerima.IDTempat, KombinasiProduk, Produk, "", item.Atribut, item.Kode, item.Berat, item.Keterangan);
                    #endregion

                    #region STOK PRODUK
                    var StokProduk = StokProduk_Class.Cari(TempatPenerima.IDTempat, KombinasiProduk.IDKombinasiProduk);

                    if (StokProduk == null)
                    {
                        if (item.PersentaseKonsinyasi > 0)
                            StokProduk_Class.MembuatStokKonsinyasi(0, TempatPenerima.IDTempat, Pengguna.IDPengguna, KombinasiProduk, item.PersentaseKonsinyasi, item.HargaJual, "");
                        else
                            StokProduk_Class.MembuatStok(0, TempatPenerima.IDTempat, Pengguna.IDPengguna, KombinasiProduk, item.HargaBeli, item.HargaJual, "");
                    }
                    else
                    {
                        StokProduk.HargaBeli = item.HargaBeli;
                        StokProduk.HargaJual = item.HargaJual;
                        StokProduk.PersentaseKonsinyasi = item.PersentaseKonsinyasi;
                    }
                    #endregion

                    db.SubmitChanges();
                }

                #region TEMPAT PENGIRIM
                var TempatPengirim = ClassTempat.Cari(FileTransferProduk.FileTempatPengirim.IDWMS);

                if (TempatPengirim == null)
                {
                    TempatPengirim = new TBTempat
                    {
                        Alamat = FileTransferProduk.FileTempatPengirim.Alamat,
                        BiayaTambahan1 = FileTransferProduk.FileTempatPengirim.BiayaTambahan1,
                        BiayaTambahan2 = FileTransferProduk.FileTempatPengirim.BiayaTambahan2,
                        BiayaTambahan3 = FileTransferProduk.FileTempatPengirim.BiayaTambahan3,
                        BiayaTambahan4 = FileTransferProduk.FileTempatPengirim.BiayaTambahan4,
                        Email = FileTransferProduk.FileTempatPengirim.Email,
                        EnumBiayaTambahan1 = FileTransferProduk.FileTempatPengirim.EnumBiayaTambahan1,
                        EnumBiayaTambahan2 = FileTransferProduk.FileTempatPengirim.EnumBiayaTambahan2,
                        EnumBiayaTambahan3 = FileTransferProduk.FileTempatPengirim.EnumBiayaTambahan3,
                        EnumBiayaTambahan4 = FileTransferProduk.FileTempatPengirim.EnumBiayaTambahan4,
                        FooterPrint = FileTransferProduk.FileTempatPengirim.FooterPrint,
                        IDKategoriTempat = FileTransferProduk.FileTempatPengirim.IDKategoriTempat,
                        IDStore = FileTransferProduk.FileTempatPengirim.IDStore,
                        _IDWMS = FileTransferProduk.FileTempatPengirim.IDWMS,
                        KeteranganBiayaTambahan1 = FileTransferProduk.FileTempatPengirim.KeteranganBiayaTambahan1,
                        KeteranganBiayaTambahan2 = FileTransferProduk.FileTempatPengirim.KeteranganBiayaTambahan2,
                        KeteranganBiayaTambahan3 = FileTransferProduk.FileTempatPengirim.KeteranganBiayaTambahan3,
                        KeteranganBiayaTambahan4 = FileTransferProduk.FileTempatPengirim.KeteranganBiayaTambahan4,
                        Kode = FileTransferProduk.FileTempatPengirim.Kode,
                        Latitude = FileTransferProduk.FileTempatPengirim.Latitude,
                        Longitude = FileTransferProduk.FileTempatPengirim.Longitude,
                        Nama = FileTransferProduk.FileTempatPengirim.Nama,
                        _TanggalInsert = FileTransferProduk.FileTempatPengirim.TanggalDaftar,
                        _TanggalUpdate = FileTransferProduk.FileTempatPengirim.TanggalUpdate,
                        Telepon1 = FileTransferProduk.FileTempatPengirim.Telepon1,
                        Telepon2 = FileTransferProduk.FileTempatPengirim.Telepon2
                    };
                }
                #endregion

                #region PENGGUNA PENGIRIM
                var PenggunaPengirim = db.TBPenggunas
                    .FirstOrDefault(item => item.Username.ToLower() == FileTransferProduk.FilePenggunaPengirim.Username.ToLower());

                if (PenggunaPengirim == null)
                {
                    //PENGGUNA PENGIRIM
                    PenggunaPengirim = new TBPengguna
                    {
                        IDGrupPengguna = FileTransferProduk.FilePenggunaPengirim.IDGrupPengguna,
                        NamaLengkap = FileTransferProduk.FilePenggunaPengirim.NamaLengkap,
                        Username = FileTransferProduk.FilePenggunaPengirim.Username,
                        Password = FileTransferProduk.FilePenggunaPengirim.Password,
                        PIN = FileTransferProduk.FilePenggunaPengirim.PIN,
                        _IsActive = FileTransferProduk.FilePenggunaPengirim.Status,
                        TBTempat = TempatPengirim,
                        TanggalLahir = DateTime.Now,
                        _IDWMS = Guid.NewGuid(),
                        TanggalBekerja = DateTime.Now,
                        _TanggalInsert = DateTime.Now,
                        _IDTempatInsert = TempatPenerima.IDTempat,
                        _IDPenggunaInsert = Pengguna.IDTempat,
                        _TanggalUpdate = DateTime.Now,
                        _IDTempatUpdate = TempatPenerima.IDTempat,
                        _IDPenggunaUpdate = Pengguna.IDTempat
                    };
                }
                #endregion

                #region TRANSFER PRODUK
                TBTransferProduk TransferProduk = new TBTransferProduk
                {
                    IDTransferProduk = FileTransferProduk.IDTransferProduk,
                    //Nomor
                    TBPengguna = PenggunaPengirim,
                    //IDPenerima
                    TBTempat = TempatPengirim,
                    IDTempatPenerima = TempatPenerima.IDTempat,
                    TanggalDaftar = FileTransferProduk.TanggalDaftar,
                    TanggalUpdate = FileTransferProduk.TanggalUpdate,
                    EnumJenisTransfer = FileTransferProduk.EnumJenisTransfer,
                    TanggalKirim = FileTransferProduk.TanggalKirim,
                    //TanggalTerima
                    TotalJumlah = FileTransferProduk.TotalJumlah,
                    GrandTotalHargaBeli = FileTransferProduk.GrandTotalHargaBeli,
                    GrandTotalHargaJual = FileTransferProduk.GrandTotalHargaJual,
                    Keterangan = FileTransferProduk.Keterangan
                };
                #endregion

                #region DETAIL TRANSFER PRODUK
                foreach (var item in FileTransferProduk.TransferProdukDetails)
                {
                    var KombinasiProduk = KombinasiProduk_Class.Cari(db, item.KombinasiProduk);

                    TransferProduk.TBTransferProdukDetails.Add(new TBTransferProdukDetail
                    {
                        //IDTransferProdukDetail
                        //IDTransferProduk
                        TBKombinasiProduk = KombinasiProduk,
                        HargaBeli = item.HargaBeli,
                        HargaJual = item.HargaJual,
                        Jumlah = item.Jumlah
                        //SubtotalHargaBeli
                        //SubtotalHargaJual
                    });
                }
                #endregion

                db.TBTransferProduks.InsertOnSubmit(TransferProduk);
                db.SubmitChanges();

                if (TransferProduk.IDTempatPenerima == Pengguna.IDTempat)
                    Response.Redirect("Pengaturan.aspx?id=" + TransferProduk.IDTransferProduk);
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}