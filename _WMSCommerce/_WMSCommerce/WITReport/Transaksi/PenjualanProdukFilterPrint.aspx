<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PenjualanProdukFilterPrint.aspx.cs" Inherits="WITReport_Transaksi_PenjualanProdukFilterPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <asp:Repeater ID="RepeaterLaporan" runat="server">
                    <ItemTemplate>
                        <thead>
                            <tr class="info">
                                <td colspan="12" style="font-size: 14pt; text-align: left;"><strong>
                                    <asp:Label ID="LabelNama" runat="server" Text='<%# Eval("Nama") %>'></asp:Label></strong></td>
                            </tr>
                            <tr class="active">
                                <th>No.</th>
                                <th>Kode</th>
                                <th>Brand</th>
                                <th>Produk</th>
                                <th>Varian</th>
                                <th>Kategori</th>
                                <th>Jumlah</th>
                                <th>Harga Pokok</th>
                                <th>Harga Jual</th>
                                <th>Potongan Harga</th>
                                <th>Subtotal</th>
                                <th>Net Sales</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                        <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                        <td><%# Eval("Produk") %></td>
                                        <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                        <td><%# Eval("Kategori") %></td>
                                        <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                        <td class="text-right fitSize"><%# Eval("HargaPokok").ToFormatHarga() %></td>
                                        <td class="text-right fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                        <td class="text-right fitSize"><%# Eval("PotonganHargaJual").ToFormatHarga() %></td>
                                        <td class="text-right fitSize"><%# Eval("Subtotal").ToFormatHarga() %></td>
                                        <td class="text-right warning fitSize"><strong><%# Eval("PenjualanBersih").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="text-right success">
                                <td colspan="6" class="text-center"><strong>TOTAL</strong></td>
                                <td><strong><%# Eval("TotalJumlahProduk").ToFormatHargaBulat() %></strong></td>
                                <td><strong><%# Eval("TotalHargaPokok").ToFormatHarga() %></strong></td>
                                <td><strong><%# Eval("TotalHargaJual").ToFormatHarga() %></strong></td>
                                <td><strong><%# Eval("TotalPotonganHargaJual").ToFormatHarga() %></strong></td>
                                <td><strong><%# Eval("TotalSubtotal").ToFormatHarga() %></strong></td>
                                <td><strong><%# Eval("TotalPenjualanBersih").ToFormatHarga() %></strong></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

