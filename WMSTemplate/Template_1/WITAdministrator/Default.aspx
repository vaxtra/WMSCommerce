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
    <div id="PanelAktifitasTransaksi3" runat="server" class="form-group">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <h4 class="font-weight-light float-left">Penjualan</h4>
                            <h4 class="text-primary float-right">
                                <asp:Label ID="LabelPenjualanBulanIni" runat="server"></asp:Label></h4>
                        </div>
                        <br />
                        <hr />
                        <div class="form-group">
                            <canvas id="CanvasChartPenjualan" class="m-0 w-100" height="180px"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <h4 class="font-weight-light float-left">Transaksi</h4>
                            <h4 class="text-primary float-right">
                                <asp:Label ID="LabelTransaksiBulanIni" runat="server"></asp:Label></h4>
                        </div>
                        <br />
                        <hr />
                        <div class="form-group">
                            <canvas id="CanvasChartTransaksi" class="m-0 w-100" height="180px"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <h4 class="font-weight-light float-left">Produk</h4>
                            <h4 class="text-primary float-right">
                                <asp:Label ID="LabelQuantityBulanIni" runat="server"></asp:Label></h4>
                        </div>
                        <br />
                        <hr />
                        <div class="form-group">
                            <canvas id="CanvasChartJumlahProduk" class="m-0 w-100" height="180px"></canvas>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group mb-0">
        <div class="row">
            <div id="panelTransaksiTerakhir" runat="server" class="col-xs-12 col-sm-12 col-md-12 col-lg-6 ">
                <div class="form-group">
                    <div class="card">
                        <h5 class="card-header bg-gradient-blue">TOP TRANSAKSI</h5>
                        <div class="table-responsive">
                            <table class="table table-sm table-bordered table-hover">
                                <thead>
                                    <tr class="thead-light">
                                        <th>ID</th>
                                        <th>Tanggal</th>
                                        <th>Pelanggan</th>
                                        <th>Qty</th>
                                        <th>Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterTopTransaksi" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("IDTransaksi") %></td>
                                                <td class="fitSize"><%# Eval("TanggalOperasional").ToFormatTanggalHari() %></td>
                                                <td><%# Eval("Pelanggan") %></td>
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
                <div class="form-group">
                    <div class="card">
                        <h5 class="card-header bg-gradient-blue">TOP PRODUK</h5>
                        <div class="table-responsive">
                            <table class="table table-sm table-bordered table-hover">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No</th>
                                        <th>Kode</th>
                                        <th>Produk</th>
                                        <th>Varian</th>
                                        <th>Jumlah</th>
                                        <th>Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterTopProduk" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                                <td class="text-right fitSize"><%# Eval("Subtotal").ToFormatHarga() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div id="panelStokProdukHabis" runat="server" class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card">
                    <h5 class="card-header bg-gradient-green">PRODUK AKAN HABIS</h5>
                    <div class="card-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DropDownListJumlahData" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="20" Value="20" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-10">
                                    <div class="form-group float-right mb-0">
                                        <asp:Button ID="ButtonPrevious" runat="server" CssClass="btn btn-outline-light" Text="<<" OnClick="ButtonPrevious_Click" />
                                        <asp:DropDownList ID="DropDownListHalaman" CssClass="select2 select2-fix-width-sm" runat="server" Style="width: 50px !important;" AutoPostBack="true" OnSelectedIndexChanged="EventData">
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
                                            <th>Produk</th>
                                            <th>Varian</th>
                                            <th>Stok</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDataStokProduk" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeX">
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("Produk") %></td>
                                                    <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
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
                    <h4 class="card-header bg-smoke">PO Bahan Baku Jatuh Tempo</h4>
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
                    <h4 class="card-header bg-smoke">PO Produk Jatuh Tempo</h4>
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
    <asp:Literal ID="LiteralChartTransaksi" runat="server"></asp:Literal>
    <asp:Literal ID="LiteralChartJumlahProduk" runat="server"></asp:Literal>
</asp:Content>
