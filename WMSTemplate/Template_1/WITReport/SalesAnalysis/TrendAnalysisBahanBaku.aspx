<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="TrendAnalysisBahanBaku.aspx.cs" Inherits="WITReport_Niion_TrendAnalysisBahanBaku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelHeader" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="form-group">
                <div class="form-inline">
                    <asp:DropDownList ID="DropDownListKombinasiProduk" CssClass="select2" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="DropDownListTempat" CssClass="select2" runat="server"></asp:DropDownList>
                    <%--<asp:DropDownList ID="DropDownListFilter" CssClass="select2" Style="width: 200px;" runat="server">--%>
                    <%--                <asp:ListItem Value="1">Penjualan</asp:ListItem>
                <asp:ListItem Value="2">Qty</asp:ListItem>
            </asp:DropDownList>--%>
                    <asp:DropDownList ID="DropDownListTahun" CssClass="select2" runat="server"></asp:DropDownList>
                    <asp:Button CssClass="btn btn-sm btn-primary" ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" />
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
                <div class="panel panel-success">
                    <div class="panel-heading">
                        Detail
                    </div>
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr class="active">
                                <th class="text-left" rowspan="2">Month</th>
                                <th class="text-center">2014</th>
                                <th class="text-center">2015</th>
                                <th class="text-right" rowspan="2">Growth</th>
                            </tr>
                            <tr class="active">
                                <th class="text-center">Harga Beli</th>
                                <th class="text-center">Harga Beli</th>
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
                                            <asp:Label ID="LabelPenjualanTahunLalu" runat="server" Text='<%# Eval("GrandTotalTahunLalu").ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("GrandTotalTahunIni".ToFormatHarga()) %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelSelisih" runat="server" Text='<%# Pertumbuhan(Eval("GrandTotalTahunIni").ToDecimal() - Eval("GrandTotalTahunLalu").ToDecimal()) %>'></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="info">
                            <td class="text-center"><b>AVERAGE</b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelGrandTotalTahunLalu" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelGrandTotalTahunIni" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelTotalSelisih" runat="server"></asp:Label></b></td>
                        </tr>
                    </table>
                    <%--                <div class="col-md-6" style="margin-top: 80px;">
                    <div class="grid simple" id="PanelAnnualSales" runat="server">
                        <div class="grid-title no-border" style="margin-top: -18px;">
                            <h3 class=" text-center"><span class="semi-bold">ANNUAL SALES</span></h3>
                        </div>
                        <div class="grid-body no-border">
                            <div class="row">
                                <div id="graph12" style="width: 100%; height: 250px;"></div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                    <%--<div class="col-lg-12" style="margin-top: 20px;">
                    <h3>SALES PER CHANNEL</h3>
                    <div class="grid simple" id="Div1" runat="server">
                        <div class="grid-body no-border">
                            <div class="row">
                                <div id="graph13" style="width: 100%; height: 220px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <table class="table table-bordered table-hover" style="width: 100%;">
                        <thead>
                            <tr class="active">
                                <th class="text-left" rowspan="2">Month</th>
                                <th class="text-center" colspan="3">2014</th>
                                <th class="text-center" colspan="3">2015</th>
                                <th class="text-right" rowspan="2">Growth</th>
                            </tr>
                            <tr class="active">
                                <th class="text-center">Sales</th>
                                <th class="text-center">Discount</th>
                                <th class="text-center">Grand Total</th>
                                <th class="text-center">Sales</th>
                                <th class="text-center">Discount</th>
                                <th class="text-center">Grand Total</th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="RepeaterSalesPerChannel" runat="server">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("NamaTempat") %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="Label1" runat="server" Text='<%# (Eval("GrandTotalTahunLalu").ToDecimal() + Eval("TotalNominalDiscountTahunLalu").ToDecimal()).ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalNominalDiscount" runat="server" Text='<%# Eval("TotalNominalDiscountTahunLalu").ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelPenjualanTahunLalu" runat="server" Text='<%# Eval("GrandTotalTahunLalu").ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="Label2" runat="server" Text='<%# (Eval("GrandTotalTahunIni").ToDecimal() + Eval("TotalNominalDiscountTahunIni").ToDecimal()).ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelPenjualanTahunIni" runat="server" Text='<%# Eval("TotalNominalDiscountTahunIni").ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("GrandTotalTahunIni").ToFormatHarga() %>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelSelisih" runat="server" Text='<%# Pertumbuhan(Eval("GrandTotalTahunIni").ToDecimal() - Eval("GrandTotalTahunLalu").ToDecimal()) %>'></asp:Label>
                                        </td>

                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="info">
                            <td class="text-center"><b>TOTAL</b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelTotalNormalSalesTahunLaluPerChannel" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelTotalDiscountTahunLaluPerChannel" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelGrandTotalTahunLaluPerChannel" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelTotalNormalSalesTahunIniPerChannel" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelTotalDiscountTahunIniPerChannel" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelGrandTotalTahunIniPerChannel" runat="server"></asp:Label></b></td>
                            <td class="text-right"><b>
                                <asp:Label ID="LabelTotalSelisihPerChannel" runat="server"></asp:Label></b></td>
                        </tr>
                    </table>
                </div>--%>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script src="/assets/plugins/Morris/raphael-min.js"></script>
    <script src="/assets/plugins/Morris/morris.min.js"></script>

    <script src="/assets/plugins/Highcharts/highcharts.js"></script>
    <script src="/assets/plugins/Highcharts/modules/exporting.js"></script>
    <asp:Literal ID="LiteralChart" runat="server"></asp:Literal>
</asp:Content>

