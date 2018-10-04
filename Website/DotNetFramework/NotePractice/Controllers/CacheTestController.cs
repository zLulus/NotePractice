using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace NotePractice.Controllers
{
    public class CacheTestController : Controller
    {
        private static MemoryCache _memoryCache;
        /// <summary>
        /// 注意这里是静态构造函数，用构造函数没有“缓存”效果（MemoryCache对象被多次创建）
        /// </summary>
        static CacheTestController()
        {
            _memoryCache = new MemoryCache("CacheTest");
        }

        [HttpGet]
        public ActionResult CacheTestByDateTime(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = "CacheTest";
            }
            // Look for cache key.
            if (_memoryCache.Get(key)==null)
            {
                DateTime cacheEntry;
                // Key not in cache, so get data.
                cacheEntry = DateTime.Now;

                // Set cache options.
                var cacheEntryOptions = new DateTimeOffset(DateTime.Now.AddSeconds(10));

                // Save data in cache.
                _memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return Json(_memoryCache.Get(key), JsonRequestBehavior.AllowGet);
        }
    }
}