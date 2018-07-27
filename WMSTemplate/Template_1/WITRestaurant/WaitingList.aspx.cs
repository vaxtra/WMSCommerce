using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITRestaurant_WaitingList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListMeja.DataSource = db.TBMejas
                    .Where(item =>
                        item.Status == true &&
                        item.IDMeja != 2 &&
                        item.VIP == false)
                    .ToArray();
                DropDownListMeja.DataValueField = "IDMeja";
                DropDownListMeja.DataTextField = "Nama";
                DropDownListMeja.DataBind();
            }

            LoadData(PilihanWaitingList.Pending);
            MultiViewWaitingList.ActiveViewIndex = 0;
        }
    }

    private void LoadData(PilihanWaitingList pilihan)
    {
        DropDownListPilihanWaitingList.SelectedValue = ((int)pilihan).ToString();

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            RepeaterWaitingList.DataSource = db.TBWaitingLists
                .Where(item => item.EnumStatusReservasi == (int)pilihan)
                .OrderBy(item => item.TanggalMasuk)
                .ToArray();
            RepeaterWaitingList.DataBind();
        }
    }
    private void Reset()
    {
        //DEFAULT VALUE
        TextBoxNama.Text = string.Empty;
        TextBoxPhone.Text = string.Empty;
        TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy HH:mm");
        TextBoxPax.Text = "1";
        TextBoxKeterangan.Text = string.Empty;
        HiddenFieldIDWaitingList.Value = null;
    }
    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        Reset();
        ButtonSimpan.Text = "Tambah";
        ButtonTambah.Visible = false;
        ButtonKeluar.Visible = false;
        DropDownListPilihanWaitingList.Visible = false;
        MultiViewWaitingList.ActiveViewIndex = 1;
    }
    protected void ButtonKeluar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        if (ButtonSimpan.Text == "Tambah")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                var Pelanggan = ClassPelanggan.Tambah(1, Pengguna.IDPengguna, TextBoxNama.Text, "", "", "", TextBoxPhone.Text, "", DateTime.Now, 0, "", true);

                db.TBWaitingLists.InsertOnSubmit(new TBWaitingList
                {
                    IDPengguna = Pengguna.IDPengguna,
                    TanggalPencatatan = DateTime.Now,
                    TBPelanggan = Pelanggan,
                    IDMeja = Parse.Int(DropDownListMeja.SelectedValue),
                    JumlahTamu = (int)Pengaturan.FormatAngkaInput(TextBoxPax.Text),
                    TanggalMasuk = Parse.dateTime(TextBoxTanggal.Text),
                    Keterangan = TextBoxKeterangan.Text,
                    EnumStatusReservasi = (int)PilihanWaitingList.Pending
                });

                db.SubmitChanges();
            }
        }
        else if (ButtonSimpan.Text == "Ubah")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var WaitingList = db.TBWaitingLists.FirstOrDefault(item => item.IDWaitingList == Parse.Int(HiddenFieldIDWaitingList.Value));

                if (WaitingList != null)
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    WaitingList.IDPengguna = Pengguna.IDPengguna;
                    WaitingList.TanggalPencatatan = DateTime.Now;
                    WaitingList.TBPelanggan.NamaLengkap = TextBoxNama.Text;
                    WaitingList.TBPelanggan.Handphone = TextBoxPhone.Text;
                    WaitingList.IDMeja = int.Parse(DropDownListMeja.SelectedValue);
                    WaitingList.JumlahTamu = (int)Pengaturan.FormatAngkaInput(TextBoxPax.Text);
                    WaitingList.TanggalMasuk = Parse.dateTime(TextBoxTanggal.Text);
                    WaitingList.Keterangan = TextBoxKeterangan.Text;

                    db.SubmitChanges();
                }
            }
        }

        Reset();
        MultiViewWaitingList.ActiveViewIndex = 0;
        LoadData(PilihanWaitingList.Pending);
    }
    protected void ButtonKembali_Click(object sender, EventArgs e)
    {
        ButtonKeluar.Visible = true;
        ButtonTambah.Visible = true;
        DropDownListPilihanWaitingList.Visible = true;
        MultiViewWaitingList.ActiveViewIndex = 0;
    }
    protected void RepeaterWaitingList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var WaitingList = db.TBWaitingLists.FirstOrDefault(item => item.IDWaitingList == Parse.Int(e.CommandArgument.ToString()));

            if (WaitingList != null)
            {
                if (e.CommandName == "Ubah" && WaitingList.EnumStatusReservasi == (int)PilihanWaitingList.Pending)
                {
                    ButtonSimpan.Text = "Ubah";
                    MultiViewWaitingList.ActiveViewIndex = 1;
                    HiddenFieldIDWaitingList.Value = WaitingList.IDWaitingList.ToString();
                    TextBoxTanggal.Text = WaitingList.TanggalMasuk.ToString("d MMMM yyyy HH:mm");
                    TextBoxNama.Text = WaitingList.TBPelanggan.NamaLengkap;
                    TextBoxPhone.Text = WaitingList.TBPelanggan.Handphone;
                    DropDownListMeja.Text = WaitingList.IDMeja.ToString();
                    TextBoxPax.Text = WaitingList.JumlahTamu.ToString();
                    TextBoxKeterangan.Text = WaitingList.Keterangan;
                }
                else if (e.CommandName == "Pending")
                {
                    WaitingList.EnumStatusReservasi = (int)PilihanWaitingList.Pending;
                    db.SubmitChanges();
                    LoadData(PilihanWaitingList.Pending);
                }
                else if (e.CommandName == "Selesai")
                {
                    WaitingList.EnumStatusReservasi = (int)PilihanWaitingList.Selesai;
                    db.SubmitChanges();
                    LoadData(PilihanWaitingList.Pending);
                }
                else if (e.CommandName == "Batal")
                {
                    WaitingList.EnumStatusReservasi = (int)PilihanWaitingList.Batal;
                    db.SubmitChanges();
                    LoadData(PilihanWaitingList.Pending);
                }
            }
        }
    }
    protected void DropDownListPilihanWaitingList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListPilihanWaitingList.SelectedValue != "0")
            LoadData((PilihanWaitingList)Parse.Int(DropDownListPilihanWaitingList.SelectedValue));
        else
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                RepeaterWaitingList.DataSource = db.TBWaitingLists
                    .OrderBy(item => item.TanggalMasuk)
                    .ToArray();
                RepeaterWaitingList.DataBind();
            }
        }
    }
}