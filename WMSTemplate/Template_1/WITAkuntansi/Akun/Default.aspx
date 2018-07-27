<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAkuntansi_Akun_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Chart of Account
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonTambah" CssClass="btn btn-success btn-sm" runat="server" Text="Tambah" OnClick="ButtonTambah_Click" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    ASSETS
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="active">
                                <th class="text-center">NO</th>
                                <th class="text-center">KODE</th>
                                <th class="text-left">NAMA</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterLaporanAktiva" runat="server" OnItemCommand="RepeaterLaporanAktiva_ItemCommand">
                                <ItemTemplate>
                                    <tr <%# Eval("ClassWarna") %>>
                                        <td style="width: 30px;"><%# Eval("Nomor") %></td>
                                        <td style="width: 30px;"><%# Eval("Kode") %></td>
                                        <td <%# (Eval("Grup").ToString()).ToBool() == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                        <td class="fitSize">
                                            <asp:Button ID="ButtonUbah" CssClass="btn btn-primary btn-xs" runat="server" Text="Ubah" CommandName="Ubah"
                                                CommandArgument='<%# (Eval("Grup").ToString()).ToBool() == true ? "idAkunGrup=" + Eval("IDAkunGrup") : "id="+ Eval("IDAkun") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    LIABILITIES
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="active">
                                <th class="text-center">NO</th>
                                <th class="text-center">KODE</th>
                                <th class="text-left">NAMA</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterLaporanPasiva" runat="server" OnItemCommand="RepeaterLaporanPasiva_ItemCommand">
                                <ItemTemplate>
                                    <tr <%# Eval("ClassWarna") %>>
                                        <td style="width: 30px;"><%# Eval("Nomor") %></td>
                                        <td style="width: 30px;"><%# Eval("Kode") %></td>
                                        <td <%# (Eval("Grup").ToString()).ToBool() == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                        <td class="fitSize">
                                            <asp:Button ID="ButtonUbah" CssClass="btn btn-primary btn-xs" runat="server" Text="Ubah" CommandName="Ubah"
                                                CommandArgument='<%# (Eval("Grup").ToString()).ToBool() == true ? "idAkunGrup=" + Eval("IDAkunGrup") : "id="+ Eval("IDAkun") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

