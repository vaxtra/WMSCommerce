<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Distance.aspx.cs" Inherits="WITAdministrator_Store_Tempat_Distance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Jarak Antar Tempat
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <asp:HiddenField ID="HiddenFieldJumlahTempat" runat="server" />
        <asp:Repeater ID="RepeaterTempat" runat="server">
            <ItemTemplate>
                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                    <span style="font-size: 25px;"><%# Eval("Nama") %></span>
                    <span style="font-weight: bold; color: lightgray;"><%# Eval("Kategori") %></span>

                    <table class="table table-condensed table-hover">
                        <asp:Repeater ID="RepeaterJarak" runat="server" DataSource='<%# Eval("Jarak") %>'>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("TBTempat1.Nama") %></td>
                                    <td class="text-right"><%# Eval("Jarak") %></td>
                                    <td class="text-right">
                                        <%# Eval("Durasi") %>
                                    </td>
                                    <td>
                                        <input id="Button" class='<%# Container.ItemIndex + 1 < Math.Ceiling(HiddenFieldJumlahTempat.Value.ToDecimal() / 2) ? "btn btn-sm btn-success" : "btn btn-sm btn-danger" %>' type="button" value=" " /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

