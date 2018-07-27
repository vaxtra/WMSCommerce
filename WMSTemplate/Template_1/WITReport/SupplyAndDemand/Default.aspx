<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITReport_RatioOnStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Supply and Demand
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-sm">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="form-inline">
            <div class="btn-group" style="margin: 5px 5px 0 0">
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMinggu" runat="server" Text="Minggu Ini" OnClick="ButtonMinggu_Click" />
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulan" runat="server" Text="Bulan Ini" OnClick="ButtonBulan_Click" />
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahun" runat="server" Text="Tahun Ini" OnClick="ButtonTahun_Click" />
            </div>
            <div class="btn-group" style="margin: 5px 5px 0 0">
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnya_Click" />
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnya_Click" />
                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnya_Click" />
            </div>
            <div style="margin: 5px 5px 0 0" class="form-group">
                <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                <thead>
                    <tr class="active">
                        <th colspan="2" rowspan="2">Lokasi</th>
                        <th colspan="4">Laporan Penjualan</th>
                        <th colspan="4">Laporan Stok</th>
                        <th colspan="2">Barang dalam Proses</th>
                    </tr>
                    <tr class="active">
                        <td class="text-right">Quantity</td>
                        <td class="text-right">%</td>
                        <td class="text-right">Nominal</td>
                        <td class="text-right">%</td>

                        <td class="text-right">Quantity</td>
                        <td class="text-right">%</td>
                        <td class="text-right">Nominal</td>
                        <td class="text-right">%</td>

                        <td>Proses</td>
                        <td>Selesai</td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLokasi" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("item.Kategori") %></td>
                                <td><a target="_blank" href='<%# "Detail.aspx?IDTempat=" + Eval("item.IDTempat") + "&TanggalAwal=" + ViewState["TanggalAwal"] + "&TanggalAkhir=" + ViewState["TanggalAkhir"] %>'><%# Eval("item.Tempat") %></a></td>

                                <td class="text-right"><%# Eval("item.TransaksiQuantity").ToFormatHargaBulat() %></td>
                                <td class="text-right info"><%# Eval("PersentaseTransaksiQuantity").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("item.TransaksiNominal").ToFormatHarga() %></td>
                                <td class="text-right success"><%# Eval("PersentaseTransaksiNominal").ToFormatHarga() %></td>

                                <td class="text-right"><%# Eval("item.StokQuantity").ToFormatHargaBulat() %></td>
                                <td class="text-right info"><%# Eval("PersentaseStokQuantity").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("item.StokNominal").ToFormatHarga() %></td>
                                <td class="text-right success"><%# Eval("PersentaseStokNominal").ToFormatHarga() %></td>

                                <td class="text-right"><%# Eval("item.ProsesProduksi").ToFormatHarga() %></td>
                                <td class="text-right active"><%# Eval("SelesaiProduksi").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="text-right warning" style="font-weight: bold;">
                        <td colspan="2"></td>

                        <td>
                            <asp:Label ID="LabelTransaksiQuantity" runat="server"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="LabelTransaksiNominal" runat="server"></asp:Label>
                        </td>
                        <td></td>

                        <td>
                            <asp:Label ID="LabelStokQuantity" runat="server"></asp:Label>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="LabelStokNominal" runat="server"></asp:Label>
                        </td>
                        <td></td>

                        <td>
                            <asp:Label ID="LabelProsesProduksi" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSelesaiProduksi" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

