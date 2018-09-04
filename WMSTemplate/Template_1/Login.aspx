<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="WITAdministrator_Login" %>

<!DOCTYPE html>

<head runat="server">
    <%--    <link rel="icon" href="../images/Support/fav.png" />--%>
    <title>LOGIN</title>

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
        }
    </style>
</head>
<body class="bg-light">
    <form id="formWITEnterpriseSystem" runat="server" class="form-signin">
        <div class="text-center mb-4">
            <img class="mb-4" src="/images/logo_wit.png?w=72" />
            <h3 class="font-weight-light">WMS COMMERCE</h3>
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
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <asp:Button ID="ButtonLogin" runat="server" class="btn btn-lg btn-success btn-block" Text="Login" OnClick="ButtonLogin_Click" />
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 text-center">
                <a href="Key.aspx" class="btn btn-link btn-sm">Store Key</a>
            </div>
        </div>
        <p class="mt-5 mb-3 text-black-50 text-center">Developed by <a href="http://wit.co.id" target="_blank">WIT. Indonesia</a>  &copy; 2018</p>
    </form>
</body>
