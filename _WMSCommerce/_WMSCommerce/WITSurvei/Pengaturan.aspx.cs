using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITSurvey_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();
    }
    protected void RepeaterSoalPertanyaan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            //MENGHAPUS SOAL
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var SoalPertanyaan = db.TBSoalPertanyaans.FirstOrDefault(item => item.IDSoalPertanyaan == e.CommandArgument.ToInt());

                    db.TBSoalJawabans.DeleteAllOnSubmit(SoalPertanyaan.TBSoalJawabans);
                    db.TBSoalPertanyaans.DeleteOnSubmit(SoalPertanyaan);
                    db.SubmitChanges();

                    Perubahan();
                }
            }
            catch (Exception)
            {
            }
        }
        else if (e.CommandName == "TambahJawaban")
        {
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var SoalPertanyaan = db.TBSoalPertanyaans.FirstOrDefault(item => item.IDSoalPertanyaan == e.CommandArgument.ToInt());

                    db.TBSoalJawabans.InsertOnSubmit(new TBSoalJawaban
                    {
                        Bobot = 0,
                        Isi = "",
                        TBSoalPertanyaan = SoalPertanyaan
                    });
                    db.SubmitChanges();
                }

                Perubahan();
            }
            catch (Exception)
            {
            }
        }
    }
    protected void RepeaterSoalJawaban_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            //MENGHAPUS JAWABAN
            try
            {
                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    var SoalJawaban = db.TBSoalJawabans.FirstOrDefault(item => item.IDSoalJawaban == e.CommandArgument.ToInt());

                    db.TBSoalJawabans.DeleteOnSubmit(SoalJawaban);
                    db.SubmitChanges();

                    Perubahan();
                }
            }
            catch (Exception)
            {
            }
        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        Perubahan();
    }
    private void LoadData()
    {
        if (Request.QueryString["id"].ToInt() > 0)
            LabelIDSoal.Text = Request.QueryString["id"];

        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Soal = db.TBSoals.FirstOrDefault(item => item.IDSoal == LabelIDSoal.Text.ToInt());

                if (Soal != null)
                {
                    LabelIDSoal.Text = Soal.IDSoal.ToString();
                    DropDownListPengguna.Items.Add(new ListItem { Value = Soal.TBPengguna.IDPengguna.ToString(), Text = Soal.TBPengguna.NamaLengkap });
                    DropDownListTempat.Items.Add(new ListItem { Value = Soal.TBTempat.IDTempat.ToString(), Text = Soal.TBTempat.Nama });

                    TextBoxJudul.Text = Soal.Judul;
                    TextBoxKeterangan.Text = Soal.Keterangan;
                    TextBoxTanggalPembuatan.Text = Soal.TanggalPembuatan.ToString("d MMMM yyyy");
                    TextBoxTanggalMulai.Text = Soal.TanggalMulai.ToString("d MMMM yyyy");
                    TextBoxTanggalSelesai.Text = Soal.TanggalSelesai.HasValue ? Soal.TanggalSelesai.Value.ToString("d MMMM yyyy") : "";
                    DropDownListStatus.SelectedValue = Soal.EnumStatusSoal.ToString();

                    RepeaterSoalPertanyaan.DataSource = Soal.TBSoalPertanyaans.ToArray();
                    RepeaterSoalPertanyaan.DataBind();

                    ButtonOk.Text = "Ubah";
                    PanelPertanyaan.Visible = true;
                }
                else
                {
                    ButtonOk.Text = "Tambah";
                    PanelPertanyaan.Visible = false;
                    PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

                    TextBoxTanggalPembuatan.Text = DateTime.Now.ToString("d MMMM yyyy");
                    DropDownListPengguna.Items.Add(new ListItem { Value = PenggunaLogin.IDPengguna.ToString(), Text = PenggunaLogin.NamaLengkap });
                    DropDownListTempat.Items.Add(new ListItem { Value = PenggunaLogin.IDTempat.ToString(), Text = PenggunaLogin.Tempat });
                }
            }
        }
        catch (Exception)
        {
        }
    }
    private void Perubahan()
    {
        //MENYIMPAN PERUBAHAN PERTANYAAN DAN JAWABAN
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBSoal Soal = db.TBSoals.FirstOrDefault(item => item.IDSoal == LabelIDSoal.Text.ToInt());

                if (Soal != null)
                {
                    PenggunaLogin PenggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

                    //MENYIMPAN SOAL
                    Soal.IDPengguna = PenggunaLogin.IDPengguna;
                    Soal.IDTempat = PenggunaLogin.IDTempat;

                    Soal.Judul = TextBoxJudul.Text;
                    Soal.Keterangan = TextBoxKeterangan.Text;

                    Soal.TanggalMulai = TextBoxTanggalMulai.Text.ToDateTime();

                    if (!string.IsNullOrWhiteSpace(TextBoxTanggalSelesai.Text))
                        Soal.TanggalSelesai = TextBoxTanggalSelesai.Text.ToDateTime();
                    else
                        Soal.TanggalSelesai = null;

                    Soal.EnumStatusSoal = DropDownListStatus.SelectedValue.ToInt();

                    #region MENYIMPAN PERTANYAAN DAN JAWABAN
                    foreach (RepeaterItem item in RepeaterSoalPertanyaan.Items)
                    {
                        Label LabelIDSoalPertanyaan = (Label)item.FindControl("LabelIDSoalPertanyaan");
                        TextBox TextBoxPertanyaan = (TextBox)item.FindControl("TextBoxPertanyaan");
                        Repeater RepeaterSoalJawaban = (Repeater)item.FindControl("RepeaterSoalJawaban");

                        var SoalPertanyaan = db.TBSoalPertanyaans
                            .FirstOrDefault(item2 => item2.IDSoalPertanyaan == LabelIDSoalPertanyaan.Text.ToInt());

                        if (SoalPertanyaan != null)
                            SoalPertanyaan.Isi = TextBoxPertanyaan.Text;

                        foreach (RepeaterItem item2 in RepeaterSoalJawaban.Items)
                        {
                            Label LabelIDSoalJawaban = (Label)item2.FindControl("LabelIDSoalJawaban");
                            TextBox TextBoxIsi = (TextBox)item2.FindControl("TextBoxIsi");
                            TextBox TextBoxBobot = (TextBox)item2.FindControl("TextBoxBobot");

                            var SoalJawaban = db.TBSoalJawabans
                                .FirstOrDefault(item3 => item3.IDSoalJawaban == LabelIDSoalJawaban.Text.ToInt());

                            if (SoalJawaban != null)
                            {
                                SoalJawaban.Isi = TextBoxIsi.Text;
                                SoalJawaban.Bobot = TextBoxBobot.Text.ToInt();
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    //MEMBUAT SOAL BARU
                    Soal = new TBSoal
                    {
                        IDPengguna = DropDownListPengguna.SelectedValue.ToInt(),
                        IDTempat = DropDownListTempat.SelectedValue.ToInt(),
                        Judul = TextBoxJudul.Text,
                        Keterangan = TextBoxKeterangan.Text,
                        TanggalPembuatan = TextBoxTanggalPembuatan.Text.ToDateTime(),
                        TanggalMulai = TextBoxTanggalMulai.Text.ToDateTime(),
                        EnumStatusSoal = DropDownListStatus.SelectedValue.ToInt()
                    };

                    if (!string.IsNullOrWhiteSpace(TextBoxTanggalSelesai.Text))
                        Soal.TanggalSelesai = TextBoxTanggalSelesai.Text.ToDateTime();

                    //MEMASUKKAN PERTANYAAN
                    TBSoalPertanyaan SoalPertanyaan = new TBSoalPertanyaan
                    {
                        Isi = "",
                        Nomor = 1,
                        TBSoal = Soal
                    };

                    //MEMASUKKAN JAWABAN
                    SoalPertanyaan.TBSoalJawabans.Add(new TBSoalJawaban
                    {
                        Bobot = 0,
                        Isi = ""
                    });

                    db.TBSoals.InsertOnSubmit(Soal);
                }

                db.SubmitChanges();

                ButtonOk.Text = "Ubah";
                LabelIDSoal.Text = Soal.IDSoal.ToString();
                LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Data berhasil disimpan");
            }

            LoadData();
        }
        catch (Exception)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan ulangi lagi");
        }
    }
    protected void ButtonTambahPertanyaan_Click(object sender, EventArgs e)
    {
        //MENYIMPAN PERUBAHAN PERTANYAAN DAN JAWABAN
        try
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TBSoal Soal = db.TBSoals.FirstOrDefault(item => item.IDSoal == LabelIDSoal.Text.ToInt());

                if (Soal != null)
                {
                    //MEMASUKKAN PERTANYAAN
                    TBSoalPertanyaan SoalPertanyaan = new TBSoalPertanyaan
                    {
                        Isi = "",
                        Nomor = 1,
                        TBSoal = Soal
                    };

                    //MEMASUKKAN PILIHAN
                    SoalPertanyaan.TBSoalJawabans.Add(new TBSoalJawaban
                    {
                        Bobot = 0,
                        Isi = ""
                    });

                    db.TBSoalPertanyaans.InsertOnSubmit(SoalPertanyaan);
                    db.SubmitChanges();
                }
            }

            Perubahan();
        }
        catch (Exception)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Silahkan ulangi lagi");
        }
    }
}