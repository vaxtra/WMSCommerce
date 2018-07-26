<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="WITAdministrator_Login" %>

<!DOCTYPE html>

<head runat="server">
    <%--    <link rel="icon" href="../images/Support/fav.png" />--%>
    <title>WIT. Management System</title>

    <!-- Bootstrap core CSS -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="/assets/Plugins/CustomWIT/floating-labels.css" rel="stylesheet" />
    <link href="/assets/Plugins/CustomWIT/css-backend.css" rel="stylesheet" />

    <style type="text/css">
        :root {
            --input-padding-x: .75rem;
            --input-padding-y: .75rem;
        }

        html,
        body {
            height: 100%;
        }

        body {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: center;
            align-items: center;
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }
    </style>
</head>
<body class="bg-light">
    <form id="formWITEnterpriseSystem" runat="server" class="form-signin">
        <div class="text-center mb-4">
            <img class="mb-4" src="/images/logo_wms.png?w=72" />
            <h3 class="font-weight-light">WIT. MANAGEMENT SYSTEM</h3>
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
        </div>

        <div class="form-label-group">
            <asp:TextBox ID="TextBoxUsername" runat="server" class="form-control" placeholder="Username" required autofocus></asp:TextBox>
            <label for="TextBoxUsername">Username</label>
        </div>

        <div class="form-label-group">
            <asp:TextBox ID="TextBoxPassword" runat="server" class="form-control" TextMode="Password" placeholder="Password" required autofocus></asp:TextBox>
            <label for="TextBoxPassword">Password</label>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">
                <asp:Button ID="ButtonLogin" runat="server" class="btn btn-lg btn-block text-white" Text="Login" style="background: #02aab0; border-color: #02aab0;" OnClick="ButtonLogin_Click" />
            </div>
            <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">
                <a href="Key.aspx" class="btn btn-danger btn-lg btn-block text-white" style="background: #00cdac; border-color: #00cdac;">Store Key</a>
            </div>
        </div>
        <p class="mt-5 mb-3 text-black-50 text-center">Developed by <a href="http://wit.co.id" target="_blank">WIT. Indonesia</a>  &copy; 2013</p>
    </form>
</body>
