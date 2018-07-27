<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="CetakPenerimaan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_CetakPenerimaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-condensed table-hover table-bordered" style="width: 100%">
                <tr class="text-center">
                    <td rowspan="2" style="width: 30%">
                        <img src="/images/logo.jpg" style="width:80%;"><br />
                        <asp:Label ID="LabelNamaStore" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <h3>GOODS RECEIPT</h3>
                        <h3>
                            <asp:Label ID="LabelJudul" runat="server"></asp:Label></h3>
                        <br />
                        <h3>
                            <asp:Label ID="LabelIDPenerimaanPOProduksiProduk" runat="server" Text="-"></asp:Label></h3>
                    </td>
                    <td rowspan="2" style="width: 30%">
                        <asp:Label ID="LabelNamaVendor" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatVendor" runat="server"></asp:Label><br />
                    </td>
                </tr>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="text-center active">
                    <td style="width: 25%"><b>ID Produksi</b></td>
                    <td style="width: 25%"><b>ID Penerimaan</b></td>
                    <td style="width: 25%"><b>Tanggal</b></td>
                    <td style="width: 25%"><b>Penerima</b></td>
                </tr>
                <tr class="text-center">
                    <td style="width: 25%">
                        <asp:Label ID="LabelIDProduksi" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="LabelIDPenerimaan" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="LabelTanggal" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="LabelPenerima" runat="server" Text="-"></asp:Label></td>
                </tr>
            </table>

            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr class="active">
                        <th class="text-center" style="width: 2%;">No</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center" style="width: 5%;">Varian</th>
                        <th class="text-center" style="width: 12.5%;">Datang</th>
                        <th class="text-center" style="width: 12.5%;">Diterima</th>
                        <th class="text-center" style="width: 12.5%;">Tolak Supplier</th>
                        <th class="text-center" style="width: 12.5%;">Tolak Gudang</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDetail" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("TBKombinasiProduk.TBProduk.Nama") %></td>
                                <td class="text-center"><%# Eval("TBKombinasiProduk.TBAtributProduk.Nama") %></td>
                                <td class="text-right"><%# Eval("Datang").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Diterima").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("TolakKeVendor").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("TolakKeGudang").ToFormatHargaBulat() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="text-center">
                    <td style="width: 30%">
                        <b>PEMBUAT</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 30%">
                        <b>PIC</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 30%">
                        <b>PENERIMA</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>

            <table style="width: 100%;">
                <tr>
                    <td class="text-center">THANK YOU</td>
                </tr>
                <tr>
                    <td class="text-center"><b>Warehouse Management System Powered by WIT.</b></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

