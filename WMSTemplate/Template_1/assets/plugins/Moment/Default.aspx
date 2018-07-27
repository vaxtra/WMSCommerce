<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_Moment_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="ParseDatetime">
        <%= DateTime.Now.AddHours(-2) %>
    </div>

    <div class="ParseDate">
        <%= DateTime.Now.AddHours(-3) %>
    </div>

    <div class="ParseTime">
        <%= DateTime.Now.AddHours(-4) %>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

