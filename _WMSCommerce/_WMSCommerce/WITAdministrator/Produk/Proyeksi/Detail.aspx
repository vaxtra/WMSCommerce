<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_Produk_Proyeksi_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Detail Proyeksi
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
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#tabBahanBaku" id="Data-tab" data-toggle="tab">Data</a></li>
                <li><a href="#tabPOProduksiBahanBaku" id="BahanBaku-tab" data-toggle="tab">Bahan Baku</a></li>
                <li><a href="#tabPOProduksiProduk" id="Produk-tab" data-toggle="tab">Produk</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabBahanBaku">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="form-label">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawai" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="form-label bold">Tanggal Proyeksi</label>
                                            <asp:TextBox ID="TextBoxTanggalProyeksi" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="form-label bold">Tanggal Target</label>
                                            <asp:TextBox ID="TextBoxTanggalTarget" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label bold">Keterangan</label>
                                <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr class="warning">
                                        <th colspan="7" style="vertical-align: middle;">
                                            <label class="form-label bold">Produk</label>
                                        </th>
                                        <th style="vertical-align: middle;">
                                            <a id="ButtonProduk" runat="server" aria-expanded="false" class="btn btn-success btn-sm dropdown-toggle btn-demo-space pull-right" data-toggle="dropdown" href="#" style="width: 180px;">Proses <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li>
                                                    <asp:LinkButton ID="LinkButtonPurchase" runat="server" CommandName="purchase" OnClick="LinkButtonPurchase_Click">PURCHASE ORDER</asp:LinkButton></li>
                                                <li>
                                                    <asp:LinkButton ID="LinkButtonProduksi" runat="server" CommandName="produksi" OnClick="LinkButtonProduksi_Click">IN-HOUSE PRODUCTION</asp:LinkButton></li>
                                                <li>
                                                    <asp:LinkButton ID="LinkButtonVendor" runat="server" CommandName="vendor" OnClick="LinkButtonVendor_Click">PRODUCTION TO VENDOR</asp:LinkButton></li>
                                            </ul>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th class="text-center" style="width: 2%;">No</th>
                                        <th class="text-center" style="width: 10%;">Kode</th>
                                        <th class="text-center" style="width: 10%;">Brand</th>
                                        <th class="text-center">Produk</th>
                                        <th class="text-center" style="width: 5%;">Varian</th>
                                        <th class="text-center" style="width: 10%;">Kategori</th>
                                        <th class="text-center" style="width: 10%;">Jumlah</th>
                                        <th class="text-center" style="width: 10%;">Belum PO</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterProduk" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("KodeKombinasiProduk") %></td>
                                                <td><%# Eval("PemilikProduk") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td><%# Eval("AtributProduk") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                <td class="text-right"><%# Eval("Sisa").ToFormatHargaBulat() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="PanelKomposisi" runat="server" Visible="false">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered">
                                            <thead>
                                                <tr class="warning">
                                                    <th colspan="4" style="vertical-align: middle;">
                                                        <label class="form-label bold">Bahan Baku Dasar</label>
                                                    </th>
                                                    <th style="vertical-align: middle;">
                                                        <asp:LinkButton ID="LinkButtonPurchaseOrder" runat="server" class="btn btn-success btn-sm pull-right" Style="width: 180px;" OnClick="LinkButtonPurchaseOrder_Click">Purchase Order</asp:LinkButton>
                                                </tr>
                                                <tr>
                                                    <th class="text-center" style="width: 2%;">No</th>
                                                    <th class="text-center" style="width: 15%;">Kategori</th>
                                                    <th class="text-center">Bahan Baku</th>
                                                    <th class="text-center" style="width: 5%;">Satuan</th>
                                                    <th class="text-center" style="width: 10%;">Butuh</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterBahanBakuDasar" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Kategori") %></td>
                                                            <td><%# Eval("BahanBaku") %></td>
                                                            <td><%# Eval("Satuan") %></td>
                                                            <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered">
                                            <asp:Repeater ID="RepeaterKomposisi" runat="server" OnItemCommand="RepeaterKomposisi_ItemCommand">
                                                <ItemTemplate>
                                                    <thead>
                                                        <tr class="warning">
                                                            <th colspan="4" style="vertical-align: middle">
                                                                <asp:Label ID="LabelLevelBahanBaku" runat="server" class="form-label bold" Text='<%# "Produksi Bahan Baku Level " + Eval("LevelProduksi") %>'></asp:Label></th>
                                                            <th style="vertical-align: middle">
                                                                <a id="ButtonProduksi" runat="server" aria-expanded="false" class="btn btn-success btn-sm dropdown-toggle btn-demo-space pull-right" style="width: 180px;" data-toggle="dropdown" href="#" visible='<%# Eval("StatusButton") %>'>Proses <span class="caret"></span></a>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:LinkButton ID="LinkButtonProduksi" runat="server" CommandArgument='<%# Eval("LevelProduksi") %>' CommandName="produksi">In-House Production</asp:LinkButton></li>
                                                                    <li>
                                                                        <asp:LinkButton ID="LinkButtonSupplier" runat="server" CommandArgument='<%# Eval("LevelProduksi") %>' CommandName="supplier">Production to Supplier</asp:LinkButton></li>
                                                                </ul>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center" style="width: 2%;">No</th>
                                                            <th class="text-center" style="width: 15%;">Kategori</th>
                                                            <th class="text-center">Bahan Baku</th>
                                                            <th class="text-center" style="width: 5%;">Satuan</th>
                                                            <th class="text-center" style="width: 10%;">Jumlah</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterSubBahanBaku" runat="server" DataSource='<%# Eval("SubData") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Kategori") %></td>
                                                                    <td><%# Eval("BahanBaku") %></td>
                                                                    <td><%# Eval("Satuan") %></td>
                                                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="tab-pane" id="tabPOProduksiBahanBaku">
                    <div class="form-group">
                        <label class="form-label bold">Produksi Bahan Baku</label>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center" style="width: 16%">ID</th>
                                        <th class="text-center" style="width: 16%">Jenis</th>
                                        <th class="text-center">Tanggal</th>
                                        <th class="text-center">Pegawai</th>
                                        <th class="text-center" style="width: 10%">Supplier</th>
                                        <th class="text-center" style="width: 10%">Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPOProduksiBahanBaku" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("IDPOProduksiBahanBaku") %></td>
                                                <td><%# Eval("Jenis") %></td>
                                                <td><%# Eval("TanggalPending").ToFormatTanggal() %></td>
                                                <td><%# Eval("Pegawai") %></td>
                                                <td class="text-center"><%# Eval("Supplier") %></td>
                                                <td class="text-right"><%# Eval("Grandtotal").ToFormatHarga() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="tabPOProduksiProduk">
                    <div class="form-group">
                        <label class="form-label bold">Produksi Produk</label>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center" style="width: 16%">ID</th>
                                        <th class="text-center" style="width: 16%">Jenis</th>
                                        <th class="text-center">Tanggal</th>
                                        <th class="text-center">Pegawai</th>
                                        <th class="text-center" style="width: 10%">Vendor</th>
                                        <th class="text-center" style="width: 10%">Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPOProduksiProduk" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("IDPOProduksiProduk") %></td>
                                                <td><%# Eval("Jenis") %></td>
                                                <td><%# Eval("TanggalPending").ToFormatTanggal() %></td>
                                                <td><%# Eval("Pegawai") %></td>
                                                <td class="text-center"><%# Eval("Vendor") %></td>
                                                <td class="text-right"><%# Eval("Grandtotal").ToFormatHarga() %></td>
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
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

