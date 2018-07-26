<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="NeracaSaldo.aspx.cs" Inherits="WITAkuntansi_NeracaSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Neraca Saldo
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
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="form-group">
                <div class="form-inline">
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:DropDownList ID="DropDownListBulan" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="ButtonCari_Click" />
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    Neraca Saldo
                </div>
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>Kode - Nama</th>
                            <th class="text-right">Debit</th>
                            <th class="text-right">Kredit</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterGrupAkun" runat="server">
                            <ItemTemplate>
                                <tr class="bold warning">
                                    <td colspan="3"><%# Eval("Nama") %></td>
                                </tr>

                                <asp:Repeater ID="RepeaterAkun" runat="server" DataSource='<%# Eval("Akuns") %>'>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Kode") + " - " + Eval("Nama") %></td>
                                            <asp:Repeater ID="RepeaterSaldo" runat="server" DataSource='<%# Eval("Results") %>'>
                                                <ItemTemplate>
                                                    <td class="text-right"><%# (Eval("Value").ToFormatHarga() != "0") ? Eval("Value").ToFormatHarga() : "" %></td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>

                        <tr class="success bold">
                            <td>Total</td>
                            <td class="text-right">
                                <asp:Label ID="LabelDebit" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelKredit" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

