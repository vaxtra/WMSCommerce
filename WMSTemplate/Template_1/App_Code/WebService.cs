using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    public WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region TEMPLATE
    //[WebMethod]
    //public void XXXX(string XXXX, string XXXX)
    //{
    //    Context.Response.Clear();
    //    Context.Response.ContentType = "application/json";

    //    try
    //    {
    //        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
    //        {
    //            Context.Response.Write(JsonConvert.SerializeObject(new XXXXXX_WebService
    //            {
    //                Pengguna = Pengguna,
    //                Result = new WebServiceResult
    //                {
    //                    EnumWebService = (int)EnumWebService.Success,
    //                    Pesan = ""
    //                }
    //            }, Formatting.Indented));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogError_Class Error = new LogError_Class(ex, "XXXXXX");

    //        Context.Response.Write(JsonConvert.SerializeObject(new XXXXXXX_WebService
    //        {
    //            Pengguna = (string)null,
    //            Result = new WebServiceResult
    //            {
    //                EnumWebService = (int)EnumWebService.Exception,
    //                Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
    //            }
    //        }, Formatting.Indented));
    //    }
    //}
    #endregion

    #region LOGIN
    [WebMethod]
    public void Login(string username, string password)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            PenggunaLogin Pengguna = new PenggunaLogin(username, password);

            if (Pengguna.IDPengguna > 0)
            {
                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Pengguna = Pengguna,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
            else
            {
                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Pengguna = (string)null,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Failed,
                        Pesan = "Pengguna tidak ditemukan"
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Login");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Pengguna = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    [WebMethod]
    public void StokProduk(int idTempat)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var ListStokProduk = db.TBStokProduks
                    .Where(item => item.IDTempat == idTempat)
                    .Select(item => new
                    {
                        IDProduk = item.TBKombinasiProduk.TBProduk.IDProduk,
                        IDKombinasiProduk = item.IDKombinasiProduk,
                        Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
                        Nama = item.TBKombinasiProduk.Nama,
                        Produk = item.TBKombinasiProduk.TBProduk.Nama,
                        Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
                        HargaJual = item.HargaJual.Value,
                        Jumlah = item.Jumlah.Value
                    })
                    .ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    StokProduk = ListStokProduk,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "StokProduk");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                StokProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void StokProdukPenyesuaian(string barcode, int idTempat, int idPengguna, int jumlah, string keterangan)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var StokProduk = db.TBStokProduks
                    .FirstOrDefault(item =>
                        item.TBKombinasiProduk.KodeKombinasiProduk == barcode &&
                        item.IDTempat == idTempat);

                if (StokProduk != null)
                {
                    int tempStokProdukLama = StokProduk.Jumlah.Value;

                    StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                    if (StokProduk_Class.Penyesuaian(idTempat, idPengguna, StokProduk, jumlah, keterangan))
                    {
                        db.SubmitChanges();

                        Context.Response.Write(JsonConvert.SerializeObject(new
                        {
                            Result = new WebServiceResult
                            {
                                EnumWebService = (int)EnumWebService.Success,
                                Pesan = "Stok Produk " + StokProduk.TBKombinasiProduk.Nama + " berubah dari " + tempStokProdukLama + " menjadi " + jumlah
                            }
                        }, Formatting.Indented));
                    }
                    else
                    {
                        Context.Response.Write(JsonConvert.SerializeObject(new
                        {
                            Result = new WebServiceResult
                            {
                                EnumWebService = (int)EnumWebService.Failed,
                                Pesan = "Stok Produk " + barcode + " tidak ditemukan"
                            }
                        }, Formatting.Indented));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "StokProdukPenyesuaian");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void IDTransaksi(string token)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        if (token == "CAD81A2F-8A0E-44E0-91FD-3A0C1D8D7E5A")
        {
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

                    decimal KonfigurasiMinimumOrderAksesWifi = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.MinimumOrderAksesWifi).ToDecimal();

                    Context.Response.Write(JsonConvert.SerializeObject(new
                    {
                        IDTransaksi = db.TBTransaksis
                            .Where(item =>
                                item.GrandTotal >= KonfigurasiMinimumOrderAksesWifi &&
                                item.IDStatusTransaksi != (int)EnumStatusTransaksi.Canceled)
                            .OrderByDescending(item => item.Nomor)
                            .Select(item => item.IDTransaksi)
                            .ToArray(),
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                LogError_Class Error = new LogError_Class(ex, "IDTransaksi");

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    IDTransaksi = (string)null,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Exception,
                        Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                    }
                }));
            }
        }
        else
        {
            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                IDTransaksi = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Failed,
                    Pesan = "Token tidak valid"
                }
            }));
        }
    }

    [WebMethod]
    public void StoreKonfigurasi(string token, int idStoreKonfigurasi)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        if (token == "CAD81A2F-8A0E-44E0-91FD-3A0C1D8D7E5A")
        {
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

                    Context.Response.Write(JsonConvert.SerializeObject(new
                    {
                        Pengaturan = StoreKonfigurasi_Class.Pengaturan(db, (EnumStoreKonfigurasi)idStoreKonfigurasi),
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                LogError_Class Error = new LogError_Class(ex, "StoreKonfigurasi");

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Pengaturan = (string)null,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Exception,
                        Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                    }
                }));
            }
        }
        else
        {
            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Pengaturan = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Failed,
                    Pesan = "Token tidak valid"
                }
            }));
        }
    }

    #region MOBILE
    [WebMethod]
    public void AtributProduk_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBAtributProduks.Select(item => new
                {
                    item.IDAtributProduk,
                    item.Nama
                }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    AtributProduk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "AtributProduk_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                AtributProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void KombinasiProduk_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBKombinasiProduks.AsEnumerable().Select(item => new
                {
                    item.Berat,
                    item.Deskripsi,
                    item.IDAtributProduk,
                    item.IDKombinasiProduk,
                    item.IDProduk,
                    item.IDWMS,
                    item.KodeKombinasiProduk,
                    item.Nama,
                    TanggalDaftar = item.TanggalDaftar.Value,
                    TanggalUpdate = item.TanggalUpdate.Value,
                    item.Urutan
                }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    KombinasiProduk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "KombinasiProduk_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                KombinasiProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void KombinasiProdukTempat_Data(string IDWMSStore, string IDWMSTempat)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBStokProduks
                    .Where(item =>
                        item.TBTempat._IDWMSStore == Guid.Parse(IDWMSStore) &&
                        item.TBTempat._IDWMS == Guid.Parse(IDWMSTempat))
                    .Select(item => new
                    {
                        item.TBKombinasiProduk.Berat,
                        item.TBKombinasiProduk.Deskripsi,
                        item.TBKombinasiProduk.IDAtributProduk,
                        item.TBKombinasiProduk.IDKombinasiProduk,
                        item.TBKombinasiProduk.IDProduk,
                        item.TBKombinasiProduk.IDWMS,
                        item.TBKombinasiProduk.KodeKombinasiProduk,
                        item.TBKombinasiProduk.Nama,
                        TanggalDaftar = item.TBKombinasiProduk.TanggalDaftar.Value,
                        TanggalUpdate = item.TBKombinasiProduk.TanggalUpdate.Value,
                        item.TBKombinasiProduk.Urutan
                    });

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    KombinasiProduk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "KombinasiProdukTempat_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                KombinasiProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void StokProduk_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBStokProduks.Select(item => new
                {
                    item.IDStokProduk,
                    item.IDTempat,
                    item.TBKombinasiProduk.IDProduk,
                    item.IDKombinasiProduk,
                    item.TBKombinasiProduk.IDAtributProduk,
                    item.IDStokProdukPenyimpanan,

                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    KombinasiProduk = item.TBKombinasiProduk.Nama,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,

                    item.Jumlah,
                    item.JumlahMinimum,

                    item.HargaBeli,
                    item.HargaJual,

                    item.DiscountStore,
                    item.EnumDiscountStore,
                    item.DiscountKonsinyasi,
                    item.EnumDiscountKonsinyasi,

                    item.PersentaseKonsinyasi,
                    item.PajakNominal,
                    item.PajakPersentase,
                })
                .ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    StokProduk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "StokProduk_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                StokProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void StokProdukTempat_Data(string IDWMSStore, string IDWMSTempat)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBStokProduks
                    .Where(item =>
                        item.TBTempat._IDWMS == Guid.Parse(IDWMSTempat) &&
                        item.TBTempat._IDWMSStore == Guid.Parse(IDWMSStore))
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.IDTempat,
                        item.TBKombinasiProduk.IDProduk,
                        item.IDKombinasiProduk,
                        item.TBKombinasiProduk.IDAtributProduk,
                        item.IDStokProdukPenyimpanan,

                        item.TBKombinasiProduk.KodeKombinasiProduk,
                        KombinasiProduk = item.TBKombinasiProduk.Nama,
                        AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,

                        item.Jumlah,
                        item.JumlahMinimum,

                        item.HargaBeli,
                        item.HargaJual,

                        item.DiscountStore,
                        item.EnumDiscountStore,
                        item.DiscountKonsinyasi,
                        item.EnumDiscountKonsinyasi,

                        item.PersentaseKonsinyasi,
                        item.PajakNominal,
                        item.PajakPersentase,
                    })
                    .ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    StokProduk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "StokProdukTempat_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                StokProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Produk_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBProduks.AsEnumerable().Select(item => new
                {
                    item.Deskripsi,
                    item.DeskripsiSingkat,
                    item.Dilihat,
                    item.IDPemilikProduk,
                    item.IDProduk,
                    item.IDProdukKategori,
                    item.IDWarna,
                    item.KodeProduk,
                    item.Nama,

                    item._IDWMSStore,
                    item._IDWMS,
                    item._Urutan,
                    _TanggalInsert = item._TanggalInsert,
                    item._IDTempatInsert,
                    item._IDPenggunaInsert,
                    _TanggalUpdate = item._TanggalUpdate,
                    item._IDTempatUpdate,
                    item._IDPenggunaUpdate,
                    item._IsActive
                })
                .ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Produk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Produk_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Produk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void ProdukKategori_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBProdukKategoris.AsEnumerable().Select(item => new
                {
                    item.Deskripsi,
                    item.IDProdukKategori,
                    item.IDProdukKategoriParent,
                    item.Nama,

                    item._IDWMSStore,
                    item._IDWMS,
                    item._Urutan,
                    _TanggalInsert = item._TanggalInsert,
                    item._IDTempatInsert,
                    item._IDPenggunaInsert,
                    _TanggalUpdate = item._TanggalUpdate,
                    item._IDTempatUpdate,
                    item._IDPenggunaUpdate,
                    item._IsActive
                }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    ProdukKategori = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "ProdukKategori_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                ProdukKategori = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void PemilikProduk_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBPemilikProduks.Select(item => new
                {
                    item.Alamat,
                    item.Email,
                    item.IDPemilikProduk,
                    item.Nama,
                    item.Telepon1,
                    item.Telepon2
                }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    PemilikProduk = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "PemilikProduk_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                PemilikProduk = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Warna_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBWarnas.Select(item => new
                {
                    item.IDWarna,
                    item.Kode,
                    item.Nama
                }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Warna = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Warna_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Warna = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void SyncData_Insert(string IDWMSStore, string IDWMSTempat, string IDWMSPengguna, string Data)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                SyncData_Class ClassSyncData = new SyncData_Class(db, PenggunaLogin);

                var SyncData = ClassSyncData.Tambah(Guid.Parse(IDWMSStore), Guid.Parse(IDWMSTempat), Guid.Parse(IDWMSPengguna), Data);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    SyncData = new
                    {
                        SyncData.IDWMSSyncData,
                        TanggalUploadFinish = (string)null,
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "SyncData_Insert");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                SyncData = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void SyncData_UploadFinish(string IDWMSSyncData)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                SyncData_Class ClassSyncData = new SyncData_Class(db, PenggunaLogin);

                var SyncData = ClassSyncData.UploadFinish(Guid.Parse(IDWMSSyncData));

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    SyncData = new
                    {
                        IDWMSSyncData = SyncData.IDWMSSyncData,
                        TanggalUploadFinish = SyncData.TanggalUploadFinish,
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "SyncData_UploadFinish");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                SyncData = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void JenisPembayaran_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBJenisPembayarans.Select(item => new
                {
                    item.IDJenisPembayaran,
                    item.IDAkun,
                    item.IDJenisBebanBiaya,
                    item.Nama,
                    item.PersentaseBiaya
                }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    JenisPembayaran = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "JenisPembayaran_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                JenisPembayaran = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Pengguna_Data(string IDWMSStore)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBPenggunas
                    .Where(item => item._IDWMSStore == Guid.Parse(IDWMSStore))
                    .Select(item => new
                    {
                        item._IDWMS,
                        item.IDPengguna,
                        item.IDGrupPengguna,
                        item.IDTempat,
                        item.Username,
                        item.NamaLengkap
                    }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Pengguna = Data,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Pengguna_Data");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Pengguna = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void RegisterDevice(string IDWMSStore, string IDWMSTempat)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Tempat = db.TBTempats
                    .Where(item =>
                        item._IDWMSStore == Guid.Parse(IDWMSStore) &&
                        item._IDWMS == Guid.Parse(IDWMSTempat) &&
                        item._IsActive)
                    .Select(item => new
                    {
                        item.IDTempat,
                        item._IDWMS,
                        item._IDWMSStore,
                        item.IDKategoriTempat,
                        item.Kode,
                        Store = item.TBStore.Nama,
                        item.Nama,
                        item.Alamat,
                        item.Email,
                        item.Telepon1,
                        item.Telepon2,

                        item.EnumBiayaTambahan1,
                        item.KeteranganBiayaTambahan1,
                        item.BiayaTambahan1,

                        item.EnumBiayaTambahan2,
                        item.KeteranganBiayaTambahan2,
                        item.BiayaTambahan2,

                        item.EnumBiayaTambahan3,
                        item.KeteranganBiayaTambahan3,
                        item.BiayaTambahan3,

                        item.EnumBiayaTambahan4,
                        item.KeteranganBiayaTambahan4,
                        item.BiayaTambahan4,

                        item.Latitude,
                        item.Longitude,

                        item.FooterPrint

                    }).ToArray();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Tempat,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "RegisterDevice");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Tempat = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Keadaan_Darurat(string IDWMSStore, string IDWMSPengguna, string Data, string IDWMSTempat)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                SyncData_Class ClassSyncData = new SyncData_Class(db, PenggunaLogin);

                var SyncData = ClassSyncData.Tambah(Guid.Parse(IDWMSStore), Guid.Parse(IDWMSTempat), Guid.Parse(IDWMSPengguna), Data);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    SyncData = new
                    {
                        SyncData.IDWMSSyncData,
                        TanggalUploadFinish = (string)null,
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Keadaan_Darurat");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                SyncData = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    [WebMethod]
    public void SyncTransaksi()
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Data = db.TBSyncDatas
                    .Where(item =>
                        !item.TanggalSync.HasValue &&
                        item.TanggalUploadFinish.HasValue &&
                        item.Data != null &&
                        item.Data != "")
                     .OrderBy(item => item.TanggalUploadFinish);

                foreach (var SyncData in Data)
                {
                    var ResultJson = JsonConvert.DeserializeObject<Mobile.RootObject>(SyncData.Data);

                    var tempTransaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == ResultJson.Transaksi.idTransaksi);

                    Transaksi_Class Transaksi;

                    if (tempTransaksi != null)
                    {
                        //UPDATE TRANSAKSI
                        Transaksi = new Transaksi_Class(ResultJson.Transaksi.idTransaksi, ResultJson.Transaksi.idPenggunaTransaksi);

                        //RESET TRANSAKSI
                        Transaksi.ResetTransaksiDetail();

                        //RESET PEMBAYARAN
                        Transaksi.ResetPembayaran();
                    }
                    else
                    {
                        //INSERT TRANSAKSI
                        Transaksi = new Transaksi_Class(ResultJson.Transaksi.idPenggunaTransaksi, ResultJson.Transaksi.idTempat, DateTime.Parse(ResultJson.Transaksi.tanggalTransaksi));
                    }

                    Transaksi.IDJenisTransaksi = ResultJson.Transaksi.idJenisTransaksi;

                    foreach (var item in ResultJson.Transaksi.transaksiDetails)
                    {
                        int IDDetailTransaksi = Transaksi.TambahDetailTransaksi(item.idKombinasiProduk, item.quantity);

                        if (item.discount != 0)
                            Transaksi.UbahPotonganHargaJualProduk(IDDetailTransaksi, item.discount.ToFormatHarga());
                    }

                    if (ResultJson.Transaksi.transaksiJenisPembayarans.Count > 0)
                    {
                        foreach (var item in ResultJson.Transaksi.transaksiJenisPembayarans)
                        {
                            Transaksi.TambahPembayaran(DateTime.Parse(item.tanggal), ResultJson.Transaksi.idPenggunaTransaksi, item.idJenisPembayaran, (decimal)item.bayar, "");
                        }
                    }

                    if (tempTransaksi != null)
                    {
                        //UPDATE TRANSAKSI
                        if (ResultJson.Transaksi.idStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
                            Transaksi.StatusPrint = true;

                            SyncData.TanggalSync = DateTime.Now;
                            Transaksi.ConfirmTransaksi(db);
                            db.SubmitChanges();
                        }
                        else if (ResultJson.Transaksi.idStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
                            Transaksi.StatusPrint = true;

                            SyncData.TanggalSync = DateTime.Now;
                            Transaksi.ConfirmTransaksi(db);
                            db.SubmitChanges();
                        }
                        else if (ResultJson.Transaksi.idStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;

                            SyncData.TanggalSync = DateTime.Now;
                            Transaksi.ConfirmTransaksi(db);
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        //INSERT TRANSAKSI
                        if (ResultJson.Transaksi.idStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
                            Transaksi.StatusPrint = true;

                            SyncData.TanggalSync = DateTime.Now;
                            Transaksi.ConfirmTransaksi(db, ResultJson.Transaksi.idTransaksi);
                            db.SubmitChanges();
                        }
                        else if (ResultJson.Transaksi.idStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
                            Transaksi.StatusPrint = true;

                            SyncData.TanggalSync = DateTime.Now;
                            Transaksi.ConfirmTransaksi(db, ResultJson.Transaksi.idTransaksi);
                            db.SubmitChanges();
                        }
                        else if (ResultJson.Transaksi.idStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                        {
                            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Canceled;

                            SyncData.TanggalSync = DateTime.Now;
                            Transaksi.ConfirmTransaksi(db, ResultJson.Transaksi.idTransaksi);
                            db.SubmitChanges();
                        }
                    }
                }

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = "Proses Sync Selesai"
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "SyncTransaksi");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region TEMPAT
    [WebMethod]
    public void Tempat_Cari(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Tempat_Class ClassTempat = new Tempat_Class(db, PenggunaLogin);

                var Tempat = ClassTempat.Cari(Nama);

                if (Tempat != null)
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new Tempat_WebService
                    {
                        Tempat = new Tempat_Model
                        {
                            IDTempat = Tempat.IDTempat,
                            Nama = Tempat.Nama
                        },
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }, Formatting.Indented));
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new Tempat_WebService
                    {
                        Tempat = null,
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoData,
                            Pesan = "Data tidak ditemukan"
                        }
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Tempat_Cari");

            Context.Response.Write(JsonConvert.SerializeObject(new Tempat_WebService
            {
                Tempat = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Tempat_Tambah(string IDWMSStore, int IDKategoriTempat, string Kode, string Nama, string Alamat, string Email, string Telepon1, string Telepon2, string KeteranganBiayaTambahan1, decimal BiayaTambahan1, string KeteranganBiayaTambahan2, decimal BiayaTambahan2, string KeteranganBiayaTambahan3, decimal BiayaTambahan3, string KeteranganBiayaTambahan4, decimal BiayaTambahan4, string Latitude, string Longitude, string FooterPrint, bool _IsActive)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Tempat_Class ClassTempat = new Tempat_Class(db, PenggunaLogin);

                var Tempat = ClassTempat.Tambah(IDKategoriTempat, Kode, Nama, Alamat, Email, Telepon1, Telepon2, KeteranganBiayaTambahan1, BiayaTambahan1, KeteranganBiayaTambahan2, BiayaTambahan2, KeteranganBiayaTambahan3, BiayaTambahan3, KeteranganBiayaTambahan4, BiayaTambahan4, Latitude, Longitude, FooterPrint, _IsActive);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Tempat_WebService
                {
                    Tempat = new Tempat_Model
                    {
                        IDTempat = Tempat.IDTempat,
                        Nama = Tempat.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassTempat.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Tempat_Tambah");

            Context.Response.Write(JsonConvert.SerializeObject(new Tempat_WebService
            {
                Tempat = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region PENGGUNA
    [WebMethod]
    public void Pengguna_CariUsername(string IDWMSStore, string Username)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Pengguna_Class ClassPengguna = new Pengguna_Class(db, PenggunaLogin);

                var Pengguna = ClassPengguna.CariUsername(Username);

                if (Pengguna != null)
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new Pengguna_WebService
                    {
                        Pengguna = new Pengguna_Model
                        {
                            IDPengguna = Pengguna.IDPengguna,
                            NamaLengkap = Pengguna.NamaLengkap,
                            Username = Pengguna.Username
                        },
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }, Formatting.Indented));
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new Pengguna_WebService
                    {
                        Pengguna = null,
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoData,
                            Pesan = "Data tidak ditemukan"
                        }
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Pengguna_CariUsername");

            Context.Response.Write(JsonConvert.SerializeObject(new Pengguna_WebService
            {
                Pengguna = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Pengguna_Tambah(string IDWMSStore, int IDGrupPengguna, int IDTempat, string NomorIdentitas, string NomorNPWP, string NomorRekening, string NamaBank, string NamaRekening, string NamaLengkap, string TempatLahir, DateTime TanggalLahir, bool JenisKelamin, string Alamat, string Agama, string Telepon, string Handphone, string Email, string StatusPerkawinan, string Kewarganegaraan, string PendidikanTerakhir, DateTime TanggalBekerja, string Username, string Password, string PIN, string Catatan)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Pengguna_Class ClassPengguna = new Pengguna_Class(db, PenggunaLogin);

                var Pengguna = ClassPengguna.Tambah(IDGrupPengguna, IDTempat, NomorIdentitas, NomorNPWP, NomorRekening, NamaBank, NamaRekening, NamaLengkap, TempatLahir, TanggalLahir, JenisKelamin, Alamat, Agama, Telepon, Handphone, Email, StatusPerkawinan, Kewarganegaraan, PendidikanTerakhir, TanggalBekerja, Username, Password, PIN, Catatan);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Pengguna_WebService
                {
                    Pengguna = new Pengguna_Model
                    {
                        IDPengguna = Pengguna.IDPengguna,
                        NamaLengkap = Pengguna.NamaLengkap,
                        Username = Pengguna.Username
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassPengguna.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Pengguna_Tambah");

            Context.Response.Write(JsonConvert.SerializeObject(new Pengguna_WebService
            {
                Pengguna = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region WARNA
    [WebMethod]
    public void Warna_Cari(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Warna_Class ClassWarna = new Warna_Class(db, PenggunaLogin);

                var Warna = ClassWarna.Cari(Nama);

                if (Warna != null)
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
                    {
                        Warna = new Warna_Model
                        {
                            IDWarna = Warna.IDWarna,
                            Nama = Warna.Nama
                        },
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }, Formatting.Indented));
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
                    {
                        Warna = null,
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoData,
                            Pesan = "Data tidak ditemukan"
                        }
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Warna_Cari");

            Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
            {
                Warna = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Warna_Tambah(string IDWMSStore, string Kode, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Warna_Class ClassWarna = new Warna_Class(db, PenggunaLogin);

                var Warna = ClassWarna.Tambah(Kode, Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
                {
                    Warna = new Warna_Model
                    {
                        IDWarna = Warna.IDWarna,
                        Nama = Warna.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassWarna.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Warna_Tambah");

            Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
            {
                Warna = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Warna_CariTambah(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Warna_Class ClassWarna = new Warna_Class(db, PenggunaLogin);

                var Warna = ClassWarna.CariTambah(Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
                {
                    Warna = new Warna_Model
                    {
                        IDWarna = Warna.IDWarna,
                        Nama = Warna.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassWarna.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Warna_CariTambah");

            Context.Response.Write(JsonConvert.SerializeObject(new Warna_WebService
            {
                Warna = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region ATRIBUT PRODUK
    [WebMethod]
    public void AtributProduk_Cari(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db, PenggunaLogin);

                var AtributProduk = ClassAtributProduk.Cari(Nama);

                if (AtributProduk != null)
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
                    {
                        AtributProduk = new AtributProduk_Model
                        {
                            IDAtributProduk = AtributProduk.IDAtributProduk,
                            Nama = AtributProduk.Nama
                        },
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }, Formatting.Indented));
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
                    {
                        AtributProduk = null,
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoData,
                            Pesan = "Data tidak ditemukan"
                        }
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "AtributProduk_Cari");

            Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
            {
                AtributProduk = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void AtributProduk_Tambah(string IDWMSStore, string AtributProdukGrup, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db, PenggunaLogin);

                var AtributProduk = ClassAtributProduk.Tambah(AtributProdukGrup, Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
                {
                    AtributProduk = new AtributProduk_Model
                    {
                        IDAtributProduk = AtributProduk.IDAtributProduk,
                        Nama = AtributProduk.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassAtributProduk.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "AtributProduk_Tambah");

            Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
            {
                AtributProduk = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void AtributProduk_CariTambah(string IDWMSStore, string AtributProdukGrup, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db, PenggunaLogin);

                var AtributProduk = ClassAtributProduk.CariTambah(AtributProdukGrup, Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
                {
                    AtributProduk = new AtributProduk_Model
                    {
                        IDAtributProduk = AtributProduk.IDAtributProduk,
                        Nama = AtributProduk.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassAtributProduk.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "AtributProduk_CariTambah");

            Context.Response.Write(JsonConvert.SerializeObject(new AtributProduk_WebService
            {
                AtributProduk = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region PRODUK KATEGORI
    [WebMethod]
    public void ProdukKategori_Cari(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db, PenggunaLogin);

                var ProdukKategori = ClassProdukKategori.Cari(Nama);

                if (ProdukKategori != null)
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new ProdukKategori_WebService
                    {
                        ProdukKategori = new ProdukKategori_Model
                        {
                            IDProdukKategori = ProdukKategori.IDProdukKategori,
                            Nama = ProdukKategori.Nama
                        },
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }, Formatting.Indented));
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new ProdukKategori_WebService
                    {
                        ProdukKategori = null,
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoData,
                            Pesan = "Data tidak ditemukan"
                        }
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "ProdukKategori_Cari");

            Context.Response.Write(JsonConvert.SerializeObject(new ProdukKategori_WebService
            {
                ProdukKategori = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void ProdukKategori_CariTambah(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db, PenggunaLogin);

                var ProdukKategori = ClassProdukKategori.CariTambah(Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new ProdukKategori_WebService
                {
                    ProdukKategori = new ProdukKategori_Model
                    {
                        IDProdukKategori = ProdukKategori.IDProdukKategori,
                        Nama = ProdukKategori.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassProdukKategori.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "ProdukKategori_CariTambah");

            Context.Response.Write(JsonConvert.SerializeObject(new ProdukKategori_WebService
            {
                ProdukKategori = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region PEMILIK PRODUK
    [WebMethod]
    public void PemilikProduk_Cari(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db, PenggunaLogin);

                var PemilikProduk = ClassPemilikProduk.Cari(Nama);

                if (PemilikProduk != null)
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new PemilikProduk_WebService
                    {
                        PemilikProduk = new PemilikProduk_Model
                        {
                            IDPemilikProduk = PemilikProduk.IDPemilikProduk,
                            Nama = PemilikProduk.Nama
                        },
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.Success,
                            Pesan = ""
                        }
                    }, Formatting.Indented));
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject(new PemilikProduk_WebService
                    {
                        PemilikProduk = null,
                        Result = new WebServiceResult
                        {
                            EnumWebService = (int)EnumWebService.NoData,
                            Pesan = "Data tidak ditemukan"
                        }
                    }, Formatting.Indented));
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "PemilikProduk_Cari");

            Context.Response.Write(JsonConvert.SerializeObject(new PemilikProduk_WebService
            {
                PemilikProduk = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void PemilikProduk_CariTambah(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db, PenggunaLogin);

                var PemilikProduk = ClassPemilikProduk.CariTambah(Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new PemilikProduk_WebService
                {
                    PemilikProduk = new PemilikProduk_Model
                    {
                        IDPemilikProduk = PemilikProduk.IDPemilikProduk,
                        Nama = PemilikProduk.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassPemilikProduk.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "PemilikProduk_CariTambah");

            Context.Response.Write(JsonConvert.SerializeObject(new PemilikProduk_WebService
            {
                PemilikProduk = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region KURIR
    [WebMethod]
    public void Kurir_CariTambah(string IDWMSStore, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Kurir_Class ClassKurir = new Kurir_Class(db, PenggunaLogin);

                var Kurir = ClassKurir.CariTambah(Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Kurir_WebService
                {
                    Kurir = new Kurir_Model
                    {
                        IDKurir = Kurir.IDKurir,
                        Nama = Kurir.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassKurir.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Kurir_CariTambah");

            Context.Response.Write(JsonConvert.SerializeObject(new Kurir_WebService
            {
                Kurir = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }

    [WebMethod]
    public void Kurir_CariByIDWMSTambah(string IDWMSStore, string IDWMS, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Kurir_Class ClassKurir = new Kurir_Class(db, PenggunaLogin);

                var Kurir = ClassKurir.CariTambah(Guid.Parse(IDWMS), Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Kurir_WebService
                {
                    Kurir = new Kurir_Model
                    {
                        IDKurir = Kurir.IDKurir,
                        Nama = Kurir.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassKurir.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Kurir_CariTambah");

            Context.Response.Write(JsonConvert.SerializeObject(new Kurir_WebService
            {
                Kurir = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region WILAYAH
    [WebMethod]
    public void Wilayah_TambahUbah(string IDWMSStore, string IDWMS, int enumWilayahGrup, string IDWMSWilayahParent, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Wilayah_Class ClassWilayah = new Wilayah_Class(db, PenggunaLogin);

                var Wilayah = ClassWilayah.TambahUbah(Guid.Parse(IDWMS), (EnumWilayahGrup)enumWilayahGrup, (!string.IsNullOrWhiteSpace(IDWMSWilayahParent) ? Guid.Parse(IDWMSWilayahParent) : (Guid?)null), Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Wilayah_WebService
                {
                    Wilayah = new Wilayah_Model
                    {
                        IDWilayah = Wilayah.IDWilayah,
                        NamaParent = Wilayah.IDWilayahParent.HasValue ? Wilayah.TBWilayah1.Nama : "",
                        Nama = Wilayah.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassWilayah.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Wilayah_TambahUbah");

            Context.Response.Write(JsonConvert.SerializeObject(new Wilayah_WebService
            {
                Wilayah = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region KURIR BIAYA
    [WebMethod]
    public void KurirBiaya_TambahUbah(string IDWMSStore, string IDWMS, string IDWMSKurir, string IDWMSWilayah, decimal Biaya)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                KurirBiaya_Class ClassKurirBiaya = new KurirBiaya_Class(db, PenggunaLogin);

                var KurirBiaya = ClassKurirBiaya.TambahUbah(Guid.Parse(IDWMS), Guid.Parse(IDWMSKurir), Guid.Parse(IDWMSWilayah), Biaya);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new KurirBiaya_WebService
                {
                    KurirBiaya = new KurirBiaya_Model
                    {
                        IDKurirBiaya = KurirBiaya.IDKurirBiaya,
                        Negara = KurirBiaya.TBWilayah.TBWilayah1.TBWilayah1.TBWilayah1.Nama,
                        Provinsi = KurirBiaya.TBWilayah.TBWilayah1.TBWilayah1.Nama,
                        Kota = KurirBiaya.TBWilayah.TBWilayah1.Nama,
                        Zona = KurirBiaya.TBWilayah.Nama,
                        Kurir = KurirBiaya.TBKurir.Nama,
                        Biaya = KurirBiaya.Biaya
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassKurirBiaya.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "KurirBiaya_TambahUbah");

            Context.Response.Write(JsonConvert.SerializeObject(new KurirBiaya_WebService
            {
                KurirBiaya = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region GRUP PELANGGAN
    [WebMethod]
    public void GrupPelanggan_TambahUbah(string IDWMSStore, string IDWMS, string Nama)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                GrupPelanggan_Class ClassGrupPelanggan = new GrupPelanggan_Class(db, PenggunaLogin);

                var GrupPelanggan = ClassGrupPelanggan.TambahUbah(Guid.Parse(IDWMS), Nama);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new GrupPelanggan_WebService
                {
                    GrupPelanggan = new GrupPelanggan_Model
                    {
                        IDGrupPelanggan = GrupPelanggan.IDGrupPelanggan,
                        Nama = GrupPelanggan.Nama
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassGrupPelanggan.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "GrupPelanggan_TambahUbah");

            Context.Response.Write(JsonConvert.SerializeObject(new GrupPelanggan_WebService
            {
                GrupPelanggan = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion

    #region PELANGGAN
    [WebMethod]
    public void Pelanggan_TambahUbah(string IDWMSStore, string IDWMS, string IDWMSGrupPelanggan, string NamaLengkap, string Username, string Password, string Email, DateTime TanggalLahir, string Handphone)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db, PenggunaLogin);

                var Pelanggan = ClassPelanggan.TambahUbah(Guid.Parse(IDWMS), Guid.Parse(IDWMSGrupPelanggan), NamaLengkap, Username, Password, Email, TanggalLahir, Handphone);

                db.SubmitChanges();

                Context.Response.Write(JsonConvert.SerializeObject(new Pelanggan_WebService
                {
                    Pelanggan = new Pelanggan_Model
                    {
                        IDPelanggan = Pelanggan.IDPelanggan,
                        Nama = Pelanggan.NamaLengkap
                    },
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ClassPelanggan.NotifikasiMessage
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "Pelanggan_TambahUbah");

            Context.Response.Write(JsonConvert.SerializeObject(new Pelanggan_WebService
            {
                Pelanggan = null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
    #endregion


    [WebMethod]
    public void SyncData_ProsesSync()
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin PenggunaLogin = new PenggunaLogin(1, 1);

                var Data = db.TBSyncDatas
                    .FirstOrDefault(item =>
                        !item.TanggalSync.HasValue &&
                        item.TanggalUploadFinish.HasValue &&
                        item.Data != null &&
                        item.Data != "");

                var ResultJson = JsonConvert.DeserializeObject<Mobile.RootObject>(Data.Data);

                Transaksi_Class Transaksi = new Transaksi_Class(1, 1, DateTime.Parse(ResultJson.Transaksi.tanggalTransaksi));

                foreach (var item in ResultJson.Transaksi.transaksiDetails)
                {
                    int IDDetailTransaksi = Transaksi.TambahDetailTransaksi(item.idKombinasiProduk, item.quantity);

                    if (item.discount != 0)
                        Transaksi.UbahPotonganHargaJualProduk(IDDetailTransaksi, item.discount.ToFormatHarga());
                }

                if (ResultJson.Transaksi.transaksiJenisPembayarans.Count > 0)
                {
                    foreach (var item in ResultJson.Transaksi.transaksiJenisPembayarans)
                    {
                        Transaksi.TambahPembayaran(DateTime.Parse(item.tanggal), 1, item.idJenisPembayaran, (decimal)item.bayar, "");
                    }
                }

                //Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;

                Transaksi.ConfirmTransaksi(db);

                Data.TanggalSync = DateTime.Now;

                db.SubmitChanges();

                var StringJson = JsonConvert.SerializeObject(Data.Data, Formatting.Indented);

                Context.Response.Write(JsonConvert.SerializeObject(new
                {
                    SyncData = StringJson,
                    Result = new WebServiceResult
                    {
                        EnumWebService = (int)EnumWebService.Success,
                        Pesan = ""
                    }
                }, Formatting.Indented));
            }
        }
        catch (Exception ex)
        {
            LogError_Class Error = new LogError_Class(ex, "SyncData_ProsesSync");

            Context.Response.Write(JsonConvert.SerializeObject(new
            {
                SyncData = (string)null,
                Result = new WebServiceResult
                {
                    EnumWebService = (int)EnumWebService.Exception,
                    Pesan = ex.Message.StartsWith("[WMS Error] ") ? ex.Message : "Terjadi kesalahan"
                }
            }, Formatting.Indented));
        }
    }
}