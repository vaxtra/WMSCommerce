<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPIN.aspx.cs" Inherits="WITAdministrator_LoginPIN" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" type="image/ico" href="/images/icon.ico" />

    <title>Login PIN</title>

    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/login.css" rel="stylesheet" />
    <link href="../assets/css/wms-customPOS.css" rel="stylesheet" />

    <!--[if lt IE 9]>
      <script src="/plugins/extra/html5shiv.min.js"></script>
      <script src="/plugins/extra/respond.min.js"></script>
    <![endif]-->

    <script>
        function Func_TextBoxPIN(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonLogin');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        //Fungsi untuk isi textbox dari button
        function setValueTextbox(id, value) {
            document.getElementById(id).value += value;
        }
    </script>
</head>
<body>
    <form id="formWITEnterpriseSystem" runat="server" class="site-wrapper">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="site-wrapper-inner">
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"></div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                <div class="row">
                    <div class="col-md-2 col-lg-3"></div>
                    <div class="col-xs-12 col-sm-12 col-md-8 col-lg-6">
                        <div class="inner cover">
                            <div class="row">
                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                    <img src="/images/logo_wms.png" data-src="/images/logo_wms.png" class="pull-left" style="margin-bottom: 20px; height: 100px;" />
                                </div>
                                <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9">
                                    <h1 class="text-right">PIN</h1>
                                    <h4 class="text-right">WIT. Management System</h4>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <h2>
                                        <asp:TextBox ClientIDMode="Static" ID="TextBoxPIN" TextMode="Password" CssClass="form-control" autocomplete="off" Style="font-size:36px; font-weight: bold; width: 100%; height:60px;" runat="server" onkeypress="return Func_TextBoxPIN(event)" onFocus="this.select();"></asp:TextBox></h2>
                                    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                                    <table style="width: 100%;" class="table table-bordered">
                                        <tr>
                                            <td style="width: 25%;">
                                                <input id="Button1" type="button" value="1" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '1');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="Button2" type="button" value="2" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '2');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="Button3" type="button" value="3" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '3');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="ButtonClear" type="button" value="Clear" style="height: 60px; font-weight: bold;" class="btn btn-warning btn-block" onclick="document.getElementById('TextBoxPIN').value = '';" onkeydown="Func_TextBoxPIN(event)" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">
                                                <input id="Button4" type="button" value="4" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '4');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="Button5" type="button" value="5" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '5');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="Button6" type="button" value="6" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '6');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;" rowspan="3">
                                                <asp:Button
                                                    ID="ButtonLogin"
                                                    ClientIDMode="Static"
                                                    runat="server"
                                                    Text="GO"
                                                    data-loading-text="Loading..."
                                                    CssClass="btn btn-success btn-block proses"
                                                    Style="height: 214px; font-weight: bold;"
                                                    OnClick="ButtonLogin_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">
                                                <input id="Button7" type="button" value="7" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '7');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="Button8" type="button" value="8" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '8');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                            <td style="width: 25%;">
                                                <input id="Button9" type="button" value="9" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '9');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 75%;" colspan="3">
                                                <input id="Button0" type="button" value="0" style="height: 60px; font-weight: bold;" class="btn btn-default btn-block" onclick="setValueTextbox('TextBoxPIN', '0');" onkeydown="Func_TextBoxPIN(event)" /></td>
                                        </tr>
                                    </table>
                                    <p class="text-center">Supported System by <a href="https://wit.co.id">WIT.</a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2 col-lg-3"></div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"></div>
        </div>
        <script src="assets/js/bootstrap.min.js"></script>
    </form>
</body>
</html>

