using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_DownPayment_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariSupplier.DataSource = db.TBSuppliers.OrderBy(item => item.Nama);
                DropDownListCariSupplier.DataTextField = "Nama";
                DropDownListCariSupplier.DataValueField = "IDSupplier";
                DropDownListCariSupplier.DataBind();
                DropDownListCariSupplier.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListIDPOProduksi.Items.Insert(0, new ListItem { Value = "0", Text = "-Pilih-" });
                DropDownListIDPOProduksi.Enabled = false;

                TextBoxPenggunaDP.Text = pengguna.NamaLengkap;
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");
                DropDownListJenisPembayaran.DataSource = db.TBJenisPembayarans.ToArray();
                DropDownListJenisPembayaran.DataTextField = "Nama";
                DropDownListJenisPembayaran.DataValueField = "IDJenisPembayaran";
                DropDownListJenisPembayaran.DataBind();
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
                DropDownListIDPOProduksi.DataSource = db.TBPOProduksiBahanBakus.Where(item => item.IDSupplier == DropDownListCariSupplier.SelectedValue.ToInt() && item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && item.TanggalDownPayment == null).OrderByDescending(item => item.Nomor);
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
            ButtonSimpan.Enabled = false;

            if (DropDownListIDPOProduksi.SelectedValue != "0")
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == DropDownListIDPOProduksi.SelectedValue);

                TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiBahanBaku.EnumJenisHPP.Value);
                TextBoxPegawaiPIC.Text = poProduksiBahanBaku.TBPengguna1.NamaLengkap;
                TextBoxPembuat.Text = poProduksiBahanBaku.TBPengguna.NamaLengkap + " / " + poProduksiBahanBaku.Tanggal.ToFormatTanggal();
                TextBoxTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman.ToFormatTanggal();

                RepeaterDetail.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.ToArray();
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = poProduksiBahanBaku.TotalJumlah.ToFormatHarga();
                LabelTotalSubtotal.Text = poProduksiBahanBaku.SubtotalTotalHargaSupplier.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(data => data.Sisa).ToFormatHarga();

                TextBoxKeterangan.Text = poProduksiBahanBaku.Keterangan;
                TextBoxBiayaLainLain.Text = poProduksiBahanBaku.BiayaLainLain.ToFormatHarga();
                TextBoxPotonganPO.Text = poProduksiBahanBaku.PotonganPOProduksiBahanBaku.ToFormatHarga();
                LabelTax.Text = "Tax (" + (poProduksiBahanBaku.PersentaseTax * 100).ToFormatHarga() + "%)";
                TextBoxTax.Text = poProduksiBahanBaku.Tax.ToFormatHarga();
                TextBoxGrandtotal.Text = poProduksiBahanBaku.Grandtotal.ToFormatHarga();

                ButtonSimpan.Enabled = true;
            }
            else
            {
                TextBoxStatusHPP.Text = string.Empty;
                TextBoxPegawaiPIC.Text = string.Empty;
                TextBoxPembuat.Text = string.Empty;
                TextBoxTanggalJatuhTempo.Text = string.Empty;
                TextBoxTanggalPengiriman.Text = string.Empty;

                RepeaterDetail.DataSource = null;
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = "0";
                LabelTotalSubtotal.Text = "0";
                LabelTotalSisa.Text = "0";

                TextBoxKeterangan.Text = string.Empty;
                TextBoxBiayaLainLain.Text = "0";
                TextBoxPotonganPO.Text = "0";
                LabelTax.Text = "Tax (0%)";
                TextBoxTax.Text = "0";
                TextBoxGrandtotal.Text = "0";
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        peringatan.Visible = false;
        bool statusBerhasil = false;
        if (Page.IsValid)
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            try
            {
                if (TextBoxDownPayment.Text.ToDecimal() > 0)
                {
                    if (TextBoxDownPayment.Text.ToDecimal() <= TextBoxGrandtotal.Text.ToDecimal())
                    {
                        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                        {
                            TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == DropDownListIDPOProduksi.SelectedValue);
                            poProduksiBahanBaku.IDPenggunaDP = pengguna.IDPengguna;
                            poProduksiBahanBaku.IDJenisPembayaran = DropDownListJenisPembayaran.SelectedValue.ToInt();
                            poProduksiBahanBaku.TanggalDownPayment = TextBoxTanggal.Text.ToDateTime();
                            poProduksiBahanBaku.DownPayment = TextBoxDownPayment.Text.ToDecimal();

                            db.SubmitChanges();

                            statusBerhasil = true;
                        }
                    }
                    else
                    {
                        LabelPeringatan.Text = "Down Payment harus lebih kecil dari grandtotal";
                        peringatan.Visible = true;
                    }
                }
                else
                {
                    LabelPeringatan.Text = "Down Payment harus lebih besar dari 0";
                    peringatan.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogError_Class LogError = new LogError_Class(ex, "Down Payment Purchase Order Produk (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}