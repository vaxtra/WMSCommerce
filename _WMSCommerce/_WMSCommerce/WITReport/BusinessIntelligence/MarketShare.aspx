<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="MarketShare.aspx.cs" Inherits="WITReport_BusinessIntelligence_CustomerBehavior" %>

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
    <div class="form-group">
        <asp:DropDownList ID="DropDownListFilter" CssClass="select2" Style="width: 200px;" runat="server">
            <asp:ListItem Value="1">Sort By Sales</asp:ListItem>
            <asp:ListItem Value="2">Sort By Sales Volume</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 200px;" runat="server"></asp:DropDownList>
        <asp:Button CssClass="btn btn-sm btn-primary" ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" Style="margin-bottom: 15px;" />
    </div>
    <asp:Panel ID="PanelMarketShare" runat="server" Visible="false">
        <div class="form-group">
            <%--                            <div>
                                <h3 class="text-left">MARKET SHARE ANALYSIS
                                </h3>
                            </div>--%>
            <div class="grid simple" id="Div1" runat="server">
                <div class="grid-body no-border">
                    <div class="row">
                        <div id="graph13" style="width: 100%; height: 220px;"></div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <table class="table table-bordered table-hover" style="width: 100%;">
                    <thead>
                        <tr class="info" style="font-size: large;">
                            <td class="text-left" colspan="2"><b>TOTAL</b></td>
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
                                        <asp:Label ID="Label9" runat="server" Text='<%# ((Eval("JumlahProduk").ToDecimal() * 100)/ Eval("SumJumlahProduk").ToDecimal()).ToFormatHargaBulat() %>'></asp:Label>
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
                    <tr class="info" style="font-size: large;">
                        <td class="text-left" colspan="2"><b>TOTAL</b></td>
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
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

