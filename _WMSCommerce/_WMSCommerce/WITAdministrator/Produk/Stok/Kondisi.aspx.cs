using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Stok_ButuhRestok : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Warna_Class ClassWarna = new Warna_Class(db);
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                Tempat_Class ClassTempat = new Tempat_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

                DropDownListWarna.Items.AddRange(ClassWarna.Dropdownlist());
                DropDownListBrand.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                DropDownListKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListVarian.Items.AddRange(ClassAtributProduk.Dropdownlist());

                UpdateMinimumStokProduk(db);
                db.SubmitChanges();
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            IQueryable<TBStokProduk> StokProduk = null;

            if (DropDownListJenisStokProduk.SelectedValue == "0")
                StokProduk = db.TBStokProduks;
            else if (DropDownListJenisStokProduk.SelectedValue == "1") //BATAS AMAN
                StokProduk = db.TBStokProduks.Where(item => item.Jumlah > item.JumlahMinimum * 2);
            else if (DropDownListJenisStokProduk.SelectedValue == "2") //MENDEKATI BATAS MINIMUM
                StokProduk = db.TBStokProduks.Where(item => item.Jumlah > item.JumlahMinimum && item.Jumlah <= item.JumlahMinimum * 2);
            else if (DropDownListJenisStokProduk.SelectedValue == "3") //MENCAPAI BATAS MINIMUM
                StokProduk = db.TBStokProduks.Where(item => item.Jumlah <= item.JumlahMinimum);
            else if (DropDownListJenisStokProduk.SelectedValue == "4") //MENDEKATI DAN MENCAPAI BATAS MINIMUM
                StokProduk = db.TBStokProduks.Where(item => item.Jumlah <= item.JumlahMinimum * 2);
            else if (DropDownListJenisStokProduk.SelectedValue == "5") //BATAS AMAN DAN MENDEKATI BATAS MINIMUM
                StokProduk = db.TBStokProduks.Where(item => item.Jumlah > item.JumlahMinimum);

            #region FILTER
            if (DropDownListTempat.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt());

            if (DropDownListVarian.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == DropDownListVarian.SelectedValue.ToInt());

            if (DropDownListBrand.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == DropDownListBrand.SelectedValue.ToInt());

            if (DropDownListWarna.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDWarna == DropDownListWarna.SelectedValue.ToInt());

            if (DropDownListKategori.SelectedValue != "0")
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 && item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == DropDownListKategori.SelectedValue.ToInt());

            if (!string.IsNullOrWhiteSpace(TextBoxProduk.Text))
                StokProduk = StokProduk.Where(item => item.TBKombinasiProduk.TBProduk.Nama.Contains(TextBoxProduk.Text));
            #endregion

            var StokProdukHabis = StokProduk
                .Select(item => new
                {
                    item.TBKombinasiProduk.IDProduk,
                    Tempat = item.TBTempat.Nama,
                    item.TBKombinasiProduk.TBAtributProduk.Nama,
                    item.Jumlah,
                    item.JumlahMinimum,
                    Alert = (item.Jumlah <= item.JumlahMinimum) ? "text-right danger" : (item.Jumlah <= item.JumlahMinimum * 2) ? "text-right warning" : "text-right success"
                });

            LabelTotalStokProduk.Text = StokProdukHabis.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelTotalStokProduk1.Text = LabelTotalStokProduk.Text;

            var RingkasanProduk = StokProdukHabis.Select(item => item.IDProduk).Distinct();

            var ListProduk = db.TBProduks
                .Where(item => RingkasanProduk.Any(id => id == item.IDProduk))
                .Select(item => new
                {
                    Produk = item.Nama,
                    Kategori = (item.TBRelasiProdukKategoriProduks.Count > 0) ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                    PemilikProduk = item.TBPemilikProduk.Nama,
                    Warna = item.TBWarna.Nama,
                    Stok = StokProdukHabis.Where(item2 => item2.IDProduk == item.IDProduk)
                }).ToArray();

            #region PROGRESS BAR
            LiteralProgressBar.Text = string.Empty;

            var JumlahProduk = StokProdukHabis.Count();

            if (JumlahProduk > 0)
            {
                var HasilProgressBar = StokProdukHabis
                    .GroupBy(item => item.Alert)
                    .Select(item => new
                    {
                        item.Key,
                        Total = item.Count()
                    });

                decimal PersentaseDanger = 0;
                decimal PersentaseWarning = 0;
                decimal PersentaseSuccess = 0;

                //DANGER
                var Result = HasilProgressBar.FirstOrDefault(item => item.Key == "text-right danger");

                if (Result != null)
                {
                    PersentaseDanger = Math.Round(Result.Total / (decimal)JumlahProduk * 100, 2);
                    LiteralProgressBar.Text += "<div class='progress-bar progress-bar-danger' style='width: " + PersentaseDanger + "%'>" + PersentaseDanger + "%</div>";
                }

                //WARNING
                Result = HasilProgressBar.FirstOrDefault(item => item.Key == "text-right warning");

                if (Result != null)
                {
                    PersentaseWarning = Math.Round(Result.Total / (decimal)JumlahProduk * 100, 2);
                    LiteralProgressBar.Text += "<div class='progress-bar progress-bar-warning' style='width: " + PersentaseWarning + "%'>" + PersentaseWarning + "%</div>";
                }

                //SUCCESS
                Result = HasilProgressBar.FirstOrDefault(item => item.Key == "text-right success");

                if (Result != null)
                {
                    PersentaseSuccess = 100 - PersentaseDanger - PersentaseWarning;
                    LiteralProgressBar.Text += "<div class='progress-bar progress-bar-success' style='width: " + PersentaseSuccess + "%'>" + PersentaseSuccess + "%</div>";
                }
            }
            #endregion

            #region USER INTERFACE
            LiteralLaporan.Text = string.Empty;
            int index = 1;
            int indexVarian = 1;

            foreach (var item in ListProduk)
            {
                indexVarian = 1;

                LiteralLaporan.Text += "<tr>";

                string rowspan = item.Stok.Count() >= 2 ? "rowspan='" + item.Stok.Count() + "'" : "";

                LiteralLaporan.Text += "<td " + rowspan + ">" + index++ + "</td>";
                LiteralLaporan.Text += "<td " + rowspan + ">" + item.Produk + "</td>";
                LiteralLaporan.Text += "<td " + rowspan + ">" + item.Warna + "</td>";
                LiteralLaporan.Text += "<td " + rowspan + ">" + item.PemilikProduk + "</td>";
                LiteralLaporan.Text += "<td " + rowspan + ">" + item.Kategori + "</td>";

                foreach (var item2 in item.Stok)
                {
                    if (indexVarian > 1)
                        LiteralLaporan.Text += "<tr>";

                    LiteralLaporan.Text += "<td>" + item2.Tempat + "</td>";
                    LiteralLaporan.Text += "<td>" + item2.Nama + "</td>";
                    LiteralLaporan.Text += "<td class='text-right active'>" + item2.JumlahMinimum.ToFormatHargaBulat() + "</td>";
                    LiteralLaporan.Text += "<td class='" + item2.Alert + "'>" + item2.Jumlah.ToFormatHargaBulat() + "</td>";
                    LiteralLaporan.Text += "</tr>";

                    indexVarian++;
                }
            }
            #endregion
        }
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }

    public void UpdateMinimumStokProduk(DataClassesDatabaseDataContext db)
    {
        StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

        var Konfigurasi = StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.MinimumStok);

        //CARI STOK PRODUK YANG TIDAK MEMILIKI MINIMUM STOK
        var StokProduk = db.TBStokProduks.Where(item => item.JumlahMinimum == 0);

        foreach (var item in StokProduk)
        {
            if (Konfigurasi.Contains("%"))
                item.JumlahMinimum = (int)Math.Ceiling((decimal)item.Jumlah * Konfigurasi.Replace("%", "").ToDecimal() / 100);
            else
                item.JumlahMinimum = Konfigurasi.ToInt();
        }
    }
}