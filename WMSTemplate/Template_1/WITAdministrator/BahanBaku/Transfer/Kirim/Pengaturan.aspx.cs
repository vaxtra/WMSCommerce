using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Transfer_Kirim_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Tempat_Class ClassTempat = new Tempat_Class(db);

                #region Default
                DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "Semua" });
                DropDownListJenisStok.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                DropDownListJenisStok.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                DropDownListJenisStok.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                DropDownListCariKategori.DataSource = db.TBKategoriBahanBakus.ToArray();
                DropDownListCariKategori.DataTextField = "Nama";
                DropDownListCariKategori.DataValueField = "IDKategoriBahanBaku";
                DropDownListCariKategori.DataBind();
                DropDownListCariKategori.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

                DropDownListCariSatuan.DataSource = db.TBSatuans.ToArray();
                DropDownListCariSatuan.DataTextField = "Nama";
                DropDownListCariSatuan.DataValueField = "IDSatuan";
                DropDownListCariSatuan.DataBind();
                DropDownListCariSatuan.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

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

                var DataTransferBahanBaku = db.TBTransferBahanBakus
                    .FirstOrDefault(item => item.IDTransferBahanBaku == Request.QueryString["id"]);

                if (DataTransferBahanBaku != null)
                {
                    //DATA TRANSFER BAHAN BAKU DITEMUKAN

                    if (DataTransferBahanBaku.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferBatal ||
                        DataTransferBahanBaku.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses ||
                        DataTransferBahanBaku.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferSelesai)
                    {
                        //TRANSFER BATAL MAKA COPY TRANSFER BAHAN BAKU LAMA KE TRANSFER BAHAN BAKU BARU
                        TextBoxTanggalKirim.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
                        DropDownListTempatPenerima.SelectedValue = DataTransferBahanBaku.IDTempatPenerima.ToString();
                        TextBoxKeterangan.Text = "Referensi Transfer #" + DataTransferBahanBaku.IDTransferBahanBaku + " - " + DataTransferBahanBaku.Keterangan;

                        #region MEMBUAT TRANSFER BAHAN BAKU BARU
                        TransferBahanBaku_Class TransferBahanBaku = new TransferBahanBaku_Class();
                        var TransferBahanBakuBaru = TransferBahanBaku.Tambah(db, Pengguna.IDPengguna, DropDownListTempatPengirim.SelectedValue.ToInt(), DropDownListTempatPenerima.SelectedValue.ToInt(), TextBoxKeterangan.Text);

                        LabelIDTransferBahanBaku.Text = TransferBahanBakuBaru.IDTransferBahanBaku;
                        #endregion

                        Dictionary<int, decimal> ListStokBahanBakuHabis = new Dictionary<int, decimal>();
                        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == DropDownListTempatPengirim.SelectedValue.ToInt()).ToArray();

                        //COPY DETAIL TRANSFER LAMA KE DETAIL TRANSFER BARU
                        foreach (var item in DataTransferBahanBaku.TBTransferBahanBakuDetails.ToArray())
                        {
                            //PENCARIAN STOK BAHAN BAKU
                            var stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

                            if (item.Jumlah <= (stokBahanBaku.Jumlah / stokBahanBaku.TBBahanBaku.Konversi))
                            {
                                //JIKA JUMLAH PERMINTAAN LEBIH KECIL ATAU SAMA DENGAN JUMLAH STOK
                                StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, item.Jumlah, item.HargaBeli, true, EnumJenisPerpindahanStok.TransferStokKeluar, "Transfer #" + LabelIDTransferBahanBaku.Text);

                                //MENAMBAHKAN DI DETAIL TRANSFER
                                TransferBahanBakuBaru.TBTransferBahanBakuDetails.Add(new TBTransferBahanBakuDetail
                                {
                                    //IDTRANSFERPBAHANBAKUDETAIL
                                    //IDTRANSFERBAHANBAKU
                                    IDBahanBaku = item.IDBahanBaku,
                                    IDSatuan = item.TBBahanBaku.IDSatuanKonversi,
                                    HargaBeli = (stokBahanBaku.HargaBeli.Value * stokBahanBaku.TBBahanBaku.Konversi.Value),
                                    Jumlah = item.Jumlah
                                    //SUBTOTALHARGABELI
                                });
                            }
                            else //MENCATAT STOK BAHAN BAKU YANG HABIS
                                ListStokBahanBakuHabis.Add(stokBahanBaku.IDBahanBaku.Value, item.Jumlah);
                        }

                        //UPDATE DATA TRANSFERBAHANBAKU
                        TransferBahanBakuBaru.TanggalUpdate = DateTime.Now;
                        TransferBahanBakuBaru.TotalJumlah = TransferBahanBakuBaru.TBTransferBahanBakuDetails.Sum(item2 => item2.Jumlah);
                        TransferBahanBakuBaru.GrandTotal = TransferBahanBakuBaru.TBTransferBahanBakuDetails.Sum(item2 => item2.Jumlah * item2.HargaBeli);

                        db.SubmitChanges();

                        LoadDataTransferBahanBaku();

                        //RESET LITERAL WARNING
                        LiteralWarning.Text = "";

                        if (ListStokBahanBakuHabis.Count > 0)
                        {
                            #region LIST STOK HABIS ADA MAKA MENAMPILKAN STOK APA SAJA YANG HABIS
                            var DataStokBahanBaku = db.TBStokBahanBakus
                                .AsEnumerable()
                                .Where(item =>
                                    item.IDTempat == Pengguna.IDTempat &&
                                    ListStokBahanBakuHabis.ContainsKey(item.IDBahanBaku.Value))
                                .Select(item => new
                                {
                                    IDBahanBaku = item.TBBahanBaku.IDBahanBaku,
                                    BahanBaku = item.TBBahanBaku.Nama,
                                    Kode = item.TBBahanBaku.KodeBahanBaku,

                                    IDSatuanKecil = item.TBBahanBaku.IDSatuan,
                                    SatuanKecil = item.TBBahanBaku.TBSatuan.Nama,

                                    Konversi = item.TBBahanBaku.Konversi,

                                    IDSatuanBesar = item.TBBahanBaku.IDSatuanKonversi,
                                    SatuanBesar = item.TBBahanBaku.TBSatuan1.Nama,

                                    IDKategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault().IDKategoriBahanBaku : 0,
                                    Kategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault().TBKategoriBahanBaku.Nama : "",

                                    IDStokBahanBaku = item.IDStokBahanBaku,

                                    Jumlah = (item.Jumlah / item.TBBahanBaku.Konversi),

                                    HargaBeli = (item.HargaBeli * item.TBBahanBaku.Konversi)
                                }).OrderBy(item => item.BahanBaku).ToArray();

                            RepeaterStokBahanBaku.DataSource = DataStokBahanBaku;
                            RepeaterStokBahanBaku.DataBind();
                            #endregion

                            #region MENGISI TEXTBOX DENGAN JUMLAH PERMINTAAN
                            foreach (RepeaterItem item in RepeaterStokBahanBaku.Items)
                            {
                                Label LabelIDBahanBaku = (Label)item.FindControl("LabelIDBahanBaku");
                                TextBox TextBoxJumlahTransfer = (TextBox)item.FindControl("TextBoxJumlahTransfer");
                                HtmlTableRow panelStok = (HtmlTableRow)item.FindControl("panelStok");

                                if (ListStokBahanBakuHabis.ContainsKey(LabelIDBahanBaku.Text.ToInt()))
                                {
                                    TextBoxJumlahTransfer.Text = ListStokBahanBakuHabis[LabelIDBahanBaku.Text.ToInt()].ToFormatHarga();
                                    panelStok.Attributes.Add("class", "danger");
                                }
                            }
                            #endregion

                            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Stok tidak cukup, silahkan cek kembali");

                            MultiViewTransferBahanBaku.ActiveViewIndex = 0;
                        }
                        else
                        {
                            //JIKA STOK BAHAN BAKU TIDAK HABIS
                            LoadDataStokBahanBaku();
                            MultiViewTransferBahanBaku.ActiveViewIndex = 1;
                        }
                    }
                    else
                    {
                        //TRANSFER BAHAN BAKU : PENDING
                        LabelIDTransferBahanBaku.Text = DataTransferBahanBaku.IDTransferBahanBaku;
                        LoadDataTransferBahanBaku();
                        MultiViewTransferBahanBaku.ActiveViewIndex = 1;
                    }
                }
                else
                {
                    //TRANSFER BAHAN BAKU TIDAK DITEMUKAN MEMBUAT TRANSFER BARU
                    MultiViewTransferBahanBaku.ActiveViewIndex = 0;
                    LoadDataStokBahanBaku();
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
    private void LoadDataStokBahanBaku()
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var DataStokBahanBaku = db.TBStokBahanBakus
                .AsEnumerable()
                .Where(item => item.IDTempat == Pengguna.IDTempat)
                .Select(item => new
                {
                    IDBahanBaku = item.TBBahanBaku.IDBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    Kode = item.TBBahanBaku.KodeBahanBaku,

                    IDSatuanKecil = item.TBBahanBaku.IDSatuan,
                    SatuanKecil = item.TBBahanBaku.TBSatuan.Nama,

                    Konversi = item.TBBahanBaku.Konversi,

                    IDSatuanBesar = item.TBBahanBaku.IDSatuanKonversi,
                    SatuanBesar = item.TBBahanBaku.TBSatuan1.Nama,

                    IDKategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault().IDKategoriBahanBaku : 0,
                    Kategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault().TBKategoriBahanBaku.Nama : "",

                    IDStokBahanBaku = item.IDStokBahanBaku,

                    Jumlah = (item.Jumlah / item.TBBahanBaku.Konversi),

                    HargaBeli = (item.HargaBeli * item.TBBahanBaku.Konversi)
                }).OrderBy(item => item.BahanBaku).ToArray();



            if (DropDownListJenisStok.SelectedValue == "1")
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.Jumlah > 0).ToArray();
            else if (DropDownListJenisStok.SelectedValue == "2")
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.Jumlah == 0).ToArray();
            else if (DropDownListJenisStok.SelectedValue == "3")
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.Jumlah < 0).ToArray();

            if (!string.IsNullOrWhiteSpace(TextBoxCariKode.Text))
            {
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.Kode.ToLower().Contains(TextBoxCariKode.Text.ToLower())).ToArray();
                TextBoxCariKode.Focus();
            }

            if (!string.IsNullOrWhiteSpace(TextBoxCariBahanBaku.Text))
            {
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.BahanBaku.ToLower().Contains(TextBoxCariBahanBaku.Text.ToLower())).ToArray();
                TextBoxCariBahanBaku.Focus();
            }

            if (DropDownListCariSatuan.SelectedValue != "0")
            {
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.IDSatuanBesar == DropDownListCariSatuan.SelectedValue.ToInt()).ToArray();
                DropDownListCariSatuan.Focus();
            }

            if (DropDownListCariKategori.SelectedValue != "0")
            {
                DataStokBahanBaku = DataStokBahanBaku.Where(item => item.Kategori == DropDownListCariKategori.SelectedItem.Text).ToArray();
                DropDownListCariKategori.Focus();
            }

            RepeaterStokBahanBaku.DataSource = DataStokBahanBaku;
            RepeaterStokBahanBaku.DataBind();
        }
    }
    protected void Event_LoadData(object sender, EventArgs e)
    {
        LoadDataStokBahanBaku();
    }
    protected void ButtonTransferSemua_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in RepeaterStokBahanBaku.Items)
        {
            Label LabelJumlah = (Label)item.FindControl("LabelJumlah");
            TextBox TextBoxJumlahTransfer = (TextBox)item.FindControl("TextBoxJumlahTransfer");

            TextBoxJumlahTransfer.Text = LabelJumlah.Text;
        }
    }
    protected void ButtonDataTransfer_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(LabelIDTransferBahanBaku.Text))
        {
            LoadDataTransferBahanBaku();
            MultiViewTransferBahanBaku.SetActiveView(ViewTransferBahanBaku);
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
                TBTransferBahanBaku DataTransferBahanBaku = null;
                TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(data => data.IDTempat == DropDownListTempatPengirim.SelectedValue.ToInt()).ToArray();
                TransferBahanBaku_Class TransferBahanBaku = new TransferBahanBaku_Class();

                foreach (RepeaterItem item in RepeaterStokBahanBaku.Items)
                {
                    Label LabelIDBahanBaku = (Label)item.FindControl("LabelIDBahanBaku");
                    Label LabelJumlah = (Label)item.FindControl("LabelJumlah");
                    TextBox TextBoxJumlahTransfer = (TextBox)item.FindControl("TextBoxJumlahTransfer");
                    HtmlTableRow panelStok = (HtmlTableRow)item.FindControl("panelStok");

                    if (TextBoxJumlahTransfer.Text.ToDecimal() > 0)
                    {
                        //JIKA JUMLAH TRANSFER VALID TIDAK NULL DAN TIDAK 0
                        decimal JumlahTransfer = TextBoxJumlahTransfer.Text.ToDecimal();

                        //PENCARIAN STOK BAHAN BAKU
                        var stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == LabelIDBahanBaku.Text.ToInt());

                        if (JumlahTransfer <= (stokBahanBaku.Jumlah / stokBahanBaku.TBBahanBaku.Konversi))
                        {
                            if (string.IsNullOrWhiteSpace(LabelIDTransferBahanBaku.Text))
                            {
                                #region MEMBUAT TRANSFER BAHAN BAKU BARU
                                DataTransferBahanBaku = TransferBahanBaku.Tambah(db, Pengguna.IDPengguna, DropDownListTempatPengirim.SelectedValue.ToInt(), DropDownListTempatPenerima.SelectedValue.ToInt(), TextBoxKeterangan.Text);

                                LabelIDTransferBahanBaku.Text = DataTransferBahanBaku.IDTransferBahanBaku;
                                #endregion
                            }
                            else
                                DataTransferBahanBaku = db.TBTransferBahanBakus.FirstOrDefault(item2 => item2.IDTransferBahanBaku == LabelIDTransferBahanBaku.Text);

                            //JIKA JUMLAH YANG AKAN DI TRANSFER LEBIH KECIL ATAU SAMA DENGAN JUMLAH STOK
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, JumlahTransfer, (stokBahanBaku.HargaBeli.Value * stokBahanBaku.TBBahanBaku.Konversi.Value), true, EnumJenisPerpindahanStok.TransferStokKeluar, "Transfer #" + LabelIDTransferBahanBaku.Text);

                            //PENGECEKAN APAKAH SUDAH ADA DI DETAIL
                            var TransferBahanBakuDetail = DataTransferBahanBaku.TBTransferBahanBakuDetails
                                .FirstOrDefault(item2 => item2.IDBahanBaku == stokBahanBaku.IDBahanBaku);

                            if (TransferBahanBakuDetail == null) //DETAIL TRANSFER BAHAN BAKU TIDAK DITEMUKAN MEMBUAT BARU
                                DataTransferBahanBaku.TBTransferBahanBakuDetails.Add(new TBTransferBahanBakuDetail
                                {
                                    IDBahanBaku = stokBahanBaku.IDBahanBaku.Value,
                                    IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuanKonversi,
                                    HargaBeli = (stokBahanBaku.HargaBeli.Value * stokBahanBaku.TBBahanBaku.Konversi.Value),
                                    Jumlah = JumlahTransfer
                                });
                            else //JIKA SUDAH TERDAPAT DI DETAIL HANYA MENAMBAHKAN QTY
                                TransferBahanBakuDetail.Jumlah += JumlahTransfer;

                            //MENGKOSONGKAN TEXTBOX - LABEL JUMLAH DIISI DENGAN JUMLAH STOK TERBARU
                            TextBoxJumlahTransfer.Text = string.Empty;
                            panelStok.Attributes.Add("class", "");
                            LabelJumlah.Text = (LabelJumlah.Text.ToDecimal() - JumlahTransfer).ToFormatHarga();

                            TransferBerhasil += "<br/>" + JumlahTransfer.ToFormatHarga() + " - " + stokBahanBaku.TBBahanBaku.Nama + " " + stokBahanBaku.TBBahanBaku.TBSatuan1.Nama; //MESSAGE TRANSFER YANG BERHASIL
                        }
                        else
                        {
                            //REFRESH LABEL JUMLAH STOK
                            LabelJumlah.Text = (stokBahanBaku.Jumlah / stokBahanBaku.TBBahanBaku.Konversi.Value).ToFormatHarga();
                            panelStok.Attributes.Add("class", "danger");

                            StokKurang = true;
                        }
                    }

                    //HANDLE POSTBACK FORMAT HARGA
                    TextBoxJumlahTransfer.Text = TextBoxJumlahTransfer.Text.ToDecimal() == 0 ? string.Empty : TextBoxJumlahTransfer.Text.ToFormatHarga();
                }

                if (DataTransferBahanBaku != null)
                {
                    //UPDATE DATA TRANSFER BAHAN BAKU
                    DataTransferBahanBaku.TanggalUpdate = DateTime.Now;
                    DataTransferBahanBaku.TotalJumlah = DataTransferBahanBaku.TBTransferBahanBakuDetails.Sum(item2 => item2.Jumlah);
                    DataTransferBahanBaku.GrandTotal = DataTransferBahanBaku.TBTransferBahanBakuDetails.Sum(item2 => item2.Jumlah * item2.HargaBeli);
                }

                db.SubmitChanges();
            }

            //MENAMPILKAN MESSAGE
            LiteralWarning.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(TransferBerhasil))
                LiteralWarning.Text += Alert_Class.Pesan(TipeAlert.Success, "Bahan Baku berhasil disimpan" + TransferBerhasil);

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
    private void LoadDataTransferBahanBaku()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var TransferBahanBakuDetail = db.TBTransferBahanBakuDetails
                .Where(item => item.IDTransferBahanBaku == LabelIDTransferBahanBaku.Text)
                .Select(item => new
                {
                    item.IDTransferBahanBakuDetail,
                    Kode = item.TBBahanBaku.KodeBahanBaku,
                    item.TBBahanBaku.Nama,
                    SatuanBesar = item.TBSatuan.Nama,
                    Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(db, null, item.TBBahanBaku),
                    item.HargaBeli,
                    item.Jumlah,
                    item.Subtotal
                }).OrderBy(item => item.Nama).ToArray();

            RepeaterTransferBahanBaku.DataSource = TransferBahanBakuDetail;
            RepeaterTransferBahanBaku.DataBind();

            LabelTotalSubtotal.Text = TransferBahanBakuDetail.Sum(item => item.Subtotal).ToFormatHarga();
        }
    }

    protected void ButtonDataStokBahanBaku_Click(object sender, EventArgs e)
    {
        MultiViewTransferBahanBaku.SetActiveView(ViewStokBahanBaku);
        LoadDataStokBahanBaku();
    }
    protected void ButtonTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataTransferBahanBaku = db.TBTransferBahanBakus.FirstOrDefault(item => item.IDTransferBahanBaku == LabelIDTransferBahanBaku.Text);

                if (DataTransferBahanBaku != null)
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    //MERUBAH STATUS DAN EDIT DATA TRANSFER BAHAN BAKU

                    //IDTRANSFERBAHANBAKU
                    //NOMOR
                    DataTransferBahanBaku.IDPengirim = Pengguna.IDPengguna;
                    //IDPENERIMA
                    DataTransferBahanBaku.IDTempatPengirim = DropDownListTempatPengirim.SelectedValue.ToInt();
                    DataTransferBahanBaku.IDTempatPenerima = DropDownListTempatPenerima.SelectedValue.ToInt();
                    //TANGGALDAFTAR
                    DataTransferBahanBaku.TanggalUpdate = DateTime.Now;
                    DataTransferBahanBaku.EnumJenisTransfer = (int)PilihanJenisTransfer.TransferProses;
                    DataTransferBahanBaku.TanggalKirim = DateTime.Parse(TextBoxTanggalKirim.Text);
                    //TANGGALTERIMA
                    DataTransferBahanBaku.TotalJumlah = DataTransferBahanBaku.TBTransferBahanBakuDetails.Sum(item => item.Jumlah);
                    DataTransferBahanBaku.GrandTotal = DataTransferBahanBaku.TBTransferBahanBakuDetails.Sum(item => item.Subtotal.Value);
                    DataTransferBahanBaku.Keterangan = TextBoxKeterangan.Text;

                    db.SubmitChanges();

                    //MEMBUAT FILE TRANSFER BAHAN BAKU
                    TransferBahanBaku_Class TransferBahanBaku = new TransferBahanBaku_Class();
                    TransferBahanBaku.GenerateFile(DataTransferBahanBaku);

                    Response.Redirect("Detail.aspx?id=" + DataTransferBahanBaku.IDTransferBahanBaku, false);
                }
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
    protected void RepeaterTransferBahanBaku_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TBTransferBahanBaku transferBahanBaku = db.TBTransferBahanBakus.FirstOrDefault(item => item.IDTransferBahanBaku == LabelIDTransferBahanBaku.Text);

                TBTransferBahanBakuDetail transferBahanBakuDetail = transferBahanBaku.TBTransferBahanBakuDetails.FirstOrDefault(item => item.IDTransferBahanBakuDetail == e.CommandArgument.ToInt());
                TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(data => data.IDTempat == DropDownListTempatPengirim.SelectedValue.ToInt()).ToArray();

                if (transferBahanBakuDetail != null)
                {
                    var stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == transferBahanBakuDetail.IDBahanBaku);

                    StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, transferBahanBakuDetail.Jumlah, transferBahanBakuDetail.HargaBeli, true, EnumJenisPerpindahanStok.TransferBatal, "Transfer Batal Item #" + LabelIDTransferBahanBaku.Text);

                    db.TBTransferBahanBakuDetails.DeleteOnSubmit(transferBahanBakuDetail);
                    transferBahanBaku.TBTransferBahanBakuDetails.Remove(transferBahanBakuDetail);
                    //db.SubmitChanges();

                    //UPDATE DATA TRANSFER BAHAN BAKU
                    

                    transferBahanBaku.TanggalUpdate = DateTime.Now;
                    transferBahanBaku.TotalJumlah = transferBahanBaku.TBTransferBahanBakuDetails.Sum(item2 => item2.Jumlah);
                    transferBahanBaku.GrandTotal = transferBahanBaku.TBTransferBahanBakuDetails.Sum(item2 => item2.Jumlah * item2.HargaBeli);
                    db.SubmitChanges();

                    LoadDataTransferBahanBaku();
                }
            }
        }
    }
    #endregion

    protected void MultiViewTransferBahanBaku_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiViewTransferBahanBaku.ActiveViewIndex == 0)
        {
            DropDownListJenisStok.Visible = true;
            ButtonTransferSemua.Visible = true;
            ButtonCari.Visible = true;
            ButtonDataStokBahanBaku.Visible = false;
            ButtonDataTransfer.Visible = true;
            ButtonTransfer.Visible = false;
        }
        else
        {
            DropDownListJenisStok.Visible = false;
            ButtonTransferSemua.Visible = false;
            ButtonCari.Visible = false;
            ButtonDataStokBahanBaku.Visible = true;
            ButtonDataTransfer.Visible = false;
            ButtonTransfer.Visible = true;
        }
    }
}