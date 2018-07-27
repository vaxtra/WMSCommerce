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
            <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                <b>PERINGATAN :</b>
                <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
            </div>
            <div class="form-group">
                <div class="card">
                    <div class="card-body">
                        <h3 class="border-bottom text-info">PIC</h3>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <label class="text-muted font-weight-bold">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawai" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                    <label class="text-muted font-weight-bold">Tanggal Proyeksi</label>
                                    <asp:TextBox ID="TextBoxTanggalProyeksi" runat="server" CssClass="form-control Tanggal"></asp:TextBox>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                    <label class="text-muted font-weight-bold">Tanggal Target</label>
                                    <asp:TextBox ID="TextBoxTanggalTarget" runat="server" CssClass="form-control Tanggal"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <h3 class="border-bottom text-info">DETAIL</h3>
                        <div class="form-group">
                            <div class="table-responsive">
                                <table class="table table-sm table-hover table-bordered">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No</th>
                                            <th>Kode</th>
                                            <th>Produk</th>
                                            <th>Warna</th>
                                            <th>Kategori</th>
                                            <th>Varian</th>
                                            <th>Jumlah</th>
                                            <th></th>
                                        </tr>
                                        <tr class="thead-light">
                                            <th></th>
                                            <th colspan="5">
                                                <asp:DropDownList ID="DropDownListStokProduk" CssClass="select2" runat="server">
                                                </asp:DropDownList></th>
                                            <th>
                                                <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" CssClass="form-control text-right InputInteger input-sm" Text="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlah" runat="server" ErrorMessage="Data harus diisi"
                                                    ControlToValidate="TextBoxJumlah" ForeColor="Red" Display="Dynamic" ValidationGroup="produk"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidatorJumlah" runat="server" ErrorMessage="-" ControlToValidate="TextBoxJumlah" ForeColor="Red"
                                                    Display="Dynamic" OnServerValidate="CustomValidatorJumlah_ServerValidate" ValidationGroup="produk"></asp:CustomValidator></th>
                                            <th class="fitSize">
                                                <asp:Button ID="ButtonSimpanDetail" runat="server" Text="Simpan" CssClass="btn btn-primary btn-block" OnClick="ButtonSimpanDetail_Click" ValidationGroup="produk" /></th>
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
                                        <tr class="table-success">
                                            <td colspan="6" class="text-center font-weight-bold">TOTAL</td>
                                            <td class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotalJumlah" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <asp:Panel ID="PanelKomposisi" runat="server" Visible="false">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered">
                                                <thead>
                                                    <tr class="table-warning">
                                                        <th colspan="7">
                                                            <h6>
                                                                <asp:Label ID="LabelBahanBakuDasar" runat="server" CssClass="font-weight-bold text-muted" Text="Bahan Baku Dasar"></asp:Label></h6>
                                                        </th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <th>No</th>
                                                        <th>Kategori</th>
                                                        <th>Bahan Baku</th>
                                                        <th>Satuan</th>
                                                        <th>Jumlah</th>
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
                                                    <table class="table table-sm table-hover table-bordered">
                                                        <thead>
                                                            <tr class="table-warning">
                                                                <th colspan="6">
                                                                    <h6>
                                                                        <asp:Label ID="LabelLevelBahanBaku" runat="server" class="font-weight-bold text-muted" Text='<%# "Produksi Bahan Baku Level " + (Container.ItemIndex + 1).ToString() %>'></asp:Label></h6>
                                                                </th>
                                                            </tr>
                                                            <tr class="thead-light">
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
                            </div>
                        </asp:Panel>
                        <div class="form-group">
                            <label class="text-muted font-weight-bold">Keterangan</label>
                            <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                        <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
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

