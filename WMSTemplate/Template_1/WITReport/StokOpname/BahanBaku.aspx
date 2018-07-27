<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="BahanBaku.aspx.cs" Inherits="WITReport_StokOpname_BahanBaku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../plugins/jsPDF/jspdf.min.js"></script>
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

        var doc = new jsPDF();
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };

        $('#cmd').click(function () {
            doc.fromHTML($('#content').html(), 15, 15, {
                'width': 170,
                'elementHandlers': specialElementHandlers
            });
            doc.save('sample-file.pdf');
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Stock Opname Bahan Baku
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
    <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" runat="server" Value="testtt" />
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
            <div class="row" id="printPDF">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th rowspan="2" style="width: 2.5%;">No.</th>
                                        <th rowspan="2" style="width: 5.5%;">Kode</th>
                                        <th rowspan="2" style="width: 9.5%">Bahan Baku</th>
                                        <th rowspan="2" style="width: 9.5%;">Kategori</th>
                                        <th rowspan="2" style="width: 3.5%">Satuan</th>
                                        <th colspan="2">Stok Sebelum SO</th>
                                        <th colspan="2">Stok Setelah SO</th>
                                        <th colspan="2">Qty</th>
                                        <th colspan="2">Nominal</th>
                                    </tr>
                                    <tr class="active">
                                        <th>Qty</th>
                                        <th>Nominal</th>
                                        <th>Qty</th>
                                        <th>Nominal</th>
                                        <th style="font-size: 16px;">+</th>
                                        <th style="font-size: 16px;">-</th>
                                        <th style="font-size: 16px;">+</th>
                                        <th style="font-size: 16px;">-</th>
                                    </tr>
                                    <tr class="active">
                                        <th colspan="2"></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxProduk" runat="server" CssClass=" form-control input-sm"></asp:TextBox>
                                        </th>
                                        <th>
                                            <asp:TextBox ID="TextBoxKategori" runat="server" CssClass=" form-control input-sm"></asp:TextBox>
                                        </th>
                                        <th colspan="5"></th>

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
                                        <td colspan="6"></td>
                                        <th style="font-weight: bold;">
                                            <asp:Label ID="LabelNominalSebelumSO" runat="server"></asp:Label>
                                        </th>
                                        <td colspan="1"></td>
                                        <th style="font-weight: bold;">
                                            <asp:Label ID="LabelNominalSetelahSO" runat="server"></asp:Label>
                                        </th>
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
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kode") %></td>
                                                <td><%# Eval("BahanBaku") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td><%# Eval("Satuan") %></td>
                                                <td class="text-right success"><%# Eval("StokSebelumSO").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("NominalSebelumSO").ToFormatHarga() %></td>
                                                <td class="text-right success"><%# Eval("StokSetelahSO").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("NominalSetelahSO").ToFormatHarga() %></td>
                                                <td class="text-right"><%# Eval("SelisihQtyPositif").ToFormatHarga() %>
                                                </td>
                                                <td class="text-right"><%# Eval("SelisihQtyNegatif").ToFormatHarga() %>
                                                </td>
                                                <td class="text-right"><%# Eval("SelisihNominalPositif").ToFormatHarga() %>
                                                </td>
                                                <td class="text-right"><%# Eval("SelisihNominalNegatif").ToFormatHarga() %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <tr class="warning" style="font-weight: bold;">
                                        <td colspan="5"></td>
                                        <th style="font-weight: bold;">
                                            <asp:Label ID="LabelNominalSebelumSO2" runat="server"></asp:Label>
                                        </th>
                                        <td colspan="1"></td>
                                        <th style="font-weight: bold;">
                                            <asp:Label ID="LabelNominalSetelahSO2" runat="server"></asp:Label>
                                        </th>
                                        <td colspan="1"></td>
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

