using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_PengaturanX : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Produk_Class ClassProduk = new Produk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();

                var Produk = ClassProduk.Cari(Request.QueryString["id"].ToInt());

                if (Produk != null)
                {
                    HiddenFieldIDProduk.Value = Produk.IDProduk.ToString();
                    TextBoxNamaProduk.Text = Produk.Nama;
                    TextBoxKodeProduk.Text = Produk.KodeProduk;
                    TextBoxWarna.Text = Produk.TBWarna.Nama;
                    TextBoxPemilikProduk.Text = Produk.TBPemilikProduk.Nama;
                    TextBoxProdukKategori.Text = Produk.TBProdukKategori.Nama;
                    TextBoxKategori.Text = KategoriProduk_Class.Data(Produk.TBRelasiProdukKategoriProduks.ToArray());
                    TextBoxDeskripsi.Text = Produk.Deskripsi;

                    Session["IDProduk"] = Produk.IDProduk;

                    ButtonOk.Text = "Ubah";
                    LabelKeterangan.Text = "Ubah";

                    LoadDataFoto(db, Produk.IDProduk);
                    LoadDataKombinasiProduk(db);
                }
                else
                {
                    ButtonOk.Text = "Tambah";
                    LabelKeterangan.Text = "Tambah";

                    AjaxFileUploadFoto.Enabled = false;
                }

                LoadDataDropdown(db);
            }
        }
    }

    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = "";

        //VALIDASI NAMA PRODUK HARUS DIISI
        if (string.IsNullOrWhiteSpace(TextBoxNamaProduk.Text))
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Nama produk harus diisi");
            return;
        }

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Produk_Class ClassProduk = new Produk_Class(db);

            var ProdukDuplikat = ClassProduk.Cari(TextBoxNamaProduk.Text);

            if (ButtonOk.Text == "Tambah")
            {
                //VALIDASI NAMA PRODUK DUPLIKAT
                if (ProdukDuplikat != null)
                {
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Nama produk <a href='/WITAdministrator/Produk/Pengaturan.aspx?id=" + ProdukDuplikat.IDProduk + "'>" + ProdukDuplikat.Nama + "</a> sudah digunakan");
                    return;
                }

                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                //PRODUK
                var Produk = ClassProduk.Tambah(TextBoxProdukKategori.Text, TextBoxWarna.Text, TextBoxPemilikProduk.Text, TextBoxKodeProduk.Text, TextBoxNamaProduk.Text, TextBoxDeskripsi.Text);

                //KATEGORI PRODUK
                KategoriProduk_Class.KategoriProduk(db, Produk, TextBoxKategori.Text);

                //KOMBINASI PRODUK
                var KombinasiProduk = KombinasiProduk_Class.Tambah(db, Produk, "", "", "", 0, "");

                //STOK PRODUK
                StokProduk_Class.MembuatStok(0, Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk, 0, 0, "");

                db.SubmitChanges();

                //GENERATE BARCODE
                KombinasiProduk_Class.PengaturanBarcode(db, Pengguna.IDTempat, KombinasiProduk);

                Session["IDProduk"] = Produk.IDProduk;

                Response.Redirect("Pengaturan.aspx?id=" + Produk.IDProduk);
            }
            else if (ButtonOk.Text == "Ubah")
            {
                //VALIDASI PRODUK TIDAK BOLEH DUPLIKAT
                if (ProdukDuplikat != null && (ProdukDuplikat.IDProduk != HiddenFieldIDProduk.Value.ToInt()))
                {
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Nama produk <a href='/WITAdministrator/Produk/Pengaturan.aspx?id=" + ProdukDuplikat.IDProduk + "'>" + TextBoxNamaProduk.Text + "</a> sudah digunakan");
                    return;
                }

                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();

                //PRODUK
                var Produk = ClassProduk.Ubah(
                    IDProduk: HiddenFieldIDProduk.Value.ToInt(),
                    warna: TextBoxWarna.Text,
                    pemilikProduk: TextBoxPemilikProduk.Text,
                    produkKategori: TextBoxProdukKategori.Text,
                    KodeProduk: TextBoxKodeProduk.Text,
                    Nama: TextBoxNamaProduk.Text,
                    Deskripsi: TextBoxDeskripsi.Text
                    );

                //KATEGORI PRODUK
                KategoriProduk_Class.KategoriProduk(db, Produk, TextBoxKategori.Text);

                db.SubmitChanges();

                Response.Redirect("Pengaturan.aspx?id=" + Produk.IDProduk);
            }
        }
    }

    protected void ButtonBuatVarian_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Produk_Class ClassProduk = new Produk_Class(db);
            KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();

            //PRODUK
            var Produk = ClassProduk.Cari(HiddenFieldIDProduk.Value.ToInt());

            //KOMBINASI PRODUK
            KombinasiProduk_Class.TambahList(db, Pengguna.IDTempat, Pengguna.IDPengguna, Produk, TextBoxAtributProduk.Text);

            TextBoxAtributProduk.Text = "";

            LoadDataKombinasiProduk(db);
        }
    }

    private void LoadDataKombinasiProduk(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

        List<dynamic> ListKombinasiProduk = new List<dynamic>();

        foreach (var item in KombinasiProduk_Class.Data(db, HiddenFieldIDProduk.Value.ToInt()))
        {
            var StokProduk = StokProduk_Class.Cari(Pengguna.IDTempat, item.IDKombinasiProduk);

            ListKombinasiProduk.Add(new
            {
                IDKombinasiProduk = item.IDKombinasiProduk,
                Atribut = item.TBAtributProduk.Nama,
                KodeKombinasiProduk = item.KodeKombinasiProduk,
                Berat = item.Berat.ToFormatHarga(),
                HargaBeli = StokProduk != null ? StokProduk.HargaBeli.ToFormatHarga() : "0",
                HargaJual = StokProduk != null ? StokProduk.HargaJual.ToFormatHarga() : "0",
                Jumlah = StokProduk != null ? StokProduk.Jumlah.ToFormatHargaBulat() : "0",
                Status = StokProduk != null ? StokProduk.Status : false,
                IDStokProduk = StokProduk != null ? StokProduk.IDStokProduk : 0,
            });
        }

        RepeaterKombinasiProduk.DataSource = ListKombinasiProduk;
        RepeaterKombinasiProduk.DataBind();
    }

    #region FOTO
    private void LoadDataFoto(DataClassesDatabaseDataContext db, int idProduk)
    {
        FotoProduk_Class ClassFotoProduk = new FotoProduk_Class();
        RepeaterFotoProduk.DataSource = ClassFotoProduk.Data(db, idProduk)
            .Select(item => new
            {
                item.IDFotoProduk,
                item.FotoUtama,
                Foto = "/images/Produk/" + item.IDFotoProduk + item.ExtensiFoto
            });
        RepeaterFotoProduk.DataBind();
    }
    protected void AjaxFileUploadFoto_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            string Folder = Server.MapPath("~/images/Produk/");

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            if (Session["IDProduk"] != null)
            {
                FotoProduk_Class ClassFotoProduk = new FotoProduk_Class();

                TBFotoProduk FotoProduk = ClassFotoProduk.Tambah(db, Session["IDProduk"].ToInt(), Path.GetExtension(e.FileName));

                AjaxFileUploadFoto.SaveAs(Folder + FotoProduk.IDFotoProduk + FotoProduk.ExtensiFoto);

                LoadDataFoto(db, FotoProduk.IDProduk);
            }
        }
    }
    protected void RepeaterFotoProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            FotoProduk_Class ClassFotoProduk = new FotoProduk_Class();

            if (e.CommandName == "Hapus")
                ClassFotoProduk.Hapus(db, e.CommandArgument.ToInt());
            else if (e.CommandName == "FotoUtama")
                ClassFotoProduk.FotoUtama(db, e.CommandArgument.ToInt());

            db.SubmitChanges();
            LoadDataFoto(db, Request.QueryString["id"].ToInt());
        }
    }

    protected void ButtonRefreshFoto_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadDataFoto(db, Request.QueryString["id"].ToInt());
        }
    }
    #endregion

    protected void RepeaterKombinasiProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Hapus")
            {
                KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();

                if (KombinasiProduk_Class.Hapus(db, e.CommandArgument.ToInt()))
                {
                    db.SubmitChanges();

                    LoadDataKombinasiProduk(db);
                }
            }
            if (e.CommandName == "UbahStatus")
            {
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                if (StokProduk_Class.UbahStatus((e.CommandArgument.ToString()).ToInt()))
                {
                    LoadDataKombinasiProduk(db);
                }
            }
        }

    }

    protected void ButtonSimpanVarian_Click(object sender, EventArgs e)
    {
        //    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        //    {
        //        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        //        KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
        //        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

        //        foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
        //        {
        //            HiddenField HiddenFieldIDKombinasiProduk = (HiddenField)item.FindControl("HiddenFieldIDKombinasiProduk");
        //            HiddenField HiddenFieldJumlah = (HiddenField)item.FindControl("HiddenFieldJumlah");
        //            TextBox TextBoxKodeKombinasiProduk = (TextBox)item.FindControl("TextBoxKodeKombinasiProduk");
        //            TextBox TextBoxAtributProduk = (TextBox)item.FindControl("TextBoxAtributProduk");
        //            TextBox TextBoxBerat = (TextBox)item.FindControl("TextBoxBerat");
        //            TextBox TextBoxHargaBeli = (TextBox)item.FindControl("TextBoxHargaBeli");
        //            TextBox TextBoxHargaJual = (TextBox)item.FindControl("TextBoxHargaJual");
        //            TextBox TextBoxJumlah = (TextBox)item.FindControl("TextBoxJumlah");

        //            //KOMBINASI PRODUK
        //            var KombinasiProduk = KombinasiProduk_Class.Ubah(db, Pengguna.IDTempat, HiddenFieldIDKombinasiProduk.Value.ToInt(), "", TextBoxAtributProduk.Text, TextBoxKodeKombinasiProduk.Text, TextBoxBerat.Text.ToDecimal(), "");

        //            //STOK PRODUK
        //            var StokProduk = StokProduk_Class.Ubah(Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk, TextBoxHargaBeli.Text.ToDecimal(), TextBoxHargaJual.Text.ToDecimal());

        //            if (StokProduk == null && (TextBoxHargaBeli.Text.ToDecimal() > 0 || TextBoxHargaJual.Text.ToDecimal() > 0 || TextBoxJumlah.Text.ToInt() > 0))
        //                StokProduk = StokProduk_Class.MembuatStok(0, Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk, TextBoxHargaBeli.Text.ToDecimal(), TextBoxHargaJual.Text.ToDecimal(), "");

        //            StokProduk_Class.Penyesuaian(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, TextBoxJumlah.Text.ToDecimal().ToInt(), "");
        //        }

        //        db.SubmitChanges();

        //        LoadDataKombinasiProduk(db);
        //    }
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    private void LoadDataDropdown(DataClassesDatabaseDataContext db)
    {
        Vendor_Class ClassVendor = new Vendor_Class(db);
        Warna_Class ClassWarna = new Warna_Class(db);
        ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);
        AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
        PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);
        KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();

        #region ATRIBUT PRODUK
        string AtributProduk = "[";

        foreach (var item in ClassAtributProduk.Data())
            AtributProduk += "\"" + item.Nama + "\", ";

        AtributProduk += "]";
        #endregion

        #region VENDOR
        string Vendor = "[";

        foreach (var item in ClassVendor.Data())
            Vendor += "\"" + item.Nama + "\", ";

        Vendor += "]";
        #endregion

        #region WARNA
        string Warna = "[";

        foreach (var item in ClassWarna.Data())
            Warna += "\"" + item.Nama + "\", ";

        Warna += "]";
        #endregion

        #region PEMILIK PRODUK
        string PemilikProduk = "[";

        foreach (var item in ClassPemilikProduk.Data())
            PemilikProduk += "\"" + item.Nama + "\", ";

        PemilikProduk += "]";
        #endregion

        #region PRODUK KATEGORI
        string ProdukKategori = "[";

        foreach (var item in ClassProdukKategori.Data())
            ProdukKategori += "\"" + item.Nama + "\", ";

        ProdukKategori += "]";
        #endregion

        #region KATEGORI PRODUK
        string KategoriProduk = "[";

        foreach (var item in KategoriProduk_Class.Data(db))
            KategoriProduk += "\"" + item.Nama + "\", ";

        KategoriProduk += "]";
        #endregion

        #region PILIHAN
        LiteralJavascript.Text = "<script type=\"text/javascript\">";
        LiteralJavascript.Text += "$(document).ready(function () { jQuery(function ($) { ";

        //KATEGORI PRODUK
        LiteralJavascript.Text += "$(\".KategoriProduk\").select2({ tags: " + KategoriProduk + ", tokenSeparators: [\",\"] });";

        //ATRIBUT PRODUK
        LiteralJavascript.Text += "$(\".AtributProduk\").select2({ tags: " + AtributProduk + ", tokenSeparators: [\",\"] });";
        LiteralJavascript.Text += "$(\".AtributProdukSatuan\").select2({ tags: " + AtributProduk + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //VENDOR
        LiteralJavascript.Text += "$(\".Vendor\").select2({ tags: " + Vendor + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //WARNA
        LiteralJavascript.Text += "$(\".Warna\").select2({ tags: " + Warna + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //PRODUK KATEGORI
        LiteralJavascript.Text += "$(\"#TextBoxProdukKategori\").select2({ tags: " + ProdukKategori + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //PEMILIK PRODUK
        LiteralJavascript.Text += "$(\".PemilikProduk\").select2({ tags: " + PemilikProduk + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        LiteralJavascript.Text += " }); });";

        LiteralJavascript.Text += "function pageLoad(sender, args) { if (args.get_isPartialLoad()) { jQuery(function ($) { ";

        //KATEGORI PRODUK
        LiteralJavascript.Text += "$(\".KategoriProduk\").select2({ tags: " + KategoriProduk + ", tokenSeparators: [\",\"] });";

        //ATRIBUT PRODUK
        LiteralJavascript.Text += "$(\".AtributProduk\").select2({ tags: " + AtributProduk + ", tokenSeparators: [\",\"] });";
        LiteralJavascript.Text += "$(\".AtributProdukSatuan\").select2({ tags: " + AtributProduk + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //VENDOR
        LiteralJavascript.Text += "$(\".Vendor\").select2({ tags: " + Vendor + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //WARNA
        LiteralJavascript.Text += "$(\".Warna\").select2({ tags: " + Warna + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //PRODUK KATEGORI
        LiteralJavascript.Text += "$(\"#TextBoxProdukKategori\").select2({ tags: " + ProdukKategori + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        //PEMILIK PRODUK
        LiteralJavascript.Text += "$(\".PemilikProduk\").select2({ tags: " + PemilikProduk + ", tokenSeparators: [\",\"], maximumSelectionSize: 1 });";

        LiteralJavascript.Text += " }); }};";
        LiteralJavascript.Text += "</script>";
        #endregion
    }

    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        LabelKeterangan.Text = "Testing";
    }

    protected void CheckBoxSemua_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
        {
            CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

            CheckBoxPilih.Checked = CheckBoxSemua.Checked;
        }
    }

    protected void ButtonUpdateBerat_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();

            foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

                if (CheckBoxPilih.Checked)
                {
                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    Label LabelKodeKombinasiProduk = (Label)item.FindControl("LabelKodeKombinasiProduk");
                    Label LabelAtribut = (Label)item.FindControl("LabelAtribut");
                    Label LabelBerat = (Label)item.FindControl("LabelBerat");

                    KombinasiProduk_Class.Ubah(db, Pengguna.IDTempat, LabelIDKombinasiProduk.Text.ToInt(), "", LabelAtribut.Text, LabelKodeKombinasiProduk.Text, TextBoxUpdateBerat.Text.ToDecimal(), "");

                    LabelBerat.Text = TextBoxUpdateBerat.Text;
                }
            }

            db.SubmitChanges();
        }
    }

    protected void ButtonUpdateHargaBeli_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
            StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

            foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

                if (CheckBoxPilih.Checked)
                {
                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    Label LabelHargaBeli = (Label)item.FindControl("LabelHargaBeli");
                    Label LabelHargaJual = (Label)item.FindControl("LabelHargaJual");
                    Label LabelStok = (Label)item.FindControl("LabelStok");

                    //STOK PRODUK
                    var StokProduk = StokProduk_Class.Ubah(Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk_Class.Cari(db, LabelIDKombinasiProduk.Text.ToInt()), TextBoxUpdateHargaBeli.Text.ToDecimal(), LabelHargaJual.Text.ToDecimal());

                    if (StokProduk == null && (TextBoxUpdateHargaBeli.Text.ToDecimal() > 0 || LabelHargaJual.Text.ToDecimal() > 0 || LabelStok.Text.ToInt() > 0))
                        StokProduk = StokProduk_Class.MembuatStok(0, Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk_Class.Cari(db, LabelIDKombinasiProduk.Text.ToInt()), TextBoxUpdateHargaBeli.Text.ToDecimal(), LabelHargaJual.Text.ToDecimal(), "");

                    StokProduk_Class.Penyesuaian(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, LabelStok.Text.ToDecimal().ToInt(), "");

                    LabelHargaBeli.Text = TextBoxUpdateHargaBeli.Text;
                }
            }

            db.SubmitChanges();
        }
    }

    protected void ButtonUpdateHargaJual_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
            StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
            foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

                if (CheckBoxPilih.Checked)
                {
                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    Label LabelHargaBeli = (Label)item.FindControl("LabelHargaBeli");
                    Label LabelHargaJual = (Label)item.FindControl("LabelHargaJual");
                    Label LabelStok = (Label)item.FindControl("LabelStok");

                    //STOK PRODUK
                    var StokProduk = StokProduk_Class.Ubah(Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk_Class.Cari(db, LabelIDKombinasiProduk.Text.ToInt()), LabelHargaBeli.Text.ToDecimal(), TextBoxUpdateHargaJual.Text.ToDecimal());

                    if (StokProduk == null && (LabelHargaBeli.Text.ToDecimal() > 0 || TextBoxUpdateHargaJual.Text.ToDecimal() > 0 || LabelStok.Text.ToInt() > 0))
                        StokProduk = StokProduk_Class.MembuatStok(0, Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk_Class.Cari(db, LabelIDKombinasiProduk.Text.ToInt()), LabelHargaBeli.Text.ToDecimal(), TextBoxUpdateHargaJual.Text.ToDecimal(), "");

                    StokProduk_Class.Penyesuaian(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, LabelStok.Text.ToDecimal().ToInt(), "");

                    LabelHargaJual.Text = TextBoxUpdateHargaJual.Text;
                }
            }

            db.SubmitChanges();
        }
    }

    protected void ButtonUpdateStok_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            KombinasiProduk_Class KombinasiProduk_Class = new KombinasiProduk_Class();
            StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
            foreach (RepeaterItem item in RepeaterKombinasiProduk.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

                if (CheckBoxPilih.Checked)
                {
                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    Label LabelHargaBeli = (Label)item.FindControl("LabelHargaBeli");
                    Label LabelHargaJual = (Label)item.FindControl("LabelHargaJual");
                    Label LabelStok = (Label)item.FindControl("LabelStok");

                    //STOK PRODUK
                    var StokProduk = StokProduk_Class.Ubah(Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk_Class.Cari(db, LabelIDKombinasiProduk.Text.ToInt()), LabelHargaBeli.Text.ToDecimal(), LabelHargaJual.Text.ToDecimal());

                    if (StokProduk == null && (LabelHargaBeli.Text.ToDecimal() > 0 || LabelHargaJual.Text.ToDecimal() > 0 || TextBoxUpdateStok.Text.ToInt() > 0))
                        StokProduk = StokProduk_Class.MembuatStok(0, Pengguna.IDTempat, Pengguna.IDPengguna, KombinasiProduk_Class.Cari(db, LabelIDKombinasiProduk.Text.ToInt()), LabelHargaBeli.Text.ToDecimal(), LabelHargaJual.Text.ToDecimal(), "");

                    StokProduk_Class.Penyesuaian(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, TextBoxUpdateStok.Text.ToDecimal().ToInt(), "");

                    LabelStok.Text = TextBoxUpdateStok.Text;
                }
            }

            db.SubmitChanges();
        }
    }
}