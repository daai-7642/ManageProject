using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCG.Utility.RedisCache
{
    public static class CacheValidate
    {
        /// <summary>
        /// 验证新缓存与旧缓存是否相同
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool CacheDataValidate(object newObj, object oldObj)
        {
            try
            {
                if (newObj.Equals(oldObj))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }








            //var operateType = newObj.GetType();
            //if(operateType==typeof(string))
            //{
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
