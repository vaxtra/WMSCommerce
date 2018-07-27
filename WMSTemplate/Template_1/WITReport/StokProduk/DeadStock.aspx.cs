using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_StokProduk_DeadStok : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Warna_Class ClassWarna = new Warna_Class(db);
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                Tempat_Class ClassTempat = new Tempat_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

                DropDownListWarna.Items.AddRange(ClassWarna.Dropdownlist());
                DropDownListBrand.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                DropDownListKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = ((PenggunaLogin)Session["PenggunaLogin"]).IDTempat.ToString();

                DropDownListVarian.Items.AddRange(ClassAtributProduk.Dropdownlist());
            }
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
        #region DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");
        #endregion

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LiteralLaporan.Text = string.Empty;
            LiteralProgressBar.Text = string.Empty;

            //MENCARI KOMBINASI PRODUK YANG MEMILIKI TRANSAKSI
            var TransaksiDetail = db.TBTransaksiDetails
                .Where(item =>
                    item.TBTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete &&
                    item.TBTransaksi.TanggalOperasional >= (DateTime)ViewState["TanggalAwal"] &&
                    item.TBTransaksi.TanggalOperasional <= (DateTime)ViewState["TanggalAkhir"])
                .Select(item => item.IDKombinasiProduk)
                .Distinct();

            //MENCARI STOK PRODUK YANG LEBIH BESAR DARI 0 DAN TIDAK MEMILIKI TRANSAKSI
            var StokProduk = db.TBStokProduks.Where(item => item.Jumlah > 0);

            #region FILTER
            if (DropDownListTempat.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt());

            if (DropDownListWarna.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDWarna == DropDownListWarna.SelectedValue.ToInt());

            if (DropDownListBrand.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == DropDownListBrand.SelectedValue.ToInt());

            if (DropDownListKategori.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 && item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == DropDownListKategori.SelectedValue.ToInt());

            if (DropDownListVarian.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == DropDownListVarian.SelectedValue.ToInt());

            if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.Nama.Contains(TextBoxProduk.Text));
            #endregion

            if (StokProduk.Count() > 0)
            {
                int TotalStokNormal = StokProduk.Sum(item => item.Jumlah.Value);

                //PENCARIAN STOK MATI
                var ResultStokProduk = StokProduk
                    .Where(item => !TransaksiDetail.Any(id => id == item.IDKombinasiProduk))
                    .Select(item => new
                    {
                        IDProduk = item.TBKombinasiProduk.IDProduk,
                        Jumlah = item.Jumlah,
                        Tempat = item.TBTempat.Nama,
                        Nama = item.TBKombinasiProduk.TBAtributProduk.Nama
                    });

                var RingkasanProduk = ResultStokProduk.Select(item => item.IDProduk).Distinct();

                //MENCARI PRODUK
                List<dynamic> Produk = new List<dynamic>();
                Produk.AddRange(db.TBProduks
                    .Where(item => RingkasanProduk.Any(id => id == item.IDProduk))
                    .Select(item => new
                    {
                        Produk = item.Nama,
                        IDKategoriProduk = (item.TBRelasiProdukKategoriProduks.Count > 0) ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk : 0,
                        Kategori = (item.TBRelasiProdukKategoriProduks.Count > 0) ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                        PemilikProduk = item.TBPemilikProduk.Nama,
                        Warna = item.TBWarna.Nama,
                        TanggalDaftar = item._TanggalInsert,
                        TotalQuantity = ResultStokProduk.Where(item2 => item2.IDProduk == item.IDProduk).Sum(item2 => item2.Jumlah),
                        Stok = ResultStokProduk.Where(item2 => item2.IDProduk == item.IDProduk),
                        JumlahStok = ResultStokProduk.Where(item2 => item2.IDProduk == item.IDProduk).Count()
                    }));

                #region ORDER BY
                string PengaturanTotalQuantity = string.Empty;
                string PengaturanTanggalPembuatan = string.Empty;

                if (DropDownListJenisPengurutan.SelectedValue == "0")
                {
                    int JumlahProduk = Produk.Count();
                    int IndexScore = 0;

                    List<dynamic> temp = new List<dynamic>();

                    foreach (var item in Produk.OrderByDescending(item => item.TotalQuantity))
                    {
                        temp.Add(new
                        {
                            item.Produk,
                            item.IDKategoriProduk,
                            item.Kategori,
                            item.PemilikProduk,
                            item.Warna,
                            item.TanggalDaftar,
                            item.TotalQuantity,
                            item.Stok,
                            item.JumlahStok,
                            Score = JumlahProduk - IndexScore
                        });

                        IndexScore++;
                    }

                    IndexScore = 0;

                    List<dynamic> temp1 = new List<dynamic>();

                    foreach (var item in temp.OrderBy(item => item.TanggalDaftar))
                    {
                        temp1.Add(new
                        {
                            item.Produk,
                            item.IDKategoriProduk,
                            item.Kategori,
                            item.PemilikProduk,
                            item.Warna,
                            item.TanggalDaftar,
                            item.TotalQuantity,
                            item.Stok,
                            item.JumlahStok,
                            Score1 = (JumlahProduk - IndexScore) + item.Score
                        });

                        IndexScore++;
                    }

                    temp = new List<dynamic>();

                    Produk = new List<dynamic>();
                    Produk.AddRange(temp1.OrderByDescending(item => item.Score1).Select(item => new
                    {
                        item.Produk,
                        item.IDKategoriProduk,
                        item.Kategori,
                        item.PemilikProduk,
                        item.Warna,
                        item.TanggalDaftar,
                        item.TotalQuantity,
                        item.Stok,
                        item.JumlahStok
                    }));

                    temp1 = new List<dynamic>();

                    PengaturanTanggalPembuatan = "warning";
                    PengaturanTotalQuantity = "warning";
                }

                if (DropDownListJenisPengurutan.SelectedValue == "1")
                {
                    Produk = Produk.OrderByDescending(item => item.TotalQuantity).ToList();
                    PengaturanTotalQuantity = "warning";
                }

                if (DropDownListJenisPengurutan.SelectedValue == "2")
                {
                    Produk = Produk.OrderBy(item => item.TanggalDaftar).ToList();
                    PengaturanTanggalPembuatan = "warning";
                }
                #endregion

                int TotalStokMati = Produk.Sum(item => item.TotalQuantity);

                #region USER INTERFACE
                int index = 1;
                int indexVarian = 1;

                foreach (var item in Produk)
                {
                    indexVarian = 1;

                    LiteralLaporan.Text += "<tr>";

                    string rowspan = item.JumlahStok >= 2 ? "rowspan='" + item.JumlahStok + "'" : "";

                    LiteralLaporan.Text += "<td " + rowspan + ">" + index++ + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + ">" + item.Produk + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + ">" + item.Warna + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + ">" + item.PemilikProduk + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + ">" + item.Kategori + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + " class='" + PengaturanTanggalPembuatan + "'>" + item.TanggalDaftar.ToFormatTanggal() + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + " class='text-right " + PengaturanTotalQuantity + "'>" + item.TotalQuantity.ToFormatHargaBulat() + "</td>";

                    foreach (var item2 in item.Stok)
                    {
                        if (indexVarian > 1)
                            LiteralLaporan.Text += "<tr>";

                        LiteralLaporan.Text += "<td>" + item2.Tempat + "</td>";
                        LiteralLaporan.Text += "<td>" + item2.Nama + "</td>";
                        LiteralLaporan.Text += "<td class='text-right'>" + item2.Jumlah.ToFormatHargaBulat() + "</td>";
                        LiteralLaporan.Text += "</tr>";

                        indexVarian++;
                    }
                }
                #endregion

                LabelTotalStokProduk.Text = TotalStokMati.ToFormatHargaBulat();
                LabelTotalStokProduk1.Text = LabelTotalStokProduk.Text;

                #region PROGRESS BAR
                decimal PersentaseStokMati = Math.Round((decimal)TotalStokMati / (decimal)TotalStokNormal * 100, 2); //Math.Round();

                LiteralProgressBar.Text += "<div class='progress-bar progress-bar-success' style='width: " + (100 - PersentaseStokMati) + "%'>" + (100 - PersentaseStokMati) + "%</div>";
                LiteralProgressBar.Text += "<div class='progress-bar progress-bar-danger' style='width: " + PersentaseStokMati + "%'>" + PersentaseStokMati + "%</div>";
                #endregion
            }
        }
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        TextBoxProduk.Text = string.Empty;
        DropDownListWarna.SelectedValue = "0";
        DropDownListBrand.SelectedValue = "0";
        DropDownListKategori.SelectedValue = "0";
        DropDownListTempat.SelectedValue = "0";
        DropDownListVarian.SelectedValue = "0";

        LoadData();
    }
}