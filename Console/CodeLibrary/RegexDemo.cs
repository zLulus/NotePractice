using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeLibrary
{
    public class RegexDemo
    {
        public static void Demo()
        {
            string ToBeRegex = @"m=39536; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, u=130****7736; Domain=uspard.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, rn=%E6%9B%BE%E5%9B%BD%E5%AF%8C; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, lid=60637; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, wx=""""; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, bs=800301%2C800201; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, t=1508417567138; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/, s=4fd514113bde1ce8c10058148cd16d6a; Domain=baidu.com; Expires=Sat, 21-Oct-2017 12:52:47 GMT; Path=/";
            //.+?任意字符串
            //()表示一个集合，?<name>为值命名
            //IgnoreCase表示忽略大小写
            Regex regex = new Regex("m=(?<m>.+?);.+?u=(?<u>.+?);",RegexOptions.IgnoreCase);
            var matches = regex.Match(ToBeRegex);
            if (matches.Success)
            {
                string m = matches.Groups["m"].Value;
                string u = matches.Groups["u"].Value;
            }
        }
    }
}
