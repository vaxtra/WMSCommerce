﻿<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="PenerimaanDetail.aspx.cs" Inherits="WITReport_Supplier_PenerimaanDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCariPurchaseOrder(e) {
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Penerimaan PO Bahan Baku Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <%--<asp:Button ID="ButtonPrintPurchaseOrder" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcelPurchaseOrder" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcelPurchaseOrder_Click" />
    <a id="LinkDownloadPurchaseOrder" runat="server" visible="false">Download File</a>--%>
    <a href="Penerimaan.aspx" class="btn btn-sm btn-danger">Kembali</a>
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
                <li><a href="PurchaseOrder.aspx">Purchase Order</a></li>
                <li class="active"><a href="#tabPenerimaan" id="Penerimaan-tab" data-toggle="tab">Penerimaan</a></li>
                <li><a href="SisaPO.aspx">Sisa Pengiriman</a></li>
                <li><a href="Retur.aspx">Retur</a></li>
                <li><a href="Penagihan.aspx">Penagihan</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabPurchaseOrder">
                    <asp:UpdatePanel ID="UpdatePanelPurchaseOrder" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
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
                                                <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" ClientIDMode="Static" />
                                            </div>
                                        </div>
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
                                                <th>ID Penerimaan</th>
                                                <th>ID PO</th>
                                                <th>Tempat</th>
                                                <th>Supplier</th>
                                                <th>Pegawai</th>
                                                <th>Tanggal</th>
                                                <th>Kode</th>
                                                <th>Bahan Baku</th>
                                                <th>Satuan</th>
                                                <th>Kategori</th>
                                                <th>Datang</th>
                                                <th>Diterima</th>
                                                <th>Tolak Supplier</th>
                                                <th>Tolak Gudang </th>
                                                <th>Subtotal</th>
                                            </tr>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariIDPenerimaanPOProduksiBahanBakuPurchaseOrder" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariPurchaseOrder(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariIDPOProduksiBahanBakuPurchaseOrder" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariPurchaseOrder(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariTempatPurchaseOrder" CssClass="select2 center-text" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariSupplierPurchaseOrder" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariPenggunaPurchaseOrder" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariKodePurchaseOrder" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariPurchaseOrder(event)" Style="width: 100%;"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariBahanBakuPurchaseOrder" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariSatuanPurchaseOrder" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariKategoriPurchaseOrder" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td colspan="4"></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelSubtotalPurchaseOrderHeader" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterPurchaseOrder" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                        <td class="fitSize"><%# Eval("IDPenerimaanPOProduksiBahanBaku") %></td>
                                                        <td class="fitSize"><%# Eval("IDPOProduksiBahanBaku") %></td>
                                                        <td class="fitSize"><%# Eval("Tempat") %></td>
                                                        <td class="fitSize"><%# Eval("Supplier") %></td>
                                                        <td class="fitSize"><%# Eval("Pengguna") %></td>
                                                        <td class="fitSize"><%# Eval("Tanggal") %></td>
                                                        <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                                        <td class="fitSize"><%# Eval("BahanBaku") %></td>
                                                        <td class="fitSize"><%# Eval("Satuan") %></td>
                                                        <td><%# Eval("Kategori") %></td>
                                                        <td class="text-right fitSize"><%# Eval("Datang") %></td>
                                                        <td class="text-right fitSize"><%# Eval("Diterima") %></td>
                                                        <td class="text-right fitSize"><%# Eval("TolakKeSupplier") %></td>
                                                        <td class="text-right fitSize"><%# Eval("TolakKeGudang") %></td>
                                                        <td class="text-right warning fitSize"><strong><%# Eval("Subtotal") %></strong></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="success" style="font-weight: bold;">
                                                <td colspan="15"></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelSubtotalPurchaseOrderFooter" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="updateProgressPurchaseOrder" runat="server" AssociatedUpdatePanelID="UpdatePanelPurchaseOrder">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressPurchaseOrder" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

