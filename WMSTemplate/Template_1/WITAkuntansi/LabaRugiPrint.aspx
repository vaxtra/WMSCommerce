<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="LabaRugiPrint.aspx.cs" Inherits="WITAkuntansi_LabaRugiPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="col-xs-6">
                <h1>
                    <img src="/images/logo.jpg">
                </h1>
            </div>
            <div class="col-xs-6 text-right">
                <h3>Income Statement</h3>
                <h5>
                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h5>
                <div>
                    Pencetak :
                <asp:Label ID="LabelNamaPencetak" runat="server"></asp:Label><br />
                    Cetak    :
                <asp:Label ID="LabelTanggalCetak" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="LabelNamaStore" runat="server" Style="font-weight: bold;"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelTeleponStore" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelWebsite" runat="server"></asp:Label>
                    <br />
                    <asp:HyperLink ID="HyperLinkEmail" runat="server"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="col-xs-5 col-xs-offset-2 text-left">
        </div>

        <div class="col-sm-12">
            <table class="table table-condensed table-bordered">
                <thead>
                    <tr class="success bold text-left">
                        <td colspan="2"><b>OPERATING INCOME</b>
                        </td>
                    </tr>
                    <tr>
                        <th>Akun</th>
                        <th>Nominal</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="LabelPenjualan" runat="server"></asp:Label>
                        </td>
                        <td style="width: 5%; text-align: right;">
                            <asp:Label ID="LabelNominalPenjualan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelCOGS" runat="server"></asp:Label>
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <asp:Label ID="LabelNominalCOGS" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="warning bold text-left">
                        <td><b>GROSS PROFIT</b>
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <asp:Label ID="LabelNominalGrossProfit" CssClass="label label-success" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>

            <table class="table table-condensed table-bordered">
                <thead>
                    <tr class="success bold text-left">
                        <td colspan="2"><b>OPERATIONAL EXPENSES</b>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-left">Akun</th>
                        <th class="text-left">Nominal</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterPengeluaran" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Nama") %></td>
                                <td style="width: 15%;" class="text-right"><%# Eval("Saldo").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="warning bold text-left">
                        <td><b>TOTAL OPERATIONAL EXPENSES</b>
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <asp:Label ID="LabelTotalOPEX" CssClass="label label-success" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>

            <table class="table table-condensed table-bordered">
                <thead>
                    <tr class="success bold text-left">
                        <td colspan="2">OTHER INCOMES
                        </td>
                    </tr>
                    <tr>
                        <th class="text-left">Akun</th>
                        <th class="text-left">Nominal</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterPemasukan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Nama") %></td>
                                <td class="text-right"><%# Eval("Saldo").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="warning bold text-left">
                        <td>EBIT (Earning Before Interests and Taxes)
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <asp:Label ID="LabelNominalEBIT" CssClass="label label-success" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>

            <table class="table table-condensed table-bordered">
                <thead>
                    <tr class="success bold text-left">
                        <td colspan="2">INTERESTS AND TAXES EXPENSES
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterPengeluaranTax" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Nama") %></td>
                                <td class="text-right"><%# Eval("Saldo").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="warning bold text-left">
                        <td>Net Income
                        </td>
                        <asp:Panel runat="server" ID="PanelProfit" Visible="false">
                            <td>
                                <asp:Label ID="LabelNetIncomeProfit" CssClass="label label-success" runat="server"></asp:Label>
                            </td>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="PanelLoss" Visible="false">
                            <td style="width: 15%; text-align: right;">
                                <asp:Label ID="LabelNetIncomeLoss" CssClass="label label-danger" runat="server"></asp:Label>
                            </td>
                        </asp:Panel>
                    </tr>
                </tbody>
            </table>
        </div>

    </div>


    <table style="width: 100%;">
        <tr>
            <asp:Label ID="LabelFooterPrint" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="text-center">THANK YOU</td>
        </tr>
        <tr>
            <td class="text-center"><b>WIT Management System Powered by WIT.</b></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

