<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_Toastr_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <button type="button" class="btn btn-info" onclick="AlertMessage('info', 'Message Alert')">Info</button>
    <button type="button" class="btn btn-info" onclick="AlertMessage('info', 'Title', 'Message Alert')">Info</button>
    <button type="button" class="btn btn-info" onclick="AlertMessageRedirect('info', 'Title', 'Default.aspx')">Info</button>
    <br />
    <br />
    <button type="button" class="btn btn-warning" onclick="AlertMessage('warning', 'Message Alert')">Warning</button>
    <button type="button" class="btn btn-warning" onclick="AlertMessage('warning', 'Title', 'Message Alert')">Warning</button>
    <button type="button" class="btn btn-warning" onclick="AlertMessageRedirect('warning', 'Title', 'Default.aspx')">Warning</button>
    <br />
    <br />
    <button type="button" class="btn btn-success" onclick="AlertMessage('success', 'Message Alert')">Success</button>
    <button type="button" class="btn btn-success" onclick="AlertMessage('success', 'Title', 'Message Alert')">Success</button>
    <button type="button" class="btn btn-success" onclick="AlertMessageRedirect('success', 'Title', 'Default.aspx')">Success</button>
    <br />
    <br />
    <button type="button" class="btn btn-danger" onclick="AlertMessage('danger', 'Message Alert')">Danger</button>
    <button type="button" class="btn btn-danger" onclick="AlertMessage('danger', 'Title', 'Message Alert')">Danger</button>
    <button type="button" class="btn btn-danger" onclick="AlertMessageRedirect('danger', 'Title', 'Default.aspx')">Danger</button>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Loading" CssClass="btn btn-primary btnLoading" />
    <a href="Default.aspx" class="btn btn-primary btnLoading">Hyperlink Loading</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

