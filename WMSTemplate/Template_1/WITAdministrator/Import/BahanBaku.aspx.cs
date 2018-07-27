using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class WITAdministrator_Import_BahanBaku : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        if (FileUploadBahanBaku.HasFile)
        {
            Server.ScriptTimeout = 1000000;
            PenggunaLogin penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            string lokasiFile = Server.MapPath("/file_excel/Bahan Baku/Import/") + "Import BahanBaku" + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

            FileUploadBahanBaku.SaveAs(lokasiFile);

            if (File.Exists(lokasiFile))
            {
                ImportExcel_Class _ImportExcel_Class = new ImportExcel_Class(penggunaLogin, lokasiFile);
                _ImportExcel_Class.ImportBahanBaku();

                //Terjadi Error
                if (_ImportExcel_Class.Message != null)
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, _ImportExcel_Class.Message);
                else
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Import Bahan Baku selesai");
            }
        }
    }
}