<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_PengaturanX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Produk
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
    <div class="card">
        <div class="card-header">
            <ul id="myTab" class="nav nav-tabs card-header-tabs">
                <li class="nav-item"><a href="#tabProduk" id="Produk-tab" class="nav-link active" data-toggle="tab">Produk</a></li>
                <li id="navVarian" runat="server" class="nav-item"><a href="#tabVarian" id="Varian-tab" class="nav-link" data-toggle="tab">Varian</a></li>
                <li id="navPhoto" runat="server" class="nav-item"><a href="#tabPhoto" id="Photo-tab" class="nav-link" data-toggle="tab">Photo</a></li>
            </ul>
        </div>
        <div class="card-body">
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabProduk">
                    <asp:HiddenField ID="HiddenFieldIDProduk" runat="server" />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label font-weight-bold text-muted">Nama</label>
                                <asp:TextBox ID="TextBoxNamaProduk" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label font-weight-bold text-muted">Kode</label>
                                <asp:TextBox ID="TextBoxKodeProduk" onkeydown="return (event.keyCode!=13);" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label font-weight-bold text-muted">Warna</label>
                                <asp:TextBox ID="TextBoxWarna" Width="100%" ClientIDMode="Static" CssClass="Warna" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label font-weight-bold text-muted">Brand</label>
                                <asp:TextBox ID="TextBoxPemilikProduk" Width="100%" ClientIDMode="Static" CssClass="PemilikProduk" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="form-label font-weight-bold text-muted">Kategori</label>
                        <asp:TextBox ID="TextBoxProdukKategori" Width="100%" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="form-label font-weight-bold text-muted">Sub Kategori</label>
                        <asp:TextBox ID="TextBoxKategori" Width="100%" ClientIDMode="Static" CssClass="KategoriProduk" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="form-label font-weight-bold text-muted">Deksripsi</label>
                        <asp:TextBox ID="TextBoxDeskripsi" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="ButtonOk" CssClass="btn btn-success btn-const" runat="server" Text="Tambah" OnClick="ButtonOk_Click" />
                        <asp:Button ID="ButtonKeluar" CssClass="btn btn-danger btn-const" runat="server" Text="Keluar" OnClick="ButtonKeluar_Click" />
                    </div>
                </div>
                <div class="tab-pane" id="tabVarian">
                    <div class="form-group">
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered mb-0">
                                <thead>
                                    <tr class="thead-light">
                                        <th>Kode</th>
                                        <th>Varian</th>
                                        <th>Berat</th>
                                        <th>Harga Beli</th>
                                        <th>Harga Jual</th>
                                        <th>Stok</th>
                                        <th colspan="2">Status</th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th colspan="6">
                                            <asp:TextBox ID="TextBoxAtributProduk" Width="100%" ClientIDMode="Static" CssClass="AtributProduk" runat="server"></asp:TextBox></th>
                                        <th colspan="2">
                                            <asp:Button ID="ButtonBuatVarian" CssClass="btn btn-primary btn-block" runat="server" Text="Tambah" OnClick="ButtonBuatVarian_Click" /></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" OnItemCommand="RepeaterKombinasiProduk_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="HiddenFieldIDKombinasiProduk" runat="server" Value='<%# Eval("IDKombinasiProduk") %>' />
                                                    <asp:TextBox ID="TextBoxKodeKombinasiProduk" CssClass="form-control" runat="server" Text='<%# Eval("KodeKombinasiProduk") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxAtributProduk" Text='<%# Eval("Atribut") %>' ClientIDMode="Static" CssClass="AtributProdukSatuan" runat="server" Width="150px"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="TextBoxBerat" runat="server" onfocus="this.select();" Text='<%# Eval("Berat") %>' CssClass="form-control text-right InputDesimal"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxHargaBeli" runat="server" onfocus="this.select();" Text='<%# Eval("HargaBeli") %>' CssClass="form-control text-right InputDesimal"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxHargaJual" runat="server" onfocus="this.select();" Text='<%# Eval("HargaJual") %>' CssClass="form-control text-right InputDesimal"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="HiddenFieldJumlah" runat="server" Value='<%# Eval("Jumlah") %>' />
                                                    <asp:TextBox ID="TextBoxJumlah" runat="server" onfocus="this.select();" Text='<%# Eval("Jumlah") %>' CssClass="form-control text-right InputInteger"></asp:TextBox>
                                                </td>
                                                <td class="fitSize text-center" style="vertical-align: middle;">
                                                    <asp:ImageButton ID="ImageButtonStatus" BorderStyle="None" ImageUrl='<%# Pengaturan.FormatStatus(Eval("Status").ToString()) %>' CommandName="UbahStatus" CommandArgument='<%# Eval("IDStokProduk") %>' runat="server" />
                                                </td>
                                                <td class="text-center fitSize" style="vertical-align: middle;">
                                                    <asp:Button ID="ButtonHapus" runat="server" Text="X" CssClass="btn btn-danger btn-xs" CommandName="Hapus" CommandArgument='<%# Eval("IDKombinasiProduk") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="ButtonSimpanVarian" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpanVarian_Click" />
                    </div>
                </div>
                <div class="tab-pane" id="tabPhoto">
                    <ajaxToolkit:AjaxFileUpload ID="AjaxFileUploadFoto" runat="server" OnUploadComplete="AjaxFileUploadFoto_UploadComplete" />
                    <br />
                    <asp:Button ID="ButtonRefreshFoto" runat="server" Text="Refresh Foto" CssClass="btn btn-outline-primary btn-block" OnClick="ButtonRefreshFoto_Click" />
                    <br />
                    <div class="table-responsive">
                        <table class="table table-sm table-bordered table-condensed">
                            <thead>
                                <tr class="thead-light">
                                    <th class="hidden"></th>
                                    <th>Photo</th>
                                    <th>Cover Photo</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterFotoProduk" runat="server" OnItemCommand="RepeaterFotoProduk_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden"><%# Eval("IDFotoProduk") %></td>
                                            <td class="fitSize">
                                                <img src='<%# Eval("Foto") %>' style="width: 45px;" /></td>
                                            <td>
                                                <asp:ImageButton ID="ImageStatus" runat="server" ImageUrl='<%# Pengaturan.FormatStatus(Eval("FotoUtama").ToString()) %>' CommandName="FotoUtama" CommandArgument='<%# Eval("IDFotoProduk") %>' BorderStyle="None" /></td>
                                            <td class="text-right fitSize">
                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDFotoProduk") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <asp:Literal ID="LiteralJavascript" runat="server"></asp:Literal>
</asp:Content>

