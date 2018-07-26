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
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Warna_Class ClassWarna = new Warna_Class(db);
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                Tempat_Class ClassTempat = new Tempat_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                Tanggal_Class Tanggal_Class = new Tanggal_Class();

                DropDownListBulan.Items.AddRange(Tanggal_Class.DropdownlistBulan());
                DropDownListTahun.Items.AddRange(Tanggal_Class.DropdownlistTahun());

                DropDownListWarna.Items.AddRange(ClassWarna.Dropdownlist());
                DropDownListBrand.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                DropDownListKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = ((PenggunaLogin)Session["PenggunaLogin"]).IDTempat.ToString();

                DropDownListVarian.Items.AddRange(ClassAtributProduk.Dropdownlist());
            }
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //LiteralLaporan.Text = string.Empty;

            int Tahun = DropDownListTahun.SelectedValue.ToInt();
            int Bulan = DropDownListBulan.SelectedValue.ToInt();
            int Hari = DateTime.DaysInMonth(Tahun, Bulan);

            var PerpindahanStokProduk = db.TBPerpindahanStokProduks
                .Where(item => item.Tanggal.Date <= new DateTime(Tahun, Bulan, Hari).Date)
                .GroupBy(item => new
                {
                    item.IDTempat,
                    item.TBStokProduk.TBKombinasiProduk.TBProduk.IDProduk,
                    item.TBStokProduk.TBKombinasiProduk.IDAtributProduk,
                    Produk = item.TBStokProduk.TBKombinasiProduk.TBProduk.Nama,
                    Warna = item.TBStokProduk.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Brand = item.TBStokProduk.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Kategori = item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0 ? "" : item.TBStokProduk.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama,
                    Varian = item.TBStokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
                })
                .OrderBy(item => item.Key.IDProduk)
                .ThenBy(item => item.Key.IDAtributProduk)
                .Select(item => new
                {
                    item.Key,
                    Total = item.Sum(item2 => item2.TBJenisPerpindahanStok.Status.Value ? item2.Jumlah : (item2.Jumlah * -1))
                });

            if (DropDownListTempat.SelectedValue != "0")
                PerpindahanStokProduk = PerpindahanStokProduk.Where(item => item.Key.IDTempat == DropDownListTempat.SelectedValue.ToInt());

            RepeaterStokProduk.DataSource = PerpindahanStokProduk;
            RepeaterStokProduk.DataBind();

            if (PerpindahanStokProduk.Count() > 0)
                LabelTotalStokProduk.Text = PerpindahanStokProduk.Sum(item => item.Total).ToFormatHargaBulat();
            else
                LabelTotalStokProduk.Text = "0";

            LabelTotalStokProduk1.Text = LabelTotalStokProduk.Text;

            //#region FILTER
            //if (DropDownListTempat.SelectedValue != "0")
            //    StokProduk = StokProduk.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt());

            //if (DropDownListWarna.SelectedValue != "0")
            //    StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDWarna == DropDownListWarna.SelectedValue.ToInt());

            //if (DropDownListBrand.SelectedValue != "0")
            //    StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == DropDownListBrand.SelectedValue.ToInt());

            //if (DropDownListKategori.SelectedValue != "0")
            //    StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 && item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == DropDownListKategori.SelectedValue.ToInt());

            //if (DropDownListVarian.SelectedValue != "0")
            //    StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == DropDownListVarian.SelectedValue.ToInt());

            //if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
            //    StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.Nama.Contains(TextBoxProduk.Text));
            //#endregion

            //var RingkasanProduk = PerpindahanStokProduk.Select(item => item.Key.IDProduk).Distinct();

            //var DataResult = db.TBProduks
            //    .Where(item => RingkasanProduk.Any(id => id == item.IDProduk))
            //    .Select(item => new
            //    {
            //        Produk = item.Nama,
            //        Kategori = (item.TBRelasiProdukKategoriProduks.Count > 0) ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
            //        PemilikProduk = item.TBPemilikProduk.Nama,
            //        Warna = item.TBWarna.Nama,
            //        Stok = PerpindahanStokProduk.Where(item2 => item2.Key.IDProduk == item.IDProduk)
            //    });

            //#region USER INTERFACE
            //int index = 1;
            //int indexVarian = 1;

            //foreach (var item in DataResult)
            //{
            //    indexVarian = 1;

            //    LiteralLaporan.Text += "<tr>";

            //    string rowspan = item.Stok.Count() >= 2 ? "rowspan='" + item.Stok.Count() + "'" : "";

            //    LiteralLaporan.Text += "<td " + rowspan + ">" + index++ + "</td>";
            //    LiteralLaporan.Text += "<td " + rowspan + ">" + item.Produk + "</td>";
            //    LiteralLaporan.Text += "<td " + rowspan + ">" + item.Warna + "</td>";
            //    LiteralLaporan.Text += "<td " + rowspan + ">" + item.PemilikProduk + "</td>";
            //    LiteralLaporan.Text += "<td " + rowspan + ">" + item.Kategori + "</td>";

            //    foreach (var item2 in item.Stok)
            //    {
            //        if (indexVarian > 1)
            //            LiteralLaporan.Text += "<tr>";

            //        LiteralLaporan.Text += "<td>" + item2.Key.Nama + "</td>";
            //        LiteralLaporan.Text += "<td class='text-right'>" + item2.Key.Stok.ToFormatHargaBulat() + "</td>";
            //        LiteralLaporan.Text += "</tr>";

            //        indexVarian++;
            //    }
            //}
            //#endregion




            //if (StokProduk.Count() > 0)
            //{
            //    int TotalStokNormal = StokProduk.Sum(item => item.Jumlah.Value);

            //    //PENCARIAN STOK MATI
            //    var ResultStokProduk = StokProduk
            //        .Where(item => !TransaksiDetail.Any(id => id == item.IDKombinasiProduk))
            //        .Select(item => new
            //        {
            //            item.TBKombinasiProduk.IDProduk,
            //            item.Jumlah,
            //            Tempat = item.TBTempat.Nama,
            //            item.TBKombinasiProduk.TBAtributProduk.Nama
            //        });

            //    var RingkasanProduk = ResultStokProduk.Select(item => item.IDProduk).Distinct();

            //    //MENCARI PRODUK
            //    List<dynamic> Produk = new List<dynamic>();
            //    Produk.AddRange();

            //    //#region ORDER BY
            //    //string PengaturanTotalQuantity = string.Empty;
            //    //string PengaturanTanggalPembuatan = string.Empty;

            //    //if (DropDownListJenisPengurutan.SelectedValue == "0")
            //    //{
            //    //    int JumlahProduk = Produk.Count();
            //    //    int IndexScore = 0;

            //    //    List<dynamic> temp = new List<dynamic>();

            //    //    foreach (var item in Produk.OrderByDescending(item => item.TotalQuantity))
            //    //    {
            //    //        temp.Add(new
            //    //        {
            //    //            item.Produk,
            //    //            item.IDKategoriProduk,
            //    //            item.Kategori,
            //    //            item.PemilikProduk,
            //    //            item.Warna,
            //    //            item.TanggalDaftar,
            //    //            item.TotalQuantity,
            //    //            item.Stok,
            //    //            item.JumlahStok,
            //    //            Score = JumlahProduk - IndexScore
            //    //        });

            //    //        IndexScore++;
            //    //    }

            //    //    IndexScore = 0;

            //    //    List<dynamic> temp1 = new List<dynamic>();

            //    //    foreach (var item in temp.OrderBy(item => item.TanggalDaftar))
            //    //    {
            //    //        temp1.Add(new
            //    //        {
            //    //            item.Produk,
            //    //            item.IDKategoriProduk,
            //    //            item.Kategori,
            //    //            item.PemilikProduk,
            //    //            item.Warna,
            //    //            item.TanggalDaftar,
            //    //            item.TotalQuantity,
            //    //            item.Stok,
            //    //            item.JumlahStok,
            //    //            Score1 = (JumlahProduk - IndexScore) + item.Score
            //    //        });

            //    //        IndexScore++;
            //    //    }

            //    //    temp = new List<dynamic>();

            //    //    Produk = new List<dynamic>();
            //    //    Produk.AddRange(temp1.OrderByDescending(item => item.Score1).Select(item => new
            //    //    {
            //    //        item.Produk,
            //    //        item.IDKategoriProduk,
            //    //        item.Kategori,
            //    //        item.PemilikProduk,
            //    //        item.Warna,
            //    //        item.TanggalDaftar,
            //    //        item.TotalQuantity,
            //    //        item.Stok,
            //    //        item.JumlahStok
            //    //    }));

            //    //    temp1 = new List<dynamic>();

            //    //    PengaturanTanggalPembuatan = "warning";
            //    //    PengaturanTotalQuantity = "warning";
            //    //}

            //    //if (DropDownListJenisPengurutan.SelectedValue == "1")
            //    //{
            //    //    Produk = Produk.OrderByDescending(item => item.TotalQuantity).ToList();
            //    //    PengaturanTotalQuantity = "warning";
            //    //}

            //    //if (DropDownListJenisPengurutan.SelectedValue == "2")
            //    //{
            //    //    Produk = Produk.OrderBy(item => item.TanggalDaftar).ToList();
            //    //    PengaturanTanggalPembuatan = "warning";
            //    //}
            //    //#endregion

            //    int TotalStokMati = Produk.Sum(item => item.TotalQuantity);


            //}
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