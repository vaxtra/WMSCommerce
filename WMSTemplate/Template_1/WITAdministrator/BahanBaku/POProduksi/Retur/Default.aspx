<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_Retur_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
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
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>ID</th>
                                <th colspan="2">Tanggal</th>
                                <th>Pegawai</th>
                                <th>Supplier</th>
                                <th>Grandtotal</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxCariIDPOProduksiBahanBakuRetur" runat="server" CssClass="form-control input-sm text-uppercase" Width="100%" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariBulan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
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
                                </th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariTahun" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                    </asp:DropDownList>
                                </th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariPegawai" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariSupplier" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                    </asp:DropDownList></th>
                                <th></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariStatus" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                        <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Baru" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Proses" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Selesai" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Batal" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                                <th class="fitSize">
                                    <asp:Button ID="ButtonCari" runat="server" Text="Cari" class="btn btn-outline-light d-none" OnClick="Event_Cari" />
                                    <asp:Button ID="ButtonTambah" runat="server" Text="Tambah" class="btn btn-success btn-block" OnClick="ButtonTambah_Click" /></th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><a href='<%# "Detail.aspx?id=" + Eval("IDPOProduksiBahanBakuRetur") %>'><%# Eval("IDPOProduksiBahanBakuRetur") %></a></td>
                                        <td colspan="2"><%# Eval("TanggalRetur").ToFormatTanggal() %></td>
                                        <td><%# Eval("Pegawai") %></td>
                                        <td><%# Eval("Supplier") %></td>
                                        <td class="text-right"><%# Eval("Grandtotal").ToFormatHarga() %></td>
                                        <td class="text-center"><%# Eval("Status") %></td>
                                        <td class="text-center fitSize">
                                            <asp:Button ID="ButtonCetak" CssClass="btn btn-light btn-xs" runat="server" Style="margin-bottom: 0px" CommandName="Cetak" CommandArgument='<%# Eval("IDPOProduksiBahanBakuRetur") %>' OnClientClick='<%# Eval("Cetak") %>' Text="Cetak" />
                                            <asp:Button ID="ButtonBatal" CssClass='<%# Eval("Batal")%>' runat="server" Style="margin-bottom: 0px" Text="Batal" CommandName="Batal" CommandArgument='<%# Eval("IDPOProduksiBahanBakuRetur") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
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

