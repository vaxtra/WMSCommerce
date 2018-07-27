<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Pengguna_PindahTempat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Sync
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonSync" CssClass="btn btn-default btn-sm" runat="server" Text="Mulai Sync" OnClick="ButtonSync_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Literal ID="LiteralData" runat="server"></asp:Literal>

<%--    <div class="row">
        <div class="col-xs-12 col-md-12 col-sm-10 col-lg-8">
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="active">
                            <th>No.</th>
                            <th>Nama</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterTempat" runat="server" OnItemCommand="RepeaterTempat_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                    <td><%# Eval("Nama") %></td>
                                    <td class="text-right fitSize">
                                        <asp:Button ID="ButtonPindah" runat="server" Text="Pindah" CssClass="btn btn-success btn-xs" CommandName="Pindah" CommandArgument='<%# Eval("IDTempat") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>--%>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

