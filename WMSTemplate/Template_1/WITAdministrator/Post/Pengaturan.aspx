<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Post_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="form-group">
                <label class="form-label">Title</label>
                <asp:TextBox CssClass="form-control input-sm" ID="TextBoxJudul" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Cover</label>
                <asp:Image ID="ImageCover" Width="100" runat="server" />
                <asp:FileUpload ID="FileUploadCover" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <label class="form-label"></label>
                <asp:TextBox ID="TextBoxIsi" CssClass="form-control input-sm" Rows="15" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <asp:Button ID="ButtonOk" CssClass="btn btn-success btn-sm" runat="server" Text="Simpan" OnClick="ButtonOk_Click" />
            <asp:Button ID="ButtonKeluar" CssClass="btn btn-danger btn-sm" runat="server" Text="Keluar" OnClick="ButtonKeluar_Click" />
        </div>
    </div>
</asp:Content>

