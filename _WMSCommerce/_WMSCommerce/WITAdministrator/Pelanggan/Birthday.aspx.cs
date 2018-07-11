using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RendFramework;

public partial class WITAdministrator_Pelanggan_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownListBulan.DataSource = Akuntansi_Class.DropdownlistBulanLaporan();
            DropDownListBulan.DataValueField = "Value";
            DropDownListBulan.DataTextField = "Text";
            DropDownListBulan.DataBind();
            DropDownListBulan.SelectedValue = (DateTime.Now.Month).ToString();

            LoadData();
        }
    }

    protected void RepeaterPelanggan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    private void LoadData()
    {
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DataDisplay DataDisplay = new DataDisplay();

                TBPelanggan[] ListData = null;

                ListData = db.TBPelanggans
                    .Where(item =>
                        item.TanggalLahir.HasValue &&
                        item.TanggalLahir.Value.Month == DropDownListBulan.SelectedValue.ToInt() &&
                        item.NamaLengkap.ToLower().Contains(TextBoxCari.Text.ToLower()))
                    .OrderBy(item => item.TanggalLahir.Value.Day).ToArray();

                int skip = 0;
                int take = 0;

                DataDisplay.Proses(ListData.Count(), DropDownListHalaman, DropDownListJumlahData, out take, out skip);

                RepeaterPelanggan.DataSource = ListData
                    .Skip(skip)
                    .Take(take)
                    .Select(item => new
                    {
                        Grup = item.TBGrupPelanggan.Nama,
                        item.NamaLengkap,
                        item.Email,
                        item.Handphone,
                        item.TanggalLahir,
                        Status = item._IsActive,
                        Transaksi = item.TBTransaksis.Where(item2 => item2.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).Count(),
                        Nominal = item.TBTransaksis.Where(item2 => item2.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).Sum(item2 => item2.GrandTotal),
                        Quantity = item.TBTransaksis.Where(item2 => item2.IDStatusTransaksi == (int)EnumStatusTransaksi.Complete).Sum(item2 => item2.JumlahProduk)
                    })
                    .ToArray();
                RepeaterPelanggan.DataBind();
            }
        }
        catch (Exception ex)
        {
            AlertMessage_Class.ShowException(this, ex, Request.Url.PathAndQuery);
        }
    }

    protected void EventData(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonPrevious_Click(object sender, EventArgs e)
    {
        DataDisplay DataDisplay = new DataDisplay();

        if (DataDisplay.Prev(DropDownListHalaman))
            LoadData();
    }
    protected void ButtonNext_Click(object sender, EventArgs e)
    {
        DataDisplay DataDisplay = new DataDisplay();

        if (DataDisplay.Next(DropDownListHalaman))
            LoadData();
    }
}