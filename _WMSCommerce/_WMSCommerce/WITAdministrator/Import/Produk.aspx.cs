using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxControlToolkit;

public partial class WITAdministrator_Import_Produk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string SessionCode = DateTime.Now.ToString("ddMMyyHHmmss");

            #region Silahkan pilih file
            if (!FileUploadProduk.HasFile)
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan pilih file");
                return;
            }
            #endregion

            #region Format file harus .xls
            if (Path.GetExtension(FileUploadProduk.FileName) != ".xls")
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Format file harus .xls");
                return;
            }
            #endregion

            string Folder = Server.MapPath("/file_excel/Produk/Import/");

            //MEMBUAT FOLDER JIKA FOLDER TIDAK DITEMUKAN
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            string LokasiFile = Folder + Pengguna.Store + " Import Produk " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

            FileUploadProduk.SaveAs(LokasiFile);

            #region Silahkan ulangi proses upload
            if (!File.Exists(LokasiFile))
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan ulangi proses upload");
                return;
            }
            #endregion

            ImportExcel_Class ImportExcel_Class = new ImportExcel_Class(Pengguna, LokasiFile);
            ImportExcel_Class.ImportProduk(RadioButtonListJenisImport.SelectedValue, SessionCode);

            if (!string.IsNullOrWhiteSpace(ImportExcel_Class.Message))
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ImportExcel_Class.Message);
            else
                Response.Redirect("/WITReport/PerpindahanStok/ProdukDetail.aspx?keterangan=Import Excel " + SessionCode + "&returnUrl=/WITAdministrator/Produk/Default.aspx", false);
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);

        }
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITAdministrator/Produk/Default.aspx");
    }

    protected void ButtonUploadChecker_Click(object sender, EventArgs e)
    {
        try
        {
            string SessionCode = DateTime.Now.ToString("ddMMyyHHmmss");

            #region Silahkan pilih file
            if (!FileUploadChecker.HasFile)
            {
                LiteralChecker.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan pilih file");
                return;
            }
            #endregion

            #region Format file harus .xls
            if (Path.GetExtension(FileUploadChecker.FileName) != ".xls")
            {
                LiteralChecker.Text = Alert_Class.Pesan(TipeAlert.Danger, "Format file harus .xls");
                return;
            }
            #endregion

            string Folder = Server.MapPath("/file_excel/Produk/Import/");

            //MEMBUAT FOLDER JIKA FOLDER TIDAK DITEMUKAN
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            string LokasiFile = Folder + Pengguna.Store + " Checker Produk " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

            FileUploadChecker.SaveAs(LokasiFile);

            #region Silahkan ulangi proses upload
            if (!File.Exists(LokasiFile))
            {
                LiteralChecker.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan ulangi proses upload");
                return;
            }
            #endregion

            ImportExcel_Class ImportExcel_Class = new ImportExcel_Class(Pengguna, LokasiFile);
            List<string> DataDuplicate = ImportExcel_Class.ImportProdukChecker(SessionCode);

            if (DataDuplicate.Count > 0)
            {
                peringatan.Visible = true;
                LiteralChecker.Text = "<b>BERIKUT DATA DUPLIKAT</b> <br /><br />";

                foreach (var item in DataDuplicate)
                {
                    LiteralChecker.Text += item;
                    LiteralChecker.Text += "<br />";
                }
            }
            else
            {
                peringatan.Visible = true;
                LiteralChecker.Text = "TIDAK ADA DATA DUPLIKAT";
            }
        }
        catch (Exception ex)
        {
            LiteralChecker.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);

        }
    }
}