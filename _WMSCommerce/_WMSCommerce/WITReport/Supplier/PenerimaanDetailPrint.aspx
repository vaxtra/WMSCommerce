<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PenerimaanDetailPrint.aspx.cs" Inherits="WITReport_Supplier_PenerimaanDetailPrint" %>

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
                        <th>Kode</th>
                        <th>Bahan Baku</th>
                        <th>Satuan</th>
                        <th>Kategori</th>
                        <th>Datang</th>
                        <th>Diterima</th>
                        <th>Tolak Supplier</th>
                        <th>Tolak Gudang </th>
                        <th>Subtotal</th>
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
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKode" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariBahanBaku" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariSatuan" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKategori" runat="server"></asp:Label></td>
                        <td colspan="4"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelSubtotalHeader" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("IDPenerimaanPOProduksiBahanBaku") %></td>
                                <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td class="fitSize"><%# Eval("Supplier") %></td>
                                <td class="fitSize"><%# Eval("Pengguna") %></td>
                                <td class="fitSize"><%# Eval("Tanggal") %></td>
                                <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                <td class="fitSize"><%# Eval("BahanBaku") %></td>
                                <td class="fitSize"><%# Eval("Satuan") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="text-right fitSize"><%# Eval("Datang") %></td>
                                <td class="text-right fitSize"><%# Eval("Diterima") %></td>
                                <td class="text-right fitSize"><%# Eval("TolakKeSupplier") %></td>
                                <td class="text-right fitSize"><%# Eval("TolakKeGudang") %></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("Subtotal") %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success text-right" style="font-weight: bold;">
                        <td colspan="15"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelSubtotalFooter" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

