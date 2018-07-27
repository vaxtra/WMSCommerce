<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="IndexHutang.aspx.cs" Inherits="WITReport_Transaksi_IndexHutang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Index Hutang
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonCari" runat="server" Text="Cari" OnClick="ButtonCari_Click" CssClass="btn btn-default" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel File" OnClick="ButtonExcel_Click" CssClass="btn btn-default" />
    <a id="LinkDownloadVendor" runat="server" visible="false">Download File</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            Index Hutang
        </div>
        <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
            <thead>
                <tr>
                    <th>No.</th>
                    <th>Pelanggan</th>
                    <th>Total Sales (Rp.)</th>
                    <th>Total Hutang (Rp.)</th>
                    <th>Hutang Tidak Ril (Rp.)</th>
                    <th>Index Hutang</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <asp:Repeater ID="RepeaterIndexHutang" runat="server">
                        <ItemTemplate>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Nama") %></td>
                            <td><%# Eval("DataSales").ToFormatHarga() %></td>
                            <td><%# Eval("DataHutang").ToFormatHarga() %></td>
                            <td><%# Eval("HutangUnreal").ToFormatHarga() %></td>
                            <td><%# Pengaturan.CekNullValueDesimal(Eval("Index")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

