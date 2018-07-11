<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_Proyeksi_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Proyeksi
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
                            <strong>Proyeksi</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="form-label bold">Pegawai</label>
                                <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">Tanggal Proyeksi</label>
                                        <asp:TextBox ID="TextBoxTanggalProyeksi" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Tanggal Target</label>
                                        <asp:TextBox ID="TextBoxTanggalTarget" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
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
                                    <tr>
                                        <th>No</th>
                                        <th>Kode</th>
                                        <th>Produk</th>
                                        <th>Warna</th>
                                        <th>Kategori</th>
                                        <th>Varian</th>
                                        <th>Jumlah</th>
                                        <th></th>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="DropDownListBrand" CssClass="select2 pull-left" Width="50%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListBrand_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownListStokProduk" CssClass="select2 pull-right" runat="server" Width="50%">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" CssClass="form-control text-right InputInteger input-sm" Text="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="produk"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidatorJumlah" runat="server" ErrorMessage="-" ControlToValidate="TextBoxJumlah" ForeColor="Red"
                                                Display="Dynamic" OnServerValidate="CustomValidatorJumlah_ServerValidate" ValidationGroup="produk"></asp:CustomValidator></td>
                                        <td class="fitSize">
                                            <asp:Button ID="ButtonSimpanDetail" runat="server" Text="Simpan" CssClass="btn btn-primary btn-sm btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="produk" /></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterProduk" runat="server" OnItemCommand="RepeaterProduk_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kode") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="text-center"><%# Eval("Warna") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td class="text-center"><%# Eval("Atribut") %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                <td class="text-center">
                                                    <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDKombinasiProduk") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="success bold">
                                        <td colspan="6" class="text-center"><b>TOTAL</b></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalJumlah" runat="server"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelKomposisi" runat="server" Visible="false">
                <div class="row form-row">
                    <div class="col-md-6">
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr class="warning">
                                        <th colspan="7">
                                            <label class="form-label">Bahan Baku Dasar</label></th>
                                    </tr>
                                    <tr>
                                        <th>No</th>
                                        <th>Kategori</th>
                                        <th>Bahan Baku</th>
                                        <th>Satuan</th>
                                        <th>Butuh</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterBahanBakuDasar" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td><%# Eval("BahanBaku") %></td>
                                                <td><%# Eval("Satuan") %></td>
                                                <td class="text-right"><%# Eval("JumlahPemakaian").ToFormatHarga() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Repeater ID="RepeaterKomposisi" runat="server">
                            <ItemTemplate>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered">
                                        <thead>
                                            <tr class="warning">
                                                <th colspan="5">
                                                    <asp:Label ID="LabelLevelBahanBaku" runat="server" class="form-label bold" Text='<%# "Produksi Bahan Baku Level " + (Container.ItemIndex + 1).ToString() %>'></asp:Label></th>
                                            </tr>
                                            <tr>
                                                <th>No</th>
                                                <th>Kategori</th>
                                                <th>Bahan Baku</th>
                                                <th>Satuan</th>
                                                <th>Jumlah</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterSubBahanBaku" runat="server" DataSource='<%# Eval("SubData") %>'>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("Kategori") %></td>
                                                        <td><%# Eval("BahanBaku") %></td>
                                                        <td><%# Eval("Satuan") %></td>
                                                        <td class="text-right"><%# Eval("JumlahPemakaian").ToFormatHarga() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="form-group">
                        <label class="form-label">Keterangan</label>
                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-sm" OnClick="ButtonSimpan_Click" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <a href="Default.aspx" class="btn btn-danger btn-sm">Kembali</a>
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

