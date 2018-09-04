<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_BahanBaku_Transfer_Detail" %>

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
    Detail Transfer Bahan Baku
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a runat="server" id="linkDownload" class="btn btn-secondary mr-1">Download</a>
    <asp:Button ID="ButtonPrint" runat="server" Text="Cetak" CssClass="btn btn-secondary btn-const mr-1" />
    <a runat="server" id="linkKembali" class="btn btn-danger btn-const">Kembali</a>
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
            <h4 class="text-uppercase">
                <asp:Label ID="LabelStatusTransfer" runat="server"></asp:Label></h4>
            <div class="form-group">
                <label class="form-label bold text-muted mb-0">ID TRansfer</label>
                <br />
                <asp:Label ID="LabelIDTransfer" runat="server" CssClass="font-weight-bold"></asp:Label>
                <br />
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                        <h3 class="border-bottom">PENGIRIM</h3>
                        <div class="form-group">
                            <label class="form-label bold text-muted mb-0">Tanggal</label>
                            <br />
                            <asp:Label ID="LabelPengirimTanggal" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold text-muted mb-0">Tempat</label>
                            <br />
                            <asp:Label ID="LabelPengirimLokasi" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold text-muted mb-0">Pegawai</label>
                            <br />
                            <asp:Label ID="LabelPengirimPengguna" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                        <h3 class="border-bottom">PENERIMA</h3>
                        <div class="form-group">
                            <label class="form-label bold text-muted mb-0">Tanggal</label>
                            <br />
                            <asp:Label ID="LabelPenerimaTanggal" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold text-muted mb-0">Tempat</label>
                            <br />
                            <asp:Label ID="LabelPenerimaLokasi" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold text-muted mb-0">Pegawai</label>
                            <br />
                            <asp:Label ID="LabelPenerimaPengguna" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No.</th>
                                <th>Bahan Baku</th>
                                <th>Satuan</th>
                                <th>Kategori</th>
                                <th>Kode</th>
                                <th>Harga</th>
                                <th>Quantity</th>
                                <th>Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterTransferBahanBaku" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("BahanBaku") %></td>
                                        <td class="fitSize"><%# Eval("SatuanBesar") %></td>
                                        <td><%# Eval("Kategori") %></td>
                                        <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                        <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                        <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                        <td class="text-right fitSize"><%# Eval("Subtotal").ToFormatHarga() %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr class="table-success">
                                <td colspan="7" class="text-center font-weight-bold">TOTAL</td>
                                <td class="text-right font-weight-bold">
                                    <asp:Label ID="LabelTotalNominal" runat="server"></asp:Label></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

