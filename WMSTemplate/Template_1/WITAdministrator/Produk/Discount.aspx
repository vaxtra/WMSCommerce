<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Discount.aspx.cs" Inherits="WITAdministrator_Produk_NewDiscount" %>

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

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Discount
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTitleRight" runat="server">
        <ContentTemplate>
            <asp:Button ID="ButtonPrint" runat="server" Text="Cetak" CssClass="btn btn-secondary btn-const" />

            <asp:UpdateProgress ID="updateProgressTitleRight" runat="server" AssociatedUpdatePanelID="UpdatePanelTitleRight">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressTitleLeft" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelDiscount" runat="server">
        <ContentTemplate>
            <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                <b>PERINGATAN :</b>
                <br />
                <asp:Literal ID="LiteralPeringatan" runat="server"></asp:Literal>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListCariTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <asp:DropDownList ID="DropDownListCariStatusDiskon" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                            <asp:ListItem Text="-Status Diskon-" Value="Semua" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Diskon" Value="Diskon"></asp:ListItem>
                            <asp:ListItem Text="Tidak Diskon" Value="TidakDiskon"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Button CssClass="btn btn-primary btn-block d-none" ID="ButtonCari" runat="server" Text="Cari" Width="100%" ClientIDMode="Static" OnClick="ButtonCari_Click" />
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th rowspan="2">#</th>
                                <th rowspan="2">No.</th>
                                <th rowspan="2">Kode</th>
                                <th rowspan="2">Pemilik</th>
                                <th rowspan="2">Produk</th>
                                <th rowspan="2">Varian</th>
                                <th rowspan="2">Kategori</th>
                                <th rowspan="2">Harga</th>
                                <th rowspan="2">Stok</th>
                                <th colspan="2">Disc. Store</th>
                                <th colspan="2">Disc. Consignment</th>
                                <th>After Disc.</th>
                            </tr>
                            <tr class="thead-light">
                                <th class="text-center warning">Persentase</th>
                                <th class="text-center info">Nominal</th>
                                <th class="text-center warning">Persentase</th>
                                <th class="text-center info">Nominal</th>
                                <th class="success"></th>
                            </tr>
                            <tr class="thead-light" style="font-weight: bold; padding-top: 0; padding-bottom: 0;">
                                <th colspan="2">
                                    <asp:CheckBox ID="CheckBoxSemua" runat="server" CssClass="float-left" AutoPostBack="true" OnCheckedChanged="CheckBoxSemua_CheckedChanged" /></th>
                                <th>
                                    <asp:TextBox ID="TextBoxCariKode" onfocus="this.select();" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariPemilikProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariAtributProduk" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariKategori" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th></th>
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxStorePersentase" onfocus="this.select();" runat="server" CssClass="form-control text-right input-sm InputDesimal"></asp:TextBox></th>
                                <th>
                                    <asp:TextBox ID="TextBoxStoreNominal" onfocus="this.select();" runat="server" CssClass="form-control text-right input-sm InputDesimal"></asp:TextBox></th>
                                <th>
                                    <asp:TextBox ID="TextBoxConsignmentPersentase" onfocus="this.select();" runat="server" CssClass="form-control text-right input-sm InputDesimal"></asp:TextBox></th>
                                <th>
                                    <asp:TextBox ID="TextBoxConsignmentNominal" onfocus="this.select();" runat="server" CssClass="form-control text-right input-sm InputDesimal"></asp:TextBox></th>
                                <th>
                                    <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-block" runat="server" Text="Simpan" Width="100%" OnClick="ButtonSimpan_Click" />
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterStokProduk" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <th class="fitSize" style="margin-top: 0px;">
                                            <asp:Label ID="LabelIDStokProduk" runat="server" Text='<%# Eval("IDStokProduk") %>' Visible="false"></asp:Label>
                                            <asp:CheckBox ID="CheckBoxPilih" runat="server" /></th>
                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize"><a href='/WITAdministrator/Produk/Barcode.aspx?id=<%# Eval("IDKombinasiProduk") %>' target="_blank"><%# Eval("KodeKombinasiProduk") %></a></td>
                                        <td><%# Eval("Brand") %></td>
                                        <td><%# Eval("Nama") %></td>
                                        <td><%# Eval("Varian") %></td>
                                        <td><%# Eval("Kategori") %></td>
                                        <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                        <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %> </td>
                                        <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right table-warning" : "text-right table-danger" %>'><%# Eval("DiscountStorePersentase").ToFormatHarga() %>% </td>
                                        <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right table-info" : "text-right table-danger" %>'><%# Eval("DiscountStoreNominal").ToFormatHarga() %> </td>
                                        <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right table-warning" : "text-right table-danger" %>'><%# Eval("DiscountKonsinyasiPersentase").ToFormatHarga() %>% </td>
                                        <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right table-info" : "text-right table-danger" %>'><%# Eval("DiscountKonsinyasiNominal").ToFormatHarga() %> </td>
                                        <td class='<%# Eval("HargaJual").ToFormatHarga() == Eval("SetelahDiskon").ToFormatHarga() ? "text-right table-success" : "text-right table-danger" %>'><strong><%# Eval("SetelahDiskon").ToFormatHarga() %></strong></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressDiscount" runat="server" AssociatedUpdatePanelID="UpdatePanelDiscount">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressDiscount" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

