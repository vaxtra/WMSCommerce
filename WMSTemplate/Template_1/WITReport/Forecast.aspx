<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Forecast.aspx.cs" Inherits="WITReport_Niion_XForecast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Setting Forecast
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
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-3 col-lg-3">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-3 col-lg-3">
                        <asp:DropDownList ID="DropDownListTipe" CssClass="select2 w-100" runat="server" OnSelectedIndexChanged="DropDownListTipe_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Bulan" Value="1" />
                            <asp:ListItem Text="Hari" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-3 col-lg-3">
                        <asp:DropDownList ID="DropDownListBulan" CssClass="select2 w-50 float-left" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2 w-50 float-right" runat="server"></asp:DropDownList>

                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-2 col-lg-2">
                        <asp:Button ID="ButtonCari" CssClass="btn btn-secondary btn-const" runat="server" Text="Cari" OnClick="ButtonCari_Click" />
                        <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="card">
                            <h5 class="card-header bg-gradient-blue">FORECAST</h5>
                            <table class="table table-sm table-bordered table-hover">
                                <thead>
                                    <tr class="thead-light">
                                        <th>Bulan</th>
                                        <th colspan="2">Nominal</th>
                                        <th colspan="2">Quantity</th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxTotalNominal" runat="server" CssClass="form-control InputDesimal text-right"></asp:TextBox>
                                        </th>
                                        <th>
                                            <asp:Button ID="ButtonBagiRata" Width="100%" CssClass="btn btn-primary" runat="server" Text="Bagi Rata" OnClick="ButtonBagiRata_Click" />
                                        </th>
                                        <th>
                                            <asp:TextBox ID="TextBoxTotalQuantity" runat="server" CssClass="form-control InputDesimal text-right"></asp:TextBox>
                                        </th>
                                        <th>
                                            <asp:Button ID="ButtonBagiRata1" Width="100%" CssClass="btn btn-primary" runat="server" Text="Bagi Rata" OnClick="ButtonBagiRata_Click" />
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterForecastBulan" runat="server">
                                        <ItemTemplate>
                                            <tr class='<%# (int)Eval("Weekend") == 0 || (int)Eval("Weekend") == 5 || (int)Eval("Weekend") == 6 ? "table-warning" : "" %>'>
                                                <td class="fitSize"><strong><%# Eval("Nama") %></strong>
                                                    <asp:HiddenField ID="HiddenFieldID" runat="server" Value='<%# Eval("Key") %>' />
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextBoxNominal" Text='<%# Eval("Nominal") %>' onfocus="this.select();" Width="100%" CssClass="form-control form-control-sm InputDesimal text-right" runat="server"></asp:TextBox></td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextBoxQuantity" Text='<%# Eval("Quantity") %>' onfocus="this.select();" Width="100%" CssClass="form-control form-control-sm InputDesimal text-right" runat="server"></asp:TextBox></td>
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

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

