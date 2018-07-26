<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_JenisBiayaProyeksi_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Jenis Biaya Proyeksi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="/WITAdministrator/Import/JenisBiayaProyeksi.aspx" class="btn btn-default">Import Excel</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr class="active">
                        <th>Urutan</th>
                        <th>Nama</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterJenisBiayaProyeksi" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="fitSize"><%# Eval("Urutan") %></td>
                                <td><%# Eval("Nama") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
            <asp:UpdatePanel ID="UpdatePanelBiayaProduksi" runat="server">
                <ContentTemplate>
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="radio" style="margin-left: 20px;">
                                <asp:RadioButtonList ID="RadioButtonListEnumBiayaProduksi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListEnumBiayaProduksi_SelectedIndexChanged">
                                    <asp:ListItem Text="Persentase dari Harga Komposisi" Value="Persentase" Selected="True" />
                                    <asp:ListItem Text="Nominal" Value="Nominal" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:TextBox ID="TextBoxJenisBiayaProyeksiDetail" runat="server" CssClass="form-control InputDesimal text-right input-sm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJenisBiayaProyeksiDetail" runat="server" ErrorMessage="Data harus diisi"
                                    ControlToValidate="TextBoxJenisBiayaProyeksiDetail" ForeColor="Red" ValidationGroup="groupJenisBiayaProyeksi" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidatorJenisBiayaProyeksiDetail" runat="server" ErrorMessage="-" ControlToValidate="TextBoxJenisBiayaProyeksiDetail" ForeColor="Red"
                                    ValidationGroup="groupJenisBiayaProyeksi" Display="Dynamic" OnServerValidate="CustomValidatorJenisBiayaProduksiDetail_ServerValidate"></asp:CustomValidator>
                                <span class="input-group-addon">
                                    <asp:Label ID="LabelStatusJenisBiayaProyeksiDetail" runat="server" CssClass="form-label" Font-Bold="true" Text="%"></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="ButtonSimpanJenisBiayaProyeksi" CssClass="btn btn-success btn-sm" runat="server" Text="Simpan" OnClick="ButtonSimpanJenisBiayaProduksi_Click" ValidationGroup="groupJenisBiayaProyeksi" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-md-4">
            <h4 class="titlenya">Batas Bawah</h4>
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center">Nama</th>
                            <th class="text-center">Jenis</th>
                            <th class="text-center">Biaya</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterBatasBawah" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nama") %></td>
                                    <td><%# Eval("Jenis") %></td>
                                    <td class="text-right"><%# Eval("Biaya") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-md-4">
            <h4 class="titlenya">Batas Tengah</h4>
            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr>
                        <th class="text-center">Nama</th>
                        <th class="text-center">Jenis</th>
                        <th class="text-center">Biaya</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterBatasTengah" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Nama") %></td>
                                <td><%# Eval("Jenis") %></td>
                                <td class="text-right"><%# Eval("Biaya") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="col-md-4">
            <h4 class="titlenya">Batas Atas</h4>
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center">Nama</th>
                            <th class="text-center">Jenis</th>
                            <th class="text-center">Biaya</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterBatasAtas" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nama") %></td>
                                    <td><%# Eval("Jenis") %></td>
                                    <td class="text-right"><%# Eval("Biaya") %></td>
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

