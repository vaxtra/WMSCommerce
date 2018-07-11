using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_MasterPageLaporanPrint : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["PenggunaLogin"] == null)
            Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=" + Request.RawUrl);
        else
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            Session.Timeout = 525000;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin _penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

            if (_penggunaLogin != null)
            {
                LabelNamaPengguna.Text = _penggunaLogin.NamaLengkap;
                LabelNamaStoreLokasi.Text = _penggunaLogin.Store + " - " + _penggunaLogin.Tempat;
                LabelTanggalPrint.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
            }
        }
    }

    protected void ButtonPrint_Click(object sender, EventArgs e)
    {
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //StringWriter stringWriter = new StringWriter();
        //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

        ////printLaporan.RenderControl(htmlTextWriter);

        //StringReader stringReader = new StringReader(stringWriter.ToString());

        //Document Doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f); ;

        //HTMLWorker htmlparser = new HTMLWorker(Doc);
        //PdfWriter.GetInstance(Doc, Response.OutputStream);

        //Doc.Open();
        //htmlparser.Parse(stringReader);
        //Doc.Close();
        //Response.Write(Doc);
        //Response.End();
    }
}
