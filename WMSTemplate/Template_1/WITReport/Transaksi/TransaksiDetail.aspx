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
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-secondary btn-const" OnClick="ButtonExcel_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-secondary btn-const mr-1" />
    <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
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
                <asp:DropDownList ID="DropDownListCariTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-6">
                <asp:DropDownList ID="DropDownListCariStatusTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
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
                                            <tr class="thead-light">
                                                <th></th>
                                                <th>
                                                    <asp:TextBox ID="TextBoxIDTransaksi" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%;"></asp:TextBox></th>
                                                <th></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariPelanggan" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th>
                                                    <asp:TextBox ID="TextBoxKode" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%"></asp:TextBox></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariPemilikProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariAtributProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariKategori" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th colspan="2"></th>
                                                <th class="text-right">
                                                    <asp:Label ID="LabelJumlahProdukHeader" runat="server" Text="0"></asp:Label></th>
                                                <th class="text-right">
                                                    <asp:Label ID="LabelSubtotalHeader" runat="server" Text="0"></asp:Label></th>
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
                                                        <td class="text-right table-warning font-weight-bold fitSize"><%# Eval("Subtotal") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="table-success font-weight-bold text-right">
                                                <td colspan="11"></td>
                                                <td>
                                                    <asp:Label ID="LabelJumlahProdukFooter" runat="server" Text="0"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="LabelSubtotalFooter" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tfoot>
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

