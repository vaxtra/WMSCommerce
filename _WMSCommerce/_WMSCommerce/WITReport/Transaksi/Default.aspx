<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCariTanggal(e) {
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Transaksi
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
    <a href="TransaksiDetail.aspx" class="btn btn-sm btn-info">Detail</a>
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
                <li class="active"><a href="#tabTransaksi" id="Transaksi-tab" data-toggle="tab">Transaksi</a></li>
                <li><a href="PenjualanProduk.aspx">Produk</a></li>
                <li><a href="../NetRevenue/Default.aspx">Net Revenue</a></li>
                <li><a href="JenisPembayaran/Default.aspx">Jenis Pembayaran</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabTransaksi">
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
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnya_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnya_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnya_Click" />
                                    </div>
                                    <div style="margin: 5px 5px 0 0" class="form-group">
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th class="text-right" rowspan="2">No.</th>
                                                <th rowspan="2"></th>
                                                <th rowspan="2">Operasional</th>
                                                <th class="text-center" colspan="2">Transaksi</th>
                                                <th class="text-center" colspan="2">Update</th>
                                                <th rowspan="2">Tempat</th>
                                                <th rowspan="2">Jenis</th>
                                                <th rowspan="2">Status</th>
                                                <th rowspan="2">Pelanggan</th>
                                                <th rowspan="2">Meja</th>
                                                <th class="text-right" colspan="2">Jumlah</th>
                                                <th class="text-right" rowspan="2">
                                                    <abbr title="Subtotal sebelum discount">Subtotal</abbr>
                                                </th>
                                                <th class="text-center" colspan="3">Discount</th>
                                                <th class="text-right" rowspan="2">
                                                    <abbr title="Subtotal setelah discount">Subtotal</abbr>
                                                </th>
                                                <th class="text-center" colspan="6">Biaya Tambahan</th>
                                                <th class="text-right fitSize" rowspan="2">Grand Total</th>
                                                <th rowspan="2">Keterangan</th>
                                            </tr>
                                            <tr class="active">
                                                <th>Tanggal</th>
                                                <th>Pengguna</th>
                                                <th>Tanggal</th>
                                                <th>Pengguna</th>
                                                <th>Tamu</th>
                                                <th>Produk</th>
                                                <th class="text-right">Transaksi</th>
                                                <th class="text-right">Produk</th>
                                                <th class="text-right">Voucher</th>
                                                <th class="text-right">1</th>
                                                <th class="text-right">2</th>
                                                <th class="text-right">3</th>
                                                <th class="text-right">4</th>
                                                <th class="text-right">Pengiriman</th>
                                                <th class="text-right">Pembulatan</th>
                                            </tr>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariIDTransaksi" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariTanggal(event)"></asp:TextBox></td>
                                                <td></td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="DropDownListCariPenggunaTransaksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="DropDownListCariPenggunaUpdate" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariTempat" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariJenisTransaksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariStatusTransaksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPelanggan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariMeja" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahTamu" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahProduk" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelSubtotalSebelumDiscount" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelDiscountProduk" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelDiscountVoucher" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelSubtotalSetelahDiscount" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahBiayaTambahan1" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahBiayaTambahan2" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahBiayaTambahan3" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahBiayaTambahan4" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahBiayaPengiriman" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelPembulatan" runat="server"></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><a href='/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>&returnUrl=/WITReport/Transaksi/Default.aspx'><%# Eval("IDTransaksi") %></a></td>
                                                        <td class="fitSize"><%# Eval("TanggalOperasional") %></td>
                                                        <td class="fitSize"><%# Eval("TanggalTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("PenggunaTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("TanggalUpdate") %></td>
                                                        <td class="fitSize"><%# Eval("PenggunaUpdate") %></td>
                                                        <td class="fitSize"><%# Eval("Tempat") %></td>
                                                        <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("Pelanggan") %></td>
                                                        <td class="fitSize text-center"><%# Eval("Meja") %></td>
                                                        <td class="text-right"><%# Eval("JumlahTamu") %></td>
                                                        <td class="text-right"><%# Eval("JumlahProduk") %></td>
                                                        <td class="text-right warning"><%# Eval("SubtotalSebelumDiscount") %></td>
                                                        <td class="text-right danger"><%# Eval("PotonganTransaksi") %></td>
                                                        <td class="text-right danger"><%# Eval("TotalPotonganHargaJualDetail") %></td>
                                                        <td class="text-right danger"><%# Eval("TotalDiscountVoucher") %></td>
                                                        <td class="text-right warning"><%# Eval("Subtotal") %></td>
                                                        <td class="text-right info"><%# Eval("BiayaTambahan1") %></td>
                                                        <td class="text-right info"><%# Eval("BiayaTambahan2") %></td>
                                                        <td class="text-right info"><%# Eval("BiayaTambahan3") %></td>
                                                        <td class="text-right info"><%# Eval("BiayaTambahan4") %></td>
                                                        <td class="text-right info"><%# Eval("BiayaPengiriman") %></td>
                                                        <td class="text-right info"><%# Eval("Pembulatan") %></td>
                                                        <td class="text-right success"><strong><%# Eval("GrandTotal") %></strong></td>
                                                        <td class="fitSize"><%# Eval("Keterangan") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="text-right success" style="font-weight: bold;">
                                                <td colspan="12"></td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahTamu1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahProduk1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelSubtotalSebelumDiscount1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelDiscountTransaksi1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelDiscountProduk1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelDiscountVoucher1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelSubtotalSetelahDiscount1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahBiayaTambahan11" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahBiayaTambahan21" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahBiayaTambahan31" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahBiayaTambahan41" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahBiayaPengiriman1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPembulatan1" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelGrandTotal1" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
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

