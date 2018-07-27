<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Penyesuaian.aspx.cs" Inherits="WITHumanCapitalManagement_Schedulling_Penyesuaian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelHeader" runat="server" Text="Penyesuaian Presensi"></asp:Label>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <asp:DropDownList ID="DropDownListPegawai" CssClass="select2" Style="width: 200px;" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 200px;" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem>2016</asp:ListItem>
                        <asp:ListItem>2017</asp:ListItem>
                        <asp:ListItem>2018</asp:ListItem>
                        <asp:ListItem>2019</asp:ListItem>
                        <asp:ListItem>2020</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <table style="margin-left: -15px;">
                        <tr>
                            <td>
                                <div class="btn-group" style="margin: 5px 5px 0 0;">
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJanuari" runat="server" Text="Januari" OnClick="ButtonJanuari_Click" Style="margin-left: 15px;" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonFebruari" runat="server" Text="Februari" OnClick="ButtonFebruari_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMaret" runat="server" Text="Maret" OnClick="ButtonMaret_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonApril" runat="server" Text="April" OnClick="ButtonApril_Click" />
                                </div>
                            </td>
                            <td>
                                <div class="btn-group" style="margin: 5px 5px 0 0">
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMei" runat="server" Text="Mei" OnClick="ButtonMei_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJuni" runat="server" Text="Juni" OnClick="ButtonJuni_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJuli" runat="server" Text="Juli" OnClick="ButtonJuli_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonAgustus" runat="server" Text="Agustus" OnClick="ButtonAgustus_Click" />
                                </div>
                            </td>
                            <td>
                                <div class="btn-group" style="margin: 5px 5px 0 0">
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonSeptember" runat="server" Text="September" OnClick="ButtonSeptember_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonOktober" runat="server" Text="Oktober" OnClick="ButtonOktober_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonNopember" runat="server" Text="Nopember" OnClick="ButtonNopember_Click" />
                                    <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonDesember" runat="server" Text="Desember" OnClick="ButtonDesember_Click" />
                                </div>
                                <div class="btn-group" style="margin: 5px 5px 0 0">
                                    <asp:Button CssClass="btn btn-sm btn-primary" ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
                                </div>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="HiddenFieldIDBulan" runat="server" />
                    <br />
                    <div class="col-md-12">
                        <asp:MultiView ID="MultiView" runat="server">
                            <asp:View ID="ScheduleHarian" runat="server">
                                <div class="col-md-4">
                                    <table class="table table-condensed table-hover">
                                        <thead>
                                            <tr class="active">
                                                <th colspan="2" class="text-center">Tanggal</th>
                                                <th class="text-center">Jam Masuk</th>
                                                <th class="text-center">Jam Keluar</th>
                                                <th class="text-center">Keterangan</th>
                                            </tr>
                                        </thead>
                                        <asp:Repeater ID="RepeaterSchedule1" runat="server">
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <asp:Label ID="LabelHiddenTanggal" runat="server" Style="display: none;"
                                                            Text='<%# new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), (int)Eval("Key")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                                        <td>
                                                            <%# Pengaturan.Hari(new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), (int)Eval("Key"))) %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LabelHari" runat="server" Text='<%# Eval("Key") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxJamMasuk" runat="server" CssClass="form-control TanggalJam" Text='<%# ((string[])Eval("Value"))[0] %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxJamKeluar" runat="server" CssClass="form-control TanggalJam" Text='<%# ((string[])Eval("Value"))[1] %>'></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxKeterangan" runat="server" CssClass="form-control" Text='<%# ((string[])Eval("Value"))[2] %>'></asp:TextBox></td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div class="col-md-4">
                                    <table class="table table-condensed table-hover">
                                        <thead>
                                            <tr class="active">
                                                <th colspan="2" class="text-center">Tanggal</th>
                                                <th class="text-center">Jam Masuk</th>
                                                <th class="text-center">Jam Keluar</th>
                                                <th class="text-center">Keterangan</th>
                                            </tr>
                                        </thead>
                                        <asp:Repeater ID="RepeaterSchedule2" runat="server">
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <asp:Label ID="LabelHiddenTanggal" runat="server" Style="display: none;"
                                                            Text='<%# new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), (int)Eval("Key")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                                        <td>
                                                            <%# Pengaturan.Hari(new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), (int)Eval("Key"))) %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LabelHari" runat="server" Text='<%# Eval("Key") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxJamMasuk" runat="server" CssClass="form-control TanggalJam" Text='<%# ((string[])Eval("Value"))[0] %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxJamKeluar" runat="server" CssClass="form-control TanggalJam" Text='<%# ((string[])Eval("Value"))[1] %>'></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxKeterangan" runat="server" CssClass="form-control" Text='<%# ((string[])Eval("Value"))[2] %>'></asp:TextBox></td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div class="col-md-4">
                                    <table class="table table-condensed table-hover">
                                        <thead>
                                            <tr class="active">
                                                <th colspan="2" class="text-center">Tanggal</th>
                                                <th class="text-center">Jam Masuk</th>
                                                <th class="text-center">Jam Keluar</th>
                                                <th class="text-center">Keterangan</th>
                                            </tr>
                                        </thead>
                                        <asp:Repeater ID="RepeaterSchedule3" runat="server">
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <asp:Label ID="LabelHiddenTanggal" runat="server" Style="display: none;"
                                                            Text='<%# new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), (int)Eval("Key")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                                        <td>
                                                            <%# Pengaturan.Hari(new DateTime(DropDownListTahun.SelectedValue.ToInt(), HiddenFieldIDBulan.Value.ToInt(), (int)Eval("Key"))) %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LabelHari" runat="server" Text='<%# Eval("Key") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxJamMasuk" runat="server" CssClass="form-control TanggalJam" Text='<%# ((string[])Eval("Value"))[0] %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxJamKeluar" runat="server" CssClass="form-control TanggalJam" Text='<%# ((string[])Eval("Value"))[1] %>'></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxKeterangan" runat="server" CssClass="form-control" Text='<%# ((string[])Eval("Value"))[2] %>'></asp:TextBox></td>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

