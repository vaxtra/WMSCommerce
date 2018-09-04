using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Kirim_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);

                #region Default
                DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "Semua" });
                DropDownListJenisStok.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                DropDownListJenisStok.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                DropDownListJenisStok.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                DropDownListCariAtributProduk.DataSource = ClassAtributProduk.Data();
                DropDownListCariAtributProduk.DataTextField = "Nama";
                DropDownListCariAtributProduk.DataValueField = "IDAtributProduk";
                DropDownListCariAtributProduk.DataBind();
                DropDownListCariAtributProduk.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

                DropDownListCariKategori.DataSource = db.TBKategoriProduks.ToArray();
                DropDownListCariKategori.DataTextField = "Nama";
                DropDownListCariKategori.DataValueField = "IDKategoriProduk";
                DropDownListCariKategori.DataBind();
                DropDownListCariKategori.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

                var ListTempat = ClassTempat.Data();

                DropDownListTempatPengirim.DataSource = ListTempat;
                DropDownListTempatPengirim.DataTextField = "Nama";
                DropDownListTempatPengirim.DataValueField = "IDTempat";
                DropDownListTempatPengirim.DataBind();
                DropDownListTempatPengirim.Items.Insert(0, new ListItem { Text = "- Lokasi -", Value = "0" });
                DropDownListTempatPengirim.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListTempatPenerima.DataSource = ListTempat.Where(item => item.IDTempat != Pengguna.IDTempat);
                DropDownListTempatPenerima.DataTextField = "Nama";
                DropDownListTempatPenerima.DataValueField = "IDTempat";
                DropDownListTempatPenerima.DataBind();

                TextBoxTanggalKirim.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
                #endregion

                var DataTransferProduk = db.TBTransferProduks
                    .FirstOrDefault(item => item.IDTransferProduk == Request.QueryString["id"]);

                if (DataTransferProduk != null)
                {
                    //DATA TRANSFER PRODUK DITEMUKAN

                    if (DataTransferProduk.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferBatal ||
                        DataTransferProduk.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses ||
                        DataTransferProduk.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferSelesai)
                    {
                        //TRANSFER BATAL MAKA COPY TRANSFER PRODUK LAMA KE TRANSFER PRODUK BARU
                        TextBoxTanggalKirim.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
                        DropDownListTempatPenerima.SelectedValue = DataTransferProduk.IDTempatPenerima.ToString();
                        TextBoxKeterangan.Text = "Referensi Transfer #" + DataTransferProduk.IDTransferProduk + " - " + DataTransferProduk.Keterangan;

                        #region MEMBUAT TRANSFER PRODUK BARU
                        TransferProduk_Class TransferProduk = new TransferProduk_Class();
                        var TransferProdukBaru = TransferProduk.Tambah(db, Pengguna.IDPengguna, DropDownListTempatPengirim.SelectedValue.ToInt(), DropDownListTempatPenerima.SelectedValue.ToInt(), TextBoxKeterangan.Text);

                        LabelIDTransferProduk.Text = TransferProdukBaru.IDTransferProduk;
                        #endregion

                        Dictionary<int, int> ListStokProdukHabis = new Dictionary<int, int>();
                        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                        //COPY DETAIL TRANSFER LAMA KE DETAIL TRANSFER BARU
                        foreach (var item in DataTransferProduk.TBTransferProdukDetails.ToArray())
                        {
                            //PENCARIAN STOK PRODUK
                            var StokProduk = StokProduk_Class.Cari(DropDownListTempatPengirim.SelectedValue.ToInt(), item.IDKombinasiProduk);

                            if (item.Jumlah <= StokProduk.Jumlah)
                            {
                                //JIKA JUMLAH PERMINTAAN LEBIH KECIL ATAU SAMA DENGAN JUMLAH STOK
                                StokProduk_Class.BertambahBerkurang(DropDownListTempatPengirim.SelectedValue.ToInt(), Pengguna.IDPengguna, StokProduk, item.Jumlah, StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.TransferStokKeluar, "Transfer #" + LabelIDTransferProduk.Text);

                                //MENAMBAHKAN DI DETAIL TRANSFER
                                TransferProdukBaru.TBTransferProdukDetails.Add(new TBTransferProdukDetail
                                {
                                    //IDTRANSFERPRODUKDETAIL
                                    //IDTRANSFERPRODUK
                                    IDKombinasiProduk = item.IDKombinasiProduk,
                                    HargaBeli = StokProduk.HargaBeli.Value,
                                    HargaJual = StokProduk.HargaJual.Value,
                                    Jumlah = item.Jumlah
                                    //SUBTOTALHARGABELI
                                    //SUBTOTALHARGAJUAL
                                });
                            }
                            else //MENCATAT STOK PRODUK YANG HABIS
                                ListStokProdukHabis.Add(StokProduk.IDKombinasiProduk, item.Jumlah);
                        }

                        //UPDATE DATA TRANSFERPRODUK
                        TransferProdukBaru.TanggalUpdate = DateTime.Now;
                        TransferProdukBaru.TotalJumlah = TransferProdukBaru.TBTransferProdukDetails.Sum(item2 => item2.Jumlah);
                        TransferProdukBaru.GrandTotalHargaBeli = TransferProdukBaru.TBTransferProdukDetails.Sum(item2 => item2.Jumlah * item2.HargaBeli);
                        TransferProdukBaru.GrandTotalHargaJual = TransferProdukBaru.TBTransferProdukDetails.Sum(item2 => item2.Jumlah * item2.HargaJual);

                        db.SubmitChanges();

                        LoadDataTransferProduk();

                        //RESET LITERAL WARNING
                        LiteralWarning.Text = "";

                        if (ListStokProdukHabis.Count > 0)
                        {
                            #region LIST STOK HABIS ADA MAKA MENAMPILKAN STOK APA SAJA YANG HABIS
                            var DataStokProduk = db.TBStokProduks
                                .AsEnumerable()
                                .Where(item =>
                                    item.IDTempat == Pengguna.IDTempat &&
                                    ListStokProdukHabis.ContainsKey(item.IDKombinasiProduk))
                                .Select(item => new
                                {
                                    IDStokProduk = item.IDStokProduk,
                                    Kode = item.TBKombinasiProduk.KodeKombinasiProduk,

                                    IDAtribut = item.TBKombinasiProduk.IDAtributProduk,
                                    Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,

                                    IDKategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk : 0,
                                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",

                                    IDPemilikProduk = item.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                                    PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,

                                    IDWarna = item.TBKombinasiProduk.TBProduk.IDWarna,
                                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,

                                    IDKombinasiProduk = item.IDKombinasiProduk,
                                    KombinasiProduk = item.TBKombinasiProduk.Nama,

                                    IDProduk = item.TBKombinasiProduk.IDProduk,
                                    Produk = item.TBKombinasiProduk.TBProduk.Nama,

                                    Jumlah = item.Jumlah,

                                    HargaBeli = item.HargaBeli,
                                    HargaJual = item.HargaJual
                                }).ToArray();

                            RepeaterStokKombinasiProduk.DataSource = DataStokProduk;
                            RepeaterStokKombinasiProduk.DataBind();

                            if (DataStokProduk.Count() > 0)
                                LabelTotalJumlahStok.Text = DataStokProduk.Sum(item => item.Jumlah).ToFormatHargaBulat();
                            else
                                LabelTotalJumlahStok.Text = "0";
                            #endregion

                            #region MENGISI TEXTBOX DENGAN JUMLAH PERMINTAAN
                            foreach (RepeaterItem item in RepeaterStokKombinasiProduk.Items)
                            {
                                Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                                TextBox TextBoxJumlahTransfer = (TextBox)item.FindControl("TextBoxJumlahTransfer");
                                HtmlTableRow panelStok = (HtmlTableRow)item.FindControl("panelStok");

                                if (ListStokProdukHabis.ContainsKey(LabelIDKombinasiProduk.Text.ToInt()))
                                {
                                    TextBoxJumlahTransfer.Text = ListStokProdukHabis[LabelIDKombinasiProduk.Text.ToInt()].ToFormatHargaBulat();
                                    panelStok.Attributes.Add("class", "danger");
                                }
                            }
                            #endregion

                            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Stok tidak cukup, silahkan cek kembali");

                            MultiViewTransferProduk.ActiveViewIndex = 0;
                        }
                        else
                        {
                            //JIKA STOK PRODUK TIDAK HABIS
                            LoadDataStokProduk();
                            MultiViewTransferProduk.ActiveViewIndex = 1;
                        }
                    }
                    else
                    {
                        //TRANSFER PRODUK : PENDING
                        LabelIDTransferProduk.Text = DataTransferProduk.IDTransferProduk;
                        LoadDataTransferProduk();
                        MultiViewTransferProduk.ActiveViewIndex = 1;
                    }
                }
                else
                {
                    //TRANSFER PRODUK TIDAK DITEMUKAN MEMBUAT TRANSFER BARU
                    MultiViewTransferProduk.ActiveViewIndex = 0;
                    LoadDataStokProduk();
                }
            }
        }
        else
            LiteralWarning.Text = "";
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    #region STOK BAHAN BAKU
    private void LoadDataStokProduk()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var DataStokProduk = db.TBStokProduks
                .Where(item => item.IDTempat == Pengguna.IDTempat)
                .Select(item => new
                {
                    IDStokProduk = item.IDStokProduk,
                    Kode = item.TBKombinasiProduk.KodeKombinasiProduk,

                    IDAtribut = item.TBKombinasiProduk.IDAtributProduk,
                    Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,

                    IDKategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().IDKategoriProduk : 0,
                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",

                    IDPemilikProduk = item.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                    PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,

                    IDWarna = item.TBKombinasiProduk.TBProduk.IDWarna,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,

                    IDKombinasiProduk = item.IDKombinasiProduk,
                    KombinasiProduk = item.TBKombinasiProduk.Nama,

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
                DataStokProduk = DataStokProduk.Where(item => item.IDAtribut == DropDownListCariAtributProduk.SelectedValue.ToInt()).ToArray();
                DropDownListCariAtributProduk.Focus();
            }

            if (DropDownListCariKategori.SelectedValue != "0")
            {
                DataStokProduk = DataStokProduk.Where(item => item.Kategori == DropDownListCariKategori.SelectedItem.Text).ToArray();
                DropDownListCariKategori.Focus();
            }

            RepeaterStokKombinasiProduk.DataSource = DataStokProduk;
            RepeaterStokKombinasiProduk.DataBind();

            if (DataStokProduk.Count() > 0)
                LabelTotalJumlahStok.Text = DataStokProduk.Sum(item => item.Jumlah).ToFormatHargaBulat();
            else
                LabelTotalJumlahStok.Text = "0";
        }
    }

    protected void Event_LoadData(object sender, EventArgs e)
    {
        LoadDataStokProduk();
    }

    protected void ButtonTransferSemua_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in RepeaterStokKombinasiProduk.Items)
        {
            Label LabelJumlah = (Label)item.FindControl("LabelJumlah");
            TextBox TextBoxJumlahTransfer = (TextBox)item.FindControl("TextBoxJumlahTransfer");

            TextBoxJumlahTransfer.Text = LabelJumlah.Text.ToFormatHargaBulat();
        }
    }

    protected void ButtonDataTransfer_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(LabelIDTransferProduk.Text))
        {
            LoadDataTransferProduk();
            MultiViewTransferProduk.ActiveViewIndex = 1;
        }
        else
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Tidak ada data transfer");
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownListTempatPenerima.Items.Count == 0)
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Anda harus membuat lokasi tujuan transfer. <a href='/WITAdministrator/Store/Tempat/Pengaturan.aspx'>Buat lokasi baru</a>");
                return;
            }

            bool StokKurang = false;
            string TransferBerhasil = string.Empty;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
                TBTransferProduk DataTransferProduk;

                if (string.IsNullOrWhiteSpace(LabelIDTransferProduk.Text))
                {
                    #region MEMBUAT TRANSFER PRODUK BARU
                    TransferProduk_Class TransferProduk = new TransferProduk_Class();
                    DataTransferProduk = TransferProduk.Tambah(db, Pengguna.IDPengguna, DropDownListTempatPengirim.SelectedValue.ToInt(), DropDownListTempatPenerima.SelectedValue.ToInt(), TextBoxKeterangan.Text);

                    LabelIDTransferProduk.Text = DataTransferProduk.IDTransferProduk;
                    #endregion
                }
                else
                    DataTransferProduk = db.TBTransferProduks.FirstOrDefault(item2 => item2.IDTransferProduk == LabelIDTransferProduk.Text);

                foreach (RepeaterItem item in RepeaterStokKombinasiProduk.Items)
                {
                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    Label LabelJumlah = (Label)item.FindControl("LabelJumlah");
                    TextBox TextBoxJumlahTransfer = (TextBox)item.FindControl("TextBoxJumlahTransfer");
                    HtmlTableRow panelStok = (HtmlTableRow)item.FindControl("panelStok");

                    if (TextBoxJumlahTransfer.Text.ToDecimal().ToInt() > 0)
                    {
                        //JIKA JUMLAH TRANSFER VALID TIDAK NULL DAN TIDAK 0
                        int JumlahTransfer = TextBoxJumlahTransfer.Text.ToDecimal().ToInt();

                        //PENCARIAN STOK PRODUK
                        var StokProduk = StokProduk_Class.Cari(DropDownListTempatPengirim.SelectedValue.ToInt(), LabelIDKombinasiProduk.Text.ToInt());

                        if (JumlahTransfer <= StokProduk.Jumlah)
                        {
                            //JIKA JUMLAH YANG AKAN DI TRANSFER LEBIH KECIL ATAU SAMA DENGAN JUMLAH STOK
                            StokProduk_Class.BertambahBerkurang(DropDownListTempatPengirim.SelectedValue.ToInt(), Pengguna.IDPengguna, StokProduk, JumlahTransfer, StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.TransferStokKeluar, "Transfer #" + LabelIDTransferProduk.Text);

                            //PENGECEKAN APAKAH SUDAH ADA DI DETAIL
                            var TransferProdukDetail = DataTransferProduk.TBTransferProdukDetails
                                .FirstOrDefault(item2 => item2.IDKombinasiProduk == StokProduk.IDKombinasiProduk);

                            if (TransferProdukDetail == null) //DETAIL TRANSFER PRODUK TIDAK DITEMUKAN MEMBUAT BARU
                                DataTransferProduk.TBTransferProdukDetails.Add(new TBTransferProdukDetail
                                {
                                    IDKombinasiProduk = StokProduk.IDKombinasiProduk,
                                    HargaBeli = StokProduk.HargaBeli.Value,
                                    HargaJual = StokProduk.HargaJual.Value,
                                    Jumlah = JumlahTransfer
                                });
                            else //JIKA SUDAH TERDAPAT DI DETAIL HANYA MENAMBAHKAN QTY
                                TransferProdukDetail.Jumlah += JumlahTransfer;

                            //MENGKOSONGKAN TEXTBOX - LABEL JUMLAH DIISI DENGAN JUMLAH STOK TERBARU
                            TextBoxJumlahTransfer.Text = string.Empty;
                            panelStok.Attributes.Add("class", "");
                            LabelJumlah.Text = (LabelJumlah.Text.ToDecimal().ToInt() - JumlahTransfer).ToFormatHargaBulat();
                            LabelTotalJumlahStok.Text = (LabelTotalJumlahStok.Text.ToDecimal().ToInt() - JumlahTransfer).ToFormatHargaBulat();

                            TransferBerhasil += "<br/>" + JumlahTransfer.ToFormatHargaBulat() + " - " + StokProduk.TBKombinasiProduk.Nama; //MESSAGE TRANSFER YANG BERHASIL
                        }
                        else
                        {
                            //REFRESH LABEL JUMLAH STOK
                            LabelTotalJumlahStok.Text = (LabelTotalJumlahStok.Text.ToDecimal().ToInt() - (LabelJumlah.Text.ToDecimal().ToInt() - StokProduk.Jumlah.Value)).ToFormatHargaBulat();
                            LabelJumlah.Text = StokProduk.Jumlah.ToFormatHargaBulat();
                            panelStok.Attributes.Add("class", "danger");

                            StokKurang = true;
                        }
                    }

                    //HANDLE POSTBACK FORMAT HARGA
                    TextBoxJumlahTransfer.Text = TextBoxJumlahTransfer.Text.ToDecimal().ToInt() == 0 ? "" : TextBoxJumlahTransfer.Text.ToDecimal().ToInt().ToString();
                }

                //UPDATE DATA TRANSFER PRODUK
                DataTransferProduk.TanggalUpdate = DateTime.Now;
                DataTransferProduk.TotalJumlah = DataTransferProduk.TBTransferProdukDetails.Sum(item2 => item2.Jumlah);
                DataTransferProduk.GrandTotalHargaBeli = DataTransferProduk.TBTransferProdukDetails.Sum(item2 => item2.Jumlah * item2.HargaBeli);
                DataTransferProduk.GrandTotalHargaJual = DataTransferProduk.TBTransferProdukDetails.Sum(item2 => item2.Jumlah * item2.HargaJual);

                db.SubmitChanges();
            }

            //MENAMPILKAN MESSAGE
            LiteralWarning.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(TransferBerhasil))
                LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Success, "Produk berhasil disimpan" + TransferBerhasil);

            if (StokKurang)
                LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Danger, "Stok tidak cukup, silahkan cek kembali");
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
    #endregion

    #region TRANSFER DETAIL
    private void LoadDataTransferProduk()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var TransferProdukDetail = db.TBTransferProdukDetails
                .Where(item => item.IDTransferProduk == LabelIDTransferProduk.Text)
                .GroupBy(item => item.TBKombinasiProduk.TBProduk)
                        .Select(item => new
                        {
                            Produk = item.Key.Nama,
                            Kategori = item.Key.TBRelasiProdukKategoriProduks.Count > 0 ? item.Key.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            Count = item.Count(),
                            Body = item.Select(item2 => new
                            {
                                item2.IDTransferProdukDetail,
                                Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                                AtributProduk = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                                item2.HargaJual,
                                item2.Jumlah,
                                item2.SubtotalHargaJual
                            }),
                            Jumlah = item.Sum(item2 => item2.Jumlah),
                            SubtotalHargaJual = item.Sum(item2 => item2.SubtotalHargaJual)
                        })
                .ToArray();

            RepeaterTransferKombinasiProduk.DataSource = TransferProdukDetail;
            RepeaterTransferKombinasiProduk.DataBind();

            LabelTotalJumlah.Text = TransferProdukDetail.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelTotalSubtotalHargaJual.Text = TransferProdukDetail.Sum(item => item.SubtotalHargaJual).ToFormatHarga();
        }
    }

    protected void ButtonDataStokProduk_Click(object sender, EventArgs e)
    {
        MultiViewTransferProduk.ActiveViewIndex = 0;
        LoadDataStokProduk();
    }

    protected void ButtonTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataTransferProduk = db.TBTransferProduks.FirstOrDefault(item => item.IDTransferProduk == LabelIDTransferProduk.Text);

                if (DataTransferProduk != null)
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    //MERUBAH STATUS DAN EDIT DATA TRANSFER PRODUK

                    //IDTRANSFERPRODUK
                    //NOMOR
                    DataTransferProduk.IDPengirim = Pengguna.IDPengguna;
                    //IDPENERIMA
                    DataTransferProduk.IDTempatPengirim = DropDownListTempatPengirim.SelectedValue.ToInt();
                    DataTransferProduk.IDTempatPenerima = DropDownListTempatPenerima.SelectedValue.ToInt();
                    //TANGGALDAFTAR
                    DataTransferProduk.TanggalUpdate = DateTime.Now;
                    DataTransferProduk.EnumJenisTransfer = (int)PilihanJenisTransfer.TransferProses;
                    DataTransferProduk.TanggalKirim = TextBoxTanggalKirim.Text.ToDateTime();
                    //TANGGALTERIMA
                    DataTransferProduk.TotalJumlah = DataTransferProduk.TBTransferProdukDetails.Sum(item => item.Jumlah);
                    DataTransferProduk.GrandTotalHargaBeli = DataTransferProduk.TBTransferProdukDetails.Sum(item => item.SubtotalHargaBeli.Value);
                    DataTransferProduk.GrandTotalHargaJual = DataTransferProduk.TBTransferProdukDetails.Sum(item => item.SubtotalHargaJual.Value);
                    DataTransferProduk.Keterangan = TextBoxKeterangan.Text;

                    db.SubmitChanges();

                    //MEMBUAT FILE TRANSFER PRODUK
                    TransferProduk_Class TransferProduk = new TransferProduk_Class();
                    TransferProduk.GenerateFile(DataTransferProduk);

                    Response.Redirect("/WITAdministrator/Produk/Transfer/Detail.aspx?id=" + DataTransferProduk.IDTransferProduk, false);
                }
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }

    }
    protected void RepeaterTransferKombinasiProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                var TransferProdukDetail = db.TBTransferProdukDetails.FirstOrDefault(item => item.IDTransferProdukDetail == e.CommandArgument.ToInt());

                if (TransferProdukDetail != null)
                {
                    StokProduk_Class.BertambahBerkurang(Pengguna.IDTempat, Pengguna.IDPengguna, TransferProdukDetail.IDKombinasiProduk, TransferProdukDetail.Jumlah, TransferProdukDetail.HargaBeli, TransferProdukDetail.HargaJual, EnumJenisPerpindahanStok.TransferBatal, "Transfer #" + TransferProdukDetail.IDTransferProduk);

                    db.TBTransferProdukDetails.DeleteOnSubmit(TransferProdukDetail);
                    db.SubmitChanges();

                    //UPDATE DATA TRANSFER PRODUK
                    var TransferProduk = db.TBTransferProduks.FirstOrDefault(item => item.IDTransferProduk == LabelIDTransferProduk.Text);

                    TransferProduk.TanggalUpdate = DateTime.Now;
                    TransferProduk.TotalJumlah = TransferProduk.TBTransferProdukDetails.Sum(item2 => item2.Jumlah);
                    TransferProduk.GrandTotalHargaBeli = TransferProduk.TBTransferProdukDetails.Sum(item2 => item2.Jumlah * item2.HargaBeli);
                    TransferProduk.GrandTotalHargaJual = TransferProduk.TBTransferProdukDetails.Sum(item2 => item2.Jumlah * item2.HargaJual);
                    db.SubmitChanges();

                    LoadDataTransferProduk();
                }
            }
        }
    }
    #endregion

    protected void MultiViewTransferProduk_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiViewTransferProduk.ActiveViewIndex == 0)
        {
            DropDownListJenisStok.Visible = true;
            ButtonTransferSemua.Visible = true;
            ButtonCari.Visible = true;
            ButtonDataStokProduk.Visible = false;
            ButtonDataTransfer.Visible = true;
            ButtonTransfer.Visible = false;
        }
        else
        {
            DropDownListJenisStok.Visible = false;
            ButtonTransferSemua.Visible = false;
            ButtonCari.Visible = false;
            ButtonDataStokProduk.Visible = true;
            ButtonDataTransfer.Visible = false;
            ButtonTransfer.Visible = true;
        }
    }

}