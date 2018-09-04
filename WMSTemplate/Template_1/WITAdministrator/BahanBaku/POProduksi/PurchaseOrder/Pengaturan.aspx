<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_PurchaseOrder_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Purchase Order Raw Material
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
                <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                    <b>PERINGATAN :</b>
                    <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="card">
                            <h4 class="card-header bg-smoke">Supplier</h4>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Proyeksi</label>
                                            <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Pegawai</label>
                                            <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-muted">Supplier</label>
                                    <asp:DropDownList ID="DropDownListSupplier" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListSupplier_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:CustomValidator ID="CustomValidatorSupplier" runat="server" ErrorMessage="-" ControlToValidate="DropDownListSupplier" ForeColor="Red"
                                        Display="Dynamic" OnServerValidate="CustomValidatorSupplier_ServerValidate" ValidationGroup="simpan"></asp:CustomValidator>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">PIC</label>
                                            <asp:DropDownList ID="DropDownListPenggunaPIC" CssClass="select2" Width="100%" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Tanggal</label>
                                            <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Tanggal Jatuh Tempo</label>
                                            <asp:TextBox ID="TextBoxTanggalJatuhTempo" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="font-weight-bold text-muted">Tanggal Pengiriman</label>
                                            <asp:TextBox ID="TextBoxTanggalPengiriman" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="card">
                    <h4 class="card-header bg-smoke">Detail</h4>
                    <div class="card-body">
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
                                            <th>Jumlah</th>
                                            <th class="d-none">Potongan</th>
                                            <th>Harga Supplier</th>
                                            <th>Subtotal</th>
                                            <th></th>
                                        </tr>
                                        <tr class="thead-light">
                                            <th></th>
                                            <th colspan="3">
                                                <asp:DropDownList ID="DropDownListStokBahanBaku" Width="100%" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStokBahanBaku_SelectedIndexChanged">
                                                </asp:DropDownList></th>
                                            <th>
                                                <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" CssClass="form-control text-right InputDesimal input-sm" Text="1.00"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="detail"></asp:RequiredFieldValidator></th>
                                            <th class="d-none">
                                                <asp:TextBox ID="TextBoxPotonganHargaSupplier" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPotonganHargaSupplier" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxPotonganHargaSupplier" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                            <th>
                                                <div class="input-group" style="width: 100%;">
                                                    <asp:TextBox ID="TextBoxHargaSupplier" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <asp:Label ID="LabelSatuan" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHargaSupplier" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxHargaSupplier" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                            <th></th>
                                            <th>
                                                <asp:Button ID="ButtonSimpanDetail" runat="server" Text="Simpan" CssClass="btn btn-primary btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="detail" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDetail" runat="server" OnItemCommand="RepeaterDetail_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("Kode") %></td>
                                                    <td><%# Eval("BahanBaku") %></td>
                                                    <td class="text-center"><%# Eval("Satuan") %></td>
                                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                    <td class="text-right d-none"><%# Eval("PotonganHarga").ToFormatHarga() %></td>
                                                    <td class="text-right"><%# Eval("Harga").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize"><strong><%# Eval("SubtotalHarga").ToFormatHarga() %></strong></td>
                                                    <td class="text-center fitSize">
                                                        <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDBahanBaku") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-success">
                                            <td colspan="6" class="text-center font-weight-bold">TOTAL</td>
                                            <td class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <label class="font-weight-bold text-muted">Keterangan</label>
                                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <div class="form-group d-none">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="font-weight-bold text-muted">Biaya lain-lain</label>
                                                <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0.00" OnTextChanged="TextBoxBiaya_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="font-weight-bold text-muted">Potongan</label>
                                                <asp:TextBox ID="TextBoxPotonganPO" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0.00" OnTextChanged="TextBoxBiaya_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6 d-none">
                                                <label class="font-weight-bold text-muted">
                                                    <asp:HiddenField ID="HiddenFieldTax" runat="server" />
                                                </label>
                                                <strong>
                                                    <asp:Label ID="LabelTax" runat="server" class="form-label" Text="Tax (0%)"></asp:Label></strong>
                                                <asp:TextBox ID="TextBoxTax" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <label class="font-weight-bold text-muted">Grandtotal</label>
                                                <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" ValidationGroup="simpan" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                        <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger btn-const" OnClick="ButtonKembali_Click" />
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

