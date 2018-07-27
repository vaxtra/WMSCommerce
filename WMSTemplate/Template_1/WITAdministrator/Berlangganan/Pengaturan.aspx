<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Berlangganan_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Berlangganan
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
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="form-group">
                <label class="form-label">Email</label>
                <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Telepon</label>
                <asp:TextBox ID="TextBoxNoTelepon" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </div>
            <asp:Button ID="ButtonOk" runat="server" CssClass="btn btn-success btn-sm" Text="Tambah" OnClick="ButtonOk_Click" />
            <asp:Button ID="ButtonKeluar" runat="server" CssClass="btn btn-danger btn-sm" Text="Keluar" OnClick="ButtonKeluar_Click" />
        </div>
    </div>
</asp:Content>

