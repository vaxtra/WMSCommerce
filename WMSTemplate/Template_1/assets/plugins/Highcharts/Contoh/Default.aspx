<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="plugins_highcharts_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container" style="width: auto; height: 600px; margin: 0 auto;"></div>

        <script src="/WITPointOfSales/js/jquery.min.js"></script>

        <script src="/plugins/highcharts/highcharts.js"></script>
        <script src="/plugins/highcharts/modules/exporting.js"></script>

        <asp:Literal ID="LiteralChart" runat="server"></asp:Literal>
    </form>
</body>
</html>
