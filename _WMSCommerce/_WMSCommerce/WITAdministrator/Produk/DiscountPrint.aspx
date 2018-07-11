<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="DiscountPrint.aspx.cs" Inherits="WITAdministrator_Produk_DiscountPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="text-center" rowspan="2">No.</th>
                        <th class="text-center" rowspan="2">Kode</th>
                        <th class="text-center" rowspan="2">Pemilik</th>
                        <th class="text-center" rowspan="2">Produk</th>
                        <th class="text-center" rowspan="2">Varian</th>
                        <th class="text-center" rowspan="2">Kategori</th>
                        <th class="text-center" rowspan="2">Harga</th>
                        <th class="text-center" rowspan="2">Stok</th>
                        <th class="text-center" colspan="2">Disc. Store</th>
                        <th class="text-center" colspan="2">Disc. Consignment</th>
                        <th class="text-center">After Disc.</th>
                    </tr>
                    <tr class="active">
                        <th class="text-center warning">Persentase</th>
                        <th class="text-center info">Nominal</th>
                        <th class="text-center warning">Persentase</th>
                        <th class="text-center info">Nominal</th>
                        <th class="success"></th>
                    </tr>
                    <tr class="active" style="font-weight: bold; padding-top: 0; padding-bottom: 0;">
                        <td></td>
                        <td>
                            <asp:Label ID="LabelCariKode" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelCariPemilikProduk" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelCariProduk" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelCariAtributProduk" runat="server" Text="Label"></asp:Label></td>
                        <td>
                            <asp:Label ID="LabelCariKategori" runat="server" Text="Label"></asp:Label></td>
                        <td class="text-right" colspan="8"></td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("KodeKombinasiProduk") %></td>
                                <td><%# Eval("Brand") %></td>
                                <td><%# Eval("Nama") %></td>
                                <td><%# Eval("Varian") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %> </td>
                                <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right warning" : "text-right danger" %>'><%# Eval("DiscountStorePersentase").ToString() != "0" ? Eval("DiscountStorePersentase").ToFormatHarga() + "%" : string.Empty %></td>
                                <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right info" : "text-right danger" %>'><%# Eval("DiscountStoreNominal").ToString() != "0" ? Eval("DiscountStoreNominal").ToFormatHarga() : string.Empty %></td>
                                <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right warning" : "text-right danger" %>'><%# Eval("DiscountKonsinyasiPersentase").ToString() != "0" ? Eval("DiscountKonsinyasiPersentase").ToFormatHarga() + "%" : string.Empty %></td>
                                <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right info" : "text-right danger" %>'><%# Eval("DiscountKonsinyasiNominal").ToString() != "0" ? Eval("DiscountKonsinyasiNominal").ToFormatHarga() : string.Empty %></td>
                                <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right success" : "text-right danger" %>'><strong><%# Eval("SetelahDiskon").ToFormatHarga() %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

