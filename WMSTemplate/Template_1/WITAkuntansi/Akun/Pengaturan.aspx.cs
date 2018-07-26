using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_Akun_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                AkunGrup_Class ClassAkunGrup = new AkunGrup_Class();
                Akun_Class ClassAkun = new Akun_Class();

                DropDownListAkuntansiGrupAkun.DataSource = ClassAkunGrup.Data(db).Where(item => item.TBAkunGrups.Count == 0).ToArray();
                DropDownListAkuntansiGrupAkun.DataTextField = "Nama";
                DropDownListAkuntansiGrupAkun.DataValueField = "IDAkunGrup";
                DropDownListAkuntansiGrupAkun.DataBind();

                DropDownListAkuntansiGrupAkunTambah.DataSource = ClassAkunGrup.Data(db).Where(item => item.TBAkuns.Count == 0).ToArray();
                DropDownListAkuntansiGrupAkunTambah.DataTextField = "Nama";
                DropDownListAkuntansiGrupAkunTambah.DataValueField = "IDAkunGrup";
                DropDownListAkuntansiGrupAkunTambah.DataBind();

                DropDownListAkuntansiGrupAkunTambah.Items.Add(new ListItem { Value = "0", Text = "- Tidak Ada Header -" });
                DropDownListAkuntansiGrupAkunTambah.SelectedValue = "0";

                if (Request.QueryString["id"] != null) //PENGATURAN UBAH TBAkun
                {
                    var Akun = ClassAkun.Cari(db, (Request.QueryString["id"]).ToInt());

                    if (Akun != null)
                    {
                        DropDownListAkuntansiGrupAkun.SelectedValue = Akun.IDAkunGrup.ToString();
                        TextBoxKode.Text = Akun.Kode;
                        TextBoxNama.Text = Akun.Nama;

                        ButtonOk.Text = "Ubah";
                        LabelTitleAkun.Text = "Pengaturan";
                        PanelAkunGrup.Visible = false;
                    }
                }
                else if (Request.QueryString["idAkunGrup"] != null) //PENGATURAN UBAH TBAkunGrup
                {
                    LabelTitleAkunGrup.Text = "Pengaturan";
                    ButtonOkAkunGrup.Text = "Ubah";
                    PanelAkun.Visible = false;

                    var AkunGrup = db.TBAkunGrups.FirstOrDefault(item => item.IDAkunGrup == (Request.QueryString["idAkunGrup"]).ToInt());

                    if (AkunGrup.IDAkunGrupParent == null)
                    {
                        DropDownListAkuntansiGrupAkunTambah.Enabled = false;
                    }

                    DropDownListPosisiAkun.SelectedValue = AkunGrup.EnumJenisAkunGrup.ToString();
                    DropDownListPosisiDebitKredit.SelectedValue = AkunGrup.EnumSaldoNormal.ToString();
                    TextBoxNamaAkunGrup.Text = AkunGrup.Nama;

                }

            }
        }
    }

    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            Akun_Class ClassAkun = new Akun_Class();

            if (ButtonOk.Text == "Tambah")
                ClassAkun.Tambah(db, (DropDownListAkuntansiGrupAkun.SelectedValue).ToInt(), TextBoxKode.Text, TextBoxNama.Text, Pengguna.IDTempat);
            else if (ButtonOk.Text == "Ubah")
                ClassAkun.Ubah(db, Request.QueryString["id"].ToInt(), (DropDownListAkuntansiGrupAkun.SelectedValue).ToInt(), TextBoxKode.Text, TextBoxNama.Text);

            db.SubmitChanges();
        }

        //Response.Redirect("Default.aspx");
    }

    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void ButtonOkAkunGrup_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            AkunGrup_Class ClassAkunGrup = new AkunGrup_Class();

            if (ButtonOkAkunGrup.Text == "Tambah")
                ClassAkunGrup.Tambah(db, TextBoxNamaAkunGrup.Text, (DropDownListAkuntansiGrupAkunTambah.SelectedValue).ToInt(), (DropDownListPosisiAkun.SelectedValue).ToInt(), (DropDownListPosisiDebitKredit.SelectedValue).ToInt());
            else if (ButtonOkAkunGrup.Text == "Ubah")
                ClassAkunGrup.Ubah(db, Request.QueryString["idAkunGrup"].ToString(), TextBoxNamaAkunGrup.Text, DropDownListAkuntansiGrupAkunTambah.SelectedValue.ToString(), (DropDownListPosisiAkun.SelectedValue).ToInt(),
                    (DropDownListPosisiDebitKredit.SelectedValue).ToInt());

            db.SubmitChanges();
            Response.Redirect("Pengaturan.aspx");
        }

        //Response.Redirect("Default.aspx");
    }
}