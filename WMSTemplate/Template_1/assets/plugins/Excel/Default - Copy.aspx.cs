using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plugins_Excel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            //PenggunaLogin Pengguna = new PenggunaLogin(1, 1);

            //#region EXCEL
            //Excel_Class Excel_Class = new Excel_Class(Pengguna, "Data Pelanggan", DateTime.Now.ToFormatTanggal() + " - " + DateTime.Now.ToFormatTanggal(), 7);
            //ExcelWorksheet Worksheet = Excel_Class.Worksheet;

            //Excel_Class.Header(1, "No.");
            //Excel_Class.Header(2, "Grup");
            //Excel_Class.Header(3, "Nama");
            //Excel_Class.Header(4, "Email");
            //Excel_Class.Header(5, "Phone");
            //Excel_Class.Header(6, "Deposit");
            //Excel_Class.Header(7, "Status");

            //int index = Excel_Class.IndexHeader;

            //using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            //{
            //    int i = 0;

            //    foreach (var item in db.TBPelanggans.Where(item => item.IDPelanggan != 1).ToArray())
            //    {
            //        index++;

            //        Excel_Class.Content(index, 1, i + 1);
            //        Excel_Class.Content(index, 2, item.TBGrupPelanggan.Nama);
            //        Excel_Class.Content(index, 3, item.NamaLengkap);
            //        Excel_Class.Content(index, 4, item.Email);
            //        Excel_Class.Content(index, 5, item.Handphone);
            //        Excel_Class.Content(index, 6, item.Deposit);
            //        Excel_Class.Content(index, 7, item._IsActive.ToString());
            //    }
            //}

            //for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
            //{
            //    index++;

            //    var Data = dataSetResult.Tables[0].Rows[i];

            //    Excel_Class.Content(index, 1, i + 1);
            //    Excel_Class.Content(index, 2, Data["TransactionType"].ToString());
            //    Excel_Class.Content(index, 3, Data["TransactionGroup"].ToString());
            //    Excel_Class.Content(index, 4, Data["TransactionSubGroup"].ToString());
            //    Excel_Class.Content(index, 5, Data["Report"].ToString());
            //    Excel_Class.Content(index, 6, Data["ReportDetail"].ToString());
            //    Excel_Class.Content(index, 7, Data["CustomerId"].ToString());
            //    Excel_Class.Content(index, 8, Data["TRACCT"].ToString());
            //    Excel_Class.Content(index, 9, Data["MainGrupCIF1"].ToString());
            //    Excel_Class.Content(index, 10, Data["AccountName"].ToString());
            //    Excel_Class.Content(index, 11, Data["Counterpart"].ToString());
            //    Excel_Class.Content(index, 12, Data["CounterpartDetail"].ToString());
            //    Excel_Class.Content(index, 13, Data["MainGrupCIF"].ToString());
            //    Excel_Class.Content(index, 14, Data["Bank"].ToString());
            //    Excel_Class.Content(index, 15, Data["BankDetail"].ToString());
            //    Excel_Class.Content(index, 16, Data["AMT_IN_IDR"].ToDecimal());
            //    Excel_Class.Content(index, 17, Data["TRREMK"].ToString());
            //    Excel_Class.Content(index, 18, Data["TRDATE"].ToDateTime());
            //    Excel_Class.Content(index, 19, Data["YearMonth"].ToString());
            //}

            //Excel_Class.Save();

            //LinkDownload.HRef = Excel_Class.LinkDownload;
            //LinkDownload.Visible = true;
            //#endregion
        }
    }

    //private void LoadExcel()
    //{


    //    Excel_Class Excel_Class = new Excel_Class(UserLogin, "Related Parties Month Year", TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text, (dataSetResult.Tables[0].Rows.Count + 3));
    //    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

    //    Index = Excel_Class.IndexHeader;

    //    int Kolom = 1;

    //    #region HEADER
    //    Index++;

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //    {
    //        Excel_Class.ContentHeader(Index, Kolom, dataSetResult.Tables[0].Rows[i]["KOLOM"].ToString());
    //        Kolom++;
    //    }

    //    Excel_Class.ContentHeader(Index, Kolom, "Grand Total");
    //    Kolom++;

    //    Excel_Class.ContentHeader(Index, Kolom, "%");
    //    Kolom++;
    //    #endregion

    //    #region SUM OF IDR
    //    Index++;
    //    Kolom = 1;

    //    Excel_Class.ContentHeader(Index, Kolom, "SUM OF IDR");
    //    Kolom++;

    //    for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //    {
    //        Excel_Class.ContentHeader(Index, Kolom, "");
    //        Kolom++;
    //    }

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;
    //    #endregion

    //    decimal TotalIncomingOutgoing = 0;
    //    decimal TotalIncomingOutgoingCount = 0;

    //    Index++;

    //    IncomingOutgoingExcel(Excel_Class, dataSetResult.Tables[1], out TotalIncomingOutgoing);

    //    HeaderDataExcel(Excel_Class, dataSetResult.Tables[2], TotalIncomingOutgoing);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[3], TotalIncomingOutgoing);

    //    HeaderDataExcel(Excel_Class, dataSetResult.Tables[4], TotalIncomingOutgoing);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[5], TotalIncomingOutgoing);

    //    #region COUNT OF TRANSACTION
    //    Kolom = 1;

    //    Excel_Class.ContentHeader(Index, Kolom, "COUNT OF TRANSACTION");
    //    Kolom++;

    //    for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //    {
    //        Excel_Class.ContentHeader(Index, Kolom, "");
    //        Kolom++;
    //    }

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    Index++;
    //    #endregion

    //    IncomingOutgoingExcel(Excel_Class, dataSetResult.Tables[6], out TotalIncomingOutgoingCount);

    //    HeaderDataExcel(Excel_Class, dataSetResult.Tables[7], TotalIncomingOutgoingCount);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[8], TotalIncomingOutgoingCount);

    //    HeaderDataExcel(Excel_Class, dataSetResult.Tables[9], TotalIncomingOutgoingCount);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[10], TotalIncomingOutgoingCount);

    //    Excel_Class.Save(false);

    //    LinkDownload.HRef = Excel_Class.LinkDownload;
    //    LinkDownload.Visible = true;
    //}


    //private void IncomingOutgoingExcel(Controller_Excel Excel_Class, DataTable ListData, out decimal GrandTotalResult)
    //{
    //    GrandTotalResult = 0;

    //    for (int i = 0; i < ListData.Rows.Count; i++)
    //    {
    //        var Data = ListData.Rows[i];

    //        decimal GrandTotal = 0;

    //        for (int j = 2; j < ListData.Columns.Count; j++)
    //        {
    //            if (j == 2)
    //                Excel_Class.ContentHeader(Index, (j - 1), Data[j].ToString());
    //            else
    //                Excel_Class.ContentHeader(Index, (j - 1), Data[j].ToDecimal());

    //            //MENGHITUNG GRAND TOTAL
    //            if (j != 2)
    //                GrandTotal += Data[j].ToDecimal();
    //        }

    //        //MENAMPILKAN GRAND TOTAL
    //        Excel_Class.ContentHeader(Index, (ListData.Columns.Count - 1), GrandTotal);
    //        Excel_Class.ContentHeader(Index, (ListData.Columns.Count), 100);

    //        GrandTotalResult = GrandTotal;

    //        Index++;
    //    }
    //}

    //private void HeaderDataExcel(Controller_Excel Excel_Class, DataTable ListData, decimal TotalSeluruhnya)
    //{
    //    for (int i = 0; i < ListData.Rows.Count; i++)
    //    {
    //        var Data = ListData.Rows[i];

    //        decimal GrandTotal = 0;

    //        for (int j = 2; j < ListData.Columns.Count; j++)
    //        {
    //            if (j == 2)
    //                Excel_Class.ContentHeader(Index, (j - 1), "      " + Data[j].ToString());
    //            else
    //                Excel_Class.ContentHeader(Index, (j - 1), Data[j].ToDecimal());

    //            //MENGHITUNG GRAND TOTAL
    //            if (j != 2)
    //                GrandTotal += Data[j].ToDecimal();
    //        }

    //        //MENAMPILKAN GRAND TOTAL
    //        Excel_Class.ContentHeader(Index, (ListData.Columns.Count - 1), GrandTotal);
    //        Excel_Class.ContentHeader(Index, (ListData.Columns.Count), (GrandTotal / TotalSeluruhnya * 100));

    //        Index++;
    //    }
    //}

    //private void DetailDataExcel(Controller_Excel Excel_Class, DataTable ListData, decimal TotalSeluruhnya)
    //{
    //    for (int i = 0; i < ListData.Rows.Count; i++)
    //    {
    //        var Data = ListData.Rows[i];

    //        decimal GrandTotal = 0;

    //        for (int j = 2; j < ListData.Columns.Count; j++)
    //        {
    //            if (j == 2)
    //                Excel_Class.Content(Index, (j - 1), "            " + Data[j].ToString());
    //            else
    //                Excel_Class.Content(Index, (j - 1), Data[j].ToDecimal());

    //            //MENGHITUNG GRAND TOTAL
    //            if (j != 2)
    //                GrandTotal += Data[j].ToDecimal();
    //        }

    //        //MENAMPILKAN GRAND TOTAL
    //        Excel_Class.Content(Index, (ListData.Columns.Count - 1), GrandTotal);
    //        Excel_Class.Content(Index, (ListData.Columns.Count), (GrandTotal / TotalSeluruhnya * 100));

    //        Index++;
    //    }
    //}


    //private void LoadExcel()
    //{
    //    DataSet dataSetResult = new DataSet();
    //    var UserLogin = (User_Model)Session["User_Model"];
    //    var ClsUser = (ClsUser)Session["ClsUser"];

    //    Database_ONeAnalytics Database = new Database_ONeAnalytics(ClsUser);

    //    Database.ALYReportIOInHouse(TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime(), DropDownListMainGrupCIF.SelectedValue.ToInt(), out dataSetResult);

    //    Controller_Excel Excel_Class = new Controller_Excel(UserLogin, "Inhouse", TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text, (dataSetResult.Tables[0].Rows.Count + 3));
    //    ExcelWorksheet Worksheet = Excel_Class.Worksheet;

    //    Index = Excel_Class.IndexHeader;

    //    int Kolom = 1;

    //    #region HEADER
    //    Index++;

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //    {
    //        Excel_Class.ContentHeader(Index, Kolom, dataSetResult.Tables[0].Rows[i]["KOLOM"].ToString());
    //        Kolom++;
    //    }

    //    Excel_Class.ContentHeader(Index, Kolom, "Grand Total");
    //    Kolom++;

    //    Excel_Class.ContentHeader(Index, Kolom, "%");
    //    Kolom++;
    //    #endregion

    //    #region SUM OF IDR
    //    Index++;
    //    Kolom = 1;

    //    Excel_Class.ContentHeader(Index, Kolom, "SUM OF IDR");
    //    Kolom++;

    //    for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //    {
    //        Excel_Class.ContentHeader(Index, Kolom, "");
    //        Kolom++;
    //    }

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;
    //    #endregion

    //    decimal TotalIncoming = 0;
    //    decimal TotalOutgoing = 0;

    //    decimal TotalIncomingCount = 0;
    //    decimal TotalOutgoingCount = 0;

    //    Index++;

    //    //INCOMING
    //    IncomingOutgoingExcel(Excel_Class, dataSetResult.Tables[1], out TotalIncoming);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[2], TotalIncoming);

    //    //OUTGOING
    //    IncomingOutgoingExcel(Excel_Class, dataSetResult.Tables[3], out TotalOutgoing);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[4], TotalOutgoing);

    //    #region COUNT OF TRANSACTION
    //    Kolom = 1;

    //    Excel_Class.ContentHeader(Index, Kolom, "COUNT OF TRANSACTION");
    //    Kolom++;

    //    for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //    {
    //        Excel_Class.ContentHeader(Index, Kolom, "");
    //        Kolom++;
    //    }

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    Excel_Class.ContentHeader(Index, Kolom, "");
    //    Kolom++;

    //    Index++;
    //    #endregion

    //    //INCOMING
    //    IncomingOutgoingExcel(Excel_Class, dataSetResult.Tables[5], out TotalIncomingCount);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[6], TotalIncomingCount);

    //    //OUTGOING
    //    IncomingOutgoingExcel(Excel_Class, dataSetResult.Tables[7], out TotalOutgoingCount);
    //    DetailDataExcel(Excel_Class, dataSetResult.Tables[8], TotalOutgoingCount);

    //    Excel_Class.Save(false);

    //    LinkDownload.HRef = Excel_Class.LinkDownload;
    //    LinkDownload.Visible = true;
    //}

    //private void IncomingOutgoingExcel(Controller_Excel Excel_Class, DataTable ListData, out decimal GrandTotalResult)
    //{
    //    GrandTotalResult = 0;

    //    for (int i = 0; i < ListData.Rows.Count; i++)
    //    {
    //        var Data = ListData.Rows[i];

    //        decimal GrandTotal = 0;

    //        for (int j = 1; j < ListData.Columns.Count; j++)
    //        {
    //            if (j == 1)
    //                Excel_Class.ContentHeader(Index, j, Data[j].ToString());
    //            else
    //                Excel_Class.ContentHeader(Index, j, Data[j].ToDecimal());

    //            //MENGHITUNG GRAND TOTAL
    //            if (j != 1)
    //                GrandTotal += Data[j].ToDecimal();
    //        }

    //        //MENAMPILKAN GRAND TOTAL
    //        Excel_Class.ContentHeader(Index, (ListData.Columns.Count), GrandTotal);
    //        Excel_Class.ContentHeader(Index, (ListData.Columns.Count + 1), 100);

    //        GrandTotalResult = GrandTotal;

    //        Index++;
    //    }
    //}

    //private void HeaderDataExcel(Controller_Excel Excel_Class, DataTable ListData, decimal TotalSeluruhnya)
    //{
    //    for (int i = 0; i < ListData.Rows.Count; i++)
    //    {
    //        var Data = ListData.Rows[i];

    //        decimal GrandTotal = 0;

    //        for (int j = 1; j < ListData.Columns.Count; j++)
    //        {
    //            if (j == 1)
    //                Excel_Class.Content(Index, j, "      " + Data[j].ToString());
    //            else
    //                Excel_Class.Content(Index, j, Data[j].ToDecimal());

    //            //MENGHITUNG GRAND TOTAL
    //            if (j != 1)
    //                GrandTotal += Data[j].ToDecimal();
    //        }

    //        //MENAMPILKAN GRAND TOTAL
    //        Excel_Class.Content(Index, (ListData.Columns.Count), GrandTotal);
    //        Excel_Class.Content(Index, (ListData.Columns.Count + 1), (GrandTotal / TotalSeluruhnya * 100));

    //        Index++;
    //    }
    //}

    //private void DetailDataExcel(Controller_Excel Excel_Class, DataTable ListData, decimal TotalSeluruhnya)
    //{
    //    for (int i = 0; i < ListData.Rows.Count; i++)
    //    {
    //        var Data = ListData.Rows[i];

    //        decimal GrandTotal = 0;

    //        for (int j = 1; j < ListData.Columns.Count; j++)
    //        {
    //            if (j == 1)
    //                Excel_Class.Content(Index, j, "            " + Data[j].ToString());
    //            else
    //                Excel_Class.Content(Index, j, Data[j].ToDecimal());

    //            //MENGHITUNG GRAND TOTAL
    //            if (j != 1)
    //                GrandTotal += Data[j].ToDecimal();
    //        }

    //        //MENAMPILKAN GRAND TOTAL
    //        Excel_Class.Content(Index, (ListData.Columns.Count), GrandTotal);
    //        Excel_Class.Content(Index, (ListData.Columns.Count + 1), (GrandTotal / TotalSeluruhnya * 100));

    //        Index++;
    //    }
    //}

    //private void LoadData(bool isExcel)
    //{
    //    DataSet dataSetResult = new DataSet();
    //    var UserLogin = (User_Model)Session["User_Model"];
    //    var ClsUser = (ClsUser)Session["ClsUser"];

    //    Database_TrendTransaction TrendTransaction = new Database_TrendTransaction(ClsUser, UserLogin);

    //    TrendTransaction.ALYTrendTransactionReport(DropDownListMainGrupCIF.SelectedValue.ToInt(), TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime(), out dataSetResult);

    //    if (isExcel)
    //    {
    //        ListViewData.DataSource = null;
    //        ListViewData.DataBind();

    //        #region EXCEL
    //        Controller_Excel Excel_Class = new Controller_Excel(UserLogin, "Trend Transaction", TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text, 19);
    //        ExcelWorksheet Worksheet = Excel_Class.Worksheet;

    //        Excel_Class.Header(1, "No.");
    //        Excel_Class.Header(2, "IN / OUT");
    //        Excel_Class.Header(3, "Type");
    //        Excel_Class.Header(4, "Sub Type");
    //        Excel_Class.Header(5, "Report");
    //        Excel_Class.Header(6, "Report Detail");
    //        Excel_Class.Header(7, "Customer");
    //        Excel_Class.Header(8, "Account ID");
    //        Excel_Class.Header(9, "Account Group");
    //        Excel_Class.Header(10, "Account Name");
    //        Excel_Class.Header(11, "Counterpart Group");
    //        Excel_Class.Header(12, "Counterpart Name");
    //        Excel_Class.Header(13, "Main Group");
    //        Excel_Class.Header(14, "Bank Group");
    //        Excel_Class.Header(15, "Bank Name");
    //        Excel_Class.Header(16, "Amount");
    //        Excel_Class.Header(17, "Description");
    //        Excel_Class.Header(18, "Date");
    //        Excel_Class.Header(19, "Year Month");

    //        int index = Excel_Class.IndexHeader;

    //        for (int i = 0; i < dataSetResult.Tables[0].Rows.Count; i++)
    //        {
    //            index++;

    //            var Data = dataSetResult.Tables[0].Rows[i];

    //            Excel_Class.Content(index, 1, i + 1);
    //            Excel_Class.Content(index, 2, Data["TransactionType"].ToString());
    //            Excel_Class.Content(index, 3, Data["TransactionGroup"].ToString());
    //            Excel_Class.Content(index, 4, Data["TransactionSubGroup"].ToString());
    //            Excel_Class.Content(index, 5, Data["Report"].ToString());
    //            Excel_Class.Content(index, 6, Data["ReportDetail"].ToString());
    //            Excel_Class.Content(index, 7, Data["CustomerId"].ToString());
    //            Excel_Class.Content(index, 8, Data["TRACCT"].ToString());
    //            Excel_Class.Content(index, 9, Data["MainGrupCIF1"].ToString());
    //            Excel_Class.Content(index, 10, Data["AccountName"].ToString());
    //            Excel_Class.Content(index, 11, Data["Counterpart"].ToString());
    //            Excel_Class.Content(index, 12, Data["CounterpartDetail"].ToString());
    //            Excel_Class.Content(index, 13, Data["MainGrupCIF"].ToString());
    //            Excel_Class.Content(index, 14, Data["Bank"].ToString());
    //            Excel_Class.Content(index, 15, Data["BankDetail"].ToString());
    //            Excel_Class.Content(index, 16, Data["AMT_IN_IDR"].ToDecimal());
    //            Excel_Class.Content(index, 17, Data["TRREMK"].ToString());
    //            Excel_Class.Content(index, 18, Data["TRDATE"].ToDateTime());
    //            Excel_Class.Content(index, 19, Data["YearMonth"].ToString());
    //        }

    //        Excel_Class.Save();

    //        LinkDownload.HRef = Excel_Class.LinkDownload;
    //        LinkDownload.Visible = true;
    //        #endregion
    //    }
    //    else
    //    {
    //        ListViewData.DataSource = dataSetResult.Tables[0];
    //        ListViewData.DataBind();
    //    }
    //}
}