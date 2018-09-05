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
using System.IO;
using System.Net.Mail;

public partial class CheckOut : System.Web.UI.Page
{
    decimal BiayaKirim = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //HIDE WARNING VALIDASI DI AWAL
            StatusValidasi.Visible = false;
            PengirimanValidasi.Visible = false;
            PembayaranValidasi.Visible = false;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {

                //VALIDASI STOK PRODUK
                ValidasiStokProdukTransaksi(db);

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
                        //DropDownListProvinsi.Items.FindByValue(Alamat.IDProvinsi.ToString()).Selected = true;
                        //DropDownListKota.Items.FindByText(Alamat.IDKota.ToString()).Selected = true;
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
        if (DropDownListProvinsi.SelectedValue != "0" && DropDownListKota.SelectedValue != "0" && DropDownListKecamatan.SelectedValue != "0")
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

                        //Alamat.IDProvinsi = DropDownListProvinsi.SelectedValue.ToInt();
                        //Alamat.IDKota = DropDownListProvinsi.SelectedValue.ToInt();
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
                                            //ListKurir.Add(item.name + " - " + item2.description + " " + item3.value.ToFormatHarga());
                                            RadioButtonListKurir.Items.Add(new ListItem("<img width='150' style='margin:0 20px;' src='./frontend/assets/shipping-logo/" + item.code.ToLower().Replace("j&t", "jnt") + ".png' />" + item.name + " - " + item2.description + " " + item3.value.ToFormatHarga(), item3.value.ToString()));
                                        }
                                    }
                                }
                            }
                        }

                        //RadioButtonListKurir.DataSource = ListKurir;
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
            StatusValidasi.Visible = false;
        }
        else
        {
            LiteralStatusValidasi.Text = "Mohon cek kembali Provinsi / Kota / Kecamatan anda";
            StatusValidasi.Visible = true;
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
            PengirimanValidasi.Visible = true;
            return;
        }
        else
        {
            LiteralWarningPilihJasaPengiriman.Text = "";
            PengirimanValidasi.Visible = false;
        }
           

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

    public void ValidasiStokProdukTransaksi(DataClassesDatabaseDataContext db)
    {
        PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

        //MENCARI TRANSAKSI SESSION
        var TransaksiECommerce = db.TBTransaksiECommerces
            .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

        if (TransaksiECommerce != null)
        {
            StokProduk_Class ClassStokProduk = new StokProduk_Class(db);

            var Result = ClassStokProduk.ValidasiStokProdukTransaksi(TransaksiECommerce.TBTransaksiECommerceDetails.ToArray());

            if (Result.Count > 0)
                Response.Redirect("Cart.aspx");
        }
        else
        {
            Response.Redirect("Cart.aspx");
        }
        
    }

    protected void ButtonProsesPemesanan_Click(object sender, EventArgs e)
    {
        if (RadioButtonListJenisPembayaran.SelectedValue == "")
        {
            LiteralWarningPilihMetodePembayaran.Text = "Metode Pembayaran belum dipilih";
            PembayaranValidasi.Visible = true;
            return;
        }
        else
        {
            LiteralWarningPilihMetodePembayaran.Text = "";
            PembayaranValidasi.Visible = false;
        }
            

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            //VALIDASI STOK PRODUK
            ValidasiStokProdukTransaksi(db);

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
            Transaksi.BiayaPengiriman = RadioButtonListKurir.SelectedValue.ToDecimal();

            Transaksi.IDStatusTransaksi = (int)EnumStatusTransaksi.AwaitingPayment;
            Transaksi.StatusPrint = true;

            Transaksi.ConfirmTransaksi(db);

            //KIRIM EMAIL NOTIFIKASI
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/frontend/assets/email-template/awaiting-payment.html")))
            {

                string body = "";
                string listProduk = "";
                body = reader.ReadToEnd();
                body = body.Replace("{nama_customer}", TransaksiECommerce.TBPelanggan.NamaLengkap);
                body = body.Replace("{nomor_order}", Transaksi.IDTransaksi);
                foreach (var item in TransaksiECommerce.TBTransaksiECommerceDetails)
                {
                    listProduk += "<tr><td>" + item.TBStokProduk.TBKombinasiProduk.Nama + "</td><td>" + item.TBStokProduk.HargaJual.ToFormatHarga() + "</td><td>" + item.Quantity + "</td><td style='text-align:right;'>" + (item.Quantity * item.TBStokProduk.HargaJual).ToFormatHarga() + "</td></tr>";
                }
                body = body.Replace("{list_produk}", listProduk);
                body = body.Replace("{subtotal}", Transaksi.Subtotal.ToFormatHarga());
                body = body.Replace("{biaya_pengiriman}", Transaksi.BiayaPengiriman.ToString().ToFormatHarga());
                body = body.Replace("{grand_total}", Transaksi.GrandTotal.ToFormatHarga());
                body = body.Replace("{nama_toko}", "Trendsetter");
                body = body.Replace("{logo_email}", "http://ecommerce.wit.co.id/assets/images/email_logo/email_logo.png");
                body = body.Replace("{url_konfirmasi}", "http://wit.co.id");
                body = body.Replace("{url_website}", "http://localhost:54517/");
                SendEmail(TransaksiECommerce.TBPelanggan.Email, "Trendsetter", "Order Notification", body);
            }

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
            DropDownListKota.Visible = true;
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
            DropDownListKecamatan.Visible = true;
        }
    }

    protected void DropDownListKecamatan_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListKecamatan.Items.Remove(DropDownListKecamatan.Items.FindByValue("0"));
    }

    protected void SendEmail(string email, string display_name, string subject, string body)
    {
        string sender = "ecommerce@wit.co.id";
        string passEmail = "empatTH3010*#";
        string smtpHost = "mail.wit.co.id";
        int smtpPort = 25;
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(sender, display_name);
        msg.To.Add(new MailAddress(email));
        msg.Subject = subject;
        msg.Body = body;
        msg.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
        smtp.Credentials = new NetworkCredential(sender, passEmail);
        smtp.Send(msg);
    }
}