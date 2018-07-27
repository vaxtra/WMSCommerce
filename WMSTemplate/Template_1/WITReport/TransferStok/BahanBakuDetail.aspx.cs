using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITReport_TransferStok_BahanBakuDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                Tempat_Class ClassTempat = new Tempat_Class(db);
                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                KategoriBahanBaku_Class KategoriBahanBaku_Class = new KategoriBahanBaku_Class();

                DropDownListCariTempatTransferDetail.Items.AddRange(ClassTempat.DataDropDownList());
                DropDownListCariTempatTransferDetail.SelectedValue = Pengguna.IDTempat.ToString();

                DropDownListCariSatuanTransferDetail.DataSource = db.TBSatuans.OrderBy(item => item.Nama).ToArray();
                DropDownListCariSatuanTransferDetail.DataValueField = "IDSatuan";
                DropDownListCariSatuanTransferDetail.DataTextField = "Nama";
                DropDownListCariSatuanTransferDetail.DataBind();
                DropDownListCariSatuanTransferDetail.Items.Insert(0, new ListItem { Text = "- Semua -", Value = "0" });

                DropDownListCariKategoriTransferDetail.Items.AddRange(KategoriBahanBaku_Class.Dropdownlist(db));

                DropDownListCariBahanBakuTransferDetail.DataSource = db.TBBahanBakus.OrderBy(item => item.Nama).ToArray();
                DropDownListCariBahanBakuTransferDetail.DataValueField = "IDBahanBaku";
                DropDownListCariBahanBakuTransferDetail.DataTextField = "Nama";
                DropDownListCariBahanBakuTransferDetail.DataBind();
                DropDownListCariBahanBakuTransferDetail.Items.Insert(0, new ListItem { Text = "-Semua-", Value = "0" });
            }

            ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
            ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];

            LoadData();
        }
    }

    #region Default
    protected void ButtonHari_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariIni()[1];
        LoadData();
    }
    protected void ButtonMinggu_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguIni()[1];
        LoadData();
    }
    protected void ButtonBulan_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanIni()[1];
        LoadData();
    }
    protected void ButtonTahun_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunIni()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunIni()[1];
        LoadData();
    }
    protected void ButtonHariSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.HariSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.HariSebelumnya()[1];
        LoadData();
    }
    protected void ButtonMingguSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.MingguSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.MingguSebelumnya()[1];
        LoadData();
    }
    protected void ButtonBulanSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.BulanSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.BulanSebelumnya()[1];
        LoadData();
    }
    protected void ButtonTahunSebelumnya_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = Pengaturan.TahunSebelumnya()[0];
        ViewState["TanggalAkhir"] = Pengaturan.TahunSebelumnya()[1];
        LoadData();
    }
    protected void ButtonCariTanggal_Click(object sender, EventArgs e)
    {
        ViewState["TanggalAwal"] = DateTime.Parse(TextBoxTanggalAwal.Text).Date;
        ViewState["TanggalAkhir"] = DateTime.Parse(TextBoxTanggalAkhir.Text).Date;
        LoadData();
    }
    #endregion

    #region Load Transfer Detail
    private void LoadData(bool GenerateExcel)
    {
        //DEFAULT
        TextBoxTanggalAwal.Text = ((DateTime)ViewState["TanggalAwal"]).ToString("d MMMM yyyy");
        TextBoxTanggalAkhir.Text = ((DateTime)ViewState["TanggalAkhir"]).ToString("d MMMM yyyy");

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], (DateTime)ViewState["TanggalAwal"], (DateTime)ViewState["TanggalAkhir"], GenerateExcel);

            if (DropDownListCariPengirimPenerimaTransferDetail.SelectedValue == "Pengirim")
            {
                var Result = Laporan_Class.TransferBahanBakuDetail(null, DropDownListCariTempatTransferDetail.SelectedValue.ToInt(), 0, 0, 0, DropDownListCariStatusTransferDetail.SelectedValue.ToInt(), TextBoxCariKodeTransferDetail.Text, DropDownListCariBahanBakuTransferDetail.SelectedValue.ToInt(), DropDownListCariSatuanTransferDetail.SelectedValue.ToInt(), DropDownListCariKategoriTransferDetail.SelectedValue.ToInt(), true);

                #region KONFIGURASI LAPORAN
                LabelPeriode.Text = Laporan_Class.Periode;

                LinkDownloadTransferDetail.Visible = GenerateExcel;

                if (LinkDownloadTransferDetail.Visible)
                    LinkDownloadTransferDetail.HRef = Laporan_Class.LinkDownload;

                ButtonPrintTransferDetail.OnClientClick = "return popitup('BahanBakuDetailPrint.aspx" + Laporan_Class.TempPencarian + "')";
                #endregion

                LabelTotalJumlahHeaderTransferDetail.Text = Result["Jumlah"];
                LabelTotalJumlahFooterTransferDetail.Text = LabelTotalJumlahHeaderTransferDetail.Text;

                LabelTotalSubtotalHeaderTransferDetail.Text = Result["Subtotal"];
                LabelTotalSubtotalFooterTransferDetail.Text = LabelTotalSubtotalHeaderTransferDetail.Text;

                RepeaterTransferDetail.DataSource = Result["Data"];
                RepeaterTransferDetail.DataBind();
            }

            else if (DropDownListCariPengirimPenerimaTransferDetail.SelectedValue == "Penerima")
            {
                var Result = Laporan_Class.TransferBahanBakuDetail(null, 0, 0, DropDownListCariTempatTransferDetail.SelectedValue.ToInt(), 0, DropDownListCariStatusTransferDetail.SelectedValue.ToInt(), TextBoxCariKodeTransferDetail.Text, DropDownListCariBahanBakuTransferDetail.SelectedValue.ToInt(), DropDownListCariSatuanTransferDetail.SelectedValue.ToInt(), DropDownListCariKategoriTransferDetail.SelectedValue.ToInt(), true);

                #region KONFIGURASI LAPORAN
                LabelPeriode.Text = Laporan_Class.Periode;

                LinkDownloadTransferDetail.Visible = GenerateExcel;

                if (LinkDownloadTransferDetail.Visible)
                    LinkDownloadTransferDetail.HRef = Laporan_Class.LinkDownload;

                ButtonPrintTransferDetail.OnClientClick = "return popitup('BahanBakuDetailPrint.aspx" + Laporan_Class.TempPencarian + "')";
                #endregion

                LabelTotalJumlahHeaderTransferDetail.Text = Result["Jumlah"];
                LabelTotalJumlahFooterTransferDetail.Text = LabelTotalJumlahHeaderTransferDetail.Text;

                LabelTotalSubtotalHeaderTransferDetail.Text = Result["Subtotal"];
                LabelTotalSubtotalFooterTransferDetail.Text = LabelTotalSubtotalHeaderTransferDetail.Text;

                RepeaterTransferDetail.DataSource = Result["Data"];
                RepeaterTransferDetail.DataBind();
            }
        }
    }

    private void LoadData()
    {
        LoadData(false);
    }
    protected void ButtonExcelTransferDetail_Click(object sender, EventArgs e)
    {
        LoadData(true);
    }
    protected void LoadData_EventTransferDetail(object sender, EventArgs e)
    {
        LoadData();
    }
    #endregion
}