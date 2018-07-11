using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Terima_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownListCariBulan.SelectedValue = DateTime.Now.Month.ToString();
            DropDownListCariTahun.Items.Insert(0, new ListItem { Value = (DateTime.Now.Year - 3).ToString(), Text = (DateTime.Now.Year - 3).ToString() });
            DropDownListCariTahun.Items.Insert(1, new ListItem { Value = (DateTime.Now.Year - 2).ToString(), Text = (DateTime.Now.Year - 2).ToString() });
            DropDownListCariTahun.Items.Insert(2, new ListItem { Value = (DateTime.Now.Year - 1).ToString(), Text = (DateTime.Now.Year - 1).ToString() });
            DropDownListCariTahun.Items.Insert(3, new ListItem { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() });
            DropDownListCariTahun.SelectedValue = DateTime.Now.Year.ToString();

            LoadData();
        }
    }

    private void LoadData()
    {
        PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var DataTransferProduk = db.TBTransferProduks
                .Where(item =>
                    item.IDTempatPenerima == pengguna.IDTempat &&
                    item.TanggalKirim.Month == DropDownListCariBulan.SelectedValue.ToInt() &&
                    item.TanggalKirim.Year == DropDownListCariTahun.SelectedValue.ToInt() &&
                    (item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses ||
                    item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferSelesai))
                .OrderByDescending(item => item.TanggalKirim)
                .ToArray();

            RepeaterTransferProses.DataSource = DataTransferProduk.Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses);
            RepeaterTransferProses.DataBind();

            RepeaterTransferSelesai.DataSource = DataTransferProduk.Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferSelesai);
            RepeaterTransferSelesai.DataBind();
        }
    }
    protected void DropDownListCari_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}