<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Ringkasan.aspx.cs" Inherits="WITReport_Vendor_Ringkasan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Summary PO Produk 
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
    <asp:UpdatePanel ID="UpdatePanelRingkasan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="form-group">
                        <div class="form-inline">
                            <div class="form-group mr-1 mb-1">
                                <a id="ButtonPeriodeTanggal" runat="server" class="btn btn-light btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Periode</a>
                                <div class="dropdown-menu p-1">
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonHari" runat="server" Text="Hari Ini" Width="115px" OnClick="ButtonHari_Click" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonMinggu" runat="server" Text="Minggu Ini" Width="115px" OnClick="ButtonMinggu_Click" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonBulan" runat="server" Text="Bulan Ini" Width="115px" OnClick="ButtonBulan_Click" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonTahun" runat="server" Text="Tahun Ini" Width="115px" OnClick="ButtonTahun_Click" />
                                    <hr class="my-1" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" Width="115px" OnClick="ButtonHariSebelumnya_Click" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" Width="115px" OnClick="ButtonMingguSebelumnya_Click" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" Width="115px" OnClick="ButtonBulanSebelumnya_Click" />
                                    <asp:Button CssClass="btn btn-outline-light border" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" Width="115px" OnClick="ButtonTahunSebelumnya_Click" />
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
                            <div class="col-sm-6 col-md-6">
                                <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <h4 class="text-uppercase mb-3">
                        <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <h5 class="card-header bg-gradient-green">PROGRESS</h5>
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th rowspan="2">No.</th>
                                                    <th rowspan="2">Jenis</th>
                                                    <th colspan="2">Baru</th>
                                                    <th colspan="2">Proses</th>
                                                    <th colspan="2">Selesai</th>
                                                    <th colspan="2">Total</th>
                                                    <th rowspan="2">Progress</th>
                                                </tr>
                                                <tr class="thead-light">
                                                    <th>PO</th>
                                                    <th>Grandtotal</th>
                                                    <th>PO</th>
                                                    <th>Grandtotal</th>
                                                    <th>PO</th>
                                                    <th>Grandtotal</th>
                                                    <th>PO</th>
                                                    <th>Grandtotal</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterPurchaseOrder" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                            <td class="fitSize"><%# Eval("JenisPO") %></td>
                                                            <td class="fitSize text-right"><strong><%# Eval("Baru").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize table-warning text-right"><strong><%# Eval("GrandtotalBaru").ToFormatHarga() %></strong></td>
                                                            <td class="fitSize text-right"><strong><%# Eval("Proses").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize table-warning text-right"><strong><%# Eval("GrandtotalProses").ToFormatHarga() %></strong></td>
                                                            <td class="fitSize text-right"><strong><%# Eval("Selesai").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize table-warning text-right"><strong><%# Eval("GrandtotalSelesai").ToFormatHarga() %></strong></td>
                                                            <td class="fitSize text-right"><strong><%# Eval("Total").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize table-warning text-right"><strong><%# Eval("GrandtotalTotal").ToFormatHarga() %></strong></td>
                                                            <td>
                                                                <div class="progress" style="margin: 0px;">
                                                                    <%# Eval("Progress") %>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tfoot>
                                                <tr class="table-success text-right" style="font-weight: bold;">
                                                    <td colspan="2"></td>
                                                    <td>
                                                        <asp:Label ID="LabelBaru" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabelGrandtotalBaru" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabelProses" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabeGrandtotalProses" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabelSelesai" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabelGrandtotalSelesai" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabelTotal" runat="server" Text="0"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="LabelGrandtotalTotal" runat="server" Text="0"></asp:Label></td>
                                                    <td></td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card">
                                            <h5 class="card-header bg-gradient-blue">SUMMARY</h5>
                                            <div class="table-responsive">
                                                <table class="table table-condensed table-hover" style="font-size: 12px; font-weight: bold;">
                                                    <tr class="active">
                                                        <th class="text-center" colspan="2"><strong>DETAIL</strong></th>
                                                    </tr>
                                                    <tbody>
                                                        <tr>
                                                            <td>Harga Komposisi</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelHargaKomposisiDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Biata Tambahan</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelBiayaTambahanDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Potongan Harga</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelPotonganHargaDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Harga Vendor</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelHargaVendorDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Jumlah Produk</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelJumlahProdukDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="success">
                                                            <td>Subtotal</td>
                                                            <td class="fitSize text-right">
                                                                <asp:Label ID="LabelSubtotalDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </tbody>
                                                    <tr class="active">
                                                        <th class="text-center" colspan="2"><strong>PURCHASE ORDER</strong></th>
                                                    </tr>
                                                    <tbody>
                                                        <tr>
                                                            <td>Potongan PO</td>
                                                            <td class="fitSize text-right  warning">
                                                                <asp:Label ID="LabelPotongan" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Biaya lain-lain</td>
                                                            <td class="fitSize text-right  warning">
                                                                <asp:Label ID="LabelBiayaLainLain" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Tax</td>
                                                            <td class="fitSize text-right  warning">
                                                                <asp:Label ID="LabelTax" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="success">
                                                            <td>Grandtotal</td>
                                                            <td class="fitSize text-right">
                                                                <asp:Label ID="LabelGrandtotal" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </tbody>
                                                    <tr class="active">
                                                        <th class="text-center" colspan="2"><strong>PEMBAYARAN</strong></th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card">
                                            <h5 class="card-header bg-gradient-blue">KATEGORI</h5>
                                            <div class="table-responsive">
                                                <table class="table table-condensed table-hover" style="font-size: 12px;">
                                                    <thead>
                                                        <tr class="active">
                                                            <th>No.</th>
                                                            <th>Kategori</th>
                                                            <th>Jumlah</th>
                                                            <th>Subtotal</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterKategori" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Kategori") %></td>
                                                                    <td class="fitSize text-right"><%# Eval("Jumlah") %></td>
                                                                    <td class="fitSize warning text-right"><strong><%# Eval("Subtotal") %></strong></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card">
                                    <h5 class="card-header bg-gradient-red">JATUH TEMPO</h5>
                                    <div class="card-body">
                                        <div class="form-group">
                                            <asp:DropDownList ID="DropDownListJatuhTempo" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListJatuhTempo_SelectedIndexChanged">
                                                <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Purchase Order" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Production To Vendor" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <ul class="nav nav-tabs" role="tablist">
                                                <li class="nav-item"><a class="nav-link active" id="satu-tab" data-toggle="tab" href="#tabSatu">0 Hari</a></li>
                                                <li class="nav-item"><a class="nav-link" id="dua-tab" data-toggle="tab" href="#tabDua">
                                                    <asp:Label ID="LabelPanelSetengahJatuhTempo" runat="server" Text="Label"></asp:Label></a></li>
                                                <li class="nav-item"><a class="nav-link" id="tiga-tab" data-toggle="tab" href="#tabTiga">
                                                    <asp:Label ID="LabelPanelJatuhTempo" runat="server" Text="Label"></asp:Label></a></li>
                                            </ul>
                                            <br />
                                            <div id="myTabContent" class="tab-content">
                                                <div class="tab-pane active" id="tabSatu">
                                                    <div class="table-responsive">
                                                        <table class="table table-sm table-hover table-bordered">
                                                            <thead>
                                                                <tr class="thead-light">
                                                                    <th>No</th>
                                                                    <th>ID</th>
                                                                    <th>Pegawai</th>
                                                                    <th>Supplier</th>
                                                                    <th>Tanggal</th>
                                                                    <th>Jatuh Tempo</th>
                                                                    <th>Jarak</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepeaterPOProdukJatuhTempoSatu" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class='<%# Eval("ClassWarna") %>'>
                                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                            <td class="fitSize"><%# Eval("IDPOProduksiProduk") %></td>
                                                                            <td><%# Eval("Pengguna") %></td>
                                                                            <td><%# Eval("Nama") %></td>
                                                                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                                            <td><%# Eval("TanggalJatuhTempo").ToFormatTanggal() %></td>
                                                                            <td class="text-right fitSize"><strong><%# Eval("Jarak").ToFormatHargaBulat() + " Hari" %></strong></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tabDua">
                                                    <div class="table-responsive">
                                                        <table class="table table-sm table-hover table-bordered">
                                                            <thead>
                                                                <tr class="thead-light">
                                                                    <th>No</th>
                                                                    <th>ID</th>
                                                                    <th>Pegawai</th>
                                                                    <th>Supplier</th>
                                                                    <th>Tanggal</th>
                                                                    <th>Jatuh Tempo</th>
                                                                    <th>Jarak</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepeaterPOProdukJatuhTempoDua" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class='<%# Eval("ClassWarna") %>'>
                                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                            <td class="fitSize"><%# Eval("IDPOProduksiProduk") %></td>
                                                                            <td><%# Eval("Pengguna") %></td>
                                                                            <td><%# Eval("Nama") %></td>
                                                                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                                            <td><%# Eval("TanggalJatuhTempo").ToFormatTanggal() %></td>
                                                                            <td class="text-right fitSize"><strong><%# Eval("Jarak").ToFormatHargaBulat() + " Hari" %></strong></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tabTiga">
                                                    <div class="table-responsive">
                                                        <table class="table table-sm table-hover table-bordered">
                                                            <thead>
                                                                <tr class="thead-light">
                                                                    <th>No</th>
                                                                    <th>ID</th>
                                                                    <th>Pegawai</th>
                                                                    <th>Supplier</th>
                                                                    <th>Tanggal</th>
                                                                    <th>Jatuh Tempo</th>
                                                                    <th>Jarak</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepeaterPOProdukJatuhTempoTiga" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class='<%# Eval("ClassWarna") %>'>
                                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                            <td class="fitSize"><%# Eval("IDPOProduksiProduk") %></td>
                                                                            <td><%# Eval("Pengguna") %></td>
                                                                            <td><%# Eval("Nama") %></td>
                                                                            <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                                            <td><%# Eval("TanggalJatuhTempo").ToFormatTanggal() %></td>
                                                                            <td class="text-right fitSize"><strong><%# Eval("Jarak").ToFormatHargaBulat() + " Hari" %></strong></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="card">
                                    <h5 class="card-header bg-gradient-blue">PENRIMAAN</h5>
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                            <thead>
                                                <tr class="active">
                                                    <th>No.</th>
                                                    <th>Brand</th>
                                                    <%--<th>Produk</th>--%>
                                                    <%--<th>Varian</th>--%>
                                                    <th>Kategori</th>
                                                    <th>Terima</th>
                                                    <th>Subtotal</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterPenerimaan" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                            <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                            <%--<td><%# Eval("Produk") %></td>--%>
                                                            <%--<td class="fitSize text-center"><%# Eval("AtributProduk") %></td>--%>
                                                            <td><%# Eval("Kategori") %></td>
                                                            <td class="fitSize text-right"><%# Eval("Diterima") %></td>
                                                            <td class="fitSize warning text-right"><strong><%# Eval("Subtotal") %></strong></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgressRingkasan" runat="server" AssociatedUpdatePanelID="UpdatePanelRingkasan">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressRingkasan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

