using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_POProduksi_PerformanceSupplier : System.Web.UI.Page
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

            var poProduksiBahanBaku = db.TBPOProduksiBahanBakus
                .Where(item => item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date &&
                    item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date &&
                    item.TBPenerimaanPOProduksiBahanBakus.Count > 0)
                .Select(item => new
                {
                    item.IDTempat,
                    item.IDPOProduksiBahanBaku,
                    item.IDSupplier,
                    item.TanggalPengiriman,
                    item.EnumJenisProduksi,
                    TanggalPenerimaanPO = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(data => data.TanggalTerima).FirstOrDefault().TanggalTerima,
                    TotalDatang = db.TBPenerimaanPOProduksiBahanBakuDetails.Where(data => data.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDPOProduksiBahanBaku == item.IDPOProduksiBahanBaku).Sum(data => data.Datang),
                    JumlahDiTerima = db.TBPenerimaanPOProduksiBahanBakuDetails.Where(data => data.TBPenerimaanPOProduksiBahanBaku.TBPOProduksiBahanBaku.IDPOProduksiBahanBaku == item.IDPOProduksiBahanBaku).Sum(data => data.Diterima)
                }).ToArray();

            //Purchase Order
            RepeaterPerformancePurchaseOrder.DataSource = db.TBSuppliers.AsEnumerable().Where(item => poProduksiBahanBaku.Where(data => data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Any(data => data.IDSupplier == item.IDSupplier)).Select(item => new
            {
                item.IDSupplier,
                Supplier = item.Nama,
                item.Alamat,
                TotalPO = poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiBahanBaku).Distinct().Count(),
                Penerimaan = poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiBahanBaku).Count() == 0 ? -1 :
                            ((decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Sum(data => data.JumlahDiTerima) / (decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Sum(data => data.TotalDatang)) * 100,
                Pengiriman = poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiBahanBaku).Count() == 0 ? -1 :
                            ((decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.TanggalPengiriman.Value.Date >= data.TanggalPenerimaanPO.Value.Date && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiBahanBaku).Count() / (decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(data => data.IDPOProduksiBahanBaku).Count()) * 100
            }).ToArray();
            RepeaterPerformancePurchaseOrder.DataBind();

            //In-House Production
            RepeaterPerformanceInHouseProduction.DataSource = db.TBTempats.AsEnumerable().Where(item => poProduksiBahanBaku.Where(data => data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Any(data => data.IDTempat == item.IDTempat)).Select(item => new
            {
                item.IDTempat,
                Tempat = item.Nama,
                item.Alamat,
                TotalPO = poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiBahanBaku).Distinct().Count(),
                Penerimaan = poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiBahanBaku).Count() == 0 ? -1 :
                            ((decimal)poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Sum(data => data.JumlahDiTerima) / (decimal)poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Sum(data => data.TotalDatang)) * 100,
                Pengiriman = poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiBahanBaku).Count() == 0 ? -1 :
                            ((decimal)poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.TanggalPengiriman.Value.Date >= data.TanggalPenerimaanPO.Value.Date && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiBahanBaku).Count() / (decimal)poProduksiBahanBaku.Where(data => data.IDTempat == item.IDTempat && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(data => data.IDPOProduksiBahanBaku).Count()) * 100
            }).ToArray();
            RepeaterPerformanceInHouseProduction.DataBind();

            //Production to Supplier
            RepeaterPerformanceProductionToSupplier.DataSource = db.TBSuppliers.AsEnumerable().Where(item => poProduksiBahanBaku.Where(data => data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Any(data => data.IDSupplier == item.IDSupplier)).Select(item => new
            {
                item.IDSupplier,
                Supplier = item.Nama,
                item.Alamat,
                TotalPO = poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiBahanBaku).Distinct().Count(),
                Penerimaan = poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiBahanBaku).Count() == 0 ? -1 :
                            ((decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Sum(data => data.JumlahDiTerima) / (decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Sum(data => data.TotalDatang)) * 100,
                Pengiriman = poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiBahanBaku).Count() == 0 ? -1 :
                            ((decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.TanggalPengiriman.Value.Date >= data.TanggalPenerimaanPO.Value.Date && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiBahanBaku).Count() / (decimal)poProduksiBahanBaku.Where(data => data.IDSupplier == item.IDSupplier && data.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(data => data.IDPOProduksiBahanBaku).Count()) * 100
            }).ToArray();
            RepeaterPerformanceProductionToSupplier.DataBind();
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
            if (e.CommandName == "Supplier")
                TextBoxNamaSupplierTempat.Text = db.TBSuppliers.FirstOrDefault(item => item.IDSupplier == e.CommandArgument.ToInt()).Nama;
            else
                TextBoxNamaSupplierTempat.Text = db.TBTempats.FirstOrDefault(item => item.IDTempat == e.CommandArgument.ToInt()).Nama;


            TBPOProduksiBahanBaku[] daftarPOProduksiBahanBaku = db.TBPOProduksiBahanBakus.Where(item => (e.CommandName == "Supplier" ? item.IDSupplier == e.CommandArgument.ToInt() : item.IDTempat == e.CommandArgument.ToInt()) &&
                item.Tanggal.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.Tanggal.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date &&
                item.TBPenerimaanPOProduksiBahanBakus.Count > 0).ToArray();

            if (e.CommandName == "Supplier")
            {
                RepeaterPurchaseOrder.DataSource = daftarPOProduksiBahanBaku.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.PurchaseOrder).Select(item => new
                {
                    item.EnumJenisProduksi,
                    item.IDPOProduksiBahanBaku,
                    Tempat = item.TBTempat.Nama,
                    Supplier = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    Tanggal_ClassTerakhirTerima = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.Date <= item.TanggalPengiriman.Value.Date ? "fitSize" : "fitSize danger",
                    TanggalTerakhirTerima = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHarga(),
                    TotalTolak = item.TBPenerimaanPOProduksiBahanBakus.Sum(item2 => item2.TotalTolakKeGudang + item2.TotalTolakKeSupplier).ToFormatHarga(),
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
                RepeaterProduksiSendiri.DataSource = daftarPOProduksiBahanBaku.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri).Select(item => new
                {
                    item.EnumJenisProduksi,
                    item.IDPOProduksiBahanBaku,
                    Tempat = item.TBTempat.Nama,
                    Supplier = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    Tanggal_ClassTerakhirTerima = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.Date <= item.TanggalPengiriman.Value.Date ? "fitSize" : "fitSize danger",
                    TanggalTerakhirTerima = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHarga(),
                    TotalTolak = item.TBPenerimaanPOProduksiBahanBakus.Sum(item2 => item2.TotalTolakKeGudang + item2.TotalTolakKeSupplier).ToFormatHarga(),
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

            if (e.CommandName == "Supplier")
            {
                RepeaterProduksiKeSupplier.DataSource = daftarPOProduksiBahanBaku.Where(item => item.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiKeSupplierVendor).Select(item => new
                {
                    item.EnumJenisProduksi,
                    item.IDPOProduksiBahanBaku,
                    Tempat = item.TBTempat.Nama,
                    Supplier = item.IDSupplier != null ? item.TBSupplier.Nama : string.Empty,
                    PIC = item.TBPengguna1.NamaLengkap,
                    Tanggal = item.Tanggal,
                    TanggalPengiriman = item.TanggalPengiriman.ToFormatTanggal(),
                    Tanggal_ClassTerakhirTerima = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.Date <= item.TanggalPengiriman.Value.Date ? "fitSize" : "fitSize danger",
                    TanggalTerakhirTerima = item.TBPenerimaanPOProduksiBahanBakus.OrderByDescending(item2 => item2.TanggalDatang).FirstOrDefault().TanggalDatang.ToFormatTanggal(),
                    TotalJumlah = item.TotalJumlah.ToFormatHarga(),
                    TotalTolak = item.TBPenerimaanPOProduksiBahanBakus.Sum(item2 => item2.TotalTolakKeGudang + item2.TotalTolakKeSupplier).ToFormatHarga(),
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
            TBPOProduksiBahanBaku poProduksiBahanBaku = db.TBPOProduksiBahanBakus.FirstOrDefault(item => item.IDPOProduksiBahanBaku == e.CommandArgument.ToString());

            TextBoxIDProyeksi.Text = poProduksiBahanBaku.IDProyeksi != null ? poProduksiBahanBaku.IDProyeksi : "-Tidak Ada Proyeksi-";
            TextBoxIDPOProduksiBahanBaku.Text = poProduksiBahanBaku.IDPOProduksiBahanBaku;
            TextBoxStatusHPP.Text = Pengaturan.StatusJenisHPP(poProduksiBahanBaku.EnumJenisHPP.Value);
            TextBoxPegawaiPIC.Text = poProduksiBahanBaku.TBPengguna1.NamaLengkap;
            TextBoxTanggalJatuhTempo.Text = poProduksiBahanBaku.TanggalJatuhTempo != null ? poProduksiBahanBaku.TanggalJatuhTempo.ToFormatTanggal() : string.Empty;
            TextBoxTanggalPengiriman.Text = poProduksiBahanBaku.TanggalPengiriman.ToFormatTanggal();
            TextBoxPegawai.Text = poProduksiBahanBaku.TBPengguna.NamaLengkap + " / " + poProduksiBahanBaku.Tanggal.ToFormatTanggal();

            if (poProduksiBahanBaku.EnumJenisProduksi != (int)PilihanEnumJenisProduksi.ProduksiSendiri)
            {
                TextBoxSupplierVendorTempat.Text = poProduksiBahanBaku.TBSupplier.Nama;
                TextBoxEmail.Text = poProduksiBahanBaku.TBSupplier.Email;
                TextBoxAlamat.Text = poProduksiBahanBaku.TBSupplier.Alamat;
                TextBoxTelepon1.Text = poProduksiBahanBaku.TBSupplier.Telepon1;
                TextBoxTelepon2.Text = poProduksiBahanBaku.TBSupplier.Telepon2;
            }
            else
            {
                TextBoxSupplierVendorTempat.Text = poProduksiBahanBaku.TBTempat.Nama;
                TextBoxEmail.Text = poProduksiBahanBaku.TBTempat.Email;
                TextBoxAlamat.Text = poProduksiBahanBaku.TBTempat.Alamat;
                TextBoxTelepon1.Text = poProduksiBahanBaku.TBTempat.Telepon1;
                TextBoxTelepon2.Text = poProduksiBahanBaku.TBTempat.Telepon2;
            }

            DetailBahanBaku.Visible = true;
            RepeaterPOProdusiBahanBakuDetail.DataSource = poProduksiBahanBaku.TBPOProduksiBahanBakuDetails.Select(item => new
            {
                item.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBBahanBaku.Nama,
                Satuan = item.TBSatuan.Nama,
                Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(db, null, item.TBBahanBaku),
                Harga = poProduksiBahanBaku.EnumJenisProduksi == (int)PilihanEnumJenisProduksi.ProduksiSendiri ? item.TotalHPP : item.TotalHargaSupplier,
                item.Jumlah,
                Datang = item.Jumlah - item.Sisa,
                item.Sisa
            });
            RepeaterPOProdusiBahanBakuDetail.DataBind();

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