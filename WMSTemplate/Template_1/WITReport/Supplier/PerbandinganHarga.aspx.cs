using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_Supplier_PerbandinganHarga : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListCariBahanBaku.DataSource = db.TBBahanBakus.OrderBy(item => item.Nama).ToArray();
                DropDownListCariBahanBaku.DataValueField = "IDBahanBaku";
                DropDownListCariBahanBaku.DataTextField = "Nama";
                DropDownListCariBahanBaku.DataBind();
                DropDownListCariBahanBaku.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });

                DropDownListCariSatuan.DataSource = db.TBBahanBakus.Select(item => item.TBSatuan1).Distinct().OrderBy(item => item.Nama).ToArray();
                DropDownListCariSatuan.DataValueField = "IDSatuan";
                DropDownListCariSatuan.DataTextField = "Nama";
                DropDownListCariSatuan.DataBind();
                DropDownListCariSatuan.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });
            }

            LoadData();
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], Pengaturan.HariIni()[0], Pengaturan.HariIni()[1], false);

            var Result = Laporan_Class.PerbandinganHargaSupplier(pengguna.IDTempat, DropDownListCariBahanBaku.SelectedValue.ToInt(), DropDownListCariSatuan.SelectedValue.ToInt());

            LiteralColspan.Text = "<td colspan='" + Result["DataJumlahSupplier"].ToString() + "'></td>";

            RepeaterSupplier.DataSource = Result["DataSupplier"];
            RepeaterSupplier.DataBind();
            RepeaterBahanBaku.DataSource = Result["DataHargaSupplier"];
            RepeaterBahanBaku.DataBind();
        }
    }

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
}