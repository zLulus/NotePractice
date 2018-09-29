using OData.Client.Model.ODataStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OData.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceUri = "http://localhost:5003/";
            var container = new Default.Container(new Uri(serviceUri));

            var product = new Product()
            {
                Name = "Yo-yo",
                Category = "Toys",
                Price = 4.95M
            };
            //https://docs.microsoft.com/zh-cn/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/odata-actions-and-functions
            //调用自定义方法
            var rate = container.GetSalesTaxRate(1);
            AddProduct(container, product);
            ListAllProducts(container);
            Console.ReadKey();
        }

        // Get an entire entity set.
        static void ListAllProducts(Default.Container container)
        {
            foreach (var p in container.Products)
            {
                Console.WriteLine("{0} {1} {2}", p.Name, p.Price, p.Category);
            }
        }

        static void AddProduct(Default.Container container, Product product)
        {
            container.AddToProducts(product);
            var serviceResponse = container.SaveChanges();
            foreach (var operationResponse in serviceResponse)
            {
                Console.WriteLine("Response: {0}", operationResponse.StatusCode);
            }
        }
    }
}
