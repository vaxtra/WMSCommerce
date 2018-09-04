<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_BahanBaku_Default" %>

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
    Bahan Baku
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="/WITAdministrator/Import/BahanBaku.aspx" class="btn btn-secondary btn-const mr-1">Import</a>
    <asp:Button ID="ButtonExport" runat="server" class="btn btn-secondary btn-const mr-1" Text="Export" OnClick="ButtonExport_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
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
                        <li class="nav-item"><a href="#tabBahanBaku" id="BahanBaku-tab" class="nav-link active" data-toggle="tab">Bahan Baku</a></li>
                        <li class="nav-item"><a href="#tabSatuan" id="Satuan-tab" class="nav-link" data-toggle="tab">Satuan</a></li>
                        <li class="nav-item"><a href="#tabKategori" id="TidakKomposisi-tab" class="nav-link" data-toggle="tab">Kategori</a></li>
                    </ul>
                </div>
                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="tabBahanBaku">
                            <asp:UpdatePanel ID="UpdatePanelBahanBaku" runat="server">
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
                                                        <th>No</th>
                                                        <th>Nama</th>
                                                        <th>Kode</th>
                                                        <th>Kategori</th>
                                                        <th>Harga</th>
                                                        <th></th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <th colspan="6">
                                                            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)" placeholder="Cari Pegawai"></asp:TextBox></th>
                                                        <th class="d-none">
                                                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block d-none" OnClick="EventData" ClientIDMode="Static" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterBahanBaku" runat="server" OnItemCommand="RepeaterBahanBaku_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Container.ItemIndex + 1 + ((DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt()))) %></td>
                                                                <td><%# Eval("Nama") %></td>
                                                                <td><%# Eval("KodeBahanBaku") %></td>
                                                                <td><%# Eval("Kategori") %></td>
                                                                <td><%# Eval("HargaBeli").ToFormatHarga() %> /<%# Eval("SatuanBesar") %></td>
                                                                <td class="text-right fitSize">
                                                                    <a href='<%# "Pengaturan.aspx?id=" + Eval("IDBahanBaku") %>' class="btn btn-outline-info btn-xs">Ubah</a>
                                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-outline-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDBahanBaku") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDBahanBaku") + "\")" %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressBahanBaku" runat="server" AssociatedUpdatePanelID="UpdatePanelBahanBaku">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressPegawai" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabSatuan">
                            <asp:UpdatePanel ID="UpdatePanelSatuan" runat="server">
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
                                                                <asp:HiddenField ID="HiddenFieldIDSatuan" runat="server" />
                                                            </th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxSatuanNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSatuanNama" runat="server" ErrorMessage="Data harus diisi"
                                                                    ControlToValidate="TextBoxSatuanNama" ForeColor="Red" ValidationGroup="groupSatuan" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                            <th class="fitSize">
                                                                <asp:Button ID="ButtonSimpanSatuan" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanSatuan_Click" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterSatuan" runat="server" OnItemCommand="RepeaterSatuan_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Nama") %></td>
                                                                    <td class="text-right fitSize">
                                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-outline-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDSatuan") %>' />
                                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-outline-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDSatuan") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDSatuan") + "\")" %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressSatuan" runat="server" AssociatedUpdatePanelID="UpdatePanelSatuan">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressSatuan" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabKategori">
                            <asp:UpdatePanel ID="UpdatePanelKategoriBahanBaku" runat="server">
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
                                                                <asp:HiddenField ID="HiddenFieldIDKategoriBahanBaku" runat="server" />
                                                            </th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxKetegoriBahanBakuNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaKetegoriBahanBaku" runat="server" ErrorMessage="Data harus diisi"
                                                                    ControlToValidate="TextBoxKetegoriBahanBakuNama" ForeColor="Red" ValidationGroup="GroupKategoriBahanBaku" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                            <th class="fitSize">
                                                                <asp:Button ID="ButtonSimpanKategoriBahanBaku" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanKategoriBahanBaku_Click" ValidationGroup="GroupKategoriBahanBaku" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterKategoriBahanBaku" runat="server" OnItemCommand="RepeaterKategoriBahanBaku_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Nama") %></td>
                                                                    <td class="text-right fitSize">
                                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-outline-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDKategoriBahanBaku") %>' />
                                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-outline-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDKategoriBahanBaku") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDKategoriBahanBaku") + "\")" %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressKategoriBahanBaku" runat="server" AssociatedUpdatePanelID="UpdatePanelKategoriBahanBaku">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressKategoriBahanBaku" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
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

