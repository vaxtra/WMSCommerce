<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Pengguna_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pegawai
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
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="card">
                <div class="card-body">
                    <ul id="myTab" class="nav nav-tabs">
                        <li class="nav-item"><a href="#TabPegawai" id="Pegawai-tab" class="nav-link active" data-toggle="tab">Pegawai</a></li>
                        <li class="nav-item"><a href="#TabGrup" id="Grup-tab" class="nav-link" data-toggle="tab">Grup</a></li>
                    </ul>
                    <br />
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="TabPegawai">
                            <asp:UpdatePanel ID="UpdatePanelPengguna" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="DropDownListJumlahData" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                                    <asp:ListItem Text="10" Value="10" />
                                                    <asp:ListItem Text="20" Value="20" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group float-right mb-0">
                                                    <asp:Button ID="ButtonPrevious" runat="server" CssClass="btn btn-outline-light" Text="<<" OnClick="ButtonPrevious_Click" />
                                                    <asp:DropDownList ID="DropDownListHalaman" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="ButtonNext" runat="server" CssClass="btn btn-outline-light" Text=">>" OnClick="ButtonNext_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered TableSorter">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th>No.</th>
                                                        <th>Nama Lengkap <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Username <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Grup <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Lokasi <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Handphone <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Status</th>
                                                        <th></th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <td colspan="7">
                                                            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block" OnClick="EventData" ClientIDMode="Static" /></td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterPengguna" runat="server" OnItemCommand="RepeaterPengguna_ItemCommand" OnItemDataBound="RepeaterPengguna_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Container.ItemIndex + 1 + ((DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt()))) %></td>
                                                                <td><%# Eval("NamaLengkap") %></td>
                                                                <td><%# Eval("Username") %></td>
                                                                <td><%# Eval("GrupPengguna") %></td>
                                                                <td><%# Eval("Tempat") %></td>

                                                                <td>
                                                                    <a href='<%# "tel:" + Eval("Handphone") %>'><%# Eval("Handphone") %></a>
                                                                </td>

                                                                <td class="text-center">
                                                                    <asp:ImageButton ID="ImageStatus" runat="server" ImageUrl='<%# Pengaturan.FormatStatus(Eval("Status").ToString()) %>' CommandName="UbahStatus" CommandArgument='<%# Eval("IDPengguna") %>' BorderStyle="None" />
                                                                </td>

                                                                <td class="text-right fitSize">
                                                                    <asp:Button ID="ButtonLogin" runat="server" CssClass="btn btn-success btn-xs" Text="Login" CommandName="Login" CommandArgument='<%# Eval("IDPengguna") %>' />
                                                                    <a href='Pengaturan.aspx?id=<%# Eval("IDPengguna") %>' class="btn btn-info btn-xs">Ubah</a>
                                                                    <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPengguna") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <asp:UpdateProgress ID="updateProgressPengguna" runat="server" AssociatedUpdatePanelID="UpdatePanelPengguna">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressPengguna" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="TabGrup">
                            <asp:UpdatePanel ID="UpdatePanelGrup" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">
                                                <div class="table-responsive">
                                                    <table class="table table-sm table-hover table-bordered">
                                                        <thead>
                                                            <tr class="thead-light">
                                                                <th>No</th>
                                                                <th>Nama</th>
                                                                <th>Default URL</th>
                                                                <th></th>
                                                            </tr>
                                                            <tr class="thead-light">
                                                                <th>
                                                                    <asp:HiddenField ID="HiddenFieldIDGrupPengguna" runat="server" />
                                                                </th>
                                                                <th>
                                                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxNama" runat="server"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNama" runat="server" ErrorMessage="Data harus diisi"
                                                                        ControlToValidate="TextBoxNama" ForeColor="Red" ValidationGroup="groupGrupPengguna" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                                <th>
                                                                    <asp:TextBox CssClass="form-control input-sm" ID="TextBoxDefaultURL" runat="server"></asp:TextBox></th>
                                                                <th class="fitSize">
                                                                    <asp:Button ID="ButtonSimpanGrup" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanGrup_Click" ValidationGroup="groupGrupPengguna" /></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterGrupPengguna" runat="server" OnItemCommand="RepeaterGrupPengguna_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("Nama") %></td>
                                                                        <td><%# Eval("DefaultURL") %></td>
                                                                        <td class="text-right fitSize">
                                                                            <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandArgument='<%# Eval("IDGrupPengguna") %>' CommandName="Ubah" />
                                                                            <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDGrupPengguna") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdateProgress ID="updateProgressGrup" runat="server" AssociatedUpdatePanelID="UpdatePanelGrup">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressGrup" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
