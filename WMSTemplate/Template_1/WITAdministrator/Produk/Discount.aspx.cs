using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_NewDiscount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    Tempat_Class ClassTempat = new Tempat_Class(db);
                    AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                    KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                    PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                    DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                    DropDownListCariTempat.Items.RemoveAt(0);
                    DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();
                    DropDownListCariPemilikProduk.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                    DropDownListCariAtributProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());
                    DropDownListCariKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                    DropDownListCariProduk.DataSource = db.TBProduks.OrderBy(item => item.Nama).ToArray();
                    DropDownListCariProduk.DataValueField = "IDProduk";
                    DropDownListCariProduk.DataTextField = "Nama";
                    DropDownListCariProduk.DataBind();
                    DropDownListCariProduk.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
            }
        }
    }

    public void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBStokProduk[] daftarStokProduk = db.TBStokProduks
                .Where(item =>
                    item.TBKombinasiProduk.TBProduk._IsActive &&
                    item.IDTempat == DropDownListCariTempat.SelectedValue.ToInt()).ToArray();

            CheckBoxSemua.Checked = false;

            if (!string.IsNullOrEmpty(TextBoxCariKode.Text))
                daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.KodeKombinasiProduk.Contains(TextBoxCariKode.Text)).ToArray();

            if (DropDownListCariPemilikProduk.SelectedValue != "0")
                daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.IDPemilikProduk == DropDownListCariPemilikProduk.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariProduk.SelectedValue != "0")
                daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.IDProduk == DropDownListCariProduk.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariAtributProduk.SelectedValue != "0")
                daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.IDAtributProduk == DropDownListCariAtributProduk.SelectedValue.ToInt()).ToArray();

            if (DropDownListCariKategori.SelectedValue != "0")
                daftarStokProduk = daftarStokProduk.Where(item => item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == DropDownListCariKategori.SelectedValue.ToInt()) != null).ToArray();

            if (DropDownListCariStatusDiskon.SelectedValue == "Semua")
            {
                RepeaterStokProduk.DataSource = daftarStokProduk.AsEnumerable()
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.IDKombinasiProduk,
                        item.TBKombinasiProduk.KodeKombinasiProduk,
                        item.TBKombinasiProduk.TBProduk.Nama,
                        Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                        Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                        Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                        Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                        item.HargaJual,
                        item.Jumlah,
                        DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore : 0,
                        DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore : 0,
                        DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi : 0,
                        DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi : 0,
                        SetelahDiskon = SetelahDiskon(item.HargaJual.Value, item.EnumDiscountStore, item.DiscountStore, item.EnumDiscountKonsinyasi, item.DiscountKonsinyasi)
                    }).OrderBy(item => item.Nama).ToArray();
                RepeaterStokProduk.DataBind();
            }
            else if (DropDownListCariStatusDiskon.SelectedValue == "Diskon")
            {
                RepeaterStokProduk.DataSource = daftarStokProduk.AsEnumerable()
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.IDKombinasiProduk,
                        item.TBKombinasiProduk.KodeKombinasiProduk,
                        item.TBKombinasiProduk.TBProduk.Nama,
                        Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                        Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                        Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                        Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                        item.HargaJual,
                        item.Jumlah,
                        DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore : 0,
                        DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore : 0,
                        DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi : 0,
                        DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi : 0,
                        SetelahDiskon = SetelahDiskon(item.HargaJual.Value, item.EnumDiscountStore, item.DiscountStore, item.EnumDiscountKonsinyasi, item.DiscountKonsinyasi)
                    }).Where(item => item.HargaJual.Value != item.SetelahDiskon).OrderBy(item => item.Nama).ToArray();
                RepeaterStokProduk.DataBind();
            }
            else if (DropDownListCariStatusDiskon.SelectedValue == "TidakDiskon")
            {
                RepeaterStokProduk.DataSource = daftarStokProduk.AsEnumerable()
                    .Select(item => new
                    {
                        item.IDStokProduk,
                        item.IDKombinasiProduk,
                        item.TBKombinasiProduk.KodeKombinasiProduk,
                        item.TBKombinasiProduk.TBProduk.Nama,
                        Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                        Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                        Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, item, null),
                        Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                        item.HargaJual,
                        item.Jumlah,
                        DiscountStoreNominal = item.EnumDiscountStore == (int)EnumDiscount.Nominal ? item.DiscountStore : 0,
                        DiscountStorePersentase = item.EnumDiscountStore == (int)EnumDiscount.Persentase ? item.DiscountStore : 0,
                        DiscountKonsinyasiNominal = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? item.DiscountKonsinyasi : 0,
                        DiscountKonsinyasiPersentase = item.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? item.DiscountKonsinyasi : 0,
                        SetelahDiskon = SetelahDiskon(item.HargaJual.Value, item.EnumDiscountStore, item.DiscountStore, item.EnumDiscountKonsinyasi, item.DiscountKonsinyasi)
                    }).Where(item => item.HargaJual.Value == item.SetelahDiskon).OrderBy(item => item.Nama).ToArray();
                RepeaterStokProduk.DataBind();
            }

            string tempPencarian = string.Empty;

            tempPencarian += "?IDTempat=" + DropDownListCariTempat.SelectedValue;
            tempPencarian += "&StatusDiskon=" + DropDownListCariStatusDiskon.SelectedValue;
            tempPencarian += "&Kode=" + TextBoxCariKode.Text;
            tempPencarian += "&IDPemilikProduk=" + DropDownListCariPemilikProduk.SelectedValue;
            tempPencarian += "&IDProduk=" + DropDownListCariProduk.SelectedValue;
            tempPencarian += "&IDAtributProduk=" + DropDownListCariAtributProduk.SelectedValue;
            tempPencarian += "&IDKategoriProduk=" + DropDownListCariKategori.SelectedValue;

            ButtonPrint.OnClientClick = "return popitup('DiscountPrint.aspx" + tempPencarian + "')";
        }
    }

    private decimal SetelahDiskon(decimal hargaJual, int enumDiscountStore, decimal discountStore, int enumDiscountKonsinyasi, decimal discountKonsinyasi)
    {
        decimal DiscountStore = 0;

        switch ((EnumDiscount)enumDiscountStore)
        {
            case EnumDiscount.Persentase:
                DiscountStore = (hargaJual * discountStore / 100);
                break;
            case EnumDiscount.Nominal:
                DiscountStore = discountStore;
                break;
        }

        decimal DiscountKonsinyasi = 0;

        switch ((EnumDiscount)enumDiscountKonsinyasi)
        {
            case EnumDiscount.Persentase:
                DiscountKonsinyasi = (hargaJual * discountKonsinyasi / 100);
                break;
            case EnumDiscount.Nominal:
                DiscountKonsinyasi = discountKonsinyasi;
                break;
        }

        return hargaJual - (DiscountStore + DiscountKonsinyasi);
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            peringatan.Visible = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                bool check = true;
                bool berhasil = true;
                LiteralPeringatan.Text = string.Empty;
                StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                if (!string.IsNullOrEmpty(TextBoxStorePersentase.Text) && !string.IsNullOrEmpty(TextBoxStoreNominal.Text))
                {
                    if (!check)
                        LiteralPeringatan.Text += "<br />";

                    check = false;
                    LiteralPeringatan.Text += "Semua Discount Store hanya boleh memilih antara Discount Persentase atau Nominal";
                    peringatan.Visible = true;
                }

                if (!string.IsNullOrEmpty(TextBoxConsignmentPersentase.Text) && !string.IsNullOrEmpty(TextBoxConsignmentNominal.Text))
                {
                    if (!check)
                        LiteralPeringatan.Text += "<br />";

                    check = false;
                    LiteralPeringatan.Text += "Semua Discount Consignment hanya boleh memilih antara Discount Persentase atau Nominal";
                    peringatan.Visible = true;
                }

                if (check)
                {
                    //VALIDASI
                    foreach (RepeaterItem item in RepeaterStokProduk.Items)
                    {
                        CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");
                        if (CheckBoxPilih.Checked == true)
                        {
                            Label LabelIDStokProduk = (Label)item.FindControl("LabelIDStokProduk");

                            var StokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == LabelIDStokProduk.Text.ToInt());

                            #region Input Diskon
                            if (!string.IsNullOrEmpty(TextBoxStorePersentase.Text) && TextBoxStorePersentase.Text != "0")
                            {
                                StokProduk.EnumDiscountStore = (int)EnumDiscount.Persentase;
                                StokProduk.DiscountStore = TextBoxStorePersentase.Text.ToDecimal();
                            }
                            else if (!string.IsNullOrEmpty(TextBoxStoreNominal.Text) && TextBoxStoreNominal.Text != "0")
                            {
                                StokProduk.EnumDiscountStore = (int)EnumDiscount.Nominal;
                                StokProduk.DiscountStore = TextBoxStoreNominal.Text.ToDecimal();
                            }
                            else
                            {
                                StokProduk.EnumDiscountStore = (int)EnumDiscount.TidakAda;
                                StokProduk.DiscountStore = 0;
                            }

                            if (!string.IsNullOrEmpty(TextBoxConsignmentPersentase.Text) && TextBoxConsignmentPersentase.Text != "0")
                            {
                                StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.Persentase;
                                StokProduk.DiscountKonsinyasi = TextBoxConsignmentPersentase.Text.ToDecimal();
                            }
                            else if (!string.IsNullOrEmpty(TextBoxConsignmentNominal.Text) && TextBoxConsignmentNominal.Text != "0")
                            {
                                StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.Nominal;
                                StokProduk.DiscountKonsinyasi = TextBoxConsignmentNominal.Text.ToDecimal();
                            }
                            else
                            {
                                StokProduk.EnumDiscountKonsinyasi = (int)EnumDiscount.TidakAda;
                                StokProduk.DiscountKonsinyasi = 0;
                            }
                            #endregion

                        }
                    }

                    if (berhasil)
                    {
                        db.SubmitChanges();
                        LoadData();
                    }
                    else
                        peringatan.Visible = true;
                }

                TextBoxStorePersentase.Text = !string.IsNullOrEmpty(TextBoxStorePersentase.Text) ? TextBoxStorePersentase.Text : null;
                TextBoxStoreNominal.Text = !string.IsNullOrEmpty(TextBoxStoreNominal.Text) ? TextBoxStoreNominal.Text : null;
                TextBoxConsignmentPersentase.Text = !string.IsNullOrEmpty(TextBoxConsignmentPersentase.Text) ? TextBoxConsignmentPersentase.Text : null;
                TextBoxConsignmentNominal.Text = !string.IsNullOrEmpty(TextBoxConsignmentNominal.Text) ? TextBoxConsignmentNominal.Text: null;
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
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
    
    protected void CheckBoxSemua_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in RepeaterStokProduk.Items)
        {
            CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");

            CheckBoxPilih.Checked = CheckBoxSemua.Checked;
        }
    }
}