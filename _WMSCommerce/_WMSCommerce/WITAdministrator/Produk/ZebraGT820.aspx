<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZebraGT820.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 33mm;
            height: 13mm;
            font-size: 8px;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 8px 6px 0 2px;
            /*background-color: red;*/
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <%--    234050658--%>
    <form id="formWITEnterpriseSystem" runat="server">
        <div style="margin-left: -8px; margin-top: -16px;">
<%--                        <div class="barcode">
                ProdukProdukProdukProduk
                <img src="/assets/Template/barcode/Default.aspx?kode=23405065812341&tinggi=28&lebar=125" />
                23405065812341
            </div>

            <div class="barcode">
                Produk
                <img src="/assets/Template/barcode/Default.aspx?kode=23405065812341&tinggi=28&lebar=125" />
                23405065812341
            </div>

            <div class="barcode">
                Produk
                <img src="/assets/Template/barcode/Default.aspx?kode=23405065812341&tinggi=28&lebar=125" />
                23405065812341
            </div>--%>
            <asp:Repeater ID="RepeaterBarcode" runat="server">
                <ItemTemplate>
                    <div class="barcode">
                        <%# Eval("Nama") %>
                        <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=28&lebar=125' />
                        <%# Eval("Kode") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
