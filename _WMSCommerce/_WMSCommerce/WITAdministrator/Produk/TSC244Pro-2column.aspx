<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSC244Pro-2column.aspx.cs" Inherits="WITAdministrator_Produk_TSC244Pro_2column" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 30mm;
            height: 13mm;
            font-size: 10px;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            margin: 10px 4px 0 14px;
            /*background-color: red;*/
            overflow: hidden;
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server">
        <div style="margin-left: -18px; margin-top: -12px;">
            <asp:Repeater ID="RepeaterBarcode" runat="server">
                <ItemTemplate>
                    <div class="barcode">
                        <span style="font-size:7px;"><%# Eval("Nama") %></span>
                        <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=25&lebar=112' style="margin-top: 1px;" />
                        <span style="float: left; margin-top: -1px; font-size:7px;"><%# Eval("Kode") %></span>
                        <span style="float: right; margin-top: -1px; font-size:8px;"><%# Eval("Harga") %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
