<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="TransaksiDetail.aspx.cs" Inherits="WITReport_Transaksi_TransaksiDetail" %>

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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Transaksi Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
    <a href="Default.aspx" class="btn btn-sm btn-danger">Kembali</a>
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
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6 col-md-6" style="font-weight: bold;">
                                        <asp:DropDownList ID="DropDownListCariTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6  col-md-6" style="font-weight: bold;">
                                        <asp:DropDownList ID="DropDownListCariStatusTransaksi" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-bordered table-hover table-condensed" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active text-center">
                                                <th>No.</th>
                                                <th>Transaksi</th>
                                                <th>Tanggal</th>
                                                <th>Pelanggan</th>
                                                <th>Kode</th>
                                                <th>Brand</th>
                                                <th>Produk</th>
                                                <th>Varian</th>
                                                <th>Kategori</th>
                                                <th>Harga Jual</th>
                                                <th>Potongan Harga</th>
                                                <th>Jumlah</th>
                                                <th>Subtotal</th>
                                            </tr>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxIDTransaksi" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPelanggan" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxKode" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPemilikProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariAtributProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariKategori" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td colspan="2"></td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelJumlahProdukHeader" runat="server" Text="0"></asp:Label></td>
                                                <td class="text-right">
                                                    <asp:Label ID="LabelSubtotalHeader" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td class="fitSize"><%# Eval("IDTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("TanggalTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("Pelanggan") %></td>
                                                        <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                        <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                        <td><%# Eval("Produk") %></td>
                                                        <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                        <td><%# Eval("Kategori") %></td>
                                                        <td class="text-right fitSize"><%# Eval("HargaJual") %></td>
                                                        <td class="text-right fitSize"><%# Eval("PotonganHargaJual") %></td>
                                                        <td class="text-right fitSize"><%# Eval("JumlahProduk") %></td>
                                                        <td class="text-right fitSize"><%# Eval("Subtotal") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="success text-right" style="font-weight: bold;">
                                                <td colspan="11"></td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahProdukFooter" runat="server" Text="0"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="LabelSubtotalFooter" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tfoot>
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

