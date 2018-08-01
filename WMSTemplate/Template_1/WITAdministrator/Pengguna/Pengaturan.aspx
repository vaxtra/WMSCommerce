<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Pengguna_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Pegawai
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
    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="card">
                <div class="card-header">
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item"><a href="#TabProfil" id="Profil-tab" class="nav-link active" data-toggle="tab">Profil</a></li>
                        <li class="nav-item"><a href="#TabLengkap" role="tab" id="Lengkap-tab" class="nav-link" data-toggle="tab">Lengkap</a></li>
                        <li class="nav-item"><a href="#tabGaji" role="tab" id="Gaji-tab" class="nav-link" data-toggle="tab">Gaji</a></li>
                    </ul>
                </div>
                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="TabProfil">
                            <asp:UpdatePanel ID="UpdatePanelProfil" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Nama Lengkap <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="TextBoxNamaLengkap" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Username <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="TextBoxUsername" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Password <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="TextBoxPassword" TextMode="Password" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">PIN <span style="color: red;">*</span></label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="TextBoxPIN" CssClass="form-control input-sm" runat="server" Height="36px" Enabled="false"></asp:TextBox>
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text p-0">
                                                                    <asp:Button ID="ButtonGeneratePIN" CssClass="btn btn-light btn-sm" runat="server" Text="GET" OnClick="ButtonGeneratePIN_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Grup Pegawai <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="DropDownListGrupPengguna" runat="server" CssClass="select2" Width="100%">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Lokasi <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="DropDownListTempat" runat="server" CssClass="select2" Width="100%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Handphone <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="TextBoxHandphone" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Email</label>
                                                        <asp:TextBox ID="TextBoxEmail" TextMode="Email" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="PanelStatus">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Status</label>
                                                        <asp:DropDownList ID="DropDownListStatus" runat="server" CssClass="select2" Width="100%">
                                                            <asp:ListItem Text="Aktif" Value="True" Selected="True" />
                                                            <asp:ListItem Text="Non Aktif" Value="False" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label class="form-label bold text-muted">Catatan</label>
                                                        <asp:TextBox ID="TextBoxCatatan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressProfil" runat="server" AssociatedUpdatePanelID="UpdatePanelProfil">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressProfil" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="tab-pane" id="TabLengkap">
                            <asp:UpdatePanel ID="UpdatePanelLengkap" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Jenis Kelamin</label>
                                                        <asp:DropDownList ID="DropDownListGender" runat="server" CssClass="select2" Width="100%">
                                                            <asp:ListItem Text="Pria" Value="True" Selected="True" />
                                                            <asp:ListItem Text="Wanita" Value="False" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">

                                                    <div class="col-md-4">
                                                        <label class="form-label bold text-muted">Telepon</label>
                                                        <asp:TextBox ID="TextBoxTelepon" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Tempat Lahir</label>
                                                        <asp:TextBox ID="TextBoxTempatLahir" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Tanggal Lahir</label>
                                                        <asp:TextBox ID="TextBoxTanggalLahir" CssClass="form-control Tanggal input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Tanggal Bekerja</label>
                                                        <asp:TextBox ID="TextBoxTanggalBekerja" CssClass="form-control Tanggal input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Alamat</label>
                                                        <asp:TextBox ID="TextBoxAlamat" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Agama</label>
                                                        <asp:TextBox ID="TextBoxAgama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Status Perkawinan</label>
                                                        <asp:TextBox ID="TextBoxStatusPerkawinan" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Pendidikan Terakahir</label>
                                                        <asp:TextBox ID="TextBoxPendidikanTerakhir" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Kewarganegaraan</label>
                                                        <asp:TextBox ID="TextBoxKewarganegaraan" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="form-label bold text-muted">Nomor Identitas</label>
                                                <asp:TextBox ID="TextBoxNomorIdentitas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Nomor NPWP</label>
                                                        <asp:TextBox ID="TextBoxNomorNPWP" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Nomor Rekening</label>
                                                        <asp:TextBox ID="TextBoxNomorRekening" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Bank</label>
                                                        <asp:TextBox ID="TextBoxNamaBank" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Nama di Rekening</label>
                                                        <asp:TextBox ID="TextBoxNamaRekening" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressLengkap" runat="server" AssociatedUpdatePanelID="UpdatePanelLengkap">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressLengkap" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="tab-pane" id="tabGaji">
                            <asp:UpdatePanel ID="UpdatePanelGaji" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Gaji Kotor</label>
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text">
                                                                    <asp:Label ID="LabelRupiahGajiKotor" runat="server" CssClass="form-label" Font-Size="XX-Small" Font-Bold="true" Text="Rp."></asp:Label>
                                                                </div>
                                                            </div>
                                                            <asp:TextBox ID="TextBoxGajiKotor" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0" AutoPostBack="true" OnTextChanged="TextBoxGaji_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Tambahan Gaji</label>
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text">
                                                                    <asp:Label ID="LabelRupiahTambahanGaji" runat="server" CssClass="form-label" Font-Size="XX-Small" Font-Bold="true" Text="Rp."></asp:Label>
                                                                </div>
                                                            </div>
                                                            <asp:TextBox ID="TextBoxTambahanGaji" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0" AutoPostBack="true" OnTextChanged="TextBoxGaji_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Potongan Gaji</label>
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text">
                                                                    <asp:Label ID="LabelRupiahPotonganGaji" runat="server" CssClass="form-label" Font-Size="XX-Small" Font-Bold="true" Text="Rp."></asp:Label>
                                                                </div>
                                                            </div>
                                                            <asp:TextBox ID="TextBoxPotonganGaji" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server" Text="0" AutoPostBack="true" OnTextChanged="TextBoxGaji_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="form-label bold text-muted">Gaji Bersih</label>
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text">
                                                                    <asp:Label ID="LabelRupiahGajiBersih" runat="server" CssClass="form-label" Font-Size="XX-Small" Font-Bold="true" Text="Rp."></asp:Label>
                                                                </div>
                                                            </div>
                                                            <asp:TextBox ID="TextBoxGajiBersih" CssClass="form-control text-right input-sm" runat="server" Text="0" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressGaji" runat="server" AssociatedUpdatePanelID="UpdatePanelGaji">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressGaji" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" runat="server" CssClass="btn btn-success btn-const" Text="Tambah" OnClick="ButtonSimpan_Click" />
                    <a href="Default.aspx" class="btn btn-danger btn-const">Batal</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

