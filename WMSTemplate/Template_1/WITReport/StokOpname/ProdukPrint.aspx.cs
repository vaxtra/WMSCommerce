using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_StokOpname_ProdukPrint : System.Web.UI.Page
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

            LabelJudul.Text = "Laporan Stok Opname Produk";
            //LabelSubJudul.Text = LaporanTop_Class.OrderByKeterangan + "<br/>Jenis Transaksi : " + LaporanTop_Class.JenisTransaksiKeterangan;
            LabelStoreTempat.Text = Pengguna.Tempat;

            LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();
            LabelPrintPengguna.Text = Pengguna.NamaLengkap;
            LabelPrintStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

            LabelPeriode.Text = "Periode "+DateTime.Parse(Request.QueryString["Awal"].ToString()).ToShortDateString();

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
    private dynamic LoadDatabase( string tanggalAwal, string tanggalAkhir, string idTempat)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //QUERY DATA
            var DataStok = db.TBStokProduks.ToArray();
            var DataPerpindahanStokProdukIncludeSO = db.TBPerpindahanStokProduks
                .Where(item =>
                    item.Tanggal >= DateTime.Parse(tanggalAwal)&&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            var DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukIncludeSO.Where(item =>
            item.Tanggal > DateTime.Parse(tanggalAwal) &&
                    item.Tanggal <= DateTime.Now)
                .ToArray();

            #region QueryPencarian
            //TEMPAT
            if (idTempat != "0")
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item => item.IDTempat == idTempat.ToInt()).ToArray();
                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item => item.IDTempat == idTempat.ToInt()).ToArray();
            }

            //PRODUK
            if (Request.QueryString["Produk"] != null )
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item => item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(Request.QueryString["Produk"].ToString().ToLower())).ToArray();
                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item => item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(Request.QueryString["Produk"].ToString().ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(Request.QueryString["Produk"].ToString().ToLower())).ToArray();
            }
            //KATEGORI
            if (Request.QueryString["Kategori"] != null)
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToString().ToLower())).ToArray();

                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToString().ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToString().ToLower())).ToArray();
            }
            //BRAND
            if (Request.QueryString["Brand"] != null)
            {
                DataPerpindahanStokProdukExcludeSO = DataPerpindahanStokProdukExcludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToString().ToLower())).ToArray();

                DataPerpindahanStokProdukIncludeSO = DataPerpindahanStokProdukIncludeSO.Where(item =>
                item.TBStokProduk.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToString().ToLower())).ToArray();

                DataStok = DataStok.Where(item => item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama.ToLower().Contains(Request.QueryString["Kategori"].ToString().ToLower())).ToArray();
            }
            #endregion

            List<Stok_Model> DataClassStokOpname = new List<Stok_Model>();

            int i = 0;
            for (int index = 0; index < DataStok.Count(); index++)
            {
                //ADA STOK OPNAME TIDAK PRODUK INI ?
                var LogPerpindahanStokOpname = DataPerpindahanStokProdukIncludeSO.Where(item2 =>
                item2.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DataStok[index].IDKombinasiProduk &&
                (item2.IDJenisPerpindahanStok == 11 || item2.IDJenisPerpindahanStok == 12)).OrderByDescending(item2 => item2.IDPerpindahanStokProduk);

                if (LogPerpindahanStokOpname.Count() > 0)
                {
                    Stok_Model newDataClassStokOpname = new Stok_Model();

                    #region Data Produk
                    DataProduk(DataStok, i, index, newDataClassStokOpname);
                    #endregion

                    bool statStokOpname = false;
                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == newDataClassStokOpname.IDKombinasiProduk).Jumlah;

                    //LOG PERPINDAHAN STOK DETAIL (SELURUH STATUS)
                    var LogPerpindahanStokDetail = DataPerpindahanStokProdukIncludeSO.Where(item2 => item2.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DataStok[index].IDKombinasiProduk).OrderByDescending(item2 => item2.IDPerpindahanStokProduk);

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

                                #region Data Produk
                                DataProduk(DataStok, i, index, newDataClassStokOpname);
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

                                #region Data Produk
                                DataProduk(DataStok, i, index, newDataClassStokOpname);
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

                    #region Data Produk
                    DataProduk(DataStok, i, index, newDataClassStokOpname);
                    #endregion

                    newDataClassStokOpname.StokMasuk = 0;
                    newDataClassStokOpname.StokKeluar = 0;

                    decimal saldoStok = (int)DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == newDataClassStokOpname.IDKombinasiProduk).Jumlah;
                    var LogPerpindahanStokDetail = DataPerpindahanStokProdukIncludeSO.Where(item2 => item2.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DataStok[index].IDKombinasiProduk).OrderByDescending(item2 => item2.IDPerpindahanStokProduk);

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
            }

            var ListPerpindahanStokProduk = DataClassStokOpname.Select(item =>
            new
            {
                Index = item.IndexClass,
                Kode = item.KodeKombinasiProduk,
                NamaProduk = item.NamaProduk,
                Varian = item.NamaKombinasiProduk,
                Kategori = item.Kategori,
                Warna = item.Warna,
                PemilikProduk = item.Brand,
                StokSebelumSO = item.StokSebelumSO,
                StokSetelahSO = item.StokSetelahSO,

                SelisihQtyPositif = item.StokSetelahSO > item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,
                SelisihQtyNegatif = item.StokSetelahSO < item.StokSebelumSO ? item.StokSetelahSO - item.StokSebelumSO : 0,

                SelisihNominalPositif = item.StokSetelahSO > item.StokSebelumSO ? (item.StokSetelahSO - item.StokSebelumSO) * DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).HargaJual
                : 0,

                SelisihNominalNegatif = item.StokSetelahSO < item.StokSebelumSO ? (item.StokSetelahSO - item.StokSebelumSO) * DataStok.FirstOrDefault(item2 => item2.IDKombinasiProduk == item.IDKombinasiProduk).HargaJual
                : 0,
            }).ToArray().OrderBy(item => item.NamaProduk).ThenByDescending(item => item.Index);

            if (ListPerpindahanStokProduk.Count() > 0)
            {
                RepeaterLaporan.DataSource = ListPerpindahanStokProduk;

                #region HEADER
                LabelGtandTotalSelisihQty.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyPositif) + ListPerpindahanStokProduk.Sum(item => item.SelisihQtyNegatif).ToFormatHarga();
                LabelGtandTotalSelisihNominal.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalPositif) + ListPerpindahanStokProduk.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                LabelTotalJumlahQtyPositif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyPositif).ToFormatHargaBulat();
                LabelTotalJumlahQtyNegatif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyNegatif).ToFormatHargaBulat();
                LabelTotalJumlahNominalPositif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                #endregion

                #region FOOTER
                LabelTotalJumlahQtyPositif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyPositif).ToFormatHargaBulat();
                LabelTotalJumlahQtyNegatif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihQtyNegatif).ToFormatHargaBulat();
                LabelTotalJumlahNominalPositif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalPositif).ToFormatHarga();
                LabelTotalJumlahNominalNegatif2.Text = ListPerpindahanStokProduk.Sum(item => item.SelisihNominalNegatif).ToFormatHarga();
                #endregion

            }
            else
            {
                RepeaterLaporan.DataSource = null;
            }

            RepeaterLaporan.DataBind();


            return ListPerpindahanStokProduk;
        }
    }

    private static void DataProduk(TBStokProduk[] DataStok, int i, int index, Stok_Model newDataClassStokOpname)
    {
        newDataClassStokOpname.IndexClass = i;
        newDataClassStokOpname.IDKombinasiProduk = DataStok[index].IDKombinasiProduk;
        newDataClassStokOpname.KodeKombinasiProduk = DataStok[index].TBKombinasiProduk.KodeKombinasiProduk;
        newDataClassStokOpname.NamaKombinasiProduk = DataStok[index].TBKombinasiProduk.Nama;
        newDataClassStokOpname.NamaProduk = DataStok[index].TBKombinasiProduk.TBProduk.Nama;
        newDataClassStokOpname.Warna = DataStok[index].TBKombinasiProduk.TBProduk.TBWarna.Nama;
        newDataClassStokOpname.Kategori = DataStok[index].TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama;
        newDataClassStokOpname.Brand = DataStok[index].TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama;
    }
}