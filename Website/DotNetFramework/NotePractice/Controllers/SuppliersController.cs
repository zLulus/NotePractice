using BLL;
using Microsoft.AspNet.OData;
using Model.ODataStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class SuppliersController : ODataController
    {
        ODataStudyManager db = new ODataStudyManager();

        [EnableQuery]
        public List<Product> GetProducts([FromODataUri] int key)
        {
            return db.GetProducts(key);
        }
    }
}