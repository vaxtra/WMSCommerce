using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataJavaScript(db);
                KategoriBahanBaku_Class KategoriBahanBaku_Class = new KategoriBahanBaku_Class();
                TBBahanBaku bahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Request.QueryString["id"].ToInt());

                if (bahanBaku != null)
                {
                    TextBoxNama.Text = bahanBaku.Nama;
                    TextBoxKodeBahanBaku.Text = bahanBaku.KodeBahanBaku;
                    TextBoxSatuanBesar.Text = bahanBaku.TBSatuan1.Nama;
                    TextBoxSatuanKecil.Text = bahanBaku.TBSatuan.Nama;
                    TextBoxKonversi.Text = bahanBaku.Konversi.ToFormatHarga();
                    TextBoxBerat.Text = bahanBaku.Berat.ToString();
                    TextBoxKategori.Text = KategoriBahanBaku_Class.Data(bahanBaku.TBRelasiBahanBakuKategoriBahanBakus.ToArray());
                    TextBoxDeskripsi.Text = bahanBaku.Deskripsi;

                    TBStokBahanBaku stokBahanBaku = bahanBaku.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == pengguna.IDTempat);
                    if (stokBahanBaku != null)
                    {
                        TextBoxHargaBeli.Text = (stokBahanBaku.HargaBeli * bahanBaku.Konversi).ToFormatHarga();
                        TextBoxStok.Text = (stokBahanBaku.Jumlah / bahanBaku.Konversi).ToFormatHarga();
                        TextBoxBatasStokAkanHabis.Text = (stokBahanBaku.JumlahMinimum / bahanBaku.Konversi).ToFormatHarga();
                    }

                    LabelSatuanHargaBeli.Text = "/" + TextBoxSatuanBesar.Text;
                    LabelSatuanKonversi.Text = TextBoxSatuanKecil.Text;
                    LabelSatuanStok.Text = TextBoxSatuanBesar.Text;
                    LabelSatuanStokAkanHabis.Text = TextBoxSatuanBesar.Text;

                    ButtonSimpan.Text = "Ubah";
                    LabelKeterangan.Text = "Ubah";
                }
                else
                    LabelKeterangan.Text = "Tambah";
            }
        }


    }

    #region PELENGKAP
    protected void TextBoxKodeBahanBaku_TextChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LabelPeringatanKodeBahanBaku.Text = "-";
            LabelPeringatanKodeBahanBaku.Visible = false;

            if (TextBoxKodeBahanBaku.Text != "")
            {
                if (ButtonSimpan.Text == "Tambah")
                {
                    var bahanBaku = db.TBBahanBakus.Select(item => new { item.KodeBahanBaku }).FirstOrDefault(item => item.KodeBahanBaku == TextBoxKodeBahanBaku.Text);

                    if (bahanBaku != null)
                    {
                        LabelPeringatanKodeBahanBaku.Text = "Kode Bahan Baku sudah digunakan";
                        LabelPeringatanKodeBahanBaku.Visible = true;
                    }
                }
                else if (ButtonSimpan.Text == "Ubah")
                {
                    var bahanBaku = db.TBBahanBakus
                        .Select(item => new { item.IDBahanBaku, item.KodeBahanBaku })
                        .FirstOrDefault(item => item.IDBahanBaku.ToString() != Request.QueryString["id"].ToString() && item.KodeBahanBaku == TextBoxKodeBahanBaku.Text);

                    if (bahanBaku != null)
                    {
                        LabelPeringatanKodeBahanBaku.Text = "Kode Bahan Baku sudah digunakan";
                        LabelPeringatanKodeBahanBaku.Visible = true;
                    }
                }
            }
            else
            {
                LabelPeringatanKodeBahanBaku.Text = "Kode Bahan Baku harus diisi";
                LabelPeringatanKodeBahanBaku.Visible = true;
            }
        }
    }

    protected void TextBoxSatuanBesar_TextChanged(object sender, EventArgs e)
    {
        LabelSatuanHargaBeli.Text = "/" + TextBoxSatuanBesar.Text;
        LabelSatuanStok.Text = TextBoxSatuanBesar.Text;
        LabelSatuanStokAkanHabis.Text = TextBoxSatuanBesar.Text;
    }

    protected void TextBoxSatuanKecil_TextChanged(object sender, EventArgs e)
    {
        LabelSatuanKonversi.Text = TextBoxSatuanKecil.Text;
    }
    #endregion

    #region MASTER
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                KategoriBahanBaku_Class KategoriBahanBaku_Class = new KategoriBahanBaku_Class();
                Satuan_Class Satuan_Class = new Satuan_Class();

                TBBahanBaku bahanBaku;
                decimal hargaBeli = TextBoxHargaBeli.Text.ToDecimal() / TextBoxKonversi.Text.ToDecimal();
                decimal stok = TextBoxStok.Text.ToDecimal() * TextBoxKonversi.Text.ToDecimal();
                decimal batasStok = TextBoxBatasStokAkanHabis.Text.ToDecimal() * TextBoxKonversi.Text.ToDecimal();

                if (ButtonSimpan.Text == "Tambah")
                {

                    TBSatuan satuanKecil = Satuan_Class.CariTambah(db, TextBoxSatuanKecil.Text);
                    TBSatuan satuanBesar = Satuan_Class.CariTambah(db, TextBoxSatuanBesar.Text);

                    bahanBaku = new TBBahanBaku
                    {
                        IDWMS = Guid.NewGuid(),
                        TBSatuan = satuanKecil,
                        TBSatuan1 = satuanBesar,
                        TanggalDaftar = DateTime.Now,
                        TanggalUpdate = DateTime.Now,
                        Urutan = null,
                        KodeBahanBaku = TextBoxKodeBahanBaku.Text,
                        Nama = TextBoxNama.Text,
                        Berat = TextBoxBerat.Text.ToDecimal(),
                        Konversi = TextBoxKonversi.Text.ToDecimal(),
                        Deskripsi = TextBoxDeskripsi.Text
                    };
                    db.TBBahanBakus.InsertOnSubmit(bahanBaku);

                    KategoriBahanBaku_Class.KategoriBahanBaku(db, bahanBaku, TextBoxKategori.Text);

                    TBStokBahanBaku stokBahanBaku = StokBahanBaku_Class.InsertStokBahanBaku(db, DateTime.Now, pengguna.IDPengguna, pengguna.IDTempat, bahanBaku, hargaBeli, stok, batasStok, "Stok Baru Manual");
                }
                else if (ButtonSimpan.Text == "Ubah")
                {
                    TBSatuan satuanKecil = Satuan_Class.CariTambah(db, TextBoxSatuanKecil.Text);
                    TBSatuan satuanBesar = Satuan_Class.CariTambah(db, TextBoxSatuanBesar.Text);

                    bahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Request.QueryString["id"].ToInt());
                    bahanBaku.TBSatuan = satuanKecil;
                    bahanBaku.TBSatuan1 = satuanBesar;
                    bahanBaku.TanggalUpdate = DateTime.Now;
                    bahanBaku.Urutan = null;
                    bahanBaku.KodeBahanBaku = TextBoxKodeBahanBaku.Text;
                    bahanBaku.Nama = TextBoxNama.Text;
                    bahanBaku.Berat = TextBoxBerat.Text.ToDecimal();
                    bahanBaku.Konversi = TextBoxKonversi.Text.ToDecimal();
                    bahanBaku.Deskripsi = TextBoxDeskripsi.Text;

                    KategoriBahanBaku_Class.KategoriBahanBaku(db, bahanBaku, TextBoxKategori.Text);

                    TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.TBBahanBaku == bahanBaku && item.IDTempat == pengguna.IDTempat);

                    if (stokBahanBaku == null)
                    {
                        stokBahanBaku = StokBahanBaku_Class.InsertStokBahanBaku(db, DateTime.Now, pengguna.IDPengguna, pengguna.IDTempat, bahanBaku, hargaBeli, stok, batasStok, "Stok Baru Manual");
                    }
                    else
                    {
                        stokBahanBaku.HargaBeli = hargaBeli;
                        stokBahanBaku.JumlahMinimum = batasStok;
                    }

                    StokBahanBaku_Class.UpdateStockOpname(db, DateTime.Now, pengguna.IDPengguna, stokBahanBaku, stok, false, "Update Stok Manual");
                }

                db.SubmitChanges();
            }

            Response.Redirect("Default.aspx");
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    #endregion

    private void LoadDataJavaScript(DataClassesDatabaseDataContext db)
    {
        #region SATUAN
        string Satuan = "[";

        foreach (var item in db.TBSatuans)
            Satuan += "\"" + item.Nama + "\", ";

        Satuan += "]";
        #endregion


        #region KATEGORI PRODUK
        string KategoriBahanBaku = "[";

        foreach (var item in db.TBKategoriBahanBakus)
            KategoriBahanBaku += "\"" + item.Nama + "\", ";

        KategoriBahanBaku += "]";
        #endregion

        #region PILIHAN
        LiteralJavascript.Text = "<script type=\"text/javascript\">";
        LiteralJavascript.Text += "$(document).ready(function () { jQuery(function ($) { ";

        //KATEGORI PRODUK
        LiteralJavascript.Text += "$(\".KategoriBahanBaku\").select2({ tags: " + KategoriBahanBaku + ", tokenSeparators: [\",\"] });";

        //PEMILIK PRODUK
        LiteralJavascript.Text += "$(\".Satuan\").select2({ tags: " + Satuan + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        LiteralJavascript.Text += " }); });";

        LiteralJavascript.Text += "function pageLoad(sender, args) { if (args.get_isPartialLoad()) { jQuery(function ($) { ";

        //KATEGORI PRODUK
        LiteralJavascript.Text += "$(\".KategoriBahanBaku\").select2({ tags: " + KategoriBahanBaku + ", tokenSeparators: [\",\"] });";

        //PEMILIK PRODUK
        LiteralJavascript.Text += "$(\".Satuan\").select2({ tags: " + Satuan + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        LiteralJavascript.Text += " }); }};";
        LiteralJavascript.Text += "</script>";
        #endregion
    }
}