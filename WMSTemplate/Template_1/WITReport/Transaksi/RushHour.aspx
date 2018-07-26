<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="RushHour.aspx.cs" Inherits="WITReport_Transaksi_RushHour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Rush Hour
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <input id="ButtonPrint" type="button" value="Print" class="btn btn-default btn-sm" onclick="window.print();" />
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
                <div class="row hidden-print" style="font-weight: bold;">
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListTempat_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="DropDownListStatusTransaksi" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStatusTransaksi_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            <asp:Label ID="LabelFilter" runat="server"></asp:Label>
                            [
                        <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                            ]</h4>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3 text-center" id="DivTotalTamu" runat="server">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Tamu :
                <asp:Label ID="LabelTotalTamu" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-3 text-center">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Quantity :
                <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-3 text-center">
                        <p class="bg-info" style="font-size: 18px;">
                            Total Transaksi :
                <asp:Label ID="LabelTotalTransaksi" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-3 text-center">
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
                        <div class="panel panel-success">
                            <div class="panel-heading text-center"><strong>00:00 - 12:00</strong></div>
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px; width: 100%">
                                    <thead>
                                        <tr class="active">
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
                                                    <td class="text-center active"><strong><%# Eval("Jam") + ":00" %></strong></td>
                                                    <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                                    <td class="text-right info"><%# Eval("PersentaseQuantity").ToFormatHarga() %> %</td>
                                                    <td class="text-right"><%# Eval("Transaksi").ToFormatHargaBulat() %></td>
                                                    <td class="text-right info"><%# Eval("PersentaseTransaksi").ToFormatHarga() %> %</td>
                                                    <td class="text-right" id="BodyTamu" runat="server"><%# Eval("JumlahTamu").ToFormatHargaBulat() %></td>
                                                    <td class="text-right info" id="BodyPersentase" runat="server"><%# Eval("PersentaseJumlahTamu").ToFormatHarga() %> %</td>
                                                    <td class="text-right warning"><strong><%# Eval("Penjualan").ToFormatHarga() %></strong></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="success" style="font-weight: bold;">
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
                        <div class="panel panel-success">
                            <div class="panel-heading text-center"><strong>12:00 - 24:00</strong></div>
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px; width: 100%">
                                    <thead>
                                        <tr class="active">
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
                                                    <td class="text-center active"><strong><%# Eval("Jam") + ":00" %></strong></td>
                                                    <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                                    <td class="text-right info"><%# Eval("PersentaseQuantity").ToFormatHarga() %> %</td>
                                                    <td class="text-right"><%# Eval("Transaksi").ToFormatHargaBulat() %></td>
                                                    <td class="text-right info"><%# Eval("PersentaseTransaksi").ToFormatHarga() %> %</td>
                                                    <td class="text-right" id="BodyTamu" runat="server"><%# Eval("JumlahTamu").ToFormatHargaBulat() %></td>
                                                    <td class="text-right info" id="BodyPersentase" runat="server"><%# Eval("PersentaseJumlahTamu").ToFormatHarga() %> %</td>
                                                    <td class="text-right warning"><strong><%# Eval("Penjualan").ToFormatHarga() %></strong></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="success" style="font-weight: bold;">
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

