using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCG.Utility.RedisCache
{
    public interface ICache
    {
        T Get<T>(string key);
        void Insert(string key, object value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration);
        void Insert(string key, object instance);
        void Insert(string key, object instance, double seconds);
        void Set(string key, object value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration);
        void Set(string key, object instance);
        void Set(string key, object instance, double seconds);
        void Remove(string key);
        void Clear();
        void Remove(string[] keys);
        void RemoveByPattern(string key);
        //修改
        void Update(string key, object value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration);
        void Update(string key, object instance);
        void Update(string key, object instance, double seconds);
    }
}
