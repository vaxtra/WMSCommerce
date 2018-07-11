using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AkuntansiDokumen_Class
{
    public TBAkuntansiDokumen Tambah(DataClassesDatabaseDataContext db, TBAkuntansiJurnal AkuntansiJurnal, string format)
    {
        var AkuntansiDokumen = new TBAkuntansiDokumen
        {
            //IDAkuntansiDokumen
            //IDAkuntansiJurnal 
            TBAkuntansiJurnal = AkuntansiJurnal,
            Format = format
        };

        return AkuntansiDokumen;
    }
}
