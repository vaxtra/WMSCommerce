<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="BlogDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
     <div class="wrap-content">

        <div class="container">
            <article class="b-post b-post-full-2 clearfix">
                <asp:Repeater ID="RepeaterPost" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="entry-header">
                                    <h2 class="entry-title"><%# Eval("Judul") %></h2>
                                </div>
                            </div>
                        </div>
                        <div class="entry-main">
                            <asp:Repeater ID="RepeaterPostDetail" runat="server" DataSource='<%# Eval("DataSourcePostDetail") %>'>
                                <ItemTemplate>

                                    <div class='<%# Eval("ClassSingleImage") %>'>
                                        <div class="col-xs-12">
                                            <div class="entry-media">
                                                <img src='<%# Eval("ImgaeSingleDefaultURL") %>' alt="foto" class="img-responsive" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class='<%# Eval("ClassTeks") %>'>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%# Eval("Konten") %>
                                              
                                            </div>
                                        </div>

                                    </div>

                                    <div class='<%# Eval("ClassSlider") %>'>
                                        <div class="col-xs-12">
                                            <div data-pagination="false" data-navigation="true" data-single-item="true" data-auto-play="7000" data-transition-style="fade" data-main-text-animation="true" data-after-init-delay="3000" data-after-move-delay="1000" data-stop-on-hover="true" class="post-carousel owl-carousel owl-theme owl-theme_mod-b enable-owl-carousel">
                                                <asp:Repeater ID="RepeaterPostDetailImage" runat="server" DataSource='<%# Eval("DataSourcePostDetailImage") %>'>
                                                    <ItemTemplate>
                                                        <img src='<%# Eval("ImgaeSliderDefaultURL") %>' alt="foto" class="img-responsive" />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--<div class="row">
                            <div class="col-md-8 col-md-offset-2">
                                <h2>Phasellus finibus consequat justo</h2>
                                <p>Accumsan blandit sem convallis a. Sed vitae euismod dolor. Cras non accumsan mi, non lacinia enim. Quisque porta et orci eget blandit. Nulla fermentum purus eleifend, egestas metus sit amet, pulvinar orci. Maecenas sit amet vestibulum dolor. In at augue justo. Integer nec iaculis tortor. Morbi quam justo, euismod in dolor in, maximus mattis nunc. Morbi sagittis laoreet ipsum.</p>
                                <p>Etiam magna lacus, facilisis eget quam id, semper bibendum mauris. Pellentesque vitae metus nec velit euismod ultricies ac lobortis dolor. Donec vehicula vulputate vulputate. Praesent varius nisi facilisis justo finibus, id interdum dui facilisis. Etiam scelerisque lacus magna, sed feugiat tellus dignissim eget. Duis posuere suscipit ante. Nunc vel fermentum mauris.</p>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <h4>Morbi finibus bibendum velit</h4>
                                        <p>Nulla rhoncus, elit bibendum elementum fermentum, lectus dolor aliquet lectus, sed scelerisque lectus nunc vitae leo. Suspendisse egestas purus nisi, et bibendum lacus faucibus dignissim. Suspendisse potenti.Ut in elementum odio. Phasellus vehicula nisl dui.</p>
                                    </div>
                                    <div class="col-sm-6">
                                        <h4>Etiam sodales lorem libero</h4>
                                        <p>Fusce finibus lorem vitae tempor scelerisque. Etiam ac diam dolor. Maecenas varius, nunc in pulvinar ullamcorper, enim erat commodo quam, et eleifend est risus in lorem. Integer lorem dui, semper ac tincidunt vulputate, ultrices ut lacus. Nulla facilisi.</p>
                                    </div>
                                </div>
                                <div class="post-border"></div>
                                <h2>Phasellus finibus consequat justo</h2>
                                <dl class="list-description">
                                    <dt>Nunc tincidunt aliquam</dt>
                                    <dd>Accumsan blandit sem convallis a. Sed vitae euismod dolor. Cras non accumsan mi, non lacinia enim. Quisque porta et orci eget blandit.</dd>
                                    <dt>Etiam sit amet dignissim</dt>
                                    <dd>Maecenas sit amet vestibulum dolor. In at augue justo. Integer nec iaculis tortor. Morbi quam justo, euismod in dolor in, maximus mattis nunc. Morbi sagittis laoreet ipsum.</dd>
                                    <dt>Nam vitae orci nulla</dt>
                                    <dd>Etiam sit amet dignissim felis. Nam vitae orci nulla. Proin sed mi aliquam, euismod massa eu, fermentum nisl. Quisque tincidunt faucibus blandit.</dd>
                                </dl>
                                <h2>Integer augue sem, suscipit vel ante</h2>
                                <p>Accumsan blandit sem convallis a. Sed vitae euismod dolor. Cras non accumsan mi, non lacinia enim. Quisque porta et orci eget blandit. Nulla fermentum purus eleifend, egestas metus sit amet, pulvinar orci. Maecenas sit amet vestibulum dolor. In at augue justo. Integer nec iaculis tortor. Morbi quam justo, euismod in dolor in, maximus mattis nunc. Morbi sagittis laoreet ipsum.</p>
                                <ul class="list list-mark-6">
                                    <li>Fusce nisi nunc, ornare id augue eu, tristique fermentum ;</li>
                                    <li>Maecenas rutrum quam ipsum, sed fermentum felis hendrerit vel;
                       
                                        <ul>
                                            <li>Morbi ac pharetra dolor;</li>
                                            <li>Sed in aliquet mi.</li>
                                        </ul>
                                    </li>
                                    <li>Vestibulum ultrices lacus mi, in congue ex bibendum vitae;</li>
                                    <li>In at augue justo. Integer nec iaculis tortor.</li>
                                </ul>
                                <p>Quisque porta et orci eget blandit. Nulla fermentum purus eleifend, egestas metus sit amet, pulvinar orci. Maecenas sit amet vestibulum dolor. In at augue justo. Integer nec iaculis tortor. Morbi quam justo, euismod in dolor in, maximus mattis nunc. Morbi sagittis laoreet ipsum.</p>
                                <div class="entry-footer">
                                    <div class="post-tags">
                                        <div class="entry-footer__title">tags:</div>
                                        <ul class="post-tags__list list-inline">
                                            <li><a href="http://templines.rocks/html/trendsetter/post-1.html">fashion</a></li>
                                            <li><a href="http://templines.rocks/html/trendsetter/post-1.html">image</a></li>
                                            <li><a href="http://templines.rocks/html/trendsetter/post-1.html">people</a></li>
                                            <li><a href="http://templines.rocks/html/trendsetter/post-1.html">clothes</a></li>
                                        </ul>
                                    </div>
                                    <div class="entry-footer__link">
                                        <div class="entry-footer__title">Share:</div>
                                        <ul class="social-net list-inline">
                                            <li class="social-net__item"><a href="http://templines.rocks/html/trendsetter/facebook.com" class="social-net__link"><i class="icon fa fa-facebook"></i></a></li>
                                            <li class="social-net__item"><a href="http://templines.rocks/html/trendsetter/twitter.com" class="social-net__link"><i class="icon fa fa-twitter"></i></a></li>
                                            <li class="social-net__item"><a href="http://templines.rocks/html/trendsetter/pinterest-p.com" class="social-net__link"><i class="icon fa fa-pinterest-p"></i></a></li>
                                            <li class="social-net__item"><a href="http://templines.rocks/html/trendsetter/vk.com" class="social-net__link"><i class="icon fa fa-vk"></i></a></li>
                                        </ul>
                                        <!-- end social-list-->
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </article>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

