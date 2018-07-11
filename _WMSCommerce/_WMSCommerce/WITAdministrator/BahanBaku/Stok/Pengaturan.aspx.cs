using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_BahanBaku_Stok_Pengaturan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["do"]))
            {
                if (Request.QueryString["do"] == "opname")
                    LabelJudul.Text = "Stock Opname";
                else if (Request.QueryString["do"] == "waste")
                    LabelJudul.Text = "Pembuangan Bahan Baku Rusak";
                else if (Request.QueryString["do"] == "restock")
                    LabelJudul.Text = "Restock Bahan Baku";
                else if (Request.QueryString["do"] == "return")
                    LabelJudul.Text = "Retur ke Tempat Supplier";
                else
                    Response.Redirect("/WITAdministrator/BahanBaku/Stok/Default.aspx");

                using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
                {
                    PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

                    Tempat_Class ClassTempat = new Tempat_Class(db);

                    List<ListItem> ListData = new List<ListItem>();

                    DropDownListJenisStok.Items.Insert(0, new ListItem { Value = "0", Text = "Semua" });
                    DropDownListJenisStok.Items.Insert(1, new ListItem { Value = "1", Text = "Ada Stok", Selected = true });
                    DropDownListJenisStok.Items.Insert(2, new ListItem { Value = "2", Text = "Tidak Ada Stok" });
                    DropDownListJenisStok.Items.Insert(3, new ListItem { Value = "3", Text = "Minus" });

                    DropDownListTempat.Items.AddRange(ClassTempat.DataDropDownList().Where(item => item.Value != "0").ToArray());
                    DropDownListTempat.SelectedValue = Pengguna.IDTempat.ToString();

                    DropDownListKategori.DataSource = db.TBKategoriBahanBakus.ToArray();
                    DropDownListKategori.DataTextField = "Nama";
                    DropDownListKategori.DataValueField = "IDKategoriBahanBaku";
                    DropDownListKategori.DataBind();
                    DropDownListKategori.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });

                    DropDownListSatuan.DataSource = db.TBSatuans.ToArray();
                    DropDownListSatuan.DataTextField = "Nama";
                    DropDownListSatuan.DataValueField = "IDSatuan";
                    DropDownListSatuan.DataBind();
                    DropDownListSatuan.Items.Insert(0, new ListItem { Value = "0", Text = "-Semua-" });
                }
            }
            else
                Response.Redirect("/WITAdministrator/BahanBaku/Stok/Default.aspx");
        }
    }
    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            Laporan_Class Laporan_Class = new Laporan_Class(db, (PenggunaLogin)Session["PenggunaLogin"], DateTime.Now, DateTime.Now, false);

            Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

            string tempPencarian = string.Empty;
            var ListStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).Select(item => new
            {
                item.IDBahanBaku,
                Kode = item.TBBahanBaku.KodeBahanBaku,
                BahanBaku = item.TBBahanBaku.Nama,
                Satuan = item.TBBahanBaku.TBSatuan1.Nama,
                RelasiKategori = item.TBBahanBaku.TBRelasiBahanBakuKategoriBahanBakus.ToArray(),
                Kategori = StokBahanBaku_Class.GabungkanSemuaKategoriBahanBaku(db, item, null),
                Jumlah = item.Jumlah / item.TBBahanBaku.Konversi,
                item.TBBahanBaku.IDSatuanKonversi,
                DropDownSatuan = CariSatuan(item.TBBahanBaku)
            }).OrderBy(item => item.BahanBaku).ToArray();

            switch (DropDownListJenisStok.Text.ToInt())
            {
                case 1: ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah > 0).ToArray(); break;
                case 2: ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah == 0).ToArray(); break;
                case 3: ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah < 0).ToArray(); break;
            }

            tempPencarian += "?IDTempat=" + DropDownListTempat.SelectedValue.ToInt();
            tempPencarian += "&JenisStokProduk=" + DropDownListJenisStok.Text.ToInt();

            if (!string.IsNullOrWhiteSpace(TextBoxBahanBaku.Text))
                ListStokBahanBaku = ListStokBahanBaku.Where(item => item.BahanBaku.ToLower().Contains(TextBoxBahanBaku.Text.ToLower())).ToArray();

            tempPencarian += "&BahanBaku=" + TextBoxBahanBaku.Text;

            if (DropDownListSatuan.SelectedValue.ToInt() != 0)
                ListStokBahanBaku = ListStokBahanBaku.Where(item => item.IDSatuanKonversi == DropDownListSatuan.SelectedValue.ToInt()).ToArray();

            tempPencarian += "&IDSatuan=" + DropDownListSatuan.SelectedValue.ToInt();
            tempPencarian += "&SatuanBesar=true";

            if (DropDownListKategori.SelectedValue.ToInt() != 0)
                ListStokBahanBaku = ListStokBahanBaku.Where(item => item.RelasiKategori.FirstOrDefault(data => data.IDKategoriBahanBaku == DropDownListKategori.SelectedValue.ToInt()) != null).ToArray();

            tempPencarian += "&IDKategori=" + DropDownListKategori.SelectedValue.ToInt();

            if (!string.IsNullOrWhiteSpace(TextBoxKodeBahanBaku.Text))
                ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Kode.ToLower().Contains(TextBoxKodeBahanBaku.Text.ToLower())).ToArray();

            tempPencarian += "&Kode=" + TextBoxKodeBahanBaku.Text;

            if (!string.IsNullOrWhiteSpace(TextBoxQuantity.Text))
            {
                if (TextBoxQuantity.Text.Contains("-"))
                {
                    string[] Range = TextBoxQuantity.Text.Split('-');
                    ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah >= Range[0].ToDecimal() && item.Jumlah <= Range[1].ToDecimal()).OrderBy(item => item.Jumlah).ToArray();
                }
                else
                    ListStokBahanBaku = ListStokBahanBaku.Where(item => item.Jumlah == TextBoxQuantity.Text.ToDecimal()).ToArray();
            }

            tempPencarian += "&Quantity=" + TextBoxQuantity.Text;

            if (Request.QueryString["do"] == "opname")
                Result.Add("Judul", "Stock Opname");
            else if (Request.QueryString["do"] == "waste")
                Result.Add("Judul", "Pembuangan Bahan Baku Rusak");
            else if (Request.QueryString["do"] == "restock")
                Result.Add("Judul", "Restock Bahan Baku");
            else if (Request.QueryString["do"] == "return")
                Result.Add("Judul", "Retur ke Tempat Supplier");
            else
                Result.Add("Judul", "");

            tempPencarian += "&do=" + Request.QueryString["do"];

            Tempat_Class ClassTempat = new Tempat_Class(db);

            Result.Add("Data", ListStokBahanBaku);
            Result.Add("Tempat", ClassTempat.Cari(DropDownListTempat.SelectedValue.ToInt()).Nama);

            //PRINT LAPORAN
            ButtonPrint.OnClientClick = "return popitup('PengaturanPrint.aspx" + tempPencarian + "')";

            RepeaterLaporan.DataSource = Result["Data"];
            RepeaterLaporan.DataBind();
        }
    }

    private ListItemCollection CariSatuan(TBBahanBaku bahanBaku)
    {
        ListItemCollection daftarSatuan = new ListItemCollection();
        daftarSatuan.Add(new ListItem(bahanBaku.TBSatuan1.Nama));
        if (bahanBaku.IDSatuanKonversi != bahanBaku.IDSatuan)
            daftarSatuan.Add(new ListItem(bahanBaku.TBSatuan.Nama));

        return daftarSatuan;
    }

    protected void LoadData_Event(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            bool StatusPerubahan = false;

            TBStokBahanBaku[] daftarStokBahanBaku = db.TBStokBahanBakus.Where(item => item.IDTempat == DropDownListTempat.SelectedValue.ToInt()).ToArray();

            foreach (RepeaterItem item2 in RepeaterLaporan.Items)
            {
                Label LabelIDBahanBaku = (Label)item2.FindControl("LabelIDBahanBaku");
                Label IDSatuanKonversi = (Label)item2.FindControl("IDSatuanKonversi");
                TextBox TextBoxStokTerbaru = (TextBox)item2.FindControl("TextBoxStokTerbaru");
                DropDownList DropDownListPilih = (DropDownList)item2.FindControl("DropDownListPilih");

                if (!string.IsNullOrWhiteSpace(TextBoxStokTerbaru.Text))
                {
                    StatusPerubahan = true;

                    TBStokBahanBaku stokBahanBaku = daftarStokBahanBaku.FirstOrDefault(data => data.IDBahanBaku == LabelIDBahanBaku.Text.ToInt());

                    if (DropDownListPilih.SelectedIndex == 0)
                    {
                        if (Request.QueryString["do"] == "opname")
                            StokBahanBaku_Class.UpdateStockOpname(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), true, "");
                        else if (Request.QueryString["do"] == "waste")
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), stokBahanBaku.HargaBeli.Value, true, EnumJenisPerpindahanStok.PembuanganBarangRusak, "");
                        else if (Request.QueryString["do"] == "restock")
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), stokBahanBaku.HargaBeli.Value, true, EnumJenisPerpindahanStok.RestockBarang, "");
                        else if (Request.QueryString["do"] == "return")
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), stokBahanBaku.HargaBeli.Value, true, EnumJenisPerpindahanStok.ReturKeTempatProduksi, "");
                        else
                            Response.Redirect("/WITWarehouse/BahanBaku.aspx");
                    }
                    else if (DropDownListPilih.SelectedIndex == 1)
                    {
                        if (Request.QueryString["do"] == "opname")
                            StokBahanBaku_Class.UpdateStockOpname(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), false, "");
                        else if (Request.QueryString["do"] == "waste")
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.PembuanganBarangRusak, "");
                        else if (Request.QueryString["do"] == "restock")
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.RestockBarang, "");
                        else if (Request.QueryString["do"] == "return")
                            StokBahanBaku_Class.UpdateBertambahBerkurang(db, DateTime.Now, Pengguna.IDPengguna, stokBahanBaku, TextBoxStokTerbaru.Text.ToDecimal(), stokBahanBaku.HargaBeli.Value, false, EnumJenisPerpindahanStok.ReturKeTempatProduksi, "");
                        else
                            Response.Redirect("/WITWarehouse/BahanBaku.aspx");
                    }
                }

                TextBoxStokTerbaru.Text = string.Empty;
            }

            if (StatusPerubahan)
                db.SubmitChanges();
        }

        LoadData();
    }
}