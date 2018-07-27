<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_Retur_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Retur Bahan Baku
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
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="card">
                            <div class="card-header bg-gradient-black">
                                <h5 class="font-weight-light">SUPPLIER</h5>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Tanggal</label>
                                            <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Supplier</label>
                                            <asp:DropDownList runat="server" ID="DropDownListSupplier" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListSupplier_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidatorSupplier" runat="server" ErrorMessage="-" ControlToValidate="DropDownListSupplier" ForeColor="Red"
                                                Display="Dynamic" OnServerValidate="CustomValidatorSupplier_ServerValidate" ValidationGroup="simpan"></asp:CustomValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="font-weight-bold text-muted">Penerimaan</label>
                                            <asp:DropDownList runat="server" ID="DropDownListPenerimaan" CssClass="select2" Width="100%" Enabled="false"></asp:DropDownList>
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
                    <div class="card-header bg-gradient-black">
                        <h5 class="font-weight-light">DETAIL</h5>
                    </div>
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
                                            <th>Harga</th>
                                            <th>Jumlah</th>
                                            <th>Subtotal</th>
                                            <th></th>
                                        </tr>
                                        <tr class="thead-light">
                                            <th></th>
                                            <th colspan="3">
                                                <asp:DropDownList runat="server" ID="DropDownListStokBahanBaku" Width="100%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStokBahanBaku_SelectedIndexChanged"></asp:DropDownList></th>
                                            <th>
                                                <div class="input-group" style="width: 100%;">
                                                    <asp:TextBox ID="TextBoxHarga" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                                    <span class="input-group-addon" style="width: 75px;">
                                                        <asp:Label ID="LabelSatuan" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                    </span>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHarga" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxHarga" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" CssClass="form-control text-right InputDesimal input-sm" Text="1.00"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="detail"></asp:RequiredFieldValidator></th>
                                            <th></th>
                                            <th class="fitSize">
                                                <asp:Button runat="server" ID="ButtonSimpanDetail" Text="Simpan" CssClass="btn btn-primary btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="detail" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="RepeaterDetail" OnItemCommand="RepeaterDetail_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("Kode") %></td>
                                                    <td><%# Eval("BahanBaku") %></td>
                                                    <td class="text-center"><%# Eval("Satuan") %></td>
                                                    <td class="text-right"><%# Eval("HargaSupplier").ToFormatHarga() %></td>
                                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize warning"><strong><%# Eval("SubtotalHargaSupplier").ToFormatHarga() %></strong></td>
                                                    <td class="text-center fitSize">
                                                        <asp:Button runat="server" ID="ButtonHapus" Text="X" CssClass="btn btn-danger btn-xs" CommandName="Hapus" CommandArgument='<%# Eval("IDStokBahanBaku") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-success">
                                            <td colspan="6" class="text-center font-weight-bold">TOTAL</td>
                                            <td colspan="2" class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotal" Text="0" runat="server"></asp:Label></td>
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

