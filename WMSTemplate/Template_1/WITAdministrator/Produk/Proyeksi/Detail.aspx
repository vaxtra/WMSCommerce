<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_Produk_Proyeksi_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Detail Proyeksi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="card">
        <div class="card-header">
            <ul id="myTab" class="nav nav-tabs card-header-tabs">
                <li class="nav-item"><a href="#tabBahanBaku" id="Data-tab" class="nav-link active font-weight-normal" data-toggle="tab">Data</a></li>
                <li class="nav-item"><a href="#tabPOProduksiBahanBaku" id="BahanBaku-tab" class="nav-link font-weight-normal" data-toggle="tab">Bahan Baku</a></li>
                <li class="nav-item"><a href="#tabPOProduksiProduk" id="Produk-tab" class="nav-link font-weight-normal" data-toggle="tab">Produk</a></li>
            </ul>
        </div>
        <div class="card-body">
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabBahanBaku">
                    <h3 class="border-bottom">PIC</h3>
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
                        </div>
                    </div>
                    <h3 class="border-bottom">DETAIL</h3>
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th colspan="7" style="vertical-align: middle;">
                                            <label class="form-label bold">Produk</label>
                                        </th>
                                        <th class="dropdown fitSize">
                                            <a id="ButtonProduk" runat="server" class="btn btn-primary btn-block dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Proses</a>
                                            <div class="dropdown-menu" aria-labelledby="ButtonProduk">
                                                <asp:LinkButton ID="LinkButtonPurchase" runat="server" class="dropdown-item py-0" CommandName="purchase" OnClick="LinkButtonPurchase_Click">PURCHASE ORDER</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonProduksi" runat="server" class="dropdown-item py-0" CommandName="produksi" OnClick="LinkButtonProduksi_Click">IN-HOUSE PRODUCTION</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButtonVendor" runat="server" class="dropdown-item py-0" CommandName="vendor" OnClick="LinkButtonVendor_Click">PRODUCTION TO VENDOR</asp:LinkButton>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th class="text-center">No</th>
                                        <th class="text-center">Kode</th>
                                        <th class="text-center">Brand</th>
                                        <th class="text-center">Produk</th>
                                        <th class="text-center">Varian</th>
                                        <th class="text-center">Kategori</th>
                                        <th class="text-center">Jumlah</th>
                                        <th class="text-center">Belum PO</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterProduk" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
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
                                        <table class="table table-sm table-hover table-bordered">
                                            <thead>
                                                <tr class="table-warning">
                                                    <th colspan="4" style="vertical-align: middle;">
                                                        <label class="form-label bold">Bahan Baku Dasar</label>
                                                    </th>
                                                    <th>
                                                        <asp:LinkButton ID="LinkButtonPurchaseOrder" runat="server" class="btn btn-primary btn-block" OnClick="LinkButtonPurchaseOrder_Click">PURCHASE</asp:LinkButton>
                                                </tr>
                                                <tr class="thead-light">
                                                    <th class="text-center">No</th>
                                                    <th class="text-center">Kategori</th>
                                                    <th class="text-center">Bahan Baku</th>
                                                    <th class="text-center">Satuan</th>
                                                    <th class="text-center">Butuh</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterBahanBakuDasar" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
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
                                        <table class="table table-sm table-hover table-bordered">
                                            <asp:Repeater ID="RepeaterKomposisi" runat="server" OnItemCommand="RepeaterKomposisi_ItemCommand">
                                                <ItemTemplate>
                                                    <thead>
                                                        <tr class="table-warning">
                                                            <th colspan="4" style="vertical-align: middle">
                                                                <asp:Label ID="LabelLevelBahanBaku" runat="server" class="form-label bold" Text='<%# "Produksi Bahan Baku Level " + Eval("LevelProduksi") %>'></asp:Label></th>
                                                            <th class="dropdown">
                                                                <a id="ButtonProduksi" runat="server" class="btn btn-primary btn-block dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">PROSES</a>
                                                                <div class="dropdown-menu" aria-labelledby="ButtonProduksi">
                                                                    <asp:LinkButton ID="LinkButtonProduksi" runat="server" class="dropdown-item py-0" CommandArgument='<%# Eval("LevelProduksi") %>' CommandName="produksi">IN-HOUSE PRODUCTION</asp:LinkButton>
                                                                    <asp:LinkButton ID="LinkButtonSupplier" runat="server" class="dropdown-item py-0" CommandArgument='<%# Eval("LevelProduksi") %>' CommandName="supplier">PROUCTION TO SUPPLIER</asp:LinkButton>
                                                                </div>
                                                            </th>
                                                        </tr>
                                                        <tr class="thead-light">
                                                            <th class="text-center">No</th>
                                                            <th class="text-center">Kategori</th>
                                                            <th class="text-center">Bahan Baku</th>
                                                            <th class="text-center">Satuan</th>
                                                            <th class="text-center">Jumlah</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterSubBahanBaku" runat="server" DataSource='<%# Eval("SubData") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
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
                    <div class="form-group">
                        <label class="form-label bold">Keterangan</label>
                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="tab-pane" id="tabPOProduksiBahanBaku">
                    <div class="form-group">
                        <label class="form-label bold">Produksi Bahan Baku</label>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th class="text-center">ID</th>
                                        <th class="text-center">Jenis</th>
                                        <th class="text-center">Tanggal</th>
                                        <th class="text-center">Pegawai</th>
                                        <th class="text-center">Supplier</th>
                                        <th class="text-center">Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPOProduksiBahanBaku" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
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
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th class="text-center">ID</th>
                                        <th class="text-center">Jenis</th>
                                        <th class="text-center">Tanggal</th>
                                        <th class="text-center">Pegawai</th>
                                        <th class="text-center">Vendor</th>
                                        <th class="text-center">Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPOProduksiProduk" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("IDPOProduksiProduk") %></td>
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

