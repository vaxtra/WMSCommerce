<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="BlogList.aspx.cs" Inherits="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="b-title-page b-title-page_mrg-btn_sm">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="b-title-page__title shuffle">Blog</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="b-gallery-5 b-isotope">
                    <%--   <ul class="b-isotope__filter list-inline">
                        <li><a href="./blog-main-2.html" data-filter="*" class="current">ALL</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".fashion">Fashion</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".lifestyle">lifestyle</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".travel">travel</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".trends">Trends</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".gadgets">Gadgets</a></li>
                    </ul>--%>
                    <div class="container">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="js-zoom-gallery grid">
                                    <div class="b-isotope__grid">
                                        <div class="grid-sizer"></div>
                                        <asp:Repeater ID="RepeaterPostHome" runat="server">
                                            <ItemTemplate>


                                                <div class="b-gallery-5__item grid-item lifestyle trends">
                                                   

                                                            <article class="b-post b-post-2 clearfix ">
                                                                 <asp:Repeater ID="RepeaterPostDetail" runat="server" DataSource='<%# Eval("DataSourcePostDetail") %>'>
                                                        <ItemTemplate>
                                                                <div  class='<%# Eval("ClassSingleImage") %>'>
                                                                    <a href="<%# Eval("ImgaeSingleDefaultURL") %>" class="js-zoom-images">
                                                                        <img src="<%# Eval("ImgaeSingleDefaultURL") %>" alt="Foto" class="img-responsive" /></a>
                                                                    <%--  <div class="entry-category">Category</div>--%>
                                                                </div>
                                                             </ItemTemplate>
                                                    </asp:Repeater>
                                                                <div class="entry-main">
                                                                    <div class="entry-header">
                                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="Blog.aspx?id=<%# Eval("ss") %>"><%# Eval("Judul") %></a></h2>
                                                                    </div>
                                                                    <div class="entry-content">
                                                                        <p><%# Eval("Deskripsi") %></p>
                                                                    </div>
                                                                    <div class="entry-footer">
                                                                        <div class="entry-meta">
                                                                            <a href="Blog.aspx?id=<%# Eval("ss") %>" class="entry-meta__date">24 Sep 2017</a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </article>

                                                       
                                                    <!-- end post-->


                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
