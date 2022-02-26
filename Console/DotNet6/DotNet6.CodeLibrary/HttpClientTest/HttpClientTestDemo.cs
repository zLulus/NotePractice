using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.HttpClientTest
{
    /// <summary>
    /// https://mp.weixin.qq.com/s/wJITvXN24kBSkiztGIOhBg
    /// </summary>
    public class HttpClientTestDemo
    {
        public static async Task Run()
        {
            const string url = "http://localhost:5000/api/values";

            await Post(url);
            await Put(url);
            await Get(url);





        }

        private static async Task Get(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetFromJsonAsync<ResultModel>(url);
                ArgumentNullException.ThrowIfNull(result);
                Console.WriteLine($"{result.Status}: {result.ErrorMsg}");
            }
        }

        private static async Task Post(string url)
        {
            using(HttpClient httpClient=new HttpClient())
            {
                //post json对象
                var response = await httpClient.PostAsJsonAsync(url, new
                {
                    Id = 1,
                    Name = "Test"
                });
                response.EnsureSuccessStatusCode();
            }

            using (HttpClient httpClient = new HttpClient())
            {
                using var content = JsonContent.Create(new { Id = 1, Name = "Test" });
                using var response = await httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
        }

        private static async Task Put(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //put json对象
                using var response = await httpClient.PutAsJsonAsync(url, new
                {
                    Id = 1,
                    Name = "Test"
                });
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<ResultModel>();
                ArgumentNullException.ThrowIfNull(result);
                Console.WriteLine($"{result.Status}: {result.ErrorMsg}");
            }
        }

        private class ResultModel
        {
            public string Status { get; set; }
            public string ErrorMsg { get; set; }
        }
    }

}
