$(document).ready(function () {
    //显示菜单栏
    $(".user-btn,#nav-on").mouseover(function (event) {
        $("#nav-on").show();
    }).mouseout(function (event) {
        $("#nav-on").hide();
    });
});