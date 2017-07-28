using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCG.Utility.RedisCache
{
    public static class CacheHelper
    {
        private static readonly ICache cache = new RedisCacheProvider();

        public static T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        public static T GetOrAdd<T>(string key, Func<T> get, int expireSeconds) where T : class
        {
            var entity = cache.Get<T>(key) ?? get();
            if (entity != null)
            {
                cache.Insert(key, entity, expireSeconds);
            }
            return entity;
        }
        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static void Insert(string key, object value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            cache.Insert(key, value, absoluteExpiration, slidingExpiration);
        }

        public static void Insert(string key, object instance)
        {
            cache.Insert(key, instance);
        }

        public static void Insert(string key, object instance, double seconds)
        {
            cache.Insert(key, instance, seconds);
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static bool Update(string key, object instance, DateTime? absoluteExpiration, TimeSpan? slidingExpiration, object historyInstance)
        {
            bool result;
            if (CacheValidate.CacheDataValidate(instance, historyInstance))
            {
                cache.Update(key, instance, absoluteExpiration, slidingExpiration);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        public static bool Update(string key, object instance, object historyInstance)
        {
            bool result;
            if (CacheValidate.CacheDataValidate(instance, historyInstance))
            {
                cache.Update(key, instance);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        /// <param name="seconds"></param>
        public static bool Update(string key, object instance, double seconds, object historyInstance)
        {
            bool result;
            if (CacheValidate.CacheDataValidate(instance, historyInstance))
            {
                cache.Update(key, instance, seconds);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public static void Remove(string key)
        {
            cache.Remove(key);
        }
        public static void RemoveByPattern(string key)
        {
            cache.RemoveByPattern(key);
        }
        public static void Remove(params string[] keys)
        {
            cache.Remove(keys);
        }
        public static void Clear()
        {
            cache.Clear();
        }

        public static IDisposable DualRemove(params string[] keys)
        {
            return new DualRemoveWrapper(keys);
        }

        public static IDisposable DualRemoveParttern(string key)
        {
            return new DualRemovePartternWrapper(key);
        }

        private class DualRemoveWrapper : IDisposable
        {
            private readonly string[] keys;
            public DualRemoveWrapper(params string[] keys)
            {
                this.keys = keys;
                CacheHelper.Remove(keys);
            }
            public void Dispose()
            {
                CacheHelper.Remove(keys);
            }
        }

        private class DualRemovePartternWrapper : IDisposable
        {
            private readonly string key;
            public DualRemovePartternWrapper(string key)
            {
                this.key = key;
                CacheHelper.RemoveByPattern(key);
            }
            public void Dispose()
            {
                CacheHelper.RemoveByPattern(key);
            }
        }
    }
}
