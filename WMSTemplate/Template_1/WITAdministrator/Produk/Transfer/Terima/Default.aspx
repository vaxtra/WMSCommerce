<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Transfer_Terima_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Terima Transfer Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Import.aspx" class="btn btn-secondary btn-const">Import</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <asp:UpdatePanel ID="UpdatePanelTanggal" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListCariBulan" runat="server" CssClass="select2 mr-1" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCari_SelectedIndexChanged">
                                    <asp:ListItem Text="Januari" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febuari" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Maret" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Mei" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Juni" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Juli" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Agustus" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oktober" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nopember" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Desember" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListCariTahun" runat="server" CssClass="select2 pull-right" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCari_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <asp:UpdateProgress ID="updateProgressTanggal" runat="server" AssociatedUpdatePanelID="UpdatePanelTanggal">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                <asp:Image ID="imgUpdateProgressTanggal" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group">
                <div class="card">
                    <div class="card-header bg-smoke">
                        <ul class="nav nav-tabs card-header-tabs">
                            <li class="nav-item"><a href="#proses" class="nav-link active" data-toggle="tab">Proses</a></li>
                            <li class="nav-item"><a href="#selesai" class="nav-link" data-toggle="tab">Selesai</a></li>
                        </ul>
                    </div>
                    <div class="card-body">
                        <div class="tab-content">
                            <div class="tab-pane active" id="proses">
                                <asp:UpdatePanel ID="UpdatePanelProses" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th>No.</th>
                                                        <th>ID</th>
                                                        <th>Pengirim</th>
                                                        <th>Tanggal</th>
                                                        <th>Kirim</th>
                                                        <th>Tujuan</th>
                                                        <th>Jumlah</th>
                                                        <th>Subtotal</th>
                                                        <th>Status</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterTransferProses" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td class="fitSize"><a href="/WITAdministrator/Produk/Transfer/Detail.aspx?id=<%# Eval("IDTransferProduk") %>"><%# Eval("IDTransferProduk") %></a></td>
                                                                <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                                                                <td><%# Eval("TanggalKirim").ToFormatTanggal() %></td>
                                                                <td><%# Eval("TBTempat.Nama") %></td>
                                                                <td><%# Eval("TBTempat1.Nama") %></td>
                                                                <td class="text-right fitSize"><%# Eval("TotalJumlah").ToFormatHargaBulat() %></td>
                                                                <td class="text-right fitSize"><%# Eval("GrandTotalHargaJual").ToFormatHarga() %></td>
                                                                <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
                                                                <td class="text-right fitSize">
                                                                    <a class="btn btn-outline-primary btn-xs" href="Pengaturan.aspx?id=<%# Eval("IDTransferProduk") %>">Terima</a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                        <asp:UpdateProgress ID="updateProgressProses" runat="server" AssociatedUpdatePanelID="UpdatePanelProses">
                                            <ProgressTemplate>
                                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                    <asp:Image ID="imgUpdateProgressProses" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="selesai">
                                <asp:UpdatePanel ID="UpdatePanelSelesai" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th>No.</th>
                                                        <th>ID</th>
                                                        <th>Pengirim</th>
                                                        <th>Tanggal</th>
                                                        <th>Kirim</th>
                                                        <th>Tujuan</th>
                                                        <th>Jumlah</th>
                                                        <th>Subtotal</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterTransferSelesai" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td class="fitSize"><a href="/WITAdministrator/Produk/Transfer/Detail.aspx?id=<%# Eval("IDTransferProduk") %>"><%# Eval("IDTransferProduk") %></a></td>
                                                                <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                                                                <td><%# Eval("TanggalKirim").ToFormatTanggal() %></td>
                                                                <td><%# Eval("TBTempat.Nama") %></td>
                                                                <td><%# Eval("TBTempat1.Nama") %></td>
                                                                <td class="text-right fitSize"><%# Eval("TotalJumlah").ToFormatHargaBulat() %></td>
                                                                <td class="text-right fitSize"><%# Eval("GrandTotalHargaJual").ToFormatHarga() %></td>
                                                                <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                        <asp:UpdateProgress ID="updateProgressSelesai" runat="server" AssociatedUpdatePanelID="UpdatePanelSelesai">
                                            <ProgressTemplate>
                                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                    <asp:Image ID="imgUpdateProgressSelesai" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

