using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Redis.Support.Locking;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FMCG.Utility.RedisCache
{
    public static class RedisHelper
    {
        private static IRedisClientsManager clientManager;

        static RedisHelper()
        {
            // 读取配置文件
             var file = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Redis.config";
            //var file = System.Web.HttpContext.Current.Server.MapPath("~/Redis.config");

            var doc = XDocument.Load(file);
            var pooled = doc.Descendants("Pooled");
            var config = pooled.Elements("clientManagerConfig").FirstOrDefault();
            var writehosts = pooled.Elements("writehosts").Elements("client");
            var readhosts = pooled.Elements("readhosts").Elements("client");

            var rhost = GetHost(readhosts);
            var whost = GetHost(writehosts);
            int maxReadPoolSize = Convert.ToInt32(config.Attribute("MaxReadPoolSize").Value);
            int maxWritePoolSize = Convert.ToInt32(config.Attribute("MaxWritePoolSize").Value);
            bool autoStart = Convert.ToBoolean(config.Attribute("AutoStart").Value);
            //redis = new RedisClient(ip, port, password);

            clientManager = new PooledRedisClientManager(
                whost,
                rhost,
                new RedisClientManagerConfig()
                {
                    MaxReadPoolSize = maxReadPoolSize,
                    MaxWritePoolSize = maxWritePoolSize,
                    AutoStart = autoStart
                });
        }

        /// <summary>
        /// 设置键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="t">value</param>
        public static void Set<T>(string key, T t)
        {
            using (var client = clientManager.GetClient())
            {
                client.Set(key, t);
            }
        }
        /// <summary>
        /// 设置多个键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        public static void Set<T>(IDictionary<string, T> map)
        {
            using (var client = clientManager.GetClient())
            {
                client.SetAll(map);
            }
        }
        /// <summary>
        /// 设置键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="time"></param>
        public static void Set<T>(string key, T t, DateTime time)
        {
            using (var client = clientManager.GetClient())
            {
                client.Set(key, t, time);
            }
        }

        /// <summary>
        /// 设置键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="time"></param>
        public static void Set<T>(string key, T t, TimeSpan time)
        {
            using (var client = clientManager.GetClient())
            {
                client.Set(key, t, time);
            }
        }

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            using (var client = clientManager.GetClient())
            {
                return client.Get<T>(key);
            }
        }

        /// <summary>
        /// 移除键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            using (var client = clientManager.GetClient())
            {
                return client.Remove(key);
            }
        }
        public static bool Remove(string[] keys)
        {
            using (var client = clientManager.GetClient())
            {
                return client.RemoveEntry(keys);
            }
        }
        public static long IncrementValue(string key, int increment)
        {
            using (var client = clientManager.GetClient())
            {
                return client.IncrementValueBy(key, increment);
            }
        }

        public static long IncrementWithTime(string key, int increment, int expireSeconds)
        {
            var lua = @" local num=redis.call('INCRBY',KEYS[1],ARGV[1]);
                         if(num==tonumber(ARGV[1])) then redis.call('EXPIRE',KEYS[1],ARGV[2]) end;
                         return num;";
            using (var client = clientManager.GetClient())
            {
                return client.ExecLuaAsInt(lua, new[] { key }, new[] { increment.ToString(), expireSeconds.ToString() });
            }
        }

        public static long IncrementValueIfExists(string key, int increment)
        {
            var lua = @" local flag=redis.call('EXISTS',KEYS[1]);
                         if flag==1 then return redis.call('INCRBY',KEYS[1],ARGV[1]) end;
                         return -1;";
            using (var client = clientManager.GetClient())
            {
                return client.ExecLuaAsInt(lua, new[] { key }, new[] { increment.ToString() });
            }
        }
        public static long IncrementOrSetValue(string key, int defaultVal, int increment)
        {
            var lua = @" local flag=redis.call('EXISTS',KEYS[1]);
                         if flag==1 then return redis.call('INCRBY',KEYS[1],ARGV[1]) else redis.call('SET',KEYS[1],ARGV[2]) end;
                         return ARGV[2];";
            using (var client = clientManager.GetClient())
            {
                return client.ExecLuaAsInt(lua, new[] { key }, new[] { increment.ToString(), defaultVal.ToString() });
            }
        }
        //
        #region 哈希操作
        /// <summary>
        /// 获取散列的键值
        /// </summary>
        /// <param name="hashId">散列编号</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetValueFromHash(string hashId, string key)
        {
            using (var client = clientManager.GetClient())
            {
                return client.GetValueFromHash(hashId, key);
            }
        }

        /// <summary>
        /// 设置散列的键值
        /// </summary>
        /// <param name="hashId">散列编号</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static bool SetValueToHash(string hashId, string key, string value)
        {
            using (var client = clientManager.GetClient())
            {
                return client.SetEntryInHash(hashId, key, value);
            }
        }
        /// <summary>
        /// 对散列的值进行增量
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public static long IncrementValueInHash(string hashId, string key, int increment)
        {
            using (var client = clientManager.GetClient())
            {
                return client.IncrementValueInHash(hashId, key, increment);
            }
        }
        /// <summary>
        /// 获取哈希表多个值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static List<string> GetHashValuesByKeys(string hashId, params string[] keys)
        {
            using (var client = clientManager.GetClient())
            {
                return client.GetValuesFromHash(hashId, keys);
            }
        }
        #endregion

        #region Redis集合及列表操作
        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="t">value</param>
        public static void Enqueue<T>(string key, T t)
        {
            using (var client = clientManager.GetClient())
            {
                var model = t.ToJson();
                client.EnqueueItemOnList(key, model);
            }
        }
        /// <summary>
        /// 出列
        /// </summary>
        /// <param name="key">key</param>
        public static T Dequeue<T>(string key)
        {
            using (var client = clientManager.GetClient())
            {
                var result = client.DequeueItemFromList(key);
                var seria = new JsonSerializer<T>();
                return seria.DeserializeFromString(result);
            }
        }

        /// <summary>
        /// 出列
        /// </summary>
        /// <param name="keys">key</param>
        /// <param name="timeOut">超时时间</param>
        public static T DequeueBlocking<T>(string[] keys, TimeSpan? timeOut)
        {
            using (var client = clientManager.GetClient())
            {
                var result = client.BlockingDequeueItemFromLists(keys, timeOut);
                var seria = new JsonSerializer<T>();
                return seria.DeserializeFromString(result.Item);
            }
        }

        /// <summary>
        /// 获取列表里面值得数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetListCount(string key)
        {
            using (var client = clientManager.GetClient())
            {
                return client.GetListCount(key);
            }
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="key">key</param>
        public static void RemoveAllFromList(string key)
        {
            using (var client = clientManager.GetClient())
            {
                client.RemoveAllFromList(key);
            }
        }
        #endregion

        #region Set集合操作

        /// <summary>
        /// 添加数据到集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        public static void AddItemToSet(string setId, string item)
        {
            using (var client = clientManager.GetClient())
            {
                client.AddItemToSet(setId, item);
            }
        }

        /// <summary>
        /// 从集合中移除数据
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        public static void RemoveItemFromSet(string setId, string item)
        {
            using (var client = clientManager.GetClient())
            {
                client.RemoveItemFromSet(setId, item);
            }
        }

        /// <summary>
        /// 判断集合中是否包含数据
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        public static bool SetContainsItem(string setId, string item)
        {
            using (var client = clientManager.GetClient())
            {
                return client.SetContainsItem(setId, item);
            }
        }

        public static HashSet<string> GetItemFromSet(string setId)
        {
            using (var client = clientManager.GetClient())
            {
                return client.GetAllItemsFromSet(setId);
            }
        }
        #endregion

        public static T WithDistributedLock<T>(string key, int timeout, Func<string, T> excute)
        {
            var lockKey = "lock_" + key;
            using (var client = clientManager.GetClient())
            {
                using (new DisposableDistributedLock(client, lockKey, timeout, timeout))
                {
                    return excute(key);
                }
            }
        }


        private static List<string> GetHost(IEnumerable<XElement> elements)
        {
            return (from writehost in elements let port = writehost.Attribute("port").Value let host = writehost.Attribute("host").Value select string.Format("{0}:{1}", host, port)).ToList();
        }

        public static void FlushAll()
        {
            using (var client = clientManager.GetCacheClient())
            {
                client.FlushAll();
            }
        }

        public static long RemoveByPattern(string key)
        {
            using (var client = clientManager.GetClient())
            {
                var lua = @"
                            local rmkeys=redis.call('keys',KEYS[1])
                            local count=0
                            if rmkeys then 
                                for _,k in ipairs(rmkeys) do 
                                    redis.call('del', k) 
                                    count=count+1
                                end                                
                            end
                            return count";
                var n = client.ExecLuaAsInt(lua, new[] { key + "*" }, new string[0]);
                return n;
            }
        }

        public static Dictionary<string, T> Get<T>(List<string> keys)
        {
            using (var client = clientManager.GetClient())
            {
                return client.GetValuesMap<T>(keys);
            }
        }
    }
}
