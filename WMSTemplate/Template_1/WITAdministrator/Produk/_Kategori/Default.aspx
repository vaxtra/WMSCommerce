<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Atribut_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Kategori Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" style="font-weight: bold;" class="btn btn-sm btn-primary hidden-print">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="table-responsive">
        <table class="table table-bordered table-condensed table-hover TableSorter">
            <thead>
                <tr class="active">
                    <th class="fitSize">No.</th>
                    <th>Kategori Utama</th>
                    <th>Nama</th>
                    <th>Deskripsi</th>
                    <th class="fitSize">Active</th>
                    <th class="fitSize hidden-print"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterProdukKategori" runat="server" OnItemCommand="RepeaterProdukKategori_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("KategoriUtama") %></td>
                            <td><%# Eval("Nama") %></td>
                            <td><%# Eval("Deskripsi") %></td>
                            <td><%# Pengaturan.FormatDataStatus(Eval("IsActive")) %></td>
                            <td class="fitSize hidden-print">
                                <a class="btn btn-xs btn-primary" href="Pengaturan.aspx?id=<%# Eval("IDProdukKategori") %>">Ubah</a>
                                <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-xs btn-danger" CommandName="Hapus" CommandArgument='<%# Eval("IDProdukKategori") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

