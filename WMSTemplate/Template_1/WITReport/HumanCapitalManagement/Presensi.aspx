<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Presensi.aspx.cs" Inherits="WITReport_HumanCapitalManagement_Presensi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Kehadiran Pegawai
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
    <asp:UpdatePanel ID="UpdatePanelPerformanceEmployee" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-3 col-md-3">
                        <asp:DropDownList ID="DropDownListEmployee" CssClass="select2 center-text" Width="100%" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <h4>
                            <table class="pull-right text-right">
                                <tr>
                                    <td>Jumlah Absensi</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="LabelAbsensi" runat="server"></asp:Label></td>
                                </tr>
                                <%--                            <tr>
                                <td>Total Jam Kerja</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Total Jam Lembur</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Total Jam Keterlambatan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="Label3" runat="server"></asp:Label></td>
                            </tr>--%>
                            </table>
                        </h4>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="form-inline">
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJanuari" runat="server" Text="Januari" OnClick="ButtonJanuari_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonFebruari" runat="server" Text="Februari" OnClick="ButtonFebruari_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMaret" runat="server" Text="Maret" OnClick="ButtonMaret_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonApril" runat="server" Text="April" OnClick="ButtonApril_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMei" runat="server" Text="Mei" OnClick="ButtonMei_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJuni" runat="server" Text="Juni" OnClick="ButtonJuni_Click" />
                    </div>
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJuli" runat="server" Text="Juli" OnClick="ButtonJuli_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonAgustus" runat="server" Text="Agustus" OnClick="ButtonAgustus_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonSeptember" runat="server" Text="September" OnClick="ButtonSeptember_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonOktober" runat="server" Text="Oktober" OnClick="ButtonOktober_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonNopember" runat="server" Text="Nopember" OnClick="ButtonNopember_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonDesember" runat="server" Text="Desember" OnClick="ButtonDesember_Click" />
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                        <thead>
                            <tr class="active">
                                <th>Tanggal</th>
                                <th>Jam Masuk</th>
                                <th>Jam Keluar</th>
                                <th>Jam Kerja</th>
                                <th>Keterlambatan</th>
                                <th>Lembur</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="7" style="padding: 0px">
                                    <div style="overflow: scroll; height: 640px;">
                                        <table class="table table-hover table-condensed" style="font-size: 12px;">
                                            <asp:Repeater ID="RepeaterEmployeePerformance" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-left" style="width: 15%;"><%# Eval("Tanggal") %></td>
                                                        <td class="text-center" style="width: 15%;"><%# Eval("JamMasuk") %></td>
                                                        <td class="text-center" style="width: 20%;"><%# Eval("JamKeluar") %></td>
                                                        <td class="text-right"><%# Math.Round(Decimal.Parse(Eval("TotalHr").ToString()), 2) + " hours" %>
                                                        </td>
                                                        <td class="text-right"><%# Math.Round(Decimal.Parse(Eval("OverTimeHr").ToString()), 2) + " hours" %>
                                                        </td>
                                                        <td class="text-right"><%# Math.Round(Decimal.Parse(Eval("TotalKeterlambatan").ToString()), 2) + " hours" %>
                                                        </td>
                                                        <%--                                                            <td style="width: 25%;">
                                                                <div class="progress" style="margin: 0px;">
                                                                    <%# Supplier.Persentase(Eval("Pengiriman").ToString()) %>
                                                                </div>
                                                            </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgressPerformanceEmployee" runat="server" AssociatedUpdatePanelID="UpdatePanelPerformanceEmployee">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressPenerimaanPOProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>


