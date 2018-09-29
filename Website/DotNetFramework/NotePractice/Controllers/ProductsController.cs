using BLL;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Model.ODataStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class ProductsController : ODataController
    {
        ODataStudyManager db = new ODataStudyManager();
        public bool ProductExists(int key)
        {
            return db.ProductExists(key);
        }

        [EnableQuery]
        public List<Product> Get()
        {
            return db.Get();
        }

        [EnableQuery]
        public Product Get([FromODataUri] int key)
        {
            return db.Get(key);
        }

        public void Create(Product product)
        {
            db.Create(product);
        }

        [System.Web.Mvc.HttpGet]
        [ODataRoute("GetSalesTaxRate(PostalCode={postalCode})")]
        public IHttpActionResult GetSalesTaxRate([FromODataUri] int postalCode)
        {
            double rate = 5.6;  // Use a fake number for the sample.
            return Ok(rate);
        }
    }
}