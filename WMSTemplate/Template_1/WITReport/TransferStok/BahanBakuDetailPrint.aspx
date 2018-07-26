<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="BahanBakuDetailPrint.aspx.cs" Inherits="WITReport_TransferStok_BahanBakuDetailPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th>No</th>
                        <th>Kode</th>
                        <th>Bahan Baku</th>
                        <th>Satuan</th>
                        <th>Kategori</th>
                        <th>Jumlah</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKode" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariBahanBaku" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariSatuan" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKategori" runat="server"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahHeaderTransferDetail" runat="server" Text="0"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalSubtotalHeaderTransferDetail" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                <td class="fitSize"><%# Eval("BahanBaku") %></td>
                                <td class="text-center fitSize"><%# Eval("Satuan") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="text-right fitSize"><strong><%# Eval("Jumlah") %></strong></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("Subtotal") %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="5"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahFooterTransferDetail" runat="server" Text="0"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalSubtotalFooterTransferDetail" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

