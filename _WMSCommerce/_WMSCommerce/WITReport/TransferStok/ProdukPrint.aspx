<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="ProdukPrint.aspx.cs" Inherits="WITReport_TransferStok_ProdukPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th>No</th>
                        <th>ID</th>
                        <th colspan="2">Pengirim</th>
                        <th>Tanggal Kirim</th>
                        <th colspan="2">Penerima</th>
                        <th>Tanggal Terima</th>
                        <th>Jumlah</th>
                        <th>Grandtotal</th>
                        <th>Status</th>
                        <th>Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariIDTransfer" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempatPengirim" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPengirim" runat="server"></asp:Label></td>
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempatPenerima" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPenerima" runat="server"></asp:Label></td>
                        <td></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahHeaderTransfer" runat="server" Text="0"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalGrandtotalHeaderTransfer" runat="server" Text="0"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariStatusTransfer" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKeterangan" runat="server"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("IDTransferProduk") %></td>
                                <td class="fitSize"><%# Eval("TempatPengirim") %></td>
                                <td class="fitSize"><%# Eval("Pengirim") %></td>
                                <td class="fitSize"><%# Eval("TanggalKirim") %></td>
                                <td class="fitSize"><%# Eval("TempatPenerima") %></td>
                                <td class="fitSize"><%# Eval("Penerima") %></td>
                                <td class="fitSize"><%# Eval("TanggalTerima") %></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("TotalJumlah") %></strong></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("GrandtotalHargaJual") %></strong></td>
                                <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
                                <td><%# Eval("Keterangan") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="8"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalJumlahFooterTransfer" runat="server" Text="0"></asp:Label></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalGrandtotalFooterTransfer" runat="server" Text="0"></asp:Label></td>
                        <td colspan="2"></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

