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
                TBSatuan[] daftarSatuan = db.TBSatuans.OrderBy(item => item.Nama).ToArray();
                DropDownListSatuanKecil.DataSource = daftarSatuan;
                DropDownListSatuanKecil.DataTextField = "Nama";
                DropDownListSatuanKecil.DataValueField = "IDSatuan";
                DropDownListSatuanKecil.DataBind();
                DropDownListSatuanKecil.Items.Insert(0, new ListItem { Text = "-Pilih Satuan-", Value = "0" });

                DropDownListSatuanBesar.DataSource = daftarSatuan;
                DropDownListSatuanBesar.DataTextField = "Nama";
                DropDownListSatuanBesar.DataValueField = "IDSatuan";
                DropDownListSatuanBesar.DataBind();
                DropDownListSatuanBesar.Items.Insert(0, new ListItem { Text = "-Pilih Satuan-", Value = "0" });

                CheckBoxListKategori.DataSource = db.TBKategoriBahanBakus.Select(item => new { item.IDKategoriBahanBaku, item.Nama }).ToArray();
                CheckBoxListKategori.DataValueField = "IDKategoriBahanBaku";
                CheckBoxListKategori.DataTextField = "Nama";
                CheckBoxListKategori.DataBind();

                TBBahanBaku bahanBaku = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Request.QueryString["id"].ToInt());

                if (bahanBaku != null)
                {
                    TextBoxNama.Text = bahanBaku.Nama;
                    TextBoxKodeBahanBaku.Text = bahanBaku.KodeBahanBaku;
                    TextBoxKonversi.Text = bahanBaku.Konversi.ToFormatHarga();
                    TextBoxBerat.Text = bahanBaku.Berat.ToString();
                    TextBoxDeskripsi.Text = bahanBaku.Deskripsi;
                    DropDownListSatuanKecil.SelectedValue = bahanBaku.IDSatuan.ToString();
                    DropDownListSatuanBesar.SelectedValue = bahanBaku.IDSatuanKonversi.ToString();
                    
                    TBStokBahanBaku stokBahanBaku = bahanBaku.TBStokBahanBakus.FirstOrDefault(item => item.IDTempat == pengguna.IDTempat);
                    if (stokBahanBaku != null)
                    {
                        TextBoxHargaBeli.Text = (stokBahanBaku.HargaBeli * bahanBaku.Konversi).ToFormatHarga();
                        TextBoxStok.Text = (stokBahanBaku.Jumlah / bahanBaku.Konversi).ToFormatHarga();
                        TextBoxBatasStokAkanHabis.Text = (stokBahanBaku.JumlahMinimum / bahanBaku.Konversi).ToFormatHarga();
                    }

                    LabelSatuanHargaBeli.Text = "/" + DropDownListSatuanBesar.SelectedItem.Text;
                    LabelSatuanKonversi.Text = DropDownListSatuanKecil.SelectedItem.Text;
                    LabelSatuanStok.Text = DropDownListSatuanBesar.SelectedItem.Text;
                    LabelSatuanStokAkanHabis.Text = DropDownListSatuanBesar.SelectedItem.Text;

                    if (bahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0)
                    {
                        foreach (var item in bahanBaku.TBRelasiBahanBakuKategoriBahanBakus)
                            CheckBoxListKategori.Items.FindByValue(item.IDKategoriBahanBaku.ToString()).Selected = true;
                    }

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
    protected void DropDownListSatuanBesar_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListSatuanBesar.SelectedValue != "0")
        {
            LabelSatuanHargaBeli.Text = "/" + DropDownListSatuanBesar.SelectedItem.Text;
            LabelSatuanStok.Text = DropDownListSatuanBesar.SelectedItem.Text;
            LabelSatuanStokAkanHabis.Text = DropDownListSatuanBesar.SelectedItem.Text;
        }
        else
        {
            LabelSatuanHargaBeli.Text = string.Empty;
            LabelSatuanStok.Text = string.Empty;
            LabelSatuanStokAkanHabis.Text = string.Empty;
        }
    }
    protected void DropDownListSatuanKecil_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListSatuanKecil.SelectedValue != "0")
            LabelSatuanKonversi.Text = DropDownListSatuanKecil.SelectedItem.Text;
        else
            LabelSatuanKonversi.Text = string.Empty;
    }
    #endregion

    #region MASTER
    private void TambahKategori(TBBahanBaku bahanBaku, DataClassesDatabaseDataContext db)
    {
        //reset kategori produk
        var kategori = db.TBRelasiBahanBakuKategoriBahanBakus.Where(item => item.TBBahanBaku == bahanBaku).ToArray();

        if (kategori.Count() > 0)
        {
            db.TBRelasiBahanBakuKategoriBahanBakus.DeleteAllOnSubmit(kategori);
        }

        foreach (ListItem item in CheckBoxListKategori.Items)
        {
            if (item.Selected)
            {
                db.TBRelasiBahanBakuKategoriBahanBakus.InsertOnSubmit(new TBRelasiBahanBakuKategoriBahanBaku
                {
                    TBBahanBaku = bahanBaku,
                    IDKategoriBahanBaku = item.Value.ToInt()
                });
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBBahanBaku bahanBaku;
                decimal hargaBeli = TextBoxHargaBeli.Text.ToDecimal() / TextBoxKonversi.Text.ToDecimal();
                decimal stok = TextBoxStok.Text.ToDecimal() * TextBoxKonversi.Text.ToDecimal();
                decimal batasStok = TextBoxBatasStokAkanHabis.Text.ToDecimal() * TextBoxKonversi.Text.ToDecimal();

                if (ButtonSimpan.Text == "Tambah")
                {
                    TBSatuan satuanKecil = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == DropDownListSatuanKecil.SelectedValue.ToInt());
                    TBSatuan satuanBesar = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == DropDownListSatuanBesar.SelectedValue.ToInt());

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

                    TambahKategori(bahanBaku, db);

                    TBStokBahanBaku stokBahanBaku = StokBahanBaku_Class.InsertStokBahanBaku(db, DateTime.Now, pengguna.IDPengguna, pengguna.IDTempat, bahanBaku, hargaBeli, stok, batasStok, "Stok Baru Manual");
                }
                else if (ButtonSimpan.Text == "Ubah")
                {
                    TBSatuan satuanKecil = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == DropDownListSatuanKecil.SelectedValue.ToInt());
                    TBSatuan satuanBesar = db.TBSatuans.FirstOrDefault(item => item.IDSatuan == DropDownListSatuanBesar.SelectedValue.ToInt());

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

                    TambahKategori(bahanBaku, db);

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

    #region VALIDATION
    protected void CustomValidatorSatuanBesar_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownListSatuanBesar.SelectedValue == "0")
        {
            args.IsValid = false;
            CustomValidatorSatuanBesar.Text = "Satuan harus dipilih";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorSatuanBesar.Text = "-";
        }
    }
    protected void CustomValidatorSatuanKecil_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownListSatuanKecil.SelectedValue == "0")
        {
            args.IsValid = false;
            CustomValidatorSatuanKecil.Text = "Satuan harus dipilih";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorSatuanKecil.Text = "-";
        }
    }
    #endregion
}