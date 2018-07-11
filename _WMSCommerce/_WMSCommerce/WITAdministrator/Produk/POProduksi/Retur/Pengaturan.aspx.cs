using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Retur_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");

                DropDownListVendor.DataSource = db.TBVendors.OrderBy(item => item.Nama).ToArray();
                DropDownListVendor.DataTextField = "Nama";
                DropDownListVendor.DataValueField = "IDVendor";
                DropDownListVendor.DataBind();
                DropDownListVendor.Items.Insert(0, new ListItem { Text = "-Pilih Vendor-", Value = "0" });

                DropDownListPenerimaan.Items.Insert(0, new ListItem { Text = "-Tanpa Penerimaan-", Value = "-" });

                TBStokProduk[] daftarStokProduk = db.TBStokProduks.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
                DropDownListStokProduk.DataSource = daftarStokProduk.Select(item => new { item.IDStokProduk, item.TBKombinasiProduk.Nama }).OrderBy(item => item.Nama).ToArray();
                DropDownListStokProduk.DataTextField = "Nama";
                DropDownListStokProduk.DataValueField = "IDStokProduk";
                DropDownListStokProduk.DataBind();

                if (DropDownListStokProduk.Items.Count == 0)
                {
                    ButtonSimpanDetail.Enabled = false;
                    ButtonSimpan.Enabled = false;
                }
                else
                {
                    TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());
                    TextBoxHarga.Text = stokProduk.HargaBeli.Value.ToFormatHarga();

                }
                ViewState["ViewStateListDetail"] = new List<StokProduk_Model>();
            }
        }
    }

    protected void DropDownListVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListVendor.SelectedValue != "0")
            {
                DropDownListPenerimaan.DataSource = db.TBPenerimaanPOProduksiProduks.Where(item => item.TBPOProduksiProduk.IDVendor == DropDownListVendor.SelectedValue.ToInt() && item.IDPOProduksiProdukPenagihan == null).Select(item => new { item.IDPenerimaanPOProduksiProduk });
                DropDownListPenerimaan.DataTextField = "IDPenerimaanPOProduksiProduk";
                DropDownListPenerimaan.DataValueField = "IDPenerimaanPOProduksiProduk";
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
    protected void CustomValidatorVendor_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownListVendor.SelectedValue == "0")
        {
            args.IsValid = false;
            CustomValidatorVendor.Text = "Vendor harus dipilih";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorVendor.Text = string.Empty;
        }
    }
    private void CekHargaStok()
    {
        TextBoxHarga.Text = "0.00";
        TextBoxJumlah.Text = "1";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());
            TextBoxHarga.Text = stokProduk.HargaBeli.ToFormatHarga();
        }
    }
    protected void DropDownListStokProduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        CekHargaStok();
    }
    protected void ButtonSimpanDetail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (TextBoxJumlah.Text.ToDecimal().ToInt() > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

                    StokProduk_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                        detail = new StokProduk_Model()
                        {
                            IDStokProduk = DropDownListStokProduk.SelectedValue.ToInt(),
                            IDAtribut = stokProduk.TBKombinasiProduk.IDAtributProduk,
                            Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk,
                            Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama,
                            Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama,
                            HargaBeli = stokProduk.HargaBeli.Value,
                            HargaVendor = TextBoxHarga.Text.ToDecimal(),
                            HargaJual = stokProduk.HargaJual.Value,
                            Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt()
                        };

                        ViewStateListDetail.Add(detail);
                    }
                    else
                    {
                        detail.HargaBeli = TextBoxHarga.Text.ToDecimal();
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt();
                    }

                    ViewState["ViewStateListDetail"] = ViewStateListDetail;

                    LoadData();
                }
            }
        }
    }

    private void LoadData()
    {
        List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

        RepeaterDetail.DataSource = ViewStateListDetail;
        RepeaterDetail.DataBind();

        if (ViewStateListDetail.Count == 0)
        {
            LabelTotal.Text = "0";
        }
        else
        {
            LabelTotal.Text = ViewStateListDetail.Sum(item => item.SubtotalHargaBeli).ToFormatHarga();
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;

    }
    protected void RepeaterDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

        ViewStateListDetail.Remove(ViewStateListDetail.FirstOrDefault(item => item.IDStokProduk == e.CommandArgument.ToInt()));
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

            List<StokProduk_Model> ViewStateListDetail = (List<StokProduk_Model>)ViewState["ViewStateListDetail"];

            if (ViewStateListDetail.Count > 0)
            {
                string IDPOProduksiProdukRetur = string.Empty;
                TBPOProduksiProdukRetur POProdukRetur = null;
                bool statusBerhasil = false;

                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        db.Proc_InsertPOProduksiProdukRetur(ref IDPOProduksiProdukRetur, pengguna.IDTempat, DropDownListVendor.SelectedValue.ToInt(), pengguna.IDPengguna, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));
                        POProdukRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item => item.IDPOProduksiProdukRetur == IDPOProduksiProdukRetur);

                        POProdukRetur.IDPenerimaanPOProduksiProduk = DropDownListPenerimaan.SelectedValue != "0" ? DropDownListPenerimaan.SelectedValue : null;
                        POProdukRetur.TBPOProduksiProdukReturDetails.AddRange(ViewStateListDetail.OrderBy(item => item.Produk).ThenBy(item => item.IDAtribut).Select(item => new TBPOProduksiProdukReturDetail
                        {
                            TBStokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == item.IDStokProduk),
                            HargaBeli = item.HargaBeli,
                            HargaRetur = item.HargaVendor,
                            HargaJual = item.HargaJual,
                            Jumlah = item.Jumlah
                        }));
                        POProdukRetur.Grandtotal = POProdukRetur.TBPOProduksiProdukReturDetails.Sum(item => item.Jumlah * item.HargaBeli);
                        POProdukRetur.EnumStatusRetur = (int)EnumStatusPORetur.Baru;
                        POProdukRetur.Keterangan = TextBoxKeterangan.Text;

                        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                        foreach (var item in POProdukRetur.TBPOProduksiProdukReturDetails)
                        {
                            StokProduk_Class.BertambahBerkurang(POProdukRetur.IDTempat.Value, pengguna.IDPengguna, item.TBStokProduk, item.Jumlah.Value, item.HargaBeli.Value, item.HargaJual.Value, EnumJenisPerpindahanStok.TransaksiBatal, "(" + item.TBStokProduk.TBKombinasiProduk.Nama + ") Retur PO #" + IDPOProduksiProdukRetur);
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
                            POProdukRetur = db.TBPOProduksiProdukReturs.FirstOrDefault(item => item.IDPOProduksiProdukRetur == IDPOProduksiProdukRetur);
                            if (POProdukRetur != null)
                            {
                                db.TBPOProduksiProdukReturDetails.DeleteAllOnSubmit(POProdukRetur.TBPOProduksiProdukReturDetails);
                                db.TBPOProduksiProdukReturs.DeleteOnSubmit(POProdukRetur);
                                db.SubmitChanges();

                                IDPOProduksiProdukRetur = string.Empty;
                            }
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Retur PO Produk (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
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
                LabelPeringatan.Text = "Tidak ada Produk yang dipilih";
                peringatan.Visible = true;
            }
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}