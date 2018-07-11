using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Kirim_Default : System.Web.UI.Page
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
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var ListTransferProduk = db.TBTransferProduks
                .Where(item =>
                    item.IDTempatPengirim == Pengguna.IDTempat &&
                    item.TanggalKirim.Month == DropDownListCariBulan.SelectedValue.ToInt() &&
                    item.TanggalKirim.Year == DropDownListCariTahun.SelectedValue.ToInt() &&
                    (item.EnumJenisTransfer != (int)PilihanJenisTransfer.PermintaanProses ||
                    item.EnumJenisTransfer != (int)PilihanJenisTransfer.PermintaanPending ||
                    item.EnumJenisTransfer != (int)PilihanJenisTransfer.PermintaanSelesai ||
                    item.EnumJenisTransfer != (int)PilihanJenisTransfer.PermintaanBatal))
                .OrderByDescending(item => item.TanggalKirim)
                .ToArray();

            RepeaterTransferProses.DataSource = ListTransferProduk
                .Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferPending || item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses);
            RepeaterTransferProses.DataBind();

            RepeaterTransferSelesai.DataSource = ListTransferProduk
                .Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferSelesai);
            RepeaterTransferSelesai.DataBind();

            RepeaterTransferBatal.DataSource = ListTransferProduk
                .Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferBatal);
            RepeaterTransferBatal.DataBind();
        }
    }

    protected void DropDownListCari_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Batal")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var TransferProduk = db.TBTransferProduks.FirstOrDefault(item => item.IDTransferProduk == e.CommandArgument.ToString());

                if (TransferProduk != null)
                {
                    StokProduk_Class StokProduk_Class = new StokProduk_Class(db);

                    foreach (var item in TransferProduk.TBTransferProdukDetails.ToArray())
                        StokProduk_Class.BertambahBerkurang(Pengguna.IDTempat, Pengguna.IDPengguna, item.IDKombinasiProduk, item.Jumlah, item.HargaBeli, item.HargaJual, EnumJenisPerpindahanStok.TransferBatal, "Transfer Batal #" + TransferProduk.IDTransferProduk);

                    TransferProduk.EnumJenisTransfer = (int)PilihanJenisTransfer.TransferBatal;

                    db.SubmitChanges();
                }
            }

            LoadData();
        }
        else if (e.CommandName == "TransferUlang")
            Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument);
    }
}