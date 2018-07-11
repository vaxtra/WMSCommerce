using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_PurchaseOrder_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                TextBoxIDProyeksi.Text = string.Empty;
                TextBoxPegawai.Text = pengguna.NamaLengkap;
                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalJatuhTempo.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalPengiriman.Text = DateTime.Now.ToString("d MMMM yyyy");

                Pengguna dmPengguna = new Pengguna();
                DropDownListPenggunaPIC.DataSource = dmPengguna.CariBawahanSemua(db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == pengguna.IDPengguna)).OrderBy(item => item.LevelJabatan).ThenBy(item => item.NamaLengkap);
                DropDownListPenggunaPIC.DataTextField = "NamaLengkap";
                DropDownListPenggunaPIC.DataValueField = "IDPengguna";
                DropDownListPenggunaPIC.DataBind();
                DropDownListPenggunaPIC.Items.Insert(0, new ListItem { Text = pengguna.NamaLengkap, Value = pengguna.IDPengguna.ToString() });

                DropDownListVendor.DataSource = db.TBVendors.OrderBy(item => item.Nama).ToArray();
                DropDownListVendor.DataTextField = "Nama";
                DropDownListVendor.DataValueField = "IDVendor";
                DropDownListVendor.DataBind();
                DropDownListVendor.Items.Insert(0, new ListItem { Text = "-Pilih Vendor-", Value = "0" });

                ViewState["ViewStateListDetail"] = new List<POProduksiDetail_Model>();

                if (!string.IsNullOrEmpty(Request.QueryString["baru"]))
                    LoadPOLama(db, Request.QueryString["baru"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                    LoadPOLama(db, Request.QueryString["edit"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
                    LoadProyeksi(db, Request.QueryString["proy"]);
                else
                {
                    DropDownListStokProduk.DataSource = db.TBStokProduks
                        .Where(item =>
                            item.IDTempat == pengguna.IDTempat &&
                            item.TBKombinasiProduk.TBProduk._IsActive)
                        .Select(item => new
                        {
                            item.IDStokProduk,
                            item.TBKombinasiProduk.Nama
                        })
                        .OrderBy(item => item.Nama)
                        .ToArray();

                    DropDownListStokProduk.DataTextField = "Nama";
                    DropDownListStokProduk.DataValueField = "IDStokProduk";
                    DropDownListStokProduk.DataBind();

                    if (DropDownListStokProduk.Items.Count == 0)
                    {
                        ButtonSimpanDetail.Enabled = false;
                        ButtonSimpan.Enabled = false;
                    }
                }
            }
        }
    }

    private void LoadPOLama(DataClassesDatabaseDataContext db, string IDPOProduksiProduk)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TBPOProduksiProduk poProduksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);

        TextBoxIDProyeksi.Text = poProduksiProduk.IDProyeksi != null ? poProduksiProduk.IDProyeksi : string.Empty;
        TextBoxPegawai.Text = pengguna.NamaLengkap;
        DropDownListPenggunaPIC.SelectedValue = poProduksiProduk.IDPenggunaPIC.ToString();
        TextBoxTanggal.Text = poProduksiProduk.Tanggal.ToString("d MMMM yyyy");
        TextBoxTanggalJatuhTempo.Text = poProduksiProduk.TanggalJatuhTempo != null ? poProduksiProduk.TanggalJatuhTempo.Value.ToString("d MMMM yyyy") : DateTime.Now.ToString("d MMMM yyyy");
        TextBoxTanggalPengiriman.Text = poProduksiProduk.TanggalPengiriman != null ? poProduksiProduk.TanggalPengiriman.Value.ToString("d MMMM yyyy") : DateTime.Now.ToString("d MMMM yyyy");

        DropDownListVendor.SelectedValue = poProduksiProduk.IDVendor.ToString();

        TextBoxAlamat.Text = poProduksiProduk.TBVendor.Alamat;
        TextBoxEmail.Text = poProduksiProduk.TBVendor.Email;
        TextBoxTelepon1.Text = poProduksiProduk.TBVendor.Telepon1;
        TextBoxTelepon2.Text = poProduksiProduk.TBVendor.Telepon2;
        HiddenFieldTax.Value = poProduksiProduk.TBVendor.PersentaseTax.ToString();
        LabelTax.Text = "Tax (" + (poProduksiProduk.TBVendor.PersentaseTax * 100).ToFormatHarga() + "%)";

        TBStokProduk[] daftarStokProduk = null;
        if (TextBoxIDProyeksi.Text != string.Empty)
        {
            TBProyeksiDetail[] proyeksiDetail = db.TBProyeksiDetails.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
            daftarStokProduk = db.TBStokProduks.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiDetail.Any(data => data.IDKombinasiProduk == item.IDKombinasiProduk)).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
        }
        else
        {
            daftarStokProduk = db.TBStokProduks.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
        }
        DropDownListStokProduk.DataSource = daftarStokProduk.Select(item => new { item.IDStokProduk, item.TBKombinasiProduk.Nama });
        DropDownListStokProduk.DataTextField = "Nama";
        DropDownListStokProduk.DataValueField = "IDStokProduk";
        DropDownListStokProduk.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in poProduksiProduk.TBPOProduksiProdukDetails)
        {
            TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
            detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
            detail.IDStokProduk = stokProduk.IDStokProduk;
            detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
            detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
            detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
            detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
            detail.Harga = item.HargaVendor;
            detail.PotonganHarga = item.PotonganHargaVendor;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Jumlah;
            detail.Sisa = detail.Jumlah;

            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        TextBoxKeterangan.Text = poProduksiProduk.Keterangan;
        TextBoxBiayaLainLain.Text = poProduksiProduk.BiayaLainLain.ToString();
        TextBoxPotonganPO.Text = poProduksiProduk.PotonganPOProduksiProduk.ToString();
        LoadData();

        decimal subtotal = (LabelTotalSubtotal.Text.ToDecimal() + poProduksiProduk.BiayaLainLain.Value - poProduksiProduk.PotonganPOProduksiProduk.Value);
        decimal tax = subtotal * HiddenFieldTax.Value.ToDecimal();
        TextBoxTax.Text = tax.ToFormatHarga();
        TextBoxGrandtotal.Text = (subtotal + tax).ToFormatHarga();

        CariHargaSupplierVendor();
    }
    private void LoadProyeksi(DataClassesDatabaseDataContext db, string IDProyeksi)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TextBoxIDProyeksi.Text = IDProyeksi;
        TextBoxPegawai.Text = pengguna.NamaLengkap;

        TBVendor vendor = null;
        if (DropDownListVendor.SelectedValue == "0")
        {
            TextBoxAlamat.Text = string.Empty;
            TextBoxEmail.Text = string.Empty;
            TextBoxTelepon1.Text = string.Empty;
            TextBoxTelepon2.Text = string.Empty;
            HiddenFieldTax.Value = "0";
            LabelTax.Text = "Tax (0%)";
        }
        else
        {
            vendor = db.TBVendors.FirstOrDefault(item => item.IDVendor == DropDownListVendor.SelectedValue.ToInt());

            TextBoxAlamat.Text = vendor.Alamat;
            TextBoxEmail.Text = vendor.Email;
            TextBoxTelepon1.Text = vendor.Telepon1;
            TextBoxTelepon2.Text = vendor.Telepon2;
            HiddenFieldTax.Value = vendor.PersentaseTax.ToString();
            LabelTax.Text = "Tax (" + (vendor.PersentaseTax * 100).ToFormatHarga() + "%)";
        }

        TBProyeksiDetail[] proyeksiDetail = db.TBProyeksiDetails.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
        TBStokProduk[] daftarStokProduk = db.TBStokProduks.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiDetail.Any(data => data.IDKombinasiProduk == item.IDKombinasiProduk)).OrderBy(item => item.TBKombinasiProduk.Nama).ToArray();
        DropDownListStokProduk.DataSource = daftarStokProduk.Select(item => new { item.IDStokProduk, item.TBKombinasiProduk.Nama });
        DropDownListStokProduk.DataTextField = "Nama";
        DropDownListStokProduk.DataValueField = "IDStokProduk";
        DropDownListStokProduk.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in proyeksiDetail.Where(item => item.Sisa > 0))
        {
            TBStokProduk stokProduk = daftarStokProduk.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
            detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
            detail.IDStokProduk = stokProduk.IDStokProduk;
            detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
            detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
            detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
            detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
            detail.Harga = vendor == null ? 0 : vendor.TBHargaVendors.FirstOrDefault(data => data.IDStokProduk == stokProduk.IDStokProduk) == null ? 0 : vendor.TBHargaVendors.FirstOrDefault(data => data.IDStokProduk == stokProduk.IDStokProduk).Harga.Value;
            detail.PotonganHarga = 0;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Sisa;
            detail.Sisa = detail.Jumlah;

            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        LoadData();

        decimal subtotal = LabelTotalSubtotal.Text.ToDecimal();
        decimal tax = subtotal * HiddenFieldTax.Value.ToDecimal();
        TextBoxTax.Text = tax.ToFormatHarga();
        TextBoxGrandtotal.Text = (subtotal + tax).ToFormatHarga();

        CariHargaSupplierVendor();
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

    protected void TextBoxBiaya_TextChanged(object sender, EventArgs e)
    {
        HitungGrandTotal();
    }

    private void HitungGrandTotal()
    {
        if (string.IsNullOrWhiteSpace(TextBoxBiayaLainLain.Text))
        {
            TextBoxBiayaLainLain.Text = "0";
        }
        if (string.IsNullOrWhiteSpace(TextBoxPotonganPO.Text))
        {
            TextBoxPotonganPO.Text = "0";
        }

        decimal subtotal = (LabelTotalSubtotal.Text.ToDecimal() + TextBoxBiayaLainLain.Text.ToDecimal() - TextBoxPotonganPO.Text.ToDecimal());
        decimal tax = subtotal * HiddenFieldTax.Value.ToDecimal();
        TextBoxTax.Text = tax.ToFormatHarga();
        TextBoxGrandtotal.Text = (subtotal + tax).ToFormatHarga();
    }

    private void CariHargaSupplierVendor()
    {
        TextBoxHargaVendor.Text = "0";
        TextBoxJumlah.Text = "1";
        TextBoxPotonganHargaVendor.Text = "0";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBHargaVendor hargaVendor = db.TBHargaVendors.FirstOrDefault(item => item.IDVendor == DropDownListVendor.SelectedValue.ToInt() && item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

            if (hargaVendor != null)
            {
                TextBoxHargaVendor.Text = hargaVendor.Harga.ToFormatHarga();
            }
        }
    }

    protected void DropDownListVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListVendor.SelectedValue == "0")
            {
                TextBoxAlamat.Text = string.Empty;
                TextBoxEmail.Text = string.Empty;
                TextBoxTelepon1.Text = string.Empty;
                TextBoxTelepon2.Text = string.Empty;
                HiddenFieldTax.Value = "0";
                LabelTax.Text = "Tax (0%)";
            }
            else
            {
                TBVendor vendor = db.TBVendors.FirstOrDefault(item => item.IDVendor == DropDownListVendor.SelectedValue.ToInt());

                TextBoxAlamat.Text = vendor.Alamat;
                TextBoxEmail.Text = vendor.Email;
                TextBoxTelepon1.Text = vendor.Telepon1;
                TextBoxTelepon2.Text = vendor.Telepon2;
                HiddenFieldTax.Value = vendor.PersentaseTax.ToString();
                LabelTax.Text = "Tax (" + (vendor.PersentaseTax * 100).ToFormatHarga() + "%)";
            }

            if (TextBoxIDProyeksi.Text != string.Empty && string.IsNullOrEmpty(Request.QueryString["edit"]))
            {
                ViewState["ViewStateListDetail"] = new List<POProduksiDetail_Model>();
                LoadProyeksi(db, TextBoxIDProyeksi.Text);
            }

            CariHargaSupplierVendor();
            HitungGrandTotal();
        }
    }

    protected void DropDownListStokProduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        CariHargaSupplierVendor();
    }

    protected void ButtonSimpanDetail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (TextBoxJumlah.Text.ToDecimal().ToInt() > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

                    POProduksiDetail_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokProduk stokProduk = db.TBStokProduks.FirstOrDefault(item => item.IDStokProduk == DropDownListStokProduk.SelectedValue.ToInt());

                        detail = new POProduksiDetail_Model();
                        detail.IDProduk = stokProduk.TBKombinasiProduk.IDProduk;
                        detail.IDKombinasiProduk = stokProduk.IDKombinasiProduk;
                        detail.IDStokProduk = stokProduk.IDStokProduk;
                        detail.Kode = stokProduk.TBKombinasiProduk.KodeKombinasiProduk;
                        detail.Produk = stokProduk.TBKombinasiProduk.TBProduk.Nama;
                        detail.Atribut = stokProduk.TBKombinasiProduk.TBAtributProduk.Nama;
                        detail.KombinasiProduk = stokProduk.TBKombinasiProduk.Nama;
                        detail.HargaPokokKomposisi = 0;
                        detail.BiayaTambahan = 0;
                        detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
                        detail.Harga = TextBoxHargaVendor.Text.ToDecimal();
                        detail.PotonganHarga = TextBoxPotonganHargaVendor.Text.ToDecimal();
                        detail.TotalHarga = detail.Harga - detail.PotonganHarga;
                        detail.Jumlah =TextBoxJumlah.Text.ToDecimal().ToInt();
                        detail.Sisa = detail.Jumlah;

                        ViewStateListDetail.Add(detail);
                    }
                    else
                    {
                        detail.Harga = TextBoxHargaVendor.Text.ToDecimal();
                        detail.PotonganHarga = TextBoxPotonganHargaVendor.Text.ToDecimal();
                        detail.TotalHarga = detail.Harga - detail.PotonganHarga;
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal().ToInt();
                        detail.Sisa = detail.Jumlah;
                    }

                    ViewState["ViewStateListDetail"] = ViewStateListDetail;
                }

                LoadData();
            }
        }
    }

    private void LoadData()
    {
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

        RepeaterDetail.DataSource = ViewStateListDetail;
        RepeaterDetail.DataBind();

        if (ViewStateListDetail.Count == 0)
        {
            LabelTotalJumlah.Text = "0";
            LabelTotalSubtotal.Text = "0";
        }
        else
        {
            LabelTotalJumlah.Text = ViewStateListDetail.Sum(item => item.Jumlah).ToFormatHargaBulat();
            LabelTotalSubtotal.Text = ViewStateListDetail.Sum(item => item.SubtotalHarga).ToFormatHarga();
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        HitungGrandTotal();
    }

    protected void RepeaterDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

        ViewStateListDetail.Remove(ViewStateListDetail.FirstOrDefault(item => item.IDKombinasiProduk == e.CommandArgument.ToInt()));
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        LoadData();
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            peringatan.Visible = false;
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

            if (ViewStateListDetail.Count > 0)
            {
                string IDPOProduksiProduk = string.Empty;
                TBPOProduksiProduk produksiProduk = null;

                bool statusBerhasil = false;

                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                        {
                            produksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == Request.QueryString["edit"]);

                            if (produksiProduk.IDProyeksi != null)
                            {
                                foreach (var item in db.TBProyeksiDetails.Where(item => item.IDProyeksi == produksiProduk.IDProyeksi).OrderBy(data => data.TBKombinasiProduk.Nama).ToArray())
                                {
                                    TBPOProduksiProdukDetail poProduksiProdukDetail = produksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);
                                    if (poProduksiProdukDetail != null)
                                    {
                                        item.Sisa = item.Sisa + poProduksiProdukDetail.Jumlah;
                                    }
                                }
                            }

                            db.TBPOProduksiProdukDetails.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukDetails);
                            produksiProduk.TBPOProduksiProdukDetails.Clear();

                            produksiProduk.IDTempat = pengguna.IDTempat;
                            produksiProduk.IDPengguna = pengguna.IDPengguna;
                            produksiProduk.EnumJenisProduksi = (int)PilihanEnumJenisProduksi.PurchaseOrder;
                            produksiProduk.Tanggal = TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                        }
                        else
                        {
                            db.Proc_InsertPOProduksiProduk(ref IDPOProduksiProduk, pengguna.IDTempat, pengguna.IDPengguna, (int)PilihanEnumJenisProduksi.PurchaseOrder, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                            produksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);
                        }

                        produksiProduk.IDProyeksi = TextBoxIDProyeksi.Text != string.Empty ? TextBoxIDProyeksi.Text : null;
                        produksiProduk.IDVendor = DropDownListVendor.SelectedValue.ToInt();
                        produksiProduk.IDPenggunaPIC = DropDownListPenggunaPIC.SelectedValue.ToInt();
                        produksiProduk.IDPenggunaDP = null;
                        produksiProduk.IDJenisPOProduksi = null;
                        produksiProduk.IDJenisPembayaran = null;
                        produksiProduk.TanggalDownPayment = null;
                        produksiProduk.TanggalJatuhTempo = TextBoxTanggalJatuhTempo.Text.ToDateTime();
                        produksiProduk.TanggalPengiriman = TextBoxTanggalPengiriman.Text.ToDateTime();
                        produksiProduk.TBPOProduksiProdukDetails.AddRange(ViewStateListDetail.OrderBy(item => item.KombinasiProduk).Select(item => new TBPOProduksiProdukDetail
                        {
                            IDKombinasiProduk = item.IDKombinasiProduk,
                            HargaPokokKomposisi = item.HargaPokokKomposisi,
                            BiayaTambahan = item.BiayaTambahan,
                            TotalHPP = item.TotalHPP,
                            HargaVendor = item.Harga,
                            PotonganHargaVendor = item.PotonganHarga,
                            TotalHargaVendor = item.TotalHarga,
                            Jumlah = (int)item.Jumlah,
                            Sisa = (int)item.Sisa
                        }));

                        produksiProduk.TotalJumlah = produksiProduk.TBPOProduksiProdukDetails.Sum(item => item.Jumlah);
                        produksiProduk.EnumJenisHPP = (int)PilihanEnumJenisHPP.HargaSupplierVendor;
                        produksiProduk.SubtotalBiayaTambahan = produksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.Jumlah * item.BiayaTambahan));
                        produksiProduk.SubtotalTotalHPP = produksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.Jumlah * item.TotalHPP));
                        produksiProduk.SubtotalTotalHargaVendor = Math.Round(produksiProduk.TBPOProduksiProdukDetails.Sum(item => (item.Jumlah * item.TotalHargaVendor)), 2, MidpointRounding.AwayFromZero);
                        produksiProduk.PotonganPOProduksiProduk =TextBoxPotonganPO.Text.ToDecimal();
                        produksiProduk.BiayaLainLain = TextBoxBiayaLainLain.Text.ToDecimal();
                        produksiProduk.PersentaseTax = HiddenFieldTax.Value.ToInt();

                        decimal subtotal = produksiProduk.SubtotalTotalHargaVendor.Value + produksiProduk.BiayaLainLain.Value - produksiProduk.PotonganPOProduksiProduk.Value;
                        produksiProduk.Tax = Math.Round((subtotal * produksiProduk.PersentaseTax.Value), 2, MidpointRounding.AwayFromZero);
                        produksiProduk.Grandtotal = subtotal + produksiProduk.Tax;
                        produksiProduk.DownPayment = 0;
                        produksiProduk.Keterangan = TextBoxKeterangan.Text;

                        if (produksiProduk.IDProyeksi != null)
                        {
                            foreach (var item in db.TBProyeksiDetails.Where(item => item.IDProyeksi == produksiProduk.IDProyeksi).OrderBy(data => data.TBKombinasiProduk.Nama).ToArray())
                            {
                                TBPOProduksiProdukDetail poProduksiProdukDetail = produksiProduk.TBPOProduksiProdukDetails.FirstOrDefault(data => data.IDKombinasiProduk == item.IDKombinasiProduk);
                                if (poProduksiProdukDetail != null)
                                {
                                    item.Sisa = item.Sisa - poProduksiProdukDetail.Jumlah;
                                }
                            }
                        }

                        TBHargaVendor[] daftarHargaVendor = db.TBHargaVendors.Where(item => item.IDVendor == produksiProduk.IDVendor).ToArray();

                        foreach (var item in ViewStateListDetail)
                        {
                            TBHargaVendor hargaVendor = daftarHargaVendor.FirstOrDefault(data => data.IDStokProduk == item.IDStokProduk);

                            if (hargaVendor == null)
                                db.TBHargaVendors.InsertOnSubmit(new TBHargaVendor() { IDStokProduk = item.IDStokProduk, IDVendor = produksiProduk.IDVendor, Tanggal = produksiProduk.Tanggal, Harga = item.Harga });

                            else
                            {
                                hargaVendor.Tanggal = produksiProduk.Tanggal;
                                hargaVendor.Harga = item.Harga;
                            }
                        }

                        db.SubmitChanges();

                        statusBerhasil = true;
                    }
                }
                catch (Exception ex)
                {
                    if (statusBerhasil != true && string.IsNullOrEmpty(Request.QueryString["edit"]))
                    {
                        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                        {
                            produksiProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == IDPOProduksiProduk);
                            if (produksiProduk != null)
                            {
                                db.TBPOProduksiProdukDetails.DeleteAllOnSubmit(produksiProduk.TBPOProduksiProdukDetails);
                                db.TBPOProduksiProduks.DeleteOnSubmit(produksiProduk);
                                db.SubmitChanges();

                                IDPOProduksiProduk = string.Empty;
                            }
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Purchase Order Produk (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
                    LabelPeringatan.Text = "Terjadi kesalahan, silahkan cek kembali data yang diinputkan";
                    peringatan.Visible = true;
                }
                finally
                {
                    if (statusBerhasil == true)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                            Response.Redirect("Detail.aspx?id=" + produksiProduk.IDPOProduksiProduk);
                        else
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
        if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
            Response.Redirect("/WITAdministrator/Produk/Proyeksi/Detail.aspx?id=" + Request.QueryString["proy"]);
        else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
            Response.Redirect("/WITAdministrator/Produk/POProduksi/PurchaseOrder/Detail.aspx?id=" + Request.QueryString["edit"]);
        else
            Response.Redirect("Default.aspx");
    }
}