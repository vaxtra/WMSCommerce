using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_PindahTempat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void ButtonSync_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext dbLocal = new DataClassesDatabaseDataContext())
        {
            using (DataClassesServerDataContext dbServer = new DataClassesServerDataContext())
            {
                int JumlahDataInsert = 0;
                int JumlahDataUpdate = 0;

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                //MEMBUAT LOG
                var LogSync = new TBSyncDatabaseLog
                {
                    IDPengguna = Pengguna.IDPengguna,
                    TanggalMulai = DateTime.Now,
                };

                dbLocal.TBSyncDatabaseLogs.InsertOnSubmit(LogSync);
                dbLocal.SubmitChanges();

                //AMBIL SEMUA DATA DARI SERVER
                foreach (var DataServer in dbServer.TBWarnas.ToArray())
                {
                    var DataLocal = dbLocal.TBWarnas
                        .FirstOrDefault(item => item._IDWMS == DataServer._IDWMS);

                    if (DataLocal == null)
                    {
                        //JIKA DATA TIDAK ADA DI DATABASE SENDIRI
                        //INSERT DATA BARU
                        dbLocal.TBWarnas.InsertOnSubmit(new TBWarna
                        {
                            //IDWarna
                            Kode = DataServer.Kode,
                            Nama = DataServer.Nama,

                            _IDPenggunaInsert = DataServer._IDPenggunaInsert,
                            _IDPenggunaUpdate = DataServer._IDPenggunaUpdate,
                            _IDTempatInsert = DataServer._IDTempatInsert,
                            _IDTempatUpdate = DataServer._IDTempatUpdate,
                            _IDWMS = DataServer._IDWMS,
                            _IDWMSStore = DataServer._IDWMSStore,
                            _IsActive = DataServer._IsActive,
                            _TanggalInsert = DataServer._TanggalInsert,
                            _TanggalUpdate = DataServer._TanggalUpdate,
                            _Urutan = DataServer._Urutan
                        });

                        dbLocal.SubmitChanges();

                        JumlahDataInsert++;
                    }
                    else if (DataLocal._TanggalUpdate != DataServer._TanggalUpdate)
                    {
                        //JIKA TANGGAL UPDATE BERBEDA
                        //UPDATE DATA LAMA

                        //IDWarna
                        DataLocal.Kode = DataServer.Kode;
                        DataLocal.Nama = DataServer.Nama;

                        DataLocal._IDPenggunaInsert = DataServer._IDPenggunaInsert;
                        DataLocal._IDPenggunaUpdate = DataServer._IDPenggunaUpdate;
                        DataLocal._IDTempatInsert = DataServer._IDTempatInsert;
                        DataLocal._IDTempatUpdate = DataServer._IDTempatUpdate;
                        DataLocal._IDWMS = DataServer._IDWMS;
                        DataLocal._IDWMSStore = DataServer._IDWMSStore;
                        DataLocal._IsActive = DataServer._IsActive;
                        DataLocal._TanggalInsert = DataServer._TanggalInsert;
                        DataLocal._TanggalUpdate = DataServer._TanggalUpdate;
                        DataLocal._Urutan = DataServer._Urutan;

                        dbLocal.SubmitChanges();

                        JumlahDataUpdate++;
                    }
                }

                var Result = "Sync TBWarna Insert : " + JumlahDataInsert + ", Update : " + JumlahDataUpdate;

                //MENAMPILKAN OUTPUT
                LiteralData.Text = Result;

                //MENYIMPAN HASIL DI LOG
                LogSync.TanggalSelesai = DateTime.Now;
                LogSync.Pesan = Result;

                Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(dbLocal, EnumAlert.success, "Sync Selesai");

                dbLocal.SubmitChanges();
            }
        }
    }
}