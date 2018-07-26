<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_DownPayment_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Down Payment Purchase Order Product
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
        <div class="card-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <h3 class="border-bottom">STATUS</h3>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Proyeksi</label>
                                    <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">ID Purchase Order</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiBahanBaku" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">PIC</label>
                                    <asp:TextBox ID="TextBoxPegawaiPIC" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Status HPP</label>
                                    <asp:TextBox ID="TextBoxStatusHPP" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Pembuat</label>
                                    <asp:TextBox ID="TextBoxPembuat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="font-weight-bold text-muted">Tanggal Jatuh Tempo</label>
                                    <asp:TextBox ID="TextBoxTanggalJatuhTempo" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="font-weight-bold text-muted">Tanggal Pengiriman</label>
                                    <asp:TextBox ID="TextBoxTanggalPengiriman" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h3 class="border-bottom">SUPPLIER</h3>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Supplier</label>
                                    <asp:TextBox ID="TextBoxSupplier" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Email</label>
                                    <asp:TextBox ID="TextBoxEmail" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Alamat</label>
                            <asp:TextBox ID="TextBoxAlamat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Telepon 1</label>
                                    <asp:TextBox ID="TextBoxTelepon1" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Telepon 2</label>
                                    <asp:TextBox ID="TextBoxTelepon2" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h3 class="border-bottom">DETAIL</h3>
            <div class="form-group">
                <label class="font-weight-bold text-muted">Bahan Baku</label>
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered mb-0">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>Kode</th>
                                <th>Bahan Baku</th>
                                <th>Satuan</th>
                                <th>Harga</th>
                                <th class="d-none">Potongan</th>
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
                                        <td class="fitSize"><%# Eval("TBBahanBaku.KodeBahanBaku") %></td>
                                        <td><%# Eval("TBBahanBaku.Nama") %></td>
                                        <td class="fitSize"><%# Eval("TBSatuan.Nama") %></td>
                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                        <td class="text-right d-none"><%# Eval("PotonganHargaSupplier").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("HargaSupplier").ToFormatHarga() %></td>
                                        <td class="text-right"><strong><%# Eval("SubtotalHargaSupplier").ToFormatHarga() %></strong></td>
                                        <td class="text-right table-danger"><strong><%# Eval("Sisa").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="table-success">
                                <td colspan="6" class="text-center font-weight-bold">TOTAL</td>
                                <td class="text-right font-weight-bold">
                                    <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></td>
                                <td class="text-right font-weight-bold">
                                    <asp:Label ID="LabelTotalSisa" runat="server" Text="0"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3 d-none">
                        <label class="font-weight-bold text-muted">Biaya lain-lain</label>
                        <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-3 d-none">
                        <label class="font-weight-bold text-muted">Potongan</label>
                        <asp:TextBox ID="TextBoxPotonganPO" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-3 d-none">
                        <label class="font-weight-bold text-muted">
                            <asp:Label ID="LabelTax" runat="server" Text="Tax (0%)"></asp:Label></label>
                        <asp:TextBox ID="TextBoxTax" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="font-weight-bold text-muted">Grandtotal</label>
                        <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="font-weight-bold text-muted">Keterangan</label>
                <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" Enabled="false" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">
                        <label class="font-weight-bold text-muted">Pembayar</label>
                        <asp:TextBox ID="TextBoxPembayar" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="font-weight-bold text-muted">Tanggal DP</label>
                        <asp:TextBox ID="TextBoxTanggalDP" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="font-weight-bold text-muted">Jenis Pembayaran</label>
                        <asp:TextBox ID="TextBoxJenisPembayaran" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="font-weight-bold text-muted">Down Payment</label>
                        <asp:TextBox ID="TextBoxDownPayment" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

