<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Password.aspx.cs" Inherits="WITAdministrator_Pengguna_UbahPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Ubah Password
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Old Password</label>
                        <asp:TextBox ID="TextBoxPasswordLama" TextMode="Password" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        <asp:Label ID="LabelWarning" runat="server" ForeColor="Red"></asp:Label>
                        <asp:RequiredFieldValidator ValidationGroup="UbahPassword" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data harus diisi"
                            ControlToValidate="TextBoxPasswordLama" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">New Password</label>
                                <asp:TextBox ID="TextBoxPasswordBaru" TextMode="Password" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="UbahPassword" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data harus diisi"
                                    ControlToValidate="TextBoxPasswordBaru" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">Confirm New Password</label>
                                <asp:TextBox ID="TextBoxConfirmPasswordBaru" TextMode="Password" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <asp:CompareValidator ValidationGroup="UbahPassword" ID="CompareValidator1" ControlToValidate="TextBoxPasswordBaru" ControlToCompare="TextBoxConfirmPasswordBaru" runat="server" ForeColor="Red" ErrorMessage="Confim new password" Display="Dynamic"></asp:CompareValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" ValidationGroup="UbahPassword" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

