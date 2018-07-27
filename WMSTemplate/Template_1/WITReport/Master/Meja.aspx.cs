using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Master_Meja : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin _pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListStatus.DataSource = db.TBStatusTransaksis.ToArray();
                DropDownListStatus.DataValueField = "IDStatusTransaksi";
                DropDownListStatus.DataTextField = "Nama";
                DropDownListStatus.DataBind();
                DropDownListStatus.Items.Insert(0, new ListItem { Text = "-Semua Status-", Value = "0" });
                DropDownListStatus.SelectedValue = "5";

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                LoadData();
            }
        }
    }

    protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
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

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            if (ViewState["TanggalAwal"].ToString() == ViewState["TanggalAkhir"].ToString())
                LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal();
            else
                LabelPeriode.Text = ViewState["TanggalAwal"].ToFormatTanggal() + " - " + ViewState["TanggalAkhir"].ToFormatTanggal();

            TBTransaksi[] _hasilTransaksi = db.TBTransaksis.Where(item => item.TanggalOperasional.Value.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.TanggalOperasional.Value.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();
            if (DropDownListStatus.SelectedValue != "0")
            {
                _hasilTransaksi = _hasilTransaksi.Where(item => item.IDStatusTransaksi == DropDownListStatus.SelectedValue.ToInt()).ToArray();
            }

            //var groupMeja = _hasilTransaksi  
            //    .GroupBy(item => new { item.IDMeja, item.TBMeja.Nama })
            //    .Select(item => new
            //    {
            //        item.Key,
            //        JumlahTransaksi = item.Count(),
            //        JumlahProduk = item.Sum(x => x.JumlahProduk),
            //        JumlahTamu = item.Sum(x => x.JumlahTamu),
            //        Grandtotal = item.Sum(x => x.GrandTotal)
            //    }).ToArray();

            var meja = db.TBMejas.AsEnumerable()
                .Select(item => new
                {
                    item.IDMeja,
                    Nama = item.Nama,
                    JumlahTransaksi = _hasilTransaksi.Where(item2 => item2.IDMeja == item.IDMeja).Count(),
                    JumlahProduk = _hasilTransaksi.Where(item2 => item2.IDMeja == item.IDMeja).Sum(x => x.JumlahProduk),
                    JumlahTamu = _hasilTransaksi.Where(item2 => item2.IDMeja == item.IDMeja).Sum(x => x.JumlahTamu),
                    Grandtotal = _hasilTransaksi.Where(item2 => item2.IDMeja == item.IDMeja).Sum(x => x.GrandTotal)
                }).ToArray();

            decimal transaksi = meja.Count() > 0 ? meja.Sum(item => item.JumlahTransaksi) : 0;
            decimal produk = meja.Sum(item => item.JumlahProduk) ?? 0;
            decimal tamu = meja.Sum(item => item.JumlahTamu) ?? 0;
            decimal grandtotal = meja.Sum(item => item.Grandtotal) ?? 0;

            var hasil = meja.Select(item => new
            {
                item.IDMeja,
                Meja = item.Nama,
                JumlahTransaksi = item.JumlahTransaksi,
                JumlahProduk = item.JumlahProduk,
                JumlahTamu = item.JumlahTamu,
                PersentaseTransaksi = transaksi > 0 ? Math.Round((item.JumlahTransaksi / transaksi) * 100, 0, MidpointRounding.AwayFromZero) : 0,
                PersentaseProduk = produk > 0 ? Math.Round((item.JumlahProduk.Value / produk) * 100, 0, MidpointRounding.AwayFromZero) : 0,
                PersentaseTamu = tamu > 0 ? Math.Round((item.JumlahTamu.Value / tamu) * 100, 0, MidpointRounding.AwayFromZero) : 0,
                Grandtotal = item.Grandtotal
            }).OrderBy(item => item.IDMeja);

            if (DropDownListFilter.SelectedValue == "Transaksi")
                hasil = hasil.OrderByDescending(item => item.JumlahTransaksi);
            else if (DropDownListFilter.SelectedValue == "Produk")
                hasil = hasil.OrderByDescending(item => item.JumlahProduk);
            else if (DropDownListFilter.SelectedValue == "Tamu")
                hasil = hasil.OrderByDescending(item => item.JumlahTamu);
            else if (DropDownListFilter.SelectedValue == "Grandtotal")
                hasil = hasil.OrderByDescending(item => item.Grandtotal);

            Repeater.DataSource = hasil;
            Repeater.DataBind();

            LabelHeaderTotalTransaksi.Text = transaksi.ToFormatHargaBulat();
            LabelHeaderTotalProduk.Text = produk.ToFormatHargaBulat();
            LabelHeaderTotalTamu.Text = tamu.ToFormatHargaBulat();
            LabelHeaderTotalGrandtotal.Text = grandtotal.ToFormatHarga();

            LabelFooterTotalTransaksi.Text = LabelHeaderTotalTransaksi.Text;
            LabelFooterTotalProduk.Text = LabelHeaderTotalProduk.Text;
            LabelFooterTotalTamu.Text = LabelHeaderTotalTamu.Text;
            LabelFooterTotalGrandtotal.Text = LabelHeaderTotalGrandtotal.Text;
        }
    }
}