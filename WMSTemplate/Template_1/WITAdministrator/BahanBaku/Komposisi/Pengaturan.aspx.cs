using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Komposisi_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                var listStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == Pengguna.IDTempat).ToArray();
                TBStokBahanBaku stokBahanBaku = listStokBahanBaku.FirstOrDefault(item => item.IDBahanBaku == Request.QueryString["id"].ToInt());

                    LabelNamaBahanBaku.Text = stokBahanBaku.TBBahanBaku.Nama + " (" + stokBahanBaku.TBBahanBaku.TBSatuan1.Nama + ")";

                    LabelHargaPokokSaatIni.Text = (stokBahanBaku.HargaBeli).ToFormatHarga() + " /" + stokBahanBaku.TBBahanBaku.TBSatuan.Nama;

                    DropDownListBahanBaku.DataSource = listStokBahanBaku
                        .Where(item => item.IDBahanBaku != Request.QueryString["id"].ToInt())
                        .Select(item => new
                        {
                            item.TBBahanBaku.IDBahanBaku,
                            item.TBBahanBaku.Nama
                        }).OrderBy(item => item.Nama);
                    DropDownListBahanBaku.DataTextField = "Nama";
                    DropDownListBahanBaku.DataValueField = "IDBahanBaku";
                    DropDownListBahanBaku.DataBind();

                if (DropDownListBahanBaku.Items.Count > 0)
                {
                    LabelSatuan.Text = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt()).TBSatuan.Nama;
                }
                else
                {
                    ButtonSimpanKomposisi.Enabled = false;
                }

                LoadDataDropDownListJenisBiayaProduksi(db);

                LoadDataJenisBiayaProduksi(db);

                LoadData(db, stokBahanBaku.TBBahanBaku);
            }
        }
    }

    private void LoadData(DataClassesDatabaseDataContext db, TBBahanBaku bahanBaku)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterKomposisi.DataSource = bahanBaku.TBKomposisiBahanBakus.Select(item => new
        {
            item.IDBahanBaku,
            item.TBBahanBaku1.Nama,
            Jumlah = item.Jumlah.ToFormatHarga(),
            Satuan = item.TBBahanBaku1.TBSatuan.Nama,
            HargaBeli = item.TBBahanBaku1.TBStokBahanBakus.FirstOrDefault(stok => stok.IDBahanBaku == item.IDBahanBaku && stok.IDTempat == pengguna.IDTempat).HargaBeli * item.Jumlah
        }).ToArray();
        RepeaterKomposisi.DataBind();

        RepeaterBiayaProduksi.DataSource = bahanBaku.TBRelasiJenisBiayaProduksiBahanBakus.Select(item => new
        {
            item.IDJenisBiayaProduksi,
            NamaJenisBiayaProduksi = item.TBJenisBiayaProduksi.Nama,
            Jenis = item.EnumBiayaProduksi == (int)PilihanBiayaProduksi.Persen ? (item.Persentase * 100).ToFormatHarga() + "% dari Komposisi Bahan Baku" : "Nominal",
            BiayaProduksi = item.EnumBiayaProduksi == (int)PilihanBiayaProduksi.Persen ? (item.Persentase * StokBahanBaku_Class.HitungHargaPokokKomposisi(db, pengguna.IDTempat, item.TBBahanBaku)).ToFormatHarga() : item.Nominal.ToFormatHarga()
        }).ToArray();
        RepeaterBiayaProduksi.DataBind();

        decimal hargaKomposisi = StokBahanBaku_Class.HitungHargaPokokKomposisi(db, pengguna.IDTempat, bahanBaku);
        LabelTotalHargaBesarKomposisi.Text = hargaKomposisi.ToFormatHarga();
        LabelSatuanBesarKomposisi.Text = "/" + bahanBaku.TBSatuan1.Nama;
        LabelTotalHargaKecilKomposisi.Text = (hargaKomposisi / bahanBaku.Konversi).ToFormatHarga();
        LabelSatuanKecilKomposisi.Text = "/" + bahanBaku.TBSatuan.Nama;


        decimal hargaBiayaProduksi = StokBahanBaku_Class.HitungBiayaProduksi(db, pengguna.IDTempat, bahanBaku);
        LabelTotalHargaBesarBiayaProduksi.Text = hargaBiayaProduksi.ToFormatHarga();
        LabelSatuanBesarBiayaProduksi.Text = LabelSatuanBesarKomposisi.Text;
        LabelTotalHargaKecilBiayaProduksi.Text = (hargaBiayaProduksi / bahanBaku.Konversi).ToFormatHarga();
        LabelSatuanKecilBiayaProduksi.Text = LabelSatuanKecilKomposisi.Text;

        LabelHitunganKomposisi.Text = LabelTotalHargaKecilKomposisi.Text + " " + LabelSatuanKecilKomposisi.Text;
        LabelHitunganBiayaProduksi.Text = (hargaBiayaProduksi / bahanBaku.Konversi).ToFormatHarga() + " " + LabelSatuanKecilKomposisi.Text;
        LabelHargaPokokProduksi.Text = ((hargaKomposisi + hargaBiayaProduksi) / bahanBaku.Konversi).ToFormatHarga() + " " + LabelSatuanKecilKomposisi.Text;
    }

    #region Komposisi
    protected void DropDownListBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListBahanBaku.SelectedItem.Value.ToString() == "0")
        {
            LabelSatuan.Text = string.Empty;
        }
        else
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LabelSatuan.Text = db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt()).TBSatuan.Nama;
            }
        }
    }

    protected void ButtonSimpanKomposisi_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBKomposisiBahanBaku komposisiBahanBaku = db.TBKomposisiBahanBakus.FirstOrDefault(item => item.IDBahanBakuProduksi == Request.QueryString["id"].ToInt() && item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt());

                if (komposisiBahanBaku == null)
                {
                    komposisiBahanBaku = new TBKomposisiBahanBaku
                    {
                        IDBahanBakuProduksi = Request.QueryString["id"].ToInt(),
                        IDBahanBaku = DropDownListBahanBaku.SelectedValue.ToInt(),
                        Jumlah = TextBoxJumlahBahanBaku.Text.ToDecimal(),
                        Keterangan = null
                    };
                    db.TBKomposisiBahanBakus.InsertOnSubmit(komposisiBahanBaku);
                }
                else
                {
                    komposisiBahanBaku.Jumlah = TextBoxJumlahBahanBaku.Text.ToDecimal();
                    komposisiBahanBaku.Keterangan = null;
                }

                db.SubmitChanges();

                TextBoxJumlahBahanBaku.Text = "0";

                LoadData(db, komposisiBahanBaku.TBBahanBaku);
            }
        }
    }

    protected void RepeaterKomposisi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBKomposisiBahanBaku komposisiBahanBaku = db.TBKomposisiBahanBakus.FirstOrDefault(item => item.IDBahanBakuProduksi == Request.QueryString["id"].ToInt() && item.IDBahanBaku == e.CommandArgument.ToInt());
                db.TBKomposisiBahanBakus.DeleteOnSubmit(komposisiBahanBaku);
                db.SubmitChanges();

                LoadData(db, komposisiBahanBaku.TBBahanBaku);
            }
        }
    }
    #endregion

    #region Biaya Produksi
    protected void DropDownListEnumBiayaProduksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListEnumBiayaProduksi.SelectedValue == "Persentase")
        {
            LabelStatusBiayaProduksi.Text = "%";
        }
        else if (DropDownListEnumBiayaProduksi.SelectedValue == "Nominal")
        {
            LabelStatusBiayaProduksi.Text = "Nominal";
        }
    }

    protected void LoadDataDropDownListJenisBiayaProduksi(DataClassesDatabaseDataContext db)
    {
        DropDownListJenisBiayaProduksi.DataSource = db.TBJenisBiayaProduksis.ToArray();
        DropDownListJenisBiayaProduksi.DataTextField = "Nama";
        DropDownListJenisBiayaProduksi.DataValueField = "IDJenisBiayaProduksi";
        DropDownListJenisBiayaProduksi.DataBind();

        if (DropDownListJenisBiayaProduksi.Items.Count == 0)
        {
            ButtonSimpanBiayaProduksi.Enabled = false;
        }
    }

    protected void ButtonSimpanBiayaProduksi_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBRelasiJenisBiayaProduksiBahanBaku relasiJenisBiayaProduksiBahanBaku = db.TBRelasiJenisBiayaProduksiBahanBakus
                    .FirstOrDefault(item => item.IDJenisBiayaProduksi == DropDownListJenisBiayaProduksi.SelectedValue.ToInt() && item.IDBahanBaku == Request.QueryString["id"].ToInt());

                if (relasiJenisBiayaProduksiBahanBaku == null)
                {
                    relasiJenisBiayaProduksiBahanBaku = new TBRelasiJenisBiayaProduksiBahanBaku
                    {
                        IDJenisBiayaProduksi = DropDownListJenisBiayaProduksi.SelectedValue.ToInt(),
                        IDBahanBaku = Request.QueryString["id"].ToInt(),

                    };

                    if (DropDownListEnumBiayaProduksi.SelectedValue == "Persentase")
                    {
                        relasiJenisBiayaProduksiBahanBaku.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Persen;
                        relasiJenisBiayaProduksiBahanBaku.Persentase = (TextBoxBiayaProduksi.Text.ToDecimal() / 100);
                        relasiJenisBiayaProduksiBahanBaku.Nominal = 0;
                        db.TBRelasiJenisBiayaProduksiBahanBakus.InsertOnSubmit(relasiJenisBiayaProduksiBahanBaku);
                    }
                    else if (DropDownListEnumBiayaProduksi.SelectedValue == "Nominal")
                    {
                        relasiJenisBiayaProduksiBahanBaku.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Harga;
                        relasiJenisBiayaProduksiBahanBaku.Persentase = 0;
                        relasiJenisBiayaProduksiBahanBaku.Nominal = TextBoxBiayaProduksi.Text.ToDecimal();
                        db.TBRelasiJenisBiayaProduksiBahanBakus.InsertOnSubmit(relasiJenisBiayaProduksiBahanBaku);
                    }
                }
                else
                {
                    if (DropDownListEnumBiayaProduksi.SelectedValue == "Persentase")
                    {
                        relasiJenisBiayaProduksiBahanBaku.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Persen;
                        relasiJenisBiayaProduksiBahanBaku.Persentase = (TextBoxBiayaProduksi.Text.ToDecimal() / 100);
                        relasiJenisBiayaProduksiBahanBaku.Nominal = 0;
                    }
                    else if (DropDownListEnumBiayaProduksi.SelectedValue == "Nominal")
                    {
                        relasiJenisBiayaProduksiBahanBaku.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Harga;
                        relasiJenisBiayaProduksiBahanBaku.Persentase = 0;
                        relasiJenisBiayaProduksiBahanBaku.Nominal = TextBoxBiayaProduksi.Text.ToDecimal();
                    }
                }

                db.SubmitChanges();

                LoadData(db, relasiJenisBiayaProduksiBahanBaku.TBBahanBaku);

                TextBoxBiayaProduksi.Text = "0";
                DropDownListEnumBiayaProduksi.SelectedValue = "Persentase";
                LabelStatusBiayaProduksi.Text = "%";
            }
        }
    }

    protected void RepeaterBiayaProduksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBRelasiJenisBiayaProduksiBahanBaku relasiJenisBiayaProduksiBahanBaku = db.TBRelasiJenisBiayaProduksiBahanBakus.FirstOrDefault(item => item.IDJenisBiayaProduksi == e.CommandArgument.ToInt() && item.IDBahanBaku == Request.QueryString["id"].ToInt());
                db.TBRelasiJenisBiayaProduksiBahanBakus.DeleteOnSubmit(relasiJenisBiayaProduksiBahanBaku);
                db.SubmitChanges();

                LoadData(db, relasiJenisBiayaProduksiBahanBaku.TBBahanBaku);
            }
        }
    }
    #endregion

    #region Jenis Biaya Produksi

    protected void LoadDataJenisBiayaProduksi(DataClassesDatabaseDataContext db)
    {
        RepeaterJenisBiayaProduksi.DataSource = db.TBJenisBiayaProduksis.ToArray();
        RepeaterJenisBiayaProduksi.DataBind();
    }

    protected void ButtonOkJenisBiayaProduksi_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                if (ButtonOkJenisBiayaProduksi.Text == "Tambah")
                    db.TBJenisBiayaProduksis.InsertOnSubmit(new TBJenisBiayaProduksi { Nama = TextBoxNamaJenisBiayaProduksi.Text });
                else if (ButtonOkJenisBiayaProduksi.Text == "Ubah")
                {
                    TBJenisBiayaProduksi jenisBiayaProduksi = db.TBJenisBiayaProduksis.FirstOrDefault(item => item.IDJenisBiayaProduksi == HiddenFieldIDJenisBiayaProduksi.Value.ToInt());
                    jenisBiayaProduksi.Nama = TextBoxNamaJenisBiayaProduksi.Text;
                }

                db.SubmitChanges();

                LoadDataJenisBiayaProduksi(db);
                LoadDataDropDownListJenisBiayaProduksi(db);

                TextBoxNamaJenisBiayaProduksi.Text = string.Empty;
                ButtonOkJenisBiayaProduksi.Text = "Tambah";
            }
        }
    }

    protected void RepeaterJenisBiayaProduksi_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Hapus")
            {
                db.TBJenisBiayaProduksis.DeleteOnSubmit(db.TBJenisBiayaProduksis.FirstOrDefault(item => item.IDJenisBiayaProduksi == e.CommandArgument.ToInt()));
                db.SubmitChanges();

                LoadData(db, db.TBBahanBakus.FirstOrDefault(item => item.IDBahanBaku == Request.QueryString["id"].ToInt()));

                LoadDataDropDownListJenisBiayaProduksi(db);

                LoadDataJenisBiayaProduksi(db);
            }
            else if (e.CommandName == "Ubah")
            {
                TBJenisBiayaProduksi jenisBiayaProduksi = db.TBJenisBiayaProduksis.FirstOrDefault(item => item.IDJenisBiayaProduksi == e.CommandArgument.ToInt());

                if (jenisBiayaProduksi != null)
                {
                    HiddenFieldIDJenisBiayaProduksi.Value = jenisBiayaProduksi.IDJenisBiayaProduksi.ToString();
                    TextBoxNamaJenisBiayaProduksi.Text = jenisBiayaProduksi.Nama;

                    ButtonOkJenisBiayaProduksi.Text = "Ubah";
                }
            }
        }
    }
    #endregion
}