using Elasticsearch.Net;
using Nest;
using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Students
{
    public class ElasticsearchService : IElasticsearchService
    {
        string address = "";
        string SearchAnalyzer = "standard";
        ElasticClient elasticClient;

        public ElasticsearchService()
        {
            elasticClient = new ElasticClient(new ConnectionSettings(new Uri(address))
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

        public async Task<IReadOnlyCollection<StudentForElasticsearch>> GetInfoOnRequestCompleted(string indexAliasName,string name)
        {
            ISearchResponse<StudentForElasticsearch> searchResponse = await elasticClient.SearchAsync<StudentForElasticsearch>(searchDescriptor =>
            {
                return searchDescriptor.Index(indexAliasName).Query(queryContainerDescriptor =>
                {
                    return queryContainerDescriptor.Bool(boolQueryDescriptor =>
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            IList<Func<QueryContainerDescriptor<StudentForElasticsearch>, QueryContainer>> queryContainers = new List<Func<QueryContainerDescriptor<StudentForElasticsearch>, QueryContainer>>();

                            queryContainers.Add(queryContainerDescriptor =>
                            {
                                return queryContainerDescriptor.MultiMatch(matchQueryDescriptor =>
                                {
                                    return matchQueryDescriptor.Fields(new string[] { ToJavaScriptPropertyName(nameof(StudentForElasticsearch.Name))}).
                                                                Analyzer(SearchAnalyzer).Query(name);
                                });
                            });
                            boolQueryDescriptor.Must(x => x.Bool(b => b.Should(queryContainers)));
                        }
                       
                        return boolQueryDescriptor;
                    });
                })
                .From(0).Size(10);
            });

            if (searchResponse.ServerError != null)
                throw new Exception($"查询服务出错。{Environment.NewLine}{searchResponse.ServerError.Error.Reason}");

            return searchResponse.Documents;
        }

        public async Task CreateIndex(string indexName, string indexAliasName)
        {
            ExistsResponse existsResponse = await elasticClient.Indices.ExistsAsync(indexName);

            if (existsResponse.ServerError != null)
                throw new Exception($"Elasticsearch连接失败。{Environment.NewLine}{existsResponse.ServerError.Error.Reason}");

            if (!existsResponse.Exists)
            {
                CreateIndexResponse createIndexResponse = await elasticClient.Indices.CreateAsync(indexName, createIndexDescriptor =>
                {
                    return createIndexDescriptor.
                        Map(typeMappingDescriptor =>
                        {
                            return typeMappingDescriptor.Properties(propertiesSelector =>
                            {
                                foreach (PropertyInfo propertyInfo in typeof(StudentForElasticsearch).GetProperties())
                                {
                                    if (!propertyInfo.CanWrite)
                                        continue;

                                    switch (propertyInfo.PropertyType.Name)
                                    {
                                        case nameof(Int16):
                                        case nameof(Int32):
                                        case nameof(Int64):
                                        case nameof(UInt16):
                                        case nameof(UInt32):
                                        case nameof(UInt64):
                                        case nameof(Decimal):
                                        case nameof(Single):
                                        case nameof(Double):
                                        case nameof(Byte):
                                            propertiesSelector = propertiesSelector.Number(propertyDescriptor => propertyDescriptor.Name(ToJavaScriptPropertyName(propertyInfo.Name)));
                                            break;

                                        case nameof(Boolean):
                                            propertiesSelector = propertiesSelector.Boolean(propertyDescriptor => propertyDescriptor.Name(ToJavaScriptPropertyName(propertyInfo.Name)));
                                            break;

                                        case nameof(DateTime):
                                            propertiesSelector = propertiesSelector.Date(propertyDescriptor => propertyDescriptor.Name(ToJavaScriptPropertyName(propertyInfo.Name)));
                                            break;

                                        case nameof(String):
                                            propertiesSelector = propertiesSelector.Keyword(propertyDescriptor => propertyDescriptor.Name(ToJavaScriptPropertyName(propertyInfo.Name)));
                                            break;

                                        default:
                                            break;
                                    }
                                }

                                return propertiesSelector;
                            });
                        });
                });

                if (createIndexResponse.ServerError != null)
                    throw new Exception($"Elasticsearch创建索引失败。{Environment.NewLine}{createIndexResponse.ServerError.Error.Reason}");

                PutAliasResponse putAliasResponse = await elasticClient.Indices.PutAliasAsync(indexName, indexAliasName);

                if (putAliasResponse.ServerError != null)
                {
                    await elasticClient.Indices.DeleteAsync(indexName);
                    throw new Exception($"Elasticsearch创建索引失败。{Environment.NewLine}{putAliasResponse.ServerError.Error.Reason}");
                }
            }
        }

        public async Task AddOrUpdateData(string indexAliasName, IList<StudentForElasticsearch> datas)
        {
            BulkResponse bulkResponse = await elasticClient.BulkAsync(bulkDescriptor =>
            {
                foreach (StudentForElasticsearch document in datas)
                {
                    bulkDescriptor = bulkDescriptor.Index<StudentForElasticsearch>(bulkIndexDescriptor =>
                    {
                        return bulkIndexDescriptor.Index(indexAliasName).Id(document.Id).Document(document);
                    });
                }

                return bulkDescriptor;
            });

            if (!bulkResponse.IsValid || bulkResponse.Errors || bulkResponse.ServerError != null)
            {
                throw new Exception($"文档索引失败{bulkResponse.ServerError?.Error?.Reason}，{Environment.NewLine}数据ID：{string.Join(",", datas.Select(data => data.Id))}");
            }
        }

        public async Task DeleteData(string indexAliasName, IList<int> deleteIds)
        {
            BulkResponse bulkResponse = await elasticClient.BulkAsync(bulkDescriptor =>
            {
                foreach (int id in deleteIds)
                {
                    bulkDescriptor = bulkDescriptor.Delete<StudentForElasticsearch>(bulkIndexDescriptor =>
                    {
                        return bulkIndexDescriptor.Index(indexAliasName).Id(id);
                    });
                }

                return bulkDescriptor;
            });

            if (!bulkResponse.IsValid || bulkResponse.ServerError != null || bulkResponse.Errors)
            {
                throw new Exception($"文档删除失败{bulkResponse.ServerError?.Error?.Reason}，{Environment.NewLine}数据ID：{string.Join(",", deleteIds)}");
            }
        }

        private string ToJavaScriptPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return propertyName;
            return propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
        }

        private string GetInfosFromApiCallDetails(IApiCallDetails r)
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
