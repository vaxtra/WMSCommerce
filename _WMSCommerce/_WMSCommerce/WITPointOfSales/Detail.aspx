<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITPointOfSales_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) {
                newwindow.focus()
            }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="hidden-print">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="LabelStatusTransaksi" runat="server" Width="100%" Font-Size="X-Large"></asp:Label>
                </div>
            </div>
            <div class="col-md-8">
                <div class="row">
                    <div class="col-xs-4 col-sm-3 col-md-3">
                        <div class="form-group">
                            <asp:Button ID="ButtonPrint1" runat="server" Text="Print POS" CssClass="btn btn-default btn-outline btn-block" OnClientClick="window.print();" />
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-3 col-md-3">
                        <div class="form-group">
                            <asp:Button ID="ButtonPrint2" runat="server" Text="Print Invoice" CssClass="btn btn-default btn-outline btn-block" />
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-3 col-md-4">
                        <div class="form-group">
                            <asp:Button ID="ButtonPrint3" runat="server" Text="Print Packing Slip" CssClass="btn btn-default btn-outline btn-block" />
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-3 col-md-2">
                        <div class="form-group">
                            <asp:Button ID="ButtonKeluar" runat="server" CssClass="btn btn-danger btn-block" Text="Keluar" OnClick="ButtonKeluar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                        <tr>
                            <td class="text-center" colspan="2" style="font-size: 18px; font-weight: bold;">
                                <asp:Label ID="LabelJenisTransaksi" runat="server"></asp:Label>
                                #<asp:Label ID="LabelIDTransaksi" runat="server"></asp:Label></td>
                        </tr>
                        <asp:Panel ID="PanelPelanggan2" runat="server">
                            <tr>
                                <td colspan="2" class="text-center info" style="font-weight: bold;">PELANGGAN</td>
                            </tr>
                            <tr>
                                <td class="fitSize" style="font-weight: bold;">Nama</td>
                                <td>
                                    <asp:Label ID="LabelPelangganNama" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold;">Telepon</td>
                                <td>
                                    <asp:Label ID="LabelPelangganTelepon" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold;">Alamat</td>
                                <td>
                                    <asp:Label ID="LabelPelangganAlamat" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2" class="text-center info" style="font-weight: bold;">TRANSAKSI</td>
                        </tr>
                        <tr>
                            <td class="fitSize" style="font-weight: bold;">Status</td>
                            <td>
                                <asp:Label ID="LabelMeja" runat="server"></asp:Label>
                                (<asp:Label ID="LabelPAX" runat="server"></asp:Label>
                                PAX)</td>
                        </tr>
                        <tr>
                            <td class="fitSize" style="font-weight: bold;">Tempat</td>
                            <td>
                                <asp:Label ID="LabelTempat" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="fitSize" style="font-weight: bold;">Operasional</td>
                            <td>
                                <asp:Label ID="LabelTanggalOperasional" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="fitSize" style="font-weight: bold;">Pegawai</td>
                            <td>
                                <asp:Label ID="LabelPenggunaTransaksi" runat="server"></asp:Label>
                                (<asp:Label ID="LabelTanggalTransaksi" runat="server"></asp:Label>)</td>
                        </tr>
                        <tr class="active" id="PanelPerubahanTerakhir1" runat="server">
                            <td class="fitSize" style="font-weight: bold;">Pegawai</td>
                            <td>
                                <asp:Label ID="LabelPenggunaUpdate" runat="server"></asp:Label>
                                (<asp:Label ID="LabelTanggalUpdate" runat="server"></asp:Label>)</td>
                        </tr>
                        <tr class="warning">
                            <td class="fitSize" style="font-weight: bold;">Subtotal</td>
                            <td class="text-right">
                                <asp:Label ID="LabelSubtotal" runat="server"></asp:Label></td>
                        </tr>
                        <tr runat="server" id="PanelDiscount">
                            <td class="fitSize" style="font-weight: bold;">Discount</td>
                            <td style="text-align: right;">
                                <asp:Label ID="LabelDiscount" runat="server"></asp:Label></td>
                        </tr>
                        <tr runat="server" id="PanelBiayaTambahan11">
                            <td class="fitSize" style="font-weight: bold;">
                                <asp:Label ID="LabelKeteranganBiayaTambahan1" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelBiayaTambahan1" runat="server"></asp:Label></td>
                        </tr>
                        <tr runat="server" id="PanelBiayaTambahan12">
                            <td class="fitSize" style="font-weight: bold;">
                                <asp:Label ID="LabelKeteranganBiayaTambahan2" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelBiayaTambahan2" runat="server"></asp:Label></td>
                        </tr>
                        <tr runat="server" id="PanelBiayaTambahan13">
                            <td class="fitSize" style="font-weight: bold;">
                                <asp:Label ID="LabelKeteranganBiayaTambahan3" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelBiayaTambahan3" runat="server"></asp:Label></td>
                        </tr>
                        <tr runat="server" id="PanelBiayaTambahan14">
                            <td class="fitSize" style="font-weight: bold;">
                                <asp:Label ID="LabelKeteranganBiayaTambahan4" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelBiayaTambahan4" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="PanelBiayaPengiriman1" runat="server">
                            <td class="fitSize" style="font-weight: bold;">Pengiriman</td>
                            <td class="text-right">
                                <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="PanelPembulatan1" runat="server">
                            <td class="fitSize" style="font-weight: bold;">Pembulatan</td>
                            <td class="text-right">
                                <asp:Label ID="LabelPembulatan" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="success" style="font-weight: bold; font-size: 16px;">
                            <td class="fitSize">Grand Total</td>
                            <td class="text-right">
                                <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="PanelKeterangan2" runat="server">
                            <td class="fitSize" style="font-weight: bold;">Keterangan</td>
                            <td style="word-wrap: break-word !important;">
                                <asp:Label ID="LabelKeterangan" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="col-md-8">
                <div class="form-group">
                    <div class="panel panel-info">
                        <div class="panel-heading">Produk</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Produk</th>
                                        <th class="text-right">Quantity</th>
                                        <th class="text-right">Harga</th>
                                        <th class="text-right">Discount</th>
                                        <th class="text-right">Subtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterDetailTransaksi" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("JumlahProduk").ToString())) %>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("HargaJual").ToString())) %>
                                                <%# Parse.Decimal(Eval("JumlahProduk").ToString()) > 0 ? Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("PotonganHargaJual").ToString()) * -1) : Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("PotonganHargaJual").ToString())) %>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("Subtotal").ToString())) %>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="text-right warning" style="font-weight: bold;">
                                        <td colspan="2"></td>
                                        <td>
                                            <asp:Label ID="LabelTotalQuantity1" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2">Sebelum Discount</td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelDiscountSebelum" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="text-right" style="font-size: 11px; font-weight: bold;" id="PanelDiscountDetailProduk" runat="server">
                                        <td colspan="5">Discount Produk</td>
                                        <td>
                                            <asp:Label ID="LabelDiscountProduk" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="text-right" style="font-size: 11px; font-weight: bold;" id="PanelDiscountDetailTransaksi" runat="server">
                                        <td colspan="5">Discount Transaksi</td>
                                        <td>
                                            <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="text-right" style="font-size: 11px; font-weight: bold;" id="PanelDiscountDetailVoucher" runat="server">
                                        <td colspan="5">Discount Voucher</td>
                                        <td>
                                            <asp:Label ID="LabelDiscountVoucher" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="text-right" style="font-weight: bold;" id="PanelTotalDiscount" runat="server">
                                        <td colspan="5">Discount</td>
                                        <td>
                                            <asp:Label ID="LabelTotalDiscount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="success text-right" style="font-weight: bold;" id="PanelSetelahDiscount" runat="server">
                                        <td colspan="5">Setelah Discount</td>
                                        <td>
                                            <asp:Label ID="LabelDiscountSetelah" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="TabelPembayaran" runat="server" class="form-group">
                    <div class="panel panel-default">
                        <div class="panel-heading">Pembayaran</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Tanggal</th>
                                        <th>Pengguna</th>
                                        <th>Jenis</th>
                                        <th class="text-right">Total</th>
                                        <th class="text-center">Keterangan</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPembayaran" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Pengaturan.FormatTanggal(Eval("Tanggal")) %></td>
                                                <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                                                <td><%# Eval("TBJenisPembayaran.Nama") %></td>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("Total").ToString())) %>
                                                <td><%# Eval("Keterangan") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tr class="text-right success" style="font-weight: bold;">
                                    <td colspan="4"></td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelTotalPembayaran" runat="server"></asp:Label></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row visible-print" style="font-size: 10pt; line-height: 12px;">
        <table style="width: 100%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="2" class="text-center">
                                <img src="/images/logo.jpg" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-center">
                                <asp:Label ID="LabelPrintStore" runat="server"></asp:Label>
                                -
                                <asp:Label ID="LabelPrintTempatNama" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-center">
                                <asp:Label ID="LabelTempatAlamat" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-center">
                                <asp:Label ID="LabelTempatTelepon" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-center">Order #<asp:Label ID="LabelPrintIDOrder" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-left">Meja :
                                        <asp:Label ID="LabelPrintMeja" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left"></td>
                            <td class="text-right">
                                <asp:Label ID="LabelPrintJenisPembayaran" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="LabelPrintPengguna" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelPrintTanggal" runat="server"></asp:Label></td>
                        </tr>
                    </table>

                    <div id="PanelPelanggan" runat="server">
                        <tr>
                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top; font-weight: bold;">Nama</td>
                                        <td style="vertical-align: top;">:</td>
                                        <td>
                                            <asp:Label ID="LabelPrintPelangganNama" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; font-weight: bold;">Telepon</td>
                                        <td style="vertical-align: top;">:</td>
                                        <td>
                                            <asp:Label ID="LabelPrintPelangganTelepon" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; font-weight: bold;">Alamat</td>
                                        <td style="vertical-align: top;">:</td>
                                        <td>
                                            <asp:Label ID="LabelPrintPelangganAlamat" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                        </tr>
                        <asp:Repeater ID="RepeaterPrintTransaksiDetail" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("JumlahProduk") %>x</td>
                                    <td><%# Eval("Produk") %></td>
                                    <td class="text-right"><%# Pengaturan.FormatHarga(Eval("TotalTanpaPotonganHargaJual")) %></td>
                                </tr>
                                <tr runat="server" visible='<%# (Parse.Decimal(Eval("PotonganHargaJual").ToString()) == 0) ? false : true %>'>
                                    <td></td>
                                    <td class="text-right">Discount</td>
                                    <td class="text-right">-<%# Pengaturan.FormatHarga(Eval("TotalPotonganHargaJual").ToString()) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                        </tr>
                        <tr class="text-right">
                            <td></td>
                            <td style="width: 100%;"><b>QUANTITY :</b></td>
                            <td class="text-right" style="width: 100%;">
                                <asp:Label ID="LabelPrintQuantity" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr class="text-right">
                            <td></td>
                            <td style="width: 100%;"><b>SUBTOTAL :</b></td>
                            <td>
                                <asp:Label ID="LabelPrintSubtotal" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right" id="PanelDiscountTransaksi" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>DISCOUNT :</b></td>
                            <td>
                                <asp:Label ID="LabelPrintDiscountTransaksi" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right" id="PanelBiayaTambahan1" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>
                                <asp:Label ID="LabelPrintKeteranganBiayaTambahan1" runat="server"></asp:Label>
                                :</b></td>
                            <td class="text-right" style="width: 100%;">
                                <asp:Label ID="LabelPrintBiayaTambahan1" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right" id="PanelBiayaTambahan2" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>
                                <asp:Label ID="LabelPrintKeteranganBiayaTambahan2" runat="server"></asp:Label>
                                :</b></td>
                            <td class="text-right" style="width: 100%;">
                                <asp:Label ID="LabelPrintBiayaTambahan2" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right" id="PanelBiayaPengiriman" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>DELIVERY :</b></td>
                            <td>
                                <asp:Label ID="LabelPrintBiayaPengiriman" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right" id="PanelPembulatan" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>PEMBULATAN :</b></td>
                            <td>
                                <asp:Label ID="LabelPrintPembulatan" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right">
                            <td></td>
                            <td style="width: 100%;"><b>TOTAL :</b></td>
                            <td class="text-right" style="width: 100%;">
                                <b>
                                    <asp:Label ID="LabelPrintGrandTotal" runat="server"></asp:Label></b></td>
                        </tr>
                        <tr class="text-right" id="PanelPembayaran" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>CASH :</b></td>
                            <td class="text-right" style="width: 100%;">
                                <asp:Label ID="LabelPrintPembayaran" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="text-right" id="PanelKembalian" runat="server">
                            <td></td>
                            <td style="width: 100%;"><b>CHANGE :</b></td>
                            <td class="text-right" style="width: 100%;">
                                <asp:Label ID="LabelPrintKembalian" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="PanelJenisPembayaran" runat="server">
                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                        </tr>
                        <tr id="PanelJenisPembayaran1" runat="server">
                            <td colspan="3">
                                <table>
                                    <asp:Repeater ID="RepeaterPrintJenisPembayaran" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="font-weight: bold;"><%# Eval("TBJenisPembayaran.Nama") %></td>
                                                <td>&nbsp;:&nbsp;</td>
                                                <td><%# Pengaturan.FormatHarga(Eval("Total")) %></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td><%# Eval("Keterangan") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr id="PanelKeterangan" runat="server">
                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                        </tr>
                        <tr id="PanelKeterangan1" runat="server">
                            <td colspan="3" class="text-center">
                                <asp:Label ID="LabelPrintKeterangan" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="PanelFooter" runat="server">
                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                        </tr>
                        <tr id="PanelFooter1" runat="server">
                            <td colspan="3">
                                <asp:Label ID="LabelPrintFooter" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%;">
                        <tr>
                            <td class="text-center">(Reprint)</td>
                        </tr>
                        <tr>
                            <td class="text-center">&nbsp;</td>
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
    </div>
</asp:Content>

