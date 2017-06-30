$(document).ready(function () {
    var likeSpan = $(".like-sign");
    likeSpan.on("click",
        (e) => {
            var id = e.target.id;
            $.ajax({
                type: "POST",
                url: "/api/Sitecore/Article/UpdateArticleLikes",
                data: { articleId: id },
                success: (data) => {
                    if (data.success && data.likes !== undefined)
                        $(e.target).text(data.likes);
                }
                //dataType: dataType
            });
        });
});