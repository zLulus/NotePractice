using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WpfDemo.SearchReferences.Models;
using WpfDemo.SearchReferences.Tools;

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
            Dispatcher.InvokeAsync(() =>
            {
                var folderPath = FolderPathTextBox.Text;
                //递归查询.cs文件
                List<string> files = new List<string>();
                GetAllCsFiles(files, folderPath);
                //正则匹配查找方法名
                //查询方法对应的body代码
                //  MethodName(parameters){ {{ }} {} } 花括号匹配，堆栈为空
                //正则匹配查询所有引用方法+参数个数（参数个数相同的多个同名方法不能区分）
                //保存Dictionary：文件位置、文件名、方法名、引用方法信息子级列表（文件位置、文件名、方法名、引用方法信息...）
                //key=FilePath_MethodName_MethodParameterCount
                Dictionary<string, CodeReferenceInfo> codeReferenceInfos = new Dictionary<string, CodeReferenceInfo>();
                foreach (var file in files)
                {
                    Regex fileRegex = new Regex(@"\\(?<fileName>.+?).cs", RegexOptions.Compiled);
                    var fileMatch = fileRegex.Match(file);
                    if (!fileMatch.Success)
                    {
                        continue;
                    }
                    var fileName = fileMatch.Groups["fileName"].Value;
                    if (fileName.Contains("\\"))
                    {
                        var s = fileName.Split('\\');
                        fileName = s[s.Length - 1];
                    }

                    var content = File.ReadAllText(file);
                    //todo 没有检查没有private关键词的method，没有检查methodName和(parameters)之间有空格的method
                    Regex regex = new Regex(@"(internal|public|private)\s+?(?<methodName>.+?)\((?<parameters>.+?)\)[\s\S]+?\{", RegexOptions.Compiled);
                    var matches = regex.Matches(content);
                    if (matches.Count > 0)
                    {
                        List<Tuple<string, string>> t = new List<Tuple<string, string>>();
                        foreach (Match match in matches)
                        {
                            string methodName = match.Groups["methodName"].Value;
                            if (methodName.Contains(" "))
                            {
                                var mmm = methodName.Split(' ');
                                methodName = mmm[mmm.Length - 1];
                            }
                            string parameters = match.Groups["parameters"].Value;
                            int parameterCount = GetParameterCount(parameters);
                            t.Add(new Tuple<string, string>(methodName, parameters));
                            var info = GetReferencesAndCode(methodName, parameters, content);

                            if (info == null)
                                continue;
                            //todo 同名同参数个数
                            var key = $"{file}_{methodName}_{parameterCount}";
                            if (!codeReferenceInfos.ContainsKey(key))
                            {
                                codeReferenceInfos.Add(key, new CodeReferenceInfo()
                                {
                                    FilePath = file,
                                    FileName = fileName,
                                    MethodName = methodName,
                                    Parameters = parameters,
                                    MethodParameterCount = parameterCount,
                                    Code = info.Item2,
                                    ChildReferences = info.Item1
                                });
                            }
                        }
                    }

                }

                //修正References,改为FilePath_MethodName_MethodParameterCount（key）
                foreach (var codeReferenceInfo in codeReferenceInfos)
                {
                    List<string> newReferences = new List<string>();
                    foreach (var reference in codeReferenceInfo.Value.ChildReferences)
                    {
                        foreach (var key in codeReferenceInfos.Keys)
                        {
                            if (key.Contains($"{reference.Item3}") && reference.Item2.Contains(codeReferenceInfos[key].FileName.ToLower()))
                            {
                                if (!newReferences.Contains(key))
                                    newReferences.Add(key);
                            }
                        }
                    }
                    //_MethodName_MethodParameterCount -> FilePath_MethodName_MethodParameterCount
                    codeReferenceInfo.Value.ChildReferencesDictionary = newReferences;
                }

                foreach (var codeReferenceInfo in codeReferenceInfos)
                {
                    foreach (var childReference in codeReferenceInfo.Value.ChildReferencesDictionary)
                    {
                        codeReferenceInfos[childReference].ParentReferences.Add(codeReferenceInfo.Key);
                    }
                }

                //根据关键词查询相关数据
                //输出到log文件
                string searchWord = "aud_PartGroup";
                Search(codeReferenceInfos, searchWord);
            });

        }

        private void Search(Dictionary<string, CodeReferenceInfo> codeReferenceInfos, string searchWord)
        {
            var logger = NLogHelper.GetFileLogger($"{searchWord}_result");
            foreach (var codeReferenceInfo in codeReferenceInfos)
            {
                if (codeReferenceInfo.Value.Code.Contains($" {searchWord} "))
                {
                    foreach (var parent in codeReferenceInfo.Value.ParentReferences)
                    {
                        //一个引用链
                        List<string> referenceList = new List<string>();
                        referenceList.Add(codeReferenceInfo.Key);
                        referenceList.Add(parent);
                        GetReferenceList(referenceList, codeReferenceInfos, parent);
                        foreach (var reference in referenceList)
                        {
                            var s = reference.Split('_');
                            logger.Info($"{s[0]} {s[1]}({codeReferenceInfos[reference].Parameters})");
                        }
                        logger.Info($"\n");
                    }


                }
            }
        }

        private void GetReferenceList(List<string> referenceList, Dictionary<string, CodeReferenceInfo> codeReferenceInfos, string target)
        {
            foreach (var parent in codeReferenceInfos[target].ParentReferences)
            {
                referenceList.Add(parent);
                GetReferenceList(referenceList, codeReferenceInfos, parent);
            }
        }

        private static int GetParameterCount(string parameters)
        {
            var parameterCount = 0;
            if (string.IsNullOrEmpty(parameters) || string.IsNullOrWhiteSpace(parameters))
            {
                parameterCount = 0;
            }
            else
            {
                if (parameters.Contains(","))
                {
                    parameterCount = parameters.Split(',').Length;
                }
                else
                {
                    parameterCount = 1;
                }
            }

            return parameterCount;
        }

        private Tuple<List<Tuple<string, string, string>>, string> GetReferencesAndCode(string methodName, string parameters, string content)
        {
            try
            {
                //返回reference为:_MethodName_MethodParameterCount
                Stack<string> stack = new Stack<string>();
                bool isFirst = true;
                //todo 没有检查methodName和(parameters)之间有空格的method
                string start = $"{methodName}({parameters})";
                var startIndex = content.IndexOf(start);
                var endIndex = startIndex;
                StringBuilder sb = new StringBuilder();
                while (stack.Count != 0 || isFirst)
                {
                    var s = content[endIndex];
                    if (s == '{')
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        stack.Push("{");
                    }
                    else if (s == '}')
                    {
                        if (stack.Peek() == "{")
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push("}");
                        }
                    }
                    if (!isFirst)
                        sb.Append(s);
                    endIndex++;
                }
                var code = sb.ToString();

                List<Tuple<string, string, string>> referenceList = new List<Tuple<string, string, string>>();
                //todo 没有检查没有private关键词的method，没有检查methodName和(parameters)之间有空格的method
                Regex regex = new Regex(@"\s(?<referenceInstanceName>.+?)\.(?<referenceMethodName>.+?)\((?<referenceParameters>.+?)\)", RegexOptions.Compiled);
                var matches = regex.Matches(code);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        string referenceInstanceName = match.Groups["referenceInstanceName"].Value;
                        if (referenceInstanceName.Contains(" "))
                        {
                            var s = referenceInstanceName.Split(' ');
                            referenceInstanceName = s[s.Length - 1];
                        }
                        string referenceMethodName = match.Groups["referenceMethodName"].Value;
                        string referenceParameters = match.Groups["referenceParameters"].Value;
                        int parameterCount = GetParameterCount(referenceParameters);
                        var newReference = $"_{referenceMethodName}_{parameterCount}";
                        referenceList.Add(new Tuple<string, string, string>("", referenceInstanceName.ToLower(), newReference));
                    }
                }

                return new Tuple<List<Tuple<string, string, string>>, string>(referenceList, code);
            }
            catch (Exception ex)
            {
                // { get { return xxx(xxx); } }
                return null;
            }

        }

        private static List<string> GetAllCsFiles(List<string> files, string folderPath)
        {
            foreach (var file in Directory.GetFiles(folderPath))
            {
                if (file.EndsWith(".cs"))
                {
                    files.Add(file);
                }
            }
            foreach (var directory in Directory.GetDirectories(folderPath))
            {
                GetAllCsFiles(files, directory);
            }
            return files;
        }

        private void SelectPath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                FolderPathTextBox.Text = dialog.FileName;
            }
        }
    }
}
