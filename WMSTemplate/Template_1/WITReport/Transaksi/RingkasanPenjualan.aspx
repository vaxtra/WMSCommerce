<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="RingkasanPenjualan.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_RingkasanPenjualan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Summary Sales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <input id="ButtonPrint" type="button" value="Cetak" class="btn btn-secondary btn-const" onclick="window.print();" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                <div class="row">
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListTempat_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListStatusTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStatusTransaksi_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <h4 class="text-uppercase mb-3"><asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="card">
                                <h5 class="card-header bg-gradient-green">SALES</h5>
                                <div class="table-responsive">
                                    <table class="table table-sm table-bordered table-hover mb-0">
                                        <tr>
                                            <td>Penjualan Item</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalPenjualanItem1" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Disc. Item</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelDiscountItemCustomer" Style="color: red;" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Disc. Transaksi</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelDiscountCustomer" Style="color: red;" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Disc. Item Member</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelDiscountItemMember" Style="color: red;" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Disc. Transaksi Member</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelDiscountMember" Style="color: red;" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="table-warning">
                                            <td><b>SUBTOTAL</b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalPenjualan" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelKeteranganBiayaTambahan1" runat="server"></asp:Label></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelBiayaTambahan1" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelKeteranganBiayaTambahan2" runat="server"></asp:Label></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelBiayaTambahan2" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Pembulatan</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelPembulatan" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Biaya Pengiriman</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="table-success">
                                            <td><b>GRAND TOTAL</b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="card">
                                <h5 class="card-header bg-gradient-green">JENIS PEMBAYARAN</h5>
                                <div class="table-responsive">
                                    <table class="table table-sm table-bordered table-hover mb-0">
                                        <asp:Repeater ID="RepeaterJenisPembayaran" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Nama") %></td>
                                                    <td class="text-right"><%# Eval("Total").ToFormatHarga() %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-warning">
                                            <td><b>SUBTOTAL</b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalNonCash" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td>CASH</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalCash" runat="server"></asp:Label></td>
                                        </tr>

                                        <tr class="table-success">
                                            <td><b>GRAND TOTAL</b></td>
                                            <td class="text-right"><b>
                                                <asp:Label ID="LabelTotalPembayaran" runat="server"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="card">
                                <h5 class="card-header bg-gradient-blue">AVERAGE</h5>
                                <div class="table-responsive">
                                    <table class="table table-sm table-bordered table-hover mb-0">
                                        <tr>
                                            <td>Produk</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalQuantity1" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="table-warning">
                                            <td><strong>Rata-Rata Produk</strong></td>
                                            <td class="text-right"><strong>
                                                <asp:Label ID="LabelRataRataItem" runat="server"></asp:Label></strong></td>
                                        </tr>
                                        <tr>
                                            <td>Transaksi</td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalTransaksi" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="table-warning">
                                            <td><strong>Rata-Rata Transaksi</strong></td>
                                            <td class="text-right"><strong>
                                                <asp:Label ID="LabelRataRataTransaksi" runat="server"></asp:Label></strong></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="card">
                                <h5 class="card-header bg-gradient-blue">KATEGORI</h5>
                                <div class="table-responsive">
                                    <table class="table table-sm table-bordered table-hover mb-0">
                                        <tr class="active">
                                            <th>No.</th>
                                            <th>Kategori</th>
                                            <th>Quantity</th>
                                            <th>Sales</th>
                                        </tr>
                                        <asp:Repeater ID="RepeaterPenjualanItem" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><strong><%# Eval("Nama") %></strong></td>
                                                    <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                                    <td class="text-right"><%# Eval("Penjualan").ToFormatHarga() %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-success">
                                            <td colspan="2"><strong>TOTAL</strong></td>
                                            <td class="text-right"><strong>
                                                <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label></strong></td>
                                            <td class="text-right"><strong>
                                                <asp:Label ID="LabelTotalPenjualanItem" runat="server"></asp:Label></strong></td>
                                        </tr>
                                    </table>
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

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

