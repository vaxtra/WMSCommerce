using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum TipeAlert
{
    Success = 1,
    Info,
    Warning,
    Danger
}

public static class Alert_Class
{
    public static string Pesan(TipeAlert tipeAlert, string pesan)
    {
        switch (tipeAlert)
        {
            case TipeAlert.Success: return Pesan(tipeAlert, "Berhasil!", pesan);
            case TipeAlert.Info: return Pesan(tipeAlert, "Informasi!", pesan);
            case TipeAlert.Warning: return Pesan(tipeAlert, "Peringatan!", pesan);
            case TipeAlert.Danger: return Pesan(tipeAlert, "Terjadi Kesalahan!", pesan);
            default: return "";
        }
    }

    public static string Pesan(TipeAlert tipeAlert, string judulPesan, string pesan)
    {
        return "<div class=\"alert alert-" + tipeAlert.ToString().ToLower() + "\">" + "<strong>" + judulPesan + "</strong> " + pesan + "</div>";
    }
}