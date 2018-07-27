<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Post_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Blog
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
            <table class="table table-hover table-condensed" id="example">
                <thead>
                    <tr class="active">
                        <th>ID</th>
                        <th>Image</th>
                        <th>Title</th>
                        <th>Date</th>
                        <th>Content</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterKonten" runat="server" OnItemCommand="RepeaterKonten_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="active"><%# Eval("IDKonten") %></td>
                                <td>
                                    <img src='/images/konten/<%# Eval("IDKonten") %>.jpg' style="width: 45px;" /></td>
                                <td><%# Eval("Judul") %></td>
                                <td class="fitSize"><%# ((DateTime)Eval("Tanggal")).ToString("d MMMM yyyy") %></td>
                                <td><%# Eval("IsiSingkat") %></td>
                                <td class="text-right fitSize">
                                    <a class="btn btn-primary btn-xs" href="Pengaturan.aspx?id=<%# Eval("IDKonten") %>">Ubah</a>
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

