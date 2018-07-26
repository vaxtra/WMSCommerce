<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeGeoff.aspx.cs" Inherits="WITAdministrator_Produk_BarcodeGeoff" %>

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
            overflow: hidden;
            width:300px;
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
                    <table style="border-color: #fff; width: 600px; height: 300px; background: #000">
                        <tr>
                            <td colspan="2" style="border-bottom: 1px solid white !important;">
                                <label style="font-style: italic; font-size: 14pt; color: #fff;">MODEL NAME</label><br />
                                <label style="font-size: 25pt; color: #fff;"><%# Eval("Nama") %></label>
                            </td>

                        </tr>
                        <tr>
                            <td style="border-bottom: 1px solid white !important;">
                                <label style="font-style: italic; font-size: 14pt; color: #fff;">COLOR</label><br />
                                <label style="font-size: 25pt; color: #fff;"><%# Eval("Warna") %></label>
                            </td>
                            <td rowspan="2" style="border-left: 1px solid white !important; text-align: center">
                                <label style="font-size: 80pt; color: #fff; font-weight: bold;"><%# Eval("Varian") %></label><br />
                                <label style="font-size: 15pt; color: #fff; font-weight: bold; text-align: left">
                                    US 10
                                    <br />
                                    UK 9
                                    <br />
                                    CM 28</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <img src="../../images/geoff.jpg" / height="80"></td>
                                        <td style="background:#fff; text-align:center;">
                                            <div class="barcode">
                                                <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=35&lebar=122' style="margin-top: 1px;" /><br />
                                                <%# Eval("Kode") %>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>



                    <%--<div class="barcode">
                        <label style="font-size: 15px; color: #FFF; font-style: italic">MODEL NAME</label>
                        <span style="font-size: 20px; color: #fff"><%# Eval("Nama") %></span>
                        <img src='/assets/Template/barcode/Default.aspx?kode=<%# Eval("Kode") %>&tinggi=25&lebar=112' style="margin-top: 1px;" />
                        <span style="float: left; margin-top: -1px; font-size: 7px;"><%# Eval("Kode") %></span>
                        <span style="float: right; margin-top: -1px; font-size: 8px;"><%# Eval("Harga") %></span>
                    </div>--%>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
