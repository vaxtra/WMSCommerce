<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Store_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Store
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card h-100">
                    <h4 class="card-header bg-smoke">Profil</h4>
                    <div class="card-body">
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Nama</label>
                            <asp:TextBox CssClass="form-control input-sm" ID="TextBoxNama" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Alamat</label>
                            <asp:TextBox CssClass="form-control input-sm" ID="TextBoxAlamat" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Email</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxEmail" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Kode Pos</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxKodePos" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Handphone</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxHandphone" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Telepon Lain</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxTeleponLain" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Website</label>
                            <asp:TextBox CssClass="form-control input-sm" ID="TextBoxWebsite" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card">
                    <h4 class="card-header bg-smoke">Konfigurasi</h4>
                    <div class="card-body">
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Upload Logo (height: 115px & Width: 380px)</label>

                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="FileUploadLogo" runat="server" Width="100%" /></td>
                                    <td style="width: 20%;">
                                        <asp:Button ID="ButtonUpload" CssClass="btn btn-primary btn-xs pull-right" runat="server" Text="Upload" OnCommand="ButtonUpload_Command" /></td>
                                </tr>
                            </table>
                            <div id="warning" runat="server" class="alert alert-danger" visible="false">
                                <asp:Label ID="LabeWarningLogo" runat="server"></asp:Label>
                            </div>
                            <table border="1" style="width: 100%; background: #ddd">
                                <tr>
                                    <td></td>
                                    <td style="width: 380px; height: 115px;">
                                        <asp:Image ID="ImageLogo" runat="server" /></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">SMTP Server</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxSMTPServer" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">SMTP Port</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxSMTPPort" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">SMTP User</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxSMTPUser" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">SMTP Password</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxSMTPPassword" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">SSL</label>
                            <div class="checkbox">
                                <asp:CheckBox ID="CheckBoxSecureSocketsLayer" Text="Enable SSL" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Send a test e-mail to</label>
                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxPercobaanEmail" runat="server"></asp:TextBox>
                                    <asp:Label ID="LabelWarning" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted" style="color: white;">Label</label>
                                    <br />
                                    <asp:Button ID="ButtonPercobaanEmail" CssClass="btn btn-primary" runat="server" Text="Send an e-mail test" OnClick="ButtonPercobaanEmail_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" ValidationGroup="groupStore" />
    </div>
</asp:Content>

