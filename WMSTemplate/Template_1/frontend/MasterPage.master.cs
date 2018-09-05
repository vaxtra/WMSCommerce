using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frontend_MasterPage : System.Web.UI.MasterPage
{
    int IDGrupPelanggan = 3; //GUEST
    int IDPengguna = 1; //DEFAULT PENGGUNA
    int IDTempat = 1; //DEFAULT TEMPAT

    protected void Page_Init(object sender, EventArgs e)
    {
        #region VALIDASI FITUR ECOMMERCE
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            StoreKonfigurasi_Class StoreKonfigurasi_Class = new StoreKonfigurasi_Class();

            if (!bool.Parse(StoreKonfigurasi_Class.Pengaturan(db, EnumStoreKonfigurasi.FiturECommerce)))
                Response.Redirect("/WITAdministrator/Default.aspx");
        }
        #endregion

        //CEK APAKAH SESSION ADA
        if (Session["PelangganLogin"] == null)
        {
            if (Request.Cookies["WMSCommerce"] != null)
            {
                //JIKA MEMILIKI COOKIES
                //LOGIN PELANGGAN MENGGUNAKAN IDWMSPelangganEnkripsi
                PelangganLogin Pelanggan = new PelangganLogin(Request.Cookies["WMSCommerce"].Value);

                if (Pelanggan.IDPelanggan > 0)
                {
                    Session["PelangganLogin"] = Pelanggan;
                    Session.Timeout = 525000;

                    //MEMBUAT COOKIES ENCRYPT
                    Response.Cookies["WMSCommerce"].Value = Pelanggan.IDWMSPelangganEnkripsi;
                    Response.Cookies["WMSCommerce"].Expires = DateTime.Now.AddYears(1);
                }
                else
                    MembuatPelanggan(); //PELANGGAN TIDAK DITEMUKAN
            }
            else
                MembuatPelanggan(); //JIKA COOKIES TIDAK ADA

        }

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PelangganLogin PelangganLogin = (PelangganLogin)Session["PelangganLogin"];
            //MENCARI TRANSAKSI SESSION
            var TransaksiECommerceDetail = db.TBTransaksiECommerceDetails
                .Where(item => item.TBTransaksiECommerce.IDPelanggan == PelangganLogin.IDPelanggan);

            LiteralTotalQuantity.Text = TransaksiECommerceDetail.Count().ToString();
        }

        Session.Timeout = 525000;

        ////////////PelangganLogin PelangganLogin = (PelangganLogin)Session["PelangganLogin"];
        ////////////Response.Write("_IDWMSPelangan : " + PelangganLogin.IDWMSPelanggan);
        ////////////Response.Write("<br/>IDPelanggan : " + PelangganLogin.IDPelanggan + "<br/><br/>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    private void MembuatPelanggan()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            #region MEMBUAT PELANGGAN
            //DEFAULT PENGGUNA
            PenggunaLogin Pengguna = new PenggunaLogin(IDPengguna, IDTempat);

            Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db, Pengguna);
            Alamat_Class ClassAlamat = new Alamat_Class();

            //MENAMBAH DATA PELANGGAN
            var Pelanggan = ClassPelanggan.Tambah(
                    IDGrupPelanggan: IDGrupPelanggan,
                    IDPenggunaPIC: IDPengguna,
                    NamaLengkap: "",
                    Username: "",
                    Password: "",
                    Email: "",
                    Handphone: "",
                    TeleponLain: "",
                    TanggalLahir: DateTime.Now,
                    Deposit: 0,
                    Catatan: "",
                    _IsActive: true
                    );

            //MENAMBAH DATA ALAMAT
            ClassAlamat.Tambah(db, 0, Pelanggan, "", "", 0, "");
            db.SubmitChanges();
            #endregion

            PelangganLogin PelangganLogin = new PelangganLogin(Pelanggan._IDWMS);

            Session["PelangganLogin"] = PelangganLogin;
            Session.Timeout = 525000;

            //MEMBUAT COOKIES ENCRYPT
            Response.Cookies["WMSCommerce"].Value = PelangganLogin.IDWMSPelangganEnkripsi;
            Response.Cookies["WMSCommerce"].Expires = DateTime.Now.AddYears(1);
        }
    }
}