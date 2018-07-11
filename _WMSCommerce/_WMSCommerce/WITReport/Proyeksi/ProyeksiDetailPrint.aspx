<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="ProyeksiDetailPrint.aspx.cs" Inherits="WITReport_Proyeksi_ProyeksiDetailPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th>No</th>
                        <th>Kode</th>
                        <th>Brand</th>
                        <th>Produk</th>
                        <th>Varian</th>
                        <th>Kategori</th>
                        <th>Jumlah</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKode" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPemilikProduk" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariProduk" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariAtributProduk" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKategori" runat="server"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahHeaderProyeksiDetail" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                <td><%# Eval("Produk") %></td>
                                <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("Jumlah").ToFormatHargaBulat() %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="6"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahFooterProyeksiDetail" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

