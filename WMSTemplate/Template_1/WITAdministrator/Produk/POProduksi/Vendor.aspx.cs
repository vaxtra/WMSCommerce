using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Vendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataVendor(db);
            }
        }
    }

    #region VENDOR
    private void LoadDataVendor(DataClassesDatabaseDataContext db)
    {
        RepeaterVendor.DataSource = db.TBVendors.OrderBy(item => item.Nama).ToArray();
        RepeaterVendor.DataBind();
    }
    protected void RepeaterVendor_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {

                TBVendor Vendor = db.TBVendors.FirstOrDefault(item => item.IDVendor == e.CommandArgument.ToInt());

                HiddenFieldIDVendor.Value = Vendor.IDVendor.ToString();
                TextBoxNamaVendor.Text = Vendor.Nama;
                TextBoxAlamatVendor.Text = Vendor.Alamat;
                TextBoxEmailVendor.Text = Vendor.Email;
                TextBoxTelepon1Vendor.Text = Vendor.Telepon1;
                TextBoxTelepon2Vendor.Text = Vendor.Telepon2;
                TextBoxTaxVendor.Text = (Vendor.PersentaseTax * 100).ToString();

                ButtonSimpanVendor.Text = "Ubah";
            }

            else if (e.CommandName == "Hapus")
            {
                Vendor_Class vendorClass = new Vendor_Class(db);
                vendorClass.Hapus(e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataVendor(db);
            }
        }
    }

    protected void ButtonSimpanVendor_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBVendor Vendor = null;

                if (ButtonSimpanVendor.Text == "Tambah")
                {
                    Vendor = new TBVendor
                    {
                        Nama = TextBoxNamaVendor.Text,
                        Alamat = TextBoxAlamatVendor.Text,
                        Email = TextBoxEmailVendor.Text,
                        Telepon1 = TextBoxTelepon1Vendor.Text,
                        Telepon2 = TextBoxTelepon2Vendor.Text,
                        PersentaseTax = TextBoxTaxVendor.Text.ToDecimal() / 100
                    };
                    db.TBVendors.InsertOnSubmit(Vendor);
                }
                else if (ButtonSimpanVendor.Text == "Ubah")
                {
                    Vendor = db.TBVendors.FirstOrDefault(item => item.IDVendor == HiddenFieldIDVendor.Value.ToInt());
                    Vendor.Nama = TextBoxNamaVendor.Text;
                    Vendor.Alamat = TextBoxAlamatVendor.Text;
                    Vendor.Email = TextBoxEmailVendor.Text;
                    Vendor.Telepon1 = TextBoxTelepon1Vendor.Text;
                    Vendor.Telepon2 = TextBoxTelepon2Vendor.Text;
                    Vendor.PersentaseTax = TextBoxTaxVendor.Text.ToDecimal() / 100;
                }

                db.SubmitChanges();

                HiddenFieldIDVendor.Value = null;
                TextBoxNamaVendor.Text = string.Empty;
                TextBoxAlamatVendor.Text = string.Empty;
                TextBoxEmailVendor.Text = string.Empty;
                TextBoxTelepon1Vendor.Text = string.Empty;
                TextBoxTelepon2Vendor.Text = string.Empty;
                TextBoxTaxVendor.Text = "0.00";
                ButtonSimpanVendor.Text = "Tambah";

                LoadDataVendor(db);
            }

        }
    }
    #endregion
}