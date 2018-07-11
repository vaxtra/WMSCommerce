using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_JurnalUmumPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Akun"] != null && Request.QueryString["Pengguna"] != null &&
                Request.QueryString["Periode1"] != null && Request.QueryString["Periode2"] != null)
            {
                LoadData();
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Jurnal_Class Jurnal_Class = new Jurnal_Class();
            var Akun = Request.QueryString["Akun"];
            var DataPenggunaAkuntansi = Request.QueryString["Pengguna"];
            var Periode1 = Request.QueryString["Periode1"];
            var Periode2 = Request.QueryString["Periode2"];

            #region DEFAULT
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            TBStore _store = db.TBStores.FirstOrDefault();

            LabelPeriode.Text = Periode1 + " - " + Periode2;
            LabelNamaPencetak.Text = Pengguna.NamaLengkap;
            LabelTanggalCetak.Text = DateTime.Now.ToString("d MMMM yyyy");

            LabelNamaStore.Text = _store.Nama;
            LabelAlamatStore.Text = _store.Alamat;
            LabelTeleponStore.Text = _store.TeleponLain + " / " + _store.Handphone;
            LabelWebsite.Text = _store.Website;
            #endregion

            if (Akun == "0")
            {
                if (DataPenggunaAkuntansi == "0")
                {
                    RepeaterJurnal.DataSource = Jurnal_Class.Data(db)
                    .Where(item =>
                        item.Tanggal.Value.Date >= Periode1.ToDateTime() &&
                        item.Tanggal.Value.Date <= Periode2.ToDateTime() &&
                        item.IDTempat == Pengguna.IDTempat);
                }
                else
                {
                    RepeaterJurnal.DataSource = Jurnal_Class.Data(db)
                                        .Where(item =>
                                            item.Tanggal.Value.Date >= Periode1.ToDateTime() &&
                                            item.Tanggal.Value.Date <= Periode2.ToDateTime() &&
                                            item.IDTempat == Pengguna.IDTempat &&
                                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.TBJurnal.IDPengguna == DataPenggunaAkuntansi.ToInt()) != null);
                }
            }
            else
            {
                if (DataPenggunaAkuntansi == "0")
                {
                    RepeaterJurnal.DataSource = Jurnal_Class.Data(db)
                    .Where(item =>
                        item.Tanggal.Value.Date >= Periode1.ToDateTime() &&
                        item.Tanggal.Value.Date <= Periode2.ToDateTime() &&
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TBJurnalDetails.FirstOrDefault(item2 => item2.IDAkun == Akun.ToInt()) != null);
                }
                else
                {
                    RepeaterJurnal.DataSource = Jurnal_Class.Data(db)
                    .Where(item =>
                        item.Tanggal.Value.Date >= Periode1.ToDateTime() &&
                        item.Tanggal.Value.Date <= Periode2.ToDateTime() &&
                        item.TBJurnalDetails.FirstOrDefault(item2 => item2.IDAkun == Akun.ToInt()) != null &&
                        item.IDTempat == Pengguna.IDTempat &&
                        item.TBJurnalDetails.FirstOrDefault(item2 => item2.TBJurnal.IDPengguna == DataPenggunaAkuntansi.ToInt()) != null);
                }
            }

            RepeaterJurnal.DataBind();
        }
    }
}
