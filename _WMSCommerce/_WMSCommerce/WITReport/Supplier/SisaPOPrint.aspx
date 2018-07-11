<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="SisaPOPrint.aspx.cs" Inherits="WITReport_Supplier_SisaPOPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th class="text-center">No</th>
                        <th class="text-center">ID</th>
                        <th class="text-center">Tempat</th>
                        <th class="text-center">Vendor</th>
                        <th class="text-center">Tanggal</th>
                        <th class="text-center">Kode</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center">Varian</th>
                        <th class="text-center">Harga</th>
                        <th class="text-center">Jumlah</th>
                        <th class="text-center">Terima</th>
                        <th class="text-center">Sisa</th>
                        <th class="text-center">Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariIDPOProduksiProduk" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariVendor" runat="server"></asp:Label></td>
                        <td colspan="7"></td>
                        <td>
                            <asp:Label ID="LabelCariStatusSisa" runat="server"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelSubtotalHeader" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td rowspan='<%# Eval("CountBahanBaku") %>' class="fitSize">
                                    <%# Container.ItemIndex + 1 %>
                                </td>
                                <td rowspan='<%# Eval("CountBahanBaku") %>'><a target="_blank" href='<%# Eval("Link")%><%# Eval("IDPOProduksiBahanBaku") %>'><%# Eval("IDPOProduksiBahanBaku") %></a></td>
                                <td rowspan='<%# Eval("CountBahanBaku") %>'><%# Eval("Tempat") %></td>
                                <td rowspan='<%# Eval("CountBahanBaku") %>'><%# Eval("Supplier") %></td>
                                <td rowspan='<%# Eval("CountBahanBaku") %>'><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                <td><%# Eval("BahanBaku.KodeBahanBaku") %></td>
                                <td><%# Eval("BahanBaku.Nama") %></td>
                                <td class="text-center"><%# Eval("BahanBaku.Satuan") %></td>
                                <td class="text-right"><%# Eval("BahanBaku.Harga").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("BahanBaku.Jumlah").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("BahanBaku.Terima").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("BahanBaku.Sisa").ToFormatHarga() %></td>
                                <td class="text-right warning"><strong><%# Eval("BahanBaku.Subtotal").ToFormatHarga() %></strong></td>
                            </tr>
                            <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("Detail") %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("KodeBahanBaku") %></td>
                                        <td><%# Eval("Nama") %></td>
                                        <td class="text-center"><%# Eval("Satuan") %></td>
                                        <td class="text-right"><%# Eval("Harga").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Terima").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Sisa").ToFormatHarga() %></td>
                                        <td class="text-right warning"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success text-right" style="font-weight: bold;">
                        <td colspan="12"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelSubtotalFooter" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

