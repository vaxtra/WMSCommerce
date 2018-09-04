<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Kategori.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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
    Top Kategori
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonTabel" runat="server" Text="Tabel" CssClass="btn btn-secondary btn-const mr-1" OnClick="ButtonTabel_Click" />
    <asp:Button ID="ButtonChart" runat="server" Text="Chart" CssClass="btn btn-secondary btn-const mr-1" OnClick="ButtonChart_Click" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-secondary btn-const" OnClick="ButtonExcel_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-secondary btn-const mr-1" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="form-inline">
            <div class="form-group mr-1 mb-1">
                <a id="ButtonPeriodeTanggal" runat="server" class="btn btn-light btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Periode</a>
                <div class="dropdown-menu p-1">
                    <asp:Button CssClass="btn btn-light border" ID="ButtonHari" runat="server" Text="Hari Ini" Width="115px" OnClick="ButtonHari_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonMinggu" runat="server" Text="Minggu Ini" Width="115px" OnClick="ButtonMinggu_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonBulan" runat="server" Text="Bulan Ini" Width="115px" OnClick="ButtonBulan_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonTahun" runat="server" Text="Tahun Ini" Width="115px" OnClick="ButtonTahun_Click" />
                    <hr class="my-1" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" Width="115px" OnClick="ButtonHariSebelumnya_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" Width="115px" OnClick="ButtonMingguSebelumnya_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" Width="115px" OnClick="ButtonBulanSebelumnya_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" Width="115px" OnClick="ButtonTahunSebelumnya_Click" />
                </div>
            </div>
            <div class="form-group mr-1 mb-1">
                <asp:TextBox ID="TextBoxTanggalAwal" CssClass="form-control input-sm Tanggal" Width="160px" runat="server"></asp:TextBox>
            </div>
            <div class="form-group mr-1 mb-1">
                <asp:TextBox ID="TextBoxTanggalAkhir" CssClass="form-control input-sm Tanggal" Width="160px" runat="server"></asp:TextBox>
            </div>
            <div class="form-group mb-1">
                <asp:Button CssClass="btn btn-light btn-const" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-4">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-4">
                <asp:DropDownList ID="DropDownListJenisTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-4">
                <asp:DropDownList ID="DropDownListOrderBy" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                    <asp:ListItem Text="Berdasarkan Quantity" Value="1" />
                    <asp:ListItem Text="Berdasarkan Discount" Value="2" />
                    <asp:ListItem Text="Berdasarkan Penjualan" Value="3" />
                    <asp:ListItem Text="Berdasarkan Quantity & Penjualan" Value="0" />
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <div class="card-header bg-gradient-green">
                <ul class="nav nav-tabs card-header-tabs">
                    <li class="nav-item"><a href="ProdukVarian.aspx" class="nav-link" style="color: #FFFFFF !important;">Produk & Varian</a></li>
                    <li class="nav-item"><a href="ProdukVarian.aspx" class="nav-link" style="color: #FFFFFF !important;">Produk & Varian</a></li>
                    <li class="nav-item"><a href="Brand.aspx" class="nav-link" style="color: #FFFFFF !important;">Brand</a></li>
                    <li class="nav-item"><a href="Warna.aspx" class="nav-link" style="color: #FFFFFF !important;">Warna</a></li>
                    <li class="nav-item"><a href="Varian.aspx" class="nav-link" style="color: #FFFFFF !important;">Varian</a></li>
                    <li class="nav-item"><a href="#tabKategori" id="Kategori-tab" class="nav-link active" data-toggle="tab">Kategori</a></li>
                </ul>
            </div>
            <div class="card-body">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="tabKategori">
                        <h4 class="text-uppercase mb-3">
                            <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                        <asp:Panel ID="PanelTabel" runat="server">
                            <div class="table-responsive">
                                <table class="table table-sm table-bordered table-hover">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No.</th>
                                            <th>Kategori</th>
                                            <th>Quantity</th>
                                            <th>Total Discount</th>
                                            <th>Total Penjualan</th>
                                            <th>%</th>
                                        </tr>
                                        <tr class="text-right font-weight-bold table-success">
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
                                                    <td class="text-right table-warning"><strong><%# Eval("Persentase").ToFormatHarga() %> %</strong></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="text-right font-weight-bold table-success">
                                            <td class="text-center" colspan="2">TOTAL</td>
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
                        </asp:Panel>

                        <asp:Panel ID="PanelChart" runat="server">
                            <div id="container" runat="server" clientidmode="Static"></div>
                        </asp:Panel>
                    </div>
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

