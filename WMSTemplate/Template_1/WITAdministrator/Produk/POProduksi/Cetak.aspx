<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Cetak.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Cetak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-condensed table-hover table-borderedd" style="width: 100%">
                <tr class="text-center">
                    <td rowspan="2" style="width: 30%">
                        <img src="/images/logo.jpg" style="width: 80%;"><br />
                        <asp:Label ID="LabelNamaStore" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label><br />
                    </td>
                    <td>
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
                    <td style="width: 25%"><b>Jatuh Tempo</b></td>
                    <td style="width: 25%"><b>Pengiriman</b></td>
                </tr>
                <tr class="text-center">
                    <td style="width: 25%">
                        <asp:Label ID="LabelIDProyeksi" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="LabelIDProduksi" runat="server" Text="-"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="LabelTanggalJatuhTempo" runat="server" Text="-"></asp:Label></td>
                                        <td style="width: 25%">
                        <asp:Label ID="LabelTanggalPengiriman" runat="server" Text="-"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-center active"><b>Pembuat</b></td>          
                    <td colspan="3"><asp:Label ID="LabelPembuat" runat="server" Text="-"></asp:Label></td> 
                </tr>
            </table>

            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr class="active">
                        <th class="text-center" style="width: 2%;">No</th>
                        <th class="text-center" style="width: 8%;">Kode</th>
                        <th class="text-center">Produk</th>
                        <th class="text-center" style="width: 5%;">Varian</th>
                        <th id="headerKomposisi" runat="server" class="text-center" style="width: 10%;">Komposisi</th>
                        <th id="headerBiaya" runat="server" class="text-center" style="width: 10%;">Biaya</th>
                        <th id="headerHarga" runat="server" class="text-center" style="width: 10%;">Harga</th>
                        <th id="headerPotongan" runat="server" class="text-center" style="width: 10%;">Potongan</th>
                        <th class="text-center" style="width: 5%;">Jumlah</th>
                        <th class="text-center" style="width: 15%;">Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDetail" runat="server" OnItemCreated="RepeaterDetail_ItemCreated">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("KodeKombinasiProduk") %></td>
                                <td><%# Eval("Produk") %></td>
                                <td class="text-center"><%# Eval("AtributProduk") %></td>
                                <td id="bodyKomposisi" runat="server" class="text-right"><%# Eval("HargaPokokKomposisi").ToFormatHarga() %></td>
                                <td id="bodyBiaya" runat="server" class="text-right"><%# Eval("BiayaTambahan").ToFormatHarga() %></td>
                                <td id="bodyHarga" runat="server" class="text-right"><%# Eval("HargaVendor").ToFormatHarga() %></td>
                                <td id="bodyPotongan" runat="server" class="text-right"><%# Eval("PotonganHargaVendor").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                <td class="text-right warning"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="bold">
                        <td colspan="6" class="text-right active"><b>TOTAL</b></td>
                        <td class="text-right warning"><strong>
                            <asp:Label ID="LabelTotalJumlah" runat="server" Text="0"></asp:Label></strong></td>
                        <td class="text-right warning"><strong>
                            <asp:Label ID="LabelTotalSubtotal" runat="server" Text="0"></asp:Label></strong></td>
                    </tr>
                    <tr class="bold hidden">
                        <td colspan="7" class="text-right active"><b>Biaya Lain-Lain</b></td>
                        <td class="text-right warning"><strong>
                            <asp:Label ID="LabelBiayaLainLain" runat="server" Text="0"></asp:Label></strong></td>
                    </tr>
                    <tr class="bold hidden">
                        <td colspan="7" class="text-right active"><b>Potongan</b></td>
                        <td class="text-right warning"><strong>
                            <asp:Label ID="LabelPotongan" runat="server" Text="0"></asp:Label></strong></td>
                    </tr>
                    <tr class="bold hidden">
                        <td colspan="7" class="text-right active"><b>
                            <asp:Label ID="LabelJudulTax" runat="server" Text="0"></asp:Label></b></td>
                        <td class="text-right warning"><strong>
                            <asp:Label ID="LabelTax" runat="server" Text="0"></asp:Label></strong></td>
                    </tr>
                    <tr class="bold">
                        <td colspan="7" class="text-right active"><b>Grandtotal</b></td>
                        <td class="text-right success"><strong>
                            <asp:Label ID="LabelGrandtotal" runat="server" Text="0"></asp:Label></strong></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="8">
                            <asp:Literal ID="LiteralKeterangan" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tfoot>
            </table>

            <div id="komposisi" class="row" runat="server">
                <div class="col-xs-6">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="active">
                                <th class="text-center" style="width: 2%;">No</th>
                                <th class="text-center">Bahan Baku</th>
                                <th class="text-center" style="width: 15%;">Satuan</th>
                                <th class="text-center" style="width: 10%;">Jumlah</th>
                                <th class="text-center" style="width: 15%;">Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterKomposisi" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("TBBahanBaku.Nama") %></td>
                                        <td class="text-center"><%# Eval("TBSatuan.Nama") %></td>
                                        <td class="text-right"><%# Eval("Kebutuhan").ToFormatHarga() %></td>
                                        <td class="text-right warning"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="success bold">
                                <td colspan="4" class="text-center"><b>TOTAL</b></td>
                                <td class="text-right"><strong>
                                    <asp:Label ID="LabelTotalSubtotalKomposisi" runat="server" Text="0"></asp:Label></strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-xs-6">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="active">
                                <th class="text-center" style="width: 2%;">No</th>
                                <th class="text-center">Biaya Produksi</th>
                                <th class="text-center" style="width: 15%;">Biaya</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterBiayaTambahan" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("TBJenisBiayaProduksi.Nama") %></td>
                                        <td class="text-right warning"><strong><%# Eval("Nominal").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr class="success bold">
                                <td colspan="2" class="text-center"><b>TOTAL</b></td>
                                <td class="text-right"><strong>
                                    <asp:Label ID="LabelTotalSubtotalBiayaTambahan" runat="server" Text="0"></asp:Label></strong></td>
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

