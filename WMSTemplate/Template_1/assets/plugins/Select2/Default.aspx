<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_Select2_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:TextBox ID="TextBox1" runat="server" class="select2Tag" Style="width: 100%;"></asp:TextBox>
    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script>
        $(document).ready(function () {
            $('.select2Tag').select2({ tags: ["red", "green", "blue"] });
        });
    </script>
</asp:Content>

