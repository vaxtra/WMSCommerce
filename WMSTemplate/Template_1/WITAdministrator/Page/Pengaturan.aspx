<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Page_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Post
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
    <asp:MultiView ID="MultiViewPage" runat="server">
        <asp:View ID="ViewPost" runat="server">
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <h3 class="border-bottom text-info">POST</h3>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                            <label class="text-muted font-weight-bold">Judul</label>
                                            <asp:TextBox ID="TextBoxJudul" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                            <label class="text-muted font-weight-bold">Align</label>
                                            <asp:DropDownList ID="DropDownListAlign" runat="server" CssClass="select2 w-100">
                                                <asp:ListItem Text="Left" Value="Left"></asp:ListItem>
                                                <asp:ListItem Text="Right" Value="Right"></asp:ListItem>
                                                <asp:ListItem Text="Center" Value="Center"></asp:ListItem>
                                                <asp:ListItem Text="Justified" Value="Justified"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <label class="text-muted font-weight-bold">Deskripsi</label>
                                            <asp:TextBox ID="TextBoxDeskripsi" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group d-none">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <label class="text-muted font-weight-bold">Tags</label>
                                            <asp:TextBox ID="TextBoxTags" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DivDetail" runat="server" class="form-group" visible="false">
                        <h3 class="border-bottom text-info">DETAIL</h3>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th>Nama</th>
                                        <th>Jenis</th>
                                        <th>Konten</th>
                                        <th>
                                            <asp:Button ID="ButtonTambahPostDetail" runat="server" class="btn btn-primary btn-const" Text="Tambah" OnClick="ButtonTambahPostDetail_Click" /></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterPostDetail" runat="server" OnItemCommand="RepeaterPostDetail_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Eval("Nama") %></td>
                                                <td class="text-center fitSize"><%# Eval("JenisBadge") %></td>
                                                <td class='<%# Eval("Jenis").ToInt() == 1 ? string.Empty : "d-none" %>'><%# Eval("Konten") %></td>
                                                <td class='<%# Eval("Jenis").ToInt() != 1 ? string.Empty : "d-none" %>'>
                                                    <asp:Repeater ID="RepeaterImage" runat="server" DataSource='<%# Eval("Images") %>'>
                                                        <ItemTemplate>
                                                            <img src='<%# Eval("DefaultURL") %>?w=100' />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                                <td class="text-center fitSize">
                                                    <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDPostDetail") %>' />
                                                    <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPostDetail") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
                    <asp:Button ID="ButtonKembali" CssClass="btn btn-danger btn-const" runat="server" Text="Kembali" OnClick="ButtonKembali_Click" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="ViewPostDetail" runat="server">
            <asp:HiddenField ID="HiddenFieldIDPostDetail" runat="server" />
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                                <label class="text-muted font-weight-bold">Nama</label>
                                <asp:TextBox ID="TextBoxDetailNama" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                <label class="text-muted font-weight-bold">Jenis</label>
                                <asp:DropDownList ID="DropDownListDetailJenis" runat="server" CssClass="select2 w-100" AutoPostBack="true" OnSelectedIndexChanged="DropDownListDetailJenis_SelectedIndexChanged">
                                    <asp:ListItem Text="Deskripsi" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Single Image" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Multiple Image" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Image Slider" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="DivKonten" runat="server" class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <h3 class="border-bottom text-info">KONTEN</h3>
                                <asp:TextBox ID="TextBoxDetailKonten" CssClass="form-control" Rows="5" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="DivSingleImage" runat="server" class="form-group" visible="false">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <h3 class="border-bottom text-info">UPLOAD IMAGE</h3>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:FileUpload ID="FileUploadSingleImage" runat="server" CssClass="form-control mr-1" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="ButtonUploadSingleImage" runat="server" Text="Upload" class="btn btn-primary" OnClick="ButtonUploadSingleImage_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Image ID="ImagePhotoProfile" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DivMultipleImage" runat="server" class="form-group" visible="false">
                        <asp:HiddenField ID="HiddenFieldPostDetailImage" runat="server" />
                        <h3 class="border-bottom text-info">UPLOAD IMAGE</h3>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                <label class="text-muted font-weight-bold">Judul</label>
                                                <asp:TextBox ID="TextBoxImageJudul" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                <label class="text-muted font-weight-bold">Image</label>
                                                <asp:FileUpload ID="FileUploadMultipleImage" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                <label class="text-muted font-weight-bold">Link</label>
                                                <asp:TextBox ID="TextBoxImageLink" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                <label class="text-muted font-weight-bold">Alt</label>
                                                <asp:TextBox ID="TextBoxImageAlt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <label class="text-muted font-weight-bold">Deskripsi</label>
                                                <asp:TextBox ID="TextBoxImageDeskripsi" CssClass="form-control" Rows="5" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="ButtonUploadMultipleImage" runat="server" Text="Tambah" class="btn btn-primary btn-const" OnClick="ButtonUploadMultipleImage_Click" />
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-bordered table-condensed">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>No</th>
                                                    <th>Photo</th>
                                                    <th>Deskripsi</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterImageMultiple" runat="server" OnItemCommand="RepeaterImageMultiple_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                            <td class="fitSize">
                                                                <img src='<%# Eval("DefaultURL") %>?w=100' /></td>
                                                            <td>
                                                                <span class="font-weight-bold">Judul : </span><%# Eval("Judul") %>
                                                                <br />
                                                                <span class="font-weight-bold">Link : </span><%# Eval("Link") %>
                                                                <br />
                                                                <span class="font-weight-bold">Alt : </span><%# Eval("Alt") %>
                                                                <br />
                                                                <br />
                                                                <span class="font-weight-bold">Deskripsi : </span>
                                                                <div class="form-group border bg-light mb-0">
                                                                    <%# Eval("Deskripsi") %>
                                                                </div>
                                                            </td>
                                                            <td class="text-center fitSize">
                                                                <asp:Button ID="ButtonUbah" runat="server" CssClass="btn btn-info btn-xs" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDPostDetailImage") %>' />
                                                                <asp:Button ID="ButtonHapus" runat="server" CssClass="btn btn-danger btn-xs" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPostDetailImage") %>' /></td>
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
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonDetailSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonDetailSimpan_Click" />
                    <asp:Button ID="ButtonDetailKembali" CssClass="btn btn-danger btn-const" runat="server" Text="Kembali" OnClick="ButtonDetailKembali_Click" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script type="text/javascript" src="/assets/plugins/Tinymce/tinymce.min.js"></script>

    <script type="text/javascript">
        tfm_path = '/assets/plugins/TinyFileManager/';

        tinymce.init({
            selector: "textarea",
            plugins: [
                "advlist autolink lists link image charmap print preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime media table contextmenu paste tinyfilemanager.net"
            ],
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
        });
    </script>
</asp:Content>

