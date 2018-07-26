using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_RatioOnStock_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);
                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());

                if (Request.QueryString["IDTempat"].ToInt() != 0)
                    DropDownListTempat.SelectedValue = Request.QueryString["IDTempat"];

                if (!string.IsNullOrWhiteSpace(Request.QueryString["TanggalAwal"]))
                    ViewState["TanggalAwal"] = Request.QueryString["TanggalAwal"].ToDateTime();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["TanggalAkhir"]))
                    ViewState["TanggalAkhir"] = Request.QueryString["TanggalAkhir"].ToDateTime();
            }

            LoadData();
        }
    }
    private void LoadData()
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
            if (DropDownListTempat.SelectedValue != "0")
            {
                var ListKombinasiProduk = db.TBKombinasiProduks
                    .Select(item => new
                    {
                        Produk = item.TBProduk.Nama,
                        Warna = item.TBProduk.TBWarna.Nama,
                        Brand = item.TBProduk.TBPemilikProduk.Nama,
                        Kategori = item.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                        Varian = item.TBAtributProduk.Nama,
                        Terjual = item.TBTransaksiDetails
                                    .Where(item2 =>
                                                    item2.TBTransaksi.IDTempat == DropDownListTempat.SelectedValue.ToInt() &&
                                                    item2.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                                    item2.TBTransaksi.TanggalOperasional.Value >= (DateTime)ViewState["TanggalAwal"] &&
                                                    item2.TBTransaksi.TanggalOperasional.Value <= (DateTime)ViewState["TanggalAkhir"]),
                        Stok = item.TBStokProduks
                                    .Where(item2 => item2.IDTempat == DropDownListTempat.SelectedValue.ToInt())
                                    .Sum(item2 => item2.Jumlah)
                    });

                var Result = ListKombinasiProduk.Select(item => new
                {
                    item,
                    Terjual = item.Terjual.Count() > 0 ? item.Terjual.Sum(item2 => item2.Quantity) : 0,
                }).OrderByDescending(item => item.Terjual);

                var TotalTerjual = Result.Sum(item => item.Terjual);

                var Result1 = Result.Select(item => new
                {
                    item.item,
                    Terjual = item.Terjual,
                    Persentase = TotalTerjual > 0 ? (decimal)item.Terjual / (decimal)TotalTerjual * 100 : 0
                });

                LabelTerjual.Text = TotalTerjual.ToFormatHargaBulat();
                LabelStok.Text = Result.Sum(item => item.item.Stok).ToFormatHargaBulat();

                LabelTerjual1.Text = LabelTerjual.Text;
                LabelStok1.Text = LabelStok.Text;

                RepeaterKombinasiProduk.DataSource = Result1;
                RepeaterKombinasiProduk.DataBind();
            }
            else
            {
                var ListKombinasiProduk = db.TBKombinasiProduks
                    .Select(item => new
                    {
                        Produk = item.TBProduk.Nama,
                        Warna = item.TBProduk.TBWarna.Nama,
                        Brand = item.TBProduk.TBPemilikProduk.Nama,
                        Kategori = item.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                        Varian = item.TBAtributProduk.Nama,
                        Terjual = item.TBTransaksiDetails
                                    .Where(item2 =>
                                                    item2.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                                                    item2.TBTransaksi.TanggalOperasional.Value >= (DateTime)ViewState["TanggalAwal"] &&
                                                    item2.TBTransaksi.TanggalOperasional.Value <= (DateTime)ViewState["TanggalAkhir"]),
                        Stok = item.TBStokProduks.Sum(item2 => item2.Jumlah)
                    });

                var Result = ListKombinasiProduk.Select(item => new
                {
                    item,
                    Terjual = item.Terjual.Count() > 0 ? item.Terjual.Sum(item2 => item2.Quantity) : 0
                }).OrderByDescending(item => item.Terjual);

                var TotalTerjual = Result.Sum(item => item.Terjual);

                var Result1 = Result.Select(item => new
                {
                    item.item,
                    Terjual = item.Terjual,
                    Persentase = TotalTerjual > 0 ? (decimal)item.Terjual / (decimal)TotalTerjual * 100 : 0
                });

                LabelTerjual.Text = TotalTerjual.ToFormatHargaBulat();
                LabelStok.Text = Result.Sum(item => item.item.Stok).ToFormatHargaBulat();

                LabelTerjual1.Text = LabelTerjual.Text;
                LabelStok1.Text = LabelStok.Text;

                RepeaterKombinasiProduk.DataSource = Result1;
                RepeaterKombinasiProduk.DataBind();
            }
        }
    }

    #region DEFAULT
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

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
}