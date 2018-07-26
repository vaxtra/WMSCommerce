<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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
    Laporan Penjualan Konsinyasi
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
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
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                    </div>
                    <div class="col-sm-6 col-md-6" style="font-weight: bold;">
                        <asp:DropDownList ID="DropDownListBrand" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                        <thead>
                            <tr>
                                <th class="active" rowspan="2">No.</th>
                                <th class="active breakWord" rowspan="2">Brand</th>
                                <th class="active breakWord" rowspan="2">Produk</th>
                                <th class="active fitSize" rowspan="2">Warna</th>
                                <th class="active fitSize" rowspan="2">Varian</th>

                                <th class="warning" colspan="3">Stock</th>
                                <th class="info" colspan="6">Sales</th>
                            </tr>
                            <tr class="active">
                                <th class="text-right">Quantity</th>
                                <th class="text-right">Harga</th>
                                <th class="text-right">Nominal</th>

                                <th class="text-right">Quantity</th>
                                <th class="text-right">Before Disc.</th>
                                <th class="text-right">Disc.</th>
                                <th class="text-right">Subtotal</th>
                                <th class="text-right">Consignment</th>
                                <th class="text-right">Pay to Brand</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr style="font-weight: bold;" class="success">
                                <td colspan="5">Total Produk :
                                <asp:Label ID="LabelTotalProduk" runat="server"></asp:Label></td>
                                <td class="text-right">
                                    <asp:Label ID="LabelStok" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td class="text-right">
                                    <asp:Label ID="LabelNominalStok" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelBeforeDiscount" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelDiscount" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelSubtotal" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelConsignment" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelPayToBrand" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.ItemIndex + 1 %></td>
                                        <td class="breakWord"><%# Eval("Key.Brand") %></td>
                                        <td class="breakWord"><%# Eval("Key.Produk") %></td>
                                        <td class="fitSize"><%# Eval("Key.Warna") %></td>
                                        <td class="fitSize text-center"><%# Eval("Key.Varian") %></td>
                                        <td class="text-right"><%# Eval("Stok").ToFormatHargaBulat() %></td>
                                        <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("NominalStok").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Penjualan.Quantity").ToFormatHargaBulat() %></td>
                                        <td class="text-right"><%# Eval("Penjualan.BeforeDiscount").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Penjualan.Discount").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Penjualan.Subtotal").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Penjualan.Consignment").ToFormatHarga()%></td>
                                        <td class="text-right"><%# Eval("Penjualan.PayToBrand").ToFormatHarga()%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                            <tr style="font-weight: bold;" class="success">
                                <td colspan="5">Total Produk :
                                <asp:Label ID="LabelTotalProduk1" runat="server"></asp:Label></td>
                                <td class="text-right">
                                    <asp:Label ID="LabelStok1" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td class="text-right">
                                    <asp:Label ID="LabelNominalStok1" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelQuantity1" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelBeforeDiscount1" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelDiscount1" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelSubtotal1" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelConsignment1" runat="server"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="LabelPayToBrand1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
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
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
