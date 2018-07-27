<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PemakaianBahanBakuPrint.aspx.cs" Inherits="WITReport_PerpindahanStok_PemakaianBahanBakuPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th>No</th>
                        <th>Tempat</th>
                        <th>Kode</th>
                        <th>Bahan Baku</th>
                        <th>Satuan</th>
                        <th>Kategori</th>
                        <th>Jumlah Pemakaian</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKode" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariBahanBaku" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariSatuan" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKategori" runat="server"></asp:Label></td>
                        <td></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalSubtotaPemakaianProduksiBahanBakuHeader" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                <td><%# Eval("BahanBaku") %></td>
                                <td class="text-center fitSize"><%# Eval("Satuan") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="text-right fitSize"><%# Eval("Jumlah") %> <%# Eval("Satuan") %></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("Subtotal") %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="7"></td>
                        <td style="text-align: right;">
                            <asp:Label ID="LabelTotalSubtotaPemakaianProduksiBahanBakuFooter" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

