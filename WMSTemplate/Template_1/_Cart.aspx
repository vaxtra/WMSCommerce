<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="_Cart.aspx.cs" Inherits="_Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            Keranjang belanja Anda kosong.<br />
            <a href="_Default.aspx">Lanjutkan Belanja</a>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <table>
                <thead>
                    <tr>
                        <th>Produk</th>
                        <th>Harga</th>
                        <th>Jumlah</th>
                        <th>Total</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterCart" runat="server" OnItemCommand="RepeaterCart_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="HiddenFieldIDTransaksiECommerceDetail" runat="server" Value='<%# Eval("IDTransaksiECommerceDetail") %>' />
                                    <%# Eval("Nama") %><br />
                                    <img src='<%# Eval("Foto") %>' style="height: 190px; width: 190px;" /><br />
                                    <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDTransaksiECommerceDetail") %>' />
                                </td>
                                <td><%# Eval("Harga").ToFormatHarga() %></td>
                                <td>
                                    <asp:TextBox ID="TextBoxQuantity" runat="server" Text='<%# Eval("Quantity") %>' TextMode="Number"></asp:TextBox></td>
                                <td><%# Eval("Total").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br />
            <br />
            <br />
            <a href="_Default.aspx">Lanjutkan Belanja</a><br />
            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" /><br />
            <a href="_Checkout.aspx">Lanjutkan Pembayaran</a><br />
        </asp:View>
    </asp:MultiView>
</asp:Content>
