﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Template_Default4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <style>
        .dropdown-submenu {
            position: relative;
        }

            .dropdown-submenu > .dropdown-menu {
                top: 0;
                left: 100%;
                margin-top: -6px;
                margin-left: -1px;
                -webkit-border-radius: 0 6px 6px 6px;
                -moz-border-radius: 0 6px 6px 6px;
                border-radius: 0 6px 6px 6px;
            }

            .dropdown-submenu:hover > .dropdown-menu {
                display: block;
            }

            .dropdown-submenu > a:after {
                display: block;
                content: " ";
                float: right;
                width: 0;
                height: 0;
                border-color: transparent;
                border-style: solid;
                border-width: 5px 0 5px 5px;
                border-left-color: #cccccc;
                margin-top: 5px;
                margin-right: -10px;
            }

            .dropdown-submenu:hover > a:after {
                border-left-color: #ffffff;
            }

            .dropdown-submenu.pull-left {
                float: none;
            }

                .dropdown-submenu.pull-left > .dropdown-menu {
                    left: -100%;
                    margin-left: 10px;
                    -webkit-border-radius: 6px 0 6px 6px;
                    -moz-border-radius: 6px 0 6px 6px;
                    border-radius: 6px 0 6px 6px;
                }
    </style>

    <div class="container-fluid">
        <div class="col-md-2">
            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu" style="display: block; position: static; margin-bottom: 5px; *width: 180px;">
                <li><a href="#">Action</a></li>
                <li><a href="#">Another action</a></li>
                <li><a href="#">Something else here</a></li>
                <li class="divider"></li>
                <li class="dropdown-submenu">
                    <a tabindex="-1" href="#">More options</a>
                    <ul class="dropdown-menu">
                        <li><a tabindex="-1" href="#">Second level</a></li>
                        <li class="dropdown-submenu">
                            <a href="#">More..</a>
                            <ul class="dropdown-menu">
                                <li><a href="#">3rd level</a></li>
                                <li class="dropdown-submenu">
                                    <ul class="dropdown-menu">
                                        <li><a href="#">3rd level</a></li>
                                        <li><a href="#">3rd level</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li><a href="#">Second level</a></li>
                        <li><a href="#">Second level</a></li>
                    </ul>
                </li>
            </ul>
        </div>
        <div class="col-md-5">
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

