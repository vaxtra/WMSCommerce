<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Vendor.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Daftar Vendor
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
    <asp:UpdatePanel ID="UpdatePanelVendor" runat="server">
        <ContentTemplate>
            <div class="card">
                <h4 class="card-header bg-smoke">Vendor</h4>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-sm table-hover table-bordered">
                            <thead>
                                <tr class="thead-light">
                                    <th>No</th>
                                    <th>Nama</th>
                                    <th>Alamat</th>
                                    <th>Email</th>
                                    <th class="fitSize">Telepon 1</th>
                                    <th class="fitSize">Telepon 2</th>
                                    <th class="hidden">Tax</th>
                                    <th></th>
                                </tr>
                                <tr class="thead-light">
                                    <th>
                                        <asp:HiddenField ID="HiddenFieldIDVendor" runat="server" />
                                    </th>
                                    <th>
                                        <asp:TextBox ID="TextBoxNamaVendor" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaVendor" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxNamaVendor" ForeColor="Red" ValidationGroup="groupVendor" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxAlamatVendor" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxEmailVendor" TextMode="Email" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxTelepon1Vendor" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxTelepon2Vendor" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                    <th class="fitSize hidden">
                                        <div class="input-group" style="width: 100px;">
                                            <asp:TextBox ID="TextBoxTaxVendor" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">%</div>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTax" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxTaxVendor" ForeColor="Red" ValidationGroup="groupVendor" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                    <th class="fitSize">
                                        <asp:Button ID="ButtonSimpanVendor" runat="server" Text="Tambah" CssClass="btn btn-success btn-block" OnClick="ButtonSimpanVendor_Click" ValidationGroup="groupVendor" /></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterVendor" runat="server" OnItemCommand="RepeaterVendor_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td><%# Eval("Nama") %></td>
                                            <td><%# Eval("Alamat") %></td>
                                            <td><%# Eval("Email") %></td>
                                            <td><%# Eval("Telepon1") %></td>
                                            <td><%# Eval("Telepon2") %></td>
                                            <td class="text-right hidden"><%# (Eval("PersentaseTax").ToDecimal() * 100).ToFormatHarga() + "%" %></td>
                                            <td class="text-right fitSize">
                                                <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-outline-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDVendor") %>' />
                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-outline-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDVendor") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDVendor") + "\")" %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressVendor" runat="server" AssociatedUpdatePanelID="UpdatePanelVendor">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressVendor" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

