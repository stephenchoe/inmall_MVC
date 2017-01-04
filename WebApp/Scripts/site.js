$(function () {
    var isMobile = /Mobile|iP(hone|od)|Android|BlackBerry|IEMobile/.test(navigator.userAgent),
    isIos = /iP(hone|ad|od)/.test(navigator.userAgent);
    $.wait = function (ms) {
        var defer = $.Deferred();
        setTimeout(function () {
            defer.resolve();
        }, ms);
        return defer;
    };
    var $win = $(window);
    var $lazy = $('img.lazy');
    if ($lazy.length > 0 && typeof ($lazy.lazyload) === 'function') {
        $lazy.lazyload({
            effect: "fadeIn",
            load: function () {
                var _self = $(this);
                if (_self.hasClass('js-ratio-4-3')) {
                    _self.height(_self.width() * .75);
                }
            }
        });
    }
    $('#btn-open-menu').on('click', toggleMainNav);
    $('.main-nav-container').on('click', closeMainNav);

    function toggleMainNav() {
        $(this).toggleClass('close');
        $('.main-nav-container').toggleClass('is-open');
        return false;
    }

    function closeMainNav(e) {
        if (e.target.className.indexOf('main-nav-container') != -1) {
            $('.main-nav-container').removeClass('is-open');
            $('#btn-open-menu').removeClass('close');
        }
    }
    //$('#index-slider').flexslider({
    //    animation: "slide",
    //    slideshow: true,
    //    start: function (slider) {
    //        $.flexloader(slider);
    //    },
    //    after: function (slider) {
    //        $.flexloader(slider);
    //    }
    //});

    var $btnSearch = $('#btn-search');
    var $btnMember = $('.mobile #btn-member');
    var $productSearchBlk = $('#product-search-blk');
    var $memberFuncBlk = $('#member-func-blk');

    $btnSearch.on('click', toggleSearchBlk);
    $btnMember.on('click', toggleMemberBlk);
    $productSearchBlk.on('click', closeSearchBlk);
    $memberFuncBlk.on('click', closeSearchBlk);

    function toggleSearchBlk() {
        $btnSearch.toggleClass('act');
        $productSearchBlk.slideToggle(300);
        document.querySelector('#product-search-ipt').focus();
        $memberFuncBlk.stop().slideUp(300);
        $btnMember.removeClass('act');
    }

    function toggleMemberBlk(e) {
        //if ($('html').hasClass('mobile')&&e.type==="click") {
        $btnMember.toggleClass('act');
        $memberFuncBlk.slideToggle(300);
        $productSearchBlk.stop().slideUp(300);
        $btnSearch.removeClass('act');
        //}
        /*if ($('html').hasClass('pc')&&e.type==="mouseover") {
            $memberFuncBlk.slideDown(300);
            $btnMember.addClass('act').on('mouseout',function(){
                $memberFuncBlk.on('mouseover',function(){
                    //$memberFuncBlk.show();
                    return;
                })
                $memberFuncBlk.slideUp(300);
            });
            
        }*/
    }

    function closeSearchBlk(e) {
        if (e.target.className == 'product-search-blk') {
            $productSearchBlk.stop().slideUp(300);
            $btnSearch.removeClass('act');
            $memberFuncBlk.stop().slideUp(300);
            $btnMember.removeClass('act');
        }
    }
    //$('#twzipcode, .twzipcode').twzipcode();
    //$('#btn-refresh-captcha').on('click', refreshCaptcha);

    
    //$('#product-slider').flexslider({
    //    slideshow: false,
    //    animation: "slide",
    //    directionNav: true,
    //    controlNav: true,
    //    start: function (slider) {
    //        $.flexloader(slider);
    //    },
    //    after: function (slider) {
    //        $.flexloader(slider);
    //    }
    //});

    //anchor scroll
    $('.js-scroll-anchor').on('click', scrollToAnchor);

    function scrollToAnchor() {
        $('html,body').animate({
            scrollTop: $($(this).attr('href')).offset().top
        }, 100);
        return false;
    }

    //switch-2-desktop
    /*$('#btn-switch-desktop').on('click', switch2Desktop);

    function switch2Desktop(argument) {
        setCookie("unmobile", 1, 1);
    }

    function setCookie(c_name, value, expiredays) {
        var exdate = new Date()
        exdate.setDate(exdate.getDate() + expiredays)
        document.cookie = c_name + "=" + escape(value) +
            ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
    }*/

    //圖片比例4:3
    $win.on('resize', ratio4to3);
    ratio4to3();

    function ratio4to3() {
        $('.js-ratio-4-3').each(function () {
            $(this).height($(this).width() * .75);
        });
    }

    //產品說明accordion
    $('.mobile .js-btn-accordion').on("click", toggleAccordion);

    function toggleAccordion() {
        var _self = $(this);
        _self.toggleClass('act')
            .siblings(".js-content-accordion").slideToggle(300);
    }

    //pc下轉換number input
    if ($("html").hasClass('win')) {
        $('.js-shopping-item-num-ipt').each(function (idx, val) {
            var _self = $(this);
            _self[0].setAttribute('type', 'text');
        })
    }

    //記憶登入前的頁面
    $('#btn-member-login').on('click', memoryCurrentUrlBeforeLogin);

    function memoryCurrentUrlBeforeLogin() {
        if (window.localStorage) {
            var _url = location.href;
            if (_url != "/Member/SignIn") {
                localStorage.setItem("url_before_login", _url);
            } else {
                localStorage.setItem("url_before_login", "/");
            }
        }
    }

    //desktop下選單展開
    var $mainNav;
    var $breadcrumb = $('.breadcrumb');
    var lv1, lv2;
    if (!isMobile) {
        $mainNav = $('#main-nav');
        $mainNav.find('.has-more').on('click', toggleSubMenu);
        if ($breadcrumb.length > 0) {
            lv1 = $breadcrumb.find('.unit').eq(1).html();
            lv2 = $breadcrumb.find('.unit').eq(2).html();
            lv3 = $('.category-title-blk .title').html();
            if (lv1) {
                $mainNav.find('.nav-title:contains(' + lv1 + ')').click();
            }
            if (lv2) {
                $mainNav.find('.link:contains(' + lv2 + ')').addClass('act');
            }
            if (lv3) {
                $mainNav.find('.link:contains(' + lv3 + ')').addClass('act');
            }
        }
    }
    function toggleSubMenu() {
        var _self = $(this);
        if (_self.hasClass('act')) return;
        $mainNav.find('.act').removeClass('act');
        _self.addClass('act');
        return false;
    }


})


