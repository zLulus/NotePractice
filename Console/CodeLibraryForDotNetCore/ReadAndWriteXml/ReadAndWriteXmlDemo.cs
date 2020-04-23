using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CodeLibraryForDotNetCore.ReadAndWriteXml
{
    public static class ReadAndWriteXmlDemo
    {
        static string filePath = $"{Directory.GetCurrentDirectory()}\\ReadAndWriteXml\\test.xml";
        static string Column = "Column";
        static string Root = "Root";
        static string Name = "Name";
        static string AliasName = "AliasName";
        static string IsCanNotModify = "IsCanNotModify";

        public static void Run()
        {
            WriteXml();
            LoadXml();
        }

        private static void WriteXml()
        {
            //获取根节点对象
            XDocument document = new XDocument();
            XElement root = new XElement(Root);
            for(int i = 0; i < 10; i++)
            {
                XElement column = new XElement(Column);
                column.SetElementValue("Name", $"列名{i}");
                column.SetElementValue("AliasName", $"AliasName{i}");
                column.SetElementValue("IsCanNotModify", $"{i%2==0}");
                root.Add(column);
            }
            root.Save(filePath);
        }

        private static void LoadXml()
        {
            //将XML文件加载进来
            XDocument document = XDocument.Load(filePath);
            //获取到XML的根元素进行操作
            XElement root = document.Root;
            //获取根元素下的所有子元素
            IEnumerable<XElement> enumerable = root.Elements();
            foreach (XElement column in enumerable)
            {
                foreach (XElement property in column.Elements())
                {
                    Console.WriteLine($"{property.Name}:{property.Value}");   
                }
                //Console.WriteLine(column.Attribute("id").Value);
            }
        }
    }
}
