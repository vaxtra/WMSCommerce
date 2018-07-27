<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Warna.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Top Warna
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonTabel" runat="server" Text="Tabel" CssClass="btn btn-default btn-sm" OnClick="ButtonTabel_Click" />
    <asp:Button ID="ButtonChart" runat="server" Text="Chart" CssClass="btn btn-default btn-sm" OnClick="ButtonChart_Click" />
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
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
                <li><a href="ProdukVarian.aspx">Produk & Varian</a></li>
                <li><a href="Produk.aspx">Produk</a></li>
                <li><a href="Brand.aspx">Brand</a></li>
                <li class="active"><a href="#tabWarna" id="Warna-tab" data-toggle="tab">Warna</a></li>
                <li><a href="Varian.aspx">Varian</a></li>
                <li><a href="Kategori.aspx">Kategori</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabWarna">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
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
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row" style="font-weight: bold;">
                            <div class="col-sm-4 col-md-4">
                                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:DropDownList ID="DropDownListJenisTransaksi" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:DropDownList ID="DropDownListOrderBy" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                    <asp:ListItem Text="Berdasarkan Quantity" Value="1" />
                                    <asp:ListItem Text="Berdasarkan Discount" Value="2" />
                                    <asp:ListItem Text="Berdasarkan Penjualan" Value="3" />
                                    <asp:ListItem Text="Berdasarkan Quantity & Penjualan" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="PanelTabel" runat="server">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                            </div>
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                    <thead>
                                        <tr class="active">
                                            <th>No.</th>
                                            <th>Warna</th>
                                            <th>Quantity</th>
                                            <th>Total Discount</th>
                                            <th>Total Penjualan</th>
                                            <th>%</th>
                                        </tr>
                                        <tr class="text-right success" style="font-weight: bold;">
                                            <td class="text-center" colspan="2">TOTAL</td>
                                            <td>
                                                <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTotalDiscount" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTotalPenjualan" runat="server"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterLaporan" runat="server" OnItemDataBound="RepeaterLaporan_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("Key") %></td>
                                                    <td class="text-right" runat="server" id="quantity"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                                    <td class="text-right" runat="server" id="totalDiscount"><%# Eval("TotalDiscount").ToFormatHarga() %></td>
                                                    <td class="text-right" runat="server" id="totalPenjualan"><%# Eval("TotalPenjualan").ToFormatHarga() %></td>
                                                    <td class="text-right info"><strong><%# Eval("Persentase").ToFormatHarga() %> %</strong></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="text-right success" style="font-weight: bold;">
                                            <td colspan="2"></td>
                                            <td>
                                                <asp:Label ID="LabelQuantity1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTotalDiscount1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTotalPenjualan1" runat="server"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PanelChart" runat="server">
                        <div id="container" runat="server" clientidmode="Static"></div>
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

