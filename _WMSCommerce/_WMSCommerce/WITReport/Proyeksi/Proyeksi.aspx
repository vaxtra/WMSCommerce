<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Proyeksi.aspx.cs" Inherits="WITReport_Proyeksi_Proyeksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCariProyeksi(e) {
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Proyeksi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrintProyeksi" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
    <asp:Button ID="ButtonExcelProyeksi" runat="server" Text="Excel" CssClass="btn btn-default btn-sm" OnClick="ButtonExcelProyeksi_Click" />
    <a id="LinkDownloadProyeksi" runat="server" visible="false">Download File</a>
    <a href="ProyeksiDetail.aspx" class="btn btn-sm btn-info">Detail</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelProyeksi" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <asp:DropDownList ID="DropDownListCariTempatProyeksi" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventProyeksi"></asp:DropDownList>
            </div>
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
                                <th>Pegawai</th>
                                <th class="fitSize">Tanggal Proyeksi</th>
                                <th class="fitSize">Tanggal Target</th>
                                <th class="fitSize">Tanggal Selesai</th>
                                <th>Total</th>
                                <th>Status</th>
                                <th>Keterangan</th>
                            </tr>
                            <tr class="success" style="font-weight: bold;">
                                <td></td>
                                <td>
                                    <asp:TextBox ID="TextBoxCariIDProyeksi" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariProyeksi(event)" Style="width: 100%;"></asp:TextBox></td>
                                <td>
                                    <asp:DropDownList ID="DropDownListCariPenggunaProyeksi" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventProyeksi"></asp:DropDownList></td>
                                <td colspan="3"></td>
                                <td class="text-right" style="vertical-align: middle;">
                                    <asp:Label ID="LabelTotalProdukHeaderProyeksi" runat="server" Text="0"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="DropDownListCariStatusProyeksi" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_EventProyeksi">
                                        <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Proses" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Selesai" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Batal" Value="3"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="TextBoxCariKeteranganProyeksi" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariProyeksi(event)" Style="width: 100%;"></asp:TextBox></td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterProyeksi" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize"><%# Eval("IDProyeksi") %></td>
                                        <td class="fitSize"><%# Eval("Pengguna") %></td>
                                        <td class="fitSize"><%# Eval("TanggalProyeksi") %></td>
                                        <td class="fitSize"><%# Eval("TanggalTarget") %></td>
                                        <td class="fitSize"><%# Eval("TanggalSelesai") %></td>
                                        <td class="text-right warning fitSize"><strong><%# Eval("TotalJumlah") %></strong></td>
                                        <td class="text-center fitSize"><%# Pengaturan.StatusProyeksi(Eval("EnumStatusProyeksi").ToString()) %></td>
                                        <td><%# Eval("Keterangan") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr class="success" style="font-weight: bold;">
                                <td colspan="6"></td>
                                <td class="text-right" style="vertical-align: middle;">
                                    <asp:Label ID="LabelTotalProdukFooterProyeksi" runat="server" Text="0"></asp:Label></td>
                                <td colspan="2"></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgressProyeksi" runat="server" AssociatedUpdatePanelID="UpdatePanelProyeksi">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressProyeksi" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

