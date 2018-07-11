using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pengguna
/// </summary>
public class Pengguna
{
    public int LevelJabatan { get; set; }
    public int IDPengguna { get; set; }
    public int IDPenggunaParent { get; set; }
    public int IDGrupPengguna { get; set; }
    public string PenggunaParent { get; set; }
    public string GrupPengguna { get; set; }
    public string NamaLengkap { get; set; }
    public string Username { get; set; }

    public List<Pengguna> CariBawahanSemua(TBPengguna pengguna)
    {
        List<Pengguna> daftarBawahan = new List<Pengguna>();
        CariBawahan(daftarBawahan, pengguna, 1);
        return daftarBawahan;
    }

    public void CariBawahan(List<Pengguna> daftarBawahan, TBPengguna atasan, int LevelJabatan)
    {
        foreach (var item in atasan.TBPenggunas)
        {
            daftarBawahan.Add(new Pengguna()
            {
                LevelJabatan = LevelJabatan,
                IDPengguna = item.IDPengguna,
                IDPenggunaParent = item.IDPenggunaParent.Value,
                IDGrupPengguna = item.IDGrupPengguna,
                PenggunaParent = atasan.NamaLengkap,
                GrupPengguna = item.TBGrupPengguna.Nama,
                NamaLengkap = item.NamaLengkap
            });
            if (item.TBPenggunas.Count > 0)
                CariBawahan(daftarBawahan, item, LevelJabatan + 1);
        }
    }
}