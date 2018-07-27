<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeStock.aspx.cs" Inherits="WITAdministrator_Produk_BarcodeStock" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Barcode System</title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 45mm;
            height: 13mm;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 0px 18px 0 10px;
            overflow: hidden;
            word-spacing: -3px;
        }

        @media print {
            .no-print {
                display: none !important;
            }
        }
    </style>

    <script>
        window.print();
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="TimerBarcode" runat="server" Interval="3000" OnTick="TimerBarcode_Tick" Enabled="false"></asp:Timer>
        <div style="margin-left: -7px; display: table;">
            <asp:Repeater ID="RepeaterBarcodeBarcode" runat="server">
                <ItemTemplate>
                    <div style="margin-bottom: 70px;">
                        <asp:Repeater ID="RepeaterBarcodeBody" runat="server" DataSource='<%# Eval("Body") %>'>
                            <ItemTemplate>
                                <div class="barcode">
                                    <div style="white-space: nowrap; font-size: 10px; margin-bottom: 2px; text-align: left;"><%# Eval("Nama") %></div>
                                    <span style="white-space: nowrap; font-size: 10px;">
                                        <span style="float: left; overflow: hidden; width: 50%; text-align: left; margin-bottom: 1px;"><%# Eval("Varian") %></span>
                                        <span style="float: right; overflow: hidden; width: 50%; font-size: 7px; text-align: right;"><%# Eval("Warna") %></span>
                                    </span>
                                    <img style="margin-top: -3px;" src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=18&lebar=125' />
                                    <br />
                                    <span style="float: left; font-size: 10px; margin: -3px 0px 2px 0px; font-size: 7px;"><%# Eval("Kode") %></span>
                                    <span style="float: right; font-size: 10px; margin: -3px 2px 2px 0px; font-size: 9px;"><%# Eval("Harga") %></span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
