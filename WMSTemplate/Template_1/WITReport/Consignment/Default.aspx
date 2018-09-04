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
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
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
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListBrand" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <h4 class="text-uppercase mb-3">
                <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
            <div class="card">
                <h5 class="card-header bg-gradient-green">SALES</h5>
                <div class="table-responsive">
                    <table class="table table-sm table-bordered table-hover">
                        <thead>
                            <tr class="thead-light">
                                <th rowspan="2">No.</th>
                                <th class="breakWord" rowspan="2">Brand</th>
                                <th class="breakWord" rowspan="2">Produk</th>
                                <th class="fitSize" rowspan="2">Warna</th>
                                <th class="fitSize" rowspan="2">Varian</th>

                                <th class="table-warning" colspan="3">Stock</th>
                                <th class="table-info" colspan="6">Sales</th>
                            </tr>
                            <tr class="thead-light">
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
                            <tr style="font-weight: bold;" class="table-success">
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
                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
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

                            <tr style="font-weight: bold;" class="table-success">
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
