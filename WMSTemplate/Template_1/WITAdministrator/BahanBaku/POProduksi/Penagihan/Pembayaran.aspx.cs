using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_POProduksi_Penagihan_Pembayaran : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiBahanBakuPenagihan poProduksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.FirstOrDefault(item => item.IDPOProduksiBahanBakuPenagihan == Request.QueryString["id"]);

                TextBoxIDPOProduksiBahanBakuPenagihan.Text = poProduksiBahanBakuPenagihan.IDPOProduksiBahanBakuPenagihan;
                TextBoxPegawai.Text = poProduksiBahanBakuPenagihan.TBPengguna.NamaLengkap;
                TextBoxTanggal.Text = Pengaturan.FormatTanggalRingkas(poProduksiBahanBakuPenagihan.Tanggal);
                TextBoxSupplier.Text = poProduksiBahanBakuPenagihan.TBSupplier.Nama;
                TextBoxKeterangan.Text = poProduksiBahanBakuPenagihan.Keterangan;

                RepeaterDetail.DataSource = poProduksiBahanBakuPenagihan.TBPenerimaanPOProduksiBahanBakus.Select(item => new
                {
                    item.IDPenerimaanPOProduksiBahanBaku,
                    item.TanggalTerima,
                    item.Grandtotal
                });
                RepeaterDetail.DataBind();
                LabelTotalPenerimaan.Text = poProduksiBahanBakuPenagihan.TotalPenerimaan.ToFormatHarga();

                RepeaterRetur.DataSource = poProduksiBahanBakuPenagihan.TBPOProduksiBahanBakuReturs.Select(item => new
                {
                    item.IDPOProduksiBahanBakuRetur,
                    item.TanggalRetur,
                    item.Grandtotal
                });
                RepeaterRetur.DataBind();
                LabelTotalRetur.Text = poProduksiBahanBakuPenagihan.TotalRetur.ToFormatHarga();

                RepeaterDownPayment.DataSource = poProduksiBahanBakuPenagihan.TBPenerimaanPOProduksiBahanBakus.Select(item => item.TBPOProduksiBahanBaku).Distinct().Where(item => item.IDPOProduksiBahanBakuPenagihan == poProduksiBahanBakuPenagihan.IDPOProduksiBahanBakuPenagihan).Select(item => new
                {
                    item.IDPOProduksiBahanBaku,
                    item.TanggalDownPayment,
                    item.DownPayment
                });
                RepeaterDownPayment.DataBind();
                LabelTotalDownPayment.Text = poProduksiBahanBakuPenagihan.TotalDownPayment.ToFormatHarga();

                RepeaterPembayaran.DataSource = poProduksiBahanBakuPenagihan.TBPOProduksiBahanBakuPenagihanDetails.Select(item => new
                {
                    Pegawai = item.TBPengguna.NamaLengkap,
                    item.Tanggal,
                    JenisPembayaran = item.TBJenisPembayaran.Nama,
                    item.Bayar
                });
                RepeaterPembayaran.DataBind();
                LabelTotalBayar.Text = poProduksiBahanBakuPenagihan.TotalBayar.ToFormatHarga();

                TextBoxTotalSisaTagihan.Text = (poProduksiBahanBakuPenagihan.TotalTagihan - poProduksiBahanBakuPenagihan.TotalBayar).ToFormatHarga();
                TextBoxTanggalBayar.Text = DateTime.Now.ToString("d MMMM yyyy");
                DropDownListJenisPembayaran.DataSource = db.TBJenisPembayarans.Where(item => item.IDJenisPembayaran != 2).Select(item => new { item.IDJenisPembayaran, item.Nama });
                DropDownListJenisPembayaran.DataTextField = "Nama";
                DropDownListJenisPembayaran.DataValueField = "IDJenisPembayaran";
                DropDownListJenisPembayaran.DataBind();
				
				//ButtonSimpan.OnClientClick = "window.open('Cetak.aspx?id=" + Request.QueryString["id"] + "', 'Cetak');";
            }
        }
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            peringatan.Visible = false;
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            bool statusBerhasil = false;

            try
            {
                if (TextBoxTotalSisaTagihan.Text.ToDecimal() >= TextBoxBayar.Text.ToDecimal())
                {
                    TBPOProduksiBahanBakuPenagihan produksiBahanBakuPenagihan = db.TBPOProduksiBahanBakuPenagihans.FirstOrDefault(item => item.IDPOProduksiBahanBakuPenagihan == Request.QueryString["id"]);

                    produksiBahanBakuPenagihan.TBPOProduksiBahanBakuPenagihanDetails.Add(new TBPOProduksiBahanBakuPenagihanDetail
                    {
                        IDPengguna = pengguna.IDPengguna,
                        Tanggal = TextBoxTanggalBayar.Text.ToDateTime(),
                        IDJenisPembayaran = DropDownListJenisPembayaran.SelectedValue.ToInt(),
                        Bayar = TextBoxBayar.Text.ToDecimal()
                    });
                    produksiBahanBakuPenagihan.TotalBayar = produksiBahanBakuPenagihan.TBPOProduksiBahanBakuPenagihanDetails.Sum(item => item.Bayar);
                    produksiBahanBakuPenagihan.StatusPembayaran = produksiBahanBakuPenagihan.TotalTagihan == produksiBahanBakuPenagihan.TotalBayar ? true : false;
                    produksiBahanBakuPenagihan.Keterangan = TextBoxKeterangan.Text;

                    foreach (var item in produksiBahanBakuPenagihan.TBPOProduksiBahanBakuReturs)
                    {
                        item.EnumStatusRetur = (int)EnumStatusPORetur.Selesai;
                    }


                    #region Arie, Input Jurnal Pembayaran Hutang PO
                    //var KonfigurasiAkun = db.TBKonfigurasiAkuns.Where(item => item.IDTempat == 1);
                    //TBJurnal Jurnal = new TBJurnal
                    //{
                    //    IDTempat = 1,
                    //    Tanggal = TextBoxTanggalBayar.Text.ToDateTime(),
                    //    Keterangan = TextBoxKeterangan.Text,
                    //    IDPengguna = pengguna.IDPengguna,
                    //    Referensi = produksiBahanBakuPenagihan.IDPOProduksiBahanBakuPenagihan
                    //};

                    //#region JURNAL
                    ////DEBIT     : PERSEDIAAN
                    ////KREDIT    : HUTANG DAGANG
                    ////KAS
                    //Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                    //{
                    //    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == produksiBahanBakuPenagihan.IDJenisPembayaran.ToString()).IDAkun,
                    //    Debit = 0,
                    //    Kredit = produksiBahanBakuPenagihan.Grandtotal
                    //});
                    ////HUTANG DAGANG
                    //Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                    //{
                    //    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG DAGANG").IDAkun,
                    //    Debit = produksiBahanBakuPenagihan.Grandtotal,
                    //    Kredit = 0
                    //});
                    //db.TBJurnals.InsertOnSubmit(Jurnal);
                    //#endregion
                    #endregion

                    db.SubmitChanges();

                    statusBerhasil = true;
                }
                else
                {
                    LabelPeringatan.Text = "Total Bayar harus lebih kecil dari sisa tagihan";
                    peringatan.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogError_Class LogError = new LogError_Class(ex, "Bayar Penagihan (ButtonSimpan_Click by : " + pengguna.NamaLengkap + ")");
            }
            finally
            {
                if (statusBerhasil == true)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}