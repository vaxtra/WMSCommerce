<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Retur_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Retur Produk Detail
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
                                    <label class="form-label bold">ID Retur</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiProdukRetur" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">ID Penerimaan</label>
                                    <asp:TextBox ID="TextBoxIDPenerimaanPOProduksiProduk" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Tanggal</label>
                                    <asp:TextBox ID="TextBoxTanggal" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">ID Penagihan</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiProdukPenagihan" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Status</label>
                                    <asp:TextBox ID="TextBoxStatus" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h3 class="border-bottom">VENDOR</h3>
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
            <h3 class="border-bottom">DETAIL</h3>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>Produk</th>
                                <th>Varian</th>
                                <th>Harga</th>
                                <th>Jumlah</th>
                                <th>Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterDetail" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("Produk") %></td>
                                        <td><%# Eval("AtributProduk") %></td>
                                        <td class="text-right"><%# Eval("HargaRetur").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                        <td class="text-right warning"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="table-success">
                                <td colspan="5" class="text-center font-weight-bold">TOTAL</td>
                                <td class="text-right font-weight-bold">
                                    <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <label class="form-label bold">Keterangan</label>
                <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" Enabled="false" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

