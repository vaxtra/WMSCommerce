<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Cetak.aspx.cs" Inherits="WITAdministrator_Produk_Proyeksi_Cetak" %>

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
                        <img src="/images/logo.jpg"><br />
                        <asp:Label ID="LabelNamaStore" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <h3>PRODUCT PROJECTION</h3>
                    </td>
                    <td rowspan="2" style="width: 30%">
                        <asp:Label ID="LabelNamaVendor" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="LabelAlamatVendor" runat="server"></asp:Label><br />
                    </td>
                </tr>
                <tr class="text-left">
                    <td>
                        <table>
                            <tr>
                                <td><strong>ID Proyeksi</strong></td>
                                <td>&nbsp;:&nbsp;</td>
                                <td>
                                    <asp:Label ID="LabelIDProyeksi" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="text-center active">
                    <td style="width: 20%"><b>Pegawai</b></td>
                    <td style="width: 20%"><b>Tempat</b></td>
                    <td style="width: 20%"><b>Tanggal Proyeksi</b></td>
                    <td style="width: 20%"><b>Tanggal Target</b></td>
                    <td style="width: 20%"><b>Status Proyeksi</b></td>
                </tr>
                <tr class="text-center">
                    <td style="width: 20%">
                        <asp:Label ID="LabelPegawai" runat="server"></asp:Label></td>
                    <td style="width: 20%">
                        <asp:Label ID="LabelTempat" runat="server"></asp:Label></td>
                    <td style="width: 20%">
                        <asp:Label ID="LabelTanggalProyeksi" runat="server"></asp:Label></td>
                    <td style="width: 20%">
                        <asp:Label ID="LabelTanggalTarget" runat="server"></asp:Label></td>
                    <td style="width: 20%">
                        <asp:Label ID="LabelStatusProyeksi" runat="server"></asp:Label></td>
                </tr>
            </table>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <thead>
                    <tr class="active">
                        <th rowspan="2" class="text-center" style="width: 2%; vertical-align: middle;">No</th>
                        <th rowspan="2" class="text-center" style="width: 10%; vertical-align: middle;">Brand</th>
                        <th rowspan="2" class="text-center" style="width: 10%; vertical-align: middle;">Kategori</th>
                        <th rowspan="2" class="text-center" style="width: 15%; vertical-align: middle;">Produk</th>
                        <th rowspan="2" class="text-center" style="width: 5%; vertical-align: middle;">Warna</th>
                        <th id="KolomVarian" runat="server" class="text-center">Varian</th>
                        <th rowspan="2" class="text-center" style="width: 10%; vertical-align: middle;">Jumlah</th>
                    </tr>
                    <tr class="active">
                        <asp:Repeater ID="RepeaterVarian" runat="server">
                            <ItemTemplate>
                                <th class="text-center"><%# Eval("Nama") %></th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterDetail" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("PemilikProduk") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td><%# Eval("Produk") %></td>
                                <td class="text-center"><%# Eval("Warna") %></td>
                                <asp:Repeater ID="RepeaterJumlah" runat="server" DataSource='<%# Eval("AtributProduk") %>'>
                                    <ItemTemplate>
                                        <td class="text-right"><%# Eval("Jumlah").ToString() != "0" ? Eval("Jumlah").ToFormatHargaBulat() : "-" %></td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td class="text-right"><%# Eval("Total") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <asp:Panel ID="PanelKomposisi" runat="server" Visible="false">
                <div class="row">
                    <div class="col-xs-6 col-md-6">
                        <asp:Repeater ID="RepeaterKomposisi" runat="server">
                            <ItemTemplate>
                                <div class="table-responsive">
                                    <table class="table table-bordered table-condensed">
                                        <thead>
                                            <tr class="warning">
                                                <th colspan="5"><strong>
                                                    <asp:Label ID="LabelLevelBahanBaku" runat="server" class="form-label" Text='<%# "Produksi Bahan Baku Level " + (Container.ItemIndex + 1).ToString() %>'></asp:Label></strong></th>
                                            </tr>
                                            <tr>
                                                <th class="text-center" style="width: 2%;">No</th>
                                                <th class="text-center" style="width: 15%;">Kategori</th>
                                                <th class="text-center">Bahan Baku</th>
                                                <th class="text-center" style="width: 5%;">Satuan</th>
                                                <th class="text-center" style="width: 10%;">Jumlah</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterSubBahanBaku" runat="server" DataSource='<%# Eval("SubData") %>'>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("Kategori") %></td>
                                                        <td><%# Eval("BahanBaku") %></td>
                                                        <td><%# Eval("Satuan") %></td>
                                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="col-xs-6 col-md-6">
                        <div class="table-responsive">
                            <table class="table table-bordered table-condensed">
                                <thead>
                                    <tr class="info">
                                        <th colspan="7"><strong>
                                            <label class="form-label">Bahan Baku Dasar</label></strong></th>
                                    </tr>
                                    <tr>
                                        <th class="text-center" style="width: 2%;">No</th>
                                        <th class="text-center" style="width: 15%;">Kategori</th>
                                        <th class="text-center">Bahan Baku</th>
                                        <th class="text-center" style="width: 5%;">Satuan</th>
                                        <th class="text-center" style="width: 10%;">Butuh</th>
                                        <th class="text-center" style="width: 10%;">Stok</th>
                                        <th class="text-center" style="width: 10%;">Kurang</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterBahanBakuDasar" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td><%# Eval("BahanBaku") %></td>
                                                <td><%# Eval("Satuan") %></td>
                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("Stok").ToFormatHarga() %></td>
                                                <td <%# Eval("Jumlah").ToDecimal() > Eval("Stok").ToDecimal() ? "class='text-right danger'" : "class='text-right'" %>><%# Eval("Kurang").ToFormatHarga() %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="active">
                    <td><b>Keterangan</b></td>
                </tr>
                <tr class="text-center">
                    <td>
                        <asp:Label ID="LabelKeterangan" runat="server" Text="-"></asp:Label></td>
            </table>


            <table class="table table-bordered table-condensed" style="width: 100%">
                <tr class="text-center">
                    <td style="width: 30%">
                        <b>PROJECTION</b>
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
                        <b>PRODUCTION</b>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>

            <hr />
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

