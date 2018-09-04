<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="SalesRegion.aspx.cs" Inherits="WITReport_BusinessIntelligence_SalesRegion" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Sales Region
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 text-center">
                    <h3>JUDUL
                        <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-inline">
                        <div class="btn-group" style="margin: 5px 5px 0 0">
                            <asp:ListBox ID="ListBoxGrupPelanggan" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple"></asp:ListBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-inline">
                        <div class="btn-group" style="margin: 5px 5px 0 0">
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonMinggu" runat="server" Text="Minggu Ini" OnClick="ButtonMinggu_Click" />
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonBulan" runat="server" Text="Bulan Ini" OnClick="ButtonBulan_Click" />
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonTahun" runat="server" Text="Tahun Ini" OnClick="ButtonTahun_Click" />
                        </div>
                        <div class="btn-group" style="margin: 5px 5px 0 0">
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnya_Click" />
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnya_Click" />
                            <asp:Button CssClass="btn btn-sm btn-default" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnya_Click" />
                        </div>
                        <div style="margin: 5px 5px 0 0" class="form-group">
                            <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                            <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" ClientIDMode="Static" OnClick="ButtonCariTanggal_Click" />
                        </div>
                        <div style="margin: 5px 5px 0 0" class="form-group pull-right">
                            <%--                    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-info btn-sm" Style="font-weight: bold;" />
                    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-info btn-sm" Style="font-weight: bold;" OnClick="ButtonExcel_Click" />
                    <a id="LinkDownload" runat="server" visible="false">Download File</a>--%>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Pelanggan Baru</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Jenis</th>
                                        <th>Transaksi</th>
                                        <th>Produk</th>
                                        <th>Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="fitSize text-center">1</td>
                                        <td>Pelanggan Baru</td>
                                        <td class="fitSize text-right">
                                            <asp:Label ID="LabelBaruJumlahTransaksi" runat="server" Text="0"></asp:Label></td>
                                        <td class="fitSize text-right">
                                            <asp:Label ID="LabelBaruJumlahProduk" runat="server" Text="0"></asp:Label></td>
                                        <td class="fitSize text-right">
                                            <asp:Label ID="LabelBaruGrandtotal" runat="server" Text="0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="fitSize text-center">2</td>
                                        <td>Pelanggan Lama</td>
                                        <td class="fitSize text-right">
                                            <asp:Label ID="LabelLamaJumlahTransaksi" runat="server" Text="0"></asp:Label></td>
                                        <td class="fitSize text-right">
                                            <asp:Label ID="LabelLamaJumlahProduk" runat="server" Text="0"></asp:Label></td>
                                        <td class="fitSize text-right">
                                            <asp:Label ID="LabelLamaGrandtotal" runat="server" Text="0"></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Grup Pelanggan</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Grup</th>
                                        <th>Transaksi</th>
                                        <th>Produk</th>
                                        <th>Grandtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterLaporanGrupPelanggan" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("GrupPelanggan") %></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahTransaksi")) %></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahProduk")) %></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("Grandtotal")) %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="success">
                                        <td class="text-center" colspan="2"><strong>TOTAL</strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalGrupPelangganJumlahTransaksi" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalGrupPelangganJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalGrupPelangganGrandtotal" runat="server" Text="0"></asp:Label></strong></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="DivPelanggan" runat="server" class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                    <div class="panel panel-success">
                        <div class="panel-heading">Pelanggan</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Nama</th>
                                        <th>Transaksi</th>
                                        <th>Diskon</th>
                                        <th class="fitSize">Net Revenue</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterLaporanPelanggan" runat="server" OnItemCommand="RepeaterLaporan_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButtonPelanggan" runat="server" CommandName="Pelanggan" CommandArgument='<%# Eval("IDPelanggan") %>'><%# Eval("Pelanggan") %></asp:LinkButton></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahTransaksi")) %></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("TotalDiskon")) %></td>
                                                <td class="fitSize text-right"><strong><%# Pengaturan.FormatHarga(Eval("NetRevenue")) %></strong></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="success">
                                        <td class="text-center" colspan="2"><strong>TOTAL</strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalPelangganJumlahTransaksi" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalPelangganJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalPelangganGrandtotal" runat="server" Text="0"></asp:Label></strong></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="DivKota" runat="server" class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                    <div class="panel panel-info">
                        <div class="panel-heading">Kota</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Kota</th>
                                        <th>Transaksi</th>
                                        <th>Diskon</th>
                                        <th class="fitSize">Net Revenue</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterLaporanKota" runat="server" OnItemCommand="RepeaterLaporan_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButtonKota" runat="server" CommandName="Kota" CommandArgument='<%# Eval("IDWilayah") %>'><%# Eval("Kota") %></asp:LinkButton></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahTransaksi")) %></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("TotalDiskon")) %></td>
                                                <td class="fitSize text-right"><strong><%# Pengaturan.FormatHarga(Eval("NetRevenue")) %></strong></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="success">
                                        <td class="text-center" colspan="2"><strong>TOTAL</strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalKotaJumlahTransaksi" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalKotaJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalKotaGrandtotal" runat="server" Text="0"></asp:Label></strong></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="DivProvinsi" runat="server" class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                    <div class="panel panel-warning">
                        <div class="panel-heading">Provinsi</div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No.</th>
                                        <th>Provinsi</th>
                                        <th>Transaksi</th>
                                        <th>Diskon</th>
                                        <th class="fitSize">Net Revenue</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterLaporanProvinsi" runat="server" OnItemCommand="RepeaterLaporan_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButtonProvinsi" runat="server" CommandName="Provinsi" CommandArgument='<%# Eval("IDWilayah") %>'><%# Eval("Provinsi") %></asp:LinkButton></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahTransaksi")) %></td>
                                                <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("TotalDiskon")) %></td>
                                                <td class="fitSize text-right"><strong><%# Pengaturan.FormatHarga(Eval("NetRevenue")) %></strong></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="success">
                                        <td class="text-center" colspan="2"><strong>TOTAL</strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalProvinsiJumlahTransaksi" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalProvinsiJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                        <td class="text-right"><strong>
                                            <asp:Label ID="LabelTotalProvinsiGrandtotal" runat="server" Text="0"></asp:Label></strong></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="DivDetail" runat="server" class="col-xs-12 col-sm-12 col-md-8 col-lg-8" visible="false">
                    <div id="PanelDetail" runat="server" class="panel">
                        <div class="panel-heading">
                            Detail
                    <asp:Label ID="LabelDetailJudul" runat="server" Text="Label"></asp:Label>
                            <asp:Button ID="ButtonKembali" runat="server" CssClass="btn btn-xs btn-danger pull-right" Text="X" OnClick="ButtonDetailTutup_Click" />
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                            <thead>
                                                <tr class="active">
                                                    <th colspan="5">TRANSAKSI</th>
                                                </tr>
                                                <tr class="active">
                                                    <th>No.</th>
                                                    <th>Bulan</th>
                                                    <th>Tahun</th>
                                                    <th>Transaksi</th>
                                                    <th class="fitSize">Net Revenue</th>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterLaporanDetailTransaksi" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Bulan") %></td>
                                                            <td class="text-center"><%# Eval("Tahun") %></td>
                                                            <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahTransaksi")) %></td>
                                                            <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("NetRevenue")) %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </tbody>
                                            <tfoot class="hidden">
                                                <tr class="success">
                                                    <td class="text-center" colspan="3"><strong>TOTAL</strong></td>
                                                    <td class="text-right"><strong>
                                                        <asp:Label ID="LabelTotalDetailJumlahTransaksi" runat="server" Text="0"></asp:Label></strong></td>
                                                    <td class="text-right"><strong>
                                                        <asp:Label ID="LabelTotalDetailNetRevenue" runat="server" Text="0"></asp:Label></strong></td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                            <thead>
                                                <tr class="active">
                                                    <th colspan="6">PRODUK</th>
                                                </tr>
                                                <tr class="active">
                                                    <th>No.</th>
                                                    <th>Bulan</th>
                                                    <th>Tahun</th>
                                                    <th class="fitSize">Nama Artikel</th>
                                                    <th>Kategori</th>
                                                    <th>Jumlah</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterLaporanDetailProduk" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Bulan") %></td>
                                                            <td class="text-center"><%# Eval("Tahun") %></td>
                                                            <td class="fitSize"><%# Eval("KombinasiProduk") %></td>
                                                            <td class="fitSize text-right"><%# Eval("KategoriProduk") %></td>
                                                            <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahProduk")) %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tfoot class="hidden">
                                                <tr class="success">
                                                    <td class="text-center" colspan="5"><strong>TOTAL</strong></td>
                                                    <td class="text-right"><strong>
                                                        <asp:Label ID="LabelTotalDetailJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

