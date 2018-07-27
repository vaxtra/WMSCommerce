using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITWON_Produk_KelolaStok : System.Web.UI.Page
{
    public Dictionary<string, dynamic> Result { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["do"]))
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                if (Request.QueryString["do"] == "opname")
                    LabelJudul.Text = "Stock Opname";
                else if (Request.QueryString["do"] == "waste")
                    LabelJudul.Text = "Pembuangan Produk Rusak";
                else if (Request.QueryString["do"] == "restock")
                    LabelJudul.Text = "Restock Produk";
                else if (Request.QueryString["do"] == "return")
                    LabelJudul.Text = "Retur ke Tempat Produksi";
                else
                    Response.Redirect("/WITAdministrator/Login.aspx?do=logout&returnUrl=" + Request.RawUrl);

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    Tempat_Class ClassTempat = new Tempat_Class(db);
                    AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                    Warna_Class ClassWarna = new Warna_Class(db);
                    KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                    PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                    DropDownListVarian.Items.AddRange(ClassAtributProduk.Dropdownlist());
                    DropDownListKategori.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));
                }
            }
            else
                Response.Redirect("/WITAdministrator/Login.aspx?do=logout&returnUrl=" + Request.RawUrl);
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Laporan_Class Laporan_Class = new Laporan_Class(db, Pengguna, DateTime.Now, DateTime.Now, false);
            Result = Laporan_Class.StokProduk_Class(Request.QueryString["do"], Pengguna.IDTempat, 0, TextBoxProduk.Text, 0, 0, DropDownListKategori.SelectedValue.ToInt(), string.Empty, DropDownListVarian.SelectedValue.ToInt(), string.Empty, TextBoxQuantity.Text);

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();
        }
    }
    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            bool StatusPerubahan = false;

            foreach (RepeaterItem item in RepeaterLaporan.Items)
            {
                Repeater RepeaterKombinasiProduk = (Repeater)item.FindControl("RepeaterKombinasiProduk");

                foreach (RepeaterItem item2 in RepeaterKombinasiProduk.Items)
                {
                    Label LabelIDStokProduk = (Label)item2.FindControl("LabelIDStokProduk");
                    TextBox TextBoxStokTerbaru = (TextBox)item2.FindControl("TextBoxStokTerbaru");

                    if (!string.IsNullOrWhiteSpace(TextBoxStokTerbaru.Text))
                    {
                        StatusPerubahan = true;
                        StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                        var StokProduk = StokProduk_Class.Cari(LabelIDStokProduk.Text.ToInt());

                        if (Request.QueryString["do"] == "opname")
                            StokProduk_Class.Penyesuaian(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, TextBoxStokTerbaru.Text.ToDecimal().ToInt(), "");
                        else if (Request.QueryString["do"] == "waste")
                            StokProduk_Class.BertambahBerkurang(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, TextBoxStokTerbaru.Text.ToDecimal().ToInt(), StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.PembuanganBarangRusak, "");
                        else if (Request.QueryString["do"] == "restock")
                            StokProduk_Class.BertambahBerkurang(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, TextBoxStokTerbaru.Text.ToDecimal().ToInt(), StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.RestockBarang, "");
                        else if (Request.QueryString["do"] == "return")
                            StokProduk_Class.BertambahBerkurang(Pengguna.IDTempat, Pengguna.IDPengguna, StokProduk, TextBoxStokTerbaru.Text.ToDecimal().ToInt(), StokProduk.HargaBeli.Value, StokProduk.HargaJual.Value, EnumJenisPerpindahanStok.ReturKeTempatProduksi, "");
                    }

                    TextBoxStokTerbaru.Text = string.Empty;
                }
            }

            if (StatusPerubahan)
                db.SubmitChanges();
        }

        LoadData();
    }
}