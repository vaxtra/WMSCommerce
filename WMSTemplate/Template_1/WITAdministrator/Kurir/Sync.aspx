<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Sync.aspx.cs" Inherits="WITAdministrator_Import_Produk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Sync
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
            <%--            <div class="form-group">
                <a href="/file_excel/template/(Format) Produk.xls">download template</a>
            </div>--%>
            <div class="form-group">
                <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            </div>
            <%--            <div class="form-group">
                <label class="form-label bold">Jika ada produk duplikat dengan database</label>
                <div style="margin-left: 20px;">
                    <asp:RadioButtonList ID="RadioButtonListJenisImport" runat="server" CssClass="radio">
                        <asp:ListItem Text="Tambah stok lama dengan stok baru" Value="2" Selected="True" />
                        <asp:ListItem Text="Ganti jumlah stok dengan yang baru" Value="1" />
                    </asp:RadioButtonList>
                </div>
            </div>--%>
            <div class="form-inline">
                <div class="form-group">
                    <%-- <asp:FileUpload ID="FileUploadExcel" runat="server" Width="100%" />--%>
                </div>
                <div class="form-group">
                    <asp:Literal ID="LiteralResult" runat="server"></asp:Literal>
                    <asp:Button ID="ButtonSync" CssClass="btn btn-success btn-sm" runat="server" Text="Sync" OnClick="ButtonSync_Click" />
                    <%--<a href="/WITAdministrator/Produk/Default.aspx" class="btn btn-danger btn-cons">Keluar</a>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

