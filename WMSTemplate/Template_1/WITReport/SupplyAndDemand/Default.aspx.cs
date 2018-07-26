using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_RatioOnStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            LoadData();
        }
    }

    private void LoadData()
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        if (TextBoxTanggalAwal.Text == TextBoxTanggalAkhir.Text)
            LabelPeriode.Text = TextBoxTanggalAwal.Text;
        else
            LabelPeriode.Text = TextBoxTanggalAwal.Text + " - " + TextBoxTanggalAkhir.Text;

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Tempat = db.TBTempats
                .GroupBy(item => new
                {
                    item.IDKategoriTempat,
                    item.IDTempat,
                    Kategori = item.TBKategoriTempat.Nama,
                    Tempat = item.Nama
                })
                .Select(item => new
                {
                    item.Key.IDKategoriTempat,
                    item.Key.IDTempat,

                    item.Key.Kategori,
                    item.Key.Tempat,
                    TransaksiQuantity = item
                                            .Sum(item2 => item2.TBTransaksis
                                            .Where(item3 =>
                                                            item3.TanggalOperasional.Value >= (DateTime)ViewState["TanggalAwal"] &&
                                                            item3.TanggalOperasional.Value <= (DateTime)ViewState["TanggalAkhir"] &&
                                                            item3.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                                            .Sum(item4 => item4.JumlahProduk)),
                    TransaksiNominal = item
                                            .Sum(item2 => item2.TBTransaksis
                                            .Where(item3 =>
                                                            item3.TanggalOperasional.Value >= (DateTime)ViewState["TanggalAwal"] &&
                                                            item3.TanggalOperasional.Value <= (DateTime)ViewState["TanggalAkhir"] &&
                                                            item3.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete)
                                            .Sum(item4 => item4.GrandTotal)),

                    StokQuantity = item.Sum(item2 => item2.TBStokProduks.Sum(item3 => item3.Jumlah) ?? 0),
                    StokNominal = item.Sum(item2 => item2.TBStokProduks.Sum(item3 => item3.Jumlah * item3.HargaJual)),

                    ProsesProduksi = item.Sum(item2 => item2.TBPOProduksiProduks.Sum(item3 => item3.TotalJumlah)) - item.Sum(item2 => item2.TBPOProduksiProduks.Sum(item3 => item3.TBPenerimaanPOProduksiProduks.Sum(item4 => item4.TotalDatang)))
                })
                .OrderBy(item => item.IDKategoriTempat)
                .ThenBy(item => item.IDTempat);

            var Result = Tempat.Select(item => new
            {
                item,
                PersentaseTransaksiQuantity = ((decimal)item.TransaksiQuantity / Tempat.Sum(item2 => item2.TransaksiQuantity) * 100),
                PersentaseTransaksiNominal = ((decimal)item.TransaksiNominal / Tempat.Sum(item2 => item2.TransaksiNominal) * 100),

                PersentaseStokQuantity = ((decimal)item.StokQuantity / Tempat.Sum(item2 => item2.StokQuantity) * 100),
                PersentaseStokNominal = ((decimal)item.StokNominal / Tempat.Sum(item2 => item2.StokNominal) * 100),

                SelesaiProduksi = item.StokQuantity + (item.TransaksiQuantity.HasValue ? item.TransaksiQuantity.Value : 0)
            });

            RepeaterLokasi.DataSource = Result;
            RepeaterLokasi.DataBind();

            LabelTransaksiQuantity.Text = Result.Sum(item => item.item.TransaksiQuantity).ToFormatHargaBulat();
            LabelTransaksiNominal.Text = Result.Sum(item => item.item.TransaksiNominal).ToFormatHarga();
            LabelStokQuantity.Text = Result.Sum(item => item.item.StokQuantity).ToFormatHargaBulat();
            LabelStokNominal.Text = Result.Sum(item => item.item.StokNominal).ToFormatHarga();
            LabelProsesProduksi.Text = Result.Sum(item => item.item.ProsesProduksi).ToFormatHarga();
            LabelSelesaiProduksi.Text = Result.Sum(item => item.SelesaiProduksi).ToFormatHarga();
        }
    }

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
}