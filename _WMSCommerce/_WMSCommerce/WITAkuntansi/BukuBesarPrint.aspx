<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="BukuBesarPrint.aspx.cs" Inherits="WITAkuntansi_BukuBesarPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <h1>
                <img src="/images/logo.jpg">
            </h1>
        </div>
        <div class="col-xs-6 text-right">
            <h3>General Ledger</h3>
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
    <table class="table table-bordered table-condensed">
        <h4><asp:Label runat="server" ID="LabelNamaAkun" Text=""></asp:Label></h4>
        <thead>
            <tr>
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
                        <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                        <td><%# Eval("Referensi") %></td>
                        <td><%# Eval("Keterangan") %></td>
                        <td class="text-right"><%# Eval("Debit")  %></td>
                        <td class="text-right"><%# Eval("Kredit") %></td>
                        <td class="text-right"><%# decimal.Parse(Eval("Saldo").ToString()) < 0 ? "<span style='color:red;'>" + Eval("Saldo").ToFormatHarga() + "</span>" : Eval("Saldo").ToFormatHarga() %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

