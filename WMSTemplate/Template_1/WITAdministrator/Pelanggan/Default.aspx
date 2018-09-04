<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Pelanggan_Default" %>

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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pelanggan
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExcel" CssClass="btn btn-secondary btn-const" runat="server" Text="Export" OnClick="ButtonExcel_Click" />
    <h5><a id="LinkDownload" runat="server" visible="false">Download File</a></h5>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelWarning" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="card">
                <div class="card-header bg-smoke">
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item"><a href="#tabPelanggan" id="Pegawai-tab" class="nav-link active" data-toggle="tab">Pelanggan</a></li>
                        <li class="nav-item"><a href="#tabGrup" role="tab" id="Grup-tab" class="nav-link" data-toggle="tab">Grup</a></li>
                    </ul>
                </div>
                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="tabPelanggan">
                            <asp:UpdatePanel ID="UpdatePanelPelanggan" runat="server">
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
                                            <table class="table table-sm table-hover table-bordered">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th class="fitSize">No</th>
                                                        <th>Nama</th>
                                                        <th>Grup</th>
                                                        <th>Email</th>
                                                        <th>Phone</th>
                                                        <th>Deposit</th>
                                                        <th>Status</th>
                                                        <th></th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <th colspan="7">
                                                            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)" placeholder="Cari Pelanggan"></asp:TextBox></th>
                                                        <th>
                                                            <a href="Pengaturan.aspx" class="btn btn-success btn-block">Tambah</a>
                                                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block d-none" OnClick="EventData" ClientIDMode="Static" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterPelanggan" runat="server" OnItemCommand="RepeaterPelanggan_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 + ((DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt()))) %></td>
                                                                <td><%# Eval("NamaLengkap") %></td>
                                                                <td><%# Eval("Grup") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("Handphone") %></td>
                                                                <td class="OutputDesimal text-right"><%# Eval("Deposit") %></td>
                                                                <td class="text-center">
                                                                    <%--<%# Pengaturan.FormatStatus(Eval("Status").ToString()) %>--%>
                                                                    <asp:ImageButton ID="ImageButtonStatus" BorderStyle="None" ImageUrl='<%# Pengaturan.FormatStatus(Eval("Status")) %>' CommandName="UbahStatus" CommandArgument='<%# Eval("IDPelanggan") %>' runat="server" />
                                                                </td>

                                                                <td class="fitSize hidden-print">
                                                                    <a class="btn btn-outline-info  btn-xs " href="Pengaturan.aspx?id=<%# Eval("IDPelanggan") %>">Ubah</a>
                                                                    <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-outline-danger btn-xs " CommandName="Hapus" CommandArgument='<%# Eval("IDPelanggan") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressPelanggan" runat="server" AssociatedUpdatePanelID="UpdatePanelPelanggan">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressPelanggan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabGrup">
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
                                                                <th>Jenis Bonus</th>
                                                                <th>Persentase</th>
                                                                <th></th>
                                                            </tr>
                                                            <tr class="thead-light">
                                                                <th>
                                                                    <asp:HiddenField ID="HiddenFieldIDGrupPelanggan" runat="server" />
                                                                </th>
                                                                <th>
                                                                    <asp:TextBox ID="TextBoxNama" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                                                <th>
                                                                    <asp:DropDownList ID="DropDownListBonusGrupPelanggan" runat="server" CssClass="select2" Width="100%">
                                                                        <asp:ListItem Text="Potongan" Value="1" />
                                                                        <asp:ListItem Text="Komisi" Value="2" />
                                                                    </asp:DropDownList></th>
                                                                <th>
                                                                    <div class="input-group">
                                                                        <asp:TextBox ID="TextBoxPersentase" onfocus="this.select();" CssClass="form-control InputDesimal input-sm text-right" runat="server" Text="0"></asp:TextBox>
                                                                        <div class="input-group-prepend">
                                                                            <div class="input-group-text">%</div>
                                                                        </div>
                                                                    </div>
                                                                </th>
                                                                <th>
                                                                    <asp:Button ID="ButtonSimpanGrup" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanGrup_Click" ValidationGroup="groupGrupPelanggan" /></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterGrupPelanggan" runat="server" OnItemCommand="RepeaterGrupPelanggan_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("Nama") %></td>
                                                                        <td><%# (PilihanBonusGrupPelanggan)Eval("EnumBonusGrupPelanggan") %></td>
                                                                        <td><%# Eval("Persentase").ToFormatHarga() %>%</td>
                                                                        <td class="text-right fitSize">
                                                                            <asp:Button Visible='<%# (int.Parse((Eval("IDGrupPelanggan").ToString())) > 1) ? true : false %>' ID="ButtonUbah" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDGrupPelanggan") %>' CssClass="btn btn-outline-info btn-xs" />
                                                                            <asp:Button Visible='<%# (int.Parse((Eval("IDGrupPelanggan").ToString())) > 1) ? true : false %>' ID="ButtonHapus" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDGrupPelanggan") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDGrupPelanggan") + "\")" %>' CssClass="btn btn-outline-danger btn-xs" />
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

