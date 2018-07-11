<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PenerimaanPrint.aspx.cs" Inherits="WITReport_Supplier_PenerimaanPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th>No</th>
                        <th>ID Penerimaan</th>
                        <th>ID PO</th>
                        <th>Tempat</th>
                        <th>Supplier</th>
                        <th>Pegawai</th>
                        <th>Tanggal</th>
                        <th>Total Datang</th>
                        <th>Total Diterima</th>
                        <th>Tolak Supplier</th>
                        <th>Tolak Gudang </th>
                        <th>Grandtotal</th>
                        <th>Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariIDPenerimaanPOProduksiBahanBaku" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariIDPOProduksiBahanBaku" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariSupplier" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPengguna" runat="server"></asp:Label></td>
                        <td colspan="5"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelGrandTotalHeader" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelCariKeterangan" runat="server"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
                                <td class="fitSize"><%# Eval("IDPenerimaanPOProduksiBahanBaku") %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td class="fitSize"><%# Eval("Supplier") %></td>
                                <td class="fitSize"><%# Eval("Pengguna") %></td>
                                <td class="fitSize"><%# Eval("Tanggal") %></td>
                                <td class="text-right fitSize"><%# Eval("TotalDatang") %></td>
                                <td class="text-right fitSize"><%# Eval("TotalDiterima") %></td>
                                <td class="text-right fitSize"><%# Eval("TotalTolakKeSupplier") %></td>
                                <td class="text-right fitSize"><%# Eval("TotalTolakKeGudang") %></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("Grandtotal") %></strong></td>
                                <td><%# Eval("Keterangan") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success text-right" style="font-weight: bold;">
                        <td colspan="11"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelGrandTotalFooter" runat="server" Text="0"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

