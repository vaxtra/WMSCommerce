using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITSurvey_Import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void ButtonImport_Click(object sender, EventArgs e)
    {
        if (!FileUploadExcel.HasFile)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "File tidak ditemukan");
            return;
        }

        if (Path.GetExtension(FileUploadExcel.FileName) != ".xls")
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Format file harus .xls");
            return;
        }

        PenggunaLogin penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

        string lokasiFolder = Server.MapPath("/file_excel/Survei/Import/");

        if (!Directory.Exists(lokasiFolder))
            Directory.CreateDirectory(lokasiFolder);

        string lokasiFile = lokasiFolder + "Import Survei " + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

        FileUploadExcel.SaveAs(lokasiFile);

        if (File.Exists(lokasiFile))
        {
            ImportExcel_Class ImportExcel_Class = new ImportExcel_Class(penggunaLogin, lokasiFile);
            int Result = ImportExcel_Class.ImportSurvei();

            //Terjadi Error
            if (ImportExcel_Class.Message != null || Result == 0)
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ImportExcel_Class.Message);
            else
                Response.Redirect("Pengaturan.aspx?id=" + Result);
        }
    }
}