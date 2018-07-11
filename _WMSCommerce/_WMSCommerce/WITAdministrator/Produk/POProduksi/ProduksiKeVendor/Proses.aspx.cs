using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_ProduksiKeVendor_Proses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["id"]);

                TextBoxIDProyeksi.Text = poProduksiProduk.IDProyeksi != null ? poProduksiProduk.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiProduk.Text = poProduksiProduk.IDPOProduksiProduk;
                TextBoxPegawaiProses.Text = pengguna.NamaLengkap;
                TextBoxTanggalProses.Text = DateTime.Now.ToString("d MMMM yyyy");

                TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
                var komposisi = poProduksiProduk.TBPOProduksiProdukKomposisis.Where(item => item.Sisa > 0).Select(item => new
                {
                    item.IDBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    SatuanPO = item.TBSatuan.Nama,
                    Sisa = item.Sisa.ToFormatHarga(),
                    Stok = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku).Jumlah.Value.ToFormatHarga(),
                    SatuanStok = item.TBBahanBaku.TBSatuan.Nama
                }).OrderBy(item => item.BahanBaku);
                RepeaterKomposisi.DataSource = komposisi;  //poProduksiProduk.TBPOProduksiProdukKomposisis.OrderBy(item => item.TBBahanBaku.Nama).ToArray();
                RepeaterKomposisi.DataBind();

                if (komposisi.Count() == 0)
                {
                    ButtonSimpan.Visible = false;
                }
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        peringatan.Visible = false;
        bool statusBerhasil = false;
        string IDPengirimanPOProduksiProduk = string.Empty;
        TBPengirimanPOProduksiProduk pengiriman = null;
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                bool pengirimanSesuai = true;
                int baris = 0;

                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["id"]);

                EntitySet<TBPengirimanPOProduksiProdukDetail> daftarKomposisi = new EntitySet<TBPengirimanPOProduksiProdukDetail>();

                foreach (RepeaterItem item in RepeaterKomposisi.Items)
                {
                    baris++;

                    Label LabelIDBahanBaku = (Label)item.FindControl("LabelIDBahanBaku");
                    TextBox TextBoxSisa = (TextBox)item.FindControl("TextBoxSisa");
                    TextBox TextBoxStok = (TextBox)item.FindControl("TextBoxStok");
                    TextBox TextBoxKirim = (TextBox)item.FindControl("TextBoxKirim");

                    if (TextBoxKirim.Text.ToDecimal() > 0)
                    {
                        if (TextBoxKirim.Text.ToDecimal() <= TextBoxStok.Text.ToDecimal())
                        {
                            if (TextBoxKirim.Text.ToDecimal() <= TextBoxSisa.Text.ToDecimal())
                            {
                                TBPOProduksiProdukKomposisi poProduksiKomposisiKomposisi = poProduksiProduk.TBPOProduksiProdukKomposisis.FirstOrDefault(data => data.IDBahanBaku == LabelIDBahanBaku.Text.ToInt());

                                daftarKomposisi.Add(new TBPengirimanPOProduksiProdukDetail()
                                {
                                    TBBahanBaku = poProduksiKomposisiKomposisi.TBBahanBaku,
                                    TBSatuan = poProduksiKomposisiKomposisi.TBSatuan,
                                    Kirim = TextBoxKirim.Text.ToDecimal()
                                });
                            }
                            else
                            {
                                LabelPeringatan.Text = "Jumlah kirim lebih besar dari jumlah kebutuhan, baris ke-" + baris.ToString();
                                peringatan.Visible = true;
                                pengirimanSesuai = false;
                                break;
                            }
                        }
                        else
                        {
                            LabelPeringatan.Text = "Jumlah kirim lebih besar dari stok, baris ke-" + baris.ToString();
                            peringatan.Visible = true;
                            pengirimanSesuai = false;
                            break;
                        }
                    }
                }

                if (pengirimanSesuai == true)
                {
                    if (daftarKomposisi.Count > 0)
                    {
                        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

                        db.Proc_InsertPengirimanPOProduksiProduk(ref IDPengirimanPOProduksiProduk, poProduksiProduk.IDPOProduksiProduk, poProduksiProduk.IDVendor, pengguna.IDTempat, pengguna.IDPengguna, TextBoxTanggalProses.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                        pengiriman = db.TBPengirimanPOProduksiProduks.FirstOrDefault(item => item.IDPengirimanPOProduksiProduk == IDPengirimanPOProduksiProduk);

                        foreach (var item in daftarKomposisi)
                        {
                            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, pengguna.IDPengguna, stokBahanBaku, item.Kirim, stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.PenguranganProduksi, "Production to Vendor #" + poProduksiProduk.IDPOProduksiProduk);

                            TBPOProduksiProdukKomposisi poProduksiProdukomposisi = poProduksiProduk.TBPOProduksiProdukKomposisis.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                            item.HargaBeli = poProduksiProdukomposisi.HargaBeli;
                            poProduksiProdukomposisi.Kirim = poProduksiProdukomposisi.Kirim + item.Kirim;
                            poProduksiProdukomposisi.Sisa = poProduksiProdukomposisi.Sisa - item.Kirim;
                        }
                        pengiriman.TBPengirimanPOProduksiProdukDetails = daftarKomposisi;
                        pengiriman.Grandtotal = pengiriman.TBPengirimanPOProduksiProdukDetails.Sum(item => (item.Kirim * item.HargaBeli));
                        pengiriman.Keterangan = null;

                        db.SubmitChanges();

                        statusBerhasil = true;
                    }
                    else
                    {
                        LabelPeringatan.Text = "Tidak ada bahan baku yang dikirim";
                        peringatan.Visible = true;
                        pengirimanSesuai = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (statusBerhasil != true)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    pengiriman = db.TBPengirimanPOProduksiProduks.FirstOrDefault(item => item.IDPengirimanPOProduksiProduk == IDPengirimanPOProduksiProduk);
                    if (pengiriman != null)
                    {
                        db.TBPengirimanPOProduksiProdukDetails.DeleteAllOnSubmit(pengiriman.TBPengirimanPOProduksiProdukDetails);
                        db.TBPengirimanPOProduksiProduks.DeleteOnSubmit(pengiriman);
                        db.SubmitChanges();

                        IDPengirimanPOProduksiProduk = string.Empty;
                    }
                }
            }
            LogError_Class LogError = new LogError_Class(ex, "Penerimaan Produksi Ke Vendor (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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