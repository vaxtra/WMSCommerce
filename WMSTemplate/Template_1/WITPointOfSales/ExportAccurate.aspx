<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="ExportAccurate.aspx.cs" Inherits="WITPointOfSales_ExportAccurate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <h3 class="text-center">Export Accurate</h3>
    </div>

    <div class="row hidden-print" style="margin-bottom: 10px;">
        <table>
            <tr>
                <td>
                    <div class="btn-group" style="margin: 5px 5px 0 0;">
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonHariIni" runat="server" Text="Hari Ini" OnClick="ButtonHariIni_Click" />
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonMingguIni" runat="server" Text="Minggu Ini" OnClick="ButtonMingguIni_Click" />
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonBulanIni" runat="server" Text="Bulan Ini" OnClick="ButtonBulanIni_Click" />
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonTahunIni" runat="server" Text="Tahun Ini" OnClick="ButtonTahunIni_Click" />
                    </div>
                </td>
                <td>
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonKemarin" runat="server" Text="Kemarin" OnClick="ButtonKemarin_Click" />
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonMingguLalu" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguLalu_Click" />
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonBulanLalu" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanLalu_Click" />
                        <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonTahunLalu" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunLalu_Click" />
                    </div>
                </td>
                <td>
                    <div style="margin: 5px 5px 0 0">
                        <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div style="margin: 5px 5px 0 0">
                        <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" Style="margin: 5px 5px 0 0" ID="ButtonExport" runat="server" Text="Export" OnClick="ButtonExport_Click" />
                </td>
            </tr>
        </table>
    </div>

    <asp:Literal ID="LiteralResult" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

