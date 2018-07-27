using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxControlToolkit;
using System.Net;
using Newtonsoft.Json;

public partial class WITAdministrator_Import_Produk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    //protected void ButtonUpload_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string SessionCode = DateTime.Now.ToString("ddMMyyHHmmss");

    //        #region Silahkan pilih file
    //        if (!FileUploadExcel.HasFile)
    //        {
    //            LiteralWarning.Text = ClassAlert.Pesan(TipeAlert.Danger, "Silahkan pilih file");
    //            return;
    //        }
    //        #endregion

    //        #region Format file harus .xls
    //        if (Path.GetExtension(FileUploadExcel.FileName) != ".xls")
    //        {
    //            LiteralWarning.Text = ClassAlert.Pesan(TipeAlert.Danger, "Format file harus .xls");
    //            return;
    //        }
    //        #endregion

    //        string Folder = Server.MapPath("/file_excel/Kurir/Import/");

    //        //MEMBUAT FOLDER JIKA FOLDER TIDAK DITEMUKAN
    //        if (!Directory.Exists(Folder))
    //            Directory.CreateDirectory(Folder);

    //        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

    //        string LokasiFile = Folder + Pengguna.Store + " Import Kurir " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

    //        FileUploadExcel.SaveAs(LokasiFile);

    //        #region Silahkan ulangi proses upload
    //        if (!File.Exists(LokasiFile))
    //        {
    //            LiteralWarning.Text = ClassAlert.Pesan(TipeAlert.Danger, "Silahkan ulangi proses upload");
    //            return;
    //        }
    //        #endregion

    //        ClassImportExcel ClassImportExcel = new ClassImportExcel(Pengguna, LokasiFile);
    //        ClassImportExcel.ImportKurir();

    //        if (!string.IsNullOrWhiteSpace(ClassImportExcel.Message))
    //            LiteralWarning.Text = ClassAlert.Pesan(TipeAlert.Danger, ClassImportExcel.Message);
    //        //else
    //        //    Response.Redirect("/WITReport/PerpindahanStok/ProdukDetail.aspx?keterangan=Import Excel " + SessionCode + "&returnUrl=/WITAdministrator/Produk/Default.aspx", false);
    //    }
    //    catch (Exception ex)
    //    {
    //        LiteralWarning.Text = ClassAlert.Pesan(TipeAlert.Danger, ex.Message);
    //        ClassLogError LogError = new ClassLogError(ex, Request.Url.PathAndQuery);

    //    }
    //}

    protected void ButtonSync_Click(object sender, EventArgs e)
    {
        //var client = new RestClient("https://api.rajaongkir.com/starter/province");
        //var request = new RestRequest(Method.GET);
        //request.AddHeader("key", "878877515f833cae89c2a0990bb40273");
        //IRestResponse response = client.Execute(request);

        string Key = "?key=878877515f833cae89c2a0990bb40273";

        string URLProvinsi = "https://api.rajaongkir.com/starter/province" + Key;
        string URLKota = "https://api.rajaongkir.com/starter/city" + Key;

        Provinsi(URLProvinsi);
    }

    private void Provinsi(string URLProvinsi)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string result = "";

        using (WebClient webClient = new WebClient())
        {
            result = webClient.DownloadString(URLProvinsi);
        }

        var ListData = JsonConvert.DeserializeObject<RajaOngkir_Provinsi.Rootobject>(result);

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            foreach (var item in ListData.rajaongkir.results)
            {
                var Data = db.TBKurirProvinsis.FirstOrDefault(item2 => item2.IDKurirProvinsi == item.province_id);

                if (Data == null)
                {
                    db.TBKurirProvinsis.InsertOnSubmit(new TBKurirProvinsi
                    {
                        IDKurirProvinsi = item.province_id,
                        Nama = item.province,

                        _IDPenggunaInsert = Pengguna.IDPengguna,
                        _IDPenggunaUpdate = Pengguna.IDPengguna,
                        _IDTempatInsert = Pengguna.IDTempat,
                        _IDTempatUpdate = Pengguna.IDTempat,
                        _IDWMSKurirProvinsi = Guid.NewGuid(),
                        _IDWMSStore = Pengguna.IDWMSStore,
                        _IsActive = true,
                        _TanggalInsert = DateTime.Now,
                        _TanggalUpdate = DateTime.Now,
                        _Urutan = 1
                    });
                }
                else
                {
                    //IDKurirProvinsi
                    Data.Nama = item.province;
                    //_IDPenggunaInsert
                    Data._IDPenggunaUpdate = Pengguna.IDPengguna;
                    //_IDTempatInsert
                    Data._IDTempatUpdate = Pengguna.IDTempat;
                    //_IDWMSKurirProvinsi
                    //_IDWMSStore
                    //_IsActive
                    //_TanggalInsert
                    Data._TanggalUpdate = DateTime.Now;
                    //_Urutan
                }
            }

            db.SubmitChanges();
        }
    }

    private void Kota(string URLKota)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        string result = "";

        using (WebClient webClient = new WebClient())
        {
            result = webClient.DownloadString(URLKota);
        }

        var ListData = JsonConvert.DeserializeObject<RajaOngkir_Kota.Rootobject>(result);

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            foreach (var item in ListData.rajaongkir.results)
            {
                var Data = db.TBKurirKotas.FirstOrDefault(item2 => item2.IDKurirKota == item.city_id);

                if (Data == null)
                {
                    db.TBKurirKotas.InsertOnSubmit(new TBKurirKota
                    {
                        IDKurirKota = item.city_id,
                        IDKurirProvinsi = item.province_id,
                        KodePos = item.postal_code,
                        Nama = item.city_name,
                        Tipe = item.type,
                        _IDPenggunaInsert = Pengguna.IDPengguna,
                        _IDPenggunaUpdate = Pengguna.IDPengguna,
                        _IDTempatInsert = Pengguna.IDTempat,
                        _IDTempatUpdate = Pengguna.IDTempat,
                        //////_IDWMSKurirProvinsi =,
                        //////_IDWMSStore =,
                        //////_IsActive =,
                        //////_TanggalInsert =,
                        //////_TanggalUpdate =,
                        _Urutan = 1
                    });
                }
                else
                {
                    //IDKurirProvinsi
                    Data.Nama = item.province;
                    //_IDPenggunaInsert
                    Data._IDPenggunaUpdate = Pengguna.IDPengguna;
                    //_IDTempatInsert
                    Data._IDTempatUpdate = Pengguna.IDTempat;
                    //_IDWMSKurirProvinsi
                    //_IDWMSStore
                    //_IsActive
                    //_TanggalInsert
                    Data._TanggalUpdate = DateTime.Now;
                    //_Urutan
                }
            }

            db.SubmitChanges();
        }
    }
}