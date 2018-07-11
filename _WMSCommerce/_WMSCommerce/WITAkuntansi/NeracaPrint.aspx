<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="NeracaPrint.aspx.cs" Inherits="WITAkuntansi_NeracaPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <h1>
                <img src="/images/logo.jpg">
            </h1>
        </div>
        <div class="col-xs-6 text-right">
            <h3>Balance Sheet</h3>
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

    <div class="row">
        <div class="col-xs-5">
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
    </div>
    <div class="row form-row">
        <div class="col-md-12">
            <div class="table-responsive">
                <table class="table table-condensed table-bordered">
                    <thead>
                        <tr class="active">
                            <th class="text-center">ASSETS</th>
                            <th class="text-center">LIABILITIES</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="text-center"><strong>
                                <asp:Label ID="LabelTotalSaldoAktiva" runat="server" Text=""></asp:Label></strong></td>
                            <td class="text-center"><strong>
                                <asp:Label ID="LabelTotalSaldoPasiva" runat="server" Text=""></asp:Label></strong></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="active">
                            <th class="text-center">NO</th>
                            <th class="text-center">NAMA</th>
                            <th class="text-center">NOMINAL</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterLaporanAktiva" runat="server">
                            <ItemTemplate>
                                <tr <%# Eval("ClassWarna") %>>
                                    <td><%# Eval("Nomor") %></td>
                                    <td <%# Eval("Grup").ToBool() == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                    <td class="text-right"><%# Eval("StatusParent").ToBool() == true ? "" : Eval("Nominal").ToFormatHarga()%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-md-6">
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="active">
                            <th class="text-center">NO</th>
                            <th class="text-center">NAMA</th>
                            <th class="text-center">NOMINAL</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterLaporanPasiva" runat="server">
                            <ItemTemplate>
                                <tr <%# Eval("ClassWarna") %>>
                                    <td><%# Eval("Nomor") %></td>
                                    <td <%# Eval("Grup").ToBool() == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                    <td class="text-right"><%# Eval("StatusParent").ToBool() == true ? "" : Eval("Nominal").ToFormatHarga() %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="success">
                            <td></td>
                            <td>Laba/Rugi Bulan Berjalan </td>
                            <td>
                                <asp:Label ID="LabelLabaRugiBulanBerjalan" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr class="success">
                            <td></td>
                            <td>Laba/Rugi Bulan Sebelumnya </td>
                            <td>
                                <asp:Label ID="LabelLabaRugiBulanSebelumnya" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
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

