<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="goods-card">
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
                <div class="b-goods-carousel">
                    <div class="col-md-1">
                        <div id="bx-pager" class="b-goods-carousel__thumb">
                            <a data-slide-index="0" href="./catalog-product-2.html" class="b-goods-carousel__thumb-link">
                                <img src="/frontend/assets/media/content/goods-carousel/thumbnails/1.jpg" alt="foto" class="b-goods-carousel__thumb-img" /></a>
                            <a data-slide-index="1" href="./catalog-product-2.html" class="b-goods-carousel__thumb-link">
                                <img src="/frontend/assets/media/content/goods-carousel/thumbnails/2.jpg" alt="foto" class="b-goods-carousel__thumb-img" /></a>
                            <a data-slide-index="2" href="./catalog-product-2.html" class="b-goods-carousel__thumb-link">
                                <img src="/frontend/assets/media/content/goods-carousel/thumbnails/3.jpg" alt="foto" class="b-goods-carousel__thumb-img" /></a>
                            <a data-slide-index="3" href="./catalog-product-2.html" class="b-goods-carousel__thumb-link">
                                <img src="/frontend/assets/media/content/goods-carousel/thumbnails/4.jpg" alt="foto" class="b-goods-carousel__thumb-img" /></a>
                            <a data-slide-index="4" href="./catalog-product-2.html" class="b-goods-carousel__thumb-link">
                                <img src="/frontend/assets/media/content/goods-carousel/thumbnails/5.jpg" alt="foto" class="b-goods-carousel__thumb-img" /></a>

                        </div>
                    </div>
                    <div class="col-md-6">
                        <ul class="b-goods-carousel__main-img bxslider">
                            <li>
                                <img src="/frontend/assets/media/content/goods-carousel/main/1.jpg" alt="foto" /></li>
                            <li>
                                <img src="/frontend/assets/media/content/goods-carousel/main/1.jpg" alt="foto" /></li>
                            <li>
                                <img src="/frontend/assets/media/content/goods-carousel/main/1.jpg" alt="foto" /></li>
                            <li>
                                <img src="/frontend/assets/media/content/goods-carousel/main/1.jpg" alt="foto" /></li>
                            <li>
                                <img src="/frontend/assets/media/content/goods-carousel/main/1.jpg" alt="foto" /></li>
                        </ul>
                    </div>
                    <!-- end b-goods-carousel-->


                    <div class="col-lg-4 col-lg-offset-1 col-md-5">
                        <section class="b-goods-3 b-goods-3_mod-a">
                            <div class="b-goods-3__category">Jackets</div>
                            <h3 class="b-goods-3__name">Hyperion</h3>
                            <div class="b-goods-3__price color-primary">$85.99</div>
                            <div class="b-goods-3__price-old">$130.00</div>
                            <div class="b-goods-3__label bg-secondary">sale</div>
                            <div class="b-goods-3__description">Donec id massa ut nisl auctor mollis eu nec magna. Duis mattis congue lacus ac elementum.</div>
                        </section>
                        <!-- end b-goods-->

                        <div class="form-goods form-goods_color_light form-horizontal" />

                        <div class="form-group">
                            <div class="col-md-8">
                                <select data-width="100%" class="selectpicker">
                                    <option selected="selected">-- Choose Size --</option>
                                    <option>46</option>
                                    <option>48</option>
                                    <option>50</option>
                                    <option>52</option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <div class="enumerator">
                                    <span class="enumerator__btn js-minus_btn">-</span>
                                    <input type="text" value="1" class="enumerator__input" /><span class="enumerator__btn js-plus_btn">+</span>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button class="btn btn-primary btn-effect" ID="ButtonAddToCart" runat="server" Text="add to cart" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="b-goods-tab">
        <!-- Nav tabs-->
        <ul class="b-goods-tab-nav nav nav-tabs">
            <li class="active"><a href="#related" data-toggle="tab">RELATED PRODUCTS</a></li>
        </ul>
        <!-- Tab panes-->
        <div class="b-goods-tab-content tab-content">
            <div id="related" class="tab-pane fade in active">
                <div class="container">
                    <div class="tab-group-goods">
                        <div class="js-scroll-content">
                            <div class="row">
                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/1.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/1.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Epimetheus</h3>
                                            <div class="b-goods__price-old">$480.00</div>
                                            <div class="b-goods__price">$450</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->
                                </div>

                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/2.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/2.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Enceladus</h3>
                                            <div class="b-goods__price-old">$180.00</div>
                                            <div class="b-goods__price">$160.00</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->
                                </div>

                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/3.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/3.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Hyperion</h3>
                                            <div class="b-goods__price">$120.00</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->
                                </div>

                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/4.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/4.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Iapetus</h3>
                                            <div class="b-goods__price">$250.00</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->
                                </div>
                            </div>
                        </div>
                        <span class="btn-scroll-next btn btn-default btn-effect center-block js-scroll-next"><i class="icon fa fa-arrow-circle-down color-primary"></i>Load more</span>
                        <div class="js-scroll-content">
                            <div class="row">
                                <div class="col-md-3">
                                    <section class="b-goods">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/1.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/1.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Epimetheus</h3>
                                            <div class="b-goods__price">$450</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->

                                </div>
                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/2.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/2.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Enceladus</h3>
                                            <div class="b-goods__price">$160.00</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->

                                </div>
                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/3.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/3.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Hyperion</h3>
                                            <div class="b-goods__price">$120.00</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->

                                </div>
                                <div class="col-md-3">
                                    <section class="b-goods b-goods_mod-b">
                                        <div class="b-goods__inner">
                                            <a href="/frontend/assets/media/content/goods/populars/280x320/4.jpg" class="b-goods__img js-zoom-images">
                                                <img src="/frontend/assets/media/content/goods/populars/280x320/4.jpg" alt="goods" class="img-responsive" /></a>
                                            <div class="b-goods__category">category</div>
                                            <h3 class="b-goods__name">Iapetus</h3>
                                            <div class="b-goods__price">$250.00</div>
                                        </div>
                                    </section>
                                    <!-- end b-goods-->
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
    <script src="/frontend/assets/plugins/bxslider/vendor/jquery.easing.1.3.js"></script>
    <script src="/frontend/assets/plugins/bxslider/vendor/jquery.fitvids.js"></script>
    <script src="/frontend/assets/plugins/bxslider/jquery.bxslider.min.js"></script>
</asp:Content>
