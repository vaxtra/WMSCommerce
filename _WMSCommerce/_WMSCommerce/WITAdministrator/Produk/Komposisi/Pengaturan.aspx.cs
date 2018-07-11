using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Komposisi_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDKombinasiProduk == Request.QueryString["id"].ToInt() && item.IDTempat == pengguna.IDTempat);

                LabelNamaKombinasiProduk.Text = "Komposisi " + stokProduk.TBKombinasiProduk.Nama;

                LabelHargaPokokSaatIni.Text = stokProduk.HargaBeli.ToFormatHarga();

                DropDownListBahanBaku.DataSource = db.TBStokBahanBakus
                    .Where(item => item.IDTempat == pengguna.IDTempat)
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

                LoadData(db, stokProduk.TBKombinasiProduk);
            }
        }
    }

    private void LoadData(DataClassesDatabaseDataContext db, TBKombinasiProduk kombinasiProduk)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterKomposisi.DataSource = kombinasiProduk.TBKomposisiKombinasiProduks.Select(item => new
        {
            item.IDBahanBaku,
            item.TBBahanBaku.Nama,
            Jumlah = item.Jumlah.ToFormatHarga(),
            Satuan = item.TBBahanBaku.TBSatuan.Nama,
            HargaBeli = item.TBBahanBaku.TBStokBahanBakus.FirstOrDefault(stok => stok.IDBahanBaku == item.IDBahanBaku && stok.IDTempat == pengguna.IDTempat).HargaBeli * item.Jumlah
        }).ToArray();
        RepeaterKomposisi.DataBind();

        RepeaterBiayaProduksi.DataSource = kombinasiProduk.TBRelasiJenisBiayaProduksiKombinasiProduks.Select(item => new
        {
            item.IDJenisBiayaProduksi,
            NamaJenisBiayaProduksi = item.TBJenisBiayaProduksi.Nama,
            Jenis = item.EnumBiayaProduksi == (int)PilihanBiayaProduksi.Persen ? (item.Persentase * 100).ToFormatHarga() + "% dari Komposisi Bahan Baku" : "Nominal",
            BiayaProduksi = item.EnumBiayaProduksi == (int)PilihanBiayaProduksi.Persen ? (item.Persentase * StokProduk_Class.HitungHargaPokokKomposisi(db, pengguna.IDTempat, item.TBKombinasiProduk)).ToFormatHarga() : item.Nominal.ToFormatHarga()
        }).ToArray();
        RepeaterBiayaProduksi.DataBind();

        decimal hargaKomposisi = StokProduk_Class.HitungHargaPokokKomposisi(db, pengguna.IDTempat, kombinasiProduk);
        LabelTotalHargaBesarKomposisi.Text = hargaKomposisi.ToFormatHarga();

        decimal hargaBiayaProduksi = StokProduk_Class.HitungBiayaProduksi(db, pengguna.IDTempat, kombinasiProduk);
        LabelTotalHargaBesarBiayaProduksi.Text = hargaBiayaProduksi.ToFormatHarga();

        LabelHitunganKomposisi.Text = LabelTotalHargaBesarKomposisi.Text;
        LabelHitunganBiayaProduksi.Text = hargaBiayaProduksi.ToFormatHarga();
        LabelHargaPokokProduksi.Text = (hargaKomposisi + hargaBiayaProduksi).ToFormatHarga();
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
                TBKomposisiKombinasiProduk komposisiKombinasiProduk = db.TBKomposisiKombinasiProduks.FirstOrDefault(item => item.IDKombinasiProduk == Request.QueryString["id"].ToInt() && item.IDBahanBaku == DropDownListBahanBaku.SelectedValue.ToInt());

                if (komposisiKombinasiProduk == null)
                {
                    komposisiKombinasiProduk = new TBKomposisiKombinasiProduk
                    {
                        IDKombinasiProduk = Request.QueryString["id"].ToInt(),
                        IDBahanBaku = DropDownListBahanBaku.SelectedValue.ToInt(),
                        Jumlah = TextBoxJumlahBahanBaku.Text.ToDecimal(),
                        Keterangan = null
                    };
                    db.TBKomposisiKombinasiProduks.InsertOnSubmit(komposisiKombinasiProduk);
                }
                else
                {
                    komposisiKombinasiProduk.Jumlah = TextBoxJumlahBahanBaku.Text.ToDecimal();
                    komposisiKombinasiProduk.Keterangan = null;
                }

                db.SubmitChanges();

                TextBoxJumlahBahanBaku.Text = "0";

                LoadData(db, komposisiKombinasiProduk.TBKombinasiProduk);
            }
        }
    }

    protected void RepeaterKomposisi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBKomposisiKombinasiProduk komposisiKombinasiProduk = db.TBKomposisiKombinasiProduks.FirstOrDefault(item => item.IDKombinasiProduk == Request.QueryString["id"].ToInt() && item.IDBahanBaku == e.CommandArgument.ToInt());
                db.TBKomposisiKombinasiProduks.DeleteOnSubmit(komposisiKombinasiProduk);
                db.SubmitChanges();

                LoadData(db, komposisiKombinasiProduk.TBKombinasiProduk);
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
                TBRelasiJenisBiayaProduksiKombinasiProduk relasiJenisBiayaProduksiKombinasiProduk = db.TBRelasiJenisBiayaProduksiKombinasiProduks
                    .FirstOrDefault(item => item.IDJenisBiayaProduksi == DropDownListJenisBiayaProduksi.SelectedValue.ToInt() && item.IDKombinasiProduk == Request.QueryString["id"].ToInt());

                if (relasiJenisBiayaProduksiKombinasiProduk == null)
                {
                    relasiJenisBiayaProduksiKombinasiProduk = new TBRelasiJenisBiayaProduksiKombinasiProduk
                    {
                        IDJenisBiayaProduksi = DropDownListJenisBiayaProduksi.SelectedValue.ToInt(),
                        IDKombinasiProduk = Request.QueryString["id"].ToInt(),

                    };

                    if (DropDownListEnumBiayaProduksi.SelectedValue == "Persentase")
                    {
                        relasiJenisBiayaProduksiKombinasiProduk.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Persen;
                        relasiJenisBiayaProduksiKombinasiProduk.Persentase = (TextBoxBiayaProduksi.Text.ToDecimal() / 100);
                        relasiJenisBiayaProduksiKombinasiProduk.Nominal = 0;
                        db.TBRelasiJenisBiayaProduksiKombinasiProduks.InsertOnSubmit(relasiJenisBiayaProduksiKombinasiProduk);
                    }
                    else if (DropDownListEnumBiayaProduksi.SelectedValue == "Nominal")
                    {
                        relasiJenisBiayaProduksiKombinasiProduk.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Harga;
                        relasiJenisBiayaProduksiKombinasiProduk.Persentase = 0;
                        relasiJenisBiayaProduksiKombinasiProduk.Nominal = TextBoxBiayaProduksi.Text.ToDecimal();
                        db.TBRelasiJenisBiayaProduksiKombinasiProduks.InsertOnSubmit(relasiJenisBiayaProduksiKombinasiProduk);
                    }
                }
                else
                {
                    if (DropDownListEnumBiayaProduksi.SelectedValue == "Persentase")
                    {
                        relasiJenisBiayaProduksiKombinasiProduk.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Persen;
                        relasiJenisBiayaProduksiKombinasiProduk.Persentase = (TextBoxBiayaProduksi.Text.ToDecimal() / 100);
                        relasiJenisBiayaProduksiKombinasiProduk.Nominal = 0;
                    }
                    else if (DropDownListEnumBiayaProduksi.SelectedValue == "Nominal")
                    {
                        relasiJenisBiayaProduksiKombinasiProduk.EnumBiayaProduksi = (int)PilihanBiayaProduksi.Harga;
                        relasiJenisBiayaProduksiKombinasiProduk.Persentase = 0;
                        relasiJenisBiayaProduksiKombinasiProduk.Nominal = TextBoxBiayaProduksi.Text.ToDecimal();
                    }
                }

                db.SubmitChanges();

                LoadData(db, relasiJenisBiayaProduksiKombinasiProduk.TBKombinasiProduk);

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
                TBRelasiJenisBiayaProduksiKombinasiProduk relasiJenisBiayaProduksiKombinasiProduk = db.TBRelasiJenisBiayaProduksiKombinasiProduks.FirstOrDefault(item => item.IDJenisBiayaProduksi == e.CommandArgument.ToInt() && item.IDKombinasiProduk == Request.QueryString["id"].ToInt());
                db.TBRelasiJenisBiayaProduksiKombinasiProduks.DeleteOnSubmit(relasiJenisBiayaProduksiKombinasiProduk);
                db.SubmitChanges();

                LoadData(db, relasiJenisBiayaProduksiKombinasiProduk.TBKombinasiProduk);
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

                LoadData(db, db.TBKombinasiProduks.FirstOrDefault(item => item.IDKombinasiProduk == Request.QueryString["id"].ToInt()));

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