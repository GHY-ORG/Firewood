$(document).ready(function () {
    $(".index-main-block").each(function () {
        var $p = $("p", $(this)).eq(0);
        while ($p.outerHeight() > 80) {
            $p.text($p.text().replace(/(\s)*([a-zA-Z0-9]+|\W)(\.\.\.)?$/, "..."));
        };
    });
});