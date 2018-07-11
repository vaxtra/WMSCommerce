<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="_Product.aspx.cs" Inherits="_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Literal ID="LiteralNama" runat="server"></asp:Literal><br />
    <asp:Literal ID="LiteralHarga" runat="server"></asp:Literal><br />
    <asp:Literal ID="LiteralDeskripsi" runat="server"></asp:Literal><br />
    <br />

    <asp:Repeater ID="RepeaterFoto" runat="server">
        <ItemTemplate>
            <img src='<%# Eval("Foto") %>' style="width: 100px; height: 100px;" />
        </ItemTemplate>
    </asp:Repeater>

    <br />
    <br />

    <asp:DropDownList ID="DropDownListStokProduk" runat="server"></asp:DropDownList>
    <asp:TextBox ID="TextBoxQuantity" runat="server" Text="1"></asp:TextBox>
    <asp:Button ID="ButtonAddToCart" runat="server" Text="Add to Cart" OnClick="ButtonAddToCart_Click" />
    <asp:Button ID="ButtonRemove" runat="server" Text="xxxx" OnClick="ButtonRemove_Click" />
    <asp:Button ID="ButtonRemoveSession" runat="server" Text="Session" OnClick="ButtonRemoveSession_Click" />
</asp:Content>
