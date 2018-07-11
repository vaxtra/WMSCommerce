using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_POProduksi_Penagihan_Pembayaran : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPOProduksiProdukPenagihan poProduksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == Request.QueryString["id"]);

                TextBoxIDPOProduksiProdukPenagihan.Text = poProduksiProdukPenagihan.IDPOProduksiProdukPenagihan;
                TextBoxPegawai.Text = poProduksiProdukPenagihan.TBPengguna.NamaLengkap;
                TextBoxTanggal.Text = Pengaturan.FormatTanggalRingkas(poProduksiProdukPenagihan.Tanggal);
                TextBoxVendor.Text = poProduksiProdukPenagihan.TBVendor.Nama;
                TextBoxKeterangan.Text = poProduksiProdukPenagihan.Keterangan;

                RepeaterDetail.DataSource = poProduksiProdukPenagihan.TBPenerimaanPOProduksiProduks.Select(item => new
                {
                    item.IDPenerimaanPOProduksiProduk,
                    item.TanggalTerima,
                    item.Grandtotal
                });
                RepeaterDetail.DataBind();
                LabelTotalPenerimaan.Text = poProduksiProdukPenagihan.TotalPenerimaan.ToFormatHarga();

                RepeaterRetur.DataSource = poProduksiProdukPenagihan.TBPOProduksiProdukReturs.Select(item => new
                {
                    item.IDPOProduksiProdukRetur,
                    item.TanggalRetur,
                    item.Grandtotal
                });
                RepeaterRetur.DataBind();
                LabelTotalRetur.Text = poProduksiProdukPenagihan.TotalRetur.ToFormatHarga();

                RepeaterDownPayment.DataSource = poProduksiProdukPenagihan.TBPenerimaanPOProduksiProduks.Select(item => item.TBPOProduksiProduk).Distinct().Where(item => item.IDPOProduksiProdukPenagihan == poProduksiProdukPenagihan.IDPOProduksiProdukPenagihan).Select(item => new
                {
                    item.IDPOProduksiProduk,
                    item.TanggalDownPayment,
                    item.DownPayment
                });
                RepeaterDownPayment.DataBind();
                LabelTotalDownPayment.Text = poProduksiProdukPenagihan.TotalDownPayment.ToFormatHarga();

                RepeaterPembayaran.DataSource = poProduksiProdukPenagihan.TBPOProduksiProdukPenagihanDetails.Select(item => new
                {
                    Pegawai = item.TBPengguna.NamaLengkap,
                    item.Tanggal,
                    JenisPembayaran = item.TBJenisPembayaran.Nama,
                    item.Bayar
                });
                RepeaterPembayaran.DataBind();
                LabelTotalBayar.Text = poProduksiProdukPenagihan.TotalBayar.ToFormatHarga();

                TextBoxTotalSisaTagihan.Text = (poProduksiProdukPenagihan.TotalTagihan - poProduksiProdukPenagihan.TotalBayar).ToFormatHarga();
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
                    TBPOProduksiProdukPenagihan produksiProdukPenagihan = db.TBPOProduksiProdukPenagihans.FirstOrDefault(item => item.IDPOProduksiProdukPenagihan == Request.QueryString["id"]);

                    produksiProdukPenagihan.TBPOProduksiProdukPenagihanDetails.Add(new TBPOProduksiProdukPenagihanDetail
                    {
                        IDPengguna = pengguna.IDPengguna,
                        Tanggal = TextBoxTanggalBayar.Text.ToDateTime(),
                        IDJenisPembayaran = DropDownListJenisPembayaran.SelectedValue.ToInt(),
                        Bayar = TextBoxBayar.Text.ToDecimal()
                    });
                    produksiProdukPenagihan.TotalBayar = produksiProdukPenagihan.TBPOProduksiProdukPenagihanDetails.Sum(item => item.Bayar);
                    produksiProdukPenagihan.StatusPembayaran = produksiProdukPenagihan.TotalTagihan == produksiProdukPenagihan.TotalBayar ? true : false;
                    produksiProdukPenagihan.Keterangan = TextBoxKeterangan.Text;

                    foreach (var item in produksiProdukPenagihan.TBPOProduksiProdukReturs)
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
                    //    Referensi = produksiProdukPenagihan.IDPOProduksiProdukPenagihan
                    //};

                    //#region JURNAL
                    ////DEBIT     : PERSEDIAAN
                    ////KREDIT    : HUTANG DAGANG
                    ////KAS
                    //Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                    //{
                    //    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == produksiProdukPenagihan.IDJenisPembayaran.ToString()).IDAkun,
                    //    Debit = 0,
                    //    Kredit = produksiProdukPenagihan.Grandtotal
                    //});
                    ////HUTANG DAGANG
                    //Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                    //{
                    //    IDAkun = KonfigurasiAkun.FirstOrDefault(item => item.Nama == "HUTANG DAGANG").IDAkun,
                    //    Debit = produksiProdukPenagihan.Grandtotal,
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