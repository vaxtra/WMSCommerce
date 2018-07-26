<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="_NilaiStok.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table1 {
            width: 200px;
            float: left;
        }

        .table2 {
            overflow: auto;
        }

            .table1 th, .table1 td, .table2 th, .table2 td {
                padding: 5px;
                white-space: nowrap;
            }
    </style>
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
    Laporan Nilai Stok Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <a id="LinkDownload" runat="server" visible="false">Download File</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="form-group">
        <div class="form-inline">
            <div class="form-group">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCari" runat="server" Text="Cari" OnClick="Event_LoadData" />
            </div>
        </div>
    </div>

    <div class="panel panel-success">
        <div class="panel-heading">
            Nilai Stok
        </div>
        <div class="table-responsive">
            <table class="table table-hover table-condensed table1" style="font-size: 12px;">
                <thead>
                    <tr>
                        <th class="text-center" style="width: 2%">No.</th>
                        <th class="text-center">Produk</th>
                    </tr>
                </thead>
                <tbody>
                <tbody style="font-size: 12px;">
                    <tr class="success" style="font-weight: bold;">
                        <td></td>
                        <td style="padding-top: 0; padding-bottom: 0; font-size: 18px;">
                            <asp:Label ID="LabelTotalHeader" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <asp:Repeater ID="RepeaterLaporanKolom1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><a href='/WITReport/PerpindahanStok/Detail.aspx?id=<%# Eval("IDProduk") %>'><%# Eval("Produk") %></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr class="success" style="font-weight: bold;">
                        <td></td>
                        <td style="padding-top: 0; padding-bottom: 0; font-size: 18px;">
                            <asp:Label ID="LabelTotalFooter" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>

            <div class="table2">
                <table class="table table-hover table-condensed" style="font-size: 12px;">
                    <thead>
                        <tr>
                            <th class="text-center">Warna</th>
                            <th class="text-center">Brand</th>
                            <th class="text-center">Kategori</th>
                            <th class="text-center">Vendor</th>

                            <th class="text-center">Kode</th>
                            <th class="text-center">Varian</th>

                            <th class="text-center">Harga Beli</th>
                            <th class="text-center">Harga Jual</th>

                            <th class="text-center">Stok Awal</th>
                            <th class="text-center">Restok</th>
                            <th class="text-center">Reject</th>
                            <th class="text-center">Bertambah</th>
                            <th class="text-center">Berkurang</th>
                            <th class="text-center">Stok Akhir</th>

                            <th class="text-center">Subtotal Harga Beli</th>
                            <th class="text-center">Subtotal Harga Jual</th>
                            <th class="text-center">Subtotal Keuntungan</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="success" style="font-weight: bold;">
                            <td colspan="13"></td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalStokAkhir" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalHargaBeli" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalHargaJual" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalKeuntungan" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <asp:Repeater ID="RepeaterLaporan" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Warna") %></td>
                                    <td><%# Eval("Brand") %></td>
                                    <td><%# Eval("Kategori") %></td>
                                    <td><%# Eval("Vendor") %></td>

                                    <td><%# Eval("Kode") %></td>
                                    <td><%# Eval("Varian") %></td>

                                    <td style="text-align: right"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                    <td style="text-align: right"><%# Eval("HargaJual").ToFormatHarga() %></td>

                                    <td style="text-align: right"><%# Eval("StokAwal") %></td>
                                    <td style="text-align: right"><%# Eval("Restok") %></td>
                                    <td style="text-align: right"><%# Eval("Reject") %></td>
                                    <td style="text-align: right"><%# Eval("Bertambah") %></td>
                                    <td style="text-align: right"><%# Eval("Berkurang") %></td>
                                    <td style="text-align: right"><%# Eval("StokAkhir") %></td>

                                    <td style="text-align: right"><%# (decimal.Parse(Eval("HargaBeli").ToString()) * decimal.Parse(Eval("StokAkhir").ToString())).ToFormatHarga() %></td>
                                    <td style="text-align: right"><%# (decimal.Parse(Eval("HargaJual").ToString()) * decimal.Parse(Eval("StokAkhir").ToString())).ToFormatHarga() %></td>
                                    <td style="text-align: right"><%# ((decimal.Parse(Eval("HargaJual").ToString()) - decimal.Parse(Eval("HargaBeli").ToString())) * decimal.Parse(Eval("StokAkhir").ToString())).ToFormatHarga() %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                        <tr class="success" style="font-weight: bold;">
                            <td colspan="13"></td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalStokAkhir1" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalHargaBeli1" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalHargaJual1" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right; padding-top: 0; padding-bottom: 0; font-size: 18px;">
                                <asp:Label ID="LabelTotalKeuntungan1" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <%--            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

