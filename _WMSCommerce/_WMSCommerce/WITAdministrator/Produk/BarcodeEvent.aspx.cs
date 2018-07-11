using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Barcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HiddenFieldID.Value = Request.QueryString["id"];
            LoadData();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        int id = HiddenFieldID.Value.ToInt() + 1;

        HiddenFieldID.Value = id.ToString();

        LoadData();

        ScriptManager.RegisterStartupScript(
            UpdatePanel1,
            this.GetType(),
            "MyAction",
            "window.print();",
            true);
    }

    private void LoadData()
    {
        int id = HiddenFieldID.Value.ToInt();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var KombinasiProduk = db.TBKombinasiProduks.FirstOrDefault(item => item.IDKombinasiProduk == id);

            List<dynamic> ListBarcode = new List<dynamic>();

            if (KombinasiProduk != null)
            {
                if (Session["PenggunaLogin"] != null)
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    for (int i = 0; i < 3; i++)
                        ListBarcode.Add(new
                        {
                            Kode = KombinasiProduk.KodeKombinasiProduk
                        });

                    RepeaterBarcode.DataSource = ListBarcode;
                    RepeaterBarcode.DataBind();
                }
                else
                    Response.Redirect("/WITAdminis/Default.aspx");
            }
            else
                Response.Redirect("/WITWarehouse/Default.aspx");
        }
    }
}