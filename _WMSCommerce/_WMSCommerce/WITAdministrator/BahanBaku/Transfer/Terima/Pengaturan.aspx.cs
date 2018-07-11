using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Transfer_Terima_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                var transferBahanBaku = db.TBTransferBahanBakus.FirstOrDefault(item => item.IDTransferBahanBaku == Request.QueryString["id"]);

                if (transferBahanBaku != null && transferBahanBaku.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses)
                {
                    LabelIDTransferBahanBaku.Text = transferBahanBaku.IDTransferBahanBaku;

                    //TEMPAT PENGIRIM
                    DropDownListTempatPengirim.Items.Insert(0, new ListItem
                    {
                        Text = transferBahanBaku.TBTempat.Nama,
                        Selected = true
                    });

                    //TEMPAT PENERIMA
                    DropDownListTempatPenerima.Items.Insert(0, new ListItem
                    {
                        Text = transferBahanBaku.TBTempat1.Nama,
                        Selected = true
                    });

                    //PENGGUNA PENGIRIM
                    DropDownListPenggunaPengirim.Items.Insert(0, new ListItem
                    {
                        Text = transferBahanBaku.TBPengguna.NamaLengkap,
                        Selected = true
                    });

                    TextBoxTanggalKirim.Text = transferBahanBaku.TanggalKirim.ToFormatTanggal();
                    TextBoxKeterangan.Text = transferBahanBaku.Keterangan;

                    RepeaterTransferBahanBaku.DataSource = transferBahanBaku.TBTransferBahanBakuDetails
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

                    LabelTotalNominal.Text = transferBahanBaku.GrandTotal.ToFormatHarga();
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }

    protected void ButtonTerima_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var TransferBahanBaku = db.TBTransferBahanBakus.FirstOrDefault(item => item.IDTransferBahanBaku == Request.QueryString["id"]);

                if (TransferBahanBaku != null && TransferBahanBaku.EnumJenisTransfer == (int)PilihanJenisTransfer.TransferProses)
                {
                    TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == Pengguna.IDTempat).ToArray();
                    foreach (var item in TransferBahanBaku.TBTransferBahanBakuDetails.ToArray())
                    {
                        var stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku);

                        if (stokBahanBaku != null)
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, item.Jumlah, item.HargaBeli, true, EnumJenisPerpindahanStok.TransferStokMasuk, "Transfer #" + LabelIDTransferBahanBaku.Text);
                        else
                        {
                            stokBahanBaku = StokBahanBaku_Class.InsertStokBahanBaku(db, DateTime.Now, Pengguna.IDPengguna, Pengguna.IDTempat, db.TBBahanBakus.FirstOrDefault(data => data.IDBahanBaku == item.IDBahanBaku), (item.HargaBeli / item.TBBahanBaku.Konversi.Value), 0, 0, "");

                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, item.Jumlah, item.HargaBeli, true, EnumJenisPerpindahanStok.TransferStokMasuk, "Transfer #" + LabelIDTransferBahanBaku.Text);
                        }
                    }

                    TransferBahanBaku.IDPenerima = Pengguna.IDPengguna;
                    TransferBahanBaku.TanggalTerima = DateTime.Now;
                    TransferBahanBaku.TanggalUpdate = DateTime.Now;
                    TransferBahanBaku.EnumJenisTransfer = (int)PilihanJenisTransfer.TransferSelesai;

                    db.SubmitChanges();

                    Response.Redirect("/WITWarehouse/BahanBaku/Transfer/Detail.aspx?id=" + TransferBahanBaku.IDTransferBahanBaku, false);
                }
                else
                    Response.Redirect("Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
            LogError_Class LogError_Class = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
}