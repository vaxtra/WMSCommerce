<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITHumanCapitalManagement_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Daftar Pegawai
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
    <asp:UpdatePanel runat="server" ID="UpdatePanelTimeClockManagement">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-8">
                    <asp:DropDownList ID="DropDownListPegawai" CssClass="select2" Style="width: 200px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPegawai_SelectedIndexChanged">
                    </asp:DropDownList>
                    <div style="margin-top: 5%;">
                        <asp:Repeater ID="RepeaterDataPegawai" runat="server" OnItemCommand="RepeaterDataPegawai_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-3 text-center" style="margin-bottom: 10px;">
                                    <asp:Button ID="Button1" BorderStyle="None" runat="server" class="img-circle" Style="background: url(thumbnail.JPG); background-size: 150%; background-position: center; width: 100px; height: 100px; margin: auto;" OnClick="Button1_Click"
                                        CommandName="Pilih" CommandArgument='<%# Eval("IDPengguna") %>' />
                                    <label>
                                        <asp:Label ID="Label" runat="server" Text='<%# Eval("NamaLengkap") %>'></asp:Label></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-md-4">
                    <div style="margin-top: 5%;">
                        <h2>Aktivitas Terakhir</h2>
                        <div class="col-md-12">
                            <asp:Repeater ID="RepeaterRecentClockInOut" runat="server" OnItemCommand="RepeaterRecentClockInOut_ItemCommand">
                                <ItemTemplate>
                                    <div class="row" style="margin-top: 5px;">

                                        <div class="col-md-3">
                                            <div class="img-circle" style="background: url(thumbnail.JPG); background-size: 150%; background-position: center; width: 70px; height: 70px; margin-top: auto;"></div>
                                        </div>
                                        <div class="col-md-9">
                                            <label>
                                                <asp:Label ID="Label" runat="server" Text='<%# Eval("NamaLengkap") %>'></asp:Label></label>
                                            <label>
                                                <i>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("RecentActivity") %>'></asp:Label></i></label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderClockInOut" runat="server" PopupControlID="ClockInOut" TargetControlID="LinkButton2" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="ClockInOut" class="col-lg-12">
                    <div class="modal-dialog text">
                        <div class="modal-content">
                            <div class="modal-header text-center">
                                <h3 class="modal-title">CLOCK IN/OUT</h3>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-8 text-center">
                                        <asp:Button ID="Button2" BorderStyle="None" runat="server" class="img-circle" Style="background: url(thumbnail.JPG); background-size: 150%; background-position: center; width: 200px; height: 200px; margin: auto;" OnClick="Button1_Click" />
                                        <asp:HiddenField ID="HiddenFieldIDPengguna" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <h3>
                                            <asp:Label ID="LabelUsername" runat="server" Text=""></asp:Label></h3>
                                        <h2>
                                            <asp:Label ID="LabelWaktuClockInOut" runat="server" Text=""></asp:Label></h2>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBoxUsername" runat="server" CssClass="form-control" placeholder="Enter your Username"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your Password" Style="margin-top: 5px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="ButtonDone" runat="server" Text="DONE" CssClass="btn btn-lg btn-primary pull-right" Style="margin-top: 5px; width: 100%;" OnClick="ButtonDone_Click" />
                                                    <asp:Button ID="ButtonKembali" runat="server" Text="BACK" CssClass="btn btn-lg btn-danger pull-right" Style="margin-top: 5px; width: 100%;" />
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

