<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZebraGT820Column2.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 5cm;
            height: 3cm;
            font-size: 14px;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 8px 9.5px 0 1px;
            /*background-color: red;*/
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server">
        <div style="margin-left: -5px; margin-top: -16px;">
            <asp:Repeater ID="RepeaterBarcode" runat="server">
                <ItemTemplate>
                    <div class="barcode">
                        <%# Eval("Nama") %>
                        <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=70&lebar=185' />
                        <%# Eval("Kode") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>