<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survei.aspx.cs" Inherits="WITSurvei_Survei_2" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">

    <link href="/WITPointOfSales/css/bootstrap.css" rel="stylesheet" />
    <link href="/WITPointOfSales/css/sticky-footer.css" rel="stylesheet" />
</head>
<body>
    <form id="formWITSurvei" runat="server">
        <div class="container">
            <asp:MultiView ID="MultiViewSurvei" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="page-header">
                        <h1>
                            <asp:Label ID="LabelJudul" runat="server"></asp:Label></h1>
                    </div>
                    <p>
                        <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                    </p>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Nama Lengkap</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="TextBoxNama" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Email</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Handphone</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="TextBoxHandphone" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <asp:Button ID="ButtonMulai" runat="server" Text="Mulai" OnClick="ButtonMulai_Click" class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="page-header text-right">
                        <asp:Label ID="LabelNomor" runat="server" Style="font-weight: bold; font-size: 17px;"></asp:Label>.
                    </div>
                    <div class="lead">
                        <asp:Label ID="LabelPertanyaan" Style="font-weight: bold;" runat="server"></asp:Label>

                        <div class="col-md-12" style="font-size: 17px;">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="RadioButtonListJawaban" runat="server" class="radio"></asp:RadioButtonList>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="ButtonSelanjutnya" runat="server" Text="Selanjutnya" OnClick="ButtonSelanjutnya_Click" class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div class="page-header">
                        <h1>Done!</h1>
                    </div>
                    <p>
                        Terima kasih Anda sudah bergabung dengan survei kami.
                    </p>
                </asp:View>
            </asp:MultiView>
        </div>

        <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
        <script src="/WITPointOfSales/js/ie10-viewport-bug-workaround.js"></script>
    </form>
</body>
</html>

