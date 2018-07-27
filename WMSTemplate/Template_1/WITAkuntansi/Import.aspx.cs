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
        if (FileUploadExcel.HasFile)
        {
            Server.ScriptTimeout = 1000000;

            PenggunaLogin penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            string Folder = string.Empty;

            Folder = Server.MapPath("/file_excel/Akuntansi/Import/");

            string lokasiFile = Folder + "Import Akuntansi " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";


            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            FileUploadExcel.SaveAs(lokasiFile);

            if (File.Exists(lokasiFile))
            {
                ImportExcel_Class _ImportExcel_Class = new ImportExcel_Class(penggunaLogin, lokasiFile);
                var _result = _ImportExcel_Class.ImportPemasukanAkuntansi();

                //Terjadi Error
                if (_ImportExcel_Class.Message != null)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, _ImportExcel_Class.Message);
                else
                {
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Import Excel selesai");

                    RepeaterJurnal.DataSource = _result["DataImport"];
                    RepeaterJurnal.DataBind();
                }
            }
        }
    }
}