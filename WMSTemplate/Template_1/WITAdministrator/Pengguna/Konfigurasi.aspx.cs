using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WITAdministrator_Pengguna_Konfigurasi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                DropDownListGrupPengguna.DataSource = db.TBGrupPenggunas.ToArray();
                DropDownListGrupPengguna.DataTextField = "Nama";
                DropDownListGrupPengguna.DataValueField = "IDGrupPengguna";
                DropDownListGrupPengguna.DataBind();
                DropDownListGrupPengguna.Items.Insert(0, new ListItem { Text = "-- Grup Pengguna --", Value = "0" });

                ButtonSimpan.Enabled = false;

                CheckBoxListKonfigurasi.DataSource = db.TBKonfigurasis.ToArray();
                CheckBoxListKonfigurasi.DataValueField = "IDKonfigurasi";
                CheckBoxListKonfigurasi.DataTextField = "Nama";
                CheckBoxListKonfigurasi.DataBind();
            }
        }
    }
    protected void DropDownListGrupPengguna_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListGrupPengguna.SelectedValue != "0")
            ButtonSimpan.Enabled = true;
        else
            ButtonSimpan.Enabled = false;

        List<int> DataKonfigurasi = new List<int>();
        string FilePath = Server.MapPath("~/App_Data/Konfigurasi/") + DropDownListGrupPengguna.SelectedValue + ".json";
        CheckBoxListKonfigurasi.ClearSelection();

        if (File.Exists(FilePath))
        {
            string Result = File.ReadAllText(FilePath);
            DataKonfigurasi = JsonConvert.DeserializeObject<List<int>>(Result);

            foreach (var item in DataKonfigurasi)
            {
                var checkbox = CheckBoxListKonfigurasi.Items.FindByValue(item.ToString());

                if (checkbox != null)
                    checkbox.Selected = true;
            }
        }
    }
    protected void ButtonSimpan_Click(object sender, EventArgs e)
    {
        List<int> DataKonfigurasi = new List<int>();
        PenggunaLogin Pengguna = (PenggunaLogin)Session["PenggunaLogin"];

        foreach (ListItem item in CheckBoxListKonfigurasi.Items)
        {
            if (item.Selected)
                DataKonfigurasi.Add(item.Value.ToInt());
        }

        string FilePath = Server.MapPath("~/App_Data/Konfigurasi/") + DropDownListGrupPengguna.SelectedValue + ".json";
        string Json = JsonConvert.SerializeObject(DataKonfigurasi);
        File.WriteAllText(FilePath, Json);
    }
}