using BarcodeLib;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plugins_barcode_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string kode = Request.QueryString["kode"];

            int tinggi;
            int.TryParse(Request.QueryString["tinggi"], out tinggi);

            if (tinggi <= 0)
                tinggi = 150;

            int lebar;
            int.TryParse(Request.QueryString["lebar"], out lebar);

            if (lebar <= 0)
                lebar = 300;

            if (!string.IsNullOrWhiteSpace(kode))
            {
                System.Drawing.Image barcodeImage = null;
                Barcode b = new Barcode();

                TYPE type = TYPE.CODE128;

                b.IncludeLabel = false;
                b.Alignment = BarcodeLib.AlignmentPositions.CENTER;

                barcodeImage = b.Encode(type, kode, lebar, tinggi);

                Response.ContentType = "image/" + "png";
                System.IO.MemoryStream MemStream = new System.IO.MemoryStream();
                barcodeImage.Save(MemStream, ImageFormat.Png);
                MemStream.WriteTo(Response.OutputStream);
                barcodeImage.Dispose();
            }
        }
    }
}