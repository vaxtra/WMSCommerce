<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_Transfer_Kirim_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Bahan Baku
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
    Transfer Bahan Baku
                        <asp:Label ID="LabelIDTransferBahanBaku" runat="server"></asp:Label>
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
                                <asp:Button ID="ButtonDataStokBahanBaku" runat="server" Text="<< Data Stok Produk" CssClass="btn btn-primary mr-1" OnClick="ButtonDataStokBahanBaku_Click" />
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

            <asp:MultiView ID="MultiViewTransferBahanBaku" runat="server" OnActiveViewChanged="MultiViewTransferBahanBaku_ActiveViewChanged">
                <asp:View ID="ViewStokBahanBaku" runat="server">
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No.</th>
                                        <th>Kode</th>
                                        <th>Bahan Baku</th>
                                        <th>Kategori</th>
                                        <th>Harga</th>
                                        <th>Stok</th>
                                        <th>Satuan</th>
                                        <th>Transfer</th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxCariKode" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxCariBahanBaku" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariKategori" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                            </asp:DropDownList></th>
                                        <th colspan="3">
                                            <asp:DropDownList ID="DropDownListCariSatuan" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="Event_LoadData">
                                            </asp:DropDownList></th>
                                        <th class="fitSize">
                                            <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-block" OnClick="ButtonSimpan_Click" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterStokBahanBaku" runat="server">
                                        <ItemTemplate>
                                            <tr runat="server" id="panelStok">
                                                <td class="fitSize"><asp:Label ID="LabelIDBahanBaku" runat="server" CssClass="d-none" Text='<%# Eval("IDBahanBaku") %>'></asp:Label><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Kode") %></td>
                                                <td><%# Eval("BahanBaku") %></td>
                                                <td><%# Eval("Kategori") %></td>
                                                <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                                <td class="text-right fitSize"><strong>
                                                    <asp:Label ID="Labeljumlah" runat="server" Text='<%# Eval("Jumlah").ToFormatHarga() %>'></asp:Label></strong></td>
                                                <td class="fitSize"><strong><%# Eval("SatuanBesar") %></strong></td>
                                                <td style="width: 200px;">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="TextBoxJumlahTransfer" CssClass="form-control text-right input-sm InputDesimal" runat="server"></asp:TextBox>
                                                        <div class="input-group-prepend">
                                                            <div class="input-group-text" style="width: 80px;"><%# Eval("SatuanBesar") %></div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="ViewTransferBahanBaku" runat="server">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <div class="card h-100">
                                    <div class="card-header bg-gradient-black">
                                        <h5 class="font-weight-light">PENGIRIM</h5>
                                    </div>
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
                                    <div class="card-header bg-gradient-black">
                                        <h5 class="font-weight-light">PENERIMA</h5>
                                    </div>
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
                            <div class="card-header bg-gradient-black">
                                <h5 class="font-weight-light">DETAIL</h5>
                            </div>
                            <div class="table-responsive">
                                <table class="table table-sm table-hover table-bordered mb-0">
                                    <thead>
                                        <tr class="active">
                                            <th>No.</th>
                                            <th>Kode</th>
                                            <th>Bahan Baku</th>
                                            <th>Satuan</th>
                                            <th>Kategori</th>
                                            <th>Harga</th>
                                            <th>Transfer</th>
                                            <th>Subtotal</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterTransferBahanBaku" runat="server" OnItemCommand="RepeaterTransferBahanBaku_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="fitSize"><%# Eval("Kode") %></td>
                                                    <td><%# Eval("Nama") %></td>
                                                    <td class="fitSize"><%# Eval("SatuanBesar") %></td>
                                                    <td><%# Eval("Kategori") %></td>
                                                    <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize"><%# Eval("Subtotal").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize">
                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="X" CommandName="Hapus" CommandArgument='<%# Eval("IDTransferBahanBakuDetail") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr class="table-success">
                                            <td class="text-center" colspan="7"><b>TOTAL</b></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalSubtotal" runat="server" Font-Bold="true"></asp:Label></td>
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

