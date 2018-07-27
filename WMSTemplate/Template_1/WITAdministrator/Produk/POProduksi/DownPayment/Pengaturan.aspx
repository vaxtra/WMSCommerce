<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_DownPayment_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Down Payment Purchase Order Product
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
            <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                <b>PERINGATAN :</b>
                <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
            </div>
            <div class="card">
                <div class="card-body">
                    <h3 class="border-bottom text-info">VENDOR</h3>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                <label class="font-weight-bold text-muted">Vendor</label>
                                <asp:DropDownList ID="DropDownListCariVendor" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariVendor_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                <label class="font-weight-bold text-muted">ID Produksi</label>
                                <asp:DropDownList ID="DropDownListIDPOProduksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListIDPOProduksi_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                <label class="font-weight-bold text-muted">PIC</label>
                                <asp:TextBox ID="TextBoxPegawaiPIC" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                <label class="font-weight-bold text-muted">Status HPP</label>
                                <asp:TextBox ID="TextBoxStatusHPP" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                <label class="font-weight-bold text-muted">Pembuat</label>
                                <asp:TextBox ID="TextBoxPembuat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="font-weight-bold text-muted">Tanggal Jatuh Tempo</label>
                                        <asp:TextBox ID="TextBoxTanggalJatuhTempo" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="font-weight-bold text-muted">Tanggal Pengiriman</label>
                                        <asp:TextBox ID="TextBoxTanggalPengiriman" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h3 class="border-bottom text-info">DETAIL</h3>
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Produk</label>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No</th>
                                        <th>Kode</th>
                                        <th>Produk</th>
                                        <th>Varian</th>
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
                                                <td class="fitSize"><%# Eval("TBKombinasiProduk.KodeKombinasiProduk") %></td>
                                                <td><%# Eval("TBKombinasiProduk.TBProduk.Nama") %></td>
                                                <td class="fitSize"><%# Eval("TBKombinasiProduk.TBAtributProduk.Nama") %></td>
                                                <td class="text-right"><%# Eval("HargaVendor").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("PotonganHargaVendor").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                <td class="text-right warning"><strong><%# Eval("SubtotalHargaVendor").ToFormatHarga() %></strong></td>
                                                <td class="text-right danger"><strong><%# Eval("Sisa").ToFormatHargaBulat() %></strong></td>
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
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Biaya lain-lain</label>
                                            <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Potongan</label>
                                            <asp:TextBox ID="TextBoxPotonganPO" runat="server" Enabled="false" CssClass="form-control text-right input-sm" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">
                                                <asp:Label ID="LabelTax" runat="server" Text="Tax (0%)"></asp:Label></label>
                                            <asp:TextBox ID="TextBoxTax" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Grandtotal</label>
                                            <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Pembayar</label>
                                            <asp:TextBox ID="TextBoxPenggunaDP" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Tanggal</label>
                                            <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Jenis Pembayaran</label>
                                            <asp:DropDownList ID="DropDownListJenisPembayaran" CssClass="select2" Width="100%" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Down Payment</label>
                                            <asp:TextBox ID="TextBoxDownPayment" runat="server" onfocus="this.select();" CssClass="form-control input-sm text-right InputDesimal" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-weight-bold text-muted">Keterangan</label>
                                    <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" Enabled="false" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" Enabled="false" OnClick="ButtonSimpan_Click" ValidationGroup="simpan" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger btn-const" OnClick="ButtonKembali_Click" />
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

