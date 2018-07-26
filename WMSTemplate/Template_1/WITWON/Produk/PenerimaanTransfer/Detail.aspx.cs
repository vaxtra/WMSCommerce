using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITWON_Produk_PenerimaanTransfer_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var DataTransferProduk = db.TBTransferProduks
                    .FirstOrDefault(item => item.IDTransferProduk == Request.QueryString["id"]);

                if (DataTransferProduk != null)
                {
                    LabelPengirimLokasi.Text = DataTransferProduk.TBTempat.Nama;
                    LabelPengirimTanggal.Text = DataTransferProduk.TanggalKirim.ToFormatTanggal();
                    LabelPengirimPengguna.Text = DataTransferProduk.TBPengguna.NamaLengkap;

                    LabelPenerimaLokasi.Text = DataTransferProduk.TBTempat1.Nama;
                    LabelPenerimaTanggal.Text = DataTransferProduk.TanggalTerima.ToFormatTanggal();
                    LabelPenerimaPengguna.Text = DataTransferProduk.IDPenerima.HasValue ? DataTransferProduk.TBPengguna1.NamaLengkap : "";

                    LabelStatusTransfer.Text = Pengaturan.JenisTransferHTML(DataTransferProduk.EnumJenisTransfer);

                    var DataTransferProdukDetail = DataTransferProduk.TBTransferProdukDetails.ToArray();

                    var ListTransferStokProdukDetail = DataTransferProdukDetail
                        .GroupBy(item => new
                        {
                            IDProduk = item.TBKombinasiProduk.TBProduk.IDProduk,
                            Produk = item.TBKombinasiProduk.TBProduk.Nama,
                            Kategori = item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.Count > 0 ? item.TBKombinasiProduk.TBProduk.TBRelasiProdukKategoriProduks.FirstOrDefault().TBKategoriProduk.Nama : "",
                            PemilikProduk = item.TBKombinasiProduk.TBProduk.TBPemilikProduk.Nama,
                            Warna = item.TBKombinasiProduk.TBProduk.TBWarna.Nama,
                        })
                        .Select(item => new
                        {
                            item.Key,
                            Detail = DataTransferProdukDetail
                                .Where(item2 => item2.TBKombinasiProduk.IDProduk == item.Key.IDProduk)
                                .Select(item2 => new StokProduk_Model
                                {
                                    IDProduk = item2.TBKombinasiProduk.TBProduk.IDProduk,
                                    Produk = item2.TBKombinasiProduk.TBProduk.Nama,
                                    IDKombinasiProduk = item2.IDKombinasiProduk,
                                    Kode = item2.TBKombinasiProduk.KodeKombinasiProduk,
                                    Atribut = item2.TBKombinasiProduk.TBAtributProduk.Nama,
                                    HargaJual = item2.HargaJual,
                                    Jumlah = item2.Jumlah
                                })
                        });

                    LiteralLaporan.Text = string.Empty;
                    int index = 1;
                    int indexVarian = 1;

                    foreach (var item in ListTransferStokProdukDetail)
                    {
                        indexVarian = 1;

                        LiteralLaporan.Text += "<tr>";

                        string rowspan = item.Detail.Count() >= 2 ? "rowspan='" + item.Detail.Count() + "'" : "";

                        LiteralLaporan.Text += "<td " + rowspan + ">" + index++ + "</td>";
                        LiteralLaporan.Text += "<td " + rowspan + ">" + item.Key.Produk + "</td>";
                        LiteralLaporan.Text += "<td " + rowspan + ">" + item.Key.Kategori + "</td>";

                        foreach (var item2 in item.Detail)
                        {
                            if (indexVarian > 1)
                                LiteralLaporan.Text += "<tr>";

                            LiteralLaporan.Text += "<td>" + item2.Atribut + "</td>";
                            LiteralLaporan.Text += "<td class='text-right'>" + item2.HargaJual.ToFormatHarga() + "</td>";
                            LiteralLaporan.Text += "<td class='text-right'>" + item2.Jumlah.ToFormatHargaBulat() + "</td>";
                            LiteralLaporan.Text += "<td class='text-right warning bold'>" + item2.Subtotal.ToFormatHarga() + "</td>";

                            LiteralLaporan.Text += "</tr>";

                            indexVarian++;
                        }
                    }

                    LabelTotalJumlah.Text = DataTransferProduk.TotalJumlah.ToFormatHargaBulat();
                    LabelTotalNominal.Text = DataTransferProduk.GrandTotalHargaJual.ToFormatHarga();

                    LabelTotalJumlah1.Text = LabelTotalJumlah.Text;
                    LabelTotalNominal1.Text = LabelTotalNominal.Text;

                    linkKeluar.HRef = "Default.aspx";
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}