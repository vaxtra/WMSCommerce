<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="plugins_datetime_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/plugins/datetime/jquery.datetimepicker.css" />
</head>
<body>
    <form id="form1" runat="server">
        <input id="datetimepicker" type="text">

        <script src="/plugins/datetime/jquery.js"></script>
        <script src="/plugins/datetime/jquery.datetimepicker.js"></script>

        <script>
            jQuery('#datetimepicker').datetimepicker();
        </script>
    </form>


</body>
</html>
