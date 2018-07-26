<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Stok_ButuhRestok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCari');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Jenis Pembayaran
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <%--                        <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-info btn-sm" style="font-weight: bold; margin: 5px 0 0 0;" />--%>
    <asp:Button ID="ButtonTable" runat="server" Text="Table" CssClass="btn btn-default btn-sm" OnClick="ButtonTable_Click" />
    <asp:Button ID="ButtonChart" runat="server" Text="Chart" CssClass="btn btn-default btn-sm" OnClick="ButtonChart_Click" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
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
            <ul id="myTab" class="nav nav-tabs">
                <li><a href="../Default.aspx">Transaksi</a></li>
                <li><a href="../PenjualanProduk.aspx">Produk</a></li>
                <li><a href="../../NetRevenue/Default.aspx">Net Revenue</a></li>
                <li class="active"><a href="#tabJenisPembayaran" id="JenisPembayaran-tab" data-toggle="tab">Jenis Pembayaran</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabJenisPembayaran">
                    <div class="form-group">
                        <div class="form-inline">
                            <div class="btn-group" style="margin: 5px 5px 0 0">
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMinggu" runat="server" Text="Minggu Ini" OnClick="ButtonMinggu_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulan" runat="server" Text="Bulan Ini" OnClick="ButtonBulan_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahun" runat="server" Text="Tahun Ini" OnClick="ButtonTahun_Click" />
                            </div>
                            <div class="btn-group" style="margin: 5px 5px 0 0">
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnya_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnya_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnya_Click" />
                            </div>
                            <div style="margin: 5px 5px 0 0" class="form-group">
                                <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                                <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                                <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6 col-md-6">
                                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <asp:DropDownList ID="DropDownListJenisTransaksi" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="divTabel" runat="server" class="panel panel-success">
                        <div class="panel-heading">
                            <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Hari</th>
                                        <th>Tanggal</th>
                                        <asp:Literal ID="LiteralHeaderTabel" runat="server"></asp:Literal>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div id="divChart" runat="server" clientidmode="Static" visible="false"></div>
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

