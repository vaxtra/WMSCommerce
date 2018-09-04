<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_ECommerce_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) {
                newwindow.focus()
            }
            return false;
        }

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
    E-Commerce
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonCetakInvoice" runat="server" Text="Cetak Invoice" CssClass="btn btn-secondary mr-1" Visible="false" />
    <asp:Button ID="ButtonCetakPackingSlip" runat="server" Text="Cetak Packing Slip" CssClass="btn btn-secondary" Visible="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:MultiView ID="MultiViewTransaksi" runat="server">
        <asp:View ID="ViewTransaksi" runat="server">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group">
                                <asp:CheckBoxList ID="CheckBoxListStatusTransaksi" runat="server" CssClass="checkboxlist">
                                    <asp:ListItem Text="Awaiting Payment" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Complete" Value="5" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Canceled" Value="6" Selected="True"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="form-group">
                                <label class="text-muted font-weight-bold">Dari</label>
                                <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label class="text-muted font-weight-bold">Sampai</label>
                                <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button CssClass="btn btn-primary btn-block" ID="ButtonCariTanggal" runat="server" Text="Cari" ClientIDMode="Static" OnClick="ButtonCariTanggal_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-8 col-lg-9">
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th>ID</th>
                                        <th>Tanggal</th>
                                        <th>Pelanggan</th>
                                        <th>Status Pembayaran</th>
                                        <th>Pengiriman</th>
                                        <th>Grandtotal</th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th class="text-center">
                                            <asp:CheckBox ID="CheckBoxPilihSemua" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPilihSemua_CheckedChanged" /></th>
                                        <th colspan="10">
                                            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block d-none" OnClick="LoadData_Event" ClientIDMode="Static" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterTransaksi" runat="server" OnItemCommand="RepeaterTransaksi_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:CheckBox ID="CheckBoxPilih" runat="server" />
                                                    <asp:Label ID="LabelIDTransaksi" runat="server" Text='<%# Eval("IDTransaksi") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="ButtonDetail" CssClass="btn btn-link btn-xs" runat="server" Text='<%# Eval("IDTransaksi") %>' CommandName="Detail" CommandArgument='<%# Eval("IDTransaksi") %>' /></td>
                                                <td><%# Eval("TanggalOperasional") %></td>
                                                <td><%# Eval("Pelanggan") %></td>
                                                <td class="text-center"><%# Eval("StatusTransaksi") %></td>
                                                <td class="text-center"><%# Eval("StatusPengiriman") %></td>
                                                <td class='<%# Eval("ClassGrandtotal") %>'><%# Eval("GrandTotal") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div class="card">
                            <div class="card-footer">
                                <a id="ButtonSemuaStatus" runat="server" class="btn btn-success btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Proses</a>
                                <div class="dropdown-menu p-2">
                                    <asp:Button CssClass="btn btn-light btn-block" ID="ButtonModalSemuaComplete" runat="server" data-toggle="modal" data-target="#exampleModalComplete" Text="Complete" OnClientClick="return false;" />
                                    <asp:Button CssClass="btn btn-light btn-block" ID="ButtonSemuaCanceled" runat="server" Text="Canceled" CommandName="Canceled" OnClick="ButtonSemuaCanceled_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="ViewDetail" runat="server">
            <div class="card">
                <h4 class="card-header bg-smoke">ID #<asp:Label ID="LabelIDTransaksi" runat="server"></asp:Label><asp:Label ID="LabelStatusTransaksi" runat="server" CssClass="float-right" Font-Size="X-Large"></asp:Label></h4>
                <div class="card-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-12 col-md-12 col-lg-6 col-xl-6">
                                <h3 class="border-bottom">PELANGGAN</h3>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Nama</label>
                                    <br />
                                    <asp:Label ID="LabelPelangganNama" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Telepon</label>
                                    <br />
                                    <asp:Label ID="LabelPelangganTelepon" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Alamat</label>
                                    <br />
                                    <asp:Label ID="LabelPelangganAlamat" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 col-lg-6 col-xl-6">
                                <h3 class="border-bottom">STATUS</h3>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Tempat</label>
                                    <br />
                                    <asp:Label ID="LabelTempat" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Tanggal Transaksi</label>
                                    <br />
                                    <asp:Label ID="LabelTanggalOperasional" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Pegawai</label>
                                    <br />
                                    <asp:Label ID="LabelPenggunaTransaksi" runat="server"></asp:Label>
                                    (<asp:Label ID="LabelTanggalTransaksi" runat="server"></asp:Label>)
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Pegawai Update</label>
                                    <br />
                                    <asp:Label ID="LabelPenggunaUpdate" runat="server"></asp:Label>
                                    (<asp:Label ID="LabelTanggalUpdate" runat="server"></asp:Label>)
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted mb-0">Pegawai Batal</label>
                                    <br />
                                    <asp:Label ID="LabelPenggunaBatal" runat="server"></asp:Label>
                                    (<asp:Label ID="LabelTanggalBatal" runat="server"></asp:Label>)
                                </div>
                            </div>
                        </div>
                    </div>
                    <h3 class="border-bottom">PRODUK</h3>
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No.</th>
                                        <th>Produk</th>
                                        <th class="text-right">Quantity</th>
                                        <th class="text-right">Harga</th>
                                        <th class="text-right">Discount</th>
                                        <th class="text-right">Subtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterDetailTransaksi" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("JumlahProduk").ToString())) %>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("HargaJual").ToString())) %>
                                                <%# Parse.Decimal(Eval("JumlahProduk").ToString()) > 0 ? Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("PotonganHargaJual").ToString()) * -1) : Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("PotonganHargaJual").ToString())) %>
                                                <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("Subtotal").ToString())) %>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="text-right font-weight-bold table-warning">
                                        <td colspan="2"></td>
                                        <td>
                                            <asp:Label ID="LabelTotalQuantity1" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2"></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelSubtotal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="text-right font-weight-bold table-warning">
                                        <td colspan="5">Discount</td>
                                        <td>
                                            <asp:Label ID="LabelDiscount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="text-right font-weight-bold table-warning">
                                        <td colspan="5">Pengiriman</td>
                                        <td>
                                            <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="text-right font-weight-bold table-warning">
                                        <td colspan="5">Pembulatan</td>
                                        <td>
                                            <asp:Label ID="LabelPembulatan" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="text-right font-weight-bold table-success">
                                        <td colspan="5">Grandtotal</td>
                                        <td>
                                            <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <h3 class="border-bottom">PEMBAYARAN</h3>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-12 col-md-12 col-lg-8 col-xl-8">
                                <div class="table-responsive">
                                    <table class="table table-sm table-hover table-bordered">
                                        <thead>
                                            <tr class="thead-light">
                                                <th>No.</th>
                                                <th>Tanggal</th>
                                                <th>Pengguna</th>
                                                <th>Jenis</th>
                                                <th class="text-right">Total</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterPembayaran" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Pengaturan.FormatTanggal(Eval("Tanggal")) %></td>
                                                        <td><%# Eval("TBPengguna.NamaLengkap") %></td>
                                                        <td><%# Eval("TBJenisPembayaran.Nama") %></td>
                                                        <%# Pengaturan.FormatHargaRepeater(Parse.Decimal(Eval("Total").ToString())) %>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tr class="text-right table-success" style="font-weight: bold;">
                                            <td colspan="4"></td>
                                            <td class="text-right">
                                                <asp:Label ID="LabelTotalPembayaran" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 col-lg-4 col-xl-4">
                                <span class="font-weight-bold">Keterangan</span>
                                <br />
                                <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-footer">
                    <a id="ButtonStatus" runat="server" class="btn btn-success btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Proses</a>
                    <div class="dropdown-menu p-2">
                        <asp:Button CssClass="btn btn-light btn-block" ID="ButtonModalComplete" runat="server" data-toggle="modal" data-target="#exampleModalComplete" Text="Complete" OnClientClick="return false;" />
                        <asp:Button CssClass="btn btn-light btn-block" ID="ButtonCanceled" runat="server" Text="Canceled" CommandName="Canceled" OnClick="ButtonCanceled_Click" />
                    </div>
                    <asp:Button ID="ButtonKembali" runat="server" CssClass="btn btn-danger btn-const" Text="Kembali" OnClick="ButtonKembali_Click" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>

    <!-- UPDATE BERAT -->
    <div class="modal fade" id="exampleModalComplete" tabindex="-1" role="dialog" aria-labelledby="exampleModalCompleteTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalUpdateBeratTitle">Jenis Pembayaran</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListJenisPembayaran" runat="server" CssClass="select2" Width="100%">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer justify-content-start">
                    <asp:Button ID="ButtonComplete" runat="server" CssClass="btn btn-primary btn-const" Text="Simpan" OnClick="ButtonComplete_Click" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

