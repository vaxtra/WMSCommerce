<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_FormatCurrency_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="sample">
        <div>Input Desimal</div>
        <input type="text" id="Test" class="InputDesimal" />

        <div>Input Integer</div>
        <input type="text" id="Test2" class="InputInteger" />
        <br />
        <br />

        Desimal : 
        <label class="OutputDesimal">10000000.65</label><br />
        Integer : 
        <label class="OutputInteger">10000000</label>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

