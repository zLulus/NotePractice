using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Tools.Caches
{
    public class MemoryCacheManager
    {
        private static MemoryCache _memoryCache;
        /// <summary>
        /// 注意这里是静态构造函数，用构造函数没有“缓存”效果（MemoryCache对象被多次创建）
        /// </summary>
        static MemoryCacheManager()
        {
            _memoryCache = new MemoryCache("MemoryCacheManager");
        }

        public object GetCache(string key)
        {
            return _memoryCache.Get(key);
        }

        public void SetCache(string key, object value,double seconds=5)
        {
            _memoryCache.Set(key,value, new DateTimeOffset(DateTime.Now.AddSeconds(seconds)));
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
