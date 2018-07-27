<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/assets/plugins/Morris/morris.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Beranda
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
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="form-group" id="PanelAktifitasTransaksi1" runat="server">
        <div class="card">
            <div class="card-header">
                <h5 class="font-weight-light">Penjualan Bulan Ini</h5>
            </div>
            <div class="card-body">
                <canvas id="CanvasChartPenjualan" class="m-0 w-100" height="300px"></canvas>
            </div>
        </div>
    </div>
    <div class="form-group mb-0">
        <div class="row">
            <div id="panelTransaksiTerakhir" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-6 pb-3">
                <div class="card h-100">
                    <div class="card-header">
                        <h5 class="font-weight-light">10 Transaksi Terakhir</h5>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-sm table-bordered table-hover">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No</th>
                                        <th>Tanggal</th>
                                        <th>Status</th>
                                        <th>Qty</th>
                                        <th>Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterOrder" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("IDTransaksi") %></td>
                                                <td class="fitSize"><%# Eval("TanggalTransaksi").ToFormatTanggalHari() %></td>
                                                <td>
                                                    <%# Eval("Persentase") %>
                                                </td>
                                                <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                                <td class="text-right fitSize"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div id="PanelAktifitasTransaksi3" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-6">
                            <div class="card">
                                <div class="card-header">
                                    <h5 class="font-weight-light">Aktifitas Hari Ini</h5>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-hover text-left mb-0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%">Penjualan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPenjualanHariIni" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Pelanggan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPelangganHariIni" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Quantity</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelQuantityHariIni" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Transaksi</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelTransaksiHariIni" runat="server"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-6">
                            <div class="card">
                                <div class="card-header">
                                    <h5 class="font-weight-light">Aktifitas Kemarin</h5>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-hover text-left mb-0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%">Penjualan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPenjualanKemarin" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Pelanggan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPelangganKemarin" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Quantity</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelQuantityKemarin" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Transaksi</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelTransaksiKemarin" runat="server"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-6">
                            <div class="card">
                                <div class="card-header">
                                    <h5 class="font-weight-light">Aktifitas Bulan Ini</h5>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-hovers text-left mb-0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%">Penjualan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPenjualanBulanIni" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Pelanggan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPelangganBulanIni" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Quantity</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelQuantityBulanIni" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Transaksi</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelTransaksiBulanIni" runat="server"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-3 col-lg-6">
                            <div class="card">
                                <div class="card-header">
                                    <h5 class="font-weight-light">Aktifitas Bulan Lalu</h5>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-hover text-left mb-0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%">Penjualan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPenjualanBulanLalu" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Pelanggan</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelPelangganBulanLalu" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Quantity</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelQuantityBulanLalu" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">Transaksi</td>
                                                <td style="width: 50%" class="text-right">
                                                    <asp:Label ID="LabelTransaksiBulanLalu" runat="server"></asp:Label></td>
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
    <div class="form-group">
        <div id="panelStokProdukHabis" runat="server" class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="font-weight-light">Stok Produk Akan Habis</h5>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:DropDownList ID="DropDownListJumlahData" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="20" Value="20" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group float-right mb-0">
                                        <asp:Button ID="ButtonPrevious" runat="server" CssClass="btn btn-outline-light" Text="<<" OnClick="ButtonPrevious_Click" />
                                        <asp:DropDownList ID="DropDownListHalaman" CssClass="select2 select2-fix-width-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                        </asp:DropDownList>
                                        <asp:Button ID="ButtonNext" runat="server" CssClass="btn btn-outline-light" Text=">>" OnClick="ButtonNext_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="table-responsive">
                                <table class="table table-sm table-bordered table-hover">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No</th>
                                            <th>Tempat</th>
                                            <th>Produk</th>
                                            <th>Varian</th>
                                            <th>Minimum</th>
                                            <th>Stok</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDataStokProduk" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeX">
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="fitSize"><%# Eval("Tempat") %></td>
                                                    <td><%# Eval("Produk") %></td>
                                                    <td><%# Eval("AtributProduk") %></td>
                                                    <td><%# Eval("JumlahMinimum").ToFormatHargaBulat() %></td>
                                                    <td><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div id="PanelPOBahanBakuJatuhTempo" runat="server" class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card">
                    <div class="card-header">
                        <h5 class="font-weight-light">PO Bahan Baku Jatuh Tempo</h5>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-sm table-bordered table-hover">
                            <thead>
                                <tr class="thead-light">
                                    <th>No</th>
                                    <th>ID</th>
                                    <th>Supplier</th>
                                    <th>Tanggal</th>
                                    <th>Jatuh Tempo</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterPOBahanBakuJatuhTempo" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# Eval("ClassWarna") %>'>
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
                                            <td class="fitSize"><%# Eval("Nama") %></td>
                                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                            <td><%# Eval("TanggalJatuhTempo").ToFormatTanggal() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div id="PanelPOProdukJatuhTempo" runat="server" class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card">
                    <div class="card-header">
                        <h5 class="font-weight-light">PO Produk Jatuh Tempo</h5>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-sm table-bordered table-hover">
                            <thead>
                                <tr class="thead-light">
                                    <th>No</th>
                                    <th>ID</th>
                                    <th>Supplier</th>
                                    <th>Tanggal</th>
                                    <th>Jatuh Tempo</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterPOProdukJatuhTempo" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# Eval("ClassWarna") %>'>
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td class="fitSize"><%# Eval("IDPOProduksiProduk") %></td>
                                            <td class="fitSize"><%# Eval("Nama") %></td>
                                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                            <td><%# Eval("TanggalJatuhTempo").ToFormatTanggal() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <!-- CHART -->
    <script src="/assets/Plugins/CustomWIT/Chart.min.js"></script>
    <script src="/assets/Plugins/CustomWIT/Chart.bundle.min.js"></script>

    <asp:Literal ID="LiteralChartPenjualan" runat="server"></asp:Literal>
</asp:Content>
