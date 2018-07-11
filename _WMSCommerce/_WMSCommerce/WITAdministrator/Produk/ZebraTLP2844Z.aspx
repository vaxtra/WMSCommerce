<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZebraTLP2844Z.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 48mm;
            height: 15mm;
            font-size: 8px;
            font-weight: bold;
            font-family: Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 3px -3px 0 24px;
            /*background-color: red;*/
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <%--    123456789012345678901234 24 digit--%>
    <form id="formWITEnterpriseSystem" runat="server">
        <div style="width: 380px;">
            <div style="margin-left: -35px;">
                <asp:Repeater ID="RepeaterBarcode" runat="server">
                    <ItemTemplate>
                        <div class="barcode">
                            <%# Eval("Nama") %>
                            <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=30&lebar=180' />
                            <span style="float: left; margin-top: -3px; font-size: 7px;"><%# Eval("Kode") %></span>
                            <span style="float: right; margin-top: -3px; font-size: 9px;"><%# Eval("Harga") %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
