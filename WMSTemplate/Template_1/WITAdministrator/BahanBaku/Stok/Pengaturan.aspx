<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_Stok_Pengaturan" %>

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

        function Func_ButtonSimpan(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonSimpan');
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
    <asp:Label ID="LabelJudul" runat="server"></asp:Label>
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
    <asp:UpdatePanel ID="UpdatePanelStokOpname" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2 mr-1" runat="server" Enabled="false"></asp:DropDownList>
                        <asp:DropDownList ID="DropDownListJenisStok" CssClass="select2 mr-1" runat="server"></asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="LoadData_Event" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-bordered table-hover">
                        <thead>
                            <tr class="thead-light">
                                <th>No.</th>
                                <th>Kode</th>
                                <th>Bahan Baku</th>
                                <th>Kategori</th>
                                <th>Quantity</th>
                                <th>Satuan</th>
                                <th colspan="2">Stok</th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxKodeBahanBaku" runat="server" Style="width: 100%;" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:TextBox ID="TextBoxBahanBaku" runat="server" Style="width: 100%;" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListKategori" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:TextBox ID="TextBoxQuantity" runat="server" Style="width: 100%;" CssClass="text-right form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListSatuan" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                    </asp:DropDownList></th>
                                <th colspan="2" class="fitSize">
                                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-block" ClientIDMode="Static" OnClick="ButtonSimpan_Click" /></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize">
                                            <%# Container.ItemIndex + 1 %>
                                        </td>
                                        <td class="fitSize">
                                            <%# Eval("Kode") %>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelIDBahanBaku" Visible="false" runat="server" Text='<%# Eval("IDBahanBaku") %>'></asp:Label><%# Eval("BahanBaku") %></td>
                                        <td><%# Eval("Kategori") %></td>

                                        <td class="fitSize text-right">
                                            <%# Eval("Jumlah").ToFormatHarga() %>
                                        </td>
                                        <td class="fitSize">
                                            <asp:Label ID="LabelIDSatuanKonversi" Visible="false" runat="server" Text='<%# Eval("IDSatuanKonversi") %>'></asp:Label><%# Eval("Satuan") %></td>
                                        <td class="fitSize">
                                            <asp:TextBox ID="TextBoxStokTerbaru" runat="server" CssClass="text-right form-control input-sm InputDesimal" onkeypress="return Func_ButtonSimpan(event)"></asp:TextBox>
                                        </td>
                                        <td class="fitSize">
                                            <asp:DropDownList ID="DropDownListPilih" runat="server" Width="75px" Height="38px" DataSource='<%# Eval("DropDownSatuan") %>'>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressStokOpname" runat="server" AssociatedUpdatePanelID="UpdatePanelStokOpname">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressStokOpname" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

