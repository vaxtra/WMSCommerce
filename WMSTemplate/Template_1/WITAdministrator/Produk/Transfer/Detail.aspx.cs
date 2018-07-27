using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Transfer_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataTransferProduk = db.TBTransferProduks
                    .FirstOrDefault(item => item.IDTransferProduk == Request.QueryString["id"]);

                if (DataTransferProduk != null)
                {
                    ButtonPrint.OnClientClick = "return popitup('Print.aspx?id=" + DataTransferProduk.IDTransferProduk + "')";

                    LabelIDTransfer.Text = DataTransferProduk.IDTransferProduk;
                    LabelStatusTransfer.Text = Pengaturan.JenisTransferHTML(DataTransferProduk.EnumJenisTransfer);

                    LabelPengirimLokasi.Text = DataTransferProduk.TBTempat.Nama;
                    LabelPengirimTanggal.Text = DataTransferProduk.TanggalKirim.ToFormatTanggal();
                    LabelPengirimPengguna.Text = DataTransferProduk.TBPengguna.NamaLengkap;

                    LabelPenerimaLokasi.Text = DataTransferProduk.TBTempat1.Nama;
                    LabelPenerimaTanggal.Text = DataTransferProduk.TanggalTerima.ToFormatTanggal();
                    LabelPenerimaPengguna.Text = DataTransferProduk.IDPenerima.HasValue ? DataTransferProduk.TBPengguna1.NamaLengkap : "";

                    RepeaterTransferKombinasiProduk.DataSource = DataTransferProduk.TBTransferProdukDetails
                        .GroupBy(item => item.TBKombinasiProduk.TBProduk)
                        .Select(item => new
                        {
                            Produk = item.Key.Nama,
                            Kategori = item.Key.TBRelasiProdukKategoriProduks.Count > 0 ? item.Key.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            Count = item.Count(),
                            Body = item.Select(item2 => new
                            {
                                item2.IDTransferProdukDetail,
                                Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                                AtributProduk = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                                item2.HargaJual,
                                item2.Jumlah,
                                item2.SubtotalHargaJual
                            }),
                            Jumlah = item.Sum(item2 => item2.Jumlah),
                            SubtotalHargaJual = item.Sum(item2 => item2.SubtotalHargaJual)
                        })
                        .ToArray();
                    RepeaterTransferKombinasiProduk.DataBind();

                    LabelTotalJumlah.Text = DataTransferProduk.TotalJumlah.ToFormatHargaBulat();
                    LabelTotalNominal.Text = DataTransferProduk.GrandTotalHargaJual.ToFormatHarga();

                    linkDownload.HRef = "~/Files/Transfer Produk/Transfer/" + DataTransferProduk.TBTempat.Nama + " " + DataTransferProduk.IDTransferProduk + " " + (DataTransferProduk.TanggalKirim).ToString("d MMMM yyyy HH.mm") + ".WIT_enc.zip";

                    if (DataTransferProduk.IDTempatPenerima == Pengguna.IDTempat)
                        linkKembali.HRef = "/WITWarehouse/Produk/Penerimaan/Default.aspx";
                    else
                        linkKembali.HRef = "Default.aspx";

                    if ((PilihanJenisTransfer)DataTransferProduk.EnumJenisTransfer == PilihanJenisTransfer.TransferBatal ||
                        (PilihanJenisTransfer)DataTransferProduk.EnumJenisTransfer == PilihanJenisTransfer.TransferPending)
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