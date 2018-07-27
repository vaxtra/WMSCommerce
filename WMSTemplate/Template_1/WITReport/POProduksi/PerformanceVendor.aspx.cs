using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_POProduksi_PerformanceVendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxTanggalAwal.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAkhir.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                    LabelPeriode.Text = TextBoxTanggalAwal.Text;
                else
                    LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;
            }

            LoadData(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));

            MultiViewPerformance.SetActiveView(ViewGrafik);
        }
    }

    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.HariIni()[0], Pengaturan.HariIni()[1]);
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.MingguIni()[0], Pengaturan.MingguIni()[1]);
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.BulanIni()[0], Pengaturan.BulanIni()[1]);
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.TahunIni()[0], Pengaturan.TahunIni()[1]);
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.HariSebelumnya()[0], Pengaturan.HariSebelumnya()[1]);
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.MingguSebelumnya()[0], Pengaturan.MingguSebelumnya()[1]);
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.BulanSebelumnya()[0], Pengaturan.BulanSebelumnya()[1]);
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        LoadData(Pengaturan.TahunSebelumnya()[0], Pengaturan.TahunSebelumnya()[1]);
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        LoadData(DateTime.Parse(TextBoxTanggalAwal.Text), DateTime.Parse(TextBoxTanggalAkhir.Text));
    }
    private void LoadData(DateTime tanggalAwal, DateTime tanggalAkhir)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwal.Text = tanggalAwal.ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = tanggalAkhir.ToString("d MMMM yyyy");

            if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                LabelPeriode.Text = TextBoxTanggalAwal.Text;
            else
                LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

            var poProduksiProduk = db.TBPOProduksiProduks
                .Where(item => item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date &&
                    item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date &&
                    item.TBPenerimaanPOProduksiProduks.Count > 0)
                .Select(item => new
                {
                    item.IDTempat,
                    item.IDPOProduksiProduk,
                    item.IDVendor,
                    item.TanggalPengiriman,
                    item.EnumJenisProduksi,
                    TanggalPenerimaanPO = item.TBPenerimaanPOProduksiProduks.OrderByDescending(data => data.TanggalTerima).FirstOrDefault().TanggalTerima,
                    TotalDatang = db.TBPenerimaanPOProduksiProdukDetails.Where(data => data.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDPOProduksiProduk == item.IDPOProduksiProduk).Sum(data => data.Datang),
                    JumlahDiTerima = db.TBPenerimaanPOProduksiProdukDetails.Where(data => data.TBPenerimaanPOProduksiProduk.TBPOProduksiProduk.IDPOProduksiProduk == item.IDPOProduksiProduk).Sum(data => data.Diterima + data.TolakKeGudang)
                }).ToArray();

            //Purchase Order
            RepeaterPerformancePurchaseOrder.DataSource = db.TBVendors.AsEnumerable().Where(item => poProduksiProduk.Where(data => data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Any(data => data.IDVendor == item.IDVendor)).Select(item => new
            {
                item.IDVendor,
                Vendor = item.Nama,
                item.Alamat,
                TotalPO = poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiProduk).Distinct().Count(),
                Penerimaan = poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiProduk).Count() == 0 ? -1 :
                            ((decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Sum(data => data.JumlahDiTerima) / (decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Sum(data => data.TotalDatang)) * 100,
                Pengiriman = poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiProduk).Count() == 0 ? -1 :
                            ((decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.TanggalPengiriman.Value.Date >= data.TanggalPenerimaanPO.Value.Date && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiProduk).Count() / (decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiProduk).Count()) * 100
            }).ToArray();
            RepeaterPerformancePurchaseOrder.DataBind();

            //In-House Production
            RepeaterPerformanceInHouseProduction.DataSource = db.TBTempats.AsEnumerable().Where(item => poProduksiProduk.Where(data => data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Any(data => data.IDTempat == item.IDTempat)).Select(item => new
            {
                item.IDTempat,
                Tempat = item.Nama,
                item.Alamat,
                TotalPO = poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiProduk).Distinct().Count(),
                Penerimaan = poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiProduk).Count() == 0 ? -1 :
                            ((decimal)poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Sum(data => data.JumlahDiTerima) / (decimal)poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Sum(data => data.TotalDatang)) * 100,
                Pengiriman = poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiProduk).Count() == 0 ? -1 :
                            ((decimal)poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.TanggalPengiriman.Value.Date >= data.TanggalPenerimaanPO.Value.Date && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiProduk).Count() / (decimal)poProduksiProduk.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiProduk).Count()) * 100
            }).ToArray();
            RepeaterPerformanceInHouseProduction.DataBind();

            //Production to Vendor
            RepeaterPerformanceProductionToVendor.DataSource = db.TBVendors.AsEnumerable().Where(item => poProduksiProduk.Where(data => data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Any(data => data.IDVendor == item.IDVendor)).Select(item => new
            {
                item.IDVendor,
                Vendor = item.Nama,
                item.Alamat,
                TotalPO = poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiProduk).Distinct().Count(),
                Penerimaan = poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiProduk).Count() == 0 ? -1 :
                            ((decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Sum(data => data.JumlahDiTerima) / (decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Sum(data => data.TotalDatang)) * 100,
                Pengiriman = poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiProduk).Count() == 0 ? -1 :
                            ((decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.TanggalPengiriman.Value.Date >= data.TanggalPenerimaanPO.Value.Date && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiProduk).Count() / (decimal)poProduksiProduk.Where(data => data.IDVendor == item.IDVendor && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiProduk).Count()) * 100
            }).ToArray();
            RepeaterPerformanceProductionToVendor.DataBind();
        }
    }

    #region View Purchase Order
    protected void RepeaterPOProduksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewPOProduksi);
        ButtonKembali.Visible = true;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            headPurchaseOrder.Visible = true;
            bodyPurchaseOrder.Visible = true;
            headProduksiSendiri.Visible = true;
            bodyProduksiSendiri.Visible = true;
            headProduksiKeSupplier.Visible = true;
            bodyProduksiKeSupplier.Visible = true;

            HiddenFieldID.Value = e.CommandArgument.ToString();
            if (e.CommandName == "Vendor")
                TextBoxNamaVendorTempat.Text = db.TBVendors.FirstOrDefault(item => item.IDVendor == e.CommandArgument.ToInt()).Nama;
            else
                TextBoxNamaVendorTempat.Text = db.TBTempats.FirstOrDefault(item => item.IDTempat == e.CommandArgument.ToInt()).Nama;


            TBPOProduksiProduk[] daftarPOProduksiProduk = db.TBPOProduksiProduks.Where(item => (e.CommandName == "Vendor" ? item.IDVendor == e.CommandArgument.ToInt() : item.IDTempat == e.CommandArgument.ToInt()) &&
                item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date &&
                item.TBPenerimaanPOProduksiProduks.Count > 0).ToArray();

            if (e.CommandName == "Vendor")
            {
                RepeaterPurchaseOrder.DataSource = daftarPOProduksiProduk.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(item => new
                {
                    item.EnumJenisProduksi,
                    item.IDPOProduksiProduk,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    Tanggal_ClassTerakhirTerima = item.TBPenerimaanPOProduksiProduks.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.Date <= item.TanggalPengiriman.Value.Date ? "fitSize" : "fitSize danger",
                    TanggalTerakhirTerima = item.TBPenerimaanPOProduksiProduks.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
                    TotalTolak = item.TBPenerimaanPOProduksiProduks.Sum(item2 => item2.TotalTolakKeGudang + item2.TotalTolakKeVendor).ToFormatHargaBulat(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterPurchaseOrder.DataBind();

                headProduksiSendiri.Visible = false;
                bodyProduksiSendiri.Visible = false;
                if (RepeaterPurchaseOrder.Items.Count == 0)
                {
                    headPurchaseOrder.Visible = false;
                    bodyPurchaseOrder.Visible = false;
                }
            }

            if (e.CommandName == "Tempat")
            {
                RepeaterProduksiSendiri.DataSource = daftarPOProduksiProduk.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(item => new
                {
                    item.EnumJenisProduksi,
                    item.IDPOProduksiProduk,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    Tanggal_ClassTerakhirTerima = item.TBPenerimaanPOProduksiProduks.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.Date <= item.TanggalPengiriman.Value.Date ? "fitSize" : "fitSize danger",
                    TanggalTerakhirTerima = item.TBPenerimaanPOProduksiProduks.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
                    TotalTolak = item.TBPenerimaanPOProduksiProduks.Sum(item2 => item2.TotalTolakKeGudang + item2.TotalTolakKeVendor).ToFormatHargaBulat(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterProduksiSendiri.DataBind();

                headPurchaseOrder.Visible = false;
                bodyPurchaseOrder.Visible = false;
                headProduksiKeSupplier.Visible = false;
                bodyProduksiKeSupplier.Visible = false;
                if (RepeaterProduksiSendiri.Items.Count == 0)
                {
                    headProduksiSendiri.Visible = false;
                    bodyProduksiSendiri.Visible = false;
                }
            }

            if (e.CommandName == "Vendor")
            {
                RepeaterProduksiKeSupplier.DataSource = daftarPOProduksiProduk.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(item => new
                {
                    item.EnumJenisProduksi,
                    item.IDPOProduksiProduk,
                    Tempat = item.TBTempat.Nama,
                    SupplierVendor = item.IDVendor != null ? item.TBVendor.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    Tanggal_ClassTerakhirTerima = item.TBPenerimaanPOProduksiProduks.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.Date <= item.TanggalPengiriman.Value.Date ? "fitSize" : "fitSize danger",
                    TanggalTerakhirTerima = item.TBPenerimaanPOProduksiProduks.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHargaBulat(),
                    TotalTolak = item.TBPenerimaanPOProduksiProduks.Sum(item2 => item2.TotalTolakKeGudang + item2.TotalTolakKeVendor).ToFormatHargaBulat(),
                    Grandtotal = item.Grandtotal.ToFormatHarga()
                }).OrderBy(item => item.Tanggal).ToArray(); ;
                RepeaterProduksiKeSupplier.DataBind();

                headProduksiSendiri.Visible = false;
                bodyProduksiSendiri.Visible = false;
                if (RepeaterProduksiKeSupplier.Items.Count == 0)
                {
                    headProduksiKeSupplier.Visible = false;
                    bodyProduksiKeSupplier.Visible = false;
                }
            }
        }
    }

    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewGrafik);
        ButtonKembali.Visible = false;
    }
    #endregion

    #region Detail
    protected void RepeaterPOProduksiDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewPOProduksiDetail);

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPOProduksiProduk poProduksiBProduk = db.TBPOProduksiProduks.FirstOrDefault(item => item.IDPOProduksiProduk == e.CommandArgument.ToString());

            TextBoxIDProyeksi.Text = poProduksiBProduk.IDProyeksi != null ? poProduksiBProduk.IDProyeksi : "-Tidak Ada Proyeksi-";
            TextBoxIDPOProduksiBahanBaku.Text = poProduksiBProduk.IDPOProduksiProduk;
            TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiBProduk.EnumJenisHPP.Value);
            TextBoxPegawaiPIC.Text = poProduksiBProduk.TBPengguna1.NamaLengkap;
            TextBoxTanggalJatuhTempo.Text = poProduksiBProduk.TanggalJatuhTempo != null ? poProduksiBProduk.TanggalJatuhTempo.ToFormatTanggal() : string.Empty;
            TextBoxTanggalPengiriman.Text = poProduksiBProduk.TanggalPengiriman.ToFormatTanggal();
            TextBoxTanggalJatuhTempo.Text = poProduksiBProduk.TanggalJatuhTempo != null ? poProduksiBProduk.TanggalJatuhTempo.ToFormatTanggal() : string.Empty;
            TextBoxTanggalPengiriman.Text = poProduksiBProduk.TanggalPengiriman.ToFormatTanggal();
            TextBoxPegawai.Text = poProduksiBProduk.TBPengguna.NamaLengkap + " / " + poProduksiBProduk.Tanggal.ToFormatTanggal();

            if (poProduksiBProduk.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri)
            {
                TextBoxVendorTempat.Text = poProduksiBProduk.TBVendor.Nama;
                TextBoxEmail.Text = poProduksiBProduk.TBVendor.Email;
                TextBoxAlamat.Text = poProduksiBProduk.TBVendor.Alamat;
                TextBoxTelepon1.Text = poProduksiBProduk.TBVendor.Telepon1;
                TextBoxTelepon2.Text = poProduksiBProduk.TBVendor.Telepon2;
            }
            else
            {
                TextBoxVendorTempat.Text = poProduksiBProduk.TBTempat.Nama;
                TextBoxEmail.Text = poProduksiBProduk.TBTempat.Email;
                TextBoxAlamat.Text = poProduksiBProduk.TBTempat.Alamat;
                TextBoxTelepon1.Text = poProduksiBProduk.TBTempat.Telepon1;
                TextBoxTelepon2.Text = poProduksiBProduk.TBTempat.Telepon2;
            }

            DetailProduk.Visible = true;
            RepeaterPOProduksiProdukDetail.DataSource = poProduksiBProduk.TBPOProduksiProdukDetails.Select(item => new
            {
                item.TBKombinasiProduk.KodeKombinasiProduk,
                Produk = item.TBKombinasiProduk.TBProduk.Nama,
                AtributProduk = item.TBKombinasiProduk.TBAtributProduk.Nama,
                Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.TBKombinasiProduk),
                Harga = poProduksiBProduk.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.TotalHPP : item.TotalHargaVendor,
                item.Jumlah,
                Datang = item.Jumlah - item.Sisa,
                item.Sisa
            });
            RepeaterPOProduksiProdukDetail.DataBind();

            ButtonKembali.Visible = false;
            ButtonKembaliPOProduksi.Visible = true;
        }
    }

    protected void ButtonKembaliPOProduksi_Click(object sender, EventArgs e)
    {
        MultiViewPerformance.SetActiveView(ViewPOProduksi);
        ButtonKembali.Visible = true;
        ButtonKembaliPOProduksi.Visible = false;
    }
    #endregion
}