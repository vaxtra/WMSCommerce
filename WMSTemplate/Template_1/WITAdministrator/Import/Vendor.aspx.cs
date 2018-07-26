using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Import_Vendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        if (FileUploadVendor.HasFile)
        {
            Server.ScriptTimeout = 1000000;
            PenggunaLogin penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            string lokasiFile = Server.MapPath("/file_excel/Vendor/Import/") + "Import Vendor" + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

            FileUploadVendor.SaveAs(lokasiFile);

            if (File.Exists(lokasiFile))
            {
                ImportExcel_Class _ImportExcel_Class = new ImportExcel_Class(penggunaLogin, lokasiFile);
                _ImportExcel_Class.ImportVendor();

                //Terjadi Error
                if (_ImportExcel_Class.Message != null)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, _ImportExcel_Class.Message);
                else
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Import Vendor selesai");
            }
        }
    }
}