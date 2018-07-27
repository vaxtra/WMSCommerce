<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_Komposisi_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelNamaBahanBaku" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-7 col-lg-7">
            <div class="card">
                <div class="card-header bg-gradient-black">
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item"><a href="#tabKomposisi" id="Komposisi-tab" class="nav-link active font-weight-normal" data-toggle="tab">Komposisi</a></li>
                        <li class="nav-item"><a href="#tabBiayaProduksi" id="BiayaProduksi-tab" class="nav-link font-weight-normal" data-toggle="tab">Biaya produksi</a></li>
                    </ul>
                </div>
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="tabKomposisi">
                            <asp:UpdatePanel ID="UpdatePanelKomposisi" runat="server">
                                <ContentTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered mb-0">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th>Bahan Baku</th>
                                                        <th>Jumlah</th>
                                                        <th colspan="2">Subtotal</th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <th>
                                                            <asp:DropDownList ID="DropDownListBahanBaku" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListBahanBaku_SelectedIndexChanged">
                                                            </asp:DropDownList></th>
                                                        <th>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="TextBoxJumlahBahanBaku" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0"></asp:TextBox>
                                                                <div class="input-group-prepend">
                                                                    <div class="input-group-text">
                                                                        <asp:Label ID="LabelSatuan" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorJumlahBahanBaku" runat="server" ErrorMessage="Data harus diisi"
                                                                ControlToValidate="TextBoxJumlahBahanBaku" ForeColor="Red" ValidationGroup="groupKomposisiProduk" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </th>
                                                        <th colspan="2" class="fitSize">
                                                            <asp:Button ID="ButtonSimpanKomposisi" runat="server" CssClass="btn btn-success btn-block" Text="Simpan" OnClick="ButtonSimpanKomposisi_Click" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterKomposisi" runat="server" OnItemCommand="RepeaterKomposisi_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Nama") %></td>
                                                                <td class="text-right" style="width: 150px;"><%# Eval("Jumlah") %>
                                                                    <%# Eval("Satuan") %>
                                                                </td>
                                                                <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                                                <td class="text-center fitSize">
                                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDBahanBaku") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="table-warning">
                                                        <td colspan="2" rowspan="2" class="text-center" style="vertical-align: middle;"><b>TOTAL HARGA KOMPOSISI</b></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalHargaBesarKomposisi" runat="server" Font-Bold="true"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="LabelSatuanBesarKomposisi" runat="server" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                    <tr class="table-warning">
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalHargaKecilKomposisi" runat="server" Font-Bold="true"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="LabelSatuanKecilKomposisi" runat="server" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabBiayaProduksi">
                            <asp:UpdatePanel ID="UpdatePanelProduksi" runat="server">
                                <ContentTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered mb-0">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th>Nama</th>
                                                        <th>Jenis</th>
                                                        <th>Biaya</th>
                                                        <th></th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <th>
                                                            <asp:DropDownList ID="DropDownListJenisBiayaProduksi" runat="server" CssClass="select2" Width="100%">
                                                            </asp:DropDownList></th>
                                                        <th>
                                                            <asp:DropDownList ID="DropDownListEnumBiayaProduksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListEnumBiayaProduksi_SelectedIndexChanged">
                                                                <asp:ListItem Text="Persentase" Value="Persentase"></asp:ListItem>
                                                                <asp:ListItem Text="Nominal" Value="Nominal"></asp:ListItem>
                                                            </asp:DropDownList></th>
                                                        <th>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="TextBoxBiayaProduksi" runat="server" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" Text="0"></asp:TextBox>

                                                                <div class="input-group-prepend">
                                                                    <div class="input-group-text">
                                                                        <asp:Label ID="LabelStatusBiayaProduksi" runat="server" CssClass="form-label" Text="%"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBiayaProduksi" runat="server" ErrorMessage="Data harus diisi"
                                                                ControlToValidate="TextBoxBiayaProduksi" ForeColor="Red" ValidationGroup="groupBiayaProduksi" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </th>
                                                        <th class="fitSize">
                                                            <asp:Button ID="ButtonSimpanBiayaProduksi" CssClass="btn btn-success btn-block" runat="server" Text="Simpan" OnClick="ButtonSimpanBiayaProduksi_Click" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterBiayaProduksi" runat="server" OnItemCommand="RepeaterBiayaProduksi_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("NamaJenisBiayaProduksi") %></td>
                                                                <td class="fitSize"><%# Eval("Jenis") %></td>
                                                                <td class="text-right fitSize" style="width: 150px;"><%# Eval("BiayaProduksi") %></td>
                                                                <td class="text-center fitSize">
                                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDJenisBiayaProduksi") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="table-warning">
                                                        <td colspan="2" rowspan="2" class="text-center" style="vertical-align: middle;"><b>TOTAL BIAYA PRODUKSI</b></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalHargaBesarBiayaProduksi" runat="server" Font-Bold="true"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="LabelSatuanBesarBiayaProduksi" runat="server" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                    <tr class="table-warning">
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalHargaKecilBiayaProduksi" runat="server" Font-Bold="true"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="LabelSatuanKecilBiayaProduksi" runat="server" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5">
            <asp:UpdatePanel ID="UpdatePanelInfo" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="card">
                            <div class="card-header bg-gradient-black">
                                <h5 class="font-weight-light">JENIS BIAYA PRODDUKSI</h5>
                            </div>
                            <table class="table table-sm table-hover table-bordered mb-0">
                                <thead>
                                    <tr class="thead-light">
                                        <th>Nama</th>
                                        <th></th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th>
                                            <asp:HiddenField ID="HiddenFieldIDJenisBiayaProduksi" runat="server" />
                                            <asp:TextBox ID="TextBoxNamaJenisBiayaProduksi" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaJenisBiayaProduksi" runat="server" ErrorMessage="Data harus diisi"
                                                ControlToValidate="TextBoxNamaJenisBiayaProduksi" ForeColor="Red" ValidationGroup="groupJenisBiayaProduksi" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                        <th class="fitSize">
                                            <asp:Button ID="ButtonOkJenisBiayaProduksi" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonOkJenisBiayaProduksi_Click" ValidationGroup="groupJenisBiayaProduksi" /></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterJenisBiayaProduksi" runat="server" OnItemCommand="RepeaterJenisBiayaProduksi_ItemCommand1">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("Nama") %></td>
                                                <td class="text-right fitSize">
                                                    <asp:Button ID="ButtonUbah" CssClass="btn btn-primary btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDJenisBiayaProduksi") %>' />
                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDJenisBiayaProduksi") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="card">
                            <div class="card-header bg-gradient-black">
                                <h5 class="font-weight-light">HARGA PRODUKSI</h5>
                            </div>
                            <table class="table table-sm table-hover table-bordered mb-0" style="font-weight: bold;">
                                <tbody>
                                    <tr>
                                        <td class="table-active">HPP Saat ini</td>
                                        <td class="text-right table-info">
                                            <asp:Label ID="LabelHargaPokokSaatIni" runat="server" Text="-"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="table-active">Komposisi</td>
                                        <td class="text-right table-warning">
                                            <asp:Label ID="LabelHitunganKomposisi" runat="server" Text="-"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="table-active">Biaya Produksi</td>
                                        <td class="text-right table-warning">
                                            <asp:Label ID="LabelHitunganBiayaProduksi" runat="server" Text="-"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="table-active">HPP Seharusnya</td>
                                        <td class="text-right table-success">
                                            <asp:Label ID="LabelHargaPokokProduksi" runat="server" Text="-"></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

