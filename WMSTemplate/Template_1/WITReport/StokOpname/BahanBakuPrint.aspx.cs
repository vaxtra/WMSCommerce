using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_StokOpname_BahanBakuPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region FIND CONTROL
            Label LabelJudul = (Label)Page.Master.FindControl("LabelJudul");
            Label LabelSubJudul = (Label)Page.Master.FindControl("LabelSubJudul");
            Label LabelStoreTempat = (Label)Page.Master.FindControl("LabelStoreTempat");

            Label LabelPrintTanggal = (Label)Page.Master.FindControl("LabelPrintTanggal");
            Label LabelPrintPengguna = (Label)Page.Master.FindControl("LabelPrintPengguna");
            Label LabelPrintStoreTempat = (Label)Page.Master.FindControl("LabelPrintStoreTempat");

            Label LabelPeriode = (Label)Page.Master.FindControl("LabelPeriode");

            HtmlGenericControl PanelPengirimHeader = (HtmlGenericControl)Page.Master.FindControl("PanelPengirimHeader");
            HtmlGenericControl PanelPengirimFooter = (HtmlGenericControl)Page.Master.FindControl("PanelPengirimFooter");

            Label LabelPengirimTempat = (Label)Page.Master.FindControl("LabelPengirimTempat");
            Label LabelPengirimPengguna = (Label)Page.Master.FindControl("LabelPengirimPengguna");
            Label LabelPengirimPengguna1 = (Label)Page.Master.FindControl("LabelPengirimPengguna1");
            Label LabelPengirimTanggal = (Label)Page.Master.FindControl("LabelPengirimTanggal");
            Label LabelPengirimAlamat = (Label)Page.Master.FindControl("LabelPengirimAlamat");
            Label LabelPengirimTelepon = (Label)Page.Master.FindControl("LabelPengirimTelepon");
            Label LabelPengirimEmail = (Label)Page.Master.FindControl("LabelPengirimEmail");

            HtmlGenericControl PanelKeterangan = (HtmlGenericControl)Page.Master.FindControl("PanelKeterangan");
            Label LabelPengirimKeterangan = (Label)Page.Master.FindControl("LabelPengirimKeterangan");

            HtmlGenericControl PanelPenerimaHeader = (HtmlGenericControl)Page.Master.FindControl("PanelPenerimaHeader");
            HtmlGenericControl PanelPenerimaFooter = (HtmlGenericControl)Page.Master.FindControl("PanelPenerimaFooter");

            Label LabelPenerimaTempat = (Label)Page.Master.FindControl("LabelPenerimaTempat");
            Label LabelPenerimaPengguna = (Label)Page.Master.FindControl("LabelPenerimaPengguna");
            Label LabelPenerimaPengguna1 = (Label)Page.Master.FindControl("LabelPenerimaPengguna1");
            Label LabelPenerimaTanggal = (Label)Page.Master.FindControl("LabelPenerimaTanggal");
            Label LabelPenerimaAlamat = (Label)Page.Master.FindControl("LabelPenerimaAlamat");
            Label LabelPenerimaTelepon = (Label)Page.Master.FindControl("LabelPenerimaTelepon");
            #endregion

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            LabelJudul.Text = "Laporan Stok Opname Bahan Baku";
            //LabelSubJudul.Text = LaporanTop_Class.OrderByKeterangan + "<br/>Jenis Transaksi : " + LaporanTop_Class.JenisTransaksiKeterangan;
            LabelStoreTempat.Text = Pengguna.Tempat;

            LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();
            LabelPrintPengguna.Text = Pengguna.NamaLengkap;
            LabelPrintStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

            LabelPeriode.Text = "Periode " + DateTime.Parse(Request.QueryString["Awal"].ToString()).ToShortDateString();

            PanelPengirimHeader.Visible = false;
            PanelPengirimFooter.Visible = false;

            //LabelPengirimTempat.Text
            //LabelPengirimPengguna.Text
            //LabelPengirimPengguna1.Text = LabelPengirimPengguna.Text;
            //LabelPengirimTanggal.Text
            //LabelPengirimAlamat.Text
            //LabelPengirimTelepon.Text
            //LabelPengirimEmail.Text

            //PanelKeterangan.Visible
            //LabelPengirimKeterangan.Text

            PanelPenerimaHeader.Visible = false;
            PanelPenerimaFooter.Visible = false;

            //LabelPenerimaTempat.Text 
            //LabelPenerimaPengguna.Text
            //LabelPenerimaPengguna1.Text = LabelPenerimaPengguna.Text;
            //LabelPenerimaTanggal.Text
            //LabelPenerimaAlamat.Text
            //LabelPenerimaTelepon.Text

            if (Request.QueryString["Awal"] != null && Request.QueryString["Akhir"] != null && Request.QueryString["IDTempat"] != null)
            {
                LoadDatabase(Request.QueryString["Awal"].ToString(),
                    Request.QueryString["Akhir"].ToString(),
                    Request.QueryString["IDTempat"].ToString());
            }
        }
    }

    private dynamic LoadDatabase(string tanggalAwal, string tanggalAkhir, string idTempat)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            //QUERY DATA
            var DataStok = db.TBStokBahanBakus.Where(item => item.IDTempat == idTempat.ToInt()).ToArray();
            var DataPerpindahanStokBahanBakuIncludeSO = db.TBPerpindahanStokBahanBakus
                .Where(item =>
                    item.Tanggal >= DateTime.Parse(tanggalAwal)&&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            var DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item =>
            item.Tanggal > DateTime.Parse(tanggalAwal) &&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

        

            if (idTempat != "0")
            {
                DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuExcludeSO.Where(item => item.IDTempat == idTempat.ToInt()).ToArray();
                DataPerpindahanStokBahanBakuIncludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item => item.IDTempat == idTempat.ToInt()).ToArray();
            }

            //BAHAN BAKU
            if (Request.QueryString["BahanBaku"] != null)
            {
                DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuExcludeSO.Where(item => item.TBStokBahanBaku.TBBahanBaku.Nama.ToLower().Contains(Request.QueryString["BahanBaku"].ToString().ToLower())).ToArray();
                DataPerpindahanStokBahanBakuIncludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item => item.TBStokBahanBaku.TBBahanBaku.Nama.ToLower().Contains(Request.QueryString["BahanBaku"].ToString().ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBBahanBaku.Nama.ToLower().Contains(Request.QueryString["BahanBaku"].ToString().ToLower())).ToArray();
            }
            //KATEGORI
            if (Request.QueryString["Kategori"] != null)
            {
                DataPerpindahanStokBahanBakuExcludeSO = DataPerpindahanStokBahanBakuExcludeSO.Where(item =>
                item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.TBKategoriBahanBaku.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToLower())) != null).ToArray();

                DataPerpindahanStokBahanBakuIncludeSO = DataPerpindahanStokBahanBakuIncludeSO.Where(item =>
               item.TBStokBahanBaku.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.TBKategoriBahanBaku.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToLower())) != null).ToArray();

                DataStok = DataStok.Where(item => item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault(data => data.TBKategoriBahanBaku.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToLower())) != null).ToArray();
            }
            List<Stok_Model> DataClassStokOpname = new List<Stok_Model>();
            int i = 0;
            for (int index = 0; index < DataStok.Count(); index++)
            {
                //ADA STOK OPNAME TIDAK BAHAN BAKU INI ?
                var LogPerpindahanStokOpname = DataPerpindahanStokBahanBakuIncludeSO.Where(item2 =>
                item2.TBStokBahanBaku.IDBahanBaku == DataStok[index].IDBahanBaku &&
                (item2.IDJenisPerpindahanStok == 11 || item2.IDJenisPerpindahanStok == 12)).OrderByDescending(item2 => item2.IDPerpindahanStokBahanBaku);
                //if (DataStok[index].TBBahanBaku.KodeBahanBaku == "BA-B01")
                //{
                if (LogPerpindahanStokOpname.Count() > 0)
                {
                    Stok_Model newDataClassStokOpname = new Stok_Model();

                    #region Data Bahan Baku
                    DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
                    #endregion

                    bool statStokOpname = false;
                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == newDataClassStokOpname.IDBahanBaku).Jumlah;

                    //LOG PERPINDAHAN STOK DETAIL (SELURUH STATUS)
                    var LogPerpindahanStokDetail = DataPerpindahanStokBahanBakuIncludeSO.Where(item2 => item2.TBStokBahanBaku.IDBahanBaku == DataStok[index].IDBahanBaku).OrderByDescending(item2 => item2.IDPerpindahanStokBahanBaku);

                    foreach (var item in LogPerpindahanStokDetail)
                    {
                        //STOK OPNAME BERKURANG
                        if (item.IDJenisPerpindahanStok == 11)
                        {
                            //SUDAH PERNAH STOK OPNAME BLM ?
                            if (statStokOpname == false)
                            {
                                newDataClassStokOpname.StokSebelumSO = saldoStok + item.Jumlah;
                                newDataClassStokOpname.StokBerkurangSO = item.Jumlah;
                                newDataClassStokOpname.StokKeluar = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                                statStokOpname = true;

                                DataClassStokOpname.Add(newDataClassStokOpname);
                            }
                            else
                            {
                                i += 1;
                                newDataClassStokOpname = new Stok_Model();

                                #region Data Bahan Baku
                                DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
                                #endregion

                                newDataClassStokOpname.StokSebelumSO = saldoStok + item.Jumlah;
                                newDataClassStokOpname.StokBerkurangSO = item.Jumlah;
                                newDataClassStokOpname.StokKeluar = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                                DataClassStokOpname.Add(newDataClassStokOpname);
                            }


                            saldoStok += item.Jumlah;
                        }
                        //STOK OPNAME BERTAMBAH
                        else if (item.IDJenisPerpindahanStok == 12)
                        {
                            if (statStokOpname == false)
                            {
                                newDataClassStokOpname.StokSebelumSO = saldoStok - item.Jumlah; //50
                                newDataClassStokOpname.StokBertambahSO = item.Jumlah;
                                newDataClassStokOpname.StokMasuk = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                                statStokOpname = true;

                                DataClassStokOpname.Add(newDataClassStokOpname);
                            }
                            else
                            {
                                i += 1;
                                newDataClassStokOpname = new Stok_Model();

                                #region Data Bahan Baku
                                DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
                                #endregion

                                newDataClassStokOpname.StokSebelumSO = saldoStok - item.Jumlah;
                                newDataClassStokOpname.StokBertambahSO = item.Jumlah;
                                newDataClassStokOpname.StokMasuk = item.Jumlah;
                                newDataClassStokOpname.StokSetelahSO = saldoStok;

                            }
                            saldoStok -= item.Jumlah;
                        }
                        //SELAIN STOK OPNAME
                        else
                        {
                            if (item.TBJenisPerpindahanStok.Status == false)
                            {
                                saldoStok += item.Jumlah;
                            }
                            else
                            {
                                saldoStok -= item.Jumlah;
                            }
                        }
                    }
                }
                else
                {
                    Stok_Model newDataClassStokOpname = new Stok_Model();

                    #region Data Bahan Baku
                    DataBahanBaku(db, DataStok, index, newDataClassStokOpname);
                    #endregion

                    newDataClassStokOpname.StokMasuk = 0;
                    newDataClassStokOpname.StokKeluar = 0;

                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == newDataClassStokOpname.IDBahanBaku).Jumlah;
                    var LogPerpindahanStokDetail = DataPerpindahanStokBahanBakuIncludeSO.Where(item2 => item2.TBStokBahanBaku.IDBahanBaku == DataStok[index].IDBahanBaku).OrderByDescending(item2 => item2.IDPerpindahanStokBahanBaku);

                    foreach (var item in LogPerpindahanStokDetail)
                    {
                        if (item.TBJenisPerpindahanStok.Status == false)
                        {
                            newDataClassStokOpname.StokKeluar = +item.Jumlah;
                            //saldoStok += item.Jumlah;
                        }
                        else
                        {
                            newDataClassStokOpname.StokMasuk = +item.Jumlah;
                            //saldoStok -= item.Jumlah;
                        }
                    }
                    newDataClassStokOpname.StokSetelahSO = Math.Abs(newDataClassStokOpname.StokMasuk - newDataClassStokOpname.StokKeluar);
                    newDataClassStokOpname.StokSebelumSO = Math.Abs(newDataClassStokOpname.StokMasuk - newDataClassStokOpname.StokKeluar);
                    i += 1;

                    DataClassStokOpname.Add(newDataClassStokOpname);
                }
                //}
            }

            var ListPerpindahanStokBahanBaku = DataClassStokOpname
                .Select(item => new
                {
                    Index = item.IndexClass,
                    Kode = item.KodeBahanBaku,
                    BahanBaku = item.NamaBahanBaku,
                    Satuan = item.SatuanKecil,
                    Kategori = item.Kategori,

                    StokSebelumSO = item.StokSebelumSO,
                    StokSetelahSO = item.StokSetelahSO,
                    NominalSebelumSO = item.StokSebelumSO / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli,
                    NominalSetelahSO = item.StokSetelahSO / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli,
                    SelisihQtyPositif = item.StokSetelahSO > item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,
                    SelisihQtyNegatif = item.StokSetelahSO < item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,

                    SelisihNominalPositif = item.StokSetelahSO > item.StokSebelumSO ? ((item.StokSetelahSO - item.StokSebelumSO) / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi) * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli
                : 0,

                    SelisihNominalNegatif = item.StokSetelahSO < item.StokSebelumSO ? ((item.StokSetelahSO - item.StokSebelumSO) / DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).TBBahanBaku.Konversi) * DataStok.FirstOrDefault(item2 => item2.IDBahanBaku == item.IDBahanBaku).HargaBeli
                : 0,
                }).ToArray().OrderBy(item => item.BahanBaku).ThenByDescending(item => item.Index);

            if (ListPerpindahanStokBahanBaku.Count() > 0)
            {
                RepeaterLaporan.DataSource = ListPerpindahanStokBahanBaku;

                #region HEADER
                LabelGtandTotalSelisihQty.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyPositif) + ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelGtandTotalSelisihNominal.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalPositif) + ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                LabelTotalJumlahQtyPositif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyPositif).ToFormatHarga();
                LabelTotalJumlahQtyNegatif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelTotalJumlahNominalPositif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                LabelNominalSebelumSO.Text = ListPerpindahanStokBahanBaku.Sum(item => item.NominalSebelumSO).ToFormatHarga();
                LabelNominalSetelahSO.Text = ListPerpindahanStokBahanBaku.Sum(item => item.NominalSetelahSO).ToFormatHarga();
                #endregion

                #region FOOTER
                LabelNominalSebelumSO2.Text = LabelNominalSebelumSO.Text;
                LabelNominalSetelahSO2.Text = LabelNominalSetelahSO.Text;
                LabelTotalJumlahQtyPositif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyPositif).ToFormatHarga();
                LabelTotalJumlahQtyNegatif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelTotalJumlahNominalPositif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif2.Text = ListPerpindahanStokBahanBaku.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                #endregion

            }
            else
            {
                RepeaterLaporan.DataSource = null;
            }

            RepeaterLaporan.DataBind();


            return ListPerpindahanStokBahanBaku;
        }
    }

    private static void DataBahanBaku(DataClassesDatabaseDataContext db, TBStokBahanBaku[] DataStok, int i, Stok_Model newDataClassStokOpname)
    {
        newDataClassStokOpname.IndexClass = i;
        newDataClassStokOpname.IDBahanBaku = (int)DataStok[i].IDBahanBaku;
        newDataClassStokOpname.KodeBahanBaku = DataStok[i].TBBahanBaku.KodeBahanBaku;
        newDataClassStokOpname.NamaBahanBaku = DataStok[i].TBBahanBaku.Nama;
        newDataClassStokOpname.SatuanKecil = DataStok[i].TBBahanBaku.TBSatuan.Nama;
        //////ERROR BANU
        //////newDataClassStokOpname.Kategori = StokBahanBaku.GabungkanSemuaKategoriBahanBaku(db, null, DataStok[i].TBBahanBaku);
    }
}