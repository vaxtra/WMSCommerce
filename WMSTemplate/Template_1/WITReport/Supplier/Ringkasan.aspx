<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Ringkasan.aspx.cs" Inherits="WITReport_Supplier_Ringkasan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Summary PO Bahan Baku 
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
                    <div class="form-group" style="font-weight: bold;">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2" runat="server" Width="100%" Style="font-weight: bold;" AutoPostBack="true" OnSelectedIndexChanged="ButtonCariTanggal_Click"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                            <thead>
                                                <tr class="active">
                                                    <th rowspan="2">No.</th>
                                                    <th rowspan="2">Jenis</th>
                                                    <th colspan="2">Baru</th>
                                                    <th colspan="2">Proses</th>
                                                    <th colspan="2">Selesai</th>
                                                    <th colspan="2">Total</th>
                                                    <th rowspan="2">Progress</th>
                                                </tr>
                                                <tr class="active">
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
                                                            <td class="fitSize text-right"><strong><%# Eval("GrandtotalBaru").ToFormatHarga() %></strong></td>
                                                            <td class="fitSize warning text-right"><strong><%# Eval("Proses").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize warning text-right"><strong><%# Eval("GrandtotalProses").ToFormatHarga() %></strong></td>
                                                            <td class="fitSize info text-right"><strong><%# Eval("Selesai").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize info text-right"><strong><%# Eval("GrandtotalSelesai").ToFormatHarga() %></strong></td>
                                                            <td class="fitSize success text-right"><strong><%# Eval("Total").ToFormatHargaBulat() %></strong></td>
                                                            <td class="fitSize success text-right"><strong><%# Eval("GrandtotalTotal").ToFormatHarga() %></strong></td>
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
                                                <tr class="success text-right" style="font-weight: bold;">
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
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-info">
                                            <div class="panel-heading"><strong>Summary</strong></div>
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
                                                            <td>Harga Supplier</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelHargaSupplierDetail" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Jumlah Bahan Baku</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelJumlahBahanBakuDetail" runat="server"></asp:Label></td>
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
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelPotongan" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Biaya lain-lain</td>
                                                            <td class="fitSize text-right warning">
                                                                <asp:Label ID="LabelBiayaLainLain" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Tax</td>
                                                            <td class="fitSize text-right warning">
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
                                        <div class="panel panel-info">
                                            <div class="panel-heading"><strong>Kategori</strong></div>
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
                                    <div class="col-md-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading"><strong>Penerimaan</strong></div>
                                            <div class="table-responsive">
                                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                    <thead>
                                                        <tr class="active">
                                                            <th>No.</th>
                                                            <%--<th>Bahan Baku</th>--%>
                                                            <%--<th>Satuan</th>--%>
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
                                                                    <%--<td><%# Eval("BahanBaku") %></td>--%>
                                                                    <%--<td class="fitSize text-center"><%# Eval("Satuan") %></td>--%>
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
                            <div class="col-md-6">
                                <div class="panel panel-danger">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Jatuh Tempo</strong>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body" style="padding: 0px;">
                                        <ul id="myTab" class="nav nav-tabs" role="tablist">
                                            <li role="presentation" class="active"><a href="#tabSatu" id="satu-tab" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">0 Hari</a></li>
                                            <li role="presentation"><a href="#tabDua" role="tab" id="dua-tab" data-toggle="tab" aria-controls="profile">
                                                <asp:Label ID="LabelPanelSetengahJatuhTempo" runat="server" Text="Label"></asp:Label></a></li>
                                            <li role="presentation"><a href="#tabTiga" role="tab" id="tiga-tab" data-toggle="tab" aria-controls="profile">
                                                <asp:Label ID="LabelPanelJatuhTempo" runat="server" Text="Label"></asp:Label></a></li>
                                            <li class="pull-right" style="font-weight:bold;">
                                                <asp:DropDownList ID="DropDownListJatuhTempo" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListJatuhTempo_SelectedIndexChanged">
                                                    <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Purchase Order" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Production To Vendor" Value="3"></asp:ListItem>
                                                </asp:DropDownList></li>
                                        </ul>
                                        <br />
                                        <div id="myTabContent" class="tab-content">
                                            <div role="tabpanel" class="tab-pane active" id="tabSatu" aria-labelledby="satu-tab">
                                                <div class="table-responsive">
                                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                        <thead>
                                                            <tr class="active">
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
                                                            <asp:Repeater ID="RepeaterPOBahanBakuJatuhTempoSatu" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class='<%# Eval("ClassWarna") %>'>
                                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
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
                                            <div role="tabpanel" class="tab-pane" id="tabDua" aria-labelledby="dua-tab">
                                                <div class="table-responsive">
                                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                        <thead>
                                                            <tr class="active">
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
                                                            <asp:Repeater ID="RepeaterPOBahanBakuJatuhTempoDua" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class='<%# Eval("ClassWarna") %>'>
                                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
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
                                            <div role="tabpanel" class="tab-pane" id="tabTiga" aria-labelledby="tiga-tab">
                                                <div class="table-responsive">
                                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                        <thead>
                                                            <tr class="active">
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
                                                            <asp:Repeater ID="RepeaterPOBahanBakuJatuhTempoTiga" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class='<%# Eval("ClassWarna") %>'>
                                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
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

