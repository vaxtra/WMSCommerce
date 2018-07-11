<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pelanggan.aspx.cs" Inherits="WITReport_Master_Pelanggan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
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
    Laporan Pelanggan
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
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#tabPelanggan" id="pelanggan-tab" data-toggle="tab">Pelanggan</a></li>
                <li><a href="#tabTransaksi" id="transaksi-tab" data-toggle="tab">Transaksi</a></li>
                <li><a href="#tabPembelianProduk" id="pembelianProduk-tab" data-toggle="tab">Pembelian Produk</a></li>
                <li><a href="#tabKomisi" id="komisi-tab" data-toggle="tab">Komisi</a></li>
                <li><a href="#tabPotongan" id="potongan-tab" data-toggle="tab">Potongan</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="tabPelanggan">
                    <asp:UpdatePanel ID="UpdatePanelPelanggan" runat="server">
                        <ContentTemplate>
                            <asp:MultiView ID="MultiViewPelanggan" runat="server">
                                <asp:View ID="ViewPelanggan" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                    <thead>
                                                        <tr class="active">
                                                            <th>No</th>
                                                            <th>Nama</th>
                                                            <th>Grup</th>
                                                            <th>Username</th>
                                                            <th>Alamat</th>
                                                            <th>Email</th>
                                                            <th>Handphone</th>
                                                            <th>Transaksi</th>
                                                            <th>Deposit</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterPelanggan" runat="server" OnItemCommand="RepeaterPelanggan_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td class="fitSize"><%# Eval("NamaLengkap") %></td>
                                                                    <td class="text-center"><%# Eval("Nama") %></td>
                                                                    <td class="fitSize"><%# Eval("Username") %></td>
                                                                    <td><%# Eval("AlamatLengkap") %></td>
                                                                    <td class="fitSize"><%# Eval("Email") %></td>
                                                                    <td class="fitSize"><%# Eval("Handphone") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("JumlahTransaksi").ToFormatHargaBulat() %></td>
                                                                    <td class="text-right warning fitSize"><strong><%# Eval("Deposit").ToFormatHarga() %></strong></td>
                                                                    <td class="text-center fitSize">
                                                                        <asp:Button ID="ButtonProfilPelanggan" CssClass="btn btn-primary btn-xs" runat="server" Text="Profil" CommandName="ProfilPelanggan" CommandArgument='<%# Eval("IDPelanggan") %>' /></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="ViewProfilPelanggan" runat="server">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <strong>Profil Pelanggan</strong>
                                            <asp:Button ID="ButtonKembaliPelanggan" runat="server" Text="Kembali" CssClass="btn btn-danger btn-xs pull-right" OnClick="ButtonKembaliPelanggan_Click" />
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Grup</label>
                                                    <asp:TextBox ID="TextBoxGrupPelanggan" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <label>Nama</label>
                                                    <asp:TextBox ID="TextBoxNamaLengkap" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Alamat</label>
                                                    <asp:TextBox ID="TextBoxAlamat" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Telepon</label>
                                                    <asp:TextBox ID="TextBoxTeleponLain" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <label>Handphone</label>
                                                    <asp:TextBox ID="TextBoxHandphone" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Username</label>
                                                    <asp:TextBox ID="TextBoxUsername" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <label>Email</label>
                                                    <asp:TextBox ID="TextBoxEmail" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Tanggal Daftar</label>
                                                    <asp:TextBox ID="TextBoxTanggalDaftar" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <label>Status</label>
                                                    <asp:TextBox ID="TextBoxStatus" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Tanggal Lahir</label>
                                                    <asp:TextBox ID="TextBoxTanggalLahir" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                    <label>Deposit</label>
                                                    <asp:TextBox ID="TextBoxDeposit" CssClass="form-control angka" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>Catatan</label>
                                                    <asp:TextBox ID="TextBoxCatatan" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>

                            <asp:UpdateProgress ID="updateProgressPelanggan" runat="server" AssociatedUpdatePanelID="UpdatePanelPelanggan">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressPelanggan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div role="tabpanel" class="tab-pane" id="tabTransaksi">
                    <asp:UpdatePanel ID="UpdatePanelTransaksi" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariTransaksi" runat="server" Text="Hari Ini" OnClick="ButtonHariTransaksi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguTransaksi" runat="server" Text="Minggu Ini" OnClick="ButtonMingguTransaksi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanTransaksi" runat="server" Text="Bulan Ini" OnClick="ButtonBulanTransaksi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunTransaksi" runat="server" Text="Tahun Ini" OnClick="ButtonTahunTransaksi_Click" />
                                    </div>
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnyaTransaksi" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnyaTransaksi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnyaTransaksi" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnyaTransaksi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnyaTransaksi" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnyaTransaksi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnyaTransaksi" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnyaTransaksi_Click" />
                                    </div>
                                    <div style="margin: 5px 5px 0 0" class="form-group">
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwalTransaksi" runat="server"></asp:TextBox>
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhirTransaksi" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTransaksi" runat="server" Text="Cari" OnClick="ButtonCariTransaksi_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListTempatTransaksi" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTransaksi_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListJenisTransaksiTransaksi" CssClass="select2 center-text" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTransaksi_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListPelangganTransaksi" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTransaksi_Click"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriodeTransaksi" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="info">
                                                <th colspan="13" style="font-size: 14pt; text-align: left;"><strong>
                                                    <asp:Label ID="LabelNamaPelanggan" runat="server"></asp:Label></strong></th>
                                            </tr>
                                            <tr class="active">
                                                <th>No</th>
                                                <th>ID Transaksi</th>
                                                <th>Tanggal Transaksi</th>
                                                <th>Tanggal Pembayaran</th>

                                                <th>Produk</th>
                                                <th>Varian</th>
                                                <th>Jumlah</th>
                                                <th>Retur</th>
                                                <th>Grandtotal</th>
                                                <th>Pembayaran</th>
                                                <th>Penagihan</th>
                                                <th>Status</th>
                                                <th>Keterangan</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterTransaksi" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("IDTransaksi") %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("TanggalOperasional").ToFormatTanggalJam() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("TanggalPembayaran").ToFormatTanggalJam() %></td>
                                                        <td class="fitSize"><%# Eval("Produk.Produk") %></td>
                                                        <td class="fitSize text-center"><%# Eval("Produk.AtributProduk") %></td>
                                                        <td class="fitSize text-right"><%# Eval("Produk.JumlahProduk").ToFormatHargaBulat() %></td>
                                                        <td class="fitSize text-right"><%# Eval("Produk.Retur").ToFormatHargaBulat() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-center warning fitSize"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right success fitSize"><strong><%# Eval("TotalPembayaran").ToFormatHarga() %></strong></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right info fitSize"><strong><%# Eval("Penagihan").ToFormatHarga() %></strong></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Keterangan") %></td>
                                                    </tr>
                                                    <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("Detail") %>'>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Eval("Produk") %></td>
                                                                <td class="fitSize text-center"><%# Eval("AtributProduk") %></td>
                                                                <td class="fitSize text-right"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                                                <td class="fitSize text-right"><%# Eval("Retur").ToFormatHargaBulat() %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProgressTransaksi" runat="server" AssociatedUpdatePanelID="UpdatePanelTransaksi">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressTransaksi" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div role="tabpanel" class="tab-pane" id="tabPembelianProduk">
                    <asp:UpdatePanel ID="UpdatePanelPembelianProduk" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariPembelianProduk" runat="server" Text="Hari Ini" OnClick="ButtonHariPembelianProduk_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguPembelianProduk" runat="server" Text="Minggu Ini" OnClick="ButtonMingguPembelianProduk_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanPembelianProduk" runat="server" Text="Bulan Ini" OnClick="ButtonBulanPembelianProduk_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunPembelianProduk" runat="server" Text="Tahun Ini" OnClick="ButtonTahunPembelianProduk_Click" />
                                    </div>
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnyaPembelianProduk" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnyaPembelianProduk_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnyaPembelianProduk" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnyaPembelianProduk_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnyaPembelianProduk" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnyaPembelianProduk_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnyaPembelianProduk" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnyaPembelianProduk_Click" />
                                    </div>
                                    <div style="margin: 5px 5px 0 0" class="form-group">
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwalPembelianProduk" runat="server"></asp:TextBox>
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhirPembelianProduk" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariPembelianProduk" runat="server" Text="Cari" OnClick="ButtonCariPembelianProduk_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-3 col-md-3">
                                        <asp:DropDownList ID="DropDownListTempatPembelianProduk" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPembelianProduk_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:DropDownList ID="DropDownListJenisTransaksiPembelianProduk" CssClass="select2 center-text" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPembelianProduk_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:DropDownList ID="DropDownListPelangganPembelianProduk" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPembelianProduk_Click"></asp:DropDownList>
                                    </div>

                                    <div class="col-sm-3 col-md-3">
                                        <asp:DropDownList ID="DropDownListStatusTransaksiPembelianProduk" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPembelianProduk_Click"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriodePembelianProduk" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <asp:Repeater ID="RepeaterPembelianProduk" runat="server">
                                            <ItemTemplate>
                                                <thead>
                                                    <tr class="info">
                                                        <th colspan="9" style="font-size: 14pt; text-align: left;"><strong>
                                                            <asp:Label ID="LabelNamaPelanggan" runat="server" Text='<%# Eval("Pelanggan") %>'></asp:Label></strong></th>
                                                    </tr>
                                                    <tr class="active">
                                                        <th>No</th>
                                                        <th>Pelanggan</th>
                                                        <th>Waktu Transaksi</th>
                                                        <th>Waktu Pembayaran</th>
                                                        <th>Produk</th>
                                                        <th>Varian</th>
                                                        <th>Harga Jual</th>
                                                        <th>Jumlah</th>
                                                        <th>Subtotal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Produk") %>'>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("NamaLengkap") %></td>
                                                                <td class="fitSize"><%# Eval("TanggalTransaksi").ToFormatTanggalJam() %></td>
                                                                <td class="fitSize"><%# Eval("TanggalPembayaran").ToFormatTanggalJam() %></td>
                                                                <td><%# Eval("Produk") %></td>
                                                                <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                                <td class="text-right fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                                <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                                                <td class="text-right warning fitSize"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="success text-right">
                                                        <td colspan="7" class="text-center"><strong>TOTAL</strong></td>
                                                        <td><strong>
                                                            <asp:Label ID="LabelTotalJumlahProduk" runat="server" Text='<%# Eval("TotalJumlahProduk").ToFormatHargaBulat() %>'></asp:Label></strong></td>
                                                        <td><strong>
                                                            <asp:Label ID="LabelTotalJumlahSubtotal" runat="server" Text='<%# Eval("TotalJumlahSubtotal").ToFormatHarga() %>'></asp:Label></strong></td>
                                                    </tr>
                                                </tfoot>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProgressPembelianProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelPembelianProduk">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressPembelianProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div role="tabpanel" class="tab-pane" id="tabKomisi">
                    <asp:UpdatePanel ID="UpdatePanelKomisi" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariKomisi" runat="server" Text="Hari Ini" OnClick="ButtonHariKomisi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguKomisi" runat="server" Text="Minggu Ini" OnClick="ButtonMingguKomisi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanKomisi" runat="server" Text="Bulan Ini" OnClick="ButtonBulanKomisi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunKomisi" runat="server" Text="Tahun Ini" OnClick="ButtonTahunKomisi_Click" />
                                    </div>
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnyaKomisi" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnyaKomisi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnyaKomisi" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnyaKomisi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnyaKomisi" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnyaKomisi_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnyaKomisi" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnyaKomisi_Click" />
                                    </div>
                                    <div style="margin: 5px 5px 0 0" class="form-group">
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwalKomisi" runat="server"></asp:TextBox>
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhirKomisi" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariKomisi" runat="server" Text="Cari" OnClick="ButtonCariKomisi_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListTempatKomisi" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariKomisi_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListJenisTransaksiKomisi" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariKomisi_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListStatusTransaksiKomisi" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariKomisi_Click"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriodeKomisi" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th>No</th>
                                                <th>Nama</th>
                                                <th>Grup</th>
                                                <th>Bonus</th>
                                                <th>Transaksi</th>
                                                <th>Komisi</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterKomisiPelanggan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("NamaLengkap") %></td>
                                                        <td class="text-center fitSize"><%# Eval("Nama") %></td>
                                                        <td class="text-center fitSize"><%# Eval("BonusPelanggan") %></td>
                                                        <td class="text-right fitSize"><%# Eval("JumlahTransaksi").ToFormatHargaBulat() %></td>
                                                        <td class="text-right warning fitSize"><strong><%# Eval("Komisi").ToFormatHarga() %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <asp:UpdateProgress ID="updateProgressKomisi" runat="server" AssociatedUpdatePanelID="UpdatePanelKomisi">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressKomisi" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div role="tabpanel" class="tab-pane" id="tabPotongan">
                    <asp:UpdatePanel ID="UpdatePanelPotongan" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariPotongan" runat="server" Text="Hari Ini" OnClick="ButtonHariPotongan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguPotongan" runat="server" Text="Minggu Ini" OnClick="ButtonMingguPotongan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanPotongan" runat="server" Text="Bulan Ini" OnClick="ButtonBulanPotongan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunPotongan" runat="server" Text="Tahun Ini" OnClick="ButtonTahunPotongan_Click" />
                                    </div>
                                    <div class="btn-group" style="margin: 5px 5px 0 0">
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnyaPotongan" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnyaPotongan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnyaPotongan" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnyaPotongan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnyaPotongan" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnyaPotongan_Click" />
                                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnyaPotongan" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnyaPotongan_Click" />
                                    </div>
                                    <div style="margin: 5px 5px 0 0" class="form-group">
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwalPotongan" runat="server"></asp:TextBox>
                                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhirPotongan" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariPotongan" runat="server" Text="Cari" OnClick="ButtonCariPotongan_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListTempatPotongan" CssClass="select2 center-text" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPotongan_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListJenisTransaksiPotongan" CssClass="select2" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPotongan_Click"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:DropDownList ID="DropDownListStatusTransaksiPotongan" CssClass="select2 center-text" Width="100%" runat="server" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariPotongan_Click"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriodePotongan" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th>No</th>
                                                <th>Nama</th>
                                                <th>Grup</th>
                                                <th>Bonus</th>
                                                <th>Transaksi</th>
                                                <th>Potongan</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterPotonganPelanggan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("NamaLengkap") %></td>
                                                        <td class="text-center fitSize"><%# Eval("Nama") %></td>
                                                        <td class="text-center fitSize"><%# Eval("BonusPelanggan") %></td>
                                                        <td class="text-right fitSize"><%# Eval("JumlahTransaksi").ToFormatHargaBulat() %></td>
                                                        <td class="text-right warning fitSize"><strong><%# Eval("Potongan").ToFormatHarga() %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <asp:UpdateProgress ID="updateProgressPotongan" runat="server" AssociatedUpdatePanelID="UpdatePanelPotongan">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressPotongan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

