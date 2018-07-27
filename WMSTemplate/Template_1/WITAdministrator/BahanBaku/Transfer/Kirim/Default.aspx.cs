using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Transfer_Kirim_Default : System.Web.UI.Page
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
            var ListTransferBahanBaku = db.TBTransferBahanBakus
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

            RepeaterTransferProses.DataSource = ListTransferBahanBaku
                .Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferPending || item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses);
            RepeaterTransferProses.DataBind();

            RepeaterTransferSelesai.DataSource = ListTransferBahanBaku
                .Where(item => item.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferSelesai);
            RepeaterTransferSelesai.DataBind();

            RepeaterTransferBatal.DataSource = ListTransferBahanBaku
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
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var TransferBahanBaku = db.TBTransferBahanBakus.FirstOrDefault(item => item.IDTransferBahanBaku == e.CommandArgument.ToString());

                if (TransferBahanBaku != null)
                {
                    TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == pengguna.IDTempat).ToArray();

                    foreach (var item in TransferBahanBaku.TBTransferBahanBakuDetails.ToArray())
                        StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, pengguna.IDPengguna, daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku), item.Jumlah, item.HargaBeli, true, EnumJenisPerpindahanStok.TransferBatal, "Transfer Batal #" + TransferBahanBaku.IDTransferBahanBaku);

                    TransferBahanBaku.EnumJenisTransfer = (int)PilihanJenisTransfer.TransferBatal;

                    db.SubmitChanges();
                }
            }

            LoadData();
        }
        else if (e.CommandName == "TransferUlang")
            Response.Redirect("Pengaturan.aspx?id=" + e.CommandArgument);
    }
}