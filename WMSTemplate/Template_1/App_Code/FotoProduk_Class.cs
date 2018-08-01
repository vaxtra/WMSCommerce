using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class FotoProduk_Class
{
    public TBFotoProduk Tambah(DataClassesDatabaseDataContext db, int idProduk, string extensiFoto)
    {
        var FotoProduk = new TBFotoProduk
        {
            //IDFotoProduk
            IDProduk = idProduk,
            ExtensiFoto = extensiFoto,
            FotoUtama = false
        };

        db.TBFotoProduks.InsertOnSubmit(FotoProduk);
        db.SubmitChanges();

        return FotoProduk;
    }
    public TBFotoProduk Cari(DataClassesDatabaseDataContext db, int idFotoProduk)
    {
        return db.TBFotoProduks.FirstOrDefault(item => item.IDFotoProduk == idFotoProduk);
    }
    public bool Hapus(DataClassesDatabaseDataContext db, int idFotoProduk)
    {
        var FotoProduk = Cari(db, idFotoProduk);

        if (FotoProduk != null)
        {
            if (FotoProduk.TBFotoKombinasiProduks.Count > 0)
                db.TBFotoKombinasiProduks.DeleteAllOnSubmit(FotoProduk.TBFotoKombinasiProduks);

            string FileFoto = HttpContext.Current.Server.MapPath("~/images/Produk/") + FotoProduk.IDFotoProduk + FotoProduk.ExtensiFoto;

            if (File.Exists(FileFoto))
                File.Delete(FileFoto);

            db.TBFotoProduks.DeleteOnSubmit(FotoProduk);

            return true;
        }
        else
            return false;
    }
    public TBFotoProduk FotoUtama(DataClassesDatabaseDataContext db, int idFotoProduk)
    {
        var FotoProduk = Cari(db, idFotoProduk);

        if (FotoProduk != null)
        {
            var FotoUtamaLama = db.TBFotoProduks
                .FirstOrDefault(item =>
                    item.IDProduk == FotoProduk.IDProduk &&
                    item.FotoUtama == true);

            //MERUBAH FOTO UTAMA YANG LAMA
            if (FotoUtamaLama != null)
                FotoUtamaLama.FotoUtama = false;

            //MERUBAH FOTO UTAMA YANG BARU
            FotoProduk.FotoUtama = true;

            string FolderCover = HttpContext.Current.Server.MapPath("~/images/Cover/");

            if (!Directory.Exists(FolderCover))
                Directory.CreateDirectory(FolderCover);

            string Foto = HttpContext.Current.Server.MapPath("~/images/Produk/") + FotoProduk.IDFotoProduk + FotoProduk.ExtensiFoto;
            string Cover = FolderCover + FotoProduk.IDProduk + ".jpg";

            if (File.Exists(Foto))
            {
                if (File.Exists(Cover))
                    File.Delete(Cover);

                File.Copy(Foto, Cover);
            }
        }

        return FotoProduk;
    }
    public TBFotoProduk[] Data(int idProduk)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return Data(db, idProduk);
        }
    }
    public TBFotoProduk[] Data(DataClassesDatabaseDataContext db, int idProduk)
    {
        return db.TBFotoProduks.Where(item => item.IDProduk == idProduk).ToArray();
    }
}