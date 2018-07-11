<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PengaturanPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="fitSize">No.</th>
                        <th>Produk</th>
                        <th>Warna</th>
                        <th>Brand</th>
                        <th>Kategori</th>
                        <th>Kode</th>
                        <th class="fitSize">Varian</th>
                        <th class="fitSize">Quantity</th>
                        <th>Harga</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr style="border-top: 3px double #aaa !important;">
                                <td class="fitSize" rowspan='<%# Eval("Count") %>'>
                                    <%# Container.ItemIndex + 1 %>
                                </td>
                                <td rowspan='<%# Eval("Count") %>'><%# Eval("Produk") %></td>
                                <td rowspan='<%# Eval("Count") %>'><%# Eval("Warna") %></td>
                                <td rowspan='<%# Eval("Count") %>'><%# Eval("Brand") %></td>
                                <td rowspan='<%# Eval("Count") %>'><%# Eval("Kategori") %></td>

                                <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" DataSource='<%# Eval("Stok") %>'>
                                    <ItemTemplate>
                                        <td><%# Eval("Kode") %></td>
                                        <td class="fitSize"><%# Eval("Varian") %></td>
                                        <td class="fitSize"></td>
                                        <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

