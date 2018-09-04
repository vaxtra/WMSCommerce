using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_PurchaseOrder_Pengaturan : System.Web.UI.Page
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

                DropDownListSupplier.DataSource = db.TBSuppliers.OrderBy(item => item.Nama).ToArray();
                DropDownListSupplier.DataTextField = "Nama";
                DropDownListSupplier.DataValueField = "IDSupplier";
                DropDownListSupplier.DataBind();
                DropDownListSupplier.Items.Insert(0, new ListItem { Text = "-Pilih Supplier-", Value = "0" });

                ViewState["ViewStateListDetail"] = new List<POProduksiDetail_Model>();

                if (!string.IsNullOrEmpty(Request.QueryString["baru"]))
                    LoadPOLama(db, Request.QueryString["baru"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                    LoadPOLama(db, Request.QueryString["edit"]);
                else if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
                    LoadProyeksi(db, Request.QueryString["proy"]);
                else
                {
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
                        LabelSatuan.Text = "/" + daftarStokBahanBaku.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt()).TBBahanBaku.TBSatuan1.Nama;
                    }
                }
            }
        }
    }

    private void LoadPOLama(DataClassesDatabaseDataContext db, string IDPOProduksiBahanBaku)
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);

        TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : string.Empty;
        TextBoxPegawai.Text = pengguna.NamaLengkap;
        DropDownListPenggunaPIC.SelectedValue = poProduksiBahanBaku.IDPenggunaPIC.ToString();
        TextBoxTanggal.Text = poProduksiBahanBaku.Tanggal.ToString("d MMMM yyyy");
        TextBoxTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo != null ? poProduksiBahanBaku.TanggalJatuhTempo.Value.ToString("d MMMM yyyy") : DateTime.Now.ToString("d MMMM yyyy");
        TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman != null ? poProduksiBahanBaku.TanggalPengiriman.Value.ToString("d MMMM yyyy") : DateTime.Now.ToString("d MMMM yyyy");

        DropDownListSupplier.SelectedValue = poProduksiBahanBaku.IDSupplier.ToString();
        HiddenFieldTax.Value = poProduksiBahanBaku.TBSupplier.PersentaseTax.ToString();
        LabelTax.Text = "Tax (" + (poProduksiBahanBaku.TBSupplier.PersentaseTax * 100).ToFormatHarga() + "%)";

        TBStokBahanBaku[] daftarStokBahanBaku = null;
        if (TextBoxIDProyeksi.Text != string.Empty)
        {
            var proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == TextBoxIDProyeksi.Text && item.BahanBakuDasar == true).GroupBy(item => new
            {
                item.IDBahanBaku
            }).ToArray();
            daftarStokBahanBaku = db.TBStokBahanBakus.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiKomposisi.Any(data => data.Key.IDBahanBaku == item.IDBahanBaku)).OrderBy(item => item.TBBahanBaku.Nama).ToArray();
        }
        else
        {
            daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();
        }
        DropDownListStokBahanBaku.DataSource = daftarStokBahanBaku.Select(item => new { item.IDStokBahanBaku, item.TBBahanBaku.Nama });
        DropDownListStokBahanBaku.DataTextField = "Nama";
        DropDownListStokBahanBaku.DataValueField = "IDStokBahanBaku";
        DropDownListStokBahanBaku.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in poProduksiBahanBaku.TBPOProduksiBahanBakuDetails)
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDBahanBaku = stokBahanBaku.TBBahanBaku.IDBahanBaku;
            detail.IDSatuan = item.IDSatuan;
            detail.IDStokBahanBaku = stokBahanBaku.IDStokBahanBaku;
            detail.Kode = stokBahanBaku.TBBahanBaku.KodeBahanBaku;
            detail.BahanBaku = stokBahanBaku.TBBahanBaku.Nama;
            detail.Satuan = item.TBSatuan.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
            detail.Harga = item.HargaSupplier;
            detail.PotonganHarga = item.PotonganHargaSupplier;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Jumlah;
            detail.Sisa = detail.Jumlah;

            ViewStateListDetail.Add(detail);
        }
        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        TextBoxKeterangan.Text = poProduksiBahanBaku.Keterangan;
        TextBoxBiayaLainLain.Text = poProduksiBahanBaku.BiayaLainLain.ToFormatHarga();
        TextBoxPotonganPO.Text = poProduksiBahanBaku.PotonganPOProduksiBahanBaku.ToFormatHarga();
        LoadData();

        decimal subtotal = (LabelTotalSubtotal.Text.ToDecimal() + poProduksiBahanBaku.BiayaLainLain.Value - poProduksiBahanBaku.PotonganPOProduksiBahanBaku.Value);
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

        TBSupplier supplier = null;

        if (DropDownListSupplier.SelectedValue == "0")
        {
            HiddenFieldTax.Value = "0";
            LabelTax.Text = "Tax (0%)";
        }
        else
        {
            supplier = db.TBSuppliers.FirstOrDefault(item => item.IDSupplier == DropDownListSupplier.SelectedValue.ToInt());
            HiddenFieldTax.Value = supplier.PersentaseTax.ToString();
            LabelTax.Text = "Tax (" + (supplier.PersentaseTax * 100).ToFormatHarga() + "%)";
        }

        var proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item => item.IDProyeksi == IDProyeksi && 
            item.BahanBakuDasar == true)
        .GroupBy(item => new
        {
            item.TBBahanBaku
        })
        .Select(item => new
        {
            item.Key,
            Sisa = item.Sum(x => x.Sisa),
        }).OrderBy(data => data.Key.TBBahanBaku.Nama);
        TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.AsEnumerable().Where(item => item.IDTempat == pengguna.IDTempat && proyeksiKomposisi.Any(data => data.Key.TBBahanBaku.IDBahanBaku == item.IDBahanBaku)).OrderBy(item => item.TBBahanBaku.Nama).ToArray();
        DropDownListStokBahanBaku.DataSource = daftarStokBahanBaku.Select(item => new { item.IDStokBahanBaku, item.TBBahanBaku.Nama }).ToArray();
        DropDownListStokBahanBaku.DataTextField = "Nama";
        DropDownListStokBahanBaku.DataValueField = "IDStokBahanBaku";
        DropDownListStokBahanBaku.DataBind();

        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];
        foreach (var item in proyeksiKomposisi.Where(item => item.Sisa > 0))
        {
            TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.Key.TBBahanBaku.IDBahanBaku);

            POProduksiDetail_Model detail = new POProduksiDetail_Model();
            detail.IDBahanBaku = stokBahanBaku.TBBahanBaku.IDBahanBaku;
            detail.IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuanKonversi;
            detail.IDStokBahanBaku = stokBahanBaku.IDStokBahanBaku;
            detail.Kode = stokBahanBaku.TBBahanBaku.KodeBahanBaku;
            detail.BahanBaku = stokBahanBaku.TBBahanBaku.Nama;
            detail.Satuan = stokBahanBaku.TBBahanBaku.TBSatuan1.Nama;
            detail.HargaPokokKomposisi = 0;
            detail.BiayaTambahan = 0;
            detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
            detail.Harga = supplier == null ? 0 : supplier.TBHargaSuppliers.FirstOrDefault(data => data.IDStokBahanBaku == stokBahanBaku.IDStokBahanBaku) == null ? 0 : supplier.TBHargaSuppliers.FirstOrDefault(data => data.IDStokBahanBaku == stokBahanBaku.IDStokBahanBaku).Harga.Value;
            detail.PotonganHarga = 0;
            detail.TotalHarga = detail.Harga - detail.PotonganHarga;
            detail.Jumlah = item.Sisa / stokBahanBaku.TBBahanBaku.Konversi.Value;
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

    protected void TextBoxBiaya_TextChanged(object sender, EventArgs e)
    {
        HitungGrandTotal();
    }

    private void HitungGrandTotal()
    {
        if (string.IsNullOrWhiteSpace(TextBoxBiayaLainLain.Text))
        {
            TextBoxBiayaLainLain.Text = "0.00";
        }
        if (string.IsNullOrWhiteSpace(TextBoxPotonganPO.Text))
        {
            TextBoxPotonganPO.Text = "0.00";
        }

        decimal subtotal = (LabelTotalSubtotal.Text.ToDecimal() + TextBoxBiayaLainLain.Text.ToDecimal() - TextBoxPotonganPO.Text.ToDecimal());
        decimal tax = subtotal * HiddenFieldTax.Value.ToDecimal();
        TextBoxTax.Text = tax.ToFormatHarga();
        TextBoxGrandtotal.Text = (subtotal + tax).ToFormatHarga();
    }

    private void CariHargaSupplierVendor()
    {
        TextBoxHargaSupplier.Text = "0.00";
        TextBoxJumlah.Text = "1.00";
        TextBoxPotonganHargaSupplier.Text = "0.00";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBHargaSupplier hargaSupplier = db.TBHargaSuppliers.FirstOrDefault(item => item.IDSupplier == DropDownListSupplier.SelectedValue.ToInt() && item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

            if (hargaSupplier != null)
            {
                TextBoxHargaSupplier.Text = hargaSupplier.Harga.ToFormatHarga();
                LabelSatuan.Text = "/" + hargaSupplier.TBStokBahanBaku.TBBahanBaku.TBSatuan1.Nama;
            }
            else
            {
                LabelSatuan.Text = "/" + db.TBStokBahanBakus.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt()).TBBahanBaku.TBSatuan1.Nama;
            }
        }
    }

    protected void DropDownListSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (DropDownListSupplier.SelectedValue == "0")
            {
                HiddenFieldTax.Value = "0";
                LabelTax.Text = "Tax (0%)";
            }
            else
            {
                TBSupplier supplier = db.TBSuppliers.FirstOrDefault(item => item.IDSupplier == DropDownListSupplier.SelectedValue.ToInt());

                HiddenFieldTax.Value = supplier.PersentaseTax.ToString();
                LabelTax.Text = "Tax (" + (supplier.PersentaseTax * 100).ToFormatHarga() + "%)";
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

    protected void DropDownListStokBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        CariHargaSupplierVendor();
    }

    protected void ButtonSimpanDetail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (TextBoxJumlah.Text.ToDecimal() > 0)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

                    POProduksiDetail_Model detail = ViewStateListDetail.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

                    if (detail == null)
                    {
                        TBStokBahanBaku stokBahanBaku = db.TBStokBahanBakus.FirstOrDefault(item => item.IDStokBahanBaku == DropDownListStokBahanBaku.SelectedValue.ToInt());

                        detail = new POProduksiDetail_Model();
                        detail.IDBahanBaku = stokBahanBaku.TBBahanBaku.IDBahanBaku;
                        detail.IDSatuan = stokBahanBaku.TBBahanBaku.IDSatuanKonversi;
                        detail.IDStokBahanBaku = stokBahanBaku.IDStokBahanBaku;
                        detail.Kode = stokBahanBaku.TBBahanBaku.KodeBahanBaku;
                        detail.BahanBaku = stokBahanBaku.TBBahanBaku.Nama;
                        detail.Satuan = stokBahanBaku.TBBahanBaku.TBSatuan1.Nama;
                        detail.HargaPokokKomposisi = 0;
                        detail.BiayaTambahan = 0;
                        detail.TotalHPP = detail.BiayaTambahan + detail.HargaPokokKomposisi;
                        detail.Harga = TextBoxHargaSupplier.Text.ToDecimal();
                        detail.PotonganHarga = TextBoxPotonganHargaSupplier.Text.ToDecimal();
                        detail.TotalHarga = detail.Harga - detail.PotonganHarga;
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal();
                        detail.Sisa = detail.Jumlah;
                        ViewStateListDetail.Add(detail);
                    }
                    else
                    {
                        detail.Harga = TextBoxHargaSupplier.Text.ToDecimal();
                        detail.PotonganHarga = TextBoxPotonganHargaSupplier.Text.ToDecimal();
                        detail.TotalHarga = detail.Harga - detail.PotonganHarga;
                        detail.Jumlah = TextBoxJumlah.Text.ToDecimal();
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
            LabelTotalSubtotal.Text = "0";
        }
        else
        {
            LabelTotalSubtotal.Text = ViewStateListDetail.Sum(item => item.SubtotalHarga).ToFormatHarga();
        }

        ViewState["ViewStateListDetail"] = ViewStateListDetail;

        HitungGrandTotal();
    }

    protected void RepeaterDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        List<POProduksiDetail_Model> ViewStateListDetail = (List<POProduksiDetail_Model>)ViewState["ViewStateListDetail"];

        ViewStateListDetail.Remove(ViewStateListDetail.FirstOrDefault(item => item.IDBahanBaku == e.CommandArgument.ToInt()));
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
                string IDPOProduksiBahanBaku = string.Empty;
                TBPOProduksiBahanBaku produksiBahanBaku = null;
                bool statusBerhasil = false;

                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                        {
                            produksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == Request.QueryString["edit"]);

                            if (produksiBahanBaku.IDProyeksi != null)
                            {
                                foreach (var item in produksiBahanBaku.TBPOProduksiBahanBakuDetails.OrderByDescending(data => data.TBBahanBaku.Nama))
                                {
                                    decimal jumlah = item.Jumlah * item.TBBahanBaku.Konversi.Value;
                                    foreach (var item2 in db.TBProyeksiKomposisis.Where(item2 => item2.IDProyeksi == produksiBahanBaku.IDProyeksi && item2.BahanBakuDasar == true && item2.IDBahanBaku == item.IDBahanBaku).OrderByDescending(item2 => item2.TBBahanBaku.Nama).ThenByDescending(item2 => item2.LevelProduksi))
                                    {
                                        if (item2.Sisa + jumlah <= item2.Jumlah)
                                        {
                                            item2.Sisa = item2.Sisa + jumlah;
                                            jumlah = 0;
                                            break;
                                        }
                                        else
                                        {
                                            jumlah = (jumlah + item2.Sisa) - item2.Jumlah;
                                            item2.Sisa = item2.Jumlah;
                                        }
                                    }
                                }
                            }

                            db.TBPOProduksiBahanBakuDetails.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuDetails);
                            produksiBahanBaku.TBPOProduksiBahanBakuDetails.Clear();

                            produksiBahanBaku.IDTempat = pengguna.IDTempat;
                            produksiBahanBaku.IDPengguna = pengguna.IDPengguna;
                            produksiBahanBaku.EnumJenisProduksi = (int)PilihanEnumJenisProduksi.PurchaseOrder;
                            produksiBahanBaku.Tanggal = TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                        }
                        else
                        {
                            db.Proc_InsertPOProduksiBahanBaku(ref IDPOProduksiBahanBaku, pengguna.IDTempat, pengguna.IDPengguna, (int)PilihanEnumJenisProduksi.PurchaseOrder, TextBoxTanggal.Text.ToDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute));

                            produksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);
                        }

                        produksiBahanBaku.IDProyeksi = TextBoxIDProyeksi.Text != string.Empty ? TextBoxIDProyeksi.Text : null;
                        produksiBahanBaku.IDSupplier = DropDownListSupplier.SelectedValue.ToInt();
                        produksiBahanBaku.IDPenggunaPIC = DropDownListPenggunaPIC.SelectedValue.ToInt();
                        produksiBahanBaku.IDPenggunaDP = null;
                        produksiBahanBaku.IDJenisPOProduksi = null;
                        produksiBahanBaku.IDJenisPembayaran = null;
                        produksiBahanBaku.LevelProduksi = null;
                        produksiBahanBaku.TanggalDownPayment = null;
                        produksiBahanBaku.TanggalJatuhTempo = TextBoxTanggalJatuhTempo.Text.ToDateTime();
                        produksiBahanBaku.TanggalPengiriman = TextBoxTanggalPengiriman.Text.ToDateTime();
                        produksiBahanBaku.TBPOProduksiBahanBakuDetails.AddRange(ViewStateListDetail.OrderBy(item => item.BahanBaku).Select(item => new TBPOProduksiBahanBakuDetail
                        {
                            IDBahanBaku = item.IDBahanBaku,
                            IDSatuan = item.IDSatuan,
                            HargaPokokKomposisi = item.HargaPokokKomposisi,
                            BiayaTambahan = item.BiayaTambahan,
                            TotalHPP = item.TotalHPP,
                            HargaSupplier = item.Harga,
                            PotonganHargaSupplier = item.PotonganHarga,
                            TotalHargaSupplier = item.TotalHarga,
                            Jumlah = item.Jumlah,
                            Sisa = item.Sisa
                        }));

                        produksiBahanBaku.TotalJumlah = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => item.Jumlah);
                        produksiBahanBaku.EnumJenisHPP = (int)PilihanEnumJenisHPP.HargaSupplierVendor;
                        produksiBahanBaku.SubtotalBiayaTambahan = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.Jumlah * item.BiayaTambahan));
                        produksiBahanBaku.SubtotalTotalHPP = produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.Jumlah * item.TotalHPP));
                        produksiBahanBaku.SubtotalTotalHargaSupplier = Math.Round(produksiBahanBaku.TBPOProduksiBahanBakuDetails.Sum(item => (item.Jumlah * item.TotalHargaSupplier)), 2, MidpointRounding.AwayFromZero);
                        produksiBahanBaku.PotonganPOProduksiBahanBaku = TextBoxPotonganPO.Text.ToDecimal();
                        produksiBahanBaku.BiayaLainLain = TextBoxBiayaLainLain.Text.ToDecimal();
                        produksiBahanBaku.PersentaseTax = HiddenFieldTax.Value.ToDecimal();

                        decimal subtotal = produksiBahanBaku.SubtotalTotalHargaSupplier.Value + produksiBahanBaku.BiayaLainLain.Value - produksiBahanBaku.PotonganPOProduksiBahanBaku.Value;
                        produksiBahanBaku.Tax = Math.Round((subtotal * produksiBahanBaku.PersentaseTax.Value), 2, MidpointRounding.AwayFromZero);
                        produksiBahanBaku.Grandtotal = subtotal + produksiBahanBaku.Tax;
                        produksiBahanBaku.DownPayment = 0;
                        produksiBahanBaku.Keterangan = TextBoxKeterangan.Text;

                        if (produksiBahanBaku.IDProyeksi != null)
                        {
                            foreach (var item in produksiBahanBaku.TBPOProduksiBahanBakuDetails.OrderBy(data => data.TBBahanBaku.Nama))
                            {
                                decimal jumlah = item.Jumlah * item.TBBahanBaku.Konversi.Value;
                                TBProyeksiKomposisi[] proyeksiKomposisi = db.TBProyeksiKomposisis.Where(item2 => item2.IDProyeksi == produksiBahanBaku.IDProyeksi && item2.BahanBakuDasar == true && item2.IDBahanBaku == item.IDBahanBaku).OrderBy(item2 => item2.TBBahanBaku.Nama).ThenBy(item2 => item2.LevelProduksi).ToArray();
                                int count = proyeksiKomposisi.Count();
                                foreach (var item2 in proyeksiKomposisi)
                                {
                                    if (item2.Sisa - jumlah >= 0)
                                    {
                                        item2.Sisa = item2.Sisa - jumlah;
                                        jumlah = 0;
                                        break;
                                    }
                                    else
                                    {
                                        if (count > 1)
                                        {
                                            jumlah = Math.Abs(item2.Sisa - jumlah);
                                            item2.Sisa = 0;
                                        }
                                        else
                                        {
                                            item2.Sisa = item2.Sisa - jumlah;
                                        }
                                    }

                                    count = count - 1;
                                }
                            }
                        }


                        TBHargaSupplier[] daftarHargaSupplier = db.TBHargaSuppliers.Where(item => item.IDSupplier == produksiBahanBaku.IDSupplier).ToArray();

                        foreach (var item in ViewStateListDetail)
                        {
                            TBHargaSupplier hargaSupplier = daftarHargaSupplier.FirstOrDefault(data => data.IDStokBahanBaku == item.IDStokBahanBaku);

                            if (hargaSupplier == null)
                                db.TBHargaSuppliers.InsertOnSubmit(new TBHargaSupplier() { IDStokBahanBaku = item.IDStokBahanBaku, IDSupplier = produksiBahanBaku.IDSupplier, Tanggal = produksiBahanBaku.Tanggal, Harga = item.Harga });

                            else
                            {
                                hargaSupplier.Tanggal = produksiBahanBaku.Tanggal;
                                hargaSupplier.Harga = item.Harga;
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
                            produksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == IDPOProduksiBahanBaku);
                            if (produksiBahanBaku != null)
                            {
                                db.TBPOProduksiBahanBakuDetails.DeleteAllOnSubmit(produksiBahanBaku.TBPOProduksiBahanBakuDetails);
                                db.TBPOProduksiBahanBakus.DeleteOnSubmit(produksiBahanBaku);
                                db.SubmitChanges();

                                IDPOProduksiBahanBaku = string.Empty;
                            }
                        }
                    }
                    LogError_Class LogError = new LogError_Class(ex, "Purchase Order Bahan Baku (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
                    LabelPeringatan.Text = "Terjadi kesalahan, silahkan cek kembali data yang diinputkan";
                    peringatan.Visible = true;
                }
                finally
                {
                    if (statusBerhasil == true)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
                            Response.Redirect("Detail.aspx?id=" + produksiBahanBaku.IDPOProduksiBahanBaku);
                        else
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
        if (!string.IsNullOrEmpty(Request.QueryString["proy"]))
            Response.Redirect("/WITAdministrator/Produk/Proyeksi/Detail.aspx?id=" + Request.QueryString["proy"]);
        else if (!string.IsNullOrEmpty(Request.QueryString["edit"]))
            Response.Redirect("/WITAdministrator/BahanBaku/POProduksi/PurchaseOrder/Detail.aspx?id=" + Request.QueryString["edit"]);
        else
            Response.Redirect("Default.aspx");
    }
}