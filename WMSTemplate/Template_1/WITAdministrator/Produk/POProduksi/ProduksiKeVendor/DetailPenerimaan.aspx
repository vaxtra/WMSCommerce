<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="DetailPenerimaan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_ProduksiKeSupplier_DetailPenerimaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Penerimaan Production to Supplier Product
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
                        <h3 class="border-bottom">PIC</h3>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Proyeksi</label>
                                    <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">ID Purchase Order</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiProduk" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Tanggal</label>
                                    <asp:TextBox ID="TextBoxTanggal" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Status</label>
                                    <asp:TextBox ID="TextBoxStatus" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">ID Invoice</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiProdukPenagihan" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h3 class="border-bottom">VENDOR</h3>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Vendor</label>
                                    <asp:TextBox ID="TextBoxVendor" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
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
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>Kode</th>
                                <th>Produk</th>
                                <th>Varian</th>
                                <th>Datang</th>
                                <th>Diterima</th>
                                <th>Tolak Vendor</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterDetail" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("TBKombinasiProduk.KodeKombinasiProduk") %></td>
                                        <td><%# Eval("TBKombinasiProduk.TBProduk.Nama") %></td>
                                        <td class="text-center fitSize"><%# Eval("TBKombinasiProduk.TBAtributProduk.Nama") %></td>
                                        <td class="text-right warning"><strong><%# Eval("Datang").ToFormatHargaBulat() %></strong></td>
                                        <td class="text-right success"><strong><%# Eval("Diterima").ToFormatHargaBulat() %></strong></td>
                                        <td class="text-right danger"><strong><%# Eval("TolakKeVendor").ToFormatHargaBulat() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <label class="font-weight-bold text-muted">Keterangan</label>
                <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" Enabled="false" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

