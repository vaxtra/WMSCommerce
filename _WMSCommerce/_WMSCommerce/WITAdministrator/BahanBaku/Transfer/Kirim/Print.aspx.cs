using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Transfer_Kirim_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBTransferBahanBaku DataTransferBahanBaku = db.TBTransferBahanBakus
                    .FirstOrDefault(item => item.IDTransferBahanBaku == Request.QueryString["id"]);

                if (DataTransferBahanBaku != null)
                {
                    LabelIDTransferBahanBaku.Text = DataTransferBahanBaku.IDTransferBahanBaku;

                    //PRINT
                    LabelPrintPengguna.Text = Pengguna.NamaLengkap;
                    LabelPrintTempat.Text = Pengguna.Tempat;
                    LabelPrintTanggal.Text = DateTime.Now.ToFormatTanggal();

                    //PENGIRIM
                    LabelPengirimTempat.Text = Pengguna.Store + " - " + DataTransferBahanBaku.TBTempat.Nama;
                    LabelPengirimTempat1.Text = LabelPengirimTempat.Text;
                    LabelPengirimanTanggal.Text = DataTransferBahanBaku.TanggalKirim.ToFormatTanggal();
                    LabelPengirimanPengguna.Text = DataTransferBahanBaku.TBPengguna.NamaLengkap;
                    LabelPengirimanPengguna1.Text = LabelPengirimanPengguna.Text;
                    LabelPengirimAlamat.Text = DataTransferBahanBaku.TBTempat.Alamat;
                    LabelPengirimTelepon.Text = DataTransferBahanBaku.TBTempat.Telepon1 + " - " + DataTransferBahanBaku.TBTempat.Telepon2;
                    LabelPengirimEmail.Text = DataTransferBahanBaku.TBTempat.Email;

                    LabelKeterangan.Text = DataTransferBahanBaku.Keterangan;

                    //PENERIMA
                    LabelPenerimaTempat.Text = Pengguna.Store + " - " + DataTransferBahanBaku.TBTempat1.Nama;
                    LabelPenerimaTanggal.Text = DataTransferBahanBaku.TanggalTerima.ToFormatTanggal();
                    LabelPenerimaPengguna.Text = DataTransferBahanBaku.IDPenerima.HasValue ? DataTransferBahanBaku.TBPengguna1.NamaLengkap : "";
                    LabelPenerimaPengguna1.Text = LabelPenerimaPengguna.Text;
                    LabelPenerimaAlamat.Text = DataTransferBahanBaku.TBTempat1.Alamat;
                    LabelPenerimaTelepon.Text = DataTransferBahanBaku.TBTempat1.Telepon1 + " - " + DataTransferBahanBaku.TBTempat1.Telepon2;
                    LabelPenerimaEmail.Text = DataTransferBahanBaku.TBTempat1.Email;

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

                    LabelGrandTotal.Text = DataTransferBahanBaku.GrandTotal.ToFormatHarga();

                    LabelGrandTotal1.Text = LabelGrandTotal.Text;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}