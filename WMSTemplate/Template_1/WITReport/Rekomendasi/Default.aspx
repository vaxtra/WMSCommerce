<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITReport_Rekomendasi_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Rekomendasi
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
    <div class="panel panel-success">
        <div class="panel-heading">
            <asp:Label ID="LabelJudulRekomendasi" runat="server"></asp:Label>
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-hover" style="font-size: 12px; font-family: Verdana, Geneva, Tahoma, sans-serif;">
                <thead>
                    <tr class="active">
                        <th class="text-center">
                            <asp:Label ID="LabelJenisRekomendasi" runat="server"></asp:Label></th>
                        <th class="text-center">Rekomendasi</th>
                        <th class="text-center">Jumlah</th>
                        <th class="text-center">Nilai</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterProduk" runat="server">
                        <ItemTemplate>
                            <tr class="success">
                                <td colspan="2" style="font-weight: bold;"><%# Eval("Nama") %></td>
                                <td class="text-right" style="font-weight: bold;"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                <td class="text-right" style="font-weight: bold;"><%# Eval("Nilai").ToFormatHarga() %></td>
                            </tr>
                            <asp:Repeater ID="RepeaterRekomendasi" runat="server" DataSource='<%# Eval("Rekomendasi") %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td></td>
                                        <td><%# Eval("TBProduk1.Nama") %></td>
                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                        <td class="text-right"><%# Eval("Nilai").ToFormatHarga() %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="RepeaterKategori" runat="server">
                        <ItemTemplate>
                            <tr class="success">
                                <td colspan="2" style="font-weight: bold;"><%# Eval("Nama") %></td>
                                <td class="text-right" style="font-weight: bold;"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                <td class="text-right" style="font-weight: bold;"><%# Eval("Nilai").ToFormatHarga() %></td>
                            </tr>
                            <asp:Repeater ID="RepeaterRekomendasi" runat="server" DataSource='<%# Eval("Rekomendasi") %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td></td>
                                        <td><%# Eval("TBKategoriProduk1.Nama") %></td>
                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                        <td class="text-right"><%# Eval("Nilai").ToFormatHarga() %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

