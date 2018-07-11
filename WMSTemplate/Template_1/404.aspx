<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="wrap-content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <ol class="breadcrumb">
                        <li><a href="./home.html">Home</a></li>
                        <li><a href="./home.html">Catalog</a></li>
                        <li><a href="./home.html">Clothong</a></li>
                        <li class="active">raincoats</li>
                    </ol>
                </div>
            </div>
        </div>

        <div class="container">

            <div class="row">
                <div class="col-md-12 ">

                    <div class="woocommerce">


                        <div class="post error404 not-found">



                            <h1 class="not-found-title">404</h1>
                            <h1 class="entry-title">Halaman tidak ditemukan</h1>


                            <div class="entry-content">
                                <p>Maaf, Halaman yang anda cari tidak ditemukan</p>
                            </div>
                            <!-- .entry-content -->




                        </div>


                    </div>

                </div>


            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
