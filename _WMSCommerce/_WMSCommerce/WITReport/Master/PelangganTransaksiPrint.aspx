<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PelangganTransaksiPrint.aspx.cs" Inherits="WITReport_Master_PelangganTransaksiPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="info">
                        <th colspan="13" style="font-size: 14pt; text-align: left;"><strong>
                            <asp:Label ID="LabelNamaPelanggan" runat="server"></asp:Label></strong></th>
                    </tr>
                    <tr class="active">
                        <th>No</th>
                        <th>ID Transaksi</th>
                        <th>Tanggal Transaksi</th>
                        <th>Tanggal Pembayaran</th>

                        <th>Produk</th>
                        <th>Varian</th>
                        <th>Jumlah</th>
                        <th>Retur</th>
                        <th>Grandtotal</th>
                        <th>Pembayaran</th>
                        <th>Penagihan</th>
                        <th>Status</th>
                        <th>Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("IDTransaksi") %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("TanggalOperasional").ToFormatTanggalJam() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("TanggalPembayaran").ToFormatTanggalJam() %></td>
                                <td class="fitSize"><%# Eval("Produk.Produk") %></td>
                                <td class="fitSize text-center"><%# Eval("Produk.AtributProduk") %></td>
                                <td class="fitSize text-right"><%# Eval("Produk.JumlahProduk").ToFormatHargaBulat() %></td>
                                <td class="fitSize text-right"><%# Eval("Produk.Retur").ToFormatHargaBulat() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-center warning fitSize"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right success fitSize"><strong><%# Eval("TotalPembayaran").ToFormatHarga() %></strong></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right info fitSize"><strong><%# Eval("Penagihan").ToFormatHarga() %></strong></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Keterangan") %></td>
                            </tr>
                            <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("Detail") %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Eval("Produk") %></td>
                                        <td class="fitSize text-center"><%# Eval("AtributProduk") %></td>
                                        <td class="fitSize text-right"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                        <td class="fitSize text-right"><%# Eval("Retur").ToFormatHargaBulat() %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

