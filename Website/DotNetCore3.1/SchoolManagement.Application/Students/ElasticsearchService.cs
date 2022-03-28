using Elasticsearch.Net;
using Nest;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Students
{
    public class ElasticsearchService : IElasticsearchService
    {
        public async Task GetInfoOnRequestCompleted()
        {
            ElasticClient elasticClient = new ElasticClient(new ConnectionSettings(new Uri(""))
                //打印请求、回复，可能影响性能
                .DisableDirectStreaming()
                .OnRequestCompleted(apiCallDetails =>
                {
                    if (apiCallDetails.Success)
                    {
                        #if DEBUG
                        string infos = GetInfosFromApiCallDetails(apiCallDetails);
                        Console.WriteLine(infos);
                        #endif
                    }
                    else
                    {
                        string infos = GetInfosFromApiCallDetails(apiCallDetails);
                        Console.WriteLine(infos);
                    }
                }));


        }

        private static string GetInfosFromApiCallDetails(IApiCallDetails r)
        {
            string infos = "";
            infos += $"Uri:\n{r.Uri}\n";
            infos += $"Success:\n{r.Success}\n";
            infos += $"SuccessOrKnownError:\n{r.SuccessOrKnownError}\n";
            infos += $"HttpMethod:\n{r.HttpMethod}\n";
            infos += $"HttpStatusCode:\n{r.HttpStatusCode}\n";
            infos += $"DebugInformation:\n{r.DebugInformation}\n";
            foreach (var deprecationWarning in r.DeprecationWarnings)
                infos += $"DeprecationWarnings:\n{deprecationWarning}\n";
            if (r.OriginalException != null)
            {
                infos += $"OriginalException.GetMessage:\n{r.OriginalException.Message}\n";
                infos += $"OriginalException.GetStackTrace:\n{r.OriginalException.Message}\n";
            }
            if (r.RequestBodyInBytes != null)
                infos += $"RequestBody:\n{Encoding.UTF8.GetString(r.RequestBodyInBytes)}\n";
            if (r.ResponseBodyInBytes != null)
                infos += $"ResponseBody:\n{Encoding.UTF8.GetString(r.ResponseBodyInBytes)}\n";
            infos += $"ResponseMimeType:\n{r.ResponseMimeType}\n";
            return infos;
        }
    }
}
