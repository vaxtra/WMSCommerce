<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="BarcodeChecked.aspx.cs" Inherits="WITAdministrator_Produk_BarcodeChecked" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Print Barcode
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="form-group">
                <table class="table table-condensed table-hover table-bordered TableSorter">
                    <thead>
                        <tr class="active">
                            <th>No</th>
                            <th>Produk</th>
                            <th class="fitSize">Varian</th>
                            <th class="fitSize">Jumlah</th>
                            <th class="fitSize"></th>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="DropDownListProduk" CssClass="select2" Width="100%" runat="server">
                                </asp:DropDownList></td>
                            <td class="fitSize" colspan="2">
                                <asp:Button ID="ButtonTambah" CssClass="btn btn-primary btn-sm btn-block" runat="server" Text="Tambah" OnClick="ButtonTambah_Click" /></td>
                            <td>
                                <asp:Button ID="ButtonCetak" runat="server" class="btn btn-success btn-sm" Text="Cetak" OnClick="ButtonCetak_Click" /></td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterDetail" runat="server" OnItemCommand="RepeaterDetail_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td rowspan='<%# Eval("CountDetail").ToInt() + 1 %>' class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                    <td rowspan='<%# Eval("CountDetail").ToInt() + 1 %>'><%# Eval("Produk") %></td>
                                    <td colspan="2" style="padding: 0px; border-bottom: 0;"></td>
                                    <td rowspan='<%# Eval("CountDetail").ToInt() + 1 %>' class="text-center">
                                        <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDProduk") %>' />
                                    </td>
                                </tr>
                                <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                    <ItemTemplate>
                                        <tr class="warning">
                                            <td class="fitSize"><%# Eval("Atribut") %></td>
                                            <td class="fitSize bold"><%# Eval("Jumlah") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

