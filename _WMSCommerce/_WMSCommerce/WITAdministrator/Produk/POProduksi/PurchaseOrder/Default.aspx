<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_PurchaseOrder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Bahan Baku
        function Func_ButtonCariPO(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariPO');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_ButtonCariPenerimaan(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariPenerimaan');
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
    Purchase Order Product
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-sm">Tambah Purchase Order</a>
    <a href="Penerimaan.aspx" class="btn btn-success btn-sm">Tambah Penerimaan</a>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#tabPurchaseOrder" id="PurchaseOrder-tab" data-toggle="tab">Purchase Order</a></li>
                <li><a href="#tabPenerimaan" id="Penerimaan-tab" data-toggle="tab">Penerimaan</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabPurchaseOrder">
                    <asp:UpdatePanel ID="UpdatePanelDataPO" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered">
                                    <thead>
                                        <tr class="active">
                                            <th>No</th>
                                            <th>ID</th>
                                            <th colspan="2">Tanggal</th>
                                            <th>Pegawai</th>
                                            <th>Vendor</th>
                                            <th>Grandtotal</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCariIDPOProduksiProduk" runat="server" CssClass="form-control input-sm text-uppercase" onkeypress="return Func_ButtonCariPO(event)"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariBulanPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                                    <asp:ListItem Text="Januari" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Febuari" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Maret" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Mei" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Juni" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Juli" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Agustus" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Oktober" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Nopember" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Desember" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariTahunPO" runat="server" CssClass="select2 pull-right" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariPegawaiPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariVendorPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                                </asp:DropDownList></td>
                                            <td></td>
                                            <td class="fitSize">
                                                <asp:Button ID="ButtonCariPO" runat="server" Text="Cari" class="btn btn-primary btn-sm btn-block" OnClick="Event_CariPO" /></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDataPO" runat="server" OnItemCommand="RepeaterDataPO_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="fitSize"><a href='<%# "Detail.aspx?id=" + Eval("IDPOProduksiProduk") %>'><%# Eval("IDPOProduksiProduk") %></a></td>
                                                    <td class="fitSize" colspan="2"><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                    <td><%# Eval("Pegawai") %></td>
                                                    <td><%# Eval("Vendor") %></td>
                                                    <td class="text-right fitSize warning"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                    <td class="text-right fitSize">
                                                        <asp:Button ID="ButtonCetak" runat="server" CssClass="btn btn-default btn-xs" Text="Cetak" OnClientClick='<%# Eval("CetakPO") %>' />
                                                        <asp:Button ID="ButtonHapus" CssClass='<%#  Eval("Hapus") %>' runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPOProduksiProduk") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <asp:UpdateProgress ID="updateProgressDataPO" runat="server" AssociatedUpdatePanelID="UpdatePanelDataPO">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressDataPO" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="tabPenerimaan">
                    <asp:UpdatePanel ID="UpdatePanelDataPenerimaan" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-condensed table-hover table-bordered">
                                    <thead>
                                        <tr class="active">
                                            <th>No</th>
                                            <th>ID</th>
                                            <th colspan="2">Tanggal</th>
                                            <th>Pegawai</th>
                                            <th>Vendor</th>
                                            <th>Grandtotal</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCariIDPenerimaanPOProduksiProduk" runat="server" CssClass="form-control input-sm text-uppercase" onkeypress="return Func_ButtonCariPenerimaan(event)"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariBulanPenerimaan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPenerimaan">
                                                    <asp:ListItem Text="Januari" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Febuari" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Maret" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Mei" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Juni" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Juli" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Agustus" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Oktober" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Nopember" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Desember" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariTahunPenerimaan" runat="server" CssClass="select2 pull-right" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPenerimaan">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariPegawaiPenerimaan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPenerimaan">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariVendorPenerimaan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPenerimaan">
                                                </asp:DropDownList></td>
                                            <td></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariStatusPenerimaan" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPenerimaan">
                                                    <asp:ListItem Text="-Semua-" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Baru" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Proses" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Selesai" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Batal" Value="4"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td class="fitSize">
                                                <asp:Button ID="ButtonCariPenerimaan" runat="server" Text="Cari" class="btn btn-primary btn-sm btn-block" OnClick="Event_CariPenerimaan" /></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterDataPenerimaan" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td><a href='<%# "DetailPenerimaan.aspx?id=" + Eval("IDPenerimaanPOProduksiProduk") %>'><%# Eval("IDPenerimaanPOProduksiProduk") %></a></td>
                                                    <td colspan="2"><%# Eval("TanggalTerima").ToFormatTanggal() %></td>
                                                    <td><%# Eval("Pegawai") %></td>
                                                    <td><%# Eval("Vendor") %></td>
                                                    <td class="text-right warning"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                    <td class="text-center"><%# Eval("Status") %></td>
                                                    <td class="text-right fitSize">
                                                        <asp:Button ID="ButtonCetak" runat="server" CssClass="btn btn-default btn-xs" Text="Cetak" OnClientClick='<%# Eval("CetakPO") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <asp:UpdateProgress ID="updateProgressDataPenerimaan" runat="server" AssociatedUpdatePanelID="UpdatePanelDataPenerimaan">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                        <asp:Image ID="imgUpdateProgressDataPenerimaan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

