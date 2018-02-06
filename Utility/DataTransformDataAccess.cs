using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class DataTransFormDataAccess
    {
        public static IList<T> EntityToEntity<T>(List<object> o) where T : new()
        {
            List<T> tlist = new List<T>();
            T t = new T();
            PropertyInfo[] fieldsO = o.GetType().GetProperties();
            PropertyInfo[] fieldsT = typeof(T).GetProperties();
            foreach (var item in o)
            {
                foreach (var oitem in item.GetType().GetProperties())
                {
                    PropertyInfo propertyInfo = fieldsT.Where(a => a.Name == oitem.Name).First();
                    propertyInfo.SetValue(t, oitem.GetValue(propertyInfo.Name), null);
                }
                tlist.Add(t);
            }
            //foreach (var item in fieldsO)
            //{
            //    PropertyInfo t = fieldsT.Where(a => a.Name == item.Name).First();
            //    t.SetValue("","",null);
            //}
            return tlist;
        }

        public static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        /// <summary>
        /// 实体属性反射
        /// </summary>
        /// <typeparam name="S">赋值对象</typeparam>
        /// <typeparam name="T">被赋值对象</typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public static void AutoMapping<S, T>(S s, T t)
        {
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            Type target = t.GetType();

            foreach (var pp in pps)
            {
                PropertyInfo targetPP = target.GetProperty(pp.Name);
                object value = pp.GetValue(s, null);

                if (targetPP != null && value != null)
                {
                    targetPP.SetValue(t, value, null);
                }
            }
        }
    }
}
