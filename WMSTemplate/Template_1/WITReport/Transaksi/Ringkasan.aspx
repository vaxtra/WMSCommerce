<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Ringkasan.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Summary Revenue
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcel_Click" />
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <br />
    <a id="LinkDownload" runat="server" visible="false" class="pull-right">Download File</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="row">
            <div class="col-md-3" style="font-weight: bold;">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-md-3" style="font-weight: bold;">
                <asp:DropDownList ID="DropDownListJenisTransaksi" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-md-2" style="font-weight: bold;">
                <asp:DropDownList ID="DropDownListJenisLaporan" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                    <asp:ListItem Text="Harian" Value="1" />
                    <asp:ListItem Text="Bulanan" Value="2" Selected="True" />
                    <asp:ListItem Text="Tahunan" Value="3" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2" runat="server" id="PanelTahun" style="font-weight: bold;">
                <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCari" runat="server" Text="Cari" OnClick="LoadData_Event" />
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-bordered table-hover" style="font-size: 12px;">
                <thead>
                    <tr class="active">
                        <th rowspan="2" style="width: 2%">No.</th>
                        <th rowspan="2" style="width: 13%"></th>
                        <th rowspan="2" class="text-right" style="width: 5%">Tamu</th>
                        <th rowspan="2" class="text-right" style="width: 5%">Quantity</th>

                        <th colspan="6" class="text-right" style="width: 50%">Transaksi</th>

                        <th rowspan="2" class="text-right" style="width: 12.5%">Transaksi</th>
                        <th rowspan="2" class="text-right" style="width: 12.5%">Nominal</th>
                    </tr>
                    <tr>
                        <th class="text-right warning" style="border-left: 3px double #000;">Pelanggan</th>
                        <th class="text-right danger">Bukan Pelanggan</th>

                        <th class="text-right warning" style="border-left: 3px double #000;">Discount</th>
                        <th class="text-right danger">Tidak Discount</th>

                        <th class="text-right warning" style="border-left: 3px double #000;">Delivery</th>
                        <th class="text-right danger" style="border-right: 3px double #000;">Bukan Delivery</th>
                    </tr>
                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="2"></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTamu" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPelanggan" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPelanggan" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelDiscount" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonDiscount" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPengiriman" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPengiriman" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelTransaksi" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNominal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Index") %></td>
                                <td class="text-right"><%# Eval("Tamu").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right" style="border-left: 3px double #000;"><%# Eval("Pelanggan").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NonPelanggan").ToFormatHarga() %></td>
                                <td class="text-right" style="border-left: 3px double #000;"><%# Eval("Discount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NonDiscount").ToFormatHarga() %></td>
                                <td class="text-right" style="border-left: 3px double #000;"><%# Eval("Pengiriman").ToFormatHarga() %></td>
                                <td class="text-right" style="border-right: 3px double #000;"><%# Eval("NonPengiriman").ToFormatHarga() %></td>
                                <td class="text-right"><strong><%# Eval("Transaksi").ToFormatHargaBulat() %></strong></td>
                                <td class="text-right"><strong><%# Eval("Nominal").ToFormatHarga() %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr class="text-right success" style="font-weight: bold;">
                        <td colspan="2"></td>
                        <td class="text-right">
                            <asp:Label ID="LabelTamu1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelQuantity1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPelanggan1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPelanggan1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelDiscount1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonDiscount1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNonPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelTransaksi1" runat="server"></asp:Label>
                        </td>
                        <td class="text-right">
                            <asp:Label ID="LabelNominal1" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

