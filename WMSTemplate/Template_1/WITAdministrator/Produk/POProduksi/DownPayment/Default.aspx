<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_DownPayment_Default" %>

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

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Down Payment Purchase Order Product
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelData" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No</th>
                                        <th>ID</th>
                                        <th colspan="2">Tanggal</th>
                                        <th>Pegawai</th>
                                        <th>Vendor</th>
                                        <th>Grandtotal</th>
                                        <th class="fitSize">Down Payment</th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxCariIDPOProduksiProduk" runat="server" CssClass="form-control input-sm text-uppercase" onkeypress="return Func_ButtonCariPO(event)"></asp:TextBox></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariBulanPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariTahunPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                            </asp:DropDownList>
                                        </th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariPegawaiPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                            </asp:DropDownList></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariVendorPO" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Event_CariPO">
                                            </asp:DropDownList></th>
                                        <th></th>
                                        <th class="fitSize">
                                            <asp:Button ID="ButtonCariPO" runat="server" Text="Cari" class="btn btn-outline-light d-none" ClientIDMode="Static" OnClick="Event_CariPO" />
                                            <asp:Button ID="ButtonTambah" runat="server" Text="Tambah" class="btn btn-success btn-block" OnClick="ButtonTambah_Click" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterDataPO" runat="server" OnItemCommand="RepeaterDataPO_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td><a href='<%# "Detail.aspx?id=" + Eval("IDPOProduksiProduk") %>'><%# Eval("IDPOProduksiProduk") %></a></td>
                                                <td colspan="2"><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                                <td><%# Eval("Pegawai") %></td>
                                                <td><%# Eval("Vendor") %></td>
                                                <td class="text-right"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                <td class="text-right table-warning"><strong><%# Eval("DownPayment").ToFormatHarga() %></strong></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressData" runat="server" AssociatedUpdatePanelID="UpdatePanelData">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressData" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

