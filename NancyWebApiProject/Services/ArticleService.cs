using System;
using System.Collections.Generic;
using System.Linq;
using NancyWebApiProject.DataProviders;
using NancyWebApiProject.Models;
using Newtonsoft.Json.Linq;

namespace NancyWebApiProject.Services
{
    public class ArticleService : IArticleService
    {
        private const string DateFormat = "yyyy-MM-dd";

        private readonly IDataProvider _dataProvider;

        public ArticleService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IEnumerable<ArticleView> GetManyBySection(string section)
        {
            JToken[] resultData = _dataProvider.GetResultData(section);
            IEnumerable<ArticleView> result = resultData.Select(MapJsonTokenToArticleView);

            return result;
        }

        public ArticleView GetFirstItem(string section)
        {
            return GetManyBySection(section).FirstOrDefault();
        }

        public IEnumerable<ArticleView> GetManySortedByUpdatedDate(string section, DateTime date)
        {
            return GetManyBySection(section).Where(w => w.Updated.Date == date.Date);
        }

        public ArticleView GetSortedByShortUrl(string section, string shortUrl)
        {
            JToken[] resultData = _dataProvider.GetResultData(section);
            JToken resultToken = resultData.FirstOrDefault(f => f.Value<string>("short_url").Contains(shortUrl));
            ArticleView result = MapJsonTokenToArticleView(resultToken);

            return result;
        }

        public IEnumerable<ArticleGroupByDateView> GetGroupedByDate(string section)
        {
            JToken[] resultData = _dataProvider.GetResultData(section);

            IEnumerable<ArticleGroupByDateView> result = resultData
                .GroupBy(g => g.Value<DateTime>("created_date").Date.ToString(DateFormat))
                .Select(s => new ArticleGroupByDateView
                {
                    Date = s.Key,
                    Total = s.Count()
                });

            return result;
        }

        private ArticleView MapJsonTokenToArticleView(JToken tokenToMap)
        {
            if (tokenToMap == null)
            {
                return null;
            }

            return new ArticleView
            {
                Heading = tokenToMap.Value<string>("title"),
                Link = tokenToMap.Value<string>("url"),
                Updated = tokenToMap.Value<DateTime>("updated_date")
            };
        }
    }
}
