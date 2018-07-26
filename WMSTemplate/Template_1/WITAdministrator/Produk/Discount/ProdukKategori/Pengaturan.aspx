<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_Discount_Event_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pengaturan Discount
    <asp:Label ID="LabelGrupPelanggan" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonSimpan" runat="server" Style="font-weight: bold;" CssClass="btn btn-sm btn-primary hidden-print" Text="Simpan" OnClick="ButtonSimpan_Click" />
    <a href="Default.aspx" style="font-weight: bold;" class="btn btn-sm btn-danger hidden-print">Keluar</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="table-responsive">
        <table class="table table-bordered table-condensed table-hover TableSorter">
            <thead>
                <tr class="active">
                    <th class="fitSize">No.</th>
                    <th>Nama</th>
                    <th>Discount</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterProdukKategori" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Container.ItemIndex + 1 %>
                                <asp:HiddenField ID="HiddenFieldIDProdukKategori" runat="server" Value='<%# Eval("IDProdukKategori") %>' />
                            </td>
                            <td><%# Eval("Nama") %></td>
                            <td>
                                <asp:TextBox ID="TextBoxDiscount" runat="server"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

