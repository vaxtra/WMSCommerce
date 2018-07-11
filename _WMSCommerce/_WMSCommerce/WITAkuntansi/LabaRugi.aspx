<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="LabaRugi.aspx.cs" Inherits="WITAkuntansi_LabaRugi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Income Statement
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExcel" runat="server" CssClass="btn btn-default btn-sm" Text="Export" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
    <asp:Button ID="ButtonPrint" runat="server" CssClass="btn btn-default btn-sm" Text="Cetak" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="form-group">
                <div class="form-inline">
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:TextBox ID="TextBoxTanggalPeriode1" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                        <asp:TextBox ID="TextBoxTanggalPeriode2" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                        <asp:Button ID="ButtonCari2" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="ButtonCari2_Click" />
                        <asp:Button ID="ButtonPrint2" runat="server" CssClass="btn btn-default btn-sm" Text="Print" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="form-inline">
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:DropDownList ID="DropDownListBulan" CssClass="select2" Width="161px" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Width="161px" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="ButtonCari_Click" />
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    Income Statement
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-condensed table-bordered">
                                <thead>
                                    <tr class="success bold text-left">
                                        <td colspan="2">OPERATING INCOME
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
                                        <td>GROSS PROFIT
                                        </td>
                                        <td style="width: 15%; text-align: right;">
                                            <asp:Label ID="LabelNominalGrossProfit" CssClass="label label-success" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-condensed table-bordered">
                                <thead>
                                    <tr class="success bold text-left">
                                        <td colspan="2">OPERATIONAL EXPENSES
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
                                        <td>TOTAL OPERATIONAL EXPENSES
                                        </td>
                                        <td style="width: 15%; text-align: right;">
                                            <asp:Label ID="LabelTotalOPEX" CssClass="label label-success" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
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
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
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
                                                <asp:Label ID="LabelNetIncomeLoss" CssClass="label label-important" runat="server"></asp:Label>
                                            </td>
                                        </asp:Panel>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

