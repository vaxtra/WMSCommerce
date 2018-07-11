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
                    <ul class="b-isotope__filter list-inline">
                        <li><a href="./blog-main-2.html" data-filter="*" class="current">ALL</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".fashion">Fashion</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".lifestyle">lifestyle</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".travel">travel</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".trends">Trends</a></li>
                        <li><a href="./blog-main-2.html" data-filter=".gadgets">Gadgets</a></li>
                    </ul>
                    <div class="container">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="js-zoom-gallery grid">
                                    <div class="b-isotope__grid">
                                        <div class="grid-sizer"></div>
                                        <div class="b-gallery-5__item grid-item lifestyle trends">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/1.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/1.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Donec diam nulla, condimentum</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Phasellus justo ligula, dictum sit amet tortor eu, iaculis tristique turpis. Mauris non orci sed est suscipit tempor ut quis felis. </p>
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
                                        <div class="b-gallery-5__item grid-item fashion">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/4.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/4.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Morbi posuere dapibus</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Curabitur sed augue eu quam hendrerit semper ac quis purus. Maecenas tempus pretium ante non mollis.</p>
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
                                        <div class="b-gallery-5__item grid-item travel gadgets">
                                            <article class="b-post b-post-2 clearfix">
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
                                        <div class="b-gallery-5__item grid-item trends">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/2.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/2.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Fusce feugiat viverra sem</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Suspendisse ut lacus ligula. Nulla vitae sodales leo, vel congue massa. Quisque pellentesque, diam sed vestibulum auctor</p>
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
                                        <div class="b-gallery-5__item grid-item fashion travel">
                                            <article class="b-post b-post-2 clearfix">
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


                                        </div>
                                        <div class="b-gallery-5__item grid-item lifestyle trends gadgets">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/5.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/5.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Maecenas eget odio dictum</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Sed varius elementum eleifend. Nullam nec facilisis risus. Nam placerat tortor sed pellentesque vestibulum.</p>
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
                                        <div class="b-gallery-5__item grid-item fashion lifestyle">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/9.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/9.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Proin pretium, libero et venenatis</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Aliquam a odio sagittis sapien lobortis ultrices id et augue. Fusce in purus tempus orci convallis tincidunt. Aliquam erat volutpat.</p>
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
                                        <div class="b-gallery-5__item grid-item travel gadgets">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/3.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/3.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Maecenas viverra</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Phasellus non sapien aliquet, sagittis odio at, volutpat dui. Praesent convallis tempor dapibus. Nulla nec mi non elit mattis maximus.</p>
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
                                        <div class="b-gallery-5__item grid-item lifestyle gadgets">
                                            <article class="b-post b-post-2 clearfix">
                                                <div class="entry-media">
                                                    <a href="/frontend/assets/media/content/posts/blog-cols/6.jpg" class="js-zoom-images">
                                                        <img src="/frontend/assets/media/content/posts/blog-cols/6.jpg" alt="Foto" class="img-responsive" /></a>
                                                    <div class="entry-category">Category</div>
                                                </div>
                                                <div class="entry-main">
                                                    <div class="entry-header">
                                                        <h2 class="entry-title entry-title_spacing ui-title-inner"><a href="./blog-post.html">Nam tempus augue sed</a></h2>
                                                    </div>
                                                    <div class="entry-content">
                                                        <p>Nulla non quam eu sem dignissim lobortis sed quis ante. Integer at massa cursus massa suscipit posuere.</p>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
