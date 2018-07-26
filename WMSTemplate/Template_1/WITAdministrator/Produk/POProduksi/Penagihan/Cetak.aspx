<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Cetak.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Penagihan_Cetak" %>

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
                        <h3>PENAGIHAN</h3>
                        <br />
                        <h3>
                            <asp:Label ID="LabelIDPOProduksiProdukPenagihan" runat="server" Text="-"></asp:Label></h3>
                    </td>
                    <td rowspan="2" style="width: 30%">
                        <asp:Label ID="LabelNamaVendor" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatVendor" runat="server"></asp:Label><br />
                    </td>
                </tr>
            </table>

            <div class="row">
                <div class="col-xs-6">
                    <table class="table table-bordered table-condensed" style="width: 100%">
                        <tr>
                            <td class="active fitSize"><b>Pegawai</b></td>
                            <td>
                                <asp:Label ID="LabelPegawai" runat="server" Text="-"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="active fitSize"><b>Status</b></td>
                            <td>
                                <asp:Label ID="LabelStatus" runat="server" Text="-"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="active fitSize"><b>Keterangan</b></td>
                            <td>
                                <asp:Label ID="LabelKeterangan" runat="server" Text="-"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-6">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="warning">
                                <th colspan="4">Penerimaan</th>
                            </tr>
                            <tr class="active">
                                <th>No</th>
                                <th>ID</th>
                                <th>Tanggal</th>
                                <th>Grandtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterDetail" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td style="width: 40%">
                                            <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPenerimaanPOProduksiProduk") %>'></asp:Label></td>
                                        <td style="width: 30%"><%# Eval("TanggalTerima").ToFormatTanggal() %></td>
                                        <td class="text-right"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="3" class="active text-center"><b>TOTAL</b></td>
                                <td class="text-right"><strong>
                                    <asp:Label ID="LabelTotalPenerimaan" runat="server" Text="0"></asp:Label></strong></td>
                            </tr>
                        </tbody>
                        <thead>
                            <tr class="danger">
                                <th colspan="4">Retur</th>
                            </tr>
                            <tr class="active">
                                <th>No</th>
                                <th>ID</th>
                                <th>Tanggal</th>
                                <th>Grandtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterRetur" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td style="width: 40%">
                                            <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPOProduksiProdukRetur") %>'></asp:Label></td>
                                        <td style="width: 30%"><%# Eval("TanggalRetur").ToFormatTanggal() %></td>
                                        <td class="text-right"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="3" class="active text-center"><b>TOTAL</b></td>
                                <td class="text-right"><strong>
                                    <asp:Label ID="LabelTotalRetur" runat="server" Text="0"></asp:Label></strong></td>
                            </tr>
                        </tbody>
                        <thead>
                            <tr class="info">
                                <th colspan="4">Down Payment</th>
                            </tr>
                            <tr class="active">
                                <th>No</th>
                                <th>ID</th>
                                <th>Tanggal</th>
                                <th>Down Payment</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterDownPayment" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td style="width: 40%">
                                            <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPOProduksiProduk") %>'></asp:Label></td>
                                        <td style="width: 30%"><%# Eval("TanggalDownPayment").ToFormatTanggal() %></td>
                                        <td class="text-right"><strong><%# Eval("DownPayment").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="3" class="active text-center"><b>TOTAL</b></td>
                                <td class="text-right"><strong>
                                    <asp:Label ID="LabelTotalDownPayment" runat="server" Text="0"></asp:Label></strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-xs-6">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="success">
                                <th colspan="5">Pembayaran</th>
                            </tr>
                            <tr class="active">
                                <th>No</th>
                                <th>Pegawai</th>
                                <th>Tanggal</th>
                                <th>Jenis Pembayaran</th>
                                <th>Bayar</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterPembayaran" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("Pegawai") %></td>
                                        <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                        <td><%# Eval("JenisPembayaran") %></td>
                                        <td class="text-right"><strong><%# Eval("Bayar").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="4" class="active text-center"><b>TOTAL</b></td>
                                <td class="text-right"><strong>
                                    <asp:Label ID="LabelTotalBayar" runat="server" Text="0"></asp:Label></strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

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

