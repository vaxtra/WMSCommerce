<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="DefaultPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="text-right" rowspan="2">No.</th>
                        <th rowspan="2"></th>
                        <th rowspan="2">Operasional</th>
                        <th class="text-center" colspan="2">Transaksi</th>
                        <th class="text-center" colspan="2">Update</th>
                        <th rowspan="2">Tempat</th>
                        <th rowspan="2">Jenis</th>
                        <th rowspan="2">Status</th>
                        <th rowspan="2">Pelanggan</th>
                        <th rowspan="2">Meja</th>
                        <th class="text-right" colspan="2">Jumlah</th>
                        <th class="text-right" rowspan="2">
                            <abbr title="Subtotal sebelum discount">Subtotal</abbr>
                        </th>
                        <th class="text-center" colspan="3">Discount</th>
                        <th class="text-right" rowspan="2">
                            <abbr title="Subtotal setelah discount">Subtotal</abbr>
                        </th>
                        <th class="text-center" colspan="6">Biaya Tambahan</th>
                        <th class="text-right fitSize" rowspan="2">Grand Total</th>
                        <th rowspan="2">Keterangan</th>
                    </tr>
                    <tr class="active">
                        <th>Tanggal</th>
                        <th>Pengguna</th>
                        <th>Tanggal</th>
                        <th>Pengguna</th>
                        <th>Tamu</th>
                        <th>Produk</th>
                        <th class="text-right">Transaksi</th>
                        <th class="text-right">Produk</th>
                        <th class="text-right">Voucher</th>
                        <th class="text-right">1</th>
                        <th class="text-right">2</th>
                        <th class="text-right">3</th>
                        <th class="text-right">4</th>
                        <th class="text-right">Pengiriman</th>
                        <th class="text-right">Pembulatan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center"><asp:Label ID="LabelCariIDTransaksi" runat="server"></asp:Label></td>
                        <td></td>
                        <td colspan="2" class="text-center"><asp:Label ID="LabelCariPenggunaTransaksi" runat="server"></asp:Label></td>
                        <td colspan="2" class="text-center"><asp:Label ID="LabelCariPenggunaUpdate" runat="server"></asp:Label></td>
                        <td class="text-center"><asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center"><asp:Label ID="LabelCariJenisTransaksi" runat="server"></asp:Label></td>
                        <td class="text-center"><asp:Label ID="LabelCariStatusTransaksi" runat="server"></asp:Label></td>
                        <td class="text-center"><asp:Label ID="LabelCariPelanggan" runat="server"></asp:Label></td>
                        <td class="text-center"><asp:Label ID="LabelCariMeja" runat="server"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelJumlahTamu" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahProduk" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSubtotalSebelumDiscount" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscountProduk" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscountVoucher" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSubtotalSetelahDiscount" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan3" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan4" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaPengiriman" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelPembulatan" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("IDTransaksi") %></td>
                                <td class="fitSize"><%# Eval("TanggalOperasional") %></td>
                                <td class="fitSize"><%# Eval("TanggalTransaksi") %></td>
                                <td class="fitSize"><%# Eval("PenggunaTransaksi") %></td>
                                <td class="fitSize"><%# Eval("TanggalUpdate") %></td>
                                <td class="fitSize"><%# Eval("PenggunaUpdate") %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                <td class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                <td class="fitSize"><%# Eval("Pelanggan") %></td>
                                <td class="fitSize text-center"><%# Eval("Meja") %></td>
                                <td class="text-right"><%# Eval("JumlahTamu") %></td>
                                <td class="text-right"><%# Eval("JumlahProduk") %></td>
                                <td class="text-right warning"><%# Eval("SubtotalSebelumDiscount") %></td>
                                <td class="text-right danger"><%# Eval("PotonganTransaksi") %></td>
                                <td class="text-right danger"><%# Eval("TotalPotonganHargaJualDetail") %></td>
                                <td class="text-right danger"><%# Eval("TotalDiscountVoucher") %></td>
                                <td class="text-right warning"><%# Eval("Subtotal") %></td>
                                <td class="text-right info"><%# Eval("BiayaTambahan1") %></td>
                                <td class="text-right info"><%# Eval("BiayaTambahan2") %></td>
                                <td class="text-right info"><%# Eval("BiayaTambahan3") %></td>
                                <td class="text-right info"><%# Eval("BiayaTambahan4") %></td>
                                <td class="text-right info"><%# Eval("BiayaPengiriman") %></td>
                                <td class="text-right info"><%# Eval("Pembulatan") %></td>
                                <td class="text-right"><strong><%# Eval("GrandTotal") %></strong></td>
                                <td class="fitSize"><%# Eval("Keterangan") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="12"></td>
                        <td>
                            <asp:Label ID="LabelJumlahTamu1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahProduk1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSubtotalSebelumDiscount1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscountTransaksi1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscountProduk1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscountVoucher1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSubtotalSetelahDiscount1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan11" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan21" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan31" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaTambahan41" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelJumlahBiayaPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelPembulatan1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelGrandTotal1" runat="server"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

