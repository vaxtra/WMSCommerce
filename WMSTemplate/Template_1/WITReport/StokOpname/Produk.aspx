<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITReport_StokOpname_Produk" %>

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
    Laporan Stock Opname Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
    <a href="/WITAdministrator/" class="btn btn-sm btn-danger">Keluar</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData"></asp:DropDownList>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th rowspan="2">No.</th>
                                        <th rowspan="2">Kode</th>
                                        <th rowspan="2">Produk</th>
                                        <th rowspan="2">Varian</th>
                                        <th rowspan="2">Warna</th>
                                        <th rowspan="2">Kategori</th>
                                        <th rowspan="2">Brand</th>
                                        <th rowspan="2">Stok Sebelum SO</th>
                                        <th rowspan="2">Stok Setelah SO</th>
                                        <th colspan="2">Qty</th>
                                        <th colspan="2">Nominal</th>
                                    </tr>
                                    <tr class="active" style="font-size: 16px;">
                                        <th>+</th>
                                        <th>-</th>
                                        <th>+</th>
                                        <th>-</th>
                                    </tr>
                                    <tr class="active">
                                        <th colspan="2"></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxProduk" runat="server" CssClass=" form-control input-sm"></asp:TextBox>
                                        </th>
                                        <th colspan="2"></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxKategori" runat="server" CssClass=" form-control input-sm"></asp:TextBox>
                                        </th>
                                        <th>
                                            <%--<asp:TextBox ID="TextBoxBrand" runat="server" CssClass=" form-control input-sm"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DropDownListBrand" runat="server" CssClass="select2">
                                            </asp:DropDownList>
                                        </th>
                                        <th colspan="2"></th>
                                        <th colspan="2" style="font-weight: bold;">
                                            <asp:Label ID="LabelGtandTotalSelisihQty" runat="server"></asp:Label>
                                        </th>
                                        <th colspan="2" style="font-weight: bold;">
                                            <asp:Label ID="LabelGtandTotalSelisihNominal" runat="server"></asp:Label>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="warning" style="font-weight: bold;">
                                        <td colspan="9"></td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahQtyPositif" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahQtyNegatif" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahNominalPositif" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahNominalNegatif" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td class="fitSize"><%# Eval("Kode") %></td>
                                                <td><%# Eval("NamaProduk") %></td>
                                                <td class="fitSize"><%# Eval("Varian") %></td>
                                                <td class="fitSize"><%# Eval("Warna") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                <td class="text-right success fitSize"><%# Eval("StokSebelumSO").ToFormatHargaBulat() %></td>
                                                <td class="text-right fitSize"><%# Eval("StokSetelahSO").ToFormatHargaBulat() %>
                                                    <%--<img src='<%# Pengaturan.FormatStatusStok((bool)Eval("Key.Status")) %>' />--%>
                                                </td>
                                                <td class="text-right fitSize"><%# Eval("SelisihQtyPositif").ToFormatHargaBulat() %>
                                                </td>
                                                <td class="text-right fitSize"><%# Eval("SelisihQtyNegatif").ToFormatHargaBulat() %>
                                                </td>
                                                <td class="text-right fitSize"><%# Eval("SelisihNominalPositif").ToFormatHargaBulat() %>
                                                </td>
                                                <td class="text-right fitSize"><%# Eval("SelisihNominalNegatif").ToFormatHargaBulat() %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <tr class="warning" style="font-weight: bold;">
                                        <td colspan="9"></td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahQtyPositif2" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahQtyNegatif2" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahNominalPositif2" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LabelTotalJumlahNominalNegatif2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
