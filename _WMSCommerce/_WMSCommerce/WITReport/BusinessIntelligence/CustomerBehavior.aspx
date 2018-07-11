<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="CustomerBehavior.aspx.cs" Inherits="WITReport_BusinessIntelligence_CustomerBehavior" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelHeader" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="form-inline">
            <div class="form-group">
                <asp:DropDownList ID="DropDownListPelanggan" CssClass="select2" Style="width: 200px;" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:DropDownList ID="DropDownListFilter" CssClass="select2" Style="width: 200px;" runat="server">
                    <asp:ListItem Value="1">Avg. Purchasing</asp:ListItem>
                    <asp:ListItem Value="2">Avg. Items/Purchasing</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 200px;" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Button CssClass="btn btn-sm btn-primary" ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" Style="margin-bottom: 15px;" />
            </div>
        </div>
        <div class="form-group">
            <div class="grid simple" id="Div3" runat="server">
                <div class="grid-body no-border">
                    <div class="row">
                        <div id="graph11" style="width: 100%; height: 220px;"></div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="PanelDetail" runat="server" Visible="false">
            <div class="form-group">
                <asp:Panel ID="PanelReportPenjualan" runat="server" Visible="false">
                    <div class="col-md-12">
                        <table class="table table-bordered table-hover" style="width: 100%;">
                            <thead>
                                <tr class="active">
                                    <th class="text-center" rowspan="2">Month</th>
                                    <th class="text-center" colspan="2">
                                        <asp:Label ID="LabelTahunLalu1" runat="server"></asp:Label></th>
                                    <th class="text-center" colspan="2">
                                        <asp:Label ID="LabelTahunIni1" runat="server"></asp:Label></th>
                                    <th class="text-center" colspan="2">Annual Growth </th>
                                </tr>
                                <tr class="active">
                                    <th class="text-center">Avg. Purchasing</th>
                                    <th class="text-center">Transaction Qty.</th>
                                    <th class="text-center">Avg. Purchasing</th>
                                    <th class="text-center">Transaction Qty.</th>
                                    <th class="text-center">Avg. Purchasing</th>
                                    <th class="text-center">Transaction Qty.</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RepeaterDetail" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("Bulan") %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelPenjualanTahunLalu" runat="server" Text='<%# Eval("averageGrandTotalTahunLalu").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalTransaksiTahunLalu").ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("averageGrandTotalTahunIni").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("TotalTransaksiTahunIni").ToFormatHargaBulat() %>'></asp:Label>
                                            </td>

                                            <td class="text-right">
                                                <asp:Label ID="LabelSelisih" runat="server" Text='<%# Pertumbuhan(Eval("averageGrandTotalTahunIni").ToDecimal() -  Eval("averageGrandTotalTahunLalu").ToDecimal()) %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="info" style="font-size: large;">
                                <td class="text-center"><b>AVERAGE</b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelAverageSalesTahunLalu" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelSalesVolumeTahunLaluPanel1" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelAverageSalesTahunIni" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelSalesVolumeTahunIniPanel1" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalAverageSales" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalGrowthSalesVolumePanel1" runat="server"></asp:Label></b></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelReportQty" runat="server" Visible="false">
                    <div class="col-md-12">
                        <table class="table table-bordered table-hover" style="width: 100%;">
                            <thead>
                                <tr class="active">
                                    <th class="text-center" rowspan="2">Month</th>
                                    <th class="text-center" colspan="2">
                                        <asp:Label ID="LabelTahunLalu2" runat="server"></asp:Label></th>
                                    <th class="text-center" colspan="2">
                                        <asp:Label ID="LabelTahunIni2" runat="server"></asp:Label></th>
                                    <th class="text-center" colspan="2">Annual Growth </th>
                                </tr>
                                <tr class="active">
                                    <th class="text-center">Avg. Items/Purchasing</th>
                                    <th class="text-center">Transaction Qty.</th>
                                    <th class="text-center">Avg. Items/Purchasing</th>
                                    <th class="text-center">Transaction Qty.</th>
                                    <th class="text-center">Avg. Items/Purchasing</th>
                                    <th class="text-center">Transaction Qty.</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RepeaterDetailQty" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("Bulan") %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelPenjualanTahunLalu" runat="server" Text='<%#Eval("averageGrandTotalTahunLalu").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("TotalTransaksiTahunLalu").ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("averageGrandTotalTahunIni").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("TotalTransaksiTahunIni").ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelSelisih" runat="server" Text='<%# Pertumbuhan(Eval("averageGrandTotalTahunIni").ToDecimal() -  Eval("averageGrandTotalTahunLalu").ToDecimal()) %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="info" style="font-size: large;">
                                <td class="text-center"><b>AVERAGE</b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelAverageQtyTahunLalu" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelSalesVolumeTahunLaluPanel2" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelAverageQtyTahunIni" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelSalesVolumeTahunIniPanel2" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalAvgItems" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalGrowthSalesVolumePanel2" runat="server"></asp:Label></b></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <div class="form-group">
                <div>
                    <h3 class="text-left">TOP 10 PURCHASING
                    </h3>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="grid simple" id="PanelProdukDetailByQty" runat="server">
                            <div class="grid-title no-border">
                                <h4 class="text-left"><span class="semi-bold">BY QTY</span></h4>
                                <hr />
                            </div>
                            <div class="grid-body no-border">
                                <div class="row">
                                    <div id="graph7" style="width: 100%; height: 250px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="grid simple" id="PanelProdukDetailPenjualanByIncome" runat="server">
                            <div class="grid-title no-border">
                                <h4 class="text-left"><span class="semi-bold">BY INCOME</span></h4>
                                <hr />
                            </div>
                            <div class="grid-body no-border">
                                <div class="row">
                                    <div id="graph8" style="width: 100%; height: 250px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-lg-12" style="margin-top: 40px;">
                        <div class="col-lg-12" style="margin-top: 20px;">
                            <div>
                                <h3 class="text-left">MARKET SHARE ANALYSIS
                                </h3>
                            </div>
                            <div class="grid simple" id="Div1" runat="server">
                                <div class="grid-body no-border">
                                    <div class="row">
                                        <div id="graph13" style="width: 100%; height: 220px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <table class="table table-bordered table-hover" style="width:100%;">
                            <thead>
                                <tr class="info" style="font-size:large;">
                                    <td class="text-left" colspan="2" "><b>TOTAL</b></td>
                                    <td class="text-right"><b>
                                        <asp:Label ID="LabelTotalJumlahTransaksiHeader" runat="server"></asp:Label></b></td>
                                    <td>&nbsp;</td>
                                    <td class="text-right"><b>
                                        <asp:Label ID="LabelJumlahProdukHeader" runat="server"></asp:Label></b></td>
                                    <td>&nbsp;</td>
                                    <td class="text-right"><b>
                                        <asp:Label ID="LabelTotalGrandTotalHeader" runat="server"></asp:Label></b></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr class="active">
                                    <th class="text-left">No.</th>
                                    <th class="text-left">City</th>
                                    <th class="text-left">Transaction Qty.</th>
                                    <th class="text-center">(%)</th>
                                    <th class="text-left">Sales Volume</th>
                                    <th class="text-center">(%)</th>
                                    <th class="text-left">Sales</th>
                                    <th class="text-center">(%)</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RepeaterMarketShare" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="text-left"><%# Container.ItemIndex + 1 %></td>
                                            <td>
                                                <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("Kota") %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelSalesPerKota" runat="server" Text='<%# Eval("JumlahTransaksi").ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label7" runat="server" Text='<%# ((Eval("JumlahTransaksi").ToDecimal() * 100) / Eval("SumJumlahTransaksi").ToDecimal()).ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("JumlahProduk").ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label9" runat="server" Text='<%# ((Eval("JumlahProduk").ToDecimal() * 100) / Eval("SumJumlahProduk").ToDecimal()).ToFormatHargaBulat() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("GrandTotal").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label6" runat="server" Text='<%# ((Eval("GrandTotal").ToDecimal() * 100) / Eval("SumGrandTotal").ToDecimal()).ToFormatHarga() %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="info" style="font-size:large;">
                                    <td class="text-left" colspan="2" "><b>TOTAL</b></td>
                                    <td class="text-right"><b>
                                        <asp:Label ID="LabelTotalJumlahTransaksiFooter" runat="server"></asp:Label></b></td>
                                    <td>&nbsp;</td>
                                    <td class="text-right"><b>
                                        <asp:Label ID="LabelJumlahProdukFooter" runat="server"></asp:Label></b></td>
                                    <td>&nbsp;</td>
                                    <td class="text-right"><b>
                                        <asp:Label ID="LabelTotalGrandTotalFooter" runat="server"></asp:Label></b></td>
                                    <td>&nbsp;</td>
                             </tr>
                        </table>
                    </div>--%>
                    <div class="col-md-12" style="margin-top: 40px;">
                        <div>
                            <h3 class="text-left">SUGGEST PRODUCT
                            </h3>
                        </div>
                        <table class="table table-bordered table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="text-center">Nomor</th>
                                    <th class="text-center">
                                        <asp:Label ID="Label5" runat="server">Nama</asp:Label></th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RepeaterSuggestProduct" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="text-left"><%# Container.ItemIndex + 1 %></td>
                                            <td>
                                                <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("Nama") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>


        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

