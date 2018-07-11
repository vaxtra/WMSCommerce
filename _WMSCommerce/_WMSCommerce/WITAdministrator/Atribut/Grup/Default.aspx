<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Atribut_Grup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Grup Atribut
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
                    <th>Nama</th>

                    <th class="fitSize hidden-print"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterAtributGrup" runat="server" OnItemCommand="RepeaterAtributGrup_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Nama") %></td>

                            <td class="fitSize hidden-print">
                                <a class="btn btn-xs btn-primary" href="Pengaturan.aspx?id=<%# Eval("IDAtributGrup") %>">Ubah</a>
                                <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-xs btn-danger" CommandName="Hapus" CommandArgument='<%# Eval("IDAtributGrup") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
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

