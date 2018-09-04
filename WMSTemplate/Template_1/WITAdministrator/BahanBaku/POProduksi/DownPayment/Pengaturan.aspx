<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_DownPayment_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Down Payment Purchase Order Raw Material
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelData" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                            <b>PERINGATAN :</b>
                            <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="card">
                            <h4 class="card-header bg-smoke">Purchase Order</h4>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="form-label bold">Supplier</label>
                                            <asp:DropDownList ID="DropDownListCariSupplier" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariSupplier_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="form-label bold">ID Produksi</label>
                                            <asp:DropDownList ID="DropDownListIDPOProduksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListIDPOProduksi_SelectedIndexChanged">
                                            </asp:DropDownList>
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
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="card">
                            <h4 class="card-header bg-smoke">Detail</h4>
                            <div class="card-body">
                                <div class="form-group">
                                    <label class="form-label bold">Bahan Baku</label>
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered mb-0">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>No</th>
                                                    <th>Kode</th>
                                                    <th>Bahan Baku</th>
                                                    <th>Satuan</th>
                                                    <th>Harga</th>
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
                                                            <td class="fitSize"><%# Eval("TBBahanBaku.KodeBahanBaku") %></td>
                                                            <td><%# Eval("TBBahanBaku.Nama") %></td>
                                                            <td class="fitSize"><%# Eval("TBSatuan.Nama") %></td>
                                                            <td class="text-right"><%# Eval("HargaSupplier").ToFormatHarga() %></td>
                                                            <td class="text-right"><%# Eval("PotonganHargaSupplier").ToFormatHarga() %></td>
                                                            <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                            <td class="text-right warning"><strong><%# Eval("SubtotalHargaSupplier").ToFormatHarga() %></strong></td>
                                                            <td class="text-right danger"><strong><%# Eval("Sisa").ToFormatHarga() %></strong></td>
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
                                        <div class="col-md-3">
                                            <label class="form-label bold">Biaya lain-lain</label>
                                            <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="form-label bold">Potongan</label>
                                            <asp:TextBox ID="TextBoxPotonganPO" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 d-none">
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
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="card">
                                <h4 class="card-header bg-smoke">Pembayaran</h4>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label class="form-label bold">Pembayar</label>
                                            <asp:TextBox ID="TextBoxPenggunaDP" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <label class="form-label bold">Tanggal</label>
                                            <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="form-label bold">Jenis Pembayaran</label>
                                            <asp:DropDownList ID="DropDownListJenisPembayaran" CssClass="select2" Width="100%" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="form-label bold">Down Payment</label>
                                            <asp:TextBox ID="TextBoxDownPayment" runat="server" onfocus="this.select();" CssClass="form-control input-sm text-right InputDesimal" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                                <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" Enabled="false" OnClick="ButtonSimpan_Click" ValidationGroup="simpan" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                                <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger btn-const" OnClick="ButtonKembali_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressData" runat="server" AssociatedUpdatePanelID="UpdatePanelData">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressData" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

