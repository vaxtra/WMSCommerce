using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_ProduksiKeVendor_Penerimaan : System.Web.UI.Page
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

                DropDownListCariVendor.DataSource = db.TBVendors.OrderBy(item => item.Nama);
                DropDownListCariVendor.DataTextField = "Nama";
                DropDownListCariVendor.DataValueField = "IDVendor";
                DropDownListCariVendor.DataBind();
                DropDownListCariVendor.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListIDPOProduksi.Items.Insert(0, new ListItem { Value = "0", Text = "-Pilih-" });
                DropDownListIDPOProduksi.Enabled = false;
            }
        }
    }

    protected void DropDownListCariVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterDetail.DataSource = null;
            RepeaterDetail.DataBind();

            if (DropDownListCariVendor.SelectedValue != "0")
            {
                DropDownListIDPOProduksi.Enabled = true;
                DropDownListIDPOProduksi.DataSource = db.TBPOProduksiProduks.Where(item => item.IDVendor == DropDownListCariVendor.SelectedValue.ToInt() && item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor && item.TBPOProduksiProdukDetails.Sum(item2 => item2.Sisa) > 0).OrderByDescending(item => item.Nomor);
                DropDownListIDPOProduksi.DataTextField = "IDPOProduksiProduk";
                DropDownListIDPOProduksi.DataValueField = "IDPOProduksiProduk";
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

                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == DropDownListIDPOProduksi.SelectedValue);
                TextBoxIDProyeksi.Text = poProduksiProduk.IDProyeksi != null ? poProduksiProduk.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxTanggalJatuhTempo.Text = poProduksiProduk.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman.ToFormatTanggal();

                var SisaDataDetailPOProduk = poProduksiProduk.TBPOProduksiProdukDetails.AsEnumerable()
                    .Where(item => item.Sisa > 0)
                    .Select(item => new
                    {
                        item.IDKombinasiProduk,
                        Produk = item.TBKombinasiProduk.TBProduk.Nama,
                        Atribut = item.TBKombinasiProduk.TBAtributProduk.Nama,
                        Sisa = item.Sisa.ToFormatHargaBulat()
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
        string IDPenerimaanPOProduksiProduk = string.Empty;
        string IDTransferProduk = string.Empty;
        TBPenerimaanPOProduksiProduk penerimaan = null;

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == DropDownListIDPOProduksi.SelectedValue);

                bool penerimaanSesuai = true;

                EntitySet<TBPenerimaanPOProduksiProdukDetail> daftarDetail = new EntitySet<TBPenerimaanPOProduksiProdukDetail>();

                int baris = 0;

                foreach (RepeaterItem item in RepeaterDetail.Items)
                {
                    baris++;

                    Label LabelIDKombinasiProduk = (Label)item.FindControl("LabelIDKombinasiProduk");
                    TextBox TextBoxSisaPesanan = (TextBox)item.FindControl("TextBoxSisaPesanan");
                    TextBox TextBoxJumlahDatang = (TextBox)item.FindControl("TextBoxJumlahDatang");
                    TextBox TextBoxJumlahTerima = (TextBox)item.FindControl("TextBoxJumlahTerima");
                    TextBox TextBoxJumlahTolakKeVendor = (TextBox)item.FindControl("TextBoxJumlahTolakKeVendor");

                    if (TextBoxJumlahDatang.Text.ToDecimal().ToInt() > 0)
                    {
                        if (TextBoxJumlahDatang.Text.ToDecimal().ToInt() >= TextBoxJumlahTerima.Text.ToDecimal().ToInt())
                        {
                            TBPOProduksiProdukDetail poProduksiProdukDetail = poProduksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == LabelIDKombinasiProduk.Text.ToInt());

                            daftarDetail.Add(new TBPenerimaanPOProduksiProdukDetail()
                            {
                                IDKombinasiProduk = poProduksiProdukDetail.IDKombinasiProduk,
                                BiayaTambahan = poProduksiProdukDetail.BiayaTambahan,
                                HargaPokokKomposisi = poProduksiProdukDetail.HargaPokokKomposisi,
                                TotalHPP = poProduksiProdukDetail.TotalHPP,
                                HargaVendor = poProduksiProdukDetail.HargaVendor,
                                PotonganHargaVendor = poProduksiProdukDetail.PotonganHargaVendor,
                                TotalHargaVendor = poProduksiProdukDetail.TotalHargaVendor,
                                Datang = TextBoxJumlahDatang.Text.ToDecimal().ToInt(),
                                Diterima = TextBoxJumlahTerima.Text.ToDecimal().ToInt(),
                                TolakKeVendor = TextBoxJumlahTolakKeVendor.Text.ToDecimal().ToInt(),
                                Sisa = TextBoxJumlahTerima.Text.ToDecimal().ToInt() <= TextBoxSisaPesanan.Text.ToDecimal().ToInt() ? TextBoxSisaPesanan.Text.ToDecimal().ToInt() - TextBoxJumlahTerima.Text.ToDecimal().ToInt() : 0
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
                    StokProduk_Class StokProduk_Class = new StokProduk_Class(db);
                    TBStokProduk[] daftarStokProduk = db.TBStokProduks.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

                    db.Proc_InsertPenerimaanPOProduksiProduk(ref IDPenerimaanPOProduksiProduk, poProduksiProduk.IDPOProduksiProduk, poProduksiProduk.IDVendor, pengguna.IDTempat, pengguna.IDPengguna, DateTime.Parse(TextBoxTanggalPenerimaan.Text).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                    penerimaan = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item => item.IDPenerimaanPOProduksiProduk == IDPenerimaanPOProduksiProduk);

                    penerimaan.IDPenggunaTerima = penerimaan.IDPenggunaDatang;
                    penerimaan.TanggalTerima = penerimaan.TanggalDatang;
                    penerimaan.TBPenerimaanPOProduksiProdukDetails = daftarDetail;
                    penerimaan.TotalDatang = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.Datang);
                    penerimaan.TotalDiterima = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.Diterima);
                    penerimaan.TotalTolakKeVendor = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TolakKeVendor);
                    penerimaan.TotalSisa = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.Sisa);
                    penerimaan.SubtotalBiayaTambahan = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.BiayaTambahan * item.Diterima);
                    penerimaan.SubtotalTotalHPP = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TotalHPP * item.Diterima);
                    penerimaan.SubtotalTotalHargaVendor = penerimaan.TBPenerimaanPOProduksiProdukDetails.Sum(item => item.TotalHargaVendor * item.Diterima);
                    penerimaan.Grandtotal = penerimaan.SubtotalTotalHPP + penerimaan.SubtotalTotalHargaVendor;
                    penerimaan.EnumStatusPenerimaan = (int)PilihanEnumStatusPenerimaanPO.Terima;
                    penerimaan.Keterangan = TextBoxKeterangan.Text;

                    foreach (var item in penerimaan.TBPenerimaanPOProduksiProdukDetails)
                    {
                        TBPOProduksiProdukDetail poProduksiProdukDetail = poProduksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);
                        poProduksiProdukDetail.Sisa = item.Sisa;

                        TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(data => data.IDTempat == pengguna.IDTempat && data.IDKombinasiProduk == item.IDKombinasiProduk);

                        StokProduk_Class.PengaturanJumlahStokPenerimaanPOProduk(penerimaan.TanggalTerima.Value, pengguna.IDPengguna, pengguna.IDTempat, stokProduk, (item.TotalHPP + item.TotalHargaVendor), item.Datang, item.TolakKeVendor, item.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDPOProduksiProduk + " - #" + item.TBPenerimaanPOProduksiProduk.IDPenerimaanPOProduksiProduk);
                    }

                    poProduksiProduk.TBPenerimaanPOProduksiProduks.Add(penerimaan);

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
                    penerimaan = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item => item.IDPenerimaanPOProduksiProduk == IDPenerimaanPOProduksiProduk);
                    if (penerimaan != null)
                    {
                        db.TBPenerimaanPOProduksiProdukDetails.DeleteAllOnSubmit(penerimaan.TBPenerimaanPOProduksiProdukDetails);
                        db.TBPenerimaanPOProduksiProduks.DeleteOnSubmit(penerimaan);
                        db.SubmitChanges();

                        IDPenerimaanPOProduksiProduk = string.Empty;
                    }
                }
            }
            LogError_Class LogError = new LogError_Class(ex, "Penerimaan Produksi Ke Supplier (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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