<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="ProyeksiProduk.aspx.cs" Inherits="WITPointOfSales_ProyeksiProduk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="bs-docs-section">
        <h4 id="tabs" class="text-left">PROYEKSI PRODUK</h4>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <asp:UpdatePanel ID="UpdatePanelProyeksiProduk" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <h6><strong><u>KOMPOSISI PRODUK</u></strong></h6>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 82%; padding-top: 5px;">
                                                    <asp:DropDownList ID="DropDownListCariKomposisiProduk" CssClass="select2" Width="100%" runat="server"></asp:DropDownList></td>
                                                <td style="width: 5px;"></td>
                                                <td>
                                                    <asp:Button CssClass="btn btn-info pull-right btn-xs" ID="ButtonCariKomposisiProduk" runat="server" Text="Cari" OnClick="ButtonCariKomposisiProduk_Click" /></td>
                                                <td style="width: 5px;"></td>
                                                <td><a class="btn btn-primary btn-xs pull-right collapsed" data-toggle="collapse" href="#CollapseKomposisiProduk" aria-expanded="true" aria-controls="CollapseKomposisiProduk">Tambah</a></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="CollapseKomposisiProduk" runat="server" aria-expanded="false" class="collapse" clientidmode="static">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="DropDownListBahanBaku" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListBahanBaku_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group" style="margin: -2px 0px 0 0">
                                                    <asp:TextBox ID="TextBoxJumlahBahanBaku" runat="server" onfocus="this.select();" CssClass="form-control input-sm text-right angkaDesimal" Width="100%" Placeholder="Jumlah"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        <asp:Label ID="LabelSatuan" runat="server" Font-Bold="true" Font-Size="XX-Small"></asp:Label>
                                                    </div>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlahBahanBaku" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxJumlahBahanBaku" ForeColor="Red" ValidationGroup="groupBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidatorJumlahBahanBaku" runat="server" ErrorMessage="-" ControlToValidate="TextBoxJumlahBahanBaku" ForeColor="Red"
                                                    ValidationGroup="groupBahanBaku" Display="Dynamic" OnServerValidate="CustomValidatorJumlahBahanBaku_ServerValidate"></asp:CustomValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button CssClass="btn btn-success btn-block btn-sm" Style="margin: -2px 0px 0 0" ID="ButtonSimpanBahanBaku" runat="server" Text="Simpan" OnClick="ButtonSimpanBahanBaku_Click" ValidationGroup="groupBahanBaku" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <h6>Komposisi Bahan Baku</h6>
                                                <div class="table-responsive">
                                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                        <thead>
                                                            <tr class="active">
                                                                <th class="text-center">No</th>
                                                                <th class="text-center">Bahan Baku</th>
                                                                <th class="text-center">Jumlah</th>
                                                                <th class="text-center">Satuan</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterKomposisiBahanBaku" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("BahanBaku") %></td>
                                                                        <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Jumlah").ToString()) %></td>
                                                                        <td class="text-center"><%# Eval("Satuan") %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                <thead>
                                                    <tr class="active">
                                                        <th class="text-center" style="width: 5%">No</th>
                                                        <th class="text-center" style="width: 60%">Bahan Baku</th>
                                                        <th class="text-center" style="width: 10%">Jumlah</th>
                                                        <th class="text-center" style="width: 5%">Satuan</th>
                                                        <th class="text-center" style="width: 15%">Subtotal</th>
                                                        <th class="text-center" style="width: 5%"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterKomposisiProduk" runat="server" OnItemCommand="RepeaterKomposisiProduk_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class='<%# Eval("StatusKomposisi") %>'>
                                                                <td class="text-center" style="width: 5%"><%# Container.ItemIndex + 1 %></td>
                                                                <td style="width: 60%">
                                                                    <a class="collapsed" data-toggle="collapse" href='<%# Eval("TargetBahanBaku") %>' aria-expanded="false" aria-controls='<%# Eval("IDBahanBaku") %>'><%# Eval("BahanBaku") %></a>
                                                                    <div style="height: 0px;" aria-expanded="false" class="collapse" id='<%# Eval("IDBahanBaku") %>'>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                                                <tbody>
                                                                                    <asp:Repeater ID="RepeaterKomposisiBahanBaku" runat="server" DataSource='<%# Eval("Komposisi") %>'>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td><%# Eval("BahanBaku") %></td>
                                                                                                <td class="text-right"><%# Pengaturan.FormatHarga(Eval("JumlahPemakaian").ToString()) %></td>
                                                                                                <td class="text-center"><%# Eval("Satuan") %></td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td class="text-right" style="width: 10%"><%# Pengaturan.FormatHarga(Eval("Jumlah").ToString()) %></td>
                                                                <td class="text-center" style="width: 5%"><%# Eval("Satuan") %></td>
                                                                <td class="text-right warning" style="width: 15%"><%# Pengaturan.FormatHarga(Eval("SubtotalHargaBeli").ToString()) %></td>
                                                                <td class="text-center">
                                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDBahanBaku") %>' /></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="success">
                                                        <td class="text-center" colspan="4"><strong>TOTAL</strong></td>
                                                        <td class="text-right"><strong>
                                                            <asp:Label ID="LabelSubtotalKomposisiProduk" runat="server" Text="0"></asp:Label></strong></td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h6><strong><u>BIAYA PRODUKSI</u></strong></h6>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 82%; padding-top: 5px;">
                                                    <asp:DropDownList ID="DropDownListCariBiayaProduksi" CssClass="select2" Width="100%" runat="server"></asp:DropDownList></td>
                                                <td style="width: 5px;"></td>
                                                <td>
                                                    <asp:Button CssClass="btn btn-info btn-xs" ID="ButtonCariBiayaProduksi" runat="server" Text="Cari" OnClick="ButtonCariBiayaProduksi_Click" /></td>
                                                <td style="width: 5px;"></td>
                                                <td><a class="btn btn-primary btn-xs pull-right collapsed" data-toggle="collapse" href="#CollapseBiayaProduksi" aria-expanded="true" aria-controls="CollapseBiayaProduksi">Tambah</a></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="CollapseBiayaProduksi" runat="server" aria-expanded="false" class="collapse" clientidmode="static">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="DropDownListJenisBiayaProduksi" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListJenisBiayaProduksi_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TextBoxNamaJenisBiayaProduksi" runat="server" CssClass="form-control input-sm" Width="100%" Placeholder="Nama"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaJenisBiayaProduksi" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxNamaJenisBiayaProduksi" ForeColor="Red" ValidationGroup="groupBiayaProduksi" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidatorNamaJenisBiayaProduksi" runat="server" ErrorMessage="-" ControlToValidate="TextBoxNamaJenisBiayaProduksi" ForeColor="Red"
                                                    ValidationGroup="groupBiayaProduksi" Display="Dynamic" OnServerValidate="CustomValidatorNamaJenisBiayaProduksi_ServerValidate"></asp:CustomValidator>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:RadioButtonList ID="RadioButtonListEnumBiayaProduksi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListEnumBiayaProduksi_SelectedIndexChanged">
                                                    <asp:ListItem Text="Persentase dari Komposisi" Value="Persentase" Selected="True" />
                                                    <asp:ListItem Text="Nominal" Value="Nominal" />
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-4">
                                                <br />
                                                <div class="input-group">
                                                    <asp:TextBox ID="TextBoxBiayaProduksi" runat="server" onfocus="this.select();" CssClass="form-control input-sm text-right angkaDesimal" Width="100%" Placeholder="Jumlah"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        <asp:Label ID="LabelStatusBiayaProduksi" runat="server" Font-Bold="true" Font-Size="XX-Small" Text="%"></asp:Label>
                                                    </div>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorBiayaProduksi" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxBiayaProduksi" ForeColor="Red" ValidationGroup="groupBiayaProduksi" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidatorBiayaProduksi" runat="server" ErrorMessage="-" ControlToValidate="TextBoxBiayaProduksi" ForeColor="Red"
                                                    ValidationGroup="groupBiayaProduksi" Display="Dynamic" OnServerValidate="CustomValidatorBiayaProduksi_ServerValidate"></asp:CustomValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <br />
                                                <asp:Button CssClass="btn btn-success btn-block btn-sm" ID="ButtonSimpanBiayaProduksi" runat="server" Text="Simpan" OnClick="ButtonSimpanBiayaProduksi_Click" ValidationGroup="groupBiayaProduksi" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                <thead>
                                                    <tr class="active">
                                                        <th class="text-center" style="width: 5%">No</th>
                                                        <th class="text-center" style="width: 30%">Nama</th>
                                                        <th class="text-center" style="width: 45%">Jenis</th>
                                                        <th class="text-center" style="width: 15%">Biaya</th>
                                                        <th class="text-center" style="width: 5%"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterBiayaProduksi" runat="server" OnItemCommand="RepeaterBiayaProduksi_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center" style="width: 5%"><%# Container.ItemIndex + 1 %></td>
                                                                <td style="width: 30%"><%# Eval("Nama") %></td>
                                                                <td class="text-right" style="width: 45%"><%# Eval("Jenis") %></td>
                                                                <td class="text-right warning" style="width: 15%"><%# Eval("Biaya") %></td>
                                                                <td class="text-center">
                                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDJenisBiayaProduksi") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="success">
                                                        <td class="text-center" colspan="3"><strong>TOTAL</strong></td>
                                                        <td class="text-right"><strong>
                                                            <asp:Label ID="LabelSubtotalBiayaProduksi" runat="server" Text="0"></asp:Label></strong></td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="DropDownListCariProduk" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariProduk_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="form-label">Nama Produk</label>
                                                <asp:TextBox ID="TextBoxNamaProduk" runat="server" CssClass="form-control input-sm" Placeholder="Nama Produk"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaProduk" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxNamaProduk" ForeColor="Red" ValidationGroup="groupProduk" Display="Dynamic"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="form-label">Brand</label>
                                                <asp:DropDownList ID="DropDownListBrand" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListBrand_SelectedIndexChanged"></asp:DropDownList>

                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-label" style="color: white;">Brand</label>
                                                <asp:TextBox ID="TextBoxBrand" runat="server" CssClass="form-control input-sm" Placeholder="Brand"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorBrand" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxBrand" ForeColor="Red" ValidationGroup="groupProduk" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="form-label">Warna</label>
                                                <asp:DropDownList ID="DropDownListWarna" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListWarna_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-label" style="color: white;">Warna</label>
                                                <asp:TextBox ID="TextBoxWarna" runat="server" CssClass="form-control input-sm" Placeholder="Warna"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorWarna" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxWarna" ForeColor="Red" ValidationGroup="groupProduk" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="form-label">Varian</label>
                                                <asp:DropDownList ID="DropDownListVarian" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListVarian_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-label" style="color: white;">Varian</label>
                                                <asp:TextBox ID="TextBoxVarian" runat="server" CssClass="form-control input-sm" Placeholder="Varian"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="form-label">Kode</label>
                                                <asp:TextBox ID="TextBoxKode" runat="server" CssClass="form-control input-sm" Placeholder="Kode"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKode" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxKode" ForeColor="Red" ValidationGroup="groupProduk" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="form-label">Kategori</label>
                                                <asp:CheckBoxList ID="CheckBoxListKategori" RepeatDirection="Vertical" RepeatColumns="3" runat="server"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="form-label">Harga Pokok Produksi</label>
                                                <asp:TextBox ID="TextBoxHargaPokokProduksi" runat="server" CssClass="form-control input-sm text-right angkaDesimal" Enabled="false" Placeholder="Harga Pokok Produksi"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-label">Harga Jual</label>
                                                <asp:TextBox ID="TextBoxHargaJual" runat="server" CssClass="form-control input-sm text-right angkaDesimal" Placeholder="Harga Jual"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHargaJual" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxHargaJual" ForeColor="Red" ValidationGroup="groupProduk" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="form-label">Keterangan</label>
                                                <asp:TextBox ID="TextBoxKeterangan" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Placeholder="Keterangan"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button CssClass="btn btn-success btn-block btn-sm" ID="ButtonSimpanProduk" runat="server" Text="Simpan" OnClick="ButtonSimpanProduk_Click" ValidationGroup="groupProduk" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button CssClass="btn btn-danger btn-block btn-sm" ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Literal ID="LiteralInformasi" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelProyeksiProduk">
                            <ProgressTemplate>
                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

