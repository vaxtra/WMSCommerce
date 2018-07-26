<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_JqueryValidation_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table>
        <tr>
            <td>Name (required, at least 2 characters)</td>
            <td>
                <input id="cname" name="name" minlength="2" type="text" required></td>
        </tr>
        <tr>
            <td>E-Mail (required)</td>
            <td>
                <input id="cemail" type="email" name="email" required></td>
        </tr>
        <tr>
            <td>URL (optional)</td>
            <td>
                <input id="curl" type="url" name="url"></td>
        </tr>
        <tr>
            <td>Your comment (required)</td>
            <td>
                <textarea id="ccomment" name="comment" required></textarea></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <select id="cselect" name="select" required>
                    <option value="">Choose Credit Card</option>
                    <option value="amex">American Express</option>
                    <option value="discover">Discover</option>
                    <option value="mastercard">MasterCard</option>
                    <option value="visa">Visa</option>
                </select></td>
        </tr>
        <tr>
            <td>Please agree to our policy</td>
            <td>
                <input id="ccheckbox" name="checkbox" type="checkbox" required></td>
        </tr>
    </table>

    <asp:Button ID="Button1" runat="server" Text="Button" />
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" formnovalidate />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

