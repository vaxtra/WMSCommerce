<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="WITAdministrator_Produk_Transfer_Kirim_Print" %>

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
            <h1>Transfer Produk</h1>
            <div style="font-size: 17px; font-weight: bold;">#<asp:Label ID="LabelIDTransferProduk" runat="server"></asp:Label></div>
            <div style="font-size: 17px; font-weight: bold;">
                <asp:Label ID="LabelPengirimTempat1" runat="server"></asp:Label>
            </div>
            <div>
                Print :
                <asp:Label ID="LabelPrintTanggal" runat="server"></asp:Label><br />
                <asp:Label ID="LabelPrintPengguna" runat="server"></asp:Label><br />
                <asp:Label ID="LabelPrintTempat" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Pengirim
                    <asp:Label ID="LabelPengirimTempat" runat="server"></asp:Label>
                    </b>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelPengirimanPengguna" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPengirimanTanggal" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LabelPengirimAlamat" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPengirimTelepon" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPengirimEmail" runat="server"></asp:Label>
                    <br />
                    <br />
                    <b>Keterangan :</b>
                    <br />
                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-xs-5 col-xs-offset-2 text-left">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Penerima
                    <asp:Label ID="LabelPenerimaTempat" runat="server"></asp:Label>
                    </b>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelPenerimaPengguna" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPenerimaTanggal" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LabelPenerimaAlamat" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPenerimaTelepon" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPenerimaEmail" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <table class="table table-bordered table-condensed laporan">
        <thead>
            <tr>
                <th>No.</th>
                <th>Produk</th>
                <th>Kategori</th>
                <th>Varian</th>
                <th>Kode</th>
                <th>Harga</th>
                <th>Transfer</th>
                <th>Subtotal</th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th>No.</th>
                <th>Produk</th>
                <th>Kategori</th>
                <th>Varian</th>
                <th>Kode</th>
                <th>Harga</th>
                <th>Transfer</th>
                <th>Subtotal</th>
            </tr>
        </tfoot>
        <tbody>
            <tr style="font-size: 10px; background-color: #e8e8e8; text-align: center;">
                <td colspan="6"></td>
                <td>Total</td>
                <td>Total</td>
            </tr>
            <tr style="font-weight: bold;">
                <td colspan="6"></td>
                <td class="text-right">
                    <asp:Label ID="LabelTotalQuantity1" runat="server"></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="LabelGrandTotal1" runat="server"></asp:Label></td>
            </tr>

            <asp:Repeater ID="RepeaterTransferKombinasiProduk" runat="server">
                <ItemTemplate>
                    <tr>
                        <td rowspan='<%# Eval("Count").ToInt() + 1 %>' class="fitSize"><%# Container.ItemIndex + 1 %></td>
                        <td rowspan='<%# Eval("Count").ToInt() + 1 %>'><%# Eval("Produk") %></td>
                        <td rowspan='<%# Eval("Count").ToInt() + 1 %>'><%# Eval("Kategori") %></td>
                        <td colspan="4" style="padding: 0px; border-bottom: 0;"></td>
                    </tr>
                    <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Eval("AtributProduk") %></td>
                                <td class="fitSize"><%# Eval("Kode") %></td>
                                <td class="text-right fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                <td class="text-right fitSize warning bold"><%# Eval("SubtotalHargaJual").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>

            <tr style="font-weight: bold;">
                <td colspan="6"></td>
                <td class="text-right">
                    <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></td>
            </tr>
            <tr style="font-size: 10px; background-color: #e8e8e8; text-align: center;">
                <td colspan="6"></td>
                <td>Total</td>
                <td>Total</td>
            </tr>
        </tbody>
    </table>

    <div class="row">
        <div class="col-xs-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Pengirim
                        <asp:Label ID="LabelPengirimanPengguna1" runat="server"></asp:Label></b>
                </div>
                <div class="panel-body">
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <div class="col-xs-3 col-xs-offset-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Penerima
                        <asp:Label ID="LabelPenerimaPengguna1" runat="server"></asp:Label></b>
                </div>
                <div class="panel-body">
                    <br />
                    <br />
                    <br />
                </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

