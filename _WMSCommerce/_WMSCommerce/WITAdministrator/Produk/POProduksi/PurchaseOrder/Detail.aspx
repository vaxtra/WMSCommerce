﻿<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_PurchaseOrder_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Purchase Order Product Detail
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonEdit" runat="server" Text="Edit" CssClass="btn btn-primary btn-sm" OnClick="ButtonEdit_Click" OnClientClick="if (!confirm('Are you sure to edit this data?')) return false;" />
    <a href="Default.aspx" class="btn btn-danger btn-sm">Kembali</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ul id="myTab" class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#tabData" id="data-tab" data-toggle="tab">Data</a></li>
                <li><a href="#tabPenerimaan" id="penerimaan-tab" data-toggle="tab">Penerimaan</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="tabData">
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
                                        <strong>Vendor</strong>
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
                                        <tr>
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
                                                    <td class="fitSize"><a href='/WITAdministrator/Produk/Barcode.aspx?id=<%# Eval("IDKombinasiProduk") %>' target="_blank"><%# Eval("TBKombinasiProduk.KodeKombinasiProduk") %></a></td>
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
                                        <tr class="success bold">
                                            <td colspan="6" class="text-center"><b>TOTAL</b></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalJumlah" runat="server" Text="0"></asp:Label></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalSisa" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3 hidden">
                                <label class="form-label bold">Biaya lain-lain</label>
                                <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                            </div>
                            <div class="col-md-3 hidden">
                                <label class="form-label bold">Potongan</label>
                                <asp:TextBox ID="TextBoxPotonganPO" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                            </div>
                            <div class="col-md-3 hidden">
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
                </div>
                <div role="tabpanel" class="tab-pane" id="tabPenerimaan">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <strong>Penerimaan</strong>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Tanggal</th>
                                        <th>Pegawai</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPenerimaan" runat="server" OnItemCommand="RepeaterPenerimaan_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("IDPenerimaanPOProduksiProduk") %></td>
                                                <td><%# Eval("TanggalDatang").ToFormatTanggal() %></td>
                                                <td><%# Eval("Pegawai") %></td>
                                                <td class="text-center fitSize">
                                                    <asp:Button ID="ButtonDetail" runat="server" class="btn btn-info btn-xs" Text="Detail" CommandName="Detail" CommandArgument='<%# Eval("IDPenerimaanPOProduksiProduk") %>' />
                                                    <asp:Button ID="ButtonCetak" runat="server" class="btn btn-default btn-xs" Text="Cetak" CommandName="Cetak" CommandArgument='<%# Eval("IDPenerimaanPOProduksiProduk") %>' OnClientClick='<%# Eval("Cetak") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanelPenerimaanDetail" runat="server">
                        <ContentTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <strong>Detail Penerimaan
                            <asp:Label ID="LabelIDPenerimaan" runat="server"></asp:Label></label></strong>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>No</th>
                                                    <th>Produk</th>
                                                    <th>Varian</th>
                                                    <th>Datang</th>
                                                    <th>Diterima</th>
                                                    <th>Tolak Vendor</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterPenerimaanDetail" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Produk") %></td>
                                                            <td class="fitSize"><%# Eval("AtributProduk") %></td>
                                                            <td class="text-right"><%# Eval("Datang").ToFormatHargaBulat() %></td>
                                                            <td class="text-right"><%# Eval("Diterima").ToFormatHargaBulat() %></td>
                                                            <td class="text-right"><%# Eval("TolakKeVendor").ToFormatHargaBulat() %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="form-group">
                                        <label class="form-label bold">Keterangan</label>
                                        <asp:TextBox ID="TextBoxKeteranganPenerimaan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RepeaterPenerimaan" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

