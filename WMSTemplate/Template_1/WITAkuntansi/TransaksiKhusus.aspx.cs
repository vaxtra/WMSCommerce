using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_Pemasukan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
            panelLiteralError.Visible = false;
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Akun = db.TBAkuns.Where(item=> item.IDTempat == Pengguna.IDTempat).OrderBy(item => item.IDAkunGrup).ThenBy(item => item.IDAkun).ToArray();

                if (Request.QueryString["do"] != null)
                {
                    LabelTitle.Text = Request.QueryString["do"].ToString();

                    if (Request.QueryString["do"] == "CashIn" || Request.QueryString["do"] == "CashOut")
                        ButtonPrint.Visible = true;
                    else
                        ButtonPrint.Visible = false;

                }

                #region LoadDropdownAkun
                DropDownListDebit.Items.Clear();
                DropDownListKredit.Items.Clear();

                DropDownListDebit.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Debit -" });
                DropDownListDebit2.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Debit -" });
                DropDownListDebit3.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Debit -" });
                DropDownListDebit4.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Debit -" });

                DropDownListKredit.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Kredit -" });
                DropDownListKredit2.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Kredit -" });
                DropDownListKredit3.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Kredit -" });
                DropDownListKredit4.Items.Add(new ListItem { Value = "0", Text = "- Pilih Akun Kredit -" });

                foreach (var item in Akun)
                {
                    DropDownListDebit.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListDebit2.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListDebit3.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListDebit4.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListKredit.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListKredit2.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListKredit3.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });

                    DropDownListKredit4.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });
                }
                #endregion

                TextBoxTanggal.Text = DateTime.Now.ToString("d MMMM yyyy");

                if (Request.QueryString["id"] != null)
                {
                    ButtonOk.Text = "Ubah";
                    var Jurnal = db.TBJurnals.FirstOrDefault(item => item.IDJurnal == (Request.QueryString["id"]).ToInt());
                    if (Jurnal != null)
                    {

                        TextBoxTanggal.Text = Jurnal.Tanggal.Value.ToString("d MMMM yyyy");
                        TextBoxKeterangan.Text = Jurnal.Keterangan;
                        TextBoxReferensi.Text = Jurnal.Referensi; 

                    }
                }
            }

        }
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (ButtonOk.Text == "Simpan")
            {
                if (TambahJurnal(db, Pengguna))
                {
                    Response.Redirect("TransaksiKhusus.aspx");
                    panelLiteralError.Visible = true;
                    LiteralWarning.Text = "Jurnal berhasil Anda posting!";
                    LiteralWarning.Visible = true;
                }
            }
            else if (ButtonOk.Text == "Ubah")
            {
                UbahJurnal(db, Pengguna);
            }

        }

    }
    private bool UbahJurnal(DataClassesDatabaseDataContext db, PenggunaLogin Pengguna)
    {
        var Jurnal = db.TBJurnals.FirstOrDefault(item => item.IDJurnal == (Request.QueryString["id"]).ToInt());

        Jurnal.IDTempat = Pengguna.IDTempat;
        Jurnal.Tanggal = (TextBoxTanggal.Text).ToDateTime();
        Jurnal.Tanggal = (TextBoxTanggal.Text).ToDateTime();
        Jurnal.Keterangan = TextBoxKeterangan.Text;
        Jurnal.IDPengguna = Pengguna.IDPengguna;
        Jurnal.Referensi = TextBoxReferensi.Text;

        decimal debit1 = (TextBoxNominalDebit.Text).ToDecimal();
        decimal debit2 = (TextBoxNominalDebit2.Text).ToDecimal();
        decimal debit3 = (TextBoxNominalDebit3.Text).ToDecimal();
        decimal debit4 = (TextBoxNominalDebit4.Text).ToDecimal();
        decimal kredit1 = (TextBoxNominalKredit.Text).ToDecimal();
        decimal kredit2 = (TextBoxNominalKredit2.Text).ToDecimal();
        decimal kredit3 = (TextBoxNominalKredit3.Text).ToDecimal();
        decimal kredit4 = (TextBoxNominalKredit4.Text).ToDecimal();

        if ((debit1 + debit2 + debit3 + debit4) != (kredit1 + kredit2 + kredit3 + kredit4))
        {
            LiteralWarning.Text = "Harap periksa kembali pengisian kolom nominal Anda, karena tidak balance";
            panelLiteralError.Visible = true;
            return false;
        }
        else
        {
            //HAPUS SELURUH DETAIL JURNAL
            db.TBJurnalDetails.DeleteAllOnSubmit(Jurnal.TBJurnalDetails);

            //MASUKAN KEMBALI JURNAL DETAIL YG BARU
            List<TBJurnalDetail> JurnalDetail = new List<TBJurnalDetail>();

            //DEBIT
            if (DropDownListDebit.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit.Text).ToDecimal(),
                    Kredit = 0
                });
            }
            if (DropDownListDebit2.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit2.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit2.Text).ToDecimal(),
                    Kredit = 0
                });
            }
            if (DropDownListDebit3.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit3.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit3.Text).ToDecimal(),
                    Kredit = 0
                });
            }
            if (DropDownListDebit4.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit4.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit4.Text).ToDecimal(),
                    Kredit = 0
                });
            }

            //KREDIT
            if (DropDownListKredit.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit.Text).ToDecimal(),
                });
            }
            if (DropDownListKredit2.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit2.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit2.Text).ToDecimal(),
                });
            }
            if (DropDownListKredit3.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit3.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit3.Text).ToDecimal(),
                });
            }
            if (DropDownListKredit4.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit4.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit4.Text).ToDecimal(),
                });
            }

            Jurnal.TBJurnalDetails.AddRange(JurnalDetail);
            db.SubmitChanges();

            panelLiteralError.Visible = false;
            return true;
        }

    }
    private bool TambahJurnal(DataClassesDatabaseDataContext db, PenggunaLogin Pengguna)
    {
        decimal debit1  = (TextBoxNominalDebit.Text).ToDecimal();
        decimal debit2  = (TextBoxNominalDebit2.Text).ToDecimal();
        decimal debit3  = (TextBoxNominalDebit3.Text).ToDecimal();
        decimal debit4  = (TextBoxNominalDebit4.Text).ToDecimal();
        decimal kredit1 = (TextBoxNominalKredit.Text).ToDecimal();
        decimal kredit2 = (TextBoxNominalKredit2.Text).ToDecimal();
        decimal kredit3 = (TextBoxNominalKredit3.Text).ToDecimal();
        decimal kredit4 = (TextBoxNominalKredit4.Text).ToDecimal();


        if ((debit1 + debit2 + debit3 + debit4) != (kredit1+ kredit2 + kredit3 + kredit4))
        {
            LiteralWarning.Text = "Harap periksa kembali pengisian kolom nominal Anda, karena tidak balance";
            panelLiteralError.Visible = true;
            return false;
        }
        else
        {
            TBJurnal Jurnal = NewJurnal(Pengguna);
            List<TBJurnalDetail> JurnalDetail = new List<TBJurnalDetail>();

            //DEBIT
            if (DropDownListDebit.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit.Text).ToDecimal(),
                    Kredit = 0
                });
            }
            if (DropDownListDebit2.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit2.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit2.Text).ToDecimal(),
                    Kredit = 0
                });
            }
            if (DropDownListDebit3.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit3.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit3.Text).ToDecimal(),
                    Kredit = 0
                });
            }
            if (DropDownListDebit4.SelectedIndex != 0)
            {
                JurnalDetail.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListDebit4.SelectedValue).ToInt(),
                    Debit = (TextBoxNominalDebit4.Text).ToDecimal(),
                    Kredit = 0
                });
            }

            //KREDIT
            if (DropDownListKredit.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit.Text).ToDecimal(),
                });
            }
            if (DropDownListKredit2.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit2.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit2.Text).ToDecimal(),
                });
            }
            if (DropDownListKredit3.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit3.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit3.Text).ToDecimal(),
                });
            }
            if (DropDownListKredit4.SelectedIndex != 0)
            {
                Jurnal.TBJurnalDetails.Add(new TBJurnalDetail
                {
                    IDAkun = (DropDownListKredit4.SelectedValue).ToInt(),
                    Debit = 0,
                    Kredit = (TextBoxNominalKredit4.Text).ToDecimal(),
                });
            }

            //if (FileUploadDokumen.HasFile)
            //    Jurnal.TBJurnalDokumens.Add(new TBJurnalDokumen
            //    {
            //        Format = (FileUploadDokumen.HasFile) ? FileUploadDokumen.FileName.Substring(FileUploadDokumen.FileName.LastIndexOf(".")) : ""
            //    });

            Jurnal.TBJurnalDetails.AddRange(JurnalDetail);
            db.TBJurnals.InsertOnSubmit(Jurnal);
            db.SubmitChanges();

            //if (FileUploadDokumen.HasFile)
            //    FileUploadDokumen.SaveAs(Server.MapPath("~/files/Akuntansi/") + Jurnal.TBJurnalDokumens.FirstOrDefault().IDJurnalDokumen + Jurnal.TBJurnalDokumens.FirstOrDefault().Format);
            
            panelLiteralError.Visible = false;
            return true;
        }
    }

    private TBJurnal NewJurnal(PenggunaLogin Pengguna)
    {
        return new TBJurnal
        {
            IDTempat = Pengguna.IDTempat,
            Tanggal = (TextBoxTanggal.Text).ToDateTime(),
            Keterangan = TextBoxKeterangan.Text,
            IDPengguna = Pengguna.IDPengguna,
            Referensi = TextBoxReferensi.Text
        };
    }
    protected void DropDownListDebit2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListDebit2.SelectedIndex != 0)
            TextBoxNominalDebit2.Enabled = true;
        else
        {
            TextBoxNominalDebit2.Text = "";
            TextBoxNominalDebit2.Enabled = false;
        }
    }

    protected void DropDownListKredit2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListKredit2.SelectedIndex != 0)
            TextBoxNominalKredit2.Enabled = true;
        else
        {
            TextBoxNominalKredit2.Text = "";
            TextBoxNominalKredit2.Enabled = false;
        }
    }

    protected void DropDownListDebit3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListDebit3.SelectedIndex != 0)
            TextBoxNominalDebit3.Enabled = true;
        else
        {
            TextBoxNominalDebit3.Text = "";
            TextBoxNominalDebit3.Enabled = false;
        }
    }

    protected void DropDownListDebit4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListDebit4.SelectedIndex != 0)
            TextBoxNominalDebit4.Enabled = true;
        else
        {
            TextBoxNominalDebit4.Text = "";
            TextBoxNominalDebit4.Enabled = false;
        }
    }

    protected void DropDownListKredit3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListKredit3.SelectedIndex != 0)
            TextBoxNominalKredit3.Enabled = true;
        else
        {
            TextBoxNominalKredit3.Text = "";
            TextBoxNominalKredit3.Enabled = false;
        }
    }

    protected void DropDownListKredit4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListKredit4.SelectedIndex != 0)
            TextBoxNominalKredit4.Enabled = true;
        else
            TextBoxNominalKredit4.Enabled = false;
    }

    protected void ButtonPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["do"] == "CashIn")
        {
            Response.Redirect("Cetak.aspx?do=CashIn"+"&date=" + TextBoxTanggal.Text + "&amount=" + (TextBoxNominalDebit.Text).ToDecimal() + "&description=" + TextBoxKeterangan.Text + "&Ref=" + TextBoxReferensi.Text);
        }
        else if (Request.QueryString["do"] == "CashOut")
        {
            Response.Redirect("Cetak.aspx?do=CashOut" + "&date=" + TextBoxTanggal.Text + "&amount=" + (TextBoxNominalKredit.Text).ToDecimal() + "&description=" + TextBoxKeterangan.Text + "&Ref=" + TextBoxReferensi.Text);
        }
    }
}