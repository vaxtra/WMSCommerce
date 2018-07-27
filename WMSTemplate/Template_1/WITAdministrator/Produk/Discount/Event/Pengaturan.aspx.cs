using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Discount_Event_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Menubar_Class ClassMenubar = new Menubar_Class(db);
                    Tempat_Class ClassTempat = new Tempat_Class(db);

                    ClassTempat.DropDownList(DropDownListTempat);
                    ClassMenubar.EnumStatusDiscountEventDropdownList(DropDownListEnumStatusDiscountEvent);

                    DiscountEvent_Class ClassDiscountEvent = new DiscountEvent_Class(db);
                    var Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    var DiscountEvent = ClassDiscountEvent.Cari(Request.QueryString["id"].ToInt());

                    if (DiscountEvent != null)
                    {
                        DropDownListTempat.SelectedValue = DiscountEvent.IDTempat.ToString();
                        TextBoxNama.Text = DiscountEvent.Nama;
                        TextBoxTanggalAwal.Text = DiscountEvent.TanggalAwal.ToString("d MMMM yyyy");
                        TextBoxTanggalAkhir.Text = DiscountEvent.TanggalAkhir.ToString("d MMMM yyyy");
                        DropDownListEnumStatusDiscountEvent.SelectedValue = DiscountEvent.EnumStatusDiscountEvent.ToString();

                        ButtonOk.Text = "Ubah";

                        PanelDiscount.Visible = true;
                        LoadData();
                    }
                    else
                    {
                        ButtonOk.Text = "Tambah";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                DiscountEvent_Class ClassDiscountEvent = new DiscountEvent_Class(db);

                if (ButtonOk.Text == "Tambah")
                {
                    var DiscountEvent = ClassDiscountEvent.Tambah(Pengguna.IDPengguna, DropDownListTempat.SelectedValue.ToInt(), TextBoxNama.Text, TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime(), (EnumStatusDiscountEvent)DropDownListEnumStatusDiscountEvent.SelectedValue.ToInt());
                    db.SubmitChanges();

                    Response.Redirect("Pengaturan.aspx?id=" + DiscountEvent.IDDiscountEvent);

                }
                else if (ButtonOk.Text == "Ubah")
                {
                    var DiscountEvent = ClassDiscountEvent.Ubah(Request.QueryString["id"].ToInt(), Pengguna.IDPengguna, DropDownListTempat.SelectedValue.ToInt(), TextBoxNama.Text, TextBoxTanggalAwal.Text.ToDateTime(), TextBoxTanggalAkhir.Text.ToDateTime(), (EnumStatusDiscountEvent)DropDownListEnumStatusDiscountEvent.SelectedValue.ToInt());

                    db.TBDiscounts.DeleteAllOnSubmit(DiscountEvent.TBDiscounts.ToArray());

                    //VALIDASI
                    foreach (RepeaterItem item in RepeaterStokProduk.Items)
                    {
                        Label LabelIDStokProduk = (Label)item.FindControl("LabelIDStokProduk");
                        TextBox TextBoxDiscountStorePersentase = (TextBox)item.FindControl("TextBoxDiscountStorePersentase");
                        TextBox TextBoxDiscountStoreNominal = (TextBox)item.FindControl("TextBoxDiscountStoreNominal");
                        TextBox TextBoxDiscountConsignmentPersentase = (TextBox)item.FindControl("TextBoxDiscountConsignmentPersentase");
                        TextBox TextBoxDiscountConsignmentNominal = (TextBox)item.FindControl("TextBoxDiscountConsignmentNominal");

                        if (!string.IsNullOrWhiteSpace(TextBoxDiscountStorePersentase.Text) && !string.IsNullOrWhiteSpace(TextBoxDiscountStoreNominal.Text))
                            WMSError.Pesan("Discount Store hanya boleh memilih antara Discount Persentase atau Nominal");

                        if (!string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentPersentase.Text) && !string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentNominal.Text))
                            WMSError.Pesan("Discount Consignment hanya boleh memilih antara Discount Persentase atau Nominal");

                        var StokProduk = db.TBStokProduks.FirstOrDefault(item2 => item2.IDStokProduk == LabelIDStokProduk.Text.ToInt());

                        var Discount = new TBDiscount
                        {
                            //IDDiscount
                            TBDiscountEvent = DiscountEvent,
                            TBStokProduk = StokProduk
                        };

                        if (!string.IsNullOrWhiteSpace(TextBoxDiscountStorePersentase.Text) && TextBoxDiscountStorePersentase.Text != "0")
                        {
                            Discount.EnumDiscountStore = (int)EnumDiscount.Persentase;
                            Discount.DiscountStore = TextBoxDiscountStorePersentase.Text.ToDecimal();
                        }
                        else if (!string.IsNullOrWhiteSpace(TextBoxDiscountStoreNominal.Text) && TextBoxDiscountStoreNominal.Text != "0")
                        {
                            Discount.EnumDiscountStore = (int)EnumDiscount.Nominal;
                            Discount.DiscountStore = TextBoxDiscountStoreNominal.Text.ToDecimal();
                        }
                        else
                        {
                            Discount.EnumDiscountStore = (int)EnumDiscount.TidakAda;
                            Discount.DiscountStore = 0;
                        }

                        if (!string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentPersentase.Text) && TextBoxDiscountConsignmentPersentase.Text != "0")
                        {
                            Discount.EnumDiscountKonsinyasi = (int)EnumDiscount.Persentase;
                            Discount.DiscountKonsinyasi = TextBoxDiscountConsignmentPersentase.Text.ToDecimal();
                        }
                        else if (!string.IsNullOrWhiteSpace(TextBoxDiscountConsignmentNominal.Text) && TextBoxDiscountConsignmentNominal.Text != "0")
                        {
                            Discount.EnumDiscountKonsinyasi = (int)EnumDiscount.Nominal;
                            Discount.DiscountKonsinyasi = TextBoxDiscountConsignmentNominal.Text.ToDecimal();
                        }
                        else
                        {
                            Discount.EnumDiscountKonsinyasi = (int)EnumDiscount.TidakAda;
                            Discount.DiscountKonsinyasi = 0;
                        }

                        if (Discount.EnumDiscountStore != (int)EnumDiscount.TidakAda || Discount.EnumDiscountKonsinyasi != (int)EnumDiscount.TidakAda)
                            db.TBDiscounts.InsertOnSubmit(Discount);
                    }

                    db.SubmitChanges();

                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    public void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var ListDiscountStokProduk = db.TBDiscounts.Where(item => item.IDDiscountEvent == Request.QueryString["id"].ToInt());

            RepeaterStokProduk.DataSource = db.TBStokProduks
                .Where(item =>
                    item.TBKombinasiProduk.TBProduk._IsActive &&
                    item.IDTempat == DropDownListTempat.SelectedValue.ToInt())
                .AsEnumerable()
                .Select(item => new
                {
                    item.IDStokProduk,
                    item.TBKombinasiProduk.KodeKombinasiProduk,
                    item.TBKombinasiProduk.TBProduk.Nama,
                    Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                    Varian = item.TBKombinasiProduk.TBAtributProduk.Nama,
                    Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                    Brand = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    item.HargaJual,
                    item.Jumlah,
                    DiscountStoreNominal = "",
                    DiscountStorePersentase = "",
                    DiscountKonsinyasiNominal = "",
                    DiscountKonsinyasiPersentase = ""
                })
                .ToArray();
            RepeaterStokProduk.DataBind();

            foreach (RepeaterItem item in RepeaterStokProduk.Items)
            {
                Label LabelIDStokProduk = (Label)item.FindControl("LabelIDStokProduk");

                var DiscountStokProduk = ListDiscountStokProduk.FirstOrDefault(item2 => item2.IDStokProduk == LabelIDStokProduk.Text.ToInt());

                if (DiscountStokProduk != null)
                {
                    TextBox TextBoxDiscountStorePersentase = (TextBox)item.FindControl("TextBoxDiscountStorePersentase");
                    TextBox TextBoxDiscountStoreNominal = (TextBox)item.FindControl("TextBoxDiscountStoreNominal");
                    TextBox TextBoxDiscountConsignmentPersentase = (TextBox)item.FindControl("TextBoxDiscountConsignmentPersentase");
                    TextBox TextBoxDiscountConsignmentNominal = (TextBox)item.FindControl("TextBoxDiscountConsignmentNominal");

                    TextBoxDiscountStorePersentase.Text = DiscountStokProduk.EnumDiscountStore == (int)EnumDiscount.Persentase ? DiscountStokProduk.DiscountStore.ToFormatHarga() : "";
                    TextBoxDiscountStoreNominal.Text = DiscountStokProduk.EnumDiscountStore == (int)EnumDiscount.Nominal ? DiscountStokProduk.DiscountStore.ToFormatHarga() : "";
                    TextBoxDiscountConsignmentPersentase.Text = DiscountStokProduk.EnumDiscountKonsinyasi == (int)EnumDiscount.Persentase ? DiscountStokProduk.DiscountKonsinyasi.ToFormatHarga() : "";
                    TextBoxDiscountConsignmentNominal.Text = DiscountStokProduk.EnumDiscountKonsinyasi == (int)EnumDiscount.Nominal ? DiscountStokProduk.DiscountKonsinyasi.ToFormatHarga() : "";
                }
            }
        }
    }
}