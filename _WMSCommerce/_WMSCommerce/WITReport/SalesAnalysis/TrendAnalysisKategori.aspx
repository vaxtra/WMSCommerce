<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="TrendAnalysisKategori.aspx.cs" Inherits="WITReport_Niion_TrendAnalysisKategori" %>

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
                    <asp:DropDownList ID="DropDownListKategoriProduk" CssClass="select2" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="DropDownListTempat" CssClass="select2" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="DropDownListFilter" CssClass="select2" runat="server">
                        <asp:ListItem Value="1">Penjualan</asp:ListItem>
                        <asp:ListItem Value="2">Qty</asp:ListItem>
                    </asp:DropDownList>
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
            <div class="form-group">
                <div class="row">
                    <asp:Panel ID="PanelDetail" runat="server" Visible="false">
                        <div class="col-lg-12" style="margin-top: 40px;">
                            <asp:Panel ID="PanelReportPenjualan" runat="server" Visible="false">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-hover">
                                        <thead>
                                            <tr class="active">
                                                <th class="text-left" rowspan="2">Month</th>
                                                <th class="text-center" colspan="3">
                                                    <asp:Label ID="LabelHeaderByPenjualanTahunKemarin" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-center" colspan="3">
                                                    <asp:Label ID="LabelHeaderByPenjualanTahunIni" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-right" rowspan="2">Growth</th>
                                            </tr>
                                            <tr class="active">
                                                <th class="text-center">Sales Before Disc.</th>
                                                <th class="text-center">Discount</th>
                                                <th class="text-center">Sales After Disc.</th>
                                                <th class="text-center">Sales Before Disc.</th>
                                                <th class="text-center">Discount</th>
                                                <th class="text-center">Sales After Disc.</th>
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
                                                <asp:Label ID="LabelTotalNormalSalesTahunLalu" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalDiscountTahunLalu" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelGrandTotalTahunLalu" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalNormalSalesTahunIni" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalDiscountTahunIni" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelGrandTotalTahunIni" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalSelisih" runat="server"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PanelReportQty" runat="server" Visible="false">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-hover">
                                        <thead>
                                            <tr class="active">
                                                <th class="text-left" rowspan="2">Month</th>
                                                <th class="text-center">
                                                    <asp:Label ID="LabelHeaderByQtyTahunKemarin" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Label ID="LabelHeaderByQtyTahunIni" runat="server"></asp:Label>
                                                </th>
                                                <th class="text-right" rowspan="2">Growth</th>
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
                                                            <asp:Label ID="LabelPenjualanTahunLalu" runat="server" Text='<%# Eval("GrandTotalTahunLalu").ToFormatHarga() %>'></asp:Label>
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
                                                <asp:Label ID="LabelTotalQtyTahunLalu" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalQtyTahunIni" runat="server"></asp:Label></b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalGrowthQty" runat="server"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <%--                <div class="col-lg-12" style="margin-top: 20px;">
                    <h3>SALES PER CHANNEL</h3>
                    <div class="grid simple" id="Div1" runat="server">
                        <div class="grid-body no-border">
                            <div class="row">
                                <div id="graph13" style="width: 100%; height: 220px;"></div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                            <%--<asp:Panel ID="PanelSalesPerChannelPenjualan" runat="server" Visible="false">
                    <div class="col-md-12">
                        <table class="table table-bordered table-hover" style="width: 100%;">
                            <thead>
                                <tr class="active">
                                    <th class="text-left" rowspan="2">Channel</th>
                                    <th class="text-center" colspan="3">
                                        <asp:Label ID="LabelFooterByPenjualanTahunKemarin" runat="server"></asp:Label>
                                    </th>
                                    <th class="text-center" colspan="3">
                                        <asp:Label ID="LabelFooterByPenjualanTahunIni" runat="server"></asp:Label>
                                    </th>
                                    <th class="text-right" rowspan="2">Growth</th>
                                </tr>
                                <tr class="active">
                                    <th class="text-center">Sales Before Disc.</th>
                                    <th class="text-center">Discount</th>
                                    <th class="text-center">Sales After Disc.</th>
                                    <th class="text-center">Sales Before Disc.</th>
                                    <th class="text-center">Discount</th>
                                    <th class="text-center">Sales After Disc.</th>
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
                    </div>
                </asp:Panel>--%>
                            <%--<asp:Panel ID="PanelSalesPerChannelQty" runat="server" Visible="false">
                    <div class="col-md-12">
                        <table class="table table-bordered table-hover" style="width: 100%;">
                            <thead>
                                <tr class="active">
                                    <th class="text-left" rowspan="2">Channel</th>
                                    <th class="text-center"><asp:Label ID="LabelFooterByQtyTahunKemarin" runat="server"></asp:Label></th>
                                    <th class="text-center"><asp:Label ID="LabelFooterByQtyTahunIni" runat="server"></asp:Label></th>
                                    <th class="text-right" rowspan="2">Growth</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RepeaterSalesPerChannelQty" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("NamaTempat") %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelPenjualanTahunLalu" runat="server" Text='<%# Eval("GrandTotalTahunLalu").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("GrandTotalTahunIni").ToFormatHarga() %>'></asp:Label>
                                            </td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelSelisih" runat="server" Text='<%# Pertumbuhan(Eval("GrandTotalTahunIni").ToDecimal() -  Eval("GrandTotalTahunLalu").ToDecimal()) %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="info">
                                <td class="text-center"><b>TOTAL</b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalQtySalesPerChannelTahunLalu" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalQtySalesPerChannelTahunIni" runat="server"></asp:Label></b></td>
                                <td class="text-right"><b>
                                    <asp:Label ID="LabelTotalGrowthQtySalesPerChannel" runat="server"></asp:Label></b></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>--%>
                            <asp:Panel ID="PanelSalesPerKota" runat="server" Visible="true">
                                <div class="col-md-12">
                                    <div class="col-lg-12" style="margin-top: 20px;">
                                        <h3>SALES PER KOTA</h3>
                                        <div class="grid simple" id="Div2" runat="server">
                                            <div class="grid-body no-border">
                                                <div class="row">
                                                    <div id="container" runat="server" clientidmode="Static"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <table class="table table-bordered table-hover" style="width: 100%;">
                                        <thead>
                                            <tr class="info" style="font-size: large;">
                                                <td class="text-left" colspan="3"><b>TOTAL</b></td>
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
                                                <th class="text-left">Region</th>
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
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Provinsi") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LabelBulan" runat="server" Text='<%# Eval("Kota") %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelSalesPerKota" runat="server" Text='<%# Eval("JumlahTransaksi")  %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("SumJumlahTransaksi").ToFormatHargaBulat() != "0" ? 
                                                        ((Eval("JumlahTransaksi").ToDecimal()*100)/Eval("SumJumlahTransaksi").ToDecimal()).ToFormatHargaBulat() : "0" %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("JumlahProduk").ToFormatHargaBulat() %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("SumJumlahProduk").ToFormatHargaBulat() != "0" ? 
                                                        ((Eval("JumlahProduk").ToDecimal()*100)/Eval("SumJumlahProduk").ToDecimal()).ToFormatHargaBulat() : "0" %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("GrandTotal").ToFormatHarga() %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("SumGrandTotal").ToFormatHarga() != "0" ? 
                                                        ((Eval("GrandTotal").ToDecimal()*100)/Eval("SumGrandTotal").ToDecimal()).ToFormatHarga()  : "0"%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="info" style="font-size: large;">
                                            <td class="text-left" colspan="3"><b>TOTAL</b></td>
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
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
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

