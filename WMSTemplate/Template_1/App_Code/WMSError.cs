using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class WMSError
{
    public static void Pesan(string pesan)
    {
        throw new Exception("[WMS Error] " + pesan);
    }
}