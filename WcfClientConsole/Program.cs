using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WcfClientConsole.Model;

namespace WcfClientConsole
{
    class Program
    {
        static string url = "http://localhost:8733/Design_Time_Addresses/WcfServicePractice/Service1/";
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            string result= client.GetDataUsingDataContract(1);

            

            //GetOneParameter(1);
            //GetManyParameter(1, 2);
            //PostOneParameter(20);
            //PostManyParameters(1, 2);
            //PostModel(new Student() { PKID = 1, StudentNumber = "A001", Name = "Jim" });
            PostModelAndParameter(new Student() { PKID = 3, StudentNumber = "A003", Name = "Ben" }, true);
            PostModelAndParameter(new Student() { PKID = 2, StudentNumber = "A002", Name = "Ann" }, false);
            //PostStream();
            //CombinationStudent(20, "B001", "Haha");
            Console.Read();
        }

        public static async void GetOneParameter(int value)
        {
            HttpClient client = new HttpClient();
            var r= await client.GetStringAsync(string.Format("{0}GetOneParameter/{1}",url,value));
        }

        public static async void GetManyParameter(int number1, int number2)
        {
            HttpClient client = new HttpClient();
            var r = await client.GetStringAsync(url+string.Format("GetManyParameter/{0}/{1}",number1,number2));
        }

        public static async void GetManyParameter2(int number1, int number2)
        {
            HttpClient client = new HttpClient();
            var r = await client.GetStringAsync(string.Format("{0}GetManyParameter?number1={1}&number2={2}",url,number1,number2));
        }

        public static async void PostOneParameter(int value)
        {
            HttpClient client = new HttpClient();
            //"application/json"不能少
            HttpContent content = new StringContent(value.ToString(), Encoding.UTF8,"application/json");
            var result=await client.PostAsync(url + "PostOneParameter", content);
            var r=  result.Content.ReadAsStringAsync().Result;
        }

        public static void PostManyParameters(int number1, int number2)
        {
            WebClient client = new WebClient();
            JObject jObject = new JObject();
            jObject.Add("number1", number1);
            jObject.Add("number2", number2);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.UploadString(url+ "PostManyParameters", jObject.ToString(Newtonsoft.Json.Formatting.None, null));
            
        }

        public static async void PostModel(Student student)
        {
            string str = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result =await client.PostAsync(url + "PostModel", content);
            var r= result.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(r);
            string name = jObject["name"].ToString();
        }

        public static async void PostModelAndParameter(Student student, bool isDelete)
        {
            string str = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PostAsync(string.Format("{0}PostModelAndParameter/{1}",url,isDelete), content);
            var r = result.Content.ReadAsStringAsync().Result;
        }

        public static async void PostStream()
        {
            string name = "123";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("This is a stream."));
            StreamContent content = new StreamContent(stream);
            HttpClient client = new HttpClient();
            var result = await client.PostAsync(string.Format("{0}PostStream/{1}",url,name), content);
            var r = result.Content.ReadAsStringAsync().Result;
        }

        public static void CombinationStudent(int PKID, string StudentNumber, string Name)
        {
            WebClient client = new WebClient();
            JObject jObject = new JObject();
            jObject.Add("PKID", PKID);
            jObject.Add("StudentNumber", StudentNumber);
            jObject.Add("Name", Name);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.UploadString(url + "CombinationStudent", jObject.ToString(Newtonsoft.Json.Formatting.None, null));
            Student s = JsonConvert.DeserializeObject<Student>(result);
        }
    }
}
