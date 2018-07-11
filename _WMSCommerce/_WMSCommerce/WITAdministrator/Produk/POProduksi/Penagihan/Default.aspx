<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Penagihan_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Bahan Baku
        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCari');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Invoice Purchase Order Product
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-sm">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <asp:UpdatePanel ID="UpdatePanelData" runat="server">
                <ContentTemplate>
                    <div class="table-responsive">
                        <table class="table the-table table-condensed table-hover">
                            <thead>
                                <tr class="active">
                                    <th>No</th>
                                    <th>ID</th>
                                    <th colspan="2">Tanggal</th>
                                    <th>Pegawai</th>
                                    <th>Vendor</th>
                                    <th>Tagihan</th>
                                    <th>Bayar</th>
                                    <th>Status</th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCariIDIDPOProduksiProdukPenagihan" runat="server" CssClass="form-control input-sm text-uppercase" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></td>
                                    <td>
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
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListCariTahun" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListCariPegawai" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListCariVendor" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                        </asp:DropDownList></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListCariStatus" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                            <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Tagihan" Value="False"></asp:ListItem>
                                            <asp:ListItem Text="Lunas" Value="True"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" class="btn btn-primary btn-sm btn-block" OnClick="Event_Cari" /></td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><a href='<%# "Detail.aspx?id=" + Eval("IDPOProduksiProdukPenagihan") %>'><%# Eval("IDPOProduksiProdukPenagihan") %></a></td>
                                            <td colspan="2"><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                            <td><%# Eval("Pegawai") %></td>
                                            <td><%# Eval("Vendor") %></td>
                                            <td class="text-right warning"><strong><%# Eval("TotalTagihan").ToFormatHarga() %></strong></td>
                                            <td class="text-right success"><strong><%# Eval("TotalBayar").ToFormatHarga() %></strong></td>
                                            <td class="text-center"><%# Eval("Status") %></td>
                                            <td class="fitSize">
                                                <a href='<%# "Pembayaran.aspx?id=" + Eval("IDPOProduksiProdukPenagihan") %>' class='<%# Eval("StatusPembayaran").ToBool() == false ? "btn btn-info btn-xs" : "hidden" %>'>Bayar</a>
                                                <asp:Button ID="ButtonCetak" CssClass="btn btn-default btn-xs" runat="server" Style="margin-bottom: 0px" CommandName="Cetak" CommandArgument='<%# Eval("IDPOProduksiProdukPenagihan") %>' OnClientClick='<%# Eval("Cetak") %>' Text="Cetak" />
                                                <asp:Button ID="ButtonHapus" CssClass='<%# Eval("StatusPembayaran").ToBool() == false ? "btn btn-danger btn-xs" : "btn btn-danger btn-xs hidden" %>' runat="server" Style="margin-bottom: 0px" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPOProduksiProdukPenagihan") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

