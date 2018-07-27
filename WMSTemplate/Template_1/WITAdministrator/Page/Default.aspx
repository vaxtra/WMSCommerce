<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Page_Default" %>

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
    Page
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
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <div class="table-responsive">
                                <table class="table table-sm table-hover table-bordered">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No.</th>
                                            <th>Template</th>
                                            <th>Page</th>
                                            <th>Deskripsi</th>
                                            <th colspan="2" class="fitSize text-center p-1"></th>
                                        </tr>
                                        <tr class="thead-light">
                                            <th>
                                                <asp:HiddenField ID="HiddenFieldIDPage" runat="server" />
                                            </th>
                                            <th>
                                                <asp:DropDownList ID="DropDownListPageTemplate" CssClass="select2" runat="server"></asp:DropDownList></th>
                                            <th colspan="2">
                                                <asp:TextBox ID="TextBoxPageNama" runat="server" CssClass="form-control"></asp:TextBox></th>

                                            <th colspan="2">
                                                <asp:Button ID="ButtonPageSimpan" runat="server" Text="Tambah" CssClass="btn btn-success btn-block" OnClick="ButtonPageSimpan_Click" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterPage" runat="server" OnItemCommand="RepeaterPage_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td rowspan='<%# Eval("Count").ToInt() + 1 %>' class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                    <td rowspan='<%# Eval("Count").ToInt() + 1 %>' class="fitSize"><%# Eval("Template") %></td>
                                                    <td colspan="2" class="fitSize table-dark"><%# Eval("Nama") %></td>
                                                    <td colspan="2" class="text-center table-dark fitSize">
                                                        <a href='<%# "Pengaturan.aspx?idPage=" + Eval("IDPage") %>' class="btn btn-primary btn-xs">Post</a>
                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDPage") %>' />
                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPage") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus page " + Eval("Nama") + "\")" %>' />
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>' OnItemCommand="RepeaterBody_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="fitSize">
                                                                <%# Eval("Judul") %>
                                                                <br />
                                                                <span class="text-muted font-italic"><%# Eval("PostDetail") %></span>
                                                            </td>
                                                            <td><%# Eval("Deskripsi") %></td>
                                                            <td class="align-middle fitSize">
                                                                <a href='<%# "Pengaturan.aspx?idPage=" + Eval("IDPage") + "&id=" + Eval("IDPost") %>' class="btn btn-outline-info btn-xs btn-block">Ubah</a>
                                                            </td>
                                                            <td class="align-middle fitSize">
                                                                <asp:Button ID="ButtonHapus" CssClass="btn btn-outline-danger btn-xs btn-block" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDPost") %>' Visible='<%# Eval("VisibleHapus") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus post " + Eval("Judul") + "\")" %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressPage" runat="server" AssociatedUpdatePanelID="UpdatePanelPage">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressPage" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

