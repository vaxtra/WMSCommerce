<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Cetak.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Retur_Cetak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div style="width: 100%; height: 100%; margin: 0; padding: 0; font-family: sans-serif; font-size: 13px; font-weight: bold;">
        <div style="width: 100%; min-height: 95mm; border-radius: 5px; background: white;">
            <table style="width: 100%">
                <tr class="text-center">
                    <td colspan="3">
                        <h4>RETUR PRODUK</h4>
                        <h4>
                            <asp:Label ID="LabelNamaStore" runat="server"></asp:Label></h4>

                    </td>
                </tr>
                <tr>
                    <td>NOMOR RETUR</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelIDReturBahanBaku"></asp:Label></td>
                </tr>
                <tr>
                    <td>VENDOR</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelVendor"></asp:Label></td>
                </tr>
                <tr>
                    <td>TANGGAL RETUR</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelTanggalRetur"></asp:Label></td>
                </tr>
                <tr>
                    <td>PEMBUAT</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelPengguna"></asp:Label></td>
                </tr>
                <tr>
                    <td>NOMOR PENERIMAAN</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelIDPenerimaanPOProduksiBahanBaku"></asp:Label></td>
                </tr>
                <tr>
                    <td>NOMOR PENAGIHAN</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelIDPenagihan"></asp:Label></td>
                </tr>
                <tr>
                    <td>STATUS</td>
                    <td>:
                        <asp:Label runat="server" ID="LabelStatusRetur"></asp:Label></td>
                </tr>
            </table>
            <br />
            <br />
            <table style="width: 100%;">
                <thead>
                    <tr style="border-bottom: 1px solid; border-top: 1px solid;">
                        <th class="text-lef fitsize">NO</th>
                        <th class="text-left">PRODUK</th>
                        <th class="text-left">VARIAN</th>
                        <th class="text-center">HARGA</th>
                        <th class="text-center">JUMLAH</th>
                        <th class="text-center">SUBTOTAL</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDetail" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Produk") %></td>
                                <td><%# Eval("AtributProduk") %></td>
                                <td class="text-right"><%# Eval("HargaRetur").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                <td class="text-right warning"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr style="border-top: 1px solid; border-bottom: 1px solid;">
                        <td colspan="5" class="text-center"><b>TOTAL</b></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="6"><br />KETERANGAN : <asp:Label runat="server" ID="LabelKeterangan" Text="-"></asp:Label></td>
                    </tr>
                </tfoot>
            </table>
            <br />
            <br />
            <table style="width: 100%;">
                <tr class="text-center">
                    <td style="width: 30%">
                        <b>DIBUAT OLEH</b>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 30%">
                        <b>DIKETAHUI</b>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 30%">
                        <b>DITERIMA</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
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

