using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PilihanAkunTipe
{
    AssetsHarta = 1,
    LiabilitiesKewajiban = 2,
    CapitalModal = 3,
    RevenuesPendapatan = 4,
    ExpensesBeban = 5
}
public class AkuntansiAkunTipe_Class
{
    public TBAkuntansiAkunTipe[] Data(DataClassesDatabaseDataContext db)
    {
        return db.TBAkuntansiAkunTipes.ToArray();
    }
}
