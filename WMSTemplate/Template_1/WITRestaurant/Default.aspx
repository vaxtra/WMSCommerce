<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITRestaurant_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .loading {
            background-image: url('/assets/images/loader.gif');
            background-position: right;
            background-repeat: no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row hidden-print">
                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;"><a href="WaitingList.aspx" class="btn btn-warning btn-lg btn-block">Waiting List</a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonOrderCheck" runat="server" CssClass="btn btn-primary btn-lg btn-block" Text="Order Check" OnClick="ButtonOrderCheck_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonPreSettlement" runat="server" CssClass="btn btn-primary btn-lg btn-block" Text="Pre Settlement" OnClick="ButtonPreSettlement_Click" />
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonSplitBill" runat="server" CssClass="btn btn-primary btn-lg btn-block" Text="Split Bill" OnClick="ButtonSplitBill_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonTransfer" runat="server" CssClass="btn btn-primary btn-lg btn-block" Text="Transfer" OnClick="ButtonTransfer_Click" />
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonPindahMeja" runat="server" CssClass="btn btn-primary btn-lg btn-block" Text="Pindah Meja" OnClick="ButtonPindahMeja_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonReprint" runat="server" CssClass="btn btn-info btn-lg btn-block" Text="Reprint" OnClick="ButtonReprint_Click" />
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonMessage" runat="server" CssClass="btn btn-info btn-lg btn-block" Text="Message" OnClick="ButtonMessage_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonWaktuPelanggan" runat="server" CssClass="btn btn-info btn-lg btn-block" Text="Waktu" OnClick="ButtonWaktuPelanggan_Click" />
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 5px;">
                            <asp:Button ID="ButtonLihatPesanan" runat="server" CssClass="btn btn-info btn-lg btn-block" Text="Lihat Pesanan" OnClick="ButtonLihatPesanan_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                            <asp:HiddenField ID="HiddenFieldPerintah" runat="server" />
                            <asp:HiddenField ID="HiddenFieldTransaksiAwal" runat="server" />

                            <div class="table-responsive">
                                <table style="background-color: rgb(248, 248, 248); width: 100%;">
                                    <tr>
                                        <td colspan="5" style="padding: 0px 2.5px 5px 2.5px">
                                            <asp:Button ID="ButtonWithoutTable" runat="server" CssClass="btn btn-default btn-outline btn-lg btn-block" Text="Without Table" OnClick="ButtonWithoutTable_Click" /></td>
                                        <td colspan="5" style="padding: 0px 2.5px 5px 2.5px">
                                            <asp:Button ID="ButtonTakeAway" runat="server" CssClass="btn btn-default btn-outline btn-lg btn-block" Text="Take Away" OnClick="ButtonTakeAway_Click" /></td>
                                    </tr>
                                    <asp:Repeater ID="RepeaterReguler" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <asp:Repeater ID="RepeaterMeja" runat="server" OnItemCommand="RepeaterMeja_ItemCommand" DataSource='<%# Eval("baris") %>'>
                                                    <ItemTemplate>
                                                        <td style="padding: 0px 2.5px 5px 2.5px; width:10%">
                                                            <asp:Button
                                                                ID="ButtonMeja"
                                                                runat="server"
                                                                CssClass='<%# Eval("Warna") + " btn btn-default btn-lg btn-block" + ((bool)Eval("Status") ? "" : " btn btn-default btn-lg btn-block invisible") %>'
                                                                Text='<%# Eval("Nama") %>'
                                                                CommandName="Pilih"
                                                                CommandArgument='<%# Eval("IDMeja") %>'
                                                                Style="font-weight: bold;" />
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <br />
                            <div class="table-responsive">
                                <table style="background-color: rgb(248, 248, 248); width: 100%;">
                                    <asp:Repeater ID="RepeaterVIP" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <asp:Repeater ID="RepeaterMeja" runat="server" OnItemCommand="RepeaterMeja_ItemCommand" DataSource='<%# Eval("baris") %>'>
                                                    <ItemTemplate>
                                                        <td style="padding: 0px 2.5px 5px 2.5px; width:20%">
                                                            <asp:Button
                                                                ID="ButtonMeja"
                                                                runat="server"
                                                                CssClass='<%# Eval("Warna") + " btn btn-default btn-lg btn-block" + ((bool)Eval("Status") ? "" : " btn btn-default btn-lg btn-block invisible") %>'
                                                                Text='<%# Eval("Nama") %>'
                                                                CommandName="Pilih"
                                                                CommandArgument='<%# Eval("IDMeja") %>'
                                                                Style="font-weight: bold;" />
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2">
                    <div style="height: 454px; overflow-y: scroll;">
                        <asp:Repeater ID="RepeaterTransaksi" runat="server" OnItemCommand="RepeaterTransaksi_ItemCommand">
                            <ItemTemplate>
                                <asp:Button
                                    ID="ButtonTransaksi"
                                    runat="server"
                                    CssClass="btn btn-default btn-lg btn-block"
                                    Text='<%# Eval("Keterangan") %>'
                                    CommandName="Pilih"
                                    CommandArgument='<%# Eval("IDTransaksi") %>'
                                    Style="margin: 0px 0px 5px 0px; font-weight: bold; white-space: pre-wrap; height: 71px; font-size: 14px;" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <asp:MultiView ID="MultiViewPrint" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="row visible-print" style="font-size: 10pt; line-height: 12px;">
                        <table style="width:240px">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="2" class="text-center">
                                                <img src="/images/blank.jpg" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="text-center">
                                                <asp:Label ID="LabelPrintOrderCheckStoreTempat" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="text-center">Order #<asp:Label ID="LabelPrintOrderCheckIDOrder" runat="server"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">Table :
                                <asp:Label ID="LabelPrintOrderCheckTable" runat="server"></asp:Label>
                                            </td>
                                            <td class="text-right">Order Check</td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="LabelPrintOrderCheckPengguna" runat="server"></asp:Label></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelPrintOrderCheckTanggal" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" class="text-center">------------------------------------------------------</td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterPrintOrderCheck" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-right"><%# Eval("JumlahProduk") %>x &nbsp;</td>
                                                    <td><%# Eval("Produk") %></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td><%# Eval("Keterangan") %></td>
                                                </tr>
                                                <tr runat="server" visible='<%# Eval("StatusKeterangan") %>'>
                                                    <td colspan="2">&nbsp;</td>
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
                                                <asp:Label ID="LabelPrintOrderCheckQuantity" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="PanelKeteranganOrderCheck" runat="server">
                                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                                        </tr>
                                        <tr id="PanelKeteranganOrderCheck1" runat="server">
                                            <td colspan="3" class="text-center">
                                                <asp:Label ID="LabelPrintOrderCheckKeterangan" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="width: 100%;">
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
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="row visible-print" style="font-size: 10pt; line-height: 12px;">
                        <table>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" class="text-center">
                                                <img src="/images/logo.jpg" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="text-center">
                                                <asp:Label ID="LabelPrintStoreTempat" runat="server"></asp:Label>
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
                                            <td class="text-left">Table :
                                <asp:Label ID="LabelPrintTable" runat="server"></asp:Label></td>
                                            <td class="text-right">Pre Settlement</td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="LabelPrintPengguna" runat="server"></asp:Label></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelPrintTanggal" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>

                                    <div runat="server" id="PanelPelanggan">
                                        <tr>
                                            <td colspan="3" class="text-center">------------------------------------------------------</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="vertical-align: top; font-weight: bold;">ID</td>
                                                        <td style="vertical-align: top;">:</td>
                                                        <td>
                                                            <asp:Label ID="LabelPrintIDPelanggan" runat="server"></asp:Label></td>
                                                    </tr>
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
                                                    <td class="text-right"><%# Eval("JumlahProduk") %>x &nbsp;</td>
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
                </asp:View>
            </asp:MultiView>

            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderMessage" runat="server" PopupControlID="Message" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <div id="Message" class="text-center hidden-print">
                <div class="col-lg-12">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h3 class="modal-title text-left" style="font-weight: bold;">MESSAGE
                                        </h3>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="ButtonKeluarMessage" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body center">
                                <div class="form-group">
                                    <asp:TextBox ID="TextBoxPesan" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Button ID="ButtonKirimSemua" CssClass="btn btn-lg btn-success btn-block" runat="server" Text="Kirim Semua" OnClick="ButtonKirimSemua_Click" />
                                        </div>
                                        <asp:Repeater ID="RepeaterPrinter" runat="server" OnItemCommand="RepeaterPrinter_ItemCommand">
                                            <ItemTemplate>
                                                <div class="col-md-4">
                                                    <asp:Button ID="ButtonKirim" CssClass="btn btn-lg btn-info btn-block" runat="server" Text='<%# Eval("Kategori") %>' CommandName='Kirim' CommandArgument='<%# Eval("IDKonfigurasiPrinter") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderWaktuPelanggan" runat="server" PopupControlID="WaktuPelanggan" TargetControlID="LinkButton2" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <div id="WaktuPelanggan" class="text-center hidden-print">
                <div class="col-lg-12">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h3 class="modal-title text-left" style="font-weight: bold;">WAKTU
                                        </h3>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="ButtonKeluarWaktuPelanggan" ClientIDMode="Static" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body" style="max-height: 550px; min-height: 550px;">
                                <div style="overflow-y: scroll; max-height: 520px; min-height: 520px;">
                                    <div class="table-responsive">
                                        <table class="table table-hover">
                                            <thead>
                                                <tr class="active">
                                                    <th>No.</th>
                                                    <th class="text-center">Meja</th>
                                                    <th class="text-center">Transaksi</th>
                                                    <th class="text-center">Keterangan</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterWaktuPelanggan" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="odd">
                                                            <td><%# Container.ItemIndex + 1 %></td>
                                                            <td class="NoWrap"><%# Eval("Meja") %></td>
                                                            <td class="NoWrap"><strong><%# Eval("IDTransaksi") %></strong></td>
                                                            <td style="word-break: break-all !important;"><%# Eval("Keterangan") %></td>
                                                            <td class="text-right NoWrap warning"><%# Eval("Waktu") %></td>
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

            <asp:LinkButton ID="LinkButton3" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderLihatPesanan" runat="server" PopupControlID="LihatPesanan" TargetControlID="LinkButton3" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <div id="LihatPesanan" class="hidden-print">
                <div class="col-lg-12">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h3 class="modal-title text-left" style="font-weight: bold;">MEJA 
                                            <asp:Label ID="LabelLihatPesananMeja" runat="server"></asp:Label>
                                            - #<asp:Label ID="LabelLihatPesananIDTransaksi" runat="server"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="ButtonKeluarLihatPesanan" ClientIDMode="Static" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body" style="max-height: 550px; min-height: 550px;">
                                <div class="col-lg-6">
                                    <table class="table table-condensed table-hover text-left">
                                        <tr>
                                            <td><b>Pengguna</b></td>
                                            <td>
                                                <asp:Label ID="LabelLihatPesananPengguna" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Tanggal</b></td>
                                            <td>
                                                <asp:Label ID="LabelLihatPesananTanggal" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Quantity</b></td>
                                            <td>
                                                <asp:Label ID="LabelLihatPesananQuantity" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Pelanggan</b></td>
                                            <td>
                                                <asp:Label ID="LabelLihatPesananPelanggan" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Waktu</b></td>
                                            <td>
                                                <asp:Label ID="LabelLihatPesananWaktuPelanggan" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Keterangan</b></td>
                                            <td style="word-break: break-all !important;">
                                                <asp:Label ID="LabelLihatPesananKeterangan" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-6">
                                    <div style="overflow-y: scroll; max-height: 510px; min-height: 510px;">
                                        <div class="table-responsive">
                                            <table class="table table-hover">
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterLihatPesananDetail" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="info">
                                                                <td class="text-right"><b><%# Eval("JumlahProduk") %></b></td>
                                                                <td class="text-left">
                                                                    <b><%# Eval("Produk") %></b>
                                                                    <br />
                                                                    <%# Eval("Keterangan") %>
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
                        </div>
                    </div>
                </div>
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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

