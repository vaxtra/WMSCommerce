using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    #region PENCARIAN PRODUK
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] PencarianKombinasiProduk(string prefixText, int count, string contextKey)
    {
        if (!prefixText.Contains("%") && !prefixText.Contains("=") && !prefixText.Contains("$"))
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                return db.Func_PencarianStokProduk(Parse.Int(contextKey), prefixText, count).Select(item => item.KodeKombinasiProduk + " ~ " + item.Nama + " ~ " + item.HargaJual + " ~ " + item.Jumlah).ToArray();
            }
        }
        else
            return null;
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckBoxBiayaTambahan1.Checked = true;
            CheckBoxBiayaTambahan2.Checked = true;

            if (Session["PenggunaLogin"] != null || ViewState["PenggunaLogin"] != null)
            {
                //Session habis, Viewstate ada
                if (Session["PenggunaLogin"] == null && ViewState["PenggunaLogin"] != null)
                {
                    Session["PenggunaLogin"] = ViewState["PenggunaLogin"];
                    Session.Timeout = 525000;

                } //Session ada, Viewstate habis
                else if (Session["PenggunaLogin"] != null && ViewState["PenggunaLogin"] == null)
                    ViewState["PenggunaLogin"] = Session["PenggunaLogin"];

                PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];
                Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    HiddenFieldPageKategori.Value = "0";
                    HiddenFieldPageKategoriAkhir.Value = (Math.Ceiling(((decimal)db.TBKategoriProduks.Where(item => item.IDKategoriProdukParent == null).Select(item => item.IDKategoriProduk).Count() + 1) / 4).ToInt() * 4).ToString();
                    LoadKategori(HiddenFieldPageKategori.Value.ToInt());

                    HiddenFieldPageProduk.Value = "0";
                    HiddenFieldPilihKategori.Value = "0";

                    if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSDaftarProduk))
                        LoadProduk(HiddenFieldPageProduk.Value.ToInt(), HiddenFieldPilihKategori.Value.ToInt());

                    //MENAMPILKAN BIAYA TAMBAHAN UNTUK SETIAP TEMPAT / PAJAK, SERVICE, DLL
                    var Tempat = db.TBTempats.FirstOrDefault(item => item.IDTempat == Pengguna.IDTempat);

                    //AUTO COMPLETE PENCARIAN PRODUK
                    AutoCompleteExtender1.ContextKey = Tempat.IDTempat.ToString();

                    LabelPrintStore.Text = Tempat.TBStore.Nama;
                    LabelPrintTempatNama.Text = Tempat.Nama;
                    LabelTempatAlamat.Text = Tempat.Alamat;
                    LabelTempatTelepon.Text = Tempat.Telepon1;

                    LabelPrintKeteranganBiayaTambahan1.Text = Tempat.KeteranganBiayaTambahan1.ToUpper();
                    LabelKeteranganBiayaTambahan1.Text = Tempat.KeteranganBiayaTambahan1.ToUpper();

                    LabelPrintKeteranganBiayaTambahan2.Text = Tempat.KeteranganBiayaTambahan2.ToUpper();
                    LabelKeteranganBiayaTambahan2.Text = Tempat.KeteranganBiayaTambahan2.ToUpper();

                    if (!string.IsNullOrWhiteSpace(Tempat.FooterPrint))
                    {
                        PanelFooter.Visible = true;
                        PanelFooter1.Visible = true;
                        LabelPrintFooter.Text = Tempat.FooterPrint;
                    }
                    else
                    {
                        PanelFooter.Visible = false;
                        PanelFooter1.Visible = false;
                    }

                    LabelPrintPengguna.Text = Pengguna.NamaLengkap;

                    TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");

                    #region GRUP PELANGGAN
                    DropDownListGrupPelanggan.Items.Clear();

                    var ListGrupPelanggan = db.TBGrupPelanggans.ToArray();

                    DropDownListGrupPelanggan.Items.AddRange(ListGrupPelanggan.Select(item => new ListItem
                    {
                        Value = item.IDGrupPelanggan.ToString(),
                        Text = item.Nama + " - " + Pengaturan.FormatHarga(item.Persentase) + "%"
                    }).ToArray());
                    #endregion

                    #region JENIS TRANSAKSI
                    DropDownListJenisTransaksi.DataSource = db.TBJenisTransaksis.OrderBy(item => item.Nama).ToArray();
                    DropDownListJenisTransaksi.DataValueField = "IDJenisTransaksi";
                    DropDownListJenisTransaksi.DataTextField = "Nama";
                    DropDownListJenisTransaksi.DataBind();
                    #endregion

                    #region TEMPAT PENGIRIM
                    PanelTempatPengirim.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSTempatPengirim);

                    Tempat_Class ClassTempat = new Tempat_Class(db);

                    DropDownListTempatPengirim.DataSource = ClassTempat.Data();
                    DropDownListTempatPengirim.DataValueField = "IDTempat";
                    DropDownListTempatPengirim.DataTextField = "Nama";
                    DropDownListTempatPengirim.DataBind();
                    DropDownListTempatPengirim.SelectedValue = Tempat.IDTempat.ToString();
                    #endregion

                    #region KURIR
                    DropDownListKurir.DataSource = db.TBKurirs.ToArray();
                    DropDownListKurir.DataValueField = "IDKurir";
                    DropDownListKurir.DataTextField = "Nama";
                    DropDownListKurir.DataBind();
                    #endregion

                    #region JENIS PEMBAYARAN
                    var ListJenisPembayaran = db.TBJenisPembayarans.ToArray();

                    RepeaterJenisPembayaran.DataSource = ListJenisPembayaran.Where(item => item.IDJenisPembayaran > 2);
                    RepeaterJenisPembayaran.DataBind();

                    RepeaterSplitPaymentJenisPembayaran.DataSource = ListJenisPembayaran.Where(item => item.IDJenisPembayaran != 2);
                    RepeaterSplitPaymentJenisPembayaran.DataBind();
                    #endregion

                    #region TEMPLATE KETERANGAN
                    RepeaterTemplateKeterangan.DataSource = db.TBTemplateKeterangans.ToArray();
                    RepeaterTemplateKeterangan.DataBind();
                    #endregion

                    Transaksi_Class Transaksi;
                    string idTransaksi = string.Empty;

                    if (!string.IsNullOrWhiteSpace(Request.QueryString["table"]) && Parse.Int(Request.QueryString["table"]) > 2)
                    {
                        //MENGIRIM PARAMETER MEJA MAKA MENCARI ID TRANSAKSI : RESTAURANT
                        var tempTransaksi = db.TBTransaksis
                            .FirstOrDefault(item =>
                                item.IDMeja == Parse.Int(Request.QueryString["table"]) &&
                                item.IDStatusTransaksi == (int)EnumStatusTransaksi.AwaitingPayment);

                        if (tempTransaksi != null)
                            idTransaksi = tempTransaksi.IDTransaksi;
                    }
                    else if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                        idTransaksi = Request.QueryString["id"]; //JIKA PARAMETER ID TRANSAKSI

                    if (!string.IsNullOrWhiteSpace(idTransaksi))
                    {
                        //JIKA QUERY STRING ADA LOAD TRANSAKSI
                        Transaksi = new Transaksi_Class(idTransaksi, Pengguna.IDPengguna);

                        if (Transaksi.IDStatusTransaksi != 0)
                        {
                            if (Transaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete || Transaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                            {
                                string IDTransaksi = Transaksi.IDTransaksi;
                                var Detail = Transaksi.Detail;
                                int IDPelanggan = Transaksi.Pelanggan.IDPelanggan;
                                string Keterangan = Transaksi.Keterangan;
                                int IDJenisTransaksi = Transaksi.IDJenisTransaksi;

                                if (Transaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete && Request.QueryString["do"] == "Retur")
                                {
                                    //JIKA STATUS COMPLETE DAN DO RETUR MAKA MELAKUKAN RETUR TRANSAKSI
                                    //MEMBUAT TRANSAKSI BARU
                                    Transaksi = TransaksiBaru(Pengguna);

                                    //MEMASUKKAN ULANG DETAIL
                                    Transaksi.Detail.AddRange(Detail
                                        .Where(item => item.Quantity > 0)
                                        .Select(item => new TransaksiRetailDetail_Model
                                        {
                                            IDDetailTransaksi = Transaksi.IDDetailTransaksiTemp,
                                            IDKombinasiProduk = item.IDKombinasiProduk,
                                            IDStokProduk = item.IDStokProduk,
                                            Barcode = item.Barcode,
                                            Nama = item.Nama,
                                            Quantity = item.Quantity * -1,
                                            Berat = item.Berat,
                                            HargaBeliKotor = item.HargaBeliKotor,
                                            HargaJual = item.HargaJual,
                                            DiscountStore = item.DiscountStore,
                                            DiscountKonsinyasi = item.DiscountKonsinyasi,
                                            Keterangan = item.Keterangan,
                                            UbahQuantity = item.UbahQuantity
                                        }));

                                    Transaksi.Keterangan = "Retur #" + IDTransaksi;
                                    Transaksi.isRetur = true;
                                }
                                else
                                {
                                    decimal BiayaPengiriman = Transaksi.BiayaPengiriman;

                                    //JIKA STATUS COMPLETE ATAU CANCELED BERARTI SEDANG MEMBUAT TRANSAKSI DARI REFERENSI TRANSAKSI
                                    //MEMBUAT TRANSAKSI BARU
                                    Transaksi = TransaksiBaru(Pengguna);

                                    //MEMASUKKAN ULANG DETAIL
                                    Transaksi.Detail.AddRange(Detail
                                        .Select(item => new TransaksiRetailDetail_Model
                                        {
                                            IDDetailTransaksi = Transaksi.IDDetailTransaksiTemp,
                                            IDKombinasiProduk = item.IDKombinasiProduk,
                                            IDStokProduk = item.IDStokProduk,
                                            Barcode = item.Barcode,
                                            Nama = item.Nama,
                                            Quantity = item.Quantity,
                                            Berat = item.Berat,
                                            HargaBeliKotor = item.HargaBeliKotor,
                                            HargaJual = item.HargaJual,
                                            DiscountStore = item.DiscountStore,
                                            DiscountKonsinyasi = item.DiscountKonsinyasi,
                                            Keterangan = item.Keterangan,
                                            UbahQuantity = item.UbahQuantity
                                        }));

                                    Transaksi.Keterangan = "Referensi Transaksi #" + IDTransaksi;

                                    //PENGATURAN BIAYA PENGIRIMAN
                                    Transaksi.BiayaPengiriman = BiayaPengiriman;
                                }

                                //JIKA MEMBUAT SEBAGAI TRANSAKSI BARU MAKA MEJA DEFAULT 2 : TAKE AWAY
                                Transaksi.Meja.IDMeja = 2;

                                //PENGATURAN PELANGGAN
                                Transaksi.PengaturanPelanggan(IDPelanggan);

                                //PENGATURAN KETERANGAN
                                Transaksi.Keterangan += " " + Keterangan;

                                //PENGATURAN JENIS TRANSAKSI
                                Transaksi.IDJenisTransaksi = IDJenisTransaksi;
                            }
                        }
                        else
                            Transaksi = TransaksiBaru(Pengguna);  //JIKA TIDAK DITEMUKAN MAKA MEMBUAT TRANSAKSI BARU
                    }
                    else if (Session["Transaksi"] != null && Pengguna.PointOfSales == TipePointOfSales.Retail) //JIKA SESSION SEBELUMNYA DITEMUKAN DAN BUKA RESTAURANT
                        Transaksi = (Transaksi_Class)Session["Transaksi"];
                    else
                        Transaksi = TransaksiBaru(Pengguna); //JIKA TIDAK DITEMUKAN MAKA MEMBUAT TRANSAKSI BARU

                    ViewState["Transaksi"] = Transaksi;
                    LoadTransaksi();
                }

                #region KONFIGURASI PENGGUNA
                ButtonDiscount.Enabled = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSDiscount);
                ButtonDiscount.Visible = ButtonDiscount.Enabled;

                ButtonTunai.Enabled = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSPembayaran);
                ButtonTunai.Visible = ButtonTunai.Enabled;
                ButtonSplitPaymentBayar.Visible = ButtonTunai.Enabled;

                ButtonConfirmPayment.Enabled = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSPaymentVerification);
                ButtonConfirmPayment.Visible = ButtonConfirmPayment.Enabled;
                #endregion
            }
            else
                Response.Redirect("/WITAdministrator/Login.aspx?returnUrl=/WITPointOfSales/");
        }
    }
    private Transaksi_Class TransaksiBaru(PenggunaLogin Pengguna)
    {
        Transaksi_Class Transaksi = new Transaksi_Class(Pengguna.IDPengguna, Pengguna.IDTempat, Parse.dateTime(TextBoxTanggal.Text));

        //JIKA MENGIRIM PARAMETER MEJA TRANSAKSI
        if (!string.IsNullOrWhiteSpace(Request.QueryString["table"]))
            Transaksi.Meja.IDMeja = Parse.Int(Request.QueryString["table"]);

        return Transaksi;
    }

    #region PENCARIAN PRODUK BUTTON
    private void LoadKategori(int skip)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<TBKategoriProduk> ListKategoriProduk = db.TBKategoriProduks.Where(item => item.IDKategoriProdukParent == null).OrderBy(item => item.Nama).ToList();
            ListKategoriProduk.Insert(0, new TBKategoriProduk { IDKategoriProduk = 0, Nama = "Semua Produk" });

            for (int i = 0; i < ListKategoriProduk.Count % 4; i++)
            {
                ListKategoriProduk.Add(new TBKategoriProduk { IDKategoriProduk = -1, Nama = string.Empty });
            }

            RepeaterKategoriProduk.DataSource = ListKategoriProduk.Skip(skip).Take(4).ToArray();
            RepeaterKategoriProduk.DataBind();
        }
    }

    protected void ButtonKategoriKiri_Click(object sender, EventArgs e)
    {
        if (HiddenFieldPageKategori.Value != "0")
        {
            HiddenFieldPageKategori.Value = (HiddenFieldPageKategori.Value.ToInt() - 4).ToString();
            LoadKategori(HiddenFieldPageKategori.Value.ToInt());
        }
    }

    protected void ButtonKategoriKanan_Click(object sender, EventArgs e)
    {
        if (HiddenFieldPageKategori.Value.ToInt() + 4 < HiddenFieldPageKategoriAkhir.Value.ToInt())
        {
            HiddenFieldPageKategori.Value = (HiddenFieldPageKategori.Value.ToInt() + 4).ToString();
            LoadKategori(HiddenFieldPageKategori.Value.ToInt());
        }
    }

    private void LoadProduk(int skip, int IDKategori)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<TBProduk> ListProduk = db.TBStokProduks
                .Where(item => item.TBKombinasiProduk.TBProduk._IsActive && item.IDTempat == Pengguna.IDTempat &&
                               (IDKategori != 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(item2 => item2.IDKategoriProduk == IDKategori) != null : true) &&
                               (!string.IsNullOrEmpty(TextBoxPencarianProduk.Text) ? item.TBKombinasiProduk.TBProduk.Nama.ToLower().Contains(TextBoxPencarianProduk.Text) : true))
                .Select(item => item.TBKombinasiProduk.TBProduk).Distinct().OrderBy(item => item.Nama).ToList();

            HiddenFieldPageProdukAkhir.Value = (Math.Ceiling((decimal)ListProduk.Count() / 12).ToInt() * 12).ToString();

            for (int i = 0; i < ListProduk.Count % 12; i++)
            {
                ListProduk.Add(new TBProduk { IDProduk = -1, Nama = string.Empty });
            }

            if (ListProduk.Count() > 0)
            {
                int[] resultReguler = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    resultReguler[i] = skip + i;
                }

                RepeaterProduk.DataSource = resultReguler.Select(item => new
                {
                    baris = ListProduk.Skip(item * 4).Take(4)
                });
                RepeaterProduk.DataBind();
            }
            else
            {
                RepeaterProduk.DataSource = null;
                RepeaterProduk.DataBind();
            }
        }
    }

    protected void ButtonProdukKiri_Click(object sender, EventArgs e)
    {
        if (HiddenFieldPageProduk.Value != "0")
        {
            HiddenFieldPageProduk.Value = (HiddenFieldPageProduk.Value.ToInt() - 3).ToString();
            LoadProduk(HiddenFieldPageProduk.Value.ToInt(), HiddenFieldPilihKategori.Value.ToInt());
        }
    }

    protected void ButtonProdukKanan_Click(object sender, EventArgs e)
    {
        if ((HiddenFieldPageProduk.Value.ToInt() + 3) * 4 < HiddenFieldPageProdukAkhir.Value.ToInt())
        {
            HiddenFieldPageProduk.Value = (HiddenFieldPageProduk.Value.ToInt() + 3).ToString();
            LoadProduk(HiddenFieldPageProduk.Value.ToInt(), HiddenFieldPilihKategori.Value.ToInt());
        }
    }
    #endregion

    protected void ButtonProdukManual_Click(object sender, EventArgs e)
    {
        //INPUT KODE PRODUK MANUAL
        LiteralWarningInputProdukManual.Text = string.Empty;
        TextBoxInputProdukManual.Text = string.Empty;
        panelKombinasiProduk.Visible = false;

        TextBoxInputProdukManual.Focus();
        ModalPopupExtenderProdukManual.Show();
    }
    protected void ButtonConfirmManualProduk_Click(object sender, EventArgs e)
    {
        string _inputProdukManual = TextBoxInputProdukManual.Text.Replace(".", "").Replace(",", ".");

        if (_inputProdukManual.StartsWith("=")) //Merubah Produk
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.UbahJumlahProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Int(_inputProdukManual.Replace("=", "")));
                LoadTransaksi();
            }
        }
        else if (_inputProdukManual.StartsWith("+") || _inputProdukManual.StartsWith("-")) //Menambah dan Mengurangi Produk
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.TambahKurangJumlahProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Int(_inputProdukManual));
                LoadTransaksi();
            }
        }
        else if (_inputProdukManual.StartsWith("%")) //Diskon Nominal
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.UbahPotonganHargaJualProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), _inputProdukManual.Replace("%", ""));
                LoadTransaksi();
            }
        }
        else if (_inputProdukManual.EndsWith("%")) //Diskon Persentase
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.UbahPotonganHargaJualProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), _inputProdukManual);
                LoadTransaksi();
            }
        }
        else if (_inputProdukManual.StartsWith("$$")) //Subtotal
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.UbahHargaSubtotalProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Decimal(_inputProdukManual.Replace("$$", "")));
                LoadTransaksi();
            }
        }
        else if (_inputProdukManual.StartsWith("$")) //Harga Produk
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.UbahHargaJualTampilProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Decimal(_inputProdukManual.Replace("$", "")));
                LoadTransaksi();
            }
        }
        else if (_inputProdukManual.StartsWith("#"))
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.Detail.FirstOrDefault(item => item.IDDetailTransaksi == int.Parse(HiddenFieldIDDetailTransaksi.Value)).Keterangan = TextBoxInputProdukManual.Text.Substring(1);
                LoadTransaksi();
            }
        }
        else
        {
            LiteralWarningInputProdukManual.Text = string.Empty;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                //MENCARI BERDASARKAN KODE PRODUK
                var ListKombinasiProduk = db.TBKombinasiProduks
                    .Where(item =>
                        item.KodeKombinasiProduk == TextBoxInputProdukManual.Text &&
                        item.TBProduk._IsActive)
                    .ToArray();

                if (ListKombinasiProduk.Count() > 1)
                {
                    //JIKA KOMBINASI PRODUK YANG MEMILIKI KODE SAMA LEBIH DARI 1
                    RepeaterKombinasiProduk.DataSource = ListKombinasiProduk;
                    RepeaterKombinasiProduk.DataBind();

                    panelKombinasiProduk.Visible = true;
                }
                else
                {
                    if (ListKombinasiProduk.Count() == 1)
                    {
                        var KombinasiProduk = ListKombinasiProduk.FirstOrDefault();

                        //MEMASUKKAN DETAIL TRANSAKSI
                        int IDDetailTransaksi = TambahDetailTransaksi(KombinasiProduk.IDKombinasiProduk, 1);

                        if (IDDetailTransaksi > 0)
                        {
                            LiteralWarningInputProdukManual.Text = Alert_Class.Pesan(TipeAlert.Success, KombinasiProduk.Nama);
                            HiddenFieldIDDetailTransaksi.Value = IDDetailTransaksi.ToString();
                        }
                        else
                            LiteralWarningInputProdukManual.Text = Alert_Class.Pesan(TipeAlert.Danger, "Produk tidak ditemukan");
                    }
                    else //KOMBINASI PRODUK TIDAK DITEMUKAN
                        LiteralWarningInputProdukManual.Text = Alert_Class.Pesan(TipeAlert.Danger, "Produk tidak ditemukan");

                    panelKombinasiProduk.Visible = false;
                }
            }
        }

        TextBoxInputProdukManual.Text = string.Empty;
        TextBoxInputProdukManual.Focus();

        ModalPopupExtenderProdukManual.Show();
    }
    private int TambahDetailTransaksi(int idKombinasiProduk, int jumlahProduk)
    {
        //MENAMBAH DETAIL TRANSAKSI BARU
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        //MENAMBAH DETAIL TRANSAKSI
        int IDDetailTransaksi = Transaksi.TambahDetailTransaksi(idKombinasiProduk, jumlahProduk);
        LoadTransaksi();

        return IDDetailTransaksi;
    }
    private void LoadTransaksi()
    {
        //LOAD SEMUA DATA TRANSAKSI
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        if (Transaksi != null)
        {
            //MENGHITUNG BANYAK DETAIL DETAIL
            int JumlahDetail = Transaksi.Detail.Count;

            //MENGISI REPEATER DETAIL TRANSAKSI
            RepeaterDetailTransaksi.DataSource = Transaksi.Detail.Select(item => new
            {
                item.IDDetailTransaksi,
                item.IDKombinasiProduk,
                item.IDStokProduk,
                item.Nama,
                item.HargaJual,
                HargaJualTampil = item.HargaJualBersih,
                PotonganHargaJual = item.Discount,
                JumlahProduk = item.Quantity,
                item.Subtotal,
                JumlahDetail = JumlahDetail, //EFEK WARNA BIRU PADA DETAIL YANG BARU
                item.UbahQuantity,
                IsDiscount = ButtonDiscount.Enabled
            });
            RepeaterDetailTransaksi.DataBind();

            LabelQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
            LabelPelangganNama.Text = Transaksi.Pelanggan.Nama;

            //DisplaySubtotal

            LabelSubtotal.Text = Pengaturan.FormatHarga(Transaksi.Detail.Sum(item => item.TotalHargaJual));
            LabelBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);

            //DisplayDiscountTransaksi

            LabelDiscountTransaksi.Text = Pengaturan.FormatHarga(Transaksi.TotalPotonganHargaJualDetail);

            //LabelKeteranganBiayaTambahan1
            //DisplayBiayaTambahan1
            //CheckBoxBiayaTambahan1

            LabelBiayaTambahan1.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan1);

            //LabelKeteranganBiayaTambahan2
            //DisplayBiayaTambahan2
            //CheckBoxBiayaTambahan2

            LabelBiayaTambahan2.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan2);

            DisplayPembulatan.Visible = Transaksi.Pembulatan != 0;

            LabelPembulatan.Text = Pengaturan.FormatHarga(Transaksi.Pembulatan);

            //DisplayGrandTotal

            LabelGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);

            if (Parse.dateTime(TextBoxTanggal.Text).Date == DateTime.Now.Date)
            {
                //JIKA MASIH PADA TANGGAL YANG SAMA KETIKA REFRESH DETAIL LOAD ULANG TANGGAL DAN JAM
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
                PengaturanTanggal();
            }

            LabelGrandTotal1.Text = LabelGrandTotal.Text;
            LabelGrandTotal2.Text = LabelGrandTotal.Text;

            TextBoxKeterangan.Text = Transaksi.Keterangan;

            DropDownListTempatPengirim.SelectedValue = Transaksi.IDTempatPengirim.ToString();
            DropDownListKurir.SelectedValue = Transaksi.IDKurir.ToString();

            if (Transaksi.Detail.Count > 0 || !string.IsNullOrWhiteSpace(Transaksi.IDTransaksi)) //JIKA DETAIL TRANSAKSI 0 MAKA PANEL KETERANGAN DI HIDDEN
                PanelPembayaran1.Visible = true;
            else
                PanelPembayaran1.Visible = false;

            Session["Transaksi"] = ViewState["Transaksi"];
        }
    }

    #region TANGGAL
    protected void TextBoxTanggal_TextChanged(object sender, EventArgs e)
    {
        PengaturanTanggal();
    }
    protected void ButtonResetTanggal_Click(object sender, EventArgs e)
    {
        TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");

        PengaturanTanggal();
    }
    private void PengaturanTanggal()
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (Transaksi != null)
            Transaksi.TanggalTransaksi = Parse.dateTime(TextBoxTanggal.Text);
    }
    #endregion

    protected void ButtonBatalBayar_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
            Response.Redirect("/WITRestaurantV2/");
        else
            ResetTransaksi();
    }
    private void ResetTransaksi()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        //MEMBUAT TRANSAKSI BARU
        Transaksi_Class Transaksi = TransaksiBaru(Pengguna);

        ViewState["Transaksi"] = Transaksi;
        Session["Transaksi"] = ViewState["Transaksi"];

        TextBoxPembayaranLainnya.Text = "";
        LabelJumlahKembalian.Text = "";
        PanelKembalian.Visible = false;
        PanelPembayaran.Visible = false;

        LoadTransaksi();
    }

    #region HELPER
    private void PengaturanHelper(string header, string textboxText, string hiddenField)
    {
        HiddenFieldIDHelper.Value = hiddenField;
        LabelHeaderHelper.Text = header;
        TextBoxHelper.Text = textboxText;
        ModalPopupExtenderHelper.Show();
        TextBoxHelper.Focus();
    }
    protected void RepeaterDetailTransaksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label LabelIDDetailTransaksi = (Label)e.Item.FindControl("LabelIDDetailTransaksi");

        if (e.CommandName == "EditJumlah")
            PengaturanHelper("Jumlah Produk", e.CommandArgument.ToString(), LabelIDDetailTransaksi.Text);
        else if (e.CommandName == "EditSubtotal")
            PengaturanHelper("Subtotal", e.CommandArgument.ToString(), LabelIDDetailTransaksi.Text);
        else if (e.CommandName == "EditHarga")
            PengaturanHelper("Harga Produk", e.CommandArgument.ToString(), LabelIDDetailTransaksi.Text);
        else if (e.CommandName == "Hapus")
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                Transaksi.UbahJumlahProduk(Parse.Int(LabelIDDetailTransaksi.Text), 0);
                LoadTransaksi();
            }
        }
        else if (e.CommandName == "Deskripsi")
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                var TransaksiDetail = Transaksi.Detail
                    .FirstOrDefault(item => item.IDDetailTransaksi == Parse.Int(LabelIDDetailTransaksi.Text));

                HiddenFieldIDKombinasiProdukDeskripsi.Value = TransaksiDetail.IDDetailTransaksi.ToString();
                TextBoxKeteranganTransaksiDetail.Text = TransaksiDetail.Keterangan;

                ModalPopupExtenderDeskripsiProduk.Show();
                TextBoxKeteranganTransaksiDetail.Focus();
            }
        }
        else if (e.CommandName == "PotonganHarga")
            PengaturanHelper("Discount Produk", e.CommandArgument.ToString(), LabelIDDetailTransaksi.Text);
    }
    protected void ButtonDiscount_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (Transaksi != null)
            PengaturanHelper("Discount", Pengaturan.FormatHarga(Transaksi.PotonganTransaksi.ToString()), "");
    }
    protected void ButtonConfirmHelper_Click(object sender, EventArgs e)
    {
        int _TextBoxHelper;
        int.TryParse(TextBoxHelper.Text.Replace("%", "").Replace(".", "").Replace(",", "."), out _TextBoxHelper);

        //membedakan dia sedang memasukkan angka tapi salah input atau memang dia sedang merubah jadi 0
        if ((_TextBoxHelper <= 0 && TextBoxHelper.Text == "0") || _TextBoxHelper > 0 || TextBoxHelper.Text.Contains("%") || LabelHeaderHelper.Text == "Jumlah Produk")
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                if (LabelHeaderHelper.Text == "Discount")
                {
                    decimal _potonganTransaksi;
                    decimal.TryParse(TextBoxHelper.Text.Replace("%", "").Replace(".", "").Replace(",", "."), out _potonganTransaksi);

                    //Jika input mengandung % maka Potongan berupa persentase
                    if (TextBoxHelper.Text.Contains('%'))
                    {
                        //Maksimal diskon 100%
                        if (_potonganTransaksi <= 100)
                        {
                            foreach (var item in Transaksi.Detail)
                            {
                                Transaksi.UbahPotonganHargaJualProduk(item.IDDetailTransaksi, TextBoxHelper.Text);
                            }
                        }
                        else
                        {
                            TextBoxHelper.Focus();
                            ModalPopupExtenderHelper.Show();
                        }
                    }
                    else //Jika input tidak mengandung persentase maka potongan berupa Harga
                    {
                        //transaksi.PotonganTransaksi = _potonganTransaksi;
                        //transaksi.EnumPotonganTransaksi = PilihanPotonganTransaksi.Harga;

                        #region COMMENT INI KALAU HITUNGAN DISKON MAU DIBAGI DARI URUTAN SUBTOTAL PALING BESAR
                        //int counter = Transaksi.Detail.Sum(item => item.Quantity);

                        //foreach (var item in Transaksi.Detail.OrderBy(item2 => item2.HargaJual))
                        //{
                        //    decimal _tempPotonganSatuan = _potonganTransaksi / counter;

                        //    Transaksi.UbahPotonganHargaJualProduk(item.IDDetailTransaksi, Pengaturan.FormatHarga(_tempPotonganSatuan));

                        //    if (_tempPotonganSatuan > item.HargaJual)
                        //        _tempPotonganSatuan = item.HargaJual;

                        //}
                        #endregion

                        #region COMMENT INI KALAU HITUNGAN DISKON MAU DIBAGI RATA KESEMUA ITEM
                        foreach (var item in Transaksi.Detail.OrderByDescending(item2 => item2.Subtotal))
                        {
                            decimal _tempPotonganSatuan = _potonganTransaksi / item.Quantity;

                            Transaksi.UbahPotonganHargaJualProduk(item.IDDetailTransaksi, Pengaturan.FormatHarga(_tempPotonganSatuan));

                            if (_tempPotonganSatuan > item.HargaJual)
                                _tempPotonganSatuan = item.HargaJual;

                            _potonganTransaksi -= _tempPotonganSatuan * item.Quantity;

                        }
                        #endregion
                    }
                }

                else if (LabelHeaderHelper.Text == "Biaya Pengiriman")
                    Transaksi.BiayaPengiriman = decimal.Parse(TextBoxHelper.Text.Replace("%", "").Replace(".", "").Replace(",", "."));

                else if (LabelHeaderHelper.Text == "Jumlah Produk")
                {
                    int _jumlah = (int)Pengaturan.FormatAngkaInput(TextBoxHelper.Text.Replace("%", ""));

                    if (_jumlah > 0 || _jumlah < 0 || (_jumlah == 0 && TextBoxHelper.Text == "0"))
                        Transaksi.UbahJumlahProduk(Parse.Int(HiddenFieldIDHelper.Value), Parse.Int(TextBoxHelper.Text.Replace("%", "").Replace(".", "").Replace(",", ".")));
                }

                else if (LabelHeaderHelper.Text == "Subtotal")
                    Transaksi.UbahHargaSubtotalProduk(Parse.Int(HiddenFieldIDHelper.Value), Parse.Decimal(TextBoxHelper.Text.Replace("%", "").Replace(".", "").Replace(",", ".")));

                else if (LabelHeaderHelper.Text == "Harga Produk")
                    Transaksi.UbahHargaJualTampilProduk(Parse.Int(HiddenFieldIDHelper.Value), Parse.Decimal(TextBoxHelper.Text.Replace("%", "").Replace(".", "").Replace(",", ".")));

                else if (LabelHeaderHelper.Text == "Discount Produk")
                    Transaksi.UbahPotonganHargaJualProduk(Parse.Int(HiddenFieldIDHelper.Value), TextBoxHelper.Text);

                else if (LabelHeaderHelper.Text == "Jumlah Tamu")
                {
                    if (Parse.Int(TextBoxHelper.Text) <= 0)
                        Transaksi.JumlahTamu = 1;
                    else
                        Transaksi.JumlahTamu = Parse.Int(TextBoxHelper.Text);

                    //ORDER TRANSAKSI
                    TextBoxKeterangan.Text = Transaksi.Keterangan;
                    ButtonSimpanKeterangan.Text = "Order";
                    TextBoxKeterangan.Focus();
                    ModalPopupExtenderKeterangan.Show();
                }

                LoadTransaksi();
            }
        }
        else
        {
            TextBoxHelper.Focus();
            ModalPopupExtenderHelper.Show();
        }
    }
    #endregion

    protected void ButtonCariProduk_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextBoxPencarianProduk.Text))
        {
            if (TextBoxPencarianProduk.Text.StartsWith("=")) //MERUBAH QUANTITY PRODUK
            {
                Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

                if (Transaksi != null)
                {
                    Transaksi.UbahJumlahProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Int(TextBoxPencarianProduk.Text.Replace("=", "")));
                    LoadTransaksi();
                    TextBoxPencarianProduk.Text = string.Empty;
                    TextBoxPencarianProduk.Focus();
                }
            }
            else if (TextBoxPencarianProduk.Text.StartsWith("%")) //Diskon Nominal
            {
                Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

                if (Transaksi != null)
                {
                    Transaksi.UbahPotonganHargaJualProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), TextBoxPencarianProduk.Text.Replace("%", ""));
                    LoadTransaksi();
                    TextBoxPencarianProduk.Text = string.Empty;
                    TextBoxPencarianProduk.Focus();
                }
            }
            else if (TextBoxPencarianProduk.Text.EndsWith("%")) //Diskon Persentase
            {
                Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

                if (Transaksi != null)
                {
                    Transaksi.UbahPotonganHargaJualProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), TextBoxPencarianProduk.Text);
                    LoadTransaksi();
                    TextBoxPencarianProduk.Text = string.Empty;
                    TextBoxPencarianProduk.Focus();
                }
            }
            else if (TextBoxPencarianProduk.Text.StartsWith("$$")) //Subtotal
            {
                Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

                if (Transaksi != null)
                {
                    Transaksi.UbahHargaSubtotalProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Decimal(TextBoxPencarianProduk.Text.Replace("$$", "")));
                    LoadTransaksi();
                    TextBoxPencarianProduk.Text = string.Empty;
                    TextBoxPencarianProduk.Focus();
                }
            }
            else if (TextBoxPencarianProduk.Text.StartsWith("$")) //Harga Produk
            {
                Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

                if (Transaksi != null)
                {
                    Transaksi.UbahHargaJualTampilProduk(Parse.Int(HiddenFieldIDDetailTransaksi.Value), Parse.Decimal(TextBoxPencarianProduk.Text.Replace("$", "")));
                    LoadTransaksi();
                    TextBoxPencarianProduk.Text = string.Empty;
                    TextBoxPencarianProduk.Focus();
                }
            }
            else
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    if (TextBoxPencarianProduk.Text.Contains(" ~ "))
                    {
                        string[] _tempPencarian = TextBoxPencarianProduk.Text.Replace(" ~ ", "~").Split('~');

                        var KombinasiProduk = db.TBKombinasiProduks
                            .FirstOrDefault(item =>
                                item.KodeKombinasiProduk == _tempPencarian[0] &&
                                item.Nama == _tempPencarian[1]);

                        if (KombinasiProduk != null)
                        {
                            HiddenFieldIDDetailTransaksi.Value = TambahDetailTransaksi(KombinasiProduk.IDKombinasiProduk, 1).ToString();
                            TextBoxPencarianProduk.Text = string.Empty;
                            TextBoxPencarianProduk.Focus();
                        }
                    }
                    else
                    {
                        HiddenFieldPageProduk.Value = "0";
                        LoadProduk(HiddenFieldPageProduk.Value.ToInt(), HiddenFieldPilihKategori.Value.ToInt());
                    }
                }
            }
        }
        else
        {
            HiddenFieldPageProduk.Value = "0";
            LoadProduk(HiddenFieldPageProduk.Value.ToInt(), HiddenFieldPilihKategori.Value.ToInt());
        }
    }

    #region PELANGGAN
    #region PENCARIAN WILAYAH
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] PencarianWilayah(string prefixText, int count)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return db.TBWilayahs
                .Where(item =>
                    item.IDGrupWilayah == 4 &&
                    item.Nama.Contains(prefixText))
                .Select(item => item.IDWilayah + " ~ " + (item.TBWilayah1.TBWilayah1.Nama) + " - " + (item.IDWilayahParent.HasValue ? item.TBWilayah1.Nama : "") + " - " + item.Nama)
                .OrderBy(item => item)
                .ToArray();
        }
    }
    #endregion

    protected void ButtonPelanggan_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        DropDownListJenisTransaksi.SelectedValue = Transaksi.IDJenisTransaksi.ToString();
        MultiViewPelanggan.ActiveViewIndex = 0;
        TextBoxPencarianPelanggan.Text = string.Empty;

        TextBoxPencarianPelanggan.Focus();
        ModalPopupExtenderPelanggan.Show();
    }
    protected void ButtonPencarianPelanggan_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextBoxPencarianPelanggan.Text))
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                var ListPelanggan = db.TBAlamats
                    .Where(item =>
                        item.NamaLengkap.Contains(TextBoxPencarianPelanggan.Text) ||
                        item.Handphone.Contains(TextBoxPencarianPelanggan.Text))
                    .Select(item => new
                    {
                        GrupPelanggan = item.TBPelanggan.TBGrupPelanggan.Nama,
                        item.IDPelanggan,
                        item.NamaLengkap,
                        item.Handphone,
                        AlamatLengkap = item.AlamatLengkap.ToString() + (item.IDNegara.HasValue ? ", " + (item.TBWilayah.IDWilayahParent.HasValue ? item.TBWilayah.Nama + ", " + item.TBWilayah.TBWilayah1.Nama : item.TBWilayah.Nama) : "") + ", " + item.KodePos,
                        Deposit = ClassPelanggan.Deposit(item.IDPelanggan.Value)
                    });

                RepeaterDataPelanggan.DataSource = ListPelanggan;
                RepeaterDataPelanggan.DataBind();
            }
        }

        TextBoxPencarianPelanggan.Focus();
        ModalPopupExtenderPelanggan.Show();
    }
    private void PengaturanPelanggan(int idPelanggan)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (Transaksi != null)
        {
            Transaksi.PengaturanPelanggan(idPelanggan);

            RepeaterDataPelanggan.DataSource = null;
            RepeaterDataPelanggan.DataBind();

            LoadTransaksi();
        }
    }
    protected void RepeaterDataPelanggan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
            PengaturanPelanggan(Parse.Int(e.CommandArgument.ToString()));
        else if (e.CommandName == "Ubah")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                //MENGUBAH DATA PELANGGAN
                var Pelanggan = db.TBPelanggans.FirstOrDefault(item => item.IDPelanggan == Parse.Int(e.CommandArgument.ToString()));

                if (Pelanggan != null)
                {
                    DropDownListGrupPelanggan.SelectedValue = Pelanggan.IDGrupPelanggan.ToString();
                    HiddenFieldIDPelanggan.Value = Pelanggan.IDPelanggan.ToString();
                    TextBoxPelangganEmail.Text = Pelanggan.Email;

                    var Alamat = Pelanggan.TBAlamats.FirstOrDefault();

                    if (Alamat != null)
                    {
                        TextBoxNamaLengkap.Text = Alamat.NamaLengkap;
                        TextBoxNomorTelepon.Text = Alamat.Handphone;
                        TextBoxAlamat.Text = Alamat.AlamatLengkap;
                        TextBoxKodePos.Text = Alamat.KodePos;

                        if (Alamat.IDNegara.HasValue)
                            TextBoxWilayah.Text = Alamat.IDNegara + " ~ " + (Alamat.TBWilayah.IDWilayahParent.HasValue ? Alamat.TBWilayah.TBWilayah1.Nama : "") + " " + Alamat.TBWilayah.Nama;
                        else
                            TextBoxWilayah.Text = "";
                    }
                    else
                    {
                        TextBoxNamaLengkap.Text = "";
                        TextBoxNomorTelepon.Text = "";
                        TextBoxAlamat.Text = "";
                        TextBoxKodePos.Text = "";
                        TextBoxWilayah.Text = "";
                    }

                    LabelKeteranganPelanggan.Text = "Ubah";
                    ButtonOkPelanggan.Text = "Ubah";

                    MultiViewPelanggan.ActiveViewIndex = 1;
                    TextBoxNamaLengkap.Focus();
                    ModalPopupExtenderPelanggan.Show();
                }
            }
        }
    }
    protected void ButtonTambahPelanggan_Click(object sender, EventArgs e)
    {
        LabelKeteranganPelanggan.Text = "Tambah";
        ButtonOkPelanggan.Text = "Tambah";
        TextBoxNamaLengkap.Focus();

        MultiViewPelanggan.ActiveViewIndex = 1;
        ModalPopupExtenderPelanggan.Show();
    }
    protected void ButtonOkPelanggan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

            Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
            Alamat_Class ClassAlamat = new Alamat_Class();

            TBPelanggan Pelanggan;
            int IDWilayah = 0;

            if (!string.IsNullOrWhiteSpace(TextBoxWilayah.Text))
            {
                string[] InputZona = TextBoxWilayah.Text.Replace(" ~ ", "~").Split('~');
                IDWilayah = Parse.Int(InputZona[0]);
            }

            if (ButtonOkPelanggan.Text == "Tambah")
            {
                //MENAMBAH DATA PELANGGAN
                Pelanggan = ClassPelanggan.Tambah(
                        IDGrupPelanggan: DropDownListGrupPelanggan.SelectedValue.ToInt(),
                        IDPenggunaPIC: Pengguna.IDPengguna,
                        NamaLengkap: TextBoxNamaLengkap.Text,
                        Username: "",
                        Password: "",
                        Email: TextBoxPelangganEmail.Text,
                        Handphone: TextBoxNomorTelepon.Text,
                        TeleponLain: "",
                        TanggalLahir: DateTime.Now,
                        Deposit: 0,
                        Catatan: "",
                        _IsActive: true
                        );

                //MENAMBAH DATA ALAMAT
                ClassAlamat.Tambah(db, IDWilayah, Pelanggan, TextBoxAlamat.Text, TextBoxKodePos.Text, Pengaturan.FormatAngkaInput(LabelBiayaPengiriman.Text), "");
            }
            else
            {
                //UBAH DATA PELANGGAN
                Pelanggan = ClassPelanggan.Ubah(
                    IDPelanggan: HiddenFieldIDPelanggan.Value.ToInt(),
                    IDGrupPelanggan: DropDownListGrupPelanggan.SelectedValue.ToInt(),
                    NamaLengkap: TextBoxNamaLengkap.Text,
                    Email: TextBoxPelangganEmail.Text,
                    Handphone: TextBoxNomorTelepon.Text
                    );

                if (Pelanggan.TBAlamats.Count > 0)
                {
                    //UBAH DATA ALAMAT
                    var Alamat = Pelanggan.TBAlamats.FirstOrDefault();
                    ClassAlamat.Ubah(db, IDWilayah, Alamat, Pelanggan, TextBoxAlamat.Text, TextBoxKodePos.Text, Pengaturan.FormatAngkaInput(LabelBiayaPengiriman.Text), "");
                }
                else //MENAMBAH DATA ALAMAT
                    ClassAlamat.Tambah(db, IDWilayah, Pelanggan, TextBoxAlamat.Text, TextBoxKodePos.Text, Pengaturan.FormatAngkaInput(LabelBiayaPengiriman.Text), "");
            }

            db.SubmitChanges();

            //PENGATURAN PELANGGAN DI TRANSAKSI
            PengaturanPelanggan(Pelanggan.IDPelanggan);

            DropDownListGrupPelanggan.SelectedValue = "1";
            TextBoxNamaLengkap.Text = "";
            TextBoxNomorTelepon.Text = "";
            TextBoxAlamat.Text = "";
            TextBoxKodePos.Text = "";
            TextBoxWilayah.Text = "";
        }
    }
    protected void DropDownListJenisTransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        Transaksi.IDJenisTransaksi = Parse.Int(DropDownListJenisTransaksi.SelectedValue);

        ModalPopupExtenderPelanggan.Show();
    }
    #endregion

    #region PRINTER
    protected void ButtonPrint1_Click(object sender, EventArgs e)
    {
        ButtonPrint1.Focus();
        ModalPopupExtenderPembayaran.Show();
    }
    protected void ButtonKeluarPrint_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
            Response.Redirect("/WITRestaurantV2/");
    }
    private void PengaturanPrint()
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        #region PRINT
        //LabelPrintStore
        //LabelPrintTempatNama
        //LabelTempatAlamat
        //LabelTempatTelepon

        LabelPrintIDOrder.Text = Transaksi.IDTransaksi;
        LabelPrintMeja.Text = Transaksi.Meja.Nama;
        LabelPrintPengguna.Text = Pengguna.NamaLengkap;
        LabelPrintTanggal.Text = Pengaturan.FormatTanggal(Transaksi.TanggalTransaksi);

        //JENIS PEMBAYARAN
        if (Transaksi.Pembayaran != null && Transaksi.Pembayaran.Count > 0)
        {
            if (Transaksi.Pembayaran.Count > 1)
                LabelPrintJenisPembayaran.Text = "Multiple Payment"; //LEBIH DARI 1 PAYMENT
            else
                LabelPrintJenisPembayaran.Text = Transaksi.Pembayaran.FirstOrDefault().NamaJenisPembayaran;
        }
        else
            LabelPrintJenisPembayaran.Text = "Awaiting Payment";

        PanelPelanggan.Visible = Transaksi.Pelanggan.IDPelanggan > 1;

        if (PanelPelanggan.Visible)
        {
            LabelPrintPelangganNama.Text = Transaksi.Pelanggan.Nama;
            LabelPrintPelangganTelepon.Text = Transaksi.Pelanggan.Handphone;
            LabelPrintPelangganAlamat.Text = Transaksi.Pelanggan.AlamatLengkap;
        }

        RepeaterPrintTransaksiDetail.DataSource = Transaksi.Detail.Select(item => new
        {
            JumlahProduk = item.Quantity,
            Produk = item.Nama,
            TotalTanpaPotonganHargaJual = item.TotalHargaJual,
            PotonganHargaJual = item.Discount,
            TotalPotonganHargaJual = item.TotalDiscount
        });
        RepeaterPrintTransaksiDetail.DataBind();

        LabelPrintQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
        LabelPrintSubtotal.Text = Pengaturan.FormatHarga(Transaksi.Detail.Sum(item => item.TotalHargaJual));

        LabelPrintDiscountTransaksi.Text = Pengaturan.FormatHarga(Transaksi.TotalPotonganHargaJualDetail);

        PanelDiscountTransaksi.Visible = LabelPrintDiscountTransaksi.Text != "0";

        PanelBiayaTambahan1.Visible = Transaksi.BiayaTambahan1 > 0;

        if (PanelBiayaTambahan1.Visible) //LabelPrintKeteranganBiayaTambahan1
            LabelPrintBiayaTambahan1.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan1);

        PanelBiayaTambahan2.Visible = Transaksi.BiayaTambahan2 > 0;

        if (PanelBiayaTambahan2.Visible)  //LabelPrintKeteranganBiayaTambahan2
            LabelPrintBiayaTambahan2.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan2);

        LabelPrintBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);

        PanelBiayaPengiriman.Visible = LabelPrintBiayaPengiriman.Text != "0";

        LabelPrintPembulatan.Text = Pengaturan.FormatHarga(Transaksi.Pembulatan);

        PanelPembulatan.Visible = LabelPrintPembulatan.Text != "0";

        LabelPrintGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);

        if (Transaksi.SisaPembayaran == 0)
            LabelPrintSisaPembayaran.Text = "";
        else if (Transaksi.SisaPembayaran > 0)
            LabelPrintSisaPembayaran.Text = "<b>Sisa pembayaran </b>" + Pengaturan.FormatHarga(Transaksi.SisaPembayaran);
        else if (Transaksi.SisaPembayaran < 0)
            LabelPrintSisaPembayaran.Text = "<b>Kelebihan pembayaran </b>" + Pengaturan.FormatHarga(Math.Abs(Transaksi.SisaPembayaran));

        //PanelPembayaran
        //LabelPrintPembayaran

        //PanelKembalian
        //LabelPrintKembalian

        RepeaterPrintJenisPembayaran.DataSource = Transaksi.Pembayaran;
        RepeaterPrintJenisPembayaran.DataBind();

        PanelKeterangan.Visible = !string.IsNullOrWhiteSpace(Transaksi.Keterangan);
        PanelKeterangan1.Visible = PanelKeterangan.Visible;

        LabelPrintKeterangan.Text = Transaksi.Keterangan;

        //PanelFooter
        //PanelFooter1
        //LabelPrintFooter

        LabelKeterangan.Text = Transaksi.Keterangan;
        #endregion

        ButtonPrint2.OnClientClick = "return popitup('Invoice.aspx?id=" + LabelPrintIDOrder.Text + "')";
        ButtonPrint3.OnClientClick = "return popitup('PackingSlip.aspx?id=" + LabelPrintIDOrder.Text + "')";

        #region BUTTON PRINT
        Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

        ButtonPrint1.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.PrintPOS);
        ButtonPrint2.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.PrintInvoice);
        ButtonPrint3.Visible = Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.PrintPackingSlip);

        if (ButtonPrint1.Visible)
            ButtonPrint1.Focus();
        else if (ButtonPrint2.Visible)
            ButtonPrint2.Focus();
        else if (ButtonPrint3.Visible)
            ButtonPrint3.Focus();
        #endregion

        ResetTransaksi();
    }
    #endregion

    protected void RepeaterKategoriProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                HiddenFieldPageProduk.Value = "0";
                HiddenFieldPilihKategori.Value = e.CommandArgument.ToString();
                TextBoxPencarianProduk.Text = string.Empty;
                LoadProduk(HiddenFieldPageProduk.Value.ToInt(), HiddenFieldPilihKategori.Value.ToInt());
            }
        }
    }
    protected void RepeaterProdukKombinasi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Tambah")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            int IDDetailTransaksi = TambahDetailTransaksi(Parse.Int(e.CommandArgument.ToString()), 1);

            //KONFIGURASI MUNCUL POPUP QUANTITY SETELAH MEMASUKKAN PRODUK
            Konfigurasi_Class Konfigurasi_Class = new Konfigurasi_Class(Pengguna.IDGrupPengguna);

            if (Konfigurasi_Class.ValidasiKonfigurasi(EnumKonfigurasi.POSMunculPopupQuantity))
                PengaturanHelper("Jumlah Produk", "1", IDDetailTransaksi.ToString());
        }
    }
    protected void RepeaterKombinasiProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var KombinasiProduk = db.TBKombinasiProduks
                    .FirstOrDefault(item => item.IDKombinasiProduk == Parse.Int(e.CommandArgument.ToString()));

                if (KombinasiProduk != null)
                {
                    //JIKA KOMBINASI ADA DIMASUKKAN KE DETAIL TRANSAKSI
                    LiteralWarningInputProdukManual.Text = Alert_Class.Pesan(TipeAlert.Success, KombinasiProduk.Nama);
                    int IDDetailTransaksi = TambahDetailTransaksi(KombinasiProduk.IDKombinasiProduk, 1);
                    HiddenFieldIDDetailTransaksi.Value = IDDetailTransaksi.ToString();
                }
                else  //KOMBINASI PRODUK TIDAK DITEMUKAN
                    LiteralWarningInputProdukManual.Text = Alert_Class.Pesan(TipeAlert.Danger, "Produk tidak ditemukan");
            }
        }

        panelKombinasiProduk.Visible = false;

        TextBoxInputProdukManual.Text = string.Empty;
        TextBoxInputProdukManual.Focus();

        ModalPopupExtenderProdukManual.Show();
    }
    protected void RepeaterProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            Button ImageButtonShow = (Button)e.Item.FindControl("ImageButtonShow");

            LabelNamaProduk.Text = ImageButtonShow.Text;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                RepeaterProdukKombinasi.DataSource = db.TBStokProduks
                    .Where(item =>
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TBKombinasiProduk.TBProduk.IDProduk == Parse.Int(e.CommandArgument.ToString()))
                    .AsEnumerable()
                    .Select(item => new
                    {
                        item.IDKombinasiProduk,
                        Nama = !string.IsNullOrWhiteSpace(item.TBKombinasiProduk.TBAtributProduk.Nama) ? item.TBKombinasiProduk.TBAtributProduk.Nama : item.TBKombinasiProduk.Nama,
                        item.HargaJual,
                        item.Jumlah
                    });
                RepeaterProdukKombinasi.DataBind();
            }

            ModalPopupExtenderKombinasiProduk.Show();
        }
    }
    protected void ButtonKeteranganTambahan_Click(object sender, EventArgs e)
    {
        //SIMPAN KETERANGAN DETAIL TRANSAKSI
        Transaksi_Class transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (transaksi != null)
        {
            var DetailTransaksi = transaksi.Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == Parse.Int(HiddenFieldIDKombinasiProdukDeskripsi.Value));

            if (TextBoxKeteranganTransaksiDetail.Text.Length > 0 && TextBoxKeteranganTransaksiDetail.Text[0] == '\n')
                TextBoxKeteranganTransaksiDetail.Text = TextBoxKeteranganTransaksiDetail.Text.Substring(1);

            DetailTransaksi.Keterangan = TextBoxKeteranganTransaksiDetail.Text;
        }
    }
    protected void ButtonOrderTunai_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
        {
            //JIKA RESTAURANT MAKA MENENTUKAN JUMLAH TAMU
            PengaturanHelper("Jumlah Tamu", Transaksi.JumlahTamu.ToString(), "");
        }
        else
        {
            //ORDER TRANSAKSI
            TextBoxKeterangan.Text = Transaksi.Keterangan;
            ButtonSimpanKeterangan.Text = "Order";
            TextBoxKeterangan.Focus();
            ModalPopupExtenderKeterangan.Show();
        }
    }
    protected void ButtonTambahKeterangan_Click(object sender, EventArgs e)
    {
        //MENAMBAHKAN KETERANGAN TRANSAKSI
        Transaksi_Class transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (transaksi != null)
        {
            TextBoxKeterangan.Text = transaksi.Keterangan;
            ButtonSimpanKeterangan.Text = "Simpan";
            TextBoxKeterangan.Focus();
            ModalPopupExtenderKeterangan.Show();
        }
    }
    protected void ButtonSimpanKeterangan_Click(object sender, EventArgs e)
    {
        try
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

                //SET KETERANGAN
                Transaksi.Keterangan = TextBoxKeterangan.Text;

                if (ButtonSimpanKeterangan.Text == "Order")
                {
                    MultiViewPembayaran.ActiveViewIndex = 2;

                    Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
                    Transaksi.StatusPrint = true;
                    LabelPrintIDOrder.Text = Transaksi.ConfirmTransaksi();

                    if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
                        Response.Redirect("/WITRestaurantV2/", false);
                    else
                    {
                        PengaturanPrint();
                        ModalPopupExtenderPembayaran.Show();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, "Point Of Sales ButtonSimpanKeterangan_Click");
        }
    }
    protected void RepeaterJenisPembayaran_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            TextBoxPembayaranLainnya.Focus();

            HiddenFieldIDJenisPembayaran.Value = e.CommandArgument.ToString();

            MultiViewPembayaran.ActiveViewIndex = 1;
            ModalPopupExtenderPembayaran.Show();
        }
    }
    protected void ButtonTunai_Click(object sender, EventArgs e)
    {
        //DEFAULT JENIS PEMBAYARAN TUNAI
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (Transaksi != null)
        {
            LoadTransaksi();

            //MENAMPLIKAN JUMLAH UANG BAYAR
            MultiViewPembayaran.ActiveViewIndex = 0;

            TextBoxJumlahBayar.Text = "";
            TextBoxJumlahBayar.Focus();

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                //MEMERIKSA APAKAH MASIH ADA SISA DEPOSIT
                ButtonPembayaranDeposit.Visible = ClassPelanggan.Deposit(Transaksi.Pelanggan.IDPelanggan) > 0 && Transaksi.Pembayaran.FirstOrDefault(item => item.IDJenisPembayaran == 2) == null;
            }

            //MENYESUAIKAN BUTTON DEPOSIT PADA SAAT BAYAR ATAU SPLIT PAYMENT
            ButtonSplitPaymentDeposit.Visible = ButtonPembayaranDeposit.Visible;

            if (Transaksi.Pembayaran.Count > 0)
            {
                LoadDataSplitPayment();
                ModalPopupExtenderSplitPayment.Show();
            }
            else
                ModalPopupExtenderPembayaran.Show();
        }
    }
    protected void ButtonConfirmPayment_Click(object sender, EventArgs e)
    {
        //DEFAULT JENIS PEMBAYARAN TUNAI
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (Transaksi != null)
        {
            LoadTransaksi();

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                //MEMERIKSA APAKAH MASIH ADA SISA DEPOSIT
                ButtonPembayaranDeposit.Visible = ClassPelanggan.Deposit(Transaksi.Pelanggan.IDPelanggan) > 0 && Transaksi.Pembayaran.FirstOrDefault(item => item.IDJenisPembayaran == 2) == null;
            }

            //MENYESUAIKAN BUTTON DEPOSIT PADA SAAT BAYAR ATAU SPLIT PAYMENT
            ButtonSplitPaymentDeposit.Visible = ButtonPembayaranDeposit.Visible;

            LoadDataSplitPayment();
            ModalPopupExtenderSplitPayment.Show();
        }
    }

    #region SPLIT PAYMENT
    private void LoadDataSplitPayment()
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        LabelSplitPaymentLebihKurang.Text = "";
        LiteralSplitPaymentWarning.Text = "";

        if (Transaksi != null)
        {
            RepeaterSplitPaymentPembayaran.DataSource = Transaksi.Pembayaran;
            RepeaterSplitPaymentPembayaran.DataBind();

            LabelSplitPaymentGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);
            LabelSplitPaymentTotalPembayaran.Text = Pengaturan.FormatHarga(Transaksi.TotalPembayaran);

            TextBoxSplitPaymentNominal.Text = "";
            TextBoxSplitPaymentKeterangan.Text = "";

            //JIKA DEPOSIT SUDAH DIGUNAKAN TIDAK DAPAT MENGGUNAKAN DEPOSIT LAGI
            if (ButtonPembayaranDeposit.Visible)
                ButtonSplitPaymentDeposit.Visible = Transaksi.Pembayaran.FirstOrDefault(item => item.IDJenisPembayaran == 2) == null;

            if (Transaksi.GrandTotal > Transaksi.TotalPembayaran)
            {
                LabelSplitPaymentLebihKurang.Text = "(Kurang : " + Pengaturan.FormatHarga((Transaksi.GrandTotal - Transaksi.TotalPembayaran)) + ")";
                LabelSplitPaymentLebihKurang.ForeColor = System.Drawing.Color.Red;
                TextBoxSplitPaymentNominal.Text = (Transaksi.GrandTotal - Transaksi.TotalPembayaran).ToString();
            }
            else if (Transaksi.GrandTotal < Transaksi.TotalPembayaran)
            {
                LabelSplitPaymentLebihKurang.Text = "(Lebih : " + Pengaturan.FormatHarga((Transaksi.TotalPembayaran - Transaksi.GrandTotal)) + ")";
                LabelSplitPaymentLebihKurang.ForeColor = System.Drawing.Color.DarkGreen;
            }

            TextBoxSplitPaymentTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");
            TextBoxSplitPaymentJam.Text = DateTime.Now.ToString("HH:mm");
            TextBoxSplitPaymentNominal.Focus();
        }
    }
    protected void RepeaterSplitPaymentJenisPembayaran_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (Pengaturan.FormatAngkaInput(TextBoxSplitPaymentNominal.Text) > 0)
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];
                Transaksi.TambahPembayaran(DateTime.Parse(TextBoxSplitPaymentTanggal.Text + " " + TextBoxSplitPaymentJam.Text), Pengguna.IDPengguna, Parse.Int(e.CommandArgument.ToString()), Pengaturan.FormatAngkaInput(TextBoxSplitPaymentNominal.Text), TextBoxSplitPaymentKeterangan.Text);

                LoadDataSplitPayment();
                ModalPopupExtenderSplitPayment.Show();
            }
        }
        else
        {
            ModalPopupExtenderSplitPayment.Show();
            TextBoxSplitPaymentNominal.Focus();
        }
    }
    protected void ButtonSplitPaymentDeposit_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (Transaksi != null)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                var SisaDeposit = ClassPelanggan.Deposit(Transaksi.Pelanggan.IDPelanggan);

                if (SisaDeposit > 0 && Pengaturan.FormatAngkaInput(TextBoxSplitPaymentNominal.Text) > 0)
                {
                    //NOMINAL SPLIT PAYMENT LEBIH KECIL DARI SISA DEPOSIT
                    if (Pengaturan.FormatAngkaInput(TextBoxSplitPaymentNominal.Text) <= SisaDeposit)
                        Transaksi.TambahPembayaran(DateTime.Parse(TextBoxSplitPaymentTanggal.Text + " " + TextBoxSplitPaymentJam.Text), Pengguna.IDPengguna, 2, Pengaturan.FormatAngkaInput(TextBoxSplitPaymentNominal.Text), TextBoxSplitPaymentKeterangan.Text);
                    else
                        Transaksi.TambahPembayaran(DateTime.Parse(TextBoxSplitPaymentTanggal.Text + " " + TextBoxSplitPaymentJam.Text), Pengguna.IDPengguna, 2, SisaDeposit, ""); //SISA DEPOSIT LEBIH KECIL DARI NOMINAL SPLIT PAYMENT

                    LoadDataSplitPayment();
                }

                ModalPopupExtenderSplitPayment.Show();
            }
        }
    }
    protected void RepeaterSplitPaymentPembayaran_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            if (Transaksi != null)
            {
                var JenisPembayaran = Transaksi.Pembayaran.FirstOrDefault(item => item.IDTransaksiJenisPembayaran == Parse.Int(e.CommandArgument.ToString()));

                bool status = false;

                if (JenisPembayaran.IDJenisPembayaran == 2)
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);
                        var SisaDeposit = ClassPelanggan.Deposit(Transaksi.Pelanggan.IDPelanggan);

                        if (SisaDeposit > 0)
                            status = true;
                    }
                }

                ButtonPembayaranDeposit.Visible = status;
                ButtonSplitPaymentDeposit.Visible = status;

                Transaksi.HapusPembayaran(JenisPembayaran);
                LoadDataSplitPayment();
            }
        }

        ModalPopupExtenderSplitPayment.Show();
    }
    #endregion

    #region CONFIRM PEMBAYARAN
    protected void ButtonSplitPaymentBayar_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        //UANG YANG DIBAYAR SAMA DENGAN TAGIHAN
        if (Transaksi.GrandTotal == Pengaturan.FormatAngkaInput(LabelSplitPaymentTotalPembayaran.Text))
            ConfirmPembayaran(Transaksi);
        else
        {
            LiteralSplitPaymentWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "", "Jumlah pembayaran tidak sesuai");
            ModalPopupExtenderSplitPayment.Show();
        }
    }
    protected void ButtonBayarTunai_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        decimal Bayar = Pengaturan.FormatAngkaInput(TextBoxJumlahBayar);

        if (Bayar >= Transaksi.GrandTotal) //UANG YANG DIBAYAR LEBIH BESAR ATAU SAMA DENGAN TAGIHAN
        {
            //MENGATUR JENIS PEMBAYARAN
            Transaksi.TambahPembayaran(DateTime.Now, Pengguna.IDPengguna, 1, Transaksi.GrandTotal, ""); //CASH

            if (ConfirmPembayaran(Transaksi))
            {
                LabelPrintPembayaran.Text = Pengaturan.FormatHarga(Bayar);
                string kembalian = Pengaturan.FormatHarga((Bayar - Transaksi.GrandTotal));

                LabelJumlahKembalian.Text = kembalian;
                LabelPrintKembalian.Text = kembalian;

                PanelKembalian.Visible = true;
                PanelPembayaran.Visible = true;
            }
        }
        else
        {
            TextBoxJumlahBayar.Focus();
            ModalPopupExtenderPembayaran.Show();
        }
    }
    protected void ButtonSimpanPembayaranLainnya_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        Transaksi.TambahPembayaran(DateTime.Now, Pengguna.IDPengguna, Parse.Int(HiddenFieldIDJenisPembayaran.Value), Transaksi.GrandTotal, TextBoxPembayaranLainnya.Text);
        ConfirmPembayaran(Transaksi);
    }
    protected void ButtonPembayaranDeposit_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];
        PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

            //MENGHITUNG SISA DEPOSIT
            var SisaDeposit = ClassPelanggan.Deposit(Transaksi.Pelanggan.IDPelanggan);

            if (SisaDeposit > 0) //JIKA DEPOSIT MASIH ADA
            {
                if (Transaksi.GrandTotal <= SisaDeposit) //TOTAL TAGIHAN LEBIH KECIL ATAU SAMA DENGAN DEPOSIT
                {
                    Transaksi.TambahPembayaran(DateTime.Now, Pengguna.IDPengguna, 2, Transaksi.GrandTotal, TextBoxPembayaranLainnya.Text);
                    ConfirmPembayaran(Transaksi);
                }
                else //BAYAR SESUAI DENGAN JUMLAH SISA DEPOSIT DAN BUKA SPLIT BILL
                {
                    Transaksi.TambahPembayaran(DateTime.Now, Pengguna.IDPengguna, 2, SisaDeposit, TextBoxPembayaranLainnya.Text);
                    LoadDataSplitPayment();
                    ModalPopupExtenderSplitPayment.Show();
                }
            }
        }
    }
    private bool ConfirmPembayaran(Transaksi_Class Transaksi)
    {
        try
        {
            if (Transaksi != null)
            {
                if (Transaksi.PeriksaDeposit())
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)ViewState["PenggunaLogin"];

                    Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.Complete;
                    LabelPrintTanggal.Text = Transaksi.TanggalTransaksi.ToString("d MMMM yyyy HH:mm");
                    Transaksi.StatusPrint = true;
                    LabelPrintIDOrder.Text = Transaksi.ConfirmTransaksi();

                    if (Pengguna.PointOfSales == TipePointOfSales.Restaurant)
                    {
                        //PENGURANGAN BAHAN BAKU
                        StokBahanBaku_Class.ProduksiByTransaksiBerhasil(Transaksi.IDPenggunaTransaksi, Pengguna.IDTempat, LabelPrintIDOrder.Text);
                    }

                    MultiViewPembayaran.ActiveViewIndex = 2;
                    PengaturanPrint();

                    ModalPopupExtenderPembayaran.Show();

                    return true;
                }
                else
                {
                    LoadDataSplitPayment();
                    ModalPopupExtenderSplitPayment.Show();

                    return false;
                }
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, "Point Of Sales ConfirmPembayaran");
            return false;
        }
    }
    #endregion

    #region CHECKBOX
    protected void CheckBoxBiayaTambahan1_CheckedChanged(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (CheckBoxBiayaTambahan1.Checked)
            Transaksi.EnumBiayaTambahan1 = EnumBiayaTambahan.Persentase;
        else
            Transaksi.EnumBiayaTambahan1 = EnumBiayaTambahan.TidakAda;

        LoadTransaksi();
    }
    protected void CheckBoxBiayaTambahan2_CheckedChanged(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        if (CheckBoxBiayaTambahan2.Checked)
            Transaksi.EnumBiayaTambahan2 = EnumBiayaTambahan.Persentase;
        else
            Transaksi.EnumBiayaTambahan2 = EnumBiayaTambahan.TidakAda;

        LoadTransaksi();
    }
    #endregion

    protected void ButtonSplitPayment_Click(object sender, EventArgs e)
    {
        LoadDataSplitPayment();
        ModalPopupExtenderSplitPayment.Show();
    }

    #region BIAYA PENGIRIMAN
    #region Pencarian Zona
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] PencarianZona(string prefixText, int count)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            return db.TBKurirBiayas
                .Where(item =>
                    item.TBWilayah.IDGrupWilayah == 4 &&
                    item.TBWilayah.Nama.Contains(prefixText))
                .OrderBy(item => item.TBWilayah.Nama)
                .Select(item => item.IDKurirBiaya + " ~ " + (item.TBWilayah.IDWilayahParent.HasValue ? item.TBWilayah.TBWilayah1.Nama : "") + " " + item.TBWilayah.TBWilayah1.TBWilayah1.Nama + " ~ " + item.TBKurir.Nama)
                .ToArray();
        }
    }
    #endregion

    protected void ButtonBiayaPengiriman_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        TextBoxBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);

        LabelTotalBeratShipping.Visible = Transaksi.BeratTotal > 0;

        if (LabelTotalBeratShipping.Visible)
            LabelTotalBeratShipping.Text = "(" + Pengaturan.FormatHarga(Transaksi.BeratTotal) + " kg)";

        TextBoxZona.Text = string.Empty;
        TextBoxZona.Focus();

        ModalPopupExtenderBiayaPengiriman.Show();
    }
    protected void ButtonPilihKurir_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextBoxZona.Text))
        {
            Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                string[] InputZona = TextBoxZona.Text.Replace(" ~ ", "~").Split('~');
                var KurirBiaya = db.TBKurirBiayas.FirstOrDefault(item => item.IDKurirBiaya == Parse.Int(InputZona[0]));

                if (KurirBiaya != null)
                {
                    Transaksi.BiayaPengirimanPerKg = KurirBiaya.Biaya;
                    Transaksi.Keterangan += (string.IsNullOrWhiteSpace(Transaksi.Keterangan) ? "" : " ") + InputZona[2];
                    TextBoxBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);
                    DropDownListKurir.SelectedValue = KurirBiaya.IDKurir.ToString();
                }
            }
        }

        TextBoxBiayaPengiriman.Focus();
        ModalPopupExtenderBiayaPengiriman.Show();
    }
    protected void ButtonSimpanBiayaPengiriman_Click(object sender, EventArgs e)
    {
        Transaksi_Class Transaksi = (Transaksi_Class)ViewState["Transaksi"];

        Transaksi.BiayaPengiriman = Parse.Decimal(TextBoxBiayaPengiriman.Text.Replace("%", "").Replace(".", "").Replace(",", "."));
        Transaksi.IDTempatPengirim = Parse.Int(DropDownListTempatPengirim.SelectedValue);
        Transaksi.IDKurir = Parse.Int(DropDownListKurir.SelectedValue);
        LoadTransaksi();
    }
    #endregion
}