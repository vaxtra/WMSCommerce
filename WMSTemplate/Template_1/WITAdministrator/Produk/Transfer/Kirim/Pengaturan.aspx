<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_Transfer_Kirim_Pengaturan" %>

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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Transfer Produk
                        <asp:Label ID="LabelIDTransferProduk" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonKeluar" runat="server" Text="Keluar" OnClick="ButtonKeluar_Click" CssClass="btn btn-danger btn-const" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTransfer" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListJenisStok" CssClass="select2 mr-1" runat="server" OnSelectedIndexChanged="Event_LoadData" AutoPostBack="true"></asp:DropDownList>
                                <asp:Button ID="ButtonTransferSemua" runat="server" Text="Transfer Semua" CssClass="btn btn-primary mr-1" OnClick="ButtonTransferSemua_Click" />
                                <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary d-none mr-1" ClientIDMode="Static" OnClick="Event_LoadData" />
                                <asp:Button ID="ButtonDataStokProduk" runat="server" Text="<< Data Stok Produk" CssClass="btn btn-primary mr-1" OnClick="ButtonDataStokProduk_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="form-inline float-right">
                            <div class="form-group">
                                <asp:Button ID="ButtonDataTransfer" runat="server" Text="Data Transfer >>" CssClass="btn btn-primary mr-1" OnClick="ButtonDataTransfer_Click" />
                                <asp:Button ID="ButtonTransfer" runat="server" Text="Transfer >>" CssClass="btn btn-success" OnClick="ButtonTransfer_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>

            <asp:MultiView ID="MultiViewTransferProduk" runat="server" OnActiveViewChanged="MultiViewTransferProduk_ActiveViewChanged">
                <asp:View ID="ViewStokProduk" runat="server">
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No.</th>
                                        <th>Kode</th>
                                        <th>Produk</th>
                                        <th>Varian</th>
                                        <th>Kategori</th>
                                        <th>Harga</th>
                                        <th>Stok</th>
                                        <th>Transfer</th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxCariKode" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxCariProduk" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariAtributProduk" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                            </asp:DropDownList></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariKategori" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                            </asp:DropDownList></th>
                                        <th></th>
                                        <th class="text-right">
                                            <asp:Label ID="LabelTotalJumlahStok" Text="0" runat="server"></asp:Label></th>
                                        <th class="fitSize">
                                            <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-block" OnClick="ButtonSimpan_Click" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterStokKombinasiProduk" runat="server">
                                        <ItemTemplate>
                                            <tr runat="server" id="panelStok">
                                                <td class="fitSize">
                                                    <asp:Label ID="LabelIDKombinasiProduk" runat="server" CssClass="d-none" Text='<%# Eval("IDKombinasiProduk") %>'></asp:Label><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kode") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td><%# Eval("Atribut") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td class="text-right fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                <td class="text-right fitSize"><strong>
                                                    <asp:Label ID="Labeljumlah" runat="server" Text='<%# Eval("Jumlah").ToFormatHargaBulat() %>'></asp:Label></strong></td>
                                                <td class="fitSize table-warning" style="width: 100px;">
                                                    <asp:TextBox ID="TextBoxJumlahTransfer" CssClass="form-control form-control-sm text-right InputInteger" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="ViewTransferProduk" runat="server">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <div class="card h-100">
                                        <h4 class="card-header bg-smoke">Pengirim</h4>
                                    <div class="card-body">
                                        <div class="form-group">
                                            <label class="form-label bold text-muted">Tempat</label>
                                            <asp:DropDownList ID="DropDownListTempatPengirim" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label bold text-muted">Tanggal</label>
                                            <asp:TextBox ID="TextBoxTanggalKirim" CssClass="form-control input-sm TanggalJam" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <div class="card">
                                        <h4 class="card-header bg-smoke">Penerima</h4>
                                    <div class="card-body">
                                        <div class="form-group">
                                            <label class="form-label bold text-muted">Tempat</label>
                                            <asp:DropDownList ID="DropDownListTempatPenerima" Style="width: 100%" CssClass="select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label bold text-muted">Keterangan</label>
                                            <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="card">
                                <h5 class="card-header bg-smoke">Detail</h5>
                            <div class="table-responsive">
                                <table class="table table-sm table-hover table-bordered mb-0">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No.</th>
                                            <th>Produk</th>
                                            <th>Kategori</th>
                                            <th>Varian</th>
                                            <th>Kode</th>
                                            <th>Harga</th>
                                            <th>Jumlah</th>
                                            <th>Subtotal</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterTransferKombinasiProduk" runat="server" OnItemCommand="RepeaterTransferKombinasiProduk_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td rowspan='<%# Eval("Count").ToInt() + 1 %>' class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td rowspan='<%# Eval("Count").ToInt() + 1 %>'><%# Eval("Produk") %></td>
                                                    <td rowspan='<%# Eval("Count").ToInt() + 1 %>'><%# Eval("Kategori") %></td>
                                                    <td colspan="4" style="padding: 0px; border-bottom: 0;"></td>
                                                </tr>
                                                <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center"><%# Eval("AtributProduk") %></td>
                                                            <td><%# Eval("Kode") %></td>
                                                            <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                            <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHargaBulat() %></td>
                                                            <td class="text-right"><%# Eval("SubtotalHargaJual").ToFormatHarga() %></td>
                                                            <td class="text-right fitSize">
                                                                <asp:Button ID="ButtonHapus" CssClass="btn btn-outline-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDTransferProdukDetail") %>' /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-success">
                                            <td colspan="6" class="text-center font-weight-bold"><b>TOTAL</b></td>
                                            <td class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotalJumlah" runat="server"></asp:Label></td>
                                            <td class="text-right font-weight-bold">
                                                <asp:Label ID="LabelTotalSubtotalHargaJual" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

            <asp:UpdateProgress ID="updateProgressTransfer" runat="server" AssociatedUpdatePanelID="UpdatePanelTransfer">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressTransfer" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

