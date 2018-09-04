<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="RushHour.aspx.cs" Inherits="WITReport_Transaksi_RushHour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Rush Hour
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <input id="ButtonPrint" type="button" value="Print" class="btn btn-secondary btn-const" onclick="window.print();" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelRushHourRestaurant" runat="server">
        <ContentTemplate>
            <div class="form-group hidden-print">
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
                <div class="row hidden-print">
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListTempat_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListStatusTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStatusTransaksi_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <h4 class="text-uppercase mb-3">
                <asp:Label ID="LabelFilter" runat="server"></asp:Label>
                [
                        <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                ]</h4>
            <div class="form-group mb-0">
                <div class="row mb-0">
                    <div class="col-md-3 text-center mb-0" id="DivTotalTamu" runat="server">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Tamu :
                <asp:Label ID="LabelTotalTamu" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-3 text-center mb-0">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Quantity :
                <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-3 text-center mb-0">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Transaksi :
                <asp:Label ID="LabelTotalTransaksi" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-3 text-center mb-0">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Penjualan :
                <asp:Label ID="LabelTotalPenjualan" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="card">
                            <h5 class="card-header bg-gradient-blue">00:00 - 12:00</h5>
                            <div class="table-responsive">
                                <table class="table table-sm table-bordered table-hover">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>Jam</th>
                                            <th>Quantity</th>
                                            <th>%</th>
                                            <th>Transaksi</th>
                                            <th>%</th>
                                            <th id="HeaderTamuPagi" runat="server">Tamu</th>
                                            <th id="HeaderPersentasePagi" runat="server">%</th>
                                            <th>Penjualan</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterSebelumTengahHari" runat="server" OnItemCreated="Repeater_ItemCreated">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center table-secondary"><strong><%# Eval("Jam") + ":00" %></strong></td>
                                                    <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                                    <td class="text-right table-info"><%# Eval("PersentaseQuantity").ToFormatHarga() %> %</td>
                                                    <td class="text-right"><%# Eval("Transaksi").ToFormatHargaBulat() %></td>
                                                    <td class="text-right table-info"><%# Eval("PersentaseTransaksi").ToFormatHarga() %> %</td>
                                                    <td class="text-right" id="BodyTamu" runat="server"><%# Eval("JumlahTamu").ToFormatHargaBulat() %></td>
                                                    <td class="text-right table-info" id="BodyPersentase" runat="server"><%# Eval("PersentaseJumlahTamu").ToFormatHarga() %> %</td>
                                                    <td class="text-right table-warning"><strong><%# Eval("Penjualan").ToFormatHarga() %></strong></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="font-weight-bold table-success">
                                            <td></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LabelSebelumTengahHariQuantity" runat="server"></asp:Label></td>
                                            <td></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LabelSebelumTengahHariTransaksi" runat="server"></asp:Label></td>
                                            <td></td>
                                            <td style="text-align: right;" id="FooterTamuPagi" runat="server">
                                                <asp:Label ID="LabelSebelumTengahHariJumlahTamu" runat="server"></asp:Label></td>
                                            <td id="FooterPersentasePagi" runat="server"></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LabelSebelumTengahHariPenjualan" runat="server"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card">
                            <h5 class="card-header bg-gradient-blue">12:00 - 24:00</h5>
                            <div class="table-responsive">
                                <table class="table table-sm table-bordered table-hover">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>Jam</th>
                                            <th>Quantity</th>
                                            <th>%</th>
                                            <th>Transaksi</th>
                                            <th>%</th>
                                            <th id="HeaderTamuMalam" runat="server">Tamu</th>
                                            <th id="HeaderPersentaseMalam" runat="server">%</th>
                                            <th>Penjualan</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterSetelahTengahHari" runat="server" OnItemCreated="Repeater_ItemCreated">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center table-secondary"><strong><%# Eval("Jam") + ":00" %></strong></td>
                                                    <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                                    <td class="text-right table-info"><%# Eval("PersentaseQuantity").ToFormatHarga() %> %</td>
                                                    <td class="text-right"><%# Eval("Transaksi").ToFormatHargaBulat() %></td>
                                                    <td class="text-right table-info"><%# Eval("PersentaseTransaksi").ToFormatHarga() %> %</td>
                                                    <td class="text-right" id="BodyTamu" runat="server"><%# Eval("JumlahTamu").ToFormatHargaBulat() %></td>
                                                    <td class="text-right table-info" id="BodyPersentase" runat="server"><%# Eval("PersentaseJumlahTamu").ToFormatHarga() %> %</td>
                                                    <td class="text-right table-warning"><strong><%# Eval("Penjualan").ToFormatHarga() %></strong></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="font-weight-bold table-success">
                                            <td></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LabelSetelahTengahHariQuantity" runat="server"></asp:Label></td>
                                            <td></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LabelSetelahTengahHariTransaksi" runat="server"></asp:Label></td>
                                            <td></td>
                                            <td style="text-align: right;" id="FooterTamuMalam" runat="server">
                                                <asp:Label ID="LabelSetelahTengahHariJumlahTamu" runat="server"></asp:Label></td>
                                            <td id="FooterPersentaseMalam" runat="server"></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="LabelSetelahTengahHariPenjualan" runat="server"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgressRushHourRestaurant" runat="server" AssociatedUpdatePanelID="UpdatePanelRushHourRestaurant">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressRushHourRestaurant" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

