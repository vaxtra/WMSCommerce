<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="WITPointOfSales_NewInvoice" %>

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
            <h1>Invoice</h1>
            <div style="font-size: 17px; font-weight: bold;">Order #<asp:Label ID="LabelIDTransaksi" runat="server"></asp:Label></div>
            <div>
                <asp:Label ID="LabelTanggalTransaksi" runat="server"></asp:Label>
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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="LabelNamaPelanggan" Style="font-weight: bold;" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelAlamatPelanggan" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelTeleponPelanggan" runat="server"></asp:Label>
                    <br />
                    <br />
                    <b>Pembayaran : </b>
                    <asp:Label ID="LabelJenisPembayaran" runat="server"></asp:Label>
                    <br />
                    <b>Status : </b>
                    <asp:Label ID="LabelStatusTransaksi" runat="server"></asp:Label>
                    <br />
                    <b>Keterangan :</b>
                    <br />
                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <table class="table table-bordered laporan">
        <thead>
            <tr>
                <th class="fitSize">No.</th>
                <th class="fitSize">Kode
                </th>
                <th>Produk
                </th>
                <th>Kategori</th>
                <th>Quantity
                </th>
                <th>Harga
                </th>
                <th class="fitSize">Disc. %
                </th>
                <th>Discount
                </th>
                <th>Subtotal
                </th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="RepeaterDetailTransaksi" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 %></td>
                        <td style="word-wrap: break-word;">
                            <%# Eval("Kode") %></td>
                        <td style="word-wrap: break-word;"><%# Eval("Nama") %></td>
                        <td style="word-wrap: break-word;"><%# Eval("Kategori") %></td>
                        <td class="text-right"><%# Eval("JumlahProduk") %></td>
                        <td class="text-right"><%# Pengaturan.FormatHarga(Eval("HargaJual").ToString()) %></td>
                        <td class="text-right"><%# (Pengaturan.FormatHarga(Eval("Persentase")) != "0") ? Pengaturan.FormatHarga(Eval("Persentase")) : "" %></td>
                        <td class="text-right"><%# (Pengaturan.FormatHarga(Eval("PotonganHargaJual")) != "0") ? Pengaturan.FormatHarga(Eval("PotonganHargaJual")) : "" %></td>
                        <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Subtotal").ToString()) %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>





    <div class="row">
        <div class="col-xs-5 text-left">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="LabelJudulPembayaran" Style="font-weight: bold;" Text="Detail Pembayaran" runat="server"></asp:Label>
                </div>

                <div class="panel-body">
                    <table>
                        <asp:Repeater ID="RepeaterPembayaran" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nama") %></td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Total")).ToString() %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>



        <div class="col-xs-5 text-right">
            <p>
                <strong>
                    <span id="panelSebelumDiscount" runat="server">Sebelum Discount</span>
                    <br />
                    <span id="panelDiscountProduct" runat="server">Discount Product</span>
                    <br />
                    <span id="panelDiscountTransaksi" runat="server">Discount Trasaction</span>
                    <br />
                    <span id="panelSetelahDiscount" runat="server">Setelah Discount</span>
                    <br />
                    <span id="panelCharge" runat="server">Charge</span>
                    <br />
                    <asp:Label ID="LabelKeteranganBiayaTambahan" runat="server"></asp:Label>
                    <br />
                    <span id="panelBiayaPengiriman" runat="server">Biaya Pengiriman</span>
                    <br />
                    <span id="panelPembulatan" runat="server">Pembulatan</span>
                    <br />
                    <span style="font-size: 17px;">Grand Total</span>
                    <br />
                    <span style="font-size: 17px;">Pembayaran</span>
                    <br />
                    <span style="font-size: 17px;">Sisa Pembayaran</span>
                    <br />
                    <br />
                    Total Quantity
                </strong>
            </p>
        </div>
        <div class="col-xs-2 text-right">
            <strong>
                <asp:Label ID="LabelSebelumDiscount" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelDiscountProduk" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelSubtotal" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelCharge" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelBiayaTambahan" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label>
                <br />
                <asp:Label ID="LabelPembulatan" runat="server"></asp:Label>
                <br />
                <span style="font-size: 17px;">
                    <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label>
                </span>
                <br />
                <span style="font-size: 17px;">
                    <asp:Label ID="LabelTotalBayar" runat="server"></asp:Label>
                </span>
                <br />
                <span style="font-size: 17px;">
                    <asp:Label ID="LabelSisaBayar" runat="server"></asp:Label>
                </span>
                <br />
                <br />
                <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
            </strong>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>
                        <asp:Label ID="LabelNamaPengirim" runat="server"></asp:Label></b>
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
                    <b>
                        <asp:Label ID="LabelNamaPenerima" runat="server"></asp:Label></b>
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
            <asp:Label ID="LabelFooterPrint" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="text-center">THANK YOU</td>
        </tr>
        <tr>
            <td class="text-center"><b>Warehouse Management System Powered by WIT.</b></td>
        </tr>
    </table>
</asp:Content>

