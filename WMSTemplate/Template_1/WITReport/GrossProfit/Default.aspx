<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

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
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Gross Profit
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
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
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
                    <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
                        <thead>
                            <tr class="active">
                                <th class="fitSize">No.</th>
                                <th>Lokasi</th>
                                <th>Quantity</th>
                                <th>Sales Before Disc.</th>
                                <th>Disc.</th>
                                <th>Sales After Disc.</th>
                                <th>COGS</th>
                                <th>Gross Profit</th>
                                <th class="fitSize">%</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr style="font-weight: bold;" class="success">
                                <td colspan="2"></td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHargaBulat(Result["Quantity"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["BeforeDiscount"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["AfterDiscount"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["COGS"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["GrossProfit"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["Persentase"]) : "0" %>
                                </td>
                            </tr>

                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("Key.Nama") %></td>
                                        <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                        <td class="text-right"><%# Eval("BeforeDiscount").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Discount").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("AfterDiscount").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("COGS").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("GrossProfit").ToFormatHarga() %></td>
                                        <td class="text-right fitSize warning"><%# ((decimal)Eval("AfterDiscount") > 0 ? ((decimal)Eval("GrossProfit") / (decimal)Eval("AfterDiscount") * 100) : 0).ToFormatHarga() %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                            <tr style="font-weight: bold;" class="success">
                                <td colspan="2"></td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHargaBulat(Result["Quantity"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["BeforeDiscount"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["Discount"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["AfterDiscount"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["COGS"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["GrossProfit"]) : "0" %>
                                </td>
                                <td class="text-right">
                                    <%= Result != null ? Parse.ToFormatHarga(Result["Persentase"]) : "0" %>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

