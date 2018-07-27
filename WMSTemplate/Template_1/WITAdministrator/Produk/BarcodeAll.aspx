<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeAll.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .barcode {
            float: left;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 10px 18px 0 0;
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server">
        <div style="margin-left: -7px; margin-top: -5px; display: table;">
            <asp:Repeater ID="RepeaterBarcode" runat="server">
                <ItemTemplate>
                    <div>
                        <div class="barcode">
                            <img style="margin-top: -1px;" src='/assets/Template/Template/barcode/Default.aspx?kode=<%# Eval("KodeKombinasiProduk") %>&tinggi=24&lebar=125' />
                            <div style="white-space: nowrap; font-size: 9px; margin-top: -3px;"><%# Eval("KodeKombinasiProduk") %></div>
                        </div>
                        <div class="barcode">
                            <%# (int)(decimal)Eval("Berat") %>. 
                            <%# Eval("Nama").ToString().Replace("...", "") %>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
