using CodeLibraryForDotNetCore.ReadAndWriteXml.Dtos;
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

        public static void Run()
        {
            List<ColumnDto> columns = CreateXml();
            WriteXml(columns);
            var cols= LoadXml();
        }

        private static List<ColumnDto> CreateXml()
        {
            List<ColumnDto> columns = new List<ColumnDto>();
            columns.Add(new ColumnDto() { Name = "Time", AliasName = "时间", IsCanNotModify = false });
            return columns;
        }

        private static void WriteXml(List<ColumnDto> columns)
        {
            //获取根节点对象
            XDocument document = new XDocument();
            XElement root = new XElement(Root);
            foreach(var columnDto in columns)
            {
                XElement column = new XElement(Column);
                column.SetElementValue(nameof(columnDto.Name), columnDto.Name);
                column.SetElementValue(nameof(columnDto.AliasName), columnDto.AliasName);
                column.SetElementValue(nameof(columnDto.IsCanNotModify), columnDto.IsCanNotModify);
                root.Add(column);
            }
            root.Save(filePath);
        }

        private static List<ColumnDto> LoadXml()
        {
            List<ColumnDto> columns = new List<ColumnDto>();
            //将XML文件加载进来
            XDocument document = XDocument.Load(filePath);
            //获取到XML的根元素进行操作
            XElement root = document.Root;
            //获取根元素下的所有子元素
            IEnumerable<XElement> enumerable = root.Elements();
            foreach (XElement column in enumerable)
            {
                ColumnDto columnDto = new ColumnDto();
                foreach (XElement property in column.Elements())
                {
                    if (property.Name == nameof(columnDto.Name))
                    {
                        columnDto.Name = property.Value;
                    }
                    else if(property.Name == nameof(columnDto.AliasName))
                    {
                        columnDto.AliasName = property.Value;
                    }
                    else if (property.Name == nameof(columnDto.IsCanNotModify))
                    {
                        columnDto.IsCanNotModify = bool.Parse(property.Value);
                    }
                    //Console.WriteLine($"{property.Name}:{property.Value}");   
                }
                columns.Add(columnDto);
                //Console.WriteLine(column.Attribute("id").Value);
            }
            return columns;
        }
    }
}
