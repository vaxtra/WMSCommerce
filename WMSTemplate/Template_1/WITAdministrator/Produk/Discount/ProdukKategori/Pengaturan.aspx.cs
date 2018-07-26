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
                    GrupPelanggan_Class GrupPelanggan_Class = new GrupPelanggan_Class(db);

                    var PelangganGrup = GrupPelanggan_Class.Cari(db, Request.QueryString["id"].ToInt());

                    if (PelangganGrup != null)
                    {
                        ProdukKategori_Class ClassProdukKategori = new ProdukKategori_Class(db);
                        DiscountProdukKategori_Class ClassDiscountGrupPelanggan = new DiscountProdukKategori_Class(db);

                        LabelGrupPelanggan.Text = PelangganGrup.Nama;

                        RepeaterProdukKategori.DataSource = ClassProdukKategori.Data();
                        RepeaterProdukKategori.DataBind();

                        var DataDiscount = ClassDiscountGrupPelanggan.Data(Request.QueryString["id"].ToInt());

                        foreach (RepeaterItem item in RepeaterProdukKategori.Items)
                        {
                            var HiddenFieldIDProdukKategori = (HiddenField)item.FindControl("HiddenFieldIDProdukKategori");
                            var TextBoxDiscount = (TextBox)item.FindControl("TextBoxDiscount");

                            var Data = DataDiscount.FirstOrDefault(item2 => item2.IDProdukKategori == HiddenFieldIDProdukKategori.Value.ToInt());

                            if (Data != null)
                                TextBoxDiscount.Text = Data.Discount.ToString();
                            else
                                TextBoxDiscount.Text = "0";
                        }
                    }
                    else
                        Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DiscountProdukKategori_Class ClassDiscountProdukKategori = new DiscountProdukKategori_Class(db);

                ClassDiscountProdukKategori.Hapus(Request.QueryString["id"].ToInt());

                db.SubmitChanges();

                foreach (RepeaterItem item in RepeaterProdukKategori.Items)
                {
                    var HiddenFieldIDProdukKategori = (HiddenField)item.FindControl("HiddenFieldIDProdukKategori");
                    var TextBoxDiscount = (TextBox)item.FindControl("TextBoxDiscount");

                    ClassDiscountProdukKategori.Tambah(Request.QueryString["id"].ToInt(), HiddenFieldIDProdukKategori.Value.ToInt(), TextBoxDiscount.Text.ToDecimal());
                }

                Notifikasi_Class Notifikasi_Class = new Notifikasi_Class(db, Pengguna.IDPengguna, EnumAlert.success, "Discount Grup Pelanggan " + LabelGrupPelanggan.Text + " Berhasil Disimpan");

                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
}