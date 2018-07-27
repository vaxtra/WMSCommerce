using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RendFramework;

public partial class WITAdministrator_Pengguna_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LiteralWarning.Text = string.Empty;

        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadDataPegawai(db);
                LoadDataGrup(db);
            }
        }
    }

    #region PEGAWAI
    protected void EventData(object sender, EventArgs e)
    {
        try
        {
            LoadDataPegawai(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            DataDisplay DataDisplay = new DataDisplay();

            if (DataDisplay.Prev(DropDownListHalaman))
                LoadDataPegawai(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void ButtonNext_Click(object sender, EventArgs e)
    {
        try
        {
            DataDisplay DataDisplay = new DataDisplay();

            if (DataDisplay.Next(DropDownListHalaman))
                LoadDataPegawai(new DataClassesDatabaseDataContext());
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }
    private void LoadDataPegawai(DataClassesDatabaseDataContext db)
    {
        DataDisplay DataDisplay = new DataDisplay();
        var ListData = db.TBPenggunas
            .Where(item => item.IDPengguna != 1 && (!string.IsNullOrWhiteSpace(TextBoxCari.Text) ? item.NamaLengkap.ToLower().Contains(TextBoxCari.Text.ToLower()) : true))
            .Select(item => new
            {
                Status = item._IsActive,
                item.NamaLengkap,
                GrupPengguna = item.TBGrupPengguna.Nama,
                Tempat = item.TBTempat.Nama,
                item.Username,
                item.Handphone,
                item.IDPengguna
            }).OrderBy(item => item.NamaLengkap).ToArray();

        int skip = 0;
        int take = 0;
        int count = ListData.Count();

        DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

        RepeaterPengguna.DataSource = ListData.Skip(skip).Take(take);
        RepeaterPengguna.DataBind();
    }
    protected void RepeaterPengguna_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "UbahStatus")
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (Pengguna.IDPengguna != e.CommandArgument.ToInt())
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var DataPengguna = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == e.CommandArgument.ToInt());

                    if (DataPengguna != null)
                        DataPengguna._IsActive = !DataPengguna._IsActive;

                    db.SubmitChanges();

                    LoadDataPegawai(db);
                }
            }
            else //Tidak bisa merubah status diri sendiri
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Tidak bisa merubah status pegawai");
        }
        else if (e.CommandName == "Hapus")
        {
            PenggunaLogin _pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (_pengguna.IDPengguna != e.CommandArgument.ToInt())
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var DataPengguna = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == e.CommandArgument.ToInt());

                    if (DataPengguna.TBJurnals.Count == 0 &&
                        DataPengguna.TBPerpindahanStokBahanBakus.Count == 0 &&
                        DataPengguna.TBPerpindahanStokProduks.Count == 0 &&
                        DataPengguna.TBPesanPrints.Count == 0 &&
                        DataPengguna.TBSoals.Count == 0 &&
                        DataPengguna.TBTransaksiJenisPembayarans.Count == 0 &&
                        DataPengguna.TBTransaksis.Count == 0 &&
                        DataPengguna.TBTransaksis1.Count == 0 &&
                        DataPengguna.TBTransaksis2.Count == 0 &&
                        DataPengguna.TBTransferBahanBakus.Count == 0 &&
                        DataPengguna.TBTransferBahanBakus1.Count == 0 &&
                        DataPengguna.TBTransferProduks.Count == 0 &&
                        DataPengguna.TBTransferProduks1.Count == 0 &&
                        DataPengguna.TBWaitingLists.Count == 0)
                    {
                        db.TBLogPenggunas.DeleteAllOnSubmit(DataPengguna.TBLogPenggunas);
                        db.TBRelasiPenggunaMenus.DeleteAllOnSubmit(DataPengguna.TBRelasiPenggunaMenus);
                        db.TBPenggunas.DeleteOnSubmit(DataPengguna);

                        db.SubmitChanges();
                    }
                    else
                        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Tidak bisa menghapus pegawai");

                    LoadDataPegawai(db);
                }
            }
            else //Tidak bisa menghapus diri sendiri
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Tidak bisa menghapus pegawai");
        }
        else if (e.CommandName == "Login")
        {
            PenggunaLogin _pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (_pengguna.IDGrupPengguna == 1)
            {
                //JIKA COOKIES ADA
                PenggunaLogin Pengguna = new PenggunaLogin(e.CommandArgument.ToString(), false);

                if (Pengguna.IDPengguna > 0)
                {
                    Session["PenggunaLogin"] = Pengguna;
                    Session.Timeout = 525000;

                    //MEMBUAT COOKIES ENCRYPT
                    Response.Cookies["WITEnterpriseSystem"].Value = Pengguna.EnkripsiIDPengguna;
                    Response.Cookies["WITEnterpriseSystem"].Expires = DateTime.Now.AddYears(1);

                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        var GrupPengguna = db.TBGrupPenggunas
                            .FirstOrDefault(item => item.IDGrupPengguna == Pengguna.IDGrupPengguna);

                        if (GrupPengguna != null && !string.IsNullOrWhiteSpace(GrupPengguna.DefaultURL))
                            Response.Redirect(GrupPengguna.DefaultURL);
                        else
                            Response.Redirect("/WITAdministrator/Default.aspx");
                    }
                }
            }
        }
    }
    protected void RepeaterPengguna_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
        Button ButtonLogin = (Button)e.Item.FindControl("ButtonLogin");

        if (Pengguna.IDGrupPengguna == (int)EnumGrupPengguna.SuperAdministrator)
            ButtonLogin.Visible = true;
        else
            ButtonLogin.Visible = false;
    }
    #endregion

    #region GRUP
    private void LoadDataGrup(DataClassesDatabaseDataContext db)
    {
        RepeaterGrupPengguna.DataSource = db.TBGrupPenggunas.ToArray();
        RepeaterGrupPengguna.DataBind();
    }

    protected void RepeaterGrupPengguna_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBGrupPengguna grupPengguna = db.TBGrupPenggunas.FirstOrDefault(item => item.IDGrupPengguna == e.CommandArgument.ToInt());

                HiddenFieldIDGrupPengguna.Value = grupPengguna.IDGrupPengguna.ToString();
                TextBoxNama.Text = grupPengguna.Nama;
                TextBoxDefaultURL.Text = grupPengguna.DefaultURL;

                ButtonSimpanGrup.Text = "Ubah";
            }
        }
        else if (e.CommandName == "Hapus")
        {
            bool status = false;

            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                try
                {
                    var GrupPengguna = db.TBGrupPenggunas.FirstOrDefault(item => item.IDGrupPengguna == e.CommandArgument.ToInt());
                    db.TBGrupPenggunas.DeleteOnSubmit(GrupPengguna);
                    db.SubmitChanges();
                    status = true;
                }
                catch (Exception)
                {

                }
                finally
                {
                    if (status)
                        LoadDataGrup(db);
                    else
                        LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Data tidak bisa dihapus");
                }
            }
        }
    }

    protected void ButtonSimpanGrup_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (ButtonSimpanGrup.Text == "Tambah")
                db.TBGrupPenggunas.InsertOnSubmit(new TBGrupPengguna { Nama = TextBoxNama.Text, DefaultURL = TextBoxDefaultURL.Text });
            else if (ButtonSimpanGrup.Text == "Ubah")
            {
                TBGrupPengguna grupPengguna = db.TBGrupPenggunas.FirstOrDefault(item => item.IDGrupPengguna == HiddenFieldIDGrupPengguna.Value.ToInt());
                grupPengguna.Nama = TextBoxNama.Text;
                grupPengguna.DefaultURL = TextBoxDefaultURL.Text;
            }
            db.SubmitChanges();

            HiddenFieldIDGrupPengguna.Value = null;
            TextBoxNama.Text = string.Empty;
            TextBoxDefaultURL.Text = string.Empty;
            ButtonSimpanGrup.Text = "Tambah";

            LoadDataGrup(db);
        }
    }
    #endregion
}