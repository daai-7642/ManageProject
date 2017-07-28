using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCG.Utility.RedisCache
{
    public class RedisCacheProvider : ICache
    {
        /// <summary>
        /// 使用Redis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            try
            {
                return RedisHelper.Get<T>(key);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public void Insert(string key, object instance, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            if (absoluteExpiration.HasValue)
            {
                RedisHelper.Set(key, instance, absoluteExpiration.Value);
                return;
            }
            if (slidingExpiration.HasValue)
            {
                RedisHelper.Set(key, instance, slidingExpiration.Value);
                return;
            }
            RedisHelper.Set(key, instance);
        }
        public void Insert(string key, object instance)
        {
            RedisHelper.Set(key, instance);
        }
        public void Insert(string key, object instance, double seconds)
        {
            RedisHelper.Set(key, instance, TimeSpan.FromSeconds(seconds));
        }

        public void Set(string key, object instance, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            Insert(key, instance, absoluteExpiration, slidingExpiration);
        }

        public void Set(string key, object instance)
        {
            Insert(key, instance);
        }

        public void Set(string key, object instance, double seconds)
        {
            Insert(key, instance, seconds);
        }

        public void Remove(string key)
        {
            RedisHelper.Remove(key);
        }
        public void Clear()
        {
            RedisHelper.FlushAll();
        }

        public void Remove(string[] keys)
        {
            RedisHelper.Remove(keys);
        }

        public void RemoveByPattern(string key)
        {
            RedisHelper.RemoveByPattern(key);
        }
        //
        public void Update(string key, object instance, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            if (absoluteExpiration.HasValue)
            {
                RedisHelper.Set(key, instance, absoluteExpiration.Value);
            }
            if (slidingExpiration.HasValue)
            {
                RedisHelper.Set(key, instance, slidingExpiration.Value);
            }
            RedisHelper.Set(key, instance);
        }
        public void Update(string key, object instance)
        {
            RedisHelper.Set(key, instance);
        }
        public void Update(string key, object instance, double seconds)
        {
            RedisHelper.Set(key, instance, TimeSpan.FromSeconds(seconds));
        }
    }
}
