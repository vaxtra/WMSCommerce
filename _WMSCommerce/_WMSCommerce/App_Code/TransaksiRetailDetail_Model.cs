using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class TransaksiRetailDetail_Model
{
    public int IDDetailTransaksi { get; set; }

    //IDTransaksi

    public int IDKombinasiProduk { get; set; }
    public int IDStokProduk { get; set; }
    public int Quantity { get; set; }
    public decimal Berat { get; set; }
    public decimal HargaBeliKotor { get; set; }

    #region HargaBeli
    public decimal HargaBeli
    {
        get
        {
            return HargaBeliKotor - DiscountKonsinyasi;
        }
    }
    #endregion

    public decimal HargaJual { get; set; }

    #region HargaJualBersih
    public decimal HargaJualBersih
    {
        get
        {
            return HargaJual - Discount;
        }
    }
    #endregion

    public decimal DiscountStore { get; set; }
    public decimal DiscountKonsinyasi { get; set; }

    #region Discount
    public decimal Discount
    {
        get
        {
            return DiscountStore + DiscountKonsinyasi;
        }
    }
    #endregion

    //Revenue

    #region TotalBerat
    public decimal TotalBerat
    {
        get
        {
            return Quantity * Berat;
        }
    }
    #endregion

    //TotalHargaBeliKotor

    #region TotalHargaBeli
    public decimal TotalHargaBeli
    {
        get
        {
            return Quantity * HargaBeli;
        }
    }
    #endregion

    public decimal TotalHargaJual
    {
        get
        {
            return Quantity * HargaJual;
        }
    }

    public decimal TotalDiscount
    {
        get
        {
            return Quantity * Discount;
        }
    }

    //TotalRevenue

    #region Subtotal
    public decimal Subtotal
    {
        get
        {
            return Quantity * HargaJualBersih;
        }
    }
    #endregion

    public string Keterangan { get; set; }

    //TAMBAHAN
    public string Nama { get; set; }
    public string Barcode { get; set; }

    public decimal PersentaseDiscount
    {
        get
        {
            return HargaJual > 0 ? (Discount / HargaJual * 100) : 0;
        }
    }

    public bool UbahQuantity { get; set; }

    public TransaksiRetailDetail_Model()
    {
        //UNTUK LOAD DARI TRANSAKSI DARI DATABASE
    }
    public TransaksiRetailDetail_Model(int idTempat, int idDetailTransaksi, int idKombinasiProduk, int jumlahProduk, bool ubahQuantity)
    {
        if (idDetailTransaksi > 0 && idKombinasiProduk > 0 && jumlahProduk > 0)
        {
            //PENCARIAN KOMBINASI PRODUK BERDASARKAN ID KOMBINASI PRODUK
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                //ID DETAIL TRANSAKSI, ID KOMBINASI PRODUK, JUMLAH PRODUK VALID
                var StokProduk = db.TBStokProduks
                .FirstOrDefault(item =>
                    item.IDKombinasiProduk == idKombinasiProduk &&
                    item.IDTempat == idTempat);

                //JIKA STOK PRODUK TIDAK ADA MAKA AKAN MEMBUAT STOK PRODUK DARI TEMPAT LAINNYA
                if (StokProduk == null)
                {
                    var TempStokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDKombinasiProduk == idKombinasiProduk);

                    if (TempStokProduk != null)
                    {
                        StokProduk = new TBStokProduk
                        {
                            IDKombinasiProduk = TempStokProduk.IDKombinasiProduk,
                            IDTempat = idTempat,

                            HargaBeli = TempStokProduk.HargaBeli,
                            HargaJual = TempStokProduk.HargaJual,
                            PersentaseKonsinyasi = TempStokProduk.PersentaseKonsinyasi,

                            Jumlah = 0,
                            JumlahMinimum = TempStokProduk.JumlahMinimum
                        };

                        db.TBStokProduks.InsertOnSubmit(StokProduk);
                        db.SubmitChanges();
                    }
                }

                if (StokProduk != null) //STOK PRODUK DITEMUKAN 
                {
                    var KombinasiProduk = StokProduk.TBKombinasiProduk;

                    IDDetailTransaksi = idDetailTransaksi; //AUTO INCREMENT DARI TRANSAKSI

                    //IDTransaksi

                    IDKombinasiProduk = idKombinasiProduk;
                    IDStokProduk = StokProduk.IDStokProduk;
                    Quantity = jumlahProduk;
                    Berat = KombinasiProduk.Berat.Value;
                    HargaBeliKotor = StokProduk.HargaBeli.Value;

                    //HargaBeli

                    HargaJual = StokProduk.HargaJual.Value;

                    //HargaJualBersih

                    DiscountStore = 0;

                    switch ((EnumDiscount)StokProduk.EnumDiscountStore)
                    {
                        case EnumDiscount.Persentase:
                            DiscountStore = (decimal)(HargaJual * StokProduk.DiscountStore / 100);
                            break;
                        case EnumDiscount.Nominal:
                            DiscountStore = (decimal)StokProduk.DiscountStore;
                            break;
                    }

                    DiscountKonsinyasi = 0;

                    switch ((EnumDiscount)StokProduk.EnumDiscountKonsinyasi)
                    {
                        case EnumDiscount.Persentase:
                            DiscountKonsinyasi = (decimal)(HargaJual * StokProduk.DiscountKonsinyasi / 100);
                            break;
                        case EnumDiscount.Nominal:
                            DiscountKonsinyasi = (decimal)StokProduk.DiscountKonsinyasi;
                            break;
                    }

                    //Discount
                    //Revenue
                    //TotalBerat
                    //TotalHargaBeliKotor
                    //TotalHargaBeli
                    //TotalHargaJual
                    //TotalDiscount
                    //TotalRevenue
                    //Subtotal

                    Keterangan = "";

                    //TAMBAHAN
                    Nama = KombinasiProduk.Nama;
                    Barcode = KombinasiProduk.KodeKombinasiProduk;

                    //PersentaseDiscount

                    UbahQuantity = ubahQuantity;
                }
            }
        }
    }

    public void PengaturanDiscount(string stringNominalDiscount)
    {
        decimal NominalDiscount = Parse.Decimal(stringNominalDiscount.Replace("%", "").Replace(".", "").Replace(",", "."));

        //JIKA INPUT BERUPA % MAKA DISCOUNT BERUPA PERSENTASE
        if (stringNominalDiscount.Contains('%'))
            NominalDiscount = HargaJual * (NominalDiscount / 100);

        //MENCEGAH DISCOUNT MELEBIHI HARGA JUAL AWAL
        if (NominalDiscount + DiscountKonsinyasi <= HargaJual)
            DiscountStore = NominalDiscount;
        else
            DiscountStore = HargaJual;
    }
    public void PengaturanHargaJualBersih(decimal hargaJualBersih)
    {
        //MENCEGAH HARGA JUAL TAMPIL MELEBIHI HARGA JUAL
        if (hargaJualBersih < HargaJual - DiscountKonsinyasi)
            DiscountStore = HargaJual - DiscountKonsinyasi - hargaJualBersih;
    }
    public void PengaturanSubtotal(decimal subtotal)
    {
        //MENCEGAH SUBTOTAL MELEBIHI SUBTOTAL YANG SEHARUSNYA
        if (subtotal < ((HargaJual - DiscountKonsinyasi) * Quantity))
            DiscountStore = HargaJual - DiscountKonsinyasi - (subtotal / Quantity);
    }
}