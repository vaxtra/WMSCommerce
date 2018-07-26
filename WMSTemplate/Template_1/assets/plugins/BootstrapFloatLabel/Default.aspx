<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="assets_Plugins_FloatingLabel_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="bootstrap-float-label.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
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
    <div class="form-group input-group">
        <span class="has-float-label">
            <input class="form-control" id="first" type="text" placeholder="" />
            <label for="first">First</label>
        </span>
        <span class="has-float-label">
            <input class="form-control" id="last" type="text" placeholder="" />
            <label for="last">Last</label>
        </span>
    </div>
    <div class="form-group input-group">
        <span class="input-group-addon">@</span>
        <span class="has-float-label">
            <input class="form-control" id="email" type="email" placeholder="" />
            <label for="email">Email</label>
        </span>
    </div>
    <div class="form-group has-float-label">
        <input class="form-control" id="password" type="password" placeholder="••••••••" />
        <label for="password">Password</label>
    </div>
    <div class="form-group has-float-label">
        <select class="select2" id="country">
            <option selected>United States</option>
            <option>Canada</option>
            <option>...</option>
        </select>
        <label for="country">Country</label>
    </div>
    <div class="form-group checkbox">
        <label>
            <input type="checkbox" />
            Subscribe to newsletter
        </label>
    </div>
    <div class="form-group">
        <input class="form-control" id="password" type="password" placeholder="••••••••" />
    </div>
    <div>
        <button class="btn btn-block btn-primary" type="submit">Sign up</button>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

