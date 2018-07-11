<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Template_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Style="font-weight: bold; margin-bottom: 5px;" Text="Sangat tidak puas" /><br />
    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Style="font-weight: bold; margin-bottom: 5px;" Text="Tidak puas" /><br />
    <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Style="font-weight: bold; margin-bottom: 5px;" Text="Biasa" /><br />
    <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" Style="font-weight: bold; margin-bottom: 5px;" Text="Puas" /><br />
    <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary" Style="font-weight: bold; margin-bottom: 5px;" Text="Sangat puas" /><br />


    <select id="mySel2" class="select2" multiple="multiple" style="width: 400px;">
        <option>One</option>
        <option>Two</option>
        <option>Three</option>
        <option>Four</option>
        <option>Five</option>
        <option>Six</option>
    </select>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script>
        $("#mySel2").selectpicker({
            multiple: true
        });
    </script>
</asp:Content>

