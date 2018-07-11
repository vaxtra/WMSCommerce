using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAkuntansi_JurnalUmum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                TextBoxTanggalPeriode1.Text = DateTime.Now.ToString("d MMMM yyyy");
                TextBoxTanggalPeriode2.Text = DateTime.Now.ToString("d MMMM yyyy");

                Akun_Class ClassAkun = new Akun_Class();

                var Akun = ClassAkun.Data(db)
                .OrderBy(item => item.IDAkunGrup)
                .ThenBy(item => item.IDAkun)
                .ToArray();

                DropDownListAkun.Items.Clear();

                foreach (var item in Akun)
                {
                    DropDownListAkun.Items.Add(new ListItem
                    {
                        Text = item.Kode + " - " + item.Nama,
                        Value = item.IDAkun.ToString()
                    });
                }

                DropDownListAkun.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

                DropDownListPengguna.Items.Insert(0, new ListItem { Value = "0", Text = "- Semua -" });

                Pengguna_Class ClassPengguna = new Pengguna_Class(db);
                DropDownListPengguna.Items.AddRange(ClassPengguna.DropDownList());
            }

            LoadData();
        }
    }
    protected void ButtonCari_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Jurnal_Class Jurnal_Class = new Jurnal_Class();
            PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

            if (DropDownListSortBy.SelectedValue == "0")
            {
                if (DropDownListAkun.SelectedValue == "0")
                {
                    //FILTER PENGGUNA
                    if (DropDownListPengguna.SelectedValue == "0")
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.
                            Where(item => item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                            item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() && item.IDTempat == Pengguna.IDTempat)
                            .Select(item => new
                            {
                                TBJurnalDetails = item.TBJurnalDetails,
                                TBPengguna = item.TBPengguna,
                                Tanggal = item.Tanggal,
                                StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                                IDJurnal = item.IDJurnal,
                                Keterangan = item.Keterangan,
                                Referensi = item.Referensi,
                                TBJurnalDokumens = item.TBJurnalDokumens,
                                PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",

                            }).ToArray();

                    }
                    else
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                                                item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                                                item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() &&
                        item.TBJurnalDetails.FirstOrDefault(item2 => item2.TBJurnal.IDPengguna == DropDownListPengguna.SelectedValue.ToInt()) != null && item.IDTempat == Pengguna.IDTempat)
                          .Select(item => new
                          {
                              TBJurnalDetails = item.TBJurnalDetails,
                              TBPengguna = item.TBPengguna,
                              Tanggal = item.Tanggal,
                              StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                              IDJurnal = item.IDJurnal,
                              Keterangan = item.Keterangan,
                              Referensi = item.Referensi,
                              TBJurnalDokumens = item.TBJurnalDokumens,
                              PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",


                          }).ToArray();
                    }
                }
                //FILTER AKUN
                else
                {
                    //FILTER PENGGUNA
                    if (DropDownListPengguna.SelectedValue == "0")
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                            item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                            item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() &&
                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.IDAkun == DropDownListAkun.SelectedValue.ToInt()) != null)
                          .Select(item => new
                          {
                              TBJurnalDetails = item.TBJurnalDetails,
                              TBPengguna = item.TBPengguna,
                              Tanggal = item.Tanggal,
                              StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                              IDJurnal = item.IDJurnal,
                              Keterangan = item.Keterangan,
                              Referensi = item.Referensi,
                              TBJurnalDokumens = item.TBJurnalDokumens,
                              PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",


                          }).ToArray();
                    }
                    else
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                            item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                            item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() &&
                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.IDAkun == DropDownListAkun.SelectedValue.ToInt()) != null &&
                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.TBJurnal.IDPengguna == DropDownListPengguna.SelectedValue.ToInt()) != null)
                          .Select(item => new
                          {
                              TBJurnalDetails = item.TBJurnalDetails,
                              TBPengguna = item.TBPengguna,
                              Tanggal = item.Tanggal,
                              StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                              IDJurnal = item.IDJurnal,
                              Keterangan = item.Keterangan,
                              Referensi = item.Referensi,
                              TBJurnalDokumens = item.TBJurnalDokumens,
                              PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",

                          }).ToArray();
                    }
                }
            }
            else
            {
                if (DropDownListAkun.SelectedValue == "0")
                {
                    if (DropDownListPengguna.SelectedValue == "0")
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                            item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                            item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime()).OrderByDescending(item => item.IDJurnal)
                         .Select(item => new
                         {
                             TBJurnalDetails = item.TBJurnalDetails,
                             TBPengguna = item.TBPengguna,
                             Tanggal = item.Tanggal,
                             StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                             IDJurnal = item.IDJurnal,
                             Keterangan = item.Keterangan,
                             Referensi = item.Referensi,
                             TBJurnalDokumens = item.TBJurnalDokumens,
                             PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",


                         }).ToArray();
                    }
                    else
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                                                item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                                                item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() &&
                        item.TBJurnalDetails.FirstOrDefault(item2 => item2.TBJurnal.IDPengguna == DropDownListPengguna.SelectedValue.ToInt()) != null).OrderByDescending(item => item.IDJurnal)
                     .Select(item => new
                     {
                         TBJurnalDetails = item.TBJurnalDetails,
                         TBPengguna = item.TBPengguna,
                         Tanggal = item.Tanggal,
                         StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                         IDJurnal = item.IDJurnal,
                         Keterangan = item.Keterangan,
                         Referensi = item.Referensi,
                         TBJurnalDokumens = item.TBJurnalDokumens,
                         PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",


                     }).ToArray();

                    }
                }
                else
                {
                    if (DropDownListPengguna.SelectedValue == "0")
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                            item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                            item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() &&
                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.IDAkun == DropDownListAkun.SelectedValue.ToInt()) != null).OrderByDescending(item => item.IDJurnal)
                        .Select(item => new
                        {
                            TBJurnalDetails = item.TBJurnalDetails,
                            TBPengguna = item.TBPengguna,
                            Tanggal = item.Tanggal,
                            StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                            IDJurnal = item.IDJurnal,
                            Keterangan = item.Keterangan,
                            Referensi = item.Referensi,
                            TBJurnalDokumens = item.TBJurnalDokumens,
                            PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",


                        }).ToArray();
                    }
                    else
                    {
                        RepeaterJurnal.DataSource = db.TBJurnals.Where(item =>
                            item.Tanggal.Value.Date >= TextBoxTanggalPeriode1.Text.ToDateTime() &&
                            item.Tanggal.Value.Date <= TextBoxTanggalPeriode2.Text.ToDateTime() &&
                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.IDAkun == DropDownListAkun.SelectedValue.ToInt()) != null &&
                            item.TBJurnalDetails.FirstOrDefault(item2 => item2.TBJurnal.IDPengguna == DropDownListPengguna.SelectedValue.ToInt()) != null).OrderByDescending(item => item.IDJurnal)
                            .Select(item => new
                            {
                                TBJurnalDetails = item.TBJurnalDetails,
                                TBPengguna = item.TBPengguna,
                                Tanggal = item.Tanggal,
                                StatusEdit = Pengguna.IDGrupPengguna == 1 || Pengguna.IDGrupPengguna == 2 ? true : false,
                                IDJurnal = item.IDJurnal,
                                Keterangan = item.Keterangan,
                                Referensi = item.Referensi,
                                TBJurnalDokumens = item.TBJurnalDokumens,
                                PopUpEdit = "return popitup('/WITAkuntansi/TransaksiKhusus.aspx?id=" + item.IDJurnal + "')",

                            }).ToArray();
                    }
                }
            }


            RepeaterJurnal.DataBind();

            ButtonPrint.OnClientClick = "return popitup('JurnalUmumPrint.aspx" + "?Akun=" + DropDownListAkun.SelectedValue + "&Pengguna=" + DropDownListPengguna.SelectedValue + "&Periode1=" + TextBoxTanggalPeriode1.Text + "&Periode2=" + TextBoxTanggalPeriode2.Text + "')";
        }
    }
    protected void RepeaterJurnal_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "JurnalPembalik")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];
                Jurnal_Class Jurnal_Class = new Jurnal_Class();
                List<int> DaftarJurnalSerupa = new List<int>();
                var Jurnal = Jurnal_Class.Cari(db, e.CommandArgument.ToInt());

                TBJurnal JurnalPembalik = new TBJurnal();

                for (int i = 0; i < Jurnal.TBJurnalDetails.Count; i++)
                {
                    //kalo doi KREDIT, masukin nya DEBIT sebesar KREDIT nya
                    if (Jurnal.TBJurnalDetails[i].Debit == 0)
                    {
                        JurnalPembalik.Tanggal = Jurnal.Tanggal;
                        JurnalPembalik.Keterangan = "JURNAL PEMBALIK " + "#" + Jurnal.IDJurnal;
                        JurnalPembalik.IDPengguna = Pengguna.IDPengguna;
                        JurnalPembalik.Referensi = Jurnal.Referensi;

                        JurnalPembalik.TBJurnalDetails.Add(new TBJurnalDetail
                        {
                            IDAkun = Jurnal.TBJurnalDetails[i].IDAkun,
                            Debit = Jurnal.TBJurnalDetails[i].Kredit,
                            Kredit = 0
                        });
                    }
                    else
                    {
                        JurnalPembalik.Tanggal = Jurnal.Tanggal;
                        JurnalPembalik.Keterangan = "JURNAL PEMBALIK " + "#" + Jurnal.IDJurnal;
                        JurnalPembalik.IDPengguna = Pengguna.IDPengguna;
                        JurnalPembalik.Referensi = Jurnal.Referensi;

                        JurnalPembalik.TBJurnalDetails.Add(new TBJurnalDetail
                        {
                            IDAkun = Jurnal.TBJurnalDetails[i].IDAkun,
                            Debit = 0,
                            Kredit = Jurnal.TBJurnalDetails[i].Debit
                        });
                    }
                }

                db.TBJurnals.InsertOnSubmit(JurnalPembalik);
                db.SubmitChanges();
            }
            LoadData();
        }
        else if (e.CommandName == "CashIn")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Jurnal = db.TBJurnals.FirstOrDefault(item => item.IDJurnal == int.Parse(e.CommandArgument.ToString()));
                Response.Redirect("Cetak.aspx?do=CashIn" + "&date=" + Jurnal.Tanggal.Value.ToString("d MMMM yyyy") + 
                    "&amount=" + Jurnal.TBJurnalDetails.FirstOrDefault(item=> item.Debit > 0).Debit.ToFormatHarga() 
                    + "&description=" + Jurnal.Keterangan);
            }
        }
        else if (e.CommandName == "CashOut")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Jurnal = db.TBJurnals.FirstOrDefault(item => item.IDJurnal == int.Parse(e.CommandArgument.ToString()));
                Response.Redirect("Cetak.aspx?do=CashOut" + "&date=" + Jurnal.Tanggal.Value.ToString("d MMMM yyyy") +
                    "&amount=" + Jurnal.TBJurnalDetails.FirstOrDefault(item => item.Kredit > 0).Kredit.ToFormatHarga()
                    + "&description=" + Jurnal.Keterangan);
            }
        }
    }

    protected void ButtonTambah_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransaksiKhusus.aspx");
    }

    protected void ButtonPrint_Click(object sender, EventArgs e)
    {

    }
}