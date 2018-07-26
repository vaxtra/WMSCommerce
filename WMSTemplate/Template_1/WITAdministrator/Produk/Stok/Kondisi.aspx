<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Kondisi.aspx.cs" Inherits="WITAdministrator_Produk_Stok_ButuhRestok" %>

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
    Kondisi Stok Produk
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
    <asp:UpdatePanel ID="UpdatePanelKondisiStok" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListJenisStokProduk" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                            <asp:ListItem Text="Batas aman" Value="1" />
                            <asp:ListItem Text="Mendekati batas minimum" Value="2" />
                            <asp:ListItem Text="Mencapai batas minimum" Value="3" />
                            <asp:ListItem Text="Mendekati dan mencapai batas minimum" Value="4" />
                            <asp:ListItem Text="Batas aman dan mendekati batas minimum" Value="5" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary d-none" ClientIDMode="Static" OnClick="ButtonCari_Click" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="progress">
                    <asp:Literal ID="LiteralProgressBar" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-bordered table-hover">
                        <thead>
                            <tr class="thead-light">
                                <th>No.</th>
                                <th>Produk</th>
                                <th>Warna</th>
                                <th>Brand</th>
                                <th>Kategori</th>
                                <th>Tempat</th>
                                <th>Varian</th>
                                <th class="text-right">Minimum</th>
                                <th class="text-right">Quantity</th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxProduk" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListWarna" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListBrand" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListKategori" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListTempat" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListVarian" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="table-success">
                                <td colspan="9" class="text-right" style="font-weight: bold;">
                                    <asp:Label ID="LabelTotalStokProduk" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>
                            <tr class="table-success">
                                <td colspan="9" class="text-right" style="font-weight: bold;">
                                    <asp:Label ID="LabelTotalStokProduk1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressKondisiStok" runat="server" AssociatedUpdatePanelID="UpdatePanelKondisiStok">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressKondisiStok" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

