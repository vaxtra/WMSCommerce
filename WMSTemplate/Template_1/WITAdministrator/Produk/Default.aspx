<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Default" %>

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
    Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="/WITAdministrator/Import/Produk.aspx" class="btn btn-secondary btn-const text-white mr-1">Import</a>
    <asp:Button ID="ButtonExport" runat="server" class="btn btn-secondary btn-const mr-1" Text="Export" OnClick="ButtonExport_Click" />
    <h5 class="mr-1"><a id="LinkDownload" runat="server" visible="false" style="font-size: 12px !important;">Download File</a></h5>
    <a href="Pengaturan.aspx" class="btn btn-success btn-const text-white">Tambah</a>
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
                <div class="card-body">
                    <ul id="myTab" class="nav nav-tabs">
                        <li class="nav-item"><a href="#tabProduk" id="BahanBaku-tab" class="nav-link active" data-toggle="tab">Produk</a></li>
                        <li class="nav-item"><a href="#tabPemilikProduk" id="PemilikProduk-tab" class="nav-link" data-toggle="tab">Brand</a></li>
                        <li class="nav-item"><a href="#tabWarna" id="Warna-tab" class="nav-link" data-toggle="tab">Warna</a></li>
                        <li class="nav-item"><a href="#tabAtributProduk" id="AtributProduk-tab" class="nav-link" data-toggle="tab">Varian</a></li>
                        <li class="nav-item"><a href="#tabKategori" id="TidakKomposisi-tab" class="nav-link" data-toggle="tab">Kategori</a></li>
                        <li class="nav-item"><a href="#tabVendor" id="Vendor-tab" class="nav-link" data-toggle="tab">Vendor</a></li>
                    </ul>
                    <br />
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="tabProduk">
                            <asp:UpdatePanel ID="UpdatePanelProduk" runat="server">
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
                                                        <th>Produk <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Varian <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th>Kategori <span aria-hidden="true" class="glyphicon glyphicon-sort pull-right"></span></th>
                                                        <th class="fitSize text-center p-1"></th>
                                                    </tr>
                                                    <tr class="thead-light">
                                                        <td colspan="4">
                                                            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block" OnClick="EventData" ClientIDMode="Static" /></td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterProduk" runat="server" OnItemCommand="RepeaterProduk_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="fitSize"><%# Container.ItemIndex + 1 + ((DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt()))) %></td>
                                                                <td class="fitSize">
                                                                    <a href='Pengaturan.aspx?id=<%# Eval("IDProduk") %>'><%# Eval("Nama") %></a>
                                                                </td>
                                                                <td class="fitSize">
                                                                    <asp:Repeater ID="RepeaterProdukKombinasi" runat="server" DataSource='<%# Eval("KombinasiProduk") %>'>
                                                                        <ItemTemplate>
                                                                            <a href='/WITAdministrator/Produk/Barcode.aspx?id=<%# Eval("IDKombinasiProduk") %>' target="_blank"><%# Eval("KodeKombinasiProduk") %></a>
                                                                            -
                                                                        <%# Eval("Atribut") %>
                                                                            <br />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                                <td class="text-center"><%# Eval("Kategori") %></td>
                                                                <td class="text-center text-middle">
                                                                    <asp:ImageButton ID="ImageButtonStatus" BorderStyle="None" ImageUrl='<%# Pengaturan.FormatStatus(Eval("Status").ToString()) %>' CommandName="UbahStatus" CommandArgument='<%# Eval("IDProduk") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelPemilikProduk">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabPemilikProduk">
                            <asp:UpdatePanel ID="UpdatePanelPemilikProduk" runat="server">
                                <ContentTemplate>
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
                                                    <th></th>
                                                </tr>
                                                <tr class="thead-light">
                                                    <th>
                                                        <asp:HiddenField ID="HiddenFieldIDPemilikProduk" runat="server" />
                                                    </th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxNamaPemilikProduk" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data harus diisi"
                                                            ControlToValidate="TextBoxNamaPemilikProduk" ForeColor="Red" ValidationGroup="groupPemilikProduk" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxAlamatPemilikProduk" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxEmailPemilikProduk" TextMode="Email" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxTelepon1PemilikProduk" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                                    <th>
                                                        <asp:TextBox ID="TextBoxTelepon2PemilikProduk" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                                    <th class="fitSize">
                                                        <asp:Button ID="ButtonSimpanPemilikProduk" runat="server" Text="Tambah" CssClass="btn btn-success btn-block" OnClick="ButtonSimpanPemilikProduk_Click" ValidationGroup="groupPemilikProduk" /></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterPemilikProduk" runat="server" OnItemCommand="RepeaterPemilikProduk_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td><%# Eval("Nama") %></td>
                                                            <td><%# Eval("Alamat") %></td>
                                                            <td><%# Eval("Email") %></td>
                                                            <td><%# Eval("Telepon1") %></td>
                                                            <td><%# Eval("Telepon2") %></td>
                                                            <td class="text-right fitSize">
                                                                <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDPemilikProduk") %>' />
                                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPemilikProduk") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDPemilikProduk") + "\")" %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressPemilikProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelPemilikProduk">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressPemilikProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabWarna">
                            <asp:UpdatePanel ID="UpdatePanelWarna" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                            <div class="table-responsive">
                                                <table class="table table-sm table-hover table-bordered">
                                                    <thead>
                                                        <tr class="thead-light">
                                                            <th>No</th>
                                                            <th>Kode</th>
                                                            <th>Nama</th>
                                                            <th></th>
                                                        </tr>
                                                        <tr class="thead-light">
                                                            <th>
                                                                <asp:HiddenField ID="HiddenFieldIDWarna" runat="server" />
                                                            </th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxKodeWarna" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxNamaWarna" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data harus diisi"
                                                                    ControlToValidate="TextBoxNamaWarna" ForeColor="Red" ValidationGroup="groupWarna" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                            <th class="fitSize">
                                                                <asp:Button ID="ButtonSimpanWarna" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanWarna_Click" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterWarna" runat="server" OnItemCommand="RepeaterWarna_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Kode") %></td>
                                                                    <td><%# Eval("Nama") %></td>
                                                                    <td class="text-right fitSize">
                                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDWarna") %>' />
                                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDWarna") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDWarna") + "\")" %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressWarna" runat="server" AssociatedUpdatePanelID="UpdatePanelWarna">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressWarna" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabAtributProduk">
                            <asp:UpdatePanel ID="UpdatePanelAtributProduk" runat="server">
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
                                                                <asp:HiddenField ID="HiddenFieldIDAtributProduk" runat="server" />
                                                            </th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxNamaAtributProduk" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAtributProdukNama" runat="server" ErrorMessage="Data harus diisi"
                                                                    ControlToValidate="TextBoxNamaAtributProduk" ForeColor="Red" ValidationGroup="groupAtributProduk" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                            <th class="fitSize">
                                                                <asp:Button ID="ButtonSimpanAtributProduk" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanAtributProduk_Click" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterAtributProduk" runat="server" OnItemCommand="RepeaterAtributProduk_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Nama") %></td>
                                                                    <td class="text-right fitSize">
                                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDAtributProduk") %>' />
                                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDAtributProduk") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDAtributProduk") + "\")" %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressAtributProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelAtributProduk">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressAtributProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabKategori">
                            <asp:UpdatePanel ID="UpdatePanelKategoriProduk" runat="server">
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
                                                                <asp:HiddenField ID="HiddenFieldIDKategoriProduk" runat="server" />
                                                            </th>
                                                            <th>
                                                                <asp:TextBox ID="TextBoxKetegoriProdukNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaKetegoriProduk" runat="server" ErrorMessage="Data harus diisi"
                                                                    ControlToValidate="TextBoxKetegoriProdukNama" ForeColor="Red" ValidationGroup="GroupKategoriProduk" Display="Dynamic"></asp:RequiredFieldValidator></th>
                                                            <th class="fitSize">
                                                                <asp:Button ID="ButtonSimpanKategoriProduk" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpanKategoriProduk_Click" ValidationGroup="GroupKategoriProduk" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RepeaterKategoriProduk" runat="server" OnItemCommand="RepeaterKategoriProduk_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                    <td><%# Eval("Nama") %></td>
                                                                    <td class="text-right fitSize">
                                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDKategoriProduk") %>' />
                                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDKategoriProduk") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDKategoriProduk") + "\")" %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdateProgress ID="updateProgressKategoriProduk" runat="server" AssociatedUpdatePanelID="UpdatePanelKategoriProduk">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                                <asp:Image ID="imgUpdateProgressKategoriProduk" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tabVendor">
                            <asp:UpdatePanel ID="UpdatePanelVendor" runat="server">
                                <ContentTemplate>
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
                                                                <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDVendor") %>' />
                                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDVendor") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data No." + Eval("IDVendor") + "\")" %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

