using Newtonsoft.Json.Linq;

namespace NancyWebApiProject.DataProviders
{
    public interface IDataProvider
    {
        string GetData(string section);

        JToken[] GetResultData(string section);
    }
}
