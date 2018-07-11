<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Separate.aspx.cs" Inherits="WITAdministrator_Produk_Separate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUploadTransfer" runat="server" /><asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" /><br />
        <asp:Label ID="LabelRows" runat="server" Text=""></asp:Label>

        <asp:Literal ID="LiteralIndex" runat="server"></asp:Literal>

    </div>
    </form>
</body>
</html>
