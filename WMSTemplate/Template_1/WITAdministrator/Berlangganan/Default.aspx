<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Berlangganan_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Berlangganan
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-sm">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>No.</th>
                            <th>Email</th>
                            <th>Telepon</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterBerlangganan" runat="server" OnItemCommand="RepeaterBerlangganan_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                    <td><%# Eval("Email") %></td>
                                    <td><%# Eval("NoTelepon") %></td>
                                    <td class="text-right fitSize">
                                        <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDBerlangganan") %>' />
                                        <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDBerlangganan") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
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

