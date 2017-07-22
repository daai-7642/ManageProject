using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
namespace FMCG.Utility.Cache
{
   
    /// <summary>
    /// 缓存
    /// </summary>
    public static class CacheHelper
    {
        public static readonly int ExpirationTime;
        static CacheHelper()
        {
            //缓存过期时间   默认为7200s
            var config = ConfigurationManager.AppSettings["ExpirationTime"];
            ExpirationTime = config != null ? Convert.ToInt32(config) : 7200;
        }
        /// <summary>
        /// 按绝对过期时间方式添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireSeconds">绝对过期秒数</param>
        public static void Add(string key, object value, TimeSpan cacheDuration)
        {
            Add(key, value, cacheDuration, CacheItemPriority.Normal);
        }

        /// <summary>
        /// 按绝对过期时间方式添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireSeconds">绝对过期秒数</param>
        /// <param name="priority">优先级</param>
        public static void Add(string key, object value, TimeSpan cacheDuration, CacheItemPriority priority)
        {
            Add(key, value, null, DateTime.Now.Add(cacheDuration), TimeSpan.Zero, priority, null);
        }

        /// <summary>
        /// 按绝对过期时间方式添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="priority">优先级</param>
        public static void Add(string key, object value, DateTime absoluteExpiration, CacheItemPriority priority)
        {
            Add(key, value, null, absoluteExpiration, TimeSpan.Zero, priority, null);
        }

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dependencies">依赖项</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="slidingExpiration">相对过期时间</param>
        /// <param name="priority">优先级</param>
        /// <param name="onRemovedCallback">缓存移除回调</param>
        public static void Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
        {
            HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
        }

        /// <summary>
        /// 按不过期方式添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void AddPermanent(string key, object value)
        {
            AddPermanent(key, value, null);
        }

        /// <summary>
        /// 按不过期方式添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值DateTime.MaxValue</param>
        /// <param name="dependencies">依赖项</param>
        public static void AddPermanent(string key, object value, CacheDependency dependencies)
        {
            Add(key, value, dependencies,DateTime.Now.Add(TimeSpan.FromSeconds(ExpirationTime)), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        #region 缓存管理器职责

        /// <summary>
        /// 获取所有缓存键组
        /// 键组：做为一组缓存的开始标志
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, int> GetCacheKeyGroups()
        {
            Dictionary<string, int> list = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            foreach (System.Collections.DictionaryEntry item in HttpRuntime.Cache)
            {
                string key = item.Key.ToString();
                //keyGroup 规则，不存在"/"及 排除缓存模板自身使用的
                if (key.Contains("::") || key.StartsWith("__"))
                {
                    continue;
                }
                //key 不存在时添加
                string[] strs = key.Split('/');
                int splitCount = strs.Length - 1;
                //if (splitCount <= 1)
                {
                    if (splitCount == 0)//第一级cache键
                    {
                        if (!list.ContainsKey(key))
                        {
                            list.Add(key, 0);
                        }
                    }
                    else//第二级cache键，用于给第一级提供计数
                    {
                        key = strs[0];
                        if (!list.ContainsKey(key))
                        {
                            list.Add(key, 1);
                        }
                        else
                        {
                            list[key] = list[key]++;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取当前指定缓存键的缓存项列表
        /// </summary>
        /// <param name="cache">缓存对象</param>
        /// <param name="cacheKey">缓存键</param>
        /// <returns>缓存项列表</returns>
        public static IList<string> GetCacheDetails(string cacheKeyGroup)
        {
            return doCacheItemOperate(cacheKeyGroup, "query");
        }

        /// <summary>
        /// 单个操作指定cacheKey的缓存项
        /// </summary>
        /// <param name="cache">缓存对象</param>
        /// <param name="cacheKey"></param>
        private static IList<string> doCacheItemOperate(string cacheKey, string operate)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            List<string> findKeys = new List<string>();
            foreach (System.Collections.DictionaryEntry cacheItem in cache)
            {
                //如果找到以当前cachekey开头，则标示为要移除cache项
                if (cacheItem.Key.ToString().StartsWith(cacheKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    findKeys.Add(cacheItem.Key.ToString());
                }
            }
            if (operate == "delete")
            {
                //移除cacheItem
                foreach (string key in findKeys)
                {
                    cache.Remove(key);
                }
            }
            return findKeys;
        }

        /// <summary>
        /// 移除BizCacheItemEnum对应的所有缓存
        /// </summary>
        /// <param name="cache">缓存对象</param>
        public static void RemoveAllCaches()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            List<string> findKeys = new List<string>();
            foreach (System.Collections.DictionaryEntry cacheItem in cache)
            {
                findKeys.Add(cacheItem.Key.ToString());
            }
            //移除cacheItem
            foreach (string key in findKeys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// 单个移除指定cacheKey的缓存项
        /// </summary>
        /// <param name="cache">缓存对象</param>
        /// <param name="cacheKey"></param>
        public static IList<string> RemoveCacheItem(string cacheKey)
        {
            return doCacheItemOperate(cacheKey, "delete");
        }


        #endregion
    }
}
