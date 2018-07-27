<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="BukuBesar.aspx.cs" Inherits="WITAkuntansi_BukuBesar" %>

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
    General Ledger
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExport" runat="server" CssClass="btn btn-default btn-sm" Text="Export" OnClick="ButtonExport_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
    <asp:Button ID="ButtonPrint" runat="server" CssClass="btn btn-default btn-sm" Text="Cetak" />
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
            <div class="panel panel-success">
                <div class="panel-heading">
                    General Ledger
                </div>
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="active">
                            <th>No</th>
                            <th>Tanggal</th>
                            <th>No. Referensi</th>
                            <th>Keterangan</th>
                            <th class="text-right">Debit</th>
                            <th class="text-right">Kredit</th>
                            <th class="text-right">Balance</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterBukuBesar" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %></td>
                                    <td><%# (Eval("Tanggal").ToFormatTanggal()) %></td>
                                    <td><%# Eval("Referensi") %></td>
                                    <td><%# Eval("Keterangan") %></td>
                                    <td class="text-right"><%# Eval("Debit")  %></td>
                                    <td class="text-right"><%# Eval("Kredit") %></td>
                                    <td class="text-right"><%# (Eval("Saldo").ToDecimal()) < 0 ? "<span style='color:red;'>" + (Eval("Saldo").ToFormatHarga()) + "</span>" : (Eval("Saldo").ToFormatHarga()) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

