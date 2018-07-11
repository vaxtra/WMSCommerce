﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_ProduksiKeSupplier_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariBulanPO.SelectedValue = DateTime.Now.Month.ToString();
                DropDownListCariTahunPO.Items.Insert(0, new ListItem { Value = (DateTime.Now.Year - 3).ToString(), Text = (DateTime.Now.Year - 3).ToString() });
                DropDownListCariTahunPO.Items.Insert(1, new ListItem { Value = (DateTime.Now.Year - 2).ToString(), Text = (DateTime.Now.Year - 2).ToString() });
                DropDownListCariTahunPO.Items.Insert(2, new ListItem { Value = (DateTime.Now.Year - 1).ToString(), Text = (DateTime.Now.Year - 1).ToString() });
                DropDownListCariTahunPO.Items.Insert(3, new ListItem { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
                DropDownListCariTahunPO.SelectedValue = DateTime.Now.Year.ToString();

                DropDownListCariPegawaiPO.DataSource = db.TBPenggunas.ToArray();
                DropDownListCariPegawaiPO.DataTextField = "NamaLengkap";
                DropDownListCariPegawaiPO.DataValueField = "IDPengguna";
                DropDownListCariPegawaiPO.DataBind();
                DropDownListCariPegawaiPO.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariSupplierPO.DataSource = db.TBSuppliers.ToArray();
                DropDownListCariSupplierPO.DataTextField = "Nama";
                DropDownListCariSupplierPO.DataValueField = "IDSupplier";
                DropDownListCariSupplierPO.DataBind();
                DropDownListCariSupplierPO.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariBulanPenerimaan.SelectedValue = DateTime.Now.Month.ToString();
                DropDownListCariTahunPenerimaan.Items.Insert(0, new ListItem { Value = (DateTime.Now.Year - 3).ToString(), Text = (DateTime.Now.Year - 3).ToString() });
                DropDownListCariTahunPenerimaan.Items.Insert(1, new ListItem { Value = (DateTime.Now.Year - 2).ToString(), Text = (DateTime.Now.Year - 2).ToString() });
                DropDownListCariTahunPenerimaan.Items.Insert(2, new ListItem { Value = (DateTime.Now.Year - 1).ToString(), Text = (DateTime.Now.Year - 1).ToString() });
                DropDownListCariTahunPenerimaan.Items.Insert(3, new ListItem { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
                DropDownListCariTahunPenerimaan.SelectedValue = DateTime.Now.Year.ToString();

                DropDownListCariPegawaiPenerimaan.DataSource = db.TBPenggunas.ToArray();
                DropDownListCariPegawaiPenerimaan.DataTextField = "NamaLengkap";
                DropDownListCariPegawaiPenerimaan.DataValueField = "IDPengguna";
                DropDownListCariPegawaiPenerimaan.DataBind();
                DropDownListCariPegawaiPenerimaan.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariSupplierPenerimaan.DataSource = db.TBSuppliers.ToArray();
                DropDownListCariSupplierPenerimaan.DataTextField = "Nama";
                DropDownListCariSupplierPenerimaan.DataValueField = "IDSupplier";
                DropDownListCariSupplierPenerimaan.DataBind();
                DropDownListCariSupplierPenerimaan.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadDataPO(db);
                LoadDataPenerimaan(db);
            }
        }
    }

    #region PO
    private void LoadDataPO(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPO.DataSource = db.TBPOProduksiBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor &&
            (!string.IsNullOrWhiteSpace(TextBoxCariIDPOProduksiBahanBaku.Text) ? item.IDPOProduksiBahanBaku.Contains(TextBoxCariIDPOProduksiBahanBaku.Text.ToUpper()) : true) &&
            (DropDownListCariBulanPO.SelectedValue != "0" ? item.Tanggal.Month == DropDownListCariBulanPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariTahunPO.SelectedValue != "0" ? item.Tanggal.Year == DropDownListCariTahunPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariPegawaiPO.SelectedValue != "0" ? item.IDPengguna == DropDownListCariPegawaiPO.SelectedValue.ToInt() : true) &&
            (DropDownListCariSupplierPO.SelectedValue != "0" ? item.IDSupplier == DropDownListCariSupplierPO.SelectedValue.ToInt() : true))
        .Select(item => new
        {
            item.IDPOProduksiBahanBaku,
            item.IDProyeksi,
            item.Nomor,
            item.Tanggal,
            Pegawai = item.TBPengguna.NamaLengkap,
            Supplier = item.TBSupplier.Nama,
            item.Grandtotal,
            StatusKirim = item.TBPOProduksiBahanBakuKomposisis.Sum(data => data.Sisa) > 0 ? "class='btn btn-info btn-xs'" : "class='d-none'",
            CetakPO = "return popitup('../Cetak.aspx?id=" + item.IDPOProduksiBahanBaku + "')",
            Hapus = item.TBPenerimaanPOProduksiBahanBakus.Count == 0 && item.TBPengirimanPOProduksiBahanBakus.Count == 0 ? "btn btn-danger btn-xs" : "d-none"
        }).OrderByDescending(item => item.Nomor).ToArray();
        RepeaterDataPO.DataBind();

    }

    protected void RepeaterDataPO_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == e.CommandArgument.ToString());

                if (poProduksiBahanBaku.IDProyeksi != null)
                {
                    foreach (var item in db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == poProduksiBahanBaku.IDProyeksi && item.LevelProduksi == Request.QueryString["urutan"].ToInt() && item.BahanBakuDasar == false).OrderBy(data => data.TBBahanBaku.Nama).ToArray())
                    {
                        TBPOProduksiBahanBakuDetail poProduksiBahanBakuDetail = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                        if (poProduksiBahanBakuDetail != null)
                        {
                            item.Sisa = item.Sisa + (poProduksiBahanBakuDetail.Jumlah * poProduksiBahanBakuDetail.TBBahanBaku.Konversi.Value);
                        }
                    }
                }

                db.TBPOProduksiBahanBakuBiayaTambahans.DeleteAllOnSubmit(poProduksiBahanBaku.TBPOProduksiBahanBakuBiayaTambahans);
                db.TBPOProduksiBahanBakuKomposisis.DeleteAllOnSubmit(poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis);
                db.TBPOProduksiBahanBakuDetails.DeleteAllOnSubmit(poProduksiBahanBaku.TBPOProduksiBahanBakuDetails);
                db.TBPOProduksiBahanBakus.DeleteOnSubmit(poProduksiBahanBaku);
                db.SubmitChanges();

                LoadDataPO(db);
            }
        }
    }

    protected void Event_CariPO(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadDataPO(db);
        }
    }
    #endregion

    #region Penerimaan
    private void LoadDataPenerimaan(DataClassesDatabaseDataContext db)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        RepeaterDataPenerimaan.DataSource = db.TBPenerimaanPOProduksiBahanBakus.Where(item => item.TBPOProduksiBahanBaku.IDTempat == pengguna.IDTempat && item.TBPOProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor &&
                (!string.IsNullOrWhiteSpace(TextBoxCariIDPenerimaanPOProduksiBahanBaku.Text) ? item.IDPenerimaanPOProduksiBahanBaku.Contains(TextBoxCariIDPenerimaanPOProduksiBahanBaku.Text.ToUpper()) : true) &&
                (DropDownListCariBulanPenerimaan.SelectedValue != "0" ? item.TanggalTerima.Value.Month == DropDownListCariBulanPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariTahunPenerimaan.SelectedValue != "0" ? item.TanggalTerima.Value.Year == DropDownListCariTahunPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariPegawaiPenerimaan.SelectedValue != "0" ? item.IDPenggunaTerima == DropDownListCariPegawaiPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariSupplierPenerimaan.SelectedValue != "0" ? item.TBPOProduksiBahanBaku.IDSupplier == DropDownListCariSupplierPenerimaan.SelectedValue.ToInt() : true) &&
                (DropDownListCariStatusPenerimaan.SelectedValue != "0" ? (DropDownListCariStatusPenerimaan.SelectedValue != "Baru" ? (DropDownListCariStatusPenerimaan.SelectedValue != "Kontra" ? item.TBPOProduksiBahanBakuPenagihan.StatusPembayaran == true : item.TBPOProduksiBahanBakuPenagihan.StatusPembayaran == false) : item.TBPOProduksiBahanBakuPenagihan == null) : true))
            .Select(item => new
            {
                item.IDPenerimaanPOProduksiBahanBaku,
                item.Nomor,
                item.TanggalTerima,
                Pegawai = item.TBPengguna.NamaLengkap,
                Supplier = item.TBPOProduksiBahanBaku.TBSupplier.Nama,
                item.Grandtotal,
                Status = item.TBPOProduksiBahanBakuPenagihan != null ? item.TBPOProduksiBahanBakuPenagihan.StatusPembayaran == false ? "<label class=\"label label-warning\">Kontra</label>" : "<label class=\"label label-success\">Lunas</label>" : "<label class=\"label label-default\">Baru</label>",
                CetakPO = "return popitup('../CetakPenerimaan.aspx?id=" + item.IDPenerimaanPOProduksiBahanBaku + "')",
            }).OrderByDescending(item => item.Nomor).ToArray();
        RepeaterDataPenerimaan.DataBind();
    }

    protected void RepeaterDataPenerimaan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Bayar")
        {
            Response.Redirect("../Penagihan/Pengaturan.aspx?id=" + e.CommandArgument);
        }
        else if (e.CommandName == "Batal")
        {
            Response.Redirect("../Penagihan/Pengaturan.aspx?id=" + e.CommandArgument);
        }
    }

    protected void Event_CariPenerimaan(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadDataPenerimaan(db);
        }
    }
    #endregion

    protected void ButtonPO_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }

    protected void ButtonPenerimaan_Click(object sender, EventArgs e)
    {
        Response.Redirect("Penerimaan.aspx");
    }
}