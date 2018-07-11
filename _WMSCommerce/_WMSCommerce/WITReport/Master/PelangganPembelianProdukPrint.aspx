<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PelangganPembelianProdukPrint.aspx.cs" Inherits="WITReport_Master_PelangganPembelianProdukPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Repeater ID="RepeaterPembelianProduk" runat="server">
        <ItemTemplate>
            <div class="row">
                <div class="col-xs-12">
                    <h3>
                        <asp:Label ID="LabelNamaPelanggan" runat="server" Text='<%# Eval("NamaPelanggan") %>'></asp:Label></h3>
                </div>
            </div>

            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="hidden"></th>
                        <th class="text-center" style="width: 2%">No</th>
                        <th class="text-center">Pelanggan</th>
                        <th class="text-center">Waktu Transaksi</th>
                        <th class="text-center">Waktu Pembayaran</th>
                        <th class="text-center">Kode</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center">Harga Jual</th>
                        <th class="text-center">Jumlah</th>
                        <th class="text-center">Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="success text-right" style="font-weight: bold;">
                        <td colspan="7" class="text-center">TOTAL</td>
                        <td><%# Eval("TotalJumlahProduk").ToFormatHargaBulat() %></td>
                        <td><%# Eval("TotalJumlahSubtotal").ToFormatHarga() %></td>
                    </tr>
                    <asp:Repeater ID="RepeaterNamaProduk" runat="server" DataSource='<%# Eval("Produk") %>'>
                        <ItemTemplate>
                            <tr>
                                <td class="hidden"></td>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("NamaLengkap") %></td>
                                <td><%# Eval("TanggalTransaksi").ToFormatTanggalJam() %></td>
                                <td><%# Eval("TanggalPembayaran").ToFormatTanggalJam() %></td>
                                <td><%# Eval("Kode") %></td>
                                <td><%# Eval("Produk") %></td>
                                <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Subtotal").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success text-right" style="font-weight: bold;">
                        <td colspan="7" class="text-center">TOTAL</td>
                        <td><%# Eval("TotalJumlahProduk").ToFormatHargaBulat() %></td>
                        <td><%# Eval("TotalJumlahSubtotal").ToFormatHarga() %></td>
                    </tr>
                </tbody>
            </table>

            <hr />

        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

