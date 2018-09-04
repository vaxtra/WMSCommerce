<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="PenjualanProduk.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Detail" %>

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
    Laporan Penjualan Produk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
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
            <div class="col-4">
                <asp:DropDownList ID="DropDownListCariTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
            </div>
            <div class="col-4">
                <asp:DropDownList ID="DropDownListCariStatusTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
            </div>
            <div class="col-4">
                <asp:DropDownList ID="DropDownListCariFilterBy" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click">
                    <asp:ListItem Text="Semua" Value="semua" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Brand" Value="brand"></asp:ListItem>
                    <asp:ListItem Text="Produk" Value="produk"></asp:ListItem>
                    <asp:ListItem Text="Varian" Value="varian"></asp:ListItem>
                    <asp:ListItem Text="Kategori" Value="kategori"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <div class="card-header bg-gradient-green">
                <ul class="nav nav-tabs card-header-tabs">
                    <li class="nav-item"><a href="../Transaksi/Default.aspx" class="nav-link" style="color: #FFFFFF !important;">Transaksi</a></li>
                    <li class="nav-item"><a href="#tabPenjualanProduk" id="PenjualanProduk-tab" class="nav-link active" data-toggle="tab">Produk</a></li>
                    <li class="nav-item"><a href="../NetRevenue/Default.aspx" class="nav-link" style="color: #FFFFFF !important;">Net Revenue</a></li>
                    <li class="nav-item"><a href="../Transaksi/JenisPembayaran.aspx" class="nav-link" style="color: #FFFFFF !important;">Jenis Pembayaran</a></li>
                </ul>
            </div>
            <div class="card-body">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="tabPenjualanProduk">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                            <ContentTemplate>
                                <h4 class="text-uppercase mb-3">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                                <div id="divSemua" runat="server">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-bordered table-hover">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>No.</th>
                                                    <th>Kode</th>
                                                    <th>Brand</th>
                                                    <th>Produk</th>
                                                    <th>Varian</th>
                                                    <th>Kategori</th>
                                                    <th style="width: 8%;">Jumlah</th>
                                                    <th>Harga Pokok</th>
                                                    <th>Harga Jual</th>
                                                    <th>Potongan Harga</th>
                                                    <th>Subtotal</th>
                                                    <th>Net Sales</th>
                                                </tr>
                                                <tr class="thead-light">
                                                    <th></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxCariKodeSemuaProduk" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%;"></asp:TextBox></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariPemilikProdukSemuaProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariProdukSemuaProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariAtributProdukSemuaProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="DropDownListCariKategoriSemuaProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelJumlahProduk" runat="server"></asp:Label></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelHargaPokok" runat="server"></asp:Label></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelHargaJual" runat="server"></asp:Label></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelPotonganHarga" runat="server"></asp:Label></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelSubtotal" runat="server"></asp:Label></th>
                                                    <th class="text-right">
                                                        <asp:Label ID="LabelPenjualanBersih" runat="server"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterSemuaProduk" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                            <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                            <td><%# Eval("Produk") %></td>
                                                            <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                            <td><%# Eval("Kategori") %></td>
                                                            <td class="text-right fitSize"><%# Eval("JumlahProduk") %></td>
                                                            <td class="text-right fitSize"><%# Eval("HargaPokok") %></td>
                                                            <td class="text-right fitSize"><%# Eval("HargaJual") %></td>
                                                            <td class="text-right fitSize"><%# Eval("PotonganHargaJual") %></td>
                                                            <td class="text-right fitSize"><%# Eval("Subtotal") %></td>
                                                            <td class="text-right table-warning fitSize"><strong><%# Eval("PenjualanBersih") %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div id="divFilter" runat="server" visible="false">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-bordered table-hover">
                                            <asp:Repeater ID="RepeaterFilterBy" runat="server">
                                                <ItemTemplate>
                                                    <thead>
                                                        <tr class="table-secondary">
                                                            <td colspan="12" style="font-size: 14pt; text-align: left;"><strong>
                                                                <asp:Label ID="LabelNama" runat="server" Text='<%# Eval("Nama") %>'></asp:Label></strong></td>
                                                        </tr>
                                                        <tr class="thead-light">
                                                            <th>No.</th>
                                                            <th>Kode</th>
                                                            <th>Brand</th>
                                                            <th>Produk</th>
                                                            <th>Varian</th>
                                                            <th>Kategori</th>
                                                            <th>Jumlah</th>
                                                            <th>Harga Pokok</th>
                                                            <th>Harga Jual</th>
                                                            <th>Potongan Harga</th>
                                                            <th>Subtotal</th>
                                                            <th>Net Sales</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                                    <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                                    <td><%# Eval("Produk") %></td>
                                                                    <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                                    <td><%# Eval("Kategori") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("HargaPokok").ToFormatHarga() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("PotonganHargaJual").ToFormatHarga() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("Subtotal").ToFormatHarga() %></td>
                                                                    <td class="text-right warning fitSize"><strong><%# Eval("PenjualanBersih").ToFormatHarga() %></strong></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr class="text-right table-success">
                                                            <td colspan="6" class="text-center"><strong>TOTAL</strong></td>
                                                            <td><strong><%# Eval("TotalJumlahProduk").ToFormatHargaBulat() %></strong></td>
                                                            <td><strong><%# Eval("TotalHargaPokok").ToFormatHarga() %></strong></td>
                                                            <td><strong><%# Eval("TotalHargaJual").ToFormatHarga() %></strong></td>
                                                            <td><strong><%# Eval("TotalPotonganHargaJual").ToFormatHarga() %></strong></td>
                                                            <td><strong><%# Eval("TotalSubtotal").ToFormatHarga() %></strong></td>
                                                            <td><strong><%# Eval("TotalPenjualanBersih").ToFormatHarga() %></strong></td>
                                                        </tr>
                                                    </tbody>
                                                </ItemTemplate>
                                            </asp:Repeater>
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
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

