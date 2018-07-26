<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITSurvey_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6 text-right">
                    <h3>Soal #<asp:Label ID="LabelIDSoal" runat="server"></asp:Label></h3>
                </div>
                <div class="col-md-6">
                    <h3>
                        <asp:Button ID="ButtonOk" runat="server" Text="Ok" CssClass="btn btn-primary btn-sm" OnClick="ButtonOk_Click" />
                        <a href="Default.aspx" class="btn btn-danger btn-sm">Keluar</a>
                    </h3>
                </div>
            </div>

            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>

            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:DropDownList ID="DropDownListTempat" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:DropDownList ID="DropDownListPengguna" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Pembuatan</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox CssClass="form-control" ID="TextBoxTanggalPembuatan" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Status</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="DropDownListStatus" Style="width: 100%" CssClass="select2" runat="server">
                                            <asp:ListItem Text="Aktif" Value="1" />
                                            <asp:ListItem Text="Tidak Aktif" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Mulai</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalMulai" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Selesai</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox CssClass="form-control Tanggal" ID="TextBoxTanggalSelesai" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Judul</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="TextBoxJudul" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Keterangan</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="TextBoxKeterangan" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="PanelPertanyaan" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover" style="font-size: 12px;">
                                <thead>
                                    <tr class="active">
                                        <th>No</th>
                                        <th>Pertanyaan</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterSoalPertanyaan" runat="server" OnItemCommand="RepeaterSoalPertanyaan_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.ItemIndex + 1 %>
                                                    <asp:Label ID="LabelIDSoalPertanyaan" Visible="false" runat="server" Text='<%# Eval("IDSoalPertanyaan") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxPertanyaan" CssClass="form-control" TextMode="MultiLine" runat="server" Text='<%# Eval("Isi") %>'></asp:TextBox>
                                                    <br />
                                                    <asp:Button ID="ButtonHapus" runat="server" Text="Hapus Pertanyaan" CssClass="btn btn-danger btn-xs" CommandName="Hapus" CommandArgument='<%# Eval("IDSoalPertanyaan") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                                                </td>
                                                <td>
                                                    <table class="table table-condensed table-hover">
                                                        <thead>
                                                            <tr class="active">
                                                                <th>Jawaban</th>
                                                                <th>Bobot</th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterSoalJawaban" runat="server" DataSource='<%# Eval("TBSoalJawabans") %>' OnItemCommand="RepeaterSoalJawaban_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="TextBoxIsi" placeholder="Jawaban" CssClass="form-control" runat="server" Text='<%# Eval("Isi") %>'></asp:TextBox>
                                                                            <asp:Label ID="LabelIDSoalJawaban" Visible="false" runat="server" Text='<%# Eval("IDSoalJawaban") %>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <div class="col-xs-4">
                                                                                <asp:TextBox ID="TextBoxBobot" placeholder="Bobot" CssClass="form-control" runat="server" Text='<%# Eval("Bobot") %>'></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-danger btn-xs" CommandName="Hapus" CommandArgument='<%# Eval("IDSoalJawaban") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Button ID="ButtonTambahJawaban" runat="server" Text="Tambah Jawaban" CssClass="btn btn-primary btn-xs" CommandName="TambahJawaban" CommandArgument='<%# Eval("IDSoalPertanyaan") %>' />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="3" class="text-right">
                                            <asp:Button ID="ButtonTambahPertanyaan" runat="server" Text="Tambah Soal" CssClass="btn btn-primary btn-xs" OnClick="ButtonTambahPertanyaan_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

