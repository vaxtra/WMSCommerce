<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pajak.aspx.cs" Inherits="WITAdministrator_Produk_Discount_Event_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pajak
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
    <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Style="width: 300px; text-align:left;" runat="server" OnSelectedIndexChanged="DropDownListTempat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="form-inline">
                <div class="form-group">
                    <asp:TextBox ID="TextBoxPajak" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="ButtonSimpan" Style="font-weight: bold;" class="btn btn-sm btn-primary hidden-print" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
            </div>
            <br />

            <div class="table-responsive">
                <table class="table table-bordered table-condensed table-hover TableSorter">
                    <thead>
                        <tr class="active">
                            <th class="text-center">
                                <asp:CheckBox ID="CheckBoxPilihSemua" runat="server" />
                            </th>
                            <th class="text-center">No.</th>
                            <th class="text-center">Kode</th>
                            <th class="text-center">Produk</th>
                            <th class="text-center">Warna</th>
                            <th class="text-center">Varian</th>
                            <th class="text-center">Kategori</th>
                            <th class="text-center">Pemilik</th>
                            <th class="text-center">Harga</th>
                            <th class="text-center">Stok</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterStokProduk" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CheckBoxPilih" runat="server" />
                                    </td>
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
                                    <td><%# Eval("Tempat") %></td>
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

