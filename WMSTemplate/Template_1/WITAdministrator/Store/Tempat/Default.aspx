<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Tempat_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Lokasi
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-const">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="card">
                <div class="card-header bg-smoke">
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item"><a href="#tabLokasi" id="Lokasi-tab" class="nav-link active" data-toggle="tab">Lokasi</a></li>
                        <li class="nav-item"><a href="#tabKategori" id="Kategori-tab" class="nav-link" data-toggle="tab">Kategori</a></li>
                    </ul>
                </div>
                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="tabLokasi">
                            <asp:UpdatePanel ID="UpdatePanelLokasi" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>No</th>
                                                    <th>Kategori</th>
                                                    <th>Nama</th>
                                                    <th>Alamat</th>
                                                    <th>Telepon</th>
                                                    <th>Telepon</th>
                                                    <th>Email</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterTempat" runat="server" OnItemCommand="RepeaterTempat_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td class="fitSize"><%# Eval("Kategori") %></td>
                                                            <td><%# Eval("Nama") %></td>
                                                            <td><%# Eval("Alamat") %></td>
                                                            <td class="fitSize"><%# Eval("Telepon1") %></td>
                                                            <td class="fitSize"><%# Eval("Telepon2") %></td>
                                                            <td class="fitSize"><%# Eval("Email") %></td>
                                                            <td class="text-right fitSize">
                                                                <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-outline-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDTempat") %>' />
                                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-outline-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDTempat") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressLokasi" runat="server" AssociatedUpdatePanelID="UpdatePanelLokasi">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressLokasi" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabKategori">
                            <asp:UpdatePanel ID="UpdatePanelKategori" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                            <div class="table-responsive">
                                                <table class="table table-sm table-hover table-bordered">
                                                    <thead>
                                                        <tr class="thead-light">
                                                            <th>No</th>
                                                            <th>Nama</th>
                                                            <th></th>
                                                        </tr>
                                                        <tr class="thead-light">
                                                            <th>
                                                                <asp:HiddenField ID="HiddenFieldIDKategoriTempat" runat="server" />
                                                            </th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxKategoriNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKategoriNama" runat="server" ErrorMessage="Data harus diisi"
                                                                    ControlToValidate="TextBoxKategoriNama" ForeColor="Red" ValidationGroup="groupKategori" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                            <th class="fitSize">
                                                                <asp:Button ID="ButtonSimpanKategori" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanKategori_Click" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterKategoriTempat" runat="server" OnItemCommand="RepeaterKategoriTempat_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Nama") %></td>
                                                                    <td class="text-right fitSize">
                                                                        <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-outline-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDKategoriTempat") %>' Visible='<%# (int)Eval("IDKategoriTempat") > 5 %>' />
                                                                        <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-outline-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDKategoriTempat") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' Visible='<%# (int)Eval("IDKategoriTempat") > 5 %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressKategori" runat="server" AssociatedUpdatePanelID="UpdatePanelKategori">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressKategori" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

