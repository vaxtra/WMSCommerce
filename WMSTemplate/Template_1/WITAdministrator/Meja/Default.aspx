<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Meja_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Konfigurasi Meja
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
    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>

    <div class="form-group">
        <div class="card">
            <h4 class="card-header bg-smoke">Reguler</h4>
            <div class="card-body">
                <div class="table-responsive">
                    <table style="width: 100%;">
                        <asp:Repeater ID="RepeaterReguler" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:Repeater ID="RepeaterMeja" runat="server" OnItemCommand="RepeaterMeja_ItemCommand" DataSource='<%# Eval("baris") %>'>
                                        <ItemTemplate>
                                            <td>
                                                <table class="table table-sm table-bordered" style="width: 100%;">
                                                    <tr class='<%# Eval("Status").ToBool() == true ? "table-success" : string.Empty %>'>
                                                        <td class="text-center" colspan="2">
                                                            <h4><strong><a href='<%# "Pengaturan.aspx?id=" + Eval("IDMeja") %>'><%# Eval("Nama") %></a></strong></h4>
                                                        </td>
                                                    </tr>
                                                    <tr class='<%# Eval("Status").ToBool() == true ? "table-success" : string.Empty %>'>
                                                        <td style="width: 50%;">
                                                            <asp:Button ID="ButtonVIP" runat="server" Text="VIP" CommandName="VIP" CommandArgument='<%# Eval("IDMeja") %>' CssClass='<%# Eval("VIP").ToBool() == true ? "btn btn-success btn-sm btn-block" : "btn btn-default btn-outline btn-sm btn-block" %>' /></td>
                                                        <td style="width: 50%;">
                                                            <asp:Button ID="ButtonAktif" runat="server" Text="AKTIF" CommandName="Status" CommandArgument='<%# Eval("IDMeja") %>' CssClass='<%# Eval("Status").ToBool() == true ? "btn btn-success btn-sm btn-block" : "btn btn-default btn-outline btn-sm btn-block" %>' /></td>
                                                    </tr>
                                                    <tr class='<%# Eval("Status").ToBool() == true ? "table-success" : string.Empty %>'>
                                                        <td colspan="3">
                                                            <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDMeja") %>' CssClass="btn btn-danger btn-sm btn-block" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <h4 class="card-header bg-smoke">VIP</h4>
            <div class="card-body">
                <div class="table-responsive">
                    <table style="width: 100%;">
                        <asp:Repeater ID="RepeaterVIP" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:Repeater ID="RepeaterMeja" runat="server" OnItemCommand="RepeaterMeja_ItemCommand" DataSource='<%# Eval("baris") %>'>
                                        <ItemTemplate>
                                            <td>
                                                <table class="table table-sm table-bordered" style="width: 100%;">
                                                    <tr class='<%# Eval("Status").ToBool() == true ? "table-success" : string.Empty %>'>
                                                        <td class="text-center" colspan="2">
                                                            <h4><strong><a href='<%# "Pengaturan.aspx?id=" + Eval("IDMeja") %>'><%# Eval("Nama") %></a></strong></h4>
                                                        </td>
                                                    </tr>
                                                    <tr class='<%# Eval("Status").ToBool() == true ? "table-success" : string.Empty %>'>
                                                        <td style="width: 50%;">
                                                            <asp:Button ID="ButtonVIP" runat="server" Text="VIP" CommandName="VIP" CommandArgument='<%# Eval("IDMeja") %>' CssClass='<%# Eval("VIP").ToBool() == true ? "btn btn-success btn-mini btn-block" : "btn btn-default btn-mini btn-block" %>' /></td>
                                                        <td style="width: 50%;">
                                                            <asp:Button ID="ButtonAktif" runat="server" Text="AKTIF" CommandName="Status" CommandArgument='<%# Eval("IDMeja") %>' CssClass='<%# Eval("Status").ToBool() == true ? "btn btn-success btn-mini btn-block" : "btn btn-default btn-mini btn-block" %>' /></td>
                                                    </tr>
                                                    <tr class='<%# Eval("Status").ToBool() == true ? "table-success" : string.Empty %>'>
                                                        <td colspan="3">
                                                            <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDMeja") %>' CssClass="btn btn-danger btn-sm btn-block" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

