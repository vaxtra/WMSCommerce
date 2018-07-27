<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="ProdukDetailPrint.aspx.cs" Inherits="WITReport_PerpindahanStok_ProdukDetailPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th>No.</th>
                        <th>Tanggal</th>
                        <th>Tempat</th>
                        <th>Pegawai</th>
                        <th>Kode</th>
                        <th>Produk</th>
                        <th>Varian</th>
                        <th>Kategori</th>
                        <th>Jenis</th>
                        <th>Jumlah</th>
                        <th>Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="2"></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPengguna" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKode" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariProduk" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariAtributProduk" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKategori" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariJenisPerpindahanStok" runat="server"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahHeader" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKeterangan" runat="server"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("Tanggal") %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td><%# Eval("Pengguna") %></td>
                                <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                <td><%# Eval("Produk") %></td>
                                <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="fitSize"><%# Eval("Jenis") %></td>
                                <td class="text-right fitSize"><%# Eval("Jumlah") %>
                                    <img src='<%# Pengaturan.FormatStatusStok((bool)Eval("Status")) %>' />
                                </td>
                                <td><%# Eval("Keterangan") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="9"></td>
                        <td style="text-align: right;">
                            <asp:Label ID="LabelTotalJumlahFooter" runat="server"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

