<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="BahanBakuPrint.aspx.cs" Inherits="WITReport_StokOpname_BahanBakuPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="table-responsive">
                <table class="table table-bordered table-hover" style="font-size: 12px;">
                    <thead>
                        <tr class="active">
                            <th rowspan="2" style="width: 2.5%;">No.</th>
                            <th rowspan="2" style="width: 5.5%;">Kode</th>
                            <th rowspan="2" style="width: 9.5%">Bahan Baku</th>
                            <th rowspan="2" style="width: 9.5%;">Kategori</th>
                            <th rowspan="2" style="width: 3.5%">Satuan</th>
                            <th colspan="2">Stok Sebelum SO</th>
                            <th colspan="2">Stok Setelah SO</th>
                            <th colspan="2">Qty</th>
                            <th colspan="2">Nominal</th>
                        </tr>
                        <tr class="active">
                            <th>Qty</th>
                            <th>Nominal</th>
                            <th>Qty</th>
                            <th>Nominal</th>
                            <th style="font-size: 16px;">+</th>
                            <th style="font-size: 16px;">-</th>
                            <th style="font-size: 16px;">+</th>
                            <th style="font-size: 16px;">-</th>
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
                            <td colspan="5"></td>
                            <th style="font-weight: bold;">
                                <asp:Label ID="LabelNominalSebelumSO" runat="server"></asp:Label>
                            </th>
                            <td colspan="1"></td>
                            <th style="font-weight: bold;">
                                <asp:Label ID="LabelNominalSetelahSO" runat="server"></asp:Label>
                            </th>
                            <td colspan="1"></td>
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
                                    <td><%# Eval("BahanBaku") %></td>
                                    <td><%# Eval("Kategori") %></td>
                                    <td><%# Eval("Satuan") %></td>
                                    <td class="text-right success"><%# Eval("StokSebelumSO").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("NominalSebelumSO").ToFormatHarga() %></td>
                                    <td class="text-right success"><%# Eval("StokSetelahSO").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("NominalSetelahSO").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("SelisihQtyPositif").ToFormatHarga() %>
                                    </td>
                                    <td class="text-right"><%# Eval("SelisihQtyNegatif").ToFormatHarga() %>
                                    </td>
                                    <td class="text-right"><%# Eval("SelisihNominalPositif").ToFormatHarga() %>
                                    </td>
                                    <td class="text-right"><%# Eval("SelisihNominalNegatif").ToFormatHarga() %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                        <tr class="warning" style="font-weight: bold;">
                            <td colspan="5"></td>
                            <th style="font-weight: bold;">
                                <asp:Label ID="LabelNominalSebelumSO2" runat="server"></asp:Label>
                            </th>
                            <td colspan="1"></td>
                            <th style="font-weight: bold;">
                                <asp:Label ID="LabelNominalSetelahSO2" runat="server"></asp:Label>
                            </th>
                            <td colspan="1"></td>
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

