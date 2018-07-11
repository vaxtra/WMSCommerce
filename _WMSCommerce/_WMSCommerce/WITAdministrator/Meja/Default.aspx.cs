using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Meja_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadData(db);
            }
        }
    }

    private void LoadData(DataClassesDatabaseDataContext db)
    {
        Meja_Class Meja_Class = new Meja_Class();

        var ListMeja = db.TBMejas
            .Where(item => item.IDMeja > 2)
            .Select(item => new
            {
                item.IDMeja,
                item.Nama,
                item.IDStatusMeja,
                Warna = WarnaMeja(item.IDStatusMeja.Value),
                item.VIP,
                item.Status,
            }).ToArray();

        var MejaReguler = ListMeja.Where(item => item.VIP == false);
        if (MejaReguler.Count() > 0)
        {
            int barisReguler = (int)Math.Ceiling((double)MejaReguler.Count() / 10);
            int[] resultReguler = new int[barisReguler];

            for (int i = 0; i < barisReguler; i++)
            {
                resultReguler[i] = i + 1;
            }

            RepeaterReguler.DataSource = resultReguler.Select(item => new
            {
                baris = MejaReguler.Skip((item * 10) - 10).Take(10)
            });
            RepeaterReguler.DataBind();
        }

        var MejaVIP = ListMeja.Where(item => item.VIP == true);
        if (MejaVIP.Count() > 0)
        {
            int barisVIP = (int)Math.Ceiling((double)MejaReguler.Count() / 5);
            int[] resultVIP = new int[barisVIP];

            for (int i = 0; i < barisVIP; i++)
            {
                resultVIP[i] = i + 1;
            }

            RepeaterVIP.DataSource = resultVIP.Select(item => new
            {
                baris = MejaVIP.Skip((item * 5) - 5).Take(5)
            });
            RepeaterVIP.DataBind();
        }
    }
    public string WarnaMeja(int idStatusMeja)
    {
        switch (idStatusMeja)
        {
            case 1: return "btn btn-outline"; //KOSONG
            case 2: return "btn btn-warning"; //ORDER TERKIRIM
            case 3: return "btn btn-success"; //TERISI
            case 4: return "btn btn-danger"; //PRESETTLEMEN BILL
            default: return "";
        }
    }

    protected void RepeaterMeja_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            if (e.CommandName == "VIP")
            {
                TBMeja meja = db.TBMejas.FirstOrDefault(item => item.IDMeja == e.CommandArgument.ToInt());
                meja.VIP = !meja.VIP;
                db.SubmitChanges();
            }
            else if (e.CommandName == "Status")
            {
                TBMeja meja = db.TBMejas.FirstOrDefault(item => item.IDMeja == e.CommandArgument.ToInt());
                meja.Status = !meja.Status;
                db.SubmitChanges();
            }
            else if (e.CommandName == "Hapus")
            {
                Meja_Class Meja_Class = new Meja_Class();
                if (Meja_Class.Hapus(db, e.CommandArgument.ToInt()))
                    LoadData(db);
                else
                    LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Meja tidak bisa dihapus");
            }

            LoadData(db);
        }
    }
}