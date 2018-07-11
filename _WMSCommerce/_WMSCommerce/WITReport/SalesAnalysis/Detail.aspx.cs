using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Niion_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LabelPeriode.Text = "DETAIL SALES REPORT " + DateTime.Now.Year.ToString();
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                KategoriTempat_Class KategoriTempat_Class = new KategoriTempat_Class();

                DropDownListKategoriTempat.DataSource = KategoriTempat_Class.Data(db);
                DropDownListKategoriTempat.DataValueField = "IDKategoriTempat";
                DropDownListKategoriTempat.DataTextField = "Nama";
                DropDownListKategoriTempat.DataBind();
                DropDownListKategoriTempat.SelectedValue = Pengguna.IDTempat.ToString();
            }

            int index = 0;
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                DropDownListTahun.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                DropDownListTahunBulanan.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
                index++;
            }
            DropDownListTahun.SelectedIndex = 5;
        }
    }
    protected void ButtonJanuari_Click(object sender, EventArgs e)
    {
        LoadData("1", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonFebruari_Click(object sender, EventArgs e)
    {
        LoadData("2", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonMaret_Click(object sender, EventArgs e)
    {
        LoadData("3", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonApril_Click(object sender, EventArgs e)
    {
        LoadData("4", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonMei_Click(object sender, EventArgs e)
    {
        LoadData("5", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonJuni_Click(object sender, EventArgs e)
    {
        LoadData("6", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonJuli_Click(object sender, EventArgs e)
    {
        LoadData("7", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonAgustus_Click(object sender, EventArgs e)
    {
        LoadData("8", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonSeptember_Click(object sender, EventArgs e)
    {
        LoadData("9", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonOktober_Click(object sender, EventArgs e)
    {
        LoadData("10", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonNopember_Click(object sender, EventArgs e)
    {
        LoadData("11", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void ButtonDesember_Click(object sender, EventArgs e)
    {
        LoadData("12", DropDownListTahunBulanan.SelectedItem.Text);
    }
    protected void LoadData(string bulan, string tahun)
    {
        switch (DropDownListKategoriTempat.SelectedItem.Text)
        {
            case "Consignment":
                Response.Redirect("ReportSales.aspx?id=5&month=" + bulan + "&year=" + tahun);
                break;
            case "Event":
                Response.Redirect("ReportSalesEvent.aspx?id=4&month=" + bulan + "&year=" + tahun);
                break;
            case "Reseller":
                Response.Redirect("ReportSales.aspx?id=5&month=" + bulan + "&year=" + tahun);
                break;
            case "Store":
                Response.Redirect("ReportSales.aspx?id=3&month=" + bulan + "&year=" + tahun);
                break;
            case "ECommerce":
                Response.Redirect("ReportSalesWebstore.aspx?id=2&month=" + bulan + "&year=" + tahun);
                break;
        }
    }
    protected void ButtonPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportSalesTahunan.aspx?year=" + DropDownListTahun.SelectedItem.Text);
    }
}