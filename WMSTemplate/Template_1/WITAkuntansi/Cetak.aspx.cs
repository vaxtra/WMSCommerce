using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_Cetak : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["do"] != "" && Request.QueryString["amount"] != "" && Request.QueryString["date"] != "")
            {
                if (Request.QueryString["do"] == "CashOut")
                    LabelJenisPrint.Text = "Telah dibayarkan oleh";
                else if (Request.QueryString["do"] == "CashIn")
                    LabelJenisPrint.Text = "Telah dibayarkan oleh";

                LoadLabelPrint();
            }
           
        }
    }
    protected void LoadLabelPrint()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        LabelNamaLengkapPrint.Text = Pengguna.NamaLengkap;
        LabelNominalPrint.Text = Request.QueryString["amount"].ToString();
        LabelTanggalCetakPrint.Text = DateTime.Now.ToString("d MMMM yyyy, HH:mm");
        LabelTanggalJurnalPrint.Text = Request.QueryString["date"];
        LabelKeteranganPrint.Text = Request.QueryString["description"] != null ? Request.QueryString["description"].ToString() : "";
        LabelReferensi.Text = Request.QueryString["Ref"] != null ? Request.QueryString["Ref"].ToString() : "";
    }
}