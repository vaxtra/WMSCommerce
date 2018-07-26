<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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
    Laporan Perpindahan Stok Produk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
    <a href="ProdukDetail.aspx" class="btn btn-sm btn-info">Detail</a>
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
                <li class="active"><a href="#tabPerpindahanStok" id="PerpindahanStok-tab" data-toggle="tab">Mutasi</a></li>
                <li><a href="StockCardProduk.aspx">Stock Card</a></li>
                <li><a href="../TransferStok/Produk.aspx">Transfer</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabPerpindahanStok">
                    <asp:UpdatePanel ID="UpdatePanelProduk" runat="server">
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
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                        <thead>
                                            <tr class="active">
                                                <th>No.</th>
                                                <th>Tempat</th>
                                                <th>Kode</th>
                                                <th>Produk</th>
                                                <th>Varian</th>
                                                <th>Kategori</th>
                                                <th>Jenis</th>
                                                <th>Jumlah</th>
                                            </tr>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariKode" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariAtributProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariKategori" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariJenisPerpindahanStok" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="LabelTotalJumlahHeader" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td class="fitSize"><%# Eval("Tempat") %></td>
                                                        <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                        <td><%# Eval("Produk") %></td>
                                                        <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                        <td><%# Eval("Kategori") %></td>
                                                        <td class="fitSize"><%# Eval("Jenis") %></td>
                                                        <td class="text-right fitSize"><%# Eval("Jumlah") %>
                                                            <img src='<%# Pengaturan.FormatStatusStok((bool)Eval("Status")) %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="success" style="font-weight: bold;">
                                                <td colspan="7"></td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="LabelTotalJumlahFooter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>

                            <asp:UpdateProgress ID="updateProgressProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelProduk">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

