<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="JenisPembayaran.aspx.cs" Inherits="WITReport_Transaksi_JenisPembayaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCari');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Jenis Pembayaran
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonTabel" runat="server" Text="Tabel" CssClass="btn btn-secondary btn-const mr-1" OnClick="ButtonTabel_Click" />
    <asp:Button ID="ButtonChart" runat="server" Text="Chart" CssClass="btn btn-secondary btn-const mr-1" OnClick="ButtonChart_Click" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-secondary btn-const" OnClick="ButtonExcel_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-secondary btn-const mr-1" />
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
        <div class="row">
            <div class="col-6">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
            </div>
            <div class="col-6">
                <asp:DropDownList ID="DropDownListJenisTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <div class="card-header bg-gradient-green">
                <ul class="nav nav-tabs card-header-tabs">
                    <li class="nav-item"><a href="Default.aspx" class="nav-link" style="color: #FFFFFF !important;">Transaksi</a></li>
                    <li class="nav-item"><a href="PenjualanProduk.aspx" class="nav-link" style="color: #FFFFFF !important;">Produk</a></li>
                    <li class="nav-item"><a href="../NetRevenue/Default.aspx" class="nav-link" style="color: #FFFFFF !important;">Net Revenue</a></li>
                    <li class="nav-item"><a href="#tabJenisPembayaran" id="JenisPembayaran-tab" class="nav-link active" data-toggle="tab">Jenis Pembayaran</a></li>
                </ul>
            </div>
            <div class="card-body">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="tabJenisPembayaran">
                        <div id="divTabel" runat="server" class="panel panel-success">
                            <h4 class="text-uppercase mb-3">
                                <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                            <div class="table-responsive">
                                <table class="table table-sm table-bordered table-hover">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No.</th>
                                            <th>Hari</th>
                                            <th>Tanggal</th>
                                            <asp:Literal ID="LiteralHeaderTabel" runat="server"></asp:Literal>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div id="divChart" runat="server" clientidmode="Static" visible="false"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script src="/assets/plugins/Morris/raphael-min.js"></script>
    <script src="/assets/plugins/Morris/morris.min.js"></script>

    <script src="/assets/plugins/Highcharts/highcharts.js"></script>
    <script src="/assets/plugins/Highcharts/modules/exporting.js"></script>
    <asp:Literal ID="LiteralChart" runat="server"></asp:Literal>
</asp:Content>

