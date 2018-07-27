<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="TransaksiDetailPrint.aspx.cs" Inherits="WITReport_Transaksi_TransaksiDetailPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th style="width: 2%">No.</th>
                        <th style="width: 8%">Transaksi</th>
                        <th style="width: 7%">Tanggal</th>
                        <th style="width: 5%">Pelanggan</th>
                        <th style="width: 8%">Kode</th>
                        <th style="width: 5%">Brand</th>
                        <th>Produk</th>
                        <th style="width: 5%">Varian</th>
                        <th style="width: 15%">Kategori</th>
                        <th style="width: 8%">Harga Jual</th>
                        <th style="width: 8%">Potongan Harga</th>
                        <th style="width: 8%">Jumlah</th>
                        <th style="width: 8%">Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariIDTransaksi" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTanggal" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPelanggan" runat="server"></asp:Label></td>
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
                        <td></td>
                        <td></td>
                        <td class="text-right">
                            <asp:Label ID="LabelJumlahProdukHeader" runat="server" Text="0"></asp:Label></td>
                        <td class="text-right">
                            <asp:Label ID="LabelSubtotalHeader" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("IDTransaksi") %></td>
                                <td class="fitSize"><%# Eval("TanggalTransaksi") %></td>
                                <td class="fitSize"><%# Eval("Pelanggan") %></td>
                                <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                <td class="fitSize"><%# Eval("Produk") %></td>
                                <td class="text-center"><%# Eval("AtributProduk") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="text-right"><%# Eval("HargaJual") %></td>
                                <td class="text-right"><%# Eval("PotonganHargaJual") %></td>
                                <td class="text-right"><%# Eval("JumlahProduk") %></td>
                                <td class="text-right"><%# Eval("Subtotal") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success text-right" style="font-weight: bold;">
                        <td colspan="11"></td>
                        <td>
                            <asp:Label ID="LabelJumlahProdukFooter" runat="server" Text="0"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelSubtotalFooter" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

