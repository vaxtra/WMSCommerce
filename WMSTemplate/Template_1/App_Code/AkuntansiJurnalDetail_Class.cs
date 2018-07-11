using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AkuntansiJurnalDetail_Class
{
    public TBAkuntansiJurnalDetail Proses(DataClassesDatabaseDataContext db, PilihanBertambahBerkurang pilihan, int idAkuntansiAkun, decimal nominal)
    {
        AkuntansiAkun_Class ClassAkuntansiAkun = new AkuntansiAkun_Class();

        var Akun = ClassAkuntansiAkun.Cari(db, idAkuntansiAkun);

        if (Akun != null)
        {
            var JurnalDetail = new TBAkuntansiJurnalDetail
            {
                //IDAkuntansiJurnalDetail
                //TBAkuntansiJurnal
                //Debit
                //Kredit
                //IDAkuntansiJurnal
                //IDAkuntansiAkun
                TBAkuntansiAkun = Akun
            };

            PilihanDebitKredit Status;

            if (pilihan == PilihanBertambahBerkurang.Bertambah)
                Status = (PilihanDebitKredit)Akun.TBAkuntansiAkunTipe.Bertambah;
            else
                Status = (PilihanDebitKredit)Akun.TBAkuntansiAkunTipe.Berkurang;


            if (Status == PilihanDebitKredit.Debit)
            {
                JurnalDetail.Debit = nominal;
                JurnalDetail.Kredit = 0;
            }
            else if (Status == PilihanDebitKredit.Kredit)
            {
                JurnalDetail.Debit = 0;
                JurnalDetail.Kredit = nominal;
            }

            return JurnalDetail;
        }
        else
            return null;
    }

    public TBAkuntansiJurnalDetail Proses(DataClassesDatabaseDataContext db, PilihanDebitKredit status, int idAkuntansiAkun, decimal nominal)
    {
        AkuntansiAkun_Class ClassAkuntansiAkun = new AkuntansiAkun_Class();

        var Akun = ClassAkuntansiAkun.Cari(db, idAkuntansiAkun);

        if (Akun != null)
        {
            var JurnalDetail = new TBAkuntansiJurnalDetail
            {
                //IDAkuntansiJurnalDetail
                //TBAkuntansiJurnal
                //Debit
                //Kredit
                //IDAkuntansiJurnal
                //IDAkuntansiAkun
                TBAkuntansiAkun = Akun
            };

            if (status == PilihanDebitKredit.Debit)
            {
                JurnalDetail.Debit = nominal;
                JurnalDetail.Kredit = 0;
            }
            else if (status == PilihanDebitKredit.Kredit)
            {
                JurnalDetail.Debit = 0;
                JurnalDetail.Kredit = nominal;
            }

            return JurnalDetail;
        }
        else
            return null;
    }
}
