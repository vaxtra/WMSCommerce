using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_Manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBPengguna[] daftarPengguna = db.TBPenggunas.Where(item => item.IDGrupPengguna > 1).OrderBy(item => item.NamaLengkap).ToArray();

                DropDownListPengguna.DataSource = daftarPengguna;
                DropDownListPengguna.DataTextField = "NamaLengkap";
                DropDownListPengguna.DataValueField = "IDPengguna";
                DropDownListPengguna.DataBind();

                LoadPIC(db, daftarPengguna);
            }
        }
    }

    protected void DropDownListPengguna_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPengguna[] daftarPengguna = db.TBPenggunas.Where(item => item.IDGrupPengguna > 1).OrderBy(item => item.NamaLengkap).ToArray();

            LoadPIC(db, daftarPengguna);
        }
    }

    private void LoadPIC(DataClassesDatabaseDataContext db, TBPengguna[] daftarPengguna)
    {
        TBPengguna pengguna = db.TBPenggunas.FirstOrDefault(item => item.IDPengguna == DropDownListPengguna.SelectedValue.ToInt());
        RepeaterPengguna.DataSource = daftarPengguna
        .Select(item => new
        {
            Sendiri = item.IDPenggunaParent == null || item.IDPenggunaParent == DropDownListPengguna.SelectedValue.ToInt() ? item.IDPengguna != DropDownListPengguna.SelectedValue.ToInt() ? CekAtasan(pengguna, item.IDPengguna) == false ? true : false : false : false,
            item.IDPenggunaParent,
            item.IDPengguna,
            PenggunaParent = item.IDPenggunaParent != null ? item.TBPengguna1.NamaLengkap : string.Empty,
            item.NamaLengkap
        });
        RepeaterPengguna.DataBind();

        foreach (RepeaterItem item in RepeaterPengguna.Items)
        {
            CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");
            HiddenField HiddenFieldIDPengguna = (HiddenField)item.FindControl("HiddenFieldIDPengguna");
            HiddenField HiddenFieldIDPenggunaParent = (HiddenField)item.FindControl("HiddenFieldIDPenggunaParent");

            if (CheckBoxPilih.Visible == true)
            {
                if (HiddenFieldIDPenggunaParent.Value == DropDownListPengguna.SelectedValue)
                    CheckBoxPilih.Checked = true;
                else
                    CheckBoxPilih.Checked = false;
            }
        }

        Pengguna dmPengguna = new Pengguna();
        List<Pengguna> daftarBawahan = dmPengguna.CariBawahanSemua(pengguna);

        RepeaterBawahan.DataSource = daftarBawahan.GroupBy(item => new
        {
            item.LevelJabatan
        }).Select(item => new
        {
            item.Key.LevelJabatan,
            Body = item.Where(item2 => item2.LevelJabatan == item.Key.LevelJabatan).OrderBy(item2 => item2.NamaLengkap)
        }).OrderBy(item => item.LevelJabatan);
        RepeaterBawahan.DataBind();
    }

    private bool CekAtasan(TBPengguna pengguna, int idPenggunaBawahanBaru)
    {
        bool status = false;

        while (pengguna.IDPenggunaParent != null)
        {
            if (pengguna.IDPenggunaParent == idPenggunaBawahanBaru)
            {
                status = true;
                break;
            }
            else
                pengguna = pengguna.TBPengguna1;
        }

        return status;
    }

    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            TBPengguna[] daftarPengguna = db.TBPenggunas.Where(item => item.IDGrupPengguna >= 2).OrderBy(item => item.NamaLengkap).ToArray();
            foreach (RepeaterItem item in RepeaterPengguna.Items)
            {
                CheckBox CheckBoxPilih = (CheckBox)item.FindControl("CheckBoxPilih");
                HiddenField HiddenFieldIDPengguna = (HiddenField)item.FindControl("HiddenFieldIDPengguna");
                HiddenField HiddenFieldIDPenggunaParent = (HiddenField)item.FindControl("HiddenFieldIDPenggunaParent");

                if (CheckBoxPilih.Visible == true)
                {
                    TBPengguna pengguna = daftarPengguna.FirstOrDefault(data => data.IDPengguna == HiddenFieldIDPengguna.Value.ToInt());
                    if (CheckBoxPilih.Checked == true)
                        pengguna.IDPenggunaParent = DropDownListPengguna.SelectedValue.ToInt();
                    else
                        pengguna.IDPenggunaParent = null;
                }
            }
            db.SubmitChanges();

            LoadPIC(db, daftarPengguna);
        }
    }
}