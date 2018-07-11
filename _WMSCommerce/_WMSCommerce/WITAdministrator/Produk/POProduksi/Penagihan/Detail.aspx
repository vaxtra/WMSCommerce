<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Penagihan_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Invoice Purchase Order Product
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
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <strong>Invoice</strong>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <label class="form-label bold">ID</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiBahanBakuPenagihan" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold">Status</label>
                                    <asp:TextBox ID="TextBoxStatus" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
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
                        <strong>Detail</strong>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Penerimaan</label>
                                    <div class="table-responsive">
                                        <table class="table the-table table-bordered table-condensed table-hover">
                                            <thead>
                                                <tr class="active">
                                                    <th>No</th>
                                                    <th>ID</th>
                                                    <th>Tanggal</th>
                                                    <th>Grandtotal</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterDetail" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td>
                                                                <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPenerimaanPOProduksiProduk") %>'></asp:Label></td>
                                                            <td><%# Eval("TanggalTerima").ToFormatTanggal() %></td>
                                                            <td class="text-right warning"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="success bold">
                                                    <td colspan="3" class="text-center"><b>TOTAL</b></td>
                                                    <td class="text-right">
                                                        <asp:Label ID="LabelTotalPenerimaan" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Retur</label>
                                    <div class="table-responsive">
                                        <table class="table the-table table-bordered table-condensed table-hover">
                                            <thead>
                                                <tr class="active">
                                                    <th>No</th>
                                                    <th>ID</th>
                                                    <th>Tanggal</th>
                                                    <th>Grandtotal</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterRetur" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td>
                                                                <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPOProduksiProdukRetur") %>'></asp:Label></td>
                                                            <td><%# Eval("TanggalRetur").ToFormatTanggal() %></td>
                                                            <td class="text-right danger"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="success bold">
                                                    <td colspan="3" class="text-center"><b>TOTAL</b></td>
                                                    <td class="text-right">
                                                        <asp:Label ID="LabelTotalRetur" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Down Payment</label>
                                    <div class="table-responsive">
                                        <table class="table the-table table-bordered table-condensed table-hover">
                                            <thead>
                                                <tr class="active">
                                                    <th>No</th>
                                                    <th>ID</th>
                                                    <th>Tanggal</th>
                                                    <th>Down Payment</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterDownPayment" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td>
                                                                <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPOProduksiProduk") %>'></asp:Label></td>
                                                            <td><%# Eval("TanggalDownPayment").ToFormatTanggal() %></td>
                                                            <td class="text-right info"><strong><%# Eval("DownPayment").ToFormatHarga() %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="success bold">
                                                    <td colspan="3" class="text-center"><b>TOTAL</b></td>
                                                    <td class="text-right">
                                                        <asp:Label ID="LabelTotalDownPayment" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Pembayaran</label>
                                    <div class="table-responsive">
                                        <table class="table the-table table-bordered table-condensed table-hover">
                                            <thead>
                                                <tr class="active">
                                                    <th>No</th>
                                                    <th>Pegawai</th>
                                                    <th>Tanggal</th>
                                                    <th>Jenis Pembayaran</th>
                                                    <th>Bayar</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterPembayaran" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Pegawai") %></td>
                                                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                            <td><%# Eval("JenisPembayaran") %></td>
                                                            <td class="text-right"><strong><%# Eval("Bayar").ToFormatHarga() %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="success bold">
                                                    <td colspan="4" class="text-center"><b>TOTAL</b></td>
                                                    <td class="text-right">
                                                        <asp:Label ID="LabelTotalBayar" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Keterangan</label>
                                    <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

