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
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-secondary btn-const" OnClick="ButtonExcel_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-secondary btn-const mr-1" />
    <a href="TransaksiDetail.aspx" class="btn btn-primary btn-const">Detail</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="form-inline">
            <div class="form-group mr-1 mb-1">
                <a id="ButtonPeriodeTanggal" runat="server" class="btn btn-light btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Periode</a>
                <div class="dropdown-menu p-1">
                    <asp:Button CssClass="btn btn-light border" ID="ButtonHari" runat="server" Text="Hari Ini" Width="115px" OnClick="ButtonHari_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonMinggu" runat="server" Text="Minggu Ini" Width="115px" OnClick="ButtonMinggu_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonBulan" runat="server" Text="Bulan Ini" Width="115px" OnClick="ButtonBulan_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonTahun" runat="server" Text="Tahun Ini" Width="115px" OnClick="ButtonTahun_Click" />
                    <hr class="my-1" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" Width="115px" OnClick="ButtonHariSebelumnya_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" Width="115px" OnClick="ButtonMingguSebelumnya_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" Width="115px" OnClick="ButtonBulanSebelumnya_Click" />
                    <asp:Button CssClass="btn btn-light border" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" Width="115px" OnClick="ButtonTahunSebelumnya_Click" />
                </div>
            </div>
            <div class="form-group mr-1 mb-1">
                <asp:TextBox ID="TextBoxTanggalAwal" CssClass="form-control input-sm Tanggal" Width="160px" runat="server"></asp:TextBox>
            </div>
            <div class="form-group mr-1 mb-1">
                <asp:TextBox ID="TextBoxTanggalAkhir" CssClass="form-control input-sm Tanggal" Width="160px" runat="server"></asp:TextBox>
            </div>
            <div class="form-group mb-1">
                <asp:Button CssClass="btn btn-light btn-const" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <div class="card-header bg-gradient-green">
                <ul class="nav nav-tabs card-header-tabs">
                    <li class="nav-item"><a href="#tabTransaksi" id="Transaksi-tab" class="nav-link active" data-toggle="tab">Transaksi</a></li>
                    <li class="nav-item"><a href="PenjualanProduk.aspx" class="nav-link" style="color: #FFFFFF !important;">Produk</a></li>
                    <li class="nav-item"><a href="../NetRevenue/Default.aspx" class="nav-link" style="color: #FFFFFF !important;">Net Revenue</a></li>
                    <li class="nav-item"><a href="../Transaksi/JenisPembayaran.aspx" class="nav-link" style="color: #FFFFFF !important;">Jenis Pembayaran</a></li>
                </ul>
            </div>
            <div class="card-body">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="tabTransaksi">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                            <ContentTemplate>
                                <h4 class="text-uppercase mb-3">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                                    <div class="table-responsive">
                                        <table class="table table-sm table-bordered table-hover">
                                            <thead>
                                                <tr class="thead-light">
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
                                                <tr class="thead-light">
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
                                                <tr class="thead-light">
                                                    <th></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxCariIDTransaksi" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariTanggal(event)"></asp:TextBox></th>
                                                    <th></th>
                                                    <th colspan="2">
                                                        <asp:DropDownList ID="DropDownListCariPenggunaTransaksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th colspan="2">
                                                        <asp:DropDownList ID="DropDownListCariPenggunaUpdate" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariTempat" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariJenisTransaksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariStatusTransaksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariPelanggan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariMeja" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahTamu" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahProduk" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelSubtotalSebelumDiscount" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelDiscountProduk" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelDiscountVoucher" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelSubtotalSetelahDiscount" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahBiayaTambahan1" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahBiayaTambahan2" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahBiayaTambahan3" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahBiayaTambahan4" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahBiayaPengiriman" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelPembulatan" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label>
                                                    </th>
                                                    <th></th>
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
                                                            <td class="text-right table-warning"><%# Eval("SubtotalSebelumDiscount") %></td>
                                                            <td class="text-right table-danger"><%# Eval("PotonganTransaksi") %></td>
                                                            <td class="text-right table-danger"><%# Eval("TotalPotonganHargaJualDetail") %></td>
                                                            <td class="text-right table-danger"><%# Eval("TotalDiscountVoucher") %></td>
                                                            <td class="text-right table-warning"><%# Eval("Subtotal") %></td>
                                                            <td class="text-right table-info"><%# Eval("BiayaTambahan1") %></td>
                                                            <td class="text-right table-info"><%# Eval("BiayaTambahan2") %></td>
                                                            <td class="text-right table-info"><%# Eval("BiayaTambahan3") %></td>
                                                            <td class="text-right table-info"><%# Eval("BiayaTambahan4") %></td>
                                                            <td class="text-right table-info"><%# Eval("BiayaPengiriman") %></td>
                                                            <td class="text-right table-info"><%# Eval("Pembulatan") %></td>
                                                            <td class="text-right table-warning"><strong><%# Eval("GrandTotal") %></strong></td>
                                                            <td class="fitSize"><%# Eval("Keterangan") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="text-right font-weight-bold table-success">
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
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

