<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Persediaan.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
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
    Persediaan Stok Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-default btn-sm" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelPersediaan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-4 col-md-4">
                    <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:DropDownList ID="DropDownListJenisStok" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary btn-sm" Style="font-weight: bold;" ClientIDMode="Static" OnClick="LoadData_Event" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-condensed table-bordered table-hover" style="font-size: 12px;">
                            <thead>
                                <tr class="active">
                                    <th rowspan="2">No.</th>
                                    <th rowspan="2">Produk</th>
                                    <th rowspan="2">Warna</th>
                                    <th rowspan="2">Brand</th>
                                    <th rowspan="2">Kategori</th>
                                    <th rowspan="2">Kode</th>
                                    <th rowspan="2">Varian</th>
                                    <th rowspan="2">COGS</th>
                                    <th rowspan="2">Price</th>
                                    <th rowspan="2">Quantity</th>
                                    <th colspan="2">Total</th>
                                </tr>
                                <tr class="active">
                                    <th>COGS</th>
                                    <th>Price</th>
                                </tr>
                                <tr>
                                    <th></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxProduk" Style="width: 100%;" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:DropDownList ID="DropDownListWarna" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                        </asp:DropDownList></th>
                                    <th>
                                        <asp:DropDownList ID="DropDownListBrand" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                        </asp:DropDownList></th>
                                    <th>
                                        <asp:DropDownList ID="DropDownListKategori" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                        </asp:DropDownList></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxKodeProduk" Style="width: 100%;" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:DropDownList ID="DropDownListVarian" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                        </asp:DropDownList></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxCOGS" Style="width: 100%;" CssClass="form-control text-right input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxHarga" Style="width: 100%;" CssClass="form-control text-right input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxQuantity" Style="width: 100%;" CssClass="form-control text-right input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxTotalCOGS" Style="width: 100%;" CssClass="form-control text-right input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxTotalHarga" Style="width: 100%;" CssClass="form-control text-right input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="font-weight: bold;" class="success">
                                    <td colspan="9"></td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHargaBulat(Result["TotalQuantity"]) : "0" %>
                                    </td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHarga(Result["TotalHargaBeli"]) : "0" %>
                                    </td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHarga(Result["TotalHargaJual"]) : "0" %>
                                    </td>
                                </tr>

                                <asp:Repeater ID="RepeaterLaporan" runat="server">
                                    <ItemTemplate>
                                        <tr style="border-top: 3px double #aaa !important;">
                                            <td class="fitSize" rowspan='<%# Eval("JumlahStok") %>'>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td rowspan='<%# Eval("JumlahStok") %>'><%# Eval("Produk") %></td>
                                            <td rowspan='<%# Eval("JumlahStok") %>' class="fitSize"><%# Eval("Warna") %></td>
                                            <td rowspan='<%# Eval("JumlahStok") %>' class="fitSize"><%# Eval("Brand") %></td>
                                            <td rowspan='<%# Eval("JumlahStok") %>'><%# Eval("Kategori") %></td>

                                            <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" DataSource='<%# Eval("Stok") %>'>
                                                <ItemTemplate>
                                                    <td class="fitSize">
                                                        <%# Eval("Kode") %>
                                                        <asp:Label ID="LabelIDKombinasiProduk" Visible="false" runat="server" Text='<%# Eval("IDKombinasiProduk") %>'></asp:Label>
                                                    </td>
                                                    <td class="fitSize"><%# Eval("Varian") %></td>
                                                    <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                    <td class="text-right fitSize">
                                                        <%# Eval("Jumlah").ToFormatHargaBulat() %>
                                                    </td>
                                                    <td class="text-right fitSize">
                                                        <%# Eval("TotalHargaBeli").ToFormatHarga() %>
                                                    </td>
                                                    <td class="text-right fitSize">
                                                        <%# Eval("TotalHargaJual").ToFormatHarga() %>
                                                    </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr style="font-weight: bold;" class="success">
                                    <td colspan="9"></td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHargaBulat(Result["TotalQuantity"]) : "0" %>
                                    </td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHarga(Result["TotalHargaBeli"]) : "0" %>
                                    </td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHarga(Result["TotalHargaJual"]) : "0" %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelPersediaan">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressPersediaan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

