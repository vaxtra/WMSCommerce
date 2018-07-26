<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div id="main-slider" data-slider-width="1920px" data-slider-height="480px" data-slider-arrows="false" data-slider-buttons="false" class="main-slider slider-pro">
        <div class="sp-slides">
            <asp:Repeater ID="RepeaterImage" runat="server">
                <ItemTemplate>
                    <div class="sp-slide">
                        <img src="<%# Eval("DefaultURL") %>" alt="slider" class="sp-image" />
                        <div class="container">
                            <div class="row">
                                <div class="col-xs-10 col-xs-offset-1">
                                    <%--  <div data-width="100%" data-show-transition="left" data-hide-transition="left" data-show-duration="800" data-show-delay="400" data-hide-delay="400" class="main-slider__label sp-layer"><%# Eval("Judul") %></div>
                                    <h2 data-width="100%" data-show-transition="left" data-hide-transition="left" data-show-duration="1200" data-show-delay="600" data-hide-delay="400" class="main-slider__title sp-layer"><%# Eval("Deskripsi") %></h2>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <!-- Slide 2-->

        </div>
    </div>
    <!-- end main-slider-->
    <div class="l-main-content">
        <h2 class="b-about-main__title text-center">Latest Product</h2>
        <div class="b-about-main__subtitle color-primary text-center"><a href="ProductList.aspx">View All Products</a> </div>
        <div class="b-goods-catalog">
            <asp:Repeater ID="RepeaterProduk" runat="server">
                <ItemTemplate>
                    <section class="b-goods b-goods_mod-a b-goods_5-col">
                        <div class="b-goods__inner">
                            <a href="<%# Eval("Foto") %>" class="b-goods__img js-zoom-images">
                                <img src="<%# Eval("Foto") %>" alt="goods" class="img-responsive" /></a>
                            <div class="b-goods__wrap">
                                <div class="b-goods__category"><%# Eval("Kategori") %></div>
                                <h3 class="b-goods__name"><%# Eval("Nama") %></h3>
                                <div class="b-goods__description"><%# Eval("Deskripsi") %></div>
                                <div class="b-goods__price"><%# Eval("Harga").ToFormatHarga() %></div>
                                <div class="b-goods-links"><a href="/Product.aspx?id=<%# Eval("IDProduk") %>" class="b-goods-links__item b-goods-links__item_main">View Detail</a></div>
                            </div>
                        </div>
                    </section>
                </ItemTemplate>
            </asp:Repeater>
        </div>


    </div>
    <div class="section-advantages-1">
        <h2 class="b-about-main__title text-center">The Blog</h2>
        <div class="b-about-main__subtitle color-primary text-center">Nam in tempus felis, eu eleifend tellus</div>
        <div class="row">

            <asp:Repeater ID="RepeaterPostHome" runat="server">
                <ItemTemplate>
                    <div class="col-sm-3">
                        <asp:Repeater ID="RepeaterPostDetail" runat="server" DataSource='<%# Eval("DataSourcePostDetail") %>'>
                            <ItemTemplate>
                                <div class='<%# Eval("ClassSingleImage") %>'>
                                    <img src="<%# Eval("ImgaeSingleDefaultURL") %>" alt="goods" class="img-responsive" />
                               

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <!-- end .b-advantages-->
                             <span class="b-advantages-1__title ui-title-inner text-center"><%# Eval("Judul") %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

    </div>

    <%-- <div class="section-area">

        <div class="block-table block-table_padd_20">
            <div class="block-table__cell col-md-4">
                <div class="block-table__inner bg-grey-2">
                    <section class="b-presentation b-presentation_sm">

                        <a href="./catalog-product.html">
                            <img src="/frontend/assets/media/components/b-presentation/bs-1.jpg" alt="goods" class="b-presentation__img scrollreveal" /></a>

                    </section>
                    <!-- end b-presentation-->
                </div>
            </div>
            <div class="block-table__cell col-md-4">
                <div class="block-table__inner bg-grey-2">
                    <section class="b-presentation b-presentation_sm">

                        <a href="./catalog-product.html">
                            <img src="/frontend/assets/media/components/b-presentation/bs-2.jpg" alt="goods" class="b-presentation__img scrollreveal" /></a>

                    </section>
                    <!-- end b-presentation-->
                </div>
            </div>
            <div class="block-table__cell col-md-4">
                <div class="block-table__inner bg-grey-2">
                    <section class="b-presentation b-presentation_sm">

                        <a href="./catalog-product.html">
                            <img src="/frontend/assets/media/components/b-presentation/bs-3.jpg" alt="goods" class="b-presentation__img scrollreveal" /></a>

                    </section>
                    <!-- end b-presentation-->
                </div>
            </div>
        </div>
    </div>--%>

    
   <%-- <section class="section-area">
        <div class="block-table block-table_padd_10 block-table_revers">
            <div class="block-table__cell col-md-6">
                <img src="/images/Konten/dsfg.jpg" alt="goods" class="b-presentation__img scrollreveal" />
            </div>
            <div class="block-table__cell col-md-6 text-center">
                <div class="block-table__inner bg-grey">

                    <div class="main-flex-box ">
                        <div class="flex-box-content">
                            <h2 class="ui-title-block ui-title-block_mod-a"><span class="text-gradient"><span class="shuffle">Women's Wear</span></span></h2>
                            <div class="ui-subtitle-block">Quisque nisi nisl accumsan vel odio</div>
                            <div data-min480="2" data-min768="4" data-min992="4" data-min1200="4" data-pagination="true" data-navigation="false" data-auto-play="4000" data-stop-on-hover="true" class="goods-carousel owl-carousel owl-theme enable-owl-carousel js-zoom-gallery">
                                <section class="b-goods">
                                    <div class="b-goods__inner">
                                        <a href="/frontend/assets/media/content/goods/womens/220x250/1.jpg" class="b-goods__img js-zoom-images">
                                            <img src="/frontend/assets/media/content/goods/womens/220x250/1.jpg" alt="goods" class="img-responsive" /></a>
                                        <div class="b-goods__category">category</div>
                                        <h3 class="b-goods__name">Pandora</h3>
                                        <div class="b-goods__price">$70.50</div>
                                    </div>
                                </section>
                                <!-- end b-goods-->

                                <section class="b-goods">
                                    <div class="b-goods__inner">
                                        <a href="/frontend/assets/media/content/goods/womens/220x250/2.jpg" class="b-goods__img js-zoom-images">
                                            <img src="/frontend/assets/media/content/goods/womens/220x250/2.jpg" alt="goods" class="img-responsive" /></a>
                                        <div class="b-goods__category">category</div>
                                        <h3 class="b-goods__name">Mundilfari</h3>
                                        <div class="b-goods__price">$220.00</div>
                                        <div class="b-goods__label bg-primary">new</div>
                                    </div>
                                </section>
                                <!-- end b-goods-->

                                <section class="b-goods">
                                    <div class="b-goods__inner">
                                        <a href="/frontend/assets/media/content/goods/womens/220x250/1.jpg" class="b-goods__img js-zoom-images">
                                            <img src="/frontend/assets/media/content/goods/womens/220x250/1.jpg" alt="goods" class="img-responsive" /></a>
                                        <div class="b-goods__category">category</div>
                                        <h3 class="b-goods__name">Pandora</h3>
                                        <div class="b-goods__price">$70.50</div>
                                    </div>
                                </section>
                                <!-- end b-goods-->

                                <section class="b-goods">
                                    <div class="b-goods__inner">
                                        <a href="/frontend/assets/media/content/goods/womens/220x250/2.jpg" class="b-goods__img js-zoom-images">
                                            <img src="/frontend/assets/media/content/goods/womens/220x250/2.jpg" alt="goods" class="img-responsive" /></a>
                                        <div class="b-goods__category">category</div>
                                        <h3 class="b-goods__name">Mundilfari</h3>
                                        <div class="b-goods__price">$220.00</div>
                                    </div>
                                </section>
                                <!-- end b-goods-->

                            </div>
                            <a href="./catalog-list.html" class="btn-link b-goods-carousel__btn-link">view all</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>--%>

    <%--  <div class="b-gallery js-zoom-gallery grid b-isotope clearfix">
          <div class="b-isotope__grid">
            <div class="grid-sizer"></div><a href="/frontend/assets/media/components/b-gallery/169x169_1.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_1.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/348x348_1.jpg" class="b-gallery__item grid-item grid-item_wx2 grid-item_hx2 js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/348x348_1.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_3.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_3.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_4.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_4.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_5.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_5.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_6.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_6.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/348x348_2.jpg" class="b-gallery__item grid-item grid-item_wx2 grid-item_hx2 js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/348x348_2.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_7.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_7.jpg" alt="foto" /></div></a>
            <div class="b-gallery__item grid-item grid-item_wx4 grid-item_hx2">
              <div class="b-gallery__main bg-grey"><i class="b-gallery__icon fa fa-instagram color-primary"></i>
                <div class="b-gallery__title">@trendsetter</div>
                <div class="b-gallery__info color-primary">Aenean feugiat libero ligula, eget cursus ipsum laoreet.</div>
              </div>
            </div><a href="/frontend/assets/media/components/b-gallery/169x169_2.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_2.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_8.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_8.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/348x348_3.jpg" class="b-gallery__item grid-item grid-item_wx2 grid-item_hx2 js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/348x348_3.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_9.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_9.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_15.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_15.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/348x348_4.jpg" class="b-gallery__item grid-item grid-item_wx2 grid-item_hx2 js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/348x348_4.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_10.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_10.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_11.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_11.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_12.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_12.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_13.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_13.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_14.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_14.jpg" alt="foto" /></div></a><a href="./assets/media/components/b-gallery/169x169_16.jpg" class="b-gallery__item grid-item js-zoom-gallery__item">
              <div class="b-gallery__inner"><img src="/frontend/assets/media/components/b-gallery/169x169_16.jpg" alt="foto" /></div></a>
          </div>
        </div>--%>
</asp:Content>
