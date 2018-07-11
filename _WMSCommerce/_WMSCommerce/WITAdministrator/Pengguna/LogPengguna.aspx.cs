using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_LogPengguna : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxCarTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");

            LoadData();
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterLogPengguna.DataSource = db.TBLogPenggunas.AsEnumerable().Where(item => item.Tanggal.Value.Date == TextBoxCarTanggal.Text.ToDateTime().Date).Select(item => new
            {
                item.TBPengguna.NamaLengkap,
                Tanggal = item.Tanggal.Value.ToString("d MMMM yyyy"),
                Hari = item.Tanggal.Value.ToString("dddd"),
                Jam = item.Tanggal.Value.ToString("HH:mm"),
                Status = item.IDLogPenggunaTipe == 1 ? "<label class=\"label label-success\">Login</label>" : "<label class=\"label label-danger\">Logout</label>"
            }).OrderBy(item => item.NamaLengkap);
            RepeaterLogPengguna.DataBind();
        }
    }

    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}