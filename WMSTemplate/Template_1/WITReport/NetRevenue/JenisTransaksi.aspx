<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="JenisTransaksi.aspx.cs" Inherits="WITReport_NetRevenue_JenisTransaksi" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Net Revenue
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <asp:Button ID="ButtonNetRevenue" runat="server" Text="Kembali" CssClass="btn btn-danger btn-sm" OnClick="ButtonNetRevenue_Click" />
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
                                <h4>Net Revenue Jenis Transaksi <%= Result != null ? Result["Periode"] : "" %></h4>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Net Revenue
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="text-right success" style="font-size: 14px;">
                                                <td colspan="3" class="text-center"><strong>GRANDTOTAL</strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelHeaderGrandtotalJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelHeaderGrandtotalGross" runat="server" Text="0"></asp:Label></strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelHeaderGrandtotalDiscount" runat="server" Text="0"></asp:Label></strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelHeaderGrandtotalNetRevenue" runat="server" Text="0"></asp:Label></strong></td>
                                                <td runat="server" id="HeaderGrandtotalCOGS"><strong>
                                                    <asp:Label ID="LabelHeaderGrandtotalCOGS" runat="server" Text="0"></asp:Label></strong></td>
                                                <td runat="server" id="HeaderGrandtotalGrossProfit"><strong>
                                                    <asp:Label ID="LabelHeaderGrandtotalGrossProfit" runat="server" Text="0"></asp:Label></strong></td>
                                            </tr>
                                            <tr class="active">
                                                <td colspan="9"></td>
                                            </tr>
                                        </thead>
                                        <asp:Repeater ID="RepeaterLaporan" runat="server">
                                            <ItemTemplate>
                                                <thead>
                                                    <tr class="info">
                                                        <td colspan="12" style="font-size: 14pt; text-align: left;"><strong>
                                                            <asp:Label ID="LabelNama" runat="server" Text='<%# Eval("JenisTransaksi") %>'></asp:Label></strong></td>
                                                    </tr>
                                                    <tr class="active">
                                                        <th class="text-center">No.</th>
                                                        <th class="text-center">Brand</th>
                                                        <th class="text-center">Kategori</th>
                                                        <th class="text-center">Quantity</th>
                                                        <th class="text-center">Gross</th>
                                                        <th class="text-center">Discount</th>
                                                        <th class="text-center">Net Revenue</th>
                                                        <th class="text-center" runat="server" id="TitleCOGS">COGS</th>
                                                        <th class="text-center" runat="server" id="TitleGrossProfit">Gross Profit</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                                <td class="fitSize"><%# Eval("Kategori") %></td>
                                                                <td class="text-right fitSize"><%# Eval("JumlahProduk").ToFormatHargaBulat() %></td>
                                                                <td class="text-right fitSize"><%# Eval("Gross").ToFormatHarga() %></td>
                                                                <td class="text-right fitSize"><%# Eval("Discount").ToFormatHarga() %></td>
                                                                <td class="text-right success fitSize"><%# Eval("NetRevenue").ToFormatHarga() %></td>
                                                                <td class="text-right fitSize" runat="server" id="PanelCOGS"><%# Eval("COGS").ToFormatHarga() %></td>
                                                                <td class="text-right fitSize" runat="server" id="PanelGrossProfit"><strong><%# Eval("GrossProfit").ToFormatHarga() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="text-right warning">
                                                        <td colspan="3" class="text-center"><strong>TOTAL</strong></td>
                                                        <td><strong><%# Eval("TotalJumlahProduk").ToFormatHargaBulat() %></strong></td>
                                                        <td><strong><%# Eval("TotalGross").ToFormatHarga() %></strong></td>
                                                        <td><strong><%# Eval("TotalDiscount").ToFormatHarga() %></strong></td>
                                                        <td><strong><%# Eval("TotalNetRevenue").ToFormatHarga() %></strong></td>
                                                        <td runat="server" id="FooterCOGS"><strong><%# Eval("TotalCOGS").ToFormatHarga() %></strong></td>
                                                        <td runat="server" id="FooterGrossProfit"><strong><%# Eval("TotalGrossProfit").ToFormatHarga() %></strong></td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tfoot>
                                            <tr class="active">
                                                <td colspan="9"></td>
                                            </tr>
                                            <tr class="text-right success" style="font-size: 14px;">
                                                <td colspan="3" class="text-center"><strong>GRANDTOTAL</strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelFooterGrandtotalJumlahProduk" runat="server" Text="0"></asp:Label></strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelFooterGrandtotalGross" runat="server" Text="0"></asp:Label></strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelFooterGrandtotalDiscount" runat="server" Text="0"></asp:Label></strong></td>
                                                <td><strong>
                                                    <asp:Label ID="LabelFooterGrandtotalNetRevenue" runat="server" Text="0"></asp:Label></strong></td>
                                                <td runat="server" id="FooterGrandtotalCOGS"><strong>
                                                    <asp:Label ID="LabelFooterGrandtotalCOGS" runat="server" Text="0"></asp:Label></strong></td>
                                                <td runat="server" id="FooterGrandtotalGrossProfit"><strong>
                                                    <asp:Label ID="LabelFooterGrandtotalGrossProfit" runat="server" Text="0"></asp:Label></strong></td>
                                            </tr>
                                        </tfoot>
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

