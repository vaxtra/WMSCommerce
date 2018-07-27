<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Laporan.aspx.cs" Inherits="WITPointOfSales_Laporan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row page-header hidden-print" style="margin-top: -20px;">
                    <h3 class="text-center">Laporan Tutup Kasir</h3>
                </div>
            </div>
            <div class="form-group hidden-print">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:DropDownList ID="DropDownListStatusCetak" CssClass="select2" runat="server" Style="width: 100%">
                                <asp:ListItem Text="Laporan Tutup Kasir Per Operator" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Laporan Tutup Kasir Keseluruhan" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:DropDownList ID="DropDownListJenisLaporan" CssClass="select2" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListJenisLaporan_SelectedIndexChanged" Visible="true">
                                <asp:ListItem Text="Ringkas" Value="Ringkas"></asp:ListItem>
                                <asp:ListItem Text="Lengkap" Value="Lengkap"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Button CssClass="btn btn-success btn-outline btn-block" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                        </div>
                    </div>
                    <div class="col-xs-6 col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Button CssClass="btn btn-success btn-outline btn-block" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Button CssClass="btn btn-success btn-block" ID="ButtonCetak" runat="server" Text="Cetak" OnClientClick="window.print();" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group" style="font-size: 10pt; line-height: 12px;">
                <center>
                <table style="background-color: #FFF">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <img src="/images/logo.jpg" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <asp:Label ID="LabelPrintNamaStore" runat="server"></asp:Label>
                                        -
                                    <asp:Label ID="LabelPrintNamaLokasi" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <asp:Label ID="LabelPrintTanggal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <br />
                                        <asp:Label ID="LabelStatusLaporan" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <asp:Label ID="LabelPrintTanggalLaporan" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>
                                <tr class="text-center">
                                    <td colspan="3"><b>TRANSAKSI</b></td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>
                                <asp:Repeater ID="RepeaterTransaksiPembayaranLainnya" runat="server">
                                    <ItemTemplate>
                                        <tr class="text-right">
                                            <td></td>
                                            <td style="width: 100%;">
                                                <b><%# Eval("Key") %> : </b>
                                            </td>
                                            <td>
                                                <%# Pengaturan.FormatHarga(Eval("Value")) %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>TOTAL PEMBAYARAN : </b></td>
                                    <td>
                                        <b>
                                            <asp:Label ID="LabelTotalJenisPembayaran" runat="server"></asp:Label></b></td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;">Sebelum Discount : </td>
                                    <td>
                                        <asp:Label ID="LabelSebelumDiscount" runat="server"></asp:Label></td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;">Discount : </td>
                                    <td>
                                        <asp:Label ID="LabelDiscount" runat="server"></asp:Label></td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>GRAND TOTAL : </b></td>
                                    <td><b>
                                        <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></b></td>
                                </tr>

                                <div id="PanelPembayaranAwaitingPayment" runat="server">
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <tr class="text-center">
                                        <td colspan="3"><b>PEMBAYARAN AWAITING PAYMENT</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <asp:Repeater ID="RepeaterPembayaranAwaitingPayment" runat="server">
                                        <ItemTemplate>
                                            <tr class="text-right">
                                                <td></td>
                                                <td style="width: 100%;">
                                                    <b><%# Eval("Key") %> : </b>
                                                </td>
                                                <td>
                                                    <%# Pengaturan.FormatHarga(Eval("Value")) %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr class="text-right">
                                        <td></td>
                                        <td style="width: 100%;"><b>TOTAL : </b></td>
                                        <td>
                                            <b>
                                                <asp:Label ID="LabelTotalJenisPembayaranAwaitingPayment" runat="server"></asp:Label></b></td>
                                    </tr>
                                </div>

                                <div id="PanelCashDrawer" runat="server">
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <tr class="text-center">
                                        <td colspan="3"><b>CASH DRAWER</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <asp:Repeater ID="RepeaterCashDrawer" runat="server">
                                        <ItemTemplate>
                                            <tr class="text-right">
                                                <td></td>
                                                <td style="width: 100%;">
                                                    <b><%# Eval("Key") %> : </b>
                                                </td>
                                                <td>
                                                    <%# Pengaturan.FormatHarga(Eval("Value")) %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr class="text-right">
                                        <td></td>
                                        <td style="width: 100%;"><b>TOTAL : </b></td>
                                        <td>
                                            <b>
                                                <asp:Label ID="LabelTotalCashDrawer" runat="server"></asp:Label></b></td>
                                    </tr>
                                </div>

                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>
                                <tr class="text-center">
                                    <td colspan="3"><b>COMPLETE</b></td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>Transaksi : </b></td>
                                    <td>
                                        <asp:Label ID="LabelJumlahTransaksi" runat="server"></asp:Label></td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>Quantity : </b></td>
                                    <td>
                                        <asp:Label ID="LabelJumlahProduk" runat="server"></asp:Label></td>
                                </tr>

                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>
                                <tr class="text-center">
                                    <td colspan="3"><b>DISCOUNT</b></td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>

                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>Transaksi : </b></td>
                                    <td>
                                        <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label></td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>Produk : </b></td>
                                    <td>
                                        <asp:Label ID="LabelDiscountProduk" runat="server"></asp:Label></td>
                                </tr>

                                <asp:Repeater ID="RepeaterTransaksiStatusLainnya" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="3" class="text-center">--------------------------------------</td>
                                        </tr>
                                        <tr class="text-center">
                                            <td colspan="3"><b><%# Eval("Nama") %></b></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="text-center">--------------------------------------</td>
                                        </tr>

                                        <tr class="text-right">
                                            <td></td>
                                            <td style="width: 100%;"><b>Transaksi : </b></td>
                                            <td><%# Pengaturan.FormatHarga(Eval("Jumlah")) %></td>
                                        </tr>
                                        <tr class="text-right">
                                            <td></td>
                                            <td style="width: 100%;"><b>Total : </b></td>
                                            <td><%# Pengaturan.FormatHarga(Eval("Total")) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <%--LAPORAN ADDICTEA--%>
                            <asp:Panel ID="PanelStokAddictea" runat="server" Visible="false">
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <tr class="text-center">
                                        <td colspan="3"><b>STOK</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>

                                    <tr class="text-right">
                                        <td></td>
                                        <td style="width: 100%;"><b>Restok :</b></td>
                                        <td>
                                            <asp:Label ID="LabelPrintRestok" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="text-right">
                                        <td></td>
                                        <td style="width: 100%;"><b>Terjual :</b></td>
                                        <td>
                                            <asp:Label ID="LabelPrintPenjualan" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="text-right">
                                        <td></td>
                                        <td style="width: 100%;"><b>Retur Ke Gudang:</b></td>
                                        <td>
                                            <asp:Label ID="LabelPrintRetur" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="text-right">
                                        <td></td>
                                        <td style="width: 100%;"><b>Stok Saat Ini :</b></td>
                                        <td class="text-right">
                                            <b>
                                                <asp:Label ID="LabelPrintStok" runat="server"></asp:Label></b></td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <asp:Repeater ID="RepeaterStokPrint" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="3" class="text-center">--------------------------------------</td>
                                            </tr>
                                            <tr class="text-center">
                                                <td colspan="3"><b><%# Eval("NamaProduk") %></b></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="text-center">--------------------------------------</td>
                                            </tr>
                                            <tr class="text-right">
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr style="font-weight: bold;">
                                                            <td class="text-left">Produk</td>
                                                            <td class="text-right">Restok</td>
                                                            <td class="text-right">Terjual</td>
                                                            <td class="text-right">Retur</td>
                                                            <td class="text-right">Stok</td>
                                                        </tr>
                                                        <asp:Repeater ID="RepeaterIsiStokPrint" runat="server" DataSource='<%# Eval("Atribut") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-right"><%# Eval("Nama") %></td>
                                                                    <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Restok")) %></td>
                                                                    <td class="text-right"><%# Pengaturan.FormatHarga(Eval("JumlahJual")) %></td>
                                                                    <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Retur")) %></td>
                                                                    <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Stok")) %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td class="text-right"><b>TOTAL</b></td>
                                                            <td class="text-right"><%# Pengaturan.FormatHarga(Eval("TotalRestok")) %></td>
                                                            <td class="text-right"><%# Pengaturan.FormatHarga(Eval("TotalJumlahJual")) %></td>
                                                            <td class="text-right"><%# Pengaturan.FormatHarga(Eval("TotalRetur")) %></td>
                                                            <td class="text-right"><%# Pengaturan.FormatHarga(Eval("TotalStok")) %></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                            <%--LAPORAN ADDICTEA--%>

                            <asp:Panel ID="PanelPenjualanProduk" runat="server" Visible="false">
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <tr class="text-center">
                                        <td colspan="3"><b>PENJUALAN PRODUK</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="text-center">--------------------------------------</td>
                                    </tr>
                                    <tr style="font-weight: bold;">
                                        <td class="text-left">Produk</td>
                                        <td class="text-right">Terjual</td>
                                    </tr>
                                    <asp:Repeater ID="RepeaterPenjualanProduk" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-left"><%# Eval("Key") %></td>
                                                <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Quantity")) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="text-center"><b>TOTAL</b></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalPenjualanProduk" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" class="text-center">--------------------------------------</td>
                                </tr>
                                <tr>
                                    <td class="text-center">THANK YOU</td>
                                </tr>
                                <tr>
                                    <td class="text-center">Warehouse Management System Powered by WIT.</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                    </center>
            </div>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

