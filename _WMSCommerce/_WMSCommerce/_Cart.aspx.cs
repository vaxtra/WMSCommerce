﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void RepeaterCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
            {
                PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

                //MENCARI TRANSAKSI SESSION
                var TransaksiECommerce = db.TBTransaksiECommerces
                    .FirstOrDefault(item => item.IDPelanggan == Pelanggan.IDPelanggan);

                var TransaksiECommerceDetail = TransaksiECommerce.TBTransaksiECommerceDetails
                    .FirstOrDefault(item => item.IDTransaksiECommerceDetail == e.CommandArgument.ToInt());

                if (TransaksiECommerceDetail != null)
                {
                    db.TBTransaksiECommerceDetails.DeleteOnSubmit(TransaksiECommerceDetail);
                    db.SubmitChanges();
                }

                LoadData();
            }
        }
    }

    private void LoadData()
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            PelangganLogin Pelanggan = (PelangganLogin)Session["PelangganLogin"];

            //MENCARI TRANSAKSI SESSION
            var TransaksiECommerceDetail = db.TBTransaksiECommerceDetails
                .Where(item => item.TBTransaksiECommerce.IDPelanggan == Pelanggan.IDPelanggan);

            if (TransaksiECommerceDetail.Count() > 0)
            {
                MultiView1.ActiveViewIndex = 1;

                RepeaterCart.DataSource = TransaksiECommerceDetail
                    .Select(item => new
                    {
                        item.IDTransaksiECommerceDetail,
                        Foto = "/images/cover/" + item.TBStokProduk.TBKombinasiProduk.IDProduk + ".jpg",
                        item.TBStokProduk.TBKombinasiProduk.Nama,
                        item.Quantity,
                        Harga = item.TBStokProduk.HargaJual,
                        Total = (item.Quantity * item.TBStokProduk.HargaJual)
                    })
                    .ToArray();
                RepeaterCart.DataBind();
            }
            else
                MultiView1.ActiveViewIndex = 0;
        }
    }

    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            bool isPerubahan = false;

            foreach (RepeaterItem item in RepeaterCart.Items)
            {
                HiddenField HiddenFieldIDTransaksiECommerceDetail = (HiddenField)item.FindControl("HiddenFieldIDTransaksiECommerceDetail");
                TextBox TextBoxQuantity = (TextBox)item.FindControl("TextBoxQuantity");

                var TransaksiECommerceDetail = db.TBTransaksiECommerceDetails
                    .FirstOrDefault(item2 => item2.IDTransaksiECommerceDetail == HiddenFieldIDTransaksiECommerceDetail.Value.ToInt());

                if (TransaksiECommerceDetail != null)
                {
                    if (TextBoxQuantity.Text.ToInt() > 0)
                        TransaksiECommerceDetail.Quantity = TextBoxQuantity.Text.ToInt();
                    else
                        db.TBTransaksiECommerceDetails.DeleteOnSubmit(TransaksiECommerceDetail);

                    isPerubahan = true;
                }
            }

            //JIKA ADA PERUBAHAN DATA
            if (isPerubahan)
                db.SubmitChanges();
        }

        LoadData();
    }
}