<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="ProyeksiPrint.aspx.cs" Inherits="WITReport_Proyeksi_ProyeksiPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th>No</th>
                        <th>ID</th>
                        <th>Tempat</th>
                        <th>Pegawai</th>
                        <th class="fitSize">Tanggal Proyeksi</th>
                        <th class="fitSize">Tanggal Target</th>
                        <th class="fitSize">Tanggal Selesai</th>
                        <th>Total</th>
                        <th>Status</th>
                        <th>Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="success" style="font-weight: bold;">
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariIDProyeksi" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPengguna" runat="server"></asp:Label></td>
                        <td colspan="3"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalProdukHeaderProyeksi" runat="server" Text="0"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariStatusProyeksi" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKeterangan" runat="server"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("IDProyeksi") %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td class="fitSize"><%# Eval("Pengguna") %></td>
                                <td class="fitSize"><%# Eval("TanggalProyeksi") %></td>
                                <td class="fitSize"><%# Eval("TanggalTarget") %></td>
                                <td class="fitSize"><%# Eval("TanggalSelesai") %></td>
                                <td class="text-right warning fitSize"><strong><%# Eval("TotalJumlah") %></strong></td>
                                <td class="text-center fitSize"><%# Pengaturan.StatusProyeksi(Eval("EnumStatusProyeksi").ToString()) %></td>
                                <td><%# Eval("Keterangan") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="7"></td>
                        <td class="text-right" style="vertical-align: middle;">
                            <asp:Label ID="LabelTotalProdukFooterProyeksi" runat="server" Text="0"></asp:Label></td>
                        <td colspan="2"></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

