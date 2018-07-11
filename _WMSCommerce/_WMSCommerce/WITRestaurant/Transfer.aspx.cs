using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITRestaurant_SplitBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    //MENCARI TRANSAKSI AWAL
                    var Transaksi = db.TBTransaksis
                    .FirstOrDefault(item =>
                        item.IDTransaksi == Request.QueryString["id"] &&
                        item.IDStatusTransaksi == 2);

                    if (Transaksi != null)
                    {
                        //JIKA TRANSAKSI DITEMUKAN
                        Transaksi_Model TransaksiAwal = new Transaksi_Model(Transaksi.IDTransaksi, Pengguna.IDPengguna);

                        ViewState["TransaksiAwal"] = TransaksiAwal;

                        //JIKA SPLIT BILL ATAU TRANSFER KE MEJA BARU
                        if (string.IsNullOrWhiteSpace(Request.QueryString["and"]) || !string.IsNullOrWhiteSpace(Request.QueryString["table"]))
                            MembuatTransaksiBaru(Pengguna, TransaksiAwal);
                        else
                        {
                            //JIKA TRANSFER ITEM
                            var transaksiTujuan = db.TBTransaksis
                                .FirstOrDefault(item =>
                                    item.IDTransaksi == Request.QueryString["and"] &&
                                    item.IDStatusTransaksi == 2);

                            if (transaksiTujuan != null)
                            {
                                Transaksi_Model TransaksiTujuan = new Transaksi_Model(transaksiTujuan.IDTransaksi, Pengguna.IDPengguna);

                                ViewState["TransaksiTujuan"] = TransaksiTujuan;

                                //KETERANGAN
                                LabelKeterangan.Text = "Transfer Meja";
                                LabelMejaAwal.Text = TransaksiAwal.Meja.Nama;
                                LabelMejaTujuan.Text = TransaksiTujuan.Meja.Nama;
                            }
                            else
                                MembuatTransaksiBaru(Pengguna, TransaksiAwal);
                        }

                        LoadData();
                    }
                    else
                        Response.Redirect("Default.aspx"); //JIKA TRANSAKSI TIDAK DITEMUKAN
                }
            }
            else
                Response.Redirect("Default.aspx");
        }
    }
    private void MembuatTransaksiBaru(PenggunaLogin Pengguna, Transaksi_Model TransaksiAwal)
    {
        //JIKA SPLIT BILL MEMBUAT OBJECT CLASS TRANSAKSI BARU
        var TransaksiTujuan = new Transaksi_Model(Pengguna.IDPengguna, Pengguna.IDTempat, DateTime.Now);

        if (!string.IsNullOrWhiteSpace(Request.QueryString["table"]))
        {
            TransaksiTujuan.Meja.IDMeja = Parse.Int(Request.QueryString["table"]); //JIKA TRANSFER KE MEJA KOSONG

            LabelKeterangan.Text = "Transfer Meja";
        }
        else
        {
            TransaksiTujuan.Meja.IDMeja = 1;
            TransaksiTujuan.Keterangan = "Split Bill #" + TransaksiAwal.Meja.Nama + " #" + TransaksiAwal.IDTransaksi;

            LabelKeterangan.Text = "Split Bill";
        }

        LabelMejaAwal.Text = TransaksiAwal.Meja.Nama;
        LabelMejaTujuan.Text = TransaksiTujuan.Meja.Nama;

        ViewState["TransaksiTujuan"] = TransaksiTujuan;
    }
    protected void RepeaterTransaksiDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pindah")
        {
            Transaksi_Model TransaksiAwal = (Transaksi_Model)ViewState["TransaksiAwal"];
            Transaksi_Model TransaksiTujuan = (Transaksi_Model)ViewState["TransaksiTujuan"];

            //MENCARI DETAIL BERDASARKAN ID DETAIL TRANSAKSI
            var DetailAwal = TransaksiAwal.Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == Parse.Int(e.CommandArgument.ToString()));

            int jumlahTransaksi = 0;
            int jumlahSplitBill = 0;

            if (DetailAwal.Quantity > 0)
            {
                jumlahSplitBill = 1;
                jumlahTransaksi = -1;
            }
            else
            {
                jumlahSplitBill = -1;
                jumlahTransaksi = 1;
            }

            //MENCARI APAKAH DETAIL ADA DI TRANSAKSI TUJUAN
            var DetailTujuan = TransaksiTujuan.Detail
                .FirstOrDefault(item =>
                    item.IDKombinasiProduk == DetailAwal.IDKombinasiProduk &&
                    item.HargaBeli == DetailAwal.HargaBeli &&
                    item.HargaJual == DetailAwal.HargaJual &&
                    item.Discount == DetailAwal.Discount);

            if (DetailTujuan == null)
            {
                //JIKA TIDAK ADA DI TRANSAKSI TUJUAN MEMBUAT DETAIL BARU
                DetailTujuan = new TransaksiRetailDetail_Model
                {
                    IDDetailTransaksi = TransaksiTujuan.IDDetailTransaksiTemp,
                    IDKombinasiProduk = DetailAwal.IDKombinasiProduk,
                    IDStokProduk = DetailAwal.IDStokProduk,
                    Barcode = DetailAwal.Barcode,
                    Nama = DetailAwal.Nama,
                    Quantity = jumlahSplitBill,
                    Berat = DetailAwal.Berat,
                    HargaBeliKotor = DetailAwal.HargaBeliKotor,
                    HargaJual = DetailAwal.HargaJual,
                    DiscountStore = DetailAwal.DiscountStore,
                    DiscountKonsinyasi = DetailAwal.DiscountKonsinyasi,
                    Keterangan = DetailAwal.Keterangan
                    //UbahQuantity
                };

                TransaksiTujuan.Detail.Add(DetailTujuan);
            }
            else //JIKA DI TRANSAKSI TUJUAN ADA DETAIL
                TransaksiTujuan.TambahKurangJumlahProduk(DetailTujuan.IDDetailTransaksi, jumlahSplitBill);

            //MELAKUKAN PENGATURAN JUMLAH DETAIL AWAL
            TransaksiAwal.TambahKurangJumlahProduk(Parse.Int(e.CommandArgument.ToString()), jumlahTransaksi);

            LoadData();
        }
    }
    protected void RepeaterTransaksiSplitBill_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pindah")
        {
            Transaksi_Model TransaksiAwal = (Transaksi_Model)ViewState["TransaksiAwal"];
            Transaksi_Model TransaksiTujuan = (Transaksi_Model)ViewState["TransaksiTujuan"];

            //MENCARI DETAIL BERDASARKAN ID DETAIL TRANSAKSI
            var DetailTujuan = TransaksiTujuan.Detail
                .FirstOrDefault(item => item.IDDetailTransaksi == Parse.Int(e.CommandArgument.ToString()));

            int jumlahTransaksi = 0;
            int jumlahSplitBill = 0;

            if (DetailTujuan.Quantity > 0)
            {
                jumlahSplitBill = 1;
                jumlahTransaksi = -1;
            }
            else
            {
                jumlahSplitBill = -1;
                jumlahTransaksi = 1;
            }

            //MENCARI APAKAH DETAIL ADA DI TRANSAKSI AWAL
            var DetailAwal = TransaksiAwal.Detail
                .FirstOrDefault(item =>
                    item.IDKombinasiProduk == DetailTujuan.IDKombinasiProduk &&
                    item.HargaBeli == DetailTujuan.HargaBeli &&
                    item.HargaJual == DetailTujuan.HargaJual &&
                    item.Discount == DetailTujuan.Discount);

            if (DetailAwal == null)
            {
                //JIKA TIDAK ADA DI TRANSAKSI AWAL MEMBUAT DETAIL BARU
                DetailAwal = new TransaksiRetailDetail_Model
                {
                    IDDetailTransaksi = TransaksiAwal.IDDetailTransaksiTemp,
                    IDKombinasiProduk = DetailTujuan.IDKombinasiProduk,
                    IDStokProduk = DetailTujuan.IDStokProduk,
                    Barcode = DetailTujuan.Barcode,
                    Nama = DetailTujuan.Nama,
                    Quantity = jumlahSplitBill,
                    Berat = DetailTujuan.Berat,
                    HargaBeliKotor = DetailTujuan.HargaBeliKotor,
                    HargaJual = DetailTujuan.HargaJual,
                    DiscountStore = DetailTujuan.DiscountStore,
                    DiscountKonsinyasi = DetailTujuan.DiscountKonsinyasi,
                    Keterangan = DetailTujuan.Keterangan
                    //UbahQuantity
                };

                TransaksiAwal.Detail.Add(DetailAwal);
            }
            else //JIKA DI TRANSAKSI AWAL ADA DETAIL
                TransaksiAwal.TambahKurangJumlahProduk(DetailAwal.IDDetailTransaksi, jumlahSplitBill);

            //MELAKUKAN PENGATURAN JUMLAH DETAIL AWAL
            TransaksiTujuan.TambahKurangJumlahProduk(Parse.Int(e.CommandArgument.ToString()), jumlahTransaksi);

            LoadData();
        }
    }
    private void LoadData()
    {
        Transaksi_Model TransaksiAwal = (Transaksi_Model)ViewState["TransaksiAwal"];
        Transaksi_Model TransaksiTujuan = (Transaksi_Model)ViewState["TransaksiTujuan"];

        RepeaterTransaksiDetail.DataSource = TransaksiAwal.Detail.Select(item => new
        {
            IDDetailTransaksi = item.IDDetailTransaksi,
            IDKombinasiProduk = item.IDKombinasiProduk,
            IDStokProduk = item.IDStokProduk,
            Nama = item.Nama,
            JumlahProduk = item.Quantity,
            PotonganHargaJual = item.Discount,
            HargaJual = item.HargaJual,
            PersentaseDiscount = item.PersentaseDiscount,
            HargaJualTampil = item.HargaJualBersih,
            Subtotal = item.Subtotal
        });
        RepeaterTransaksiDetail.DataBind();

        int JumlahDetail = TransaksiTujuan.Detail.Count;

        RepeaterTransaksiSplitBill.DataSource = TransaksiTujuan.Detail.Select(item => new
        {
            IDDetailTransaksi = item.IDDetailTransaksi,
            IDKombinasiProduk = item.IDKombinasiProduk,
            IDStokProduk = item.IDStokProduk,
            Nama = item.Nama,
            JumlahProduk = item.Quantity,
            PotonganHargaJual = item.Discount,
            HargaJual = item.HargaJual,
            PersentaseDiscount = item.PersentaseDiscount,
            HargaJualTampil = item.HargaJualBersih,
            Subtotal = item.Subtotal,
            JumlahDetail = JumlahDetail
        });
        RepeaterTransaksiSplitBill.DataBind();
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Transaksi_Model TransaksiAwal = (Transaksi_Model)ViewState["TransaksiAwal"];
            Transaksi_Model TransaksiTujuan = (Transaksi_Model)ViewState["TransaksiTujuan"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TransaksiAwal.ConfirmTransaksi(db);
                TransaksiTujuan.ConfirmTransaksi(db);
                db.SubmitChanges();
            }

            Response.Redirect("Default.aspx", false);
        }
        catch (Exception ex)
        {
            LogError_Class LogError = new LogError_Class(ex, Request.Url.PathAndQuery);
        }
    }
}