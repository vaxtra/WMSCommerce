using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public static partial class JenisBiayaProyeksi_Class
{
    public static TBJenisBiayaProyeksi CariTambahJenisBiayaProyeksiByUrutanNama(DataClassesDatabaseDataContext db, int urutan, string namaJenisBiayaProyeksi)
    {
        if (!string.IsNullOrWhiteSpace(namaJenisBiayaProyeksi))
        {
            TBJenisBiayaProyeksi jenisBiayaProyeksi = db.TBJenisBiayaProyeksis.FirstOrDefault(item => item.Nama.ToLower() == namaJenisBiayaProyeksi.ToLower());

            if (jenisBiayaProyeksi == null)
            {
                jenisBiayaProyeksi = new TBJenisBiayaProyeksi { Urutan = urutan, Nama = namaJenisBiayaProyeksi };
                db.TBJenisBiayaProyeksis.InsertOnSubmit(jenisBiayaProyeksi);
            }
            else
            {
                jenisBiayaProyeksi.Urutan = urutan;
                jenisBiayaProyeksi.Nama = namaJenisBiayaProyeksi;
            }

            return jenisBiayaProyeksi;
        }
        else
            return null;
    }
}