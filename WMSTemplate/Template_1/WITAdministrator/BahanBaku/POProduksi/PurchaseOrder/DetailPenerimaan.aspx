<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="DetailPenerimaan.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_PurchaseOrder_DetailPenerimaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Penerimaan Purchase Order Raw Material
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
                        <h3 class="border-bottom">PENERIMAAN</h3>
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
                                    <asp:TextBox ID="TextBoxIDPOProduksiBahanBakuPenagihan" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
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
            <div class="form-group">
                <h3 class="border-bottom">DETAIL</h3>
                <label class="font-weight-bold text-muted">Bahan Baku</label>
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered mb-0">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>Kode</th>
                                <th>Bahan Baku</th>
                                <th>Satuan</th>
                                <th>Datang</th>
                                <th>Diterima</th>
                                <th>Tolak Supplier</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterDetail" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("TBBahanBaku.KodeBahanBaku") %></td>
                                        <td><%# Eval("TBBahanBaku.Nama") %></td>
                                        <td class="fitSize"><%# Eval("TBSatuan.Nama") %></td>
                                        <td class="text-right table-warning"><strong><%# Eval("Datang").ToFormatHarga() %></strong></td>
                                        <td class="text-right table-success"><strong><%# Eval("Diterima").ToFormatHarga() %></strong></td>
                                        <td class="text-right table-danger"><strong><%# Eval("TolakKeSupplier").ToFormatHarga() %></strong></td>
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

