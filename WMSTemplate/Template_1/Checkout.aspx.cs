using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {

                //CLOSE SEMUA PANEL KECUALI PANEL PELANNGAN
                FormPengiriman.Attributes.Add("style", "display:none");
                FormPembayaran.Attributes.Add("style", "display:none");

                //PROVINSI
                DropDownListProvinsi.DataSource = db.TBKurirProvinsis.OrderBy(item => item.Nama).ToArray();
                DropDownListProvinsi.DataValueField = "IDKurirProvinsi";
                DropDownListProvinsi.DataTextField = "Nama";
                DropDownListProvinsi.DataBind();
                DropDownListProvinsi.Items.Insert(0, new ListItem { Selected = true, Text = "- Pilih Provinsi -", Value = "0" });

                PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

                var Data = db.TBPelanggans
                    .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

                if (Data != null)
                {
                    TextBoxNamaLengkap.Text = Data.NamaLengkap;
                    TextBoxAlamatEmail.Text = Data.Email;
                    TextBoxNomorTelepon.Text = Data.Handphone;

                    var Alamat = Data.TBAlamats
                        .OrderByDescending(item => item.IDAlamat)
                        .FirstOrDefault();

                    if (Alamat != null)
                    {
                        TextBoxAlamat.Text = Alamat.AlamatLengkap;
                        TextBoxKodePos.Text = Alamat.KodePos;
                    }

                    //MENCARI TRANSAKSI SESSION
                    var TransaksiECommerceDetail = db.TBTransaksiECommerceDetails
                        .Where(item => item.TBTransaksiECommerce.IDPelanggan == Pelanggan.IDPelanggan);

                    if (TransaksiECommerceDetail.Count() > 0)
                    {
                        RepeaterCart.DataSource = TransaksiECommerceDetail
                            .Select(item => new
                            {
                                item.IDTransaksiECommerceDetail,
                                Foto = "/images/cover/" + item.TBStokProduk.TBKombinasiProduk.IDProduk + ".jpg",
                                item.TBStokProduk.TBKombinasiProduk.Nama,
                                item.Quantity,
                                Harga = item.TBStokProduk.HargaJual,
                                Total = (item.Quantity * item.TBStokProduk.HargaJual)
                            })
                            .ToArray();
                        RepeaterCart.DataBind();
                    }
                    else
                        Response.Redirect("Cart.aspx");
                }
                else
                    Response.Redirect("Cart.aspx");
            }
        }
    }

    protected void ButtonKembaliKeKeranjangBelanja_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cart.aspx");
    }

    protected void ButtonLanjutkanKePengiriman_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

            var Data = db.TBPelanggans
                .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

            if (Data != null)
            {
                //IDGrupPelanggan

                Data.NamaLengkap = TextBoxNamaLengkap.Text;
                Data.Email = TextBoxAlamatEmail.Text;
                Data.Handphone = TextBoxNomorTelepon.Text;
                Data.Username = TextBoxAlamatEmail.Text;

                var Alamat = Data.TBAlamats
                    .OrderByDescending(item => item.IDAlamat)
                    .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(Alamat.AlamatLengkap))
                {
                    //IDAlamat
                    //IDPelanggan
                    //IDNegara
                    //IDProvinsi
                    //IDKota

                    Alamat.NamaLengkap = TextBoxNamaLengkap.Text;
                    Alamat.AlamatLengkap = TextBoxAlamat.Text;
                    Alamat.KodePos = TextBoxKodePos.Text;
                    Alamat.Handphone = TextBoxNomorTelepon.Text;

                    Alamat.Negara = DropDownListNegara.SelectedItem.Text;
                    Alamat.Provinsi = DropDownListProvinsi.SelectedItem.Text;
                    Alamat.Kota = DropDownListKota.SelectedItem.Text;
                    Alamat.Kecamatan = DropDownListKecamatan.SelectedItem.Text;

                    //Keterangan
                    //BiayaPengiriman
                    //TeleponLain
                }
                else
                {
                    Data.TBAlamats.Add(new TBAlamat
                    {
                        //IDAlamat
                        //IDPelanggan
                        //IDNegara
                        //IDProvinsi
                        //IDKota

                        NamaLengkap = TextBoxNamaLengkap.Text,
                        AlamatLengkap = TextBoxAlamat.Text,
                        KodePos = TextBoxKodePos.Text,
                        Handphone = TextBoxNomorTelepon.Text,

                        Negara = DropDownListNegara.SelectedItem.Text,
                        Provinsi = DropDownListProvinsi.SelectedItem.Text,
                        Kota = DropDownListKota.SelectedItem.Text,
                        Kecamatan = DropDownListKecamatan.SelectedItem.Text,

                        //Keterangan
                        //BiayaPengiriman
                        //TeleponLain
                    });
                }

                db.SubmitChanges();

                //MENCARI TRANSAKSI SESSION
                var TransaksiECommerce = db.TBTransaksiECommerces
                    .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

                var TotalBerat = TransaksiECommerce.TBTransaksiECommerceDetails
                        .Sum(item => item.Quantity * item.TBStokProduk.TBKombinasiProduk.Berat).Value;

                TotalBerat = TotalBerat * 1000; //KONVERSI KE GRAM

                //REQUEST DATA BIAYA
                using (WebClient webClient = new WebClient())
                {
                    var Values = new NameValueCollection();
                    Values["key"] = "4fd02ed39a703a1d052da67aebdf8d2d";

                    Values["origin"] = "23";
                    Values["originType"] = "city";

                    Values["destination"] = DropDownListKota.SelectedValue;
                    Values["destinationType"] = "city";

                    Values["weight"] = TotalBerat.ToString(); //GRAM
                    Values["courier"] = "jne:jnt:sicepat"; // //jne, pos, tiki, rpx, esl, pcp, pandu, wahana, sicepat, jnt, pahala, cahaya, sap, jet, indah, dse, slis, first, ncs, star, nss

                    var Respose = webClient.UploadValues(new Uri("https://pro.rajaongkir.com/api/cost"), Values);

                    string Result = Encoding.Default.GetString(Respose);

                    var ResultJson = JsonConvert.DeserializeObject<RajaOngkir_Biaya.Rootobject>(Result);

                    List<string> ListKurir = new List<string>();

                    foreach (var item in ResultJson.rajaongkir.results)
                    {
                        //"code":"jne",
                        //"name":"Jalur Nugraha Ekakurir (JNE)",

                        if (item.costs.Count() > 0)
                        {
                            foreach (var item2 in item.costs)
                            {
                                //"service":"OKE",
                                //"description":"Ongkos Kirim Ekonomis",

                                if (item2.cost.Count() > 0)
                                {
                                    foreach (var item3 in item2.cost)
                                    {
                                        //"value":26000,
                                        //"etd":"6-7",
                                        //"note":""

                                        ListKurir.Add(item.name + " - " + item2.description + " " + item3.value.ToFormatHarga());
                                    }
                                }
                            }
                        }
                    }

                    RadioButtonListKurir.DataSource = ListKurir;
                    RadioButtonListKurir.DataBind();

                    FormPelanggan.Attributes.Add("style", "display:none");
                    FormPengiriman.Attributes.Remove("style");
                }
            }
            else
                Response.Redirect("_Cart.aspx");

            //LOAD DATA PELANGGAN
            LiteralNamaLengkap.Text = TextBoxNamaLengkap.Text;
            LiteralAlamat.Text = TextBoxAlamat.Text;
            LiteralKecamatan.Text = DropDownListKecamatan.SelectedItem.Text;
            LiteralKota.Text = DropDownListKota.SelectedItem.Text;
            LiteralProvinsi.Text = DropDownListProvinsi.SelectedItem.Text;
            LiteralKodePos.Text = TextBoxKodePos.Text;
            LiteralNegara.Text = DropDownListNegara.SelectedItem.Text;
            LiteralNomorTelepon.Text = TextBoxNomorTelepon.Text;
        }
    }

    protected void ButtonKembaliKeInformasiPelanggan_Click(object sender, EventArgs e)
    {

    }

    protected void ButtonLanjutkanKePembayaran_Click(object sender, EventArgs e)
    {
        if (RadioButtonListKurir.SelectedValue == "")
        {
            LiteralWarningPilihJasaPengiriman.Text = "Jasa Pengiriman belum dipilih";
            return;
        }
        else
            LiteralWarningPilihJasaPengiriman.Text = "";

        List<string> ListJenisPembayaran = new List<string>();

        ListJenisPembayaran.Add("Bank Transfer - Pembayaran Lewat ATM atau internet Banking. Verifikasi manual.");

        RadioButtonListJenisPembayaran.DataSource = ListJenisPembayaran;
        RadioButtonListJenisPembayaran.DataBind();

        FormPengiriman.Attributes.Add("style", "display:none;");
        FormPembayaran.Attributes.Remove("style");
    }

    protected void ButtonKembaliKeDetailPengiriman_Click(object sender, EventArgs e)
    {
        FormPembayaran.Attributes.Add("style", "display:none");
        FormPengiriman.Attributes.Remove("style");
    }

    protected void ButtonProsesPemesanan_Click(object sender, EventArgs e)
    {
        //if (RadioButtonListJenisPembayaran.SelectedValue == "")
        //{
        //    LiteralWarningPilihMetodePembayaran.Text = "Metode Pembayaran belum dipilih";
        //    return;
        //}
        //else
        //    LiteralWarningPilihMetodePembayaran.Text = "";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

            //MENCARI TRANSAKSI SESSION
            var TransaksiECommerce = db.TBTransaksiECommerces
                .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

            //INSERT TRANSAKSI
            Transaksi_Class Transaksi = new Transaksi_Class((int)EnumPengguna.RendyHerdiawan, 1, DateTime.Now);

            Transaksi.IDJenisTransaksi = (int)EnumJenisTransaksi.ECommerce;

            foreach (var item in TransaksiECommerce.TBTransaksiECommerceDetails)
            {
                int IDDetailTransaksi = Transaksi.TambahDetailTransaksi(item.TBStokProduk.IDKombinasiProduk, item.Quantity);

                if (item.TBStokProduk.DiscountStore != 0)
                    Transaksi.UbahPotonganHargaJualProduk(IDDetailTransaksi, item.TBStokProduk.DiscountStore.ToFormatHarga());
            }

            Transaksi.PengaturanPelanggan(Pelanggan.IDPelanggan);
            Transaksi.BiayaPengiriman = 0;

            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
            Transaksi.StatusPrint = true;

            Transaksi.ConfirmTransaksi(db);

            //MENGHAPUS DATA TRANSAKSI ECOMMERCE
            db.TBTransaksiECommerceDetails.DeleteAllOnSubmit(TransaksiECommerce.TBTransaksiECommerceDetails);
            db.TBTransaksiECommerces.DeleteOnSubmit(TransaksiECommerce);

            db.SubmitChanges();

            Response.Redirect("Thankyou.aspx?order=" + Transaksi.IDWMSTransaksi);
        }
    }

    protected void DropDownListProvinsi_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            DropDownListProvinsi.Items.Remove(DropDownListProvinsi.Items.FindByValue("0"));

            DropDownListKota.DataSource = db.TBKurirKotas
                .Where(item => item.IDKurirProvinsi == DropDownListProvinsi.SelectedValue.ToInt())
                .OrderBy(item => item.Nama)
                .ToArray();
            DropDownListKota.DataValueField = "IDKurirKota";
            DropDownListKota.DataTextField = "Nama";
            DropDownListKota.DataBind();
            DropDownListKota.Items.Insert(0, new ListItem { Selected = true, Text = "- Pilih Kota -", Value = "0" });
        }
    }

    protected void DropDownListKota_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListKota.Items.Remove(DropDownListKota.Items.FindByValue("0"));

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var CountKecamatan = db.TBKurirKecamatans
                    .Where(item => item.IDKurirKota == DropDownListKota.SelectedValue.ToInt())
                    .Count();

            if (CountKecamatan == 0)
            {
                string result = "";

                using (WebClient webClient = new WebClient())
                {
                    result = webClient.DownloadString("https://pro.rajaongkir.com/api/subdistrict?key=4fd02ed39a703a1d052da67aebdf8d2d&city=" + DropDownListKota.SelectedValue);
                }

                var ListData = JsonConvert.DeserializeObject<RajaOngkir_Kecamatan.Rootobject>(result);

                db.TBKurirKecamatans.InsertAllOnSubmit(ListData.rajaongkir.results.Select(item => new TBKurirKecamatan
                {
                    IDKurirKecamatan = item.subdistrict_id,
                    IDKurirKota = item.city_id,
                    Nama = item.subdistrict_name.ToUpper(),
                    _IDPenggunaInsert = 1,
                    _IDPenggunaUpdate = 1,
                    _IDTempatInsert = 1,
                    _IDTempatUpdate = 1,
                    _IDWMSKurirKecamatan = Guid.NewGuid(),
                    _IDWMSStore = Guid.Parse("96220AC7-7CE3-41F7-A159-2E2E64A3A9BA"),
                    _IsActive = true,
                    _TanggalInsert = DateTime.Now,
                    _TanggalUpdate = DateTime.Now,
                    _Urutan = 1
                }));

                db.SubmitChanges();
            }

            var DataKecamatan = db.TBKurirKecamatans
                    .Where(item => item.IDKurirKota == DropDownListKota.SelectedValue.ToInt())
                    .ToArray();

            DropDownListKecamatan.DataSource = DataKecamatan;
            DropDownListKecamatan.DataValueField = "IDKurirKecamatan";
            DropDownListKecamatan.DataTextField = "Nama";
            DropDownListKecamatan.DataBind();
            DropDownListKecamatan.Items.Insert(0, new ListItem { Selected = true, Text = "- Pilih Kecamatan -", Value = "0" });
        }
    }

    protected void DropDownListKecamatan_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListKecamatan.Items.Remove(DropDownListKecamatan.Items.FindByValue("0"));
    }
}