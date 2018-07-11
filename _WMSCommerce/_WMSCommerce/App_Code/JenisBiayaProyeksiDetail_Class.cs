using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public static partial class JenisBiayaProyeksiDetail_Class
{
    public static TBJenisBiayaProyeksiDetail CariTambahJenisBiayaProyeksiDetailByNama(DataClassesDatabaseDataContext db, TBJenisBiayaProyeksi jenisBiayaProyeksi, string namaJenisBiayaProyeksiDetail, int enumBiayaProyeksi, decimal persentase, decimal nominal, int statusBatas)
    {
        if (!string.IsNullOrWhiteSpace(namaJenisBiayaProyeksiDetail))
        {
            TBJenisBiayaProyeksiDetail jenisBiayaProyeksiDetail = db.TBJenisBiayaProyeksiDetails.FirstOrDefault(item => item.TBJenisBiayaProyeksi.Nama.ToLower() == namaJenisBiayaProyeksiDetail.ToLower() && item.StatusBatas == statusBatas);

            if (jenisBiayaProyeksiDetail == null)
            {
                jenisBiayaProyeksiDetail = new TBJenisBiayaProyeksiDetail
                {
                    TBJenisBiayaProyeksi = jenisBiayaProyeksi,
                    EnumBiayaProyeksi = enumBiayaProyeksi,
                    Persentase = persentase,
                    Nominal = nominal,
                    StatusBatas = statusBatas
                };
                db.TBJenisBiayaProyeksiDetails.InsertOnSubmit(jenisBiayaProyeksiDetail);
            }
            else
            {
                jenisBiayaProyeksiDetail.TBJenisBiayaProyeksi = jenisBiayaProyeksi;
                jenisBiayaProyeksiDetail.EnumBiayaProyeksi = enumBiayaProyeksi;
                jenisBiayaProyeksiDetail.Persentase = persentase;
                jenisBiayaProyeksiDetail.Nominal = nominal;
                jenisBiayaProyeksiDetail.StatusBatas = statusBatas;
            }

            return jenisBiayaProyeksiDetail;
        }
        else
            return null;
    }
}