using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class PelangganLogin
{
    #region PROPERTY
    private int iDPelanggan;
    public int IDPelanggan
    {
        get { return iDPelanggan; }
    }

    private Guid iDWMSPelanggan;
    public Guid IDWMSPelanggan
    {
        get { return iDWMSPelanggan; }
    }

    private string namaLengkap;
    public string NamaLengkap
    {
        get { return namaLengkap; }
    }
    #endregion

    public string IDWMSPelangganEnkripsi
    {
        get { return EncryptDecrypt.Encrypt(DateTime.Now + "|" + IDWMSPelanggan.ToString()); }
    }

    /// <summary>
    /// LOGIN BERDASARKAN IDWMSPelanggan
    /// </summary>
    public PelangganLogin(Guid IDWMSPelanggan)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Pelanggan = db.TBPelanggans
                .FirstOrDefault(item =>
                    item._IDWMS == IDWMSPelanggan);

            if (Pelanggan != null)
            {
                iDWMSPelanggan = Pelanggan._IDWMS;
                iDPelanggan = Pelanggan.IDPelanggan;
                namaLengkap = Pelanggan.NamaLengkap;
            }
        }
    }

    /// <summary>
    /// LOGIN BERDASARKAN IDWMSPelangganEnkripsi
    /// </summary>
    public PelangganLogin(string IDWMSPelangganEnkripsi)
    {
        var temp = EncryptDecrypt.Decrypt(IDWMSPelangganEnkripsi).Split('|');

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            var Pelanggan = db.TBPelanggans
                .FirstOrDefault(item =>
                    item._IDWMS == Guid.Parse(temp[1]));

            if (Pelanggan != null)
            {
                iDWMSPelanggan = Pelanggan._IDWMS;
                iDPelanggan = Pelanggan.IDPelanggan;
                namaLengkap = Pelanggan.NamaLengkap;
            }
        }
    }
}