using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class HargaVendor_Class
{
    public static void Update(DataClassesDatabaseDataContext db, TBVendor vendor, TBStokProduk stokProduk, decimal harga)
    {
        if (vendor != null)
        {
            TBHargaVendor hargaVendor = db.TBHargaVendors.FirstOrDefault(item => item.TBVendor == vendor && item.TBStokProduk == stokProduk);

            if (hargaVendor == null)
            {
                hargaVendor = new TBHargaVendor
                {
                    TBVendor = vendor,
                    Tanggal = DateTime.Now,
                    Harga = harga,
                    TBStokProduk = stokProduk
                };

                db.TBHargaVendors.InsertOnSubmit(hargaVendor);
            }
            else
            {
                hargaVendor.Tanggal = DateTime.Now;
                hargaVendor.Harga = harga;
            }
        }        
    }

    public static TBHargaVendor CariHargaVendor(DataClassesDatabaseDataContext db, TBVendor vendor, TBStokProduk stokProduk)
    {
        return db.TBHargaVendors.FirstOrDefault(item => item.TBVendor == vendor && item.TBStokProduk == stokProduk);
    }

    public static TBHargaVendor CariHargaVendor(DataClassesDatabaseDataContext db, int IDVendor, int IDStokProduk, int IDKombinasiProduk)
    {
        if (IDStokProduk != 0)
            return db.TBHargaVendors.FirstOrDefault(item => item.IDVendor == IDVendor && item.IDStokProduk == IDStokProduk);
        else if (IDKombinasiProduk != 0)
            return db.TBHargaVendors.FirstOrDefault(item => item.IDVendor == IDVendor && item.TBStokProduk.IDKombinasiProduk == IDKombinasiProduk);
        else
            return null;
    }
}
