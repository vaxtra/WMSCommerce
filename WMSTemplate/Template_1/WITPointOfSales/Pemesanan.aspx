<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Pemesanan.aspx.cs" Inherits="WITPointOfSales_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-7">
            <div class="table-responsive">
                <table class="table table-condensed table-hover" style="font-size: 12px;">
                    <thead>
                        <tr class="active">
                            <th>No.</th>
                            <th>Produk</th>
                            <th>Jumlah</th>
                            <th>Harga</th>
                            <th>Discount</th>
                            <th>Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterKombinasiProduk" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# (Container.ItemIndex + 1) %>
                                        <asp:HiddenField ID="HiddenFieldIDKombinasiProduk" runat="server" Value='<%# Eval("IDKombinasiProduk") %>' />
                                    </td>
                                    <td><%# Eval("Nama") %></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxJumlahProduk" onfocus="this.select();" CssClass="angka input-sm form-control text-right" Width="80px" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxHarga" ReadOnly="true" CssClass="form-control input-sm text-right" Width="80px" runat="server" Text='<%# Pengaturan.FormatHarga(Eval("HargaJual")) %>'></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxDiscount" onfocus="this.select();" CssClass="form-control input-sm text-right" Width="80px" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSubtotal" ReadOnly="true" CssClass="form-control input-sm text-right" Width="100px" runat="server"></asp:TextBox></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-md-5">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-4 control-label">Tanggal</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBoxTanggal" class="form-control Tanggal" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Pelanggan</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DropDownListPelanggan" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPelanggan_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Grup</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="DropDownListGrupPelanggan" Style="width: 100%;" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGrupPelanggan_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Nama</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBoxNama" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Telepon</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBoxTelepon" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Alamat</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBoxAlamat" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Subtotal</label>
                    <div class="col-sm-8 control-label" style="font-weight: bold;">
                        <asp:Label ID="LabelSubtotal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Discount</label>
                    <div class="col-sm-8 control-label" style="font-weight: bold;">
                        <asp:Label ID="LabelDiscount" runat="server"></asp:Label>

                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Pengiriman</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBoxBiayaPengiriman" CssClass="angka text-right form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Grand Total</label>
                    <div class="col-sm-8 control-label" style="font-weight: bold;">
                        <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label"></label>
                    <div class="col-sm-8">
                        <asp:Button ID="ButtonHitung" OnClick="ButtonHitung_Click" CssClass="btn" runat="server" Text="Hitung" />
                        <asp:Button ID="ButtonProduksi" OnClick="ButtonProduksi_Click" CssClass="btn btn-info" runat="server" Text="Produksi" />
                        <asp:Button ID="ButtonPesan" OnClick="ButtonPesan_Click" CssClass="btn btn-primary" runat="server" Text="Pesan" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