function GetObjById(id) {
    return $('#' + id);
}

function Loading(id) {
    if (id == '') {
        $('body').loading();
    } else {
        $('#' + id).loading({});
    }
    
}
function StopLoading(id) {
    
    var key ='body';
    if (id != '') {
        key ='#' + id ;
    }
    var loadingDiv = $(key + '_loading-overlay');
    if (loadingDiv) {
        loadingDiv.fadeOut(300, function () {
            $(this).remove()
        });
    }
   
}

function ShowAlert(type, title, text, confirm) {
    
    var settings = {
        type: type,
        title:title,
        text: text,
      
        showConfirmButton: confirm
    };

    if (!confirm) {
        settings.timer = 1500;
    }

    swal(settings);
}

function ConfirmAlert(type, title, text, cancel) {
    
    var settings = {
        type: type,
        title: title,
        text: text,
       
        showConfirmButton: true,
        showCancelButton: cancel,
    };

    if (!confirm) {
        settings.timer = 1500;
    }

    swal(settings, function (isConfirm) {
        if (isConfirm) {
            swal("Deleted!", "Your imaginary file has been deleted.", "success");
        } else {
            swal("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });
}


function ShowConfirm(urlString) {
    var divConfirm = $("#divConfirmAlert");
    divConfirm.empty();

    $.ajax({
        type: "GET",
        cache: false,
        url: urlString,
        dataType: 'html',
        async: true,
        success: function (data) {
            divConfirm.html(data);
        },
        error: function () {
            alert('載入失敗!');
        }
    });

}

function HideConfirm() {
    $('#myModal').modal('hide');
}

function IsNullorEmpty(str) {
    return !str || $.trim(str) === "";
}


function ErrorHtml(msg) {
    var htmlString = "<h5 class='text-danger'>";
    htmlString += "<span class='glyphicon glyphicon-exclamation-sign'></span>";
    htmlString += msg;
    htmlString += "</h5>";

    return htmlString;
}



function SetValidatorErrorStyle(id) {

    var form = GetObjById(id);
    var formData = $.data(form[0]);
    var settings = formData.validator.settings;
    var oldErrorPlacement = settings.errorPlacement;
    var oldSuccess = settings.success;

    settings.errorPlacement = function (label, element) {

        // Call old handler so it can update the HTML
        oldErrorPlacement(label, element);

        label.parents('.flex-ipt-blk').each(function () {
            $(this).addClass('err');
            $(this).find('input').addClass('err');
        });
    };

    settings.success = function (label) {
        // Remove error class from <div class="form-group">, but don't worry about
        // validation error messages as the plugin is going to remove it anyway
        label.parents('.form-group').removeClass('has-error');

        // Call old handler to do rest of the work
        oldSuccess(label);
    };


}

function RecaptchaCallback(data) {
   
    var inputErrMsg = $('#recaptchaErrMsg');
    if (inputErrMsg) {
        inputErrMsg.text('');       
    } 
    
}

