using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Produk_Maintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private void UpdateKodeKombinasiProduk(DataClassesDatabaseDataContext db, string KodeLama, string KodeBaru)
    {
        var KombinasiProdukLama = db.TBKombinasiProduks.FirstOrDefault(item => item.KodeKombinasiProduk == KodeLama.Trim());
        var KombinasiProdukBaru = db.TBKombinasiProduks.FirstOrDefault(item => item.KodeKombinasiProduk == KodeBaru.Trim());

        string tempKombinasiLama = KombinasiProdukLama.Nama;
        string tempKombinasiBaru = KombinasiProdukBaru.Nama;

        if (KombinasiProdukBaru != null && KombinasiProdukLama != null)
        {
            var TransaksiPrint = KombinasiProdukLama.TBTransaksiPrints.ToArray();

            foreach (var item in TransaksiPrint)
            {
                item.TBKombinasiProduk = KombinasiProdukBaru;
            }

            var TransferProdukDetail = KombinasiProdukLama.TBTransferProdukDetails.ToArray();

            foreach (var item in TransferProdukDetail)
            {
                item.TBKombinasiProduk = KombinasiProdukBaru;
            }

            var TransaksiDetail = KombinasiProdukLama.TBTransaksiDetails.ToArray();

            foreach (var item in TransaksiDetail)
            {
                item.TBKombinasiProduk = KombinasiProdukBaru;
            }

            var POProdukDetail = KombinasiProdukLama.TBPOProduksiProdukDetails.ToArray();

            foreach (var item in POProdukDetail)
            {
                item.TBKombinasiProduk = KombinasiProdukBaru;
            }

            var StokProduk = KombinasiProdukLama.TBStokProduks.ToArray();

            foreach (var item in StokProduk)
            {
                db.TBPerpindahanStokProduks.DeleteAllOnSubmit(item.TBPerpindahanStokProduks);
                db.TBHargaVendors.DeleteAllOnSubmit(item.TBHargaVendors);
            }

            var PenerimaanPOProdukDetail = KombinasiProdukLama.TBPenerimaanPOProduksiProdukDetails.ToArray();

            foreach (var item in PenerimaanPOProdukDetail)
            {
                item.TBKombinasiProduk = KombinasiProdukBaru;
            }

            db.TBStokProduks.DeleteAllOnSubmit(StokProduk);

            db.TBKombinasiProduks.DeleteOnSubmit(KombinasiProdukLama);
        }
    }

    protected void ButtonProses_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            try
            {
                UpdateKodeKombinasiProduk(db, TextBoxKombinasiProdukLama.Text, TextBoxKombinasiProdukBaru.Text);

                db.SubmitChanges();

                LiteralResult.Text = "Berhasil Migrasi <b>" + TextBoxKombinasiProdukLama.Text + "</b> --> <b>" + TextBoxKombinasiProdukBaru.Text + "</b>";

                TextBoxKombinasiProdukBaru.Text = "";
                TextBoxKombinasiProdukLama.Text = "";

                LabelKombinasiProdukLama.Text = "";
                LabelKombinasiProdukBaru.Text = "";

                ButtonProses.Visible = false;
                ButtonCancel.Visible = false;
                ButtonValidasi.Visible = true;

                TextBoxKombinasiProdukBaru.Enabled = true;
                TextBoxKombinasiProdukLama.Enabled = true;
            }
            catch (Exception)
            {
                LiteralResult.Text = "Terjadi Kesalahan, Migrasi Gagal <b>" + TextBoxKombinasiProdukLama.Text + "</b> --> <b>" + TextBoxKombinasiProdukBaru.Text + "</b>";
            }
        }
    }

    protected void ButtonValidasi_Click(object sender, EventArgs e)
    {
        LiteralResult.Text = "";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var KombinasiProdukLama = db.TBKombinasiProduks.FirstOrDefault(item => item.KodeKombinasiProduk == TextBoxKombinasiProdukLama.Text.Trim());
            var KombinasiProdukBaru = db.TBKombinasiProduks.FirstOrDefault(item => item.KodeKombinasiProduk == TextBoxKombinasiProdukBaru.Text.Trim());

            LabelKombinasiProdukLama.Text = KombinasiProdukLama != null ? KombinasiProdukLama.Nama : "NOT FOUND!!";
            LabelKombinasiProdukBaru.Text = KombinasiProdukBaru != null ? KombinasiProdukBaru.Nama : "NOT FOUND!!";

            if (KombinasiProdukLama != null && KombinasiProdukBaru != null)
            {
                ButtonProses.Visible = true;
                ButtonCancel.Visible = true;
                ButtonValidasi.Visible = false;

                TextBoxKombinasiProdukBaru.Enabled = false;
                TextBoxKombinasiProdukLama.Enabled = false;

                ButtonProses.Focus();
            }
        }
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        TextBoxKombinasiProdukBaru.Text = "";
        TextBoxKombinasiProdukLama.Text = "";

        LabelKombinasiProdukLama.Text = "";
        LabelKombinasiProdukBaru.Text = "";

        ButtonProses.Visible = false;
        ButtonCancel.Visible = false;
        ButtonValidasi.Visible = true;

        TextBoxKombinasiProdukBaru.Enabled = true;
        TextBoxKombinasiProdukLama.Enabled = true;
    }

    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploadMaintenance.HasFile)
            {
                Server.ScriptTimeout = 1000000;
                PenggunaLogin penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

                string lokasiFile = Server.MapPath("/file_excel/Maintenance/Import/");

                if (!Directory.Exists(lokasiFile))
                    Directory.CreateDirectory(lokasiFile);

                lokasiFile += "Import KodeKombinasiProduk" + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

                FileUploadMaintenance.SaveAs(lokasiFile);

                if (File.Exists(lokasiFile))
                {
                    bool valid = true;
                    string Message = string.Empty;
                    Workbook book = Workbook.Load(lokasiFile);
                    Worksheet sheet = book.Worksheets[0];


                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        TBKombinasiProduk[] daftarKombinasiProduk = db.TBKombinasiProduks.ToArray();

                        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                        {
                            Row row = sheet.Cells.GetRow(rowIndex);

                            for (int colIndex = 0; colIndex <= 2; colIndex++)
                            {
                                Cell cell = row.GetCell(colIndex);

                                if (colIndex == 1 || colIndex == 2)
                                {
                                    if (cell.IsEmpty)
                                    {
                                        Message = "Kode wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                                        valid = false; break;
                                    }
                                    else if (daftarKombinasiProduk.FirstOrDefault(data => data.KodeKombinasiProduk == cell.StringValue.Trim()) == null)
                                    {
                                        Message = "Kode " + cell.StringValue + " tidak ada dalam sistem (Baris ke " + (rowIndex + 1).ToString() + ")";
                                        valid = false; break;
                                    }
                                }
                            }

                            if (valid == false)
                            {
                                break;
                            }
                        }

                        if (valid)
                        {
                            for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                            {
                                Row row = sheet.Cells.GetRow(rowIndex);

                                UpdateKodeKombinasiProduk(db, row.GetCell(1).StringValue, row.GetCell(2).StringValue);
                            }

                            db.SubmitChanges();

                            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Success, "Migrasi Kode Berhasil");
                        }
                        else
                        {
                            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, Message);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
        }
    }

    protected void ButtonView_Click(object sender, EventArgs e)
    {
        try
        {
            PanelViewData.Visible = true;

            if (FileUploadMaintenance.HasFile)
            {
                Server.ScriptTimeout = 1000000;
                PenggunaLogin penggunaLogin = (PenggunaLogin)Session["PenggunaLogin"];

                string lokasiFile = Server.MapPath("/file_excel/Maintenance/Import/");

                if (!Directory.Exists(lokasiFile))
                    Directory.CreateDirectory(lokasiFile);

                lokasiFile += "Import KodeKombinasiProduk" + DateTime.Now.ToString("d MMMM yyyy hh.mm.ss") + ".xls";

                FileUploadMaintenance.SaveAs(lokasiFile);

                if (File.Exists(lokasiFile))
                {
                    bool valid = true;
                    string Message = string.Empty;
                    Workbook book = Workbook.Load(lokasiFile);
                    Worksheet sheet = book.Worksheets[0];

                    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                    {
                        TBKombinasiProduk[] daftarKombinasiProduk = db.TBKombinasiProduks.ToArray();

                        LiteralViewData.Text = string.Empty;

                        for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                        {
                            LiteralViewData.Text += "<tr>";
                            LiteralViewData.Text += "<td>" + rowIndex + "</td>";
                            Row row = sheet.Cells.GetRow(rowIndex);


                            Cell cellLama = row.GetCell(1);
                            Cell cellBaru = row.GetCell(2);

                            TBKombinasiProduk kombinasiProdukLama = daftarKombinasiProduk.FirstOrDefault(data => data.KodeKombinasiProduk == cellLama.StringValue.Trim());
                            TBKombinasiProduk kombinasiProdukBaru = daftarKombinasiProduk.FirstOrDefault(data => data.KodeKombinasiProduk == cellBaru.StringValue.Trim());
                            if (cellLama.IsEmpty)
                            {
                                Message = "Kode wajib diisi (Baris ke " + (rowIndex + 1).ToString() + ")";
                                valid = false; break;
                            }
                            else
                            {
                                if (kombinasiProdukLama == null)
                                {
                                    LiteralViewData.Text += "<td class='danger'>" + cellLama.StringValue.Trim() + "<br />NOT FOUND!!</td>";
                                    valid = false;
                                }
                                else
                                {
                                    if (kombinasiProdukLama != null && kombinasiProdukBaru != null)
                                    {
                                        if (kombinasiProdukLama.TBProduk != kombinasiProdukBaru.TBProduk)
                                            LiteralViewData.Text += "<td class='warning'>";
                                        else
                                            LiteralViewData.Text += "<td>";
                                    }
                                    else
                                        LiteralViewData.Text += "<td>";

                                    LiteralViewData.Text += kombinasiProdukLama.KodeKombinasiProduk + "<br />" + kombinasiProdukLama.Nama;
                                    LiteralViewData.Text += "</td>";
                                }
                                if (kombinasiProdukBaru == null)
                                {
                                    LiteralViewData.Text += "<td class='danger'>" + cellBaru.StringValue.Trim() + "<br />NOT FOUND!!</td>";
                                    valid = false;
                                }
                                else
                                {
                                    if (kombinasiProdukLama != null && kombinasiProdukBaru != null)
                                    {
                                        if (kombinasiProdukLama.TBProduk != kombinasiProdukBaru.TBProduk)
                                            LiteralViewData.Text += "<td class='warning'>";
                                        else
                                            LiteralViewData.Text += "<td>";
                                    }
                                    else
                                        LiteralViewData.Text += "<td>";

                                    LiteralViewData.Text += kombinasiProdukBaru.KodeKombinasiProduk + "<br />" + kombinasiProdukBaru.Nama;
                                    LiteralViewData.Text += "</td>";
                                }
                            }

                            LiteralViewData.Text += "</tr>";
                        }

                        if (valid)
                        {
                            ButtonUpload.Enabled = true;
                        }
                        else
                        {
                            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, "Terdapat kode yang tidak ada di sistem");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LiteralWarning.Text = Alert_Class.Pesan(TipeAlert.Danger, ex.Message);
        }
    }
}