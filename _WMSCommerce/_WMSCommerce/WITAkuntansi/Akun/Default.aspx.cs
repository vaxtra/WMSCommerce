using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_Akun_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                LoadAktiva(db);
                LoadPasiva(db);
            }
        }
    }
    private class ListAkun
    {
        public TBAkunGrup TBAkunGrup { get; set; }
        public string Nomor { get; set; }
        public string Kode { get; set; }
        public int IDAkun { get; set; }
        public int IDAkunGrup { get; set; }
        public int IDAkunGrupParent { get; set; }
        public int EnumJenisAkunGrup { get; set; }
        public bool Grup { get; set; }
        public bool StatusParent { get; set; }
        public bool StatusButtonUbah { get; set; }
        public string ClassWarna { get; set; }
        public string Nama { get; set; }
    }

    private List<ListAkun> LoadAktiva(DataClassesDatabaseDataContext db)
    {
        List<ListAkun> listAkun = new List<ListAkun>();

        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Aktiva)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup("1", result, listAkun);

        RepeaterLaporanAktiva.DataSource = listAkun;
        RepeaterLaporanAktiva.DataBind();

        return listAkun;
    }
    private void CariAkunGrup(string index, TBAkunGrup[] listAkunGrup, List<ListAkun> listAkun)
    {
        int urutan = 1;
        foreach (var item in listAkunGrup)
        {
            listAkun.Add(new ListAkun
            {
                TBAkunGrup = item.TBAkunGrup1,
                IDAkunGrup = item.IDAkunGrup,
                Nomor = item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan,
                Kode = string.Empty,
                Grup = true,
                ClassWarna = item.IDAkunGrupParent == null ? "class='success'" : "class='warning'",
                Nama = item.Nama,
                StatusParent = item.IDAkunGrupParent == null ? true : false,
                StatusButtonUbah = item.TBAkunGrups.Count == 0 ? true : false
            });

            #region CARI AKUN
            if (item.TBAkuns.Count > 0)
            {
                //CARI AKUN
                CariAkun(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item, listAkun);
            }
            #endregion

            #region CARI ANAK AKUN GRUP
            if (item.TBAkunGrups.Count > 0)
            {
                //CARI ANAK AKUN GRUP
                CariAkunGrup(item.IDAkunGrupParent == null ? urutan.ToString() : "&nbsp&nbsp&nbsp" + index + "." + urutan, item.TBAkunGrups.ToArray(), listAkun);
            }
            #endregion
            urutan++;
        }
    }

    private void CariAkun(string index, TBAkunGrup akunGrup, List<ListAkun> listAkun)
    {
        int urutan = 1;
        foreach (var item in akunGrup.TBAkuns.OrderBy(item => item.Kode))
        {
            listAkun.Add(new ListAkun
            {
                TBAkunGrup = item.TBAkunGrup,
                IDAkunGrup = (int)item.IDAkunGrup,
                IDAkun = item.IDAkun,
                Nomor = "&nbsp&nbsp&nbsp" + index + "." + urutan,
                Kode = item.Kode,
                Grup = false,
                ClassWarna = string.Empty,
                Nama = item.Nama,
            });

            urutan++;
        }
    }

    private List<ListAkun> LoadPasiva(DataClassesDatabaseDataContext db)
    {
        List<ListAkun> listAkun = new List<ListAkun>();

        var result = db.TBAkunGrups.Where(item => item.IDAkunGrupParent == null
        && (item.EnumJenisAkunGrup == (int)PilihanJenisAkunGrup.Pasiva)).ToArray();

        //CARI AKUN GRUP
        CariAkunGrup("1", result, listAkun);

        RepeaterLaporanPasiva.DataSource = listAkun;
        RepeaterLaporanPasiva.DataBind();

        return listAkun;
    }

    protected void RepeaterLaporanAktiva_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
        {
            Response.Redirect("Pengaturan.aspx?" + e.CommandArgument.ToString());
        }
    }

    protected void RepeaterLaporanPasiva_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ubah")
        {
            Response.Redirect("Pengaturan.aspx?" + e.CommandArgument.ToString());
        }
    }

    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pengaturan.aspx");
    }
}