$(document).ready(function () {
    //显示Class2
    getClass2();
    $("#class1").change(function () {
        getClass2();
    });
    function getClass2() {
        var class2 = [["文娱", "财经", "体育", "创意", "其他"], ["宣讲会", "财经", "文化素质", "分享", "其他"]];
        var index = $("#class1")[0].selectedIndex;
        var class2Selector = $("#class2")[0];
        var selector = class2[0];
        $("#class2").show();
        if (index == 0) {
            selector = class2[0];
            for (var i = 0; i < selector.length; i++) {
                class2Selector[i] = new Option(selector[i], selector[i]);
            }
        }
        else if (index == 1) {
            selector = class2[1];
            for (var i = 0; i < selector.length; i++) {
                class2Selector[i] = new Option(selector[i], selector[i]);
            }
        } else {
            $("#class2").hide();
        }
    }
});