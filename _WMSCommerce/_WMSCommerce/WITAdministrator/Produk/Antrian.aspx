<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Antrian.aspx.cs" Inherits="WITAdministrator_Produk_Barcode" %>

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
                <table>
                    <tr>
                        <td style="font-family:Tahoma; font-size:18px; font-weight:bold; text-align:center;">EXHIBITION SURABAYA</td>
                    </tr>
                    <tr>
                        <td><img style="margin-top: -1px;" src="/images/logo.jpg" /></td>
                    </tr>
                    <tr>
                        <td style="text-align:center; font-family:Tahoma; font-size:72px"><asp:Label ID="LabelNomor" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-family:Tahoma; font-size:16px; text-align:center;">www.erigostore.com</td>
                    </tr>
                    <tr>
                        <td style="font-family:Tahoma; font-size:11px; text-align:center;">Powered by WIT. Management System</td>
                    </tr>
                </table>


                
                
               


                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
