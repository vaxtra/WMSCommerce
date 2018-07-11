using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Retur_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");

                DropDownListSupplier.DataSource = db.TBSuppliers.OrderBy(item => item.Nama).ToArray();
                DropDownListSupplier.DataTextField = "Nama";
                DropDownListSupplier.DataValueField = "IDSupplier";
                DropDownListSupplier.DataBind();
                DropDownListSupplier.Items.Insert(0, new ListItem { Text = "-Pilih Supplier-", Value = "0" });

                DropDownListPenerimaan.Items.Insert(0, new ListItem { Text = "-Tanpa Penerimaan-", Value = "-" });

                TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
                DropDownListStokBahanBaku.DataSource = daftarStokBahanBaku.Select(item => new { item.IDStokBahanBaku, item.TBBahanBaku.Nama }).OrderBy(item => item.Nama).ToArray();
                DropDownListStokBahanBaku.DataTextField = "Nama";
                DropDownListStokBahanBaku.DataValueField = "IDStokBahanBaku";
                DropDownListStokBahanBaku.DataBind();

                if (DropDownListStokBahanBaku.Items.Count == 0)
                {
                    ButtonSimpanDetail.Enabled = false;
                    ButtonSimpan.Enabled = false;
                }
                else
                {
                    TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());
                    TextBoxHarga.Text = (stokBahanBaku.HargaBeli.Value * stokBahanBaku.TBBahanBaku.Konversi.Value).ToFormatHarga();
                    LabelSatuan.Text = "/" + stokBahanBaku.TBBahanBaku.TBSatuan1.Nama;

                }
                ViewState["ViewStateListDetail"] = new List<StokBahanBaku_Model>();
            }
        }
    }

    protected void DropDownListSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListSupplier.SelectedValue != "0")
            {
                DropDownListPenerimaan.DataSource = db.TBPenerimaanPOProduksiBahanBakus.Where(item => item.TBPOProduksiBahanBaku.IDSupplier == DropDownListSupplier.SelectedValue.ToInt() && item.IDPOProduksiBahanBakuPenagihan == null).Select(item => new { item.IDPenerimaanPOProduksiBahanBaku });
                DropDownListPenerimaan.DataTextField = "IDPenerimaanPOProduksiBahanBaku";
                DropDownListPenerimaan.DataValueField = "IDPenerimaanPOProduksiBahanBaku";
                DropDownListPenerimaan.DataBind();
                DropDownListPenerimaan.Items.Insert(0, new ListItem { Text = "-Tanpa Penerimaan-", Value = "0" });
                DropDownListPenerimaan.Enabled = true;
            }
            else
            {
                DropDownListPenerimaan.Items.Clear();
                DropDownListPenerimaan.Items.Insert(0, new ListItem { Text = "-Tanpa Penerimaan-", Value = "0" });
                DropDownListPenerimaan.Enabled = false;
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
    private void CekHargaStok()
    {
        TextBoxHarga.Text = "0.00";
        TextBoxJumlah.Text = "1.00";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());
            TextBoxHarga.Text = (stokBahanBaku.HargaBeli * stokBahanBaku.TBBahanBaku.Konversi).ToFormatHarga();
            LabelSatuan.Text = "/" + stokBahanBaku.TBBahanBaku.TBSatuan1.Nama;
        }
    }
    protected void DropDownListStokBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        CekHargaStok();
    }
    protected void ButtonSimpanDetail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (TextBoxJumlah.Text.ToDecimal() > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    List<StokBahanBaku_Model> ViewStateListDetail = (List<StokBahanBaku_Model>)ViewState["ViewStateListDetail"];

                    StokBahanBaku_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

                        detail = new StokBahanBaku_Model()
                        {
                            IDStokBahanBaku = DropDownListStokBahanBaku.SelectedValue.ToInt(),
                            IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuanKonversi,
                            BahanBaku = stokBahanBaku.TBBahanBaku.Nama,
                            HargaBeli = stokBahanBaku.HargaBeli.Value * stokBahanBaku.TBBahanBaku.Konversi.Value,
                            HargaSupplier = TextBoxHarga.Text.ToDecimal(),
                            Jumlah = TextBoxJumlah.Text.ToDecimal()
                        };

                        ViewStateListDetail.Add(detail);
                    }
                    else
                    {
                        detail.HargaBeli = TextBoxHarga.Text.ToDecimal();
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal();
                    }

                    ViewState["ViewStateListDetail"] = ViewStateListDetail;

                    LoadData();
                }
            }
        }
    }

    private void LoadData()
    {
        List<StokBahanBaku_Model> ViewStateListDetail = (List<StokBahanBaku_Model>)ViewState["ViewStateListDetail"];

        RepeaterDetail.DataSource = ViewStateListDetail;
        RepeaterDetail.DataBind();

        if (ViewStateListDetail.Count == 0)
        {
            LabelTotal.Text = "0";
        }
        else
        {
            LabelTotal.Text = ViewStateListDetail.Sum(item => item.SubtotalHargaSupplier).ToFormatHarga();
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;

    }
    protected void RepeaterDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<StokBahanBaku_Model> ViewStateListDetail = (List<StokBahanBaku_Model>)ViewState["ViewStateListDetail"];

        ViewStateListDetail.Remove(ViewStateListDetail.FirstOrDefault(item => item.IDStokBahanBaku == e.CommandArgument.ToInt()));
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        LoadData();
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //DateTime tanggalretur = DateTime.Parse(TextBoxTanggalRetur.Text + " " + DateTime.Now.ToString("HH:mm:ss tt"));
            peringatan.Visible = false;
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            List<StokBahanBaku_Model> ViewStateListDetail = (List<StokBahanBaku_Model>)ViewState["ViewStateListDetail"];

            if (ViewStateListDetail.Count > 0)
            {
                string IDPOProduksiBahanBakuRetur = string.Empty;
                TBPOProduksiBahanBakuRetur POBahanBakuRetur = null;
                bool statusBerhasil = false;

                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        db.Proc_InsertPOProduksiBahanBakuRetur(ref IDPOProduksiBahanBakuRetur, pengguna.IDTempat, DropDownListSupplier.SelectedValue.ToInt(), pengguna.IDPengguna, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));
                        POBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item => item.IDPOProduksiBahanBakuRetur == IDPOProduksiBahanBakuRetur);

                        POBahanBakuRetur.IDPenerimaanPOProduksiBahanBaku = DropDownListPenerimaan.SelectedValue != "0" ? DropDownListPenerimaan.SelectedValue : null;
                        POBahanBakuRetur.TBPOProduksiBahanBakuReturDetails.AddRange(ViewStateListDetail.OrderBy(item => item.BahanBaku).Select(item => new TBPOProduksiBahanBakuReturDetail
                        {
                            TBStokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item2 => item2.IDStokBahanBaku == item.IDStokBahanBaku),
                            IDSatuan = item.IDSatuan,
                            HargaBeli = item.HargaBeli,
                            HargaRetur = item.HargaSupplier,
                            Jumlah = item.Jumlah
                        }));
                        POBahanBakuRetur.Grandtotal = POBahanBakuRetur.TBPOProduksiBahanBakuReturDetails.Sum(item => item.Jumlah * item.HargaRetur);
                        POBahanBakuRetur.EnumStatusRetur = (int)EnumStatusPORetur.Baru;
                        POBahanBakuRetur.Keterangan = TextBoxKeterangan.Text;

                        foreach (var item in POBahanBakuRetur.TBPOProduksiBahanBakuReturDetails)
                        {
                            StokBahanBaku_Class.UpdateBertambahBerkurang(
                            db: db,
                            tanggal: DateTime.Now,
                            idPengguna: pengguna.IDPengguna,
                            stokBahanBaku: item.TBStokBahanBaku,
                            jumlahStok: item.Jumlah.Value,
                            hargaBeli: item.HargaBeli.Value,
                            satuanBesar: true,
                            enumJenisPerpindahanStok: EnumJenisPerpindahanStok.ReturKeTempatProduksi,
                            keterangan: "(" + item.TBStokBahanBaku.TBBahanBaku.Nama + ") Retur PO #" + IDPOProduksiBahanBakuRetur);
                        }

                        db.SubmitChanges();

                        statusBerhasil = true;
                    }
                }
                catch (Exception ex)
                {
                    if (statusBerhasil != true)
                    {
                        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                        {
                            POBahanBakuRetur = db.TBPOProduksiBahanBakuReturs.FirstOrDefault(item => item.IDPOProduksiBahanBakuRetur == IDPOProduksiBahanBakuRetur);
                            if (POBahanBakuRetur != null)
                            {
                                db.TBPOProduksiBahanBakuReturDetails.DeleteAllOnSubmit(POBahanBakuRetur.TBPOProduksiBahanBakuReturDetails);
                                db.TBPOProduksiBahanBakuReturs.DeleteOnSubmit(POBahanBakuRetur);
                                db.SubmitChanges();

                                IDPOProduksiBahanBakuRetur = string.Empty;
                            }
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Retur PO Bahan Baku (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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
            else
            {
                LabelPeringatan.Text = "Tidak ada Bahan Baku yang dipilih";
                peringatan.Visible = true;
            }
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}