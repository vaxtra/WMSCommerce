<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_ProduksiSendiri_Detail" %>

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
    In-House Production Raw Material Detail
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonEdit" runat="server" Text="Edit" CssClass="btn btn-primary btn-const mr-1" OnClick="ButtonEdit_Click" OnClientClick="if (!confirm('Are you sure to edit this data?')) return false;" />
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
        <div class="card-header bg-smoke">
            <ul id="myTab" class="nav nav-tabs card-header-tabs">
                <li class="nav-item"><a href="#tabData" id="data-tab" class="nav-link active" data-toggle="tab">Data</a></li>
                <li class="nav-item"><a href="#tabPengiriman" id="pengiriman-tab" class="nav-link" data-toggle="tab">Pengiriman</a></li>
                <li class="nav-item"><a href="#tabPenerimaan" id="penerimaan-tab" class="nav-link" data-toggle="tab">Penerimaan</a></li>
            </ul>
        </div>
        <div class="card-body">
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="tabData">
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
                                    <label class="font-weight-bold text-muted">Pembuat</label>
                                    <asp:TextBox ID="TextBoxPembuat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
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
                                        <th>Komposisi</th>
                                        <th>Biaya</th>
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
                                                <td class="text-right"><%# Eval("HargaPokokKomposisi").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("BiayaTambahan").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                <td class="text-right"><strong><%# Eval("SubtotalHPP").ToFormatHarga() %></strong></td>
                                                <td class="text-right table-danger"><strong><%# Eval("Sisa").ToFormatHarga() %></strong></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="table-success">
                                        <td colspan="6" class="text-center font-weight-bold">TOTAL</td>
                                        <td class="text-right font-weight-bold">
                                            <asp:Label ID="LabelTotalJumlah" runat="server" Text="0"></asp:Label></td>
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
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">Komposisi</label>
                                <div class="table-responsive">
                                    <table class="table table-sm table-hover table-bordered mb-0">
                                        <thead>
                                            <tr class="thead-light">
                                                <th>No</th>
                                                <th>Bahan Baku</th>
                                                <th>Satuan</th>
                                                <th>Kebutuhan</th>
                                                <th>Subtotal</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterKomposisi" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("TBBahanBaku.Nama") %></td>
                                                        <td class="fitSize"><%# Eval("TBSatuan.Nama") %></td>
                                                        <td class="text-right fitSize"><%# Eval("Kebutuhan").ToFormatHarga() %></td>
                                                        <td class="text-right fitSize"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="table-success">
                                                <td colspan="4" class="text-center font-weight-bold">TOTAL</td>
                                                <td class="text-right font-weight-bold">
                                                    <asp:Label ID="LabelTotalSubtotalKomposisi" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">Biaya Tambahan</label>
                                <div class="table-responsive">
                                    <table class="table table-sm table-hover table-bordered mb-0">
                                        <thead>
                                            <tr class="thead-light">
                                                <th>No</th>
                                                <th>Nama</th>
                                                <th>Biaya</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterBiayaTambahan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("TBJenisBiayaProduksi.Nama") %></td>
                                                        <td class="text-right fitSize warning"><strong><%# Eval("Nominal").ToFormatHarga() %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="table-success">
                                                <td colspan="2" class="text-center font-weight-bold">TOTAL</td>
                                                <td class="text-right font-weight-bold">
                                                    <asp:Label ID="LabelTotalSubtotalBiayaTambahan" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Keterangan</label>
                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" Enabled="false" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="tabPengiriman" aria-labelledby="penerimaan-tab">
                    <div class="form-group">
                        <h3 class="border-bottom">PENGIRIMAN</h3>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>ID</th>
                                        <th>Tanggal</th>
                                        <th>Pegawai</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPengiriman" runat="server" OnItemCommand="RepeaterPengiriman_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("IDPengirimanPOProduksiBahanBaku") %></td>
                                                <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                <td><%# Eval("Pegawai") %></td>
                                                <td class="text-right fitSize">
                                                    <asp:Button ID="ButtonDetail" runat="server" class="btn btn-info btn-xs" Style="margin-bottom: 0px;" Text="Detail" CommandName="Detail" CommandArgument='<%# Eval("IDPengirimanPOProduksiBahanBaku") %>' />
                                                    <asp:Button ID="ButtonCetak" runat="server" class="btn btn-light btn-xs" Style="margin-bottom: 0px;" Text="Cetak" CommandName="Cetak" CommandArgument='<%# Eval("IDPengirimanPOProduksiBahanBaku") %>' OnClientClick='<%# Eval("Cetak") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanelPengirimanDetail" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <h3 class="border-bottom">DETAIL PENGIRIMAN
                            <asp:Label ID="LabelIDPengiriman" runat="server"></asp:Label></h3>
                                <div class="table-responsive">
                                    <table class="table table-sm table-hover table-bordered">
                                        <thead>
                                            <tr class="thead-light">
                                                <th>No</th>
                                                <th>Bahan Baku</th>
                                                <th>Satuan</th>
                                                <th>Kirim</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterPengirimanDetail" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("BahanBaku") %></td>
                                                        <td class="fitSize"><%# Eval("Satuan") %></td>
                                                        <td class="text-right"><%# Eval("Kirim").ToFormatHarga() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RepeaterPengiriman" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div role="tabpanel" class="tab-pane" id="tabPenerimaan" aria-labelledby="penerimaan-tab">
                    <div class="form-group">
                        <h3 class="border-bottom">PENERIMAAN</h3>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
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
                                                <td class="fitSize"><%# Eval("IDPenerimaanPOProduksiBahanBaku") %></td>
                                                <td><%# Eval("TanggalDatang").ToFormatTanggal() %></td>
                                                <td><%# Eval("Pegawai") %></td>
                                                <td class="text-right fitSize">
                                                    <asp:Button ID="ButtonDetail" runat="server" class="btn btn-info btn-xs" Style="margin-bottom: 0px;" Text="Detail" CommandName="Detail" CommandArgument='<%# Eval("IDPenerimaanPOProduksiBahanBaku") %>' />
                                                    <asp:Button ID="ButtonCetak" runat="server" class="btn btn-light btn-xs" Style="margin-bottom: 0px;" Text="Cetak" CommandName="Cetak" CommandArgument='<%# Eval("IDPenerimaanPOProduksiBahanBaku") %>' OnClientClick='<%# Eval("Cetak") %>' />
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
                            <div class="form-group">
                                <h3 class="border-bottom">DETAIL PENERIMAAN
                            <asp:Label ID="LabelIDPenerimaan" runat="server"></asp:Label></h3>
                                <div class="table-responsive">
                                    <table class="table table-sm table-hover table-bordered">
                                        <thead>
                                            <tr class="thead-light">
                                                <th>No</th>
                                                <th>Bahan Baku</th>
                                                <th>Satuan</th>
                                                <th>Datang</th>
                                                <th>Diterima</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterPenerimaanDetail" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("BahanBaku") %></td>
                                                        <td class="fitSize"><%# Eval("Satuan") %></td>
                                                        <td class="text-right"><%# Eval("Datang").ToFormatHarga() %></td>
                                                        <td class="text-right"><%# Eval("Diterima").ToFormatHarga() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="font-weight-bold text-muted">Keterangan</label>
                                <asp:TextBox ID="TextBoxKeteranganPenerimaan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
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

