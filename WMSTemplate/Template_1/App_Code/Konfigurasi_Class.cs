using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class Konfigurasi_Class
{
    private List<int> ListIDKonfigurasi;

    public Konfigurasi_Class(int idGrupPengguna)
    {
        ListIDKonfigurasi = new List<int>();

        string _path = HttpContext.Current.Server.MapPath("~/App_Data/Konfigurasi/") + idGrupPengguna + ".json";

        if (File.Exists(_path))
        {
            string _result = File.ReadAllText(_path);
            ListIDKonfigurasi = JsonConvert.DeserializeObject<List<int>>(_result);
        }
        else
            ListIDKonfigurasi.Add(0);
    }

    public bool ValidasiKonfigurasi(EnumKonfigurasi konfigurasi)
    {
        int _pencarianID = ListIDKonfigurasi.FirstOrDefault(item => item == (int)konfigurasi);

        if (_pencarianID != 0)
            return true;
        else
            return false;
    }
}