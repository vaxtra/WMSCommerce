<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITAdministrator_Atribut_Produk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelJudul" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonOk" runat="server" Style="font-weight: bold;" CssClass="btn btn-sm btn-primary hidden-print" Text="Simpan" OnClick="ButtonOk_Click" />
    <asp:Button ID="ButtonKeluar" runat="server" Style="font-weight: bold;" CssClass="btn btn-sm btn-danger hidden-print" Text="Keluar" OnClick="ButtonKeluar_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="table-responsive">
        <table class="table table-bordered table-condensed table-hover TableSorter">
            <thead>
                <tr class="active">
                    <th class="fitSize">No.</th>
                    <th>Nama</th>
                    <th>Pilihan</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterAtribut" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Container.ItemIndex + 1 %>
                                <asp:HiddenField ID="HiddenFieldIDAtribut" runat="server" Value='<%# Eval("IDAtribut") %>' />
                            </td>
                            <td><%# Eval("Nama") %></td>
                            <td class="fitSize">
                                <asp:TextBox ID="TextBoxValue" runat="server" CssClass='<%# (bool)Eval("Pilihan") ? "Atribut" + Eval("IDAtribut") : "form-control" %>' Text='<%# Eval("Value") %>' Style="width: 500px;"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <asp:Literal ID="LiteralJavascript" runat="server"></asp:Literal>
</asp:Content>

