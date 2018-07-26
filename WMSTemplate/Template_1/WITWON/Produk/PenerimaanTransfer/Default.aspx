﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WITWON/MasterPageWebView.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITWON_Produk_PenerimaanTransfer_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleLeft" runat="Server">
    Penerimaan Transfer Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#proses" data-toggle="tab">Proses</a></li>
                <li><a href="#selesai" data-toggle="tab">Selesai</a></li>
                <li class="pull-right">
                    <asp:UpdatePanel ID="UpdatePanelTanggal" runat="server">
                        <ContentTemplate>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownListCariBulan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCari_SelectedIndexChanged">
                                        <asp:ListItem Text="Januari" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Febuari" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Maret" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Mei" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Juni" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Juli" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Agustus" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oktober" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nopember" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Desember" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownListCariTahun" runat="server" CssClass="select2 pull-right" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCari_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <asp:UpdateProgress ID="updateProgressTanggal" runat="server" AssociatedUpdatePanelID="UpdatePanelTanggal">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressTanggal" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
            <br />
            <div class="tab-content">
                <div class="tab-pane active" id="proses">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                    <thead>
                                        <tr class="active">
                                            <th>No.</th>
                                            <th>ID</th>
                                            <th>Pengirim</th>
                                            <th>Tanggal</th>
                                            <th>Kirim</th>
                                            <th>Jumlah</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterTransferProses" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="fitSize"><a href="Detail.aspx?id=<%# Eval("IDTransferProduk") %>"><%# Eval("IDTransferProduk") %></a></td>
                                                    <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                                                    <td><%# Eval("TanggalKirim").ToFormatTanggal() %></td>
                                                    <td><%# Eval("TBTempat.Nama") %></td>
                                                    <td class="text-right fitSize"><%# Eval("TotalJumlah").ToFormatHargaBulat() %></td>
                                                    <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
                                                    <td class="text-right fitSize">
                                                        <a class="btn btn-info btn-xs" href="Pengaturan.aspx?id=<%# Eval("IDTransferProduk") %>">Terima</a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="selesai">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                    <thead>
                                        <tr class="active">
                                            <th>No.</th>
                                            <th>ID</th>
                                            <th>Pengirim</th>
                                            <th>Tanggal</th>
                                            <th>Kirim</th>
                                            <th>Jumlah</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterTransferSelesai" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="fitSize"><a href="Detail.aspx?id=<%# Eval("IDTransferProduk") %>"><%# Eval("IDTransferProduk") %></a></td>
                                                    <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                                                    <td><%# Eval("TanggalKirim").ToFormatTanggal() %></td>
                                                    <td><%# Eval("TBTempat.Nama") %></td>
                                                    <td class="text-right fitSize"><%# Eval("TotalJumlah").ToFormatHargaBulat() %></td>
                                                    <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

