<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_Discount_Event_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pengaturan Discount Event
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
    <div class="form-horizontal">
        <div class="form-group form-group-sm">
            <label class="col-sm-3 control-label">Tempat</label>
            <div class="col-sm-9">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" runat="server" Style="width: 100%"></asp:DropDownList>
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-3 control-label">Nama</label>
            <div class="col-sm-9">
                <asp:TextBox ID="TextBoxNama" runat="server" CssClass="form-control" required></asp:TextBox>
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-3 control-label">Tanggal Awal</label>
            <div class="col-sm-9">
                <asp:TextBox ID="TextBoxTanggalAwal" runat="server" CssClass="form-control Tanggal" required></asp:TextBox>
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-3 control-label">Tanggal Akhir</label>
            <div class="col-sm-9">
                <asp:TextBox ID="TextBoxTanggalAkhir" runat="server" CssClass="form-control Tanggal" required></asp:TextBox>
            </div>
        </div>

        <div class="form-group form-group-sm">
            <label class="col-sm-3 control-label">Enum Discount Event</label>
            <div class="col-sm-9">
                <asp:DropDownList ID="DropDownListEnumStatusDiscountEvent" CssClass="select2" runat="server" Style="width: 100%"></asp:DropDownList>
            </div>
        </div>

        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:Button ID="ButtonOk" runat="server" CssClass="btn btn-sm btn-primary" Text="Ok" OnClick="ButtonOk_Click" />
                <asp:Button ID="ButtonKeluar" runat="server" CssClass="btn btn-sm btn-danger" Text="Keluar" OnClick="ButtonKeluar_Click" formnovalidate />
            </div>
        </div>
    </div>

    <div class="row" runat="server" id="PanelDiscount" visible="false">
        <div class="col-md-12">
            <div class="table-responsive">
                <table class="table table-bordered table-condensed table-hover TableSorter">
                    <thead>
                        <tr class="active">
                            <th class="text-center" rowspan="2">No.</th>
                            <th class="text-center" rowspan="2">Kode</th>
                            <th class="text-center" rowspan="2">Produk</th>
                            <th class="text-center" rowspan="2">Warna</th>
                            <th class="text-center" rowspan="2">Varian</th>
                            <th class="text-center" rowspan="2">Kategori</th>
                            <th class="text-center" rowspan="2">Pemilik</th>
                            <th class="text-center" rowspan="2">Harga</th>
                            <th class="text-center" rowspan="2">Stok</th>
                            <th class="text-center" colspan="2">Disc. Store</th>
                            <th class="text-center" colspan="2">Disc. Consignment</th>
                        </tr>
                        <tr class="active">
                            <th class="text-center">Persentase</th>
                            <th class="text-center">Nominal</th>
                            <th class="text-center">Persentase</th>
                            <th class="text-center">Nominal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterStokProduk" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %></td>
                                    <td class="hidden">
                                        <asp:Label ID="LabelIDStokProduk" runat="server" Text='<%# Eval("IDStokProduk") %>'></asp:Label>
                                    </td>
                                    <td><%# Eval("KodeKombinasiProduk") %></td>
                                    <td><%# Eval("Nama") %></td>
                                    <td><%# Eval("Warna") %></td>
                                    <td><%# Eval("Varian") %></td>
                                    <td><%# Eval("Kategori") %></td>
                                    <td><%# Eval("Brand") %></td>
                                    <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %> </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountStorePersentase" Text='<%# Eval("DiscountStorePersentase") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountStoreNominal" Text='<%# Eval("DiscountStoreNominal") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountConsignmentPersentase" Text='<%# Eval("DiscountKonsinyasiPersentase") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountConsignmentNominal" Text='<%# Eval("DiscountKonsinyasiNominal") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

