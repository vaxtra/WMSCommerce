<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="plugins_BootstrapMultiselect_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <select class="BootstrapMultiselect" multiple="multiple">
        <option value="cheese">Cheese</option>
        <option value="tomatoes">Tomatoes</option>
        <option value="mozarella">Mozzarella</option>
        <option value="mushrooms">Mushrooms</option>
        <option value="pepperoni">Pepperoni</option>
        <option value="onions">Onions</option>
    </select>

    <%--class="BootstrapMultiselect" multiple="multiple"--%>
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="Select2Tag">
        <asp:ListItem Text="1" Value="1" />
        <asp:ListItem Text="2" Value="2" />
        <asp:ListItem Text="3" Value="3" />
        <asp:ListItem Text="4" Value="4" />
    </asp:DropDownList>

    <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple">
        <asp:ListItem Text="1" Value="1" />
        <asp:ListItem Text="2" Value="2" />
        <asp:ListItem Text="3" Value="3" />
        <asp:ListItem Text="4" Value="4" />
    </asp:ListBox>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

