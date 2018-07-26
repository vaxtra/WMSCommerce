<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Tempat_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Tempat
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
                            <th>No</th>
                            <th>Kategori</th>
                            <th>Nama</th>
                            <th>Alamat</th>
                            <th>Telepon</th>
                            <th>Telepon</th>
                            <th>Email</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterTempat" runat="server" OnItemCommand="RepeaterTempat_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                    <td class="fitSize"><%# Eval("Kategori") %></td>
                                    <td><%# Eval("Nama") %></td>
                                    <td><%# Eval("Alamat") %></td>
                                    <td class="fitSize"><%# Eval("Telepon1") %></td>
                                    <td class="fitSize"><%# Eval("Telepon2") %></td>
                                    <td class="fitSize"><%# Eval("Email") %></td>
                                    <td class="text-right fitSize">
                                        <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDTempat") %>' />
                                        <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDTempat") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

