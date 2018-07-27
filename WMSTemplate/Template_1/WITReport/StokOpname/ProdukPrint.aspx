<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="ProdukPrint.aspx.cs" Inherits="WITReport_StokOpname_ProdukPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover" style="font-size: 12px;">
                            <thead>
                                <tr class="active">
                                    <th rowspan="2">No.</th>
                                    <th rowspan="2">Kode</th>
                                    <th rowspan="2">Produk</th>
                                    <th rowspan="2">Varian</th>
                                    <th rowspan="2">Warna</th>
                                    <th rowspan="2">Kategori</th>
                                    <th rowspan="2">Brand</th>
                                    <th rowspan="2">Stok Akhir</th>
                                    <th rowspan="2">Hasil Stok Opname</th>
                                    <th colspan="2">Qty</th>
                                    <th colspan="2">Nominal</th>
                                </tr>
                                <tr class="active" style="font-size: 16px;">
                                    <th>+</th>
                                    <th>-</th>
                                    <th>+</th>
                                    <th>-</th>
                                </tr>
                                <tr class="active">
                                    <th colspan="9"></th>
                                    <th colspan="2" style="font-weight: bold;">
                                        <asp:Label ID="LabelGtandTotalSelisihQty" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="2" style="font-weight: bold;">
                                        <asp:Label ID="LabelGtandTotalSelisihNominal" runat="server"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="warning" style="font-weight: bold;">
                                    <td colspan="9"></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahQtyPositif" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahQtyNegatif" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahNominalPositif" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahNominalNegatif" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <asp:Repeater ID="RepeaterLaporan" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%# Eval("Kode") %></td>
                                            <td><%# Eval("NamaProduk") %></td>
                                            <td><%# Eval("Varian") %></td>
                                            <td><%# Eval("Warna") %></td>
                                            <td><%# Eval("Kategori") %></td>
                                            <td><%# Eval("PemilikProduk") %></td>
                                            <td class="text-right success"><%# Eval("StokSebelumSO").ToFormatHargaBulat() %></td>
                                            <td class="text-right"><%# Eval("StokSetelahSO").ToFormatHargaBulat() %>
                                                <%--<img src='<%# Pengaturan.FormatStatusStok((bool)Eval("Key.Status")) %>' />--%>
                                            </td>
                                            <td class="text-right"><%# Eval("SelisihQtyPositif").ToFormatHargaBulat() %>
                                            </td>
                                            <td class="text-right"><%# Eval("SelisihQtyNegatif").ToFormatHargaBulat() %>
                                            </td>
                                            <td class="text-right"><%# Eval("SelisihNominalPositif").ToFormatHargaBulat() %>
                                            </td>
                                            <td class="text-right"><%# Eval("SelisihNominalNegatif").ToFormatHargaBulat() %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr class="warning" style="font-weight: bold;">
                                    <td colspan="9"></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahQtyPositif2" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahQtyNegatif2" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahNominalPositif2" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="LabelTotalJumlahNominalNegatif2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
</asp:Content>


