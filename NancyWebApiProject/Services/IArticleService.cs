using System;
using System.Collections.Generic;
using NancyWebApiProject.Models;

namespace NancyWebApiProject.Services
{
    public interface IArticleService
    {
        IEnumerable<ArticleView> GetManyBySection(string section);

        ArticleView GetFirstItem(string section);

        IEnumerable<ArticleView> GetManySortedByUpdatedDate(string section, DateTime date);

        ArticleView GetSortedByShortUrl(string section, string shortUrl);

        IEnumerable<ArticleGroupByDateView> GetGroupedByDate(string section);
    }
}
