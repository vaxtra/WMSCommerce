<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="ProdukDetail.aspx.cs" Inherits="WITReport_TransferStok_ProdukDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Transfer Produk Detail
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrintTransferDetail" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcelTransferDetail" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcelTransferDetail_Click" />
    <a id="LinkDownloadTransferDetail" runat="server" visible="false">Download File</a>
    <a href="Produk.aspx" class="btn btn-sm btn-danger">Kembali</a>
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
                    <asp:UpdatePanel ID="UpdatePanelTransferDetail" runat="server">
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
                                    <div class="col-sm-4 col-md-4" style="font-weight: bold;">
                                        <asp:DropDownList ID="DropDownListCariTempatTransferDetail" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4" style="font-weight: bold;">
                                        <asp:DropDownList ID="DropDownListCariPengirimPenerimaTransferDetail" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail">
                                            <asp:ListItem Text="Pengirim" Value="Pengirim" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Penerima" Value="Penerima"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4" style="font-weight: bold;">
                                        <asp:DropDownList ID="DropDownListCariStatusTransferDetail" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail">
                                            <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Proses" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Selesai" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Batal" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
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
                                                <th>Kode</th>
                                                <th>Brand</th>
                                                <th>Produk</th>
                                                <th>Varian</th>
                                                <th>Kategori</th>
                                                <th>Jumlah</th>
                                                <th>Subtotal</th>
                                            </tr>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariKodeTransferDetail" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariTransferDetail(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPemilikProdukTransferDetail" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariProdukTransferDetail" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariAtributProdukTransferDetail" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariKategoriTransferDetail" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventTransferDetail"></asp:DropDownList></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalJumlahHeaderTransferDetail" runat="server" Text="0"></asp:Label></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalSubtotalHeaderTransferDetail" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterTransferDetail" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                        <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                        <td class="fitSize"><%# Eval("Produk") %></td>
                                                        <td class="text-center fitSize"><%# Eval("AtributProduk") %></td>
                                                        <td><%# Eval("Kategori") %></td>
                                                        <td class="text-right fitSize"><strong><%# Eval("Jumlah") %></strong></td>
                                                        <td class="text-right warning fitSize"><strong><%# Eval("SubtotalHargaJual") %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="success" style="font-weight: bold;">
                                                <td colspan="6"></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalJumlahFooterTransferDetail" runat="server" Text="0"></asp:Label></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelTotalSubtotalFooterTransferDetail" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProgressTransferDetail" runat="server" AssociatedUpdatePanelID="UpdatePanelTransferDetail">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressTransferDetail" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

