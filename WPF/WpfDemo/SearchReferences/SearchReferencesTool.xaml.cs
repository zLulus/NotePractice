using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WpfDemo.SearchReferences.Models;

namespace WpfDemo.SearchReferences
{
    /// <summary>
    /// Interaction logic for SearchReferencesTool.xaml
    /// </summary>
    public partial class SearchReferencesTool : UserControl
    {
        public SearchReferencesTool()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var folderPath = @"C:\Files\others\test";
            //递归查询.cs文件
            var files = GetAllCsFiles(folderPath);
            //正则匹配查找方法名
            //查询方法对应的body代码
            //  MethodName(parameters){ {{ }} {} } 花括号匹配，堆栈为空
            //正则匹配查询所有引用方法+参数个数（参数个数相同的多个同名方法不能区分）
            //保存Dictionary：文件位置、文件名、方法名、引用方法信息子级列表（文件位置、文件名、方法名、引用方法信息...）
            //key=FilePath_MethodName_MethodParameterCount
            Dictionary<string, CodeReferenceInfo> codeReferenceInfos = new Dictionary<string, CodeReferenceInfo>();
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                //todo
                Regex regex = new Regex(@"internal(?<methodName>.+?)[\s\S]+?\((?<parameters>.+?)\)[\s\S]+?\{", RegexOptions.Compiled);
                var matches = regex.Matches(content);
                if (matches.Count > 0)
                {
                    List<Tuple<string, string>> t = new List<Tuple<string, string>>();
                    foreach (Match match in matches)
                    {
                        string methodName = match.Groups["methodName"].Value;
                        string parameters = match.Groups["parameters"].Value;
                        t.Add(new Tuple<string, string>(methodName, parameters));
                        //todo
                        //var info = GetParameterCountAndReferencesAndCode(methodName, content);
                        //codeReferenceInfos.Add($"{file}_{methodName}_{info.Item1}", new CodeReferenceInfo()
                        //{
                        //    FilePath = file,
                        //    MethodName = methodName,
                        //    MethodParameterCount = info.Item1,
                        //    Code = info.Item3,
                        //    References = info.Item2
                        //});
                    }
                }

            }

            //修正References,改为FilePath_MethodName_MethodParameterCount（key）
            foreach (var codeReferenceInfo in codeReferenceInfos)
            {
                List<string> newReferences = new List<string>();
                foreach (var reference in codeReferenceInfo.Value.References)
                {
                    foreach (var key in codeReferenceInfos.Keys)
                    {
                        if (key.Contains($"{reference}"))
                        {
                            newReferences.Add(key);
                        }
                    }
                }
                //_MethodName_MethodParameterCount -> FilePath_MethodName_MethodParameterCount
                codeReferenceInfo.Value.References = newReferences;
            }

            //根据关键词查询相关数据
            //输出到log文件
            string searchWord = "aud_PartGroup";

            foreach (var codeReferenceInfo in codeReferenceInfos)
            {
                if (codeReferenceInfo.Value.Code.Contains($" {searchWord} "))
                {
                    List<CodeReferenceInfo> referenceList = new List<CodeReferenceInfo>();
                    GetReferenceList(referenceList, codeReferenceInfos, codeReferenceInfo);
                }
            }
        }

        private void GetReferenceList(List<CodeReferenceInfo> referenceList, Dictionary<string, CodeReferenceInfo> codeReferenceInfos, KeyValuePair<string, CodeReferenceInfo> target)
        {
            foreach (var codeReferenceInfo in codeReferenceInfos)
            {
                if (codeReferenceInfo.Value.References.Contains(target.Key))
                {
                    referenceList.Add(codeReferenceInfo.Value);
                    GetReferenceList(referenceList, codeReferenceInfos, codeReferenceInfo);
                }
            }
        }

        private Tuple<int, List<string>, string> GetParameterCountAndReferencesAndCode(string methodName, string content)
        {
            //返回reference为:_MethodName_MethodParameterCount
            return null;
        }

        private static List<string> GetAllCsFiles(string folderPath)
        {
            List<string> files = new List<string>();
            foreach (var file in Directory.GetFiles(folderPath))
            {
                if (file.EndsWith(".cs"))
                {
                    files.Add(file);
                }
            }
            foreach (var directory in Directory.GetDirectories(folderPath))
            {
                GetAllCsFiles(directory);
            }
            return files;
        }
    }
}
