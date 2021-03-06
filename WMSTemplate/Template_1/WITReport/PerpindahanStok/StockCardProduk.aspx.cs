﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_PerpindahanStok_StockCardProduk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                KategoriBahanBaku_Class KategoriBahanBaku_Class = new KategoriBahanBaku_Class();
                JenisPerpindahanStok_Class JenisPerpindahanStok_Class = new JenisPerpindahanStok_Class();

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempat.Items.RemoveAt(0);
                DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariKombinasiProduk.DataSource = db.TBKombinasiProduks.OrderBy(item => item.TBProduk.Nama).ThenBy(item => item.IDAtributProduk).ToArray();
                DropDownListCariKombinasiProduk.DataValueField = "IDKombinasiProduk";
                DropDownListCariKombinasiProduk.DataTextField = "Nama";
                DropDownListCariKombinasiProduk.DataBind();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");
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
    #endregion

    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
            LabelPeriode.Text = TextBoxTanggalAwal.Text;
        else
            LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPerpindahanStokProduk[] perpindahanStok = db.TBPerpindahanStokProduks.Where(item => item.Tanggal.Date >= TextBoxTanggalAwal.Text.ToDateTime() && item.IDTempat == DropDownListCariTempat.SelectedValue.ToInt() && item.TBStokProduk.TBKombinasiProduk.IDKombinasiProduk == DropDownListCariKombinasiProduk.SelectedValue.ToInt()).OrderBy(item => item.Tanggal).ToArray();

            if (perpindahanStok.Count() > 0)
            {
                int StokSekarang = perpindahanStok.FirstOrDefault().TBStokProduk.Jumlah.Value;
                int StokAwal = StokSekarang + perpindahanStok.Sum(item => (item.TBJenisPerpindahanStok.Status == true ? item.Jumlah * (-1) : item.Jumlah));

                List<DataModelPerpindahanStokDetail> ListPerpindahanStokProduk = new List<DataModelPerpindahanStokDetail>();

                foreach (var item in perpindahanStok.AsEnumerable().Where(item => item.Tanggal.Date <= TextBoxTanggalAkhir.Text.ToDateTime())
                    .Select(item => new
                    {
                        Tanggal = item.Tanggal.ToFormatTanggalJam(),
                        item.Keterangan,
                        Jenis = item.TBJenisPerpindahanStok.Nama,
                        Status = item.TBJenisPerpindahanStok.Status,
                        Masuk = item.TBJenisPerpindahanStok.Status == true ? item.Jumlah : 0,
                        Keluar = item.TBJenisPerpindahanStok.Status == false ? item.Jumlah : 0
                    }))
                {
                    ListPerpindahanStokProduk.Add(new DataModelPerpindahanStokDetail
                    {
                        Tanggal = item.Tanggal,
                        Keterangan = item.Keterangan,
                        Status = item.Status.Value,
                        Jenis = item.Jenis,
                        Masuk = item.Masuk,
                        Keluar = item.Keluar,
                        Saldo = StokAwal + (item.Status == true ? item.Masuk : item.Keluar * (-1))
                    });

                    StokAwal += (item.Status == true ? item.Masuk : item.Keluar * (-1));
                }

                RepeaterLaporan.DataSource = ListPerpindahanStokProduk;
                RepeaterLaporan.DataBind();
            }
            else
            {
                RepeaterLaporan.DataSource = null;
                RepeaterLaporan.DataBind();
            }
        }
    }
    private void LoadData()
    {
        LoadData(false);
    }
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }

    public class DataModelPerpindahanStokDetail
    {
        public string Tanggal { get; set; }
        public string KombinasiProduk { get; set; }
        public int Masuk { get; set; }
        public int Keluar { get; set; }
        public int Saldo { get; set; }
        public string Jenis { get; set; }
        public bool Status { get; set; }
        public string Keterangan { get; set; }
    }
}