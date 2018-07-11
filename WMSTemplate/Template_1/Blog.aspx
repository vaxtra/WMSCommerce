<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="BlogDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="wrap-content">
        <div class="text-center">
            <ol class="breadcrumb">
                <li><a href="./home.html">Home</a></li>
                <li><a href="./home.html">Catalog</a></li>
                <li><a href="./home.html">Clothong</a></li>
                <li class="active">raincoats</li>
            </ol>
        </div>
        <div class="container">
            <article class="b-post b-post-full-2 clearfix">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="entry-header">
                            <div class="entry-category bg-primary">Category</div>
                            <h2 class="entry-title">Ut tincidunt purus sem,
                                <br />
                                in scelerisque</h2>
                            <div class="entry-meta">
                                <a href="./blog-post.html" class="entry-meta__date">24 Sep 2017</a>
                                <div class="entry-meta__inner"><span class="entry-meta__item"><i class="entry-meta__icon color-primary fa fa-heart-o"></i><a href="./blog-post.html" class="entry-meta__link">18</a></span><span class="entry-meta__item"><i class="entry-meta__icon color-primary fa fa-comment-o"></i><a href="./blog-post.html" class="entry-meta__link">3</a></span></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="entry-main">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="entry-media">
                                <img src="/frontend/assets/media/content/posts/carousel/img-lg-1.jpg" alt="foto" class="img-responsive" /></div>
                        </div>
                    </div>
                    <div class="entry-content">
                        <div class="row">
                            <div class="col-md-8 col-md-offset-2">
                                <h2 class="entry-subtitle">Proin sed felis sollicitudin, fermentum lectus non, pellentesque ipsum. Vivamus bibendum mollis magna eget tempus. Nunc ultrices, magna vel tristique venenatis.</h2>
                                <p>Proin sed felis sollicitudin, fermentum lectus non, pellentesque ipsum. Vivamus bibendum mollis magna eget tempus. Nunc ultrices, magna vel tristique venenatis, lacus dui fringilla mi, rhoncus molestie ipsum nibh vel nulla. Mauris scelerisque vestibulum molestie. Aenean vitae orci non risus ornare ultrices faucibus in enim. Duis a porta nisl, eu dictum massa. Praesent tristique mi eget lorem porta imperdiet. Nam condimentum tellus in sollicitudin vulputate. Pellentesque lacinia nulla ut tortor malesuada pretium. Curabitur sed scelerisque ex. Maecenas quis ex nibh. Sed tincidunt eu justo et egestas. Sed interdum viverra elit a hendrerit.</p>
                            </div>
                        </div>
                        <div class="b-post-foto">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="foto-description">
                                        <div class="icon fa fa-camera color-primary"></div>
                                        <div class="foto-description__author">Mauris scelerisque</div>
                                        <div class="foto-description__info">Sed tincidunt eu justo et egestas. Sed interdum viverra elit a hendrerit. Sed ac ante lacinia, malesuada ex eget.</div>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <img src="/frontend/assets/media/content/posts/780x400/1.jpg" alt="foto" class="img-responsive" /></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 col-md-offset-2">
                                <p>Aenean efficitur sapien id interdum gravida. Fusce nec purus dolor. Nullam id euismod lorem. Phasellus finibus consequat justo, accumsan blandit sem convallis a. Sed vitae euismod dolor. Cras non accumsan mi, non lacinia enim. Quisque porta et orci eget blandit. Nulla fermentum purus eleifend, egestas metus sit amet, pulvinar orci. Maecenas sit amet vestibulum dolor. In at augue justo. Integer nec iaculis tortor. Morbi quam justo, euismod in dolor in, maximus mattis nunc. Morbi sagittis laoreet ipsum.</p>
                                <p>Proin felis elit, egestas at faucibus eu, tincidunt eu massa. Praesent ex dolor, commodo id dictum id, sollicitudin eu neque. Mauris massa urna, pellentesque nec interdum nec, faucibus ut tortor. Curabitur posuere iaculis nunc, vitae bibendum nisi pharetra ac. Vestibulum ullamcorper ut odio quis tristique.</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div data-pagination="false" data-navigation="true" data-single-item="true" data-auto-play="7000" data-transition-style="fade" data-main-text-animation="true" data-after-init-delay="3000" data-after-move-delay="1000" data-stop-on-hover="true" class="post-carousel owl-carousel owl-theme owl-theme_mod-b enable-owl-carousel">
                                    <img src="/frontend/assets/media/content/posts/carousel/img-lg-2.jpg" alt="foto" class="img-responsive" /><img src="/frontend/assets/media/content/posts/carousel/img-lg-1.jpg" alt="foto" class="img-responsive" /></div>
                            </div>
                        </div>
                        <div class="row">
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
                        </div>
                    </div>
                </div>
            </article>
        </div>
        <div class="section-type-2 bg-grey">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12">
                        <h2 class="ui-title-block-3">Related Posts</h2>
                        <div data-min480="2" data-min768="3" data-min992="3" data-min1200="3" data-pagination="false" data-navigation="true" data-auto-play="4000" data-stop-on-hover="true" class="social-carousel owl-carousel owl-theme owl-theme_mod-a owl-theme_mrg-top enable-owl-carousel js-zoom-gallery">
                            <article class="b-post b-post-2 b-post-2_mod-a clearfix">
                                <div class="entry-media">
                                    <a href="/frontend/assets/media/content/posts/blog-cols/8.jpg" class="js-zoom-images">
                                        <img src="/frontend/assets/media/content/posts/blog-cols/8.jpg" alt="Foto" class="img-responsive" /></a>
                                    <div class="entry-category">Category</div>
                                </div>
                                <div class="entry-main">
                                    <div class="entry-header">
                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Curabitur tellus dolor, sagittis</a></h2>
                                    </div>
                                    <div class="entry-content">
                                        <p>Proin eu urna nisl. Suspendisse potenti. Suspendisse potenti. In in rhoncus metus, ut lobortis diam.</p>
                                    </div>
                                    <div class="entry-footer">
                                        <div class="entry-meta">
                                            <a href="./blog-post.html" class="entry-meta__date">24 Sep 2017</a>
                                            <div class="entry-meta__inner"><span class="entry-meta__item"><i class="entry-meta__icon color-primary fa fa-heart-o"></i><a href="./blog-post.html" class="entry-meta__link">18</a></span><span class="entry-meta__item"><i class="entry-meta__icon color-primary fa fa-comment-o"></i><a href="./blog-post.html" class="entry-meta__link">3</a></span></div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                            <!-- end post-->


                            <article class="b-post b-post-2 b-post-2_mod-a clearfix">
                                <div class="entry-media">
                                    <a href="/frontend/assets/media/content/posts/blog-cols/7.jpg" class="js-zoom-images">
                                        <img src="/frontend/assets/media/content/posts/blog-cols/7.jpg" alt="Foto" class="img-responsive" /></a>
                                    <div class="entry-category">Category</div>
                                </div>
                                <div class="entry-main">
                                    <div class="entry-header">
                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Aliquam ultrices pharetra cursus</a></h2>
                                    </div>
                                    <div class="entry-content">
                                        <p>Donec lorem felis, auctor id posuere quis, mattis euismod mauris. Phasellus ut dolor leo. Integer ac arcu est. Aliquam hendrerit metus</p>
                                    </div>
                                    <div class="entry-footer">
                                        <div class="entry-meta">
                                            <a href="./blog-post.html" class="entry-meta__date">24 Sep 2017</a>
                                            <div class="entry-meta__inner"><span class="entry-meta__item"><i class="entry-meta__icon color-primary fa fa-heart-o"></i><a href="./blog-post.html" class="entry-meta__link">18</a></span><span class="entry-meta__item"><i class="entry-meta__icon color-primary fa fa-comment-o"></i><a href="./blog-post.html" class="entry-meta__link">3</a></span></div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                            <!-- end post-->


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

