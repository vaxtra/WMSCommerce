using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITSurvei_Survei_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"].ToInt() > 0)
            {
                try
                {
                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        var Soal = db.TBSoals
                            .FirstOrDefault(item =>
                                item.IDSoal == Request.QueryString["id"].ToInt() &&
                                DateTime.Now.Date >= item.TanggalMulai.Date &&
                                item.EnumStatusSoal == (int)PilihanStatusSoal.Aktif);

                        if (Soal != null)
                        {
                            if (DateTime.Now.Date <= Soal.TanggalSelesai.Value.Date)
                            {
                                LabelJudul.Text = Soal.Judul;
                                LabelKeterangan.Text = Soal.Keterangan;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

                MultiViewSurvei.ActiveViewIndex = 0;
            }
        }
    }
    protected void ButtonMulai_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Pelanggan_Class ClassPelanggan = new Pelanggan_Class(db);

            var Pelanggan = ClassPelanggan.Cari(TextBoxEmail.Text, TextBoxHandphone.Text);

            if (Pelanggan != null)
                ClassPelanggan.Ubah(
                    IDPelanggan: Pelanggan.IDPelanggan,
                    NamaLengkap: TextBoxNama.Text,
                    Email: TextBoxEmail.Text,
                    Handphone: Pengaturan.InputHandphone(TextBoxHandphone.Text)
                    );
            else
                ClassPelanggan.Tambah(
                        IDGrupPelanggan: (int)EnumGrupPelanggan.Customer,
                        IDPenggunaPIC: (int)EnumPengguna.RendyHerdiawan,
                        NamaLengkap: TextBoxNama.Text,
                        Username: "",
                        Password: "",
                        Email: TextBoxEmail.Text,
                        Handphone: Pengaturan.InputHandphone(TextBoxHandphone.Text),
                        TeleponLain: "",
                        TanggalLahir: DateTime.Now,
                        Deposit: 0,
                        Catatan: "",
                        _IsActive: true
                        );

            db.SubmitChanges();

            //MEMBUAT SESSION PELANGGAN
            PelangganLogin pelangganLogin = new PelangganLogin(Pelanggan._IDWMS);

            Session["PelangganLogin"] = pelangganLogin;

            int nomor = LabelNomor.Text.ToInt() + 1;
            bool status = true;

            while (status)
            {
                //MENCARI PERTANYAAN PERTAMA
                var Pertanyaan = db.TBSoalPertanyaans.FirstOrDefault(item => item.IDSoal == Request.QueryString["id"].ToInt() && item.Nomor == nomor);

                if (Pertanyaan != null)
                {
                    if (db.TBSoalJawabanPelanggans.FirstOrDefault(item => item.IDPelanggan == pelangganLogin.IDPelanggan && item.TBSoalJawaban.IDSoalPertanyaan == Pertanyaan.IDSoalPertanyaan) != null)
                        nomor++;
                    else
                    {
                        LabelNomor.Text = Pertanyaan.Nomor.ToString();
                        LabelPertanyaan.Text = Pertanyaan.Isi;

                        RadioButtonListJawaban.DataSource = Pertanyaan.TBSoalJawabans.ToArray();
                        RadioButtonListJawaban.DataValueField = "IDSoalJawaban";
                        RadioButtonListJawaban.DataTextField = "Isi";
                        RadioButtonListJawaban.DataBind();

                        MultiViewSurvei.ActiveViewIndex = 1;
                        status = false;
                    }
                }
                else
                {
                    MultiViewSurvei.ActiveViewIndex = 2;
                    status = false;
                }
            }
        }
    }

    protected void ButtonSelanjutnya_Click(object sender, EventArgs e)
    {
        if (RadioButtonListJawaban.SelectedItem != null)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                //MENYIMPAN JAWABAN PELANGGAN
                PelangganLogin pelangganLogin = (PelangganLogin)Session["PelangganLogin"];

                //MENCARI APAKAH SUDAH PERNAH MENJAWAB SEBELUMNYA
                TBSoalJawabanPelanggan soalJawabanPelanggan = db.TBSoalJawabanPelanggans
                    .FirstOrDefault(item =>
                        item.IDPelanggan == pelangganLogin.IDPelanggan &&
                        item.IDSoalJawaban == int.Parse(RadioButtonListJawaban.SelectedValue));

                if (soalJawabanPelanggan != null)
                    soalJawabanPelanggan.IDSoalJawaban = int.Parse(RadioButtonListJawaban.SelectedValue);
                else
                {
                    db.TBSoalJawabanPelanggans.InsertOnSubmit(new TBSoalJawabanPelanggan
                    {
                        IDPelanggan = pelangganLogin.IDPelanggan,
                        IDSoalJawaban = RadioButtonListJawaban.SelectedValue.ToInt()
                    });
                }

                db.SubmitChanges();

                int nomor = LabelNomor.Text.ToInt() + 1;
                bool status = true;

                while (status)
                {
                    TBSoalPertanyaan Pertanyaan = db.TBSoalPertanyaans.FirstOrDefault(item => item.IDSoal == Request.QueryString["id"].ToInt() && item.Nomor == nomor);

                    if (Pertanyaan != null)
                    {
                        if (db.TBSoalJawabanPelanggans.FirstOrDefault(item => item.IDPelanggan == pelangganLogin.IDPelanggan && item.TBSoalJawaban.IDSoalPertanyaan == Pertanyaan.IDSoalPertanyaan) != null)
                            nomor++;
                        else
                        {
                            LabelNomor.Text = Pertanyaan.Nomor.ToString();
                            LabelPertanyaan.Text = Pertanyaan.Isi;

                            RadioButtonListJawaban.DataSource = Pertanyaan.TBSoalJawabans.ToArray();
                            RadioButtonListJawaban.DataValueField = "IDSoalJawaban";
                            RadioButtonListJawaban.DataTextField = "Isi";
                            RadioButtonListJawaban.DataBind();

                            status = false;
                        }
                    }
                    else
                    {
                        MultiViewSurvei.ActiveViewIndex = 2;
                        status = false;
                    }
                }
            }
        }
    }
}