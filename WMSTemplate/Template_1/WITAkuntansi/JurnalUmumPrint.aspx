<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="JurnalUmumPrint.aspx.cs" Inherits="WITAkuntansi_JurnalUmumPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
        <div class="row">
        <div class="col-xs-6">
            <h1>
                <img src="/images/logo.jpg">
            </h1>
        </div>
        <div class="col-xs-6 text-right">
            <h3>Journal History</h3>
            <h5>
                <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h5>
            <div>
                Pencetak :
                <asp:Label ID="LabelNamaPencetak" runat="server"></asp:Label><br />
                Cetak    :
                <asp:Label ID="LabelTanggalCetak" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="LabelNamaStore" runat="server" Style="font-weight: bold;"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelTeleponStore" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelWebsite" runat="server"></asp:Label>
                    <br />
                    <asp:HyperLink ID="HyperLinkEmail" runat="server"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="col-xs-5 col-xs-offset-2 text-left">
        </div>
    </div>

    <table class="table table-bordered laporan">
            <thead>
                <tr>
                    <th class="text-center">Tanggal</th>
                    <th class="text-center">Nama Lengkap</th>
                    <th></th>
                    <th class="text-center">Keterangan</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterJurnal" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
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
                                                    <td class="text-right"><%# (Eval("Debit").ToFormatHarga() != "0") ? Eval("Debit").ToFormatHarga() : "" %></td>
                                                    <td class="text-right"><%# (Eval("Kredit").ToFormatHarga() != "0") ? Eval("Kredit").ToFormatHarga() : "" %></td>
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
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

<%--    <div class="col-xs-6">
        <h4>Pemasukan</h4>
        <table class="table table-bordered laporan">
            <thead>
                <tr class="active">
                    <th class="text-center">No.</th>
                    <th class="text-center">Akun</th>
                    <th class="text-center">Nominal</th>
                </tr>
            </thead>
            <tbody>
                <tr class="text-right success" style="font-weight: bold;">
                    <td class="text-center" colspan="2">TOTAL</td>
                    <td>
                        <asp:Label ID="LabelHeaderTotalNominalPemasukan" runat="server"></asp:Label></td>
                </tr>
                <asp:Repeater ID="RepeaterPemasukan" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Nama") %></td>
                            <td class="text-right"><%# Eval("Saldo").ToFormatHarga() %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="text-right success" style="font-size: 14px; font-weight: bold;">
                    <td class="text-center" colspan="2">TOTAL</td>
                    <td>
                        <asp:Label ID="LabelFooterTotalNominalPemasukan" runat="server"></asp:Label></td>
                </tr>
                <tr class="warning bold text-right" runat="server" id="panelRugi">
                    <td colsan="2">Rugi</td>
                    <td>
                        <asp:Label ID="LabelNominalRugi" runat="server" CssClass="label label-important"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <div class="col-xs-6">
        <h4>Pengeluaran</h4>
        <table class="table table-bordered laporan">
            <thead>
                <tr class="active">
                    <th class="text-center">No.</th>
                    <th class="text-center">Akun</th>
                    <th class="text-center">Nominal</th>
                </tr>
            </thead>
            <tbody>
                <tr class="text-right success" style="font-weight: bold;">
                    <td class="text-center" colspan="2">TOTAL</td>
                    <td>
                        <asp:Label ID="LabelHeaderTotalNominalPengeluaran" runat="server"></asp:Label></td>
                </tr>
                <asp:Repeater ID="RepeaterPengeluaran" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Nama") %></td>
                            <td class="text-right"><%# Eval("Saldo").ToFormatHarga() %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="text-right success" style="font-size: 14px; font-weight: bold;">
                    <td class="text-center" colspan="2">TOTAL</td>
                    <td>
                        <asp:Label ID="LabelFooterTotalNominalPengeluaran" runat="server"></asp:Label></td>
                </tr>
                <tr class="warning bold text-right" runat="server" id="panelLaba">
                    <td colspan="2">Laba</td>
                    <td>
                        <asp:Label ID="LabelNominalLaba" runat="server" CssClass="label label-success"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>--%>

    <table style="width: 100%;">
        <tr>
            <asp:Label ID="LabelFooterPrint" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="text-center">THANK YOU</td>
        </tr>
        <tr>
            <td class="text-center"><b>WIT Management System Powered by WIT.</b></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" Runat="Server">
</asp:Content>

