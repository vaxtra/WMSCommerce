<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POProduksi.aspx.cs" Inherits="WITEmail_POProduksi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="formWITManagementSystem" runat="server">
    <div style="color: rgb(0, 0, 0); font-family: Verdana; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0; text-transform: none; white-space: normal; widows: 2; word-spacing: 0; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0; font-size: medium;">
        <table bgcolor="#EEEEEE" border="0" cellpadding="0" cellspacing="0" style="-webkit-text-size-adjust: none; -webkit-font-smoothing: antialiased;" width="100%">
            <tr>
                <td colspan="1" rowspan="1">
                    <table bgcolor="#2E3C47" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" colspan="1" rowspan="1" style="padding: 0 15px;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="1" rowspan="1" style="font-family: Verdana; font-size: 21px; line-height: 25px; color: rgb(255, 255, 255); text-align: left;">{Store} - {Tempat}</td>
                                        <td colspan="1" height="64" rowspan="1" width="22"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table align="center" bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" colspan="1" rowspan="1">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" colspan="1" rowspan="1">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" bgcolor="#FFFFFF" colspan="1" height="40" rowspan="1" valign="top" width="580">
                                                        <div>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td colspan="1" rowspan="1" style="background-color: rgb(255, 255, 255); padding: 0 16px; font-family: Verdana; font-size: 21px; line-height: 21px; height: 40px; color: rgb(61, 69, 76); text-align: center; background-position: initial initial; background-repeat: initial initial;">Jatuh Tempo</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="1" rowspan="1">
                                            <table border="1" cellpadding="5" cellspacing="0" style="font-family: Verdana; font-size: 16px; line-height: 24px; color: rgb(61, 69, 76); text-align: left;">
                                                <thead>
                                                    <tr style="background-color:grey; color:white; text-align:center;">
                                                        <th>No</th>
                                                        <th>ID</th>
                                                        <th>Supplier</th>
                                                        <th>Tanggal</th>
                                                        <th>Jatuh Tempo</th>
                                                        <th>Jarak</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" colspan="1" rowspan="1" style="font-family: Verdana; font-size: 13px; line-height: 26px; color: rgb(95, 106, 124); text-align: center; padding: 24px 20px 0;">
                                {Logo}<br />
                                Warehouse Management System Powered by <a href="http://wit.co.id/" target="_blank">WIT.</a><span>&nbsp;</span><br clear="none" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1" rowspan="1">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td colspan="1" rowspan="1" style="padding: 10px 0 30px;" valign="top"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
