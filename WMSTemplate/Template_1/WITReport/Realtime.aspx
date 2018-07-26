<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Realtime.aspx.cs" Inherits="WITReport_Realtime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="LiteralRefresh" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Penjualan Bulan Ini
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
    <div class="form-group">
        <div class="row">
            <div class="col-md-8">
                <div id="PanelAktifitasTransaksi1" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:DropDownList ID="DropDownListTempat" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListTempat_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="DropDownListDurasiRefresh" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListDurasiRefresh_SelectedIndexChanged">
                                <asp:ListItem Text="1 Menit" Value="1" />
                                <asp:ListItem Text="5 Menit" Value="5" />
                                <asp:ListItem Text="10 Menit" Value="10" />
                                <asp:ListItem Text="30 Menit" Value="30" />
                                <asp:ListItem Text="1 Jam" Value="60" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div id="graph2" style="height: 200px;"></div>
                    </div>
                </div>

                <div id="PanelAktifitasTransaksi2" runat="server">
                    <div class="form-group">
                        <h4>Transaksi dan Pelanggan Bulan Ini</h4>
                    </div>
                    <div class="form-group">
                        <div id="graph" style="height: 200px;"></div>
                    </div>
                </div>

                <div id="panelStokProdukHabis" runat="server">
                    <div class="form-group">
                        <h4>Stok Produk Akan Habis
                        <span class="badge">
                            <asp:Label ID="LabelJumlahStokProdukHabis" runat="server"></asp:Label></span>
                        </h4>
                    </div>
                    <div class="form-group">
                        <table class="table table-condensed table-hover">
                            <thead>
                                <tr>
                                    <th>No</th>
                                    <th>Tempat</th>
                                    <th>Produk</th>
                                    <th>Stok</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterDataStokProduk" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%# Eval("Tempat") %></td>
                                            <td><%# Eval("Nama") %></td>
                                            <td><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div id="PanelAktifitasTransaksi3" runat="server">
                    <div class="form-group">
                        <table class="table table-condensed table-hover">
                            <thead>
                                <tr class="active">
                                    <th>Aktifitas Hari Ini</th>
                                    <th class="text-right">
                                        <span id="LabelAktifitasHariIni" runat="server" class="label label-success">
                                            <asp:Label ID="LabelPenjualanHariIni" runat="server"></asp:Label>
                                        </span>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
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

                    <div class="form-group">
                        <table class="table table-condensed table-hover">
                            <thead>
                                <tr class="active">
                                    <th>Aktifitas Kemarin</th>
                                    <th class="text-right">
                                        <span id="LabelAktifitasKemarin" runat="server" class="label label-success">
                                            <asp:Label ID="LabelPenjualanKemarin" runat="server"></asp:Label>
                                        </span>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
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

                    <div class="form-group">
                        <table class="table table-condensed table-hover">
                            <thead>
                                <tr class="active">
                                    <th>Aktifitas Bulan Ini</th>
                                    <th class="text-right">
                                        <span id="LabelAktifitasBulanIni" runat="server" class="label label-success">
                                            <asp:Label ID="LabelPenjualanBulanIni" runat="server"></asp:Label>
                                        </span>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
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

                    <div class="form-group">
                        <table class="table table-condensed table-hover">
                            <thead>
                                <tr class="active">
                                    <th>Aktifitas Bulan Lalu</th>
                                    <th class="text-right">
                                        <span id="LabelAktifitasBulanLalu" runat="server" class="label label-success">
                                            <asp:Label ID="LabelPenjualanBulanLalu" runat="server"></asp:Label>
                                        </span>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
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
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script src="/assets/plugins/plugins/Morris/raphael-min.js"></script>
    <script src="/assets/plugins/plugins/Morris/morris.min.js"></script>

    <script src="/assets/plugins/Highcharts/highcharts.js"></script>
    <script src="/assets/plugins/Highcharts/modules/exporting.js"></script>
    <asp:Literal ID="LiteralChart" runat="server"></asp:Literal>
</asp:Content>

