<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="RingkasanPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th rowspan="2">No.</th>
                        <th rowspan="2"></th>
                        <th rowspan="2" class="text-right">Tamu</th>
                        <th rowspan="2" class="text-right">Quantity</th>

                        <th colspan="6" class="text-right">Transaksi</th>

                        <th rowspan="2" class="text-right">Transaksi</th>
                        <th rowspan="2" class="text-right">Nominal</th>
                    </tr>
                    <tr>
                        <th class="text-right warning" style="border-left: 3px double #000;">Pelanggan</th>
                        <th class="text-right danger">Bukan Pelanggan</th>

                        <th class="text-right warning" style="border-left: 3px double #000;">Discount</th>
                        <th class="text-right danger">Tidak Discount</th>

                        <th class="text-right warning" style="border-left: 3px double #000;">Delivery</th>
                        <th class="text-right danger" style="border-right: 3px double #000;">Bukan Delivery</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="2"></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTamu" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPelanggan" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPelanggan" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelDiscount" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonDiscount" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPengiriman" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPengiriman" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelTransaksi" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNominal" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Index") %></td>
                                <td class="text-right"><%# Eval("Tamu").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right" style="border-left: 3px double #000;"><%# Eval("Pelanggan").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NonPelanggan").ToFormatHarga() %></td>
                                <td class="text-right" style="border-left: 3px double #000;"><%# Eval("Discount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NonDiscount").ToFormatHarga() %></td>
                                <td class="text-right" style="border-left: 3px double #000;"><%# Eval("Pengiriman").ToFormatHarga() %></td>
                                <td class="text-right" style="border-right: 3px double #000;"><%# Eval("NonPengiriman").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Transaksi").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Nominal").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="2"></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTamu1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelQuantity1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPelanggan1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPelanggan1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelDiscount1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonDiscount1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelTransaksi1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNominal1" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>