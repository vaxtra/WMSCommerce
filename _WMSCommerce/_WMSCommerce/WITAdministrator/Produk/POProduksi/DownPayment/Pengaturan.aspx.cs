using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_DownPayment_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DropDownListCariVendor.DataSource = db.TBVendors.OrderBy(item => item.Nama);
                DropDownListCariVendor.DataTextField = "Nama";
                DropDownListCariVendor.DataValueField = "IDVendor";
                DropDownListCariVendor.DataBind();
                DropDownListCariVendor.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

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

    protected void DropDownListCariVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterDetail.DataSource = null;
            RepeaterDetail.DataBind();

            if (DropDownListCariVendor.SelectedValue != "0")
            {
                DropDownListIDPOProduksi.Enabled = true;
                DropDownListIDPOProduksi.DataSource = db.TBPOProduksiProduks.Where(item => item.IDVendor == DropDownListCariVendor.SelectedValue.ToInt() && item.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri && item.TanggalDownPayment == null).OrderByDescending(item => item.Nomor);
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
            ButtonSimpan.Enabled = false;

            if (DropDownListIDPOProduksi.SelectedValue != "0")
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == DropDownListIDPOProduksi.SelectedValue);

                TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiProduk.EnumJenisHPP.Value);
                TextBoxPegawaiPIC.Text = poProduksiProduk.TBPengguna1.NamaLengkap;
                TextBoxPembuat.Text = poProduksiProduk.TBPengguna.NamaLengkap + " / " + poProduksiProduk.Tanggal.ToFormatTanggal();
                TextBoxTanggalJatuhTempo.Text = poProduksiProduk.TanggalJatuhTempo.ToFormatTanggal();
                TextBoxTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman.ToFormatTanggal();

                RepeaterDetail.DataSource = poProduksiProduk.TBPOProduksiProdukDetails.ToArray();
                RepeaterDetail.DataBind();
                LabelTotalJumlah.Text = poProduksiProduk.TotalJumlah.ToFormatHargaBulat();
                LabelTotalSubtotal.Text = poProduksiProduk.SubtotalTotalHargaVendor.ToFormatHarga();
                LabelTotalSisa.Text = poProduksiProduk.TBPOProduksiProdukDetails.Sum(data => data.Sisa).ToFormatHargaBulat();

                TextBoxKeterangan.Text = poProduksiProduk.Keterangan;
                TextBoxBiayaLainLain.Text = poProduksiProduk.BiayaLainLain.ToFormatHarga();
                TextBoxPotonganPO.Text = poProduksiProduk.PotonganPOProduksiProduk.ToFormatHarga();
                LabelTax.Text = "Tax (" + (poProduksiProduk.PersentaseTax * 100).ToFormatHarga() + "%)";
                TextBoxTax.Text = poProduksiProduk.Tax.ToFormatHarga();
                TextBoxGrandtotal.Text = poProduksiProduk.Grandtotal.ToFormatHarga();

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
                            TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == DropDownListIDPOProduksi.SelectedValue);
                            poProduksiProduk.IDPenggunaDP = pengguna.IDPengguna;
                            poProduksiProduk.IDJenisPembayaran = DropDownListJenisPembayaran.SelectedValue.ToInt();
                            poProduksiProduk.TanggalDownPayment = TextBoxTanggal.Text.ToDateTime();
                            poProduksiProduk.DownPayment = TextBoxDownPayment.Text.ToDecimal();

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
                LogError_Class LogError = new LogError_Class(ex, "Down Payment Purchase Order Bahan Baku (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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