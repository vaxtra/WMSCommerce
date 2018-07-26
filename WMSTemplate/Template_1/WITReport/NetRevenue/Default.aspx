<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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
    Laporan Net Revenue
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <asp:Button ID="ButtonJenisTransaksi" runat="server" Text="Jenis Transaksi" CssClass="btn btn-info btn-sm" OnClick="ButtonJenisTransaksi_Click" />
    <br />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
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
                <li><a href="../Transaksi/Default.aspx">Transaksi</a></li>
                <li><a href="../Transaksi/PenjualanProduk.aspx">Produk</a></li>
                <li class="active"><a href="#tabNetRevenue" id="NetRevenue-tab" data-toggle="tab">Net Revenue</a></li>
                <li><a href="../Transaksi/JenisPembayaran/Default.aspx">Jenis Pembayaran</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabNetRevenue">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:ListBox ID="ListBoxTempat" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple"></asp:ListBox>
                                    <asp:ListBox ID="ListBoxJenisTransaksi" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple"></asp:ListBox>
                                    <asp:ListBox ID="ListBoxStatusTransaksi" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple"></asp:ListBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control input-sm TanggalJam" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control input-sm TanggalJam" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                                    <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
                                </div>
                            </div>
                            <div class="form-group">
                                <h4><%= Result != null ? Result["Periode"] : "" %></h4>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Jenis Pembayaran
                                        </div>
                                        <div class="table-responsive">
                                            <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
                                                <thead>
                                                    <tr class="active">
                                                        <th class="fitSize">No.</th>
                                                        <th class="text-center">Jenis Pembayaran</th>
                                                        <th class="text-center">Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="font-weight: bold;">
                                                        <td class="success" colspan="2"></td>
                                                        <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalJenisPembayaran"]) : "0" %></td>
                                                    </tr>

                                                    <asp:Repeater ID="RepeaterJenisPembayaran" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("Key.Nama") %></td>
                                                                <td class="info text-right"><%# Eval("Total").ToFormatHarga() %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                    <tr style="font-weight: bold;">
                                                        <td class="success" colspan="2"></td>
                                                        <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalJenisPembayaran"]) : "0" %></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Grandtotal
                                        </div>
                                        <table class="table table-bordered table-condensed table-hover" style="font-size: 12px; font-weight: bold;">
                                            <tr>
                                                <td>Sebelum Retur</td>
                                                <td class="text-right warning"><%= Result != null ? Parse.ToFormatHarga(Result["SebelumRetur"]) : "0" %></td>
                                            </tr>
                                            <tr>
                                                <td>Retur</td>
                                                <td class="text-right danger"><%= Result != null ? Parse.ToFormatHarga(Result["ReturNetRevenue"] * -1) : "0" %></td>
                                            </tr>
                                            <tr>
                                                <td>Grand Total</td>
                                                <td class="text-right success"><%= Result != null ? Parse.ToFormatHarga(Result["NetRevenue"]) : "0" %></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Transaksi
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th class="fitSize">No.</th>
                                                <th class="text-center">ID Transaksi</th>
                                                <th class="text-center">Tanggal</th>
                                                <th class="text-center">Jenis</th>
                                                <th class="text-center">Status</th>
                                                <th class="text-center">Produk</th>
                                                <th class="text-center">Kategori</th>
                                                <th class="text-center">Brand</th>
                                                <th class="text-center">Price</th>
                                                <th class="text-center">Discount</th>
                                                <th class="text-center">Biaya Pengiriman</th>
                                                <th class="text-center">Pembulatan</th>
                                                <th class="text-center">Grand Total</th>
                                                <th class="text-center">Net Revenue</th>
                                                <th class="text-center" runat="server" id="Title1COGS">COGS</th>
                                                <th class="text-center" runat="server" id="Title1GrossProfit">Gross Profit</th>
                                                <th class="text-center">Pembayaran</th>
                                                <th class="text-center">Keterangan</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr style="font-weight: bold;">
                                                <td class="success" colspan="8"></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPrice"]) : "0" %></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["BiayaPengiriman"]) : "0" %></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Pembulatan"]) : "0" %></td>
                                                <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["GrandTotal"]) : "0" %></td>
                                                <td class="warning text-right"><%= Result != null ? Parse.ToFormatHarga(Result["NetRevenue"]) : "0" %></td>
                                                <td class="success text-right" runat="server" id="Title2COGS"><%= Result != null ? Parse.ToFormatHarga(Result["TotalCOGS"]) : "0" %></td>
                                                <td class="success text-right" runat="server" id="Title2GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrossProfit"]) : "0" %></td>
                                                <td class="success" colspan="2"></td>
                                            </tr>

                                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><a href='/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>'><%# Eval("IDTransaksi") %></a></td>
                                                        <td class="fitSize" rowspan='<%# Eval("CountProduk") %>'><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                        <td class="fitSize" rowspan='<%# Eval("CountProduk") %>'><%# Eval("JenisTransaksi") %></td>
                                                        <td class="fitSize" rowspan='<%# Eval("CountProduk") %>'><%# Eval("StatusTransaksi") %></td>

                                                        <td class="fitSize"><%# Eval("Produk.Nama") %></td>
                                                        <td class="fitSize"><%# Eval("Produk.Kategori") %></td>
                                                        <td class="fitSize"><%# Eval("Produk.Brand") %></td>
                                                        <td class="text-right"><%# Eval("Produk.BeforeDiscount").ToFormatHarga() %></td>
                                                        <td class="text-right"><%# Eval("Produk.Discount").ToFormatHarga() %></td>

                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right"><%# Eval("BiayaPengiriman").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right"><%# Eval("Pembulatan").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right info"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right warning"><%# Eval("NetRevenue").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right" runat="server" id="PanelCOGS"><%# Eval("TotalHargaBeli").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="text-right" runat="server" id="PanelGrossProfit"><%# Eval("TotalGrossProfit").ToFormatHarga() %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize">
                                                            <asp:Repeater ID="RepeaterPembayaran" runat="server" DataSource='<%# Eval("Pembayaran") %>'>
                                                                <ItemTemplate>
                                                                    <%# Eval("Tanggal").ToFormatTanggalJam() %> - <%# Eval("JenisPembayaran") %> - <%# Eval("Total").ToFormatHarga() %><br />
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Keterangan") %></td>
                                                    </tr>
                                                    <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("Detail") %>'>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Eval("Nama") %></td>
                                                                <td class="fitSize"><%# Eval("Kategori") %></td>
                                                                <td class="fitSize"><%# Eval("Brand") %></td>
                                                                <td class="text-right"><%# Eval("BeforeDiscount").ToFormatHarga() %></td>
                                                                <td class="text-right"><%# Eval("Discount").ToFormatHarga() %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr style="font-weight: bold;">
                                                <td class="success" colspan="8"></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPrice"]) : "0" %></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["BiayaPengiriman"]) : "0" %></td>
                                                <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["Pembulatan"]) : "0" %></td>
                                                <td class="info text-right"><%= Result != null ? Parse.ToFormatHarga(Result["GrandTotal"]) : "0" %></td>
                                                <td class="warning text-right"><%= Result != null ? Parse.ToFormatHarga(Result["NetRevenue"]) : "0" %></td>
                                                <td class="success text-right" runat="server" id="Footer1COGS"><%= Result != null ? Parse.ToFormatHarga(Result["TotalCOGS"]) : "0" %></td>
                                                <td class="success text-right" runat="server" id="Footer1GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrossProfit"]) : "0" %></td>
                                                <td class="success" colspan="2"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    Retur
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th class="fitSize">No.</th>
                                                <th class="text-center">ID Transaksi</th>
                                                <th class="text-center">Jenis</th>
                                                <th class="text-center">Status</th>
                                                <th class="text-center">Keterangan</th>
                                                <th class="text-center">Produk</th>
                                                <th class="text-center">Kategori</th>
                                                <th class="text-center">Brand</th>
                                                <th class="text-center">Price</th>
                                                <th class="text-center">Discount</th>
                                                <th class="text-center">Qty</th>
                                                <th class="text-center">Net Revenue</th>
                                                <th class="text-center" runat="server" id="Title3COGS">COGS</th>
                                                <th class="text-center" runat="server" id="Title3GrossProfit">Gross Profit</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr style="font-weight: bold;" class="success">
                                                <td colspan="10"></td>
                                                <td class="text-right"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturQty"]) : "0" %></td>
                                                <td class="text-right"><%= Result != null ? Parse.ToFormatHarga(Result["ReturNetRevenue"]) : "0" %></td>
                                                <td class="text-right" runat="server" id="Title4COGS"><%= Result != null ? Parse.ToFormatHarga(Result["ReturCOGS"]) : "0" %></td>
                                                <td class="text-right" runat="server" id="Title4GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["ReturGrossProfit"]) : "0" %></td>
                                            </tr>

                                            <asp:Repeater ID="RepeaterRetur" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td><a href='/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>'><%# Eval("IDTransaksi") %></a></td>
                                                        <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                                        <td class="fitSize"><%# Eval("StatusTransaksi") %></td>
                                                        <td><%# Eval("Keterangan") %></td>
                                                        <td class="fitSize"><%# Eval("Produk") %></td>
                                                        <td class="fitSize"><%# Eval("Kategori") %></td>
                                                        <td class="fitSize"><%# Eval("Brand") %></td>
                                                        <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                        <td class="text-right"><%# Eval("Discount").ToFormatHarga() %></td>
                                                        <td class="text-right"><%# Eval("Quantity").ToFormatHarga() %></td>
                                                        <td class="text-right warning"><%# Eval("NetRevenue").ToFormatHarga() %></td>
                                                        <td class="text-right" runat="server" id="PanelCOGS"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                                        <td class="text-right" runat="server" id="PanelGrossProfit"><%# Eval("GrossProfit").ToFormatHarga() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr style="font-weight: bold;" class="success">
                                                <td colspan="10"></td>
                                                <td class="text-right"><%= Result != null ? Parse.ToFormatHargaBulat(Result["ReturQty"]) : "0" %></td>
                                                <td class="text-right"><%= Result != null ?Parse.ToFormatHarga(Result["ReturNetRevenue"]) : "0" %></td>
                                                <td class="text-right" runat="server" id="Footer2COGS"><%= Result != null ? Parse.ToFormatHarga(Result["ReturCOGS"]) : "0" %></td>
                                                <td class="text-right" runat="server" id="Footer2GrossProfit"><%= Result != null ? Parse.ToFormatHarga(Result["ReturGrossProfit"]) : "0" %></td>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

