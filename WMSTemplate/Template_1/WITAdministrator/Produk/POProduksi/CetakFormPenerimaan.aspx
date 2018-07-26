<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="CetakFormPenerimaan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_CetakFormPenerimaan" %>

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
                        <img src="/images/logo.jpg" style="width: 80%;"><br />
                        <asp:Label ID="LabelNamaStore" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <h3>FORM RECEIPT</h3>
                        <h3>
                            <asp:Label ID="LabelJudul" runat="server"></asp:Label></h3>
                    </td>
                    <td rowspan="2" style="width: 30%">
                        <asp:Label ID="LabelNamaVendor" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatVendor" runat="server"></asp:Label><br />
                    </td>
                </tr>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="text-center active">
                    <td style="width: 25%"><b>ID Proyeksi</b></td>
                    <td style="width: 25%"><b>ID Produksi</b></td>
                    <td style="width: 50%"><b>Tanggal</b></td>
                </tr>
                <tr class="text-center">
                    <td style="width: 25%">
                        <asp:Label ID="LabelIDProyeksi" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="LabelIDProduksi" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 50%"></td>
                </tr>
            </table>

            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr class="active">
                        <th class="text-center" style="width: 2%;">No</th>
                        <th class="text-center" style="width: 5%;">Kode</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center" style="width: 5%;">Varian</th>
                        <th class="text-center" style="width: 15%;">Kategori</th>
                        <th class="text-center" style="width: 10%;">Jumlah</th>
                        <th class="text-center" style="width: 10%;">Sisa</th>
                        <th class="text-center" style="width: 10%;">Datang</th>
                        <th class="text-center" style="width: 10%;">Terima</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDetail" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("KodeKombinasiProduk") %></td>
                                <td><%# Eval("Produk") %></td>
                                <td class="text-center"><%# Eval("AtributProduk") %></td>
                                <td class="text-center"><%# Eval("Kategori") %></td>
                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Sisa").ToFormatHargaBulat() %></td>
                                <td class="text-right"></td>
                                <td class="text-right"></td>
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

