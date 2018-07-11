<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="BahanBakuDetailPrint.aspx.cs" Inherits="WITReport_PerpindahanStok_BahanBakuDetailPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active text-center">
                        <th>No.</th>
                        <th>Tanggal</th>
                        <th>Tempat</th>
                        <th>Pegawai</th>
                        <th>Kode</th>
                        <th>Bahan Baku</th>
                        <th>Satuan</th>
                        <th>Kategori</th>
                        <th>Jenis</th>
                        <th>Jumlah</th>
                        <th>Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="2"></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariTempat" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariPengguna" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKode" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariBahanBaku" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariSatuan" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKategori" runat="server"></asp:Label></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariJenisPerpindahanStok" runat="server"></asp:Label></td>
                        <td></td>
                        <td class="text-center">
                            <asp:Label ID="LabelCariKeterangan" runat="server"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td class="fitSize"><%# Eval("Tanggal") %></td>
                                <td class="fitSize"><%# Eval("Tempat") %></td>
                                <td><%# Eval("Pengguna") %></td>
                                <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                <td><%# Eval("BahanBaku") %></td>
                                <td class="text-center fitSize"><%# Eval("Satuan") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="fitSize"><%# Eval("Jenis") %></td>
                                <td class="text-right fitSize"><%# Eval("Jumlah") %> <%# Eval("Satuan") %>
                                    <img src='<%# Pengaturan.FormatStatusStok((bool)Eval("Status")) %>' />
                                </td>
                                <td><%# Eval("Keterangan") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

