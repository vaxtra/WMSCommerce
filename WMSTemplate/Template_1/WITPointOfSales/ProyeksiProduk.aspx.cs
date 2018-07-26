using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITPointOfSales_ProyeksiProduk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Warna_Class ClassWarna = new Warna_Class(db);

                var listKombinasiProduk = db.TBStokProduks.Where(item => item.IDTempat == Pengguna.IDTempat).Select(item => new { item.TBKombinasiProduk.IDKombinasiProduk, item.TBKombinasiProduk.Nama, item.TBKombinasiProduk.TBProduk }).ToArray();

                #region Komposisi Produk
                DropDownListCariKomposisiProduk.DataSource = listKombinasiProduk;
                DropDownListCariKomposisiProduk.DataTextField = "Nama";
                DropDownListCariKomposisiProduk.DataValueField = "IDKombinasiProduk";
                DropDownListCariKomposisiProduk.DataBind();
                DropDownListCariKomposisiProduk.Items.Insert(0, new ListItem { Text = "-Pilih Produk-", Value = "0" });

                var listStokBahanBaku = db.TBStokBahanBakus.ToArray();
                DropDownListBahanBaku.DataSource = listStokBahanBaku.Where(item => item.IDTempat == Pengguna.IDTempat).Select(item => item.TBBahanBaku).ToArray();
                DropDownListBahanBaku.DataTextField = "Nama";
                DropDownListBahanBaku.DataValueField = "IDBahanBaku";
                DropDownListBahanBaku.DataBind();

                LabelSatuan.Text = listStokBahanBaku.FirstOrDefault() == null ? string.Empty : listStokBahanBaku.FirstOrDefault().TBBahanBaku.TBSatuan.Nama;
                ViewState["KomposisiProduk"] = new List<StokBahanBaku_Model>();
                #endregion

                #region Biaya Produksi
                DropDownListCariBiayaProduksi.DataSource = listKombinasiProduk;
                DropDownListCariBiayaProduksi.DataTextField = "Nama";
                DropDownListCariBiayaProduksi.DataValueField = "IDKombinasiProduk";
                DropDownListCariBiayaProduksi.DataBind();
                DropDownListCariBiayaProduksi.Items.Insert(0, new ListItem { Text = "-Pilih Produk-", Value = "0" });

                DropDownListJenisBiayaProduksi.DataSource = db.TBJenisBiayaProduksis.ToArray();
                DropDownListJenisBiayaProduksi.DataTextField = "Nama";
                DropDownListJenisBiayaProduksi.DataValueField = "IDJenisBiayaProduksi";
                DropDownListJenisBiayaProduksi.DataBind();
                DropDownListJenisBiayaProduksi.Items.Insert(0, new ListItem { Text = "-Tambah Baru-", Value = "0" });

                ViewState["BiayaProduksi"] = new List<JenisBiayaProduksi_Model>();
                #endregion

                #region Produk
                DropDownListCariProduk.DataSource = listKombinasiProduk.Select(item => item.TBProduk).Distinct().ToArray();
                DropDownListCariProduk.DataTextField = "Nama";
                DropDownListCariProduk.DataValueField = "IDProduk";
                DropDownListCariProduk.DataBind();
                DropDownListCariProduk.Items.Insert(0, new ListItem { Text = "-Produk Baru-", Value = "0" });

                DropDownListBrand.DataSource = db.TBPemilikProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListBrand.DataTextField = "Nama";
                DropDownListBrand.DataValueField = "IDPemilikProduk";
                DropDownListBrand.DataBind();
                DropDownListBrand.Items.Insert(0, new ListItem { Text = "-Tambah Baru-", Value = "0" });

                DropDownListWarna.DataSource = ClassWarna.Data();
                DropDownListWarna.DataTextField = "Nama";
                DropDownListWarna.DataValueField = "IDWarna";
                DropDownListWarna.DataBind();
                DropDownListWarna.Items.Insert(0, new ListItem { Text = "-Tambah Baru-", Value = "0" });

                DropDownListVarian.DataSource = db.TBAtributProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListVarian.DataTextField = "Nama";
                DropDownListVarian.DataValueField = "IDAtributProduk";
                DropDownListVarian.DataBind();
                DropDownListVarian.Items.Insert(0, new ListItem { Text = "-Tidak Ada-", Value = "-1" });
                DropDownListVarian.Items.Insert(1, new ListItem { Text = "-Tambah Baru-", Value = "0" });


                CheckBoxListKategori.DataSource = db.TBKategoriProduks.Select(item => new { item.IDKategoriProduk, Nama = item.Nama }).OrderBy(item => item.Nama).ToArray();
                CheckBoxListKategori.DataValueField = "IDKategoriProduk";
                CheckBoxListKategori.DataTextField = "Nama";
                CheckBoxListKategori.DataBind();
                #endregion
            }

            if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
            {
                if (Request.QueryString["Status"] == "true")
                    LiteralInformasi.Text = "<div class=\"alert alert-success\" role=\"alert\"><strong>Berhasil.</strong> Data produk telah tersimpan.</div>";
            }
        }
    }



    #region Komposisi Produk
    protected void ButtonCariKomposisiProduk_Click(object sender, EventArgs e)
    {
        if (DropDownListCariKomposisiProduk.SelectedValue != "0")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
                komposisiProduk.Clear();
                List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];

                var listStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

                komposisiProduk.AddRange(db.TBKomposisiKombinasiProduks.AsEnumerable().Where(item => item.IDKombinasiProduk == Parse.Int(DropDownListCariKomposisiProduk.SelectedValue)).Select(item => new StokBahanBaku_Model
                {
                    IDBahanBaku = item.IDBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    IDSatuan = item.TBBahanBaku.IDSatuan,
                    Jumlah = item.Jumlah.Value,
                    Satuan = item.TBBahanBaku.TBSatuan.Nama,
                    HargaBeli = listStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku).HargaBeli.Value,
                    Komposisi = db.TBBahanBakus.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku).TBKomposisiBahanBakus.Select(data => new KomposisiBahanBaku_Model
                    {
                        BahanBaku = data.TBBahanBaku1.Nama,
                        JumlahPemakaian = item.Jumlah.Value * data.Jumlah.Value,
                        Satuan = data.TBBahanBaku1.TBSatuan.Nama
                    }).OrderBy(data => data.BahanBaku).ToList()
                }));

                DropDownListCariKomposisiProduk.SelectedValue = "0";

                LoadKomposisiProduk(komposisiProduk, biayaProduksi);
                LoadBiayaProduksi(komposisiProduk, biayaProduksi);

                ViewState["KomposisiProduk"] = komposisiProduk;
                ViewState["BiayaProduksi"] = biayaProduksi;
            }
        }
        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");

    }
    private void LoadKomposisiProduk(List<StokBahanBaku_Model> komposisiProduk, List<JenisBiayaProduksi_Model> biayaProduksi)
    {
        RepeaterKomposisiProduk.DataSource = komposisiProduk.Select(item => new
        {
            item.IDBahanBaku,
            TargetBahanBaku = "#" + item.IDBahanBaku,
            item.BahanBaku,
            item.Jumlah,
            item.Satuan,
            StatusKomposisi = item.Komposisi.Count > 0 ? "info" : string.Empty,
            item.HargaBeli,
            item.SubtotalHargaBeli,
            item.Komposisi
        }).OrderBy(item => item.BahanBaku);
        RepeaterKomposisiProduk.DataBind();

        LabelSubtotalKomposisiProduk.Text = Pengaturan.FormatHarga(komposisiProduk.Sum(item => item.SubtotalHargaBeli));

        TextBoxHargaPokokProduksi.Text = (komposisiProduk.Sum(item => item.SubtotalHargaBeli) + biayaProduksi.Sum(item => item.Biaya)).ToString();
        TextBoxHargaJual.Text = TextBoxHargaPokokProduksi.Text;
    }
    protected void RepeaterKomposisiProduk_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
            List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];
            komposisiProduk.Remove(komposisiProduk.FirstOrDefault(item => item.IDBahanBaku == Parse.Int(e.CommandArgument.ToString())));

            LoadKomposisiProduk(komposisiProduk, biayaProduksi);
            LoadBiayaProduksi(komposisiProduk, biayaProduksi);

            ViewState["KomposisiProduk"] = komposisiProduk;
            ViewState["BiayaProduksi"] = biayaProduksi;

            CollapseBiayaProduksi.Attributes.Add("class", "collapse");
            CollapseKomposisiProduk.Attributes.Add("class", "collapse");
        }
    }
    protected void DropDownListBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBBahanBaku bahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Parse.Int(DropDownListBahanBaku.SelectedValue));
            LabelSatuan.Text = bahanBaku.TBSatuan.Nama;
            RepeaterKomposisiBahanBaku.DataSource = bahanBaku.TBKomposisiBahanBakus.Select(item => new
            {
                BahanBaku = item.TBBahanBaku1.Nama,
                Jumlah = item.Jumlah,
                Satuan = item.TBBahanBaku1.TBSatuan.Nama
            }).OrderBy(item => item.BahanBaku);
            RepeaterKomposisiBahanBaku.DataBind();
        }

        TextBoxJumlahBahanBaku.Focus();
        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse in");
    }
    protected void ButtonSimpanBahanBaku_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Pengaturan.FormatAngkaInput(TextBoxJumlahBahanBaku.Text) > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
                    List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];

                    var komposisi = komposisiProduk.FirstOrDefault(item => item.IDBahanBaku == Parse.Int(DropDownListBahanBaku.SelectedValue));
                    if (komposisi == null)
                    {
                        TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == pengguna.IDTempat && item.IDBahanBaku == Parse.Int(DropDownListBahanBaku.SelectedValue));

                        StokBahanBaku_Model StokBahanBaku_Model = new StokBahanBaku_Model
                        {
                            IDBahanBaku = stokBahanBaku.IDBahanBaku.Value,
                            BahanBaku = stokBahanBaku.TBBahanBaku.Nama,
                            IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuan,
                            Jumlah = Pengaturan.FormatAngkaInput(TextBoxJumlahBahanBaku.Text),
                            Satuan = stokBahanBaku.TBBahanBaku.TBSatuan.Nama,
                            HargaBeli = stokBahanBaku.HargaBeli.Value,
                            Komposisi = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Parse.Int(DropDownListBahanBaku.SelectedValue)).TBKomposisiBahanBakus.Select(item => new KomposisiBahanBaku_Model
                            {
                                BahanBaku = item.TBBahanBaku1.Nama,
                                JumlahPemakaian = Pengaturan.FormatAngkaInput(TextBoxJumlahBahanBaku.Text) * item.Jumlah.Value,
                                Satuan = item.TBBahanBaku1.TBSatuan.Nama
                            }).OrderBy(item => item.BahanBaku).ToList()
                        };

                        komposisiProduk.Add(StokBahanBaku_Model);
                    }
                    else
                        komposisi.Jumlah = Pengaturan.FormatAngkaInput(TextBoxJumlahBahanBaku.Text);

                    LoadKomposisiProduk(komposisiProduk, biayaProduksi);
                    LoadBiayaProduksi(komposisiProduk, biayaProduksi);

                    ViewState["KomposisiProduk"] = komposisiProduk;
                    ViewState["BiayaProduksi"] = biayaProduksi;
                }
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse in");
        TextBoxJumlahBahanBaku.Text = string.Empty;
        TextBoxJumlahBahanBaku.Focus();
    }
    protected void CustomValidatorJumlahBahanBaku_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Pengaturan.FormatAngkaInput(TextBoxJumlahBahanBaku.Text) == 0)
        {
            args.IsValid = false;
            CustomValidatorJumlahBahanBaku.Text = "Jumlah tidak boleh 0";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorJumlahBahanBaku.Text = "-";
        }
    }
    #endregion

    #region Biaya Produksi
    protected void ButtonCariBiayaProduksi_Click(object sender, EventArgs e)
    {
        if (DropDownListCariBiayaProduksi.SelectedValue != "0")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
                decimal hargaPokokKomposisi = komposisiProduk.Sum(item => item.SubtotalHargaBeli);
                List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];
                biayaProduksi.Clear();

                biayaProduksi.AddRange(db.TBRelasiJenisBiayaProduksiKombinasiProduks.Where(item => item.IDKombinasiProduk == Parse.Int(DropDownListCariBiayaProduksi.SelectedValue)).Select(item => new JenisBiayaProduksi_Model
                {
                    IDJenisBiayaProduksi = item.IDJenisBiayaProduksi,
                    Nama = item.TBJenisBiayaProduksi.Nama,
                    EnumBiayaProduksi = item.EnumBiayaProduksi.Value,
                    Persentase = item.Persentase.Value,
                    Nominal = item.Nominal.Value
                }));

                DropDownListCariBiayaProduksi.SelectedValue = "0";

                LoadKomposisiProduk(komposisiProduk, biayaProduksi);
                LoadBiayaProduksi(komposisiProduk, biayaProduksi);

                ViewState["KomposisiProduk"] = komposisiProduk;
                ViewState["BiayaProduksi"] = biayaProduksi;
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    private void LoadBiayaProduksi(List<StokBahanBaku_Model> komposisiProduk, List<JenisBiayaProduksi_Model> biayaProduksi)
    {
        decimal hargaPokokKomposisi = komposisiProduk.Sum(item => item.SubtotalHargaBeli);

        foreach (var item in biayaProduksi)
        {
            if (item.EnumBiayaProduksi == (int)PilihanBiayaProduksi.Persen)
            {
                item.Biaya = item.Persentase * hargaPokokKomposisi;
            }
            else
            {
                item.Biaya = item.Nominal;
            }
        }

        RepeaterBiayaProduksi.DataSource = biayaProduksi.Select(item => new
        {
            item.IDJenisBiayaProduksi,
            item.Nama,
            Jenis = item.EnumBiayaProduksi == 1 ? Pengaturan.FormatHarga(item.Persentase * 100) + "% dari Komposisi Produk" : "Nominal",
            Biaya = Pengaturan.FormatHarga(item.Biaya)
        }).OrderBy(item => item.Nama);
        RepeaterBiayaProduksi.DataBind();

        LabelSubtotalBiayaProduksi.Text = Pengaturan.FormatHarga(biayaProduksi.Sum(item => item.Biaya));

        TextBoxHargaPokokProduksi.Text = (komposisiProduk.Sum(item => item.SubtotalHargaBeli) + biayaProduksi.Sum(item => item.Biaya)).ToString();
        TextBoxHargaJual.Text = TextBoxHargaPokokProduksi.Text;
    }
    protected void RepeaterBiayaProduksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
            List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];
            biayaProduksi.Remove(biayaProduksi.FirstOrDefault(item => item.IDJenisBiayaProduksi == Parse.Int(e.CommandArgument.ToString())));

            LoadBiayaProduksi(komposisiProduk, biayaProduksi);

            ViewState["KomposisiProduk"] = komposisiProduk;
            ViewState["BiayaProduksi"] = biayaProduksi;

            CollapseBiayaProduksi.Attributes.Add("class", "collapse");
            CollapseKomposisiProduk.Attributes.Add("class", "collapse");
        }
    }
    protected void DropDownListJenisBiayaProduksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListJenisBiayaProduksi.SelectedValue == "0")
        {
            TextBoxNamaJenisBiayaProduksi.Enabled = true;
        }
        else
        {
            TextBoxNamaJenisBiayaProduksi.Enabled = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxNamaJenisBiayaProduksi.Text = DropDownListJenisBiayaProduksi.SelectedItem.Text;
            }
        }

        TextBoxBiayaProduksi.Focus();
        CollapseBiayaProduksi.Attributes.Add("class", "collapse in");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    protected void RadioButtonListEnumBiayaProduksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase")
        {
            LabelStatusBiayaProduksi.Text = "%";
        }
        else if (RadioButtonListEnumBiayaProduksi.SelectedValue == "Nominal")
        {
            LabelStatusBiayaProduksi.Text = "Nominal";
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse in");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    protected void ButtonSimpanBiayaProduksi_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Pengaturan.FormatAngkaInput(TextBoxBiayaProduksi.Text) > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
                    decimal hargaPokokKomposisi = komposisiProduk.Sum(item => item.SubtotalHargaBeli);
                    List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];

                    var biaya = biayaProduksi.FirstOrDefault(item => item.Nama == TextBoxNamaJenisBiayaProduksi.Text);
                    if (biaya == null)
                    {
                        TBJenisBiayaProduksi jenisBiayaProduksi = null;

                        if (DropDownListJenisBiayaProduksi.SelectedValue == "0")
                        {
                            DropDownListJenisBiayaProduksi.Items.Insert(DropDownListJenisBiayaProduksi.Items.Count, new ListItem { Text = TextBoxNamaJenisBiayaProduksi.Text, Value = TextBoxNamaJenisBiayaProduksi.Text });
                        }
                        else
                        {
                            jenisBiayaProduksi = db.TBJenisBiayaProduksis.FirstOrDefault(item => item.Nama == DropDownListJenisBiayaProduksi.SelectedItem.Text);
                        }

                        JenisBiayaProduksi_Model JenisBiayaProduksi_Model = new JenisBiayaProduksi_Model
                        {
                            IDJenisBiayaProduksi = jenisBiayaProduksi == null ? 0 : jenisBiayaProduksi.IDJenisBiayaProduksi,
                            Nama = jenisBiayaProduksi == null ? TextBoxNamaJenisBiayaProduksi.Text : jenisBiayaProduksi.Nama,
                            EnumBiayaProduksi = RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase" ? (int)PilihanBiayaProduksi.Persen : (int)PilihanBiayaProduksi.Harga,
                            Persentase = RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase" ? (Pengaturan.FormatAngkaInput(TextBoxBiayaProduksi.Text) / 100) : 0,
                            Nominal = RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase" ? 0 : Pengaturan.FormatAngkaInput(TextBoxBiayaProduksi.Text)
                        };

                        biayaProduksi.Add(JenisBiayaProduksi_Model);
                    }
                    else
                    {
                        biaya.EnumBiayaProduksi = RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase" ? (int)PilihanBiayaProduksi.Persen : (int)PilihanBiayaProduksi.Harga;
                        biaya.Persentase = RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase" ? (Pengaturan.FormatAngkaInput(TextBoxBiayaProduksi.Text) / 100) : 0;
                        biaya.Nominal = RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase" ? 0 : Pengaturan.FormatAngkaInput(TextBoxBiayaProduksi.Text);
                    }

                    LoadKomposisiProduk(komposisiProduk, biayaProduksi);
                    LoadBiayaProduksi(komposisiProduk, biayaProduksi);

                    ViewState["KomposisiProduk"] = komposisiProduk;
                    ViewState["BiayaProduksi"] = biayaProduksi;
                }
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse in");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
        TextBoxBiayaProduksi.Text = string.Empty;
        TextBoxBiayaProduksi.Focus();
    }
    protected void CustomValidatorBiayaProduksi_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Pengaturan.FormatAngkaInput(TextBoxBiayaProduksi.Text) == 0)
        {
            args.IsValid = false;
            CustomValidatorBiayaProduksi.Text = "Jumlah tidak boleh 0";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorBiayaProduksi.Text = "-";
        }
    }
    protected void CustomValidatorNamaJenisBiayaProduksi_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (TextBoxNamaJenisBiayaProduksi.Enabled == true)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (db.TBJenisBiayaProduksis.FirstOrDefault(item => item.Nama.ToLower() == TextBoxNamaJenisBiayaProduksi.Text.ToLower()) != null)
                {
                    args.IsValid = false;
                    CustomValidatorNamaJenisBiayaProduksi.Text = "Biaya Produksi sudah ada";
                }
                else
                {
                    args.IsValid = true;
                    CustomValidatorNamaJenisBiayaProduksi.Text = "-";
                }
            }
        }
    }
    #endregion

    #region Produk
    protected void DropDownListCariProduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListCariProduk.SelectedValue != "0")
            {
                var produk = db.TBProduks.FirstOrDefault(item => item.IDProduk == Parse.Int(DropDownListCariProduk.SelectedValue));

                TextBoxNamaProduk.Text = produk.Nama;
                TextBoxNamaProduk.Enabled = false;

                DropDownListBrand.SelectedValue = produk.IDPemilikProduk.ToString();
                TextBoxBrand.Enabled = false;
                TextBoxBrand.Text = DropDownListBrand.SelectedItem.Text;

                DropDownListWarna.SelectedValue = produk.IDWarna.ToString();
                TextBoxWarna.Enabled = false;
                TextBoxWarna.Text = DropDownListWarna.SelectedItem.Text;

                foreach (var item in produk.TBRelasiProdukKategoriProduks)
                    CheckBoxListKategori.Items.FindByValue(item.IDKategoriProduk.ToString()).Selected = true;
            }
            else
            {
                DropDownListCariProduk.SelectedValue = "0";
                TextBoxNamaProduk.Text = string.Empty;
                TextBoxNamaProduk.Enabled = true;

                DropDownListBrand.SelectedValue = "0";
                TextBoxBrand.Text = string.Empty;
                TextBoxBrand.Enabled = true;

                DropDownListWarna.SelectedValue = "0";
                TextBoxWarna.Text = string.Empty;
                TextBoxWarna.Enabled = true;

                DropDownListVarian.SelectedValue = "-1";
                TextBoxVarian.Text = string.Empty;
                TextBoxVarian.Enabled = true;

                TextBoxKode.Text = string.Empty;

                CheckBoxListKategori.ClearSelection();

                TextBoxHargaJual.Text = string.Empty;
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    protected void DropDownListBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListBrand.SelectedValue == "0")
        {
            TextBoxBrand.Enabled = true;
            TextBoxBrand.Text = string.Empty;
        }
        else
        {
            TextBoxBrand.Enabled = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxBrand.Text = DropDownListBrand.SelectedItem.Text;
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    protected void DropDownListWarna_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListWarna.SelectedValue == "0")
        {
            TextBoxWarna.Enabled = true;
            TextBoxWarna.Text = string.Empty;
        }
        else
        {
            TextBoxWarna.Enabled = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxWarna.Text = DropDownListWarna.SelectedItem.Text;
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    protected void DropDownListVarian_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListVarian.SelectedValue == "0")
        {
            TextBoxVarian.Enabled = true;
            TextBoxVarian.Text = string.Empty;
        }
        else
        {
            TextBoxVarian.Enabled = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxVarian.Text = DropDownListVarian.SelectedItem.Text;
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }

    protected void ButtonSimpanProduk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                string informasi = string.Empty;
                bool pemilikProdukValid = true;
                bool warnaValid = true;
                bool atributProdukValid = true;
                bool kodevalid = true;
                bool produkValid = true;

                TBPemilikProduk pemilikProduk = null;
                TBWarna warna = null;
                TBAtributProduk atributProduk = null;

                #region Brand
                if (DropDownListBrand.SelectedValue == "0")
                {
                    if (db.TBPemilikProduks.FirstOrDefault(item => item.Nama.ToLower() == TextBoxBrand.Text.ToLower()) == null)
                    {
                        pemilikProduk = new TBPemilikProduk { Nama = TextBoxBrand.Text };
                        db.TBPemilikProduks.InsertOnSubmit(pemilikProduk);
                    }
                    else
                    {
                        pemilikProdukValid = false;
                        informasi += "<br/>Brand sudah ada";
                    }
                }
                else
                    pemilikProduk = db.TBPemilikProduks.FirstOrDefault(item => item.IDPemilikProduk == Parse.Int(DropDownListBrand.SelectedValue));
                #endregion

                #region Warna
                if (DropDownListWarna.SelectedValue == "0")
                {
                    if (db.TBWarnas.FirstOrDefault(item => item.Nama.ToLower() == TextBoxWarna.Text.ToLower()) == null)
                    {
                        warna = new TBWarna { Nama = TextBoxWarna.Text };
                        db.TBWarnas.InsertOnSubmit(warna);
                    }
                    else
                    {
                        warnaValid = false;
                        informasi += "<br/>Warna sudah ada";
                    }
                }
                else
                    warna = db.TBWarnas.FirstOrDefault(item => item.IDWarna == Parse.Int(DropDownListWarna.SelectedValue));
                #endregion

                #region Varian
                if (DropDownListVarian.SelectedValue == "0")
                {
                    if (db.TBAtributProduks.FirstOrDefault(item => item.Nama.ToLower() == TextBoxVarian.Text.ToLower()) == null)
                    {
                        atributProduk = new TBAtributProduk { Nama = TextBoxVarian.Text };
                        db.TBAtributProduks.InsertOnSubmit(atributProduk);
                    }
                    else
                    {
                        atributProdukValid = false;
                        informasi += "<br/>Varian sudah ada";
                    }
                }
                else if (Parse.Int(DropDownListVarian.SelectedValue) > 0)
                    atributProduk = db.TBAtributProduks.FirstOrDefault(item => item.IDAtributProduk == Parse.Int(DropDownListVarian.SelectedValue));
                #endregion

                #region Kode
                if (db.TBKombinasiProduks.FirstOrDefault(item => item.KodeKombinasiProduk.ToLower() == TextBoxKode.Text.ToLower()) != null)
                {
                    kodevalid = false;
                    informasi += "<br/>Kode Produk sudah dipakai";
                }
                #endregion

                #region Produk
                if (db.TBProduks.FirstOrDefault(item => item.Nama.ToLower() == TextBoxNamaProduk.Text.ToLower()) == null)
                {
                    string namaKombinasiProduk;
                    if (atributProduk == null)
                    {
                        namaKombinasiProduk = TextBoxNamaProduk.Text;
                    }
                    else
                    {
                        namaKombinasiProduk = TextBoxNamaProduk.Text + " (" + atributProduk.Nama + ")";
                    }

                    TBProduk produk = new TBProduk
                    {
                        TBWarna = warna,
                        TBPemilikProduk = pemilikProduk,
                        Nama = TextBoxNamaProduk.Text,
                        _IsActive = true
                    };

                    db.TBProduks.InsertOnSubmit(produk);

                    TBKombinasiProduk kombinasiProduk = new TBKombinasiProduk
                    {
                        TBProduk = produk,
                        TBAtributProduk = atributProduk,
                        TanggalDaftar = DateTime.Now,
                        KodeKombinasiProduk = TextBoxKode.Text,
                        Nama = namaKombinasiProduk,
                        Deskripsi = TextBoxKeterangan.Text
                    };

                    db.TBKombinasiProduks.InsertOnSubmit(kombinasiProduk);

                    db.TBStokProduks.InsertOnSubmit(new TBStokProduk
                    {
                        IDTempat = pengguna.IDTempat,
                        TBKombinasiProduk = kombinasiProduk,
                        HargaBeli = Pengaturan.FormatAngkaInput(TextBoxHargaPokokProduksi.Text),
                        HargaJual = Pengaturan.FormatAngkaInput(TextBoxHargaJual.Text),
                        PersentaseKonsinyasi = 0,
                        Jumlah = 0,
                        JumlahMinimum = 0,
                    });

                    foreach (ListItem item in CheckBoxListKategori.Items)
                    {
                        if (item.Selected)
                        {
                            db.TBRelasiProdukKategoriProduks.InsertOnSubmit(new TBRelasiProdukKategoriProduk
                            {
                                TBKategoriProduk = db.TBKategoriProduks.FirstOrDefault(data => data.IDKategoriProduk == Parse.Int(item.Value)),
                                TBProduk = produk
                            });
                        }
                    }

                    #region Komposisi Produk
                    List<StokBahanBaku_Model> komposisiProduk = (List<StokBahanBaku_Model>)ViewState["KomposisiProduk"];
                    db.TBKomposisiKombinasiProduks.InsertAllOnSubmit(komposisiProduk.Select(item => new TBKomposisiKombinasiProduk
                    {
                        TBKombinasiProduk = kombinasiProduk,
                        IDBahanBaku = item.IDBahanBaku,
                        Jumlah = item.Jumlah,
                        Keterangan = null
                    }));
                    #endregion

                    #region Jenis Biaya Produksi
                    List<JenisBiayaProduksi_Model> biayaProduksi = (List<JenisBiayaProduksi_Model>)ViewState["BiayaProduksi"];

                    db.TBRelasiJenisBiayaProduksiKombinasiProduks.InsertAllOnSubmit(biayaProduksi.Select(item => new TBRelasiJenisBiayaProduksiKombinasiProduk
                    {
                        TBKombinasiProduk = kombinasiProduk,
                        TBJenisBiayaProduksi = cariJenisBiayaProduksi(db, item.IDJenisBiayaProduksi, item.Nama),
                        EnumBiayaProduksi = item.EnumBiayaProduksi,
                        Persentase = item.Persentase,
                        Nominal = item.Nominal
                    }));
                    #endregion
                }
                else
                {
                    produkValid = false;
                    informasi += "<br/>Produk sudah ada";
                }
                #endregion


                if (pemilikProdukValid == true && warnaValid == true && atributProdukValid == true && kodevalid == true && produkValid == true)
                {
                    db.SubmitChanges();

                    Response.Redirect("ProyeksiProduk.aspx?status=true");
                }
                else
                {
                    LiteralInformasi.Text = "<div class=\"alert alert-danger\" role=\"alert\"><strong>Terjadi Kesalahan.</strong>" + informasi + "</div>";
                }
            }
        }

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    private TBJenisBiayaProduksi cariJenisBiayaProduksi(DataClassesDatabaseDataContext db, int iDJenisBiayaProduksi, string nama)
    {
        if (iDJenisBiayaProduksi == 0)
        {
            TBJenisBiayaProduksi jenisBiayaProduksi = new TBJenisBiayaProduksi { Nama = nama };
            db.TBJenisBiayaProduksis.InsertOnSubmit(jenisBiayaProduksi);
            return jenisBiayaProduksi;
        }
        else
            return db.TBJenisBiayaProduksis.FirstOrDefault(item => item.IDJenisBiayaProduksi == iDJenisBiayaProduksi);
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        ViewState["KomposisiProduk"] = new List<StokBahanBaku_Model>();
        ViewState["BiayaProduksi"] = new List<JenisBiayaProduksi_Model>();

        LoadKomposisiProduk(new List<StokBahanBaku_Model>(), new List<JenisBiayaProduksi_Model>());
        LoadBiayaProduksi(new List<StokBahanBaku_Model>(), new List<JenisBiayaProduksi_Model>());

        DropDownListCariProduk.SelectedValue = "0";
        TextBoxNamaProduk.Text = string.Empty;
        TextBoxNamaProduk.Enabled = true;

        DropDownListBrand.SelectedValue = "0";
        TextBoxBrand.Text = string.Empty;
        TextBoxBrand.Enabled = true;

        DropDownListWarna.SelectedValue = "0";
        TextBoxWarna.Text = string.Empty;
        TextBoxWarna.Enabled = true;

        DropDownListVarian.SelectedValue = "-1";
        TextBoxVarian.Text = string.Empty;
        TextBoxVarian.Enabled = true;

        TextBoxKode.Text = string.Empty;

        CheckBoxListKategori.ClearSelection();

        TextBoxHargaPokokProduksi.Text = string.Empty;
        TextBoxHargaJual.Text = string.Empty;

        CollapseBiayaProduksi.Attributes.Add("class", "collapse");
        CollapseKomposisiProduk.Attributes.Add("class", "collapse");
    }
    #endregion
}