using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_Marketing_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                #region Default
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "Semua" });
                DropDownListJenisStok.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                DropDownListJenisStok.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                DropDownListJenisStok.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                DropDownListCariAtributProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());
                DropDownListCariKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));
                DropDownListCariPemilik.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                #endregion

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                ////MENAMPILKAN MESSAGE
                LiteralWarning.Text = "";

                var DataTransaksi = db.TBTransaksis.FirstOrDefault(item => item.IDTransaksi == Request.QueryString["id"]);

                if (DataTransaksi != null)
                {
                    //DATA TRANSAKSI DITEMUKAN
                    if (DataTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete || DataTransaksi.IDStatusTransaksi == (int)EnumStatusTransaksi.Canceled)
                    {
                        //TRANSAKSI COMPLETE ATAU CANCELED COPY KE TRANSAKSI BARU
                        var Transaksi = new Transaksi_Model(Pengguna.IDPengguna, Pengguna.IDTempat, DateTime.Now);

                        Transaksi.IDJenisTransaksi = (int)EnumJenisTransaksi.Marketing; //MARKETING

                        Dictionary<int, int> ListStokProdukHabis = new Dictionary<int, int>();
                        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
                        int TotalTransfer = 0;
                        string PesanTransaksi = "";

                        //COPY DETAIL TRANSAKSI LAMA KE DETAIL TRANSAKSI BARU
                        foreach (var item in DataTransaksi.TBTransaksiDetails.ToArray())
                        {
                            //PENCARIAN STOK PRODUK
                            var StokProduk = StokProduk_Class.Cari(Pengguna.IDTempat, item.IDKombinasiProduk);

                            if (item.Quantity <= StokProduk.Jumlah)
                            {
                                //JIKA JUMLAH YANG AKAN DI TRANSAKSI LEBIH KECIL ATAU SAMA DENGAN JUMLAH STOK

                                Transaksi.TambahDetailTransaksiMarketing(item.IDKombinasiProduk, item.Quantity);

                                //MESSAGE TRANSFER YANG BERHASIL
                                TotalTransfer += item.Quantity;
                                PesanTransaksi += "<br/>" + Pengaturan.FormatHarga(item.Quantity) + " - " + item.TBKombinasiProduk.Nama;
                            }
                            else //MENCATAT STOK PRODUK YANG HABIS
                                ListStokProdukHabis.Add(StokProduk.IDKombinasiProduk, item.Quantity);
                        }

                        if (TotalTransfer > 0)
                        {
                            Transaksi.Keterangan = "Referensi Transaksi #" + DataTransaksi.IDTransaksi + " - " + DataTransaksi.Keterangan;

                            LabelIDTransaksi.Text = Transaksi.ConfirmTransaksi(db);
                            db.SubmitChanges();

                            LoadDataTransaksiDetail();

                            PesanTransaksi += "<br/><h5><b>Total : " + TotalTransfer + "</b></h5>";
                            LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Success, "Produk berhasil disimpan" + PesanTransaksi);
                        }

                        if (ListStokProdukHabis.Count > 0)
                        {
                            DropDownListJenisStok.SelectedValue = "2";

                            #region MENAMPILKAN STOK APA SAJA YANG HABIS
                            var DataStokProduk = db.TBStokProduks
                                .AsEnumerable()
                                .Where(item =>
                                    item.IDTempat == Pengguna.IDTempat &&
                                    ListStokProdukHabis.ContainsKey(item.IDKombinasiProduk))
                                .Select(item => new
                                {
                                    IDStokProduk = item.IDStokProduk,
                                    IDKombinasiProduk = item.IDKombinasiProduk,
                                    Kode = item.TBKombinasiProduk.KodeKombinasiProduk,

                                    RelasiKategoriProduk = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks,
                                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),

                                    IDPemilikProduk = item.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                                    PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,

                                    IDAtributProduk = item.TBKombinasiProduk.IDAtributProduk,
                                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,

                                    IDProduk = item.TBKombinasiProduk.IDProduk,
                                    Produk = item.TBKombinasiProduk.TBProduk.Nama,

                                    Jumlah = item.Jumlah,

                                    HargaBeli = item.HargaBeli,
                                    HargaJual = item.HargaJual
                                }).ToArray();

                            RepeaterStokKombinasiProduk.DataSource = DataStokProduk;
                            RepeaterStokKombinasiProduk.DataBind();

                            if (DataStokProduk.Count() > 0)
                                LabelTotalJumlahStok.Text = Pengaturan.FormatHarga(DataStokProduk.Sum(item => item.Jumlah));
                            else
                                LabelTotalJumlahStok.Text = "0";
                            #endregion

                            #region MENGISI TEXTBOX DENGAN JUMLAH PERMINTAAN
                            foreach (RepeaterItem item in RepeaterStokKombinasiProduk.Items)
                            {
                                Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                                TextBox TextBoxJumlahTransaksi = (TextBox)item.FindControl("TextBoxJumlahTransaksi");
                                HtmlTableRow PanelStok = (HtmlTableRow)item.FindControl("PanelStok");

                                if (ListStokProdukHabis.ContainsKey(LabelIDKombinasiProduk.Text.ToInt()))
                                {
                                    TextBoxJumlahTransaksi.Text = Pengaturan.FormatHarga(ListStokProdukHabis[LabelIDKombinasiProduk.Text.ToInt()]);
                                    PanelStok.Attributes.Add("class", "danger");
                                }
                            }
                            #endregion

                            LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Danger, "Stok tidak cukup, silahkan cek kembali");
                            MultiViewTransaksi.ActiveViewIndex = 0;
                        }
                        else
                        {
                            //JIKA STOK PRODUK TIDAK HABIS
                            LoadDataStokProduk();
                            MultiViewTransaksi.ActiveViewIndex = 1;
                        }
                    }
                    else
                    {
                        //STATUS TRANSAKSI SELAIN COMPLETE DAN CANCELED
                        LabelIDTransaksi.Text = DataTransaksi.IDTransaksi;
                        LoadDataTransaksiDetail();
                        MultiViewTransaksi.ActiveViewIndex = 1;
                    }
                }
                else
                {
                    //TRANSAKSI TIDAK DITEMUKAN
                    MultiViewTransaksi.ActiveViewIndex = 0;
                    LoadDataStokProduk();
                }
            }
        }
        else
            LiteralWarning.Text = "";
    }
    private void LoadDataStokProduk()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var DataStokProduk = db.TBStokProduks
                .Where(item =>
                    item.TBKombinasiProduk.TBProduk._IsActive &&
                    item.IDTempat == Pengguna.IDTempat)
                .Select(item => new
                {
                    IDStokProduk = item.IDStokProduk,
                    IDKombinasiProduk = item.IDKombinasiProduk,
                    Kode = item.TBKombinasiProduk.KodeKombinasiProduk,

                    RelasiKategoriProduk = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),

                    IDPemilikProduk = item.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                    PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,

                    IDAtributProduk = item.TBKombinasiProduk.IDAtributProduk,
                    AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,

                    IDProduk = item.TBKombinasiProduk.IDProduk,
                    Produk = item.TBKombinasiProduk.TBProduk.Nama,

                    Jumlah = item.Jumlah,

                    HargaBeli = item.HargaBeli,
                    HargaJual = item.HargaJual
                }).ToArray();

            if (DropDownListJenisStok.SelectedValue == "1")
                DataStokProduk = DataStokProduk.Where(item => item.Jumlah > 0).ToArray();
            else if (DropDownListJenisStok.SelectedValue == "2")
                DataStokProduk = DataStokProduk.Where(item => item.Jumlah == 0).ToArray();
            else if (DropDownListJenisStok.SelectedValue == "3")
                DataStokProduk = DataStokProduk.Where(item => item.Jumlah < 0).ToArray();

            if (!string.IsNullOrWhiteSpace(TextBoxCariKode.Text))
            {
                DataStokProduk = DataStokProduk.Where(item => item.Kode.ToLower().Contains(TextBoxCariKode.Text.ToLower())).ToArray();
                TextBoxCariKode.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxCariProduk.Text))
            {
                DataStokProduk = DataStokProduk.Where(item => item.Produk.ToLower().Contains(TextBoxCariProduk.Text.ToLower())).ToArray();
                TextBoxCariProduk.Focus();
            }

            if (DropDownListCariAtributProduk.SelectedValue != "0")
            {
                DataStokProduk = DataStokProduk.Where(item => item.IDAtributProduk == DropDownListCariAtributProduk.SelectedValue.ToInt()).ToArray();
                DropDownListCariAtributProduk.Focus();
            }

            if (DropDownListCariKategori.SelectedValue != "0")
            {
                DataStokProduk = DataStokProduk.Where(item => item.RelasiKategoriProduk.FirstOrDefault(item2 => item2.IDKategoriProduk == DropDownListCariKategori.SelectedValue.ToInt()) != null).ToArray();
                DropDownListCariKategori.Focus();
            }

            if (DropDownListCariPemilik.SelectedValue != "0")
            {
                DataStokProduk = DataStokProduk.Where(item => item.PemilikProduk == DropDownListCariPemilik.SelectedItem.Text).ToArray();
                DropDownListCariPemilik.Focus();
            }

            RepeaterStokKombinasiProduk.DataSource = DataStokProduk;
            RepeaterStokKombinasiProduk.DataBind();

            if (DataStokProduk.Count() > 0)
                LabelTotalJumlahStok.Text = Pengaturan.FormatHarga(DataStokProduk.Sum(item => item.Jumlah));
            else
                LabelTotalJumlahStok.Text = "0";
        }
    }
    private void LoadDataTransaksiDetail()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Transaksi_Model Transaksi = new Transaksi_Model(LabelIDTransaksi.Text, Pengguna.IDPengguna);

            RepeaterTransaksiKombinasiProduk.DataSource = Transaksi.Detail
                .Join(db.TBKombinasiProduks,
                        Detail => Detail.IDKombinasiProduk,
                        KombinasiProduk => KombinasiProduk.IDKombinasiProduk,
                        (Detail, KombinasiProduk) => new
                        {
                            Detail = Detail,
                            KombinasiProduk = KombinasiProduk
                        })
                .Select(item => new
                {
                    Produk = item.KombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.KombinasiProduk.TBAtributProduk.Nama,
                    Barcode = item.Detail.Barcode,
                    Kategori = item.KombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.KombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                    PemilikProduk = item.KombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    HargaJual = item.Detail.HargaJual,
                    JumlahProduk = item.Detail.Quantity,
                    Subtotal = item.Detail.Subtotal,
                    IDDetailTransaksi = item.Detail.IDDetailTransaksi,
                })
                .OrderBy(item => item.Produk);
            RepeaterTransaksiKombinasiProduk.DataBind();

            LabelTotalJumlah.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
            LabelTotalSubtotalHargaJual.Text = Pengaturan.FormatHarga(Transaksi.Subtotal);
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            //STATUS STOK KURANG ATAU TIDAK
            bool StokKurang = false;

            int TotalTransfer = 0;
            string PesanTransaksi = "";

            //MENAMPILKAN MESSAGE
            LiteralWarning.Text = "";

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
                Transaksi_Model Transaksi;

                //CLASS TRANSAKSI
                if (string.IsNullOrWhiteSpace(LabelIDTransaksi.Text))
                {
                    //MEMBUAT TRANSAKSI BARU
                    Transaksi = new Transaksi_Model(Pengguna.IDPengguna, Pengguna.IDTempat, DateTime.Now);

                    Transaksi.IDJenisTransaksi = (int)EnumJenisTransaksi.Marketing; //MARKETING
                }
                else
                    Transaksi = new Transaksi_Model(LabelIDTransaksi.Text, Pengguna.IDPengguna);

                foreach (RepeaterItem item in RepeaterStokKombinasiProduk.Items)
                {
                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    Label LabelJumlah = (Label)item.FindControl("LabelJumlah");
                    TextBox TextBoxJumlahTransaksi = (TextBox)item.FindControl("TextBoxJumlahTransaksi");
                    HtmlTableRow PanelStok = (HtmlTableRow)item.FindControl("PanelStok");
                    int JumlahTransaksi = (int)Pengaturan.FormatAngkaInput(TextBoxJumlahTransaksi);

                    if (JumlahTransaksi > 0)
                    {
                        //JIKA JUMLAH TRANSAKSI VALID TIDAK NULL DAN TIDAK 0

                        //PENCARIAN STOK PRODUK
                        var StokProduk = StokProduk_Class.Cari(Pengguna.IDTempat, LabelIDKombinasiProduk.Text.ToInt());

                        if (JumlahTransaksi <= StokProduk.Jumlah)
                        {
                            //JIKA JUMLAH YANG AKAN DI TRANSAKSI LEBIH KECIL ATAU SAMA DENGAN JUMLAH STOK

                            //APAKAH SUDAH ADA DI DETAIL
                            var TransaksiDetail = Transaksi.Detail.FirstOrDefault(item2 => item2.IDKombinasiProduk == LabelIDKombinasiProduk.Text.ToInt());

                            if (TransaksiDetail == null)
                                Transaksi.TambahDetailTransaksiMarketing(LabelIDKombinasiProduk.Text.ToInt(), JumlahTransaksi);
                            else
                                Transaksi.TambahKurangJumlahProduk(TransaksiDetail.IDDetailTransaksi, JumlahTransaksi);

                            //MENGKOSONGKAN TEXTBOX - LABEL JUMLAH DIISI DENGAN JUMLAH STOK TERBARU
                            TextBoxJumlahTransaksi.Text = "";
                            PanelStok.Attributes.Add("class", "");
                            LabelJumlah.Text = Pengaturan.FormatHarga(Pengaturan.FormatAngkaInput(LabelJumlah.Text) - JumlahTransaksi);
                            LabelTotalJumlahStok.Text = Pengaturan.FormatHarga(Pengaturan.FormatAngkaInput(LabelTotalJumlahStok.Text) - JumlahTransaksi);

                            //MESSAGE TRANSFER YANG BERHASIL
                            TotalTransfer += JumlahTransaksi;
                            PesanTransaksi += "<br/>" + Pengaturan.FormatHarga(JumlahTransaksi) + " - " + StokProduk.TBKombinasiProduk.Nama;
                        }
                        else
                        {
                            //REFRESH LABEL JUMLAH STOK
                            LabelTotalJumlahStok.Text = Pengaturan.FormatHarga(Pengaturan.FormatAngkaInput(LabelTotalJumlahStok.Text) - (Pengaturan.FormatAngkaInput(LabelJumlah.Text) - StokProduk.Jumlah));
                            LabelJumlah.Text = Pengaturan.FormatHarga(StokProduk.Jumlah);
                            PanelStok.Attributes.Add("class", "danger");

                            StokKurang = true;
                        }
                    }

                    //HANDLE POSTBACK FORMAT HARGA
                    TextBoxJumlahTransaksi.Text = Pengaturan.FormatAngkaInput(TextBoxJumlahTransaksi.Text) == 0 ? "" : Pengaturan.FormatAngkaInput(TextBoxJumlahTransaksi.Text).ToString();
                }

                if (TotalTransfer > 0)
                {
                    LabelIDTransaksi.Text = Transaksi.ConfirmTransaksi(db);
                    db.SubmitChanges();
                }
            }

            if (TotalTransfer > 0)
            {
                PesanTransaksi += "<br/><h5><b>Total : " + TotalTransfer + "</b></h5>";
                LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Success, "Produk berhasil disimpan" + PesanTransaksi);
            }

            if (StokKurang)
                LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Danger, "Stok tidak cukup, silahkan cek kembali");
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
    protected void ButtonTransaksiDetail_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(LabelIDTransaksi.Text))
        {
            LoadDataTransaksiDetail();
            MultiViewTransaksi.ActiveViewIndex = 1;
        }
        else
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Tidak ada data transaksi");
    }
    protected void ButtonStokProduk_Click(object sender, EventArgs e)
    {
        MultiViewTransaksi.ActiveViewIndex = 0;
        LoadDataStokProduk();
    }

    protected void Event_LoadData(object sender, EventArgs e)
    {
        LoadDataStokProduk();
    }
    protected void MultiViewTransaksi_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiViewTransaksi.ActiveViewIndex == 0)
        {
            DropDownListJenisStok.Visible = true;
            ButtonCari.Visible = true;
            ButtonStokProduk.Visible = false;

            ButtonTransaksiDetail.Visible = true;
            ButtonPointOfSales.Visible = false;

            ButtonSimpan.Visible = true;
        }
        else
        {
            DropDownListJenisStok.Visible = false;
            ButtonCari.Visible = false;
            ButtonStokProduk.Visible = true;

            ButtonTransaksiDetail.Visible = false;
            ButtonPointOfSales.Visible = true;

            ButtonSimpan.Visible = false;
        }
    }
    protected void RepeaterTransaksiKombinasiProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Transaksi_Model Transaksi = new Transaksi_Model(LabelIDTransaksi.Text, Pengguna.IDPengguna);

                Transaksi.UbahJumlahProduk(e.CommandArgument.ToInt(), 0);

                Transaksi.ConfirmTransaksi(db);
                db.SubmitChanges();

                LoadDataTransaksiDetail();
            }
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Transaksi.aspx");
    }
    protected void ButtonPointOfSales_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITPointOfSales/Default.aspx?id=" + LabelIDTransaksi.Text);
    }
}