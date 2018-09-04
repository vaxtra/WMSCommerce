<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_PengaturanX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonKeluar" CssClass="btn btn-danger btn-const" runat="server" Text="Kembali" OnClick="ButtonKeluar_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
    <div class="form-group">
        <div class="card">
            <h4 class="card-header bg-smoke">Produk</h4>
            <div class="card-body">
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
                    <label class="form-label font-weight-bold text-muted">Koleksi</label>
                    <asp:TextBox ID="TextBoxKategori" Width="100%" ClientIDMode="Static" CssClass="KategoriProduk" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="form-label font-weight-bold text-muted">Deksripsi</label>
                    <asp:TextBox ID="TextBoxDeskripsi" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="card-footer">
                <asp:Button ID="ButtonOk" CssClass="btn btn-success btn-const" runat="server" Text="Tambah" OnClick="ButtonOk_Click" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <h4 class="card-header bg-smoke">Varian</h4>
            <div class="table-responsive">
                <table class="table table-sm table-hover table-bordered mb-0">
                    <thead>
                        <tr class="thead-light">
                            <th></th>
                            <th colspan="2">Varian</th>
                            <th>Berat</th>
                            <th>Harga Beli</th>
                            <th>Harga Jual</th>
                            <th>Stok</th>
                            <th colspan="2">Status</th>
                        </tr>
                        <tr class="thead-light">
                            <th class="fitSize">
                                <asp:CheckBox ID="CheckBoxSemua" runat="server" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="CheckBoxSemua_CheckedChanged" /></th>
                            <th colspan="6">
                                <asp:TextBox ID="TextBoxAtributProduk" Width="100%" ClientIDMode="Static" CssClass="AtributProduk" runat="server"></asp:TextBox></th>
                            <th colspan="2">
                                <asp:Button ID="ButtonBuatVarian" CssClass="btn btn-primary btn-block" runat="server" Text="Simpan" OnClick="ButtonBuatVarian_Click" /></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" OnItemCommand="RepeaterKombinasiProduk_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="text-center align-middle">
                                        <asp:CheckBox ID="CheckBoxPilih" runat="server" CssClass="checkbox" /></td>
                                    <td>
                                        <asp:Label ID="LabelIDKombinasiProduk" runat="server" CssClass="d-none" Text='<%# Eval("IDKombinasiProduk") %>'></asp:Label>
                                        <asp:Label ID="LabelKodeKombinasiProduk" runat="server" Text='<%# Eval("KodeKombinasiProduk") %>'></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LabelAtribut" runat="server" Text='<%# Eval("Atribut") %>'></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelBerat" runat="server" Text='<%# Eval("Berat") %>'></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelHargaBeli" runat="server" Text='<%# Eval("HargaBeli") %>'></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelHargaJual" runat="server" Text='<%# Eval("HargaJual") %>'></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelStok" runat="server" Text='<%# Eval("Jumlah") %>'></asp:Label>
                                    </td>
                                    <td class="fitSize text-center" style="vertical-align: middle;">
                                        <asp:ImageButton ID="ImageButtonStatus" BorderStyle="None" ImageUrl='<%# Pengaturan.FormatStatus(Eval("Status").ToString()) %>' CommandName="UbahStatus" CommandArgument='<%# Eval("IDStokProduk") %>' runat="server" />
                                    </td>
                                    <td class="text-center fitSize" style="vertical-align: middle;">
                                        <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-outline-danger btn-xs" CommandName="Hapus" CommandArgument='<%# Eval("IDKombinasiProduk") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="card-footer">
                <asp:Button ID="ButtonSimpanVarian" runat="server" Text="Simpan" CssClass="btn btn-success btn-const d-none" OnClick="ButtonSimpanVarian_Click" />
                <a id="ButtonUpdate" runat="server" class="btn btn-success btn-const dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Update</a>
                <div class="dropdown-menu p-2" aria-labelledby="ButtonProduk">
                    <asp:Button CssClass="btn btn-light btn-block" ID="ButtonPilihUpdateBerat" runat="server" data-toggle="modal" data-target="#exampleModalUpdateBerat" Text="Berat" OnClientClick="return false;" />
                    <asp:Button CssClass="btn btn-light btn-block" ID="ButtonPilihUpdateHargaBeli" runat="server" data-toggle="modal" data-target="#exampleModalUpdateHargaBeli" Text="Harga Beli" OnClientClick="return false;" />
                    <asp:Button CssClass="btn btn-light btn-block" ID="ButtonPilihUpdateHargaJual" runat="server" data-toggle="modal" data-target="#exampleModalUpdateHargaJual" Text="Harga Jual" OnClientClick="return false;" />
                    <asp:Button CssClass="btn btn-light btn-block" ID="ButtonPilihUpdateStok" runat="server" data-toggle="modal" data-target="#exampleModalUpdateStok" Text="Stok" OnClientClick="return false;" />
                </div>
            </div>

            <!-- UPDATE BERAT -->
            <div class="modal fade" id="exampleModalUpdateBerat" tabindex="-1" role="dialog" aria-labelledby="exampleModalUpdateBeratTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalUpdateBeratTitle">Update Berat</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxUpdateBerat" CssClass="form-control InputDesimal w-100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-start">
                            <asp:Button ID="ButtonUpdateBerat" runat="server" CssClass="btn btn-primary btn-const" Text="Simpan" OnClick="ButtonUpdateBerat_Click" UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- UPDATE HARGA BELI -->
            <div class="modal fade" id="exampleModalUpdateHargaBeli" tabindex="-1" role="dialog" aria-labelledby="exampleModalUpdateHargaBeliTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalUpdateHargaBeliTitle">Update Harga Beli</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxUpdateHargaBeli" CssClass="form-control InputDesimal w-100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-start">
                            <asp:Button ID="ButtonUpdateHargaBeli" runat="server" CssClass="btn btn-primary btn-const" Text="Simpan" OnClick="ButtonUpdateHargaBeli_Click" UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- UPDATE HARGA JUAL -->
            <div class="modal fade" id="exampleModalUpdateHargaJual" tabindex="-1" role="dialog" aria-labelledby="exampleModalUpdateHargaJualTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalUpdateHargaJualTitle">Update Harga Jual</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxUpdateHargaJual" CssClass="form-control InputDesimal w-100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-start">
                            <asp:Button ID="ButtonUpdateHargaJual" runat="server" CssClass="btn btn-primary btn-const" Text="Update" OnClick="ButtonUpdateHargaJual_Click" UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- UPDATE STOK -->
            <div class="modal fade" id="exampleModalUpdateStok" tabindex="-1" role="dialog" aria-labelledby="exampleModalUpdateStokTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalUpdateStokTitle">Update Stok</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxUpdateStok" CssClass="form-control InputInteger w-100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-start">
                            <asp:Button ID="ButtonUpdateStok" runat="server" CssClass="btn btn-primary btn-const" Text="Update" OnClick="ButtonUpdateStok_Click" UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="card">
            <h4 class="card-header bg-smoke">Photo</h4>
            <ajaxToolkit:AjaxFileUpload ID="AjaxFileUploadFoto" runat="server" CssClass="form-control" OnUploadComplete="AjaxFileUploadFoto_UploadComplete" />
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
            <div class="card-footer">
                <asp:Button ID="ButtonRefreshFoto" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonRefreshFoto_Click" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <asp:Literal ID="LiteralJavascript" runat="server"></asp:Literal>
</asp:Content>
