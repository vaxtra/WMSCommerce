<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="JurnalUmum.aspx.cs" Inherits="WITAkuntansi_JurnalUmum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Journal History
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" CssClass="btn btn-default btn-sm" Text="Print" />
    <asp:Button ID="ButtonTambah" runat="server" CssClass="btn btn-success btn-sm" Text="Tambah" OnClick="ButtonTambah_Click" />
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
                        <asp:DropDownList ID="DropDownListAkun" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBoxTanggalPeriode1" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                        <asp:TextBox ID="TextBoxTanggalPeriode2" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                        <asp:Button ID="ButtonCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="ButtonCari_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            Journal History
        </div>
        <table class="table table-condensed table-hover table-bordered">
            <thead>
                <tr class="active">
                    <th>Tanggal</th>
                    <th>Nama Lengkap</th>
                    <th></th>
                    <th>Keterangan</th>
                    <th></th>
                </tr>
                <tr>
                    <th>
                        <asp:DropDownList ID="DropDownListSortBy" CssClass="select2" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ButtonCari_Click">
                            <asp:ListItem Text="Tanggal Jurnal" Value="0">
                            </asp:ListItem>
                            <asp:ListItem Text="Tanggal Pembuatan" Value="1">
                            </asp:ListItem>
                        </asp:DropDownList></th>
                    <th>
                        <asp:DropDownList ID="DropDownListPengguna" CssClass="select2" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCari_Click">
                        </asp:DropDownList>
                    </th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterJurnal" runat="server" OnItemCommand="RepeaterJurnal_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# (Eval("Tanggal").ToDateTime()) + " - " + Eval("IDJurnal")%></td>
                            <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                            <td>
                                <table class="table table-bordered table-condensed">
                                    <thead>
                                        <tr>
                                            <th class="text-center">Akun</th>
                                            <th class="text-right">Debit</th>
                                            <th class="text-right">Kredit</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("TBJurnalDetails") %>'>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("TBAkun.Kode") + " - " + Eval("TBAkun.Nama") %></td>
                                                    <td class="text-right"><%# ((Eval("Debit")).ToFormatHarga() != "0") ? (Eval("Debit")).ToFormatHarga() : "" %></td>
                                                    <td class="text-right"><%# ((Eval("Kredit")).ToFormatHarga() != "0") ? (Eval("Kredit")).ToFormatHarga() : "" %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </td>
                            <td>
                                <div><%# Eval("Keterangan") %></div>
                                <br />
                                <div><em>Ref : </em><%# Eval("Referensi") %></div>

                                <asp:Repeater ID="RepeaterDokumen" runat="server" DataSource='<%# Eval("TBJurnalDokumens") %>'>
                                    <ItemTemplate>
                                        <br />
                                        <%# "<a href='/files/Akuntansi/" + Eval("IDJurnalDokumen") + Eval("Format") + "'>Download</a>"  %>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButtonCetakFormPenerimaan" runat="server" CommandName="Edit" CommandArgument='<%# Eval("IDJurnal") %>' OnClientClick='<%# Eval("PopUpEdit") %>' Visible='<%# Eval("StatusEdit") %>' CssClass="btn btn-danger btn-sm">Edit</asp:LinkButton>
                                <asp:Button ID="ButtonJurnalPembalik" runat="server" CssClass="btn btn-danger btn-sm" Text="Jurnal Pembalik" CommandName="JurnalPembalik" CommandArgument='<%# Eval("IDJurnal") %>'
                                    OnClientClick="if (!confirm('Are you sure to do this?')) return false;" />
                                <%--                                    <asp:Button ID="ButtonReprint" runat="server" CssClass="btn btn-default btn-sm" Text="Cash In" CommandName="CashIn" CommandArgument='<%# Eval("IDJurnal") %>'
                                        OnClientClick="if (!confirm('Are you sure to do this?')) return false;" />
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default btn-sm" Text="Cash Out" CommandName="CashOut" CommandArgument='<%# Eval("IDJurnal") %>'
                                        OnClientClick="if (!confirm('Are you sure to do this?')) return false;" />--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

