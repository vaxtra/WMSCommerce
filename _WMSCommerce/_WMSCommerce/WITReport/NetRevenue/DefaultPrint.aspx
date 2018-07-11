<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="DefaultPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="fitSize">No.</th>
                        <th class="text-center">ID Transaksi</th>
                        <th class="text-center">Jenis</th>
                        <th class="text-center">Status</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center">Kategori</th>
                        <th class="text-center">Brand</th>
                        <th class="text-center">Price</th>
                        <th class="text-center">Discount</th>
                        <th class="text-center">Biaya Pengiriman</th>
                        <th class="text-center">Pembulatan</th>
                        <th class="text-center">Grand Total</th>
                        <th class="text-center">Net Revenue</th>
                        <th class="text-center" runat="server" id="Title1COGS">COGS</th>
                        <th class="text-center" runat="server" id="Title1GrossProfit">Gross Profit</th>
                        <th class="text-center">Pembayaran</th>
                        <th class="text-center">Keterangan</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="font-weight: bold;">
                        <td class="success" colspan="7"></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPrice"]) : "0" %></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["BiayaPengiriman"]) : "0" %></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Pembulatan"]) : "0" %></td>
                        <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["GrandTotal"]) : "0" %></td>
                        <td class="warning text-right"><%= Result != null ? Parse.ToFormatHarga(Result["NetRevenue"]) : "0" %></td>
                        <td class="success text-right" runat="server" id="Title2COGS"><%= Result != null ? Parse.ToFormatHarga(Result["TotalCOGS"]) : "0" %></td>
                        <td class="success text-right" runat="server" id="Title2GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrossProfit"]) : "0" %></td>
                        <td class="success" colspan="2"></td>
                    </tr>

                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td rowspan='<%# Eval("CountProduk") %>' class="fitSize">
                                    <%# Container.ItemIndex + 1 %>
                                </td>
                                <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("IDTransaksi") %></td>
                                <td class="fitSize" rowspan='<%# Eval("CountProduk") %>'><%# Eval("JenisTransaksi") %></td>
                                <td class="fitSize" rowspan='<%# Eval("CountProduk") %>'><%# Eval("StatusTransaksi") %></td>

                                <td class="fitSize"><%# Eval("Produk.Nama") %></td>
                                <td class="fitSize"><%# Eval("Produk.Kategori") %></td>
                                <td class="fitSize"><%# Eval("Produk.Brand") %></td>
                                <td class="text-right"><%# Eval("Produk.BeforeDiscount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Produk.Discount").ToFormatHarga() %></td>

                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right"><%# Eval("BiayaPengiriman").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right"><%# Eval("Pembulatan").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right info"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right warning"><%# Eval("NetRevenue").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right" runat="server" id="PanelCOGS"><%# Eval("TotalHargaBeli").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="text-right" runat="server" id="PanelGrossProfit"><%# Eval("TotalGrossProfit").ToFormatHarga() %></td>
                                <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("JenisPembayaran") %></td>
                                <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Keterangan") %></td>
                            </tr>
                            <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("Detail") %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Eval("Nama") %></td>
                                        <td class="fitSize"><%# Eval("Kategori") %></td>
                                        <td class="fitSize"><%# Eval("Brand") %></td>
                                        <td class="text-right"><%# Eval("BeforeDiscount").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Discount").ToFormatHarga() %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr style="font-weight: bold;">
                        <td class="success" colspan="7"></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPrice"]) : "0" %></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %></td>
                        <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["BiayaPengiriman"]) : "0" %></td>
                        <td class="success text-right"><%= Result != null ?Parse.ToFormatHarga(Result["Pembulatan"]) : "0" %></td>
                        <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["GrandTotal"]) : "0" %></td>
                        <td class="warning text-right"><%= Result != null ? Parse.ToFormatHarga(Result["NetRevenue"]) : "0" %></td>
                        <td class="success text-right" runat="server" id="Footer1COGS"><%= Result != null ? Parse.ToFormatHarga(Result["TotalCOGS"]) : "0" %></td>
                        <td class="success text-right" runat="server" id="Footer1GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrossProfit"]) : "0" %></td>
                        <td class="success" colspan="2"></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="table-responsive">
                <table class="table table-bordered table-condensed laporan">
                    <thead>
                        <tr class="active">
                            <th class="fitSize">No.</th>
                            <th class="text-center">Jenis Pembayaran</th>
                            <th class="text-center">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr style="font-weight: bold;">
                            <td class="success" colspan="2"></td>
                            <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalJenisPembayaran"]) : "0" %></td>
                        </tr>

                        <asp:Repeater ID="RepeaterJenisPembayaran" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                    <td><%# Eval("Key.Nama") %></td>
                                    <td class="info text-right"><%# Eval("Total").ToFormatHarga() %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                        <tr style="font-weight: bold;">
                            <td class="success" colspan="2"></td>
                            <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalJenisPembayaran"]) : "0" %></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="fitSize">No.</th>
                        <th class="text-center">ID Transaksi</th>
                        <th class="text-center">Jenis</th>
                        <th class="text-center">Status</th>
                        <th class="text-center">Keterangan</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center">Kategori</th>
                        <th class="text-center">Brand</th>
                        <th class="text-center">Price</th>
                        <th class="text-center">Discount</th>
                        <th class="text-center">Qty</th>
                        <th class="text-center">Net Revenue</th>
                        <th class="text-center" runat="server" id="Title3COGS">COGS</th>
                        <th class="text-center" runat="server" id="Title3GrossProfit">Gross Profit</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="font-weight: bold;" class="success">
                        <td colspan="10"></td>
                        <td class="text-right"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturQty"]) : "0" %></td>
                        <td class="text-right"><%= Result != null ? Parse.ToFormatHarga(Result["ReturNetRevenue"]) : "0" %></td>
                        <td class="text-right" runat="server" id="Title4COGS"><%= Result != null ? Parse.ToFormatHarga(Result["ReturCOGS"]) : "0" %></td>
                        <td class="text-right" runat="server" id="Title4GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["ReturGrossProfit"]) : "0" %></td>
                    </tr>

                    <asp:Repeater ID="RepeaterRetur" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("IDTransaksi") %></td>
                                <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                <td class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                <td><%# Eval("Keterangan") %></td>
                                <td class="fitSize"><%# Eval("Produk") %></td>
                                <td class="fitSize"><%# Eval("Kategori") %></td>
                                <td class="fitSize"><%# Eval("Brand") %></td>
                                <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Discount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right warning"><%# Eval("NetRevenue").ToFormatHarga() %></td>
                                <td class="text-right" runat="server" id="PanelCOGS"><%# Eval("HargaBeli") %></td>
                                <td class="text-right" runat="server" id="PanelGrossProfit"><%# Eval("GrossProfit").ToFormatHarga() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr style="font-weight: bold;" class="success">
                        <td colspan="10"></td>
                        <td class="text-right"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturQty"]) : "0" %></td>
                        <td class="text-right"><%= Result != null ? Parse.ToFormatHarga(Result["ReturNetRevenue"]) : "0" %></td>
                        <td class="text-right" runat="server" id="Footer2COGS"><%= Result != null ? Parse.ToFormatHarga(Result["ReturCOGS"]) : "0" %></td>
                        <td class="text-right" runat="server" id="Footer2GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["ReturGrossProfit"]) : "0" %></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10 text-right">
            <label class="control-label">Sebelum Retur</label>
            <br />
            <label class="control-label">Retur</label>
            <br />
            <label class="control-label">Grand Total</label>
            <br />
        </div>
        <div class="col-md-2 text-right">
            <label class="control-label">
                <%= Result != null ? Parse.ToFormatHarga(Result["SebelumRetur"]) : "0" %>
            </label>
            <br />
            <label class="control-label">
                <%= Result != null ? Parse.ToFormatHarga(Result["ReturNetRevenue"] * -1) : "0" %>
            </label>
            <br />
            <label class="control-label">
                <%= Result != null ? Parse.ToFormatHarga(Result["NetRevenue"]) : "0" %>
            </label>
            <br />
        </div>
    </div>
</asp:Content>

