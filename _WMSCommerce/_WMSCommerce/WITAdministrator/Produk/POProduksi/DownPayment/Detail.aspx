<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_DownPayment_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Down Payment Purchase Order Product
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Default.aspx" class="btn btn-danger btn-sm">Kembali</a>
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
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <strong>Purchase Order</strong>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">Proyeksi</label>
                                    <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">ID Purchase Order</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiProduk" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">PIC</label>
                                    <asp:TextBox ID="TextBoxPegawaiPIC" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Status HPP</label>
                                    <asp:TextBox ID="TextBoxStatusHPP" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">Pembuat</label>
                                    <asp:TextBox ID="TextBoxPembuat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label bold">Tanggal Jatuh Tempo</label>
                                    <asp:TextBox ID="TextBoxTanggalJatuhTempo" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label bold">Tanggal Pengiriman</label>
                                    <asp:TextBox ID="TextBoxTanggalPengiriman" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <strong>Supplier</strong>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">Vendor</label>
                                    <asp:TextBox ID="TextBoxVendor" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Email</label>
                                    <asp:TextBox ID="TextBoxEmail" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold">Alamat</label>
                            <asp:TextBox ID="TextBoxAlamat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">Telepon 1</label>
                                    <asp:TextBox ID="TextBoxTelepon1" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Telepon 2</label>
                                    <asp:TextBox ID="TextBoxTelepon2" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <strong>Produk</strong>
            </div>
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="active">
                            <th>No</th>
                            <th>Kode</th>
                            <th>Produk</th>
                            <th>Varian</th>
                            <th>Harga Vendor</th>
                            <th>Potongan</th>
                            <th>Jumlah</th>
                            <th>Subtotal</th>
                            <th>Sisa</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterDetail" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                    <td class="fitSize"><%# Eval("TBKombinasiProduk.KodeKombinasiProduk") %></td>
                                    <td><%# Eval("TBKombinasiProduk.TBProduk.Nama") %></td>
                                    <td class="fitSize"><%# Eval("TBKombinasiProduk.TBAtributProduk.Nama") %></td>
                                    <td class="text-right"><%# Eval("HargaVendor").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("PotonganHargaVendor").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                    <td class="text-right warning"><strong><%# Eval("SubtotalHargaVendor").ToFormatHarga() %></strong></td>
                                    <td class="text-right danger"><strong><%# Eval("Sisa").ToFormatHargaBulat() %></strong></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="success">
                            <td colspan="6" class="text-center"><b>TOTAL</b></td>
                            <td class="text-right"><strong>
                                <asp:Label ID="LabelTotalJumlah" runat="server" Text="0"></asp:Label></strong></td>
                            <td class="text-right"><strong>
                                <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></strong></td>
                            <td class="text-right"><strong>
                                <asp:Label ID="LabelTotalSisa" runat="server" Text="0"></asp:Label></strong></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-md-3">
                <label class="form-label bold">Biaya lain-lain</label>
                <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label bold">Potongan</label>
                <asp:TextBox ID="TextBoxPotonganPO" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label bold">
                    <asp:Label ID="LabelTax" runat="server" Text="Tax (0%)"></asp:Label></label>
                <asp:TextBox ID="TextBoxTax" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label bold">Grandtotal</label>
                <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="form-label bold">Keterangan</label>
        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" Enabled="false" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-md-3">
                <label class="form-label bold">Pembayar</label>
                <asp:TextBox ID="TextBoxPembayar" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label bold">Tanggal DP</label>
                <asp:TextBox ID="TextBoxTanggalDP" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label bold">Jenis Pembayaran</label>
                <asp:TextBox ID="TextBoxJenisPembayaran" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label bold">Down Payment</label>
                <asp:TextBox ID="TextBoxDownPayment" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

