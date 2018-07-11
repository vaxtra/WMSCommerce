<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="SisaPO.aspx.cs" Inherits="WITReport_Vendor_SisaPO" %>

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
    Laporan Sisa PO Produk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <%--<asp:Button ID="ButtonPrintPurchaseOrder" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcelPurchaseOrder" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcelPurchaseOrder_Click" />
    <a id="LinkDownloadPurchaseOrder" runat="server" visible="false">Download File</a>--%>
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
                <li><a href="Penerimaan.aspx">Penerimaan</a></li>
                <li class="active"><a href="#tabSisaPO" id="SisaPO-tab" data-toggle="tab">Sisa Pengiriman</a></li>
                <li><a href="Retur.aspx">Retur</a></li>
                <li><a href="Penagihan.aspx">Penagihan</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabSisaPO">
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
                                                <th class="text-center">No</th>
                                                <th class="text-center">ID</th>
                                                <th class="text-center">Tempat</th>
                                                <th class="text-center">Vendor</th>
                                                <th class="text-center">Tanggal</th>
                                                <th class="text-center">Kode</th>
                                                <th class="text-center">Produk</th>
                                                <th class="text-center">Varian</th>
                                                <th class="text-center">Harga</th>
                                                <th class="text-center">Jumlah</th>
                                                <th class="text-center">Terima</th>
                                                <th class="text-center">Sisa</th>
                                                <th class="text-center">Subtotal</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="success" style="font-weight: bold;">
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCariIDPOProduksiProdukPurchaseOrder" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariPurchaseOrder(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariTempatPurchaseOrder" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariVendorPurchaseOrder" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder"></asp:DropDownList></td>
                                                <td colspan="7"></td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListCariStatusSisaPurchaseOrder" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventPurchaseOrder">
                                                        <asp:ListItem Text="Tersisa" Value="Tersisa" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Tidak Tersisa" Value="TidakTersisa"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td class="text-right" style="vertical-align: middle;">
                                                    <asp:Label ID="LabelSubtotalHeaderPurchaseOrder" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="RepeaterPurchaseOrder" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td rowspan='<%# Eval("CountProduk") %>' class="fitSize">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><a target="_blank" href='<%# Eval("Link")%><%# Eval("IDPOProduksiProduk") %>'><%# Eval("IDPOProduksiProduk") %></a></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Tempat") %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Vendor") %></td>
                                                        <td rowspan='<%# Eval("CountProduk") %>'><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                        <td><%# Eval("Produk.KodeKombinasiProduk") %></td>
                                                        <td><%# Eval("Produk.Nama") %></td>
                                                        <td class="text-center"><%# Eval("Produk.AtributProduk") %></td>
                                                        <td class="text-center"><%# Eval("Produk.Harga").ToFormatHarga() %></td>
                                                        <td class="text-right"><%# Eval("Produk.Jumlah").ToFormatHargaBulat() %></td>
                                                        <td class="text-right"><%# Eval("Produk.Terima").ToFormatHargaBulat() %></td>
                                                        <td class="text-right"><%# Eval("Produk.Sisa").ToFormatHargaBulat() %></td>
                                                        <td class="text-right warning"><strong><%# Eval("Produk.Subtotal").ToFormatHarga() %></strong></td>
                                                    </tr>
                                                    <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("Detail") %>'>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("KodeKombinasiProduk") %></td>
                                                                <td><%# Eval("Nama") %></td>
                                                                <td class="text-center"><%# Eval("AtributProduk") %></td>
                                                                <td class="text-center"><%# Eval("Harga").ToFormatHarga() %></td>
                                                                <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                                <td class="text-right"><%# Eval("Terima").ToFormatHargaBulat() %></td>
                                                                <td class="text-right"><%# Eval("Sisa").ToFormatHargaBulat() %></td>
                                                                <td class="text-right warning"><strong><%# Eval("Subtotal").ToFormatHarga() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tr class="text-right success" style="font-weight: bold;">
                                            <td colspan="12"></td>
                                            <td class="text-right" style="vertical-align: middle;">
                                                <asp:Label ID="LabelSubtotalFooterPurchaseOrder" runat="server" Text="0"></asp:Label>
                                            </td>
                                        </tr>
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

