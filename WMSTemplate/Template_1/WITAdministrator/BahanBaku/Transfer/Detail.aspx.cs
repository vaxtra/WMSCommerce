using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Transfer_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataTransferBahanBaku = db.TBTransferBahanBakus
                    .FirstOrDefault(item => item.IDTransferBahanBaku == Request.QueryString["id"]);

                if (DataTransferBahanBaku != null)
                {
                    LabelIDTransfer.Text = DataTransferBahanBaku.IDTransferBahanBaku;
                    ButtonPrint.OnClientClick = "return popitup('Print.aspx?id=" + DataTransferBahanBaku.IDTransferBahanBaku + "')";

                    LabelPengirimLokasi.Text = DataTransferBahanBaku.TBTempat.Nama;
                    LabelPengirimTanggal.Text = DataTransferBahanBaku.TanggalKirim.ToFormatTanggal();
                    LabelPengirimPengguna.Text = DataTransferBahanBaku.TBPengguna.NamaLengkap;

                    LabelPenerimaLokasi.Text = DataTransferBahanBaku.TBTempat1.Nama;
                    LabelPenerimaTanggal.Text = DataTransferBahanBaku.TanggalTerima.ToFormatTanggal();
                    LabelPenerimaPengguna.Text = DataTransferBahanBaku.IDPenerima.HasValue ? DataTransferBahanBaku.TBPengguna1.NamaLengkap : "";

                    LabelStatusTransfer.Text = Pengaturan.JenisTransferHTML(DataTransferBahanBaku.EnumJenisTransfer);

                    RepeaterTransferBahanBaku.DataSource = DataTransferBahanBaku.TBTransferBahanBakuDetails
                        .Select(item => new
                        {
                            BahanBaku = item.TBBahanBaku.Nama,
                            SatuanBesar = item.TBSatuan.Nama,
                            Kategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.Count > 0 ? item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.FirstOrDefault().TBKategoriBahanBaku.Nama : "",
                            KodeBahanBaku = item.TBBahanBaku.KodeBahanBaku,
                            HargaBeli = item.HargaBeli,
                            Jumlah = item.Jumlah,
                            Subtotal = item.Subtotal
                        });
                    RepeaterTransferBahanBaku.DataBind();

                    LabelTotalNominal.Text = DataTransferBahanBaku.GrandTotal.ToFormatHarga();

                    linkDownload.HRef = "~/Files/Transfer Bahan Baku/Transfer/" + DataTransferBahanBaku.TBTempat.Nama + " " + DataTransferBahanBaku.IDTransferBahanBaku + " " + (DataTransferBahanBaku.TanggalKirim).ToString("d MMMM yyyy HH.mm") + ".WIT_enc.zip";

                    if (DataTransferBahanBaku.IDTempatPenerima == Pengguna.IDTempat)
                        linkKembali.HRef = "/WITWarehouse/BahanBaku/Penerimaan/Default.aspx";
                    else
                        linkKembali.HRef = "Default.aspx";

                    if ((PilihanJenisTransfer)DataTransferBahanBaku.EnumJenisTransfer == PilihanJenisTransfer.TransferBatal ||
                        (PilihanJenisTransfer)DataTransferBahanBaku.EnumJenisTransfer == PilihanJenisTransfer.TransferPending)
                    {
                        ButtonPrint.Visible = false;
                        linkDownload.Visible = false;
                    }
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}