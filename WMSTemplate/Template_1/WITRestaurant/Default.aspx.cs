using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITRestaurant_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadMeja(db);

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                LabelPrintOrderCheckStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;

                var Tempat = db.TBTempats.FirstOrDefault(item => item.IDTempat == Pengguna.IDTempat);

                LabelPrintStoreTempat.Text = Pengguna.Store + " - " + Pengguna.Tempat;
                LabelTempatAlamat.Text = Tempat.Alamat;
                LabelTempatTelepon.Text = Tempat.Telepon1;

                LabelPrintKeteranganBiayaTambahan1.Text = Tempat.KeteranganBiayaTambahan1.ToUpper();
                LabelPrintKeteranganBiayaTambahan2.Text = Tempat.KeteranganBiayaTambahan2.ToUpper();

                PanelFooter.Visible = !string.IsNullOrWhiteSpace(Tempat.FooterPrint);
                PanelFooter1.Visible = PanelFooter.Visible;
                LabelPrintFooter.Text = Tempat.FooterPrint;
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(HiddenFieldPerintah.Value))
                LiteralWarning.Text = string.Empty;
        }
    }
    private void LoadMeja(DataClassesDatabaseDataContext db)
    {
        var ListMeja = db.TBMejas
            .Where(item => item.IDMeja > 2)
            .Select(item => new
            {
                item.IDMeja,
                item.Nama,
                item.IDStatusMeja,
                Warna = WarnaMeja(item.IDStatusMeja.Value),
                item.VIP,
                item.Status,
            }).ToArray();

        var MejaReguler = ListMeja.Where(item => item.VIP == false);
        if (MejaReguler.Count() > 0)
        {
            int barisReguler = (int)Math.Ceiling((double)MejaReguler.Count() / 10);
            int[] resultReguler = new int[barisReguler];

            for (int i = 0; i < barisReguler; i++)
            {
                resultReguler[i] = i + 1;
            }

            RepeaterReguler.DataSource = resultReguler.Select(item => new
            {
                baris = MejaReguler.Skip((item * 10) - 10).Take(10)
            });
            RepeaterReguler.DataBind();
        }

        var MejaVIP = ListMeja.Where(item => item.VIP == true);
        if (MejaVIP.Count() > 0)
        {
            int barisVIP = (int)Math.Ceiling((double)MejaReguler.Count() / 5);
            int[] resultVIP = new int[barisVIP];

            for (int i = 0; i < barisVIP; i++)
            {
                resultVIP[i] = i + 1;
            }

            RepeaterVIP.DataSource = resultVIP.Select(item => new
            {
                baris = MejaVIP.Skip((item * 5) - 5).Take(5)
            });
            RepeaterVIP.DataBind();
        }

        //TRANSAKSI TANPA MEJA
        RepeaterTransaksi.DataSource = db.TBTransaksis
            .Where(item => (item.IDMeja == 1 || item.IDMeja == 2) && item.IDStatusTransaksi == 2)
            .Select(item => new
            {
                item.IDTransaksi,
                Keterangan = item.Keterangan.ToString() + " " + item.IDTransaksi,
                item.TanggalTransaksi
            })
            .OrderByDescending(item => item.TanggalTransaksi);
        RepeaterTransaksi.DataBind();
    }
    public string WarnaMeja(int idStatusMeja)
    {
        switch (idStatusMeja)
        {
            case 1: return "btn btn-outline"; //KOSONG
            case 2: return "btn btn-warning"; //ORDER TERKIRIM
            case 3: return "btn btn-success"; //TERISI
            case 4: return "btn btn-danger"; //PRESETTLEMEN BILL
            default: return "";
        }
    }

    protected void RepeaterMeja_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            if (!string.IsNullOrWhiteSpace(HiddenFieldPerintah.Value))
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var Transaksi = db.TBTransaksis
                    .FirstOrDefault(item =>
                        item.IDMeja == Parse.Int(e.CommandArgument.ToString()) &&
                        item.IDStatusTransaksi == 2);

                    if (Transaksi != null)
                        PengaturanTransaksi(db, Transaksi);
                    else if (HiddenFieldPerintah.Value == "Transfer Item")
                    {
                        if (!string.IsNullOrWhiteSpace(HiddenFieldTransaksiAwal.Value))
                            Response.Redirect("Transfer.aspx?id=" + HiddenFieldTransaksiAwal.Value + "&table=" + e.CommandArgument.ToString());
                    }
                    else if (HiddenFieldPerintah.Value == "Pindah Meja" && !string.IsNullOrWhiteSpace(HiddenFieldTransaksiAwal.Value))
                    {
                        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                        Transaksi_Class TransaksiAwal = new Transaksi_Class(HiddenFieldTransaksiAwal.Value, Pengguna.IDPengguna);

                        var MejaTransaksi = db.TBMejas.FirstOrDefault(item => item.IDMeja == TransaksiAwal.Meja.IDMeja);

                        MejaTransaksi.IDStatusMeja = 1; //KOSONG

                        TransaksiAwal.Meja.IDMeja = Parse.Int(e.CommandArgument.ToString());

                        TransaksiAwal.ConfirmTransaksi(db);
                        db.SubmitChanges();

                        HiddenFieldPerintah.Value = string.Empty;
                        HiddenFieldTransaksiAwal.Value = string.Empty;
                        LiteralWarning.Text = string.Empty;

                        LoadMeja(db);
                    }
                }
            }
            else
                Response.Redirect("/WITPointOfSales/Default.aspx?table=" + e.CommandArgument);
        }
    }
    protected void RepeaterTransaksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            if (!string.IsNullOrWhiteSpace(HiddenFieldPerintah.Value))
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var Transaksi = db.TBTransaksis
                    .FirstOrDefault(item =>
                        item.IDTransaksi == e.CommandArgument.ToString() &&
                        item.IDStatusTransaksi == 2);

                    if (Transaksi != null)
                        PengaturanTransaksi(db, Transaksi);
                }
            }
            else
                Response.Redirect("/WITPointOfSales/Default.aspx?id=" + e.CommandArgument);
        }
    }
    private void PengaturanTransaksi(DataClassesDatabaseDataContext db, TBTransaksi Transaksi)
    {
        if (HiddenFieldPerintah.Value == "Order Check")
            PrintOrderCheck(Transaksi);
        else if (HiddenFieldPerintah.Value == "Pre Settlement")
            PrintPreSettlement(db, Transaksi);
        else if (HiddenFieldPerintah.Value == "Lihat Pesanan")
            LihatPesanan(Transaksi);
        else if (HiddenFieldPerintah.Value == "Split Bill")
            Response.Redirect("Transfer.aspx?id=" + Transaksi.IDTransaksi);
        else if (HiddenFieldPerintah.Value == "Transfer Item")
        {
            if (string.IsNullOrWhiteSpace(HiddenFieldTransaksiAwal.Value) || HiddenFieldTransaksiAwal.Value == Transaksi.IDTransaksi)
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja tujuan untuk Transfer Item Meja " + Transaksi.TBMeja.Nama);
                HiddenFieldTransaksiAwal.Value = Transaksi.IDTransaksi;
            }
            else
                Response.Redirect("Transfer.aspx?id=" + HiddenFieldTransaksiAwal.Value + "&and=" + Transaksi.IDTransaksi);
        }
        else if (HiddenFieldPerintah.Value == "Pindah Meja")
        {
            if (string.IsNullOrWhiteSpace(HiddenFieldTransaksiAwal.Value) || HiddenFieldTransaksiAwal.Value == Transaksi.IDTransaksi)
            {
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja tujuan " + Transaksi.TBMeja.Nama);
                HiddenFieldTransaksiAwal.Value = Transaksi.IDTransaksi;
            }
            else
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Transaksi_Class TransaksiAwal = new Transaksi_Class(HiddenFieldTransaksiAwal.Value, Pengguna.IDPengguna);
                Transaksi_Class TransaksiTujuan = new Transaksi_Class(Transaksi.IDTransaksi, Pengguna.IDPengguna);

                foreach (var item in TransaksiAwal.Detail)
                {
                    var DetailTujuan = TransaksiTujuan.Detail
                        .FirstOrDefault(item2 =>
                            item2.IDKombinasiProduk == item.IDKombinasiProduk &&
                            item2.HargaBeli == item.HargaBeli &&
                            item2.HargaJual == item.HargaJual &&
                            item2.Discount == item.Discount);

                    if (DetailTujuan != null)
                        DetailTujuan.Quantity += item.Quantity;
                    else
                        TransaksiTujuan.Detail.Add(item);
                }

                TransaksiAwal.ResetTransaksiDetail();

                TransaksiAwal.ConfirmTransaksi(db);
                TransaksiTujuan.ConfirmTransaksi(db);
                db.SubmitChanges();

                HiddenFieldPerintah.Value = string.Empty;
                HiddenFieldTransaksiAwal.Value = string.Empty;
                LiteralWarning.Text = string.Empty;

                LoadMeja(db);
            }
        }
        else if (HiddenFieldPerintah.Value == "Reprint")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Transaksi_Class TransaksiReprint = new Transaksi_Class(Transaksi.IDTransaksi, Pengguna.IDPengguna);

            TransaksiReprint.PrintOrder(PilihanStatusPrint.Reprint);
            TransaksiReprint.StatusPrint = true;
            TransaksiReprint.ConfirmTransaksi();

            HiddenFieldPerintah.Value = string.Empty;
            LiteralWarning.Text = string.Empty;
        }
    }
    private void PrintOrderCheck(TBTransaksi Transaksi)
    {
        MultiViewPrint.ActiveViewIndex = 0;
        LabelPrintOrderCheckIDOrder.Text = Transaksi.IDTransaksi;
        LabelPrintOrderCheckTable.Text = Transaksi.TBMeja.Nama;
        LabelPrintOrderCheckPengguna.Text = Transaksi.IDPenggunaUpdate.HasValue ? Transaksi.TBPengguna2.NamaLengkap : Transaksi.TBPengguna.NamaLengkap;
        LabelPrintOrderCheckTanggal.Text = Transaksi.TanggalUpdate.HasValue ? Pengaturan.FormatTanggalJam(Transaksi.TanggalUpdate.Value) : Pengaturan.FormatTanggalJam(Transaksi.TanggalTransaksi);

        RepeaterPrintOrderCheck.DataSource = Transaksi.TBTransaksiDetails
            .Select(item => new
            {
                JumlahProduk = item.Quantity,
                Produk = item.TBKombinasiProduk.Nama,
                Keterangan = item.Keterangan.Replace("\n", "<br/>"),
                StatusKeterangan = !string.IsNullOrWhiteSpace(item.Keterangan)
            }).ToArray();
        RepeaterPrintOrderCheck.DataBind();

        LabelPrintOrderCheckQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
        PanelKeteranganOrderCheck.Visible = !string.IsNullOrWhiteSpace(Transaksi.Keterangan);
        PanelKeteranganOrderCheck1.Visible = PanelKeteranganOrderCheck.Visible;
        LabelPrintOrderCheckKeterangan.Text = Transaksi.Keterangan;

        //LiteralWarning.Text = @"<script>window.print();</script>";

        HiddenFieldPerintah.Value = string.Empty;

        //UPDATE PANEL PRINT
        LiteralWarning.Text = string.Empty;
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "MyAction", "window.print();", true);
    }
    private void PrintPreSettlement(DataClassesDatabaseDataContext db, TBTransaksi Transaksi)
    {
        MultiViewPrint.ActiveViewIndex = 1;
        LabelPrintIDOrder.Text = Transaksi.IDTransaksi;
        LabelPrintPengguna.Text = Transaksi.IDPenggunaUpdate.HasValue ? Transaksi.TBPengguna2.NamaLengkap : Transaksi.TBPengguna.NamaLengkap;
        LabelPrintTanggal.Text = Transaksi.TanggalUpdate.HasValue ? Pengaturan.FormatTanggalJam(Transaksi.TanggalUpdate.Value) : Pengaturan.FormatTanggalJam(Transaksi.TanggalTransaksi);

        PanelPelanggan.Visible = Transaksi.TBPelanggan.IDPelanggan > 1;
        LabelPrintIDPelanggan.Text = Transaksi.TBPelanggan.IDPelanggan.ToString();
        LabelPrintPelangganNama.Text = Transaksi.TBPelanggan.NamaLengkap;
        LabelPrintPelangganTelepon.Text = Transaksi.TBPelanggan.TeleponLain;

        var Alamat = Transaksi.TBPelanggan.TBAlamats.FirstOrDefault();

        if (Alamat != null)
            LabelPrintPelangganAlamat.Text = Alamat.AlamatLengkap;

        RepeaterPrintTransaksiDetail.DataSource = Transaksi.TBTransaksiDetails
            .Select(item => new
            {
                JumlahProduk = item.Quantity,
                Produk = item.TBKombinasiProduk.Nama,
                TotalTanpaPotonganHargaJual = item.HargaJual * item.Quantity,
                TotalPotonganHargaJual = item.Discount * item.Quantity,
                PotonganHargaJual = item.Discount
            }).ToArray();
        RepeaterPrintTransaksiDetail.DataBind();

        LabelPrintQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
        LabelPrintSubtotal.Text = Pengaturan.FormatHarga(Transaksi.TBTransaksiDetails.Sum(item => item.Quantity * item.HargaJual));

        PanelDiscountTransaksi.Visible = Transaksi.PotonganTransaksi > 0;
        LabelPrintDiscountTransaksi.Text = Pengaturan.FormatHarga(Transaksi.PotonganTransaksi);

        PanelBiayaTambahan1.Visible = Transaksi.BiayaTambahan1 > 0;
        LabelPrintBiayaTambahan1.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan1);

        PanelBiayaTambahan2.Visible = Transaksi.BiayaTambahan2 > 0;
        LabelPrintBiayaTambahan2.Text = Pengaturan.FormatHarga(Transaksi.BiayaTambahan2);

        PanelBiayaPengiriman.Visible = Transaksi.BiayaPengiriman > 0;
        LabelPrintBiayaPengiriman.Text = Pengaturan.FormatHarga(Transaksi.BiayaPengiriman);

        PanelPembulatan.Visible = Transaksi.Pembulatan != 0;
        LabelPrintPembulatan.Text = Pengaturan.FormatHarga(Transaksi.Pembulatan);

        LabelPrintGrandTotal.Text = Pengaturan.FormatHarga(Transaksi.GrandTotal);

        PanelKeterangan.Visible = !string.IsNullOrWhiteSpace(Transaksi.Keterangan);
        PanelKeterangan1.Visible = PanelKeterangan.Visible;
        LabelPrintKeterangan.Text = Transaksi.Keterangan;

        LabelPrintTable.Text = Transaksi.TBMeja.Nama;

        //MERUBAH WARNA MEJA
        Transaksi.TBMeja.IDStatusMeja = 4;
        db.SubmitChanges();

        LoadMeja(db);

        //LiteralWarning.Text = @"<script>window.print();</script>";

        HiddenFieldPerintah.Value = string.Empty;

        //UPDATE PANEL PRINT
        LiteralWarning.Text = string.Empty;
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "MyAction", "window.print();", true);
    }
    private void LihatPesanan(TBTransaksi Transaksi)
    {
        LabelLihatPesananMeja.Text = Transaksi.TBMeja.Nama;
        LabelLihatPesananIDTransaksi.Text = Transaksi.IDTransaksi;
        LabelLihatPesananPengguna.Text = Transaksi.IDPenggunaUpdate.HasValue ? Transaksi.TBPengguna2.NamaLengkap : Transaksi.TBPengguna.NamaLengkap;
        LabelLihatPesananTanggal.Text = Transaksi.TanggalUpdate.HasValue ? Pengaturan.FormatTanggalJam(Transaksi.TanggalUpdate.Value) : Pengaturan.FormatTanggalJam(Transaksi.TanggalTransaksi);
        LabelLihatPesananQuantity.Text = Pengaturan.FormatHarga(Transaksi.JumlahProduk);
        LabelLihatPesananPelanggan.Text = Transaksi.TBPelanggan.NamaLengkap;
        LabelLihatPesananWaktuPelanggan.Text = PenghitunganWaktu(Transaksi.TanggalTransaksi.Value);
        LabelLihatPesananKeterangan.Text = Transaksi.Keterangan;

        RepeaterLihatPesananDetail.DataSource = Transaksi.TBTransaksiDetails
                    .Select(item => new
                    {
                        JumlahProduk = item.Quantity,
                        Produk = item.TBKombinasiProduk.Nama,
                        Keterangan = !string.IsNullOrWhiteSpace(item.Keterangan) ? "# " + item.Keterangan : ""
                    }).ToArray();
        RepeaterLihatPesananDetail.DataBind();

        HiddenFieldPerintah.Value = string.Empty;
        ModalPopupExtenderLihatPesanan.Show();
    }
    protected void ButtonWithoutTable_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITPointOfSales/Default.aspx?table=1");
    }
    protected void ButtonPreSettlement_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja untuk Print Pre Settlement");
        HiddenFieldPerintah.Value = "Pre Settlement";
    }
    protected void ButtonSplitBill_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja untuk Split Bill");
        HiddenFieldPerintah.Value = "Split Bill";
    }
    protected void ButtonTransfer_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja untuk Transfer Item");
        HiddenFieldPerintah.Value = "Transfer Item";
    }
    protected void ButtonPindahMeja_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja");
        HiddenFieldPerintah.Value = "Pindah Meja";
    }
    protected void ButtonReprint_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja untuk Reprint Order");
        HiddenFieldPerintah.Value = "Reprint";
    }
    protected void ButtonOrderCheck_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja untuk Print Order Check");
        HiddenFieldPerintah.Value = "Order Check";
    }
    protected void ButtonLihatPesanan_Click(object sender, EventArgs e)
    {
        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Warning, "Pilih meja untuk Melihat Pesanan");
        HiddenFieldPerintah.Value = "Lihat Pesanan";
    }
    protected void ButtonTakeAway_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WITPointOfSales/");
    }
    protected void ButtonMessage_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            ModalPopupExtenderMessage.Show();
            TextBoxPesan.Text = string.Empty;

            RepeaterPrinter.DataSource = db.TBKonfigurasiPrinters.ToArray();
            RepeaterPrinter.DataBind();
        }
    }
    protected void ButtonKirimSemua_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            foreach (var item in db.TBKonfigurasiPrinters.ToArray())
            {
                db.TBPesanPrints.InsertOnSubmit(new TBPesanPrint
                {
                    IDKonfigurasiPrinter = item.IDKonfigurasiPrinter,
                    IDPengguna = Pengguna.IDPengguna,
                    Isi = TextBoxPesan.Text,
                    Tanggal = DateTime.Now
                });
            }

            db.SubmitChanges();
        }
    }
    protected void RepeaterPrinter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Kirim")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                db.TBPesanPrints.InsertOnSubmit(new TBPesanPrint
                {
                    IDKonfigurasiPrinter = Parse.Int(e.CommandArgument.ToString()),
                    IDPengguna = Pengguna.IDPengguna,
                    Isi = TextBoxPesan.Text,
                    Tanggal = DateTime.Now
                });

                db.SubmitChanges();
            }
        }
    }
    protected void ButtonWaktuPelanggan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterWaktuPelanggan.DataSource = db.TBTransaksis
                .Where(item => item.IDStatusTransaksi == 2)
                .Select(item => new
                {
                    Meja = item.TBMeja.Nama,
                    item.IDTransaksi,
                    item.Keterangan,
                    item.TanggalTransaksi,
                    Waktu = PenghitunganWaktu(item.TanggalTransaksi.Value)
                })
                .OrderBy(item => item.TanggalTransaksi);
            RepeaterWaktuPelanggan.DataBind();

            ModalPopupExtenderWaktuPelanggan.Show();
        }
    }
    private string PenghitunganWaktu(DateTime tanggal)
    {
        var waktu = DateTime.Now - tanggal;

        string result = string.Empty;

        //HARI
        result += waktu.Days > 0 ? waktu.Days + " Hari " : "";

        //JAM
        result += waktu.Hours > 0 ? waktu.Hours + " Jam " : "";

        //MENIT
        result += waktu.Minutes > 0 ? waktu.Minutes + " Menit " : "";

        return result;
    }
}