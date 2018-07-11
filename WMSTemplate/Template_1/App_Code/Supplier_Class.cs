using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Supplier_Class
/// </summary>
public class Supplier_Class
{
    public static void DeleteSupplier(DataClassesDatabaseDataContext db, int idSupplier)
    {
        TBSupplier supplier = db.TBSuppliers.FirstOrDefault(item => item.IDSupplier == idSupplier);

        if (supplier.TBHargaSuppliers.Count == 0 &&
            supplier.TBPOProduksiBahanBakus.Count == 0)
        {
            db.TBSuppliers.DeleteOnSubmit(supplier);
        }
    }

    public static string StatusProduksiBahanBaku(string enumStatusPO)
    {
        string status = string.Empty;

        switch (int.Parse(enumStatusPO))
        {
            case (int)PilihanStatusProduksi.Pending:
                status = "Pending";
                break;
            case (int)PilihanStatusProduksi.Proses:
                status = "Proses";
                break;
            case (int)PilihanStatusProduksi.Selesai:
                status = "Selesai";
                break;
        }

        return status;
    }

    public static string Persentase(string jumlah)
    {
        if (decimal.Parse(jumlah) == -1)
            return "<div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + 0 + "%;\"><p style=\"color:black\"></p></div>";
        else if (decimal.Parse(jumlah) > 0 && decimal.Parse(jumlah) < 25)
            return "<div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
        else if (decimal.Parse(jumlah) >= 25 && decimal.Parse(jumlah) < 50)
            return "<div class=\"progress-bar progress-bar-warning\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
        else if (decimal.Parse(jumlah) >= 50 && decimal.Parse(jumlah) < 75)
            return "<div class=\"progress-bar progress-bar-info\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
        else
            return "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\" aria-valuenow=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + jumlah + "%;\"><p style=\"color:black\">" + jumlah.ToFormatHarga() + "%</p></div>";
    }
}