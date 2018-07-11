<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_ProduksiKeVendor_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Production Product to Vendor
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
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                        <b>PERINGATAN :</b>
                        <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <strong>Production Raw Material to Vendor</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">Proyeksi</label>
                                        <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Pegawai</label>
                                        <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">PIC</label>
                                        <asp:DropDownList ID="DropDownListPenggunaPIC" CssClass="select2" Width="100%" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Tanggal</label>
                                        <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">Tanggal Jatuh Tempo</label>
                                        <asp:TextBox ID="TextBoxTanggalJatuhTempo" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Tanggal Pengiriman</label>
                                        <asp:TextBox ID="TextBoxTanggalPengiriman" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <strong>Vendor</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="form-label bold">Vendor</label>
                                <asp:DropDownList ID="DropDownListVendor" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListVendor_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidatorVendor" runat="server" ErrorMessage="-" ControlToValidate="DropDownListVendor" ForeColor="Red"
                                    Display="Dynamic" OnServerValidate="CustomValidatorVendor_ServerValidate" ValidationGroup="simpan"></asp:CustomValidator>
                            </div>
                            <div class="form-group">
                                <label class="form-label bold">Alamat</label>
                                <asp:TextBox ID="TextBoxAlamat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label class="form-label bold">Email</label>
                                        <asp:TextBox ID="TextBoxEmail" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label bold">Telepon 1</label>
                                        <asp:TextBox ID="TextBoxTelepon1" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label bold">Telepon 2</label>
                                        <asp:TextBox ID="TextBoxTelepon2" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>Produk</strong>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr class="active">
                                        <th>No</th>
                                        <th>Kode</th>
                                        <th>Produk</th>
                                        <th>Varian</th>
                                        <th>Komposisi</th>
                                        <th>Biaya</th>
                                        <th>Harga</th>
                                        <th>Potongan</th>
                                        <th>Jumlah</th>
                                        <th>Subtotal</th>
                                        <th></th>
                                    </tr>
                                    <tr>
                                        <td>No</td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="DropDownListStokProduk" Width="100%" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListStokProduk_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxHargaVendor" onfocus="this.select();" Width="100%" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHargaVendor" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxHargaVendor" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxPotonganHargaVendor" onfocus="this.select();" Width="100%" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPotonganHargaVendor" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxPotonganHargaVendor" ForeColor="Red" ValidationGroup="detail" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" Width="100%" CssClass="form-control text-right InputInteger input-sm" Text="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="detail"></asp:RequiredFieldValidator></td>
                                        <td></td>
                                        <td class="fitSize">
                                            <asp:Button ID="ButtonSimpanDetail" runat="server" Text="Simpan" CssClass="btn btn-primary btn-sm btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="detail" /></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterDetail" runat="server" OnItemCommand="RepeaterDetail_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kode") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="fitSize"><%# Eval("Atribut") %></td>
                                                <td class="text-right"><%# Eval("HargaPokokKomposisi").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("BiayaTambahan").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("Harga").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("PotonganHarga").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                <td class="text-right fitSize warning"><strong><%# Eval("SubtotalHarga").ToFormatHarga() %></strong></td>
                                                <td class="text-center fitSize">
                                                    <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDKombinasiProduk") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="success">
                                        <td colspan="8" class="text-center"><b>TOTAL</b></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalJumlah" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></strong></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>Harga Pokok Produksi</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="form-label bold">Status HPP</label>
                                <asp:RadioButtonList ID="RadioButtonListStatusHPP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListStatusHPP_SelectedIndexChanged">
                                    <asp:ListItem Text="Komposisi tiap bahan baku + Harga supplier" Value="4" Selected="True" />
                                    <asp:ListItem Text="Rata-Rata dari keseluruhan komposisi + Harga supplier" Value="5" />
                                    <asp:ListItem Text="Harga Supplier" Value="1" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">Komposisi</label>
                                        <div class="table-responsive">
                                            <table class="table table-condensed table-hover table-bordered">
                                                <thead>
                                                    <tr class="active">
                                                        <th>No</th>
                                                        <th class="fitSize">Bahan Baku</th>
                                                        <th>Satuan</th>
                                                        <th>Jumlah</th>
                                                        <th>Subtotal</th>
                                                    </tr>
                                                    <tr id="PanelTambahKomposisi" runat="server" visible="false">
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
                                                    <tr class="success bold">
                                                        <td colspan="4" class="text-center"><b>TOTAL</b></td>
                                                        <td class="text-right"><strong>
                                                            <asp:Label ID="LabelTotalSubtotalKomposisi" runat="server" Text="0"></asp:Label></strong></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Biaya Tambahan</label>
                                        <div class="table-responsive">
                                            <table class="table table-condensed table-hover table-bordered">
                                                <thead>
                                                    <tr class="active">
                                                        <th>No</th>
                                                        <th>Nama</th>
                                                        <th>Biaya</th>
                                                        <th></th>
                                                    </tr>
                                                    <tr id="PanelTambahBiayaProduksi" runat="server" visible="false">
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
                                                    <tr class="success bold">
                                                        <td colspan="2" class="text-center"><b>TOTAL</b></td>
                                                        <td colspan="2" class="text-right"><strong>
                                                            <asp:Label ID="LabelTotalSubtotalBiayaTambahan" runat="server" Text="0"></asp:Label></strong></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-group">
                        <label class="form-label bold">Keterangan</label>
                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-group hidden">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label bold">Biaya lain-lain</label>
                                <asp:TextBox ID="TextBoxBiayaLainLain" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0" OnTextChanged="TextBoxBiaya_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label bold">Potongan</label>
                                <asp:TextBox ID="TextBoxPotonganPO" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0" OnTextChanged="TextBoxBiaya_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6 hidden">
                                <asp:HiddenField ID="HiddenFieldTax" runat="server" />
                                <strong>
                                    <asp:Label ID="LabelTax" runat="server" class="form-label bold" Text="Tax (0%)"></asp:Label></strong>
                                <asp:TextBox ID="TextBoxTax" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <label class="form-label bold">Grandtotal</label>
                                <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control text-right input-sm" Text="0" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-sm" OnClick="ButtonSimpan_Click" ValidationGroup="simpan" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger btn-sm" OnClick="ButtonKembali_Click" />
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

