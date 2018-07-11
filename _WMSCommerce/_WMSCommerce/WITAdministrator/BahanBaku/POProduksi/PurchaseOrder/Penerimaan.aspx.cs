using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_PurchaseOrder_Penerimaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TextBoxIDProyeksi.Text = "-Tidak Ada Proyeksi-";

                TextBoxTanggalPenerimaan.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxPegawai.Text = pengguna.NamaLengkap;

                DropDownListCariSupplier.DataSource = db.TBSuppliers.OrderBy(item => item.Nama);
                DropDownListCariSupplier.DataTextField = "Nama";
                DropDownListCariSupplier.DataValueField = "IDSupplier";
                DropDownListCariSupplier.DataBind();
                DropDownListCariSupplier.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListIDPOProduksi.Items.Insert(0, new ListItem { Value = "0", Text = "-Pilih-" });
                DropDownListIDPOProduksi.Enabled = false;
            }
        }
    }

    protected void DropDownListCariSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterDetail.DataSource = null;
            RepeaterDetail.DataBind();

            if (DropDownListCariSupplier.SelectedValue != "0")
            {
                DropDownListIDPOProduksi.Enabled = true;
                DropDownListIDPOProduksi.DataSource = db.TBPOProduksiBahanBakus.Where(item => item.IDSupplier == DropDownListCariSupplier.SelectedValue.ToInt() && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder && item.TBPOProduksiBahanBakuDetails.Sum(item2 => item2.Sisa) > 0).OrderByDescending(item => item.Nomor);
                DropDownListIDPOProduksi.DataTextField = "IDPOProduksiBahanBaku";
                DropDownListIDPOProduksi.DataValueField = "IDPOProduksiBahanBaku";
                DropDownListIDPOProduksi.DataBind();
                DropDownListIDPOProduksi.Items.Insert(0, new ListItem { Value = "0", Text = "-Pilih-" });
            }
            else
            {
                DropDownListIDPOProduksi.SelectedValue = "0";
                DropDownListIDPOProduksi.Enabled = false;
            }
        }
    }

    protected void DropDownListIDPOProduksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListIDPOProduksi.SelectedValue != "0")
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == DropDownListIDPOProduksi.SelectedValue);
                TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman.ToFormatTanggal();

                var SisaDataDetailPOProduk = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.AsEnumerable()
                    .Where(item => item.Sisa > 0)
                    .Select(item => new
                    {
                        item.IDBahanBaku,
                        BahanBaku = item.TBBahanBaku.Nama,
                        Satuan = item.TBSatuan.Nama,
                        Sisa = item.Sisa.ToFormatHarga()
                    });

                if (SisaDataDetailPOProduk.Count() == 0)
                    ButtonTerima.Visible = false;

                RepeaterDetail.DataSource = SisaDataDetailPOProduk;
                RepeaterDetail.DataBind();
            }
            else
            {
                RepeaterDetail.DataSource = null;
                RepeaterDetail.DataBind();
            }
        }
    }

    protected void ButtonTerima_Click(object sender, EventArgs e)
    {
        peringatan.Visible = false;
        bool statusBerhasil = false;
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        string IDPenerimaanPOProduksiBahanBaku = string.Empty;
        string IDTransferBahanBaku = string.Empty;
        TBPenerimaanPOProduksiBahanBaku penerimaan = null;

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == DropDownListIDPOProduksi.SelectedValue);

                bool penerimaanSesuai = true;

                EntitySet<TBPenerimaanPOProduksiBahanBakuDetail> daftarDetail = new EntitySet<TBPenerimaanPOProduksiBahanBakuDetail>();

                int baris = 0;

                foreach (RepeaterItem item in RepeaterDetail.Items)
                {
                    baris++;

                    Label LabelIDBahanBaku = (Label)item.FindControl("LabelIDBahanBaku");
                    TextBox TextBoxSisaPesanan = (TextBox)item.FindControl("TextBoxSisaPesanan");
                    TextBox TextBoxJumlahDatang = (TextBox)item.FindControl("TextBoxJumlahDatang");
                    TextBox TextBoxJumlahTerima = (TextBox)item.FindControl("TextBoxJumlahTerima");
                    TextBox TextBoxJumlahTolakKeSupplier = (TextBox)item.FindControl("TextBoxJumlahTolakKeSupplier");

                    if (TextBoxJumlahDatang.Text.ToDecimal() > 0)
                    {
                        if (TextBoxJumlahDatang.Text.ToDecimal() >= TextBoxJumlahTerima.Text.ToDecimal())
                        {
                            TBPOProduksiBahanBakuDetail poProduksiBahanBakuDetail = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.FirstOrDefault(data => data.IDBahanBaku == LabelIDBahanBaku.Text.ToInt());

                            daftarDetail.Add(new TBPenerimaanPOProduksiBahanBakuDetail()
                            {
                                TBBahanBaku = poProduksiBahanBakuDetail.TBBahanBaku,
                                TBSatuan = poProduksiBahanBakuDetail.TBSatuan,
                                BiayaTambahan = poProduksiBahanBakuDetail.BiayaTambahan,
                                HargaPokokKomposisi = poProduksiBahanBakuDetail.HargaPokokKomposisi,
                                TotalHPP = poProduksiBahanBakuDetail.TotalHPP,
                                HargaSupplier = poProduksiBahanBakuDetail.HargaSupplier,
                                PotonganHargaSupplier = poProduksiBahanBakuDetail.PotonganHargaSupplier,
                                TotalHargaSupplier = poProduksiBahanBakuDetail.TotalHargaSupplier,
                                Datang = TextBoxJumlahDatang.Text.ToDecimal(),
                                Diterima = TextBoxJumlahTerima.Text.ToDecimal(),
                                TolakKeSupplier = TextBoxJumlahTolakKeSupplier.Text.ToDecimal(),
                                Sisa = TextBoxJumlahTerima.Text.ToDecimal() <= TextBoxSisaPesanan.Text.ToDecimal() ? TextBoxSisaPesanan.Text.ToDecimal() - TextBoxJumlahTerima.Text.ToDecimal() : 0
                            });
                        }
                        else
                        {
                            LabelPeringatan.Text = "Jumlah diterima lebih besar dari jumlah datang, baris ke-" + baris.ToString();
                            peringatan.Visible = true;
                            penerimaanSesuai = false;
                            break;
                        }
                    }
                }

                if (penerimaanSesuai == true)
                {
                    TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

                    db.Proc_InsertPenerimaanPOProduksiBahanBaku(ref IDPenerimaanPOProduksiBahanBaku, poProduksiBahanBaku.IDPOProduksiBahanBaku, poProduksiBahanBaku.IDSupplier, pengguna.IDTempat, pengguna.IDPengguna, DateTime.Parse(TextBoxTanggalPenerimaan.Text).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                    penerimaan = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPenerimaanPOProduksiBahanBaku == IDPenerimaanPOProduksiBahanBaku);

                    penerimaan.IDPenggunaTerima = penerimaan.IDPenggunaDatang;
                    penerimaan.TanggalTerima = penerimaan.TanggalDatang;
                    penerimaan.TBPenerimaanPOProduksiBahanBakuDetails = daftarDetail;
                    penerimaan.TotalDatang = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.Datang);
                    penerimaan.TotalDiterima = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.Diterima);
                    penerimaan.TotalTolakKeSupplier = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TolakKeSupplier);
                    penerimaan.TotalSisa = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.Sisa);
                    penerimaan.SubtotalBiayaTambahan = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.BiayaTambahan * item.Diterima);
                    penerimaan.SubtotalTotalHPP = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TotalHPP * item.Diterima);
                    penerimaan.SubtotalTotalHargaSupplier = penerimaan.TBPenerimaanPOProduksiBahanBakuDetails.Sum(item => item.TotalHargaSupplier * item.Diterima);
                    penerimaan.Grandtotal = penerimaan.SubtotalTotalHPP + penerimaan.SubtotalTotalHargaSupplier;
                    penerimaan.EnumStatusPenerimaan = (int)PilihanEnumStatusPenerimaanPO.Terima;
                    penerimaan.Keterangan = TextBoxKeterangan.Text;

                    foreach (var item in penerimaan.TBPenerimaanPOProduksiBahanBakuDetails)
                    {
                        TBPOProduksiBahanBakuDetail poProduksiBahanBakuDetail = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                        poProduksiBahanBakuDetail.Sisa = item.Sisa;

                        TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

                        StokBahanBaku_Class.PengaturanJumlahStokPenerimaanPOBahanBaku(db, penerimaan.TanggalTerima.Value, pengguna.IDPengguna, pengguna.IDTempat, stokBahanBaku, item.TotalHargaSupplier, item.TBSatuan, item.Datang, item.TolakKeSupplier, item.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDPOProduksiBahanBaku + " - #" + item.TBPenerimaanPOProduksiBahanBaku.IDPenerimaanPOProduksiBahanBaku);
                    }

                    poProduksiBahanBaku.TBPenerimaanPOProduksiBahanBakus.Add(penerimaan);

                    db.SubmitChanges();

                    statusBerhasil = true;
                }
                else
                    peringatan.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (statusBerhasil != true)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    penerimaan = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPenerimaanPOProduksiBahanBaku == IDPenerimaanPOProduksiBahanBaku);
                    if (penerimaan != null)
                    {
                        db.TBPenerimaanPOProduksiBahanBakuDetails.DeleteAllOnSubmit(penerimaan.TBPenerimaanPOProduksiBahanBakuDetails);
                        db.TBPenerimaanPOProduksiBahanBakus.DeleteOnSubmit(penerimaan);
                        db.SubmitChanges();

                        IDPenerimaanPOProduksiBahanBaku = string.Empty;
                    }
                }
            }
            LogError_Class LogError = new LogError_Class(ex, "Penerimaan Purchase Order Bahan Baku (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
            LabelPeringatan.Text = "Terjadi kesalahan, silahkan cek kembali data yang diinputkan";
            peringatan.Visible = true;
        }
        finally
        {
            if (statusBerhasil == true)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}