using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Penagihan_Pengaturan : System.Web.UI.Page
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

                DropDownListSupplier.DataSource = db.TBSuppliers.OrderBy(item => item.Nama).ToArray();
                DropDownListSupplier.DataTextField = "Nama";
                DropDownListSupplier.DataValueField = "IDSupplier";
                DropDownListSupplier.DataBind();
                DropDownListSupplier.Items.Insert(0, new ListItem { Text = "-Pilih Supplier-", Value = "0" });
            }
        }
    }

    protected void CustomValidatorSupplier_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownListSupplier.SelectedValue == "0")
        {
            args.IsValid = false;
            CustomValidatorSupplier.Text = "Supplier harus dipilih";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorSupplier.Text = string.Empty;
        }
    }

    protected void DropDownListSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            ButtonSimpan.Enabled = true;

            if (DropDownListSupplier.SelectedValue != "0")
            {
                RepeaterDetailPenerimaan.DataSource = db.TBPenerimaanPOProduksiBahanBakus.Where(item => item.TBPOProduksiBahanBaku.IDSupplier == DropDownListSupplier.SelectedValue.ToInt() && item.IDPOProduksiBahanBakuPenagihan == null).Select(item => new
                {
                    item.IDPenerimaanPOProduksiBahanBaku,
                    item.TanggalTerima,
                    item.Grandtotal
                });
                RepeaterDetailPenerimaan.DataBind();
                LabelTotalPenerimaan.Text = "0";

                RepeaterRetur.DataSource = db.TBPOProduksiBahanBakuReturs.Where(item => item.TBSupplier.IDSupplier == DropDownListSupplier.SelectedValue.ToInt() && item.IDPOProduksiBahanBakuPenagihan == null).Select(item => new
                {
                    item.IDPOProduksiBahanBakuRetur,
                    item.TanggalRetur,
                    item.Grandtotal
                });
                RepeaterRetur.DataBind();
                LabelTotalRetur.Text = "0";

                RepeaterDownPayment.DataSource = null;
                RepeaterDownPayment.DataBind();
                LabelTotalDownPayment.Text = "0";

                TextBoxTotalPenagihan.Text = "0";

                if (RepeaterDetailPenerimaan.Items.Count == 0)
                {
                    ButtonSimpan.Enabled = false;
                }
            }
            else
            {
                ButtonSimpan.Enabled = false;
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            peringatan.Visible = false;
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            string IDPOProduksiBahanBakuPenagihan = string.Empty;
            TBPOProduksiBahanBakuPenagihan produksiBahanBakuPenagihan = null;
            bool statusBerhasil = false;

            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    db.Proc_InsertPOProduksiBahanBakuPenagihan(ref IDPOProduksiBahanBakuPenagihan, DropDownListSupplier.SelectedValue.ToInt(), pengguna.IDTempat, pengguna.IDPengguna, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                    produksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.FirstOrDefault(item => item.IDPOProduksiBahanBakuPenagihan == IDPOProduksiBahanBakuPenagihan);

                    foreach (RepeaterItem item in RepeaterDetailPenerimaan.Items)
                    {
                        CheckBox CheckBoxPilihPenerimaan = (CheckBox)item.FindControl("CheckBoxPilihPenerimaan");
                        Label LabelIDPenerimaanPOProduksiBahanBaku = (Label)item.FindControl("LabelIDPenerimaanPOProduksiBahanBaku");

                        if (CheckBoxPilihPenerimaan.Checked == true)
                        {
                            TBPenerimaanPOProduksiBahanBaku penerimaanPOProduksiBahanBaku = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item2 => item2.IDPenerimaanPOProduksiBahanBaku == LabelIDPenerimaanPOProduksiBahanBaku.Text);
                            penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBakuPenagihan = produksiBahanBakuPenagihan;
                        }
                    }

                    foreach (RepeaterItem item in RepeaterRetur.Items)
                    {
                        CheckBox CheckBoxPilihRetur = (CheckBox)item.FindControl("CheckBoxPilihRetur");
                        Label LabelIDPOProduksiBahanBakuRetur = (Label)item.FindControl("LabelIDPOProduksiBahanBakuRetur");

                        if (CheckBoxPilihRetur.Checked == true)
                        {
                            TBPOProduksiBahanBakuRetur POProduksiBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item2 => item2.IDPOProduksiBahanBakuRetur == LabelIDPOProduksiBahanBakuRetur.Text);
                            POProduksiBahanBakuRetur.TBPOProduksiBahanBakuPenagihan = produksiBahanBakuPenagihan;
                            POProduksiBahanBakuRetur.EnumStatusRetur = (int)EnumStatusPORetur.Proses;
                        }
                    }

                    foreach (RepeaterItem item in RepeaterDownPayment.Items)
                    {
                        Label LabelIDPOProduksiBahanBaku = (Label)item.FindControl("LabelIDPOProduksiBahanBaku");

                        TBPOProduksiBahanBaku POProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item2 => item2.IDPOProduksiBahanBaku == LabelIDPOProduksiBahanBaku.Text);
                        POProduksiBahanBaku.TBPOProduksiBahanBakuPenagihan = produksiBahanBakuPenagihan;
                    }

                    produksiBahanBakuPenagihan.TotalPenerimaan = LabelTotalPenerimaan.Text.ToDecimal();
                    produksiBahanBakuPenagihan.TotalRetur = LabelTotalRetur.Text.ToDecimal();
                    produksiBahanBakuPenagihan.TotalDownPayment = LabelTotalDownPayment.Text.ToDecimal();
                    produksiBahanBakuPenagihan.TotalBayar = 0;
                    produksiBahanBakuPenagihan.StatusPembayaran = false;
                    produksiBahanBakuPenagihan.Keterangan = TextBoxKeterangan.Text;

                    if (TextBoxTotalPenagihan.Text.ToDecimal() > 0)
                    {
                        db.SubmitChanges();
                        statusBerhasil = true;
                    }
                    else
                    {
                        db.TBPOProduksiBahanBakuPenagihans.DeleteOnSubmit(produksiBahanBakuPenagihan);

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
                        produksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.FirstOrDefault(item => item.IDPOProduksiBahanBakuPenagihan == IDPOProduksiBahanBakuPenagihan);
                        if (produksiBahanBakuPenagihan != null)
                        {
                            produksiBahanBakuPenagihan.TBPenerimaanPOProduksiBahanBakus.ToList().ForEach(item => item.IDPOProduksiBahanBakuPenagihan = null);
                            db.TBPOProduksiBahanBakuPenagihans.DeleteOnSubmit(produksiBahanBakuPenagihan);
                            db.SubmitChanges();

                            IDPOProduksiBahanBakuPenagihan = string.Empty;
                        }
                    }
                }
                LogError_Class LogError = new LogError_Class(ex, "Invoice Purchase Order Bahan Baku (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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
            List<TBPOProduksiBahanBaku> Produksi = new List<TBPOProduksiBahanBaku>();
            decimal TotalPenerimaan = 0;

            foreach (RepeaterItem item in RepeaterDetailPenerimaan.Items)
            {
                CheckBox CheckBoxPilihPenerimaan = (CheckBox)item.FindControl("CheckBoxPilihPenerimaan");
                Label LabelIDPenerimaanPOProduksiBahanBaku = (Label)item.FindControl("LabelIDPenerimaanPOProduksiBahanBaku");

                if (CheckBoxPilihPenerimaan.Checked == true)
                {
                    TBPenerimaanPOProduksiBahanBaku penerimaanPOProduksiBahanBaku = db.TBPenerimaanPOProduksiBahanBakus.FirstOrDefault(item2 => item2.IDPenerimaanPOProduksiBahanBaku == LabelIDPenerimaanPOProduksiBahanBaku.Text);

                    if (penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDPOProduksiBahanBakuPenagihan == null)
                    {
                        if (Produksi.FirstOrDefault(item2 => item2.IDPOProduksiBahanBaku == penerimaanPOProduksiBahanBaku.IDPOProduksiBahanBaku) == null)
                        {
                            Produksi.Add(penerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku);
                        }
                    }

                    TotalPenerimaan += penerimaanPOProduksiBahanBaku.Grandtotal.Value;
                }
            }
            LabelTotalPenerimaan.Text = TotalPenerimaan.ToFormatHarga();

            var DownPayment = Produksi.Select(item => new
            {
                item.IDPOProduksiBahanBaku,
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
                Label LabelIDPOProduksiBahanBakuRetur = (Label)item.FindControl("LabelIDPOProduksiBahanBakuRetur");

                if (CheckBoxPilihRetur.Checked == true)
                {
                    TBPOProduksiBahanBakuRetur POProduksiBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item2 => item2.IDPOProduksiBahanBakuRetur == LabelIDPOProduksiBahanBakuRetur.Text);
                    TotalRetur += POProduksiBahanBakuRetur.Grandtotal.Value;
                }
            }
            LabelTotalRetur.Text = TotalRetur.ToFormatHarga();
            TextBoxTotalPenagihan.Text = (LabelTotalPenerimaan.Text.ToDecimal() - LabelTotalRetur.Text.ToDecimal() - LabelTotalDownPayment.Text.ToDecimal()).ToFormatHarga();
        }
    }
}