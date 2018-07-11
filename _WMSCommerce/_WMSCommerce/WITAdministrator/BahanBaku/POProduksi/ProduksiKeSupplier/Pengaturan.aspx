<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_ProduksiKeSupplier_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Production Raw Material to Supplier
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
                            <div class="card-header bg-gradient-black">
                                <h5 class="font-weight-light">SUPPLIER</h5>
                            </div>
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
                                            <th>Komposisi</th>
                                            <th>Biaya</th>
                                            <th>Harga</th>
                                            <th>Potongan</th>
                                            <th>Jumlah</th>
                                            <th>Subtotal</th>
                                            <th></th>
                                        </tr>
                                        <tr class="thead-light">
                                            <th></th>
                                            <th colspan="3">
                                                <asp:DropDownList ID="DropDownListStokBahanBaku" Width="100%" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStokBahanBaku_SelectedIndexChanged">
                                                </asp:DropDownList></th>
                                            <th></th>
                                            <th></th>
                                            <th>
                                                <div class="input-group" style="width: 100%;">
                                                    <asp:TextBox ID="TextBoxHargaSupplier" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text" style="width: 80px;">
                                                            <asp:Label ID="LabelSatuan" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHargaSupplier" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxHargaSupplier" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                            <th>
                                                <asp:TextBox ID="TextBoxPotonganHargaSupplier" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPotonganHargaSupplier" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxPotonganHargaSupplier" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                            <th>
                                                <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" CssClass="form-control text-right InputDesimal input-sm" Text="1.00"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="detail"></asp:RequiredFieldValidator></th>
                                            <th></th>
                                            <th class="fitSize">
                                                <asp:Button ID="ButtonSimpanDetail" runat="server" Text="Simpan" CssClass="btn btn-primary btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="detail" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDetail" runat="server" OnItemCommand="RepeaterDetail_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("Kode") %></td>
                                                    <td><%# Eval("BahanBaku") %></td>
                                                    <td class="fitSize"><%# Eval("Satuan") %></td>
                                                    <td class="text-right"><%# Eval("HargaPokokKomposisi").ToFormatHarga() %></td>
                                                    <td class="text-right"><%# Eval("BiayaTambahan").ToFormatHarga() %></td>
                                                    <td class="text-right"><%# Eval("Harga").ToFormatHarga() %></td>
                                                    <td class="text-right"><%# Eval("PotonganHarga").ToFormatHarga() %></td>
                                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize"><strong><%# Eval("SubtotalHarga").ToFormatHarga() %></strong></td>
                                                    <td class="text-center fitSize">
                                                        <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDBahanBaku") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-success">
                                            <td colspan="8" class="text-center font-weight-bold">TOTAL</td>
                                            <td class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotalJumlah" runat="server" Text="0"></asp:Label></td>
                                            <td class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Status HPP</label>
                            <asp:RadioButtonList ID="RadioButtonListStatusHPP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListStatusHPP_SelectedIndexChanged">
                                <asp:ListItem Text="Komposisi tiap bahan baku + Harga supplier" Value="4" Selected="True" />
                                <asp:ListItem Text="Rata-Rata dari keseluruhan komposisi + Harga supplier" Value="5" />
                                <asp:ListItem Text="Harga Supplier" Value="1" />
                            </asp:RadioButtonList>
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
                                                    <th class="fitSize">Bahan Baku</th>
                                                    <th>Satuan</th>
                                                    <th>Jumlah</th>
                                                    <th>Subtotal</th>
                                                </tr>
                                                <tr id="PanelTambahKomposisi" runat="server" class="thead-light" visible="false">
                                                    <th></th>
                                                    <th>
                                                        <asp:HiddenField ID="HiddenFieldHargaBeli" runat="server" />
                                                        <asp:DropDownList ID="DropDownListBahanBaku" runat="server" Width="100%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListBahanBaku_SelectedIndexChanged">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:HiddenField ID="HiddenFieldKonversi" runat="server" />
                                                        <asp:DropDownList ID="DropDownListSatuan" runat="server" Width="100%" CssClass="select2">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxJumlahBahanBaku" runat="server" onfocus="this.select();" CssClass="form-control text-right InputDesimal input-sm" Text="1.00"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlahBahanBaku" runat="server" ErrorMessage="Data harus diisi"
                                                            ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="bahanbaku"></asp:RequiredFieldValidator></th>
                                                    <th class="fitSize">
                                                        <asp:Button ID="ButtonSimpanBahanBaku" runat="server" Text="Simpan" CssClass="btn btn-primary btn-sm btn-block" OnClick="ButtonSimpanBahanBaku_Click" ValidationGroup="bahanbaku" /></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterKomposisi" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td class="fitSize"><%# Eval("BahanBaku") %></td>
                                                            <td><%# Eval("Satuan") %></td>
                                                            <td class="text-right"><%# Eval("JumlahKebutuhan").ToFormatHarga() %></td>
                                                            <td class="text-right warning"><strong><%# Eval("SubtotalKebutuhan").ToFormatHarga() %></strong></td>
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
                                                    <th></th>
                                                </tr>
                                                <tr id="PanelTambahBiayaProduksi" runat="server" class="thead-light" visible="false">
                                                    <th></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListJenisBiayaProduksi" runat="server" Width="100%" CssClass="select2">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxJumlahBiayaTambahan" runat="server" onfocus="this.select();" CssClass="form-control text-right InputDesimal input-sm" Text="1.00"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlahBiayaTambahan" runat="server" ErrorMessage="Data harus diisi"
                                                            ControlToValidate="TextBoxJumlahBiayaTambahan" ForeColor="Red" Display="Dynamic" ValidationGroup="biaya"></asp:RequiredFieldValidator></th>
                                                    <th class="fitSize">
                                                        <asp:Button ID="ButtonSimpanBiayaTambahan" runat="server" Text="Simpan" CssClass="btn btn-primary btn-sm btn-block" OnClick="ButtonSimpanBiayaTambahan_Click" ValidationGroup="biaya" /></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterBiayaTambahan" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Nama") %></td>
                                                            <td colspan="2" class="text-right fitSize warning"><strong><%# Eval("Biaya").ToFormatHarga() %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="table-success">
                                                    <td colspan="2" class="text-center font-weight-bold">TOTAL</td>
                                                    <td colspan="2" class="text-right font-weight-bold">
                                                        <asp:Label ID="LabelTotalSubtotalBiayaTambahan" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
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
                                                <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0" OnTextChanged="TextBoxBiaya_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="font-weight-bold text-muted">Potongan</label>
                                                <asp:TextBox ID="TextBoxPotonganPO" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0" OnTextChanged="TextBoxBiaya_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6 d-none">
                                                <asp:HiddenField ID="HiddenFieldTax" runat="server" />
                                                <strong>
                                                    <asp:Label ID="LabelTax" runat="server" class="font-weight-bold text-muted" Text="Tax (0%)"></asp:Label></strong>
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

