<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Layout1.aspx.cs" Inherits="WITAdministrator_Page_Layout1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Layout Page
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
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <h3 class="border-bottom text-info">POST</h3>
                                <label class="text-muted font-weight-bold">Page</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownListPage" runat="server" CssClass="select2 w-100" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <label class="text-muted font-weight-bold">Post</label>
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>Urutan</th>
                                                    <th>Post</th>
                                                    <th>Jenis</th>
                                                    <th>Konten</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterPage" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td rowspan='<%# Eval("Count").ToInt() + 1 %>' class="fitSize"><%# Eval("Urutan") %></td>
                                                            <td colspan="3" class="fitSize font-weight-bold"><%# Eval("Post") %></td>
                                                        </tr>
                                                        <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fitSize"><%# Eval("Nama") %></td>
                                                                    <td class="text-center fitSize"><%# Eval("JenisBadge") %></td>
                                                                    <td class='<%# Eval("Jenis").ToInt() == 1 ? string.Empty : "d-none" %>'><%# Eval("Konten") %></td>
                                                                    <td class='<%# Eval("Jenis").ToInt() != 1 ? string.Empty : "d-none" %>'>
                                                                        <asp:Repeater ID="RepeaterImage" runat="server" DataSource='<%# Eval("Images") %>'>
                                                                            <ItemTemplate>
                                                                                <img src='<%# Eval("DefaultURL") %>' style="width: 100px;" />
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
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
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <h3 class="border-bottom text-info">LAYOUT</h3>
                                <div id="DivAlertSwap" runat="server">
                                    <asp:Label ID="LabelAlert" runat="server" Text="Pilih page layout"></asp:Label></div>
                                <div class="card">
                                    <div class="card-body bg-light">
                                        <asp:HiddenField ID="HiddenFieldIDPostDetail" runat="server" />
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover">
                                                <asp:Repeater ID="RepeateraLayout" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center font-weight-bold"><%# Eval("Post") %></td>
                                                        </tr>
                                                        <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>' OnItemCommand="RepeaterBody_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="ButtonPost" CssClass='<%# Eval("ClassButton") %>' runat="server" Text='<%# Eval("Nama") %>' CommandName="Switch" CommandArgument='<%# Eval("IDPostDetail") %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </div>
                                </div>
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

