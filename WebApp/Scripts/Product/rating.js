//$(function () {

$.wait = function (ms) {
    var defer = $.Deferred();
    setTimeout(function () {
        defer.resolve();
    }, ms);
    return defer;
};

var $ratedNums = $('#rated-nums'),
    $ratingBlk = $('#rating-blk'),
    $btnDoRate = $('#btn-do-rate'),
    $btnRatingClose = $('#btn-rating-close'),
    $ratingForm = $('#ratingForm'),
    $starInput = $('#starInput'),
    defaultRatedLv;
$btnDoRate.on('click', doProductRate);
$btnRatingClose.on('click', cancelProductRate);
function doProductRate(e) {
    e.preventDefault();
    var _self = $(this);
    var _rateLv;
    if (_self.hasClass('disabled')) {
        return;
    }
    if (!$ratingBlk.hasClass('do-rate')) {
        alert('no submit');
        defaultRatedLv = $ratingBlk.data('rated');
        _self.html('送出');
        $ratingBlk.addClass('do-rate');
        $ratingBlk.find('.heart').addClass('narrow').on('click', setRatingLv);
        $.wait(400).then(function () {
            $ratingBlk.attr('data-rated', '0').data('rated', 0).find('.heart').removeClass('narrow');
            $btnRatingClose.delay(300).fadeIn(300);
        })
        //$ratedNums.fadeOut();
    } else {
        _rateLv = $ratingBlk.data('rated');
        if (_rateLv === 0) {
            $ratingBlk.find('.heart').addClass('shake');
            $.wait(1000).then(function () {
                $ratingBlk.find('.heart').removeClass('shake')
            })
            return;
        }
        $starInput.val(_rateLv);
        $ratingForm.submit();

    }
}
function resetRatingLv(resp) {
    console.log(resp);
    var _rateLv = resp.Rating;
    $btnRatingClose.fadeOut();
    $btnDoRate.html('已評等').addClass('disabled');
    $ratingBlk.find('.heart').addClass('narrow').off('click', setRatingLv);
    $.wait(350).then(function () {
        $ratingBlk.removeClass('do-rate');
        $ratingBlk.attr('data-rated', _rateLv).data('rated', _rateLv).find('.heart').removeClass('narrow');
    })
}
function cancelProductRate() {
    $btnRatingClose.fadeOut();
    $btnDoRate.html('進行評等');
    $ratingBlk.find('.heart').addClass('narrow').off('click', setRatingLv);
    $.wait(350).then(function () {
        $ratingBlk.removeClass('do-rate');
        $ratingBlk.attr('data-rated', defaultRatedLv).data('rated', defaultRatedLv).find('.heart').removeClass('narrow');
        //$ratedNums.fadeIn();
    })
}
function setRatingLv() {
    var _lv = $(this).index() + 1;
    $ratingBlk.attr('data-rated', _lv).data('rated', _lv);
}
function ajaxError(xhr) {
    console.log(xhr);
    return;
}



//})