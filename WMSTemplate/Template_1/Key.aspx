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

    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server" class="site-wrapper">
        <div class="site-wrapper-inner">
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"></div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                <div class="row">
                    <div class="col-md-1 col-lg-2"></div>
                    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-8">
                        <div class="inner cover">
                            <div class="row">
                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                    <img src="/images/logo_wms.png" data-src="/images/logo_wms.png" class="pull-left" style="margin-bottom: 20px; height: 100px;" />
                                </div>
                                <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9">
                                    <h1 class="text-right">STORE KEY</h1>
                                    <h4 class="text-right">WIT. Management System</h4>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBoxStoreKey" runat="server" CssClass="customtextbox" placeholder="Store Key" onfocus="this.select();"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                                <a href="Login.aspx" class="btn btn-danger btn-block" style="background: #2cb5e8; border-color: #2cb5e8;">Login</a>
                                            </div>
                                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                                <asp:Button ID="ButtonVerifikasi" runat="server" Text="Verifikasi" CssClass="btn btn-success btn-block" Style="background: #0fb8ad; border-color: #0fb8ad;" OnClick="ButtonVerifikasi_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <p class="text-center">Supported System by <a href="https://wit.co.id">WIT.</a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1 col-lg-2"></div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"></div>
        </div>
        <script src="assets/js/bootstrap.min.js"></script>
    </form>
</body>
