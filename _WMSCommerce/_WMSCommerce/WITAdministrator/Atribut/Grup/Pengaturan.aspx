<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Atribut_Grup_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Grup Atribut
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-horizontal">
        <div class="form-group form-group-sm">
            <label class="col-sm-3 control-label">Nama</label>
            <div class="col-sm-9">
                <asp:TextBox ID="TextBoxNama" runat="server" CssClass="form-control" required></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:Button ID="ButtonOk" runat="server" CssClass="btn btn-sm btn-primary" Text="Ok" OnClick="ButtonOk_Click" />
                <asp:Button ID="ButtonKeluar" runat="server" CssClass="btn btn-sm btn-danger" Text="Keluar" OnClick="ButtonKeluar_Click" formnovalidate />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

