using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Transfer_Terima_Import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        string NamaFile = Path.GetFileNameWithoutExtension(FileUploadTransferBahanBaku.FileName);
        string ExtensiFile = Path.GetExtension(FileUploadTransferBahanBaku.FileName);

        #region Format import harus .zip
        if (ExtensiFile != ".zip")
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Format import harus .zip");
            return;
        }
        #endregion

        if (FileUploadTransferBahanBaku.HasFile)
        {
            string Folder = Server.MapPath("~/Files/Transfer Bahan Baku/Penerimaan/");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            string LokasiFile = Folder + NamaFile + ExtensiFile;
            string Output = Folder + NamaFile + "_dec" + ExtensiFile;

            FileUploadTransferBahanBaku.SaveAs(LokasiFile);

            EncryptDecrypt.Decrypt(LokasiFile, Output);

            string file = File.ReadAllText(Output);

            File.Delete(Output);

            var FileTransferBahanBaku = JsonConvert.DeserializeObject<FileTransferBahanBaku>(file);

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                #region Transfer transfer sudah terdaftar
                if (db.TBTransferBahanBakus.FirstOrDefault(item => item.IDTransferBahanBaku == FileTransferBahanBaku.IDTransferBahanBaku) != null)
                {
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Data Transfer sudah terdaftar");
                    return;
                }
                #endregion

                Tempat_Class ClassTempat = new Tempat_Class(db);
                BahanBaku_Class ClassBahanBaku = new BahanBaku_Class();
                KategoriBahanBaku_Class KategoriBahanBaku_Class = new KategoriBahanBaku_Class();

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                #region TEMPAT PENERIMA
                var TempatPenerima = ClassTempat.Cari(FileTransferBahanBaku.FileTempatPenerima.IDWMS);

                if (TempatPenerima == null)
                {
                    TempatPenerima = new TBTempat
                    {
                        Alamat = FileTransferBahanBaku.FileTempatPenerima.Alamat,
                        BiayaTambahan1 = FileTransferBahanBaku.FileTempatPenerima.BiayaTambahan1,
                        BiayaTambahan2 = FileTransferBahanBaku.FileTempatPenerima.BiayaTambahan2,
                        BiayaTambahan3 = FileTransferBahanBaku.FileTempatPenerima.BiayaTambahan3,
                        BiayaTambahan4 = FileTransferBahanBaku.FileTempatPenerima.BiayaTambahan4,
                        Email = FileTransferBahanBaku.FileTempatPenerima.Email,
                        EnumBiayaTambahan1 = FileTransferBahanBaku.FileTempatPenerima.EnumBiayaTambahan1,
                        EnumBiayaTambahan2 = FileTransferBahanBaku.FileTempatPenerima.EnumBiayaTambahan2,
                        EnumBiayaTambahan3 = FileTransferBahanBaku.FileTempatPenerima.EnumBiayaTambahan3,
                        EnumBiayaTambahan4 = FileTransferBahanBaku.FileTempatPenerima.EnumBiayaTambahan4,
                        FooterPrint = FileTransferBahanBaku.FileTempatPenerima.FooterPrint,
                        IDKategoriTempat = FileTransferBahanBaku.FileTempatPenerima.IDKategoriTempat,
                        IDStore = FileTransferBahanBaku.FileTempatPenerima.IDStore,
                        _IDWMS = FileTransferBahanBaku.FileTempatPenerima.IDWMS,
                        KeteranganBiayaTambahan1 = FileTransferBahanBaku.FileTempatPenerima.KeteranganBiayaTambahan1,
                        KeteranganBiayaTambahan2 = FileTransferBahanBaku.FileTempatPenerima.KeteranganBiayaTambahan2,
                        KeteranganBiayaTambahan3 = FileTransferBahanBaku.FileTempatPenerima.KeteranganBiayaTambahan3,
                        KeteranganBiayaTambahan4 = FileTransferBahanBaku.FileTempatPenerima.KeteranganBiayaTambahan4,
                        Kode = FileTransferBahanBaku.FileTempatPenerima.Kode,
                        Latitude = FileTransferBahanBaku.FileTempatPenerima.Latitude,
                        Longitude = FileTransferBahanBaku.FileTempatPenerima.Longitude,
                        Nama = FileTransferBahanBaku.FileTempatPenerima.Nama,
                        _TanggalInsert = FileTransferBahanBaku.FileTempatPenerima.TanggalDaftar,
                        _TanggalUpdate = FileTransferBahanBaku.FileTempatPenerima.TanggalUpdate,
                        Telepon1 = FileTransferBahanBaku.FileTempatPenerima.Telepon1,
                        Telepon2 = FileTransferBahanBaku.FileTempatPenerima.Telepon2
                    };

                    db.TBTempats.InsertOnSubmit(TempatPenerima);
                    db.SubmitChanges();
                }
                #endregion

                //MASTER DATA
                foreach (var item in FileTransferBahanBaku.TransferBahanBakuDetails)
                {
                    #region BAHAN BAKU
                    var BahanBaku = ClassBahanBaku.Cari(db, item.BahanBaku);

                    if (BahanBaku == null)
                        BahanBaku = ClassBahanBaku.Tambah(db, item.SatuanKecil, item.SatuanBesar, item.BahanBaku, item.Konversi);
                    else
                        BahanBaku = ClassBahanBaku.Ubah(db, BahanBaku, item.SatuanKecil, item.SatuanBesar, item.Konversi);
                    #endregion

                    #region KATEGORI
                    KategoriBahanBaku_Class.KategoriBahanBaku(db, BahanBaku, item.Kategori);
                    #endregion

                    #region STOK BAHAN BAKU
                    var stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(data => data.IDBahanBaku == BahanBaku.IDBahanBaku && data.IDTempat == TempatPenerima.IDTempat);

                    if (stokBahanBaku == null)
                    {
                        stokBahanBaku = StokBahanBaku_Class.InsertStokBahanBaku(db, DateTime.Now, Pengguna.IDPengguna, TempatPenerima.IDTempat, BahanBaku, (item.HargaBeli / item.Konversi), 0, 0, "");
                    }
                    else
                    {
                        stokBahanBaku.HargaBeli = item.HargaBeli;
                    }
                    #endregion

                    db.SubmitChanges();
                }

                #region TEMPAT PENGIRIM
                var TempatPengirim = ClassTempat.Cari(FileTransferBahanBaku.FileTempatPengirim.IDWMS);

                if (TempatPengirim == null)
                {
                    TempatPengirim = new TBTempat
                    {
                        Alamat = FileTransferBahanBaku.FileTempatPengirim.Alamat,
                        BiayaTambahan1 = FileTransferBahanBaku.FileTempatPengirim.BiayaTambahan1,
                        BiayaTambahan2 = FileTransferBahanBaku.FileTempatPengirim.BiayaTambahan2,
                        BiayaTambahan3 = FileTransferBahanBaku.FileTempatPengirim.BiayaTambahan3,
                        BiayaTambahan4 = FileTransferBahanBaku.FileTempatPengirim.BiayaTambahan4,
                        Email = FileTransferBahanBaku.FileTempatPengirim.Email,
                        EnumBiayaTambahan1 = FileTransferBahanBaku.FileTempatPengirim.EnumBiayaTambahan1,
                        EnumBiayaTambahan2 = FileTransferBahanBaku.FileTempatPengirim.EnumBiayaTambahan2,
                        EnumBiayaTambahan3 = FileTransferBahanBaku.FileTempatPengirim.EnumBiayaTambahan3,
                        EnumBiayaTambahan4 = FileTransferBahanBaku.FileTempatPengirim.EnumBiayaTambahan4,
                        FooterPrint = FileTransferBahanBaku.FileTempatPengirim.FooterPrint,
                        IDKategoriTempat = FileTransferBahanBaku.FileTempatPengirim.IDKategoriTempat,
                        IDStore = FileTransferBahanBaku.FileTempatPengirim.IDStore,
                        _IDWMS = FileTransferBahanBaku.FileTempatPengirim.IDWMS,
                        KeteranganBiayaTambahan1 = FileTransferBahanBaku.FileTempatPengirim.KeteranganBiayaTambahan1,
                        KeteranganBiayaTambahan2 = FileTransferBahanBaku.FileTempatPengirim.KeteranganBiayaTambahan2,
                        KeteranganBiayaTambahan3 = FileTransferBahanBaku.FileTempatPengirim.KeteranganBiayaTambahan3,
                        KeteranganBiayaTambahan4 = FileTransferBahanBaku.FileTempatPengirim.KeteranganBiayaTambahan4,
                        Kode = FileTransferBahanBaku.FileTempatPengirim.Kode,
                        Latitude = FileTransferBahanBaku.FileTempatPengirim.Latitude,
                        Longitude = FileTransferBahanBaku.FileTempatPengirim.Longitude,
                        Nama = FileTransferBahanBaku.FileTempatPengirim.Nama,
                        _TanggalInsert = FileTransferBahanBaku.FileTempatPengirim.TanggalDaftar,
                        _TanggalUpdate = FileTransferBahanBaku.FileTempatPengirim.TanggalUpdate,
                        Telepon1 = FileTransferBahanBaku.FileTempatPengirim.Telepon1,
                        Telepon2 = FileTransferBahanBaku.FileTempatPengirim.Telepon2
                    };
                }
                #endregion

                #region PENGGUNA PENGIRIM
                var PenggunaPengirim = db.TBPenggunas
                    .FirstOrDefault(item => item.Username.ToLower() == FileTransferBahanBaku.FilePenggunaPengirim.Username.ToLower());

                if (PenggunaPengirim == null)
                {
                    //PENGGUNA PENGIRIM
                    PenggunaPengirim = new TBPengguna
                    {
                        IDGrupPengguna = FileTransferBahanBaku.FilePenggunaPengirim.IDGrupPengguna,
                        NamaLengkap = FileTransferBahanBaku.FilePenggunaPengirim.NamaLengkap,
                        Username = FileTransferBahanBaku.FilePenggunaPengirim.Username,
                        Password = FileTransferBahanBaku.FilePenggunaPengirim.Password,
                        PIN = FileTransferBahanBaku.FilePenggunaPengirim.PIN,
                        _IsActive = FileTransferBahanBaku.FilePenggunaPengirim.Status,
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

                #region TRANSFER BAHAN BAKU
                TBTransferBahanBaku TransferBahanBaku = new TBTransferBahanBaku
                {
                    IDTransferBahanBaku = FileTransferBahanBaku.IDTransferBahanBaku,
                    //Nomor
                    TBPengguna = PenggunaPengirim,
                    //IDPenerima
                    TBTempat = TempatPengirim,
                    IDTempatPenerima = TempatPenerima.IDTempat,
                    TanggalDaftar = FileTransferBahanBaku.TanggalDaftar,
                    TanggalUpdate = FileTransferBahanBaku.TanggalUpdate,
                    EnumJenisTransfer = FileTransferBahanBaku.EnumJenisTransfer,
                    TanggalKirim = FileTransferBahanBaku.TanggalKirim,
                    //TanggalTerima
                    TotalJumlah = FileTransferBahanBaku.TotalJumlah,
                    GrandTotal = FileTransferBahanBaku.GrandTotal,
                    Keterangan = FileTransferBahanBaku.Keterangan
                };
                #endregion

                #region DETAIL TRANSFER BAHAN BAKU
                foreach (var item in FileTransferBahanBaku.TransferBahanBakuDetails)
                {
                    var BahanBaku = db.TBBahanBakus.FirstOrDefault(data => data.Nama.ToLower() == item.BahanBaku.ToLower());

                    TransferBahanBaku.TBTransferBahanBakuDetails.Add(new TBTransferBahanBakuDetail
                    {
                        //IDTransferBahanBakuDetail
                        //IDTransferBahanBaku
                        TBBahanBaku = BahanBaku,
                        TBSatuan = BahanBaku.TBSatuan1,
                        HargaBeli = item.HargaBeli,
                        Jumlah = item.Jumlah
                        //Subtotal
                    });
                }
                #endregion

                db.TBTransferBahanBakus.InsertOnSubmit(TransferBahanBaku);
                db.SubmitChanges();

                if (TransferBahanBaku.IDTempatPenerima == Pengguna.IDTempat)
                    Response.Redirect("Pengaturan.aspx?id=" + TransferBahanBaku.IDTransferBahanBaku);
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}