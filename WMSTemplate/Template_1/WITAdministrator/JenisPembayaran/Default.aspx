<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_JenisPembayaran_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Jenis Pembayaran
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-const">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <table class="table table-sm table-hover table-bordered mb-0">
                    <thead>
                        <tr class="thead-light">
                            <th>No.</th>
                            <th>Nama</th>
                            <th>Beban Biaya</th>
                            <th>Biaya (%)</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterJenisPembayaran" runat="server" OnItemCommand="RepeaterJenisPembayaran_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                    <td><%# Eval("Nama") %></td>
                                    <td><%# Eval("TBJenisBebanBiaya.Nama") %></td>
                                    <td><%# Eval("PersentaseBiaya").ToFormatHarga() %></td>
                                    <td class="text-right fitSize">
                                        <a class="btn btn-xs btn-info" href="Pengaturan.aspx?id=<%# Eval("IDJenisPembayaran") %>">Ubah</a>
                                        <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-danger btn-xs " CommandName="Hapus" CommandArgument='<%# Eval("IDJenisPembayaran") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

