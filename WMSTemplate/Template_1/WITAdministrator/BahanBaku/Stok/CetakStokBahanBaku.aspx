<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="CetakStokBahanBaku.aspx.cs" Inherits="WITAdministrator_BahanBaku_Stok_CetakStokBahanBaku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <h1>
                <img src="/images/logo.jpg">
            </h1>
        </div>
        <div class="col-xs-6 text-right">
            <h1>Stok Bahan Baku</h1>
            <div style="font-size: 17px; font-weight: bold;">
                <asp:Label ID="LabelTempatStok" runat="server"></asp:Label>
            </div>
            <div>
                Print :
                    <asp:Label ID="LabelTanggalPrint" runat="server"></asp:Label><br />
                <asp:Label ID="LabelNamaPengguna" runat="server"></asp:Label><br />
                <asp:Label ID="LabelTempatPengguna" runat="server"></asp:Label>
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
    </div>

    <div class="row">
        <div class="col-md-12">
            <h3>
                <asp:Label ID="LabelSubtotal" runat="server" class="form-label bold pull-right" Text="Subtotal : 0"></asp:Label></h3>
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center" style="width: 2%">No</th>
                            <th class="text-center">Kode</th>
                            <th class="text-center">Bahan Baku</th>
                            <th class="text-center">Kategori</th>
                            <th class="text-center">Harga Beli</th>
                            <th class="text-center">Batas Stok</th>
                            <th class="text-center">Stok</th>
                            <th class="text-center">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterStokBahanBakuBisaDihitung" runat="server">
                            <ItemTemplate>
                                <tr class="gradeX">
                                    <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                    <td><%# Eval("KodeBahanBaku") %></td>
                                    <td><%# Eval("BahanBaku") %></td>
                                    <td><%# Eval("KategoriBahanBaku") %></td>
                                    <td class="text-right"><%# Eval("HargaBeli").ToFormatHarga() %> /<%# Eval("Satuan") %></td>
                                    <td class="text-right"><%# Eval("JumlahMinimum").ToFormatHarga() %> <%# Eval("Satuan") %></td>
                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %> <%# Eval("Satuan") %></td>
                                    <td class="text-center"><%# StokBahanBaku_Class.StatusStokBahanBaku(Eval("Status").ToString()) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <table style="width: 100%;">
        <tr>
            <td class="text-center">THANK YOU</td>
        </tr>
        <tr>
            <td class="text-center"><b>Warehouse Management System Powered by WIT.</b></td>
        </tr>
    </table>
</asp:Content>

