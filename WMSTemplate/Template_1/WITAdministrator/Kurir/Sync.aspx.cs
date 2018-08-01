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
using System.Collections.Specialized;
using System.Text;

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

        string Key = "?key=4fd02ed39a703a1d052da67aebdf8d2d";

        string URLProvinsi = "https://pro.rajaongkir.com/api/province" + Key;
        string URLKota = "https://pro.rajaongkir.com/api/city" + Key;

        //https://pro.rajaongkir.com/api/subdistrict?key=4fd02ed39a703a1d052da67aebdf8d2d&city=23

        Provinsi(URLProvinsi);
        Kota(URLKota);

        //using (WebClient webClient = new WebClient())
        //{
        //    var Values = new NameValueCollection();
        //    Values["key"] = "878877515f833cae89c2a0990bb40273";
        //    Values["origin"] = "79";
        //    Values["destination"] = "23";
        //    Values["weight"] = "1000";
        //    Values["courier"] = "jne";

        //    var Respose = webClient.UploadValues(new Uri("https://api.rajaongkir.com/starter/cost"), Values);

        //    string Result = Encoding.Default.GetString(Respose);

        //    //Response.Write(Result);

        //    LiteralResult.Text = Result;

        //    //var ResultJson = JsonConvert.DeserializeObject<Result_Product_Combination>(Result);

        //    //Console.WriteLine(ResultJson.Message + " : " + ResultJson.Message_Detail);
        //}
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
                        Nama = item.province.ToUpper(),

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
                    Data.Nama = item.province.ToUpper();
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
                        Nama = (item.type.Replace("Kabupaten", "Kab.") + " " + item.city_name).ToUpper(),
                        Tipe = item.type.ToUpper(),
                        _IDPenggunaInsert = Pengguna.IDPengguna,
                        _IDPenggunaUpdate = Pengguna.IDPengguna,
                        _IDTempatInsert = Pengguna.IDTempat,
                        _IDTempatUpdate = Pengguna.IDTempat,
                        _IDWMSKurirKota = Guid.NewGuid(),
                        _IDWMSStore = Pengguna.IDWMSStore,
                        _IsActive = true,
                        _TanggalInsert = DateTime.Now,
                        _TanggalUpdate = DateTime.Now,
                        _Urutan = 1
                    });
                }
                else
                {
                    //IDKurirKota
                    Data.IDKurirProvinsi = item.province_id;
                    Data.KodePos = item.postal_code;
                    Data.Nama = (item.type.Replace("Kabupaten", "Kab.") + " " + item.city_name).ToUpper();
                    Data.Tipe = item.type.ToUpper();
                    //_IDPenggunaInsert
                    Data._IDPenggunaUpdate = Pengguna.IDPengguna;
                    //_IDTempatInsert
                    Data._IDTempatUpdate = Pengguna.IDTempat;
                    //_IDWMSKurirKota
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