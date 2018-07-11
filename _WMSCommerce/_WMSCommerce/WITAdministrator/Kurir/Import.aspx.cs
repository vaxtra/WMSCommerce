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
            if (!FileUploadExcel.HasFile)
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan pilih file");
                return;
            }
            #endregion

            #region Format file harus .xls
            if (Path.GetExtension(FileUploadExcel.FileName) != ".xls")
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Format file harus .xls");
                return;
            }
            #endregion

            string Folder = Server.MapPath("/file_excel/Kurir/Import/");

            //MEMBUAT FOLDER JIKA FOLDER TIDAK DITEMUKAN
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            string LokasiFile = Folder + Pengguna.Store + " Import Kurir " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

            FileUploadExcel.SaveAs(LokasiFile);

            #region Silahkan ulangi proses upload
            if (!File.Exists(LokasiFile))
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan ulangi proses upload");
                return;
            }
            #endregion

            ImportExcel_Class ImportExcel_Class = new ImportExcel_Class(Pengguna, LokasiFile);
            ImportExcel_Class.ImportKurir();

            if (!string.IsNullOrWhiteSpace(ImportExcel_Class.Message))
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ImportExcel_Class.Message);
            //else
            //    Response.Redirect("/WITReport/PerpindahanStok/ProdukDetail.aspx?keterangan=Import Excel " + SessionCode + "&returnUrl=/WITAdministrator/Produk/Default.aspx", false);
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);

        }
    }
}