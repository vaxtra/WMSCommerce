using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Stok_CetakStokBahanBaku : System.Web.UI.Page
{

    public Dictionary<string, dynamic> Result;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {

                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBStore store = db.TBStores.FirstOrDefault();
                TBTempat tempat = new TBTempat();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["IDTempat"]))
                    tempat = db.TBTempats.FirstOrDefault(item => item.IDTempat == Request.QueryString["IDTempat"].ToInt());
                else
                    tempat = db.TBTempats.FirstOrDefault(item => item.IDTempat == pengguna.IDTempat.ToInt());

                LabelTempatStok.Text = tempat.Nama;
                LabelNamaStore.Text = store.Nama + " - " + tempat.Nama;
                LabelAlamatStore.Text = tempat.Alamat;
                LabelTeleponStore.Text = tempat.Telepon1;
                LabelWebsite.Text = store.Website;
                HyperLinkEmail.Text = tempat.Email;
                HyperLinkEmail.NavigateUrl = tempat.Email;

                LabelTanggalPrint.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
                LabelNamaPengguna.Text = pengguna.NamaLengkap;
                LabelTempatPengguna.Text = pengguna.Tempat;

                Laporan_Class LaporanStok = new Laporan_Class(db, pengguna, DateTime.Now, DateTime.Now, false);
                Result = LaporanStok.DataStokBahanBaku(Request.QueryString["IDTempat"].ToInt(), 0, Request.QueryString["Kategori"].ToInt(), Request.QueryString["KondisiStok"], Request.QueryString["Kode"], Request.QueryString["BahanBaku"], Request.QueryString["PilihSatuan"], Request.QueryString["Status"].ToLower());

                LabelSubtotal.Text = "Subtotal : " + Parse.ToFormatHarga(Result["Subtotal"]);
                RepeaterStokBahanBakuBisaDihitung.DataSource = Result["Data"];
                RepeaterStokBahanBakuBisaDihitung.DataBind();
            }
        }
    }
}