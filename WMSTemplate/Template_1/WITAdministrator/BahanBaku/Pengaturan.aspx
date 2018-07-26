<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Bahan Baku
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
    <asp:UpdatePanel ID="UpdatePanelBahanBaku" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Nama</label>
                                        <asp:TextBox ID="TextBoxNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNama" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxNama" ForeColor="Red" ValidationGroup="groupBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Kode Bahan Baku</label>
                                        <asp:TextBox ID="TextBoxKodeBahanBaku" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnTextChanged="TextBoxKodeBahanBaku_TextChanged"></asp:TextBox>
                                        <asp:Label ID="LabelPeringatanKodeBahanBaku" runat="server" Text="-" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="text-muted font-weight-bold">Satuan Besar</label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListSatuanBesar" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListSatuanBesar_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidatorSatuanBesar" runat="server" ErrorMessage="-" ControlToValidate="DropDownListSatuanBesar" ForeColor="Red"
                                            ValidationGroup="groupBahanBaku" Display="Dynamic" OnServerValidate="CustomValidatorSatuanBesar_ServerValidate"></asp:CustomValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="text-muted font-weight-bold">Satuan Kecil</label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListSatuanKecil" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListSatuanKecil_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidatorSatuanKecil" runat="server" ErrorMessage="-" ControlToValidate="DropDownListSatuanKecil" ForeColor="Red"
                                            ValidationGroup="groupBahanBaku" Display="Dynamic" OnServerValidate="CustomValidatorSatuanKecil_ServerValidate"></asp:CustomValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Konversi</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxKonversi" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <asp:Label ID="LabelSatuanKonversi" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorKonversi" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxKonversi" ForeColor="Red" ValidationGroup="groupBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Harga Beli</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxHargaBeli" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <asp:Label ID="LabelSatuanHargaBeli" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHargaBeli" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxHargaBeli" ForeColor="Red" ValidationGroup="groupBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Stok</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxStok" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <asp:Label ID="LabelSatuanStok" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorStok" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxStok" ForeColor="Red" ValidationGroup="groupBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Minimum Stok</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxBatasStokAkanHabis" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <asp:Label ID="LabelSatuanStokAkanHabis" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBatasStokAkanHabis" runat="server" ErrorMessage="Data harus diisi"
                                            ControlToValidate="TextBoxBatasStokAkanHabis" ForeColor="Red" ValidationGroup="groupBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="text-muted font-weight-bold">Berat</label>
                                        <div class="controls">
                                            <asp:TextBox ID="TextBoxBerat" onfocus="this.select();" CssClass="InputDesimal form-control text-right input-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="text-muted font-weight-bold">Deskripsi</label>
                                        <asp:TextBox ID="TextBoxDeskripsi" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="text-muted font-weight-bold">Kategori Bahan Baku</label><br />
                                        <asp:CheckBoxList ID="CheckBoxListKategori" runat="server" class="checkbox-inline"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Tambah" OnClick="ButtonSimpan_Click" ValidationGroup="groupBahanBaku" />
                    <asp:Button ID="ButtonKembali" CssClass="btn btn-danger btn-const" runat="server" Text="Kembali" OnClick="ButtonKembali_Click" />
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressBahanBaku" runat="server" AssociatedUpdatePanelID="UpdatePanelBahanBaku">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressBahanBaku" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="server">
</asp:Content>

