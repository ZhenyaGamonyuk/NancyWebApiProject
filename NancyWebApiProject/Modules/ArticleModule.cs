using System.Collections.Generic;
using Nancy;
using NancyWebApiProject.Models;
using NancyWebApiProject.Services;

namespace NancyWebApiProject.Modules
{
    public sealed class ArticleModule : NancyModule
    {
        private readonly IArticleService _articleService;

        public ArticleModule(IArticleService articleService)
        {
            _articleService = articleService;

            Get("/", _ => Response.AsText("{\"status\": \"OK\"}", "application/json"));

            Get("/list/{section}", GetArticleList);

            Get("/list/{section}/first", GetFirstArticle);

            Get("/list/{section}/{updatedDate:datetime(yyyy-MM-dd)}", GetArticleListSortedByUpdatedDate);

            Get("/article/{shortUrl:length(7, 7)}", GetArticleByShortUrl);

            Get("/group/{section}", GetGroupedByDate);
        }

        public object GetArticleList(dynamic parameter)
        {
            IEnumerable<ArticleView> result = _articleService.GetManyBySection(parameter.section);

            return Response.AsJson(result);
        }

        public object GetArticleListSortedByUpdatedDate(dynamic parameter)
        {
            IEnumerable<ArticleView> result = _articleService.GetManySortedByUpdatedDate(parameter.section, parameter.updatedDate);

            return Response.AsJson(result);
        }

        public object GetFirstArticle(dynamic parameter)
        { 
            ArticleView result = _articleService.GetFirstItem(parameter.section);

            return Response.AsJson(result);
        }

        public object GetArticleByShortUrl(dynamic parameter)
        {
            ArticleView result = _articleService.GetSortedByShortUrl("home", parameter.shortUrl);

            return Response.AsJson(result);
        }

        public object GetGroupedByDate(dynamic parameter)
        {
            IEnumerable<ArticleGroupByDateView> result = _articleService.GetGroupedByDate(parameter.section);

            return Response.AsJson(result);
        }
    }
}
