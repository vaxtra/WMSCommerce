<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITSurvey_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-6 text-right">
            <h3>Soal</h3>
        </div>
        <div class="col-md-6">
            <h3>
                <a href="Pengaturan.aspx" class="btn btn-sm btn-primary">Tambah</a>
                <a href="Import.aspx" class="btn btn-sm btn-primary">Import</a>
            </h3>
        </div>
    </div>

    <table class="table table-condensed table-hover" style="font-size: 12px;">
        <thead>
            <tr class="active">
                <th>No</th>
                <th>Judul</th>
                <th>Mulai</th>
                <th>Selesai</th>
                <th>Keterangan</th>
                <th>Status</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="RepeaterSoal" runat="server" OnItemCommand="RepeaterSoal_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 %></td>
                        <td><a href='Survei.aspx?id=<%# Eval("IDSoal") %>' target="_blank"><%# Eval("Judul") %></a></td>
                        <td><%# Eval("TanggalMulai").ToFormatTanggal() %></td>
                        <td><%# Eval("TanggalSelesai").ToFormatTanggal() %></td>
                        <td><%# Eval("Keterangan") %></td>
                        <td><%# Pengaturan.StatusSoal(Eval("EnumStatusSoal")) %></td>
                        <td>
                            <a href="Pengaturan.aspx?id=<%# Eval("IDSoal") %>" class="btn btn-primary btn-xs">Ubah</a>
                            <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDSoal") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

