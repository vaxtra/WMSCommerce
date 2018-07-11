using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class KonfigurasiPrinter_Class
{
    public TBKonfigurasiPrinter[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBKonfigurasiPrinters.ToArray();
    }
    public TBKonfigurasiPrinter Cari(DataClassesDatabaseDataContext db, int idKonfigurasiPrinter)
    {
        return db.TBKonfigurasiPrinters.FirstOrDefault(item => item.IDKonfigurasiPrinter == idKonfigurasiPrinter);
    }
    public void Ubah(DataClassesDatabaseDataContext db, int idKonfigurasiPrinter, string namaPrinter)
    {
        var KonfigurasiPrinter = Cari(db, idKonfigurasiPrinter);

        if (KonfigurasiPrinter != null)
            KonfigurasiPrinter.NamaPrinter = namaPrinter;
    }
}
