<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="ReportSalesEvent.aspx.cs" Inherits="WITReport_Niion_ReportSalesEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="text-center">
                    <td rowspan="2" style="width: 30%">
                        <img src="/images/logo_wms.png" width="80" height="80"><br />
                        <asp:Label ID="LabelNamaStore" runat="server" Font-Size="Smaller" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <h3>REPORT <asp:Label ID="LabelNamaKategoriTempat" runat="server"></asp:Label></h3>
                    </td>
                    <td style="width: 30%" colspan="2">
                        <h3>DIVISI SALES</h3>
                        <br />
                    </td>
                </tr>
                <tr class="text-left">
                    <td style="text-align: center">
                        <h4><asp:Label ID="LabelNamaBulan" runat="server"></asp:Label></h4>
                    </td>
                    <td style="text-align: center"><h4>HEAD OF RESSELLER</h4>
                    </td>
                    <td style="text-align: center"><h4>  </h4>
                    </td>
                </tr>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <thead>
                    <tr class="active">
                        <th class="text-center" rowspan="2" style="padding-bottom: 20px;">No</th>
                        <th class="text-center" rowspan="2" style="padding-bottom: 20px;">Date</th>
                        <th class="text-center" rowspan="2" style="padding-bottom: 20px;">Event</th>
                        <th class="text-center" colspan="3">Forecast Event Sales</th>
                        <th class="text-center" rowspan="2" style="padding-bottom: 20px;">Actual Qty</th>
                        <th class="text-center" rowspan="2" style="padding-bottom: 20px;">Actual Income</th>
                    </tr>
                    <tr>
                        <th class="text-center">Qty</th>
                        <th class="text-center">Forecast Income (IDR)</th>
                        <th class="text-center">Head of Admin</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDataSales" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-right"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Key.TanggalTransaksi").ToFormatTanggalHari() %></td>
                                <td><%# Eval("Key.Tempat") %></td>
                                <td class="text-right"><%# Eval("ForecastQuantity") %></td>
                                <td class="text-right"><%# Eval("ForecastIncome").ToFormatHarga() %> </td>
                                <td class="text-right"><%# Eval("Key.NamaPenggunaTransaksi") %></td>
                                <td class="text-right"><%# Eval("Qty").ToFormatHargaBulat() %> </td>
                                <td class="text-right"><%# Eval("ActualIncome").ToFormatHarga() %> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success" style="font-weight: bold;">
                        <td colspan="3" class="text-right"><b>BALANCE</b></td>
                        <td class="text-right"><asp:Label ID="LabelTotalForecastQuantity" runat="server"></asp:Label></td>
                        <td class="text-right"><asp:Label ID="LabelTotalForecastIncome" runat="server"></asp:Label></td>
                        <td></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTotalQty" runat="server"></asp:Label></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTotalActualIncome" runat="server"></asp:Label></td>
                    </tr>
                </tbody>
            </table>

            <table class="table table-condensed table-hover table-bordered" style="width: 100%">
                <tr class="text-center">
                    <td style="width: 30%">
                        <b>HEAD OF EVENT</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 30%">
                        <b>HEAD OF SALES</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 30%">
                        <b>HEAD OF FINANCE</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

