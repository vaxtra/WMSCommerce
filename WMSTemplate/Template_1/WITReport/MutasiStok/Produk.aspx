<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariTanggal');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Mutasi Stok Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <%-- <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
            <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
            <a id="LinkDownload" runat="server" visible="false">Download File</a>
            <asp:Button ID="ButtonKeluar" runat="server" Text="Keluar" CssClass="btn btn-sm btn-danger" OnClick="ButtonKeluar_Click" />--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ul id="myTab" class="nav nav-tabs">
                <li><a href="../PerpindahanStok/Produk.aspx">Perpindahan Stok</a></li>
                <li><a href="../TransferStok/Produk.aspx">Transfer</a></li>
                <li class="active"><a href="#tabMutasiStok" id="MutasiStok-tab" data-toggle="tab">Mutasi Stok</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabMutasiStok">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMinggu" runat="server" Text="Minggu Ini" OnClick="ButtonMinggu_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulan" runat="server" Text="Bulan Ini" OnClick="ButtonBulan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahun" runat="server" Text="Tahun Ini" OnClick="ButtonTahun_Click" />
                                    </div>
                                    <div class="form-group" style="margin: 5px 5px 0 0">
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" ClientIDMode="Static" OnClick="ButtonCariTanggal_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListTempat_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th rowspan="2">No.</th>
                                                <th rowspan="2">Produk</th>
                                                <th rowspan="2">Varian</th>
                                                <th rowspan="2" class="fitSize">Stok Awal</th>
                                                <th colspan="8" class="fitSize info">IN</th>
                                                <th colspan="8" class="fitSize danger">OUT</th>
                                                <th rowspan="2" class="fitSize">Stok Akhir</th>
                                            </tr>
                                            <tr class="active">
                                                <th class="breakWord">Stok Opname</th>
                                                <th class="breakWord">Transfer</th>
                                                <th class="breakWord">Transaksi Batal</th>
                                                <th class="breakWord">Restock</th>
                                                <th class="breakWord">Purchase Order</th>
                                                <th class="breakWord">Produksi</th>
                                                <th class="breakWord">Retur Pelanggan</th>
                                                <th class="fitSize">TOTAL</th>

                                                <th class="breakWord">Stok Opname</th>
                                                <th class="breakWord">Transfer</th>
                                                <th class="breakWord">Transaksi</th>
                                                <th class="breakWord">Pembuangan Rusak</th>
                                                <th class="breakWord">Retur Purchase Order</th>
                                                <th class="breakWord">Retur Produksi</th>
                                                <th class="breakWord">Pengurangan untuk Produksi</th>
                                                <th class="fitSize">TOTAL</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="success" style="font-weight: bold;">
                                                <td colspan="3"></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokAwal"]) : "0" %></td>

                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokOpnameIN"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TransferIN"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TransaksiBatal"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["Restock"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["PurchaseOrder"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["Produksi"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturPelanggan"]) : "0" %></td>
                                                <td class="info" style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["IN"]) : "0" %></td>

                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokOpnameOUT"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TransferOUT"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["Transaksi"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["PembuanganRusak"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturPurchaseOrder"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturProduksi"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["PenguranganUntukProduksi"]) : "0" %></td>
                                                <td class="danger" style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["OUT"]) : "0" %></td>

                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokAkhir"]) : "0" %></td>
                                            </tr>
                                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td class="fitSize"><a href='ProdukDetail.aspx?id=<%# Eval("IDKombinasiProduk") %>' target="_blank"><%# Eval("Produk") %></a></td>
                                                        <td class="fitSize"><%# Eval("Varian") %></td>
                                                        <td class="text-right fitSize warning"><strong><%# Eval("StokAwal").ToFormatHargaBulat() %></strong></td>

                                                        <td class="text-right fitSize"><%# Eval("StokOpnameIN").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("TransferIN").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("TransaksiBatal").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("Restock").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("PurchaseOrder").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("Produksi").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("ReturPelanggan").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize info"><%# Eval("IN").ToFormatHargaBulat() %></td>

                                                        <td class="text-right fitSize"><%# Eval("StokOpnameOUT").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("TransferOUT").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("Transaksi").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("PembuanganRusak").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("ReturPurchaseOrder").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("ReturProduksi").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize"><%# Eval("PenguranganUntukProduksi").ToFormatHargaBulat() %></td>
                                                        <td class="text-right fitSize danger"><%# Eval("OUT").ToFormatHargaBulat() %></td>

                                                        <td class="text-right fitSize warning"><strong><%# Eval("StokAkhir").ToFormatHargaBulat() %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="success" style="font-weight: bold;">
                                                <td colspan="3"></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokAwal"]) : "0" %></td>

                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokOpnameIN"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TransferIN"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TransaksiBatal"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["Restock"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["PurchaseOrder"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["Produksi"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturPelanggan"]) : "0" %></td>
                                                <td class="info" style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["IN"]) : "0" %></td>

                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokOpnameOUT"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TransferOUT"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["Transaksi"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["PembuanganRusak"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturPurchaseOrder"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturProduksi"]) : "0" %></td>
                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["PenguranganUntukProduksi"]) : "0" %></td>
                                                <td class="danger" style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["OUT"]) : "0" %></td>

                                                <td style="text-align: right;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["StokAkhir"]) : "0" %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

