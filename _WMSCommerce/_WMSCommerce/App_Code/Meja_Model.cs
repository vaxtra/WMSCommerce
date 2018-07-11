using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class Meja_Model
{
    private int iDMeja;
    public int IDMeja
    {
        get { return iDMeja; }
        set
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                var Meja = db.TBMejas.FirstOrDefault(item => item.IDMeja == value);

                if (Meja != null)
                {
                    nama = Meja.Nama;
                    iDMeja = value;
                }
                else
                {
                    nama = "Take Away";
                    iDMeja = 2;
                }
            }


        }
    }

    private string nama;
    public string Nama
    {
        get { return nama; }
    }

    public Meja_Model(int idMeja)
    {
        IDMeja = idMeja;
    }
}