<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArgoxOS214-2column.aspx.cs" Inherits="WITAdministrator_Produk_ArgoxOS214_2column" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Barcode System</title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 30mm;
            height: 13mm;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 0 0 0 50px;
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
        <div style="margin-left: 0px; display: table;">
            <asp:Repeater ID="RepeaterBarcode2" runat="server">
                <ItemTemplate>
                    <div class="barcode">
                        <div style="white-space: nowrap; font-size: 9px; margin-bottom: -2px;"><%# Eval("Nama") %></div>
                        <span style="white-space: nowrap; font-size: 10px;">
                            <span style="float: left; overflow: hidden; width: 50%; text-align: left; margin-bottom: 1px;"><%# Eval("Varian") %></span>
                            <span style="float: right; overflow: hidden; width: 50%; font-size: 7px; text-align: right;"><%# Eval("Warna") %></span>
                        </span>
                        <img style="margin-top: -3px;" src='/plugins/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=24&lebar=125' />
                        <span style="float: left; margin-top: -3px; font-size: 7px;"><%# Eval("Kode") %></span>
                        <span style="float: right; margin-top: -3px; font-size: 9px;"><%# Eval("Harga") %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="no-print" style="font-family: Tahoma; margin-left: 600px;" runat="server" id="PanelKeterangan">
                <h3>Jumlah Print :
                    <asp:Label ID="LabelJumlahPrint" runat="server"></asp:Label></h3>
                <h3>Tidak Terpakai :
                    <asp:Label ID="LabelTidakTerpakai" runat="server"></asp:Label></h3>
            </div>
        </div>
    </form>
</body>
</html>
