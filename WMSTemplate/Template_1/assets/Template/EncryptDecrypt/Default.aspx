<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="plugins_EncryptDecrypt_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="ButtonEncrypt" runat="server" Text="Encrypt" OnClick="ButtonEncrypt_Click" />
        <asp:Button ID="ButtonDecrypt" runat="server" Text="Decrypt" OnClick="DecryptFile" />
    
    
    
    </form>
</body>
</html>
