using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_JenisBiayaProyeksi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterJenisBiayaProyeksi.DataSource = db.TBJenisBiayaProyeksis.ToArray();
            RepeaterJenisBiayaProyeksi.DataBind();

            var jenisBiayaProyeksiDetail = db.TBJenisBiayaProyeksiDetails.AsEnumerable().Select(item => new
            {
                Nama = item.TBJenisBiayaProyeksi.Nama,
                Jenis = item.EnumBiayaProyeksi == (int)PilihanBiayaProyeksi.Persen ? "Persentase" : "Nominal",
                Biaya = item.EnumBiayaProyeksi == (int)PilihanBiayaProyeksi.Persen ? (item.Persentase * 100).ToFormatHarga() + "%" : item.Nominal.ToFormatHarga(),
                StatusBatas = item.StatusBatas
            });

            RepeaterBatasBawah.DataSource = jenisBiayaProyeksiDetail.Where(item => item.StatusBatas == (int)PilihanStatusBatasProyeksi.BatasBawah);
            RepeaterBatasBawah.DataBind();

            RepeaterBatasTengah.DataSource = jenisBiayaProyeksiDetail.Where(item => item.StatusBatas == (int)PilihanStatusBatasProyeksi.BatasTengah);
            RepeaterBatasTengah.DataBind();

            RepeaterBatasAtas.DataSource = jenisBiayaProyeksiDetail.Where(item => item.StatusBatas == (int)PilihanStatusBatasProyeksi.BatasAtas);
            RepeaterBatasAtas.DataBind();
        }
    }

    protected void CustomValidatorJenisBiayaProduksiDetail_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (TextBoxJenisBiayaProyeksiDetail.Text.ToDecimal() == 0)
        {
            args.IsValid = false;
            CustomValidatorJenisBiayaProyeksiDetail.Text = "Persentase harus lebih dari 0";
        }
        else
        {
            args.IsValid = true;
            CustomValidatorJenisBiayaProyeksiDetail.Text = "-";
        }
    }
    protected void ButtonSimpanJenisBiayaProduksi_Click(object sender, EventArgs e)
    {

    }
    protected void RadioButtonListEnumBiayaProduksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListEnumBiayaProduksi.SelectedValue == "Persentase")
        {
            LabelStatusJenisBiayaProyeksiDetail.Text = "%";
        }
        else if (RadioButtonListEnumBiayaProduksi.SelectedValue == "Nominal")
        {
            LabelStatusJenisBiayaProyeksiDetail.Text = "Nominal";
        }
    }
}