using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility.Cache
{
    /// <summary>
    /// 缓存基类（默认存储Session中）
    /// </summary>
    public class BaseCache : IDisposable
    {
        protected string def_ip = string.Empty;
        protected int def_port = 0;
        protected string def_password = string.Empty;
        protected string CacheKey = "SeesionKey";

        public BaseCache()
        {

        }
        
        public virtual void InitCache(bool isReadAndWriter = true, string ip = "", int port = 0, string password = "")
        {

        }

        public virtual bool SetCache<T>(string key, T t, int timeOutMinute = 10, bool isSerilize = false) where T : class, new()
        {
            var isfalse = false;

            try
            {
                key = key ?? CacheKey;
                if (t == null) { return isfalse; }

                var session_json = JsonConvert.SerializeObject(t);
                HttpContext.Current.Session.Timeout = timeOutMinute;
                HttpContext.Current.Session.Add(key, session_json);
                isfalse = true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return isfalse;
        }

        public virtual T GetCache<T>(string key = null, bool isSerilize = false) where T : class, new()
        {
            var t = default(T);

            try
            {

                key = key ?? CacheKey;
                var session = HttpContext.Current.Session[key];
                if (session == null) { return t; }

                t = JsonConvert.DeserializeObject<T>(session.ToString());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return t;
        }

        public virtual bool Remove(string key = null)
        {
            var isfalse = false;

            try
            {
                key = key ?? CacheKey;
                HttpContext.Current.Session.Remove(key);
                isfalse = true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return isfalse;
        }

        /// <summary>
        /// 增加缓存时间
        /// </summary>
        /// <returns></returns>
        public virtual bool AddExpire(string key, int nTimeMinute = 10)
        {
            return true;
        }

        public virtual bool FlushAll()
        {

            return false;
        }

        public virtual bool Any(string key)
        {

            return false;
        }

        public virtual bool SetHashCache<T>(string hashId, string key, T t, int nTimeMinute = 10) where T : class, new()
        {

            return false;
        }

        public virtual List<string> GetHashKeys(string hashId)
        {

            return null;
        }

        public virtual List<string> GetHashValues(string hashId)
        {

            return null;
        }

        public virtual T GetHashValue<T>(string hashId, string key) where T : class, new()
        {
            var t = default(T);
            return t;
        }

        public virtual bool RemoveHashByKey(string hashId, string key)
        {

            return false;
        }


        public virtual void Dispose(bool isfalse)
        {

            if (isfalse)
            {


            }
        }

        //手动释放
        public void Dispose()
        {

            this.Dispose(true);
            //不自动释放
            GC.SuppressFinalize(this);
        }
    }

}
