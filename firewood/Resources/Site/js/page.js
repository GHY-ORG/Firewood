$(function () {
    //分页
    $pageli = $('.pageli');
    var n = window.location.href.substr(7).split('/');
    if (n.length == 9) { $('.pagination').hide(); }
    var pagenow = n[6];//当前页
    var pagesum = $pageli.length;//总页数
    $pageli.parent().removeClass('active');
    $("#pageli" + pagenow).parent().addClass('active');
    if (pagenow == 1) {
        $('#pre').parent().addClass('disabled');
        $('#pre').attr('href', '');
    } else {
        $('#pre').parent().removeClass('disabled');
        $('#pre').attr('href', $('#pre').attr('href') + (Number(pagenow) - 1));
    }
    if (pagenow == pagesum) {
        $('#next').parent().addClass('disabled');
        $('#next').attr('href', '');
    } else {
        $('#next').parent().removeClass('disabled');
        $('#next').attr('href', $('#next').attr('href') + (Number(pagenow) + 1));
    }
});