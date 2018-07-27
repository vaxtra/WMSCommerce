using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Stok_Default : System.Web.UI.Page
{
    public Dictionary<string, dynamic> Result;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);

                var kategoriBahanBaku = db.TBKategoriBahanBakus.ToArray();
                var satuan = db.TBSatuans.ToArray();

                #region TEMPAT
                DropDownListTempat.DataSource = ClassTempat.Data();
                DropDownListTempat.DataTextField = "Nama";
                DropDownListTempat.DataValueField = "IDTempat";
                DropDownListTempat.DataBind();
                DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();
                #endregion

                #region Bahan Baku Bisa Dihitung
                DropDownListKondisiStokBahanBaku.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });
                DropDownListKondisiStokBahanBaku.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                DropDownListKondisiStokBahanBaku.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                DropDownListKondisiStokBahanBaku.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                DropDownListCariKategoriBahanBaku.DataSource = kategoriBahanBaku;
                DropDownListCariKategoriBahanBaku.DataTextField = "Nama";
                DropDownListCariKategoriBahanBaku.DataValueField = "IDKategoriBahanBaku";
                DropDownListCariKategoriBahanBaku.DataBind();
                DropDownListCariKategoriBahanBaku.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });
                #endregion
            }
        }
    }

    #region Bahan Baku Bisa Dihitung
    private void LoadDataBahanBaku(bool GenerateExcel)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            //STOK BISA DIHITUNG
            Laporan_Class LaporanStok = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], DateTime.Now, DateTime.Now, GenerateExcel);
            Result = LaporanStok.DataStokBahanBaku(DropDownListTempat.SelectedValue.ToInt(), 0, DropDownListCariKategoriBahanBaku.SelectedValue.ToInt(), DropDownListKondisiStokBahanBaku.SelectedValue, TextBoxCariKodeBahanBaku.Text, TextBoxCariBahanBaku.Text, DropDownListCariSatuanBahanBaku.SelectedValue, DropDownListCariStatusBahanBaku.SelectedItem.Text.ToLower());

            LabelSubtotal.Text = Parse.ToFormatHarga(Result["Subtotal"]);
            RepeaterStokBahanBaku.DataSource = Result["Data"];
            RepeaterStokBahanBaku.DataBind();

            ButtonCetakBahanBaku.OnClientClick = "return popitup('CetakStokBahanBaku.aspx" + LaporanStok.TempPencarian + "')";

            LinkDownload.Visible = GenerateExcel;

            if (LinkDownload.Visible)
                LinkDownload.HRef = LaporanStok.LinkDownload;
        }
    }

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadDataBahanBaku(false);
    }
    protected void DropDownListCariKategoriBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataBahanBaku(false);
    }
    protected void DropDownListCariSatuanBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataBahanBaku(false);
    }
    protected void DropDownListCariStatusBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataBahanBaku(false);
    }
    protected void DropDownListJenisStokBahanBaku_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataBahanBaku(false);
    }
    #endregion
    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        LoadDataBahanBaku(true);
    }
}