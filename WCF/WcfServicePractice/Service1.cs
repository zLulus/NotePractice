using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServicePractice.Model;

namespace WcfServicePractice
{
    public class Service1 : IService1
    {

        [WebInvoke(Method = "GET", UriTemplate = "GetOneParameter/{value}", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //写在UriTemplate中的参数，必须定义为string类型
        public string GetOneParameter(string value)
        {
            return string.Format("You get: {0}", value);
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetManyParameter?number1={number1}&number2={number2}", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //UriTemplate采用key=value的写法
        public string GetManyParameter2(string number1, string number2)
        {
            return string.Format("get : number1 * number2 = " + (int.Parse(number1) * int.Parse(number2)));
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetManyParameter/{number1}/{number2}", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //UriTemplate决定了number1和number2的位置
        public string GetManyParameter(string number1, string number2)
        {
            return string.Format("get : number1 * number2 = "+ (int.Parse(number1)*int.Parse(number2)) );
        }

        [WebInvoke(Method = "POST", UriTemplate = "PostOneParameter", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string PostOneParameter(int value)
        {
            return string.Format("You post: {0}", value);
        }

        [WebInvoke(Method = "POST", UriTemplate = "PostManyParameters", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //多参数POST必须采用Wrapped/WrappedRequest，指明number1是哪个，number2是哪个
        //单参数Bare/Wrapped均可，但是服务器和客户端请求要对应
        public string PostManyParameters(int number1,int number2)
        {
            return string.Format("post : number1 - number2 = " + (number1 - number2));
        }

        [WebInvoke(Method = "POST", UriTemplate = "PostModel", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //客户端手动序列化对象，服务端自动反序列化为类对象
        //客户端、服务端类可以不同，只会反序列化回名字相同的字段、属性（忽略大小写）
        //实体类加上DataContract和DataMember特性
        public string PostModel(Student student)
        {
            return string.Format("You post a student info: StudentNumber-{0}, Name-{1}", student.StudentNumber, student.Name);
        }

        [WebInvoke(Method = "POST", UriTemplate = "PostModelAndParameter/{isDelete}", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string PostModelAndParameter(Student student,string isDelete)
        {
            if(bool.Parse(isDelete))
            {
                return string.Format("You want to delete a student, info: StudentNumber-{0}, Name-{1}", student.StudentNumber, student.Name);
            }
            else
            {
                return string.Format("You want to add a student, info: StudentNumber-{0}, Name-{1}", student.StudentNumber, student.Name);
            }
        }

        [WebInvoke(Method = "POST", UriTemplate = "PostStream/{name}", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string PostStream(string name, Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string result= string.Format("You post stream : {0} && its name is {1}.", reader.ReadToEnd(), name);
            return result;
        }

        [WebInvoke(Method = "POST", UriTemplate = "CombinationStudent", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public Student CombinationStudent(int PKID, string StudentNumber, string Name)
        {
            Student s = new Student() { PKID = PKID, StudentNumber = StudentNumber, Name = Name };
            return s;
        }
    }
}
