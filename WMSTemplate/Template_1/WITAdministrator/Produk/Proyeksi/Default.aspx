<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Proyeksi_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
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
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>ID Proyeksi</th>
                                <th>Pegawai</th>
                                <th>Tanggal</th>
                                <th>Selesai</th>
                                <th>Total</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariIDProyeksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariPegawai" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                    </asp:DropDownList></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariStatus" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_Cari">
                                        <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Proses" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Selesai" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Batal" Value="3"></asp:ListItem>
                                    </asp:DropDownList></th>
                                <th class="fitSize">
                                    <asp:Button ID="ButtonCari" runat="server" Text="Cari" class="btn btn-outline-light d-none" ClientIDMode="Static" OnClick="Event_Cari" />
                                    <asp:Button ID="ButtonTambah" runat="server" Text="Tambah" class="btn btn-success btn-block" OnClick="ButtonTambah_Click" /></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterProyeksi" runat="server" OnItemCommand="RepeaterProyeksi_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><a href='<%# "Detail.aspx?id=" + Eval("IDProyeksi") %>'><%# Eval("IDProyeksi") %></a></td>
                                        <td><%# Eval("Pegawai") %></td>
                                        <td><%# Eval("TanggalProyeksi").ToFormatTanggal() %></td>
                                        <td><%# Eval("TanggalSelesai").ToFormatTanggal() %></td>
                                        <td class="text-right"><%# Eval("Totaljumlah").ToFormatHargaBulat() %></td>
                                        <td class="text-center fitSize"><%# Eval("Status") %></td>
                                        <td class="fitSize">
                                            <asp:Button ID="ButtonSelesai" runat="server" CssClass="btn btn-primary btn-xs" Style="margin-bottom: 0px" Text="Selesai" CommandName="Selesai" CommandArgument='<%# Eval("IDProyeksi") %>' Visible='<%# Eval("Selesai") %>' OnClientClick='<%# "return confirm(\"Are you sure projection " + Eval("IDProyeksi") + " has been completed?\")" %>' />
                                            <asp:Button ID="ButtonCetak" runat="server" class="btn btn-light btn-xs" Style="margin-bottom: 0px" Text="Cetak" CommandName="Cetak" CommandArgument='<%# Eval("IDProyeksi") %>' OnClientClick='<%# Eval("CetakRAP") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

