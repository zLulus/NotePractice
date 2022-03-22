using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DotNet6.CodeLibrary.XMLToJsonTest
{
    public class XMLToJsonTestDemo
    {
        public static void Run()
        {
            string txt = "<response><itemList><item><number>5</number></item></itemList></response>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(txt);

            var node = doc.ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(0);

            var attribute = doc.CreateAttribute("json", "Array", "http://james.newtonking.com/projects/json");
            attribute.InnerText = "true";
            node.Attributes.Append(attribute);

            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);

            var res = JsonConvert.DeserializeObject<XMLToJsonModel>(json);
        }
    }
}
