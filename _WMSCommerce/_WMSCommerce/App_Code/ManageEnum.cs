using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManageOpsi
/// </summary>
/// 

public enum EnumStatusProject
{
    Baru = 1,
    Aktif,
    NonAktif
}

public enum EnumStatusTaskAssign
{
    Pending = 1,
    InProgress,
    Verification,
    Finish
}

public enum EnumColor
{
    Empty = 0,
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info,
    Light,
    Dark
}

public enum EnumColorChart
{
    Empty = 0,
    Primary,
    Success,
    Info,
    Warning,
    Danger,
    Secondary,
    Dark,
    Light    
}

public enum EnumColorSAP
{
    Hue1 = 0,
    Hue2,
    Hue3,
    Hue4,
    Hue5,
    Hue6,
    Hue7,
    Hue8,
    Hue9,
    Hue10,
    Hue11,
    SemanticRed,
    SemanticYellow,
    SemanticGreen,
    SemanticGray
}

public enum EnumAlertCRUD
{
    Empty = 0,
    BerhasilTambah,
    BerhasilUbah,
    BerhasilHapus,
    GagalTambah,
    GagalUbah,
    GagalHapus
}

public static class ManageEnum
{
    public static string GetProjectJenis(EnumStatusProject Enum)
    {
        switch (Enum)
        {
            case EnumStatusProject.Baru:
                return "<span class=\"badge badge-pill badge-light\">Baru</span>";
            case EnumStatusProject.Aktif:
                return "<span class=\"badge badge-pill badge-success\">Aktif</span>";
            case EnumStatusProject.NonAktif:
                return "<span class=\"badge badge-pill badge-secondary\">Non Aktif</span>";
            default:
                return string.Empty;
        }
    }

    public static string GetProjectJenis(int Enum)
    {
        switch (Enum)
        {
            case (int)EnumStatusProject.Baru:
                return "<span class=\"badge badge-pill badge-light\">Baru</span>";
            case (int)EnumStatusProject.Aktif:
                return "<span class=\"badge badge-pill badge-success\">Aktif</span>";
            case (int)EnumStatusProject.NonAktif:
                return "<span class=\"badge badge-pill badge-secondary\">Non Aktif</span>";
            default:
                return string.Empty;
        }
    }
}