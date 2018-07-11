<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Halaman_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Page
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
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr class="active">
                        <th>ID</th>
                        <th>Title</th>
                        <th>Content</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterKonten" runat="server" OnItemCommand="RepeaterKonten_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="fitSize"><%# Eval("IDKonten") %></td>
                                <td><%# Eval("Judul") %></td>
                                <td><%# Eval("IsiSingkat") %></td>
                                <td class="text-right fitSize">
                                    <a class="btn btn-info btn-xs" href="Pengaturan.aspx?id=<%# Eval("IDKonten") %>">Ubah</a>
                                    <asp:Button CssClass="btn btn-danger btn-xs" ID="ButtonHapus" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDKonten") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDKonten") + "\")" %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

