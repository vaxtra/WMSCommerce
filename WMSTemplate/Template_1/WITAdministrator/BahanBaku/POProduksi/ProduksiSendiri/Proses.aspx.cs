using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_ProduksiSendiri_Proses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]);

                TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : "-Tidak Ada Proyeksi-";
                TextBoxIDPOProduksiBahanBaku.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;
                TextBoxPegawaiProses.Text = pengguna.NamaLengkap;
                TextBoxTanggalProses.Text = DateTime.Now.ToString("d MMMM yyyy");

                TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
                var komposisi = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.Where(item => item.Sisa > 0).Select(item => new
                {
                    item.IDBahanBaku,
                    BahanBaku = item.TBBahanBaku.Nama,
                    SatuanPO = item.TBSatuan.Nama,
                    Sisa = item.Sisa.ToFormatHarga(),
                    Stok = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku).Jumlah.Value.ToFormatHarga(),
                    SatuanStok = item.TBBahanBaku.TBSatuan.Nama
                }).OrderBy(item => item.BahanBaku);
                RepeaterKomposisi.DataSource = komposisi;  //poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.OrderBy(item => item.TBBahanBaku.Nama).ToArray();
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
        string IDPengirimanPOProduksiBahanBaku = string.Empty;
        TBPengirimanPOProduksiBahanBaku pengiriman = null;
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                bool pengirimanSesuai = true;
                int baris = 0;

                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["id"]);

                EntitySet<TBPengirimanPOProduksiBahanBakuDetail> daftarKomposisi = new EntitySet<TBPengirimanPOProduksiBahanBakuDetail>();

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
                                TBPOProduksiBahanBakuKomposisi poProduksiBahanBakuKomposisi = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.FirstOrDefault(data => data.IDBahanBaku == LabelIDBahanBaku.Text.ToInt());

                                daftarKomposisi.Add(new TBPengirimanPOProduksiBahanBakuDetail()
                                {
                                    TBBahanBaku = poProduksiBahanBakuKomposisi.TBBahanBaku,
                                    TBSatuan = poProduksiBahanBakuKomposisi.TBSatuan,
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

                        db.Proc_InsertPengirimanPOProduksiBahanBaku(ref IDPengirimanPOProduksiBahanBaku, poProduksiBahanBaku.IDPOProduksiBahanBaku, 0, pengguna.IDTempat, pengguna.IDPengguna, TextBoxTanggalProses.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                        pengiriman = db.TBPengirimanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPengirimanPOProduksiBahanBaku == IDPengirimanPOProduksiBahanBaku);

                        foreach (var item in daftarKomposisi)
                        {
                            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, pengiriman.Tanggal, pengguna.IDPengguna, stokBahanBaku, item.Kirim, stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.PenguranganProduksi, "In-House Production #" + poProduksiBahanBaku.IDPOProduksiBahanBaku);

                            TBPOProduksiBahanBakuKomposisi poProduksiBahanBakuKomposisi = poProduksiBahanBaku.TBPOProduksiBahanBakuKomposisis.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);
                            item.HargaBeli = poProduksiBahanBakuKomposisi.HargaBeli;
                            poProduksiBahanBakuKomposisi.Kirim = poProduksiBahanBakuKomposisi.Kirim + item.Kirim;
                            poProduksiBahanBakuKomposisi.Sisa = poProduksiBahanBakuKomposisi.Sisa - item.Kirim;
                        }
                        pengiriman.TBPengirimanPOProduksiBahanBakuDetails = daftarKomposisi;
                        pengiriman.Grandtotal = pengiriman.TBPengirimanPOProduksiBahanBakuDetails.Sum(item => (item.Kirim * item.HargaBeli));
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
                    pengiriman = db.TBPengirimanPOProduksiBahanBakus.FirstOrDefault(item => item.IDPengirimanPOProduksiBahanBaku == IDPengirimanPOProduksiBahanBaku);
                    if (pengiriman != null)
                    {
                        db.TBPengirimanPOProduksiBahanBakuDetails.DeleteAllOnSubmit(pengiriman.TBPengirimanPOProduksiBahanBakuDetails);
                        db.TBPengirimanPOProduksiBahanBakus.DeleteOnSubmit(pengiriman);
                        db.SubmitChanges();

                        IDPengirimanPOProduksiBahanBaku = string.Empty;
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