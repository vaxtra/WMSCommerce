<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Stok_Default" %>

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
    Stok Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTitleRight" runat="server">
        <ContentTemplate>
            <div class="form-inline">
                <div class="form-group">
                    <asp:Button ID="ButtonExcel" runat="server" Text="Export" CssClass="btn btn-dark btn-const mr-1" OnClick="ButtonExcel_Click" />
                    <h5><a id="LinkDownload" runat="server" class="mr-1" visible="false">Download File</a></h5>
                    <asp:Button ID="ButtonPrint" runat="server" Text="Cetak" CssClass="btn btn-dark btn-const" />
                </div>
            </div>

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
    <asp:UpdatePanel ID="UpdatePanelStokProduk" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListTempat" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                <asp:DropDownList ID="DropDownListJenisStok" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary d-none" ClientIDMode="Static" OnClick="LoadData_Event" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-inline float-right">
                            <div class="form-group">
                                <h4 class="mr-5 text-primary">QUANTITY :
                <asp:Label ID="LabelTotalJumlah" Text="0" runat="server"></asp:Label></h4>
                                <h4 class="text-success">SUBTOTAL : 
                        <asp:Label ID="LabelTotalNominal" Text="0" runat="server"></asp:Label></h4>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No.</th>
                                <th>Produk</th>
                                <th>Warna</th>
                                <th>Brand</th>
                                <th>Kategori</th>
                                <th>
                                    <table class="table table-sm table-borderless" style="margin-top: -7px; margin-bottom: -5px;">
                                        <tr class="thead-light">
                                            <th style="width: 150px;">Kode</th>
                                            <th style="width: 100px;">Varian</th>
                                            <th style="width: 100px;" class="text-right">Harga Jual</th>
                                            <th style="width: 100px;" class="text-right">Stok</th>
                                        </tr>
                                    </table>
                                </th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxProduk" ClientIDMode="Static" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListWarna" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListPemilik" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListKategori" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <table class="table table-condensed" style="margin-top: -7px; margin-bottom: -5px; padding: 0px;">
                                        <tbody>
                                            <tr>
                                                <th style="width: 80px;">
                                                    <asp:TextBox ID="TextBoxKode" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                                <th style="width: 100px;">
                                                    <asp:DropDownList ID="DropDownListVarian" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                                </tdh>
                                                    <th style="width: 80px;">
                                                        <asp:TextBox ID="TextBoxHargaJual" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                                <th style="width: 50px;">
                                                    <asp:TextBox ID="TextBoxStok" CssClass="form-control input-sm" Style="width: 100%;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </tbody>
                                    </table>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterProduk" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("Produk") %></td>
                                        <td><%# Eval("Warna") %></td>
                                        <td><%# Eval("PemilikProduk") %></td>
                                        <td><%# Eval("Kategori") %></td>
                                        <td>
                                            <table class="table table-hover table-condensed table-bordered" style="margin: 0px;">
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterProduk" runat="server" DataSource='<%# Eval("Stok") %>'>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 80px;"><a href='/WITAdministrator/Produk/Barcode.aspx?id=<%# Eval("IDKombinasiProduk") %>&jumlah=<%# Eval("Jumlah") %>' target="_blank"><%# Eval("Kode") %></a></td>
                                                                <td style="width: 110px;"><%# Eval("Atribut") %></td>
                                                                <td style="width: 80px;" class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                                <td style="width: 50px;" class="text-right"><strong><%# Eval("Jumlah").ToFormatHargaBulat() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressStokProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelStokProduk">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressStokProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

