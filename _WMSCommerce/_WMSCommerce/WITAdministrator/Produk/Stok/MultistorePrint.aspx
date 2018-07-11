<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="MultistorePrint.aspx.cs" Inherits="WITAdministrator_Produk_Stok_MultiStorePrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="fitSize">No.</th>
                        <th class="fitSize">Kode</th>
                        <th class="fitSize">Produk</th>
                        <th class="fitSize">Brand</th>
                        <th class="fitSize">Kategori</th>
                        <th class="fitSize">Total</th>

                        <asp:Repeater ID="RepeaterTempat" runat="server">
                            <ItemTemplate>
                                <th class="fitSize"><%# Eval("Nama") %></th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </thead>
                <tbody>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="6"></td>
                        <td class="text-right fitSize"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TempatTotal"]) : "" %></td>
                        <asp:Repeater ID="RepeaterTotalTempat1" runat="server">
                            <ItemTemplate>
                                <td class="text-right fitSize"><%# Pengaturan.ReplaceNol(Eval("Total").ToFormatHargaBulat()) %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>

                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                <td class="fitSize"><%# Eval("Nama") %></td>
                                <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                <td class="fitSize"><%# Eval("KategoriProduk") %></td>
                                <td class="text-right fitSize info"><%# Pengaturan.ReplaceNol(Eval("Total").ToFormatHargaBulat()) %></td>

                                <asp:Repeater ID="RepeaterStokProduk" runat="server" DataSource='<%# Eval("StokProduk") %>'>
                                    <ItemTemplate>
                                        <td class="text-right fitSize"><%# Eval("Stok") != null ? Eval("Stok.Jumlah").ToFormatHargaBulat() : ""  %></td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr class="success" style="font-weight: bold;">
                        <td colspan="6"></td>
                        <td class="text-right fitSize"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TempatTotal"]) : "" %></td>
                        <asp:Repeater ID="RepeaterTotalTempat2" runat="server">
                            <ItemTemplate>
                                <td class="text-right fitSize"><%# Pengaturan.ReplaceNol(Eval("Total").ToFormatHargaBulat()) %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

