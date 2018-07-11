<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="PerformanceVendor.aspx.cs" Inherits="WITReport_POProduksi_PerformanceVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Performa Vendor
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTitleRight" runat="server">
        <ContentTemplate>
            <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-sm btn-danger" OnClick="ButtonKembali_Click" Visible="false" />
            <asp:Button ID="ButtonKembaliPOProduksi" runat="server" Text="Kembali" CssClass="btn btn-sm btn-danger" OnClick="ButtonKembaliPOProduksi_Click" Visible="false" />

            <asp:UpdateProgress ID="updateProgressTitleRight" runat="server" AssociatedUpdatePanelID="UpdatePanelTitleRight">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressTitleRight" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
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
                <li><a href="PerformancePengguna.aspx">Pegawai</a></li>
                <li><a href="PerformanceSupplier.aspx">Supplier</a></li>
                <li class="active"><a href="tabVendor" id="Vendor-tab" data-toggle="tab">Vendor</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabVendor">
                    <asp:UpdatePanel ID="UpdatePanelPerformanceVendor" runat="server">
                        <ContentTemplate>
                            <asp:MultiView ID="MultiViewPerformance" runat="server">
                                <asp:View ID="ViewGrafik" runat="server">
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
                                        <h4>Laporan [
                        <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                                            ]</h4>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        Purchase Order
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                            <thead>
                                                                <tr class="active">
                                                                    <th>No</th>
                                                                    <th>Vendor</th>
                                                                    <th>Total PO</th>
                                                                    <th>Ketepatan Pengiriman</th>
                                                                    <th>Kualitas Penerimaan</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepeaterPerformancePurchaseOrder" runat="server" OnItemCommand="RepeaterPOProduksi_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                            <td class="fitSize">
                                                                                <asp:LinkButton ID="LinkButtonVendor" runat="server" CommandName="Vendor" CommandArgument='<%# Eval("IDVendor") %>'><%# Eval("Vendor") %></asp:LinkButton></td>
                                                                            <td class="text-right fitSize"><%# Eval("TotalPO").ToFormatHargaBulat() %></td>
                                                                            <td>
                                                                                <div class="progress" style="margin: 0px;">
                                                                                    <%# Pengaturan.ProgressBar((int)Eval("Pengiriman")) %>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="progress" style="margin: 0px;">
                                                                                    <%# Pengaturan.ProgressBar((int)Eval("Penerimaan")) %>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        In-House Production
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                            <thead>
                                                                <tr class="active">
                                                                    <th>No</th>
                                                                    <th>Tempat</th>
                                                                    <th>Total PO</th>
                                                                    <th>Ketepatan Pengiriman</th>
                                                                    <th>Kualitas Penerimaan</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepeaterPerformanceInHouseProduction" runat="server" OnItemCommand="RepeaterPOProduksi_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                            <td class="fitSize">
                                                                                <asp:LinkButton ID="LinkButtonVendor" runat="server" CommandName="Tempat" CommandArgument='<%# Eval("IDTempat") %>'><%# Eval("Tempat") %></asp:LinkButton></td>
                                                                            <td class="text-right fitSize"><%# Eval("TotalPO").ToFormatHargaBulat() %></td>
                                                                            <td>
                                                                                <div class="progress" style="margin: 0px;">
                                                                                    <%# Pengaturan.ProgressBar((int)Eval("Pengiriman")) %>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="progress" style="margin: 0px;">
                                                                                    <%# Pengaturan.ProgressBar((int)Eval("Penerimaan")) %>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        Production to Supplier
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                            <thead>
                                                                <tr class="active">
                                                                    <th>No</th>
                                                                    <th>Vendor</th>
                                                                    <th>Total PO</th>
                                                                    <th>Ketepatan Pengiriman</th>
                                                                    <th>Kualitas Penerimaan</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepeaterPerformanceProductionToVendor" runat="server" OnItemCommand="RepeaterPOProduksi_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                            <td class="fitSize">
                                                                                <asp:LinkButton ID="LinkButtonVendor" runat="server" CommandName="Vendor" CommandArgument='<%# Eval("IDVendor") %>'><%# Eval("Vendor") %></asp:LinkButton></td>
                                                                            <td class="text-right fitSize"><%# Eval("TotalPO").ToFormatHargaBulat() %></td>
                                                                            <td>
                                                                                <div class="progress" style="margin: 0px;">
                                                                                    <%# Pengaturan.ProgressBar((int)Eval("Pengiriman")) %>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="progress" style="margin: 0px;">
                                                                                    <%# Pengaturan.ProgressBar((int)Eval("Penerimaan")) %>
                                                                                </div>
                                                                            </td>
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
                                </asp:View>
                                <asp:View ID="ViewPOProduksi" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:HiddenField ID="HiddenFieldID" runat="server" />
                                            <label>Nama</label>
                                            <asp:TextBox ID="TextBoxNamaVendorTempat" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                    <thead id="headPurchaseOrder" runat="server">
                                                        <tr class="info">
                                                            <th colspan="11" style="font-size: 14pt; text-align: left"><strong>PURCHASE ORDER</strong></th>
                                                        </tr>
                                                        <tr class="active">
                                                            <th>No</th>
                                                            <th>ID</th>
                                                            <th>Supplier</th>
                                                            <th>PIC</th>
                                                            <th>Tanggal</th>
                                                            <th>Pengiriman</th>
                                                            <th>Terkahir Terima</th>
                                                            <th>Jumlah</th>
                                                            <th>Tolak</th>
                                                            <th>Grandtotal</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="bodyPurchaseOrder" runat="server">
                                                        <asp:Repeater ID="RepeaterPurchaseOrder" runat="server" OnItemCommand="RepeaterPOProduksiDetail_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td class="fitSize">
                                                                        <asp:LinkButton ID="LinkButtonPengguna" runat="server" CommandName='<%# Eval("IDPOProduksiProduk") %>' CommandArgument='<%# Eval("IDPOProduksiProduk") %>'><%# Eval("IDPOProduksiProduk") %></asp:LinkButton></td>
                                                                    <td class="fitSize"><%# Eval("SupplierVendor") %></td>
                                                                    <td class="fitSize"><%# Eval("PIC") %></td>
                                                                    <td class="fitSize"><%# Eval("Tanggal") %></td>
                                                                    <td class="fitSize"><%# Eval("TanggalPengiriman") %></td>
                                                                    <td class='<%# Eval("Tanggal_ClassTerakhirTerima") %>'><%# Eval("TanggalTerakhirTerima") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("TotalJumlah") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("TotalTolak") %></td>
                                                                    <td class="text-right warning fitSize"><strong><%# Eval("Grandtotal") %></strong></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                    <thead id="headProduksiSendiri" runat="server">
                                                        <tr class="info">
                                                            <th colspan="11" style="font-size: 14pt; text-align: left"><strong>IN-HOUSE PRODUCTION</strong></th>
                                                        </tr>
                                                        <tr class="active">
                                                            <th>No</th>
                                                            <th>ID</th>
                                                            <th>Tempat</th>
                                                            <th>PIC</th>
                                                            <th>Tanggal</th>
                                                            <th>Pengiriman</th>
                                                            <th>Terkahir Terima</th>
                                                            <th>Jumlah</th>
                                                            <th>Tolak</th>
                                                            <th>Grandtotal</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="bodyProduksiSendiri" runat="server">
                                                        <asp:Repeater ID="RepeaterProduksiSendiri" runat="server" OnItemCommand="RepeaterPOProduksiDetail_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td class="fitSize">
                                                                        <asp:LinkButton ID="LinkButtonPengguna" runat="server" CommandName='<%# Eval("IDPOProduksiProduk") %>' CommandArgument='<%# Eval("IDPOProduksiProduk") %>'><%# Eval("IDPOProduksiProduk") %></asp:LinkButton></td>
                                                                    <td class="fitSize"><%# Eval("Tempat") %></td>
                                                                    <td class="fitSize"><%# Eval("PIC") %></td>
                                                                    <td class="fitSize"><%# Eval("Tanggal") %></td>
                                                                    <td class="fitSize"><%# Eval("TanggalPengiriman") %></td>
                                                                    <td class='<%# Eval("Tanggal_ClassTerakhirTerima") %>'><%# Eval("TanggalTerakhirTerima") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("TotalJumlah") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("TotalTolak") %></td>
                                                                    <td class="text-right warning fitSize"><strong><%# Eval("Grandtotal") %></strong></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                    <thead id="headProduksiKeSupplier" runat="server">
                                                        <tr class="info">
                                                            <th colspan="11" style="font-size: 14pt; text-align: left"><strong>PRODUCTION TO SUPPLIER/VENDOR</strong></th>
                                                        </tr>
                                                        <tr class="active">
                                                            <th>No</th>
                                                            <th>ID</th>
                                                            <th>Supplier / Vendor</th>
                                                            <th>PIC</th>
                                                            <th>Tanggal</th>
                                                            <th>Pengiriman</th>
                                                            <th>Terkahir Terima</th>
                                                            <th>Jumlah</th>
                                                            <th>Tolak</th>
                                                            <th>Grandtotal</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="bodyProduksiKeSupplier" runat="server">
                                                        <asp:Repeater ID="RepeaterProduksiKeSupplier" runat="server" OnItemCommand="RepeaterPOProduksiDetail_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td class="fitSize">
                                                                        <asp:LinkButton ID="LinkButtonPengguna" runat="server" CommandName='<%# Eval("IDPOProduksiProduk") %>' CommandArgument='<%# Eval("IDPOProduksiProduk") %>'><%# Eval("IDPOProduksiProduk") %></asp:LinkButton></td>
                                                                    <td class="fitSize"><%# Eval("SupplierVendor") %></td>
                                                                    <td class="fitSize"><%# Eval("PIC") %></td>
                                                                    <td class="fitSize"><%# Eval("Tanggal") %></td>
                                                                    <td class="fitSize"><%# Eval("TanggalPengiriman") %></td>
                                                                    <td class='<%# Eval("Tanggal_ClassTerakhirTerima") %>'><%# Eval("TanggalTerakhirTerima") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("TotalJumlah") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("TotalTolak") %></td>
                                                                    <td class="text-right warning fitSize"><strong><%# Eval("Grandtotal") %></strong></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="ViewPOProduksiDetail" runat="server">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <strong>Purchase Order</strong>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Proyeksi</label>
                                                                <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">ID Purchase Order</label>
                                                                <asp:TextBox ID="TextBoxIDPOProduksiBahanBaku" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">PIC</label>
                                                                <asp:TextBox ID="TextBoxPegawaiPIC" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Status HPP</label>
                                                                <asp:TextBox ID="TextBoxStatusHPP" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Pegawai</label>
                                                                <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label class="form-label bold">Tanggal Jatuh Tempo</label>
                                                                <asp:TextBox ID="TextBoxTanggalJatuhTempo" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label class="form-label bold">Tanggal Pengiriman</label>
                                                                <asp:TextBox ID="TextBoxTanggalPengiriman" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <strong>Vendor</strong>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Vendor / Tempat Produksi</label>
                                                                <asp:TextBox ID="TextBoxVendorTempat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Email</label>
                                                                <asp:TextBox ID="TextBoxEmail" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="form-label bold">Alamat</label>
                                                        <asp:TextBox ID="TextBoxAlamat" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Telepon 1</label>
                                                                <asp:TextBox ID="TextBoxTelepon1" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="form-label bold">Telepon 2</label>
                                                                <asp:TextBox ID="TextBoxTelepon2" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DetailProduk" runat="server" class="form-group">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <strong>Produk</strong>
                                            </div>
                                            <div class="table-responsive">
                                                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                    <thead>
                                                        <tr class="active">
                                                            <th>No</th>
                                                            <th>Kode</th>
                                                            <th>Produk</th>
                                                            <th>Varian</th>
                                                            <th>Kategori</th>
                                                            <th>Harga</th>
                                                            <th>Jumlah</th>
                                                            <th>Datang</th>
                                                            <th>Sisa</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterPOProduksiProdukDetail" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                                    <td><%# Eval("Produk") %></td>
                                                                    <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                                    <td><%# Eval("Kategori") %></td>
                                                                    <td class="text-right fitSize"><%# Eval("Harga").ToFormatHarga() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("Datang").ToFormatHargaBulat() %></td>
                                                                    <td class="text-right fitSize"><%# Eval("Sisa").ToFormatHargaBulat() %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <asp:UpdateProgress ID="updateProgressPerformanceVendor" runat="server" AssociatedUpdatePanelID="UpdatePanelPerformanceVendor">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressPenerimaanPOProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

