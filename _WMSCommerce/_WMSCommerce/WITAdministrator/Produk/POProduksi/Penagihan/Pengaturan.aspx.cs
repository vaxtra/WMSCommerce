using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Penagihan_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TextBoxPegawai.Text = pengguna.NamaLengkap;
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");

                DropDownListVendor.DataSource = db.TBVendors.OrderBy(item => item.Nama).ToArray();
                DropDownListVendor.DataTextField = "Nama";
                DropDownListVendor.DataValueField = "IDVendor";
                DropDownListVendor.DataBind();
                DropDownListVendor.Items.Insert(0, new ListItem { Text = "-Pilih Supplier-", Value = "0" });
            }
        }
    }

    protected void CustomValidatorVendor_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownListVendor.SelectedValue == "0")
        {
            args.IsValid = false;
            CustomValidatorVendor.Text = "Supplier harus dipilih";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorVendor.Text = string.Empty;
        }
    }

    protected void DropDownListVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Penerimaan = db.TBPenerimaanPOProduksiProduks.Where(item => item.TBPOProduksiProduk.IDVendor == DropDownListVendor.SelectedValue.ToInt() && item.IDPOProduksiProdukPenagihan == null).Select(item => new
            {
                item.IDPenerimaanPOProduksiProduk,
                item.TanggalTerima,
                item.Grandtotal
            });
            RepeaterDetailPenerimaan.DataSource = Penerimaan;
            RepeaterDetailPenerimaan.DataBind();
            LabelTotalPenerimaan.Text = "0";

            var Retur = db.TBPOProduksiProdukReturs.Where(item => item.TBVendor.IDVendor == DropDownListVendor.SelectedValue.ToInt() && item.IDPOProduksiProdukPenagihan == null).Select(item => new
            {
                item.IDPOProduksiProdukRetur,
                item.TanggalRetur,
                item.Grandtotal
            });
            RepeaterRetur.DataSource = Retur;
            RepeaterRetur.DataBind();
            LabelTotalRetur.Text = "0";

            RepeaterDownPayment.DataSource = null;
            RepeaterDownPayment.DataBind();
            LabelTotalDownPayment.Text = "0";

            TextBoxTotalPenagihan.Text = "0";
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            peringatan.Visible = false;
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            string IDPOProduksiProdukPenagihan = string.Empty;
            TBPOProduksiProdukPenagihan produksiProdukPenagihan = null;
            bool statusBerhasil = false;

            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    db.Proc_InsertPOProduksiProdukPenagihan(ref IDPOProduksiProdukPenagihan, DropDownListVendor.SelectedValue.ToInt(), pengguna.IDTempat, pengguna.IDPengguna, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                    produksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == IDPOProduksiProdukPenagihan);

                    foreach (RepeaterItem item in RepeaterDetailPenerimaan.Items)
                    {
                        CheckBox CheckBoxPilihPenerimaan = (CheckBox)item.FindControl("CheckBoxPilihPenerimaan");
                        Label LabelIDPenerimaanPOProduksiProduk = (Label)item.FindControl("LabelIDPenerimaanPOProduksiProduk");

                        if (CheckBoxPilihPenerimaan.Checked == true)
                        {
                            TBPenerimaanPOProduksiProduk penerimaanPOProduksiProduk = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item2 => item2.IDPenerimaanPOProduksiProduk == LabelIDPenerimaanPOProduksiProduk.Text);
                            penerimaanPOProduksiProduk.TBPOProduksiProdukPenagihan = produksiProdukPenagihan;
                        }
                    }

                    foreach (RepeaterItem item in RepeaterRetur.Items)
                    {
                        CheckBox CheckBoxPilihRetur = (CheckBox)item.FindControl("CheckBoxPilihRetur");
                        Label LabelIDPOProduksiProdukRetur = (Label)item.FindControl("LabelIDPOProduksiProdukRetur");

                        if (CheckBoxPilihRetur.Checked == true)
                        {
                            TBPOProduksiProdukRetur POProduksiProdukRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item2 => item2.IDPOProduksiProdukRetur == LabelIDPOProduksiProdukRetur.Text);
                            POProduksiProdukRetur.TBPOProduksiProdukPenagihan = produksiProdukPenagihan;
                            POProduksiProdukRetur.EnumStatusRetur = (int)EnumStatusPORetur.Proses;
                        }
                    }

                    produksiProdukPenagihan.TotalPenerimaan = LabelTotalPenerimaan.Text.ToDecimal();
                    produksiProdukPenagihan.TotalRetur = LabelTotalRetur.Text.ToDecimal();
                    produksiProdukPenagihan.TotalDownPayment = LabelTotalDownPayment.Text.ToDecimal();
                    produksiProdukPenagihan.TotalBayar = 0;
                    produksiProdukPenagihan.StatusPembayaran = false;
                    produksiProdukPenagihan.Keterangan = TextBoxKeterangan.Text;

                    if (TextBoxTotalPenagihan.Text.ToDecimal() > 0)
                    {
                        db.SubmitChanges();
                        statusBerhasil = true;
                    }
                    else
                    {
                        db.TBPOProduksiProdukPenagihans.DeleteOnSubmit(produksiProdukPenagihan);

                        LabelPeringatan.Text = "Total tagihan dibawah 0";
                        peringatan.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (statusBerhasil != true)
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        produksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == IDPOProduksiProdukPenagihan);
                        if (produksiProdukPenagihan != null)
                        {
                            produksiProdukPenagihan.TBPenerimaanPOProduksiProduks.ToList().ForEach(item => item.IDPOProduksiProdukPenagihan = null);
                            db.TBPOProduksiProdukPenagihans.DeleteOnSubmit(produksiProdukPenagihan);
                            db.SubmitChanges();

                            IDPOProduksiProdukPenagihan = string.Empty;
                        }
                    }
                }
                LogError_Class LogError = new LogError_Class(ex, "Invoice Purchase Order Produk (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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

    protected void CheckBoxPilihPenerimaan_CheckedChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<TBPOProduksiProduk> Produksi = new List<TBPOProduksiProduk>();
            decimal TotalPenerimaan = 0;

            foreach (RepeaterItem item in RepeaterDetailPenerimaan.Items)
            {
                CheckBox CheckBoxPilihPenerimaan = (CheckBox)item.FindControl("CheckBoxPilihPenerimaan");
                Label LabelIDPenerimaanPOProduksiProduk = (Label)item.FindControl("LabelIDPenerimaanPOProduksiProduk");

                if (CheckBoxPilihPenerimaan.Checked == true)
                {
                    TBPenerimaanPOProduksiProduk penerimaanPOProduksiProduk = db.TBPenerimaanPOProduksiProduks.FirstOrDefault(item2 => item2.IDPenerimaanPOProduksiProduk == LabelIDPenerimaanPOProduksiProduk.Text);

                    if (Produksi.FirstOrDefault(item2 => item2.IDPOProduksiProduk == penerimaanPOProduksiProduk.IDPOProduksiProduk) == null)
                    {
                        Produksi.Add(penerimaanPOProduksiProduk.TBPOProduksiProduk);
                    }

                    TotalPenerimaan += penerimaanPOProduksiProduk.Grandtotal.Value;
                }
            }
            LabelTotalPenerimaan.Text = TotalPenerimaan.ToFormatHarga();

            var DownPayment = Produksi.Select(item => new
            {
                item.IDPOProduksiProduk,
                item.TanggalDownPayment,
                item.DownPayment
            }).Distinct();
            RepeaterDownPayment.DataSource = DownPayment;
            RepeaterDownPayment.DataBind();
            LabelTotalDownPayment.Text = DownPayment.Sum(item => item.DownPayment).ToFormatHarga();
            TextBoxTotalPenagihan.Text = (LabelTotalPenerimaan.Text.ToDecimal() - LabelTotalRetur.Text.ToDecimal() - LabelTotalDownPayment.Text.ToDecimal()).ToFormatHarga();
        }
    }

    protected void CheckBoxPilihRetur_CheckedChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            decimal TotalRetur = 0;

            foreach (RepeaterItem item in RepeaterRetur.Items)
            {
                CheckBox CheckBoxPilihRetur = (CheckBox)item.FindControl("CheckBoxPilihRetur");
                Label LabelIDPOProduksiProdukRetur = (Label)item.FindControl("LabelIDPOProduksiProdukRetur");

                if (CheckBoxPilihRetur.Checked == true)
                {
                    TBPOProduksiProdukRetur POProduksiProdukRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item2 => item2.IDPOProduksiProdukRetur == LabelIDPOProduksiProdukRetur.Text);
                    TotalRetur += POProduksiProdukRetur.Grandtotal.Value;
                }
            }
            LabelTotalRetur.Text = TotalRetur.ToFormatHarga();
            TextBoxTotalPenagihan.Text = (LabelTotalPenerimaan.Text.ToDecimal() - LabelTotalRetur.Text.ToDecimal() - LabelTotalDownPayment.Text.ToDecimal()).ToFormatHarga();
        }
    }
}