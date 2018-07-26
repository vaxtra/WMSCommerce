using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Laporan_Transaksi_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                StatusTransaksi_Class StatusTransaksi_Class = new StatusTransaksi_Class();
                AtributProduk_Class ClassAtributProduk = new AtributProduk_Class(db);
                KategoriProduk_Class KategoriProduk_Class = new KategoriProduk_Class();
                PemilikProduk_Class ClassPemilikProduk = new PemilikProduk_Class(db);

                DropDownListCariPemilikProdukSemuaProduk.Items.AddRange(ClassPemilikProduk.Dropdownlist());
                DropDownListCariAtributProdukSemuaProduk.Items.AddRange(ClassAtributProduk.Dropdownlist());
                DropDownListCariKategoriSemuaProduk.Items.AddRange(KategoriProduk_Class.Dropdownlist(db));

                DropDownListCariProdukSemuaProduk.DataSource = db.TBProduks.OrderBy(item => item.Nama).ToArray();
                DropDownListCariProdukSemuaProduk.DataValueField = "IDProduk";
                DropDownListCariProdukSemuaProduk.DataTextField = "Nama";
                DropDownListCariProdukSemuaProduk.DataBind();
                DropDownListCariProdukSemuaProduk.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });

                DropDownListCariTempat.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempat.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariStatusTransaksi.Items.AddRange(StatusTransaksi_Class.DataDropDownList(db));
                DropDownListCariStatusTransaksi.SelectedValue = ((int)EnumStatusTransaksi.Complete).ToString();

                TextBoxTanggalAwal.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalAwal.Text = DateTime.Now.ToString("d MMMM yyyy");

                ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
                ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
            }

            LoadData();
        }
    }

    #region Filter By
    #region DEFAULT
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
            TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

            if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
                LabelPeriode.Text = TextBoxTanggalAwal.Text;
            else
                LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

            if (DropDownListCariFilterBy.SelectedValue == "semua")
            {
                divSemua.Visible = true;
                divFilter.Visible = false;
                #region FILTER
                Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], false);

                var Result = Laporan_Class.TransaksiDetail(DropDownListCariTempat.SelectedValue.ToInt(), DropDownListCariStatusTransaksi.SelectedValue.ToInt(), null, 0, TextBoxCariKodeSemuaProduk.Text, DropDownListCariPemilikProdukSemuaProduk.SelectedValue.ToInt(), DropDownListCariProdukSemuaProduk.SelectedValue.ToInt(), DropDownListCariAtributProdukSemuaProduk.SelectedValue.ToInt(), DropDownListCariKategoriSemuaProduk.SelectedValue.ToInt(), true);

                #region KONFIGURASI LAPORAN
                LabelPeriode.Text = Laporan_Class.Periode;

                //LinkDownloadSemuaProduk.Visible = GenerateExcel;

                //if (LinkDownloadSemuaProduk.Visible)
                //    LinkDownloadSemuaProduk.HRef = Laporan_Class.LinkDownload;

                //ButtonPrintSemuaProduk.OnClientClick = "return popitup('PenjualanProdukPrint.aspx" + Laporan_Class.TempPencarian + "')";
                #endregion

                LabelJumlahProduk.Text = Result["JumlahProduk"];
                LabelHargaPokok.Text = Result["HargaPokok"];
                LabelHargaJual.Text = Result["HargaJual"];
                LabelPotonganHarga.Text = Result["PotonganHargaJual"];
                LabelSubtotal.Text = Result["Subtotal"];
                LabelPenjualanBersih.Text = Result["PenjualanBersih"];

                RepeaterSemuaProduk.DataSource = Result["Data"];
                RepeaterSemuaProduk.DataBind();
                #endregion
            }
            else
            {
                divSemua.Visible = false;
                divFilter.Visible = true;
                #region FILTER
                string _tempPencarian = string.Empty;

                _tempPencarian += "?TanggalAwal=" + DateTime.Parse(TextBoxTanggalAwal.Text).Date;
                _tempPencarian += "&TanggalAkhir=" + DateTime.Parse(TextBoxTanggalAkhir.Text).Date;

                _tempPencarian += "&IDTempat=" + DropDownListCariTempat.SelectedValue;
                _tempPencarian += "&IDStatusTransaksi=" + DropDownListCariStatusTransaksi.SelectedValue;
                _tempPencarian += "&Filter=" + DropDownListCariFilterBy.SelectedValue;

                var _hasilDetailTransaksi = db.TBTransaksiDetails.ToArray();

                if (DropDownListCariTempat.SelectedValue != "0")
                {
                    _hasilDetailTransaksi = _hasilDetailTransaksi.Where(item => item.TBTransaksi.IDTempat == DropDownListCariTempat.SelectedValue.ToInt()).ToArray();
                }

                if (DropDownListCariStatusTransaksi.SelectedValue != "0")
                {
                    _hasilDetailTransaksi = _hasilDetailTransaksi.Where(item => item.TBTransaksi.IDStatusTransaksi == DropDownListCariStatusTransaksi.SelectedValue.ToInt()).ToArray();
                }

                var _dataDatabase = _hasilDetailTransaksi.Where(item => item.TBTransaksi.TanggalOperasional.Value.Date >= DateTime.Parse(TextBoxTanggalAwal.Text).Date && item.TBTransaksi.TanggalOperasional.Value.Date <= DateTime.Parse(TextBoxTanggalAkhir.Text).Date).ToArray();

                var _data = _dataDatabase.AsEnumerable().GroupBy(item => new
                {
                    item.TBKombinasiProduk
                })
                .Select(item => new
                {
                    KodeKombinasiProduk = item.Key.TBKombinasiProduk.KodeKombinasiProduk,
                    PemilikProduk = item.Key.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                    Produk = item.Key.TBKombinasiProduk.TBProduk.Nama,
                    AtributProduk = item.Key.TBKombinasiProduk.TBAtributProduk.Nama,
                    IDPemilikProduk = item.Key.TBKombinasiProduk.TBProduk.IDPemilikProduk,
                    IDProduk = item.Key.TBKombinasiProduk.TBProduk.IDProduk,
                    IDAtributProduk = item.Key.TBKombinasiProduk.IDAtributProduk,
                    RelasiKategori = item.Key.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks,
                    Kategori = StokProduk_Class.GabungkanSemuaKategoriProduk(db, null, item.Key.TBKombinasiProduk),
                    JumlahProduk = item.Sum(item2 => item2.Quantity),
                    HargaPokok = item.Sum(item2 => item2.HargaBeli * item2.Quantity),
                    HargaJual = item.Sum(item2 => item2.HargaJual * item2.Quantity),
                    PotonganHargaJual = item.Sum(item2 => item2.Discount * item2.Quantity),
                    Subtotal = item.Sum(item2 => item2.Subtotal),
                    PenjualanBersih = item.Sum(item2 => (item2.HargaJual - item2.Discount - item2.HargaBeli) * item2.Quantity),
                });

                if (DropDownListCariFilterBy.SelectedValue == "brand")
                {
                    var hasil = db.TBPemilikProduks.AsEnumerable().Where(item => _data.Any(data => data.IDPemilikProduk == item.IDPemilikProduk)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk),
                        TotalJumlahProduk = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.IDPemilikProduk == item.IDPemilikProduk).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterFilterBy.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterFilterBy.DataBind();
                }
                else if (DropDownListCariFilterBy.SelectedValue == "produk")
                {
                    var hasil = db.TBProduks.AsEnumerable().Where(item => _data.Any(data => data.IDProduk == item.IDProduk)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.IDProduk == item.IDProduk),
                        TotalJumlahProduk = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.IDProduk == item.IDProduk).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterFilterBy.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterFilterBy.DataBind();
                }
                else if (DropDownListCariFilterBy.SelectedValue == "varian")
                {
                    var hasil = db.TBAtributProduks.AsEnumerable().Where(item => _data.Any(data => data.IDAtributProduk == item.IDAtributProduk)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk),
                        TotalJumlahProduk = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.IDAtributProduk == item.IDAtributProduk).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterFilterBy.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterFilterBy.DataBind();
                }
                else if (DropDownListCariFilterBy.SelectedValue == "kategori")
                {
                    var hasil = db.TBKategoriProduks.AsEnumerable().Where(item => _data.Any(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null)).Select(item => new
                    {
                        Nama = item.Nama,
                        Body = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null),
                        TotalJumlahProduk = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.JumlahProduk),
                        TotalHargaPokok = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.HargaPokok),
                        TotalHargaJual = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.HargaJual),
                        TotalPotonganHargaJual = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.PotonganHargaJual),
                        TotalSubtotal = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.Subtotal),
                        TotalPenjualanBersih = _data.Where(data => data.RelasiKategori.FirstOrDefault(relasi => relasi.IDKategoriProduk == item.IDKategoriProduk) != null).Sum(data => data.PenjualanBersih),
                    }).ToArray();

                    RepeaterFilterBy.DataSource = hasil.OrderBy(item => item.Nama);
                    RepeaterFilterBy.DataBind();
                }
                #endregion
            }
        }
    }
    #endregion

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
}