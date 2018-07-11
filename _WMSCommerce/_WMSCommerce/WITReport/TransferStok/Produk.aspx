<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITReport_TransferStok_Produk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCariTransfer(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariTanggalTransfer');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        //Ketika memasukkan Kode Produk
        function Func_ButtonCariTransferDetail(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariTanggalTransferDetail');
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
    Laporan Transfer Produk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrintTransfer" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcelTransfer" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcelTransfer_Click" />
    <a id="LinkDownloadTransfer" runat="server" visible="false">Download File</a>
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
                <li><a href="../PerpindahanStok/Produk.aspx">Mutasi</a></li>
                <li><a href="../PerpindahanStok/StockCardProduk.aspx">Stock Card</a></li>
                <li class="active"><a href="#tabTransfer" id="Transfer-tab" data-toggle="tab">Transfer</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabTransfer">
                    <asp:UpdatePanel ID="UpdatePanelTransfer" runat="server">
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
                                                <th>No</th>
                                                <th>ID</th>
                                                <th colspan="2">Pengirim</th>
                                                <th>Tanggal Kirim</th>
                                                <th colspan="2">Penerima</th>
                                                <th>Tanggal Terima</th>
                                                <th>Jumlah</th>
                                                <th>Grandtotal</th>
                                                <th>Status</th>
                                                <th>Keterangan</th>
                                            </tr>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariIDTransfer" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariTransfer(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariTempatPengirimTransfer" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransfer"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPengirimTransfer" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransfer"></asp:DropDownList></td>
                                                <td></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariTempatPenerimaTransfer" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransfer"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPenerimaTransfer" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransfer"></asp:DropDownList></td>
                                                <td></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalJumlahHeaderTransfer" runat="server" Text="0"></asp:Label></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalGrandtotalHeaderTransfer" runat="server" Text="0"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariStatusTransfer" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransfer">
                                                        <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pending" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Proses" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Selesai" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="Batal" Value="3"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariKeteranganTransfer" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariTransfer(event)" Style="width: 100%;"></asp:TextBox></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterTransfer" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td class="fitSize"><%# Eval("IDTransferProduk") %></td>
                                                        <td class="fitSize"><%# Eval("TempatPengirim") %></td>
                                                        <td class="fitSize"><%# Eval("Pengirim") %></td>
                                                        <td class="fitSize"><%# Eval("TanggalKirim") %></td>
                                                        <td class="fitSize"><%# Eval("TempatPenerima") %></td>
                                                        <td class="fitSize"><%# Eval("Penerima") %></td>
                                                        <td class="fitSize"><%# Eval("TanggalTerima") %></td>
                                                        <td class="text-right warning fitSize"><strong><%# Eval("TotalJumlah") %></strong></td>
                                                        <td class="text-right warning fitSize"><strong><%# Eval("GrandtotalHargaJual") %></strong></td>
                                                        <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
                                                        <td><%# Eval("Keterangan") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="success" style="font-weight: bold;">
                                                <td colspan="8"></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalJumlahFooterTransfer" runat="server" Text="0"></asp:Label></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalGrandtotalFooterTransfer" runat="server" Text="0"></asp:Label></td>
                                                <td colspan="2"></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProgressransfer" runat="server" AssociatedUpdatePanelID="UpdatePanelTransfer">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressTransfer" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

