<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="DefaultPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="fitSize">No.</th>
                        <th>Lokasi</th>
                        <th>Quantity</th>
                        <th>Sales Before Disc.</th>
                        <th>Disc.</th>
                        <th>Sales After Disc.</th>
                        <th>COGS</th>
                        <th>Gross Profit</th>
                        <th class="fitSize">%</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="font-weight: bold;" class="success">
                        <td colspan="2"></td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHargaBulat(Result["Quantity"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["BeforeDiscount"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["AfterDiscount"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["COGS"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["GrossProfit"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["Persentase"]) : "0" %>
                        </td>
                    </tr>

                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Key.Nama") %></td>
                                <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("BeforeDiscount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Discount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("AfterDiscount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("COGS").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("GrossProfit").ToFormatHarga() %></td>
                                <td class="text-right fitSize warning"><%# Parse.ToFormatHarga((decimal)Eval("GrossProfit") / (decimal)Eval("AfterDiscount") * 100) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr style="font-weight: bold;" class="success">
                        <td colspan="2"></td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHargaBulat(Result["Quantity"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["BeforeDiscount"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["AfterDiscount"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["COGS"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["GrossProfit"]) : "0" %>
                        </td>
                        <td class="text-right">
                            <%= Result != null ? Parse.ToFormatHarga(Result["Persentase"]) : "0" %>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

