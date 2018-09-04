<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Key.aspx.cs" Inherits="WITAdministrator_Login" %>

<!DOCTYPE html>
<meta http-equiv="content-type" content="text/html;charset=UTF-8" />
<head runat="server">
    <meta charset="utf-8" />
    <title>WIT. Enterprise System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link rel="icon" type="image/ico" href="/images/icon.ico" />

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
            <img class="mb-4" src="/images/logo_wms.png?w=72" />
            <h3 class="font-weight-light">WMS COMMERCE</h3>
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
        </div>

        <div class="form-label-group">
            <asp:TextBox ID="TextBoxStoreKey" runat="server" class="form-control" placeholder="Store Key" required autofocus></asp:TextBox>
            <label for="TextBoxStoreKey">Store Key</label>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <asp:Button ID="ButtonVerifikasi" runat="server" Text="Verifikasi" CssClass="btn btn-lg btn-primary btn-block" OnClick="ButtonVerifikasi_Click" />
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 text-center">
                <a href="Login.aspx" class="btn btn-link btn-sm">Login</a>
            </div>
        </div>
        <p class="mt-5 mb-3 text-black-50 text-center">Developed by <a href="http://wit.co.id" target="_blank">WIT. Indonesia</a>  &copy; 2018</p>
    </form>
</body>
