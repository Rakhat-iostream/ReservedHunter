function getArticles() {
    $.get(
        '/articles'
    ).done(function (data) {
        let art = data;
        for (let i = 0; i < art.length; i++) {
            $('div.articles').append('<article style="margin-bottom: 30px;">' +
                '<img src="https://mobile-review.com/news/wp-content/uploads/Microsoft-Logo-2012.jpg" style="display: block; max-width: 100%;">' +
                '<h2 class="article-title">' + art[i].title + '</h2>' + '<h3 style="color: orange; margin-bottom: 40px">' + art[i].publishDateString + '</h3>' +
                '<p><a class="signout" style="color: white; text-decoration: none; margin-top: 20px; background: dodgerblue; padding: 10px 20px" href="/articles/id=' + art[i].articleId + '">Learn more...</a></p>' +
                '</article>');
        }
    });
}
getArticles();
function getNews() {
    $.get(
        '/news'
    ).done(function (data) {
        let news = data;
        for (let i = 0; i < news.length; i++) {
            $('div.news').append('<article>' + 
                '<img src="https://mobile-review.com/news/wp-content/uploads/Microsoft-Logo-2012.jpg" style="display: block; max-width: 100%;">' +
                '<h2 class="news-title">' + news[i].title + '</h2>' + 
                    '<p style="display: block; text-align:center;"><a class="signout" style="color: white; text-decoration: none; margin-top: 20px; background: mediumspringgreen; padding: 10px 20px" href="/news/id=' + news[i].newsId + '">Learn more...</a></p>' + '</article>');
        }
    });
}
getNews();
function getCompanies() {
    $.get(
        '/companies'
    ).done(function (data) {
        let companies = data;
        for (let i = 0; i < companies.length; i++) {
            $('ul.companies').append('<li><a href="/companies/id=' + companies[i].companyId + '">' + companies[i].name + '</a></li>');
        }
    });
}
getCompanies();

function getPositions() {
    $.get(
        '/positions'
    ).done(function (data) {
        let positions = data;
        for (let i = 0; i < positions.length; i++) {
            $('div#positions').append('<div class="position"><h2>' + positions[i].name + '</h2><div> ' + "Advertisements amount:  " +  positions[i].amount
                + '</div><a href="/home/advertisements/pos=' + positions[i].positionId + '">' + 'Learn more...' + '</a></div>');
        }
    });
}
getPositions();

$('div.articles article a').hover(function () {
    $('div.articles article a').css('color', 'skyblue');
});
$('div.news article a').hover(function () {
    $('div.news article a').css('color', 'skyblue');
});

function substr(index, str) {
    var s = "";
    for (let i = index; i < str.length; i++) s += str[i];
    return s;
}