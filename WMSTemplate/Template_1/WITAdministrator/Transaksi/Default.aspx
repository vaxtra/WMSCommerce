<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Transaksi_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) {
                newwindow.focus()
            }
            return false;
        }

        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCari');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Transaksi
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
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiViewTransaksi" runat="server">
                <asp:View ID="ViewTransaksi" runat="server">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="form-group">
                                        <asp:CheckBoxList ID="CheckBoxListStatusTransaksi" runat="server" CssClass="checkboxlist">
                                            <asp:ListItem Text="Pending Shipping Cost" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Awaiting Payment" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Payment Verification" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Payment Verified" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Complete" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Canceled" Value="6"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="form-group">
                                        <label class="text-muted font-weight-bold">Dari</label>
                                        <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label class="text-muted font-weight-bold">Sampai</label>
                                        <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button CssClass="btn btn-primary btn-block" ID="ButtonCariTanggal" runat="server" Text="Cari" ClientIDMode="Static" OnClick="ButtonCariTanggal_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-9">
                            <div class="card">
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>ID</th>
                                                    <th>Tanggal</th>
                                                    <th>Jenis</th>
                                                    <th>Status</th>
                                                    <th>Pelanggan</th>
                                                    <th>Jumlah</th>
                                                    <th>Grandtotal</th>
                                                </tr>
                                                <tr class="thead-light">
                                                    <th colspan="11">
                                                        <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block d-none" OnClick="LoadData_Event" ClientIDMode="Static" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterTransaksi" runat="server" OnItemCommand="RepeaterTransaksi_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="ButtonDetail" CssClass="btn btn-link btn-xs" runat="server" Text='<%# Eval("IDTransaksi") %>' CommandName="Detail" CommandArgument='<%# Eval("IDTransaksi") %>' /></td>
                                                            <td class="fitSize"><%# Eval("TanggalOperasional") %></td>
                                                            <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                                            <td class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                                            <td class="fitSize"><%# Eval("Pelanggan") %></td>
                                                            <td class="text-right fitSize"><%# Eval("JumlahProduk") %></td>
                                                            <td class="text-right fitSize font-weight-bold"><%# Eval("GrandTotal") %></td>
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
                </asp:View>
                <asp:View ID="ViewDetail" runat="server">
                    <div class="card">
                        <div class="card-header pb-0">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group text-center">
                                        <asp:Label ID="LabelStatusTransaksi" runat="server" CssClass="w-100" Font-Size="X-Large"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6 col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="ButtonPrint2" runat="server" Text="Print Invoice" CssClass="btn btn-secondary btn-block" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="ButtonPrint3" runat="server" Text="Print Packing Slip" CssClass="btn btn-secondary btn-block" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered">
                                            <tr>
                                                <td class="text-center" colspan="2" style="font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="LabelJenisTransaksi" runat="server"></asp:Label>
                                                    #<asp:Label ID="LabelIDTransaksi" runat="server"></asp:Label></td>
                                            </tr>
                                            <asp:Panel ID="PanelPelanggan2" runat="server">
                                                <tr>
                                                    <td colspan="2" class="text-center table-info" style="font-weight: bold;">PELANGGAN</td>
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
                                                <td colspan="2" class="text-center table-info" style="font-weight: bold;">TRANSAKSI</td>
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
                                        <h3 class="text-info border-bottom">PRODUK</h3>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered">
                                                <thead>
                                                    <tr class="thead-light">
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
                                                    <tr class="text-right table-warning" style="font-weight: bold;">
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
                                                    <tr class="table-success text-right" style="font-weight: bold;" id="PanelSetelahDiscount" runat="server">
                                                        <td colspan="5">Setelah Discount</td>
                                                        <td>
                                                            <asp:Label ID="LabelDiscountSetelah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div id="TabelPembayaran" runat="server" class="form-group">
                                        <h3 class="text-info border-bottom">PEMBAYARAN</h3>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered">
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
                                                <tr class="text-right table-success" style="font-weight: bold;">
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
                        <div class="card-footer">
                            <a id="ButtonStatus" runat="server" class="btn btn-success btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Proses</a>
                            <div class="dropdown-menu p-2" aria-labelledby="ButtonProduk">
                                <asp:Button CssClass="btn btn-light btn-block" ID="ButtonStatusPendingShippingCost" runat="server" Text="Pending" CommandName="Pending Shipping Cost" OnClick="ButtonStatusPendingShippingCost_Click" />
                                <asp:Button CssClass="btn btn-light btn-block" ID="ButtonAwaitingPayment" runat="server" Text="Awaiting Payment" CommandName="Awaiting Payment" OnClick="ButtonAwaitingPayment_Click" />
                                <asp:Button CssClass="btn btn-light btn-block" ID="ButtonAwaitingPaymentVerification" runat="server" Text="Payment Verification" CommandName="Awaiting Payment Verification" OnClick="ButtonAwaitingPaymentVerification_Click" />
                                <asp:Button CssClass="btn btn-light btn-block" ID="ButtonPaymentVerified" runat="server" Text="Payment Verified" CommandName="Payment Verified" OnClick="ButtonPaymentVerified_Click" />
                                <asp:Button CssClass="btn btn-light btn-block" ID="ButtonComplete" runat="server" Text="Complete" CommandName="Complete" OnClick="ButtonComplete_Click" />
                                <asp:Button CssClass="btn btn-light btn-block" ID="ButtonCanceled" runat="server" Text="Canceled" CommandName="Canceled" OnClick="ButtonCanceled_Click" />
                            </div>
                            <asp:Button ID="ButtonKembali" runat="server" CssClass="btn btn-danger btn-const" Text="Kembali" OnClick="ButtonKembali_Click" />
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

            <asp:UpdateProgress ID="updateProgressPage" runat="server" AssociatedUpdatePanelID="UpdatePanelPage">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressPage" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

