<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeEvent.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .barcode {
            float: left;
            width: 30mm;
            height: 13mm;
            font-weight: bold;
            font-family: Lucida Console, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            text-align: center;
            margin: 10px 18px 0 0;
            overflow: hidden;
            word-spacing: -3px;
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenFieldID" runat="server" />
                <div style="margin-left: -7px; margin-top: -5px; display: table;">

                    <asp:Repeater ID="RepeaterBarcode" runat="server">
                        <ItemTemplate>
                            <div class="barcode">
                                <span style="margin-left: 13px; font-size: 8px;">System Powered by WIT.</span>
                                <img style="margin-top: -1px;" src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=24&lebar=125' />
                                <div style="white-space: nowrap; font-size: 9px; margin-top: -3px;"><%# Eval("Kode") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
