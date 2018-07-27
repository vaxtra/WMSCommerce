<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="ViewStoreKey.aspx.cs" Inherits="WITAdministrator_ViewStoreKey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="table-responsive">
            <table class="table table-condensed table-hover table-bordered">
                <tr>
                    <td>No</td>
                    <td>IDStore Key</td>
                    <td>IDStore</td>
                    <td>IDPengguna Aktif</td>
                    <td>Tanggal Key</td>
                    <td>Tanggal Aktif</td>
                    <td>Is Aktif</td>
                </tr>
                <asp:Repeater ID="RepeaterData" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("IDStoreKey") %></td>
                            <td><%# Eval("IDStore") %></td>
                            <td><%# Eval("IDPenggunaAKtif") %></td>
                            <td><%# Eval("TanggalKey").ToFormatTanggal() %></td>
                            <td><%# Eval("TanggalAktif").ToFormatTanggal() %></td>
                            <td><%# Eval("IsAKtif") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>


