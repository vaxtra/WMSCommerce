<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Neraca.aspx.cs" Inherits="WITAkuntansi_Neraca" %>

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
    Balance Sheet
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
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="form-group">
                <div class="form-inline">
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:DropDownList ID="DropDownListBulan" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="ButtonCari_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Balance Sheet
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="active">
                                <th>No</th>
                                <th>Kode</th>
                                <th colspan="2">Nama</th>
                                <th>Nominal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                <ItemTemplate>
                                    <tr <%# Eval("ClassWarna") %>>
                                        <td class="bold"><%# Eval("Nomor") %></td>
                                        <td <%# (Eval("Grup").ToBool()) == true ? "class='hidden'" : string.Empty %>><%# Eval("Kode") %></td>
                                        <td <%# (Eval("Grup").ToBool()) == true ? "colspan='2' style='font-weight: bold;'" : string.Empty %>>
                                            <%# Eval("Nama") %>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick='<%# Eval("GeneralLedger") %>' Visible='<%# Eval("StatusGeneralLedger") %>'>
                                                Detail
                                            </asp:LinkButton>
                                        </td>
                                        <td class="text-right"><%# (Eval("Grup").ToBool()) == true ? string.Empty : (Eval("Nominal").ToFormatHarga()) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    Summary
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered">
                                        <thead>
                                            <tr class="active">
                                                <th colspan="3" class="text-center" style="width: 50%;">ASSETS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="text-center"><strong>
                                                    <asp:Label ID="LabelTotalSaldoAktiva" runat="server" Text=""></asp:Label></strong></td>
                                            </tr>
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
                                                        <td class="bold"><%# Eval("Nomor") %></td>
                                                        <td <%# (Eval("Grup").ToBool()) == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                                        <td class="text-right"><%# (Eval("StatusParent").ToBool()) == true ? "" : (Eval("Nominal").ToFormatHarga()) %></td>
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
                                                <th colspan="3" class="text-center" style="width: 50%;">LIABILITIES</th>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="text-center"><strong>
                                                    <asp:Label ID="LabelTotalSaldoPasiva" runat="server" Text=""></asp:Label></strong></td>
                                            </tr>
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
                                                        <td class="bold"><%# Eval("Nomor") %></td>
                                                        <td <%# (Eval("Grup").ToBool()) == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                                        <td class="text-right"><%# (Eval("StatusParent").ToBool()) == true ? "" : (Eval("Nominal").ToFormatHarga()) %></td>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

