<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_Tablesorter_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table class="table table-bordered table-condensed TableSorter">
        <thead>
            <tr>
                <th>Last Name</th>
                <th>First Name</th>
                <th>Email</th>
                <th>Due</th>
                <th>Web Site</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Smith</td>
                <td>John</td>
                <td>jsmith@gmail.com</td>
                <td>$50.00</td>
                <td>http://www.jsmith.com</td>
            </tr>
            <tr>
                <td>Bach</td>
                <td>Frank</td>
                <td>fbach@yahoo.com</td>
                <td>$50.00</td>
                <td>http://www.frank.com</td>
            </tr>
            <tr>
                <td>Doe</td>
                <td>Jason</td>
                <td>jdoe@hotmail.com</td>
                <td>$100.00</td>
                <td>http://www.jdoe.com</td>
            </tr>
            <tr>
                <td>Conway</td>
                <td>Tim</td>
                <td>tconway@earthlink.net</td>
                <td>$50.00</td>
                <td>http://www.timconway.com</td>
            </tr>
        </tbody>
    </table>

        <table class="table table-bordered table-condensed">
        <thead>
            <tr>
                <th>Last Name</th>
                <th>First Name</th>
                <th>Email</th>
                <th>Due</th>
                <th>Web Site</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Smith</td>
                <td>John</td>
                <td>jsmith@gmail.com</td>
                <td>$50.00</td>
                <td>http://www.jsmith.com</td>
            </tr>
            <tr>
                <td>Bach</td>
                <td>Frank</td>
                <td>fbach@yahoo.com</td>
                <td>$50.00</td>
                <td>http://www.frank.com</td>
            </tr>
            <tr>
                <td>Doe</td>
                <td>Jason</td>
                <td>jdoe@hotmail.com</td>
                <td>$100.00</td>
                <td>http://www.jdoe.com</td>
            </tr>
            <tr>
                <td>Conway</td>
                <td>Tim</td>
                <td>tconway@earthlink.net</td>
                <td>$50.00</td>
                <td>http://www.timconway.com</td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

