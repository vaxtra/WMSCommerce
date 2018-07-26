<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITPointOfSales_Marketing_Default" %>

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
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row page-header" style="margin-top: -20px;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="row">
                        <div class="col-md-4">
                            <h3>
                                <asp:Label ID="LabelIDTransaksi" runat="server" CssClass="label label-info pull-left"></asp:Label></h3>
                        </div>
                        <div class="col-md-4">
                            <h3 class="text-center">Marketing</h3>
                        </div>
                        <div class="col-md-4">
                            <h3>
                                <asp:Button ID="ButtonKeluar" runat="server" Text="Keluar" CssClass="btn btn-danger btn-sm pull-right" OnClick="ButtonKeluar_Click" /></h3>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom: 10px;">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:DropDownList ID="DropDownListJenisStok" CssClass="select2" Style="width: 175px;" runat="server" OnSelectedIndexChanged="Event_LoadData" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary btn-sm" ClientIDMode="Static" Style="margin-top: -5px;" OnClick="Event_LoadData" />
                            <asp:Button ID="ButtonStokProduk" runat="server" Text="<< Stok Produk" CssClass="btn btn-warning btn-sm" Style="margin-left:-5px;" OnClick="ButtonStokProduk_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 text-right">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:Button ID="ButtonTransaksiDetail" runat="server" Text="Detail Transaksi >>" CssClass="btn btn-warning btn-sm" OnClick="ButtonTransaksiDetail_Click" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="ButtonPointOfSales" runat="server" Text="Point Of Sales >>" CssClass="btn btn-success btn-sm" OnClick="ButtonPointOfSales_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>

            <asp:MultiView ID="MultiViewTransaksi" runat="server" OnActiveViewChanged="MultiViewTransaksi_ActiveViewChanged">
                <asp:View ID="ViewStokProduk" runat="server">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="table-responsive">
                                <table class="table table-hover table-condensed table-bordered" style="font-size: 12px;">
                                    <thead>
                                        <tr class="active">
                                            <th class="text-center">No.</th>
                                            <th class="text-center">Kode</th>
                                            <th class="text-center">Produk</th>
                                            <th class="text-center">Varian</th>
                                            <th class="text-center">Kategori</th>
                                            <th class="text-center">Brand</th>
                                            <th class="text-center">HPP</th>
                                            <th class="text-center">Harga</th>
                                            <th class="text-center">Stok</th>
                                            <th class="text-center">Transaksi</th>
                                        </tr>
                                        <tr class="active">
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCariKode" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCariProduk" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariAtributProduk" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariKategori" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCariPemilik" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                                </asp:DropDownList></td>
                                            <td></td>
                                            <td></td>
                                            <td class="text-center text-middle">
                                                <h4>
                                                    <asp:Label ID="LabelTotalJumlahStok" Text="0" runat="server"></asp:Label></h4>
                                            </td>
                                            <td class="fitSize">
                                                <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-sm btn-block" OnClick="ButtonSimpan_Click" /></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterStokKombinasiProduk" runat="server">
                                            <ItemTemplate>
                                                <tr runat="server" id="PanelStok">
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="hidden">
                                                        <asp:Label ID="LabelIDKombinasiProduk" runat="server" Text='<%# Eval("IDKombinasiProduk") %>'></asp:Label>
                                                    </td>
                                                    <td class="fitSize"><%# Eval("Kode") %></td>
                                                    <td><%# Eval("Produk") %></td>
                                                    <td class="fitSize text-center"><%# Eval("AtributProduk") %></td>
                                                    <td><%# Eval("Kategori") %></td>
                                                    <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                    <td class="text-right info"><strong><%# Pengaturan.FormatHarga(Eval("HargaBeli")) %></strong></td>
                                                    <td class="text-right success"><strong><%# Pengaturan.FormatHarga(Eval("HargaJual")) %></strong></td>
                                                    <td class="text-right warning"><strong>
                                                        <asp:Label ID="LabelJumlah" runat="server"><%# Pengaturan.FormatHarga(Eval("Jumlah")) %></asp:Label></strong></td>
                                                    <td class="text-right">
                                                        <asp:TextBox ID="TextBoxJumlahTransaksi" Style="width: 100px;" CssClass="text-right angka" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="ViewTransaksiProduk" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Detail</h3>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover table-condensed" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th class="fitSize">No.</th>
                                        <th class="text-center">Kode</th>
                                        <th class="text-center">Produk</th>
                                        <th class="text-center">Varian</th>
                                        <th class="text-center">Kategori</th>
                                        <th class="text-center">Pemilik</th>
                                        <th class="text-right">Harga</th>
                                        <th class="text-right">Quantity</th>
                                        <th class="fitSize">Subtotal</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterTransaksiKombinasiProduk" runat="server" OnItemCommand="RepeaterTransaksiKombinasiProduk_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td class="fitSize"><%# Eval("Barcode") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="fitSize"><%# Eval("AtributProduk") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                <td class="text-right"><%# Pengaturan.FormatHarga(Eval("HargaJual")) %></td>
                                                <td class="text-right"><%# Pengaturan.FormatHarga(Eval("JumlahProduk")) %></td>
                                                <td class="text-right warning"><%# Pengaturan.FormatHarga(Eval("Subtotal")) %></td>
                                                <td class="fitSize">
                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDDetailTransaksi") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="success">
                                        <td class="text-center" colspan="7"><b>TOTAL</b></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalJumlah" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalSubtotalHargaJual" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

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
