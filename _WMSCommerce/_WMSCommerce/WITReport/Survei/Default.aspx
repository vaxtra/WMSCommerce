<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITReport_Survei_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Survei
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
            Question
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-hover" style="font-size: 12px;">
                <thead>
                    <tr class="active">
                        <th style="width: 2%">No</th>
                        <th>Isi</th>
                        <th style="width: 50%;">
                            <table class="table table-condensed" style="margin-bottom: 0;">
                                <tr class="active">
                                    <td style="width: 50%;">Jawaban</td>
                                    <td style="width: 30%;">Persentase</td>
                                    <td style="width: 10%;" class="text-right">Jumlah</td>
                                    <td style="width: 10%;" class="text-right">Bobot</td>
                                </tr>
                            </table>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterSoalPertanyaan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Pertanyaan") %></td>
                                <td style="width: 50%;">
                                    <table class="table table-condensed table-hover">
                                        <asp:Repeater ID="RepeaterSoalJawaban" DataSource='<%# Eval("SoalJawabans") %>' runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 40%;"><%# Eval("Jawaban") %></td>
                                                    <td style="width: 30%;">
                                                        <div class="progress">
                                                            <div class="progress-bar" role="progressbar" aria-valuenow='<%# Eval("Persentase") %>' aria-valuemin="0" aria-valuemax="100" style='<%# "width: " + Eval("Persentase") + "%" %>'>
                                                                <%# Eval("Persentase") %>%
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td style="width: 15%;" class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                    <td style="width: 15%;" class="text-right"><%# Eval("Bobot").ToFormatHarga() %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
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

