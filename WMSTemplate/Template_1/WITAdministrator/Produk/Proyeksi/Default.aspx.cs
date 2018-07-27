using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Proyeksi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBProyeksi[] daftarProyeksi = db.TBProyeksis.OrderByDescending(item => item.TanggalProyeksi).ToArray();

                DropDownListCariIDProyeksi.DataSource = daftarProyeksi.Where(item => item.IDProyeksi != null).Select(item => new { item.IDProyeksi }).Distinct();
                DropDownListCariIDProyeksi.DataTextField = "IDProyeksi";
                DropDownListCariIDProyeksi.DataValueField = "IDProyeksi";
                DropDownListCariIDProyeksi.DataBind();
                DropDownListCariIDProyeksi.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                DropDownListCariPegawai.DataSource = daftarProyeksi.Select(item => new { item.IDPengguna, item.TBPengguna.NamaLengkap }).Distinct();
                DropDownListCariPegawai.DataTextField = "NamaLengkap";
                DropDownListCariPegawai.DataValueField = "IDPengguna";
                DropDownListCariPegawai.DataBind();
                DropDownListCariPegawai.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                LoadData(db.TBProyeksis.ToArray());
            }
        }
    }

    private void LoadData(TBProyeksi[] daftarProyeksi)
    {
        if (DropDownListCariIDProyeksi.SelectedValue != "0")
            daftarProyeksi = daftarProyeksi.Where(item => item.IDProyeksi == DropDownListCariIDProyeksi.SelectedValue).ToArray();

        if (DropDownListCariPegawai.SelectedValue != "0")
            daftarProyeksi = daftarProyeksi.Where(item => item.IDPengguna == DropDownListCariPegawai.SelectedValue.ToInt()).ToArray();

        if (DropDownListCariStatus.SelectedValue != "0")
            daftarProyeksi = daftarProyeksi.Where(item => item.EnumStatusProyeksi == DropDownListCariStatus.SelectedValue.ToInt()).ToArray();

        RepeaterProyeksi.DataSource = daftarProyeksi.Select(item => new
        {
            item.IDProyeksi,
            Pegawai = item.TBPengguna.NamaLengkap,
            item.TanggalProyeksi,
            item.TanggalSelesai,
            item.TotalJumlah,
            item.GrandTotalHargaJual,
            Status = Pengaturan.StatusProyeksi(item.EnumStatusProyeksi.ToString()),
            item.Keterangan,
            Selesai = item.EnumStatusProyeksi == (int)PilihanEnumStatusProyeksi.Proses ? true : false,
            Cetak = "return popitup('Cetak.aspx?id=" + item.IDProyeksi + "')",
            CetakRAP = "return popitup('ProyekPrint.aspx?id=" + item.IDProyeksi + "')"
        }).OrderBy(item => item.TanggalProyeksi).ToArray();
        RepeaterProyeksi.DataBind();
    }

    protected void RepeaterProyeksi_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Selesai")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                TBProyeksi[] daftarProyeksi = db.TBProyeksis.Where(item => item.EnumStatusProyeksi == (int)PilihanEnumStatusProyeksi.Proses).OrderByDescending(item => item.TanggalProyeksi).ToArray();

                TBProyeksi proyeksi = daftarProyeksi.FirstOrDefault(item => item.IDProyeksi == e.CommandArgument.ToString());
                proyeksi.EnumStatusProyeksi = (int)PilihanEnumStatusProyeksi.Selesai;

                db.SubmitChanges();

                LoadData(daftarProyeksi);
            }
        }
    }

    protected void Event_Cari(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            LoadData(db.TBProyeksis.OrderByDescending(item => item.TanggalProyeksi).ToArray());
        }
    }

    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }
}