using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Cache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : BaseCache
    {
        public IRedisClient redis = null;

        public RedisCache()
        {

            //这里去读取默认配置文件数据
            def_ip = ConfigurationManager.AppSettings["Redis_IP"];
            def_port =int.Parse(ConfigurationManager.AppSettings["Redis_Port"]) ;
            def_password = ConfigurationManager.AppSettings["Redis_PassWord"]; ;
        }

        #region Redis缓存

        public static object _lockCache = new object();
        public override void InitCache(bool isReadAndWriter = true, string ip = "", int port = 0, string password = "")
        {

            if (redis == null)
            {
                ip = string.IsNullOrEmpty(ip) ? def_ip : ip;
                port = port == 0 ? def_port : port;
                password = string.IsNullOrEmpty(password) ? def_password : password;

                //单个redis服务
                //redis = new RedisClient(ip, port, password);

                //集群服务 如果密码，格式如：pwd@ip:port
                var readAndWritePorts = new List<string> { "shenniubuxing3@127.0.0.1:6379" };
                var onlyReadPorts = new List<string> {
                    "shenniubuxing3@127.0.0.1:6378",
                    "shenniubuxing3@127.0.0.1:6377"
                };

                var redisPool = new PooledRedisClientManager(
                    readAndWritePorts,
                    onlyReadPorts,
                    new RedisClientManagerConfig
                    {
                        AutoStart = true,
                        //最大读取链接
                        MaxReadPoolSize = 20,
                        //最大写入链接
                        MaxWritePoolSize = 10
                    })
                {
                    //每个链接超时时间
                    ConnectTimeout = 20,
                    //连接池超时时间
                    PoolTimeout = 60
                };

                lock (_lockCache)
                {
                    redis = isReadAndWriter ? redisPool.GetClient() : redisPool.GetReadOnlyClient();
                }
            }
        }

        public override bool AddExpire(string key, int nTimeMinute = 10)
        {
            var isfalse = false;
            try
            {
                if (string.IsNullOrEmpty(key)) { return isfalse; }

                InitCache();
                //isfalse = redis.ExpireEntryIn(key, TimeSpan.FromMinutes(nTimeMinute));
                isfalse = redis.ExpireEntryAt(key, DateTime.Now.AddMinutes(nTimeMinute));
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return isfalse;
        }

        public override bool SetCache<T>(string key, T t, int timeOutMinute = 10, bool isSerilize = false)
        {

            var isfalse = false;

            try
            {
                if (string.IsNullOrEmpty(key)) { return isfalse; }

                InitCache();
                if (isSerilize)
                {
                    var data = JsonConvert.SerializeObject(t);
                    var bb = System.Text.Encoding.UTF8.GetBytes(data);
                    isfalse = redis.Set<byte[]>(key, bb, TimeSpan.FromMinutes(timeOutMinute));
                }
                else { isfalse = redis.Set<T>(key, t, TimeSpan.FromMinutes(timeOutMinute)); }
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return isfalse;
        }


        public override T GetCache<T>(string key, bool isSerilize = false)
        {
            var t = default(T);
            try
            {
                if (string.IsNullOrEmpty(key)) { return t; }

                InitCache(false);
                if (isSerilize)
                {

                    var bb = redis.Get<byte[]>(key);
                    if (bb.Length <= 0) { return t; }
                    var data = System.Text.Encoding.UTF8.GetString(bb);
                    t = JsonConvert.DeserializeObject<T>(data);
                }
                else { t = redis.Get<T>(key); }
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return t;
        }

        public override bool Remove(string key)
        {
            var isfalse = false;
            try
            {
                if (string.IsNullOrEmpty(key)) { return isfalse; }

                InitCache();
                isfalse = redis.Remove(key);
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return isfalse;
        }

        public override bool SetHashCache<T>(string hashId, string key, T t, int nTimeMinute = 10)
        {

            var isfalse = false;

            try
            {
                if (string.IsNullOrEmpty(hashId) || string.IsNullOrEmpty(key) || t == null) { return isfalse; }

                InitCache();

                var result = JsonConvert.SerializeObject(t);
                if (string.IsNullOrEmpty(result)) { return isfalse; }
                isfalse = redis.SetEntryInHash(hashId, key, result);
                if (isfalse) { AddExpire(key, nTimeMinute); }
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return isfalse;
        }

        public override List<string> GetHashKeys(string hashId)
        {
            var hashKeys = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(hashId)) { return hashKeys; }

                InitCache();
                hashKeys = redis.GetHashKeys(hashId);

            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return hashKeys;
        }

        public override List<string> GetHashValues(string hashId)
        {
            var hashValues = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(hashId)) { return hashValues; }

                InitCache();
                hashValues = redis.GetHashValues(hashId);
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return hashValues;
        }

        public override T GetHashValue<T>(string hashId, string key)
        {
            var t = default(T);
            try
            {
                if (string.IsNullOrEmpty(hashId) || string.IsNullOrEmpty(key)) { return t; }

                InitCache();
                var result = redis.GetValueFromHash(hashId, key);
                if (string.IsNullOrEmpty(result)) { return t; }

                t = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return t;
        }

        public override bool RemoveHashByKey(string hashId, string key)
        {
            var isfalse = false;

            try
            {
                if (string.IsNullOrEmpty(hashId) || string.IsNullOrEmpty(key)) { return isfalse; }

                InitCache();
                isfalse = redis.RemoveEntryFromHash(hashId, key);
            }
            catch (Exception ex)
            {
            }
            finally { this.Dispose(); }
            return isfalse;
        }

        public override void Dispose(bool isfalse)
        {

            if (isfalse && redis != null)
            {

                redis.Dispose();
                redis = null;
            }
        }

        #endregion
    }
}
