<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITReport_TransferStok_Produk" %>

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
    Laporan Transfer Produk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-secondary btn-const" OnClick="ButtonExcel_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-secondary btn-const mr-1" />
    <a href="ProdukDetail.aspx" class="btn btn-primary btn-const">Detail</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
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
        <div class="card">
            <div class="card-header bg-gradient-green">
                <ul class="nav nav-tabs card-header-tabs">
                    <li class="nav-item"><a href="../PerpindahanStok/Produk.aspx" class="nav-link" style="color: #FFFFFF !important;">Mutasi</a></li>
                    <li class="nav-item"><a href="../PerpindahanStok/StockCardProduk.aspx" class="nav-link" style="color: #FFFFFF !important;">Stock Card</a></li>
                    <li class="nav-item"><a href="#tabTransfer" id="Transfer-tab" class="nav-link active" data-toggle="tab">Transfer</a></li>
                </ul>
            </div>
            <div class="card-body">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="tabTransfer">
                        <asp:UpdatePanel ID="UpdatePanelTransfer" runat="server">
                            <ContentTemplate>
                                <h4 class="text-uppercase mb-3">
                                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                                <div class="table-responsive">
                                    <table class="table table-sm table-bordered table-hover">
                                        <thead>
                                            <tr class="thead-light">
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
                                            <tr class="thead-light">
                                                <th></th>
                                                <th>
                                                    <asp:TextBox ID="TextBoxCariIDTransfer" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%;"></asp:TextBox></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariTempatPengirimTransfer" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariPengirimTransfer" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariTempatPenerimaTransfer" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariPenerimaTransfer" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                                <th></th>
                                                <th class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalJumlahHeaderTransfer" runat="server" Text="0"></asp:Label></th>
                                                <th class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalGrandtotalHeaderTransfer" runat="server" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:DropDownList ID="DropDownListCariStatusTransfer" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                                        <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Pending" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Proses" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Selesai" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="Batal" Value="3"></asp:ListItem>
                                                    </asp:DropDownList></th>
                                                <th>
                                                    <asp:TextBox ID="TextBoxCariKeteranganTransfer" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)" Style="width: 100%;"></asp:TextBox></th>
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
                                                        <td class="text-right fitSize"><strong><%# Eval("TotalJumlah") %></strong></td>
                                                        <td class="text-right table-warning fitSize"><strong><%# Eval("GrandtotalHargaJual") %></strong></td>
                                                        <td class="text-center fitSize"><%# Pengaturan.StatusTransfer(Eval("EnumJenisTransfer").ToString()) %></td>
                                                        <td><%# Eval("Keterangan") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="font-weight-bold table-success">
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
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

