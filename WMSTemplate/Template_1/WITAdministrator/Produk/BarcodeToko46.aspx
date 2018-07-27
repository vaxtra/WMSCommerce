<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeToko46.aspx.cs" Inherits="WITAdministrator_Produk_BarcodeToko46" %>

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
            /*margin: 10px 4px 0 14px;*/
            overflow: hidden;
            width: 60mm;
        }
    </style>
    <script>
        window.print();
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server">
        <div style="padding: 20px 20px 20px 20px; margin-top:310px; background-color: black; height: 100mm; width: 70mm; transform:rotate(270deg); transform-origin: top left; text-transform: uppercase;">
            <div style="background-color: white; height: 90mm; width: 60mm; padding: 20px 20px 20px 20px;">
                <asp:Repeater ID="RepeaterBarcode" runat="server">
                    <ItemTemplate>
                        <table>
                            <tr >
                                <td colspan="3"><h3><%# Eval("Nama") %></h3></td>
                            </tr>
                            <tr>
                                <td>COLOR</td>
                                <td>:</td>
                                <td><%# Eval("Warna") %></td>
                            </tr>
                            <tr>
                                <td width="100">LENGTH</td>
                                <td width="10">:</td>
                                <td><%# Eval("Varian") %></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align:center;" >
                                    <br /><br />
                                    <div class="barcode">
                                        <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=35&lebar=125' style="margin-top: 1px;" /><br />
                                        <%# Eval("Kode") %>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>


            </div>

        </div>
    </form>
</body>
</html>
