<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="ReportSalesTahunan.aspx.cs" Inherits="WITReport_Niion_ReportSalesTahunan" %>

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
                        <h3>ANNUAL SALES REPORT</h3>
                    </td>
                    <td style="width: 30%" colspan="2">
                        <h3>DIVISI SALES</h3>
                        <br />
                    </td>
                </tr>
                <tr class="text-left">
                    <td style="text-align: center">
                        <h4>
                            <asp:Label ID="LabelTahun" runat="server"></asp:Label>
                        </h4>
                    </td>
                    <td style="text-align: center">
                        <h4>HEAD OF RESSELLER</h4>
                    </td>
                    <td style="text-align: center">
                        <h4>  </h4>
                    </td>
                </tr>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <thead>
                    <tr class="active">
                        <th class="text-center">Month</th>
                        <asp:Repeater ID="RepeaterKategoriTempat" runat="server">
                            <ItemTemplate>
                                <th class="text-center"><%# Eval("Nama") %></th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDataSales" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("NamaBulan") %></td>
                                <asp:Repeater ID="RepeaterData" runat="server" DataSource='<%# Eval("Hasil") %>'>
                                    <ItemTemplate>
                                        <td class="text-right"><%# Eval("GrandTotal").ToFormatHarga() %> </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="success" style="font-weight: bold;">
                        <td class="text-right"><b>TOTAL</b></td>
                        <asp:Repeater ID="RepeaterTotalKategoriTempat" runat="server">
                            <ItemTemplate>
                                <td class="text-right"><%# Eval("GrandTotalKategoriTempat").ToFormatHarga() %> </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr class="success" style="font-weight: bold;">
                        
                        <td class="text-right"><h3>GRAND TOTAL</h3></td>
                        <td colspan="5" class="text-center"><h3><asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></h3></td>
                        
                    </tr>
                </tbody>

            </table>

            <table class="table table-condensed table-hover table-bordered" style="width: 100%">
                <tr class="text-center">
                    <td style="width: 30%">
                        <b>HEAD OF RESELLER</b>
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

