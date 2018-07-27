<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="VarianPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th style="width:2%;">No.</th>
                        <th>Varian</th>
                        <th class="text-right">Quantity</th>
                        <th class="text-right">Total Discount</th>
                        <th class="text-right">Total Penjualan</th>
                        <th class="text-right">%</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="font-weight: bold;" class="text-right success">
                        <td colspan="2" class="text-center">TOTAL</td>
                        <td>
                            <asp:Label ID="LabelTotalQuantity1" runat="server"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelTotalDiscount1" runat="server"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelTotalPenjualan1" runat="server"></asp:Label></td>
                        <td></td>
                    </tr>

                    <asp:Repeater ID="RepeaterData" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Key") %></td>
                                <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("TotalDiscount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("TotalPenjualan").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Persentase").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr style="font-weight: bold;" class="text-right success">
                        <td colspan="2" class="text-center">TOTAL</td>
                        <td>
                            <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelTotalDiscount" runat="server"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelTotalPenjualan" runat="server"></asp:Label></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

