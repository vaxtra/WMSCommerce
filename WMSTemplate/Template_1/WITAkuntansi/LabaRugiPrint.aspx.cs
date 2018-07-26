using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_LabaRugiPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Bulan"] != null && Request.QueryString["Tahun"] != null)
            {
                LoadData();
            }
            else if (Request.QueryString["Periode1"] != null && Request.QueryString["Periode2"] != null)
            {
                LoadData2(Request.QueryString["Periode1"], Request.QueryString["Periode2"]);
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region DEFAULT
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            TBStore _store = db.TBStores.FirstOrDefault();

            var Bulan = Request.QueryString["Bulan"];
            var Tahun = Request.QueryString["Tahun"];

            LabelPeriode.Text = "Bulan " + Bulan + "/" + Tahun;
            LabelNamaPencetak.Text = Pengguna.NamaLengkap;
            LabelTanggalCetak.Text = DateTime.Now.ToString("d MMMM yyyy");

            LabelNamaStore.Text = _store.Nama;
            LabelAlamatStore.Text = _store.Alamat;
            LabelTeleponStore.Text = _store.TeleponLain + " / " + _store.Handphone;
            LabelWebsite.Text = _store.Website;
            #endregion

            var _result = Akuntansi_Class.LaporanLabaRugi(Bulan, Tahun, false, Pengguna, "LabaRugi");

            RepeaterPemasukan.DataSource = _result["Pemasukan"];
            RepeaterPemasukan.DataBind();

            RepeaterPengeluaran.DataSource = _result["Pengeluaran"];
            RepeaterPengeluaran.DataBind();

            #region MOD TEST
            LabelPenjualan.Text = _result["NamaAkunPenjualan"];
            LabelNominalPenjualan.Text = Parse.ToFormatHarga(_result["NominalAkunPenjualan"]);
            LabelCOGS.Text = _result["NamaAkunCOGS"];
            LabelNominalCOGS.Text = Parse.ToFormatHarga(_result["NominalCOGS"]);
            LabelNominalGrossProfit.Text = Parse.ToFormatHarga(_result["NominalGrossProfit"]);
            LabelTotalOPEX.Text = Parse.ToFormatHarga(_result["NominalOPEX"]);
            LabelNominalEBIT.Text = Parse.ToFormatHarga(_result["NominalEBIT"]);
            #endregion

            RepeaterPemasukan.DataSource = _result["Pemasukan"];
            RepeaterPemasukan.DataBind();

            RepeaterPengeluaran.DataSource = _result["Pengeluaran"];
            RepeaterPengeluaran.DataBind();

            RepeaterPengeluaranTax.DataSource = _result["PengeluaranTax"];
            RepeaterPengeluaranTax.DataBind();

            var NetIncome = _result["NominalNetIncome"];

            if (NetIncome >= 0)
            {
                PanelProfit.Visible = true;
                PanelLoss.Visible = false;
                LabelNetIncomeProfit.Text = Pengaturan.FormatHarga(NetIncome);
            }
            else
            {
                PanelProfit.Visible = false;
                PanelLoss.Visible = true;
                LabelNetIncomeLoss.Text = Pengaturan.FormatHarga(NetIncome);
            }
        }
    }
    private void LoadData2(string _tgl1, string _tgl2)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            #region DEFAULT
            TBStore _store = db.TBStores.FirstOrDefault();

            var Bulan = Request.QueryString["Bulan"];
            var Tahun = Request.QueryString["Tahun"];

            LabelPeriode.Text = "Bulan " + Bulan + "/" + Tahun;
            LabelNamaPencetak.Text = Pengguna.NamaLengkap;
            LabelTanggalCetak.Text = DateTime.Now.ToString("d MMMM yyyy");

            LabelNamaStore.Text = _store.Nama;
            LabelAlamatStore.Text = _store.Alamat;
            LabelTeleponStore.Text = _store.TeleponLain + " / " + _store.Handphone;
            LabelWebsite.Text = _store.Website;
            #endregion

            var _result = Akuntansi_Class.LaporanLabaRugi(false, Pengguna, "LabaRugi", _tgl1,_tgl2);

            RepeaterPemasukan.DataSource = _result["Pemasukan"];
            RepeaterPemasukan.DataBind();

            RepeaterPengeluaran.DataSource = _result["Pengeluaran"];
            RepeaterPengeluaran.DataBind();

            #region MOD TEST
            LabelPenjualan.Text = _result["NamaAkunPenjualan"];
            LabelNominalPenjualan.Text = Parse.ToFormatHarga(_result["NominalAkunPenjualan"]);
            LabelCOGS.Text = _result["NamaAkunCOGS"];
            LabelNominalCOGS.Text = Parse.ToFormatHarga(_result["NominalCOGS"]);
            LabelNominalGrossProfit.Text = Parse.ToFormatHarga(_result["NominalGrossProfit"]);
            LabelTotalOPEX.Text = Parse.ToFormatHarga(_result["NominalOPEX"]);
            LabelNominalEBIT.Text = Parse.ToFormatHarga(_result["NominalEBIT"]);
            #endregion

            RepeaterPemasukan.DataSource = _result["Pemasukan"];
            RepeaterPemasukan.DataBind();

            RepeaterPengeluaran.DataSource = _result["Pengeluaran"];
            RepeaterPengeluaran.DataBind();

            RepeaterPengeluaranTax.DataSource = _result["PengeluaranTax"];
            RepeaterPengeluaranTax.DataBind();

            var NetIncome = _result["NominalNetIncome"];

            if (NetIncome >= 0)
            {
                PanelProfit.Visible = true;
                PanelLoss.Visible = false;
                LabelNetIncomeProfit.Text = NetIncome.ToFormatHarga();
            }
            else
            {
                PanelProfit.Visible = false;
                PanelLoss.Visible = true;
                LabelNetIncomeLoss.Text = NetIncome.ToFormatHarga();
            }
        }
        
    }


}