<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITReport_RatioOnStock_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Supply and Demand Detail
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
    <div class="form-group">
        <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                <thead>
                    <tr class="active">
                        <th>No.</th>
                        <th>Produk</th>
                        <th>Warna</th>
                        <th>Brand</th>
                        <th>Kategori</th>
                        <th>Varian</th>
                        <th>%</th>
                        <th class="text-right">Terjual</th>
                        <th class="text-right">Stok</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="7"></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTerjual" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelStok" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <asp:Repeater ID="RepeaterKombinasiProduk" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("item.Produk") %></td>
                                <td class="fitSize"><%# Eval("item.Warna") %></td>
                                <td class="fitSize"><%# Eval("item.Brand") %></td>
                                <td><%# Eval("item.Kategori") %></td>
                                <td class="fitSize"><%# Eval("item.Varian") %></td>
                                <td class="text-right warning fitSize"><%# Eval("Persentase").ToFormatHarga() %></td>
                                <td class="text-right fitSize"><%# Eval("Terjual").ToFormatHarga() %></td>
                                <td class="text-right fitSize"><%# Eval("item.Stok").ToFormatHargaBulat() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr class="success" style="font-weight: bold;">
                        <td colspan="7"></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTerjual1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelStok1" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

