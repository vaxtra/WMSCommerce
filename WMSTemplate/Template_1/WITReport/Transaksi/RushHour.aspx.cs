using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITReport_Transaksi_RushHour : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);

                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataBind();
                DropDownListTempat.Items.Insert(0, new ListItem { Text = "- Semua Tempat -", Value = "0" });
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListStatusTransaksi.DataSource = db.TBStatusTransaksis.ToArray();
                DropDownListStatusTransaksi.DataValueField = "IDStatusTransaksi";
                DropDownListStatusTransaksi.DataTextField = "Nama";
                DropDownListStatusTransaksi.DataBind();
                DropDownListStatusTransaksi.Items.Insert(0, new ListItem { Text = "- Semua Status -", Value = "0" });
                DropDownListStatusTransaksi.SelectedValue = ((int)EnumStatusTransaksi.Complete).ToString();

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

                if (Pengguna.PointOfSales == TipePointOfSales.Retail)
                {
                    DivTotalTamu.Visible = false;

                    HeaderTamuPagi.Visible = false;
                    HeaderPersentasePagi.Visible = false;
                    HeaderTamuMalam.Visible = false;
                    HeaderPersentaseMalam.Visible = false;

                    FooterTamuPagi.Visible = false;
                    FooterPersentasePagi.Visible = false;
                    FooterTamuMalam.Visible = false;
                    FooterPersentaseMalam.Visible = false;
                }
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

            LabelFilter.Text = "(" + DropDownListTempat.SelectedItem.Text + " - " + DropDownListStatusTransaksi.SelectedItem.Text + ")";

            var _hasilTransaksi = db.TBTransaksis.Select(item => new { item.IDTempat, item.IDStatusTransaksi, item.TanggalTransaksi, item.JumlahProduk, item.GrandTotal, item.JumlahTamu }).ToArray();

            if (DropDownListTempat.SelectedValue != "0")
            {
                _hasilTransaksi = _hasilTransaksi.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();
            }

            if (DropDownListStatusTransaksi.SelectedValue != "0")
            {
                _hasilTransaksi = _hasilTransaksi.Where(item => item.IDStatusTransaksi == DropDownListStatusTransaksi.SelectedValue.ToInt()).ToArray();
            }

            var _dataTransaksi = _hasilTransaksi
            .Where(item => item.TanggalTransaksi.Value.Date >= (DateTime)ViewState["TanggalAwal"] && item.TanggalTransaksi.Value.Date <= (DateTime)ViewState["TanggalAkhir"])
            .GroupBy(item => item.TanggalTransaksi.Value.Hour)
            .Select(item => new
            {
                Jam = item.Key,
                Quantity = item.Sum(item2 => item2.JumlahProduk) == null ? 0 : item.Sum(item2 => item2.JumlahProduk),
                Transaksi = item.Count(),
                Penjualan = item.Sum(item2 => item2.GrandTotal),
                JumlahTamu = item.Sum(item2 => item2.JumlahTamu)
            });

            if (_dataTransaksi.Count() > 0)
            {
                LabelTotalPenjualan.Text = _dataTransaksi.Sum(item => item.Penjualan).ToFormatHarga();
                LabelTotalQuantity.Text = _dataTransaksi.Sum(item => item.Quantity).ToFormatHargaBulat();
                LabelTotalTransaksi.Text = _dataTransaksi.Sum(item => item.Transaksi).ToFormatHargaBulat();
                LabelTotalTamu.Text = _dataTransaksi.Sum(item => item.JumlahTamu).ToFormatHargaBulat();
            }
            else
            {
                LabelTotalPenjualan.Text = "0";
                LabelTotalQuantity.Text = "0";
                LabelTotalTransaksi.Text = "0";
                LabelTotalTamu.Text = "0";
            }

            decimal _quantity = 0;
            decimal _transaksi = 0;
            decimal _penjualan = 0;
            decimal _jumlahtamu = 0;
            decimal _persentaseTransaksi = 0;
            decimal _persentaseQuantity = 0;
            decimal _persentaseJumlahTamu = 0;

            RepeaterSebelumTengahHari.DataSource = null;
            RepeaterSebelumTengahHari.DataBind();

            if (_dataTransaksi.Where(item => item.Jam < 12).Count() > 0)
            {
                _transaksi = _dataTransaksi.Where(item => item.Jam < 12).Count() > 0 ? _dataTransaksi.Where(item => item.Jam < 12).Sum(item => item.Transaksi) : 0;
                _quantity = _dataTransaksi.Where(item => item.Jam < 12).Sum(item => item.Quantity) ?? 0;
                _jumlahtamu = _dataTransaksi.Where(item => item.Jam < 12).Sum(item => item.JumlahTamu) ?? 0;

                var sebelumTengahHari = _dataTransaksi
                    .Where(item => item.Jam < 12)
                    .Select(item => new
                    {
                        item.Jam,
                        item.Penjualan,
                        item.Quantity,
                        item.Transaksi,
                        item.JumlahTamu,
                        PersentaseTransaksi = _transaksi > 0 ? Math.Round((decimal)(item.Transaksi / _transaksi * 100), 0, MidpointRounding.AwayFromZero) : 0,
                        PersentaseQuantity = _quantity > 0 ? Math.Round((decimal)(item.Quantity / _quantity * 100), 0, MidpointRounding.AwayFromZero) : 0,
                        PersentaseJumlahTamu = _jumlahtamu > 0 ? Math.Round((decimal)(item.JumlahTamu / _jumlahtamu * 100), 0, MidpointRounding.AwayFromZero) : 0
                    });

                _penjualan = sebelumTengahHari.Sum(item => item.Penjualan) ?? 0;
                _persentaseTransaksi = sebelumTengahHari.Sum(item => item.PersentaseTransaksi);
                _persentaseQuantity = sebelumTengahHari.Sum(item => item.PersentaseQuantity);
                _persentaseJumlahTamu = sebelumTengahHari.Sum(item => item.PersentaseJumlahTamu);

                RepeaterSebelumTengahHari.DataSource = sebelumTengahHari.OrderBy(item => item.Jam);
                RepeaterSebelumTengahHari.DataBind();
            }

            LabelSebelumTengahHariTransaksi.Text = _transaksi.ToFormatHargaBulat();
            LabelSebelumTengahHariQuantity.Text = _quantity.ToFormatHargaBulat();
            LabelSebelumTengahHariJumlahTamu.Text = _jumlahtamu.ToFormatHargaBulat();
            LabelSebelumTengahHariPenjualan.Text = _penjualan.ToFormatHarga();

            RepeaterSetelahTengahHari.DataSource = null;
            RepeaterSetelahTengahHari.DataBind();

            _quantity = 0;
            _transaksi = 0;
            _penjualan = 0;
            _jumlahtamu = 0;
            _persentaseTransaksi = 0;
            _persentaseQuantity = 0;
            _persentaseJumlahTamu = 0;

            if (_dataTransaksi.Where(item => item.Jam >= 12).Count() > 0)
            {
                _transaksi = _dataTransaksi.Where(item => item.Jam >= 12).Count() > 0 ? _dataTransaksi.Where(item => item.Jam >= 12).Sum(item => item.Transaksi) : 0;
                _quantity = _dataTransaksi.Where(item => item.Jam >= 12).Sum(item => item.Quantity) ?? 0;
                _jumlahtamu = _dataTransaksi.Where(item => item.Jam >= 12).Sum(item => item.JumlahTamu) ?? 0;

                var setelahTengahHari = _dataTransaksi
                    .Where(item => item.Jam >= 12)
                    .Select(item => new
                    {
                        item.Jam,
                        item.Penjualan,
                        item.Quantity,
                        item.Transaksi,
                        PersentaseTransaksi = _transaksi > 0 ? Math.Round((decimal)(item.Transaksi / _transaksi * 100), 0, MidpointRounding.AwayFromZero) : 0,
                        PersentaseQuantity = _quantity > 0 ? Math.Round((decimal)(item.Quantity / _quantity * 100), 0, MidpointRounding.AwayFromZero) : 0,
                        PersentaseJumlahTamu = _jumlahtamu > 0 ? Math.Round((decimal)(item.JumlahTamu / _jumlahtamu * 100), 0, MidpointRounding.AwayFromZero) : 0,
                        item.JumlahTamu
                    });

                _penjualan = setelahTengahHari.Sum(item => item.Penjualan) ?? 0;
                _persentaseTransaksi = setelahTengahHari.Sum(item => item.PersentaseTransaksi);
                _persentaseQuantity = setelahTengahHari.Sum(item => item.PersentaseQuantity);
                _persentaseJumlahTamu = setelahTengahHari.Sum(item => item.PersentaseJumlahTamu);

                RepeaterSetelahTengahHari.DataSource = setelahTengahHari.OrderBy(item => item.Jam);
                RepeaterSetelahTengahHari.DataBind();
            }

            LabelSetelahTengahHariTransaksi.Text = _transaksi.ToFormatHargaBulat();
            LabelSetelahTengahHariQuantity.Text = _quantity.ToFormatHargaBulat();
            LabelSetelahTengahHariJumlahTamu.Text = _jumlahtamu.ToFormatHargaBulat();
            LabelSetelahTengahHariPenjualan.Text = _penjualan.ToFormatHarga();
        }
    }

    protected void DropDownListTempat_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    protected void DropDownListStatusTransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }

    protected void Repeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        if (Pengguna.PointOfSales == TipePointOfSales.Retail)
        {
            ((HtmlTableCell)(e.Item.FindControl("BodyTamu"))).Attributes.Add("class", "d-none");
            ((HtmlTableCell)(e.Item.FindControl("BodyPersentase"))).Attributes.Add("class", "d-none");
        }
    }
}