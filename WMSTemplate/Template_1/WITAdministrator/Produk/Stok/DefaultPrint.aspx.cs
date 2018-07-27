using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Stok_DefaultPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext();

            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Tempat_Class ClassTempat = new Tempat_Class(db);
            Store_Class ClassStore = new Store_Class(db);

            TBStore Store = ClassStore.Data();
            TBTempat Tempat = new TBTempat();

            if (!string.IsNullOrWhiteSpace(Request.QueryString["IDTempat"]))
                Tempat = ClassTempat.Cari(Request.QueryString["IDTempat"].ToInt());
            else
                Tempat = ClassTempat.Cari(Pengguna.IDTempat);

            LabelTempatStok.Text = Tempat.Nama;
            LabelNamaStore.Text = Store.Nama + " - " + Tempat.Nama;
            LabelAlamatStore.Text = Tempat.Alamat;
            LabelTeleponStore.Text = Tempat.Telepon1;
            LabelWebsite.Text = Store.Website;
            HyperLinkEmail.Text = Tempat.Email;
            HyperLinkEmail.NavigateUrl = Tempat.Email;

            LabelTanggalPrint.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
            LabelNamaPengguna.Text = Pengguna.NamaLengkap;
            LabelNamaPengguna1.Text = Pengguna.NamaLengkap;
            LabelTempatPengguna.Text = Pengguna.Tempat;

            #region TEMPAT
            var _stokProduk = db.TBStokProduks
                .Where(item =>
                    item.IDTempat == Tempat.IDTempat &&
                    item.TBKombinasiProduk.TBProduk._IsActive);
            #endregion

            #region STATUS STOK
            if (Request.QueryString["IDJenisStok"] == "1")
                _stokProduk = _stokProduk.Where(item => item.Jumlah > 0);
            else if (Request.QueryString["IDJenisStok"] == "2")
                _stokProduk = _stokProduk.Where(item => item.Jumlah == 0);
            else if (Request.QueryString["IDJenisStok"] == "3")
                _stokProduk = _stokProduk.Where(item => item.Jumlah < 0);
            #endregion

            //KODE
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Kode"]))
                _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(Request.QueryString["Kode"]));

            //PRODUK
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Produk"]))
                _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.Nama.Contains(Request.QueryString["Produk"]));

            //WARNA
            if (Request.QueryString["IDWarna"] != "-1" && !string.IsNullOrWhiteSpace(Request.QueryString["IDWarna"]))
                _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDWarna == Request.QueryString["IDWarna"].ToInt());

            #region HARGA JUAL
            if (!string.IsNullOrWhiteSpace(Request.QueryString["HargaJual"]))
            {
                if (Request.QueryString["HargaJual"].Contains("-"))
                {
                    string[] _angka = Request.QueryString["HargaJual"].Split('-');
                    _stokProduk = _stokProduk.Where(item => item.HargaJual >= _angka[0].ToDecimal() && item.HargaJual <= _angka[1].ToDecimal()).OrderBy(item => item.HargaJual);
                }
                else
                    _stokProduk = _stokProduk.Where(item => item.HargaJual == Request.QueryString["HargaJual"].ToDecimal());
            }
            #endregion

            //STOK PRODUK
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Stok"]))
            {
                if (Request.QueryString["Stok"].Contains("-"))
                {
                    string[] _angka = Request.QueryString["Stok"].Split('-');
                    _stokProduk = _stokProduk.Where(item => item.Jumlah >= _angka[0].ToInt() && item.Jumlah <= _angka[1].ToInt()).OrderBy(item => item.Jumlah);
                }
                else
                    _stokProduk = _stokProduk.Where(item => item.Jumlah == Request.QueryString["Stok"].ToInt());
            }

            //PEMILIK PRODUK
            if (Request.QueryString["IDPemilikProduk"] != "-1" && !string.IsNullOrWhiteSpace(Request.QueryString["IDPemilikProduk"]))
                _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == Request.QueryString["IDPemilikProduk"].ToInt());

            //ATRIBUT
            if (Request.QueryString["IDAtribut"] != "-1" && !string.IsNullOrWhiteSpace(Request.QueryString["IDAtribut"]))
                _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == Request.QueryString["IDAtribut"].ToInt());

            //KATEGORI
            if (Request.QueryString["IDKategori"] != "-1" && !string.IsNullOrWhiteSpace(Request.QueryString["IDKategori"]))
            {
                if (Request.QueryString["IDKategori"] == "0")
                    _stokProduk = _stokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count == 0);
                else
                {
                    _stokProduk = _stokProduk.Where(item =>
                        item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 &&
                        item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk == int.Parse(Request.QueryString["IDKategori"]));
                }
            }

            var _dataStok = _stokProduk.Select(item => new StokProduk_Model
            {
                IDProduk = item.TBKombinasiProduk.TBProduk.IDProduk,
                IDKombinasiProduk = item.IDKombinasiProduk,
                Kode = item.TBKombinasiProduk.KodeKombinasiProduk,
                Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
                HargaJual = item.HargaJual.Value,
                Jumlah = item.Jumlah.Value
            });

            if (_dataStok.Count() > 0)
            {
                var _dataProduk = _dataStok.Select(item => item.IDProduk).Distinct();

                var _produk = db.TBProduks
                    .Where(item => _dataProduk.Any(item2 => item2 == item.IDProduk))
                    .Select(item => new
                    {
                        Produk = item.Nama,
                        Kategori = (item.TBRelasiProdukKategoriProduks.Count > 0) ? item.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                        PemilikProduk = item.TBPemilikProduk.Nama,
                        Warna = item.TBWarna.Nama,
                        Stok = _dataStok.Where(item2 => item2.IDProduk == item.IDProduk)
                    }).OrderBy(item => item.Produk).ToList();

                LiteralLaporan.Text = string.Empty;
                int index = 1;
                int indexVarian = 1;

                foreach (var item in _produk)
                {
                    indexVarian = 1;

                    LiteralLaporan.Text += "<tr>";

                    string rowspan = item.Stok.Count() >= 2 ? "rowspan='" + item.Stok.Count() + "'" : "";

                    LiteralLaporan.Text += "<td " + rowspan + " class='fitSize'>" + index++ + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + " class='fitSize'>" + item.Produk + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + " class='fitSize'>" + item.Warna + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + " class='fitSize'>" + item.PemilikProduk + "</td>";
                    LiteralLaporan.Text += "<td " + rowspan + " class='fitSize'>" + item.Kategori + "</td>";

                    foreach (var item2 in item.Stok)
                    {
                        if (indexVarian > 1)
                            LiteralLaporan.Text += "<tr>";

                        LiteralLaporan.Text += "<td class='fitSize'>" + item2.Kode + "</td>";
                        LiteralLaporan.Text += "<td class='fitSize'>" + item2.Atribut + "</td>";
                        LiteralLaporan.Text += "<td class='text-right'>" + item2.HargaJual.ToFormatHarga() + "</td>";
                        LiteralLaporan.Text += "<td class='text-right'>" + item2.Jumlah.ToFormatHargaBulat() + "</td>";
                        LiteralLaporan.Text += "<td class='text-right'>" + item2.Subtotal.ToFormatHarga() + "</td>";

                        LiteralLaporan.Text += "</tr>";

                        indexVarian++;
                    }
                }
            }

            LabelTotalQuantity.Text = _dataStok.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelRataRataHargaJual.Text = _dataStok.Average(item => item.HargaJual).ToFormatHarga();
            LabelGrandTotal.Text = _dataStok.ToList().Sum(item => item.Subtotal).ToFormatHarga();

            LabelTotalQuantity1.Text = LabelTotalQuantity.Text;
            LabelRataRataHargaJual1.Text = LabelRataRataHargaJual.Text;
            LabelGrandTotal1.Text = LabelGrandTotal.Text;
        }
    }
}