using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_ExportAccurate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxTanggalAwal.Text = Pengaturan.HariIni()[0].ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = Pengaturan.HariIni()[1].ToString("d MMMM yyyy");
        }
    }
    private void ExportLaporan(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TextBoxTanggalAwal.Text = tanggalAwal.ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = tanggalAkhir.ToString("d MMMM yyyy");

        tanggalAwal = tanggalAwal.Date;
        tanggalAkhir = tanggalAkhir.Date;

        string path = Server.MapPath("/files/Accurate/");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fileName = Guid.NewGuid().ToString();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //TRANSAKSI COMPLETE
            var TransaksiComplete = db.TBTransaksis
                .Where(item =>
                    item.TBTempat.IDTempat == Pengguna.IDTempat &&
                    item.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                    item.TanggalOperasional.Value >= tanggalAwal &&
                    item.TanggalOperasional.Value <= tanggalAkhir)
                    .ToArray();

            //DETAIL TRANSAKSI
            var DataProduk = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.IDTempat == Pengguna.IDTempat &&
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                    item.TBTransaksi.TanggalOperasional.Value >= tanggalAwal &&
                    item.TBTransaksi.TanggalOperasional.Value <= tanggalAkhir)
                .GroupBy(item => new
                {
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    item.TBKombinasiProduk.Nama,
                    item.HargaJual,
                    Persentase = (item.Discount / item.HargaJual) * 100
                })
                .Select(item => new
                {
                    Key = item.Key,
                    Quantity = item.Sum(item2 => item2.Quantity)
                })
                .OrderBy(item => item.Key.Nama)
                .ToArray();

            var Discount = TransaksiComplete.Sum(item => item.PotonganTransaksi + item.TotalDiscountVoucher);
            var GrandTotal = TransaksiComplete.Sum(item => item.GrandTotal);

            string BranchCode = "1433135629";
            string DepartmentID = "51.1";
            string WarehouseID = "Samarinda";
            string WarehouseID2 = "Pabrik ";
            string Description = "Percobaan Import WMS Rendy";

            string CustomerID = "0601";
            string CustomerName = "Outlet Samarinda";

            string InvoiceDate = DateTime.Now.ToString("yyyy-MM-dd");

            string isiFile = string.Empty;

            isiFile += "<?xml version=\"1.0\"?>";
            isiFile += "<NMEXML EximID=\"586\" BranchCode=\"" + BranchCode + "\" ACCOUNTANTCOPYID=\"\">";
            isiFile += "<TRANSACTIONS OnError=\"CONTINUE\">";

            isiFile += "<SALESINVOICE operation=\"Add\" REQUESTID=\"1\">";
            isiFile += "<TRANSACTIONID>9254</TRANSACTIONID>";

            int i = 1;

            foreach (var item in DataProduk)
            {
                isiFile += "<ITEMLINE operation=\"Add\">";
                isiFile += "<KeyID>" + i + "</KeyID>";
                isiFile += "<ITEMNO>" + item.Key.KodeKombinasiProduk + "</ITEMNO>";
                isiFile += "<QUANTITY>" + item.Quantity + "</QUANTITY>";
                isiFile += "<ITEMUNIT>Dus</ITEMUNIT>";
                isiFile += "<UNITRATIO>1</UNITRATIO>";
                isiFile += "<ITEMRESERVED1/>";
                isiFile += "<ITEMRESERVED2/>";
                isiFile += "<ITEMRESERVED3/>";
                isiFile += "<ITEMRESERVED4/>";
                isiFile += "<ITEMRESERVED5/>";
                isiFile += "<ITEMRESERVED6/>";
                isiFile += "<ITEMRESERVED7/>";
                isiFile += "<ITEMRESERVED8/>";
                isiFile += "<ITEMRESERVED9/>";
                isiFile += "<ITEMRESERVED10/>";
                isiFile += "<ITEMOVDESC>" + item.Key.Nama + "</ITEMOVDESC>";
                isiFile += "<UNITPRICE>" + item.Key.HargaJual + "</UNITPRICE>";

                if (item.Key.Persentase > 0)
                    isiFile += "<ITEMDISCPC>" + item.Key.Persentase + "</ITEMDISCPC>";
                else
                    isiFile += "<ITEMDISCPC/>";

                isiFile += "<TAXCODES/>";
                isiFile += "<DEPTID>" + DepartmentID + "</DEPTID>";
                isiFile += "<SOSEQ/>";
                isiFile += "<BRUTOUNITPRICE>" + item.Key.HargaJual + "</BRUTOUNITPRICE>";
                isiFile += "<WAREHOUSEID>" + WarehouseID + "</WAREHOUSEID>";
                isiFile += "<QTYCONTROL>0</QTYCONTROL>";
                isiFile += "<DOSEQ/>";
                isiFile += "<DOID/>";
                isiFile += "</ITEMLINE>";

                i++;
            }

            isiFile += "<INVOICENO>" + fileName + "</INVOICENO>";
            isiFile += "<INVOICEDATE>" + InvoiceDate + "</INVOICEDATE>";
            isiFile += "<TAX1CODE/>";
            isiFile += "<TAX2CODE/>";
            isiFile += "<TAX1RATE>0</TAX1RATE>";
            isiFile += "<TAX2RATE>0</TAX2RATE>";
            isiFile += "<RATE>1</RATE>";
            isiFile += "<INCLUSIVETAX>0</INCLUSIVETAX>";
            isiFile += "<CUSTOMERISTAXABLE>0</CUSTOMERISTAXABLE>";
            isiFile += "<CASHDISCOUNT>" + Discount + "</CASHDISCOUNT>";
            isiFile += "<CASHDISCPC/>";
            isiFile += "<INVOICEAMOUNT>" + GrandTotal + "</INVOICEAMOUNT>";
            isiFile += "<FREIGHT>0</FREIGHT>";
            isiFile += "<TERMSID>C.O.D</TERMSID>";
            isiFile += "<FOB/>";
            isiFile += "<PURCHASEORDERNO/>";
            isiFile += "<WAREHOUSEID>" + WarehouseID2 + "</WAREHOUSEID>";
            isiFile += "<DESCRIPTION>" + Description + "</DESCRIPTION>";
            isiFile += "<SHIPDATE>" + InvoiceDate + "</SHIPDATE>";
            isiFile += "<DELIVERYORDER></DELIVERYORDER>";
            isiFile += "<FISCALRATE>1</FISCALRATE>";
            isiFile += "<TAXDATE>" + InvoiceDate + "</TAXDATE>";
            isiFile += "<CUSTOMERID>" + CustomerID + "</CUSTOMERID>";
            isiFile += "<PRINTED>0</PRINTED>";
            isiFile += "<SHIPTO1>" + CustomerName + "</SHIPTO1>";
            isiFile += "<SHIPTO2/>";
            isiFile += "<SHIPTO3/>";
            isiFile += "<SHIPTO4/>";
            isiFile += "<SHIPTO5/>";
            isiFile += "<ARACCOUNT>10.01-00.2100</ARACCOUNT>";
            isiFile += "<TAXFORMNUMBER/>";
            isiFile += "<TAXFORMCODE/>";
            isiFile += "<CURRENCYNAME>Rupiah</CURRENCYNAME>";
            isiFile += "</SALESINVOICE>";
            isiFile += "</TRANSACTIONS>";
            isiFile += "</NMEXML>";

            File.WriteAllText(path + fileName + ".xml", isiFile);

            LiteralResult.Text = "<a href='" + "/files/Accurate/" + fileName + ".xml" + "' target='_blank'>Download</a>";
        }
    }
    protected void ButtonHariIni_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMingguIni_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulanIni_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahunIni_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonKemarin_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguLalu_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanLalu_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunLalu_Click(object sender, EventArgs e)
    {
        ExportLaporan(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        ExportLaporan(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));
    }
}