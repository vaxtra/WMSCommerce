<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="DefaultPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr>
                        <th class="active" rowspan="2">No.</th>
                        <th class="active breakWord" rowspan="2">Brand</th>
                        <th class="active breakWord" rowspan="2">Produk</th>
                        <th class="active fitSize" rowspan="2">Warna</th>
                        <th class="active fitSize" rowspan="2">Varian</th>

                        <th class="warning" colspan="3">Stock</th>
                        <th class="info" colspan="6">Sales</th>
                    </tr>
                    <tr class="active">
                        <th class="text-right">Quantity</th>
                        <th class="text-right">Harga</th>
                        <th class="text-right">Nominal</th>

                        <th class="text-right">Quantity</th>
                        <th class="text-right">Before Disc.</th>
                        <th class="text-right">Disc.</th>
                        <th class="text-right">Subtotal</th>
                        <th class="text-right">Consignment</th>
                        <th class="text-right">Pay to Brand</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="font-weight: bold;" class="success">
                        <td colspan="5">Total Produk :
                                <asp:Label ID="LabelTotalProduk" runat="server"></asp:Label></td>
                        <td class="text-right">
                            <asp:Label ID="LabelStok" runat="server"></asp:Label>
                        </td>
                        <td></td>
                        <td class="text-right">
                            <asp:Label ID="LabelNominalStok" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelBeforeDiscount" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelDiscount" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelSubtotal" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelConsignment" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPayToBrand" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td class="breakWord"><%# Eval("Key.Brand") %></td>
                                <td class="breakWord"><%# Eval("Key.Produk") %></td>
                                <td class="fitSize"><%# Eval("Key.Warna") %></td>
                                <td class="fitSize text-center"><%# Eval("Key.Varian") %></td>
                                <td class="text-right"><%# Eval("Stok").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NominalStok").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Penjualan.Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Penjualan.BeforeDiscount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Penjualan.Discount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Penjualan.Subtotal").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Penjualan.Consignment").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Penjualan.PayToBrand").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr style="font-weight: bold;" class="success">
                        <td colspan="5">Total Produk :
                                <asp:Label ID="LabelTotalProduk1" runat="server"></asp:Label></td>
                        <td class="text-right">
                            <asp:Label ID="LabelStok1" runat="server"></asp:Label>
                        </td>
                        <td></td>
                        <td class="text-right">
                            <asp:Label ID="LabelNominalStok1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelQuantity1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelBeforeDiscount1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelDiscount1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelSubtotal1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelConsignment1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPayToBrand1" runat="server"></asp:Label>
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

