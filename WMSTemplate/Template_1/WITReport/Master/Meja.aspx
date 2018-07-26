<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Meja.aspx.cs" Inherits="WITReport_Master_Meja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Meja
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
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
                <div class="row" style="font-weight: bold;">
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="DropDownListFilter" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                            <asp:ListItem Text="Meja" Value="Meja"></asp:ListItem>
                            <asp:ListItem Text="Transaksi" Value="Transaksi"></asp:ListItem>
                            <asp:ListItem Text="Produk" Value="Produk"></asp:ListItem>
                            <asp:ListItem Text="Tamu" Value="Tamu"></asp:ListItem>
                            <asp:ListItem Text="Grandtotal" Value="Grandtotal" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="DropDownListStatus" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged"></asp:DropDownList>
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
                                <th>No</th>
                                <th>Meja</th>
                                <th>Jumlah Transaksi</th>
                                <th>%</th>
                                <th>Jumlah Produk</th>
                                <th>%</th>
                                <th>Jumlah Tamu</th>
                                <th>%</th>
                                <th>Grandtotal</th>
                            </tr>
                            <tr class="success text-right" style="font-weight: bold;">
                                <td class="text-center" colspan="2">TOTAL</td>
                                <td>
                                    <asp:Label ID="LabelHeaderTotalTransaksi" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="LabelHeaderTotalProduk" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="LabelHeaderTotalTamu" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="LabelHeaderTotalGrandtotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="Repeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td class="text-center fitSize"><%# Eval("Meja") %></td>
                                        <td class="text-right fitSize"><%# Eval("JumlahTransaksi").ToFormatHargaBulat() %></td>
                                        <td class="text-right warning fitSize"><%# Eval("PersentaseTransaksi") %> %</td>
                                        <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                        <td class="text-right warning fitSize"><%# Eval("PersentaseProduk") %> %</td>
                                        <td class="text-right fitSize"><%# Eval("JumlahTamu").ToFormatHargaBulat() %></td>
                                        <td class="text-right warning fitSize"><%# Eval("PersentaseTamu") %> %</td>
                                        <td class="text-right fitSize"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                        <tfoot>
                            <tr class="success text-right" style="font-weight: bold;">
                                <td class="text-center" colspan="2">TOTAL</td>
                                <td>
                                    <asp:Label ID="LabelFooterTotalTransaksi" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="LabelFooterTotalProduk" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="LabelFooterTotalTamu" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="LabelFooterTotalGrandtotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

