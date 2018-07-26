<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Barcode.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Barcode System</title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 30mm;
            height: 14mm;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin-top:8px;
            overflow: hidden;
            word-spacing: -3px;
			margin-left:5px;
        }
		
		#formWITEnterpriseSystem > div .barcode:nth-child(2)
		{
			margin-left:16px;
		}
		
		#formWITEnterpriseSystem > div .barcode:nth-child(3)
		{
			margin-left:25px;
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
                        <div style="white-space: nowrap; font-size: 8px; margin-bottom: -2px; text-transform: uppercase";><%# Eval("Nama") %></div>
                        <span style="white-space: nowrap; font-size: 8px;">
                            <span style="float: left; overflow: hidden; width: 50%; text-align: left; margin-bottom: 1px;"><%# Eval("Varian") %></span>
                            <span style="float: right; overflow: hidden; width: 50%; font-size: 7px; text-align: right;"><%# Eval("Warna") %></span>
                        </span>
                        <img style="margin-top: -3px;" src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=20&lebar=125' />
                        <span style="float: left; margin-top: -3px; margin-left: 8px; font-size: 7px;"><%# Eval("Kode") %></span>
                        <%--<span style="float: right; margin-top: -3px; font-size: 8px;"><%# Eval("Harga") %></span>--%>
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
