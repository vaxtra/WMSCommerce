<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Supplier.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_Supplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Daftar Supplier
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
    <asp:UpdatePanel ID="UpdatePanelSupplier" runat="server">
        <ContentTemplate>
            <div class="card">
                <h4 class="card-header bg-smoke">Supplier</h4>
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
                                    <td>
                                        <asp:HiddenField ID="HiddenFieldIDSupplier" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNama" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxNama" ForeColor="Red" ValidationGroup="groupSupplier" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxAlamat" CssClass="form-control input-sm" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxEmail" TextMode="Email" CssClass="form-control input-sm" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelepon1" CssClass="form-control input-sm" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelepon2" CssClass="form-control input-sm" runat="server"></asp:TextBox></td>
                                    <td class="fitSize hidden">
                                        <div class="input-group" style="width: 100px;">
                                            <asp:TextBox ID="TextBoxTax" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0.00"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">%</div>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTax" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxTax" ForeColor="Red" ValidationGroup="groupSupplier" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                    <td class="fitSize">
                                        <asp:Button ID="ButtonSimpanSupplier" runat="server" Text="Tambah" CssClass="btn btn-success btn-block" OnClick="ButtonSimpanSupplier_Click" ValidationGroup="groupSupplier" /></td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterSupplier" runat="server" OnItemCommand="RepeaterSupplier_ItemCommand">
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
                                                <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-outline-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDSupplier") %>' />
                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-outline-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDSupplier") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDSupplier") + "\")" %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressSupplier" runat="server" AssociatedUpdatePanelID="UpdatePanelSupplier">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressSupplier" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

