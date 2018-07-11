<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="KomposisiBahanBaku.aspx.cs" Inherits="WITAdministrator_Import_KomposisiBahanBaku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Import Excel Komposisi Bahan Baku
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
    <div class="card">
        <div class="card-body">
            <div class="form-group">
                <a href="/file_excel/template/(Format) Komposisi Bahan Baku.xls">download Komposisi Bahan Baku List template</a>
            </div>
            <div class="form-group">
                <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            </div>
            <div class="form-group">
                <asp:FileUpload ID="FileUploadKomposisiBahanBaku" runat="server" CssClass="form-control" Width="100%" />
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID="ButtonUpload" CssClass="btn btn-success btn-const" runat="server" Text="Upload" OnClick="ButtonUpload_Click" />
            <a href="/WITAdministrator/BahanBaku/Komposisi/Default.aspx" class="btn btn-danger btn-const">Kembali</a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

