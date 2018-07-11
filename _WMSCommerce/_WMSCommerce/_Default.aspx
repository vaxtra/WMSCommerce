<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="_Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Repeater ID="RepeaterProduk" runat="server">
        <ItemTemplate>
            <%# Eval("Nama") %>
            <img src='<%# Eval("Foto") %>' style="width: 50px; height: 50px;" />
            <%# Eval("Harga").ToFormatHarga() %>
            <a href='/_Product.aspx?id=<%# Eval("IDProduk") %>'>Link</a>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
