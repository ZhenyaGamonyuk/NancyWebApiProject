using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace NancyWebApiProject.DataProviders
{
    public class NewYorkTimesDataProvider : IDataProvider
    {
        private const string Url = "https://api.nytimes.com/svc/topstories/v2/{0}.json?api-key={1}";
        private const string ApiKey = "Uy11fiO61SguISOCQKGSsKdYtU9iAWLA";

        public string GetData(string section)
        {
            string result = null;

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    result = webClient.DownloadString(String.Format(Url, section, ApiKey));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public JToken[] GetResultData(string section)
        {
            string result = GetData(section);

            if (String.IsNullOrEmpty(result))
            {
                return new JToken[0];
            }
            else
            {
                return JObject.Parse(result).GetValue("results").ToObject<JToken[]>();
            }
        }
    }
}
