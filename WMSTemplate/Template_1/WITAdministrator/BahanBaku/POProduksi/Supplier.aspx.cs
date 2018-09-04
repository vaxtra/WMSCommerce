using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Supplier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataSupplier(db);
            }
        }
    }
    #region SUPPLIER
    private void LoadDataSupplier(DataClassesDatabaseDataContext db)
    {
        RepeaterSupplier.DataSource = db.TBSuppliers.OrderBy(item => item.Nama).ToArray();
        RepeaterSupplier.DataBind();
    }
    protected void RepeaterSupplier_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "Ubah")
            {

                TBSupplier supplier = db.TBSuppliers.FirstOrDefault(item => item.IDSupplier == e.CommandArgument.ToInt());

                HiddenFieldIDSupplier.Value = supplier.IDSupplier.ToString();
                TextBoxNama.Text = supplier.Nama;
                TextBoxAlamat.Text = supplier.Alamat;
                TextBoxEmail.Text = supplier.Email;
                TextBoxTelepon1.Text = supplier.Telepon1;
                TextBoxTelepon2.Text = supplier.Telepon2;
                TextBoxTax.Text = (supplier.PersentaseTax * 100).ToString();

                ButtonSimpanSupplier.Text = "Ubah";
            }
            else if (e.CommandName == "Hapus")
            {
                Supplier_Class.DeleteSupplier(db, e.CommandArgument.ToInt());
                db.SubmitChanges();
                LoadDataSupplier(db);
            }
        }
    }

    protected void ButtonSimpanSupplier_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBSupplier supplier = null;

                if (ButtonSimpanSupplier.Text == "Tambah")
                {
                    supplier = new TBSupplier
                    {
                        Nama = TextBoxNama.Text,
                        Alamat = TextBoxAlamat.Text,
                        Email = TextBoxEmail.Text,
                        Telepon1 = TextBoxTelepon1.Text,
                        Telepon2 = TextBoxTelepon2.Text,
                        PersentaseTax = TextBoxTax.Text.ToDecimal() / 100
                    };
                    db.TBSuppliers.InsertOnSubmit(supplier);
                }
                else if (ButtonSimpanSupplier.Text == "Ubah")
                {
                    supplier = db.TBSuppliers.FirstOrDefault(item => item.IDSupplier == HiddenFieldIDSupplier.Value.ToInt());
                    supplier.Nama = TextBoxNama.Text;
                    supplier.Alamat = TextBoxAlamat.Text;
                    supplier.Email = TextBoxEmail.Text;
                    supplier.Telepon1 = TextBoxTelepon1.Text;
                    supplier.Telepon2 = TextBoxTelepon2.Text;
                    supplier.PersentaseTax = TextBoxTax.Text.ToDecimal() / 100;
                }

                db.SubmitChanges();

                HiddenFieldIDSupplier.Value = null;
                TextBoxNama.Text = string.Empty;
                TextBoxAlamat.Text = string.Empty;
                TextBoxEmail.Text = string.Empty;
                TextBoxTelepon1.Text = string.Empty;
                TextBoxTelepon2.Text = string.Empty;
                TextBoxTax.Text = "0.00";
                ButtonSimpanSupplier.Text = "Tambah";

                LoadDataSupplier(db);
            }

        }
    }
    #endregion
}