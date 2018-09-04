<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Retur_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Retur Produk
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
<%--    <asp:UpdatePanel ID="UpdatePanelData" runat="server">
        <ContentTemplate>--%>
            <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                <b>PERINGATAN :</b>
                <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
            </div>
            <div class="card">
                <div class="card-body">
                    <h3 class="border-bottom">VENDOR</h3>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-4 col-md-6 col-lg-3">
                                <label class="font-weight-bold text-muted">Vendor</label>
                                <asp:DropDownList runat="server" ID="DropDownListVendor" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListVendor_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidatorVendor" runat="server" ErrorMessage="-" ControlToValidate="DropDownListVendor" ForeColor="Red"
                                    Display="Dynamic" OnServerValidate="CustomValidatorVendor_ServerValidate" ValidationGroup="simpan"></asp:CustomValidator>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-6 col-lg-3">
                                <label class="font-weight-bold text-muted">Penerimaan</label>
                                <asp:DropDownList runat="server" ID="DropDownListPenerimaan" CssClass="select2" Width="100%" Enabled="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-4 col-md-6 col-lg-6">
                                <label class="font-weight-bold text-muted">Tanggal</label>
                                <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <h3 class="border-bottom">DETAIL</h3>
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
                                        <th>Jumlah</th>
                                        <th>Subtotal</th>
                                        <th></th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th colspan="3">
                                            <asp:DropDownList runat="server" ID="DropDownListStokProduk" Width="100%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStokProduk_SelectedIndexChanged"></asp:DropDownList></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxHarga" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHarga" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxHarga" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                        <th>
                                            <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" CssClass="form-control text-right InputInteger input-sm" Text="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="detail"></asp:RequiredFieldValidator></th>
                                        <th></th>
                                        <th class="fitSize">
                                            <asp:Button runat="server" ID="ButtonSimpanDetail" Text="Tambah" CssClass="btn btn-primary btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="detail" /></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="RepeaterDetail" OnItemCommand="RepeaterDetail_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kode") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="text-center"><%# Eval("Atribut") %></td>
                                                <td class="text-right"><%# Eval("HargaVendor").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                <td class="text-right fitSize"><strong><%# Eval("SubtotalHargaBeli").ToFormatHarga() %></strong></td>
                                                <td class="text-center fitSize">
                                                    <asp:Button runat="server" ID="ButtonHapus" Text="Hapus" CssClass="btn btn-outline-danger btn-xs" CommandName="Hapus" CommandArgument='<%# Eval("IDStokProduk") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="table-success">
                                        <td colspan="6" class="text-center font-weight-bold">TOTAL</td>
                                        <td class="text-right font-weight-bold">
                                            <asp:Label ID="LabelTotal" Text="0" runat="server"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Keterangan</label>
                        <asp:TextBox runat="server" ID="TextBoxKeterangan" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" ValidationGroup="simpan" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger btn-const" OnClick="ButtonKembali_Click" />
                </div>
            </div>

<%--            <asp:UpdateProgress ID="updateProgressData" runat="server" AssociatedUpdatePanelID="UpdatePanelData">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressData" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

