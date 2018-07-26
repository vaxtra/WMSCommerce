﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Vendor_Ringkasan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();
            }

            LoadData();
        }
    }

    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }

    protected void DropDownListJatuhTempo_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            decimal batas = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)EnumStoreKonfigurasi.JumlahHariSebelumJatuhTempo).Pengaturan.ToDecimal();
            TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt() && item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();
            var daftarJatuhTempo = daftarPOProduksiProduk
                            .Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas))
                            .Select(item => new
                            {
                                ClassWarna = Warna((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays), batas),
                                Pengguna = item.TBPengguna.NamaLengkap,
                                item.IDPOProduksiProduk,
                                item.TBVendor.Nama,
                                item.EnumJenisProduksi,
                                item.Tanggal,
                                item.TanggalJatuhTempo,
                                Jarak = (item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays
                            })
                            .OrderBy(item => item.Jarak)
                            .ToArray();

            if (DropDownListJatuhTempo.SelectedValue == "0")
            {
                RepeaterPOProdukJatuhTempoSatu.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "danger");
                RepeaterPOProdukJatuhTempoSatu.DataBind();
                RepeaterPOProdukJatuhTempoDua.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "warning");
                RepeaterPOProdukJatuhTempoDua.DataBind();
                RepeaterPOProdukJatuhTempoTiga.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == string.Empty);
                RepeaterPOProdukJatuhTempoTiga.DataBind();
            }
            else
            {
                RepeaterPOProdukJatuhTempoSatu.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "danger" && item.EnumJenisProduksi == DropDownListJatuhTempo.SelectedValue.ToInt());
                RepeaterPOProdukJatuhTempoSatu.DataBind();
                RepeaterPOProdukJatuhTempoDua.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "warning" && item.EnumJenisProduksi == DropDownListJatuhTempo.SelectedValue.ToInt());
                RepeaterPOProdukJatuhTempoDua.DataBind();
                RepeaterPOProdukJatuhTempoTiga.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == string.Empty && item.EnumJenisProduksi == DropDownListJatuhTempo.SelectedValue.ToInt());
                RepeaterPOProdukJatuhTempoTiga.DataBind();
            }
        }
    }
    #endregion

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                LabelPeriode.Text = TextBoxTanggalAwal.Text;
            else
                LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

            TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt() && item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();
            TBPOProduksiProdukDetail[] daftarPOProduksiProdukDetail = db.TBPOProduksiProdukDetails.Where(item => item.TBPOProduksiProduk.IDTempat == DropDownListTempat.SelectedValue.ToInt() && item.TBPOProduksiProduk.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.TBPOProduksiProduk.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();
            TBPenerimaanPOProduksiProdukDetail[] daftarPenerimaanPOProduksiProdukDetail = db.TBPenerimaanPOProduksiProdukDetails.Where(item => item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDTempat == DropDownListTempat.SelectedValue.ToInt() && item.TBPenerimaanPOProduksiProduk.TanggalTerima.Value.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.TBPenerimaanPOProduksiProduk.TanggalTerima.Value.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();

            #region Purchase Order
            var purchaseOrder = daftarPOProduksiProduk.GroupBy(item => new
            {
                item.EnumJenisProduksi
            })
            .Select(item => new
            {
                item.Key.EnumJenisProduksi,
                JenisPO = Pengaturan.JenisPOProduksi(item.Key.EnumJenisProduksi, "Produk"),
                Baru = item.Count(),
                GrandtotalBaru = item.Sum(data => data.Grandtotal),
                Proses = item.Count(),
                GrandtotalProses = item.Sum(data => data.Grandtotal),
                Selesai = item.Count(),
                GrandtotalSelesai = item.Sum(data => data.Grandtotal),
                Total = item.Count(),
                GrandtotalTotal = item.Sum(data => data.Grandtotal),
                Progress = Persentase(item.Count() == 0 ? -1 : ((decimal)item.Count() / (decimal)item.Count()) * 100),
            }).OrderBy(item => item.EnumJenisProduksi).ToArray();
            RepeaterPurchaseOrder.DataSource = purchaseOrder;
            RepeaterPurchaseOrder.DataBind();

            LabelBaru.Text = purchaseOrder.Sum(item => item.Baru).ToFormatHargaBulat();
            LabelGrandtotalBaru.Text = purchaseOrder.Sum(item => item.GrandtotalBaru).ToFormatHarga();
            LabelProses.Text = purchaseOrder.Sum(item => item.Proses).ToFormatHargaBulat();
            LabeGrandtotalProses.Text = purchaseOrder.Sum(item => item.GrandtotalProses).ToFormatHarga();
            LabelSelesai.Text = purchaseOrder.Sum(item => item.Selesai).ToFormatHargaBulat();
            LabelGrandtotalSelesai.Text = purchaseOrder.Sum(item => item.GrandtotalSelesai).ToFormatHarga();
            LabelTotal.Text = purchaseOrder.Sum(item => item.Total).ToFormatHargaBulat();
            LabelGrandtotalTotal.Text = purchaseOrder.Sum(item => item.GrandtotalTotal).ToFormatHarga();
            #endregion

            #region Summary
            LabelPotongan.Text = daftarPOProduksiProduk.Sum(item => item.PotonganPOProduksiProduk).ToFormatHarga();
            LabelBiayaLainLain.Text = daftarPOProduksiProduk.Sum(item => item.BiayaLainLain).ToFormatHarga();
            LabelTax.Text = daftarPOProduksiProduk.Sum(item => item.Tax).ToFormatHarga();
            LabelGrandtotal.Text = daftarPOProduksiProduk.Sum(item => item.Grandtotal).ToFormatHarga();
            LabelHargaKomposisiDetail.Text = daftarPOProduksiProdukDetail.Sum(item => item.Jumlah * item.HargaPokokKomposisi).ToFormatHarga();
            LabelBiayaTambahanDetail.Text = daftarPOProduksiProdukDetail.Sum(item => item.Jumlah * item.BiayaTambahan).ToFormatHarga();
            LabelPotonganHargaDetail.Text = daftarPOProduksiProdukDetail.Sum(item => item.Jumlah * item.PotonganHargaVendor).ToFormatHarga();
            LabelJumlahProdukDetail.Text = daftarPOProduksiProdukDetail.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelHargaVendorDetail.Text = daftarPOProduksiProdukDetail.Sum(item => item.Jumlah * item.HargaVendor).ToFormatHarga();
            LabelSubtotalDetail.Text = daftarPOProduksiProdukDetail.Sum(item => item.SubtotalHPP + item.SubtotalHargaVendor).ToFormatHarga();
            #endregion

            #region Kategori
            RepeaterKategori.DataSource = daftarPOProduksiProdukDetail.GroupBy(item => new
            {
                Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : ""
            })
            .Select(item => new
            {
                item.Key.Kategori,
                Jumlah = item.Sum(data => data.Jumlah).ToFormatHargaBulat(),
                Subtotal = item.Sum(data => data.SubtotalHPP + data.SubtotalHargaVendor).ToFormatHarga()
            });
            RepeaterKategori.DataBind();
            #endregion

            #region
            //RepeaterPenerimaan.DataSource = daftarPenerimaanPOProduksiProdukDetail.GroupBy(item => new
            //{
            //    item.TBKombinasiProduk,

            //}).Select(item => new
            //{
            //    PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
            //    Produk = item.Key.TBKombinasiProduk.Nama,
            //    AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
            //    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.Key.TBKombinasiProduk),
            //    Diterima = item.Sum(data => data.Diterima).ToFormatHargaBulat(),
            //    Subtotal = item.FirstOrDefault().TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.Sum(data => data.SubtotalHargaVendor).ToFormatHarga() : item.Sum(data => data.SubtotalHPP).ToFormatHarga()
            //});
            //RepeaterPenerimaan.DataBind();

            RepeaterPenerimaan.DataSource = daftarPenerimaanPOProduksiProdukDetail.GroupBy(item => new
            {
                item.TBKombinasiProduk.TBProduk.TBPemilikProduk,
                Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.TBKombinasiProduk),

            }).Select(item => new
            {
                PemilikProduk = item.Key.TBPemilikProduk.Nama,
                Kategori = item.Key.Kategori,
                Diterima = item.Sum(data => data.Diterima).ToFormatHargaBulat(),
                Subtotal = item.FirstOrDefault().TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.Sum(data => data.SubtotalHargaVendor).ToFormatHarga() : item.Sum(data => data.SubtotalHPP).ToFormatHarga()
            }).OrderBy(item => item.PemilikProduk).ThenBy(item => item.Kategori);
            RepeaterPenerimaan.DataBind();
            #endregion

            #region Jatuh Tempo
            decimal batas = db.TBStoreKonfigurasis.FirstOrDefault(item => item.IDStoreKonfigurasi == (int)EnumStoreKonfigurasi.JumlahHariSebelumJatuhTempo).Pengaturan.ToDecimal();
            LabelPanelSetengahJatuhTempo.Text = "1-" + Math.Floor(batas / 2).ToFormatHargaBulat() + " Hari";
            LabelPanelJatuhTempo.Text = Math.Floor(batas / 2).ToFormatHargaBulat() + "-" + batas.ToFormatHargaBulat() + " Hari";

            var daftarJatuhTempo = daftarPOProduksiProduk
                            .Where(item => item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && ((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays) < batas))
                            .Select(item => new
                            {
                                ClassWarna = Warna((int)((item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays), batas),
                                Pengguna = item.TBPengguna.NamaLengkap,
                                item.IDPOProduksiProduk,
                                item.TBVendor.Nama,
                                item.EnumJenisProduksi,
                                item.Tanggal,
                                item.TanggalJatuhTempo,
                                Jarak = (item.TanggalJatuhTempo.Value.Date - DateTime.Now.Date).TotalDays
                            })
                            .OrderBy(item => item.Jarak)
                            .ToArray();

            if (DropDownListJatuhTempo.SelectedValue == "0")
            {
                RepeaterPOProdukJatuhTempoSatu.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "danger");
                RepeaterPOProdukJatuhTempoSatu.DataBind();
                RepeaterPOProdukJatuhTempoDua.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "warning");
                RepeaterPOProdukJatuhTempoDua.DataBind();
                RepeaterPOProdukJatuhTempoTiga.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == string.Empty);
                RepeaterPOProdukJatuhTempoTiga.DataBind();
            }
            else
            {
                RepeaterPOProdukJatuhTempoSatu.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "danger" && item.EnumJenisProduksi == DropDownListJatuhTempo.SelectedValue.ToInt());
                RepeaterPOProdukJatuhTempoSatu.DataBind();
                RepeaterPOProdukJatuhTempoDua.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == "warning" && item.EnumJenisProduksi == DropDownListJatuhTempo.SelectedValue.ToInt());
                RepeaterPOProdukJatuhTempoDua.DataBind();
                RepeaterPOProdukJatuhTempoTiga.DataSource = daftarJatuhTempo.Where(item => item.ClassWarna == string.Empty && item.EnumJenisProduksi == DropDownListJatuhTempo.SelectedValue.ToInt());
                RepeaterPOProdukJatuhTempoTiga.DataBind();
            }
            #endregion   
        }
    }
    private void LoadData()
    {
        LoadData(false);
    }

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }

    public static string Persentase(decimal jumlah)
    {
        if (jumlah == -1)
            return "<div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + 0 + "%;\"><p style=\"color:black\">None</p></div>";
        else if (jumlah > 0 && jumlah < 25)
            return "<div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
        else if (jumlah >= 25 && jumlah < 50)
            return "<div class=\"progress-bar progress-bar-warning\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
        else if (jumlah >= 50 && jumlah < 75)
            return "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
        else
            return "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
    }

    private string Warna(int jumlahHari, decimal batas)
    {
        if (jumlahHari <= batas && jumlahHari > Math.Floor(batas / 2))
            return string.Empty;
        else if (jumlahHari <= Math.Floor(batas / 2) && jumlahHari > 0)
            return "warning";
        else
            return "danger";
    }
}